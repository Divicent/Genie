using Genie.Core.Base.Generating.Concrete;

namespace Genie.Core.Templates.Infrastructure.Filters.Concrete
{
    internal class OrderJoinTemplate : GenieTemplate
    {
        public OrderJoinTemplate(string path) : base(path)
        {
        }

        public override string Generate()
        {
            L($@"

using {GenerationContext.BaseNamespace}.Infrastructure.Filters.Abstract;

namespace {GenerationContext.BaseNamespace}.Infrastructure.Filters.Concrete
{{
    public class OrderJoin<T, TQ> : IOrderJoin<T, TQ> where T : IOrderContext
    {{
        private readonly T _t;
        private readonly TQ _q;

        internal OrderJoin(T t, TQ q)
        {{
            _t = t;
            _q = q;
        }}

        public T And {{ get {{ _t.And(); return _t; }} }}

        public TQ Order()
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