using System;
using System.IO;
using Genie.Base.Abstract;
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
            IBasicConfiguration config = null;

            try
            {
                output.WriteInformation("Checking configuration file.");

                if(!File.Exists(pathToConfiguraionJsonFile))
                    throw new Exception(string.Format("The configuration file ({0}) could not be found (File.Exists returned false).", pathToConfiguraionJsonFile));

                output.WriteSuccess("Configuration file found, ready to read.");

                output.WriteInformation("Deserializing configuration file.");

                try
                {
                    config = JsonConvert.DeserializeObject<GenieBasicConfiguration>(File.ReadAllText(pathToConfiguraionJsonFile));
                }
                catch (Exception exception)
                {
                    throw new Exception(string.Format("Unable to deserialize the configuration file ({0}), the configuration file may have syntax errors.",exception.Message));
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

            if (!string.IsNullOrEmpty(result.Error))
            {
                result.Success = false;
                return result;
            }
            IDatabaseSchemaReader schemaReader = new DatabaseSchemaReader();
            var schema = schemaReader.Read(config, output);
            //var schema = Reader.Read(config);
            //var model = Parser.Parse(schema, config);
            //var content = new DA().TransformText(model);
            //Writer.Write(content, config.ProjectPath + "\\DA.cs");
            //result.Success = true;
            return result;

        }
    }
}
