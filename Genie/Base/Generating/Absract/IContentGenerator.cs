using System.Collections.Generic;
using Genie.Core.Base.Configuration.Abstract;
using Genie.Core.Base.Reading.Abstract;
using Genie.Core.Models.Abstract;

namespace Genie.Core.Base.Generating.Absract
{
    /// <summary>
    ///     Helps to generate a DAL content from a database schema and settings
    /// </summary>
    internal interface IContentGenerator
    {
        /// <summary>
        ///     Generates list of file content using given schema and configurations
        /// </summary>
        /// <param name="schema">schema to use</param>
        /// <param name="configuration">basic configuration to use</param>
        /// <returns>list of files</returns>
        List<IContentFile> Generate(IDatabaseSchema schema, IConfiguration configuration);
    }
}