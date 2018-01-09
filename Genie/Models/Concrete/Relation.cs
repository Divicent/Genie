#region Usings

using System.Collections.Generic;
using Genie.Core.Models.Abstract;
using Genie.Core.Models.Concrete.SchemaCaching;

#endregion

namespace Genie.Core.Models.Concrete
{
    public class Relation : Model, IRelation
    {
        public string Name { get; set; }
        public List<IAttribute> Attributes { get; set; }
        public List<IForeignKeyAttribute> ForeignKeyAttributes { get; set; }
        public List<IReferenceList> ReferenceLists { get; set; }
        public string FieldName { get; set; }
        public string Comment { get; set; }

        public override IEnumerable<ISimpleAttribute> GetAttributes()
        {
            return Attributes;
        }

        public override string GetName()
        {
            return Name;
        }

        public override IEnumerable<IForeignKeyAttribute> GetForeignKeyAttributes()
        {
            return ForeignKeyAttributes;
        }

        public override IEnumerable<IReferenceList> GetReferenceLists()
        {
            return ReferenceLists;
        }
    }
}