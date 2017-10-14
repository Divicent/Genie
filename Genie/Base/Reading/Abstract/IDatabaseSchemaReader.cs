using Genie.Core.Base.Configuration.Abstract;
using Genie.Core.Base.ProcessOutput.Abstract;

namespace Genie.Core.Base.Reading.Abstract
{
    /// <summary>
    ///     Provides an interface to read database schema of a database
    /// </summary>
    internal interface IDatabaseSchemaReader
    {
        /// <summary>
        ///     Reads the database and provides the schema of the database
        /// </summary>
        /// <returns></returns>
        IDatabaseSchema Read(IConfiguration configuration, IProcessOutput output);
    }
}