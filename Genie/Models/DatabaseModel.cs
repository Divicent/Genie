using System.Collections.Generic;

namespace Genie.Models
{
    internal class DatabaseModel
    {
        public string BaseNamespace { get; set; }
        public string ConnectionName { get; set; }
        internal List<Relation> Relations { get; set; }
    }
}
