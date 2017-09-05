using Genie.Base.Generating.Concrete;
using Genie.Templates;

namespace Genie.Templates.Infrastructure.Filters.Abstract
{
    internal class IBoolFilterTemplate: GenieTemplate
    {
        public IBoolFilterTemplate(string path) : base(path){}

public override string Generate()
{
L($@"

namespace {GenerationContext.BaseNamespace}.Infrastructure.Filters.Abstract
{{
	public interface IBoolFilter<out T, out TQ> where T : IFilterContext
	{{
		IExpressionJoin<T, TQ> Is(bool value);
		IExpressionJoin<T, TQ> IsFalse();
		IExpressionJoin<T, TQ> IsTrue();
		IExpressionJoin<T, TQ> IsNull();
		IExpressionJoin<T, TQ> IsNotNull();
		IExpressionJoin<T, TQ> IsNull(bool isNull);
	}}
}}

 ");

return E();
    
}
    }
}