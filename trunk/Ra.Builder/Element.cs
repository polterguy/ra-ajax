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
        private string elementName;
        HtmlBuilder _builder;
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
