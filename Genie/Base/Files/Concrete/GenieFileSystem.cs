using System.Collections.Generic;
using System.IO;
using System.Reflection;
using Genie.Core.Base.Files.Abstract;

namespace Genie.Core.Base.Files.Concrete
{
    internal class GenieFileSystem : IFileSystem
    {
        public string ReadText(string filePath)
        {
            return File.ReadAllText(filePath);
        }

        public bool Exists(string filePath)
        {
            return File.Exists(filePath);
        }

        public string CombinePaths(string partOne, string partTwo)
        {
            return Path.Combine(partOne, partTwo);
        }

        public void WriteText(string content, string pathToFile)
        {
            File.WriteAllText(pathToFile, content);
        }

        public bool DirectoryExists(string directoryPath)
        {
            return Directory.Exists(directoryPath);
        }

        public void CreateDirectory(string path)
        {
            Directory.CreateDirectory(path);
        }

        public string GetDirectoryOfAFile(string filePath)
        {
            return new FileInfo(filePath).Directory.FullName;
        }

        public IEnumerable<string> GetDirectories(string directoryPath)
        {
            return Directory.GetDirectories(directoryPath);
        }

        public void DeleteDirectory(string path, bool recursive)
        {
            Directory.Delete(path, recursive);
        }

        public string GetCurrentAssemblyLocation()
        {
            return Path.GetDirectoryName(Assembly.GetEntryAssembly().Location);
        }
    }
}