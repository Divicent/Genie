#region Usings

using Genie.Core.Base.Configuration.Abstract;
using Genie.Core.Base.Generating;
using Genie.Core.Tools;

#endregion

namespace Genie.Core.Templates.Dapper
{
    public class SqlMapperExtensionsTemplate : GenieTemplate
    {
        private readonly IConfiguration _configuration;

        public SqlMapperExtensionsTemplate(string path, IConfiguration configuration) : base(path)
        {
            _configuration = configuration;
        }

        public override string Generate()
        {
            const string write = ".First()";
            const string getTypeInfo = "GetTypeInfo().";

            var quote = FormatHelper.GetDbmsSpecificQuoter(_configuration);
            var container = FormatHelper.GetDbmsSpecificTemplatePartsContainer(_configuration);

            var getRetriveQueryNewQueryBuilder = "";
            var getRetriveQueryPaging = "";

            if (_configuration.DBMS == "mssql")
            {
                getRetriveQueryNewQueryBuilder =
                    $@"            var queryBuilder = new StringBuilder(whereOnly ? """" : string.Format(""select {{0}} {{1}} from "" + query.Target, query.Limit != null ? "" top "" + query.Limit : """", isCount ? ""count(*)"" : CreateSelectColumnList(query.Columns, query.Target)));";
                getRetriveQueryPaging =
                    $@"	        if (query.Page != null && query.PageSize != null)
	        {{
	            queryBuilder.Append($"" OFFSET ({{query.Page * query.PageSize}}) ROWS "" + $"" FETCH NEXT {{query.PageSize}} ROWS ONLY "");
	        }}
	        else
	        {{
	            if (query.Skip != null)
	                queryBuilder.Append($"" OFFSET ({{query.Skip}}) ROWS "");

	            if (query.Take != null)
	                queryBuilder.Append($"" FETCH NEXT {{query.Take}} ROWS ONLY "");
	        }}
";
            }
            else if (_configuration.DBMS == "mysql")
            {
                getRetriveQueryNewQueryBuilder =
                    $@"            var queryBuilder = new StringBuilder(whereOnly ? """" : string.Format(""select {{0}} from "" + query.Target, isCount ? ""count(*)"" : CreateSelectColumnList(query.Columns, query.Target)));";
                getRetriveQueryPaging =
                    $@"	        if (query.Page != null && query.PageSize != null)
	        {{
	            queryBuilder.Append($"" LIMIT {{query.Page * query.PageSize}}, {{query.PageSize}} "");
	        }}
	        else if(query.Skip != null || query.Take != null || query.Limit != null)
	        {{
                queryBuilder.Append($"" LIMIT {{query.Skip??0}},  {{query.Take??query.Limit??0}} "");
	        }}";
            }

            L($@"

using System;
using System.Data;
using System.Threading.Tasks;
using {container.SqlClientNamespace};
using System.Linq;
using {GenerationContext.BaseNamespace}.Infrastructure.Models.Concrete;
using QB = {GenerationContext.BaseNamespace}.Infrastructure.Querying.QueryBuilder;

namespace {GenerationContext.BaseNamespace}.Dapper
{{
	public static class SqlMapperExtensions
    {{
        /// <summary>
        /// Inserts an entity into table ""T"" and returns identity id.
        /// </summary>
        /// <param name=""connection"">Open {container.SqlConnectionClassName}</param>
        /// <param name=""entityToInsert"">Entity to insert</param>
        /// <param name=""transaction""></param>
        /// <param name=""commandTimeout""></param>
        /// <returns>Identity of inserted entity</returns>
        public static long? Insert(this IDbConnection connection, BaseModel entityToInsert, IDbTransaction transaction = null, int? commandTimeout = null)
        {{
			using(connection = new {container.SqlConnectionClassName}(connection.ConnectionString))
			{{
				connection.Open();
				var cmd = QB.Insert(entityToInsert);
                connection.Execute(cmd, entityToInsert, transaction, commandTimeout);
                var r = connection.Query(QB.GetId(), transaction: transaction, commandTimeout: commandTimeout).ToList();
                long id = 0;
                if (r.Any())
                {{
                    try
                    {{
                        id = (long)r.First().id;
                    }}
                    catch (Exception)
                    {{ /*Ignored*/ }}
                }}
                return id;
			}}
        }}



        /// <summary>
        /// Inserts an entity into table ""T"" and returns identity id asynchronously.
        /// </summary>
        /// <param name=""connection"">Open {container.SqlConnectionClassName}</param>
        /// <param name=""entityToInsert"">Entity to insert</param>
        /// <param name=""transaction""></param>
        /// <param name=""commandTimeout""></param>
        /// <returns>Identity of inserted entity</returns>
        public static async Task<long?> InsertAsync(this IDbConnection connection, BaseModel entityToInsert, IDbTransaction transaction = null, int? commandTimeout = null)
        {{
			using(connection = new {container.SqlConnectionClassName}(connection.ConnectionString))
			{{
				connection.Open();
				var cmd = QB.Insert(entityToInsert);
                await connection.ExecuteAsync(cmd, entityToInsert, transaction, commandTimeout);
                var r = (await connection.QueryAsync(QB.GetId(), transaction: transaction, commandTimeout: commandTimeout)).ToList();
                long id = 0;
                if (r.Any())
                {{
                    try
                    {{
                        id = (long)r.First().id;
                    }}
                    catch (Exception)
                    {{ /*Ignored*/ }}
                }}
                return id;
			}}
        }}

	    /// <summary>
	    /// Updates entity in table ""Ts"", checks if the entity is modified if the entity is tracked by the Get() extension.
	    /// </summary>
	    /// <param name=""connection"">Open {container.SqlConnectionClassName}</param>
	    /// <param name=""entityToUpdate"">Entity to be updated</param>
	    /// <param name=""transaction""></param>
	    /// <param name=""commandTimeout""></param>
	    /// <returns>true if updated, false if not found or not modified (tracked entities)</returns>
	    public static bool Update(this IDbConnection connection, BaseModel entityToUpdate, IDbTransaction transaction = null, int? commandTimeout = null)
        {{
            var query = QB.Update(entityToUpdate);
            if(query == null)
            {{
                return false;
            }}
			using(connection = new {container.SqlConnectionClassName}(connection.ConnectionString))
			{{
				connection.Open();
				var updated = connection.Execute(query, entityToUpdate, commandTimeout: commandTimeout, transaction: transaction);
				return updated > 0;
			}}
        }}

	    /// <summary>
	    /// Updates entity in table ""Ts"", checks if the entity is modified if the entity is tracked by the Get() extension asynchronously.
	    /// </summary>
	    /// <param name=""connection"">Open {container.SqlConnectionClassName}</param>
	    /// <param name=""entityToUpdate"">Entity to be updated</param>
	    /// <param name=""transaction""></param>
	    /// <param name=""commandTimeout""></param>
	    /// <returns>true if updated, false if not found or not modified (tracked entities)</returns>
	  	public static async Task<bool> UpdateAsync(this IDbConnection connection, BaseModel entityToUpdate, IDbTransaction transaction = null, int? commandTimeout = null)
        {{
            var query = QB.Update(entityToUpdate);
            if(query == null)
            {{
                return false;
            }}
			using(connection = new {container.SqlConnectionClassName}(connection.ConnectionString))
			{{
				connection.Open();
				var updated = await connection.ExecuteAsync(query, entityToUpdate, commandTimeout: commandTimeout, transaction: transaction);
				return updated > 0;
			}}
        }}

	    /// <summary>
	    /// Delete entity in table ""Ts"".
	    /// </summary>
	    /// <param name=""connection"">Open {container.SqlConnectionClassName}</param>
	    /// <param name=""entity""></param>
	    /// <param name=""transaction""></param>
	    /// <param name=""commandTimeout""></param>
	    /// <returns>true if deleted, false if not found</returns>
	    public static bool Delete(this IDbConnection connection, BaseModel entity, IDbTransaction transaction = null, int? commandTimeout = null)
        {{
			using(connection = new {container.SqlConnectionClassName}(connection.ConnectionString))
			{{
				connection.Open();
				var deleted = connection.Execute(QB.Delete(entity), entity, transaction: transaction, commandTimeout: commandTimeout) > 0;
				return deleted;
			}}
        }}


        /// <summary>
	    /// Delete entity in table ""Ts"" asynchronously.
	    /// </summary>
	    /// <param name=""connection"">Open {container.SqlConnectionClassName}</param>
	    /// <param name=""entity""></param>
	    /// <param name=""transaction""></param>
	    /// <param name=""commandTimeout""></param>
	    /// <returns>true if deleted, false if not found</returns>
	   	public static async Task<bool> DeleteAsync(this IDbConnection connection, BaseModel entity, IDbTransaction transaction = null, int? commandTimeout = null)
        {{
			using(connection = new {container.SqlConnectionClassName}(connection.ConnectionString))
			{{
				connection.Open();
				var deleted = await connection.ExecuteAsync(QB.Delete(entity), entity, transaction: transaction, commandTimeout: commandTimeout) > 0;
				return deleted;
			}}
        }}
    }}
}}");

            return E();
        }
    }
}