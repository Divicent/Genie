#region Usings

using System.Collections.Generic;
using Genie.Core.Models.Abstract;

#endregion

namespace Genie.Core.Templates.Infrastructure.Models.Abstract.Context
{
    public class IModelOrderContextTemplate : GenieTemplate
    {
        private readonly List<ISimpleAttribute> _attributes;
        private readonly string _name;

        public IModelOrderContextTemplate(string path, string name, List<ISimpleAttribute> attributes) : base(path)
        {
            _name = name;
            _attributes = attributes;
        }

        public override string Generate()
        {

            const string template =
@"
                /// <summary>
                /// Helps to build order for queries on the data source {{name}}
                /// </summary>
                public interface I{{name}}OrderContext: IOrderContext
                {
{% for atd in attributes %}
		            /// <summary>{{atd.commentStr}}
		            ///  Apply order by on {{atd.Name}} attribute . this order by expression will be preserved within entire query context
		            /// </summary>
		            IOrderElement<I{{name}}OrderContext,I{{name}}QueryContext> {{atd.Name}} { get; }
{% endfor %}
                }
";
            return Process(nameof(IModelOrderContextTemplate), template, new
            {
                name = _name,
                attributes = _attributes
            });
        }
    }
}