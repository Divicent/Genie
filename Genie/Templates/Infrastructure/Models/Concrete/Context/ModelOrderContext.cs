#region Usings

using System.Collections.Generic;
using System.Text;
using Genie.Core.Base.Generating;
using Genie.Core.Models.Abstract;

#endregion

namespace Genie.Core.Templates.Infrastructure.Models.Concrete.Context
{
    internal class ModelOrderContextTemplate : GenieTemplate
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
            var fields = new StringBuilder();
            var props = new StringBuilder();

            foreach (var atd in _attributes)
            {
                fields.AppendLine(
                    $@"        private IOrderElement<I{_name}OrderContext, I{_name}QueryContext> {atd.FieldName};");
                props.AppendLine(
                    $@"	    public IOrderElement<I{_name}OrderContext, I{_name}QueryContext> {
                            atd.Name
                        } {{ get {{ return {atd.FieldName} ?? ( {atd.FieldName} = new OrderElement<I{
                            _name
                        }OrderContext, I{
                            _name
                        }QueryContext>(""{atd.Name}"", this, _queryContext)); }} }}");
            }

            L($@"
using {GenerationContext.BaseNamespace}.Infrastructure.Models.Abstract;
using {GenerationContext.BaseNamespace}.Infrastructure.Filters.Abstract;
using {GenerationContext.BaseNamespace}.Infrastructure.Filters.Concrete;
using {GenerationContext.BaseNamespace}.Infrastructure.Models.Abstract.Context;

namespace {GenerationContext.BaseNamespace}.Infrastructure.Models.Concrete.Context
{{
	public class {_name}OrderContext : BaseOrderContext, I{_name}OrderContext
    {{
		private readonly I{_name}QueryContext  _queryContext;
		internal {_name}OrderContext(I{_name}QueryContext context) {{ _queryContext = context; }}
{fields}

{props}
    }}
}}");

            return E();
        }
    }
}