using System;

namespace LZ.ActorLite.Calc
{
    public class Counter : Actor<int>
    {
        private int _value;

        protected override void Receive(int message)
        {
            if (message == -1)
            {
                Console.WriteLine(_value);
                Exit();
            }

            _value += message;
        }
    }
}