using System;
using System.Threading;
using NUnit.Framework;

namespace NUnitTests
{
    [TestFixture]
    public class TestSelector : TestBase
    {
        protected override string Url
        {
            get { return "SelectorTests.aspx"; }
        }

        [NUnit.Framework.Test]
        public void EasyTest()
        {
            Browser.Button("easyTest_firstButton").Click();
            Assert.AreEqual(Browser.Button("easyTest_firstButton").Text, "firstButton");
        }

        [NUnit.Framework.Test]
        public void RecursiveTest()
        {
            Browser.Button("recursiveTest_recursive3_recursive4_thirdButton").Click();
            Assert.AreEqual(Browser.Button("recursiveTest_recursive3_recursive4_thirdButton").Text, "thirdButton");
        }
    }
}
















