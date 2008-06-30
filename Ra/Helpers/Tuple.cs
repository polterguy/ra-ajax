using System;
using System.Collections.Generic;
using System.Text;
using System.Runtime.Serialization;

namespace Ra.Helpers
{
    public class Tuple<TFirst, TSecond>
    {
        private TFirst _first;
        private TSecond _second;

        public Tuple()
        {
        }

        public Tuple(TFirst first, TSecond second)
        {
            _first = first;
            _second = second;
        }

        public TSecond Second
        {
            get { return _second; }
            set { _second = value; }
        }

        public TFirst First
        {
            get { return _first; }
            set { _first = value; }
        }
    }
}
