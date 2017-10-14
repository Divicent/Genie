#region Usings



#endregion

using Genie.Core.Base.Generating;

namespace Genie.Core.Templates.Infrastructure.Interfaces
{
    internal class IOperationTemplate : GenieTemplate
    {
        public IOperationTemplate(string path) : base(path)
        {
        }

        public override string Generate()
        {
            L($@"

using {GenerationContext.BaseNamespace}.Infrastructure.Models.Concrete;

namespace {GenerationContext.BaseNamespace}.Infrastructure.Interfaces
{{
    /// <summary>
    /// Type of an operation
    /// </summary>
    public enum OperationType
    {{
        Add = 1,
        Remove = 2,
        Update = 3
    }}

    /// <summary>
    /// A database operation
    /// </summary>
	public interface IOperation
    {{
        OperationType Type {{ get; }}
        BaseModel Object {{ get; }}
    }}
}}
");

            return E();
        }
    }
}