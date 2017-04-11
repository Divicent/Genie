using System.Collections.Generic;
using DatabaseSchemaReader.DataSchema;

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

        public static string GetCSharpDataType(string csharpDatatype, bool nullable)
        {
            if (!nullable)
                return csharpDatatype;
            if (NullableTypes.Contains(csharpDatatype))
                return csharpDatatype + "?";
            return csharpDatatype;
        }
    }
}
