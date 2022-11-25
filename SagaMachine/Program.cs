using MassTransit;
using Messages.BusConfiguration;
using Microsoft.Extensions.Hosting;
using SagaMachine.StateMachine;
using System.Threading.Tasks;

namespace SagaMachine
{
    class Program
    {
        static async Task Main(string[] args)
        {
            var builder = new HostBuilder()
               .ConfigureServices((hostContext, services) =>
               {
                   services.AddMassTransit(cfg =>
                   {
                       cfg.AddSagaStateMachine<OrderStateMachine, OrderStateData>()
                        .InMemoryRepository();

                       cfg.UsingRabbitMq((context, cfg) =>
                       {
                           cfg.Host(BusConstants.RabbitMqHostUri);
                           cfg.ConfigureEndpoints(context);
                       });
                   });

               });

            await builder.RunConsoleAsync();
        }
    }
}
