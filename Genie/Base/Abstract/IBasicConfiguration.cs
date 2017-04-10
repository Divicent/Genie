namespace Genie.Base.Abstract
{
    /// <summary>
    /// Basic configuration for genie
    /// </summary>
    internal interface IBasicConfiguration: IValidatiableConfiguration
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
    }
}
