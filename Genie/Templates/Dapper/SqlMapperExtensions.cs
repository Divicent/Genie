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
using System.Collections.Concurrent;
using System.Collections.Generic;
using System.Data;
using System.Threading.Tasks;
using {container.SqlClientNamespace};
using System.Linq;
using System.Reflection;
using System.Text;
using {GenerationContext.BaseNamespace}.Infrastructure.Models.Concrete;
using {GenerationContext.BaseNamespace}.Infrastructure.Filters.Abstract;

namespace {GenerationContext.BaseNamespace}.Dapper
{{
	public static class SqlMapperExtensions
    {{
        public interface IProxy
        {{
            bool IsDirty {{ get; set; }}
        }}


        public class SqlWhereOrderCache
        {{
            public string Sql {{ get; set; }}
            public IEnumerable<string> Where {{ get; set; }}
            public IEnumerable<string> Order {{ get; set; }}
        }}

        private static readonly ConcurrentDictionary<RuntimeTypeHandle, IEnumerable<PropertyInfo>> KeyProperties = new ConcurrentDictionary<RuntimeTypeHandle, IEnumerable<PropertyInfo>>();
        private static readonly ConcurrentDictionary<RuntimeTypeHandle, IEnumerable<PropertyInfo>> IdentityProperties = new ConcurrentDictionary<RuntimeTypeHandle, IEnumerable<PropertyInfo>>();
        private static readonly ConcurrentDictionary<RuntimeTypeHandle, IEnumerable<PropertyInfo>> TypeProperties = new ConcurrentDictionary<RuntimeTypeHandle, IEnumerable<PropertyInfo>>();
        private static readonly ConcurrentDictionary<RuntimeTypeHandle, string> TypeTableName = new ConcurrentDictionary<RuntimeTypeHandle, string>();
        private static readonly ConcurrentDictionary<string, string> SelectParts = new ConcurrentDictionary<string, string>();

        private static IEnumerable<PropertyInfo> KeyPropertiesCache(Type type)
        {{

            IEnumerable<PropertyInfo> pi;
            if (KeyProperties.TryGetValue(type.TypeHandle, out pi))
            {{
                return pi;
            }}

            var allProperties = TypePropertiesCache(type).ToList();
            var keyProperties = allProperties.Where(p => p.GetCustomAttributes(true).Any(a => a is KeyAttribute)).ToList();

            KeyProperties[type.TypeHandle] = keyProperties;
            return keyProperties;
        }}

        private static IEnumerable<PropertyInfo> IdentityPropertiesCache(Type type)
        {{

            IEnumerable<PropertyInfo> pi;
            if (IdentityProperties.TryGetValue(type.TypeHandle, out pi))
            {{
                return pi;
            }}

            var allProperties = TypePropertiesCache(type).ToList();
            var identityProperties = allProperties.Where(p => p.GetCustomAttributes(true).Any(a => a is IdentityAttribute)).ToList();

            IdentityProperties[type.TypeHandle] = identityProperties;
            return identityProperties;
        }}

        private static IEnumerable<PropertyInfo> TypePropertiesCache(Type type)
        {{
            IEnumerable<PropertyInfo> pis;
            if (TypeProperties.TryGetValue(type.TypeHandle, out pis))
            {{
                return pis;
            }}

            var properties = type.GetProperties().Where(IsWriteable).ToList();
            TypeProperties[type.TypeHandle] = properties;
            return properties;
        }}



        private static bool AndOrOr(string str)
	    {{
	        return str == ""and"" || str == ""or"";
	    }}


        private static string GetTableName(Type type)
        {{
            string name;
            if (TypeTableName.TryGetValue(type.TypeHandle, out name)) return name;
            name = type.Name + ""s"";
            if (type.{getTypeInfo}IsInterface && name.StartsWith(""I""))
                name = name.Substring(1);

            var tableattr = type.{
                    getTypeInfo
                }GetCustomAttributes(false).SingleOrDefault(attr => attr.GetType().Name == ""TableAttribute"") as
                dynamic;
            if (tableattr != null)
                name = tableattr.Name;
            TypeTableName[type.TypeHandle] = name;
            return name;
        }}


        /// <summary>
        /// Creates the select part of the query
        /// </summary>
        /// <param name=""columnNames"">columnNames</param>
        /// <param name=""target"">Target source</param>
        /// <returns>a string</returns>
        private static string CreateSelectColumnList(IEnumerable<string> columnNames, string target)
        {{
            string[] columnNamesList;
            if (string.IsNullOrWhiteSpace(target) || columnNames == null || (columnNamesList = columnNames.ToArray()).Length < 1)
                return ""*"";

            string result;
            if (SelectParts.TryGetValue(target, out result))
                return result;

            var builder = new StringBuilder();
            var first = true;
            foreach (var columnName in columnNamesList)
            {{
                builder.Append($""{{(!first ? "", "" : """")}}{quote("{columnName}")}"");
                first = false;
            }}
            return SelectParts[target] = builder.ToString();
        }}


        private static Tuple<string, string, string> GetInsertParameters(BaseModel entityToInsert)
        {{
            var type = entityToInsert.GetType();

            var name = GetTableName(type);

            var sbColumnList = new StringBuilder(null);

            var allProperties = TypePropertiesCache(type).ToList();
            var keyProperties = KeyPropertiesCache(type).ToList();
            var identityProperties = IdentityPropertiesCache(type).ToList();
            var allPropertiesExceptIndentity = allProperties.Except(identityProperties).ToList();

            var index = 0;
            var lst = allProperties.Count == keyProperties.Count ? keyProperties : allPropertiesExceptIndentity;
            foreach (var property in lst)
            {{
                sbColumnList.AppendFormat(""{quote("{0}")}"", property.Name);
                if (index < lst.Count - 1)
                sbColumnList.Append("", "");
                index++;
            }}

            index = 0;
            var sbParameterList = new StringBuilder(null);

            foreach (var property in lst)
            {{
                sbParameterList.AppendFormat(""@{{0}}"", property.Name);
                if (index < lst.Count - 1)
                sbParameterList.Append("", "");
                index++;
            }}

            return new Tuple<string, string, string>(name, sbColumnList.ToString(), sbParameterList.ToString());
        }}

        private static string BuildUpdateQuery(BaseModel entityToUpdate) 
        {{
            if (entityToUpdate.DatabaseModelStatus != ModelStatus.Retrieved)
                return null;

            if (entityToUpdate.UpdatedProperties == null || entityToUpdate.UpdatedProperties.Count < 1)
                return null;

            var type = entityToUpdate.GetType();

            var keyProperties = KeyPropertiesCache(type).ToList();
            if (!keyProperties.Any())
                throw new ArgumentException(""Entity must have at least one [Key] property"");

            var name = GetTableName(type);

            var sb = new StringBuilder();
            sb.AppendFormat(""update {{0}} set "", name);

            var allProperties = TypePropertiesCache(type);
            var nonIdProps = allProperties.Where(a => !keyProperties.Contains(a) && entityToUpdate.UpdatedProperties.Contains(a.Name)).ToList(); // Only updated properties


            for (var i = 0; i < nonIdProps.Count; i++)
            {{
                var property = nonIdProps.ElementAt(i);
                sb.AppendFormat(""{quote("{0}")} = @{{1}}"", property.Name, property.Name);
                if (i < nonIdProps.Count - 1)
                    sb.AppendFormat("", "");
            }}

            sb.Append("" where "");
            for (var i = 0; i < keyProperties.Count; i++)
            {{
                var property = keyProperties.ElementAt(i);
                sb.AppendFormat(""{quote("{0}")} = @{{1}}"", property.Name, property.Name);
                if (i < keyProperties.Count - 1)
                    sb.AppendFormat("" and "");
            }}

            return sb.ToString();
        }}


        private static string GetDeleteQuery(BaseModel entity) 
        {{
             if (entity == null)
	        {{
                throw new ArgumentException(""The entity is null, cannot delete a null entity"", nameof(entity));
            }}

            var type = entity.GetType();
            var keyProperties = KeyPropertiesCache(type).ToList();

            if (!keyProperties.Any())
                throw new ArgumentException(""Entity must have at least one [Key] property"");

            var name = GetTableName(type);

            var sb = new StringBuilder();
            sb.AppendFormat(""delete from {{0}} where "", name);

            for (var i = 0; i < keyProperties.Count; i++)
            {{
                var property = keyProperties.ElementAt(i);
                sb.AppendFormat(""{quote("{0}")} = @{{1}}"", property.Name, property.Name);
                if (i < keyProperties.Count - 1)
                    sb.AppendFormat("" and "");
            }}

            return sb.ToString();
        }}

	    private static string GetRetriveQuery(IRepoQuery query, bool isCount = false, bool whereOnly = false)
	    {{
{getRetriveQueryNewQueryBuilder}
            
            var where = query.Where == null ? new Queue<string>() : new Queue<string>(query.Where);
            var order = query.Order == null ? new Queue<string>() : new Queue<string>(query.Order);

	        if (where.Count > 0)
	        {{
	            queryBuilder.Append("" where "");

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
	                    queryBuilder.Append($"" {{current}} "");
	                }}
                    else if (current == "")"" || current == ""("")
	                {{
	                    if (current == ""("" && !first && !AndOrOr(previous))
	                        queryBuilder.Append("" and "");

	                    previous = current;
                        queryBuilder.Append($"" {{current}} "");
                    }}
	                else
	                {{
	                    if (!first && (previous != ""("" && previous != "")"") && !AndOrOr(previous))
                        {{
	                        queryBuilder.Append("" and "");
	                    }}

	                    previous = current;
	                    queryBuilder.Append($"" {{current}} "");
	                }}

	                first = false;
	            }}
	        }}

	        if (whereOnly)
	            return queryBuilder.ToString();

	        if (order.Count > 0)
	        {{
	            queryBuilder.Append("" order by "");
	            while (order.Count > 0)
	            {{
	                var item = order.Dequeue();
	                queryBuilder.Append($"" {{item}} "");
	            }}
	        }}

{getRetriveQueryPaging}

	        return queryBuilder.ToString();
        }}



        public static bool IsWriteable(PropertyInfo pi)
        {{
            var attributes = pi.GetCustomAttributes(typeof(WriteAttribute), false).ToList();
            if (attributes.Count != 1)
                return true;
            var write = (WriteAttribute)attributes{write};
            return write.Write;
        }}

	    /// <summary>
	    /// Return all  
	    /// </summary>
	    /// <typeparam name=""T"">Interface type to create and populate</typeparam>
	    /// <param name=""connection"">Open {container.SqlConnectionClassName}</param>
	    /// <param name=""query""></param>
	    /// <returns>Entity of T</returns>
	    public static IEnumerable<T> Get<T>(this IDbConnection connection, IRepoQuery query)
        {{
			using(connection = new {container.SqlConnectionClassName}(connection.ConnectionString))
			{{
				connection.Open();
				return connection.Query<T>(GetRetriveQuery(query), transaction: query.Transaction);
			}}
        }}


        /// <summary>
	    /// Return all  asynchronously
	    /// </summary>
	    /// <typeparam name=""T"">Interface type to create and populate</typeparam>
	    /// <param name=""connection"">Open {container.SqlConnectionClassName}</param>
	    /// <param name=""query""></param>
	    /// <returns>Entity of T</returns>
	    public static async Task<IEnumerable<T>> GetAsync<T>(this IDbConnection connection, IRepoQuery query)
        {{
			using(connection = new {container.SqlConnectionClassName}(connection.ConnectionString))
			{{
				connection.Open();
				return await connection.QueryAsync<T>(GetRetriveQuery(query), transaction: query.Transaction);
			}}
        }}

	    /// <summary>
	    /// Returns count of rows
	    /// </summary>
	    /// <param name=""connection"">Open {container.SqlConnectionClassName}</param>
	    /// <param name=""query""></param>
	    /// <returns>Entity of T</returns>
	    public static int  Count(this IDbConnection connection, IRepoQuery query)
        {{
			using(connection = new {container.SqlConnectionClassName}(connection.ConnectionString))
			{{
				return connection.ExecuteScalar<int>(GetRetriveQuery(query, true), transaction: query.Transaction);
			}}
        }}
        
	    /// <summary>
	    /// Returns count of rows asynchronously
	    /// </summary>
	    /// <param name=""connection"">Open {container.SqlConnectionClassName}</param>
	    /// <param name=""query""></param>
	    /// <returns>Entity of T</returns>
	    public static async Task<int> CountAsync(this IDbConnection connection, IRepoQuery query)
        {{
			using(connection = new {container.SqlConnectionClassName}(connection.ConnectionString))
			{{
				return await connection.ExecuteScalarAsync<int>(GetRetriveQuery(query, true), transaction: query.Transaction);
			}}
        }}

		/// <summary>
	    /// Returns the where clause of the resulting query
	    /// </summary>
	    /// <param name=""connection"">Open {container.SqlConnectionClassName}</param>
	    /// <param name=""query""></param>
	    /// <returns>Entity of T</returns>
	    public static string GetWhereClause(this IDbConnection connection, IRepoQuery query)
	    {{
	        return GetRetriveQuery(query, false, true);
	    }}


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
            var parameters = GetInsertParameters(entityToInsert);
			using(connection = new {container.SqlConnectionClassName}(connection.ConnectionString))
			{{
				connection.Open();
				var cmd = string.Format(""insert into {{0}} ({{1}}) values ({{2}})"", parameters.Item1, parameters.Item2, parameters.Item3);
                connection.Execute(cmd, entityToInsert, transaction, commandTimeout);
                var r = connection.Query(""select @@IDENTITY id"", transaction: transaction, commandTimeout: commandTimeout).ToList();
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
            var parameters = GetInsertParameters(entityToInsert);
			using(connection = new {container.SqlConnectionClassName}(connection.ConnectionString))
			{{
				connection.Open();
				var cmd = string.Format(""insert into {{0}} ({{1}}) values ({{2}})"", parameters.Item1, parameters.Item2, parameters.Item3);
                await connection.ExecuteAsync(cmd, entityToInsert, transaction, commandTimeout);
                var r = (await connection.QueryAsync(""select @@IDENTITY id"", transaction: transaction, commandTimeout: commandTimeout)).ToList();
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
            var query = BuildUpdateQuery(entityToUpdate);
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
            var query = BuildUpdateQuery(entityToUpdate);
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
				var deleted = connection.Execute(GetDeleteQuery(entity), entity, transaction: transaction, commandTimeout: commandTimeout) > 0;
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
				var deleted = await connection.ExecuteAsync(GetDeleteQuery(entity), entity, transaction: transaction, commandTimeout: commandTimeout) > 0;
				return deleted;
			}}
        }}
    }}
}}");

            return E();
        }
    }
}