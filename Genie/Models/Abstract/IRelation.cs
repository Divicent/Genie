using System.Collections.Generic;

namespace Genie.Models.Abstract
{
    /// <summary>
    /// Represents a relation (Table) of a database
    /// </summary>
    internal interface IRelation
    {
        /// <summary>
        /// Name of the relation
        /// </summary>
        string Name { get; set; }

        /// <summary>
        /// List of attributes (columns) of the relation
        /// </summary>
        List<IAttribute> Attributes { get; set; }

        /// <summary>
        /// List of foreign Key attributes of the relation
        /// </summary>
        List<IForeignKeyAttribute> ForeignKeyAttributes { get; set; }
    }
}
