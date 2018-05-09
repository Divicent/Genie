#region Usings

using System.Collections.Generic;
using Genie.Core.Models.Abstract;

#endregion

namespace Genie.Core.Templates.Infrastructure.Models.Concrete.Context
{
    public class ModelOrderContextTemplate : GenieTemplate
    {
        private readonly List<ISimpleAttribute> _attributes;
        private readonly string _name;

        public ModelOrderContextTemplate(string path, string name, List<ISimpleAttribute> attributes) : base(path)
        {
            _name = name;
            _attributes = attributes;
        }

        public override string Generate()
        {

            const string template =
@"
	            public class {{name}}OrderContext : BaseOrderContext, I{{name}}OrderContext
                {
		            private readonly I{{name}}QueryContext  _queryContext;
		            internal {{name}}OrderContext(I{{name}}QueryContext context) { _queryContext = context; }
{% for atd in attributes %}
                    private IOrderElement<I{{name}}OrderContext, I{{name}}QueryContext> {{atd.FieldName}};
{% endfor %}
{% for atd in attributes %}
                    public IOrderElement<I{{name}}OrderContext, I{{name}}QueryContext> {{atd.Name}} { get { return {{atd.FieldName}} ?? ( {{atd.FieldName}} = new OrderElement<I{{name}}OrderContext, I{{name}}QueryContext>(""{{atd.Name}}"", this, _queryContext)); } }
{% endfor %}
                }
";
            return Process(nameof(ModelOrderContextTemplate), template, new {name = _name, attributes = _attributes,});
        }
    }
}