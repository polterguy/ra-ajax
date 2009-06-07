using System;
using System.Threading;
using NUnit.Framework;
using Ra.Builder;
using System.IO;

namespace Ra.Tests.Builder
{
    [TestFixture]
    public class TestBuilder
    {
        [NUnit.Framework.Test]
        public void SimpleElement()
        {
            using (HtmlBuilder builder = new HtmlBuilder())
            {
                using (Element el = builder.CreateElement("div"))
                {
                }
                Assert.AreEqual("<div></div>", builder.ToString());
            }
        }

        [NUnit.Framework.Test]
        public void ElementWithContent()
        {
            using (HtmlBuilder builder = new HtmlBuilder())
            {
                using (Element el = builder.CreateElement("div"))
                {
                    builder.Writer.Write("howdy");
                }
                Assert.AreEqual("<div>howdy</div>", builder.ToString());
            }
        }

        [NUnit.Framework.Test]
        public void ElementWithMultipleContent()
        {
            using (HtmlBuilder builder = new HtmlBuilder())
            {
                using (Element el = builder.CreateElement("div"))
                {
                    builder.Writer.Write("howdy");
                    builder.Writer.Write("howdy");
                }
                Assert.AreEqual("<div>howdyhowdy</div>", builder.ToString());
            }
        }

        [NUnit.Framework.Test]
        public void ElementWithAttribute()
        {
            using (HtmlBuilder builder = new HtmlBuilder())
            {
                using (Element el = builder.CreateElement("div"))
                {
                    el.AddAttribute("attribute", "value");
                }
                Assert.AreEqual("<div attribute=\"value\"></div>", builder.ToString());
            }
        }

        [NUnit.Framework.Test]
        public void ElementWithMultipleAttributes()
        {
            using (HtmlBuilder builder = new HtmlBuilder())
            {
                using (Element el = builder.CreateElement("div"))
                {
                    el.AddAttribute("attribute", "value");
                    el.AddAttribute("howdy", "yahoo");
                }
                Assert.AreEqual("<div attribute=\"value\" howdy=\"yahoo\"></div>", builder.ToString());
            }
        }

        [NUnit.Framework.Test]
        public void ElementWithMultipleAttributesAndMultipleContent()
        {
            using (HtmlBuilder builder = new HtmlBuilder())
            {
                using (Element el = builder.CreateElement("div"))
                {
                    el.AddAttribute("attribute", "value");
                    el.AddAttribute("howdy", "yahoo");
                    builder.Writer.Write("howdy");
                    builder.Writer.Write("yahoo");
                }
                Assert.AreEqual("<div attribute=\"value\" howdy=\"yahoo\">howdyyahoo</div>", builder.ToString());
            }
        }

        [NUnit.Framework.Test]
        public void MultipleElements()
        {
            using (HtmlBuilder builder = new HtmlBuilder())
            {
                using (Element el = builder.CreateElement("div"))
                {
                }
                using (Element el = builder.CreateElement("span"))
                {
                }
                Assert.AreEqual("<div></div><span></span>", builder.ToString());
            }
        }

        [NUnit.Framework.Test]
        public void NestedElements()
        {
            using (HtmlBuilder builder = new HtmlBuilder())
            {
                using (Element el = builder.CreateElement("div"))
                {
                    using (Element el2 = builder.CreateElement("span"))
                    {
                    }
                }
                Assert.AreEqual("<div><span></span></div>", builder.ToString());
            }
        }

        [NUnit.Framework.Test]
        public void NestedElementsWithAttributes()
        {
            using (HtmlBuilder builder = new HtmlBuilder())
            {
                using (Element el = builder.CreateElement("div"))
                {
                    el.AddAttribute("howdy", "hello");
                    using (Element el2 = builder.CreateElement("span"))
                    {
                        el2.AddAttribute("thomas", "hansen");
                    }
                }
                Assert.AreEqual("<div howdy=\"hello\"><span thomas=\"hansen\"></span></div>", builder.ToString());
            }
        }

        [NUnit.Framework.Test]
        public void NestedElementsWithContent()
        {
            using (HtmlBuilder builder = new HtmlBuilder())
            {
                using (Element el = builder.CreateElement("div"))
                {
                    builder.Writer.Write("thomas");
                    using (Element el2 = builder.CreateElement("span"))
                    {
                        builder.Writer.Write("hansen");
                    }
                }
                Assert.AreEqual("<div>thomas<span>hansen</span></div>", builder.ToString());
            }
        }

        [NUnit.Framework.Test]
        public void NestedElementsWithContentAndAttributes()
        {
            using (HtmlBuilder builder = new HtmlBuilder())
            {
                using (Element el = builder.CreateElement("div"))
                {
                    el.AddAttribute("h", "a");
                    builder.Writer.Write("thomas");
                    using (Element el2 = builder.CreateElement("span"))
                    {
                        el2.AddAttribute("f", "g");
                        builder.Writer.Write("hansen");
                    }
                }
                Assert.AreEqual("<div h=\"a\">thomas<span f=\"g\">hansen</span></div>", builder.ToString());
            }
        }

        [NUnit.Framework.Test]
        public void NestedElementsWithContentAndAttributesUseElementWrite()
        {
            using (HtmlBuilder builder = new HtmlBuilder())
            {
                using (Element el = builder.CreateElement("div"))
                {
                    el.AddAttribute("h", "a");
                    el.Write("thomas");
                    using (Element el2 = builder.CreateElement("span"))
                    {
                        el2.AddAttribute("f", "g");
                        el2.Write("hansen");
                    }
                }
                Assert.AreEqual("<div h=\"a\">thomas<span f=\"g\">hansen</span></div>", builder.ToString());
            }
        }
    }
}
















