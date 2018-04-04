using System.Linq;
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
            var name = relation.Name;
            L($@"
        /// <summary>
        /// An API to access data of the data source Models.Concrete.{name}
        /// </summary>
	    public interface I{name}Repository : IRepository<Models.Concrete.{name}>
	    {{
            /// <summary>
            /// Get a new query context to query the data source
            /// </summary>
            /// <returns>A query context</returns>
		    I{name}QueryContext Get();");
            var keys = relation.Attributes.Where(a => a.IsKey).ToList();
            if (keys.Count > 0)
            {
                var keyString = keys.Aggregate("", (c, n) => c + ", " + n.DataType + " " + n.Name.ToLower())
                    .TrimStart(',').TrimStart(' ');
                var keyCommentString = keys.Aggregate("", (c, n) => c +
                                                                    $@"
            /// <param name=""{n.Name.ToLower()}"">Value for primary key {n.Name}</param>");
                L($@"
            /// <summary>
            /// Get an object of {name} using the values of its primary key(s)
            /// </summary>
{keyCommentString}
            /// <returns>A registered {name} object</returns>
            Models.Concrete.{name} GetByKey({keyString});
            
            /// <summary>
            /// Get an object of {name} asynchronously using the values of its primary key(s)
            /// </summary>
{keyCommentString}
            /// <returns>A registered {name} object</returns>
            Task<Models.Concrete.{name}> GetByKeyAsync({keyString});
            
            /// <summary>
            /// Remove an object of {name} using the values of its primary key(s)
            /// </summary>
{keyCommentString}
            void RemoveByKey({keyString});

        }}
");
            }
            return E();
        }

        private string View(IView view)
        {
            L($@"
        /// <summary>
        /// A read only API to access data of the data source {view.Name}
        /// </summary>
	    public interface I{view.Name}Repository : IReadOnlyRepository<Models.Concrete.{view.Name}>
	    {{
            /// <summary>
            /// Get a new query context to query the data source
            /// </summary>
            /// <returns>A query context</returns>
		    I{view.Name}QueryContext Get();
	    }}");
            return E();
        }
    }
}
