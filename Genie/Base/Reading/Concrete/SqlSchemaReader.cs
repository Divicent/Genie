using System;
using System.Collections.Generic;
using System.Linq;
using Genie.Base.Configuration.Abstract;
using Genie.Base.Exceptions;
using Genie.Base.ProcessOutput.Abstract;
using Genie.Base.Reading.Abstract;
using Genie.Base.Reading.Concrete.Models;
using Genie.Extensions;
using Genie.Models.Abstract;
using Genie.Models.Concrete;
using Genie.Tools;
using Enum = Genie.Models.Concrete.Enum;
using Attribute = Genie.Models.Concrete.Attribute;
using System.Data;

namespace Genie.Base.Reading.Concrete
{
    /// <summary>
    /// Base class for Sql schema reader implementations
    /// Written while listening to Kasun Kalhara and Indrachapa :)
    /// </summary>
    internal abstract class SqlSchemaReader
    {
        protected string _queryToReadColumns = "";
        protected string _queryToGetParameters = "";
        protected string _queryToGetExtendedProperties = "";
        public IDatabaseSchema Read(IConfiguration configuration, IProcessOutput output)
        {
            output.WriteInformation("Reading database meta data.");
            Setup(configuration);
            DatabaseSchema schema;
            try
            {
                schema = ReadDatabase(configuration.ConnectionString, configuration);
                schema.BaseNamespace = configuration.BaseNamespace;
            }
            catch (Exception e)
            {
                throw new GenieException("Unable to read database meta data", e);
            }

            output.WriteSuccess("Schema reading successful.");

            ProcessRelationships(schema.Relations, output);

            if (configuration.Enums != null && configuration.Enums.Count > 0)
            {
                foreach (var e in configuration.Enums)
                    if (schema.Relations.All(r => r.Name != e.Table))
                        throw new GenieException($"{e.Table} is not a table. (Enums)");
                schema.Enums = ReadEnums(configuration.ConnectionString, configuration.Enums, output, configuration);
            }
            else
            {
                schema.Enums = new List<IEnum>();
            }

            return schema;
        }

        private DatabaseSchema ReadDatabase(string connectionString, IConfiguration configuration)
        {
            var databaseSchemaColumns = new List<DatabaseSchemaColumn>();
            var databaseParameters = new List<DatabaseParameter>();
            var databaseExtendedProperties = new List<ExtendedPropertyInfo>();

            using (var connection = GetConnection(connectionString))
            {
                connection.Open();
                var transaction = connection.BeginTransaction();

                var commandToGetColumns = GetCommand(_queryToReadColumns, connection, transaction);

                using (var reader = commandToGetColumns.ExecuteReader())
                {
                    while (reader.Read())
                    {
                        databaseSchemaColumns.Add(ReadColumn(reader));
                    }

                    var filtered = new List<DatabaseSchemaColumn>();
                    foreach (var databaseSchemaColumn in databaseSchemaColumns.Where(
                        databaseSchemaColumn => !filtered.Any(
                            f =>
                                f.TableName == databaseSchemaColumn.TableName && f.Name == databaseSchemaColumn.Name)))
                        filtered.Add(databaseSchemaColumn);

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

                var commandToGetParameters = GetCommand(
                    _queryToGetParameters, connection, transaction);

                using (var reader = commandToGetParameters.ExecuteReader())
                {
                    while (reader.Read())
                    {
                        databaseParameters.Add(ReadParameter(reader));
                    }
                }

                if (!string.IsNullOrWhiteSpace(_queryToGetExtendedProperties))
                {
                    var commandToGetExtendedProperties = GetCommand(_queryToGetExtendedProperties, connection, transaction);
                    using (var reader = commandToGetExtendedProperties.ExecuteReader())
                    {
                        while (reader.Read())
                        {
                            databaseExtendedProperties.Add(ReadExtendedProperty(reader));
                        }
                    }
                }

                transaction.Commit();
                connection.Close();
            }

            return Process(databaseSchemaColumns, databaseParameters, databaseExtendedProperties);
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
                    foreach (var foreignKeyAttribute in relation.ForeignKeyAttributes)
                    {
                        var referencingRelation =
                            relations.FirstOrDefault(r => r.Name == foreignKeyAttribute.ReferencingRelationName);
                        referencingRelation?.ReferenceLists.Add(new ReferenceList
                        {
                            ReferencedPropertyName = foreignKeyAttribute.ReferencingNonForeignKeyAttribute.Name,
                            ReferencedPropertyOnThisRelation = foreignKeyAttribute.ReferencingTableColumnName,
                            ReferencedRelationName = relation.Name
                        });
                    }
            }
            catch (Exception e)
            {
                throw new GenieException("Unable to process relationships of the tables", e);
            }
            output.WriteSuccess("Relationships processed.");
        }



        private static DatabaseSchema Process(IReadOnlyCollection<DatabaseSchemaColumn> columns,
          IReadOnlyCollection<DatabaseParameter> parameters, IReadOnlyCollection<ExtendedPropertyInfo> extendedProperties)
        {
            if (columns == null || columns.Count < 1)
                return null;

            var tables = new List<IRelation>();
            var views = new List<IView>();
            var storedProcedures = new List<IStoredProcedure>();

            foreach (var databaseSchemaColumn in columns)
            {
                string dataType;
                bool lit;
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
                                ReferenceLists = new List<IReferenceList>(),
                                Comment = databaseSchemaColumn.TableComment
                            };

                            tables.Add(table);
                        }
                        dataType = CommonTools.GetCSharpDataType(databaseSchemaColumn.DataType,
                            databaseSchemaColumn.Nullable);
                        lit = dataType == "string" || dataType.StartsWith("DateTime");

                        var attribute = new Attribute
                        {
                            IsLiteralType = lit,
                            IsKey = databaseSchemaColumn.IsPrimaryKey,
                            Name = databaseSchemaColumn.Name,
                            DataType = dataType,
                            FieldName = "_" + (databaseSchemaColumn.Name.First() + "").ToLower() +
                                        databaseSchemaColumn.Name.Substring(1),
                            Comment = databaseSchemaColumn.Comment,
                            IsIdentity = databaseSchemaColumn.IsIdentity
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

                        var extendedProperty = extendedProperties.FirstOrDefault(e => e.ObjectName == table.Name && e.ColumnName == attribute.Name);
                        if (extendedProperty != null)
                        {
                            attribute.Comment = extendedProperty.Property;
                        }

                        table.Attributes.Add(attribute);
                        break;
                    case "VIEW":
                        var view = views.FirstOrDefault(t => t.Name == databaseSchemaColumn.TableName);
                        if (view == null)
                        {
                            view = new View
                            {
                                Comment = databaseSchemaColumn.TableComment,
                                FieldName =
                                    "_" + (databaseSchemaColumn.TableName.First() + "").ToLower() +
                                    databaseSchemaColumn.TableName.Substring(1),
                                Name = databaseSchemaColumn.TableName,
                                Attributes = new List<ISimpleAttribute>()
                            };

                            views.Add(view);
                        }

                        dataType = CommonTools.GetCSharpDataType(databaseSchemaColumn.DataType,
                            databaseSchemaColumn.Nullable);
                        lit = dataType == "string" || dataType.StartsWith("DateTime");


                        var attr = new SimpleAttribute
                        {
                            Name = databaseSchemaColumn.Name,
                            IsLiteralType = lit,
                            FieldName = "_" + (databaseSchemaColumn.Name.First() + "").ToLower() +
                                        databaseSchemaColumn.Name.Substring(1),
                            DataType = dataType,
                            Comment = databaseSchemaColumn.Comment
                        };

                        var extendedProp = extendedProperties.FirstOrDefault(e => e.ObjectName == view.Name && e.ColumnName == attr.Name);
                        if (extendedProp != null)
                        {
                            attr.Comment = extendedProp.Property;
                        }

                        view.Attributes.Add(attr);
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
                        procedure = new StoredProcedure
                        {
                            Name = parameter.Procedure,
                            Parameters = new List<ProcedureParameter>()
                        };
                        storedProcedures.Add(procedure);
                    }

                    procedure.Parameters.Add(
                        new ProcedureParameter { DataType = parameter.DataType, Name = parameter.Name });
                }

                foreach (var storedProcedure in storedProcedures)
                {
                    var parameterString = storedProcedure.Parameters.Aggregate("", (current, param) => current +
                                                                                                       $"{CommonTools.GetCSharpDataType(param.DataType, true)} {param.Name.Replace("@", "")} = null" +
                                                                                                       ",");
                    var parameterPassString = storedProcedure.Parameters.Aggregate("", (current, param) => current +
                                                                                                           $"{param.Name} = \"+({param.Name.Replace("@", "")} == null ? \"NULL\" : \"'\" + {param.Name.Replace("@", "")} + \"'\")+\"" +
                                                                                                           ",");

                    storedProcedure.ParamString = parameterString.TrimEnd(',');
                    storedProcedure.PassString = parameterPassString.TrimEnd(',');
                }
            }

            return new DatabaseSchema { Procedures = storedProcedures, Relations = tables, Views = views };
        }

        private List<IEnum> ReadEnums(string connectionString, IEnumerable<IConfigurationEnumTable> enumTables,
            IProcessOutput output, IConfiguration configuration)
        {
            output.WriteInformation("Reading enum tables.");
            using (var connection = GetConnection(connectionString))
            {
                connection.Open();
                var enums = new List<IEnum>();
                foreach (var configurationEnumTable in enumTables)
                {
                    var query = GetEnumValueQuery(configuration, configurationEnumTable);

                    var type = configurationEnumTable.Type;
                    var values = new List<IEnumValue>();

                    using (var command = GetCommand(query, connection, null))
                    {
                        using (var reader = command.ExecuteReader())
                        {
                            while (reader.Read())
                            {
                                string name;
                                try
                                {
                                    name = reader.GetString(0);
                                    if (string.IsNullOrWhiteSpace(name))
                                        throw new GenieException("Name should not be empty of an enum");
                                }
                                catch (Exception)
                                {
                                    throw new GenieException(
                                        $"Cannot read Name as string. in the enum table {configurationEnumTable.Table}");
                                }

                                object value = null;
                                try
                                {
                                    switch (type)
                                    {
                                        case "string":
                                            value = $"\"{reader.GetString(1)}\"";
                                            break;
                                        case "int":
                                            value = reader.GetInt32(1);
                                            break;
                                        case "double":
                                            value = reader.GetDouble(1);
                                            break;
                                        case "bool":
                                            value = reader.GetBoolean(1);
                                            break;
                                    }
                                }
                                catch (Exception e)
                                {
                                    throw new GenieException("Unable to read enum table using specified value type.", e);
                                }
                                name = name.Replace(" ", "_");
                                values.Add(new EnumValue { Name = name, FieldName = name.ToFieldName(), Value = value });
                            }

                            enums.Add(new Enum { Name = configurationEnumTable.Table + "Enum", Values = values, Type = type });
                        }
                    }
                }

                connection.Close();

                return enums;
            }
        }

        internal abstract void Setup(IConfiguration configuration);
        protected abstract IDbConnection GetConnection(string connectionString);
        protected abstract IDbCommand GetCommand(string query, IDbConnection connection, IDbTransaction transaction);
        protected abstract DatabaseSchemaColumn ReadColumn(IDataReader reader);
        protected abstract DatabaseParameter ReadParameter(IDataReader reader);
        protected abstract ExtendedPropertyInfo ReadExtendedProperty(IDataReader reader);
        protected abstract string GetEnumValueQuery(IConfiguration configuration, IConfigurationEnumTable  enumTable);
    }
}