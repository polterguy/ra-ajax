/*
 * Ra-Ajax - A Managed Ajax Library for ASP.NET and Mono
 * Copyright 2008 - 2009 - Thomas Hansen thomas@ra-ajax.org
 * This code is licensed under the GPL version 3 which 
 * can be found in the license.txt file on disc.
 * 
 */

using System;

namespace Ra.Builder
{
    /**
     * Embed an instance of this object inside a using statement and you'll get deterministic
     * execution of contents inside your two delegates.
     */
    public class DeterministicExecutor : IDisposable
    {
        /**
         * method type that will be called in CTOR and DTOR of object
         */
        public delegate void Functor();

        private Functor _end;
        private Functor _start;
        private bool disposed;

        public DeterministicExecutor(Functor end)
            : this(null, end)
        { }

        /**
         * CTOR taking a starter and an ender. The starter will be called when the object is contructed
         * while the ender will be automatically called when the object is being disposed.
         */
        public DeterministicExecutor(Functor start, Functor end)
        {
            if (end == null)
                throw new NullReferenceException("No point in using a DeterminsticExecutor unless you supply both a start delegate and an end delegate");
            Start = start;
            _end = end;
        }

        public Functor Start
        {
            set
            {
                if (_start != null)
                    throw new ArgumentException("Can't set Start property twice on DeterministicExecutor");
                _start = value;
                if (_start != null)
                    _start();
            }
        }

        public void Dispose()
        {
            Dispose(true);
            GC.SuppressFinalize(this);
        }

        protected virtual void Dispose(bool disposing)
        {
            if (!disposed)
            {
                if (disposing)
                {
                    _end();
                }
            }
            disposed = true;
        }
    }
}
