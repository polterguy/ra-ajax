using System;
using System.Diagnostics;
using System.Configuration;
using NUnit.Framework;
using WatiN.Core;
using WatiN.Core.Interfaces;
using System.IO;

namespace Ra.Tests.Ajax
{
    public abstract class TestBase
    {
        [SetUpFixture]
        protected class SetupTests
        {
            private static Process _server;
            public static IBrowser _browser;

            [SetUp]
            public void Setup()
            {
                StartWebDev();
                PreCompileTestWebsite();
                _browser = WatiN.Core.BrowserFactory.Create(BrowserType.InternetExplorer);
            }

            [TearDown]
            public void TearDown()
            {
                _server.Kill();
                _browser.Dispose();
            }

            private void StartWebDev()
            {
                string webServerExePath = ConfigurationSettings.AppSettings["WebServerExePath"];
                _server = new Process();
                _server = Process.Start(webServerExePath, GetWebServerArguments());
            }

            private void PreCompileTestWebsite()
            {
                Process aspnetCompiler = new Process();
                aspnetCompiler.StartInfo.FileName = ConfigurationSettings.AppSettings["ASPNETCompilerPath"];
                aspnetCompiler.StartInfo.Arguments = string.Format("-p \"{0}\"", GetWebApplicationPath());
                aspnetCompiler.Start();
                aspnetCompiler.WaitForExit();
            }

            private static string GetWebServerArguments()
            {
                return String.Format("/port:{0} /path:\"{1}\"", GetPort(), GetWebApplicationPath());
            }

            private static string GetPort()
            {
                return ConfigurationSettings.AppSettings["Port"];
            }

            private static string GetWebApplicationPath()
            {
                return Path.Combine(
                    Directory.GetParent(Directory.GetParent(Directory.GetParent(
                        Environment.CurrentDirectory
                    ).ToString()).ToString()).ToString(),
                    ConfigurationSettings.AppSettings["WebApplicationPath"]
                );
            }
            
        }

        private IBrowser _browser;

        protected IBrowser Browser
        {
            get { return _browser; }
        }

        protected void AssertSuccess(string errMsg)
        {
            string success = Browser.Div("results").InnerHtml;
            Browser.Eval("init();");
            Assert.AreEqual("success", success, errMsg);
        }

        protected abstract string Url
        {
            get;
        }

        [TestFixtureSetUp]
        public void Setup()
        {
            string url = ConfigurationSettings.AppSettings["DefaultPageUrl"] + Url;
            _browser = SetupTests._browser;
            _browser.GoTo(url);
        }
    }
}
