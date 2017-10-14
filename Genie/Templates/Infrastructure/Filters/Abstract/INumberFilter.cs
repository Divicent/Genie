using Genie.Core.Base.Generating.Concrete;

namespace Genie.Core.Templates.Infrastructure.Filters.Abstract
{
    internal class INumberFilterTemplate : GenieTemplate
    {
        public INumberFilterTemplate(string path) : base(path)
        {
        }

        public override string Generate()
        {
            L($@"

namespace {GenerationContext.BaseNamespace}.Infrastructure.Filters.Abstract
{{
    public interface INumberFilter<out T, out TQ> where T : IFilterContext
    {{
        IExpressionJoin<T, TQ> EqualsTo(double number);
        IExpressionJoin<T, TQ> NotEquals(double number);
        IExpressionJoin<T, TQ> LargerThan(double number);
        IExpressionJoin<T, TQ> LessThan(double number);
        IExpressionJoin<T, TQ> LargerThanOrEqualTo(double number);
        IExpressionJoin<T, TQ> LessThanOrEqualTo(double number);
        IExpressionJoin<T, TQ> Between(double from, double to);
        IExpressionJoin<T, TQ> IsNull();
        IExpressionJoin<T, TQ> IsNotNull();
		IExpressionJoin<T, TQ> IsNull(bool isNull);
        IExpressionJoin<T, TQ> In(params double[] items);
        IExpressionJoin<T, TQ> NotIn(params double[] items);
    }}
}} 

");

            return E();
        }
    }
}