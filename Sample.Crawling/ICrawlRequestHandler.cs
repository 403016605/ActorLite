using ActorLite;

namespace Sample.Crawling
{
    internal interface ICrawlRequestHandler
    {
        void Crawl(IPort<ICrawlResponseHandler> collector, string url);
    }
}