#region Usings

using System.Collections.Generic;

#endregion

namespace Genie.Core.Models.Abstract
{
  /// <summary>
  ///     Represents a relation (Table) of a database
  /// </summary>
  internal interface IRelation : IModel
  {
    /// <summary>
    ///     Name of the relation
    /// </summary>
    string Name { get; set; }

    /// <summary>
    ///     List of attributes (columns) of the relation
    /// </summary>
    List<IAttribute> Attributes { get; set; }

    /// <summary>
    ///     List of foreign Key attributes of the relation
    /// </summary>
    List<IForeignKeyAttribute> ForeignKeyAttributes { get; set; }

    /// <summary>
    ///     List of reference collections
    /// </summary>
    List<IReferenceList> ReferenceLists { get; set; }

    /// <summary>
    ///     The Field Name
    /// </summary>
    string FieldName { get; set; }

    /// <summary>
    ///     Comment of the table if available
    /// </summary>
    string Comment { get; set; }
  }
}