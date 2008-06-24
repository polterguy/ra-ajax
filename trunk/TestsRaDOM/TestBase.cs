using System;
using System.Diagnostics;
using System.Configuration;
using NUnit.Framework;
using WatiN.Core;
using WatiN.Core.Interfaces;

namespace TestsRaDOM
{
    public abstract class TestBase
    {
        private Process _server;
        private IBrowser _browser;

        protected IBrowser Browser
        {
            get { return _browser; }
        }

        [NUnit.Framework.TestFixtureSetUp]
        public void Init()
        {
            StartWebDev();
            StartBrowser();
        }

        protected void AssertSuccess(string errMsg)
        {
            string success = Browser.Div("results").InnerHtml;
            Browser.Eval("init();");
            Assert.AreEqual("success", success, errMsg);
        }

        private void StartBrowser()
        {
            string url = ConfigurationSettings.AppSettings["DefaultPageUrl"] + Url;
            _browser = WatiN.Core.BrowserFactory.Create(BrowserType.InternetExplorer);
            _browser.GoTo(url);
        }

        private void StartWebDev()
        {
            string webServerExePath = (string)ConfigurationSettings.AppSettings["WebServerExePath"];
            _server = new Process();
            _server = Process.Start(webServerExePath, GetWebServerArguments());
        }

        protected abstract string Url
        {
            get;
        }

        [NUnit.Framework.TestFixtureTearDown]
        public void End()
        {
            _server.Kill();
            _browser.Dispose();
        }


        private static string GetWebServerArguments()
        {
            string args = String.Format("/port:{0} /path:\"{1}\"", GetPort(), GetWebApplicationPath());
            return args;
        }

        private static string GetPort()
        {
            string port = ConfigurationSettings.AppSettings["Port"] as String;
            return port;
        }

        private static string GetWebApplicationPath()
        {
            string webApplicationPath = ConfigurationSettings.AppSettings["WebApplicationPath"] as String;
            return webApplicationPath;
        }
    }
}
