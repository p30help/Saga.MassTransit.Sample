using System;

namespace Messages.Events.Stock
{
    public class InventoryCalculatingCompensateEvent
    {
        public Guid OrderId { get; set; }
    }
}
