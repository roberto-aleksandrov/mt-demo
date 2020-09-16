using MassTransit;
using MassTransit.Definition;
using Microsoft.AspNetCore.Builder;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Hosting;
using MT.WebApplicationFactory.Demo.Configurations;
using MT.WebApplicationFactory.Demo.IntegrationEvents.Handlers;

namespace MT.WebApplicationFactory.Demo
{
    public class Startup
    {
        public IConfiguration Configuration { get; private set; }

        public Startup(IConfiguration configuration)
        {
            Configuration = configuration;
        }

        public void ConfigureServices(IServiceCollection services)
        {
            var config = Configuration.GetSection("MessageBus").Get<MessageBusConfig>();

            services.AddMassTransit(serviceCfg =>
            {
                serviceCfg.UsingRabbitMq((registrationContext, busFactoryCfg) =>
                {
                    busFactoryCfg.Host(config.Host, "/", hostCfg =>
                    {
                        hostCfg.Username(config.Username);
                        hostCfg.Password(config.Password);
                    });

                    busFactoryCfg.ConfigureEndpoints(registrationContext, new KebabCaseEndpointNameFormatter(true));
                });

                serviceCfg.AddConsumersFromNamespaceContaining<IntegrationEventHandler>();
            });

            services.AddMassTransitHostedService();
        }

        public void Configure(IApplicationBuilder app, IHostEnvironment env)
        {
            if (env.IsDevelopment())
            {
                app.UseDeveloperExceptionPage();
            }
        }
    }
}
