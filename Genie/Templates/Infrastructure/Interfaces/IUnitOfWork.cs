using System.Text;
using Genie.Base.Generating.Concrete;
using Genie.Base.Reading.Abstract;
using Genie.Templates;

namespace Genie.Templates.Infrastructure.Interfaces
{
    internal class IUnitOfWorkTemplate: GenieTemplate
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

    foreach(var relation in _schema.Relations)
    {
        relations.AppendLine($@"      I{relation.Name}Repository {relation.Name}Repository {{ get; }}");
    }

    foreach(var view in _schema.Views)
    {
        views.AppendLine($@"      I{view.Name}Repository {view.Name}Repository {{ get; }}");
    }

L($@"

using System;
using System.Data;
using {GenerationContext.BaseNamespace}.Infrastructure.Models.Concrete;
using System.Collections.Generic;
using {GenerationContext.BaseNamespace}.Infrastructure.Repositories.Abstract;

namespace {GenerationContext.BaseNamespace}.Infrastructure.Interfaces
{{
	public interface IUnitOfWork : IDisposable
    {{

        IDbTransaction BeginTransaction();

	    void AddOp(IOperation operation);
        void AddObj(BaseModel obj);

{relations}

{views}

		IProcedureContainer Procedures {{ get; }}
        
        void Commit();
    }}
}}
");

return E();
    
}
    }
}