using ActorLite;

namespace Sample.Calc
{
    public interface ICountRequestHandler
    {
        void Count(IPort<ICountResponseHandler> collector, int baseValue, int value);
    }
}