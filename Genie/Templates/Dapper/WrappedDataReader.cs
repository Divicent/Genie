#region Usings

#endregion

using Genie.Core.Base.Generating;

namespace Genie.Core.Templates.Dapper
{
    internal class WrappedDataReaderTemplate : GenieTemplate
    {
        public WrappedDataReaderTemplate(string path) : base(path)
        {
        }

        public override string Generate()
        {
            L($@"
using System.Data;

namespace {GenerationContext.BaseNamespace}.Dapper
{{
    /// <summary>
    /// Describes a reader that controls the lifetime of both a command and a reader,
    /// exposing the downstream command/reader as properties.
    /// </summary>
    public interface IWrappedDataReader : IDataReader
    {{
        /// <summary>
        /// Obtain the underlying reader
        /// </summary>
        IDataReader Reader {{ get; }}
        /// <summary>
        /// Obtain the underlying command
        /// </summary>
        IDbCommand Command {{ get; }}
    }}
}}

");

            return E();
        }
    }
}