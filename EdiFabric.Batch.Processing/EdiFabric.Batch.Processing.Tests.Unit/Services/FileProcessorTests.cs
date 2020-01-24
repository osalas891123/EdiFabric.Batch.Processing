using System;
using Autofac.Extras.Moq;
using EdiFabric.Batch.Processing.Contracts.Application.Contracts.Interfaces;
using EdiFabric.Batch.Processing.Services.Services;
using Moq;
using Xunit;

namespace EdiFabric.Batch.Processing.Tests.Unit.Services
{
    public class FileProcessorTests : IClassFixture<AppSettingsFixture>, IDisposable
    {
        private readonly AppSettingsFixture _appSettingsFixture;
        private readonly AutoMock _mock;
        private readonly FileProcessor _sut;

        public FileProcessorTests(AppSettingsFixture appSettingsFixture)
        {
            _appSettingsFixture = appSettingsFixture;
            _mock = AutoMock.GetLoose();
            _mock.Provide<IAppSettings>(_appSettingsFixture.AppSettings);
            _sut = _mock.Create<FileProcessor>();
        }

        [Fact]
        public void FileProcessor_GetAllTransactionsFromFile_Returns_Collection_Type_TS837()
        {

        }

        [Fact]
        public void FileProcessor_GetEdiFilesToProcess_Files_Exist_Return_Files()
        {
            // arrange
            var files = new string[] { "file.txt", "file.txt" };

            _mock.Mock<IFileSystemWrapper>().Setup(x => x.GetFiles
                (_appSettingsFixture.AppSettings.FileLocations.EdiFilePickupLocation)
            );

            _mock.Mock<IFileSystemWrapper>().Setup(x => x.GetFullFilePaths(It.IsAny<string[]>())).Returns(files);

            // act
            var result = _sut.GetEdiFilesToProcess();

            // assert
            Assert.NotEmpty(result);
        }

        [Fact]
        public void FileProcessor_GetEdiFilesToProcess_Not_Exist_Return_Empty()
        {
            // arrange
            var files = new string[] { };

            _mock.Mock<IFileSystemWrapper>().Setup(x => x.GetFiles
                (_appSettingsFixture.AppSettings.FileLocations.EdiFilePickupLocation)
            );

            _mock.Mock<IFileSystemWrapper>().Setup(x => x.GetFullFilePaths(It.IsAny<string[]>())).Returns(files);

            // act
            var result = _sut.GetEdiFilesToProcess();

            // assert
            Assert.Empty(result);
        }

        public void Dispose()
        {
            _mock?.Dispose();
        }
    }
}
