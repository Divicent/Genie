#region Usings



#endregion

using Genie.Core.Base.Generating;

namespace Genie.Core.Templates.Infrastructure.Filters.Abstract
{
    internal class IPropertyFilterTemplate : GenieTemplate
    {
        public IPropertyFilterTemplate(string path) : base(path)
        {
        }

        public override string Generate()
        {
            L($@"

namespace {GenerationContext.BaseNamespace}.Infrastructure.Filters.Abstract
{{

    /// <summary>
    /// Holds a filter to be applied on an attribute
    /// </summary>
    public interface IPropertyFilter
    {{
        /// <summary>
        /// The property name to apply the filter
        /// </summary>
        string PropertyName {{ get; set; }}

        /// <summary>
        /// Expression type
        /// </summary>
        string Type {{ get; set; }}
            
        /// <summary>
        /// Value if available
        /// </summary>
        object Value {{ get; set; }}
    }}
}}
");

            return E();
        }
    }
}