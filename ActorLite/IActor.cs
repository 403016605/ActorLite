namespace ActorLite
{
    /// <summary>
    /// Actor接口定义
    /// </summary>
    internal interface IActor
    {
        /// <summary>
        /// 退出状态
        /// </summary>
        bool Exited { get; }

        /// <summary>
        /// 消息数量
        /// </summary>
        int MessageCount { get; }

        /// <summary>
        /// Actor上下文
        /// </summary>
        ActorContext Context { get; }

        /// <summary>
        /// 执行
        /// </summary>
        void Execute();
    }
}