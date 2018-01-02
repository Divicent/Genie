#region Usings

using System;
using System.IO;
using Genie.Core.Base.Configuration.Abstract;
using Genie.Core.Base.Exceptions;
using Genie.Core.Base.ProcessOutput.Abstract;

#endregion

namespace Genie.Core.Base.ObstacleManaging
{
  /// <summary>
  ///     Helps to clear the target folder before generating
  /// </summary>
  internal static class ObstacleManager
  {

    ///<summary>
    ///     Clears the provided folder
    /// </summary>
    /// <param name="basePath">folder path</param>
    /// <param name="output">a process output to use</param>
    public static void Clear(string basePath, IProcessOutput output, IConfiguration configuration)
    {
      output.WriteInformation("Cleaning existing files before creating new files.");

      try
      {
        DeleteIfExists(Path.Combine(basePath, "Dapper"));
        DeleteIfExists(Path.Combine(basePath, "Infrastructure"));
        if (configuration.AbstractModelsEnabled)
        {
          DeleteDirectory(configuration.AbstractModelsLocation);
        }

      }
      catch (Exception e)
      {
        throw new GenieException("Unable to clear target folder", e);
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
    ///     Depth-first recursive delete, with handling for descendant
    ///     directories open in Windows Explorer.
    /// </summary>
    public static void DeleteDirectory(string path)
    {
      foreach (var directory in Directory.GetDirectories(path))
      {
        DeleteDirectory(directory);
      }

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