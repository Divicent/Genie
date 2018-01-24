#region Usings

using System.Collections.Generic;
using Genie.Core.Models.Abstract;

#endregion

namespace Genie.Core.Models.Concrete
{
    public class StoredProcedure : IStoredProcedure
    {
        public string Name { get; set; }
        public string PassString { get; set; }
        public string ParamString { get; set; }
        public List<ProcedureParameter> Parameters { get; set; }
    }
}