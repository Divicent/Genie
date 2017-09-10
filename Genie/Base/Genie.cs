using System;
using System.IO;
using System.Linq;
using Genie.Base.Configuration.Abstract;
using Genie.Base.Configuration.Concrete;
using Genie.Base.Exceptions;
using Genie.Base.Generating.Absract;
using Genie.Base.Generating.Concrete;
using Genie.Base.ObstacleManaging.Abstract;
using Genie.Base.ObstacleManaging.Concrete;
using Genie.Base.ProcessOutput;
using Genie.Base.ProcessOutput.Abstract;
using Genie.Base.ProjectFileManaging.Abstract;
using Genie.Base.ProjectFileManaging.Concrete;
using Genie.Base.Reading.Abstract;
using Genie.Base.Reading.Concrete;
using Genie.Base.Writing.Abstract;
using Genie.Base.Writing.Concrete;
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
        /// <param name="pathToConfigurationJsonFile">the full readable path to the configuration JSON file.</param>
        /// <param name="output">Just implement your own one and pass it here or leave it null if you don't need a detailed output</param>
        /// <returns>the result of the generation process</returns>
        public static GenieGenerationResult Generate(string pathToConfigurationJsonFile, IProcessOutput output = null)
        {
            if(output == null)
                output = new NonFunctioningProcessOutput();

            IMessageFormatter exceptionFormatter = new GenieExceptionMessageFormatter();

            var result = new GenieGenerationResult();
            IConfiguration config = null;

            try
            {
                output.WriteInformation("Checking configuration file.");

                // if(!File.Exists(pathToConfigurationJsonFile))
                //     throw new GenieException($"The configuration file ({pathToConfigurationJsonFile}) could not be found (File.Exists returned false).");

                output.WriteSuccess("Configuration file found, ready to read.");

                output.WriteInformation("Deserializing configuration file.");

                try
                {
                    config = JsonConvert.DeserializeObject<GenieConfiguration>(@"{
    ""connectionString"": ""Server=localhost;Database=employees;Uid=root;Pwd=password;"",
    ""projectPath"": ""/home/rusith/Documents/Sandbox/Genie/DALTest.DAL"",
    ""baseNamespace"": ""DALTes.DAL"",
    ""ProjectFile"": ""DALTest.DAL.csproj"",
    ""noDapper"": false,
    ""core"": true,
    ""dbms"": ""mysql"",
    ""schema"": ""employees""
}");
                }
                catch (Exception exception)
                {
                    throw new GenieException($"Unable to deserialize the configuration file ({exception.Message}), the configuration file may have syntax errors.");
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
                IDatabaseSchemaReaderFactory databaseSchemaReaderFactory = new DatabaseSchemaReaderFactory();

                IDatabaseSchemaReader schemaReader = databaseSchemaReaderFactory.GetReader(config.DBMS);
                var schema = schemaReader.Read(config, output);

                IDalGenerator dalGenerator = new DalGenerator();
                var contentFiles = dalGenerator.Generate(schema, config, output).ToList();

                IObstacleManager obstacleManager = new ObstacleManager();
                obstacleManager.Clear(config.ProjectPath, output);

                IFileWriter writer = new DalWriter();
                writer.Write(contentFiles, config.ProjectPath, output);

                if (!string.IsNullOrWhiteSpace(config.ProjectFile) && !config.Core)
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
