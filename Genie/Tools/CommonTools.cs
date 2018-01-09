#region Usings

using System.Collections.Generic;
using System.Security.Cryptography;
using System.Text;
using Newtonsoft.Json;

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
            if (!nullable) return csharpDatatype;

            if (NullableTypes.Contains(csharpDatatype)) return csharpDatatype + "?";
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
                case "timestamp":
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
                case "smallint":
                    return "short";
                case "varbinary":
                    return "byte[]";
                case "float":
                    return "float";
                default:
                    return "";
            }
        }

        public static string CalculateMd5Hash(object input)
        {
            var hash = MD5.Create().ComputeHash(Encoding.UTF8.GetBytes(JsonConvert.SerializeObject(input)));
            var sb = new StringBuilder();
            
            for (var i = 0; i < hash.Length; i++)
                sb.Append(hash[i].ToString("X2"));
            
            return sb.ToString();
        }
    }
}