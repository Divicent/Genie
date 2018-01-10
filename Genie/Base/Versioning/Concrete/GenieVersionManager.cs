using Genie.Core.Base.Exceptions;
using Genie.Core.Base.Files.Abstract;
using Genie.Core.Base.Versioning.Abstract;

namespace Genie.Core.Base.Versioning.Concrete
{
    public class GenieVersionManager : IVersionManager
    {
        private readonly IFileSystem _fileSystem;

        public GenieVersionManager(IFileSystem fileSystem)
        {
            _fileSystem = fileSystem;
        }

        public string GetCurrentVersion()
        {
            var assembliLocation = _fileSystem.GetCurrentAssemblyLocation();
            var versionFileLocation = _fileSystem.CombinePaths(assembliLocation, ".version");

            if (!_fileSystem.Exists(versionFileLocation))
                throw new GenieException($".version file not found in the location {assembliLocation}");

            return _fileSystem.ReadText(versionFileLocation);
        }
    }
}