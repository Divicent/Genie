#region Usings

using Genie.Core.Base.Configuration.Abstract;
using Genie.Core.Base.Generating;
using Genie.Core.Tools;

#endregion


namespace Genie.Core.Templates.Infrastructure
{
    internal class DapperContextTemplate : GenieTemplate
    {
        private readonly IConfiguration _configuration;

        public DapperContextTemplate(string path, IConfiguration configuration) : base(path)
        {
            _configuration = configuration;
        }

        public override string Generate()
        {
            var usingConfiguration = GenerationContext.Core
                ? "using Microsoft.Extensions.Configuration;"
                : "using System.Configuration;";
            var constructor = GenerationContext.Core
                ? @"        public DapperContext(IConfiguration configuration)
        {
			 _connectionString = configuration[""connectionString""];
		}
        
        public DapperContext(string connectionString) 
        {
            if(string.IsNullOrWhiteSpace(connectionString))
            {
                throw new System.Exception(""ConnectionString is empty or null"");
            }
             _connectionString = connectionString;
        }"
                : @"		public DapperContext()
        {
			var connectionStringName = ConfigurationManager.AppSettings[""UsedConnectionString""];
			_connectionString = ConfigurationManager.ConnectionStrings[connectionStringName].ConnectionString;	
		}";

            var container = FormatHelper.GetDbmsSpecificTemplatePartsContainer(_configuration);

            L($@"
{usingConfiguration}
using System.Data;
using {container.SqlClientNamespace};
using {GenerationContext.BaseNamespace}.Infrastructure.Interfaces;

namespace {GenerationContext.BaseNamespace}.Infrastructure
{{
	/// <summary>
    /// An Implementation that uses {container.SqlConnectionClassName}
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
        public IDbConnection Connection => _connection ?? (_connection = new {
                    container.SqlConnectionClassName
                }(_connectionString));

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