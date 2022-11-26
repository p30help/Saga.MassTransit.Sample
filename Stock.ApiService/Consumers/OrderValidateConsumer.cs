using MassTransit;
using Messages.Events;
using System.Threading.Tasks;

namespace Stock.ApiService.Consumers
{
    /// <summary>
    /// read more on https://masstransit-project.com/usage/consumers.html
    /// </summary>
    public class OrderValidateConsumer : IConsumer<IOrderValidateEvent>
    {
        public async Task Consume(ConsumeContext<IOrderValidateEvent> context)
        {
            var data = context.Message;

            if (data.Price < 0)
            {
                await context.Publish<IOrderCancelEvent>(new
                {
                    context.Message.OrderId,
                    context.Message.Price
                });
            }
            else
            {
                // send to next microservice
            }

        }
    }
}
