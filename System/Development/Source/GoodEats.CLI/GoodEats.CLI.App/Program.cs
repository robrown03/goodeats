using GoodEats.CLI.Configurations;
using GoodEats.CLI.Core;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using System;
using System.IO;
using System.Linq;
using System.Threading.Tasks;

namespace GoodEats.CLI
{
    public class Program
    {
        /// <summary>
        /// Defines the entry point of the application.
        /// </summary>
        /// <param name="args">The arguments.</param>
        public static async Task Main(string[] args)
        {
            var services = new ServiceCollection();
            ConfigureServices(services);
            var serviceProvider = services.BuildServiceProvider();
            await serviceProvider.GetService<App>().Run(args);
        }
        private static void ConfigureServices(IServiceCollection services)
        {
            var configuration = new ConfigurationBuilder()
             .SetBasePath(Directory.GetCurrentDirectory())
             .AddJsonFile("appsettings.json", false)
             .Build();

            RegisterServices(services, typeof(IOperation), ServiceLifetime.Scoped);
            RegisterServices(services, typeof(IManager), ServiceLifetime.Scoped);
            RegisterServices(services, typeof(IProvider), ServiceLifetime.Scoped);
            RegisterServices(services, typeof(IRepository), ServiceLifetime.Scoped);
            RegisterServices(services, typeof(IClientContextRepository), ServiceLifetime.Singleton);
            

            services.AddOptions();
            services.Configure<DataConfig>(configuration.GetSection("DataConfig"));
            services.Configure<CosmosConfig>(configuration.GetSection("CosmosConfig"));
            services.AddTransient<App>();
        }
        private static void RegisterServices(IServiceCollection services, Type interfaceType, ServiceLifetime lifetime)
        {
            var types = AppDomain.CurrentDomain.GetAssemblies()
                .SelectMany(s => s.GetTypes())
                .Where(x => interfaceType.IsAssignableFrom(x) && !x.IsInterface && !x.IsAbstract);

            foreach (var type in types)
            {
                var interfaces = type.GetInterfaces().Where(p => !p.IsNested).ToList();
                foreach (var i in interfaces)
                {
                    services.Add(new ServiceDescriptor(i, type, lifetime));

                }
            }
        }
    }
}
