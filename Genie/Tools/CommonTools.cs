#region Usings

using System.Collections.Generic;

#endregion

namespace Genie.Core.Tools
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
            {
                return csharpDatatype;
            }

            if (NullableTypes.Contains(csharpDatatype))
            {
                return csharpDatatype + "?";
            }

            return csharpDatatype;
        }

        private static string ConvertDataType(string dataType)
        {
            var type = dataType.ToLowerInvariant();
            switch (type)
            {
                case "int":
                    return type;
                case "numeric":
                case "decimal":
                    return "decimal";
                case "datetime2":
                case "datetime":
                case "smalldatetime":
                case "date":
                    return "DateTime";
                case "varchar":
                case "nvarchar":
                case "nchar":
                case "char":
                case "longtext":
                case "enum":
                case "set":
                    return "string";
                case "bit":
                    return "bool";
                case "bigint":
                    return "long";
                case "tinyint":
                    return "short";
                case "float":
                    return "float";
                default:
                    return "";
            }
        }
    }
}