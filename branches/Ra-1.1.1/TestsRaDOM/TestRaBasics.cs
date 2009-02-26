using System;
using System.Threading;
using NUnit.Framework;

namespace NUnitTests
{
    [TestFixture]
    public class TestRaBasics : TestBase
    {
        protected override string Url
        {
            get { return "RaDOMBasics.aspx"; }
        }

        [NUnit.Framework.Test]
        public void RaNamespace()
        {
            Browser.Eval("checkForRa();");
            AssertSuccess("Ra namespace doesn't exists");
        }

        [NUnit.Framework.Test]
        public void Dollar()
        {
            Browser.Eval("checkForRaDollar();");
            AssertSuccess("Ra $ doesn't work");
        }

        [NUnit.Framework.Test]
        public void CreateClass()
        {
            Browser.Eval("checkCreateClass();");
            AssertSuccess("Ra classCreate doesn't work");
        }

        [NUnit.Framework.Test]
        public void ExtendExists()
        {
            Browser.Eval("checkExtend();");
            AssertSuccess("Ra extend doesn't work");
        }

        [NUnit.Framework.Test]
        public void ExtendWorksSimple()
        {
            Browser.Eval("checkExtendFunctionalSimple();");
            AssertSuccess("Ra extend doesn't work");
        }

        [NUnit.Framework.Test]
        public void ExtendWorksSimpleMethod()
        {
            Browser.Eval("checkExtendFunctionalSimpleMethod();");
            AssertSuccess("Ra extend doesn't work");
        }

        [NUnit.Framework.Test]
        public void ExtendWorksSimpleMethodInvoke()
        {
            Browser.Eval("checkExtendFunctionalMethodInvoke();");
            AssertSuccess("Ra extend doesn't work");
        }

        [NUnit.Framework.Test]
        public void ExtendWorksPrototypeInstance()
        {
            Browser.Eval("checkExtendFunctionalMethodPrototype();");
            AssertSuccess("Ra extend doesn't work");
        }

        [NUnit.Framework.Test]
        public void ExtendWorksPrototypeInstanceInitWithArgs()
        {
            Browser.Eval("checkExtendFunctionalMethodPrototypeWithInitArguments();");
            AssertSuccess("Ra extend doesn't work");
        }

        [NUnit.Framework.Test]
        public void ExtendWorksPrototypeInstanceInitWithMultipleArgs()
        {
            Browser.Eval("checkExtendFunctionalMethodPrototypeWithMultipleInitArguments();");
            AssertSuccess("Ra extend doesn't work");
        }

        [NUnit.Framework.Test]
        public void ExtendWithThisCheck()
        {
            Browser.Eval("checkExtendMethodPrototypeWithThisArgument();");
            AssertSuccess("Ra extend doesn't work");
        }

        [NUnit.Framework.Test]
        public void ExtendWithInheritanceOverride()
        {
            Browser.Eval("checkExtendInheritanceOverride();");
            AssertSuccess("Ra extend doesn't work");
        }

        [NUnit.Framework.Test]
        public void ElementExtended()
        {
            Browser.Eval("checkRaElementExtendWorks();");
            AssertSuccess("Ra extend doesn't work");
        }

        [NUnit.Framework.Test]
        public void ElementReplace()
        {
            Browser.Eval("checkRaElementReplaceWorks();");
            AssertSuccess("Ra.Element.replace doesn't work");
        }

        [NUnit.Framework.Test]
        public void ElementSetVisible()
        {
            Browser.Eval("checkRaElementReplaceWorks();");
            AssertSuccess("Ra.Element.setVisible doesn't work");
        }

        [NUnit.Framework.Test]
        public void Remove()
        {
            Browser.Eval("checkRemove();");
            AssertSuccess("Ra.Element.remove doesn't work");
        }

        [NUnit.Framework.Test]
        public void SetContent()
        {
            Browser.Eval("checkSetContent();");
            AssertSuccess("Ra.Element.setContent doesn't work");
        }

        [NUnit.Framework.Test]
        public void AddClassName()
        {
            Browser.Eval("checkAddClassName();");
            AssertSuccess("Ra.Element.addClassName doesn't work");
        }

        [NUnit.Framework.Test]
        public void RemoveClassName()
        {
            Browser.Eval("checkRemoveClassName();");
            AssertSuccess("Ra.Element.removeClassName doesn't work");
        }

        [NUnit.Framework.Test]
        public void Opacity()
        {
            Browser.Eval("checkOpacity();");
            AssertSuccess("Ra.Element.setOpacity/getOpacity doesn't work");
        }

        [NUnit.Framework.Test]
        public void FadeAndAppear()
        {
            Browser.Eval("testFadeAndAppear();");
            System.Threading.Thread.Sleep(2500);
            AssertSuccess("Ra.Element.Fade/Appear doesn't work");
        }

        [NUnit.Framework.Test]
        public void ElementObserve()
        {
            Browser.Button("evtTestBtn").Click();
            AssertSuccess("Ra.Element.observe doesn't work");
        }

        [NUnit.Framework.Test]
        public void ElementStopObserving()
        {
            Browser.Button("evtTestBtnPre").Click();
            Browser.Button("evtTestBtn2").Click();
            AssertSuccess("Ra.Element.stopObserving doesn't work");
        }

        [NUnit.Framework.Test]
        public void XHRBasics()
        {
            Browser.Button("testXHR").Click();
            AssertSuccess("Ra.XHR basics doesn't work");
        }

        [NUnit.Framework.Test]
        public void XHRSingleParameter()
        {
            Browser.Button("testXHRParams").Click();
            AssertSuccess("Ra.XHR basics doesn't work");
        }

        [NUnit.Framework.Test]
        public void FormSubmit()
        {
            Browser.Button("testFormCallback").Click();
            AssertSuccess("Ra.Form basics doesn't work");
        }

        [NUnit.Framework.Test]
        public void FormSubmitNoCallingContext()
        {
            Browser.Button("testFormCallback2").Click();
            AssertSuccess("Ra.Form basics doesn't work");
        }

        [NUnit.Framework.Test]
        public void FormSubmitWithError()
        {
            Browser.Button("testFormCallbackError").Click();
            AssertSuccess("Ra.Form submit with error doesn't work");
        }

        [NUnit.Framework.Test]
        public void FormSubmitParams1()
        {
            Browser.Button("testFormCallbackWithTextInputField").Click();
            AssertSuccess("Ra.Form submit with parameters doesn't work");
        }

        [NUnit.Framework.Test]
        public void FormSubmitParamsWeird()
        {
            Browser.Button("testFormCallbackWithWeirdTextInputField").Click();
            AssertSuccess("Ra.Form submit with parameters doesn't work");
        }

        [NUnit.Framework.Test]
        public void FormSubmitParamsMultiple()
        {
            Browser.Button("testFormCallbackMultiple").Click();
            AssertSuccess("Ra.Form submit with parameters doesn't work");
        }

        [NUnit.Framework.Test]
        public void MultipleQueuedAjaxRequests()
        {
            Browser.Button("testMultipleRequestsBtn").Click();
            System.Threading.Thread.Sleep(1000);
            AssertSuccess("Ra.Ajax with multiple queue requests doesn't work");
        }
    }
}
















