#region Usings

using System;
using System.Data;
using System.Data.SqlClient;
using System.Linq;
using Genie.Core.Base.Configuration.Abstract;
using Genie.Core.Base.Reading.Abstract;
using Genie.Core.Base.Reading.Concrete.Models;
using Genie.Core.Models.Abstract;
using Genie.Core.Tools;

#endregion

namespace Genie.Core.Base.Reading.Concrete
{
    internal class SqlServerSchemaReader : SqlSchemaReader, IDatabaseSchemaReader
    {
        protected override IDbCommand GetCommand(string query, IDbConnection connection, IDbTransaction transaction)
        {
            return new SqlCommand(query, connection as SqlConnection, transaction as SqlTransaction);
        }

        protected override IDbConnection GetConnection(string connectionString)
        {
            return new SqlConnection(connectionString);
        }

        protected override string GetEnumValueQuery(IConfiguration configuration,
            IConfigurationEnumTable configurationEnumTable)
        {
            return $"SELECT [{configurationEnumTable.NameColumn}] AS [Name]," +
                   $"       [{configurationEnumTable.ValueColumn}] AS [Value]" +
                   $" FROM [dbo].[{configurationEnumTable.Table}]";
        }

        protected override void ProcessProcedureParameters(IStoredProcedure storedProcedure)
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

        protected override DatabaseSchemaColumn ReadColumn(IDataReader reader)
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
                IsForeignKey = reader.GetInt32(7) == 1,
                IsIdentity = reader.GetInt32(8) == 1
            };

            if (column.IsForeignKey)
            {
                column.ReferencedTableName = reader.GetString(9);
                column.ReferencedColumnName = reader.GetString(10);
            }

            return column;
        }

        protected override ExtendedPropertyInfo ReadExtendedProperty(IDataReader reader)
        {
            return new ExtendedPropertyInfo
            {
                SchemaName = reader.GetString(0),
                ObjectName = reader.GetString(1),
                ColumnName = reader.GetString(2),
                Property = reader.GetString(3)
            };
        }

        protected override DatabaseParameter ReadParameter(IDataReader reader)
        {
            return new DatabaseParameter
            {
                Procedure = reader.GetString(0),
                Name = reader.GetString(1),
                DataType = reader.GetString(2)
            };
        }

        internal override void Setup(IConfiguration configuration)
        {
            QueryToReadColumns = $@"
                       SELECT 
	            c.COLUMN_NAME AS [Name]
               ,'[' + t.TABLE_SCHEMA + ']' + '.[' + t.TABLE_NAME + ']' AS [TableFullName]
               ,t.TABLE_Name [TableName]
               ,t.TABLE_TYPE  AS TableType
               ,c.IS_NULLABLE AS [Nullable]
               ,c.DATA_TYPE AS [DataType]
               ,CASE WHEN pkc.CONSTRAINT_NAME IS NULL THEN 0 ELSE 1 END AS IsPrimaryKey
               ,CASE WHEN fkc.CONSTRAINT_NAME IS NULL THEN 0 ELSE 1 END AS IsForeignKey
               ,ISNULL(COLUMNPROPERTY(object_id('[' + t.TABLE_SCHEMA + ']' + '.[' + t.TABLE_NAME + ']'), c.COLUMN_NAME, 'IsIdentity'), 0) AS IsIdentity
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
            WHERE t.TABLE_SCHEMA = '{configuration.Schema}'
            ORDER BY c.TABLE_NAME, c.ORDINAL_POSITION";


            QueryToGetParameters = $@"
                    SELECT 
                        p.SPECIFIC_NAME AS SP
	                    ,p.PARAMETER_NAME AS [Name]
	                    ,p.DATA_TYPE AS DataType
                    FROM INFORMATION_SCHEMA.PARAMETERS p
	                    INNER JOIN INFORMATION_SCHEMA.ROUTINES r
		                    ON p.SPECIFIC_NAME = r.SPECIFIC_NAME
                    WHERE r.ROUTINE_TYPE = 'PROCEDURE' AND p.SPECIFIC_SCHEMA = '{configuration.Schema}'
                    ORDER BY p.SCOPE_NAME , p.ORDINAL_POSITION";


            QueryToGetExtendedProperties = $@"SELECT
	                     s.[name] AS SchemaName
	                    ,oo.[name] AS ObjectName
	                    ,col.[name] AS ColumnName
	                    ,ep.[value] AS Property
                        FROM sys.all_objects oo 
                    INNER JOIN sys.extended_properties ep ON ep.major_id = oo.object_id 
                    LEFT JOIN sys.schemas s on oo.schema_id = s.schema_id
                    INNER JOIN sys.columns AS col ON ep.major_id = col.object_id AND ep.minor_id = col.column_id
                    WHERE s.[name] = '{configuration.Schema}' AND ep.[value] IS NOT NULL AND ep.[value] <> ''";
        }
    }
}