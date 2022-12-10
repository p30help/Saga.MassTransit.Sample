using System;

namespace Messages.Events.Orders
{
    public interface IOrderSubmittingEvent
    {
        public Guid OrderId { get; set; }
        public string ProductName { get; set; }
        public decimal Price { get; set; }
        public string Mobile { get; set; }
    }
}
