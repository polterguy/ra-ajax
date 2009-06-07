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

        /**
         * CTOR taking only a end functor. Normally you'd use this one only when inheriting from class
         * and you need to set the Start functor later. Then the Start functor will be executed immediately
         * when you set it with the Start property.
         */
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

        /**
         * Use this one ONLY in combination with the CTOR taking only the End functor. The method
         * passed will be executed immediately when this property is called.
         */
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
