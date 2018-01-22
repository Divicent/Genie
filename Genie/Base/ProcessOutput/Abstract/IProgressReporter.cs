using System;

namespace Genie.Core.Base.ProcessOutput.Abstract
{
    /// <summary>
    /// Reports a progress of an operation
    /// This must be desposed after use
    /// </summary>
    public interface IProgressReporter
    {
        void Tick(string message);
    }
}