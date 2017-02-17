using ActorLite;

namespace Sample.Calc
{
    public class Counter : Actor<Counter>, ICountRequestHandler
    {
        void ICountRequestHandler.Count(IPort<ICountResponseHandler> collector, int baseValue, int value)
        {
            collector.Post(c => c.OutCurrentTotal(this, baseValue + value));
        }
    }
}