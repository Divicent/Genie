using System.Collections.Generic;
using Genie.Base.Reading.Abstract;
using Genie.Models.Abstract;

namespace Genie.Base.Reading.Concrete
{
    internal class DatabaseSchema : IDatabaseSchema
    {
        public string BaseNamespace { get; set; }
        public List<IRelation> Relations { get; set; }
        public List<IView> Views { get; set; }
        public List<IStoredProcedure> Procedures { get; set; }
        public List<IEnum> Enums { get; set; }
    }
}
