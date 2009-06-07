using System;
using System.Threading;
using NUnit.Framework;

namespace Ra.Tests.Ajax
{
    [TestFixture]
    public class TestRaControlNoViewState : TestBase
    {
        protected override string Url
        {
            get { return "RaControlNoViewState.aspx"; }
        }

        [NUnit.Framework.Test]
        public void SetStyleValue()
        {
            Browser.Button("testChangeStyle").Click();
            Browser.Eval("checkStylesAfterServerChange();");
            AssertSuccess("Ra JSON serialization doesn't work");
        }

        [NUnit.Framework.Test]
        public void VerifyNoStyleInViewState()
        {
            Browser.Button("verifyNoStylesChanged").Click();
            Assert.AreEqual("success", Browser.Button("verifyNoStylesChanged").Text);
        }
    }
}
















