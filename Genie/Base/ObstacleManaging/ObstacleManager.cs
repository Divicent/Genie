#region Usings

using System;
using Genie.Core.Base.Configuration.Abstract;
using Genie.Core.Base.Exceptions;
using Genie.Core.Base.Files.Abstract;
using Genie.Core.Base.ProcessOutput.Abstract;

#endregion

namespace Genie.Core.Base.ObstacleManaging
{
  /// <summary>
  ///     Helps to clear the target folder before generating
  /// </summary>
  internal static class ObstacleManager
    {
        /// <summary>
        ///     Clears the provided folder
        /// </summary>
        /// <param name="basePath">folder path</param>
        /// <param name="output">a process output to use</param>
        /// <param name="configuration">Configuration</param>
        /// <param name="fileSystem">File system to be used</param>
        public static void Clear(string basePath, IProcessOutput output, IConfiguration configuration, IFileSystem fileSystem)
        {
            output.WriteInformation("Cleaning existing files before creating new files.");

            try
            {
                DeleteIfExists(fileSystem.CombinePaths(basePath, "Dapper"), fileSystem);
                DeleteIfExists(fileSystem.CombinePaths(basePath, "Infrastructure"), fileSystem);
                if (configuration.AbstractModelsEnabled) DeleteDirectory(configuration.AbstractModelsLocation, fileSystem);
            }
            catch (Exception e)
            {
                throw new GenieException("Unable to clear target folder", e);
            }


            output.WriteSuccess("Folder cleared.");
        }

        private static void DeleteIfExists(string path, IFileSystem fileSystem)
        {
            if (fileSystem.Exists(path)) DeleteDirectory(path, fileSystem);
        }

      /// <summary>
      ///     Depth-first recursive delete, with handling for descendant
      ///     directories open in Windows Explorer.
      /// </summary>
      public static void DeleteDirectory(string path, IFileSystem fileSystem)
        {
            foreach (var directory in fileSystem.GetDirectories(path)) DeleteDirectory(directory, fileSystem);

            try
            {
                fileSystem.DeleteDirectory(path, true);
            }
            catch (UnauthorizedAccessException)
            {
                fileSystem.DeleteDirectory(path, true);
            }
            catch
            {
                fileSystem.DeleteDirectory(path, true);
            }
        }
    }
}