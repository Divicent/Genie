using Genie.Core.Base.ProcessOutput.Abstract;
using ShellProgressBar;

namespace GenieCLI
{
    public class ProgressReporter: IProgressReporter
    {
        private readonly ProgressBar _progressbar;
        private readonly ChildProgressBar _progressBarChild;
        
        public ProgressReporter(int totalClicks, string initalMessage)
        {
            _progressbar = new ProgressBar(totalClicks, initalMessage);
        }

        private ProgressReporter(ChildProgressBar progressBarChild)
        {
            _progressBarChild = progressBarChild;
        }

        public void Dispose()
        {
            _progressbar?.Dispose();
            _progressBarChild?.Dispose();

        }

        public void Tick()
        {
            _progressbar?.Tick();
            _progressBarChild?.Tick();
        }
        
        public void Tick(string message)
        {
            _progressbar?.Tick(message);
            _progressBarChild?.Tick(message);
        }

        public IProgressReporter Child(int ticks, string message)
        {
            return new ProgressReporter(_progressbar.Spawn(ticks, message));
        }
    }
}