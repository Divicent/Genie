using System.Collections.Generic;
using System.Text;
using Genie.Core.Base.Generating.Concrete;
using Genie.Core.Models.Abstract;

namespace Genie.Core.Templates.Infrastructure.Models.Abstract.Context
{
    internal class IModelOrderContextTemplate : GenieTemplate
    {
        private readonly List<ISimpleAttribute> _attributes;
        private readonly string _name;

        public IModelOrderContextTemplate(string path, string name, List<ISimpleAttribute> attributes) : base(path)
        {
            _name = name;
            _attributes = attributes;
        }

        public override string Generate()
        {
            var props = new StringBuilder();

            foreach (var atd in _attributes)
            {
                if (!string.IsNullOrWhiteSpace(atd.Comment))
                    props.AppendLine($@"		/// <summary>
		/// {atd.Comment}
		/// </summary>");

                props.AppendLine($@"		IOrderElement<I{_name}OrderContext,I{_name}QueryContext> {atd.Name} {{ get; }}");
            }

            L($@"
using {GenerationContext.BaseNamespace}.Infrastructure.Filters.Abstract;

namespace {GenerationContext.BaseNamespace}.Infrastructure.Models.Abstract.Context
{{
    public interface I{_name}OrderContext: IOrderContext
    {{

{props}

    }}
}}");

            return E();
        }
    }
}