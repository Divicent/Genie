using System;
using System.Collections.Generic;
using System.Data.SqlClient;
using System.Linq;
using Genie.Base.Abstract;
using Genie.Models;
using Genie.Models.Abstract;
using Genie.Tools;

namespace Genie.Base
{
    internal class DatabaseSchemaReader : IDatabaseSchemaReader
    {
        public IDatabaseSchema Read(IBasicConfiguration configuration, IProcessOutput output)
        {
            output.WriteInformation("Reading database meta data.");

            DatabaseSchema schema;
            try
            {
                schema = ReadDatabase(configuration.ConnectionString);
                schema.BaseNamespace = configuration.BaseNamespace;

            }
            catch (Exception e)
            {
                throw new Exception("Unable to read database meta data", e);
            }
            
            output.WriteSuccess("Schema reading successful.");

            ProcessRelationships(schema.Relations, output);

            return schema;
        }
        
        private static void ProcessRelationships(IReadOnlyCollection<IRelation> relations, IProcessOutput output)
        {
            output.WriteInformation("Processing relationships.");

            try
            {
                /*
                 * for each r in relationship
                 *      if r has a foreign key to any other table
                 *          get the table from list
                 *              add a list to that table
                 * 
                 */

                foreach (var relation in relations)
                {
                    foreach (var foreignKeyAttribute in relation.ForeignKeyAttributes)
                    {
                        var referencingRelation =
                            relations.FirstOrDefault(r => r.Name == foreignKeyAttribute.ReferencingRelationName);
                        referencingRelation?.ReferenceLists.Add(new ReferenceList
                        {
                            ReferencedPropertyName = foreignKeyAttribute.ReferencingNonForeignKeyAttribute.Name,
                            ReferencedPropertyOnThisRelation = foreignKeyAttribute.ReferencingTableColumnName,
                            ReferncedRelationName = relation.Name
                        });
                    }
                }
            }
            catch (Exception e)
            {
                throw new Exception("Unable to process relationships of the tables", e);
            }
            output.WriteSuccess("Relationships processed.");
        }




        private static DatabaseSchema ReadDatabase(string connectionString)
        {

            var databaseSchemaColumns = new List<DatabaseSchemaColumn>();
            var databaseParameters = new List<DatabaseParameter>();

            using (var connection = new SqlConnection(connectionString))
            {
                connection.Open();
                var transaction = connection.BeginTransaction();

                var commandToGetColumns = new SqlCommand(
                    @"
                SELECT 
	            c.COLUMN_NAME AS [Name]
               ,'[' + t.TABLE_SCHEMA + ']' + '.[' + t.TABLE_NAME + ']' AS [TableFullName]
               ,t.TABLE_Name [TableName]
               ,t.TABLE_TYPE  AS TableType
               ,c.IS_NULLABLE AS [Nullable]
               ,c.DATA_TYPE AS [DataType]
               ,CASE WHEN pkc.CONSTRAINT_NAME IS NULL THEN 0 ELSE 1 END AS IsPrimaryKey
               ,CASE WHEN fkc.CONSTRAINT_NAME IS NULL THEN 0 ELSE 1 END AS IsForeignKey
               ,rct.TABLE_NAME AS ReferencedTableName
               ,rcuc.COLUMN_NAME AS ReferencedColumn
            FROM INFORMATION_SCHEMA.COLUMNS c
	            INNER JOIN INFORMATION_SCHEMA.TABLES t
		            ON c.TABLE_NAME = t.TABLE_NAME
	            LEFT OUTER JOIN INFORMATION_SCHEMA.CONSTRAINT_COLUMN_USAGE ccu
		            ON c.TABLE_NAME = ccu.TABLE_NAME AND c.COLUMN_NAME = ccu.COLUMN_NAME
	            LEFT OUTER JOIN INFORMATION_SCHEMA.TABLE_CONSTRAINTS pkc
		            ON pkc.CONSTRAINT_TYPE = 'PRIMARY KEY' AND pkc.CONSTRAINT_NAME = ccu.CONSTRAINT_NAME
	            LEFT OUTER JOIN INFORMATION_SCHEMA.TABLE_CONSTRAINTS fkc
		            ON fkc.CONSTRAINT_TYPE = 'FOREIGN KEY' AND fkc.CONSTRAINT_NAME = ccu.CONSTRAINT_NAME
	            LEFT OUTER JOIN INFORMATION_SCHEMA.REFERENTIAL_CONSTRAINTS rc
		            ON fkc.CONSTRAINT_NAME = rc.CONSTRAINT_NAME
	            LEFT OUTER JOIN INFORMATION_SCHEMA.TABLE_CONSTRAINTS rct
		            ON rct.CONSTRAINT_NAME = rc.UNIQUE_CONSTRAINT_NAME
	            LEFT OUTER JOIN INFORMATION_SCHEMA.CONSTRAINT_COLUMN_USAGE rcuc
		            ON rc.UNIQUE_CONSTRAINT_NAME = rcuc.CONSTRAINT_NAME
            ORDER BY c.TABLE_NAME, c.ORDINAL_POSITION
            ", connection, transaction);

                using (var reader = commandToGetColumns.ExecuteReader())
                {
                    while (reader.HasRows && reader.Read())
                    {
                        var column = new DatabaseSchemaColumn
                        {
                            Name = reader.GetString(0),
                            TableFullName = reader.GetString(1),
                            TableName = reader.GetString(2),
                            Type = reader.GetString(3),
                            Nullable = reader.GetString(4) == "YES",
                            DataType = reader.GetString(5),
                            IsPrimaryKey = reader.GetInt32(6) == 1,
                            IsForeignKey = reader.GetInt32(7) == 1
                        };

                        if (column.IsForeignKey)
                        {
                            column.ReferencedTableName = reader.GetString(8);
                            column.ReferencedColumnName = reader.GetString(9);
                        }

                        databaseSchemaColumns.Add(column);
                    }

                    var filtered = new List<DatabaseSchemaColumn>();
                    foreach (var databaseSchemaColumn in databaseSchemaColumns.Where(databaseSchemaColumn => !filtered.Any(
                        f =>
                            f.TableName == databaseSchemaColumn.TableName && f.Name == databaseSchemaColumn.Name)))
                    {
                        filtered.Add(databaseSchemaColumn);
                    }

                    foreach (var databaseSchemaColumn in filtered)
                    {
                        var column = databaseSchemaColumn;
                        var nonFiltered =
                            databaseSchemaColumns.Where(
                                d =>
                                    d.TableName == column.TableName && d.Name == column.Name);

                        foreach (var schemaColumn in nonFiltered)
                        {
                            if (schemaColumn.IsForeignKey)
                            {
                                databaseSchemaColumn.IsForeignKey = true;
                                databaseSchemaColumn.ReferencedColumnName = schemaColumn.ReferencedColumnName;
                                databaseSchemaColumn.ReferencedTableName = schemaColumn.ReferencedTableName;
                            }

                            if (schemaColumn.IsPrimaryKey)
                                databaseSchemaColumn.IsPrimaryKey = true;
                        }
                    }

                    databaseSchemaColumns = filtered;
                }

                var commandToGetParameters = new SqlCommand(
                    @"
                    SELECT 
	                        p.SPECIFIC_NAME AS SP
	                    ,p.PARAMETER_NAME AS [Name]
	                    ,p.DATA_TYPE AS DataType
                    FROM INFORMATION_SCHEMA.PARAMETERS p
	                    INNER JOIN INFORMATION_SCHEMA.ROUTINES r
		                    ON p.SPECIFIC_NAME = r.SPECIFIC_NAME
                    WHERE r.ROUTINE_TYPE = 'PROCEDURE'
                    ORDER BY p.SCOPE_NAME , p.ORDINAL_POSITION", connection, transaction);

                using (var reader = commandToGetParameters.ExecuteReader()) 
                {
                    while (reader.HasRows && reader.Read())
                    {
                        databaseParameters.Add(new DatabaseParameter
                        {
                            Procedure =  reader.GetString(0),
                            Name = reader.GetString(1),
                            DataType = reader.GetString(2)
                        });           
                    }
                }

                transaction.Commit();
                connection.Close();
            }

            return Process(databaseSchemaColumns, databaseParameters);
        }

        private static DatabaseSchema Process(IReadOnlyCollection<DatabaseSchemaColumn> columns, IReadOnlyCollection<DatabaseParameter> parameters)
        {
            if (columns == null || columns.Count < 1)
                return null;

            var tables = new List<IRelation>();
            var views = new List<IView>();
            var storedProcedures = new List<IStoredProcedure>();

            foreach (var databaseSchemaColumn in columns)
            {
                switch (databaseSchemaColumn.Type)
                {
                    case "BASE TABLE":
                        var table = tables.FirstOrDefault(t => t.Name == databaseSchemaColumn.TableName);
                        if (table == null)
                        {
                            table = new Relation
                            {
                                Name = databaseSchemaColumn.TableName,
                                FieldName =
                                    "_" + (databaseSchemaColumn.TableName.First() + "").ToLower() +
                                    databaseSchemaColumn.TableName.Substring(1),
                                Attributes = new List<IAttribute>(),
                                ForeignKeyAttributes = new List<IForeignKeyAttribute>(),
                                ReferenceLists = new List<IReferenceList>()
                            };

                            tables.Add(table);
                        }

                        var attribute = new Models.Attribute
                        {
                            IsKey = databaseSchemaColumn.IsPrimaryKey,
                            Name = databaseSchemaColumn.Name,
                            DataType = CommonTools.GetCSharpDataType(databaseSchemaColumn.DataType, databaseSchemaColumn.Nullable),
                            FieldName = "_" + (databaseSchemaColumn.Name.First() + "").ToLower() + databaseSchemaColumn.Name.Substring(1)
                        };

                        if (databaseSchemaColumn.IsForeignKey)
                        {
                            var fkAttribute = new ForeignKeyAttribute
                            {
                                ReferencingNonForeignKeyAttribute = attribute,
                                ReferencingRelationName = databaseSchemaColumn.ReferencedTableName,
                                ReferencingTableColumnName = databaseSchemaColumn.ReferencedColumnName
                            };

                            attribute.RefPropName = attribute.FieldName + "Obj";
                            table.ForeignKeyAttributes.Add(fkAttribute);
                        }

                        table.Attributes.Add(attribute);
                        break;
                    case "VIEW":
                        var view = views.FirstOrDefault(t => t.Name == databaseSchemaColumn.TableName);
                        if (view == null)
                        {
                            view = new View
                            {
                                FieldName =
                                    "_" + (databaseSchemaColumn.TableName.First() + "").ToLower() +
                                    databaseSchemaColumn.TableName.Substring(1),
                                Name = databaseSchemaColumn.TableName,
                                Attributes = new List<ISimpleAttribute>()
                            };

                            views.Add(view);
                        }

                        view.Attributes.Add(new SimpleAttribute
                        {
                            Name = databaseSchemaColumn.Name,
                            FieldName = "_" + (databaseSchemaColumn.Name.First() + "").ToLower() + databaseSchemaColumn.Name.Substring(1),
                            DataType = CommonTools.GetCSharpDataType(databaseSchemaColumn.DataType, databaseSchemaColumn.Nullable)
                        });
                        break;
                }
            }

            if (parameters != null && parameters.Count > 0)
            {
                foreach (var parameter in parameters)
                {
                    var procedure = storedProcedures.FirstOrDefault(p => p.Name == parameter.Procedure);
                    if (procedure == null)
                    {
                        procedure = new StoredProcedure { Name = parameter.Procedure, Parameters = new List<ProcedureParameter>() };
                        storedProcedures.Add(procedure);
                    }

                    procedure.Parameters.Add(new ProcedureParameter {DataType = parameter.DataType, Name = parameter.Name});
                }

                foreach (var storedProcedure in storedProcedures)
                {
                    var parameterString = storedProcedure.Parameters.Aggregate("", (current, param) => current + string.Format("{0} {1}", CommonTools.GetCSharpDataType(param.DataType, false), param.Name.Replace("@", "")) + ",");
                    var parameterPassString = storedProcedure.Parameters.Aggregate("", (current, param) => current + string.Format("{1} = '\"+{0}+\"'", param.Name.Replace("@", ""), param.Name) + ",");

                    storedProcedure.ParamString = parameterString.TrimEnd(',');
                    storedProcedure.PassString = parameterPassString.TrimEnd(',');
                }
            }

            return new DatabaseSchema {Procedures = storedProcedures, Relations = tables, Views = views};
        }
    }
    

    internal class DatabaseSchemaColumn
    {
        public string Name { get; set; }
        public string TableFullName { get; set; }
        public string TableName { get; set; }
        public string Type { get; set; }
        public bool Nullable { get; set; }
        public string DataType { get; set; }
        public bool IsPrimaryKey { get; set; }
        public bool IsForeignKey { get; set; }
        public string ReferencedTableName { get; set; }
        public string ReferencedColumnName { get; set; }
    }

    internal class DatabaseParameter
    {
        public string Procedure { get; set; }
        public string Name { get; set; }
        public string DataType { get; set; }
    }
}