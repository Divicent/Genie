#region Usings

#endregion

using Genie.Core.Base.Generating;

namespace Genie.Core.Templates.Infrastructure
{
    public class ReadOnlyRepositoryTemplate : GenieTemplate
    {
        public ReadOnlyRepositoryTemplate(string path) : base(path)
        {
        }

        public override string Generate()
        {
            L($@"

using System.Collections.Generic;
using System.Data;
using System.Threading.Tasks;
using System.Linq;
using {GenerationContext.BaseNamespace}.Dapper;
using {GenerationContext.BaseNamespace}.Infrastructure.Interfaces;
using {GenerationContext.BaseNamespace}.Infrastructure.Filters.Abstract;

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

	    public virtual T GetFirstOrDefault(IRepoQuery query)
        {{
            return Conn.Get<T>(query).FirstOrDefault();
        }}


        public virtual async Task<T> GetFirstOrDefaultAsync(IRepoQuery query)
        {{
            return (await Conn.GetAsync<T>(query)).FirstOrDefault();
        }}

        public virtual IEnumerable<T> Get(IRepoQuery query)
        {{
            return Conn.Get<T>(query).ToList();
        }}

        public virtual async Task<IEnumerable<T>> GetAsync(IRepoQuery query)
        {{
            return (await Conn.GetAsync<T>(query)).ToList();
        }}

        public virtual int Count(IRepoQuery query)
        {{
            return Conn.Count(query);
        }}

        public virtual async Task<int> CountAsync(IRepoQuery query)
        {{
            return await Conn.CountAsync(query);
        }}

		public string GetWhereClause(IRepoQuery query) 
		{{
			return Conn.GetWhereClause(query);
		}}
    }}
}}
");

            return E();
        }
    }
}