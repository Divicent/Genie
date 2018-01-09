namespace Genie.Core.Base.Versioning.Abstract
{
    /// <summary>
    /// An implementation will help to manage version of the software
    /// </summary>
    public interface IVersionManager
    {
        /// <summary>
        /// Get the current version of the software
        /// </summary>
        /// <returns></returns>
        string GetCurrentVersion();
    }
}