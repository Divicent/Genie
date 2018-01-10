#region Usings

using System.Collections.Generic;

#endregion

namespace Genie.Core.Models.Abstract
{
    /// <summary>
    ///     Represents an enum of a table
    /// </summary>
    public interface IEnum
    {
        string Type { get; }
        string Name { get; }
        List<IEnumValue> Values { get; }
    }
}