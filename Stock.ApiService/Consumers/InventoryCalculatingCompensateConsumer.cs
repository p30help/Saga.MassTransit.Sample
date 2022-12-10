using MassTransit;
using Messages.Events.Stock;
using System.Threading.Tasks;

namespace Order.ApiService.Consumers
{
    /// <summary>
    /// read more on https://masstransit-project.com/usage/consumers.html
    /// </summary>
    public class InventoryCalculatingCompensateConsumer : IConsumer<InventoryCalculatingCompensateEvent>
    {
        
        public InventoryCalculatingCompensateConsumer()
        {
        }

        public async Task Consume(ConsumeContext<InventoryCalculatingCompensateEvent> context)
        {
            // revert all of thing that happend in stock
        }
    }
}
