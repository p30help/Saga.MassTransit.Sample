using MassTransit;
using Messages.Events.Stock;
using System.Threading.Tasks;

namespace Stock.ApiService.Consumers
{
    /// <summary>
    /// read more on https://masstransit-project.com/usage/consumers.html
    /// </summary>
    public class InventoryCalculatingConsumer : IConsumer<IInventoryCalculatingEvent>
    {
        public async Task Consume(ConsumeContext<IInventoryCalculatingEvent> context)
        {
            var data = context.Message;

            if (data.Price >= 0)
            {
                // send to next microservice
                await context.Publish<IInventoryCalculatedEvent>(new
                {
                    data.OrderId,
                    data.Mobile,
                    data.Price,
                    data.ProductName
                });
            }
            else
            {
                // compensate
                await context.Publish<IInventoryCalculatingFailedEvent>(new
                {
                    data.OrderId,
                });
            }

        }
    }
}
