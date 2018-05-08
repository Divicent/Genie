using Genie.Core.Models.Abstract;

namespace Genie.Core.Templates.Infrastructure.Repositories
{
    public class IRepositoryTemplate: GenieTemplate
    {
        private readonly IModel _model;
        public IRepositoryTemplate(string path, IModel model) : base(path)
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
            /// <summary>
            /// An API to access data of the data source Models.Concrete.{{name}}
            /// </summary>
            public interface I{{name}}Repository : IRepository<Models.Concrete.{{name}}>
            {
                /// <summary>
                /// Get a new query context to query the data source
                /// </summary>
                /// <returns>A query context</returns>
                I{{name}}QueryContext Get();

{% if relation.hasKeys %}
                /// <summary>
                /// Get an object of {{name}} using the values of its primary key(s)
                /// </summary>
{{relation.keyCommentString}}
                /// <returns>A registered {{name}} object</returns>
                Models.Concrete.{{name}} GetByKey({{relation.keyString}});

                /// <summary>
                /// Get an object of {{name}} asynchronously using the values of its primary key(s)
                /// </summary>
{{relation.keyCommentString}}
                /// <returns>A registered {{name}} object</returns>
                Task<Models.Concrete.{{name}}> GetByKeyAsync({{relation.keyString}});

                /// <summary>
                /// Remove an object of {{name}} using the values of its primary key(s)
                /// </summary>
{{relation.keyCommentString}}
                void RemoveByKey({{relation.keyString}});
{% endif %}
            }
";
            return Process(nameof(IRepositoryTemplate) + "Relation", template, new
            {
                name = relation.Name,
                relation
            });
        }

        private string View(IView view)
        {
            const string template =
@"
            /// <summary>
            /// A read only API to access data of the data source {{name}}
            /// </summary>
            public interface I{{name}}Repository : IReadOnlyRepository<Models.Concrete.{{name}}>
            {
                /// <summary>
                /// Get a new query context to query the data source
                /// </summary>
                /// <returns>A query context</returns>
		        I{{name}}QueryContext Get();
            }
";
            return Process(nameof(IRepositoryTemplate) + "View", template, new
            {
                name = view.Name,
            });
        }
    }
}
