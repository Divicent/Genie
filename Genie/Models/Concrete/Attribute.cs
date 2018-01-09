#region Usings

using Genie.Core.Models.Abstract;

#endregion

namespace Genie.Core.Models.Concrete
{
    /// <summary>
    ///     represents an attribute
    /// </summary>
    internal class Attribute : SimpleAttribute, IAttribute
    {
        public bool IsKey { get; set; }
        public string RefPropName { get; set; }
        public bool IsIdentity { get; set; }
    }
}