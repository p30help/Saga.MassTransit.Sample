using System;

namespace Messages.Events.Stock
{
    public interface IInventoryCalculatedEvent
    {
        public Guid OrderId { get; }
        public string ProductName { get; }
        public decimal Price { get; }
        public string Mobile { get; }

    }
}
