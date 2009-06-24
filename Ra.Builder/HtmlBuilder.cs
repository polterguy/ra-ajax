/*
 * Ra-Ajax - A Managed Ajax Library for ASP.NET and Mono
 * Copyright 2008 - 2009 - Thomas Hansen thomas@ra-ajax.org
 * This code is licensed under the GPL version 3 which 
 * can be found in the license.txt file on disc.
 *
 */

using System;
using System.IO;
using System.Web.UI;

namespace Ra.Builder
{
    /**
     * Use in combination with the Element class to create HTML in your C# code with way better syntax.
     */
    public class HtmlBuilder : IDisposable
    {
        private HtmlTextWriter _writer;
        private Element _lastElement;
        private bool disposed;
        private MemoryStream _stream;

        /**
         * CTOR taking no writer. This will internally create a MemoryStream which the content
         * will be written to. When you're finished writing to this HtmlBuilder you can call
         * the ToString method to get the HTML into a string.
         */
        public HtmlBuilder()
            : this(null)
        { }

        /**
         * CTOR taking a TextWriter which will be used to output your contents into.
         * Note that when using this contructor the HtmlBuilder will not in ANY ways
         * "take ownership" of the Writer in any ways and the TextWriter will not be 
         * disposed in the Dispose method of this class.
         */
        public HtmlBuilder(HtmlTextWriter writer)
        {
            if (writer == null)
            {
                _stream = new MemoryStream();
                TextWriter tmp = new StreamWriter(_stream);
                _writer = new HtmlTextWriter(tmp);
            }
            else
            {
                _writer = writer;
            }
        }

        /**
         * Closes the opening element (if any) and returns the TextWriter back so that you can
         * write contents to the current Element.
         */
        public HtmlTextWriter Writer
        {
            get
            {
                if (_lastElement != null)
                    _lastElement.CloseOpeningElement();
                return _writer;
            }
        }

        internal HtmlTextWriter WriterUnClosed
        {
            get { return _writer; }
        }

        internal Stream Stream
        {
            get { return _stream; }
        }

        /**
         * Creates a new element with the given name and returns it to the caller. Make
         * sure you call dispose on the returned element, either directly or by wrapping the
         * returned element from this method inside a using statement in C#.
         */
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

        /**
         * Returns the entire HTML of the HtmlBuilder. Notice that this method will ONLY
         * work if the CTOR taking no arguments is being used, since otherwise the HtmlBuilder
         * will have no mechanism for retrieving the underlaying Stream in any ways - which is
         * needed to be able to set the Position for the stream and create a new TextReader
         * which is what this method actually internally does.
         */
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

        /**
         * Returns the entire HTML of the HtmlBuilder. Notice that this method will ONLY
         * work if the CTOR taking no arguments is being used, since otherwise the HtmlBuilder
         * will have no mechanism for retrieving the underlaying Stream in any ways - which is
         * needed to be able to set the Position for the stream and create a new TextReader
         * which is what this method actually internally does. This version will return the 
         * HTML as escaped so that it's useful for 
         */
        public string ToJSONString()
        {
            if (_stream == null)
                return base.ToString();
            _writer.Flush();
            _stream.Flush();
            _stream.Position = 0;
            TextReader reader = new StreamReader(_stream);
            string retVal = reader.ReadToEnd();
            retVal = retVal.Replace("\\", "\\\\").Replace("'", "\\'").Replace("\r", "\\r").Replace("\n", "\\n");
            return retVal;
        }
    }
}
