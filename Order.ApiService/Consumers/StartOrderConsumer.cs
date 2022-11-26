using MassTransit;
using Messages.Events;
using Microsoft.Extensions.Logging;
using System.Threading.Tasks;

namespace Order.ApiService.Consumers
{
    /// <summary>
    /// read more on https://masstransit-project.com/usage/consumers.html
    /// </summary>
    public class StartOrderConsumer : IConsumer<StartOrder>
    {
        readonly ILogger<StartOrderConsumer> _logger;
        public StartOrderConsumer()
        {
        }

        public StartOrderConsumer(ILogger<StartOrderConsumer> logger)
        {
            _logger = logger;
        }
        public async Task Consume(ConsumeContext<StartOrder> context)
        {
            // do some tasks
            _logger.LogInformation("Order Transation Started and event published: {OrderId}", context.Message.OrderId);

            await context.Publish<IOrderStartedEvent>(new
            {
                context.Message.OrderId,
                context.Message.Price,
                context.Message.ProductName
            });

        }
    }
}
