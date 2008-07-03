using System;
using System.Threading;
using NUnit.Framework;

namespace NUnitTests
{
    [TestFixture]
    public class TestControlsComplexSingle : TestBase
    {
        protected override string Url
        {
            get { return "RaControlsComplexSingle.aspx"; }
        }

        // TODO: Clicking checkbox doesn't work...
        // Probably bug due to IE8...
        //[NUnit.Framework.Test]
        //public void ClickCheckBox()
        //{
        //    Browser.CheckBox("chk_CTRL").Click();
        //    Assert.AreEqual("New text", Browser.Label("chk_LBL").Text);
        //}

        [NUnit.Framework.Test]
        public void SetCheckBoxToInVisible()
        {
            Browser.Button("setChkToInvisible").Click();
            Browser.Eval("verifyCheckBoxInVisible();");
            AssertSuccess("CheckBox wasn't set to invisible correct");
        }

        [NUnit.Framework.Test]
        public void SetCheckBoxToVisible()
        {
            Browser.Button("btnSetChkVisible").Click();
            Browser.Eval("verifyCheckBoxVisible();");
            AssertSuccess("CheckBox wasn't set to visible correct");
        }

        [NUnit.Framework.Test]
        public void ToggleCheckBoxMultipleTimes()
        {
            Browser.Button("btnToggleChk").Click();
            Browser.Eval("verifyToggleCheckBoxInVisible();");
            AssertSuccess("CheckBox wasn't set to visible correct");

            Browser.Button("btnToggleChk").Click();
            Browser.Eval("verifyToggleCheckBoxVisible();");
            AssertSuccess("CheckBox wasn't set to visible correct");

            Browser.Button("btnToggleChk").Click();
            Browser.Eval("verifyToggleCheckBoxInVisible();");
            AssertSuccess("CheckBox wasn't set to visible correct");

            Browser.Button("btnToggleChk").Click();
            Browser.Eval("verifyToggleCheckBoxVisible();");
            AssertSuccess("CheckBox wasn't set to visible correct");

            Browser.Button("btnToggleChk").Click();
            Browser.Eval("verifyToggleCheckBoxInVisible();");
            AssertSuccess("CheckBox wasn't set to visible correct");

            Browser.Button("btnToggleChk").Click();
            Browser.Eval("verifyToggleCheckBoxVisible();");
            AssertSuccess("CheckBox wasn't set to visible correct");

        }

        [NUnit.Framework.Test]
        public void ChangeStyleOfCheckBox()
        {
            Browser.Button("btnChangeChkStyle").Click();
            Browser.Eval("verifyStylesChanged();");
            AssertSuccess("CheckBox didn't change style correct");
        }
    }
}
















