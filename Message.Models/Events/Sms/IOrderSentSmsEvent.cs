using System;

namespace Messages.Events.Sms
{
    public interface IOrderSentSmsEvent
    {
        public Guid OrderId { get; }
        public string Mobile { get; }
    }
}
