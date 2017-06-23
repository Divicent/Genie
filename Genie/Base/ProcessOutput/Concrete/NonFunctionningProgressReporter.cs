using Genie.Base.ProcessOutput.Abstract;

namespace Genie.Base.ProcessOutput.Concrete
{
    internal class NonFunctionningProgressReporter : IProgressReporter
    {
        public void Dispose()
        {
            /*Does nothing*/
        }

        public void Report(int progress, string text)
        {
            /*Does nothing*/
        }
    }
}
