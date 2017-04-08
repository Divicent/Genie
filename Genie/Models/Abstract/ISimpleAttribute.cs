namespace Genie.Models.Abstract
{
    /// <summary>
    /// Represents an attribute that does not need to track changes (must be a simple property)
    /// </summary>
    internal interface ISimpleAttribute
    {
        /// <summary>
        /// Data type of the attribute
        /// </summary>
        string DataType { get; set; }

        /// <summary>
        /// Name of the attribute
        /// </summary>
        string Name { get; set; }
    }
}
