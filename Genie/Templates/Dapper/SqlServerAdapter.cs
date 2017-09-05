using Genie.Base.Generating.Concrete;
using Genie.Templates;

namespace Genie.Templates.Dapper
{
    internal class SqlServerAdapterTemplate: GenieTemplate
    {
        public SqlServerAdapterTemplate(string path) : base(path){}

public override string Generate()
{
  var dapperUsing = GenerationContext.NoDapper ? "using Dapper;": "";
L($@"

using System;
using System.Collections.Generic;
using System.Data;
using System.Linq;
using System.Reflection;
{dapperUsing}

namespace {GenerationContext.BaseNamespace}.Dapper
{{
	public class SqlServerAdapter : ISqlAdapter
    {{
        public int? Insert(IDbConnection connection, IDbTransaction transaction, int? commandTimeout, String tableName, string columnList, string parameterList, List<PropertyInfo> keyProperties, object entityToInsert)
        {{
            var cmd = string.Format(""insert into {{0}} ({{1}}) values ({{2}})"", tableName, columnList, parameterList);
            connection.Execute(cmd, entityToInsert, transaction, commandTimeout);
            var r = connection.Query(""select @@IDENTITY id"", transaction: transaction, commandTimeout: commandTimeout).ToList();
            if (r.Any())
            {{
                try
                {{
                    var id = (int)r.First().id;
                    return id;
                }}
                catch (Exception)
                {{

                    return null;
                }}
            }}
            return null;
        }}
    }}
}} ");

return E();
    
}
    }
}