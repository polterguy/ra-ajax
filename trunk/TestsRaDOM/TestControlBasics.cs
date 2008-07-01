using System;
using System.Threading;
using NUnit.Framework;

namespace NUnitTests
{
    [TestFixture]
    public class TestRaControlBasics : TestBase
    {
        protected override string Url
        {
            get { return "RaControlBasics.aspx"; }
        }

        [NUnit.Framework.Test]
        public void JSONBasics()
        {
            Browser.Button("testJSONBasicsBtn").Click();
            AssertSuccess("Ra JSON serialization doesn't work");
        }

        [NUnit.Framework.Test]
        public void SettingButtonInVisible()
        {
            Browser.Button("testCallback").Click();
            Browser.Eval("checkThatButtonWasDeleted();");
            AssertSuccess("Ra Control was not removed out of collection when set to invisible");
        }

        [NUnit.Framework.Test]
        public void SettingButtonInVisibleMadeVisible()
        {
            Browser.Eval("checkThatButtonIsInitiallyCreatedInVisible();");
            Browser.Button("testCallbackSetButtonVisible").Click();
            Browser.Eval("checkThatButtonWasCreated();");
            AssertSuccess("Ra Control was not created");
        }

        [NUnit.Framework.Test]
        public void CheckCssClass()
        {
            Browser.Eval("checkCssClass();");
            AssertSuccess("Ra Control CssClass was not serialized on initial rendering");
        }

        [NUnit.Framework.Test]
        public void CheckStyleSerialization()
        {
            Browser.Button("testAddStyles").Click();
            Browser.Eval("checkStylesAfterServerChange();");
            AssertSuccess("Ra Control Style serialization / JSON serialization didn't work");
        }

        [NUnit.Framework.Test]
        public void SetTextOfButton()
        {
            Browser.Button("testSettingTextProperty").Click();
            Assert.AreEqual("New Text", Browser.Button("testSettingTextProperty").Text);
        }

        [NUnit.Framework.Test]
        public void ViewStateSerializationOfStyleProperty()
        {
            Browser.Button("testChangeStyleValue").Click();
            System.Threading.Thread.Sleep(1000);
            Browser.Button("testVerifyStyleValue").Click();
            System.Threading.Thread.Sleep(1000);
            Assert.AreEqual("success", Browser.Button("testVerifyStyleValue").Text);
        }

        [NUnit.Framework.Test]
        public void StyleSerializedByDefault()
        {
            Browser.Eval("checkStylesWasSerialized();");
            AssertSuccess("Serialization of RaWebControl styles didn't work");
        }
    }
}
















