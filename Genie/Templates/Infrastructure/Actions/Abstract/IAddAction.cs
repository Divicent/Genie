using Genie.Base.Generating.Concrete;
using Genie.Templates;

namespace Genie.Templates.Infrastructure.Actions.Abstract
{
    internal class IAddActionTemplate: GenieTemplate
    {
        public IAddActionTemplate(string path) : base(path){}

public override string Generate()
{
L($@"


namespace {GenerationContext.BaseNamespace}.Infrastructure.Actions.Abstract
{{
    internal interface IAddAction
    {{
        void Run();
    }}
}}
");

return E();
    
}
    }
}