#region Usings

using System;
using System.Collections.Generic;
using Genie.Core.Base.Exceptions;
using Genie.Core.Base.Files.Abstract;
using Genie.Core.Base.ProcessOutput.Abstract;
using Genie.Core.Models.Abstract;

#endregion

namespace Genie.Core.Base.Writing
{
    internal static class DalWriter
    {
        /// <summary>
        ///     Write files to the disk
        /// </summary>
        /// <param name="files">Files to write</param>
        /// <param name="basePath">Base path </param>
        /// <param name="progress">A Progress output</param>
        /// <param name="fileSystem"></param>
        public static void Write(IEnumerable<IContentFile> files, string basePath, IProgressReporter progress, IFileSystem fileSystem)
        {
            try
            {
                var createdDirectories = new HashSet<string>();

                foreach (var contentFile in files)
                {
                    progress.Tick(contentFile.Path);
                    var file = fileSystem.CombinePaths(basePath, contentFile.Path);
                    var directory = fileSystem.GetDirectoryOfAFile(file);

                    if (!createdDirectories.Contains(directory) && directory != null && !fileSystem.DirectoryExists(directory))
                    {
                        fileSystem.CreateDirectory(directory);
                        createdDirectories.Add(directory);
                    }

                    fileSystem.WriteText(contentFile.Content, file);
                }
            }
            catch (Exception e)
            {
                throw new GenieException("Unable to write files to the disc", e);
            }
        }
    }
}