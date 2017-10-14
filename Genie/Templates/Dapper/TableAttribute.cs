#region Usings

using Genie.Core.Base.Generating.Concrete;

#endregion

namespace Genie.Core.Templates.Dapper
{
    internal class TableAttributeTemplate : GenieTemplate
    {
        public TableAttributeTemplate(string path) : base(path)
        {
        }

        public override string Generate()
        {
            L($@"

using System;

namespace {GenerationContext.BaseNamespace}.Dapper
{{
	[AttributeUsage(AttributeTargets.Class)]
    public class TableAttribute : Attribute
    {{
        public TableAttribute(string tableName)
        {{
            Name = tableName;
        }}
        public string Name {{ get; private set; }}
    }}
}} ");

            return E();
        }
    }
}