using Messages.Events.Stock;
using System;

namespace SagaMachine.StateMachine
{
    public class InventoryCalculatingEvent : IInventoryCalculatingEvent
    {
        private readonly OrderStateData orderSagaState;
        public InventoryCalculatingEvent(OrderStateData orderStateData)
        {
            orderSagaState = orderStateData;
        }

        public Guid OrderId => orderSagaState.OrderId;
        public decimal Price => orderSagaState.Price;
        public string ProductName => orderSagaState.ProductName;

        public string Mobile => orderSagaState.Mobile;
    }
}
