#region Usings

using System.Linq;
using System.Text;
using Genie.Core.Base.Generating;
using Genie.Core.Base.Reading.Abstract;

#endregion

namespace Genie.Core.Templates.Infrastructure.Repositories
{
    public class RepositoryImplementationTemplate : GenieTemplate
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

            foreach (var relation in _schema.Relations)
            {
                relations.AppendLine($@"
      /// <summary>
      /// An API to access data of the data source {relation.Name}
      /// </summary>
	    public interface I{relation.Name}Repository : IRepository<{relation.Name}>
	    {{
          /// <summary>
          /// Get a new query context to query the data source
          /// </summary>
          /// <returns>A query context</returns>
		      I{relation.Name}QueryContext Get();");
                var keys = relation.Attributes.Where(a => a.IsKey).ToList();
                var keyString = "";
                var keyCommentString = "";
                if (keys.Count > 0)
                {
                    keyString = keys.Aggregate("", (c, n) => c + ", " + n.DataType + " " + n.Name.ToLower())
                        .TrimStart(',').TrimStart(' ');
                    keyCommentString = keys.Aggregate("", (c, n) => c +
                                                                    $@"
            /// <param name=""{n.Name.ToLower()}"">Value for primary key {n.Name}</param>");
                    relations.AppendLine($@"
            /// <summary>
            /// Get an object of {relation.Name} using the values of its primary key(s)
            /// </summary>
{keyCommentString}
            /// <returns>A registered {relation.Name} object</returns>
            {relation.Name} GetByKey({keyString});
            
            /// <summary>
            /// Get an object of {relation.Name} asynchronously using the values of its primary key(s)
            /// </summary>
{keyCommentString}
            /// <returns>A registered {relation.Name} object</returns>
            Task<{relation.Name}> GetByKeyAsync({keyString});");
                }

                relations.AppendLine("	    }");


                relationsImpl.AppendLine(
                    $@"	    internal class {relation.Name}Repository : Repository<{relation.Name}> , I{
                            relation.Name
                        }Repository
	    {{
            internal {
                            relation.Name
                        }Repository(IDapperContext context, IUnitOfWork unitOfWork) : base(context, unitOfWork)
            {{
            }}

		        public I{relation.Name}QueryContext Get() {{ return new {relation.Name}QueryContext(this); }}

");
                if (keys.Count > 0)
                {
                    var str = keys.Aggregate("", (c, n) => c + ".And." + n.Name + ".EqualsTo(" + n.Name.ToLower() + ")")
                        .TrimStart('.').TrimStart('A').TrimStart('n').TrimStart('d');
                    relationsImpl.AppendLine($@"            public {relation.Name} GetByKey({keyString})
            {{
                return Get().Where
                    {str}.Filter().FirstOrDefault();
            }}");


                    relationsImpl.AppendLine(
                        $@"            public async Task<{relation.Name}> GetByKeyAsync({keyString})
            {{
                return await Get().Where
                    {str}.Filter().FirstOrDefaultAsync();
            }}");
                }

                relationsImpl.AppendLine($@"	    }}");
            }

            foreach (var view in _schema.Views)
            {
                views.AppendLine($@"
      /// <summary>
      /// A read only API to access data of the data source {view.Name}
      /// </summary>
	    public interface I{view.Name}Repository : IReadOnlyRepository<{view.Name}>
	    {{
          /// <summary>
          /// Get a new query context to query the data source
          /// </summary>
          /// <returns>A query context</returns>
		      I{view.Name}QueryContext Get();
	    }}");

                viewImpl.AppendLine(
                    $@"	    internal class {view.Name}Repository : ReadOnlyRepository<{view.Name}>, I{
                            view.Name
                        }Repository
	    {{
            internal {view.Name}Repository(IDapperContext context) : base(context)
            {{
            }}

		    public I{view.Name}QueryContext Get() {{ return new {view.Name}QueryContext(this); }}
	    }}");
            }

            L($@"

using System.Linq;
using System.Threading.Tasks;
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