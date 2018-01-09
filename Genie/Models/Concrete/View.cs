#region Usings

using System.Collections.Generic;
using Genie.Core.Models.Abstract;
using Genie.Core.Models.Concrete.SchemaCaching;

#endregion

namespace Genie.Core.Models.Concrete
{
    public class View : Model, IView
    {
        public string Comment { get; set; }
        public string FieldName { get; set; }
        public string Name { get; set; }
        public List<ISimpleAttribute> Attributes { get; set; }

        public override IEnumerable<ISimpleAttribute> GetAttributes()
        {
            return Attributes;
        }

        public override IEnumerable<IForeignKeyAttribute> GetForeignKeyAttributes()
        {
            return new List<IForeignKeyAttribute>();
        }

        public override string GetName()
        {
            return Name;
        }

        public override IEnumerable<IReferenceList> GetReferenceLists()
        {
            return new List<IReferenceList>();
        }
    }
}