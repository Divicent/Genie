#region Usings

#endregion

using Genie.Core.Base.Generating;

namespace Genie.Core.Templates.Dapper
{
    public class DataTableHandlerTemplate : GenieTemplate
    {
        public DataTableHandlerTemplate(string path) : base(path)
        {
        }

        public override string Generate()
        {
            L($@"
using System;
using System.Data;
#if !NETSTANDARD1_3
namespace {GenerationContext.BaseNamespace}.Dapper
{{
    internal sealed class DataTableHandler : SqlMapper.ITypeHandler
    {{
        public object Parse(Type destinationType, object value)
        {{
            throw new NotImplementedException();
        }}

        public void SetValue(IDbDataParameter parameter, object value)
        {{
            TableValuedParameter.Set(parameter, value as DataTable, null);
        }}
    }}
}}
#endif
");

            return E();
        }
    }
}