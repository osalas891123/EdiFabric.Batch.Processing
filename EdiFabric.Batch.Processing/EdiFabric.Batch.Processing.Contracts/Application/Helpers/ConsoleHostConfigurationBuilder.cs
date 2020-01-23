using System.IO;
using Microsoft.Extensions.Configuration;

namespace EdiFabric.Batch.Processing.Contracts.Application.Helpers
{
    public class ConsoleHostConfigurationBuilder : ConfigurationBuilder
    {
        public ConsoleHostConfigurationBuilder()
        {
            this.SetBasePath(Directory.GetCurrentDirectory());
            this.AddJsonFile("appsettings.json", optional: true, reloadOnChange: true);
        }

        public new IConfigurationRoot Build() => base.Build();
    }
}
