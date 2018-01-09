#region Usings

#endregion

using Genie.Core.Base.Generating;

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

    /// <summary>
    /// Helps to apply filters on a number attribute to a query
    /// </summary>
    /// <typeparam name=""T"">Type of the filter context</typeparam>
    /// <typeparam name=""TQ"">Type of the query context</typeparam>
    public interface INumberFilter<out T, out TQ> where T : IFilterContext
    {{

        /// <summary>
        /// Adds an expression to the query to accept items where the current attribute's value is equal to the value of <paramref name=""number""/>
        /// </summary>
        /// <param name=""number"">Value to apply</param>
        /// <returns>A join to continue the query</returns>
        IExpressionJoin<T, TQ> EqualsTo(double number);

        /// <summary>
        /// Adds an expression to the query to accept items where the current attribute's value is not equal to the value of <paramref name=""number""/>
        /// </summary>
        /// <param name=""number"">Value to apply</param>
        /// <returns>A join to continue the query</returns>
        IExpressionJoin<T, TQ> NotEquals(double number);
        
        /// <summary>
        /// Adds an expression to the query to accept items where the current attribute's value is larger than the value of <paramref name=""number""/>
        /// </summary>
        /// <param name=""number"">Value to apply</param>
        /// <returns>A join to continue the query</returns>
        IExpressionJoin<T, TQ> LargerThan(double number);

        /// <summary>
        /// Adds an expression to the query to accept items where the current attribute's value is less than the value of <paramref name=""number""/>
        /// </summary>
        /// <param name=""number"">Value to apply</param>
        /// <returns>A join to continue the query</returns>
        IExpressionJoin<T, TQ> LessThan(double number);

        /// <summary>
        /// Adds an expression to the query to accept items where the current attribute's value is larger than or equal to the value of <paramref name=""number""/>
        /// </summary>
        /// <param name=""number"">Value to apply</param>
        /// <returns>A join to continue the query</returns>
        IExpressionJoin<T, TQ> LargerThanOrEqualTo(double number);
        
        /// <summary>
        /// Adds an expression to the query to accept items where the current attribute's value is less than or equal to the value of <paramref name=""date""/>
        /// </summary>
        /// <param name=""date"">Value to apply</param>
        /// <returns>A join to continue the query</returns>
        IExpressionJoin<T, TQ> LessThanOrEqualTo(double number);
        
        /// <summary>
        /// Adds an expression to the query to accept items where the current attribute's value is in between the value of <paramref name=""from""/> and <paramref name=""to""/>.
        /// </summary>
        /// <param name=""from"">The from value</param>
        /// <param name=""to"">The to value</param>
        /// <returns>A join to continue the query</returns>
        IExpressionJoin<T, TQ> Between(double from, double to);

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
        
        /// <summary>
        /// Adds an expression to the query to accept items where the current attribute's value is in the <paramref name=""value""/> array
        /// </summary>
        /// <param name=""items"">Values to check</param>
        /// <returns>A join to continue the query</returns>
        IExpressionJoin<T, TQ> In(params double[] items);
        
        /// <summary>
        /// Adds an expression to the query to accept items where the current attribute's value is not in the <paramref name=""value""/> array
        /// </summary>
        /// <param name=""items"">Values to check</param>
        /// <returns>A join to continue the query</returns>
        IExpressionJoin<T, TQ> NotIn(params double[] items);
    }}
}} 

");

            return E();
        }
    }
}