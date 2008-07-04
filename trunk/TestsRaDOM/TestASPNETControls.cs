using System;
using System.Threading;
using NUnit.Framework;

namespace NUnitTests
{
    [TestFixture]
    public class TestASPNETControls : TestBase
    {
        protected override string Url
        {
            get { return "ASPNETControlsIntegration.aspx"; }
        }

        [NUnit.Framework.Test]
        public void SubmitFormWithLotsOfASPNETControls()
        {
            Browser.Button("btn").Click();
            Assert.AreEqual("texttextsecondFalseTrueFalseTrue", Browser.Span("lbl").Text);
        }
    }
}
















