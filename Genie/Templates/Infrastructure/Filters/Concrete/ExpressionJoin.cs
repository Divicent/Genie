#region Usings

using Genie.Core.Base.Generating.Concrete;

#endregion

namespace Genie.Core.Templates.Infrastructure.Filters.Concrete
{
    internal class ExpressionJoinTemplate : GenieTemplate
    {
        public ExpressionJoinTemplate(string path) : base(path)
        {
        }

        public override string Generate()
        {
            L($@"

using {GenerationContext.BaseNamespace}.Infrastructure.Filters.Abstract;

namespace {GenerationContext.BaseNamespace}.Infrastructure.Filters.Concrete
{{
    public class ExpressionJoin<T, TQ> : IExpressionJoin<T, TQ> where T : IFilterContext
    {{
        private readonly T _t;
        private readonly TQ _q;

        internal ExpressionJoin(T t, TQ q)
        {{
            _t = t;
            _q = q;
        }}

        public T And
        {{
            get
            {{
                _t.And();
                return _t;
            }}
        }}

        public T Or
        {{
            get
            {{
                _t.Or();
                return _t;
            }}
        }}

        public IExpressionJoin<T, TQ> Start
        {{
            get
            {{

                _t.StartScope();
                return this;
            }}
        }}

        public IExpressionJoin<T, TQ> End
        {{
            get
            {{
                _t.EndScope();
                return this;
            }}
        }}

        public TQ Filter()
        {{
            return _q;
        }}
    }}
}}
");

            return E();
        }
    }
}