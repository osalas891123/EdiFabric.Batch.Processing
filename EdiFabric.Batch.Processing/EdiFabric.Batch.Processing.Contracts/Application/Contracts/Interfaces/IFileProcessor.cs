using System.Collections.Generic;
using EdiFabric.Templates.X12005010;

namespace EdiFabric.Batch.Processing.Contracts.Application.Contracts.Interfaces
{
    public interface IFileProcessor
    {
        IEnumerable<string> GetEdiFilesToProcess();
        IEnumerable<TS837> GetAllTransactionsFromFile(string file);
    }
}
