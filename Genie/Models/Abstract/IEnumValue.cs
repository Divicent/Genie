namespace Genie.Core.Models.Abstract
{
    public interface IEnumValue
    {
        string Name { get; }
        object Value { get; }
        string FieldName { get; }
    }
}