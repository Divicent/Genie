#region Usings

using DotLiquid;
using Genie.Core.Models.Abstract;

#endregion

namespace Genie.Core.Models.Concrete
{
    public class EnumValue : IEnumValue, ILiquidizable
    {
        public string Name { get; set; }
        public object Value { get; set; }
        public string FieldName { get; set; }
        public object ToLiquid()
        {
            return new
            {
                Name,
                Value,
                FieldName
            };
        }
    }
}