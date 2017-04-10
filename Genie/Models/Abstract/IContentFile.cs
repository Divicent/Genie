namespace Genie.Models.Abstract
{
    /// <summary>
    /// Result entity from database generation
    /// </summary>
    public interface IContentFile
    {
        /// <summary>
        /// Content of the file
        /// </summary>
        string Content { get; set; }

        /// <summary>
        /// Relative path of the file to the base path
        /// </summary>
        string Path { get; set; }
    }
}
