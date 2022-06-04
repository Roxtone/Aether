using Aether.Internal;
using System;

namespace Aether
{
    public abstract class Event<E> : BaseEvent<E> where E : AbstractEvent
    {
        public static void AddListener(Action<E> action, Priority priority = Priority.Default, Func<E, bool> predicate = null)
        {
            AddListenerBase(action, null, priority, predicate);
        }

        public static void RemoveListener(Action<E> action)
        {
            RemoveListenerBase(action);
        }
    }
}