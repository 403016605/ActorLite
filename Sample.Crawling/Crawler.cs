using System;
using System.Linq;
using System.Net;
using System.Text.RegularExpressions;
using ActorLite;

namespace Sample.Crawling
{
    public class Crawler : Actor<Crawler>, ICrawlRequestHandler
    {
        #region ICrawlRequestHandler Members

        void ICrawlRequestHandler.Crawl(IPort<ICrawlResponseHandler> collector, string url)
        {
            var client = new WebClient();
            client.DownloadStringCompleted += (sender, e) =>
            {
                if (e.Error == null)
                {
                    var matches = Regex.Matches(e.Result, @"href=""(http://[^""]+)""").Cast<Match>();
                    var links = matches.Select(m => m.Groups[1].Value).Distinct().ToList();
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