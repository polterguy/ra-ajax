/*
 * Ra-Ajax - A Managed Ajax Library for ASP.NET and Mono
 * Copyright 2008 - 2009 - Thomas Hansen thomas@ra-ajax.org
 * This code is licensed under the GPL version 3 which 
 * can be found in the license.txt file on disc.
 * 
 */

using System;
using System.IO;

namespace Ra.Core
{
    internal class PostbackFilter : Stream
    {
        private Stream _next;
        private MemoryStream _stream = new MemoryStream();

        public PostbackFilter(Stream next)
        {
            _next = next;
        }

        public override bool CanRead
        {
            get { return false; }
        }

        public override bool CanSeek
        {
            get { return false; }
        }

        public override bool CanWrite
        {
            get { return true; }
        }

        public override long Length
        {
            get { return 0; }
        }

        public override long Position
        {
            get { return 0; }
            set { }
        }

        public override void Close()
        {
            AjaxManager.Instance.RenderPostback(_next, _stream);
            base.Close();
        }

        public override void Flush()
        {
        }

        public override long Seek(long offset, SeekOrigin origin)
        {
            return 0;
        }

        public override void SetLength(long value)
        {
        }

        public override int Read(byte[] buffer, int offset, int count)
        {
            return 0;
        }

        public override void Write(byte[] buffer, int offset, int count)
        {
            _stream.Write(buffer, offset, count);
        }
    }
}
