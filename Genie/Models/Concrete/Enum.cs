using System.Collections.Generic;
using Genie.Models.Abstract;

namespace Genie.Models.Concrete
{
    internal class Enum: IEnum
    {
        public string TableName { get; set; }
        public Dictionary<string, object> Values { get; set; }
    }
}
