using System;
using MbUnit.Framework;
using Subtext.Web.SiteMap;

namespace UnitTests.Subtext.SubtextWeb
{
    [TestFixture]
    public class Sitemap
    {

        [Test]
        public void UrlCollectionAcceptsEntries()
        {
            UrlCollection urlCollection = new UrlCollection();
            urlCollection.Add(new Url(new Uri("http://someurl.com"), DateTime.Today, ChangeFrequency.Daily, 1));
            Assert.AreEqual(1, urlCollection.Count);
        }

        [Test]
        public void UrlClassStoresValues()
        {
            Url url = new Url(new Uri("http://someurl.com"), DateTime.MinValue, ChangeFrequency.Never, 0);
            StringAssert.AreEqualIgnoreCase("http://someurl.com/", url.Location);
            Assert.AreEqual(DateTime.MinValue, url.LastModified);
            Assert.AreEqual(ChangeFrequency.Never, url.ChangeFrequency);
            Assert.AreEqual(0, url.Priority);
        }

		//[Test]
		//public void UrlClassHtmlEncodesLocationWhenFillThroughConstructor()
		//{
		//    // using property
		//    Url url = new Url(new Uri("http://someurl.com"), DateTime.MinValue, ChangeFrequency.Never, 0);
		//    url.Location = "http://ščž.com";
		//    StringAssert.AreEqualIgnoreCase("http://ščž.com/", url.Location);
		//}

		//[Test]
		//[ExpectedException(typeof(ArgumentOutOfRangeException))]
		//public void UrlClassHtmlEncodesLocationWhenFillThroughProperty()
		//{        
		//    // using property
		//    Url url = new Url();
		//    url.Location = "http://someurl withspace.com";                       
		//}

        [Test]
        [ExpectedException(typeof(ArgumentOutOfRangeException))]
        public void UrlClassExpectesPriorityGreaterThenZero()
        {
            // using property
            Url url = new Url();
            url.Priority = -0.5M;            
        }
        
    }
}