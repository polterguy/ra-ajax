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

using System.Globalization;
using System.IO;
using System.Xml;
using NAnt.Core;
using NAnt.Core.Attributes;
using NAnt.Core.Types;

namespace Ra.Build.Tasks
{
    [TaskName("file-insert-text")]
    public class InsertTextTask : Task
    {
        #region [-- Log Messages --]

        protected const string INFO_MSG = "Text was inserted into file '{0}' successfully.";
        protected const string NO_POSITION = "You must speify insert-after-this or insert-before-this.";
        protected const string NO_CONTENT_MSG = "You must speify file content.";
        protected const string ERROR_FINDING_FILE = "Could not find file '{0}'.";

        #endregion

        #region [-- Private Fields --]

        protected string _fileName;
        protected RawXml _insertAfter;
        protected RawXml _insertBefore;
        protected RawXml _content;

        #endregion

        #region [-- Public Properties --]

        [TaskAttribute("filename")]
        [StringValidator(AllowEmpty = false)]
        public virtual string FileName
        {
            get { return _fileName; }
            set { _fileName = value; }
        }

        [BuildElement("insert-after")]
        public virtual RawXml InsertAfter
        {
            get { return _insertAfter; }
            set { _insertAfter = value; }
        }

        [BuildElement("insert-before")]
        public virtual RawXml InsertBefore
        {
            get { return _insertBefore; }
            set { _insertBefore = value; }
        }

        [BuildElement("text-to-insert", Required = true)]
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

            if (_insertAfter == null && _insertBefore == null)
                throw new BuildException(string.Format(CultureInfo.InvariantCulture, NO_POSITION));
        }
        
        protected override void ExecuteTask()
        {
            if (!File.Exists(_fileName))
            {
                Log(Level.Info, string.Format(ERROR_FINDING_FILE, _fileName));
                return;
            }
            else
            {
                string fileContent;

                using (StreamReader sr = new StreamReader(_fileName))
                    fileContent = sr.ReadToEnd();
                
                using (StreamWriter sw = new StreamWriter(_fileName, false, System.Text.Encoding.UTF8))
                {
                    string content = _content.Xml.InnerText;

                    if (_insertAfter != null)
                    {
                        string insertAfter = _insertAfter.Xml.InnerText;
                        sw.Write(fileContent.Replace(insertAfter, insertAfter + content));
                    }
                    else if (_insertBefore != null)
                    {
                        string insertBefore = _insertBefore.Xml.InnerText;
                        sw.Write(fileContent.Replace(insertBefore, content + insertBefore));
                    }
                }
                Log(Level.Info, string.Format(INFO_MSG, _fileName));
            }
        }
    }
}
