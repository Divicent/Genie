namespace Genie.Models.Abstract
{
    /// <summary>
    /// Represents a foreign key attribute of a relation
    /// </summary>
    internal interface IForeignKeyAttribute
    {
        /// <summary>
        /// Non foreign key attribute related to this
        /// </summary>
        IAttribute ReferencingNonForeignKeyAttribute { get; set; }

        /// <summary>
        /// Name of the referencing relation ( table )
        /// </summary>
        string ReferencingRelationName { get; set; }

        /// <summary>
        /// The column that is referred in the foreign table
        /// </summary>
        string ReferencingTableColumnName { get; set; }
    }
}
