using Genie.Core.Models.Abstract;

namespace Genie.Core.Templates.Infrastructure.Models.Abstract.Context
{
    public class IModelColumnSelectorTemplate : GenieTemplate
    {
        private readonly IModel _model;

        public IModelColumnSelectorTemplate(string path, IModel model) : base(path)
        {
            _model = model;
        }

       

        public override string Generate()
        {
            const string template =
@"
                public interface I{{name}}ColumnSelector
                {
{% for attribute in attributes %}
                    /// <summary>
                    /// Select {{attribute.Name}} Column
                    /// </summary>
                    IColumn<{{attribute.DataType}}> {{attribute.Name}} { get; }
{% endfor %}
                }
";
            return Process(nameof(IModelColumnSelectorTemplate), template, new
            {
                name = _model.GetName(),
                attributes = _model.GetAttributes()
            });
        }
    }
}