using System.IO;
using EdiFabric.Batch.Processing.Contracts.Application.Contracts.Models;
using Microsoft.Extensions.Configuration;

/// <summary>
/// This class fixture is meant to minimize code and share the same instance of appsettings throughout all unit test classes
/// that use it.
/// </summary>

namespace EdiFabric.Batch.Processing.Tests.Unit.Services
{
    public class AppSettingsFixture
    {
        public AppSettings AppSettings { get; private set; }

        public AppSettingsFixture()
        {
            var configuration = new ConfigurationBuilder()
              .SetBasePath(Directory.GetCurrentDirectory())
              .AddJsonFile("appSettings.json")
              .Build();

            // Get values from appsettings.json.
            AppSettings = configuration.GetSection(nameof(AppSettings)).Get<AppSettings>();
        }
    }
}
