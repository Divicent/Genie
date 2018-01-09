#region Usings

#endregion

using Genie.Core.Base.Generating;

namespace Genie.Core.Templates.Dapper
{
    internal class SqlDataRecordHandlerTemplate : GenieTemplate
    {
        public SqlDataRecordHandlerTemplate(string path) : base(path)
        {
        }

        public override string Generate()
        {
            L($@"
using System;
using System.Collections.Generic;
using System.Data;

namespace {GenerationContext.BaseNamespace}.Dapper
{{
    internal sealed class SqlDataRecordHandler : SqlMapper.ITypeHandler
    {{
        public object Parse(Type destinationType, object value)
        {{
            throw new NotSupportedException();
        }}

        public void SetValue(IDbDataParameter parameter, object value)
        {{
            SqlDataRecordListTVPParameter.Set(parameter, value as IEnumerable<Microsoft.SqlServer.Server.SqlDataRecord>, null);
        }}
    }}
}}

");

            return E();
        }
    }
}