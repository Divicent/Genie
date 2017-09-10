using System.Data;
using Genie.Base.Configuration.Abstract;
using Genie.Base.Reading.Abstract;
using Genie.Base.Reading.Concrete.Models;
using MySql.Data.MySqlClient;

namespace Genie.Base.Reading.Concrete
{
    internal class MySqlSchemaReader : SqlSchemaReader, IDatabaseSchemaReader
    {
        protected override IDbCommand GetCommand(string query, IDbConnection connection, IDbTransaction transaction)
        {
            return new MySqlCommand(query, connection as MySqlConnection, transaction as MySqlTransaction);
        }

        protected override IDbConnection GetConnection(string connectionString)
        {
            return new MySqlConnection(connectionString);
        }

        protected override string GetEnumValueQuery(IConfiguration configuration,IConfigurationEnumTable configurationEnumTable)
        {
            return $"SELECT `{configurationEnumTable.NameColumn}` AS `Name`," +
                $"       `{configurationEnumTable.ValueColumn}` AS `Value`" +
                $" FROM `{configuration.Schema}`.`{configurationEnumTable.Table}`";
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
                TableComment = reader.GetString(10),
                Comment = reader.GetString(11)
            };

            if (column.IsForeignKey)
            {

                column.ReferencedTableName = reader.GetString(8);
                column.ReferencedColumnName = reader.GetString(9);
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
            /*
                Copied from  sql server reader and it just worked
             */

            _queryToReadColumns = $@" SELECT 
                c.COLUMN_NAME AS `Name`
            ,concat(t.TABLE_SCHEMA, '.', t.TABLE_NAME)  AS `TableFullName`
            ,t.TABLE_Name `TableName`
            ,t.TABLE_TYPE  AS TableType
            ,c.IS_NULLABLE AS `Nullable`
            ,c.DATA_TYPE AS `DataType`
            ,CASE WHEN pkc.CONSTRAINT_NAME IS NULL THEN 0 ELSE 1 END AS IsPrimaryKey
            ,CASE WHEN fkc.CONSTRAINT_NAME IS NULL THEN 0 ELSE 1 END AS IsForeignKey
            ,rct.TABLE_NAME AS ReferencedTableName
            ,rcuc.COLUMN_NAME AS ReferencedColumn
            ,t.TABLE_COMMENT
            ,c.COLUMN_COMMENT
            FROM INFORMATION_SCHEMA.COLUMNS c
                INNER JOIN INFORMATION_SCHEMA.TABLES t
                    ON c.TABLE_NAME = t.TABLE_NAME
                LEFT OUTER JOIN INFORMATION_SCHEMA.KEY_COLUMN_USAGE ccu
                    ON c.TABLE_NAME = ccu.TABLE_NAME AND c.COLUMN_NAME = ccu.COLUMN_NAME
                LEFT OUTER JOIN INFORMATION_SCHEMA.TABLE_CONSTRAINTS pkc
                    ON pkc.CONSTRAINT_TYPE = 'PRIMARY KEY' AND pkc.CONSTRAINT_NAME = ccu.CONSTRAINT_NAME
                LEFT OUTER JOIN INFORMATION_SCHEMA.TABLE_CONSTRAINTS fkc
                    ON fkc.CONSTRAINT_TYPE = 'FOREIGN KEY' AND fkc.CONSTRAINT_NAME = ccu.CONSTRAINT_NAME
                LEFT OUTER JOIN INFORMATION_SCHEMA.REFERENTIAL_CONSTRAINTS rc
                    ON fkc.CONSTRAINT_NAME = rc.CONSTRAINT_NAME
                LEFT OUTER JOIN INFORMATION_SCHEMA.TABLE_CONSTRAINTS rct
                    ON rct.CONSTRAINT_NAME = rc.UNIQUE_CONSTRAINT_NAME
                LEFT OUTER JOIN INFORMATION_SCHEMA.KEY_COLUMN_USAGE rcuc
                    ON rc.UNIQUE_CONSTRAINT_NAME = rcuc.CONSTRAINT_NAME
            WHERE  c.`table_schema` = '{configuration.Schema}'
            ORDER BY c.TABLE_NAME, c.ORDINAL_POSITION ";

            _queryToGetParameters = @"
                    SELECT 
	                        p.SPECIFIC_NAME AS SP
	                    ,p.PARAMETER_NAME AS [Name]
	                    ,p.DATA_TYPE AS DataType
                    FROM INFORMATION_SCHEMA.PARAMETERS p
	                    INNER JOIN INFORMATION_SCHEMA.ROUTINES r
		                    ON p.SPECIFIC_NAME = r.SPECIFIC_NAME
                    WHERE r.ROUTINE_TYPE = 'PROCEDURE'
                    ORDER BY p.ORDINAL_POSITION";

            /*
                i lied; changed a bit ;|
             */
        }
    }
}