#region Usings

using Genie.Core.Base.Reading.Abstract;

#endregion

namespace Genie.Core.Base.Reading.Concrete
{
    internal class DatabaseSchemaReaderFactory : IDatabaseSchemaReaderFactory
    {
        public IDatabaseSchemaReader GetReader(string databaseManagementSystemName)
        {
            switch (databaseManagementSystemName.ToLower())
            {
                case "mssql":
                    return new SqlServerSchemaReader();
                case "mysql":
                    return new MySqlSchemaReader();
                default:
                    return null;
            }
        }
    }
}