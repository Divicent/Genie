namespace Genie.Models.Abstract
{
    /// <summary>
    /// A reference from one relation to another (one to many)
    /// </summary>
    internal interface IReferenceList
    {
        /// <summary>
        /// Referenced Relation Name
        /// </summary>
        string ReferncedRelationName { get; set; }

        /// <summary>
        /// Referenced property name
        /// </summary>
        string ReferencedPropertyName { get; set; }

        /// <summary>
        /// Referenced Property on this relation
        /// </summary>
        string ReferencedPropertyOnThisRelation { get; set; }

    }
}
