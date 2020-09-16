using System.Threading.Tasks;
using Microsoft.Extensions.DependencyInjection;
using MT.WebApplicationFactory.Demo.IntegrationEvents.Events;

namespace WebAppFactoryTest
{
    internal class Program
    {
        private static async Task Main(string[] args)
        {
            var appFactory = new TestWebApplicationFactory();
            appFactory.CreateClient();

            using (var scope = appFactory.Services.CreateScope())
            {
                var integrationEventService = scope.ServiceProvider.GetService<IntegrationEventService>();
                await integrationEventService.PublishAsync(new IntegrationEvent());
            }

            await Task.Delay(1_000);

            appFactory.Dispose();
            
            appFactory = new TestWebApplicationFactory();
            appFactory.CreateClient();

            using (var scope = appFactory.Services.CreateScope())
            {
                var integrationEventService = scope.ServiceProvider.GetService<IntegrationEventService>();
                await integrationEventService.PublishAsync(new IntegrationEvent());
            }

            while (true)
            {
                await Task.Delay(1_000);
            }
        }
    }
}
