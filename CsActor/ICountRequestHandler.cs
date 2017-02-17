using ActorLite;

namespace CsActor
{
    public interface ICountRequestHandler
    {
        void Count(IPort<ICountResponseHandler> collector, int baseValue, int value);
    }
}