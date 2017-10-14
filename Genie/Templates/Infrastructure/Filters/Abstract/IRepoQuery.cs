#region Usings

using Genie.Core.Base.Generating.Concrete;

#endregion

namespace Genie.Core.Templates.Infrastructure.Filters.Abstract
{
    internal class IRepoQueryTemplate : GenieTemplate
    {
        public IRepoQueryTemplate(string path) : base(path)
        {
        }

        public override string Generate()
        {
            L($@"

using System.Collections.Generic;
using System.Data;

namespace {GenerationContext.BaseNamespace}.Infrastructure.Filters.Abstract
{{
    public interface IRepoQuery
    {{
        string Target {{ get; set; }}
        Queue<string> Where {{ get; set; }}
        Queue<string> Order {{ get; set; }}
        int? PageSize {{ get; set; }}
        int? Page {{ get; set; }}
        int? Limit {{ get; set; }}
        int? Skip {{ get; set; }}
        int? Take {{ get; set; }}
        IDbTransaction Transaction {{ get; set; }}
    }}
}}

");

            return E();
        }
    }
}