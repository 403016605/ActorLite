using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Sample.Crawling
{
    public interface IProcessingUnit
    {
        void Processing(string content);
    }
}
