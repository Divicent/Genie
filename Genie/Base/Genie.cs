using System.IO;
using Genie.Templates.Complex;
using Newtonsoft.Json;

namespace Genie.Base
{
    public class GenieConfiguration
    {
        public string ConnectionString { get; set; }
        public string ProjectPath { get; set; }
        public string BaseNamespace { get; set; }
    }

    public class GenieGenerationResult
    {
        public bool Success { get; set; }
        public string Error { get; set; }    
    }

    public class Genie
    {
        public static GenieGenerationResult Generate(string pathToConfigFile)
        {
            /*Genie will generate the DAL in 4 phases
                01 Read schema 
                02 Parse Schema
                03 Generate File(s)
                04 White File(s)
             */

            var result = new GenieGenerationResult();

            if (!File.Exists(pathToConfigFile))
            {
                result.Error = "Configuration file not found";
                return result;
            }

            GenieConfiguration config;
            try
            {
                config = JsonConvert.DeserializeObject<GenieConfiguration>(File.ReadAllText(pathToConfigFile));
            }
            catch
            {
                result.Error = "Unable to read configuration file";
                return result;
            }

            if (string.IsNullOrWhiteSpace(config.ConnectionString))
                result.Error = "connectionString is not provided";
            else if (string.IsNullOrWhiteSpace(config.BaseNamespace))
                result.Error = "baseNamespace not provided";
            else if (string.IsNullOrWhiteSpace(config.ProjectPath))
                result.Error = "projectPath not provided";

            if (!string.IsNullOrEmpty(result.Error))
            {
                result.Success = false;
                return result;
            }

            var schema = Reader.Read(config);
            var model = Parser.Parse(schema, config);
            var content = new DA().TransformText(model);
            Writer.Write(content, config.ProjectPath + "\\DA.cs");
            result.Success = true;
            return result;

        }
    }
}
