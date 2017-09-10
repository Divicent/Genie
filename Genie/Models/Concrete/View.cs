using System.Collections.Generic;
using Genie.Models.Abstract;

namespace Genie.Models.Concrete
{
    internal class View : IView
    {
        public string Comment { get; set; }
        public string FieldName { get; set; }
        public string Name { get; set; }
        public List<ISimpleAttribute> Attributes { get; set; }
    }
}
