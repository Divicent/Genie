#region Usings



#endregion

using Genie.Core.Base.Generating;

namespace Genie.Core.Templates.Infrastructure.Interfaces
{
    internal class IRepositoryTemplate : GenieTemplate
    {
        public IRepositoryTemplate(string path) : base(path)
        {
        }

        public override string Generate()
        {
            L($@"

using System.Collections.Generic;
using System.Data;
using System.Threading.Tasks;
using {GenerationContext.BaseNamespace}.Infrastructure.Models.Concrete;
using {GenerationContext.BaseNamespace}.Infrastructure.Filters.Abstract;

namespace {GenerationContext.BaseNamespace}.Infrastructure.Interfaces
{{
    /// <summary>
    /// A repository is an interface which provides CRUD operations on a data source.
    /// </summary>
    /// <typeparam name=""T"">Object type of the repository</typeparam>
	public interface IRepository<T>
        where T : BaseModel
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
        /// Add an entity to the database . this will add an operation to unit and will be committed to the source when commit is called
        /// </summary>
        /// <param name=""entity"">Entity to add</param>
        /// <param name=""transaction"">Transaction if available</param>
        /// <param name=""commandTimeout"">Timeout</param>
        void Add(T entity, IDbTransaction transaction = null, int? commandTimeout = null);

        /// <summary>
        /// Add a list of entities to the database . this will add an operation to unit and will be committed to the source when commit is called
        /// </summary>
        /// <param name=""entities"">Entities to add</param>
        /// <param name=""transaction"">Transaction if available</param>
        /// <param name=""commandTimeout"">Timeout</param>
        void Add(IEnumerable<T> entities, IDbTransaction transaction = null, int? commandTimeout = null);
        
        /// <summary>
        ///  Remove an entity from the database . this will add an operation to unit and will be committed to the source when commit is called
        /// </summary>
        /// <param name=""entity"">Entity to remove</param>
        /// <param name=""transaction"">Transaction if available</param>
        /// <param name=""commandTimeout"">Timeout</param>
        void Remove(T entity, IDbTransaction transaction = null, int? commandTimeout = null);
        
         /// <summary>
        /// Remove a list entity from the database . this will add an operation to unit and will be committed to the source when commit is called
        /// </summary>
        /// <param name=""entity"">Entities to remove</param>
        /// <param name=""transaction"">Transaction if available</param>
        /// <param name=""commandTimeout"">Timeout</param>
        void Remove(IEnumerable<T> entity, IDbTransaction transaction = null, int? commandTimeout = null);

        /// <summary>
        /// Executes given query on repository
        /// </summary>
        /// <param name=""query"">Query to use</param>
        /// <returns>Collection of <typeparamref name=""T""/> </returns>
        IEnumerable<T> Get(IRepoQuery query);

        /// <summary>
        /// Asynchronously Executes given query on repository
        /// </summary>
        /// <param name=""query"">Query to use</param>
        /// <returns>Collection of <typeparamref name=""T""/> </returns>
        Task<IEnumerable<T>> GetAsync(IRepoQuery query);
        
        /// <summary>
        /// Get the first occurrence or null if the result is empty from the query
        /// </summary>
        /// <param name=""query"">Query to execute in the repository</param>
        /// <returns>an object with type <typeparamref name=""T""/></returns>
		T GetFirstOrDefault(IRepoQuery query);

        /// <summary>
        /// Asynchronously get the first occurrence or null if the result is empty from the query
        /// </summary>
        /// <param name=""query"">Query to execute in the repository</param>
        /// <returns>an object with type <typeparamref name=""T""/></returns>
		Task<T> GetFirstOrDefaultAsync(IRepoQuery query);
	      
        /// <summary>
        /// Executes given query on the repository and returns count of the result set
        /// </summary>
        /// <param name=""query"">Query to execute in the repository</param>
        /// <returns>an integer</returns>
        int Count(IRepoQuery query);

        /// <summary>
        /// Asynchronously executes given query on the repository and returns count of the result set
        /// </summary>
        /// <param name=""query"">Query to execute in the repository</param>
        /// <returns>an integer</returns>
        Task<int> CountAsync(IRepoQuery query);
		    
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