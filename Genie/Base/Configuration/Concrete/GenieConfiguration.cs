#region Usings

using System;
using System.Collections.Generic;
using System.Text;
using Genie.Core.Base.Configuration.Abstract;
using Genie.Core.Base.Exceptions;

#endregion


namespace Genie.Core.Base.Configuration.Concrete
{
    /// <inheritdoc />
    /// <summary>
    ///     Contains configurations that are need to do the data access layer generation
    /// </summary>
    public class GenieConfiguration : IConfiguration
    {
       // private readonly IVersionManager _versionManager;

//        public GenieConfiguration(IVersionManager versionManager)
//        {
//          //  _versionManager = versionManager;
//        }

        public string ConnectionString { get; set; }
        public string ProjectPath { get; set; }
        public string BaseNamespace { get; set; }
        public List<ConfigurationEnumTable> Enums { get; set; }
        public string DBMS { get; set; }
        public string Schema { get; set; }
        public string ProjectFile { get; set; }
        public string AbstractModelsLocation { get; set; }
        public string AbstractModelsNamespace { get; set; }

        public bool AbstractModelsEnabled => !string.IsNullOrWhiteSpace(AbstractModelsLocation) &&
                                             !string.IsNullOrWhiteSpace(AbstractModelsNamespace);

        public string GenieVersion { get; set; }

        public void Validate()
        {
            var error = new StringBuilder();
            if (string.IsNullOrWhiteSpace(ConnectionString))
                error.AppendLine("ConnectionString (connectionString in JSON) not found in the configuration");

            if (string.IsNullOrWhiteSpace(ProjectPath))
                error.AppendLine("ProjectPath (projectPath in JSON) not found in the configuration");

            if (string.IsNullOrWhiteSpace(BaseNamespace))
                error.AppendLine("BaseNamespace (baseNamespace in JSON) not found in the configuration file");

            if (string.IsNullOrWhiteSpace(DBMS))
                error.AppendLine("DBMS (dbms in JSON) not found in the configuration file");

            if (string.IsNullOrWhiteSpace(Schema))
                error.AppendLine("Schema (schema in JSON) not found in the configuration file");
            else
                switch (DBMS.ToLowerInvariant())
                {
                    case "mssql":
                    case "mysql":
                        break;
                    default:
                        error.AppendLine(
                            $"DBMS name {DBMS} is not supported values are mssql(Microsoft SQL Server) and mysql (MySQL)");
                        break;
                }

            if (Enums != null && Enums.Count > 0)
                foreach (var configurationEnumTable in Enums)
                    try
                    {
                        configurationEnumTable.Validate();
                    }
                    catch (Exception e)
                    {
                        error.AppendLine(e.Message);
                    }

            if (error.Length > 0) throw new GenieException(error.ToString());
        }

        public void Setup()
        {
            // GenieVersion = _versionManager.GetCurrentVersion();
        }
    }
}
