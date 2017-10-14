#region Usings



#endregion

using Genie.Core.Base.Generating;

namespace Genie.Core.Templates.Dapper
{
    internal class SqlMapper_DontMapTemplate : GenieTemplate
    {
        public SqlMapper_DontMapTemplate(string path) : base(path)
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
        /// Dummy type for excluding from multi-map
        /// </summary>
        class DontMap {{ }}
    }}
}}");

            return E();
        }
    }
}