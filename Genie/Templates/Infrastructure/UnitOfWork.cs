#region Usings

using System.Text;
using Genie.Core.Base.Generating.Concrete;
using Genie.Core.Base.Reading.Abstract;

#endregion

namespace Genie.Core.Templates.Infrastructure
{
    internal class UnitOfWorkTemplate : GenieTemplate
    {
        private readonly IDatabaseSchema _schema;

        public UnitOfWorkTemplate(string path, IDatabaseSchema schema) : base(path)
        {
            _schema = schema;
        }

        public override string Generate()
        {
            var relationFields = new StringBuilder();
            var viewFields = new StringBuilder();
            var relationRepositories = new StringBuilder();
            var viewRepositories = new StringBuilder();

            foreach (var relation in _schema.Relations)
            {
                relationFields.AppendLine(
                    $@"        private I{relation.Name}Repository {relation.FieldName}Repository;");
                relationRepositories.AppendLine(
                    $@"        public I{relation.Name}Repository {relation.Name}Repository {{ get {{ return {
                            relation.FieldName
                        }Repository ?? ({relation.FieldName}Repository = new {
                            relation.Name
                        }Repository(Context, this)); }} }}");
            }

            foreach (var view in _schema.Views)
            {
                viewFields.AppendLine($@"        private I{view.Name}Repository {view.FieldName}Repository;");
                viewRepositories.AppendLine(
                    $@"        public I{view.Name}Repository {view.Name}Repository {{ get {{ return {
                            view.FieldName
                        }Repository ?? ({view.FieldName}Repository = new {view.Name}Repository(Context)); }} }}");
            }

            L($@"

using System;
using System.Collections.Generic;
using System.Data;
using System.Linq;
using {GenerationContext.BaseNamespace}.Dapper;
using {GenerationContext.BaseNamespace}.Infrastructure.Interfaces;
using {GenerationContext.BaseNamespace}.Infrastructure.Models.Concrete;
using {GenerationContext.BaseNamespace}.Infrastructure.Repositories.Abstract;
using {GenerationContext.BaseNamespace}.Infrastructure.Repositories.Concrete;

namespace {GenerationContext.BaseNamespace}.Infrastructure
{{
    public class UnitOfWork : IUnitOfWork, IDisposable
    {{
		private IProcedureContainer _procedureContainer;

        private readonly List<IOperation> _operations;
        private readonly HashSet<BaseModel> _objects;

{relationFields}

{viewFields}

		public IProcedureContainer Procedures {{ get {{ return _procedureContainer ?? ( _procedureContainer = new ProcedureContainer(Context)); }} }}

        private IDapperContext Context {{ get;}}
        private IDbTransaction Transaction {{ get; set; }}

        public UnitOfWork(IDapperContext context)
        {{
            Context = context;
            _objects = new HashSet<BaseModel>();
            _operations = new List<IOperation>();
        }}
            
            
{relationRepositories}

{viewRepositories}

        public IDbTransaction BeginTransaction()
        {{
            if (Transaction != null)
            {{
                throw new NullReferenceException(""Not finished previous transaction"");
            }}
            Transaction = Context.Connection.BeginTransaction();
            return Transaction;
        }}


        public void Commit()
        {{
            var updated = _objects.Where(o => o.UpdatedProperties != null && o.UpdatedProperties.Count > 0).ToList();

            try
            {{
                if (updated.Count > 0)
                    _operations.AddRange(updated.Select(u => new Operation(OperationType.Update, u)));

                if (_operations.Count > 0)
                {{
                    var toAdd = _operations.Where(o => o.Type == OperationType.Add).ToList();
                    var toDelete = _operations.Where(o => o.Type == OperationType.Remove).ToList();
                    var toUpdate = _operations.Where(o => o.Type == OperationType.Update).ToList();

                    var connection = Context.Connection;

					if (toDelete.Count > 0)
					{{
                    	foreach (var operation in toDelete)
						{{
                            var deleted = connection.Delete(operation.Object);
                            if (deleted) {{ operation.Object.DatabaseModelStatus = ModelStatus.Deleted; }}
                        }}
					}}

					if (toAdd.Count > 0)
					{{
                        foreach (var operation in toAdd)
                        {{
                            var newId = connection.Insert(operation.Object);
                             if(newId != null)
                                operation.Object.SetId((int)newId);
                            operation.Object.DatabaseModelStatus = ModelStatus.Retrieved;
                            if (operation.Object.ActionsToRunWhenAdding != null && operation.Object.ActionsToRunWhenAdding.Count > 0)
                            {{
                                foreach (var addAction in operation.Object.ActionsToRunWhenAdding)
                                    addAction.Run();
                                operation.Object.ActionsToRunWhenAdding.Clear();
                            }}
                        }}
                    }}

					if (toUpdate.Count > 0)
					{{
						foreach (var operation in toUpdate)
						{{
                            connection.Update(operation.Object);
                            operation.Object.UpdatedProperties.Clear();
                        }}
					}}
                    
					_operations.Clear();
                }}
            }}
            catch (Exception e)
            {{
                throw new Exception(""Unable to commit changes"", e);
            }}
        }}


        public void Dispose()
        {{
            if (Transaction != null)
            {{
                Transaction.Dispose();
            }}
        }}

        public void AddOp(IOperation operation)
        {{
            _operations.Add(operation);
        }}

        public void AddObj(BaseModel obj)
        {{
            _objects.Add(obj);
        }}    
    }}
}}
");

            return E();
        }
    }
}