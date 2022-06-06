namespace Aether.Internal
{
    public abstract class AbstractEvent
    {
        public abstract void Invoke();
        public abstract void InvokeImmediate();
    }
}