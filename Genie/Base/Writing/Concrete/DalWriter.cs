using System;
using System.Collections.Generic;
using System.IO;
using Genie.Base.Abstract;
using Genie.Base.Writing.Abstract;
using Genie.Models.Abstract;

namespace Genie.Base.Writing.Concrete
{
    internal class DalWriter : IFileWriter
    {
        public void Write(List<IContentFile> files, string basePath, IProcessOutput output)
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
                throw new Exception("Unable to write files to the disc", e);
            }

            output.WriteSuccess(string.Format("Successfully written {0} file to the disk.", files.Count));
        }
    }
}
