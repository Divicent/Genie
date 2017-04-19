
using System.Collections.Generic;
using Genie.Models.Abstract;

namespace Genie.Models
{
    internal class Relation : IRelation
    {
        public string Name { get; set; }
        public List<IAttribute> Attributes { get; set; }
        public List<IForeignKeyAttribute> ForeignKeyAttributes { get; set; }
        public List<IReferenceList> ReferenceLists { get; set; }
        public string FieldName { get; set; }
    }
}
