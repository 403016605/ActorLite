using System.Threading;

namespace LZ.ActorLite
{
    internal class Dispatcher
    {
        public static Dispatcher Instance { get; } = new Dispatcher();

        public void ReadyToExecute(IActor actor)
        {
            if (actor.Existed) return;

            var status = Interlocked.CompareExchange(ref actor.Context.Status,ActorContext.EXECUTING,ActorContext.WAITING);

            if (status == ActorContext.WAITING)
            {
                ThreadPool.QueueUserWorkItem(this.Execute, actor);
            }
        }

        private void Execute(object o)
        {
            IActor actor = (IActor)o;
            actor.Execute();

            if (actor.Existed)
            {
                Thread.VolatileWrite(ref actor.Context.Status,ActorContext.EXITED);
            }
            else
            {
                Thread.VolatileWrite(ref actor.Context.Status,ActorContext.WAITING);

                if (actor.MessageCount > 0)
                {
                    this.ReadyToExecute(actor);
                }
            }
        }
    }
}
