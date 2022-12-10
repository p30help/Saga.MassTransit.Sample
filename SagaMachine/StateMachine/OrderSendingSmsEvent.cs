using Messages.Events.Sms;
using SagaMachine.StateMachine;
using System;

namespace Messages.Events
{
    public class OrderSendingSmsEvent : IOrderSendingSmsEvent
    {
        private readonly OrderStateData orderSagaState;
        public OrderSendingSmsEvent(OrderStateData orderStateData)
        {
            orderSagaState = orderStateData;
        }

        public Guid OrderId => orderSagaState.OrderId;
        public string Mobile => orderSagaState.Mobile;
    }
}
