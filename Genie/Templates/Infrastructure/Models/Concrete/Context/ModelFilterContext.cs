using System.Collections.Generic;
using System.Linq;
using System.Text;
using Genie.Core.Base.Generating.Concrete;
using Genie.Core.Models.Abstract;

namespace Genie.Core.Templates.Infrastructure.Models.Concrete.Context
{
    internal class ModelFilterContextTemplate : GenieTemplate
    {
        private readonly List<ISimpleAttribute> _attributes;
        private readonly string _name;

        public ModelFilterContextTemplate(string path, string name, List<ISimpleAttribute> attributes) : base(path)
        {
            _attributes = attributes;
            _name = name;
        }

        public override string Generate()
        {
            var props = new StringBuilder();
            var fields = new StringBuilder();
            foreach (var atd in _attributes)
            {
                props.AppendLine();
                if (!string.IsNullOrWhiteSpace(atd.Comment))
                    props.AppendLine($@"		/// <summary>
		/// {atd.Comment}
		/// </summary>");

                if (atd.DataType == "string")
                {
                    fields.AppendLine(
                        $@"		private IStringFilter<I{_name}FilterContext, I{_name}QueryContext> {atd.FieldName};");
                    props.AppendLine(
                        $@"		public IStringFilter<I{_name}FilterContext, I{_name}QueryContext> {
                                atd.Name
                            } {{ get {{ return {atd.FieldName} ?? ( {atd.FieldName} = new StringFilter<I{
                                _name
                            }FilterContext, I{_name}QueryContext>(""{atd.Name}"", this, _queryContext)); }} }}");
                }
                else if (atd.DataType == "int" || atd.DataType == "int?" || atd.DataType == "double" ||
                         atd.DataType == "double?" || atd.DataType == "decimal" || atd.DataType == "decimal?" ||
                         atd.DataType == "long" || atd.DataType == "long?")
                {
                    fields.AppendLine(
                        $@"		private INumberFilter<I{_name}FilterContext, I{_name}QueryContext> {atd.FieldName};");
                    props.AppendLine(
                        $@"		public INumberFilter<I{_name}FilterContext, I{_name}QueryContext> {
                                atd.Name
                            } {{ get {{ return {atd.FieldName} ?? ( {atd.FieldName} = new NumberFilter<I{
                                _name
                            }FilterContext, I{_name}QueryContext>(""{atd.Name}"", this, _queryContext)); }} }}");
                }
                else if (atd.DataType == "DateTime" || atd.DataType == "DateTime?")
                {
                    fields.AppendLine(
                        $@"    	private IDateFilter<I{_name}FilterContext, I{_name}QueryContext> {atd.FieldName};");
                    props.AppendLine(
                        $@"		public IDateFilter<I{_name}FilterContext, I{_name}QueryContext> {
                                atd.Name
                            } {{ get {{ return {atd.FieldName} ?? ( {atd.FieldName} = new DateFilter<I{
                                _name
                            }FilterContext, I{_name}QueryContext>(""{atd.Name}"", this, _queryContext)); }} }}");
                }
                else if (atd.DataType == "bool" || atd.DataType == "bool?")
                {
                    fields.AppendLine(
                        $@"    	private IBoolFilter<I{_name}FilterContext, I{_name}QueryContext> {atd.FieldName};");
                    props.AppendLine(
                        $@"		public IBoolFilter<I{_name}FilterContext, I{_name}QueryContext> {
                                atd.Name
                            } {{ get {{ return {atd.FieldName} ?? ( {atd.FieldName} = new BoolFilter<I{
                                _name
                            }FilterContext, I{_name}QueryContext>(""{atd.Name}"", this, _queryContext)); }} }}");
                }
            }

            var startName = _attributes.Any(a => a.Name == "Start") ? "StartScope" : "Start";
            var endName = _attributes.Any(a => a.Name == "End") ? "EndScope" : "End";


            L($@"
using {GenerationContext.BaseNamespace}.Infrastructure.Models.Abstract;
using {GenerationContext.BaseNamespace}.Infrastructure.Filters.Abstract;
using {GenerationContext.BaseNamespace}.Infrastructure.Filters.Concrete;
using {GenerationContext.BaseNamespace}.Infrastructure.Models.Abstract.Context;

namespace {GenerationContext.BaseNamespace}.Infrastructure.Models.Concrete.Context
{{
    internal class {_name}FilterContext : BaseFilterContext, I{_name}FilterContext 
    {{
		private readonly I{_name}QueryContext  _queryContext;
		internal {_name}FilterContext({_name}QueryContext context) {{ _queryContext = context; }}
{fields}

{props}

        public I{_name}FilterContext {startName}
        {{
            get
            {{
                StartScope();
                return this;
            }}
        }}
        public I{_name}FilterContext {endName}
        {{
            get
            {{
                EndScope();
                return this;
            }}
        }}
	}}
}}
");

            return E();
        }
    }
}
