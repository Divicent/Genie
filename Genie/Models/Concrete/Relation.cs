#region Usings

using System.Collections.Generic;
using Genie.Core.Models.Abstract;

#endregion

namespace Genie.Core.Models.Concrete
{
    internal class Relation : IRelation
    {
        public string Name { get; set; }
        public List<IAttribute> Attributes { get; set; }
        public List<IForeignKeyAttribute> ForeignKeyAttributes { get; set; }
        public List<IReferenceList> ReferenceLists { get; set; }
        public string FieldName { get; set; }
        public string Comment { get; set; }
    }
}