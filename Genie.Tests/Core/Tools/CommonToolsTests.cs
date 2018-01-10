using Genie.Core.Tools;
using Xunit;
using static Xunit.Assert;

namespace Genie.Tests.Core.Tools
{
    public class CommonToolsTests
    {
        [Fact]
        public void CalculateMd5Hash()
        {
            const string input1 = "Rusith";
            const string input2 = "Rusith";
            const string input3 = "S";
            var hash1 = CommonTools.CalculateMd5Hash(input1);
            NotEmpty(hash1);
            var hash2 = CommonTools.CalculateMd5Hash(input2);
            NotEmpty(hash2);
            Equal(hash1, hash2);
            var hash3 = CommonTools.CalculateMd5Hash(input3);
            NotEmpty(hash3);
            NotEqual(hash1, hash3);
        }
    }
}