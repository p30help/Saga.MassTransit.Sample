using System;

namespace Messages.Events
{
    public interface IOrderStartedEvent
    {
        public Guid OrderId { get; set; }
        public string ProductName { get; set; }
        public decimal Price { get; set; }
    }
}
