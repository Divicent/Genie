using System.Collections.Generic;
using System.Linq;
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
            var name = relation.GetName();
            var keys = relation.Attributes.Where(a => a.IsKey).ToList();

            var keyString = keys.Aggregate("", (c, n) => c + ", " + n.DataType + " " + n.Name.ToLower())
                .TrimStart(',').TrimStart(' ');
            L(
                $@"	    internal class {name}Repository : Repository<Models.Concrete.{name}> , I{name}Repository
	    {{
            internal {name}Repository(IDBContext context, IUnitOfWork unitOfWork) : base(context, unitOfWork)
            {{
            }}

		    public I{name}QueryContext Get() {{ return new {name}QueryContext(this); }}

");
            if (keys.Count > 0)
            {
                var str = keys.Aggregate("", (c, n) => c + ".And." + n.Name + ".EqualsTo(" + n.Name.ToLower() + ")")
                    .TrimStart('.').TrimStart('A').TrimStart('n').TrimStart('d');
                L($@"            public Models.Concrete.{name} GetByKey({keyString})
		    {{
                return Get().Where
                    {str}.Filter().FirstOrDefault();
            }}
");


                L(
                    $@"            public async Task<Models.Concrete.{name}> GetByKeyAsync({keyString})
            {{
                return await Get().Where
                    {str}.Filter().FirstOrDefaultAsync();
            }}
");

                L(
                    $@"            public void RemoveByKey({keyString})
            {{
                Remove(new Models.Concrete.{name}
                {{
                    {keys.Aggregate("", (c, n) => $"{c}                    {n.Name} = {n.Name.ToLower()},\n")}
                    __DatabaseModelStatus = ModelStatus.Retrieved
                }});
            }}
}}");

            }

            return E();
        }

        private string View(IView view)
        {
            L(
                $@"	    internal class {view.Name}Repository : ReadOnlyRepository<Models.Concrete.{view.Name}>, I{view.Name}Repository
	    {{
            internal {view.Name}Repository(IDBContext context) : base(context)
            {{
            }}

		    public I{view.Name}QueryContext Get() {{ return new {view.Name}QueryContext(this); }}
	    }}");

            return E();
        }

    }
}
