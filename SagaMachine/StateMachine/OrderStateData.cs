using MassTransit;
using System;

namespace SagaMachine.StateMachine
{
    public class OrderStateData : SagaStateMachineInstance
    {
        public Guid CorrelationId { get; set; }
        public string CurrentState { get; set; }
        public DateTime? OrderCreationDateTime { get; set; }
        public DateTime? OrderCancelDateTime { get; set; }

        public Guid OrderId { get; set; }
        public decimal Price { get; set; }
        public string ProductName { get; set; }
    }
}
