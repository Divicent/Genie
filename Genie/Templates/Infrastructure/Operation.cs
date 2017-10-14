#region Usings

using Genie.Core.Base.Generating.Concrete;

#endregion

namespace Genie.Core.Templates.Infrastructure
{
    internal class OperationTemplate : GenieTemplate
    {
        public OperationTemplate(string path) : base(path)
        {
        }

        public override string Generate()
        {
            L($@"

using {GenerationContext.BaseNamespace}.Infrastructure.Interfaces;
using {GenerationContext.BaseNamespace}.Infrastructure.Models.Concrete;

namespace {GenerationContext.BaseNamespace}.Infrastructure
{{
    internal class Operation : IOperation
    {{
        internal Operation(OperationType type, BaseModel model)
        {{
            Type = type;
            Object = model;
        }}

        public OperationType Type {{ get; }}
        public BaseModel Object {{ get; }}
    }}
}}
");

            return E();
        }
    }
}