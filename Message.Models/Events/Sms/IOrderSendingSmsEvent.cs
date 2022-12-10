using System;

namespace Messages.Events.Sms
{
    public interface IOrderSendingSmsEvent
    {
        public Guid OrderId { get; }
        public string Mobile { get; }
    }
}
