#region Usings

using System.Text;
using Genie.Core.Base.Configuration.Abstract;
using Genie.Core.Models.Abstract;
using Genie.Core.Tools;

#endregion


namespace Genie.Core.Templates.Infrastructure.Models.Abstract
{
    public class IModelTemplate : GenieTemplate
    {
        private readonly IConfiguration _configuration;

        private readonly IModel _model;

        internal IModelTemplate(string path, IModel model, IConfiguration configuration) : base(path)
        {
            _configuration = configuration;
            _model = model;
        }

        public override string Generate()
        {
            var entity = _model;
            var name = _model.GetName();
            var quote = FormatHelper.GetDbmsSpecificQuoter(_configuration);

            var attrProperties = new StringBuilder();

            foreach (var atd in entity.GetAttributes())
            {
                attrProperties.AppendLine();
                if (!string.IsNullOrWhiteSpace(atd.Comment))
                    attrProperties.AppendLine($@"      /// <summary>
      /// {atd.Comment}
      /// </summary>");
                attrProperties.AppendLine(
                    $@"      {atd.DataType} {atd.Name} {{ get; set; }}");
            }

            L($@"
using System;

namespace {_configuration.AbstractModelsNamespace}
{{
    public interface I{name}
    {{
{attrProperties}
    }}
}}
");

            return E();
        }
    }
}