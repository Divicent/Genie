#region Usings

using DotLiquid;
using Genie.Core.Models.Abstract;

#endregion

namespace Genie.Core.Models.Concrete
{
    /// <summary>
    ///     represents an attribute
    /// </summary>
    public class Attribute : SimpleAttribute, IAttribute
    {
        public bool IsKey { get; set; }
        public string RefPropName { get; set; }
        public bool IsIdentity { get; set; }

        public new object ToLiquid()
        {
            return new
            {
                DataType,
                Name,
                FieldName,
                IsLiteralType,
                Comment,
                IsKey,
                RefPropName,
                IsIdentity,
                HasComment = !string.IsNullOrWhiteSpace(Comment),
                RefPropNameNull = RefPropName != null ? RefPropName + " = null;" : ""
            };
        }
    }
}