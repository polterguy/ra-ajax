/*
 * Ra Ajax - An Ajax Library for Mono ++
 * Copyright 2008 - Thomas Hansen polterguy@gmail.com
 * This code is licensed under an MIT(ish) kind of license which 
 * can be found in the license.txt file on disc in addition to that 
 * the code also is licensed under a pure GPL license for those that
 * cannot for some reasons obey by rules in the MIT(ish) kind of license.
 * 
 */

using System;

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
