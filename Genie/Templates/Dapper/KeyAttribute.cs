#region Usings

using Genie.Core.Base.Generating.Concrete;

#endregion

namespace Genie.Core.Templates.Dapper
{
    internal class KeyAttributeTemplate : GenieTemplate
    {
        public KeyAttributeTemplate(string path) : base(path)
        {
        }

        public override string Generate()
        {
            L($@"

using System;

namespace {GenerationContext.BaseNamespace}.Dapper 
{{
	[AttributeUsage(AttributeTargets.Property)]
    public class KeyAttribute : Attribute
    {{
    }}
}}");

            return E();
        }
    }
}