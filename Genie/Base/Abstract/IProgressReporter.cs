using System;

namespace Genie.Base.Abstract
{
    /// <summary>
    /// Reports a progress
    /// </summary>
    public interface IProgressReporter: IDisposable
    {
        /// <summary>
        /// Set current progress
        /// </summary>
        /// <param name="progress">Progress</param>
        /// <param name="text">Progress</param>
        void Report(int progress, string text);
    }
}
