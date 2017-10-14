using Genie.Core.Base.ProcessOutput.Abstract;

namespace Genie.Core.Base.ProcessOutput.Concrete
{
    /// <summary>
    ///     A process output implementation that does not do anything
    /// </summary>
    internal class NonFunctioningProcessOutput : IProcessOutput
    {
        public void WriteInformation(string content)
        {
            /*Does nothing*/
        }

        //public IProgressReporter WriteProgress()
        //{
        //    return new NonFunctionningProgressReporter();
        //}

        public void WriteSuccess(string content)
        {
            /*Does nothing*/
        }

        public void WriteWarning(string content)
        {
            /*Does nothing*/
        }
    }
}