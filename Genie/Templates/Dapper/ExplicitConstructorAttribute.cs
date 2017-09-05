using Genie.Base.Generating.Concrete;
using Genie.Templates;

namespace Genie.Templates.Dapper
{
    internal class ExplicitConstructorAttributeTemplate: GenieTemplate
    {
        public ExplicitConstructorAttributeTemplate(string path) : base(path){}

public override string Generate()
{
L($@"

using System;

namespace {GenerationContext.BaseNamespace}.Dapper 
{{
    /// <summary>
    /// Tell Dapper to use an explicit constructor, passing nulls or 0s for all parameters
    /// </summary>
    [AttributeUsage(AttributeTargets.Constructor, AllowMultiple = false)]
    public sealed class ExplicitConstructorAttribute : Attribute
    {{
    }}
}}");

return E();
    
}
    }
}