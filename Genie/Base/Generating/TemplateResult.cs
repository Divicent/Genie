#region Usings

using Genie.Core.Templates.Abstract;

#endregion

namespace Genie.Core.Base.Generating
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