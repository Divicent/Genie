using System.Linq;
using System.Text;
using Genie.Base.Generating.Concrete;
using Genie.Base.Reading.Abstract;
using Genie.Templates;

namespace Genie.Templates.Infrastructure.Repositories
{
    internal class RepositoryImplementationTemplate: GenieTemplate
    {
        private readonly IDatabaseSchema _schema;
        public RepositoryImplementationTemplate(string path, IDatabaseSchema schema) : base(path) 
        {
            _schema = schema;
        }

public override string Generate()
{
    var relations = new StringBuilder();
    var views = new StringBuilder();

    var relationsImpl = new StringBuilder();
    var viewImpl = new StringBuilder();

    foreach(var relation in _schema.Relations) 
    {
        relations.AppendLine($@"	    public interface I{relation.Name}Repository : IRepository<{relation.Name}>
	    {{
		    I{relation.Name}QueryContext Get();");
        var keys= relation.Attributes.Where(a => a.IsKey).ToList();
        var keyString = "";
        if(keys.Count > 0) 
        {
            keyString = keys.Aggregate("", (c,n) => c + (", " + n.DataType  + " " + n.Name.ToLower())).TrimStart(',').TrimStart(' ');
            relations.AppendLine($@"            {relation.Name} GetByKey({keyString});");
        }
        relations.AppendLine("	    }");



        relationsImpl.AppendLine($@"	    internal class {relation.Name}Repository : Repository<{relation.Name}> , I{relation.Name}Repository
	    {{
            internal {relation.Name}Repository(IDapperContext context, IUnitOfWork unitOfWork) : base(context, unitOfWork)
            {{
            }}

		    public I{relation.Name}QueryContext Get() {{ return new {relation.Name}QueryContext(this); }}

");
        if(keys.Count > 0) 
        {
            var str = keys.Aggregate("", (c,n) => c + (".And." + n.Name + ".EqualsTo(" + n.Name.ToLower() + ")")).TrimStart('.').TrimStart('A').TrimStart('n').TrimStart('d');
            relationsImpl.AppendLine($@"            public {relation.Name} GetByKey({keyString})
            {{
                return Get().Where
                    {str}.Filter().Query().FirstOrDefault();
            }}");            
        }

        relationsImpl.AppendLine($@"	    }}");
    }

    foreach(var view in _schema.Views) 
    {
        views.AppendLine($@"	    public interface I{view.Name}Repository : IReadOnlyRepository<{view.Name}>
	    {{
		    I{view.Name}QueryContext Get();
	    }}");

        viewImpl.AppendLine($@"	    internal class {view.Name}Repository : ReadOnlyRepository<{view.Name}>, I{view.Name}Repository
	    {{
            internal {view.Name}Repository(IDapperContext context) : base(context)
            {{
            }}

		    public I{view.Name}QueryContext Get() {{ return new {view.Name}QueryContext(this); }}
	    }}");

    }
L($@"

using System.Linq;
using {GenerationContext.BaseNamespace}.Infrastructure.Interfaces;
using {GenerationContext.BaseNamespace}.Infrastructure.Models.Abstract.Context;
using {GenerationContext.BaseNamespace}.Infrastructure.Models.Concrete;
using {GenerationContext.BaseNamespace}.Infrastructure.Models.Concrete.Context;
using {GenerationContext.BaseNamespace}.Infrastructure.Repositories.Abstract;

namespace {GenerationContext.BaseNamespace}.Infrastructure.Repositories
{{

    namespace Abstract
    {{
{relations}

{views}

    }}

    namespace Concrete
    {{
{relationsImpl}

{viewImpl}
    }}
}}");

return E();
    
}
    }
}