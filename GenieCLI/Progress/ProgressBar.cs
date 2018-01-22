using System;
using System.Text;

namespace GenieCLI.Progress
{
    /// <summary>
    /// A Console ProgressBar implementaion
    /// </summary>
    public sealed class ProgressBar
    {
        private const int BlockCount = 10;
        private const string Animation = @"|/-\";
        private string _currentText = string.Empty;
        private int _animationIndex;

        public void Report(double value, string text)
        {
            value = Math.Max(0, Math.Min(1, value));
            var txt = !string.IsNullOrEmpty(text) ? $" - {text}" : "";
            if (txt.Length > 33)
                txt = $"...{txt.Substring(txt.Length - 30)}";
            
            UpdateText(value, txt );
        }

        private void UpdateText(double value, string message)
        {
            var progressBlockCount = (int) (value * BlockCount);
            var percent = (int) (value * 100);
            var text =
                $"[{new string('#', progressBlockCount)}{new string('-', BlockCount - progressBlockCount)}] {percent,3}% {Animation[_animationIndex++ % Animation.Length]}{message}";
            
            var commonPrefixLength = 0;
            var commonLength = Math.Min(_currentText.Length, text.Length);
            while (commonPrefixLength < commonLength && text[commonPrefixLength] == _currentText[commonPrefixLength])
            {
                commonPrefixLength++;
            }

            var outputBuilder = new StringBuilder();
            outputBuilder.Append('\b', _currentText.Length - commonPrefixLength);

            outputBuilder.Append(text.Substring(commonPrefixLength));

            var overlapCount = _currentText.Length - text.Length;
            if (overlapCount > 0)
            {
                outputBuilder.Append(' ', overlapCount);
                outputBuilder.Append('\b', overlapCount);
            }

            Console.Write(outputBuilder);
            _currentText = text;
        }
    }
}