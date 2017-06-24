using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using Genie.Base.Configuration.Abstract;

namespace Genie.Base.Configuration.Concrete
{
    public class ConfigurationEnumTable: IConfigurationEnumTable
    {
        public string Table { get; set; }
        public string ValueColumn { get; set; }
        public string NameColumn { get; set; }
        public string Type { get; set; }

        public void Validate()
        {
           var error = new StringBuilder();
            if (string.IsNullOrWhiteSpace(Table))
                error.AppendLine("Table is not provided in the Enum.");
            if(string.IsNullOrWhiteSpace(ValueColumn))
                error.AppendLine("ValueColumn is not provided in the Enum.");
            if(string.IsNullOrWhiteSpace(NameColumn))
                error.AppendLine("NameColumn not is provided in the Enum.");
            if (string.IsNullOrWhiteSpace(Type))
                error.AppendLine("NameColumn not is provided in the Enum.");
            if (!(Type == "string" || Type == "int" || Type == "bool" || Type == "double"))
                error.AppendLine(
                    $"{Type} is not a supported type. only string, int, bool, double are supported for enum table type");
            if (error.Length > 0)
            {
                throw new Exception(error.ToString());
            }
        }
    }
}
