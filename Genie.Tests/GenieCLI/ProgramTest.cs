using GenieCLI;
using Xunit;

namespace Genie.Tests.GenieCLI
{
    
    public class ProgramTest
    {
        [Fact]
        public void TesProgram()
        {
            Program.Main(new string[] { "-y" });
        }
    }
}