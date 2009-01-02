/* 
Copyright 2008 - 2009 - Thomas Hansen thomas@ra-ajax.org

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
using System.Xml;
using System.Globalization;
using NAnt.Core;
using NAnt.Core.Types;
using NAnt.Core.Attributes;

namespace Ra.Build.Tasks
{
    [TaskName("file-create")]
    public class CreateFileTask : Task
    {
        #region [-- Log Messages --]

        protected const string INFO_MSG = "Created '{0}' successfully.";
        protected const string NO_CONTENT_MSG = "You must speify file content.";
        protected const string ERROR_CREATING_FILE = "Could not create file '{0}'.";

        #endregion

        #region [-- Private Fields --]

        protected bool _overwrite;
        protected string _fileName;
        protected RawXml _content;

        #endregion

        #region [-- Public Properties --]

        [TaskAttribute("overwrite")]
        [BooleanValidator()]
        public virtual bool Overwrite
        {
            get { return _overwrite; }
            set { _overwrite = value; }
        }

        [TaskAttribute("filename")]
        [StringValidator(AllowEmpty = false)]
        public virtual string FileName
        {
            get { return _fileName; }
            set { _fileName = value; }
        }

        [BuildElement("content", Required = true)]
        public virtual RawXml Content
        {
            get { return _content; }
            set { _content = value; }
        }

        #endregion

        protected override void InitializeTask(XmlNode taskNode)
        {
            if (_content == null)
                throw new BuildException(string.Format(CultureInfo.InvariantCulture, NO_CONTENT_MSG), Location);
        }
        
        protected override void ExecuteTask()
        {
            if (File.Exists(_fileName) && !_overwrite)
                return;

            using (StreamWriter sw = new StreamWriter(_fileName, false, System.Text.Encoding.UTF8))
                sw.Write(_content.Xml.InnerText);
                
            if (!File.Exists(_fileName))
                Log(Level.Info, string.Format(ERROR_CREATING_FILE, _fileName));
            else
                Log(Level.Info, string.Format(INFO_MSG, _fileName));
        }
    }
}
