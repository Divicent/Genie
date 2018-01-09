#region Usings

using System.Collections.Generic;
using Genie.Core.Base.Configuration.Concrete;

#endregion

namespace Genie.Core.Base.Configuration.Abstract
{
  /// <summary>
  ///     Set of supported Database Management Systems
  /// </summary>
  public enum DBMS
    {
        MSSQL = 1,
        MySQL = 2
    }

  /// <inheritdoc />
  /// <summary>
  ///     Basic configuration for genie
  /// </summary>
  public interface IConfiguration : IValidatableConfiguration
    {
      /// <summary>
      ///     Open able , accessible connection string to the target database
      /// </summary>
      string ConnectionString { get; }

      /// <summary>
      ///     Path to the DAL layer of the target solution / project
      ///     <para />
      ///     This should point to the Data access layer , not to the project path
      /// </summary>
      string ProjectPath { get; }

      /// <summary>
      ///     Base namespace of the data access layer usually, @projectName.DA | @projectName.DataAccess or something like that.
      ///     choice is yours ;)
      /// </summary>
      string BaseNamespace { get; }

      /// <summary>
      ///     Relative path to the project file
      /// </summary>
      string ProjectFile { get; }

      /// <summary>
      ///     Should integrate dapper code ? if false dapper should be referenced externally
      /// </summary>
      bool NoDapper { get; }

      /// <summary>
      ///     Is for a core environment (.net core)
      /// </summary>
      bool Core { get; }

      /// <summary>
      ///     List of enum table definitions
      /// </summary>
      List<ConfigurationEnumTable> Enums { get; }

      /// <summary>
      ///     Name of the Database Management System
      /// </summary>
      string DBMS { get; }

      /// <summary>
      ///     Default database schema name
      /// </summary>
      string Schema { get; }

      /// <summary>
      ///     Optional Location to generate abstract models
      /// </summary>
      /// <returns></returns>
      string AbstractModelsLocation { get; set; }

      /// <summary>
      ///     Namespace of the abstract models
      /// </summary>
      /// <returns></returns>
      string AbstractModelsNamespace { get; set; }

      /// <summary>
      ///     Check whether the abstract models are enabled
      /// </summary>
      /// <returns></returns>
      bool AbstractModelsEnabled { get; }

      /// <summary>
      ///     Current version of genie
      /// </summary>
      string GenieVersion { get; set; }

      /// <summary>
      ///     Setup the configuration
      /// </summary>
      void Setup();
    }
}