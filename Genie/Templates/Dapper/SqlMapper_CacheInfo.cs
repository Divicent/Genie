#region Usings

#endregion

using Genie.Core.Base.Generating;

namespace Genie.Core.Templates.Dapper
{
    internal class SqlMapper_CacheInfoTemplate : GenieTemplate
    {
        public SqlMapper_CacheInfoTemplate(string path) : base(path)
        {
        }

        public override string Generate()
        {
            L($@"
using System;
using System.Data;
using System.Threading;

namespace {GenerationContext.BaseNamespace}.Dapper
{{
    public static partial class SqlMapper
    {{
        private class CacheInfo
        {{
            public DeserializerState Deserializer {{ get; set; }}
            public Func<IDataReader, object>[] OtherDeserializers {{ get; set; }}
            public Action<IDbCommand, object> ParamReader {{ get; set; }}
            private int hitCount;
            public int GetHitCount() {{ return Interlocked.CompareExchange(ref hitCount, 0, 0); }}
            public void RecordHit() {{ Interlocked.Increment(ref hitCount); }}
        }}
    }}
}}

");

            return E();
        }
    }
}