using Genie.Core.Base.Exceptions;
using Genie.Core.Base.Files.Abstract;
using Genie.Core.Base.Versioning.Abstract;
using Genie.Core.Base.Versioning.Concrete;
using Moq;
using Xunit;
using static Xunit.Assert;

namespace Genie.Tests.Core.Versioning
{
    public class VersionManagerTests
    {
        [Fact]
        public void TestReadingCurrentVersionButVersionFileDoesNotExist()
        {
            var fileSystemMock = new Mock<IFileSystem>();
            fileSystemMock.Setup(fs => fs.Exists(It.IsAny<string>()))
                .Returns(false);

            IVersionManager varsionManager = new GenieVersionManager(fileSystemMock.Object);
            var exception = Throws<GenieException>(() => { varsionManager.GetCurrentVersion(); });
            NotNull(exception);
            NotEmpty(exception.Message);
        }

        [Fact]
        public void TestReadingCurrentVersionWithExistingVersionFile()
        {
            var fileSystemMock = new Mock<IFileSystem>();
            fileSystemMock.Setup(fs => fs.Exists(It.IsAny<string>()))
                .Returns(true);

            const string version = "v0.0.1";
            fileSystemMock.Setup(fs => fs.ReadText(It.IsAny<string>()))
                .Returns(version);

            IVersionManager versionManager = new GenieVersionManager(fileSystemMock.Object);
            var currentVersion = versionManager.GetCurrentVersion();
            Equal(version, currentVersion);
        }
    }
}