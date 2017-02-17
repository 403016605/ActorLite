using System;

namespace ActorLite
{
    public interface IPort<out T>
    {
        void Post(Action<T> message);
    }
}