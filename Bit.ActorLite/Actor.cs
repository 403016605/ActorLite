﻿using System.Collections.Concurrent;
using Bit.ActorLite.Components;

namespace Bit.ActorLite
{
    public class Actor<T> : IActor
        where T:IMessage
    {
        
        private readonly ActorContext _context;
        private readonly ConcurrentQueue<T> _messageQueue = new ConcurrentQueue<T>();
        private bool _exited;

        public Actor()
        {
            _context = new ActorContext(this);
        }

        #region IActor

        ActorContext IActor.Context => _context;

        bool IActor.Exited => _exited;

        int IActor.MessageCount => _messageQueue.Count;

        void IActor.Execute()
        {
            T message;
            if (_messageQueue.TryDequeue(out message))
            {
                Receive(message);
            }

            
        }

        #endregion

        protected void Exit()
        {
            _exited = true;
        }

        public void Post(T message)
        {
            if (_exited) return;

            _messageQueue.Enqueue(message);

            Dispatcher.Instance.ReadyToExecute(this);
        }

        protected void Receive(T message)
        {
            var bus =ObjectContainer.Resolve<IBus>();
            bus.publish(message);
        }


    }
}
