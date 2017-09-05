using Genie.Base.Generating.Concrete;
using Genie.Templates;

namespace Genie.Templates.Infrastructure.Filters.Abstract
{
    internal class IDateFilterTemplate: GenieTemplate
    {
        public IDateFilterTemplate(string path) : base(path){}

public override string Generate()
{
L($@"

using System;

namespace {GenerationContext.BaseNamespace}.Infrastructure.Filters.Abstract
{{
    public interface IDateFilter<out T, out TQ> where T : IFilterContext
    {{
        IExpressionJoin<T, TQ> EqualsTo(DateTime date);
        IExpressionJoin<T, TQ> NotEquals(DateTime date);
        IExpressionJoin<T, TQ> LargerThan(DateTime number);
        IExpressionJoin<T, TQ> LessThan(DateTime date);
        IExpressionJoin<T, TQ> LargerThanOrEqualTo(DateTime date);
        IExpressionJoin<T, TQ> LessThanOrEqualTo(DateTime date);
        IExpressionJoin<T, TQ> Between(DateTime from, DateTime to);
        IExpressionJoin<T, TQ> IsNull();
        IExpressionJoin<T, TQ> IsNotNull();
		IExpressionJoin<T, TQ> IsNull(bool isNull);
        IExpressionJoin<T, TQ> In(params DateTime[] items);
        IExpressionJoin<T, TQ> NotIn(params DateTime[] items);
    }}
}}

 ");

return E();
    
}
    }
}