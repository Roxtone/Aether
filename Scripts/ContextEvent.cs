using Aether.Internal;
using System;

namespace Aether
{
    public abstract class ContextEvent<E, C> : BaseEvent<E> where E : AbstractEvent where C : class, IContext
    {
        public static readonly C GlobalContext = null;

        public static void AddListener(Action<E> action, C context, Priority priority = Priority.Default, Func<E, bool> predicate = null)
        {
            AddListenerBase(action, context, priority, predicate);
        }

        public static void RemoveListener(Action<E> action)
        {
            RemoveListenerBase(action);
        }

        public readonly C Context;

        public ContextEvent(C context)
        {
            baseContext = context;
            Context = context;
        }
    }
}