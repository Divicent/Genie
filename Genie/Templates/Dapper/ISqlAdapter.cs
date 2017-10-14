#region Usings

using Genie.Core.Base.Generating.Concrete;

#endregion

namespace Genie.Core.Templates.Dapper
{
    internal class ISqlAdapterTemplate : GenieTemplate
    {
        public ISqlAdapterTemplate(string path) : base(path)
        {
        }

        public override string Generate()
        {
            L($@"

using System;
using System.Collections.Generic;
using System.Data;
using System.Reflection;

namespace {GenerationContext.BaseNamespace}.Dapper 
{{
	public interface ISqlAdapter
	{{
		int? Insert(IDbConnection connection, IDbTransaction transaction, int? commandTimeout, String tableName, string columnList, string parameterList, List<PropertyInfo> keyProperties, object entityToInsert);
	}}

}}");

            return E();
        }
    }
}