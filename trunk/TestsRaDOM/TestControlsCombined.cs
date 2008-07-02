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
        public void VerifyValueOfPasswordIsCorrect()
        {
            Browser.Button("testPassword").Click();
            Assert.AreEqual("success", Browser.Button("testPassword").Text);
        }

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
















