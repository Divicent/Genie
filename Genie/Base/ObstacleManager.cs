using System;
using System.IO;
using Genie.Base.Abstract;

namespace Genie.Base
{
    internal class ObstacleManager : IObstacleManager
    {
        public void Clear(string basePath, IProcessOutput output)
        {
            output.WriteInformation("Cleaning existing files before creating new files.");

            try
            {
                DeleteIfExists(Path.Combine(basePath, "Dapper"));
                DeleteIfExists(Path.Combine(basePath, "General"));
                DeleteIfExists(Path.Combine(basePath, "Infrastructure"));
                DeleteIfExists(Path.Combine(basePath, "SqlMaker"));
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
                Directory.Delete(path, true);
            }
        }
    }
}
