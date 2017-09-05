namespace Genie.Base.Configuration.Abstract
{
    public interface IConfigurationEnumTable: IValidatableConfiguration
    {
        string Table { get; set; }
        string ValueColumn { get; set; }
        string NameColumn { get; set; }
        string Type { get; set; }
    }
}
