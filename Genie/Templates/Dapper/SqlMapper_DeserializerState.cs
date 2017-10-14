#region Usings



#endregion

using Genie.Core.Base.Generating;

namespace Genie.Core.Templates.Dapper
{
    internal class SqlMapper_DeserializerStateTemplate : GenieTemplate
    {
        public SqlMapper_DeserializerStateTemplate(string path) : base(path)
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
        struct DeserializerState
        {{
            public readonly int Hash;
            public readonly Func<IDataReader, object> Func;

            public DeserializerState(int hash, Func<IDataReader, object> func)
            {{
                Hash = hash;
                Func = func;
            }}
        }}
    }}
}}");

            return E();
        }
    }
}