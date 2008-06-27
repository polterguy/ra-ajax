using System;
using System.Threading;
using NUnit.Framework;

namespace TestsRaDOM
{
    [TestFixture]
    public class TestRaBasics : TestBase
    {
        protected override string Url
        {
            get { return "RaDOMBasics.aspx"; }
        }

        [NUnit.Framework.Test]
        public void TestRaNamespace()
        {
            Browser.Eval("checkForRa();");
            AssertSuccess("Ra namespace doesn't exists");
        }

        [NUnit.Framework.Test]
        public void TestDollar()
        {
            Browser.Eval("checkForRaDollar();");
            AssertSuccess("Ra $ doesn't work");
        }

        [NUnit.Framework.Test]
        public void TestCreateClass()
        {
            Browser.Eval("checkCreateClass();");
            AssertSuccess("Ra classCreate doesn't work");
        }

        [NUnit.Framework.Test]
        public void TestExtendExists()
        {
            Browser.Eval("checkExtend();");
            AssertSuccess("Ra extend doesn't work");
        }

        [NUnit.Framework.Test]
        public void TestExtendWorksSimple()
        {
            Browser.Eval("checkExtendFunctionalSimple();");
            AssertSuccess("Ra extend doesn't work");
        }

        [NUnit.Framework.Test]
        public void TestExtendWorksSimpleMethod()
        {
            Browser.Eval("checkExtendFunctionalSimpleMethod();");
            AssertSuccess("Ra extend doesn't work");
        }

        [NUnit.Framework.Test]
        public void TestExtendWorksSimpleMethodInvoke()
        {
            Browser.Eval("checkExtendFunctionalMethodInvoke();");
            AssertSuccess("Ra extend doesn't work");
        }

        [NUnit.Framework.Test]
        public void TestExtendWorksPrototypeInstance()
        {
            Browser.Eval("checkExtendFunctionalMethodPrototype();");
            AssertSuccess("Ra extend doesn't work");
        }

        [NUnit.Framework.Test]
        public void TestExtendWorksPrototypeInstanceInitWithArgs()
        {
            Browser.Eval("checkExtendFunctionalMethodPrototypeWithInitArguments();");
            AssertSuccess("Ra extend doesn't work");
        }

        [NUnit.Framework.Test]
        public void TestExtendWorksPrototypeInstanceInitWithMultipleArgs()
        {
            Browser.Eval("checkExtendFunctionalMethodPrototypeWithMultipleInitArguments();");
            AssertSuccess("Ra extend doesn't work");
        }

        [NUnit.Framework.Test]
        public void TestExtendWithThisCheck()
        {
            Browser.Eval("checkExtendMethodPrototypeWithThisArgument();");
            AssertSuccess("Ra extend doesn't work");
        }

        [NUnit.Framework.Test]
        public void TestExtendWithInheritanceOverride()
        {
            Browser.Eval("checkExtendInheritanceOverride();");
            AssertSuccess("Ra extend doesn't work");
        }

        [NUnit.Framework.Test]
        public void TestElementExtended()
        {
            Browser.Eval("checkRaElementExtendWorks();");
            AssertSuccess("Ra extend doesn't work");
        }

        [NUnit.Framework.Test]
        public void TestElementReplace()
        {
            Browser.Eval("checkRaElementReplaceWorks();");
            AssertSuccess("Ra.Element.replace doesn't work");
        }

        [NUnit.Framework.Test]
        public void TestElementSetVisible()
        {
            Browser.Eval("checkRaElementReplaceWorks();");
            AssertSuccess("Ra.Element.setVisible doesn't work");
        }

        [NUnit.Framework.Test]
        public void TestRemove()
        {
            Browser.Eval("checkRemove();");
            AssertSuccess("Ra.Element.remove doesn't work");
        }

        [NUnit.Framework.Test]
        public void TestSetContent()
        {
            Browser.Eval("checkSetContent();");
            AssertSuccess("Ra.Element.setContent doesn't work");
        }

        [NUnit.Framework.Test]
        public void TestGetWidth()
        {
            Browser.Eval("checkWidth();");
            AssertSuccess("Ra.Element.getWidth doesn't work");
        }

        [NUnit.Framework.Test]
        public void TestGetHeight()
        {
            Browser.Eval("checkHeight();");
            AssertSuccess("Ra.Element.getHeight doesn't work");
        }

        [NUnit.Framework.Test]
        public void TestSetHeight()
        {
            Browser.Eval("checkSetHeight();");
            AssertSuccess("Ra.Element.setHeight doesn't work");
        }

        [NUnit.Framework.Test]
        public void TestSetWidth()
        {
            Browser.Eval("checkSetWidth();");
            AssertSuccess("Ra.Element.setWidth doesn't work");
        }

        [NUnit.Framework.Test]
        public void TestAddClassName()
        {
            Browser.Eval("checkAddClassName();");
            AssertSuccess("Ra.Element.addClassName doesn't work");
        }

        [NUnit.Framework.Test]
        public void TestRemoveClassName()
        {
            Browser.Eval("checkRemoveClassName();");
            AssertSuccess("Ra.Element.removeClassName doesn't work");
        }

        [NUnit.Framework.Test]
        public void TestOpacity()
        {
            Browser.Eval("checkOpacity();");
            AssertSuccess("Ra.Element.setOpacity/getOpacity doesn't work");
        }

        [NUnit.Framework.Test]
        public void TestPositioning()
        {
            Browser.Eval("testPosition();");
            AssertSuccess("Ra.Element.setLeft/setTop/getLeft/getTop doesn't work");
        }

        [NUnit.Framework.Test]
        public void TestFadeAndAppear()
        {
            Browser.Eval("testFadeAndAppear();");
            System.Threading.Thread.Sleep(2500);
            AssertSuccess("Ra.Element.Fade/Appear doesn't work");
        }

        [NUnit.Framework.Test]
        public void TestElementObserve()
        {
            Browser.Button("evtTestBtn").Click();
            AssertSuccess("Ra.Element.observe doesn't work");
        }

        [NUnit.Framework.Test]
        public void TestElementStopObserving()
        {
            Browser.Button("evtTestBtnPre").Click();
            Browser.Button("evtTestBtn2").Click();
            AssertSuccess("Ra.Element.stopObserving doesn't work");
        }

        [NUnit.Framework.Test]
        public void TestXHRBasics()
        {
            Browser.Button("testXHR").Click();
            AssertSuccess("Ra.XHR basics doesn't work");
        }

        [NUnit.Framework.Test]
        public void TestXHRSingleParameter()
        {
            Browser.Button("testXHRParams").Click();
            AssertSuccess("Ra.XHR basics doesn't work");
        }

        [NUnit.Framework.Test]
        public void TestFormSubmit()
        {
            Browser.Button("testFormCallback").Click();
            AssertSuccess("Ra.Form basics doesn't work");
        }

        [NUnit.Framework.Test]
        public void TestFormSubmitNoCallingContext()
        {
            Browser.Button("testFormCallback2").Click();
            AssertSuccess("Ra.Form basics doesn't work");
        }

        [NUnit.Framework.Test]
        public void TestFormSubmitWithError()
        {
            Browser.Button("testFormCallbackError").Click();
            AssertSuccess("Ra.Form submit with error doesn't work");
        }
    }
}
















