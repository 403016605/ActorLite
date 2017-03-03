using System;

namespace Bit.ActorLite.Calc
{
    public class AddNumberHandler : IHandler<AddNumberMessage>
    {
        public void Handle(AddNumberMessage message)
        {
            Console.WriteLine(message.Right);
        }
    }
}