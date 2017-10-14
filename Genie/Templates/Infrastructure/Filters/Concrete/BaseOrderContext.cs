using Genie.Core.Base.Generating.Concrete;

namespace Genie.Core.Templates.Infrastructure.Filters.Concrete
{
    internal class BaseOrderContextTemplate : GenieTemplate
    {
        public BaseOrderContextTemplate(string path) : base(path)
        {
        }

        public override string Generate()
        {
            L($@"

using System.Collections.Generic;
using {GenerationContext.BaseNamespace}.Infrastructure.Filters.Abstract;

namespace {GenerationContext.BaseNamespace}.Infrastructure.Filters.Concrete
{{
    public abstract class BaseOrderContext : IOrderContext
    {{
        protected BaseOrderContext() {{ Expressions = new Queue<string>(); }}
        protected Queue<string> Expressions {{ get; set; }}
        public void And() {{ Expressions.Enqueue("",""); }}
        public void Add(string expression) {{ Expressions.Enqueue(expression); }}
        public Queue<string> GetOrderExpressions() {{ return Expressions; }}
    }}
}}
");

            return E();
        }
    }
}