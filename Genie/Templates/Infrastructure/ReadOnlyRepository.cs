#region Usings

#endregion

using Genie.Core.Base.Configuration.Abstract;
using Genie.Core.Base.Generating;
using Genie.Core.Tools;

namespace Genie.Core.Templates.Infrastructure
{
    public class ReadOnlyRepositoryTemplate : GenieTemplate
    {
        private readonly IConfiguration _configuration;
        public ReadOnlyRepositoryTemplate(string path, IConfiguration configuration) : base(path)
        {
            _configuration = configuration;
        }

        public override string Generate()
        {
            var container = FormatHelper.GetDbmsSpecificTemplatePartsContainer(_configuration);
            L($@"

using System.Collections.Generic;
using System.Data;
using System.Threading.Tasks;
using {container.SqlClientNamespace};
using {GenerationContext.BaseNamespace}.Dapper;
using {GenerationContext.BaseNamespace}.Infrastructure.Interfaces;
using {GenerationContext.BaseNamespace}.Infrastructure.Filters.Abstract;
using {GenerationContext.BaseNamespace}.Infrastructure.Querying;

namespace {GenerationContext.BaseNamespace}.Infrastructure
{{
    public class ReadOnlyRepository<T> : IReadOnlyRepository<T>
        where T : class
    {{
        public IDbConnection Conn {{ get; }}
        public IDBContext Context {{ get;}}

        protected ReadOnlyRepository(IDBContext context)
        {{
            Context = context;
            Conn = Context.Connection;
        }}

        public virtual IEnumerable<T> Get(IRepoQuery query)
        {{
            using (var connection = new {container.SqlConnectionClassName}(Conn.ConnectionString))
            {{
                return connection.Query<T>(new QueryBuilder(query).Get());
            }}
        }}

        public virtual async Task<IEnumerable<T>> GetAsync(IRepoQuery query)
        {{
            using (var connection = new {container.SqlConnectionClassName}(Conn.ConnectionString))
            {{
                return  await connection.QueryAsync<T>(new QueryBuilder(query).Get());
            }}
        }}

		public virtual T GetFirstOrDefault(IRepoQuery query)
        {{
            using (var connection = new {container.SqlConnectionClassName}(Conn.ConnectionString))
            {{
                return connection.QuerySingleOrDefault<T>(new QueryBuilder(query).Get());
            }}
        }}

        public virtual async Task<T> GetFirstOrDefaultAsync(IRepoQuery query)
        {{
            using (var connection = new {container.SqlConnectionClassName}(Conn.ConnectionString))
            {{
                return await connection.QuerySingleOrDefaultAsync<T>(new QueryBuilder(query).Get());
            }}
        }}

        public virtual int Count(IRepoQuery query)
        {{
            using (var connection = new {container.SqlConnectionClassName}(Conn.ConnectionString))
            {{
                return  connection.ExecuteScalar<int>(new QueryBuilder(query).Count());
            }}
        }}

        public virtual async Task<int> CountAsync(IRepoQuery query)
        {{
            using (var connection = new {container.SqlConnectionClassName}(Conn.ConnectionString))
            {{
                return await connection.ExecuteScalarAsync<int>(new QueryBuilder(query).Count());
            }}
        }}


        public virtual TA SumBy<TA>(IRepoQuery query, string column)
        {{
            using (var connection = new {container.SqlConnectionClassName}(Conn.ConnectionString))
            {{
                return  connection.ExecuteScalar<TA>(new QueryBuilder(query).SumBy(column));
            }}
        }}


        public virtual async Task<TA> SumByAsync<TA>(IRepoQuery query, string column)
        {{
            using (var connection = new {container.SqlConnectionClassName}(Conn.ConnectionString))
            {{
                return await connection.ExecuteScalarAsync<TA>(new QueryBuilder(query).SumBy(column));
            }}
        }}

		public string GetWhereClause(IRepoQuery query) 
		{{
			return new QueryBuilder(query).WhereClause();
		}}
    }}
}}
");

            return E();
        }
    }
}