/* 
Copyright 2008 - 2009 - Ra-Software AS

Permission is hereby granted, free of charge, to any person obtaining a copy of
this software and associated documentation files (the "Software"), to deal in
the Software without restriction, including without limitation the rights to
use, copy, modify, merge, publish, distribute, sublicense, and/or sell copies
of the Software, and to permit persons to whom the Software is furnished to do
so, subject to the following conditions:

The above copyright notice and this permission notice shall be included in all
copies or substantial portions of the Software.

The Software shall be used for Good, not Evil.

THE SOFTWARE IS PROVIDED "AS IS", WITHOUT WARRANTY OF ANY KIND, EXPRESS OR
IMPLIED, INCLUDING BUT NOT LIMITED TO THE WARRANTIES OF MERCHANTABILITY,
FITNESS FOR A PARTICULAR PURPOSE AND NONINFRINGEMENT. IN NO EVENT SHALL THE
AUTHORS OR COPYRIGHT HOLDERS BE LIABLE FOR ANY CLAIM, DAMAGES OR OTHER
LIABILITY, WHETHER IN AN ACTION OF CONTRACT, TORT OR OTHERWISE, ARISING FROM,
OUT OF OR IN CONNECTION WITH THE SOFTWARE OR THE USE OR OTHER DEALINGS IN THE
SOFTWARE.
 

*/

using System;
using System.IO;
using System.IO.Compression;
using NAnt.Core.Attributes;

namespace Ra.Build.Tasks
{
    [TaskName("jsmin")]
    public class JsMinTask : MinificationTask
    {
        protected override void Minify(string srcPath, string destPath)
        {
            if (_overwrite)
            {
                string tempDestPath = destPath.Insert(destPath.LastIndexOf('.'), "_temp");

                new JavaScriptMinifier().Minify(srcPath, tempDestPath);

                File.Move(tempDestPath, destPath);
            }
            else
            {
                new JavaScriptMinifier().Minify(srcPath, destPath);
            }

            if (_gZip)
            {
                byte[] buffer;
                using (FileStream jsStream = new FileStream(destPath, FileMode.Open, FileAccess.Read, FileShare.Read))
                {
                    buffer = new byte[jsStream.Length];
                    jsStream.Read(buffer, 0, Convert.ToInt32(jsStream.Length));
                }

                using (FileStream fs = File.Create(destPath))
                {
                    using (GZipStream gzs = new GZipStream(fs, CompressionMode.Compress))
                        gzs.Write(buffer, 0, buffer.Length);
                }
            }
        }
    }
}
