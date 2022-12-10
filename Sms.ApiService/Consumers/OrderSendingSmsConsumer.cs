using MassTransit;
using Messages.Events.Sms;
using System.Threading.Tasks;

namespace Sms.ApiService.Consumers
{
    /// <summary>
    /// read more on https://masstransit-project.com/usage/consumers.html
    /// </summary>
    public class OrderSendingSmsConsumer : IConsumer<IOrderSendingSmsEvent>
    {
        public async Task Consume(ConsumeContext<IOrderSendingSmsEvent> context)
        {
            var data = context.Message;

            if(string.IsNullOrWhiteSpace(data.Mobile) == false)
            {
                //try to sending sms

                // them publish success event
                await context.Publish<IOrderSentSmsEvent>(new
                {
                    OrderId = data.OrderId,
                    Mobile = data.Mobile
                });

            }
            else
            {
                // revert
                await context.Publish<IOrderSendingSmsFailedEvent>(new 
                {
                    OrderId = data.OrderId
                });
            }
        }
    }
}
