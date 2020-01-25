using EdiFabric.Batch.Processing.Contracts.Application.Contracts.Models;

namespace EdiFabric.Batch.Processing.Contracts.Application.Contracts.Interfaces
{
    public interface IAppSettings
    {
        FileLocations FileLocations { get; set; }
    }
}
