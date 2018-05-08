#region Usings

using System.Collections.Generic;
using System.Linq;
using System.Text;
using Genie.Core.Base.Generating;
using Genie.Core.Models.Abstract;

#endregion

namespace Genie.Core.Templates.Infrastructure.Models.Abstract.Context
{
    public class IModelFilterContextTemplate : GenieTemplate
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

            const string template =
@"
                /// <summary>
                /// Helps to build filters for queries on the data source {{name}}
                /// </summary>
  	            public interface I{{name}}FilterContext : IFilterContext
	            {
            {% for atd in attributes %}
		            /// <summary>{{atd.commentStr}}
		            ///  Apply filters on {{atd.Name}} attribute . these filters will be preserved within entire query context
		            /// </summary>
                {% if atd.DataType == 'string' %}
                    IStringFilter<I{{name}}FilterContext,I{{name}}QueryContext> {{atd.Name}} { get; }
                {% else if atd.DataType contains 'int' or atd.DataType contains 'double' or atd.DataType contains 'decimal' or atd.DataType contains 'long' %}
                    INumberFilter<I{{name}}FilterContext,I{{name}}QueryContext> {{atd.Name}} { get; }
                {% else if atd.DataType contains 'DateTime'%}
                    IDateFilter<I{{name}}FilterContext,I{{name}}QueryContext> {{atd.Name}} { get; }
                {% else if atd.DataType contains 'bool'%}
                    IBoolFilter<I{{name}}FilterContext,I{{name}}QueryContext> {{atd.Name}} { get; }
                {% endif %}
            {% endfor %}

                    /// <summary>
                    /// Start Parenthesizes
                    /// </summary>
                    I{{name}}FilterContext {{startName}} { get; }

                    /// <summary>
                    /// Start Parenthesizes
                    /// </summary>
                    I{{name}}FilterContext {{endName}} { get; }
                }
";
            return Process(nameof(IModelFilterContextTemplate), template, new
            {
                name = _name,
                attributes = _attributes,
                startName = _attributes.Any(a => a.Name == "Start") ? "StartScope" : "Start",
                endName = _attributes.Any(a => a.Name == "End") ? "EndScope" : "End"
            });
        }
    }
}