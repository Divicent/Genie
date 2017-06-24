
namespace Genie.Models.Abstract
{
    /// <summary>
    /// Represents an attribute (column) of a relation
    /// </summary>
    internal interface IAttribute : ISimpleAttribute
    {
        /// <summary>
        /// Is this a primary key of the relation 
        /// </summary>
        bool IsKey { get; set; }

        /// <summary>
        /// Reference property name if available
        /// </summary>
        string RefPropName { get; set; }
    }
}
