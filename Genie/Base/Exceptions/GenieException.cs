#region Usings

using System;

#endregion

namespace Genie.Core.Base.Exceptions
{
    public class GenieException : Exception
    {
        public GenieException(string message) : base(message)
        {
        }

        public GenieException(string message, Exception innerException) : base(message, innerException)
        {
        }
    }
}