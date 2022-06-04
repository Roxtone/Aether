using System.Collections.Generic;

namespace Aether.Internal
{
    public static class EventQueue
    {
        private static Queue<AbstractEvent> queue = new Queue<AbstractEvent>();

        public static void Queue(AbstractEvent eventData)
        {
            queue.Enqueue(eventData);
            if (queue.Count == 1)
            {
                ProcessQueue();
            }
        }

        private static void ProcessQueue()
        {
            while (queue.Count > 0)
            {
                AbstractEvent eventData = queue.Peek();
                eventData.InvokeImmediate();
                queue.Dequeue();
            }
        }
    }
}