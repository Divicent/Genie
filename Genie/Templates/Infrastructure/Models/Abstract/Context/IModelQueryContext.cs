#region Usings

using Genie.Core.Base.Generating.Concrete;

#endregion

namespace Genie.Core.Templates.Infrastructure.Models.Abstract.Context
{
    internal class IModelQueryContextTemplate : GenieTemplate
    {
        private readonly string _name;

        public IModelQueryContextTemplate(string path, string name) : base(path)
        {
            _name = name;
        }

        public override string Generate()
        {
            L($@"
using System;
using System.Data;
using System.Collections.Generic;
using {GenerationContext.BaseNamespace}.Infrastructure.Filters.Abstract;

namespace {GenerationContext.BaseNamespace}.Infrastructure.Models.Abstract.Context
{{
	public interface I{_name}QueryContext
	{{
        I{_name}QueryContext Page(int pageSize, int page);
        I{_name}QueryContext Top(int limit);
        I{_name}QueryContext Skip(int skip);
        I{_name}QueryContext Take(int take);
		I{_name}FilterContext Where {{ get; }}
		I{_name}OrderContext OrderBy {{ get; }}
	    IEnumerable<Concrete.{_name}> Query(IDbTransaction transaction = null);
		Concrete.{_name} FirstOrDefault(IDbTransaction transaction = null);
		I{_name}QueryContext Filter(IEnumerable<IPropertyFilter> filters);
		I{_name}QueryContext SortBy(Tuple<string, bool> sortInfo);
	    int Count(IDbTransaction transaction = null);
		string GetWhereClause();
	}}
}}");

            return E();
        }
    }
}