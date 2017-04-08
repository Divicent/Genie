using Genie.Base.Abstract;

namespace Genie.Base
{
    internal class NonFunctioningProcessOutput : IProcessOutput
    {
        public void WriteInformation(string content)
        {
            /*Does nothing*/
        }

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
