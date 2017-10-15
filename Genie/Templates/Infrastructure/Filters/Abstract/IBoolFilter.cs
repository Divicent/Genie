#region Usings



#endregion

using Genie.Core.Base.Generating;

namespace Genie.Core.Templates.Infrastructure.Filters.Abstract
{
    internal class IBoolFilterTemplate : GenieTemplate
    {
        public IBoolFilterTemplate(string path) : base(path)
        {
        }

        public override string Generate()
        {
            L($@"

namespace {GenerationContext.BaseNamespace}.Infrastructure.Filters.Abstract
{{
  /// <summary>
  /// Helps to apply filters on a boolean attribute
  /// </summary>
  /// <typeparam name=""T"">Type of the filter context</typeparam>
  /// <typeparam name=""TQ"">Type of the query context</typeparam>
	public interface IBoolFilter<out T, out TQ> where T : IFilterContext
	{{

    /// <summary>
    /// Is the value equals to the provided value
    /// </summary>
    /// <param name=""value"">Value to apply</param>
    /// <returns>A join to continue the query</returns>
		IExpressionJoin<T, TQ> Is(bool value);
  
    /// <summary>
    /// Is the value false
    /// </summary>
    /// <returns>A join to continue the query</returns>
    IExpressionJoin<T, TQ> IsFalse();

    /// <summary>
    /// Is the value true
    /// </summary>
    /// <returns>A join to continue the query</returns>
		IExpressionJoin<T, TQ> IsTrue();

    /// <summary>
    /// Is the value null
    /// </summary>
    /// <returns>A join to continue the query</returns>
		IExpressionJoin<T, TQ> IsNull();

    /// <summary>
    /// Is the value not null
    /// </summary>
    /// <returns>A join to continue the query</returns>
		IExpressionJoin<T, TQ> IsNotNull();

    /// <summary>
    /// Conditional is null using provided value, if true -> isNull is applied else isNotNull is applied.
    /// </summary>
    /// <param name=""isNull""></param>
    /// <returns>A join to continue the query</returns>
		IExpressionJoin<T, TQ> IsNull(bool isNull);
	}}
}}

 ");

            return E();
        }
    }
}