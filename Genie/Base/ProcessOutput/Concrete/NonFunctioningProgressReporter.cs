using Genie.Core.Base.ProcessOutput.Abstract;

namespace Genie.Core.Base.ProcessOutput.Concrete
{
    public class NonFunctioningProgressReporter: IProgressReporter
    {
        public void Dispose()
        {
            /*Does nothing*/
        }

        public void Tick()
        {
            /*Does nothing*/
        }

        public void Tick(string message)
        {
            /*Does nothing*/
        }

        public IProgressReporter Child(int total, string initalMessage)
        {
            return this;
        }
    }
}