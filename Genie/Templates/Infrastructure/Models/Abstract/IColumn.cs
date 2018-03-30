using Genie.Core.Base.Generating;

namespace Genie.Core.Templates.Infrastructure.Models.Abstract
{
    public class IColumnTemplate: GenieTemplate
    {
        public IColumnTemplate(string path) : base(path)
        {
        }

        public override string Generate()
        {
            L($@"
namespace {GenerationContext.BaseNamespace}.Infrastructure.Models.Abstract
{{
    /// A column represents a table column metadata
    /// </summary>
    /// <typeparam name=""T"">Type of the column</typeparam>
    public interface IColumn<T>
    {{
        string Name {{ get; }}
    }}
}}");
            return E();
        }
    }
}