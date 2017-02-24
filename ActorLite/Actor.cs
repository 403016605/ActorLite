using System;
using System.Collections.Generic;

namespace ActorLite
{
    public abstract class Actor<T> : IActor, IPort<T>
        where T : Actor<T>
    {
        private readonly ActorContext _context;
        private readonly Queue<Action<T>> _messageQueue = new Queue<Action<T>>();
        private bool _exited;

        protected Actor()
        {
            _context = new ActorContext(this);
        }

        #region IActor

        ActorContext IActor.Context => _context;

        bool IActor.Exited => _exited;

        int IActor.MessageCount => _messageQueue.Count;

        void IActor.Execute()
        {
            Action<T> message;
            lock (_messageQueue)
            {
                message = _messageQueue.Dequeue();
            }

            Receive(message);
        }

        #endregion

        protected void Exit()
        {
            _exited = true;
        }

        #region IPort

        public void Post(Action<T> message)
        {
            if (_exited) return;

            lock (_messageQueue)
            {
                _messageQueue.Enqueue(message);
            }

            Dispatcher.Instance.ReadyToExecute(this);
        }

        #endregion

        protected virtual void Receive(Action<T> message)
        {
            message.DynamicInvoke(this);
        }

        
    }
}