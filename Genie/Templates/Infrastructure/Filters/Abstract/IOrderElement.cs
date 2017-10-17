#region Usings



#endregion

using Genie.Core.Base.Generating;

namespace Genie.Core.Templates.Infrastructure.Filters.Abstract
{
    internal class IOrderElementTemplate : GenieTemplate
    {
        public IOrderElementTemplate(string path) : base(path)
        {
        }

        public override string Generate()
        {
            L($@"

namespace {GenerationContext.BaseNamespace}.Infrastructure.Filters.Abstract
{{

    /// <summary>
    /// Helps to add complete order expressions to the target query
    /// </summary>
    /// <typeparam name=""T"">Type of the order context</typeparam>
    /// <typeparam name=""TQ"">type of the query contexts</typeparam>
	public interface IOrderElement<out T, out TQ> where T : IOrderContext
	{{

        /// <summary>
        /// Adds an expression to the query to order the result by current attribute ascending
        /// </summary>
        /// <returns>A join to continue the query</returns>
		IOrderJoin<T, TQ> Ascending();
		
        /// <summary>
        /// Adds an expression to the query to order the result by current attribute descending
        /// </summary>
        /// <returns>A join to continue the query</returns>
        IOrderJoin<T, TQ> Descending();
	}}
}}

");

            return E();
        }
    }
}