using System.Collections.Generic;
using System.IO;
using EdiFabric.Batch.Processing.Contracts.Application.Contracts.Interfaces;

namespace EdiFabric.Batch.Processing.Contracts.Application.Wrappers
{
    public class FileSystemWrapper : IFileSystemWrapper
    {
        public string[] GetFiles(string path)
        {
            return Directory.GetFiles(path);
        }

        public IEnumerable<string> GetFullFilePaths(string[] paths)
        {
            foreach (var p in paths)
                yield return Path.GetFullPath(p.ToString());
        }
    }
}
