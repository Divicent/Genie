#region Usings

using System.Collections.Generic;
using Genie.Core.Models.Abstract;

#endregion

namespace Genie.Core.Models.Concrete
{
    internal class View : IView
    {
        public string Comment { get; set; }
        public string FieldName { get; set; }
        public string Name { get; set; }
        public List<ISimpleAttribute> Attributes { get; set; }
    }
}