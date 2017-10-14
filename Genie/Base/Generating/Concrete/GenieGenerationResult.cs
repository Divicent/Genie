namespace Genie.Core.Base.Generating.Concrete
{
    /// <summary>
    ///     Result of the process of generation
    /// </summary>
    public class GenieGenerationResult
    {
        /// <summary>
        ///     Was the process successful or not
        /// </summary>
        public bool Success { get; set; }

        /// <summary>
        ///     Formatted error message if the process failed
        /// </summary>
        public string Error { get; set; }
    }
}