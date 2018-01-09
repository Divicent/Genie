#region Usings

#endregion

using Genie.Core.Base.Generating;

namespace Genie.Core.Templates.Infrastructure.Filters.Concrete
{
    internal class BaseFilterContextTemplate : GenieTemplate
    {
        public BaseFilterContextTemplate(string path) : base(path)
        {
        }

        public override string Generate()
        {
            L($@"
using System;
using System.Collections.Generic;
using {GenerationContext.BaseNamespace}.Infrastructure.Filters.Abstract;

namespace {GenerationContext.BaseNamespace}.Infrastructure.Filters.Concrete
{{
    public abstract class BaseFilterContext : IFilterContext
    {{
        private int _startedScopes;

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

        public void StartScope()
        {{
            Expressions.Enqueue(""("");
            _startedScopes++;
        }}

        public void EndScope()
        {{
            if(_startedScopes <1 )
                throw new Exception(""No scopes are started"");

            Expressions.Enqueue("")"");
            _startedScopes--;
        }}
    }}
}}
");

            return E();
        }
    }
}