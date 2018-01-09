#region Usings

using System.Collections.Generic;
using Genie.Core.Models.Abstract;

#endregion

namespace Genie.Core.Base.Reading.Abstract
{
    /// <summary>
    ///     Schema of a database
    /// </summary>
    public interface IDatabaseSchema
    {
        /// <summary>
        ///     Base namespace of the DAL
        /// </summary>
        string BaseNamespace { get; set; }

        /// <summary>
        ///     List of relations
        /// </summary>
        List<IRelation> Relations { get; set; }

        /// <summary>
        ///     List of view
        /// </summary>
        List<IView> Views { get; set; }

        /// <summary>
        ///     List of stored procedures
        /// </summary>
        List<IStoredProcedure> Procedures { get; set; }

        /// <summary>
        ///     List of table enums
        /// </summary>
        List<IEnum> Enums { get; set; }
        
        /// <summary>
        /// Current version of the generator
        /// </summary>
        string GenieVersion { get; set; }
        
        /// <summary>
        /// Schema that used to read the schema
        /// </summary>
        string Schema { get; set; }
    }
}