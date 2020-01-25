using System;
using System.Collections.Generic;
using System.Linq;
using EdiFabric.Batch.Processing.Contracts.Application.Contracts.Interfaces;
using EdiFabric.Core.Model.Edi;
using EdiFabric.Templates.X12005010;

namespace EdiFabric.Batch.Processing.Services.Services
{
    public class FileProcessor : IFileProcessor
    {
        private readonly IFileSystemWrapper _fileSystemWrapper;
        private readonly IAppSettings _appSettings;
        private readonly IX12ReaderWrapperFactory _x12ReaderWrapperFactory;

        public FileProcessor(IFileSystemWrapper fileSystemWrapper, IAppSettings appSettings, IX12ReaderWrapperFactory x12ReaderWrapperFactory)
        {
            _fileSystemWrapper = fileSystemWrapper;
            _appSettings = appSettings;
            _x12ReaderWrapperFactory = x12ReaderWrapperFactory;
        }

        public IEnumerable<TS837> GetAllTransactionsFromFile(string file)
        {
            var ediItems = Enumerable.Empty<IEdiItem>();

            // Translate EDI file. 
            using var reader = _x12ReaderWrapperFactory.CreateInstance(file, "EdiFabric.Batch.Processing.Contracts");
            ediItems = reader.ReadToEnd();

            return ediItems.OfType<TS837>();
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
