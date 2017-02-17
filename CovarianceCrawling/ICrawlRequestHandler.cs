using ActorLite;

namespace CovarianceCrawling
{
    internal interface ICrawlRequestHandler
    {
        void Crawl(IPort<ICrawlResponseHandler> collector, string url);
    }
}