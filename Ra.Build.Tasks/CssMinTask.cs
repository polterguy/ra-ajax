using System;
using System.Collections.Generic;
using System.Text;
using System.IO;
using NAnt.Core;
using NAnt.Core.Attributes;
using NAnt.Core.Types;
using System.Xml;
using System.Globalization;
using NAnt.Core.Tasks;
using System.Text.RegularExpressions;

/* 
Copyright 2008 - Thomas Hansen thomas@ra-ajax.org

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

namespace Ra.Build.Tasks
{
    [TaskName("cssmin")]
    public class CssMinTask : JsMinTask
    {
        #region Private Fields

        private bool _inPlace;

        #endregion

        #region Public Properties

        [TaskAttribute("inplace")]
        [BooleanValidator()]
        public virtual bool InPlace
        {
            get { return _inPlace; }
            set { _inPlace = value; }
        }

        [BuildElement("fileset", Required = true)]
        public virtual FileSet CssFiles
        {
            get { return base.JsFiles; }
            set { base.JsFiles = value; }
        }

        [TaskAttribute("todir")]
        public override DirectoryInfo ToDirectory
        {
            get { return base.ToDirectory; }
            set { base.ToDirectory = value; }
        }

        #endregion

        protected override void InitializeTask(XmlNode taskNode)
        {
            if (!_inPlace && ToDirectory == null)
                throw new BuildException(
                    string.Format(
                        CultureInfo.InvariantCulture, 
                        "The 'todir' attribute must be set to specify the output directory of the minified CSS files."),
                    Location);

            if (CssFiles == null)
                throw new BuildException(
                    string.Format(
                        CultureInfo.InvariantCulture,
                        "The <fileset> element must be used to specify the CSS files to minify."), 
                    Location);

            if (ToDirectory != null && !_inPlace && !ToDirectory.Exists)
                ToDirectory.Create();
        }
        
        protected override void ExecuteTask()
        {
            if (CssFiles.BaseDirectory == null)
                CssFiles.BaseDirectory = new DirectoryInfo(Project.BaseDirectory);

            Log(Level.Info, "Minifying {0} CSS file(s).", CssFiles.FileNames.Count);

            foreach (string srcPath in CssFiles.FileNames)
            {
                FileInfo srcFile = new FileInfo(srcPath);

                if (srcFile.Exists)
                {
                    string destPath;

                    if (!_inPlace)
                    {
                        destPath = GetDestPath(CssFiles.BaseDirectory, srcFile);
                        DirectoryInfo destDir = new DirectoryInfo(Path.GetDirectoryName(destPath));

                        if (!destDir.Exists)
                            destDir.Create();
                    }
                    else
                    {
                        destPath = srcPath;
                    }
                   
                    Log(Level.Verbose, "Minifying '{0}' to '{1}'.", srcPath, destPath);
                    MinifyCss(srcPath, destPath);
                }
                else
                {
                    throw new BuildException(
                        string.Format(
                            CultureInfo.InvariantCulture,
                            "Could not find file '{0}' to minify.",
                            srcFile.FullName),
                        Location);
                }
            }
        }

        protected void MinifyCss(string srcPath, string destPath)
        {
            string css;
            using (StreamReader sr = new StreamReader(srcPath))
                css = sr.ReadToEnd();

            string[] cssLines = css.Split(new string[] { Environment.NewLine }, StringSplitOptions.RemoveEmptyEntries);
            StringBuilder newCssBuilder = new StringBuilder();

            foreach (string cssLine in cssLines)
            {
                string newCssLine = cssLine.Trim();
                if (!string.IsNullOrEmpty(newCssLine))
                    newCssBuilder.Append(newCssLine);
            }

            string newCss = newCssBuilder.ToString();

            newCss = Regex.Replace(newCss, "/\\*[\\d\\D]*?\\*/", string.Empty, RegexOptions.Compiled);
            newCss = Regex.Replace(newCss, "\\s*{\\s*", "{", RegexOptions.Compiled | RegexOptions.ECMAScript);
            newCss = Regex.Replace(newCss, "\\s*}\\s*", "}", RegexOptions.Compiled | RegexOptions.ECMAScript);
            newCss = Regex.Replace(newCss, "\\s*:\\s*", ":", RegexOptions.Compiled | RegexOptions.ECMAScript);
            newCss = Regex.Replace(newCss, "\\s*,\\s*", ",", RegexOptions.Compiled | RegexOptions.ECMAScript);
            newCss = Regex.Replace(newCss, "\\s*;\\s*", ";", RegexOptions.Compiled | RegexOptions.ECMAScript);

            using (StreamWriter sw = new StreamWriter(destPath))
                sw.WriteLine(newCss);
        }
    }
}
