#region Usings

#endregion

using Genie.Core.Base.Generating;

namespace Genie.Core.Templates.Dapper
{
    public class SqlMapper_LiteralTokenTemplate : GenieTemplate
    {
        public SqlMapper_LiteralTokenTemplate(string path) : base(path)
        {
        }

        public override string Generate()
        {
            L($@"
using System.Collections.Generic;

namespace {GenerationContext.BaseNamespace}.Dapper
{{
    public static partial class SqlMapper
    {{
        /// <summary>
        /// Represents a placeholder for a value that should be replaced as a literal value in the resulting sql
        /// </summary>
        internal struct LiteralToken
        {{
            /// <summary>
            /// The text in the original command that should be replaced
            /// </summary>
            public string Token {{ get; }}

            /// <summary>
            /// The name of the member referred to by the token
            /// </summary>
            public string Member {{ get; }}

            internal LiteralToken(string token, string member)
            {{
                Token = token;
                Member = member;
            }}

            internal static readonly IList<LiteralToken> None = new LiteralToken[0];
        }}
    }}
}}

");

            return E();
        }
    }
}