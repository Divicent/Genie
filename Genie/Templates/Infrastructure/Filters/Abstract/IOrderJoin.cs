#region Usings



#endregion

using Genie.Core.Base.Generating;

namespace Genie.Core.Templates.Infrastructure.Filters.Abstract
{
    internal class IOrderJoinTemplate : GenieTemplate
    {
        public IOrderJoinTemplate(string path) : base(path)
        {
        }

        public override string Generate()
        {
            L($@"

namespace {GenerationContext.BaseNamespace}.Infrastructure.Filters.Abstract
{{
    public interface IOrderJoin<out T, out TQ> where T : IOrderContext
    {{
        T And {{ get; }}
        TQ Order();
    }}
}}

");

            return E();
        }
    }
}