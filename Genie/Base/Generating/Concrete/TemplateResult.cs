using Genie.Core.Templates.Abstract;

namespace Genie.Core.Base.Generating.Concrete
{
    internal class TemplateFile
    {
        internal string Path { get; set; }
        internal ITemplate Template { get; set; }

        internal string Generate()
        {
            return Template.Generate();
        }
    }
}