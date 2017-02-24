using System;

namespace LZ.ActorLite.Calc
{
    internal class Program
    {
        private static void Main(string[] args)
        {
            var counter = new Counter();
            for (var i = 0; i < 10000; i++)
            {
                counter.Post(i);
            }

            counter.Post(-1);

            Console.ReadLine();
        }
    }
}
