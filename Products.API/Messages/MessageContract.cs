using System;

namespace Products.API.Messages
{
    public class MessageContract
    {
        public MessageContract()
        {
            EventId = Guid.NewGuid();
            EventCreationDate = DateTime.Now;
        }

        public Guid EventId { get; set; }

        public DateTime EventCreationDate { get; set; }
    }
}