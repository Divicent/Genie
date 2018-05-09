#region Usings

using DotLiquid;
using Genie.Core.Models.Abstract;

#endregion

namespace Genie.Core.Models.Concrete
{
    /// <summary>
    ///     represents an attribute
    /// </summary>
    public class Attribute: IAttribute, ILiquidizable
    {
        public string DataType { get; set; }
        public string Name { get; set; }
        public string FieldName { get; set; }
        public bool IsLiteralType { get; set; }
        public string Comment { get; set; }
        public bool IsKey { get; set; }
        public string RefPropName { get; set; }
        public bool IsIdentity { get; set; }

        public object ToLiquid()
        {
            var atdComment = !string.IsNullOrWhiteSpace(Comment);

            return new
            {
                DataType,
                Name,
                FieldName,
                IsLiteralType,
                Comment,
                atdComment = !string.IsNullOrWhiteSpace(Comment),
                commentStr = atdComment
                    ? $@"
        /// <para>{Comment}</para>" : "",
                IsKey,
                RefPropName,
                IsIdentity,
                HasComment = !string.IsNullOrWhiteSpace(Comment),
                RefPropNameNull = RefPropName != null ? RefPropName + " = null;" : ""
            };
        }
    }
}