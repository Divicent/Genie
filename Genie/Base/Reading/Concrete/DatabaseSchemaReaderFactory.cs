#region Usings

using Genie.Core.Base.Reading.Abstract;

#endregion

namespace Genie.Core.Base.Reading.Concrete
{
    internal static class DatabaseSchemaReaderFactory
    {
        public static IDatabaseSchemaReader GetReader(string databaseManagementSystemName)
        {
            switch (databaseManagementSystemName.ToLowerInvariant())
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