
using Genie.Core.Base.Configuration.Abstract;
using Genie.Core.Base.Files.Abstract;
using Genie.Core.Base.Reading.Abstract;
using Genie.Core.Base.SchemaCaching.Abstract;
using Genie.Core.Base.SchemaCaching.Concrete;
using Genie.Core.Models.Abstract;
using Moq;
using Xunit;
using static Xunit.Assert;

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

//        [Fact]
//        public void ConvertingSchemaToCache()
//        {
//            var schemaMock = new Mock<IDatabaseSchema>();
//            var schemaCachingManager = GetMockeCachingManager();
//            dynamic cache = schemaCachingManager.ConvertSchemaToSchemaCache(schemaMock.Object);
//
//            NotNull(cache.root);
//            IsType<object>(cache.root);
//        }

        [Fact]
        public void CreatingTheRootPartOfTheCacheObject()
        {
            
            const string genieVersion = "v1";
            const string baseNamespace = "a.b";
            const bool isCore = true;
            const bool noDapper = true;
            const string schema = "bestAppEver";
            
            var configurationMock = new Mock<IConfiguration>();
            
            configurationMock.SetupGet(c => c.GenieVersion).Returns(genieVersion);
            configurationMock.SetupGet(c => c.BaseNamespace).Returns(baseNamespace);
            configurationMock.SetupGet(c => c.Core).Returns(isCore);
            configurationMock.SetupGet(c => c.NoDapper).Returns(noDapper);
            configurationMock.SetupGet(c => c.Schema).Returns(schema);
            
            var fileSystemMock = new Mock<IFileSystem>();
            
            ISchemaCachingManager cachingManager = new SchemaCachingManager(configurationMock.Object, fileSystemMock.Object);
            var schemaMock = new Mock<IDatabaseSchema>();
            var root = cachingManager.CreateRootObject();
            
            NotNull(root);
            
            Equal(root.GenieVersion, genieVersion);
            Equal(root.BaseNamespace, baseNamespace);
            Equal(root.IsCore, isCore);
            Equal(root.NoDapper, noDapper);
            Equal(root.Schema, schema);
            
        }

//        [Fact]
//        public void CreateModelCacheObject()
//        {
//            var cacheManager = GetMockeCachingManager();
//            var co = cacheManager.CreateModelCacheObject(new Mock<IModel>(new { Name = "SomeName" }).Object);
//            NotNull(co);
//            Equal("SomeName", co.Name);
//        }

        private static ISchemaCachingManager GetMockeCachingManager()
        {
            var configurationMock = new Mock<IConfiguration>();
            var fileSystemMock = new Mock<IFileSystem>();
            return new SchemaCachingManager(configurationMock.Object, fileSystemMock.Object);
        }
    }
}
