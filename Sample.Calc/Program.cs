using System;

namespace Sample.Calc
{
    internal class Program
    {
        private static void Main(string[] args)
        {
            var calc = new Calculator();
            calc.Post(c => c.StartCalculate(10000));
            Console.ReadLine();
        }
    }
}