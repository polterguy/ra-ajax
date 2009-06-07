/*
 * Ra-Ajax - A Managed Ajax Library for ASP.NET and Mono
 * Copyright 2008 - 2009 - Thomas Hansen thomas@ra-ajax.org
 * This code is licensed under the GPL version 3 which 
 * can be found in the license.txt file on disc.
 * 
 */

using System;
using System.IO;

namespace Ra.Builder
{
    public class HtmlBuilder : IDisposable
    {
        private TextWriter _writer;
        private Element _lastElement;
        private bool disposed;
        private MemoryStream _stream;

        public HtmlBuilder()
            : this(null)
        { }

        public HtmlBuilder(TextWriter writer)
        {
            if (writer == null)
            {
                _stream = new MemoryStream();
                _writer = new StreamWriter(_stream);
            }
            else
            {
                _writer = writer;
            }
        }

        public TextWriter Writer
        {
            get
            {
                if (_lastElement != null)
                    _lastElement.CloseOpeningElement();
                return _writer;
            }
        }

        public Stream Stream
        {
            get { return _stream; }
        }

        public Element CreateElement(string elementName)
        {
            if (_lastElement != null)
                _lastElement.CloseOpeningElement();
            _lastElement = new Element(this, elementName);
            return _lastElement;
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
                    if (_lastElement != null)
                        _lastElement.CloseOpeningElement();
                    if (_stream != null)
                        _stream.Dispose();
                }
            }
            disposed = true;
        }

        public override string ToString()
        {
            if (_stream == null)
                return base.ToString();
            _writer.Flush();
            _stream.Flush();
            _stream.Position = 0;
            TextReader reader = new StreamReader(_stream);
            return reader.ReadToEnd();
        }
    }
}
