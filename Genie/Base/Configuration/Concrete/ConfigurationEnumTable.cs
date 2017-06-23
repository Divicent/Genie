using Genie.Base.Configuration.Abstract;

namespace Genie.Base.Configuration.Concrete
{
    public class ConfigurationEnumTable: IConfigurationEnumTable
    {
        public string Table { get; set; }
        public string ValueColumn { get; set; }
        public string NameColumn { get; set; }
    }
}
