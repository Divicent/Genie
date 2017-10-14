#region Usings



#endregion

using Genie.Core.Base.Generating;

namespace Genie.Core.Templates.Dapper
{
    internal class WriteAttributeTemplate : GenieTemplate
    {
        public WriteAttributeTemplate(string path) : base(path)
        {
        }

        public override string Generate()
        {
            L($@"

using System;

namespace {GenerationContext.BaseNamespace}.Dapper
{{
	[AttributeUsage(AttributeTargets.Property)]
    public class WriteAttribute : Attribute
    {{
        public WriteAttribute(bool write)
        {{
            Write = write;
        }}
        public bool Write {{ get; private set; }}
    }}
}} ");

            return E();
        }
    }
}