using Microsoft.Azure.Functions.Worker;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Hosting;
using myCustomResource.services;

namespace myCustomResource
{
  public class Program
  {
    public static void Main()
    {
      var host = new HostBuilder()
        .ConfigureFunctionsWebApplication()
        .ConfigureServices(services =>
        {
          services.AddApplicationInsightsTelemetryWorkerService();
          services.ConfigureFunctionsApplicationInsights();
          services.AddSingleton(new AzureManagement());
        })
        .Build();

      host.Run();
    }
  }
}