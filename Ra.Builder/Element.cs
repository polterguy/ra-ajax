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
     * Element class to help create better syntax when constructing HTML. Kind of like
     * and alternative for String.Format. Use in combination with HtmlBuilder. Implements
     * IDisposable pattern so that you can wrap Element construction into a using statement
     * to have assurance of writing the end element to the underlaying stream.
     */
    public class Element : DeterministicExecutor
    {
        private HtmlBuilder _builder;
        private bool _closed;

        /**
         * CTOR taking an HtmlBuilder and the element tag name.
         */
        public Element(HtmlBuilder builder, string elementName)
        {
            _builder = builder;
            _builder.Writer.Write("<" + elementName);
            End = delegate 
            {
                this.CloseOpeningElement();
                this._builder.Writer.Write("</" + elementName + ">");
            };
        }

        /**
         * Adds an attribute to the Element. Dependant upon that the opening element has NOT
         * been closed yet. Notice that an element will be closed when you access the underlaying
         * stream in any ways. Like for instance when writing to the Element by calling the Write
         * method or by writing to the HtmlBuilder by accessing the Writer of the HtmlBuilder.
         * This means you must add ALL attributes BEFORE you start writing any other content
         * to the Element/Stream.
         */
        public void AddAttribute(string name, string value)
        {
            if (_closed)
                throw new Exception("Can't add an attribute once the attribute is closed due to accessing the underlaying Writer or something else");
            _builder.WriterUnClosed.Write(" " + name + "=\"" + value + "\"");
        }

        /**
         * Will write the given content formated to the underlaying writer. Think in terms of 
         * String.Format
         */
        public void Write(string content, params object[] args)
        {
            _builder.Writer.Write(content, args);
        }

        /**
         * Will write the given content to the underlaying writer.
         */
        public void Write(string content)
        {
            _builder.Writer.Write(content);
        }

        internal void CloseOpeningElement()
        {
            if (_closed)
                return;
            _closed = true;
            _builder.Writer.Write(">");
        }

        protected override void Dispose(bool disposing)
        {
            base.Dispose(disposing);
            _builder.Writer.Flush();
        }
    }
}
