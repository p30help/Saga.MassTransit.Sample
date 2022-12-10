using MassTransit;
using Messages.Events;
using Messages.Events.Orders;
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
        private readonly IPublishEndpoint _publishEndpoint;
        private readonly IOrderDataAccess _orderDataAccess;

        public OrderController(
          ISendEndpointProvider sendEndpointProvider, IOrderDataAccess orderDataAccess, IPublishEndpoint publishEndpoint)
        {
            _sendEndpointProvider = sendEndpointProvider;
            _orderDataAccess = orderDataAccess;
            _publishEndpoint = publishEndpoint;
        }
        [HttpPost]
        [Route("CreateOrder")]
        public async Task<IActionResult> CreateOrderUsingStateMachineInDb([FromBody] OrderModel orderModel)
        {
            orderModel.OrderId = Guid.NewGuid();

            var startQueueUri = new Uri("queue:" + nameof(StartOrder));
            var endpoint = await _sendEndpointProvider.GetSendEndpoint(startQueueUri);

            _orderDataAccess.SaveOrder(orderModel);

            var msg = new StartOrder()
            {
                OrderId = orderModel.OrderId,
                Price = orderModel.Price,
                ProductName = orderModel.ProductName,
                Mobile = orderModel.Mobile
            };

            // read more on https://masstransit-project.com/usage/producers.html
            await endpoint.Send<StartOrder>(msg);

            return Ok("Order created");
        }

        [HttpPost]
        [Route("CreateOrderWithDirectlyCallSaga")]
        public async Task<IActionResult> CreateOrderWithDirectlyCallSaga([FromBody] OrderModel orderModel)
        {
            orderModel.OrderId = Guid.NewGuid();
            _orderDataAccess.SaveOrder(orderModel);

            // the important point is the message must be publish in quque
            // that define in SagaStateMachine as first event otherwise it won't work.
            // when publish message IOrderStartedEvent we start the saga transaction
            // read more on https://masstransit-project.com/usage/producers.html
            await _publishEndpoint.Publish<IOrderSubmittingEvent>(new
            {
                OrderId = orderModel.OrderId,
                Price = orderModel.Price,
                ProductName = orderModel.ProductName,
                Mobile = orderModel.Mobile
            });

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