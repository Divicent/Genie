
namespace Genie.Models.Abstract
{
    /// <summary>
    /// Represents an attribute (column) of a relation
    /// </summary>
    internal interface IAttribute
    {
        /// <summary>
        /// Is this a primary key of the relation 
        /// </summary>
        bool IsKey { get; set; }
        
        /// <summary>
        /// Data type of the attribute
        /// </summary>
        string DataType { get; set; }
        
        /// <summary>
        /// Name of the attribute
        /// </summary>
        string Name { get; set; }
        
        /// <summary>
        /// Preprocessed field name of the attribute 
        /// </summary>
        string FieldName { get; set; }

        /// <summary>
        /// Reference property name if available
        /// </summary>
        string RefPropName { get; set; }
    }
}
