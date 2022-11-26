using Order.ApiService.ViewModel;
using System;
using System.Collections.Generic;

namespace Order.ApiService.Infra
{
    public interface IOrderDataAccess
    {
        List<OrderModel> GetOrders();

        void SaveOrder(OrderModel order);

        OrderModel GetOrder(Guid orderId);

        void DeleteOrder(Guid orderId);
    }
}
