namespace CovarianceCrawling
{
    public interface IStatisticResponseHandler
    {
        void ReplyCrawledCount(int count);
        void ReplyContent(string url, string content);
    }
}