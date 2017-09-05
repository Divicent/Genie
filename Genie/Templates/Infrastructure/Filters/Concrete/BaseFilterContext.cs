using Genie.Base.Generating.Concrete;
using Genie.Templates;

namespace Genie.Templates.Infrastructure.Filters.Concrete
{
    internal class BaseFilterContextTemplate: GenieTemplate
    {
        public BaseFilterContextTemplate(string path) : base(path){}

public override string Generate()
{
L($@"

using System.Collections.Generic;
using {GenerationContext.BaseNamespace}.Infrastructure.Filters.Abstract;

namespace {GenerationContext.BaseNamespace}.Infrastructure.Filters.Concrete
{{
    public abstract class BaseFilterContext : IFilterContext
    {{
        protected BaseFilterContext()
        {{
            Expressions = new Queue<string>();
        }}

        public Queue<string> Expressions {{ get; set; }}

        public void And()
        {{
            Expressions.Enqueue(""and"");
        }}

        public void Or()
        {{
            Expressions.Enqueue(""or"");
        }}

        public void Add(string expression)
        {{
            Expressions.Enqueue(expression);
        }}

        public Queue<string> GetFilterExpressions()
        {{
            return Expressions;
        }}
    }}
}}
");

return E();
    
}
    }
}