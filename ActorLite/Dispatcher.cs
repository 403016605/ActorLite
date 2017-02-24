using System.Threading;

namespace ActorLite
{
    /// <summary>
    /// Actor分发器
    /// </summary>
    internal class Dispatcher
    {
        public static Dispatcher Instance { get; } = new Dispatcher();

        /// <summary>
        /// 准备执行
        /// </summary>
        /// <param name="actor"></param>
        public void ReadyToExecute(IActor actor)
        {
            //actor状态为退出，则返回。
            if (actor.Exited) return;

            var status = Interlocked.CompareExchange(ref actor.Context.Status, ActorContext.EXECUTING,ActorContext.WAITING);

            if (status == ActorContext.WAITING)
            {
                ThreadPool.QueueUserWorkItem(Execute, actor);
            }
        }

        private void Execute(object o)
        {
            var actor = (IActor) o;
            actor.Execute();

            if (actor.Exited)
            {
                Thread.VolatileWrite(ref actor.Context.Status, ActorContext.EXITED);
            }
            else
            {
                Thread.VolatileWrite(ref actor.Context.Status, ActorContext.WAITING);

                if (actor.MessageCount > 0)
                    ReadyToExecute(actor);
            }
        }
    }
}