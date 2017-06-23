namespace Genie.Base.Configuration.Abstract
{
    public interface IConfigurationEnumTable
    {
        string Table { get; set; }
        string ValueColumn { get; set; }
        string NameColumn { get; set; }
    }
}
