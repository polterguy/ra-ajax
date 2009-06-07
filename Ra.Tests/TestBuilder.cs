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
    }
}
















