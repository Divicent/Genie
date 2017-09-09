
using System.Collections.Generic;
using Genie.Base.Configuration.Concrete;

namespace Genie.Base.Configuration.Abstract
{
    /// <summary>
    /// Basic configuration for genie
    /// </summary>
    internal interface IConfiguration : IValidatableConfiguration
    {
        /// <summary>
        /// Open able , accessible connection string to the target database 
        /// </summary>
        string ConnectionString { get; }

        /// <summary>
        ///Path to the DAL layer of the target solution / project 
        /// <para/>
        /// This should point to the Data access layer , not to the project path 
        /// </summary>
        string ProjectPath { get; }

        /// <summary>
        /// Base namespace of the data access layer usually, @projectName.DA | @projectName.DataAccess or something like that. choice is yours ;)
        /// </summary>
        string BaseNamespace { get; }

        /// <summary>
        /// Relative path to the project file
        /// </summary>
        string ProjectFile { get; }

        /// <summary>
        /// Should integrate dapper code ? if false dapper should be referenced externally
        /// </summary>
        bool NoDapper { get; }

        /// <summary>
        /// Is for a core environment (.net core)
        /// </summary>
        bool Core { get; }

        /// <summary>
        /// List of enum table definitions
        /// </summary>
        List<ConfigurationEnumTable> Enums { get; }

        /// <summary>
        /// Name of the Database Management System
        /// </summary>
        string DBMS { get; }
    }
}
