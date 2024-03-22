using FileManager.ContentProvider;
using Moq;

namespace FileManager.Tests
{
    public class Tests
    {
        [Fact]
        public async Task StoreAsync_WhenValid_ReturnsSuccess()
        {
            // Arrange
            var mockFile = new Mock<IFileHandler<int>>();
            mockFile.Setup(x => x.CreateAsync(It.IsAny<int>(), It.IsAny<StreamInfo>(), It.IsAny<CancellationToken>())).Verifiable();
  
            var provider = new Mock<FileContentProvider<int>>(mockFile.Object);
            
            // Act
            var result = await provider.Object.StoreAsync(It.IsAny<int>(), It.IsAny<StreamInfo>(), It.IsAny<CancellationToken>());

            // Assert
            Assert.True(result.Success);
        }

        [Fact]
        public async Task StoreAsync_WhenException_ReturnsFail()
        {
            // Arrange
            var mockFile = new Mock<IFileHandler<int>>();
            mockFile.Setup(x => x.CreateAsync(It.IsAny<int>(), It.IsAny<StreamInfo>(), It.IsAny<CancellationToken>())).Throws<Exception>();

            var provider = new Mock<FileContentProvider<int>>(mockFile.Object);

            // Act
            var result = await provider.Object.StoreAsync(It.IsAny<int>(), It.IsAny<StreamInfo>(), It.IsAny<CancellationToken>());

            // Assert
            Assert.True(result.Fail);
        }

        [Fact]
        public async Task ExistsAsync_WhenFileExists_ReturnsTrue()
        {
            // Arrange
            var mockFile = new Mock<IFileHandler<int>>();
            mockFile.Setup(x => x.Exists(It.IsAny<int>())).Returns(true);

            var provider = new Mock<FileContentProvider<int>>(mockFile.Object);

            // Act
            var result = await provider.Object.ExistsAsync(It.IsAny<int>(), It.IsAny<CancellationToken>());

            // Assert
            Assert.True(result.ResultObject);
        }

        [Fact]
        public async Task ExistsAsync_WhenFileNotExists_ReturnsFalse()
        {
            // Arrange
            var mockFile = new Mock<IFileHandler<int>>();
            mockFile.Setup(x => x.Exists(It.IsAny<int>())).Returns(false);

            var provider = new Mock<FileContentProvider<int>>(mockFile.Object);

            // Act
            var result = await provider.Object.ExistsAsync(It.IsAny<int>(), It.IsAny<CancellationToken>());

            // Assert
            Assert.False(result.ResultObject);
        }

        [Fact]
        public async Task ExistsAsync_WhenException_ReturnsFalse()
        {
            // Arrange
            var mockFile = new Mock<IFileHandler<int>>();
            mockFile.Setup(x => x.Exists(It.IsAny<int>())).Throws<Exception>();

            var provider = new Mock<FileContentProvider<int>>(mockFile.Object);

            // Act
            var result = await provider.Object.ExistsAsync(It.IsAny<int>(), It.IsAny<CancellationToken>());

            // Assert
            Assert.False(result.ResultObject);
        }

        [Fact]
        public async Task GetAsync_WhenValid_ReturnsStreamInfo()
        {
            // Arrange
            var mockFile = new Mock<IFileHandler<int>>();
            mockFile.Setup(x => x.OpenRead(It.IsAny<int>())).Returns(It.IsAny<FileStream>());

            var provider = new Mock<FileContentProvider<int>>(mockFile.Object);

            // Act
            var result = await provider.Object.GetAsync(It.IsAny<int>(), It.IsAny<CancellationToken>());

            // Assert
            Assert.True(result.Success);
            Assert.NotNull(result.ResultObject);
            Assert.IsType<StreamInfo>(result.ResultObject);
        }

        [Fact]
        public async Task GetAsync_WhenException_ReturnsNull()
        {
            // Arrange
            var mockFile = new Mock<IFileHandler<int>>();
            mockFile.Setup(x => x.OpenRead(It.IsAny<int>())).Throws<Exception>();

            var provider = new Mock<FileContentProvider<int>>(mockFile.Object);

            // Act
            var result = await provider.Object.GetAsync(It.IsAny<int>(), It.IsAny<CancellationToken>());

            // Assert
            Assert.True(result.Fail);
            Assert.Null(result.ResultObject);
        }

        [Fact]
        public async Task GetBytesAsync_WhenValid_ReturnsByteArray()
        {
            // Arrange
            var mockFile = new Mock<IFileHandler<int>>();
            mockFile.Setup(x => x.ReadAllBytesAsync(It.IsAny<int>(), It.IsAny<CancellationToken>())).Returns(Task.FromResult(Array.Empty<byte>()));

            var provider = new Mock<FileContentProvider<int>>(mockFile.Object);

            // Act
            var result = await provider.Object.GetBytesAsync(It.IsAny<int>(), It.IsAny<CancellationToken>());

            // Assert
            Assert.True(result.Success);
            Assert.NotNull(result.ResultObject);
            Assert.IsType<byte[]>(result.ResultObject);
        }

        [Fact]
        public async Task GetBytesAsync_WhenException_ReturnsNull()
        {
            // Arrange
            var mockFile = new Mock<IFileHandler<int>>();
            mockFile.Setup(x => x.ReadAllBytesAsync(It.IsAny<int>(), It.IsAny<CancellationToken>())).Throws<Exception>();

            var provider = new Mock<FileContentProvider<int>>(mockFile.Object);

            // Act
            var result = await provider.Object.GetBytesAsync(It.IsAny<int>(), It.IsAny<CancellationToken>());

            // Assert
            Assert.True(result.Fail);
            Assert.Null(result.ResultObject);
        }

        [Fact]
        public async Task UpdateAsync_WhenValid_ReturnsSuccess()
        {
            // Arrange
            var mockFile = new Mock<IFileHandler<int>>();
            mockFile.Setup(x => x.CreateAsync(It.IsAny<int>(), It.IsAny<StreamInfo>(), It.IsAny<CancellationToken>())).Verifiable();

            var provider = new Mock<FileContentProvider<int>>(mockFile.Object);

            // Act
            var result = await provider.Object.UpdateAsync(It.IsAny<int>(), It.IsAny<StreamInfo>(), It.IsAny<CancellationToken>());

            // Assert
            Assert.True(result.Success);
        }

        [Fact]
        public async Task UpdateAsync_WhenException_ReturnsFail()
        {
            // Arrange
            var mockFile = new Mock<IFileHandler<int>>();
            mockFile.Setup(x => x.CreateAsync(It.IsAny<int>(), It.IsAny<StreamInfo>(), It.IsAny<CancellationToken>())).Throws<Exception>();

            var provider = new Mock<FileContentProvider<int>>(mockFile.Object);

            // Act
            var result = await provider.Object.UpdateAsync(It.IsAny<int>(), It.IsAny<StreamInfo>(), It.IsAny<CancellationToken>());

            // Assert
            Assert.True(result.Fail);
        }

        [Fact]
        public async Task DeleteAsync_WhenValid_ReturnsSuccess()
        {
            // Arrange
            var mockFile = new Mock<IFileHandler<int>>();
            mockFile.Setup(x => x.Exists(It.IsAny<int>())).Returns(true);
            mockFile.Setup(x => x.Delete(It.IsAny<int>())).Verifiable();

            var provider = new Mock<FileContentProvider<int>>(mockFile.Object);

            // Act
            var result = await provider.Object.DeleteAsync(It.IsAny<int>(), It.IsAny<CancellationToken>());

            // Assert
            Assert.True(result.Success);
        }

        [Fact]
        public async Task DeleteAsync_WhenException_ReturnsFail()
        {
            // Arrange
            var mockFile = new Mock<IFileHandler<int>>();
            mockFile.Setup(x => x.Exists(It.IsAny<int>())).Returns(true);
            mockFile.Setup(x => x.Delete(It.IsAny<int>())).Throws<Exception>();

            var provider = new Mock<FileContentProvider<int>>(mockFile.Object);

            // Act
            var result = await provider.Object.DeleteAsync(It.IsAny<int>(), It.IsAny<CancellationToken>());

            // Assert
            Assert.True(result.Fail);
        }

        [Fact]
        public async Task GetHashAsync_WhenValid_ReturnsHash()
        {
            // Arrange
            var mockFile = new Mock<IFileHandler<int>>();
            mockFile.Setup(x => x.GetHashAsync(It.IsAny<int>(), It.IsAny<CancellationToken>())).Returns(Task.FromResult(string.Empty));

            var provider = new Mock<FileContentProvider<int>>(mockFile.Object);

            // Act
            var result = await provider.Object.GetHashAsync(It.IsAny<int>(), It.IsAny<CancellationToken>());

            // Assert
            Assert.True(result.Success);
            Assert.NotNull(result.ResultObject);
            Assert.IsType<string>(result.ResultObject);
        }

        [Fact]
        public async Task GetHashAsync_WhenException_ReturnsNull()
        {
            // Arrange
            var mockFile = new Mock<IFileHandler<int>>();
            mockFile.Setup(x => x.GetHashAsync(It.IsAny<int>(), It.IsAny<CancellationToken>())).Throws<Exception>();

            var provider = new Mock<FileContentProvider<int>>(mockFile.Object);

            // Act
            var result = await provider.Object.GetHashAsync(It.IsAny<int>(), It.IsAny<CancellationToken>());

            // Assert
            Assert.True(result.Fail);
            Assert.Null(result.ResultObject);
        }
    }
}