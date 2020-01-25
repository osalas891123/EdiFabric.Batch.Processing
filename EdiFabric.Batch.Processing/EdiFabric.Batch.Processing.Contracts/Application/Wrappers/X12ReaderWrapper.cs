using System;
using System.Collections.Generic;
using System.IO;
using EdiFabric.Batch.Processing.Contracts.Application.Contracts.Interfaces;
using EdiFabric.Core.Model.Edi;
using EdiFabric.Framework.Readers;

namespace EdiFabric.Batch.Processing.Contracts.Application.Wrappers
{
    public class X12ReaderWrapper : IX12ReaderWrapper
    {
        private readonly X12Reader _x12Reader;

        public X12ReaderWrapper(string path, string rulesAssembly)
        {
            _x12Reader = CreateX12Reader(path, rulesAssembly);
        }

        public IEnumerable<IEdiItem> ReadToEnd()
        {
            return _x12Reader.ReadToEnd();
        }

        private X12Reader CreateX12Reader(string path, string rulesAssembly)
        {
            if (string.IsNullOrEmpty(path) || string.IsNullOrEmpty(rulesAssembly))
                throw new ArgumentNullException("X12Reader");

            return new X12Reader(File.OpenRead(path), rulesAssembly);
        }

        public void Dispose()
        {
            _x12Reader?.Dispose();
        }
    }
}
