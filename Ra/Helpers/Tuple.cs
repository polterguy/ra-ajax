/*
 * Ra Ajax - An Ajax Library for Mono ++
 * Copyright 2008 - Thomas Hansen thomas@ra-ajax.org
 * This code is licensed under the LGPL version 3 which 
 * can be found in the license.txt file on disc.
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
