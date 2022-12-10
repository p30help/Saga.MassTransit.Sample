using System;

namespace Messages.Events.Sms
{
    public interface IOrderSendingSmsFailedEvent
    {
        public Guid OrderId { get; }
    }
}