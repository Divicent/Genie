using Genie.Core.Base.Generating.Concrete;

namespace Genie.Core.Templates.Infrastructure.Filters.Abstract
{
    internal class IOrderContextTemplate : GenieTemplate
    {
        public IOrderContextTemplate(string path) : base(path)
        {
        }

        public override string Generate()
        {
            L($@"

using System.Collections.Generic;
namespace {GenerationContext.BaseNamespace}.Infrastructure.Filters.Abstract
{{
	public interface IOrderContext
    {{
        void And();
        void Add(string expression);
        Queue<string> GetOrderExpressions();
    }}
}}

");

            return E();
        }
    }
}