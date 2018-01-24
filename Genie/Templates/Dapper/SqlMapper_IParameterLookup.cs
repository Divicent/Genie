
#region Usings

using Genie.Core.Base.Generating;

#endregion

namespace Genie.Core.Templates.Dapper
{
    public class SqlMapper_IParameterLookupTemplate : GenieTemplate
    {
        public SqlMapper_IParameterLookupTemplate(string path) : base(path)
        {
        }

        public override string Generate()
        {
            L($@"
namespace {GenerationContext.BaseNamespace}.Dapper
{{
    public static partial class SqlMapper
    {{
        /// <summary>
        /// Extends IDynamicParameters providing by-name lookup of parameter values
        /// </summary>
        public interface IParameterLookup : IDynamicParameters
        {{
            /// <summary>
            /// Get the value of the specified parameter (return null if not found)
            /// </summary>
            /// <param name=""name"">The name of the parameter to get.</param>
            object this[string name] {{ get; }}
        }}
    }}
}}

");

            return E();
        }
    }
}
