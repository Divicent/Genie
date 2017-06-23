using System.Collections.Generic;
using Genie.Base.Configuration.Abstract;
using Genie.Base.ProcessOutput.Abstract;
using Genie.Base.Reading.Abstract;
using Genie.Models.Abstract;

namespace Genie.Base.Generating.Absract
{
    /// <summary>
    /// Helps to generate list of files from a schema
    /// </summary>
    internal interface IDalGenerator
    {
        /// <summary>
        /// Generate the DAL using schema and configuration
        /// </summary>
        /// <param name="schema">Schema to use</param>
        /// <param name="configuration">Configuration to use</param>
        /// <param name="output">Process output</param>
        /// <returns>Collection of file contents</returns>
        List<IContentFile> Generate(IDatabaseSchema schema, IConfiguration configuration, IProcessOutput output);
    }
}
