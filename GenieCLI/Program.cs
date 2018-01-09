using System;
using System.Linq;
using Genie.Core.Base.Generating;

namespace GenieCLI
{
    public static class Program
    {
        public static void Main(string[] args)
        {
            var fileName = "genieSettings.json";
            var output = new ProcessOutput();
            if (args.Length > 0)
            {
                if (args.Contains("-ni")) output.NoInfo = true;

                if (args.Contains("-s")) output.Silent = true;

                if (args.Contains("-f"))
                {
                    var index = args.ToList().IndexOf("-f");
                    if (args.Length > index + 1)
                    {
                        var fn = args[index + 1];
                        if (!string.IsNullOrWhiteSpace(fn)) fileName = fn;
                    }
                }
            }

            var path = $"./{fileName}";

            GenieGenerationResult result = null;
            if (args.Contains("-s"))
                result = Genie.Core.Base.Genie.Generate(path);
            else
                result = Genie.Core.Base.Genie.Generate(path, output);

            if (!result.Success)
            {
                Console.Write(":> ");
                Console.ForegroundColor = ConsoleColor.Red;
                Console.WriteLine(result.Error);
                Console.ResetColor();
            }
        }
    }
}