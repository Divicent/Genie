using System.Collections.Generic;
using Genie.Core.Base.Files.Abstract;
using System.IO;
using System.Reflection;

namespace Genie.Core.Base.Files.Concrete
{
    internal class GenieFileSystem: IFileSystem
    {
        public string ReadText(string filePath) => 
            File.ReadAllText(filePath);
        public bool Exists(string filePath) => File.Exists(filePath);
        public string CombinePaths(string partOne, string partTwo) => Path.Combine(partOne, partTwo);
        public void WriteText(string content, string pathToFile) => File.WriteAllText(pathToFile, content);
        public bool DirectoryExists(string directoryPath) => Directory.Exists(directoryPath);
        public void CreateDirectory(string path) => Directory.CreateDirectory(path);
        public string GetDirectoryOfAFile(string filePath) => new FileInfo(filePath).Directory.FullName;
        public IEnumerable<string> GetDirectories(string directoryPath) => Directory.GetDirectories(directoryPath);
        public void DeleteDirectory(string path, bool recursive) => Directory.Delete(path, recursive);
        public string GetCurrentAssemblyLocation() => Path.GetDirectoryName(Assembly.GetEntryAssembly().Location);
    }
}
