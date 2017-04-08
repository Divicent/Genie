using System;
using Genie.Base.Abstract;

namespace Genie.Base
{
    /// <summary>
    /// Contains configurations that are need to do the data access layer generation
    /// </summary>
    public class GenieBasicConfiguration : IValidatiableConfiguration
    {
        /// <summary>
        /// Open able , accessible connection string to the target database 
        /// </summary>
        public string ConnectionString { get; set; }
        
        /// <summary>
        ///Path to the DAL layer of the target solution / project 
        /// <para/>
        /// This should point to the Data access layer , not to the project path 
        /// </summary>
        public string ProjectPath { get; set; }

        /// <summary>
        /// Base namespace of the data access layer usually, @projectName.DA | @projectName.DataAccess or something like that. choice is yours ;)
        /// </summary>
        public string BaseNamespace { get; set; }

        public void Validate()
        {
            string error = null;
            if (string.IsNullOrWhiteSpace(ConnectionString))
                error = "ConnectionString (connectionString in JSON) not found in the configuration";
            else if (string.IsNullOrWhiteSpace(ProjectPath))
                error = "ProjectPath (projectPath in JSON) not found in the configuration";
            else if (string.IsNullOrWhiteSpace(BaseNamespace))
                error = "BaseNamespace (baseNamespace in JSON) not found in the configuration file";

            if(error != null)
                throw new Exception(error);
        }
    }
}
