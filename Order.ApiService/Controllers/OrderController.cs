using MassTransit;
using Messages.BusConfiguration;
using Messages.Events;
using Microsoft.AspNetCore.Mvc;
using Order.ApiService.Infra;
using Order.ApiService.ViewModel;
using System;
using System.Threading.Tasks;

namespace Order.ApiService.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class OrderController : ControllerBase
    {
        private readonly ISendEndpointProvider _sendEndpointProvider;
        private readonly IOrderDataAccess _orderDataAccess;

        public OrderController(
          ISendEndpointProvider sendEndpointProvider, IOrderDataAccess orderDataAccess)
        {
            _sendEndpointProvider = sendEndpointProvider;
            _orderDataAccess = orderDataAccess;
        }

        [HttpPost]
        [Route("CreateOrder")]
        public async Task<IActionResult> CreateOrderUsingStateMachineInDb([FromBody] OrderModel orderModel)
        {
            orderModel.OrderId = Guid.NewGuid();

            // the important point is the message must be publish in quque
            // that define in SagaStateMachine as first event otherwise it won't work.
            var startQueueUri = new Uri("queue:" + BusConstants.OrderSagaQueue);
            var endpoint = await _sendEndpointProvider.GetSendEndpoint(startQueueUri);

            _orderDataAccess.SaveOrder(orderModel);

            var msg = new
            {
                orderModel.OrderId,
                orderModel.Price,
                orderModel.ProductName
            };
            await endpoint.Send<StartOrder>(msg);

            return Ok("Order created");
        }

        [HttpGet]
        [Route("GetOrders")]
        public async Task<IActionResult> GetOrders()
        {
            var list = _orderDataAccess.GetOrders();

            return Ok(list);
        }
    }
}