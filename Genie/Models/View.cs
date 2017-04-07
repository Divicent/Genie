using System.Collections.Generic;

namespace Genie.Models
{
    public class View
    {
        public string Name { get; set; }
        public string RelationName { get; set; }
        internal List<Attribute> Attributes { get; set; }
    }
}
