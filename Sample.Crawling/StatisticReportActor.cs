using System;
using ActorLite;

namespace Sample.Crawling
{
    public class StatisticReportActor : Actor<StatisticReportActor>, IStatisticResponseHandler
    {
        private readonly IPort<IStatisticRequestHandelr> _statisticPort;

        public StatisticReportActor(IPort<IStatisticRequestHandelr> statisticPort)
        {
            _statisticPort = statisticPort;
        }

        void IStatisticResponseHandler.ReplyCrawledCount(int count)
        {
            Console.WriteLine("Crawled: {0}", count);
        }

        void IStatisticResponseHandler.ReplyContent(string url, string content)
        {
            Console.WriteLine("URL: {0}", url);
            Console.WriteLine("content: {0}", content);
        }

        public void Start()
        {
            while (true)
            {
                Console.ReadLine();
                _statisticPort.Post(s => s.GetCrawledCount(this));
                _statisticPort.Post(s => s.GetContent(this, @"http://home.cnblogs.com/q/"));
            }
        }
    }
}