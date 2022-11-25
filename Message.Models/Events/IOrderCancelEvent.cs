using System;

namespace Messages.Events
{
    public interface IOrderCancelEvent
    {
        public Guid OrderId { get; }
        public string ProductName { get; }
        public decimal Price { get; }
    }
}
