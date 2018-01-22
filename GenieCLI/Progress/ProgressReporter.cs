using System;
using Genie.Core.Base.ProcessOutput.Abstract;

namespace GenieCLI.Progress
{
    public class ProgressReporter : IProgressReporter
    {
        private readonly ProgressBar _progressbar;
        private readonly string _endMessage;
        private readonly int _maxTicks;
        private int _lastTick;

        public ProgressReporter(int totalClicks, string initialMessage, string endMessage)
        {
            Console.Write($"-> {initialMessage} ");
            _maxTicks = totalClicks;
            _progressbar = new ProgressBar();
            _endMessage = endMessage;
        }

        public void Tick(string message = "")
        {
            _lastTick ++;
            var value = ( ((double)_lastTick / _maxTicks) * 100) / 100;
            _progressbar.Report(value, message);
            if (_lastTick < _maxTicks) 
                return;
            Console.Write($"\n-> {_endMessage}\n");
        }
    }
}