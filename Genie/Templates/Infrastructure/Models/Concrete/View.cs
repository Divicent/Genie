#region Usings

using System.Text;
using Genie.Core.Base.Configuration.Abstract;
using Genie.Core.Base.Generating;
using Genie.Core.Models.Abstract;
using Genie.Core.Tools;

#endregion

namespace Genie.Core.Templates.Infrastructure.Models.Concrete
{
    internal class ViewTemplate : GenieTemplate
    {
        private readonly IConfiguration _configuration;
        private readonly IView _view;

        public ViewTemplate(string path, IView view, IConfiguration configuration) : base(path)
        {
            _view = view;
            _configuration = configuration;
        }

        public override string Generate()
        {
            var entity = _view;
            var name = _view.Name;

            var quote = FormatHelper.GetDbmsSpecificQuoter(_configuration);

            var attributes = new StringBuilder();

            foreach (var atd in entity.Attributes)
            {
                if (!string.IsNullOrWhiteSpace(atd.Comment))
                    attributes.AppendLine($@"		/// <summary>
		/// {atd.Comment}
		/// </summary>");

                attributes.AppendLine($@"		public {atd.DataType} {atd.Name} {{ get; set; }} ");
            }

            var abstractModelsNamespace = _configuration.AbstractModelsEnabled
                ? $"using {_configuration.AbstractModelsNamespace};\n"
                : "";
            var absImplement = _configuration.AbstractModelsEnabled ? $": I{name}" : "";
            L($@"

using System;
using {GenerationContext.BaseNamespace}.Dapper;
{abstractModelsNamespace}
namespace {GenerationContext.BaseNamespace}.Infrastructure.Models.Concrete
{{
    [Table(""{quote(_configuration.Schema)}.{quote(name)}"")]
    public class {name} {absImplement}
    {{

{attributes}

    }}
}}
");

            return E();
        }
    }
}