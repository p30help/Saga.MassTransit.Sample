using System;

namespace Order.ApiService.ViewModel
{
    public class OrderModel
    {
        public Guid OrderId { get; set; }
        public string ProductName { get; set; }
        public decimal Price { get; set; }
        public string UserId { get; set; }
    }
}
