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
using System.Text;
using System.Text.RegularExpressions;
using NAnt.Core.Attributes;

namespace Ra.Build.Tasks
{
    [TaskName("cssmin")]
    public class CssMinTask : MinificationTask
    {
        protected override void Minify(string srcPath, string destPath)
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
                sw.Write(newCss);
        }
    }
}
