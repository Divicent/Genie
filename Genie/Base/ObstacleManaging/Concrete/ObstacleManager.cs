using System;
using System.IO;
using Genie.Base.ObstacleManaging.Abstract;
using Genie.Base.ProcessOutput.Abstract;

namespace Genie.Base.ObstacleManaging.Concrete
{
    internal class ObstacleManager : IObstacleManager
    {
        public void Clear(string basePath, IProcessOutput output)
        {
            output.WriteInformation("Cleaning existing files before creating new files.");

            try
            {
                DeleteIfExists(Path.Combine(basePath, "Dapper"));
                DeleteIfExists(Path.Combine(basePath, "Infrastructure"));
            }
            catch (Exception e)
            {
                throw new Exception("Unable to clear target folder", e);
            }
            

            output.WriteSuccess("Folder cleared.");
        }

        private static void DeleteIfExists(string path)
        {
            if (Directory.Exists(path))
            {
                DeleteDirectory(path);
            }
        }

        /// <summary>
        /// Depth-first recursive delete, with handling for descendant 
        /// directories open in Windows Explorer.
        /// </summary>
        public static void DeleteDirectory(string path)
        {
            foreach (var directory in Directory.GetDirectories(path))
                DeleteDirectory(directory);
            try
            {
                Directory.Delete(path, true);
            }
            catch (IOException)
            {
                Directory.Delete(path, true);
            }
            catch (UnauthorizedAccessException)
            {
                Directory.Delete(path, true);
            }
        }
    }
}
