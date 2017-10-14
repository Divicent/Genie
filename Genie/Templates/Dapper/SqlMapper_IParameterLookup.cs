#region Usings

using Genie.Core.Base.Generating.Concrete;

#endregion

namespace Genie.Core.Templates.Dapper
{
    internal class SqlMapper_IParameterLookupTemplate : GenieTemplate
    {
        public SqlMapper_IParameterLookupTemplate(string path) : base(path)
        {
        }

        public override string Generate()
        {
            L($@"


namespace {GenerationContext.BaseNamespace}.Dapper
{{
    partial class SqlMapper
    {{
        /// <summary>
        /// Extends IDynamicParameters providing by-name lookup of parameter values
        /// </summary>
        public interface IParameterLookup : IDynamicParameters
        {{
            /// <summary>
            /// Get the value of the specified parameter (return null if not found)
            /// </summary>
            object this[string name] {{ get; }}
        }}
    }}
}}
 ");

            return E();
        }
    }
}