#region Usings

using Genie.Core.Base.Generating.Concrete;

#endregion

namespace Genie.Core.Templates.Infrastructure.Filters.Abstract
{
    internal class IOrderElementTemplate : GenieTemplate
    {
        public IOrderElementTemplate(string path) : base(path)
        {
        }

        public override string Generate()
        {
            L($@"

namespace {GenerationContext.BaseNamespace}.Infrastructure.Filters.Abstract
{{
	public interface IOrderElement<out T, out TQ> where T : IOrderContext
	{{
		IOrderJoin<T, TQ> Ascending();
		IOrderJoin<T, TQ> Descending();
	}}
}}

");

            return E();
        }
    }
}