#region Usings

#endregion

using Genie.Core.Base.Generating;

namespace Genie.Core.Templates.Infrastructure.Filters.Abstract
{
    public class IFilterContextTemplate : GenieTemplate
    {
        public IFilterContextTemplate(string path) : base(path)
        {
        }

        public override string Generate()
        {
            L($@"

using System.Collections.Generic;

namespace {GenerationContext.BaseNamespace}.Infrastructure.Filters.Abstract
{{
  
   /// <summary>
   /// A filter context is used to build the where clause of the target query
   /// </summary>
	public interface IFilterContext
	{{

        /// <summary>
        /// Current expressions as a Queue
        /// </summary>
		Queue<string> Expressions {{ get; set; }}
		
        /// <summary>
        /// Adds an and condition
        /// </summary>
        void And();

        /// <summary>
        /// Adds an or condition
        /// </summary>
		void Or();
		
        /// <summary>
        /// Adds a custom expression
        /// </summary>
        /// <param name=""expression"">Expression to apply</param>
        void Add(string expression);

        /// <summary>
        /// Current expressions as a Queue
        /// </summary>
		Queue<string> GetFilterExpressions();

        /// <summary>
        /// Starts a scope inside the query (paranthezes)
        /// </summary>
        void StartScope();

        /// <summary>
        /// Ends a scope inside the query (paranthezes)
        /// </summary>
        void EndScope();
	
  }}
}}
");

            return E();
        }
    }
}