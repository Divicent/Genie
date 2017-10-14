namespace Genie.Core.Base.Generating.Concrete
{
    /// <summary>
    ///     Holds values that are needed for content generation
    /// </summary>
    internal static class GenerationContext
    {
        /// <summary>
        ///     Current base namespace of the schema
        /// </summary>
        internal static string BaseNamespace { get; set; }

        /// <summary>
        ///     Is for Net Core
        /// </summary>
        internal static bool Core { get; set; }

        /// <summary>
        ///     Internal dapper or not
        /// </summary>
        internal static bool NoDapper { get; set; }
    }
}