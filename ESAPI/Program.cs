using Autofac.Extensions.DependencyInjection;
using CelloMicros.ConfigurationProvider;
using ESAPI;
using Microsoft.AspNetCore.Hosting;

public static class Program
{
    public static void Main(string[] args)
    {
        var host = Host.CreateDefaultBuilder(args)
            .UseServiceProviderFactory(new AutofacServiceProviderFactory())
            .ConfigureWebHostDefaults(webHostBuilder => {
                webHostBuilder
            .UseIISIntegration()
            .UseStartup<Startup>()
            .ConfigureAppConfigurations();
            })
            .Build();

        host.Run();
    }
}