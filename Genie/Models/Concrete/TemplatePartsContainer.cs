using Genie.Core.Models.Abstract;

namespace Genie.Core.Models.Concrete
{
    public class TemplatePartsContainer : ITemplatePartsContainer
    {
        public string SqlClientNamespace { get; set; }
        public string SqlConnectionClassName { get; set; }
        public string StoredProcedureCallString { get; set; }
    }
}