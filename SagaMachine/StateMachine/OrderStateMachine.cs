using MassTransit;
using Messages.Events;
using Messages.Events.Orders;
using Messages.Events.Sms;
using Messages.Events.Stock;
using System;

namespace SagaMachine.StateMachine
{
    /// <summary>
    /// read more
    /// https://masstransit-project.com/usage/sagas/automatonymous.html
    /// </summary>
    public class OrderStateMachine : MassTransitStateMachine<OrderStateData>
    {
        public State InventoryCalculating { get; private set; }
        public State InventoryCalculatingFailed { get; private set; }

        public State SmsSending { get; private set; }
        public State SmsFailed { get; private set; }

        public State OrderFinished { get; private set; }


        public Event<IOrderSubmittingEvent> IOrderSubmittingEvent { get; private set; }
        public Event<IInventoryCalculatedEvent> InventoryCalculatedEvent { get; private set; }
        public Event<IInventoryCalculatingFailedEvent> InventoryCalculatingFailedEvent { get; private set; }
        public Event<IOrderSentSmsEvent> OrderSentSmsEvent { get; private set; }
        public Event<IOrderSendingSmsFailedEvent> OrderSendingSmsFailedEvent { get; private set; }


        public OrderStateMachine()
        {
            Event(() => IOrderSubmittingEvent, x => x.CorrelateById(m => m.Message.OrderId));
            Event(() => InventoryCalculatedEvent, x => x.CorrelateById(m => m.Message.OrderId));
            Event(() => InventoryCalculatingFailedEvent, x => x.CorrelateById(m => m.Message.OrderId));
            Event(() => OrderSentSmsEvent, x => x.CorrelateById(m => m.Message.OrderId));
            Event(() => OrderSendingSmsFailedEvent, x => x.CorrelateById(m => m.Message.OrderId));

            InstanceState(x => x.CurrentState);

            // first step - submitting order 
            Initially(
               When(IOrderSubmittingEvent)
                .Then(context =>
                {
                    context.Saga.OrderCreationDateTime = DateTime.Now;
                    context.Saga.OrderId = context.Message.OrderId;
                    context.Saga.Price = context.Message.Price;
                    context.Saga.ProductName = context.Message.ProductName;
                    context.Saga.Mobile = context.Message.Mobile;
                })

               // publish next step event
               .TransitionTo(InventoryCalculating)
               .Publish(context => new InventoryCalculatingEvent(context.Saga))
               );

            // hanlidng next step
            During(InventoryCalculating,
                  When(InventoryCalculatedEvent)

                   // publish next step event
                   .TransitionTo(SmsSending)
                   .Publish(context => new OrderSendingSmsEvent(context.Saga))
                  );

            // handling error
            During(InventoryCalculating,
                 When(InventoryCalculatingFailedEvent)
                      .Then(context => context.Saga.OrderCancelDateTime = DateTime.Now)
                      .TransitionTo(InventoryCalculatingFailed)
                      .Publish(context => new OrderCompensateEvent()
                      {
                          OrderId = context.Message.OrderId,
                      })
                 );

            // hanlidng next step
            During(SmsSending,
                  When(OrderSentSmsEvent)
                   .TransitionTo(OrderFinished)
                  );

            // handling error
            During(SmsSending,
                When(OrderSendingSmsFailedEvent)
                    .Then(context => context.Saga.OrderCancelDateTime = DateTime.Now)
                    .TransitionTo(SmsFailed)
                    .Publish(context => new InventoryCalculatingCompensateEvent()
                    {
                        OrderId = context.Message.OrderId,
                    })
                    .Publish(context => new OrderCompensateEvent()
                    {
                        OrderId = context.Message.OrderId,
                    })
                );
        }
    }
}
