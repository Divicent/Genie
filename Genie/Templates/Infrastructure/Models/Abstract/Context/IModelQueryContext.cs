#region Usings



#endregion

using Genie.Core.Base.Generating;

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

    /// <summary>
    /// Helps to build retrieve queries on {_name}. the query can be built as one statement, and an object of this class can be used to add elements to the query. all the elements are persisted inside the object
    /// </summary>
	public interface I{_name}QueryContext
	{{
        /// <summary>
        /// Apply paging on the query. this will apply SQL level paging on the query
        /// <para>
        /// Note: paging cannot be applied without ordering the query
        /// </para>
        /// </summary>
        /// <param name=""pageSize"">Size of one page</param>
        /// <param name=""page"">Page number</param>
        /// <returns>The current context</returns>
        I{_name}QueryContext Page(int pageSize, int page);
        
        /// <summary>
        /// Limit query results by applying a limit on the query. this will apply SQL level limit on the query
        /// </summary>
        /// <param name=""limit"">Maximum number of records</param>
        /// <returns>The current context</returns>
        I{_name}QueryContext Top(int limit);
        
        /// <summary>
        /// Limit certain number of result by applying a skip on the query. this will apply SQL level skip on the query
        /// </summary>
        /// <param name=""skip"">Number of records to skip</param>
        /// <returns>The current context</returns>
        I{_name}QueryContext Skip(int skip);

        /// <summary>
        /// Take certain number of result by applying a take on the query. this will apply SQL level take on the query
        /// </summary>
        /// <param name=""take"">Number of records to take</param>
        /// <returns>The current context</returns>
        I{_name}QueryContext Take(int take);

        /// <summary>
        /// The where clause of the query. this can be reused within this object
        /// </summary>
		I{_name}FilterContext Where {{ get; }}

        /// <summary>
        /// The order by clause of the query. this can be reused within this object
        /// </summary>
		I{_name}OrderContext OrderBy {{ get; }}
	      
        /// <summary>
        /// Start querying the data source, this will build the query for the specific DBMS ,
        /// pull data and map to a list of {_name},
        /// all returned objects are registered in the db context and change tracking is applied.
        /// All the items in the query can be reused
        /// </summary>
        /// <param name=""transaction"">Transaction to use</param>
        /// <returns>Collection of {_name}</returns>
        IEnumerable<Concrete.{_name}> Query(IDbTransaction transaction = null);
		    
        /// <summary>
        /// Apply top(1) and start querying the data source, this will build the query for the specific DBMS ,
        /// pull data and take the first object,
        /// returned object is registered in the db context and change tracking is applied.
        /// All the items in the query can be reused
        /// </summary>
        /// <param name=""transaction"">Transaction to use</param>
        /// <returns>An object of {_name} or null</returns>
        Concrete.{_name} FirstOrDefault(IDbTransaction transaction = null);
		    
        /// <summary>
        /// Applies given filters to the where object, this can be used to apply filters in a customized way.
        /// </summary>
        /// <param name=""filters"">Filters to apply</param>
        /// <returns>The current context</returns>
        I{_name}QueryContext Filter(IEnumerable<IPropertyFilter> filters);
		    
        /// <summary>
        /// Applies given expressions to the order by object, this can be used to apply sorting in a customized way
        /// </summary>
        /// <param name=""sortInfo"">Order by expressions to apply</param>
        /// <returns>The current context</returns>
        I{_name}QueryContext SortBy(Tuple<string, bool> sortInfo);
	      
        /// <summary>
        /// This will build the query for specific DBMS, Query only the count of the results. this will perform an SLQ level count aggregate
        /// </summary>
        /// <param name=""transaction"">Transaction to use</param>
        /// <returns>Count to the results</returns>
        int Count(IDbTransaction transaction = null);

        /// <summary>
        /// Extract built where clause
        /// </summary>
        /// <returns></returns>
		string GetWhereClause();
	}}
}}");

            return E();
        }
    }
}