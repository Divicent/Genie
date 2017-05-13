using System.Collections.Generic;

namespace Genie.Tools
{
    public static class CommonTools
    {
        private static readonly HashSet<string> NullableTypes = new HashSet<string>
        {
            "int",
            "short",
            "long",
            "double",
            "decimal",
            "float",
            "bool",
            "DateTime"
        };

        public static string GetCSharpDataType(string dataType, bool nullable)
        {
            var csharpDatatype = ConvertDataType(dataType);
            if (!nullable)
                return csharpDatatype;
            if (NullableTypes.Contains(csharpDatatype))
                return csharpDatatype + "?";
            return csharpDatatype;
        }

        private static string ConvertDataType(string dataType)
        {
            var type = dataType.ToLower();
            switch (type)
            {
                case "int":
                    return type;
                case "numeric":
                case "decimal":
                    return "decimal";
                case "datetime2":
                case "datetime":
                case "date":
                    return "DateTime";
                case "varchar":
                case "nvarchar":
                case "nchar":
                case "char":
                    return "string";
                case "bit":
                    return "bool";
            }
            return "";
        }
    }
}
