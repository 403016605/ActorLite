using ActorLite;

namespace CovarianceCrawling
{
    public interface IStatisticRequestHandelr
    {
        void GetCrawledCount(IPort<IStatisticResponseHandler> requester);
        void GetContent(IPort<IStatisticResponseHandler> requester, string url);
    }
}