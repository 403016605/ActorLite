using System;
using ActorLite;

namespace CsActor
{
    public class Calculator : Actor<Calculator>, ICountResponseHandler
    {
        private IPort<ICountRequestHandler> _counter;
        private int _currentValue;
        private int _index;
        private int _maxValue;

        void ICountResponseHandler.OutCurrentTotal(IPort<ICountRequestHandler> counter, int newValue)
        {
            _currentValue = newValue;
            _index++;
            if (_index <= _maxValue)
            {
                counter.Post(ct => ct.Count(this, _currentValue, _index));
            }
            else
            {
                Console.WriteLine("CurrentTotal: {0}", newValue);
            }
        }

        public void StartCalculate(int maxValue)
        {
            this._maxValue = maxValue;
            _counter = new Counter();
            _counter.Post(ct => ct.Count(this, _currentValue, _index));
        }
    }
}