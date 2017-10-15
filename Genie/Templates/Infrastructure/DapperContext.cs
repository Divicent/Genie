#region Usings



#endregion

using Genie.Core.Base.Generating;

namespace Genie.Core.Templates.Infrastructure
{
    internal class DapperContextTemplate : GenieTemplate
    {
        public DapperContextTemplate(string path) : base(path)
        {
        }

        public override string Generate()
        {
            var usingConfiguration = GenerationContext.Core
                ? "using Microsoft.Extensions.Configuration;"
                : "using System.Configuration;";
            var constructor = GenerationContext.Core
                ? @"        public DapperContext(IConfiguration configuration)
        {{
			 _connectionString = configuration[""connectionString""];
		}}"
                : @"		public DapperContext()
        {{
			var connectionStringName = ConfigurationManager.AppSettings[""UsedConnectionString""];
			_connectionString = ConfigurationManager.ConnectionStrings[connectionStringName].ConnectionString;	
		}}";
            L($@"


{usingConfiguration}
using System.Data;
using System.Data.SqlClient;
using {GenerationContext.BaseNamespace}.Infrastructure.Interfaces;

namespace {GenerationContext.BaseNamespace}.Infrastructure
{{
	/// <summary>
    /// An Implementation that uses SqlConnection
    /// </summary>
	public class DapperContext : IDapperContext
    {{
        private readonly string _connectionString;
        private IDbConnection _connection;

		/// <summary>
        /// Initialize  a new dapper context 
        /// </summary>
{constructor}

    		/// <summary>
        /// Get the connection to the database
        /// </summary>
        public IDbConnection Connection => _connection ?? (_connection = new SqlConnection(_connectionString));

        public IUnitOfWork Unit() 
        {{
            return new UnitOfWork(this);
        }}
    }}
}}
");

            return E();
        }
    }
}