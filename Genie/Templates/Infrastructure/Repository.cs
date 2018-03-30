#region Usings

#endregion

using Genie.Core.Base.Configuration.Abstract;
using Genie.Core.Base.Generating;
using Genie.Core.Tools;


namespace Genie.Core.Templates.Infrastructure
{
    public class RepositoryTemplate : GenieTemplate
    {
        private readonly IConfiguration _configuration;
        
        public RepositoryTemplate(string path, IConfiguration configuration) : base(path)
        {
            _configuration = configuration;
        }

        public override string Generate()
        {
            var container = FormatHelper.GetDbmsSpecificTemplatePartsContainer(_configuration);
            L($@"

using System;
using System.Collections.Generic;
using System.Data;
using System.Threading.Tasks;
using System.Linq;
using {container.SqlClientNamespace};
using {GenerationContext.BaseNamespace}.Infrastructure.Filters.Abstract;
using {GenerationContext.BaseNamespace}.Dapper;
using {GenerationContext.BaseNamespace}.Infrastructure.Interfaces;
using {GenerationContext.BaseNamespace}.Infrastructure.Models.Concrete;
using {GenerationContext.BaseNamespace}.Infrastructure.Querying;

namespace {GenerationContext.BaseNamespace}.Infrastructure
{{
    public abstract class Repository<T> : IRepository<T>
        where T : BaseModel 
    {{
        public IDbConnection Conn {{ get; }}
        public IDBContext Context {{ get;}}
        private IUnitOfWork UnitOfWork {{ get;}}

        protected Repository(IDBContext context, IUnitOfWork unitOfWork)
        {{
            Context = context;
            Conn = Context.Connection;
            UnitOfWork = unitOfWork;
        }}

        public virtual void Add(T entity, IDbTransaction transaction = null, int? commandTimeout = null)
        {{
            if (entity == null)
            {{
                throw new ArgumentNullException(""entity"", ""Add to DB null entity"");
            }}
            
            entity.DatabaseUnitOfWork = UnitOfWork;           
            var operation = new Operation(OperationType.Add, entity);
            UnitOfWork.AddOp(operation);    
            entity.DatabaseModelStatus = ModelStatus.ToAdd;  
        }}

        public virtual void Add(IEnumerable<T> entities, IDbTransaction transaction = null, int? commandTimeout = null)
        {{
            if (entities == null)
            {{
                throw new ArgumentNullException(""entities"", ""Add to DB null entity"");
            }}
            
            foreach(var entity in entities)
                Add(entity, transaction, commandTimeout);
        }}

        public virtual void Remove(T entity, IDbTransaction transaction = null, int? commandTimeout = null)
        {{
            if (entity == null)
            {{
                throw new ArgumentNullException(""entity"", ""Remove in DB null entity"");
            }}
            
            var operation = new Operation(OperationType.Remove, entity);
            UnitOfWork.AddOp(operation);
        }}

        public virtual void Remove(IEnumerable<T> entities, IDbTransaction transaction = null, int? commandTimeout = null)
        {{
            if (entities == null)
            {{
                throw new ArgumentNullException(""entities"", ""Remove in DB null entity"");
            }}

            foreach(var entity in entities)
                Remove(entity, transaction, commandTimeout);
        }}

        public virtual IEnumerable<T> Get(IRepoQuery query)
        {{
            using (var connection = new {container.SqlConnectionClassName}(Conn.ConnectionString))
            {{
                var items = connection.Query<T>(new QueryBuilder(query).Get()).ToList();
                foreach (var item in items)
                    AddItemToUnit(item);
                return items;    
            }}
        }}

        public virtual async Task<IEnumerable<T>> GetAsync(IRepoQuery query)
        {{
            using (var connection = new {container.SqlConnectionClassName}(Conn.ConnectionString))
            {{
                var items = (await connection.QueryAsync<T>(new QueryBuilder(query).Get())).ToList();
                foreach (var item in items)
                    AddItemToUnit(item);
                return items;    
            }}
        }}

		public virtual T GetFirstOrDefault(IRepoQuery query)
        {{
            using (var connection = new {container.SqlConnectionClassName}(Conn.ConnectionString))
            {{
                var item = connection.QuerySingleOrDefault<T>(new QueryBuilder(query).Get());
                if(item == null)
                    return null;
                AddItemToUnit(item);
                return item;  
            }}
        }}

        public virtual async Task<T> GetFirstOrDefaultAsync(IRepoQuery query)
        {{
            using (var connection = new {container.SqlConnectionClassName}(Conn.ConnectionString))
            {{
                var item = await connection.QuerySingleOrDefaultAsync<T>(new QueryBuilder(query).Get());
                if(item == null)
                    return null;
                AddItemToUnit(item);
                return item;  
            }}
        }}

        public virtual int Count(IRepoQuery query)
        {{
            using (var connection = new {container.SqlConnectionClassName}(Conn.ConnectionString))
            {{
                return  connection.ExecuteScalar<int>(new QueryBuilder(query).Count());
            }}
        }}

        public virtual async Task<int> CountAsync(IRepoQuery query)
        {{
            using (var connection = new {container.SqlConnectionClassName}(Conn.ConnectionString))
            {{
                return await connection.ExecuteScalarAsync<int>(new QueryBuilder(query).Count());
            }}
        }}


        public virtual TA SumBy<TA>(IRepoQuery query, string column)
        {{
            using (var connection = new {container.SqlConnectionClassName}(Conn.ConnectionString))
            {{
                return  connection.ExecuteScalar<TA>(new QueryBuilder(query).SumBy(column));
            }}
        }}


        public virtual async Task<TA> SumByAsync<TA>(IRepoQuery query, string column)
        {{
            using (var connection = new {container.SqlConnectionClassName}(Conn.ConnectionString))
            {{
                return await connection.ExecuteScalarAsync<TA>(new QueryBuilder(query).SumBy(column));
            }}
        }}

		public string GetWhereClause(IRepoQuery query) 
		{{
			return new QueryBuilder(query).WhereClause();
		}}

		private void AddItemToUnit(T item) 
		{{
			item.DatabaseUnitOfWork = UnitOfWork;
            item.DatabaseModelStatus = ModelStatus.Retrieved;
            UnitOfWork.AddObj(item);
		}}
    }}
}}
");

            return E();
        }
    }
}