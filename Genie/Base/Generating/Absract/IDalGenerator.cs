using System.Collections.Generic;
using Genie.Core.Base.Configuration.Abstract;
using Genie.Core.Base.ProcessOutput.Abstract;
using Genie.Core.Base.Reading.Abstract;
using Genie.Core.Models.Abstract;

namespace Genie.Core.Base.Generating.Absract
{
    /// <summary>
    ///     Helps to generate list of files from a schema
    /// </summary>
    internal interface IDalGenerator
    {
        /// <summary>
        ///     Generate the DAL using schema and configuration
        /// </summary>
        /// <param name="schema">Schema to use</param>
        /// <param name="configuration">Configuration to use</param>
        /// <param name="output">Process output</param>
        /// <returns>Collection of file contents</returns>
        IEnumerable<IContentFile> Generate(IDatabaseSchema schema, IConfiguration configuration, IProcessOutput output);
    }
}