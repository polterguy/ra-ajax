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

        [NUnit.Framework.Test]
        public void ToggleStyleOfCheckBox()
        {
            Browser.Button("btnToggleStyle").Click();
            Browser.Eval("verifyTogglingOfStylesON();");
            AssertSuccess("CheckBox wasn't set to visible correct");

            Browser.Button("btnToggleStyle").Click();
            Browser.Eval("verifyTogglingOfStylesOFF();");
            AssertSuccess("CheckBox wasn't set to visible correct");

            Browser.Button("btnToggleStyle").Click();
            Browser.Eval("verifyTogglingOfStylesON();");
            AssertSuccess("CheckBox wasn't set to visible correct");

            Browser.Button("btnToggleStyle").Click();
            Browser.Eval("verifyTogglingOfStylesOFF();");
            AssertSuccess("CheckBox wasn't set to visible correct");

            Browser.Button("btnToggleStyle").Click();
            Browser.Eval("verifyTogglingOfStylesON();");
            AssertSuccess("CheckBox wasn't set to visible correct");

        }

        // TODO: Doesn't work, probably IE8...
        //[NUnit.Framework.Test]
        //public void AccessKeyOfCheckBox()
        //{
        //    Browser.Eval("verifyAccessKeyForCheckBox();");
        //    AssertSuccess("CheckBox didn't have AccessKey working");
        //}

        // TODO: Doesn't work, probably IE8...
        //[NUnit.Framework.Test]
        //public void ToggleDisabledCheckBox()
        //{
        //    Browser.Eval("verifyCheckBoxDisabled();");
        //    AssertSuccess("CheckBox was enabled when shouldn't");

        //    Browser.Button("btnEnabledCheckBox").Click();

        //    Browser.Eval("verifyCheckBoxEnabled();");
        //    AssertSuccess("CheckBox was disabled when shouldn't");

        //    Browser.Button("btnEnabledCheckBox").Click();

        //    Browser.Eval("verifyCheckBoxDisabled();");
        //    AssertSuccess("CheckBox was enabled when shouldn't");

        //    Browser.Button("btnEnabledCheckBox").Click();

        //    Browser.Eval("verifyCheckBoxEnabled();");
        //    AssertSuccess("CheckBox was disabled when shouldn't");
        //}
    }
}
















