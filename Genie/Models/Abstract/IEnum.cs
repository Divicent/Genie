using System.Collections.Generic;

namespace Genie.Models.Abstract
{
    /// <summary>
    /// Represents an enum of a table
    /// </summary>
    internal interface IEnum
    {
        string TableName { get; set; }
        Dictionary<string, object> Values { get; set; }
    }
}
