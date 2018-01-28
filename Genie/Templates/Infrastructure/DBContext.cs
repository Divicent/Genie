#region Usings

using Genie.Core.Base.Configuration.Abstract;
using Genie.Core.Base.Generating;
using Genie.Core.Tools;

#endregion


namespace Genie.Core.Templates.Infrastructure
{
    public class DBContextTemplate : GenieTemplate
    {
        private readonly IConfiguration _configuration;

        public DBContextTemplate(string path, IConfiguration configuration) : base(path)
        {
            _configuration = configuration;
        }

        public override string Generate()
        {

            var container = FormatHelper.GetDbmsSpecificTemplatePartsContainer(_configuration);

            L($@"
using System.Data;
using {container.SqlClientNamespace};
using {GenerationContext.BaseNamespace}.Infrastructure.Interfaces;

namespace {GenerationContext.BaseNamespace}.Infrastructure
{{
	/// <summary>
    /// An Implementation that uses {container.SqlConnectionClassName}
    /// </summary>
	public class DBContext : IDBContext
    {{
        private readonly string _connectionString;
        private IDbConnection _connection;

		/// <summary>
        /// Initialize  a new dapper context 
        /// </summary>
        public DBContext(IConnectionStringProvider connectionStringProvider)
        {{
            _connectionString = connectionStringProvider.GetConnectionString();
        }}

    	/// <summary>
        /// Get the connection to the database
        /// </summary>
        public IDbConnection Connection => _connection ?? (_connection = new {container.SqlConnectionClassName}(_connectionString));

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