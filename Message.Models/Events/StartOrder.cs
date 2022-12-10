using System;

namespace Messages.Events
{
    public class StartOrder
    {
        public Guid OrderId { get; set; }
        public string ProductName { get; set; }
        public decimal Price { get; set; }
        public string Mobile { get; set; }
    }
}
