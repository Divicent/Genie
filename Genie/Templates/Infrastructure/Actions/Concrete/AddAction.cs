using Genie.Base.Generating.Concrete;
using Genie.Templates;

namespace Genie.Templates.Infrastructure.Actions.Concrete
{
    internal class AddActionTemplate: GenieTemplate
    {
        public AddActionTemplate(string path) : base(path){}

public override string Generate()
{
L($@"

using System;
using {GenerationContext.BaseNamespace}.Infrastructure.Actions.Abstract;

namespace {GenerationContext.BaseNamespace}.Infrastructure.Actions.Concrete
{{
    internal class AddAction: IAddAction
    {{
        private readonly Action<object> _action;
        private readonly object _parameter;

        internal AddAction(Action<object> action, object parameter )
        {{
            _action = action;
            _parameter = parameter;
        }}

        public void Run()
        {{
            if(_action == null || _parameter ==null)
            {{ return; }}
            _action(_parameter);
        }}
    }}
}}");

return E();
    
}
    }
}