using System;

namespace Messages.Events.Stock
{
    public interface IInventoryCalculatingEvent
    {
        public Guid OrderId { get; }
        public string ProductName { get; }
        public decimal Price { get; }
        public string Mobile { get; }

    }
}
