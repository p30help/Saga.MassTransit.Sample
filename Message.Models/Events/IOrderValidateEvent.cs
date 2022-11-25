using System;

namespace Messages.Events
{
    public interface IOrderValidateEvent
    {
        public Guid OrderId { get; }
        public string ProductName { get; }
        public decimal Price { get; }
    }
}
