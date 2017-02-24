using System.Collections.Generic;

namespace Sample.Crawling
{
    public interface IProcessingUnit
    {
        List<string> Processing(string content);
    }
}