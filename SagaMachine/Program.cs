using MassTransit;
using Messages.BusConfiguration;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Hosting;
using SagaMachine.DbConfiguration;
using SagaMachine.StateMachine;
using System;
using System.Reflection;
using System.Threading.Tasks;

namespace SagaMachine
{
    class Program
    {
        static async Task Main(string[] args)
        {
            Console.WriteLine("Starting Saga State Machine...");

            string ConnectionString = "Server=.;Database=OrderDb;Trusted_Connection=True;";

            var builder = new HostBuilder()
               .ConfigureServices((hostContext, services) =>
               {
                   services.AddMassTransit(cfg =>
                   {
                       // read more on https://masstransit-project.com/usage/sagas/automatonymous.html
                       var saga = cfg.AddSagaStateMachine<OrderStateMachine, OrderStateData>();

                       // read more on https://masstransit-project.com/usage/sagas/efcore.html
                       saga.EntityFrameworkRepository(r =>
                        {
                            r.ConcurrencyMode = ConcurrencyMode.Pessimistic; // or use Optimistic, which requires RowVersion
                        
                            r.AddDbContext<DbContext, OrderStateDbContext>((provider, builder) =>
                            {
                                builder.UseSqlServer(ConnectionString, m =>
                                {
                                    m.MigrationsAssembly(Assembly.GetExecutingAssembly().GetName().Name);
                                    m.MigrationsHistoryTable($"__{nameof(OrderStateDbContext)}");
                                });
                            });
                        });

                       // read more on https://masstransit-project.com/quick-starts/rabbitmq.html
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
