using System;

namespace Genie.Core.Base.ProcessOutput.Abstract
{
    /// <summary>
    /// Reports a progress of an operation
    /// This must be desposed after use
    /// </summary>
    public interface IProgressReporter: IDisposable
    {
        void Tick();
        void Tick(string message);
        IProgressReporter Child(int total, string initalMessage, string endMessage);
    }
}