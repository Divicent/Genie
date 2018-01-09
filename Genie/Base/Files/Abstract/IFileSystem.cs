using System.Collections.Generic;

namespace Genie.Core.Base.Files.Abstract
{
    /// <summary>
    /// Helps to interact with the file system
    /// </summary>
    public interface IFileSystem
    {
        /// <summary>
        /// Read all text of a file
        /// </summary>
        /// <param name="filePath">Path to the file</param>
        /// <returns>Content of the file</returns>
        string ReadText(string filePath);

        /// <summary>
        /// Can be used to check whether a file exists or not
        /// </summary>
        /// <param name="filePath">Path to the target file</param>
        /// <returns>true if exists otherwise false</returns>
        bool Exists(string filePath);

        /// <summary>
        /// Combine two file path parts
        /// </summary>
        /// <param name="partOne">Part one</param>
        /// <param name="partTwo">Part two</param>
        /// <returns>The combined path</returns>
        string CombinePaths(string partOne, string partTwo);

        /// <summary>
        /// Will write given content to the given file
        /// </summary>
        /// <param name="content">Content to write</param>
        /// <param name="pathToFile">Path to target file</param>
        void WriteText(string content, string pathToFile);

        /// <summary>
        /// Can be used to check whether the a directory exists or not
        /// </summary>
        /// <param name="directoryPath">Path to directory</param>
        /// <returns>true if exists , otherwise false</returns>
        bool DirectoryExists(string directoryPath);

        /// <summary>
        /// Create provided directory path
        /// </summary>
        /// <param name="path">Path to target directory</param>
        void CreateDirectory(string path);

        /// <summary>
        /// Can be used to get full directory path of the file
        /// </summary>
        /// <param name="filePath">Path to file</param>
        /// <returns>Directory path to file</returns>
        string GetDirectoryOfAFile(string filePath);

        /// <summary>
        /// Can be used to get sub directories of a directory
        /// </summary>
        /// <param name="directoryPath">Path to root directory</param>
        /// <returns>List of directory paths</returns>
        IEnumerable<string> GetDirectories(string directoryPath);

        /// <summary>
        /// Can be used to delete a directory
        /// </summary>
        /// <param name="path">Path to directory</param>
        /// <param name="recursive">Is recursive delete</param>
        void DeleteDirectory(string path, bool recursive);

        /// <summary>
        /// Get current location of the application
        /// </summary>
        string GetCurrentAssemblyLocation();
    }
}
