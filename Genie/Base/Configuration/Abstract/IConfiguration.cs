
namespace Genie.Base.Configuration.Abstract
{
    /// <summary>
    /// Basic configuration for genie
    /// </summary>
    internal interface IConfiguration: IValidatiableConfiguration
    {
        /// <summary>
        /// Open able , accessible connection string to the target database 
        /// </summary>
        string ConnectionString { get; set; }

        /// <summary>
        ///Path to the DAL layer of the target solution / project 
        /// <para/>
        /// This should point to the Data access layer , not to the project path 
        /// </summary>
        string ProjectPath { get; set; }

        /// <summary>
        /// Base namespace of the data access layer usually, @projectName.DA | @projectName.DataAccess or something like that. choice is yours ;)
        /// </summary>
        string BaseNamespace { get; set; }

        /// <summary>
        /// Relative path to the project file
        /// </summary>
        string ProjectFile { get; set; }

        /// <summary>
        /// Should integrate dapper code ? if false dapper should be referenced externally
        /// </summary>
        bool NoDapper { get; set; }

        /// <summary>
        /// Is for a core environment (.net core)
        /// </summary>
        bool Core { get; set; }


    }
}
