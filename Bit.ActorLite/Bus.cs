using System;
using System.Collections.Concurrent;
using System.Collections.Generic;

namespace Bit.ActorLite
{
    public class Bus : IBus
    {
        private readonly ConcurrentDictionary<Type, List<Action<IMessage>>> _route = new ConcurrentDictionary<Type, List<Action<IMessage>>>();

        public void publish<T>(T message) where T : IMessage
        {
            List<Action<IMessage>> handlers;

            if (!_route.TryGetValue(typeof(T), out handlers)) return;

            foreach (var handler in handlers)
            {
                handler(message);
            }
        }

        public void Register<T>(Action<T> handler) where T : IMessage
        {
            List<Action<IMessage>> handlers;

            if (!_route.TryGetValue(typeof(T), out handlers))
            {
                handlers = new List<Action<IMessage>>();
                _route.TryAdd(typeof(T), handlers);
            }

            handlers.Add(x => handler((T)x));
        }
    }
}
