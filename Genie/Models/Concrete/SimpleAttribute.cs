using Genie.Models.Abstract;

namespace Genie.Models.Concrete
{
    internal class SimpleAttribute: ISimpleAttribute
    {
        public string DataType { get; set; }
        public string Name { get; set; }
        public string FieldName { get; set; }
        public bool IsLiteralType { get; set; }
    }
}
