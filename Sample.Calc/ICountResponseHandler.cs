using ActorLite;

namespace Sample.Calc
{
    public interface ICountResponseHandler
    {
        void OutCurrentTotal(IPort<ICountRequestHandler> counter, int newValue);
    }
}