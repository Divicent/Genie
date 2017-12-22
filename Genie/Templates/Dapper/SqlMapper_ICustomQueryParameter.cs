#region Usings



#endregion

using Genie.Core.Base.Generating;

namespace Genie.Core.Templates.Dapper
{
    internal class SqlMapper_ICustomQueryParameterTemplate : GenieTemplate
    {
        public SqlMapper_ICustomQueryParameterTemplate(string path) : base(path)
        {
        }

        public override string Generate()
        {
            L($@"
using System.Data;

namespace {GenerationContext.BaseNamespace}.Dapper
{{
    public static partial class SqlMapper
    {{
        /// <summary>
        /// Implement this interface to pass an arbitrary db specific parameter to Dapper
        /// </summary>
        public interface ICustomQueryParameter
        {{
            /// <summary>
            /// Add the parameter needed to the command before it executes
            /// </summary>
            /// <param name=""command"">The raw command prior to execution</param>
            /// <param name=""name"">Parameter name</param>
            void AddParameter(IDbCommand command, string name);
        }}
    }}
}}

");

            return E();
        }
    }
}