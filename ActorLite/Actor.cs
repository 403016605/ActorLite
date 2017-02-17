using System;
using System.Collections.Generic;

namespace ActorLite
{
    public abstract class Actor<T> : IActor, IPort<T>
        where T : Actor<T>
    {
        private readonly ActorContext m_context;
        private readonly Queue<Action<T>> m_messageQueue = new Queue<Action<T>>();

        private bool m_exited;

        protected Actor()
        {
            m_context = new ActorContext(this);
        }

        ActorContext IActor.Context
        {
            get { return m_context; }
        }

        bool IActor.Exited
        {
            get { return m_exited; }
        }

        int IActor.MessageCount
        {
            get { return m_messageQueue.Count; }
        }

        void IActor.Execute()
        {
            Action<T> message;
            lock (m_messageQueue)
            {
                message = m_messageQueue.Dequeue();
            }

            Receive(message);
        }

        public void Post(Action<T> message)
        {
            if (m_exited) return;

            lock (m_messageQueue)
            {
                m_messageQueue.Enqueue(message);
            }

            Dispatcher.Instance.ReadyToExecute(this);

            //message.DynamicInvoke(this);
        }

        protected virtual void Receive(Action<T> message)
        {
            message.DynamicInvoke(this);
        }

        protected void Exit()
        {
            m_exited = true;
        }
    }
}