using System;

namespace Messages.Events.Stock
{
    public class OrderCompensateEvent
    {
        public Guid OrderId { get; set; }
    }
}
