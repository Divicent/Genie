#region Usings

#endregion

using Genie.Core.Base.Generating;

namespace Genie.Core.Templates.Infrastructure.Filters.Abstract
{
    public class IOrderContextTemplate : GenieTemplate
    {
        public IOrderContextTemplate(string path) : base(path)
        {
        }

        public override string Generate()
        {
            L($@"

using System.Collections.Generic;
namespace {GenerationContext.BaseNamespace}.Infrastructure.Filters.Abstract
{{
    /// <summary>
    /// An order context is used to build the order by clause of the target query
    /// </summary>
	public interface IOrderContext
    {{
        /// <summary>
        /// Adds an and condition
        /// </summary>
        void And();
        
        /// <summary>
        /// Adds a custom expression
        /// </summary>
        /// <param name=""expression"">Expression to apply</param>
        void Add(string expression);
        
        /// <summary>
        /// Current expressions as a Queue
        /// </summary>
        Queue<string> GetOrderExpressions();
    }}
}}

");

            return E();
        }
    }
}