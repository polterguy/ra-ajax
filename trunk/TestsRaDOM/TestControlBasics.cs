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
        public void TestJSONBasics()
        {
            Browser.Button("testJSONBasicsBtn").Click();
            AssertSuccess("Ra JSON serialization doesn't work");
        }

        [NUnit.Framework.Test]
        public void TestSettingButtonInVisible()
        {
            Browser.Button("testCallback").Click();
            Browser.Eval("checkThatButtonWasDeleted();");
            AssertSuccess("Ra Control was not removed out of collection when set to invisible");
        }

        [NUnit.Framework.Test]
        public void TestSettingButtonInVisibleMadeVisible()
        {
            Browser.Eval("checkThatButtonIsInitiallyCreatedInVisible();");
            Browser.Button("testCallbackSetButtonVisible").Click();
            Browser.Eval("checkThatButtonWasCreated();");
            AssertSuccess("Ra Control was not created");
        }

        [NUnit.Framework.Test]
        public void TestCheckCssClass()
        {
            Browser.Eval("checkCssClass();");
            AssertSuccess("Ra Control CssClass was not serialized on initial rendering");
        }

        [NUnit.Framework.Test]
        public void TestCheckStyleSerialization()
        {
            Browser.Button("testAddStyles").Click();
            Browser.Eval("checkStylesAfterServerChange();");
            AssertSuccess("Ra Control Style serialization / JSON serialization didn't work");
        }
    }
}
















