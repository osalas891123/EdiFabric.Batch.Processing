using EdiFabric.Batch.Processing.Contracts.Application.Contracts.Interfaces;
using EdiFabric.Batch.Processing.Contracts.Application.Wrappers;

namespace EdiFabric.Batch.Processing.Contracts.Application.Factories
{
    public class X12ReaderWrapperFactory : IX12ReaderWrapperFactory
    {
        public IX12ReaderWrapper CreateInstance(string path, string rulesAssembly)
        {
            return new X12ReaderWrapper(path, rulesAssembly);
        }
    }
}
