using System;
using DatabaseSchemaReader;
using DatabaseSchemaReader.DataSchema;

namespace Genie.Base
{
    internal class Reader
    {
        private const string Providername = "System.Data.SqlClient";
        internal static DatabaseSchema Read(GenieConfiguration configuration)
        {
            try
            {
                var reader = new DatabaseReader(configuration.ConnectionString, Providername);
                return reader.ReadAll();
            }
            catch (Exception e)
            {
                throw  new Exception("Unable to read database", e);
            }
        }
    }
}
