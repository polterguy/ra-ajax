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
    }
}
















