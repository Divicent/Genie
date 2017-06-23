using System.Collections.Generic;
using Genie.Base.Reading.Abstract;
using Genie.Models.Abstract;

namespace Genie.Base.Reading.Concrete
{
    internal class DatabaseSchema : IDatabaseSchema
    {
        public string BaseNamespace { get; set; }

        /// <summary>
        /// List of relations 
        /// </summary>
        public List<IRelation> Relations { get; set; }

        /// <summary>
        /// List of view
        /// </summary>
        public List<IView> Views { get; set; }

        /// <summary>
        /// List of stored procedures
        /// </summary>
        public List<IStoredProcedure> Procedures { get; set; }
    }
}
