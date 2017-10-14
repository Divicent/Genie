#region Usings

using System.Collections.Generic;
using Genie.Core.Base.ProcessOutput.Abstract;
using Genie.Core.Models.Abstract;

#endregion

namespace Genie.Core.Base.Writing.Abstract
{
    /// <summary>
    ///     Writes list of content file to the disk
    /// </summary>
    internal interface IFileWriter
    {
        /// <summary>
        ///     Write files to the disk
        /// </summary>
        /// <param name="files">Files to write</param>
        /// <param name="basePath">Base path </param>
        /// <param name="output">A Process output</param>
        void Write(List<IContentFile> files, string basePath, IProcessOutput output);
    }
}