using Genie.Base.Configuration.Abstract;
using Genie.Base.ProcessOutput.Abstract;
using Genie.Base.Reading.Abstract;
using MySql.Data.MySqlClient;

namespace  Genie.Base.Reading.Concrete 
{
    internal class MySqlSchemaReader : IDatabaseSchemaReader
    {

        public IDatabaseSchema Read(IConfiguration configuration, IProcessOutput output)
        {
            var  connection = GetConnection(configuration.ConnectionString);
            try
            {

            }
            catch
            {

            }
            finally 
            {
                connection.Close();
                connection.Dispose();
            }
        }

        private MySqlConnection GetConnection(string connectionString) 
        {
            return new MySqlConnection(connectionString);
        }
    }
}