#region Usings

using Genie.Core.Models.Abstract;

#endregion

namespace Genie.Core.Models.Concrete
{
    internal class ContentFile : IContentFile
    {
        public string Content { get; set; }
        public string Path { get; set; }
    }
}