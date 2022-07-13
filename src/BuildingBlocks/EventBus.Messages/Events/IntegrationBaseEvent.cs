using System;

namespace EventBus.Messages.Events
{
    public class IntegrationBaseEvent
    {
        public IntegrationBaseEvent()
        {
            Id = Guid.NewGuid();
            CreatedOn = DateTime.UtcNow;
        }
        public IntegrationBaseEvent(Guid id,DateTime createdOn)
        {
            Id = id;
            CreatedOn = createdOn;
        }
         
        public Guid Id { get;private set; }
        public DateTime CreatedOn { get; set; }
    }
}
