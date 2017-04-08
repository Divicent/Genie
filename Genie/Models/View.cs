using System.Collections.Generic;
using Genie.Models.Abstract;

namespace Genie.Models
{
    internal class View: IView
    {
        public string Name { get; set; }
        public List<ISimpleAttribute> Attributes { get; set; }
    }
}
