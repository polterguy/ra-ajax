using System;
using System.Threading;
using NUnit.Framework;

namespace NUnitTests
{
    [TestFixture]
    public class TestEventSystem : TestBase
    {
        protected override string Url
        {
            get { return "EventSystem.aspx"; }
        }

        [NUnit.Framework.Test]
        public void TestClick()
        {
            Browser.Span("click").Click();
            Assert.AreEqual("success", Browser.Span("click").Text);
        }

        [NUnit.Framework.Test]
        public void TestDblClick()
        {
            Browser.Span("dblClick").FireEvent("ondblclick");
            Assert.AreEqual("success", Browser.Span("dblClick").Text);
        }

        [NUnit.Framework.Test]
        public void TestKeyDown()
        {
            Browser.Span("keyDown").FireEvent("onkeydown");
            Assert.AreEqual("success", Browser.Span("keyDown").Text);
        }
    }
}
















