using Order.ApiService.ViewModel;
using System;
using System.Collections.Generic;
using System.Linq;

namespace Order.ApiService.Infra
{
    public class OrderTempMemory : IOrderDataAccess
    {
        List<OrderModel> list { get; set; }

        public OrderTempMemory()
        {
            list = new List<OrderModel>();
        }

        public List<OrderModel> GetOrders()
        {
            return list;
        }
        public void SaveOrder(OrderModel order)
        {
            list.Add(order);
        }

        public void DeleteOrder(Guid orderId)
        {
            list.RemoveAll(x => x.OrderId == orderId);
        }

        public OrderModel GetOrder(Guid orderId)
        {
            return list.FirstOrDefault(x => x.OrderId == orderId);
        }

    }
}
