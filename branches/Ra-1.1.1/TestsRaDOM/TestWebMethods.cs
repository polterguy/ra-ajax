using System;
using System.Threading;
using NUnit.Framework;

namespace NUnitTests
{
    [TestFixture]
    public class TestWebMethods : TestBase
    {
        protected override string Url
        {
            get { return "WebMethods.aspx"; }
        }

        [NUnit.Framework.Test]
        public void TestSimpleMethod()
        {
            Browser.Button("btn").Click();
            AssertSuccess("Didn't work");
        }

        [NUnit.Framework.Test]
        public void TestNestedUserControls()
        {
            Browser.Button("btn2").Click();
            AssertSuccess("Didn't work");
        }
    }
}
















