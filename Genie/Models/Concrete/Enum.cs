#region Usings

using System.Collections.Generic;
using DotLiquid;
using Genie.Core.Models.Abstract;

#endregion

namespace Genie.Core.Models.Concrete
{
    public class Enum : IEnum, ILiquidizable
    {
        public string Type { get; set; }
        public string Name { get; set; }
        public List<IEnumValue> Values { get; set; }
        public object ToLiquid()
        {
            return new
            {
                Type,
                Name,
                Values
            };
        }
    }
}