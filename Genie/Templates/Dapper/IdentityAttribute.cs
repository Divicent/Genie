using Genie.Base.Generating.Concrete;
using Genie.Templates;

namespace Genie.Templates.Dapper
{
    internal class IdentityAttributeTemplate: GenieTemplate
    {
        public IdentityAttributeTemplate(string path) : base(path){}

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