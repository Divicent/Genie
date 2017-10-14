#region Usings

using Genie.Core.Base.Generating.Concrete;

#endregion

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

#if !COREFX
namespace {GenerationContext.BaseNamespace}.Dapper 
{{
    sealed class SqlDataRecordHandler : SqlMapper.ITypeHandler
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
#endif");

            return E();
        }
    }
}