using Genie.Base.Generating.Concrete;
using Genie.Templates;
using System.Collections.Generic;
using Genie.Models.Abstract;
using System.Text;
using System.Linq;

namespace Genie.Templates.Infrastructure.Models.Concrete.Context
{
    internal class ModelQueryContextTemplate: GenieTemplate
    {
		private readonly List<ISimpleAttribute> _attributes;
		private readonly string _name;
        public ModelQueryContextTemplate(string path, string name, List<ISimpleAttribute> attributes) : base(path)
		{
            _name = name;
			_attributes = attributes;
        }

public override string Generate()
{

   var lit = _attributes.Where(a => a.IsLiteralType).ToList();
   var nonLit = _attributes.Where(a => !a.IsLiteralType).ToList();

   var cases = new StringBuilder();
   foreach(var a in lit) 
   {
	   cases.AppendLine($@"				case ""{a.Name.ToLower()}"":
				propertyName = ""{a.Name}"";
				return true;");
   }
   foreach(var a in nonLit) 
   {
	   cases.AppendLine($@"				case ""{a.Name.ToLower()}"":
				propertyName = ""{a.Name}"";
				return false;");
   }

L($@"
using System;
using System.Data;
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

		public {_name} FirstOrDefault(IDbTransaction transaction = null)
	    {{
			Top(1);
	        return _repo.GetFirstOrDefault(GetQuery(transaction));
	    }}

	    public int Count(IDbTransaction transaction = null)
	    {{
            return _repo.Count(GetQuery(transaction));
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
	            Target = ""[dbo].[{_name}]"",
	            Where = _where?.GetFilterExpressions(),
	            Order = _order?.GetOrderExpressions(),
	            PageSize = _pageSize,
	            Page = _page,
	            Limit = _limit,
	            Skip = _skip,
	            Take = _take,
	            Transaction = transaction
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