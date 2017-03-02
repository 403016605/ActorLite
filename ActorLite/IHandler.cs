namespace ActorLite
{
    /// <summary>
    /// 消息处理器
    /// </summary>
    /// <typeparam name="T"></typeparam>
    public interface IHandler<in T>
        where T : IMessage
    {
        void Handler(T message);
    }
}