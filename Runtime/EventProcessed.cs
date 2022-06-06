using Aether.Internal;

namespace Aether
{
    public class EventProcessed : Event<EventProcessed>
    {
        public readonly AbstractEvent InnerEvent;

        public EventProcessed(AbstractEvent innerEvent)
        {
            InnerEvent = innerEvent;
        }
    }
}