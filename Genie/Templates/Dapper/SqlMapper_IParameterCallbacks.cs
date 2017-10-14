#region Usings

using Genie.Core.Base.Generating.Concrete;

#endregion

namespace Genie.Core.Templates.Dapper
{
    internal class SqlMapper_IParameterCallbacksTemplate : GenieTemplate
    {
        public SqlMapper_IParameterCallbacksTemplate(string path) : base(path)
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
        /// Extends IDynamicParameters with facilities for executing callbacks after commands have completed
        /// </summary>
        public interface IParameterCallbacks : IDynamicParameters
        {{
            /// <summary>
            /// Invoked when the command has executed
            /// </summary>
            void OnCompleted();
        }}
    }}
}}
");

            return E();
        }
    }
}