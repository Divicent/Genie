
using Genie.Core.Base.Configuration.Abstract;
using Genie.Core.Base.Files.Abstract;
using Genie.Core.Base.SchemaCaching.Abstract;
using Genie.Core.Base.SchemaCaching.Concrete;
using Moq;
using Xunit;

namespace Genie.Tests.Core.SchemaCaching
{
    public class SchemaCachingManagerTests
    {
        [Fact]
        public void TestSetupCacheSchemaFileDoesNotExists()
        {
            var configurationMock = new Mock<IConfiguration>();
            var fileSystemMock = new Mock<IFileSystem>();

            fileSystemMock.Setup((f) => f.Exists(It.IsAny<string>()))
                .Returns(false);

            ISchemaCachingManager manager = new SchemaCachingManager(configurationMock.Object, fileSystemMock.Object);
        }
    }
}
