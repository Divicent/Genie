using System.Text;
using Genie.Core.Base.Generating.Concrete;
using Genie.Core.Models.Abstract;

namespace Genie.Core.Templates.Infrastructure.Models.Concrete
{
    internal class ViewTemplate : GenieTemplate
    {
        private readonly IView _view;

        public ViewTemplate(string path, IView view) : base(path)
        {
            _view = view;
        }

        public override string Generate()
        {
            var entity = _view;
            var name = _view.Name;

            var attributes = new StringBuilder();

            foreach (var atd in entity.Attributes)
            {
                if (!string.IsNullOrWhiteSpace(atd.Comment))
                    attributes.AppendLine($@"		/// <summary>
		/// {atd.Comment}
		/// </summary>");
                attributes.AppendLine($@"		public {atd.DataType} {atd.Name} {{ get; set; }} ");
            }

            L($@"

using System;
using {GenerationContext.BaseNamespace}.Dapper;

namespace {GenerationContext.BaseNamespace}.Infrastructure.Models.Concrete
{{
    [Table(""[dbo].[{name}]"")]
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