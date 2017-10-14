using System;
using Genie.Core.Base.ProcessOutput.Abstract;

namespace GenieCLI
{
    public class ProcessOutput : IProcessOutput
    {
        public  bool NoInfo { get; set; }
        public bool Silent { get; set; }

        public void WriteInformation(string content)
        {
            if(Silent || NoInfo) { return; }
            Console.WriteLine("-> " + content); // Noncompliant
        }

        public void WriteSuccess(string content)
        {
            if (Silent) {  return; }
            Console.Write("-> ");
            Console.ForegroundColor = ConsoleColor.DarkGreen;
            Console.WriteLine(content); // Noncompliant
            Console.ResetColor();
        }

        public void WriteWarning(string content)
        {
            if (Silent) { return; }
            Console.Write("-> ");
            Console.ForegroundColor = ConsoleColor.DarkYellow;
            Console.WriteLine(content); // Noncompliant
            Console.ResetColor();
        }
    }
}
