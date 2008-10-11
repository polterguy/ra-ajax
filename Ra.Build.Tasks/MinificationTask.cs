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

using System;
using System.IO;
using System.Xml;
using System.Globalization;
using NAnt.Core;
using NAnt.Core.Types;
using NAnt.Core.Attributes;

namespace Ra.Build.Tasks
{
    public abstract class MinificationTask : Task
    {
        #region [-- Log Messages --]

        protected const string TODIR_OR_INPLACE_MISSING =
            "Either the 'inplace' attribute must be set to true or the 'todir' attribute must be specified.";
        protected const string FILESET_MISSING = 
            "The <fileset> element must be used to specify the files to be minified.";
        protected const string MINIFY_VERBOSE_MSG = "Minifying '{0}' to '{1}'.";
        protected const string MINIFY_INFO_MSG = "Minifying {0} file(s) {1}.";
        protected const string FILE_NOT_FOUND = "Could not find file '{0}' to minify.";

        #endregion

        #region [-- Private Fields --]

        protected bool _overwrite;
        protected bool _flatten;
        protected bool _gZip;
        protected DirectoryInfo _toDirectory;
        protected FileSet _files = new FileSet();

        #endregion

        #region [-- Public Properties --]

        [TaskAttribute("todir")]
        public virtual DirectoryInfo ToDirectory
        {
            get { return _toDirectory; }
            set { _toDirectory = value; }
        }

        [TaskAttribute("gzip")]
        [BooleanValidator()]
        public virtual bool GZip
        {
            get { return _gZip; }
            set { _gZip = value; }
        }

        [TaskAttribute("flatten")]
        [BooleanValidator()]
        public virtual bool Flatten
        {
            get { return _flatten; }
            set { _flatten = value; }
        }

        [TaskAttribute("overwrite")]
        [BooleanValidator()]
        public virtual bool Overwrite
        {
            get { return _overwrite; }
            set { _overwrite = value; }
        }

        [BuildElement("fileset", Required = true)]
        public virtual FileSet Files
        {
            get { return _files; }
            set { _files = value; }
        }

        #endregion

        protected abstract void Minify(string srcPath, string destPath);

        protected override void InitializeTask(XmlNode taskNode)
        {
            if (!_overwrite && _toDirectory == null)
                throw new BuildException(
                    string.Format(CultureInfo.InvariantCulture, TODIR_OR_INPLACE_MISSING), 
                    Location);

            if (_files == null)
                throw new BuildException(string.Format(CultureInfo.InvariantCulture, FILESET_MISSING), Location);

            if (!_overwrite && !_toDirectory.Exists)
                _toDirectory.Create();
        }
        
        protected override void ExecuteTask()
        {
            if (_files.BaseDirectory == null)
                _files.BaseDirectory = new DirectoryInfo(Project.BaseDirectory);

            Log(Level.Info, MINIFY_INFO_MSG, _files.FileNames.Count, 
                _overwrite ? "inplace" : string.Format(CultureInfo.InvariantCulture, "to '{0}'", _toDirectory.FullName));

            foreach (string srcPath in _files.FileNames)
            {
                FileInfo srcFile = new FileInfo(srcPath);

                if (srcFile.Exists)
                {
                    string destPath;

                    if (!_overwrite)
                    {
                        destPath = GetDestPath(_files.BaseDirectory, srcFile);

                        if (!Directory.Exists(Path.GetDirectoryName(destPath)))
                            Directory.CreateDirectory(Path.GetDirectoryName(destPath));
                    }
                    else
                    {
                        destPath = srcPath;
                    }

                    Log(Level.Verbose, MINIFY_VERBOSE_MSG, srcPath, destPath);
                    Minify(srcPath, destPath);
                }
                else
                {
                    throw new BuildException(
                        string.Format(CultureInfo.InvariantCulture, FILE_NOT_FOUND, srcFile.FullName),
                        Location);
                }
            }
        }

        protected virtual string GetDestPath(DirectoryInfo srcBase, FileInfo srcFile)
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
