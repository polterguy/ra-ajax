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
    }
}
















