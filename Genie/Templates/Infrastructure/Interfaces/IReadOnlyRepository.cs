#region Usings



#endregion

using Genie.Core.Base.Generating;

namespace Genie.Core.Templates.Infrastructure.Interfaces
{
    internal class IReadOnlyRepositoryTemplate : GenieTemplate
    {
        public IReadOnlyRepositoryTemplate(string path) : base(path)
        {
        }

        public override string Generate()
        {
            L($@"

using System.Collections.Generic;
using System.Data;
using {GenerationContext.BaseNamespace}.Infrastructure.Filters.Abstract;

namespace {GenerationContext.BaseNamespace}.Infrastructure.Interfaces
{{
    /// <summary>
    /// A read only repository only contains methods to get data from  the data source.
    /// </summary>
    /// <typeparam name=""T"">Object type of the repository</typeparam>
	public interface IReadOnlyRepository<out T>
		where T : class
	{{
        /// <summary>
        /// Current database connection
        /// </summary>
		IDbConnection Conn {{ get; }}

        /// <summary>
        /// Current context
        /// </summary>
		IDapperContext Context {{ get; }}

        /// <summary>
        /// Get Executes given query on repository
        /// </summary>
        /// <param name=""query"">Query to use</param>
        /// <returns>Collection of <typeparamref name=""T""/> </returns>
        IEnumerable<T> Get(IRepoQuery query);
		
        /// <summary>
        /// Get the first occurrence or null if the result is empty from the query
        /// </summary>
        /// <param name=""query"">Query to execute in the repository</param>
        /// <returns>an object with type <typeparamref name=""T""/></returns>
        T GetFirstOrDefault(IRepoQuery query);

        /// <summary>
        /// Executes given query on the repository and returns count of the result set
        /// </summary>
        /// <param name=""query"">Query to execute in the repository</param>
        /// <returns>an integer</returns>
	    int Count(IRepoQuery query);

        /// <summary>
        /// Extracts the where clause of the provided query object
        /// </summary>
        /// <param name=""query"">Query to use</param>
        /// <returns>The where clause as a string</returns>
		string GetWhereClause(IRepoQuery query);
	}}
}}
");

            return E();
        }
    }
}