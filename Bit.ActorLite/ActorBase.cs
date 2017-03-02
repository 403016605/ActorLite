using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Bit.ActorLite
{
    

    public class ActorBase : Actor<IMessage>
    {
        protected override void Receive(IMessage message)
        {
            throw new NotImplementedException();
        }
    }
}
