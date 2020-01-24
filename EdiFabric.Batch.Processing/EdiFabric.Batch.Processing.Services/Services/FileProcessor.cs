using System.Collections.Generic;
using EdiFabric.Batch.Processing.Contracts.Application.Contracts.Interfaces;
using EdiFabric.Templates.X12005010;

namespace EdiFabric.Batch.Processing.Services.Services
{
    public class FileProcessor : IFileProcessor
    {
        private readonly IFileSystemWrapper _fileSystemWrapper;
        private readonly IAppSettings _appSettings;

        public FileProcessor(IFileSystemWrapper fileSystemWrapper, IAppSettings appSettings)
        {
            _fileSystemWrapper = fileSystemWrapper;
            _appSettings = appSettings;
        }

        public IEnumerable<TS837> GetAllTransactionsFromFile(string file)
        {
            throw new System.NotImplementedException();
        }

        public IEnumerable<string> GetEdiFilesToProcess()
        {
            var files = _fileSystemWrapper
                .GetFullFilePaths(_fileSystemWrapper.GetFiles(_appSettings.FileLocations.EdiFilePickupLocation));

            foreach (var f in files)
                yield return f;
        }
    }
}
