using System.Collections.Generic;
using Genie.Core.Models.Abstract;

namespace Genie.Core.Models.Concrete
{
    internal class Enum : IEnum
    {
        public string Type { get; set; }
        public string Name { get; set; }
        public List<IEnumValue> Values { get; set; }
    }
}