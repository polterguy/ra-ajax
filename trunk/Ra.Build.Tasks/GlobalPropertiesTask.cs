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
using NAnt.Core.Tasks;

namespace Ra.Build.Tasks
{
    [TaskName("load-properties")]
    public class GlobalPropertiesTask : Task
    {
        #region [-- Log Messages --]

        protected const string INFO_MSG = "Loaded {0} properties from {1}.";
        protected const string NO_PROPERTIES_FILE = "The properties-file must be specified.";
        protected const string FILE_NOT_FOUND = "Could not find the specified properties file '{0}'.";

        #endregion

        #region [-- Private Fields --]

        protected string _propertiesFile;

        #endregion

        #region [-- Public Properties --]

        [TaskAttribute("properties-file", Required = true)]
        [StringValidator(AllowEmpty = false)]
        public virtual string PropertiesFile
        {
            get { return _propertiesFile; }
            set { _propertiesFile = value; }
        }

        #endregion

        protected override void InitializeTask(XmlNode taskNode)
        {
            if (string.IsNullOrEmpty(_propertiesFile))
                throw new BuildException(
                    string.Format(CultureInfo.InvariantCulture, NO_PROPERTIES_FILE), 
                    Location);

            if (!File.Exists(_propertiesFile))
                throw new BuildException(
                    string.Format(CultureInfo.InvariantCulture, FILE_NOT_FOUND, _propertiesFile), Location);
        }
        
        protected override void ExecuteTask()
        {
            XmlDocument xml = new XmlDocument();
            using (XmlTextReader reader = new XmlTextReader(_propertiesFile))
                xml.Load(reader);

            XmlNodeList properties = xml.GetElementsByTagName("property");

            int counter = 0;
            foreach (XmlNode property in properties)
            {
                string name = string.Empty, value = string.Empty;
                bool readOnly = false, dynamic = false, overwrite = true;

                if (property.Attributes["name"] != null)
                    name = string.IsNullOrEmpty(property.Attributes["name"].Value)
                        ? string.Empty : property.Attributes["name"].Value;

                if (property.Attributes["value"] != null)
                    value = string.IsNullOrEmpty(property.Attributes["value"].Value)
                        ? string.Empty : property.Attributes["value"].Value;

                if (property.Attributes["readonly"] != null)
                    readOnly = string.IsNullOrEmpty(property.Attributes["readonly"].Value)
                        ? false : GetBooleanValue(property.Attributes["readonly"].Value, false);

                if (property.Attributes["dynamic"] != null)
                    dynamic = string.IsNullOrEmpty(property.Attributes["dynamic"].Value)
                        ? false : GetBooleanValue(property.Attributes["dynamic"].Value, false);

                if (property.Attributes["overwrite"] != null)
                    overwrite = string.IsNullOrEmpty(property.Attributes["overwrite"].Value)
                        ? true : GetBooleanValue(property.Attributes["overwrite"].Value, true);

                if (property.Attributes["projects"] != null && !string.IsNullOrEmpty(property.Attributes["projects"].Value))
                {
                    string[] projects = property.Attributes["projects"].Value.Split(
                        new char[]{','}, StringSplitOptions.RemoveEmptyEntries);

                    if (Array.Exists(projects, delegate(string project) { return project == Project.ProjectName; }))
                    {
                        PropertyTask task = new PropertyTask();
                        task.Project = Project;
                        task.NamespaceManager = NamespaceManager;
                        task.PropertyName = name;
                        task.Value = value;
                        task.ReadOnly = readOnly;
                        task.Dynamic = dynamic;
                        task.Overwrite = overwrite;
                        task.Execute();
                        counter += 1;
                    }
                }
                else
                {
                    PropertyTask task = new PropertyTask();
                    task.Project = Project;
                    task.NamespaceManager = NamespaceManager;
                    task.PropertyName = name;
                    task.Value = value;
                    task.ReadOnly = readOnly;
                    task.Dynamic = dynamic;
                    task.Overwrite = overwrite;
                    task.Execute();
                    counter += 1;
                }
            }
            Log(Level.Info, INFO_MSG, counter, _propertiesFile);
        }

        private bool GetBooleanValue(string stringValue, bool defaultValue)
        {
            if (stringValue.ToLowerInvariant() == bool.TrueString.ToLowerInvariant())
                return true;
            else if (stringValue.ToLowerInvariant() == bool.FalseString.ToLowerInvariant())
                return false;
            else
                return defaultValue;
        }
    }
}
