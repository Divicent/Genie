namespace Genie.Core.Models.Abstract
{
    /// <summary>
    ///     A reference from one relation to another (one to many)
    /// </summary>
    public interface IReferenceList
    {
        /// <summary>
        ///     Referenced Relation Name
        /// </summary>
        string ReferencedRelationName { get; set; }

        /// <summary>
        ///     Referenced property name
        /// </summary>
        string ReferencedPropertyName { get; set; }

        /// <summary>
        ///     Referenced Property on this relation
        /// </summary>
        string ReferencedPropertyOnThisRelation { get; set; }
    }
}