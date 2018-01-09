#region Usings

using System.Collections.Generic;
using System.Linq;
using System.Text;
using Genie.Core.Base.Generating;
using Genie.Core.Models.Abstract;

#endregion

namespace Genie.Core.Templates.Infrastructure.Models.Abstract.Context
{
    internal class IModelFilterContextTemplate : GenieTemplate
    {
        private readonly List<ISimpleAttribute> _attributes;
        private readonly string _name;

        public IModelFilterContextTemplate(string path, string name, List<ISimpleAttribute> attributes) : base(path)
        {
            _name = name;
            _attributes = attributes;
        }

        public override string Generate()
        {
            var props = new StringBuilder();
            foreach (var atd in _attributes)
            {
                props.AppendLine();
                var atdComment = !string.IsNullOrWhiteSpace(atd.Comment);
                var commentStr = atdComment
                    ? $@"
        /// <para>{atd.Comment}</para>"
                    : "";
                props.AppendLine($@"		/// <summary>{commentStr}
		///  Apply filters on {atd.Name} attribute . these filters will be preserved within entire query context
		/// </summary>");

                if (atd.DataType == "string")
                    props.AppendLine(
                        $@"	    IStringFilter<I{_name}FilterContext,I{_name}QueryContext> {atd.Name} {{ get; }}
");
                else if (atd.DataType == "int" || atd.DataType == "int?" || atd.DataType == "double" ||
                         atd.DataType == "double?" || atd.DataType == "decimal" || atd.DataType == "decimal?" ||
                         atd.DataType == "long" || atd.DataType == "long?")
                    props.AppendLine(
                        $@"		INumberFilter<I{_name}FilterContext,I{_name}QueryContext> {atd.Name} {{ get; }}
");
                else if (atd.DataType == "DateTime" || atd.DataType == "DateTime?")
                    props.AppendLine(
                        $@"	    IDateFilter<I{_name}FilterContext,I{_name}QueryContext> {atd.Name} {{ get; }}
");
                else if (atd.DataType == "bool" || atd.DataType == "bool?")
                    props.AppendLine(
                        $@"	    IBoolFilter<I{_name}FilterContext,I{_name}QueryContext> {atd.Name} {{ get; }}
");
            }

            var startName = _attributes.Any(a => a.Name == "Start") ? "StartScope" : "Start";
            var endName = _attributes.Any(a => a.Name == "End") ? "EndScope" : "End";

            L($@"
using {GenerationContext.BaseNamespace}.Infrastructure.Filters.Abstract;

namespace {GenerationContext.BaseNamespace}.Infrastructure.Models.Abstract.Context
{{

  /// <summary>
  /// Helps to build filters for queries on the data source {_name}
  /// </summary>
	public interface I{_name}FilterContext : IFilterContext
	{{

{props}

        /// <summary>
        /// Start Parenthesizes
        /// </summary>
        I{_name}FilterContext {startName} {{ get; }}

        /// <summary>
        /// Start Parenthesizes
        /// </summary>
        I{_name}FilterContext {endName} {{ get; }}

    }}
}}");

            return E();
        }
    }
}