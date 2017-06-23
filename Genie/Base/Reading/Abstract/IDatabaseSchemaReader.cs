using Genie.Base.Abstract;
using Genie.Base.Configuration.Abstract;

namespace Genie.Base.Reading.Abstract
{

    /// <summary>
    /// Provides an interface to read database schema of a database
    /// </summary>
    internal interface IDatabaseSchemaReader
    {
        /// <summary>
        /// Reads the database and provides the schema of the database
        /// </summary>
        /// <returns></returns>
        IDatabaseSchema Read(IConfiguration configuration, IProcessOutput output);
    }
}
