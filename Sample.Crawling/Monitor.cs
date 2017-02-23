using System;
using System.Collections.Generic;
using ActorLite;

namespace Sample.Crawling
{
    public class Monitor : Actor<Monitor>, ICrawlResponseHandler, IStatisticRequestHandelr
    {
        private readonly HashSet<string> _allUrls;

        private readonly Queue<string> _readyToCrawl;

        private readonly Dictionary<string, string> _urlContent;

        public Monitor(int crawlerCount)
        {
            _allUrls = new HashSet<string>();
            _readyToCrawl = new Queue<string>();
            _urlContent = new Dictionary<string, string>();
            MaxCrawlerCount = crawlerCount;
            WorkingCrawlerCount = 0;
        }

        public int MaxCrawlerCount { get; }

        public int WorkingCrawlerCount { private set; get; }

        public void Crawl(string url)
        {
            if (_allUrls.Contains(url)) return;
            _allUrls.Add(url);

            if (WorkingCrawlerCount < MaxCrawlerCount)
            {
                WorkingCrawlerCount++;
                IPort<ICrawlRequestHandler> crawler = new Crawler();
                crawler.Post(c => c.Crawl(this, url));
            }
            else
            {
                _readyToCrawl.Enqueue(url);
            }
        }

        private void DispatchCrawlingTasks(IPort<ICrawlRequestHandler> reusableCrawler)
        {
            if (_readyToCrawl.Count <= 0)
                WorkingCrawlerCount--;

            var url = _readyToCrawl.Dequeue();
            reusableCrawler.Post(c => c.Crawl(this, url));

            while ((_readyToCrawl.Count > 0) &&
                   (WorkingCrawlerCount < MaxCrawlerCount))
            {
                var newUrl = _readyToCrawl.Dequeue();
                IPort<ICrawlRequestHandler> crawler = new Crawler();
                var a = new Action<ICrawlRequestHandler>(c => c.Crawl(this, newUrl));
                crawler.Post(a);

                WorkingCrawlerCount++;
            }
        }

        #region ICrawlResponseHandler Members

        void ICrawlResponseHandler.Succeeded(IPort<ICrawlRequestHandler> crawler, string url, string content,
            List<string> links)
        {
            _urlContent[url] = content;
            Console.WriteLine("{0} crawled, {1} link(s).", url, links.Count);

            foreach (var newUrl in links)
                if (!_allUrls.Contains(newUrl))
                {
                    _allUrls.Add(newUrl);
                    _readyToCrawl.Enqueue(newUrl);
                }

            DispatchCrawlingTasks(crawler);
        }

        void ICrawlResponseHandler.Failed(IPort<ICrawlRequestHandler> crawler, string url, Exception ex)
        {
            Console.WriteLine("{0} error occurred: {1}.", url, ex.Message);
            DispatchCrawlingTasks(crawler);
        }

        #endregion

        #region IStatisticRequestHandelr Members

        void IStatisticRequestHandelr.GetCrawledCount(IPort<IStatisticResponseHandler> requester)
        {
            requester.Post(r => r.ReplyCrawledCount(_urlContent.Count));
        }

        void IStatisticRequestHandelr.GetContent(IPort<IStatisticResponseHandler> requester, string url)
        {
            string content;
            if (!_urlContent.TryGetValue(url, out content))
                content = null;

            requester.Post(r => r.ReplyContent(url, content));
        }

        #endregion
    }
}