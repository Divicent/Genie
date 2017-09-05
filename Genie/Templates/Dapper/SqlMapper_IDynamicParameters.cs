using Genie.Base.Generating.Concrete;
using Genie.Templates;

namespace Genie.Templates.Dapper
{
    internal class SqlMapper_IDynamicParametersTemplate: GenieTemplate
    {
        public SqlMapper_IDynamicParametersTemplate(string path) : base(path){}

public override string Generate()
{
L($@"

using System.Data;

namespace {GenerationContext.BaseNamespace}.Dapper 
{{
    partial class SqlMapper
    {{
        /// <summary>  
        /// Implement this interface to pass an arbitrary db specific set of parameters to Dapper
        /// </summary>
        public interface IDynamicParameters
        {{
            /// <summary>
            /// Add all the parameters needed to the command just before it executes
            /// </summary>
            /// <param name=""command"">The raw command prior to execution</param>
            /// <param name=""identity"">Information about the query</param>
            void AddParameters(IDbCommand command, Identity identity);
        }}
    }}
}}
");

return E();
    
}
    }
}