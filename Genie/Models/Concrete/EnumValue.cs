using Genie.Models.Abstract;

namespace Genie.Models.Concrete
{
    internal class EnumValue: IEnumValue
    {
        public string Name { get; set; }
        public object Value { get; set; }
        public string FieldName { get; set; }
    }
}
