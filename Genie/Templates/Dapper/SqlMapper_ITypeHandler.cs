using Genie.Core.Base.Generating.Concrete;

namespace Genie.Core.Templates.Dapper
{
    internal class SqlMapper_ITypeHandlerTemplate : GenieTemplate
    {
        public SqlMapper_ITypeHandlerTemplate(string path) : base(path)
        {
        }

        public override string Generate()
        {
            L($@"

using System;
using System.Data;

namespace {GenerationContext.BaseNamespace}.Dapper
{{
    partial class SqlMapper
    {{
        /// <summary>
        /// Implement this interface to perform custom type-based parameter handling and value parsing
        /// </summary>
        public interface ITypeHandler
        {{
            /// <summary>
            /// Assign the value of a parameter before a command executes
            /// </summary>
            /// <param name=""parameter"">The parameter to configure</param>
            /// <param name=""value"">Parameter value</param>
            void SetValue(IDbDataParameter parameter, object value);

            /// <summary>
            /// Parse a database value back to a typed value
            /// </summary>
            /// <param name=""value"">The value from the database</param>
            /// <param name=""destinationType"">The type to parse to</param>
            /// <returns>The typed value</returns>
            object Parse(Type destinationType, object value);
        }}
    }}
}} 
");

            return E();
        }
    }
}