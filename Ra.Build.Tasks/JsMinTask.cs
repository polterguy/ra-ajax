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
    [TaskName("jsmin")]
    public class JsMinTask : Task
    {
        #region Private Fields
            
        private DirectoryInfo _toDirectory;
        private bool _flatten;
        private FileSet _jsFiles = new FileSet();

        #endregion

        #region Public Properties

        [TaskAttribute("todir", Required = true)]
        public virtual DirectoryInfo ToDirectory
        {
            get { return _toDirectory; }
            set { _toDirectory = value; }
        }

        [TaskAttribute("flatten")]
        [BooleanValidator()]
        public virtual bool Flatten
        {
            get { return _flatten; }
            set { _flatten = value; }
        }

        [BuildElement("fileset", Required = true)]
        public virtual FileSet JsFiles
        {
            get { return _jsFiles; }
            set { _jsFiles = value; }
        }

        #endregion

        protected override void InitializeTask(XmlNode taskNode)
        {
            if (_toDirectory == null)
                throw new BuildException(
                    string.Format(
                        CultureInfo.InvariantCulture, 
                        "The 'todir' attribute must be set to specify the output directory of the minified JS files."),
                    Location);

            if (!_toDirectory.Exists)
                _toDirectory.Create();

            if (_jsFiles == null)
                throw new BuildException(
                    string.Format(
                        CultureInfo.InvariantCulture,
                        "The <fileset> element must be used to specify the JS files to minify."), 
                    Location);
        }
        
        protected override void ExecuteTask()
        {
            if (_jsFiles.BaseDirectory == null)
                _jsFiles.BaseDirectory = new DirectoryInfo(Project.BaseDirectory);

            Log(Level.Info, "Minifying {0} JavaScript file(s) to '{1}'.", _jsFiles.FileNames.Count, _toDirectory.FullName);

            foreach (string srcPath in _jsFiles.FileNames)
            {
                FileInfo srcFile = new FileInfo(srcPath);

                if (srcFile.Exists)
                {
                    string destPath = GetDestPath(_jsFiles.BaseDirectory, srcFile);

                    DirectoryInfo destDir = new DirectoryInfo(Path.GetDirectoryName(destPath));

                    if (!destDir.Exists)
                        destDir.Create();

                    Log(Level.Verbose, "Minifying '{0}' to '{1}'.", srcPath, destPath);

                    new JavaScriptMinifier().Minify(srcPath, destPath);
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

        private string GetDestPath(DirectoryInfo srcBase, FileInfo srcFile)
        {
            string destPath = string.Empty;

            if (_flatten)
            {
                destPath = Path.Combine(_toDirectory.FullName, srcFile.Name);
            }
            else
            {
                if (srcFile.FullName.IndexOf(srcBase.FullName, 0) != -1)
                    destPath = srcFile.FullName.Substring(srcBase.FullName.Length);
                else
                    destPath = srcFile.Name;

                if (destPath[0] == Path.DirectorySeparatorChar)
                    destPath = destPath.Substring(1);

                destPath = Path.Combine(_toDirectory.FullName, destPath);
            }

            return destPath;
        }
    }
}
