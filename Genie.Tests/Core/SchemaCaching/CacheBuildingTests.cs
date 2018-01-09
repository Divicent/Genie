using Genie.Core.Models.Abstract;
using Genie.Core.Models.Concrete;
using Genie.Core.Tools;
using Xunit;
using static Xunit.Assert;

namespace Genie.Tests.Core.SchemaCaching
{
    
    public class CacheBuildingTests
    {
        [Fact]
        public void GetHashOfASimpleAttribuete()
        {
            var attribute = new SimpleAttribute
            {
                Name = "TheName",
                Comment = "TheComment",
                DataType = "string"
            };

            var hash = attribute.GetHash();
            Equal( CommonTools.CalculateMd5Hash(new {
                Name = "TheName",
                DataType = "string",
                Comment = "TheComment",
            }), hash);
            
        }
    }
}