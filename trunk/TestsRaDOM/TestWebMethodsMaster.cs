using System;
using System.Threading;
using NUnit.Framework;

namespace NUnitTests
{
    [TestFixture]
    public class TestWebMethodsMaster : TestBase
    {
        protected override string Url
        {
            get { return "WebMethodsMaster.aspx"; }
        }

        [NUnit.Framework.Test]
        public void TestSimpleMethodInMasterPage()
        {
            Browser.Button("btn").Click();
            AssertSuccess("Didn't work");
        }
    }
}
















