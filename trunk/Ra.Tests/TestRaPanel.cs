using System;
using System.Threading;
using NUnit.Framework;

namespace Ra.Tests.Ajax
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

        [NUnit.Framework.Test]
        public void MakePanelINVisible()
        {
            Assert.IsTrue(Browser.Div("pnlVisible").Exists);
            Browser.Eval("verifyPanel2DoesExist();");
            AssertSuccess("Panel didn't exist when it was supposed to");
            Browser.Button("btnMakeINVisible").Click();
            Assert.IsTrue(Browser.Span("pnlVisible").Exists);
            Browser.Eval("verifyPanel2DoesntExist();");
            AssertSuccess("Panel did exist when it was NOT supposed to");
        }

        [NUnit.Framework.Test]
        public void TogglePanel()
        {
            Assert.IsTrue(Browser.Div("pnlToggle").Exists);
            Browser.Eval("verifyPanel3DoesExist();");
            AssertSuccess("Panel didn't exist when it was supposed to");
            Browser.Button("btnToggle").Click();
            Assert.IsTrue(Browser.Span("pnlToggle").Exists);
            Browser.Eval("verifyPanel3DoesntExist();");
            AssertSuccess("Panel did exist when it was NOT supposed to");

            Browser.Button("btnToggle").Click();
            Assert.IsTrue(Browser.Div("pnlToggle").Exists);
            Browser.Eval("verifyPanel3DoesExist();");
            AssertSuccess("Panel didn't exist when it was supposed to");

            Browser.Button("btnToggle").Click();
            Assert.IsTrue(Browser.Span("pnlToggle").Exists);
            Browser.Eval("verifyPanel3DoesntExist();");
            AssertSuccess("Panel did exist when it was NOT supposed to");

            Browser.Button("btnToggle").Click();
            Assert.IsTrue(Browser.Div("pnlToggle").Exists);
            Browser.Eval("verifyPanel3DoesExist();");
            AssertSuccess("Panel didn't exist when it was supposed to");

            Browser.Button("btnToggle").Click();
            Assert.IsTrue(Browser.Span("pnlToggle").Exists);
            Browser.Eval("verifyPanel3DoesntExist();");
            AssertSuccess("Panel did exist when it was NOT supposed to");
        }

        [NUnit.Framework.Test]
        public void ClickButtonInPanel()
        {
            Browser.Button("pnlControls_btnTest").Click();
            Assert.AreEqual("clicked", Browser.Button("pnlControls_btnTest").Text);
        }

        [NUnit.Framework.Test]
        public void ShowPanelAndClickButtonInside()
        {
            Browser.Button("setPnlVisible").Click();
            Browser.Button("pnlControlsINVisible_btnTestINVisible").Click();
            Assert.AreEqual("clicked", Browser.Button("pnlControlsINVisible_btnTestINVisible").Text);
        }

        [NUnit.Framework.Test]
        public void ShowRecursivePanels()
        {
            Browser.Button("btnShowPnlRec").Click();
            Browser.Button("btnShowPnlRec").Click();
            Browser.Button("pnlRec1_pnlRec2_btnRec1").Click();
            Assert.AreEqual("clicked", Browser.Button("pnlRec1_pnlRec2_btnRec1").Text);
        }

        [NUnit.Framework.Test]
        public void ShowRecursivePanelsWhereInnerVisible()
        {
            Browser.Button("btnShowPnlRec2").Click();
            Browser.Button("pnlRec3_pnlRec4_btnRec2").Click();
            Assert.AreEqual("clicked", Browser.Button("pnlRec3_pnlRec4_btnRec2").Text);

            Browser.Eval("verifyOnlyOneDOMElInsidePanel();");
            AssertSuccess("Panel did not render correctly when made visible");

            // Now making INvisible again (and verify controls are destroyed)
            Browser.Button("btnShowPnlRec2").Click();
            Browser.Eval("verifyPanelInnerAndButtonDestroyed();");
            AssertSuccess("Panel did not render correctly when made visible");
        }

        [NUnit.Framework.Test]
        public void ChangeStyleOfPanel()
        {
            Browser.Button("btnSetPnlStyle").Click();
            Browser.Eval("verifyPanelStyle();");
            AssertSuccess("Panel did not change style correctly");
        }

        [NUnit.Framework.Test]
        public void ChangeStyleThenMakeVisible()
        {
            Browser.Button("btnChangeStyleBeforeVisible").Click();
            Browser.Button("btnSetVisibleAfterStyleChange").Click();
            Browser.Eval("verifyPanelStyleAfterVisible();");
            AssertSuccess("Style serialization of invisible controls doesn't work correctly");
        }

        [NUnit.Framework.Test]
        public void VerifyNoDoubleEventsOnRecursivePanelsAndButton()
        {
            Browser.Button("pnlRecVisible1_pnlRecVisible2_btnRecVisible").Click();
            Assert.AreEqual("Recursive Changes Textx", Browser.Button("pnlRecVisible1_pnlRecVisible2_btnRecVisible").Text);
        }
    }
}
















