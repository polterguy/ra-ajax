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
    public class Element : DeterministicExecutor
    {
        private HtmlBuilder _builder;
        private bool _closed;

        public Element(HtmlBuilder builder, string elementName)
            : base(delegate
        {
            this.CloseOpeningElement();
            this._builder.Writer.Write("</" + elementName + ">");
        })
        {
            _builder = builder;
            _builder.Writer.Write("<" + elementName);
        }

        public void AddAttribute(string name, string content)
        {
            if (_closed)
                throw new Exception("Can't add an attribute once the attribute is closed due to accessing the underlaying Writer or something else");
            _builder.WriterUnClosed.Write(" " + name + "=\"" + content + "\"");
        }

        public void Write(string content, params object[] args)
        {
            _builder.Writer.Write(content, args);
        }

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
