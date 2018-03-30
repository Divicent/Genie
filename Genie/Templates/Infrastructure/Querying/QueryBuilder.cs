using Genie.Core.Base.Configuration.Abstract;
using Genie.Core.Base.Generating;
using Genie.Core.Tools;

namespace Genie.Core.Templates.Infrastructure.Querying
{
    public class QueryBuilderTemplate: GenieTemplate
    {
	    private readonly IConfiguration _configuration;
	    
        public QueryBuilderTemplate(string path, IConfiguration configuration) : base(path)
        {
	        _configuration = configuration;
        }

        public override string Generate()
        {
	        var quote = FormatHelper.GetDbmsSpecificQuoter(_configuration);

	        var selectFunction = "";
	        var pageFunction = "";
	        
	        switch (_configuration.DBMS)
	        {
		        case "mssql":
			        selectFunction = $@"
		   _select = string.Format(""select {{0}} {{1}} from "" + _repoQuery.Target, _repoQuery.Limit != null ? "" top "" 
					+ _repoQuery.Limit : """", isCount ? ""count(*)"" : CreateSelectColumnList(_repoQuery.Columns.ToList(), _repoQuery.Target));
		   return this;";

			        pageFunction =
				        $@"	        if (_repoQuery.Page != null && _repoQuery.PageSize != null)
	        {{
	            _page = $"" OFFSET ({{_repoQuery.Page * _repoQuery.PageSize}}) ROWS "" + $"" FETCH NEXT {{_repoQuery.PageSize}} ROWS ONLY "";
	        }}
	        else
	        {{
				var builder = new StringBuilder();
	            if (_repoQuery.Skip != null)
	                builder.Append($"" OFFSET ({{_repoQuery.Skip}}) ROWS "");

	            if (_repoQuery.Take != null)
	                builder.Append($"" FETCH NEXT {{_repoQuery.Take}} ROWS ONLY "");
				
				_page = builder.ToString();
	        }}
";
			        break;
		        case "mysql":
			        selectFunction =
				        $@"		   _select = string.Format(""select {{0}} from "" + _repoQuery.Target, isCount ? ""count(*)"" : CreateSelectColumnList(_repoQuery.Columns.ToList(), _repoQuery.Target));
		   return this;";
			        pageFunction =
				        $@"	        if (_repoQuery.Page != null && _repoQuery.PageSize != null)
	        {{
	            _page = $"" LIMIT {{_repoQuery.Page * _repoQuery.PageSize}}, {{_repoQuery.PageSize}} "";
	        }}
	        else if(_repoQuery.Skip != null || _repoQuery.Take != null || _repoQuery.Limit != null)
	        {{
                _page = $"" LIMIT {{_repoQuery.Skip??0}},  {{_repoQuery.Take??_repoQuery.Limit??0}} "";
	        }}";
			        break;
	        }
	        
            L($@"
using System.Collections.Generic;
using System.Linq;
using System.Text;
using {GenerationContext.BaseNamespace}.Infrastructure.Filters.Abstract;
using Cache = {GenerationContext.BaseNamespace}.Infrastructure.Querying.QueryBuilderCache;

namespace {GenerationContext.BaseNamespace}.Infrastructure.Querying
{{
    internal class QueryBuilder
    {{
        private readonly IRepoQuery _repoQuery;
	    private readonly StringBuilder _builder;

	    private string _select;
	    private string _where;
	    private string _order;
	    private string _page;
	    private string _group;
        
        internal QueryBuilder(IRepoQuery repoQuery)
        {{
	        _repoQuery = repoQuery;
	        _builder = new StringBuilder();
        }}

        internal string Get()
        {{
	        return Select(false)
		    .Where()
		    .Order()
		    .Page()
		    .Build();
        }}

	    internal string Count()
	    {{
		    return Select(true)
			    .Where()
			    .Page()
			    .Build();
	    }}
	    
	    internal string SumBy(string name)
	    {{
		    return SelectSum(name)
			    .Where()
			    .Page()
			    .GroupByAllExcept(name)
			    .Build();
	    }}
	    
	    
	    internal string WhereClause()
	    {{
		    return Where()
			    .Build();
	    }}

	    private QueryBuilder Select(bool isCount)
	    {{
{selectFunction}
	    }}

	    private QueryBuilder SelectSum(string name)
	    {{
		    _select = $""select sum({{name}})"";
		    return this;
	    }}

	    private QueryBuilder Where()
	    {{
		    var where = _repoQuery.Where == null ? new Queue<string>() : new Queue<string>(_repoQuery.Where);

		    if (where.Count <= 0) return this;
		    var builder = new StringBuilder();
		    builder.Append("" where "");

		    var first = true;
		    var previous = """";

		    while (where.Count > 0)
		    {{
			    var current = where.Dequeue();

			    if (AndOrOr(current))
			    {{
				    if (first)
				    {{
					    first = false;
					    previous = current;
					    continue;
				    }}

				    if (AndOrOr(previous))
				    {{
					    previous = current;
					    continue;
				    }}

				    previous = current;
				    builder.Append($"" {{current}} "");
			    }}
			    else if (current == "")"" || current == ""("")
			    {{
				    if (current == ""("" && !first && !AndOrOr(previous))
					    builder.Append("" and "");

				    previous = current;
				    builder.Append($"" {{current}} "");
			    }}
			    else
			    {{
				    if (!first && previous != ""("" && previous != "")"" && !AndOrOr(previous))
				    {{
					    builder.Append("" and "");
				    }}

				    previous = current;
				    builder.Append($"" {{current}} "");
			    }}

			    first = false;
		    }}
		    _where = builder.ToString();

		    return this;
	    }}

	    private QueryBuilder Order()
	    {{
		    var order = _repoQuery.Order == null ? new Queue<string>() : new Queue<string>(_repoQuery.Order);

		    if (order.Count <= 0) 
			    return this;
		    
		    var builder = new StringBuilder();
		    builder.Append("" order by "");
		    while (order.Count > 0)
		    {{
			    var item = order.Dequeue();
			    builder.Append($"" {{item}} "");
		    }}
		    _order = builder.ToString();

		    return this;
	    }}

	    private QueryBuilder Page()
	    {{
{pageFunction}

		    return this;
	    }}

	    private QueryBuilder GroupByAllExcept(string name)
	    {{
		    if (string.IsNullOrWhiteSpace(name) )
			    return this;

		    var groupColumns = _repoQuery.Columns
			    .Where(c => c != name);
		    
		    var builder = new StringBuilder(""group by "");
		    var first = true;
		    foreach (var columnName in groupColumns)
		    {{
			    builder.Append($""{{(!first ? "", "" : """")}}`{{columnName}}`"");
			    first = false;
		    }}

		    _group = builder.ToString();

		    return this;
	    }}

	    private string Build()
	    {{
		    if (_select != null)
			    _builder.AppendLine(_select);
		    
		    if(_where != null)
			    _builder.AppendLine(_where);
		    
		    if(_order != null)
			    _builder.AppendLine(_order);

		    if (_page != null)
			    _builder.AppendLine(_page);
		    
		    if(_group != null)
			    _builder.AppendLine(_group);

		    return _builder.ToString();
	    }}

	    private static bool AndOrOr(string str)
	    {{
		    return str == ""and"" || str == ""or"";
	    }}

	    private static string CreateSelectColumnList(IReadOnlyCollection<string> columnNames, string target)
	    {{
		    if (string.IsNullOrWhiteSpace(target) || columnNames == null || columnNames.Count < 1)
			    return ""*"";

		    if (Cache.SelectParts.TryGetValue(target, out var result))
			    return result;

		    var builder = new StringBuilder();
		    var first = true;
		    foreach (var columnName in columnNames)
		    {{
			    builder.Append($""{{(!first ? "", "" : """")}}{quote("{columnName}")}"");
			    first = false;
		    }}
		    
		    return Cache.SelectParts[target] = builder.ToString();
	    }}
    }}
}}");
	        return E();
        }
    }
}