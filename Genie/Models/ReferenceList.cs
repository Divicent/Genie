using Genie.Models.Abstract;

namespace Genie.Models
{
    internal class ReferenceList: IReferenceList
    {
        public string ReferncedRelationName { get; set; }
        public string ReferencedPropertyName { get; set; }
        public string ReferencedPropertyOnThisRelation { get; set; }
    }
}
