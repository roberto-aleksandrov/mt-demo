﻿using System.Threading.Tasks;
using MassTransit;

namespace MT.WebApplicationFactory.Demo.IntegrationEvents.Services
{
    public class IntegrationEventService
    {
        private readonly IBus _bus;

        public IntegrationEventService(IBus bus)
        {
            _bus = bus;
        }

        public Task PublishAsync<T>(T @event)
        {
            return _bus.Publish(@event);
        }
    }
}
