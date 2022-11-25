using System;

namespace Messages.Events
{
    public class StartOrder
    {
        public Guid OrderId { get; }
        public string ProductName { get; }
        public decimal Price { get; }
    }
}
