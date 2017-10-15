#region Usings

using System.Text;
using Genie.Core.Base.Generating;
using Genie.Core.Base.Reading.Abstract;

#endregion

namespace Genie.Core.Templates.Infrastructure.Interfaces
{
    internal class IUnitOfWorkTemplate : GenieTemplate
    {
        private readonly IDatabaseSchema _schema;

        public IUnitOfWorkTemplate(string path, IDatabaseSchema schema) : base(path)
        {
            _schema = schema;
        }

        public override string Generate()
        {
            var relations = new StringBuilder();
            var views = new StringBuilder();

            foreach (var relation in _schema.Relations)
            {
                relations.AppendLine($@"
      /// <summary>
      /// The {relation.Name} repository that belongs to this unit of work.
      /// </summary>
      I{relation.Name}Repository {relation.Name}Repository {{ get; }}
");
            }

            foreach (var view in _schema.Views)
            {
                views.AppendLine($@"
      /// <summary>
      /// The {view.Name} repository (read only) that belongs to this unit of work.
      /// </summary>
      I{view.Name}Repository {view.Name}Repository {{ get; }}
");
            }

            L($@"

using System;
using System.Data;
using {GenerationContext.BaseNamespace}.Infrastructure.Models.Concrete;
using System.Collections.Generic;
using {GenerationContext.BaseNamespace}.Infrastructure.Repositories.Abstract;

namespace {GenerationContext.BaseNamespace}.Infrastructure.Interfaces
{{
  /// <summary>
  /// A unit of work is a collection of operations on a data source. operations could be updates, deletes, inserts
  /// </summary>
	public interface IUnitOfWork : IDisposable
    {{

        /// <summary>
        /// Starts a transaction on this unit of work
        /// </summary>
        /// <returns>A new transaction</returns>
        IDbTransaction BeginTransaction();

	      void AddOp(IOperation operation);
        void AddObj(BaseModel obj);

{relations}

{views}

        /// <summary>
        /// The procedure container that belongs to this unit of work
        /// </summary>
		    IProcedureContainer Procedures {{ get; }}
        
        /// <summary>
        /// This will commit all changes to the data source performed in this unit of work one by one. including all tracked changes of the objects retrieved from this unit of work
        /// </summary>
        void Commit();
    }}
}}
");

            return E();
        }
    }
}