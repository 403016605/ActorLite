using System;

namespace Bit.ActorLite.Calc
{
    class Program
    {
        static void Main(string[] args)
        {
            Bootstrap.Start();
            new Actor<AddNumberMessage>().Post(new AddNumberMessage(1));

            Console.ReadLine();

        }
    }
}
