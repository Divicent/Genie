namespace Genie.Core.Base.Generating
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
        ///     Internal dapper or not
        /// </summary>
        internal static bool NoDapper { get; set; }
    }
}