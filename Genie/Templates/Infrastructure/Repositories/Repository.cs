using Genie.Core.Models.Abstract;

namespace Genie.Core.Templates.Infrastructure.Repositories
{
    public class RepositoryTemplate : GenieTemplate
    {
        private readonly IModel _model;

        public RepositoryTemplate(string path, IModel model) : base(path)
        {
            _model = model;
        }

        public override string Generate()
        {
            return _model is IRelation relation ? Relation(relation) : View(_model as IView);
        }

        private string Relation(IRelation relation)
        {
            const string template =
@"
	        internal class {{name}}Repository : Repository<Models.Concrete.{{name}}> , I{{name}}Repository
            {
                internal {{name}}Repository(IDBContext context, IUnitOfWork unitOfWork) : base(context, unitOfWork) {}
        
		        public I{{name}}QueryContext Get() { return new {{name}}QueryContext(this); }

{% if relation.hasKeys %}
                public Models.Concrete.{{name}} GetByKey({{relation.keyString}})
		        {
                    return Get().Where{{relation.keyGetter}}.Filter().FirstOrDefault();
                }

                public async Task<Models.Concrete.{{name}}> GetByKeyAsync({{relation.keyString}})
                {
                    return await Get().Where{{relation.keyGetter}}.Filter().FirstOrDefaultAsync();
                }

                public void RemoveByKey({{relation.keyString}})
                {
                    Remove(new Models.Concrete.{{name}}
                    {
{{relation.removeKeys}}
                        __DatabaseModelStatus = ModelStatus.Retrieved
                    });
                }
{% endif %}
            }
";

            return Process($"{nameof(RepositoryTemplate)}Repository", template, new
            {
                name = relation.GetName(),
                relation,
            });
        }

        private string View(IView view)
        {
            const string template =
@"
	        internal class {{name}}Repository : ReadOnlyRepository<Models.Concrete.{{name}}>, I{{name}}Repository
            {
                internal {{name}}Repository(IDBContext context) : base(context) {}
                public I{{name}}QueryContext Get() { return new {{name}}QueryContext(this); }
            }
";
            return Process($"{nameof(RepositoryTemplate)}View", template, new
            {
                name = view.Name
            });
        }
    }
}
