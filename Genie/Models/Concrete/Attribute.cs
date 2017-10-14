#region Usings

using Genie.Core.Models.Abstract;

#endregion

namespace Genie.Core.Models.Concrete
{
    /// <summary>
    ///     represents an attribute
    /// </summary>
    internal class Attribute : IAttribute
    {
        public bool IsKey { get; set; }
        public string DataType { get; set; }
        public string Name { get; set; }
        public string FieldName { get; set; }
        public string RefPropName { get; set; }
        public bool IsLiteralType { get; set; }
        public string Comment { get; set; }
        public bool IsIdentity { get; set; }
    }
}