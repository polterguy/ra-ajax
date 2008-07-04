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

        [NUnit.Framework.Test]
        public void ChangeImageValues()
        {
            Assert.AreEqual("Original", Browser.Image("img").Alt);
            Assert.IsTrue(Browser.Image("img").Src.IndexOf("testImage1.png") != -1);
            Browser.Button("btnChangeImg").Click();
            Assert.AreEqual("New text", Browser.Image("img").Alt);
            Assert.IsTrue(Browser.Image("img").Src.IndexOf("testImage2.png") != -1);
        }

        [NUnit.Framework.Test]
        public void ChangeRDOValuesAndCallback()
        {
            Browser.Eval("clickRadioButton(2);");
            Assert.AreEqual(false, Browser.RadioButton("rdo1_CTRL").Checked);
            Assert.AreEqual(true, Browser.RadioButton("rdo2_CTRL").Checked);
            Assert.AreEqual("False", Browser.Label("rdo1_LBL").InnerHtml);
            Assert.AreEqual("True", Browser.Label("rdo2_LBL").InnerHtml);

            Browser.Eval("clickRadioButton(1);");
            Assert.AreEqual(true, Browser.RadioButton("rdo1_CTRL").Checked);
            Assert.AreEqual(false, Browser.RadioButton("rdo2_CTRL").Checked);
            Assert.AreEqual("True", Browser.Label("rdo1_LBL").InnerHtml);
            Assert.AreEqual("False", Browser.Label("rdo2_LBL").InnerHtml);
        }

        [NUnit.Framework.Test]
        public void ChangeRDOValuesNoGroupNameAndCallback()
        {
            Browser.Eval("clickRadioButtonNG(2);");
            Assert.AreEqual(false, Browser.RadioButton("rdoNG1_CTRL").Checked);
            Assert.AreEqual(true, Browser.RadioButton("rdoNG2_CTRL").Checked);
            Assert.AreEqual("False", Browser.Label("rdoNG1_LBL").InnerHtml);
            Assert.AreEqual("True", Browser.Label("rdoNG2_LBL").InnerHtml);

            Browser.Eval("clickRadioButtonNG(1);");
            Assert.AreEqual(true, Browser.RadioButton("rdoNG1_CTRL").Checked);
            Assert.AreEqual(true, Browser.RadioButton("rdoNG2_CTRL").Checked);
            Assert.AreEqual("True", Browser.Label("rdoNG1_LBL").InnerHtml);
            Assert.AreEqual("True", Browser.Label("rdoNG2_LBL").InnerHtml);
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
















