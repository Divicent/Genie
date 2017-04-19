using System.Collections.Generic;

namespace Genie.Models.Abstract
{
    /// <summary>
    /// Represents a view in a database
    /// </summary>
    internal interface IView
    {
        /// <summary>
        /// Name of the view
        /// </summary>
        string Name { get; }

        /// <summary>
        /// List of attributes (resulting columns) of the view
        /// </summary>
        List<ISimpleAttribute> Attributes { get; }

        /// <summary>
        /// The Field Name
        /// </summary>
        string FieldName { get; set; }
    }
}
