using System;
using System.Threading;
using NUnit.Framework;

namespace NUnitTests
{
    [TestFixture]
    public class TestControlsCombined : TestBase
    {
        protected override string Url
        {
            get { return "RaControlsCombined.aspx"; }
        }

        [NUnit.Framework.Test]
        public void VerifyLabelIsRendered()
        {
            Browser.Eval("verifyLabelIsRendered();");
            AssertSuccess("Ra JSON serialization doesn't work");
        }
    }
}
















