using System;
using System.Threading;
using NUnit.Framework;

namespace NUnitTests
{
    [TestFixture]
    public class TestRaPanel : TestBase
    {
        protected override string Url
        {
            get { return "RaPanel.aspx"; }
        }

        [NUnit.Framework.Test]
        public void InitialRendering()
        {
            Assert.IsTrue(Browser.Div("pnl1").InnerHtml.IndexOf("testing rendering of panel") != -1);
        }

        [NUnit.Framework.Test]
        public void MakePanelVisible()
        {
            Assert.IsTrue(Browser.Span("pnlInvisible").Exists);
            Browser.Eval("verifyPanelDoesntExist();");
            AssertSuccess("Panel existed before it was supposed to");
            Browser.Button("btnMakeVisible").Click();
            Assert.IsTrue(Browser.Div("pnlInvisible").InnerHtml.IndexOf("Panel was visible, howdie's are cool") != -1);
            Browser.Eval("verifyPanelDoesExist();");
            AssertSuccess("Panel did NO exist when it was supposed to");
        }
    }
}
















