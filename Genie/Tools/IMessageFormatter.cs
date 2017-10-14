using System;

namespace Genie.Core.Tools
{
    /// <summary>
    ///     Helps to format messages (exceptions ..)
    /// </summary>
    internal interface IMessageFormatter
    {
        /// <summary>
        ///     Format Exception message with given base message as the title
        /// </summary>
        /// <param name="exception">exception to use</param>
        /// <param name="baseMessage">message title </param>
        /// <returns>resulting string</returns>
        string FormatException(Exception exception, string baseMessage);
    }
}