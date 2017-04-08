
namespace Genie.Base.Abstract
{
    /// <summary>
    /// Used to give output to the generator user
    /// </summary>
    public interface IProcessOutput
    {
        /// <summary>
        /// Writes a warning message to the output
        /// </summary>
        /// <param name="content"></param>
        void WriteWarning(string content);

        /// <summary>
        /// Writes a success message to the output
        /// </summary>
        /// <param name="content">Content to write</param>
        void WriteSuccess(string content);

        /// <summary>
        /// Writes an information message to the output
        /// </summary>
        /// <param name="content">Content to write</param>
        void WriteInformation(string content);
    }
}
