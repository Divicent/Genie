#region Usings

using Genie.Core.Base.Configuration.Abstract;
using Genie.Core.Models.Abstract;

#endregion


namespace Genie.Core.Templates.Infrastructure.Models.Abstract
{
    public class IModelTemplate : GenieTemplate
    {
        private readonly IConfiguration _configuration;
        private readonly IModel _model;

        public IModelTemplate(string path, IModel model, IConfiguration configuration, bool external) : base(path)
        {
            _configuration = configuration;
            _model = model;
            External = external;
        }

        public override string Generate()
        {
            const string template =
@"
using System;

namespace {{configuration.AbstractModelsNamespace}}
{
    public interface I{{name}}
    {
{% for atd in attributes %}
      /// <summary>
      /// {{atd.Comment}}
      /// </summary>
      {{atd.DataType}} {{atd.Name}} { get; set; }
{% endfor %}
    }
}
";
            return Process(nameof(IModelTemplate), template, new
            {
                configuration = _configuration,
                name = _model.GetName(),
                attributes = _model.GetAttributes()
            });
        }
    }
}