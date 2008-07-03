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

        [NUnit.Framework.Test]
        public void ChangeValueOfLabel()
        {
            Browser.Button("textChangeLabelValue").Click();
            Assert.AreEqual("New value", Browser.Span("testChangeValue").Text);
        }

        [NUnit.Framework.Test]
        public void VerifyAccessKeyWorks()
        {
            Browser.Eval("verifyAccessKeyWorks();");
            Assert.AreEqual("New value", Browser.Span("testChangeValue").Text);
        }

        [NUnit.Framework.Test]
        public void ChangeValueOfTextBox()
        {
            Browser.Button("testChangeTextBoxValue").Click();
            Assert.AreEqual("New text", Browser.TextField("txtBox").Text);
        }

        [NUnit.Framework.Test]
        public void ChangeValueOfTextBoxToComplexValue()
        {
            Browser.Button("changeToComplexValue").Click();
            Browser.Button("verifyComplexValue").Click();
            Assert.AreEqual("success", Browser.Button("verifyComplexValue").Text);
        }

        [NUnit.Framework.Test]
        public void ChangeValueOfTextAreaAndCheckLoadPostBackData()
        {
            Browser.Button("testTextArea").Click();
            Assert.AreEqual("success1", Browser.TextField("textArea").Text);
            Browser.Button("testTextArea2").Click();
            Assert.AreEqual("success2", Browser.TextField("textArea").Text);
            Browser.TextField("textArea").Value = "changed";
            Browser.Button("testTextArea3").Click();
            Assert.AreEqual("success", Browser.TextField("textArea").Text);
        }

        [NUnit.Framework.Test]
        public void ChangeRowsAndColsOfTextArea()
        {
            Browser.Button("changeColsRowsOfTextArea").Click();
            Browser.Eval("verifyColsAndRowsOfTextAreaWasChanged();");
        }

        [NUnit.Framework.Test]
        public void VerifyValueOfPasswordIsCorrect()
        {
            Browser.Button("testPassword").Click();
            Assert.AreEqual("success", Browser.Button("testPassword").Text);
        }

        [NUnit.Framework.Test]
        public void CheckPasswordValueChanges()
        {
            Browser.Button("testPassword2").Click();
            Browser.Eval("verifyPasswordValueChanged();");
            AssertSuccess("Password value didn't change");
        }

        [NUnit.Framework.Test]
        public void CheckImageButtonsValuesChanged()
        {
            Browser.Button("imgBtn").Click();
            Browser.Eval("verifyImageButtonUpdated();");
            AssertSuccess("Image button values wasnt serialized");
        }

        [NUnit.Framework.Test]
        public void CheckDefaultSerializationOfDropDownList()
        {
            Browser.Eval("verifyDropDownListInitiallySerializedCorrect();");
            AssertSuccess("DropDownList didn't render initial HTML correct");
        }

        [NUnit.Framework.Test]
        public void DeleteFromDropDownList()
        {
            Browser.Button("deleteFromDDL").Click();
            Browser.Eval("verifyAfterDelete1();");
            AssertSuccess("Deleting items from DropDownList didn't work");

            Browser.Button("deleteFromDDL").Click();
            Browser.Eval("verifyAfterDelete2();");
            AssertSuccess("Deleting items from DropDownList didn't work");

            Browser.Button("submitFromDeletedDDL").Click();
            Assert.AreEqual("success", Browser.Button("submitFromDeletedDDL").Value);
        }

        [NUnit.Framework.Test]
        public void ChangeSelectedValueOfDDLFromButton()
        {
            Browser.Button("selectNewDDLValue").Click();
            Assert.AreEqual("Text of fourth", Browser.SelectList("dropDownListCallback").SelectedItem);
        }

        [NUnit.Framework.Test]
        public void ChangeSelectedValueOfDDLFromDDLChangeEvent()
        {
            Browser.SelectList("dropDownListCallback").SelectByValue("valueOfFirst");
            Assert.AreEqual("success", Browser.Button("selectNewDDLValue").Value);
        }

        [NUnit.Framework.Test]
        public void DisabledEnabledDDL()
        {
            Browser.Button("disabledDDL").Click();
            Assert.AreEqual(false, Browser.SelectList("testDisabledDDL").Enabled);

            Browser.Button("enabledDDL").Click();
            Assert.AreEqual(true, Browser.SelectList("testDisabledDDL").Enabled);

        }

        [NUnit.Framework.Test]
        public void DisableButton()
        {
            Assert.AreEqual(true, Browser.Button("disableButton").Enabled);
            Browser.Button("disableButton").Click();
            Assert.AreEqual(false, Browser.Button("disableButton").Enabled);
        }

        [NUnit.Framework.Test]
        public void DisableTextBox()
        {
            Assert.AreEqual(true, Browser.TextField("disabledTextBox").Enabled);
            Browser.Button("willDisableTextBox").Click();
            Assert.AreEqual(false, Browser.TextField("disabledTextBox").Enabled);
            Assert.AreEqual("is now disabled", Browser.TextField("disabledTextBox").Value);
        }

        [NUnit.Framework.Test]
        public void DisableTextArea()
        {
            Assert.AreEqual(true, Browser.TextField("disabledTextArea").Enabled);
            Browser.Button("willDisableTextArea").Click();
            Assert.AreEqual(false, Browser.TextField("disabledTextArea").Enabled);
            Assert.AreEqual("is now disabled", Browser.TextField("disabledTextArea").Value);
        }

        [NUnit.Framework.Test]
        public void VerifyControlsRendersInitiallyDisabled()
        {
            Assert.AreEqual(false, Browser.Button("btnDisabled").Enabled);
            Assert.AreEqual(false, Browser.TextField("txtDisabled").Enabled);
            Assert.AreEqual(false, Browser.TextField("txtAreaDisabled").Enabled);
            Assert.AreEqual(false, Browser.SelectList("ddlDisabled").Enabled);
            Assert.AreEqual(false, Browser.Button("imgDisabled").Enabled);
        }

        //[NUnit.Framework.Test]
        //public void VerifyDisabledControlsArentPassed()
        //{
        //    Browser.Button("verifyDisabledControlsDoesnPass").Click();
        //    Assert.AreEqual("success", Browser.Button("verifyDisabledControlsDoesnPass").Value);
        //}

        // TODO: Figure out how to test this...
        //[NUnit.Framework.Test]
        //public void ChangeValueOfTextBoxAndCallback()
        //{
        //    Browser.TextField("testCallBack").Focus();
        //    Browser.TextField("testCallBack").Value = "testing value";

        //    // Need to remove focus...
        //    Browser.TextField("testCallBack").

        //    Assert.AreEqual("After change", Browser.TextField("testCallBack").Text);
        //}
    }
}
















