using Genie.Base.Abstract;

namespace Genie.Base
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
