
using Genie.Core.Base.Configuration.Abstract;
using Genie.Core.Base.Files.Abstract;
using Genie.Core.Base.Reading.Abstract;
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
            var schemaMock = new Mock<IDatabaseSchema>();

            fileSystemMock.Setup((f) => f.Exists(It.IsAny<string>()))
                .Returns(false);
                
            ISchemaCachingManager manager = new SchemaCachingManager(configurationMock.Object, fileSystemMock.Object);
            manager.SetupCache(schemaMock.Object);
        }

        [Fact]
        public void ConvertingSchemaToCache()
        {
            var configurationMock = new Mock<IConfiguration>();
            var fileSystemMock = new Mock<IFileSystem>();

            var schemaMock = new Mock<IDatabaseSchema>();
            ISchemaCachingManager schemaCachingManager = new SchemaCachingManager(configurationMock.Object, fileSystemMock.Object);
            schemaCachingManager.ConvertSchemaToSchemaCache(schemaMock.Object);
        }
    }
}
