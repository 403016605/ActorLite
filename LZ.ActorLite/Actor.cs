using System.Collections.Generic;

namespace LZ.ActorLite
{
    public abstract class Actor<T> : IActor
    {
        private bool _exited;

        private readonly Queue<T> _messageQueue = new Queue<T>();


        protected abstract void Receive(T message);

        protected Actor()
        {
            _context = new ActorContext(this);
        }

        private readonly ActorContext _context;

        ActorContext IActor.Context => _context;

        bool IActor.Exited => _exited;

        int IActor.MessageCount => _messageQueue.Count;

        void IActor.Execute()
        {
            T message;
            lock (_messageQueue)
            {
                message = _messageQueue.Dequeue();
            }

            Receive(message);
        }

        

        protected void Exit()
        {
            _exited = true;
        }

        public void Post(T message)
        {
            if (_exited) return;

            lock (_messageQueue)
            {
                _messageQueue.Enqueue(message);
            }

            Dispatcher.Instance.ReadyToExecute(this);
        }
    }
}
