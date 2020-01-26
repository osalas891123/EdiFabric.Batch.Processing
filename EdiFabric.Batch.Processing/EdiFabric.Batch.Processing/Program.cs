using System;
using System.Diagnostics;
using System.Threading.Tasks;
using EdiFabric.Batch.Processing.Contracts.Application.Contracts.Interfaces;
using EdiFabric.Batch.Processing.Contracts.Application.Contracts.Models;
using EdiFabric.Batch.Processing.Contracts.Application.Enums;
using EdiFabric.Batch.Processing.Contracts.Application.Factories;
using EdiFabric.Batch.Processing.Contracts.Application.Helpers;
using EdiFabric.Batch.Processing.Contracts.Application.Wrappers;
using EdiFabric.Batch.Processing.Services.Services;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using NLog.Web;

namespace EdiFabric.Batch.Processing
{
    class Program
    {
        public static string EnvironmentName => Environment.GetEnvironmentVariable("ASPNETCORE_ENVIRONMENT");

        public static async Task<int> Main()
        {
            var _logger = NLogBuilder.ConfigureNLog(string.IsNullOrEmpty(EnvironmentName) ? "nlog.config" : $"nlog.{EnvironmentName}.config").GetCurrentClassLogger();

            // Create new stopwatch and begin timing.
            Stopwatch stopwatch = new Stopwatch();
            stopwatch.Start();

            try
            {
                _logger.Info("Application started.");

                ReturnCode result = ReturnCode.Failure;

                var configuration = new ConsoleHostConfigurationBuilder()
                    .Build();

                var serviceProvider = BuildServiceProvider(configuration);

                using (serviceProvider as IDisposable)
                {
                    _logger.Info("Application processing.");
                    var supervisor = serviceProvider.GetService<ISupervisor>();
                    result = await supervisor.SuperviseWork();
                }

                stopwatch.Stop();
                _logger.Info("Time elapsed: {0}", stopwatch.Elapsed);

                return (int)result;
            }
            catch (Exception ex)
            {
                // Nlog: catch setup errors.
                _logger.Error(ex, "Unhandled exception; Program main exiting.");
                throw;
            }
            finally
            {
                // Ensure to flush and stop internal timers/threads before application-exit (Avoid segmentation fault on Linux).
                NLog.LogManager.Shutdown();
                _logger.Info("Application ended.");
            }
        }

        static IServiceProvider BuildServiceProvider(IConfiguration configuration)
        {
            var services = new ServiceCollection();

            // Add scoped.
            services.AddScoped<ISupervisor, Supervisor>();
            services.AddScoped<IFileProcessor, FileProcessor>();
            services.AddScoped<IFileSystemWrapper, FileSystemWrapper>();
            services.AddScoped<IX12ReaderWrapper, X12ReaderWrapper>();
            services.AddScoped<IX12ReaderWrapperFactory, X12ReaderWrapperFactory>();

            // Add singletons.
            services.AddSingleton<IAppSettings>(configuration.GetSection(nameof(AppSettings)).Get<AppSettings>());

            return services.BuildServiceProvider();
        }
    }
}
