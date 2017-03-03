using System;

namespace Bit.ActorLite
{
    public interface IBus
    {
        void Register<T>(Action<T> handler) where T : IMessage;

        void publish<T>(T message) where T : IMessage;
    }
}