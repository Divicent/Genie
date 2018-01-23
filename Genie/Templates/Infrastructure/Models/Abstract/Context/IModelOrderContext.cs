#region Usings

using System.Collections.Generic;
using System.Text;
using Genie.Core.Base.Generating;
using Genie.Core.Models.Abstract;

#endregion

namespace Genie.Core.Templates.Infrastructure.Models.Abstract.Context
{
    public class IModelOrderContextTemplate : GenieTemplate
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
                var atdComment = !string.IsNullOrWhiteSpace(atd.Comment);
                var commentStr = atdComment
                    ? $@"
        /// <para>{atd.Comment}</para>"
                    : "";
                props.AppendLine($@"		/// <summary>
{commentStr}
		///  Apply order by on {atd.Name} attribute . this order by expression will be preserved within entire query context
		/// </summary>");


                props.AppendLine($@"		IOrderElement<I{_name}OrderContext,I{_name}QueryContext> {atd.Name} {{ get; }}
");
            }

            L($@"
using {GenerationContext.BaseNamespace}.Infrastructure.Filters.Abstract;

namespace {GenerationContext.BaseNamespace}.Infrastructure.Models.Abstract.Context
{{

    /// <summary>
    /// Helps to build order for queries on the data source {_name}
    /// </summary>
    public interface I{_name}OrderContext: IOrderContext
    {{

{props}

    }}
}}");

            return E();
        }
    }
}