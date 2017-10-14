#region Usings



#endregion

using Genie.Core.Base.Generating;

namespace Genie.Core.Templates.Infrastructure.Filters.Abstract
{
    internal class IFilterContextTemplate : GenieTemplate
    {
        public IFilterContextTemplate(string path) : base(path)
        {
        }

        public override string Generate()
        {
            L($@"

using System.Collections.Generic;

namespace {GenerationContext.BaseNamespace}.Infrastructure.Filters.Abstract
{{
	public interface IFilterContext
	{{
		Queue<string> Expressions {{ get; set; }}
		void And();
		void Or();
		void Add(string expression);
		Queue<string> GetFilterExpressions();
        void StartScope();
        void EndScope();
	}}
}}

");

            return E();
        }
    }
}