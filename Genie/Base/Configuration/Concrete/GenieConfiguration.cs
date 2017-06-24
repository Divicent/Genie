using System;
using System.Collections.Generic;
using System.Text;
using Genie.Base.Configuration.Abstract;

namespace Genie.Base.Configuration.Concrete
{
    /// <summary>
    /// Contains configurations that are need to do the data access layer generation
    /// </summary>
    public class GenieConfiguration : IConfiguration
    {
        public string ConnectionString { get; set; }

        public string ProjectPath { get; set; }

        public string BaseNamespace { get; set; }

        public bool NoDapper { get; set; }

        public bool Core { get; set; }
        public List<IConfigurationEnumTable> Enums { get; set; }

        public void Validate()
        {
            var error = new StringBuilder();
            if (string.IsNullOrWhiteSpace(ConnectionString))
                error.AppendLine("ConnectionString (connectionString in JSON) not found in the configuration");
            if (string.IsNullOrWhiteSpace(ProjectPath))
                error.AppendLine("ProjectPath (projectPath in JSON) not found in the configuration");
            if (string.IsNullOrWhiteSpace(BaseNamespace))
                error.AppendLine("BaseNamespace (baseNamespace in JSON) not found in the configuration file");
            if (Enums != null && Enums.Count > 0)
            {
                foreach (var configurationEnumTable in Enums)
                {
                    try
                    {
                        configurationEnumTable.Validate();
                    }
                    catch (Exception e)
                    {
                        error.AppendLine(e.Message);
                    }
                }
            }
            if(error.Length > 0)
                throw new Exception(error.ToString());
        }

        public string ProjectFile { get; set; }
    }
}
