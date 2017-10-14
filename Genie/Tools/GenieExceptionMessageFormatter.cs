#region Usings

using System;
using System.Text;

#endregion

namespace Genie.Core.Tools
{
    internal class GenieExceptionMessageFormatter
    {
        /// <summary>
        ///     Includes base message, exception message , exception stack trace
        /// </summary>
        /// <param name="exception">exception to include</param>
        /// <param name="baseMessage">base message or the title of the result</param>
        /// <returns>formatted string</returns>
        public static string FormatException(Exception exception, string baseMessage)
        {
            const string messageTemplate =
                "$baseMessage$\n" +
                "\t$exceptionMessage$" +
                "\t\t$exceptionTrace$\n";

            return new StringBuilder(messageTemplate)
                .Replace("$baseMessage$", baseMessage ?? "")
                .Replace("$exceptionMessage$", exception.Message ?? "")
                .Replace("$exceptionTrace$",
                    exception.Source.Replace(Environment.NewLine, "\t\t" + Environment.NewLine))
                .ToString();
        }
    }
}