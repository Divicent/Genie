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
    /// Helps to apply filters on a boolean attribute to a query
    /// </summary>
    /// <typeparam name=""T"">Type of the filter context</typeparam>
    /// <typeparam name=""TQ"">Type of the query context</typeparam>>
	public interface IBoolFilter<out T, out TQ> where T : IFilterContext
	{{

        /// <summary>
        /// Adds an expression to the query to accept items where the current attribute's value is equal to the value of <paramref name=""value""/>
        /// </summary>
        /// <param name=""value"">Value to apply</param>
        /// <returns>A join to continue the query</returns>
		IExpressionJoin<T, TQ> Is(bool value);
  
        /// <summary>
        /// Adds an expression to the query to accept items where the current attribute's value is equal to false (0 in bit format)
        /// </summary>
        /// <returns>A join to continue the query</returns>
        IExpressionJoin<T, TQ> IsFalse();

        /// <summary>
        /// Adds an expression to the query to accept items where the current attribute's value is equal to true (1 in bit format)
        /// </summary>
        /// <returns>A join to continue the query</returns>
		IExpressionJoin<T, TQ> IsTrue();

        /// <summary>
        /// Adds an expression to the query to accept items where the current attribute's value is null (no value)
        /// </summary>
        /// <returns>A join to continue the query</returns>
		IExpressionJoin<T, TQ> IsNull();

        /// <summary>
        /// Adds an expression to the query to accept items where the current attribute's value is not null
        /// </summary>
        /// <returns>A join to continue the query</returns>
		IExpressionJoin<T, TQ> IsNotNull();

        /// <summary>
        /// Adds an expression to the query to accept items where the current attribute's value is null or not depending on the passed value to the parameter <paramref name=""isNull""/>
        /// </summary>
        /// <param name=""isNull"">true is null while false is not null</param>
        /// <returns>A join to continue the query</returns>
		IExpressionJoin<T, TQ> IsNull(bool isNull);
	}}
}}

 ");

            return E();
        }
    }
}