#region Usings



#endregion

using Genie.Core.Base.Generating;

namespace Genie.Core.Templates.Infrastructure.Interfaces
{
    internal class IReadOnlyRepositoryTemplate : GenieTemplate
    {
        public IReadOnlyRepositoryTemplate(string path) : base(path)
        {
        }

        public override string Generate()
        {
            L($@"

using System.Collections.Generic;
using System.Data;
using {GenerationContext.BaseNamespace}.Infrastructure.Filters.Abstract;

namespace {GenerationContext.BaseNamespace}.Infrastructure.Interfaces
{{
	public interface IReadOnlyRepository<out T>
		where T : class
	{{
		IDbConnection Conn {{ get; }}
		IDapperContext Context {{ get; }}

        IEnumerable<T> Get(IRepoQuery query);
		T GetFirstOrDefault(IRepoQuery query);
	    int Count(IRepoQuery query);
		string GetWhereClause(IRepoQuery query);
	}}
}}
");

            return E();
        }
    }
}