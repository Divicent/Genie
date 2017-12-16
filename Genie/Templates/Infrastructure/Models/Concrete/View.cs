#region Usings

using System.Text;
using Genie.Core.Base.Generating;
using Genie.Core.Models.Abstract;
using Genie.Core.Base.Configuration.Abstract;
using Genie.Core.Tools;

#endregion

namespace Genie.Core.Templates.Infrastructure.Models.Concrete
{
    internal class ViewTemplate : GenieTemplate
    {
        private readonly IView _view;
        private readonly IConfiguration _configuration;

        public ViewTemplate(string path, IView view, IConfiguration configuration) : base(path)
        {
            _view = view;
            _configuration = configuration;
        }

        public override string Generate()
        {
            var entity = _view;
            var name = _view.Name;

            var quote = FormatHelper.GetDBMSSpecificQuoter(_configuration);

            var attributes = new StringBuilder();

            foreach (var atd in entity.Attributes)
            {
                if (!string.IsNullOrWhiteSpace(atd.Comment))
                {
                    attributes.AppendLine($@"		/// <summary>
		/// {atd.Comment}
		/// </summary>");
                }

                attributes.AppendLine($@"		public {atd.DataType} {atd.Name} {{ get; set; }} ");
            }

            L($@"

using System;
using {GenerationContext.BaseNamespace}.Dapper;

namespace {GenerationContext.BaseNamespace}.Infrastructure.Models.Concrete
{{
    [Table(""{quote(_configuration.Schema)}.{quote(name)}"")]
    public class {name} 
    {{

{attributes}

    }}
}}
");

            return E();
        }
    }
}