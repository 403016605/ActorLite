namespace Bit.ActorLite
{
    public interface IHandler<in T>
        where T : IMessage
    {
        void Handle(T message);
    }
}