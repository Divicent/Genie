#region Usings

using Genie.Core.Base.Generating;

#endregion

namespace Genie.Core.Templates.Dapper
{
    public class IdentityAttributeTemplate : GenieTemplate
    {
        public IdentityAttributeTemplate(string path) : base(path)
        {
        }

        public override string Generate()
        {
            L($@"

using System;

namespace {GenerationContext.BaseNamespace}.Dapper 
{{
	  [AttributeUsage(AttributeTargets.Property)]
    public class IdentityAttribute : Attribute
    {{
    }}
}}");

            return E();
        }
    }
}