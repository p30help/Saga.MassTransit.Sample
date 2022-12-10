using System;

namespace Messages.Events.Stock
{
    public interface IInventoryCalculatingFailedEvent
    {
        public Guid OrderId { get; }
    }
}
