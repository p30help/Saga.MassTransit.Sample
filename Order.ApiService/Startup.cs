using MassTransit;
using Messages.BusConfiguration;
using Microsoft.AspNetCore.Builder;
using Microsoft.AspNetCore.Hosting;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Hosting;
using Order.ApiService.Consumers;
using Order.ApiService.Infra;

namespace Order.ApiService
{
    public class Startup
    {
        public Startup(IConfiguration configuration)
        {
            Configuration = configuration;
        }

        public IConfiguration Configuration { get; }

        public void ConfigureServices(IServiceCollection services)
        {
            services.AddControllers();

            //services.TryAddSingleton(KebabCaseEndpointNameFormatter.Instance);
            services.AddMassTransit(cfg =>
            {
                //cfg.AddRequestClient<IStartOrder>();

                cfg.AddConsumer<StartOrderConsumer>();
                cfg.AddConsumer<OrderCancelledConsumer>();

                // read more on https://masstransit-project.com/quick-starts/rabbitmq.html
                cfg.UsingRabbitMq((context, cfg) =>
                {
                    cfg.Host(BusConstants.RabbitMqHostUri);
                    cfg.ConfigureEndpoints(context);
                });

            });

            services.AddSingleton<IOrderDataAccess, OrderTempMemory>();

            services.AddSwaggerGen();
        }

        public void Configure(IApplicationBuilder app, IWebHostEnvironment env)
        {
            if (env.IsDevelopment())
            {
                app.UseDeveloperExceptionPage();

                app.UseSwagger();
                app.UseSwaggerUI();
            }

            app.UseHttpsRedirection();

            app.UseRouting();

            app.UseAuthorization();

            app.UseEndpoints(endpoints =>
            {
                endpoints.MapControllers();
            });
        }
    }
}
