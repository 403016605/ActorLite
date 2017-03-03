using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Bit.ActorLite.Calc
{
    public class AddNumberMessage:IMessage
    {
        public AddNumberMessage(int right)
        {
            Right = right;
        }

        public int Right { get;  }
    }
}
