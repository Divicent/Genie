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
                Directory.Delete(Path.Combine(basePath, "Dapper"));
                Directory.Delete(Path.Combine(basePath, "General"));
                Directory.Delete(Path.Combine(basePath, "Infrastructure"));
                Directory.Delete(Path.Combine(basePath, "SqlMaker"));
            }
            catch (Exception e)
            {
                throw new Exception("Unable to clear target folder", e);
            }
            

            output.WriteSuccess("Folder cleared.");
        }
    }
}
