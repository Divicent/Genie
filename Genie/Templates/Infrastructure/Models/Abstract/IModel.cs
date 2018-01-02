#region Usings

using System.Linq;
using System.Text;
using Genie.Core.Base.Generating;
using Genie.Core.Extensions;
using Genie.Core.Models.Abstract;
using Genie.Core.Tools;
using Genie.Core.Base.Configuration.Abstract;
using Genie.Core.Templates;

#endregion


namespace Genie.Core.Templates.Infrastructure.Models.Abstract
{
  internal class IModelTemplate : GenieTemplate
  {

    private readonly IModel _model;
    private readonly IConfiguration _configuration;

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
        {
          attrProperties.AppendLine($@"      /// <summary>
      /// {atd.Comment}
      /// </summary>");
        }
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
