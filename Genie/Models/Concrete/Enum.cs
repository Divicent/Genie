using System.Collections.Generic;
using Genie.Models.Abstract;

namespace Genie.Models.Concrete
{
    internal class Enum: IEnum
    {
        public string Type { get; set; }
        public string Name { get; set; }
        public List<IEnumValue> Values { get; set; }
    }
}
