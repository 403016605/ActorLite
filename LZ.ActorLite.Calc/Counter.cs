using System;

namespace LZ.ActorLite.Calc
{
    public class Counter : Actor<int>
    {
        private int _value = 0;

        protected override void Receive(int message)
        {
            if (message == -1)
            {
                Console.WriteLine(this._value);
                this.Exit();
            }

            this._value += message;
        }
    }
}