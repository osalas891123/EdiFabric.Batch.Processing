using System.Collections.Generic;
using System.IO;

namespace EdiFabric.Batch.Processing.Contracts.Application.Contracts.Interfaces
{
    public interface IFileSystemWrapper
    {
        public string[] GetFiles(string path);
        public IEnumerable<string> GetFullFilePaths(string[] paths);
    }
}
