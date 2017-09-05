namespace Genie.Base.Configuration.Abstract
{
    /// <summary>
    /// Validates a configuration object
    /// </summary>
    public interface IValidatableConfiguration
    {
        /// <summary>
        /// Validates the configuration object , Implementations should throw appropriate error messages if fails
        /// </summary>
        void Validate();
    }
}
