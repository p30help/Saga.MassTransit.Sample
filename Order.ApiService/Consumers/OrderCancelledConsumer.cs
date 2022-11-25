using MassTransit;
using Messages.Events;
using Order.ApiService.Infra;
using System.Threading.Tasks;

namespace Order.ApiService.Consumers
{
    public class OrderCancelledConsumer : IConsumer<IOrderCancelEvent>
    {
        private readonly IOrderDataAccess _orderDataAccess;

        public OrderCancelledConsumer(IOrderDataAccess orderDataAccess)
        {
            _orderDataAccess = orderDataAccess;
        }

        public async Task Consume(ConsumeContext<IOrderCancelEvent> context)
        {
            var data = context.Message;
            _orderDataAccess.DeleteOrder(data.OrderId);
        }
    }
}
