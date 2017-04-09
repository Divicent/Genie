using Genie.Base.Abstract;
using Genie.Models.Abstract;

namespace Genie.Base
{
    /// <summary>
    /// Holds values that are needed for content generation
    /// </summary>
    internal static class GenerationContext
    {
        /// <summary>
        /// Current database schema
        /// </summary>
        internal static IDatabaseSchema Schema { get; set; }
       
        /// <summary>
        /// Current basic configuration
        /// </summary>
        internal static IBasicConfiguration Configuration { get; set; }
        
        /// <summary>
        /// Current base namespace of the schema
        /// </summary>
        internal static string BaseNamespace { get { return Schema.BaseNamespace; } }

        /// <summary>
        /// Currently generating relation
        /// </summary>
        internal static IRelation CurrentRelation { get; set; }

        /// <summary>
        /// Currently generating view
        /// </summary>
        internal static IView CurrentView { get; set; }
    }
}
