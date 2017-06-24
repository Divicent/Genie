using System.Collections.Generic;
using Genie.Models.Abstract;

namespace Genie.Models.Concrete
{
    internal class StoredProcedure : IStoredProcedure
    {
        public string Name { get; set; }
        public string PassString { get; set; }
        public string ParamString { get; set; }
        public List<ProcedureParameter> Parameters { get; set; }
    }
}
