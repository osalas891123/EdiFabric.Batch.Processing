using System;
using System.Collections.Generic;
using EdiFabric.Core.Model.Edi;

namespace EdiFabric.Batch.Processing.Contracts.Application.Contracts.Interfaces
{
    public interface IX12ReaderWrapper : IDisposable
    {
        IEnumerable<IEdiItem> ReadToEnd();
    }
}
