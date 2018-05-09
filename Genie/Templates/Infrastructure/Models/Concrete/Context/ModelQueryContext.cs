#region Usings

using System.Collections.Generic;
using System.Linq;
using System.Text;
using Genie.Core.Base.Configuration.Abstract;
using Genie.Core.Models.Abstract;
using Genie.Core.Tools;

#endregion

namespace Genie.Core.Templates.Infrastructure.Models.Concrete.Context
{
    public class ModelQueryContextTemplate : GenieTemplate
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
            const string template =
@"
                internal class {{name}}QueryContext: BaseQueryContext, I{{name}}QueryContext
	            {
		            private I{{name}}FilterContext _where; 
	                private I{{name}}OrderContext _order;
		            private readonly I{{name}}Repository _repo;
		            private readonly string[] _columnNames = { {{columnNames}} };

                    public I{{name}}QueryContext Page(int pageSize, int page)
                    {
                        _page = page;
                        _pageSize = pageSize;
                        return this;
                    }

                    public I{{name}}QueryContext Top(int limit)
                    {
                        _limit = limit;
                        return this;
                    }

                    public I{{name}}QueryContext Skip(int skip)
                    {
                        _skip = skip;
                        return this;
                    }

                    public I{{name}}QueryContext Take(int take)
                    {
                        _take = take;
                        return this;
                    }
		
		            internal {{name}}QueryContext(I{{name}}Repository repo) { _repo = repo; }
		
		            public I{{name}}FilterContext Where { get { return _where ?? (_where = new {{name}}FilterContext(this)); } }
        
		            public I{{name}}OrderContext OrderBy { get { return _order ?? (_order = new {{name}}OrderContext(this)); } }

                    public IEnumerable<{{name}}> Query(IDbTransaction transaction = null)
	                {
	                    return _repo.Get(GetQuery(transaction));
	                }

                    public async Task<IEnumerable<{{name}}>> QueryAsync(IDbTransaction transaction = null)
	                {
	                    return await _repo.GetAsync(GetQuery(transaction));
	                }

		            public {{name}} FirstOrDefault(IDbTransaction transaction = null)
	                {
			            Top(1);
	                    return _repo.GetFirstOrDefault(GetQuery(transaction));
	                }


                    public async Task<{{name}}> FirstOrDefaultAsync(IDbTransaction transaction = null)
	                {
			            Top(1);
	                    return await _repo.GetFirstOrDefaultAsync(GetQuery(transaction));
	                }

	                public int Count(IDbTransaction transaction = null)
	                {
                        return _repo.Count(GetQuery(transaction));
	                }

                    public async Task<int> CountAsync(IDbTransaction transaction = null)
	                {
                        return await _repo.CountAsync(GetQuery(transaction));
	                }

		            public T SumBy<T>(Func<I{{name}}ColumnSelector, IColumn<T>> predicate, IDbTransaction transaction = null) where T : struct
		            {
			            return _repo.SumBy<T>(GetQuery(transaction), predicate(new {{name}}ColumnSelector()).Name);
		            }

		            public async Task<T> SumByAsync<T>(Func<I{{name}}ColumnSelector, IColumn<T>> predicate, IDbTransaction transaction = null) where T : struct
		            {
			            return await _repo.SumByAsync<T>(GetQuery(transaction), predicate(new {{name}}ColumnSelector()).Name);
		            }

		            public I{{name}}QueryContext Filter(IEnumerable<IPropertyFilter> filters) 
		            {
                        ProcessFilter(Where.GetFilterExpressions(), filters);
			            return this;	
		            }

		            public I{{name}}QueryContext SortBy(Tuple<string, bool> sortInfo)
	                {
			            if(sortInfo == null)
				            return this;

                        Sort(OrderBy.GetOrderExpressions(), sortInfo.Item1, sortInfo.Item2);
	                    return this;
	                }

	                private IRepoQuery GetQuery(IDbTransaction transaction)
	                {
	                    return new RepoQuery
	                    {
	                        Target = ""{{schemaQuoted}}.{{nameQuoted}}"",
	                        Where = _where?.GetFilterExpressions(),
	                        Order = _order?.GetOrderExpressions(),
	                        PageSize = _pageSize,
	                        Page = _page,
	                        Limit = _limit,
	                        Skip = _skip,
	                        Take = _take,
	                        Transaction = transaction,
                            Columns = _columnNames
	                    };
	                }

		            protected override bool? IsQuoted(ref string propertyName) 
		            {
			            switch(propertyName.ToLower()) 
			            {
            {{cases}}
				            default: return null;
			            }
		            }

		            protected override string FixStringValue(string propertyName, string value) 
		            {
			            switch(propertyName.ToLower()) 
			            {
            {{dateCases}}
				            default: return value;
			            }
		            }

		            public string GetWhereClause() 
		            {
			            return _repo.GetWhereClause(GetQuery(null));
		            }
	            }
";

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

            var dateTypes = _attributes.Where(a => a.DataType == "DateTime").ToList();
            var dateCases = new StringBuilder();
            if (dateTypes.Count > 0)
            {
                foreach (var dateType in dateTypes)
                {
                    dateCases.AppendLine($@"				case ""{dateType.Name.ToLower()}"":
					return Convert.ToDateTime(value).ToString();
                    ");
                }
            }

            return Process(nameof(ModelQueryContextTemplate), template, new
            {
                name = _name,
                schemaQuoted = quote(_configuration.Schema),
                nameQuoted = quote(_name),
                cases = cases.ToString(),
                dateCases = dateCases.ToString(),
                columnNames = columnNames.ToString()
            });
            
        }
    }
}