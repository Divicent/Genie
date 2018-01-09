using System.Dynamic;
using Genie.Core.Base.Configuration.Abstract;
using Genie.Core.Base.Files.Abstract;
using Genie.Core.Base.Reading.Abstract;
using Genie.Core.Base.SchemaCaching.Abstract;
using Newtonsoft.Json;

namespace Genie.Core.Base.SchemaCaching.Concrete
{
    public class SchemaCachingManager: ISchemaCachingManager
    {
        private readonly IConfiguration _configuration;
        private readonly IFileSystem _fileSystem;

        public SchemaCachingManager(IConfiguration configuration, IFileSystem fileSystem)
        {
            _configuration = configuration;
            _fileSystem = fileSystem;
        }

        public void SetupCache(IDatabaseSchema schema)
        {
            var pathToSchemaFile = _fileSystem.CombinePaths(_configuration.ProjectPath, "genie-schema.json");
            var write = false;
            if (!_fileSystem.Exists(pathToSchemaFile))
                write = !Validate(pathToSchemaFile, _fileSystem);

            if(write)
                _fileSystem.WriteText(JsonConvert.SerializeObject(ConvertSchemaToSchemaCache(schema)), pathToSchemaFile);
        }

        private static bool Validate(string pathToFile, IFileSystem fileSystem)
        {
            try
            {
                JsonConvert.DeserializeObject(fileSystem.ReadText(pathToFile));
                return true;
            }
            catch
            {
                return false;
            }
        }

        private static object ConvertSchemaToSchemaCache(IDatabaseSchema schema)
        {
            dynamic result = new ExpandoObject();
            return result;
        }
    }
}
