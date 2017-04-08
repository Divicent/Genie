namespace Genie.Base.Abstract
{
    /// <summary>
    /// Validates a configuration object
    /// </summary>
    internal interface IValidatiableConfiguration
    {
        /// <summary>
        /// Validates the configuration object , Implementations should throw appropriate error messages if fails
        /// </summary>
        void Validate();
    }
}
