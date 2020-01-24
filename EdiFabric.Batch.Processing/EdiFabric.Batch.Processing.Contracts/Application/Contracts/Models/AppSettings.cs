using EdiFabric.Batch.Processing.Contracts.Application.Contracts.Interfaces;

namespace EdiFabric.Batch.Processing.Contracts.Application.Contracts.Models
{
    public class AppSettings : IAppSettings
    {
        public FileLocations FileLocations { get; set; }
    }

    public class FileLocations
    {
        public string EdiFilePickupLocation { get; set; }
    }
}
