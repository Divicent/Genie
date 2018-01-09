using System.Collections.Generic;
using Genie.Core.Models.Abstract;

namespace Genie.Core.Models.Concrete.SchemaCaching
{
    public abstract class Model: IModel
    {
        public abstract IEnumerable<ISimpleAttribute> GetAttributes();

        public abstract string GetName();

        public abstract IEnumerable<IForeignKeyAttribute> GetForeignKeyAttributes();

        public abstract IEnumerable<IReferenceList> GetReferenceLists();

        public string GetHash()
        {
            throw new System.NotImplementedException();
        }
    }
}