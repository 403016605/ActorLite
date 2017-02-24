namespace ActorLite
{
    /// <summary>
    /// actor上下文
    /// </summary>
    internal class ActorContext
    {
        /// <summary>
        /// 等待（Waiting）：邮箱为空，或刚执行完一个消息，正等待分配任务
        /// </summary>
        public const int WAITING = 0;

        /// <summary>
        /// 执行（Executing）：正在执行一个消息（确切地说，由于线程池的缘故，它也可能是还在队列中等待，不过从概念上理解，我们认为它“已经”执行了）
        /// </summary>
        public const int EXECUTING = 1;

        /// <summary>
        /// 退出（Exited）：已经退出，不会再执行任何消息
        /// </summary>
        public const int EXITED = 2;

        /// <summary>
        /// actor状态
        /// </summary>
        public int Status;

        public ActorContext(IActor actor)
        {
            Actor = actor;
        }

        /// <summary>
        /// Actor实例
        /// </summary>
        public IActor Actor { get; private set; }
    }
}