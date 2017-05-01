
using Genie.Models.Abstract;

namespace Genie.Models
{
    /// <summary>
    /// represents an attribute
    /// </summary>
    internal class Attribute : IAttribute
    {
        public bool IsKey { get; set; }
        public string DataType { get; set; }
        public string Name { get; set; }
        public string FieldName { get; set; }
        public string RefPropName { get; set; }
        public bool IsLiteralType { get; set; }          
    }
}
