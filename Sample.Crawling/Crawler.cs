using System;
using System.Net;
using ActorLite;

namespace Sample.Crawling
{
    public class Crawler : Actor<Crawler>, ICrawlRequestHandler
    {
        protected IProcessingUnit _processingUnit;

        public Crawler(IProcessingUnit processingUnit)
        {
            _processingUnit = processingUnit;
        }

        #region ICrawlRequestHandler Members

        void ICrawlRequestHandler.Crawl(IPort<ICrawlResponseHandler> collector, string url)
        {
            var client = new WebClient();
            client.DownloadStringCompleted += (sender, e) =>
            {
                if (e.Error == null)
                {
                    var links = _processingUnit.Processing(e.Result);
                    collector.Post(c => c.Succeeded(this, url, e.Result, links));
                }
                else
                {
                    collector.Post(c => c.Failed(this, url, e.Error));
                }
            };

            client.DownloadStringAsync(new Uri(url));
        }

        #endregion
    }
}