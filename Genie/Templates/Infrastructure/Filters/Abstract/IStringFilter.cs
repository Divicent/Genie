#region Usings

#endregion

using Genie.Core.Base.Generating;

namespace Genie.Core.Templates.Infrastructure.Filters.Abstract
{
    public class IStringFilterTemplate : GenieTemplate
    {
        public IStringFilterTemplate(string path) : base(path)
        {
        }

        public override string Generate()
        {
            L($@"

namespace {GenerationContext.BaseNamespace}.Infrastructure.Filters.Abstract
{{

    /// <summary>
    /// Helps to apply filters on a string attribute to a query
    /// </summary>
    /// <typeparam name=""T"">Type of the filter context</typeparam>
    /// <typeparam name=""TQ"">Type of the query context</typeparam>
    public interface IStringFilter<out T, out TQ> where T : IFilterContext
    {{
        /// <summary>
        /// Adds an expression to the query to accept items where the current attribute's value is equal to the value of <paramref name=""str""/>
        /// </summary>
        /// <param name=""str"">Value to apply</param>
        /// <returns>A join to continue the query</returns>
        IExpressionJoin<T, TQ> EqualsTo(string str);

        /// <summary>
        /// Adds an expression to the query to accept items where the current attribute's value is not equal to the value of <paramref name=""str""/>
        /// </summary>
        /// <param name=""str"">Value to apply</param>
        /// <returns>A join to continue the query</returns>
        IExpressionJoin<T, TQ> NotEquals(string str);

        /// <summary>
        /// Adds an expression to the query to accept items where the current attribute's value contains the value of <paramref name=""str""/>
        /// </summary>
        /// <param name=""str"">Value to apply</param>
        /// <returns>A join to continue the query</returns>
        IExpressionJoin<T, TQ> Contains(string str);

        /// <summary>
        /// Adds an expression to the query to accept items where the current attribute's value starts with the value of <paramref name=""str""/>
        /// </summary>
        /// <param name=""str"">Value to apply</param>
        /// <returns>A join to continue the query</returns>
        IExpressionJoin<T, TQ> StartsWith(string str);

        /// <summary>
        /// Adds an expression to the query to accept items where the current attribute's value ends with the value of <paramref name=""str""/>
        /// </summary>
        /// <param name=""str"">Value to apply</param>
        /// <returns>A join to continue the query</returns>
        IExpressionJoin<T, TQ> EndsWith(string str);

        /// <summary>
        /// Adds an expression to the query to accept items where the current attribute's value doesnt start with the value of <paramref name=""str""/>
        /// </summary>
        /// <param name=""str"">Value to apply</param>
        /// <returns>A join to continue the query</returns>
        IExpressionJoin<T, TQ> NotStartsWith(string str);

        /// <summary>
        /// Adds an expression to the query to accept items where the current attribute's value doesnt end with the value of <paramref name=""str""/>
        /// </summary>
        /// <param name=""str"">Value to apply</param>
        /// <returns>A join to continue the query</returns>
        IExpressionJoin<T, TQ> NotEndsWith(string str);

        /// <summary>
        /// Adds an expression to the query to accept items where the current attribute's value is empty
        /// </summary>
        /// <returns>A join to continue the query</returns>
        IExpressionJoin<T, TQ> IsEmpty();
        
        /// <summary>
        /// Adds an expression to the query to accept items where the current attribute's value is not empty
        /// </summary>
        /// <returns>A join to continue the query</returns>
        IExpressionJoin<T, TQ> IsNotEmpty();
        
        /// <summary>
        /// Adds an expression to the query to accept items where the current attribute's value is null (no value)
        /// </summary>
        /// <returns>A join to continue the query</returns>
        IExpressionJoin<T, TQ> IsNull();
		    
        /// <summary>
        /// Adds an expression to the query to accept items where the current attribute's value is null or not depending on the passed value to the parameter <paramref name=""isNull""/>
        /// </summary>
        /// <param name=""isNull"">true is null while false is not null</param>
        /// <returns>A join to continue the query</returns>
        IExpressionJoin<T, TQ> IsNull(bool isNull);

        /// <summary>
        /// Adds an expression to the query to accept items where the current attribute's value is not null
        /// </summary>
        /// <returns>A join to continue the query</returns>
        IExpressionJoin<T, TQ> IsNotNull();
        
        /// <summary>
        /// Adds an expression to the query to accept items where the current attribute's value is in the <paramref name=""value""/> array
        /// </summary>
        /// <param name=""items"">Values to check</param>
        /// <returns>A join to continue the query</returns>
        IExpressionJoin<T, TQ> In(params string[] items);
        
        /// <summary>
        /// Adds an expression to the query to accept items where the current attribute's value is not in the <paramref name=""value""/> array
        /// </summary>
        /// <param name=""items"">Values to check</param>
        /// <returns>A join to continue the query</returns>
        IExpressionJoin<T, TQ> NotIn(params string[] items);
    }}
}} 

");

            return E();
        }
    }
}