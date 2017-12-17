#region Usings

using Genie.Core.Tools;
using Genie.Core.Base.Generating;
using Genie.Core.Base.Configuration.Abstract;

#endregion



namespace Genie.Core.Templates.Infrastructure.Filters.Concrete
{
    internal class OrderElementTemplate : GenieTemplate
    {
        private readonly IConfiguration _configuration;
        public OrderElementTemplate(string path, IConfiguration configuration) : base(path)
        {
            _configuration = configuration;
        }

        public override string Generate()
        {
            var quote = FormatHelper.GetDbmsSpecificQuoter(_configuration);
            L($@"

using {GenerationContext.BaseNamespace}.Infrastructure.Filters.Abstract;

namespace {GenerationContext.BaseNamespace}.Infrastructure.Filters.Concrete
{{
    public class OrderElement<T, TQ> : IOrderElement<T, TQ> where T : IOrderContext
    {{
        private readonly string _propertyName;
        private readonly T _parent;
        private readonly TQ _q;

        internal OrderElement(string propertyName, T parent, TQ q)
        {{
            _parent = parent;
            _propertyName = propertyName;
            _q = q;
        }}

        public IOrderJoin<T, TQ> Ascending()
        {{
            _parent.Add(string.Format(""{quote("{0}")} ASC"", _propertyName));
            return new OrderJoin<T, TQ>(_parent, _q);
        }}

        public IOrderJoin<T, TQ> Descending()
        {{
            _parent.Add(string.Format(""{quote("{0}")} DESC"", _propertyName));
            return new OrderJoin<T, TQ>(_parent, _q);
        }}
    }}
}}
");

            return E();
        }
    }
}