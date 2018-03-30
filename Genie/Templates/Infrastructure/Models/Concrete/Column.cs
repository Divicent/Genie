using Genie.Core.Base.Generating;

namespace Genie.Core.Templates.Infrastructure.Models.Concrete
{
    public class ColumnTemplate: GenieTemplate
    {
        public ColumnTemplate(string path) : base(path)
        {
        }

        public override string Generate()
        {
            L($@"
using {GenerationContext.BaseNamespace}.Infrastructure.Models.Abstract;

namespace {GenerationContext.BaseNamespace}.Infrastructure.Models.Concrete
{{
    public class Column<T>: IColumn<T>
    {{
        internal Column(string name) 
        {{
            Name = name;
        }}

        public string Name {{ get; }}
    }}
}}");
            return E();
        }
    }
}