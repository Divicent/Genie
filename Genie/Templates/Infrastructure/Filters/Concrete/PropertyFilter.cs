#region Usings



#endregion

using Genie.Core.Base.Generating;

namespace Genie.Core.Templates.Infrastructure.Filters.Concrete
{
    internal class PropertyFilterTemplate : GenieTemplate
    {
        public PropertyFilterTemplate(string path) : base(path)
        {
        }

        public override string Generate()
        {
            L($@"

using {GenerationContext.BaseNamespace}.Infrastructure.Filters.Abstract;

namespace {GenerationContext.BaseNamespace}.Infrastructure.Filters.Concrete
{{
    internal class PropertyFilter : IPropertyFilter
    {{
        public string PropertyName {{ get; set; }}
        public string Type {{ get; set; }}
        public object Value {{ get; set; }}
    }}
}}
");

            return E();
        }
    }
}