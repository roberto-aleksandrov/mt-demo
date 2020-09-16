using System.Threading.Tasks;
using MassTransit;
using MT.WebApplicationFactory.Demo.IntegrationEvents.Events;

namespace MT.WebApplicationFactory.Demo.IntegrationEvents.Handlers
{
    public class IntegrationEventHandler : IConsumer<IntegrationEvent>
    {
        public Task Consume(ConsumeContext<IntegrationEvent> context)
        {
            return Task.CompletedTask;
        }
    }
}
