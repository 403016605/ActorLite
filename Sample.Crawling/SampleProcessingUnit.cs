using System.Collections.Generic;
using System.Linq;
using System.Text.RegularExpressions;

namespace Sample.Crawling
{
    public class SampleProcessingUnit : IProcessingUnit
    {
        public List<string> Processing(string content)
        {
            var matches = Regex.Matches(content, @"href=""(http://[^""]+)""").Cast<Match>();
            return matches.Select(m => m.Groups[1].Value).Distinct().ToList();
        }
    }
}