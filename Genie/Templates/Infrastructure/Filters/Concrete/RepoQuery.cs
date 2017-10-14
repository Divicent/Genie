using Genie.Core.Base.Generating.Concrete;

namespace Genie.Core.Templates.Infrastructure.Filters.Concrete
{
    internal class RepoQueryTemplate : GenieTemplate
    {
        public RepoQueryTemplate(string path) : base(path)
        {
        }

        public override string Generate()
        {
            L($@"

using System.Collections.Generic;
using System.Data;
using {GenerationContext.BaseNamespace}.Infrastructure.Filters.Abstract;

namespace {GenerationContext.BaseNamespace}.Infrastructure.Filters.Concrete
{{
    public class RepoQuery : IRepoQuery
    {{
        internal RepoQuery()
        {{
        }}

        public string Target {{ get; set; }}
        public Queue<string> Where {{ get; set; }}
        public Queue<string> Order {{ get; set; }}
        public int? PageSize {{ get; set; }}
        public int? Page {{ get; set; }}
        public int? Limit {{ get; set; }}
        public int? Skip {{ get; set; }}
        public int? Take {{ get; set; }}
        public IDbTransaction Transaction {{ get; set; }}
    }}

}}
");

            return E();
        }
    }
}