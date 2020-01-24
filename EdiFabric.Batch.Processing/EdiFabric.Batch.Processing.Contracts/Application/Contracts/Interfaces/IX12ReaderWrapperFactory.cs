namespace EdiFabric.Batch.Processing.Contracts.Application.Contracts.Interfaces
{
    public interface IX12ReaderWrapperFactory
    {
        IX12ReaderWrapper CreateInstance(string path, string rulesAssembly);
    }
}
