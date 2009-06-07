using System;
using System.Threading;
using NUnit.Framework;

namespace Ra.Tests.Ajax
{
    [TestFixture]
    public class TestASPNETControls : TestBase
    {
        protected override string Url
        {
            get { return "ASPNETControlsIntegration.aspx"; }
        }

        [NUnit.Framework.Test]
        public void SubmitFormWithLotsOfASPNETControlsThroughRaAjax()
        {
            Browser.Button("btn").Click();
            Assert.AreEqual("texttextsecondFalseTrueFalseTrue", Browser.Span("lbl").Text);
        }

        [NUnit.Framework.Test]
        public void SubmitFormWithLotsOfRaAjaxControlsThroughASPNET()
        {
            Browser.Button("btnASP").Click();
            Assert.AreEqual("texttextsecondFalseTrueFalseTrue", Browser.Span("lblASP").Text);
        }
    }
}
















