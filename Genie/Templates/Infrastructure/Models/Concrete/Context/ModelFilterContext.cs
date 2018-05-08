#region Usings

using System.Collections.Generic;
using System.Linq;
using Genie.Core.Models.Abstract;

#endregion

namespace Genie.Core.Templates.Infrastructure.Models.Concrete.Context
{
    public class ModelFilterContextTemplate : GenieTemplate
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
            const string template =
@"
                internal class {{name}}FilterContext : BaseFilterContext, I{{name}}FilterContext 
                {
		            private readonly I{{name}}QueryContext  _queryContext;
		            internal {{name}}FilterContext({{name}}QueryContext context) { _queryContext = context; }

{% for atd in attributes%}
{% if atd.DataType == 'string' %}
                    private IStringFilter<I{{name}}FilterContext, I{{name}}QueryContext> {{atd.FieldName}};
{% else if atd.DataType contains 'int' or atd.DataType contains 'double' or atd.DataType contains 'decimal' or atd.DataType contains 'long' %}
                    private INumberFilter<I{{name}}FilterContext, I{{name}}QueryContext> {{atd.FieldName}};
{% else if atd.DataType contains 'DateTime'%}
    	            private IDateFilter<I{{name}}FilterContext, I{{name}}QueryContext> {{atd.FieldName}};
{% else if atd.DataType contains 'bool'%}
    	            private IBoolFilter<I{{name}}FilterContext, I{{name}}QueryContext> {{atd.FieldName}};
{% endif %}
{% endfor %}

{% for atd in attributes%}
{% if atd.DataType == 'string' %}
                    public IStringFilter<I{{name}}FilterContext, I{{name}}QueryContext> {{atd.Name}} { get { return {{atd.FieldName}} ?? ( {{atd.FieldName}} = new StringFilter<I{{name}}FilterContext, I{{name}}QueryContext>(""{{atd.Name}}"", this, _queryContext)); } }
{% else if atd.DataType contains 'int' or atd.DataType contains 'double' or atd.DataType contains 'decimal' or atd.DataType contains 'long' %}
		            public INumberFilter<I{{name}}FilterContext, I{{name}}QueryContext> {{atd.Name}} { get { return {{atd.FieldName}} ?? ( {{atd.FieldName}} = new NumberFilter<I{{name}}FilterContext, I{{name}}QueryContext>(""{{atd.Name}}"", this, _queryContext)); } }
{% else if atd.DataType contains 'DateTime'%}
		            public IDateFilter<I{{name}}FilterContext, I{{name}}QueryContext> {{atd.Name}} { get { return {{atd.FieldName}} ?? ( {{atd.FieldName}} = new DateFilter<I{{name}}FilterContext, I{{name}}QueryContext>(""{{atd.Name}}"", this, _queryContext)); } }
{% else if atd.DataType contains 'bool'%}
		            public IBoolFilter<I{{name}}FilterContext, I{{name}}QueryContext> {{atd.Name}} { get { return {{atd.FieldName}} ?? ( {{atd.FieldName}} = new BoolFilter<I{{name}}FilterContext, I{{name}}QueryContext>(""{atd.Name}"", this, _queryContext)); } }
{% endif %}
{% endfor %}

                    public I{{name}}FilterContext {{startName}}
                    {
                        get
                        {
                            StartScope();
                            return this;
                        }
                    }
                    public I{{name}}FilterContext {{endName}}
                    {
                        get
                        {
                            EndScope();
                            return this;
                        }
                    }
	            }
";
            return Process(nameof(ModelFilterContextTemplate), template, new
            {
                startName = _attributes.Any(a => a.Name == "Start") ? "StartScope" : "Start",
                endName = _attributes.Any(a => a.Name == "End") ? "EndScope" : "End",
                name = _name,
                attributes = _attributes
            });
        }
    }
}