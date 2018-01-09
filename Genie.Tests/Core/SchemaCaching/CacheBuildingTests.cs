using System.Collections.Generic;
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

        [Fact]
        public void GenerateHashOfAModel()
        {
            IModel model = new Relation
            {
                Name = "TestView", 
                Comment = "TestComment",
                
                Attributes = new List<IAttribute>
                {
                    new Attribute
                    {
                        Name = "Attribute1",
                        Comment = "TheComment1",
                        DataType = "string"
                    },
                    new Attribute
                    {
                        Name = "Attribute2",
                        Comment = "TheComment2",
                        DataType = "string"
                    }
                }
                
            };
        }
    }
}