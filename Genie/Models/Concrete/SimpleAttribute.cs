#region Usings

using Genie.Core.Models.Abstract;
using Genie.Core.Tools;

#endregion

namespace Genie.Core.Models.Concrete
{
    public class SimpleAttribute : ISimpleAttribute
    {
        public string DataType { get; set; }
        public string Name { get; set; }
        public string FieldName { get; set; }
        public bool IsLiteralType { get; set; }
        public string Comment { get; set; }
        
        public string GetHash()
        {
            return CommonTools.CalculateMd5Hash(new
            {
                Name,
                DataType,
                Comment
            });
        }
    }
}