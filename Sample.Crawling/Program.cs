namespace Sample.Crawling
{
    internal static class Program
    {
        private static void Main(string[] args)
        {
            var monitor = new Monitor(50);
            monitor.Post(m => m.Crawl("http://www.cnblogs.com/"));
            new StatisticReportActor(monitor).Start();
        }
    }
}