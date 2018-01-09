using System;
using Genie.Core.Base.ProcessOutput.Abstract;
using ShellProgressBar;

namespace GenieCLI
{
  public class ProgressReporter : IProgressReporter
  {
    private readonly ProgressBar _progressbar;
    private readonly ChildProgressBar _progressBarChild;
    private string _endMessage;

    public ProgressReporter(int totalClicks, string initalMessage, string endMessage)
    {
      _progressbar = new ProgressBar(totalClicks + 1, initalMessage, new ProgressBarOptions
      {
        CollapseWhenFinished = false,
        ProgressCharacter = '─',
        ProgressBarOnBottom = true
      });

      _endMessage = endMessage;
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
      var currentTick = (_progressbar?.CurrentTick ?? _progressBarChild?.CurrentTick).GetValueOrDefault() + 1;
      var maxTicks = (_progressbar?.MaxTicks ?? _progressBarChild?.MaxTicks).GetValueOrDefault();

      if (currentTick == maxTicks)
      {
        if(_progressbar != null)
          _progressbar.Message = _endMessage;
        else if(_progressBarChild != null)
          _progressBarChild.Message = _endMessage;
      }
      _progressbar?.Tick();
      _progressBarChild?.Tick();
    }

    public void Tick(string message)
    {
      var currentTick = (_progressbar?.CurrentTick ?? _progressBarChild?.CurrentTick).GetValueOrDefault() + 1;
      var maxTicks = (_progressbar?.MaxTicks ?? _progressBarChild?.MaxTicks).GetValueOrDefault();

      message = (currentTick == maxTicks) ? _endMessage : $"{currentTick} of {maxTicks - 1} -  {message}";
      _progressbar?.Tick(message);
      _progressBarChild?.Tick(message);
    }

    public IProgressReporter Child(int ticks, string message, string endMessage)
    {
      var options = new ProgressBarOptions
      {
        CollapseWhenFinished = false,
        ProgressCharacter = '─',
        ProgressBarOnBottom = true,
        ForegroundColor = ConsoleColor.Yellow,
        BackgroundColor = ConsoleColor.DarkYellow,
      };

      var pb = new ProgressReporter(_progressbar.Spawn(ticks + 1, message, options));
      pb._endMessage = endMessage;
      return pb;
    }
  }
}