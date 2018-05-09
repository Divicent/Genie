using Genie.Core.Models.Abstract;

namespace Genie.Core.Templates.Infrastructure.Models.Concrete.Context
{
    public class ModelColumnSelectorTemplate: GenieTemplate
    {
        private readonly IModel _model;
        
        public ModelColumnSelectorTemplate(string path, IModel model) : base(path)
        {
            _model = model;
        }

        public override string Generate()
        {
            const string template =
@"
                public class {{name}}ColumnSelector: I{{name}}ColumnSelector
                {
{% for attribute in attributes%}
                    public IColumn<{{attribute.DataType}}> {{attribute.Name}} { get { return new Column<{{attribute.DataType}}>(""{{attribute.Name}}""); } }
{% endfor %}
                }
";
            return Process(nameof(ModelColumnSelectorTemplate), template, new
            {
                name = _model.GetName(),
                attributes = _model.GetAttributes()
            });
        }
    }
}