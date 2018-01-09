using System;
using System.Dynamic;
using Genie.Core.Base.Configuration.Abstract;
using Genie.Core.Base.Files.Abstract;
using Genie.Core.Base.Reading.Abstract;
using Genie.Core.Base.SchemaCaching.Abstract;
using Genie.Core.Models.Abstract;
using Genie.Core.Models.Abstract.SchemaCaching;
using Genie.Core.Models.Concrete.SchemaCaching;
using Genie.Core.Tools;
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

        public object ConvertSchemaToSchemaCache(IDatabaseSchema schema)
        {
            string CreateHash(object obj) => CommonTools.CalculateMd5Hash(JsonConvert.SerializeObject(obj));
            
            dynamic result = new ExpandoObject();
            result.root = CreateHash(CreateRootObject());
            return null;
        }
        

        public ISchemaCacheRootObject CreateRootObject()
        {
            return new SchemaCacheRootObject
            {
                BaseNamespace = _configuration.BaseNamespace,
                GenieVersion = _configuration.GenieVersion,
                IsCore = _configuration.Core,
                NoDapper = _configuration.NoDapper,
                Schema = _configuration.Schema
            };
        }

        public IModelCacheObject CreateModelCacheObject(IModel model)
        {
            throw new NotImplementedException();
        }
    }
}
