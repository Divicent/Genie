#region Usings



#endregion

using Genie.Core.Base.Generating;

namespace Genie.Core.Templates.Infrastructure.Filters.Abstract
{
    internal class IStringFilterTemplate : GenieTemplate
    {
        public IStringFilterTemplate(string path) : base(path)
        {
        }

        public override string Generate()
        {
            L($@"

namespace {GenerationContext.BaseNamespace}.Infrastructure.Filters.Abstract
{{
    public interface IStringFilter<out T, out TQ> where T : IFilterContext
    {{
        IExpressionJoin<T, TQ> EqualsTo(string str);
        IExpressionJoin<T, TQ> NotEquals(string str);
        IExpressionJoin<T, TQ> Contains(string str);
        IExpressionJoin<T, TQ> StartsWith(string str);
        IExpressionJoin<T, TQ> EndsWith(string str);
        IExpressionJoin<T, TQ> NotStartsWith(string str);
        IExpressionJoin<T, TQ> NotEndsWith(string str);
        IExpressionJoin<T, TQ> IsEmpty();
        IExpressionJoin<T, TQ> IsNotEmpty();
        IExpressionJoin<T, TQ> IsNull();
		IExpressionJoin<T, TQ> IsNull(bool isNull);
        IExpressionJoin<T, TQ> IsNotNull();
        IExpressionJoin<T, TQ> In(params string[] items);
        IExpressionJoin<T, TQ> NotIn(params string[] items);
    }}
}} 

");

            return E();
        }
    }
}