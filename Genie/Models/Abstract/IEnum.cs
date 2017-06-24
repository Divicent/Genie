using System.Collections.Generic;

namespace Genie.Models.Abstract
{
    /// <summary>
    /// Represents an enum of a table
    /// </summary>
    internal interface IEnum
    {
        string Type { get; }
        string Name { get; }
        List<IEnumValue> Values { get; }
    }
}
