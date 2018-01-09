#region Usings

using System.Collections.Generic;
using System.Linq;
using System.Text;
using Genie.Core.Base.Configuration.Abstract;
using Genie.Core.Base.Generating;
using Genie.Core.Models.Abstract;
using Genie.Core.Tools;

#endregion

namespace Genie.Core.Templates.Infrastructure.Models.Concrete.Context
{
    internal class ModelQueryContextTemplate : GenieTemplate
    {
        private readonly List<ISimpleAttribute> _attributes;
        private readonly IConfiguration _configuration;
        private readonly string _name;

        public ModelQueryContextTemplate(string path, string name, List<ISimpleAttribute> attributes,
            IConfiguration configuration) : base(path)
        {
            _name = name;
            _attributes = attributes;
            _configuration = configuration;
        }

        public override string Generate()
        {
            var quote = FormatHelper.GetDbmsSpecificQuoter(_configuration);
            var lit = _attributes.Where(a => a.IsLiteralType).ToList();
            var nonLit = _attributes.Where(a => !a.IsLiteralType).ToList();

            var cases = new StringBuilder();
            var columnNames = new StringBuilder();
            var firstColumn = true;
            foreach (var a in lit)
            {
                columnNames.Append($"{(!firstColumn ? ", " : "")}\"{a.Name}\"");
                firstColumn = false;
                cases.AppendLine($@"				case ""{a.Name.ToLower()}"":
				propertyName = ""{a.Name}"";
				return true;");
            }

            foreach (var a in nonLit)
            {
                columnNames.Append($"{(!firstColumn ? ", " : "")}\"{a.Name}\"");
                firstColumn = false;
                cases.AppendLine($@"				case ""{a.Name.ToLower()}"":
				propertyName = ""{a.Name}"";
				return false;");
            }

            L($@"
using System;
using System.Data;
using System.Threading.Tasks;
using System.Collections.Generic;
using {GenerationContext.BaseNamespace}.Infrastructure.Models.Abstract;
using {GenerationContext.BaseNamespace}.Infrastructure.Filters.Abstract;
using {GenerationContext.BaseNamespace}.Infrastructure.Filters.Concrete;
using {GenerationContext.BaseNamespace}.Infrastructure.Repositories.Abstract;
using {GenerationContext.BaseNamespace}.Infrastructure.Models.Abstract.Context;

namespace {GenerationContext.BaseNamespace}.Infrastructure.Models.Concrete.Context
{{
    internal class {_name}QueryContext: BaseQueryContext, I{_name}QueryContext
	{{
		private I{_name}FilterContext _where; 
	    private I{_name}OrderContext _order;
		private readonly I{_name}Repository _repo;

        public I{_name}QueryContext Page(int pageSize, int page)
        {{
            _page = page;
            _pageSize = pageSize;
            return this;
        }}

        public I{_name}QueryContext Top(int limit)
        {{
            _limit = limit;
            return this;
        }}

        public I{_name}QueryContext Skip(int skip)
        {{
            _skip = skip;
            return this;
        }}

        public I{_name}QueryContext Take(int take)
        {{
            _take = take;
            return this;
        }}
		
		internal {_name}QueryContext(I{_name}Repository repo) {{ _repo = repo; }}
		
		public I{_name}FilterContext Where {{ get {{ return _where ?? (_where = new {_name}FilterContext(this)); }}}}
        
		public I{_name}OrderContext OrderBy {{ get {{ return _order ?? (_order = new {_name}OrderContext(this)); }} }}

        public IEnumerable<{_name}> Query(IDbTransaction transaction = null)
	    {{
	        return _repo.Get(GetQuery(transaction));
	    }}

        public async Task<IEnumerable<{_name}>> QueryAsync(IDbTransaction transaction = null)
	    {{
	        return await _repo.GetAsync(GetQuery(transaction));
	    }}

		public {_name} FirstOrDefault(IDbTransaction transaction = null)
	    {{
			Top(1);
	        return _repo.GetFirstOrDefault(GetQuery(transaction));
	    }}


        public async Task<{_name}> FirstOrDefaultAsync(IDbTransaction transaction = null)
	    {{
			Top(1);
	        return await _repo.GetFirstOrDefaultAsync(GetQuery(transaction));
	    }}

	    public int Count(IDbTransaction transaction = null)
	    {{
            return _repo.Count(GetQuery(transaction));
	    }}

        public async Task<int> CountAsync(IDbTransaction transaction = null)
	    {{
            return await _repo.CountAsync(GetQuery(transaction));
	    }}

		public I{_name}QueryContext Filter(IEnumerable<IPropertyFilter> filters) 
		{{
            ProcessFilter(Where.GetFilterExpressions(), filters);
			return this;	
		}}

		public I{_name}QueryContext SortBy(Tuple<string, bool> sortInfo)
	    {{
			if(sortInfo == null)
				return this;

            Sort(OrderBy.GetOrderExpressions(), sortInfo.Item1, sortInfo.Item2);
	        return this;
	    }}

	    private IRepoQuery GetQuery(IDbTransaction transaction)
	    {{
	        return new RepoQuery
	        {{
	            Target = ""{quote(_configuration.Schema)}.{quote(_name)}"",
	            Where = _where?.GetFilterExpressions(),
	            Order = _order?.GetOrderExpressions(),
	            PageSize = _pageSize,
	            Page = _page,
	            Limit = _limit,
	            Skip = _skip,
	            Take = _take,
	            Transaction = transaction,
                Columns = new [] {{ {columnNames} }}
	        }};
	    }}

		protected override bool? IsQuoted(ref string propertyName) 
		{{
			switch(propertyName.ToLower()) 
			{{
{cases}
				default: return null;
			}}
		}}

		public string GetWhereClause() 
		{{
			return _repo.GetWhereClause(GetQuery(null));
		}}
	}}
}}");

            return E();
        }
    }
}