using System.Collections.Generic;

namespace LZ.ActorLite
{
    public abstract class Actor<T> : IActor
    {
        private bool _exited = false;
        private readonly Queue<T> _messageQueue = new Queue<T>();


        protected abstract void Receive(T message);

        protected Actor()
        {
            this._context = new ActorContext(this);
        }

        private readonly ActorContext _context = null;
        ActorContext IActor.Context => this._context;

        bool IActor.Existed => this._exited;

        int IActor.MessageCount => this._messageQueue.Count;

        void IActor.Execute()
        {
            T message;
            lock (this._messageQueue)
            {
                message = this._messageQueue.Dequeue();
            }

            this.Receive(message);
        }

        

        protected void Exit()
        {
            this._exited = true;
        }

        public void Post(T message)
        {
            if (this._exited) return;

            lock (this._messageQueue)
            {
                this._messageQueue.Enqueue(message);
            }

            Dispatcher.Instance.ReadyToExecute(this);
        }
    }
}
