using System;
using System.IO;
using System.Linq;
using Genie.Base.Abstract;
using Genie.Base.Configuration.Abstract;
using Genie.Base.Configuration.Concrete;
using Genie.Base.Reader.Abstract;
using Genie.Base.Reader.Concrete;
using Genie.Tools;
using Newtonsoft.Json;

namespace Genie.Base
{
    /// <summary>
    /// The base genie class that only provides a static method to generate
    /// </summary>
    public static class Genie
    {
        /// <summary>
        /// Generates the Data Access Layer using given configuration file
        /// </summary>
        /// <param name="pathToConfiguraionJsonFile">the full readable path to the configuration JSON file.</param>
        /// <param name="output">Just implement your own one and pass it here or leave it null if you don't need a detailed output</param>
        /// <returns>the result of the generation process</returns>
        public static GenieGenerationResult Generate(string pathToConfiguraionJsonFile, IProcessOutput output = null)
        {
            if(output == null)
                output = new NonFunctioningProcessOutput();

            IMessageFormatter exceptionFormatter = new GenieExceptionMessageFormatter();

            var result = new GenieGenerationResult();
            IConfiguration config = null;

            try
            {
                output.WriteInformation("Checking configuration file.");

                if(!File.Exists(pathToConfiguraionJsonFile))
                    throw new Exception($"The configuration file ({pathToConfiguraionJsonFile}) could not be found (File.Exists returned false).");

                output.WriteSuccess("Configuration file found, ready to read.");

                output.WriteInformation("Deserializing configuration file.");

                try
                {
                    config = JsonConvert.DeserializeObject<GenieConfiguration>(File.ReadAllText(pathToConfiguraionJsonFile));
                }
                catch (Exception exception)
                {
                    throw new Exception($"Unable to deserialize the configuration file ({exception.Message}), the configuration file may have syntax errors.");
                }

                output.WriteSuccess("Successfully Deserialized the configuration file.");

                output.WriteInformation("Validating configuration.");

                config.Validate();

                output.WriteSuccess("Configuration validated successfully.");

            }
            catch (Exception exception)
            {
                result.Error = exceptionFormatter.FormatException(exception,
                    "En error occurred while trying to initialize generation process (probably a configuration error)");
            }

            if (!string.IsNullOrEmpty(result.Error) || config ==  null)
            {
                result.Success = false;
                return result;
            }

            try
            {
                IDatabaseSchemaReader schemaReader = new DatabaseSchemaReader();
                var schema = schemaReader.Read(config, output);

                IDalGenerator dalGenerator = new DalGenerator();
                var contentFiles = dalGenerator.Generate(schema, config, output);

                IObstacleManager obstacleManager = new ObstacleManager();
                obstacleManager.Clear(config.ProjectPath, output);

                IFileWriter writer = new DalWriter();
                writer.Write(contentFiles, config.ProjectPath, output);

                if (!string.IsNullOrWhiteSpace(config.ProjectFile))
                {
                    IProjectItemManager projectItemManager = new CSharpProjectItemManager();
                    projectItemManager.Process(Path.Combine(config.ProjectPath, config.ProjectFile), contentFiles.Select(c => c.Path).ToList(), output);
                }

                output.WriteSuccess("Successfully completed.");

            }
            catch (Exception e)
            {
                result.Error = exceptionFormatter.FormatException(e.InnerException, e.Message);
                result.Success = false;
                return result;
            }

            return result;
        }
    }
}
