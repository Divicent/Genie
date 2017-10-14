#region Usings

using System;
using System.Collections.Generic;
using System.IO;
using Genie.Core.Base.Exceptions;
using Genie.Core.Base.ProcessOutput.Abstract;
using Genie.Core.Models.Abstract;

#endregion

namespace Genie.Core.Base.Writing
{
    internal class DalWriter
    {
        /// <summary>
        ///     Write files to the disk
        /// </summary>
        /// <param name="files">Files to write</param>
        /// <param name="basePath">Base path </param>
        /// <param name="output">A Process output</param>
        public static void Write(List<IContentFile> files, string basePath, IProcessOutput output)
        {
            output.WriteInformation("Writing files to the disk");
            output.WriteInformation("Base path = " + basePath);

            try
            {
                var createdDirectories = new HashSet<string>();

                foreach (var contentFile in files)
                {
                    output.WriteInformation(string.Format("Writing {0}.", contentFile.Path));
                    var file = new FileInfo(Path.Combine(basePath, contentFile.Path));
                    var directory = file.Directory?.FullName;

                    if (!createdDirectories.Contains(directory) && directory != null && !Directory.Exists(directory))
                    {
                        Directory.CreateDirectory(directory);
                        createdDirectories.Add(directory);
                    }
                    File.WriteAllText(file.FullName, contentFile.Content);
                }
            }
            catch (Exception e)
            {
                throw new GenieException("Unable to write files to the disc", e);
            }

            output.WriteSuccess(string.Format("Successfully written {0} file to the disk.", files.Count));
        }
    }
}