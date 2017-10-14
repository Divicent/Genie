using Genie.Core.Base.Generating.Concrete;

namespace Genie.Core.Templates.Infrastructure.Filters.Abstract
{
    internal class IExpressionJoinTemplate : GenieTemplate
    {
        public IExpressionJoinTemplate(string path) : base(path)
        {
        }

        public override string Generate()
        {
            L($@"

namespace {GenerationContext.BaseNamespace}.Infrastructure.Filters.Abstract
{{
    public interface IExpressionJoin<out T, out TQ> where T : IFilterContext
    {{
        T And {{ get; }}
        T Or {{ get; }}
        TQ Filter();
    }}
}}

");

            return E();
        }
    }
}