using System;
using System.Collections.Generic;

namespace Aether.Internal
{
    public abstract class BaseEvent<E> : AbstractEvent where E : AbstractEvent
    {
        private static List<Listener> globalListeners = new List<Listener>() { new Listener() { Action = SetLast } };
        private static Dictionary<IContext, List<Listener>> contextListeners = new Dictionary<IContext, List<Listener>>();

        public static E Last { get; private set; }

        protected static void AddListenerBase(Action<E> action, IContext context, Priority priority, Func<E, bool> predicate)
        {
            List<Listener> listenersInContext = globalListeners;
            if (context != null && !contextListeners.TryGetValue(context, out listenersInContext))
            {
                listenersInContext = new List<Listener>();
                contextListeners.Add(context, listenersInContext);
            }
            listenersInContext.Add(new Listener() { Action = action, Priority = priority, Predicate = predicate });
        }

        protected static void RemoveListenerBase(Action<E> action)
        {
            globalListeners.RemoveAll(l => l.Action == action);
            foreach (KeyValuePair<IContext, List<Listener>> listenersInContext in contextListeners)
            {
                listenersInContext.Value.RemoveAll(l => l.Action == action);
            }
        }

        private static void SetLast(E e)
        {
            Last = e;
        }

        private static void InvokeBase(BaseEvent<E> e)
        {
            EventQueue.Queue(e);
            if (!(e is EventProcessed))
            {
                new EventProcessed(e).Invoke();
            }
        }

        private static void RemoveInactiveListeners(IContext context)
        {
            List<Listener> listenersInContext;
            if (context == null)
            {
                listenersInContext = globalListeners;
            }
            else
            {
                contextListeners.TryGetValue(context, out listenersInContext);
            }
            if (listenersInContext != null)
            {
                listenersInContext.RemoveAll(l => l.Action.Target != null && l.Action.Target is UnityEngine.Object o && o == null);
            }
        }

        protected IContext baseContext;

        public override void Invoke()
        {
            InvokeBase(this);
        }

        public override void InvokeImmediate()
        {
            RemoveInactiveListeners(null);
            RemoveInactiveListeners(baseContext);
            InvokeImmediate(globalListeners, Priority.Pre);
            InvokeImmediate(globalListeners, Priority.Default);
            InvokeImmediate(globalListeners, Priority.Post);
            if (baseContext != null && contextListeners.TryGetValue(baseContext, out List<Listener> listenersInContext))
            {
                InvokeImmediate(listenersInContext, Priority.Pre);
                InvokeImmediate(listenersInContext, Priority.Default);
                InvokeImmediate(listenersInContext, Priority.Post);
            }
        }

        private void InvokeImmediate(List<Listener> listenersInContext, Priority priority)
        {
            for (int i = 0; i < listenersInContext.Count; i++)
            {
                if (listenersInContext[i].Priority == priority && (listenersInContext[i].Predicate == null || listenersInContext[i].Predicate(this as E)))
                {
                    listenersInContext[i].Action.Invoke(this as E);
                }
            }
        }

        private class Listener
        {
            public Action<E> Action;
            public Priority Priority;
            public Func<E, bool> Predicate;
        }
    }
}