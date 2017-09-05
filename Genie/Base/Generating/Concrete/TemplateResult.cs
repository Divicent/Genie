using Genie.Templates.Abstract;

namespace Genie.Base.Generating.Concrete 
{
    internal  class TemplateFile 
    {
        internal string Path { get; set; }
        internal ITemplate Template {get;set; } 
        internal string Generate() 
        {
            return Template.Generate();
        }
    }
}