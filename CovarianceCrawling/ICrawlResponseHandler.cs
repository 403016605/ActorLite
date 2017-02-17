using System;
using System.Collections.Generic;
using ActorLite;

namespace CovarianceCrawling
{
    internal interface ICrawlResponseHandler
    {
        void Succeeded(IPort<ICrawlRequestHandler> crawler, string url, string content, List<string> links);
        void Failed(IPort<ICrawlRequestHandler> crawler, string url, Exception ex);
    }
}