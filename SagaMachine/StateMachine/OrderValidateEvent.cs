using Messages.Events;
using System;

namespace SagaMachine.StateMachine
{
    public class OrderValidateEvent : IOrderValidateEvent
    {
        private readonly OrderStateData orderSagaState;
        public OrderValidateEvent(OrderStateData orderStateData)
        {
            orderSagaState = orderStateData;
        }

        public Guid OrderId => orderSagaState.OrderId;
        public decimal Price => orderSagaState.Price;
        public string ProductName => orderSagaState.ProductName;
    }
}
