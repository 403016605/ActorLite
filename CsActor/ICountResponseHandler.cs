using ActorLite;

namespace CsActor
{
    public interface ICountResponseHandler
    {
        void OutCurrentTotal(IPort<ICountRequestHandler> counter, int newValue);
    }
}