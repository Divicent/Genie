
#region Usings

using Genie.Core.Base.Generating;

#endregion

namespace Genie.Core.Templates.Dapper
{
    public class SqlMapper_DontMapTemplate : GenieTemplate
    {
        public SqlMapper_DontMapTemplate(string path) : base(path)
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
        /// Dummy type for excluding from multi-map
        /// </summary>
        private class DontMap {{ /* hiding constructor */ }}
    }}
}}

");

            return E();
        }
    }
}
