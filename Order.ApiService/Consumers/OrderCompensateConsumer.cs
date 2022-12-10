using MassTransit;
using Messages.Events.Stock;
using Order.ApiService.Infra;
using System.Threading.Tasks;

namespace Order.ApiService.Consumers
{
    /// <summary>
    /// read more on https://masstransit-project.com/usage/consumers.html
    /// </summary>
    public class OrderCompensateConsumer : IConsumer<OrderCompensateEvent>
    {
        private readonly IOrderDataAccess _orderDataAccess;

        public OrderCompensateConsumer(IOrderDataAccess orderDataAccess)
        {
            _orderDataAccess = orderDataAccess;
        }

        public async Task Consume(ConsumeContext<OrderCompensateEvent> context)
        {
            var data = context.Message;
            _orderDataAccess.DeleteOrder(data.OrderId);
        }
    }
}
