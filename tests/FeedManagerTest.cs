using System;
using System.Linq;
using Microsoft.VisualStudio.TestTools.UnitTesting;

namespace DumpertNotifier.Tests
{
    [TestClass]
    public class FeedManagerTest
    {
        private readonly Uri _feedUrl = new Uri("http://www.dumpert.nl/rss.xml.php");

        [TestMethod]
        public void GetFeedFromValidUrl()
        {
            var manager = new FeedManager(_feedUrl);
            var feed = manager.GetFeed();
            Assert.IsNotNull(feed.Items);
        }

        [TestMethod]
        public void GetItemsWithCorrectDateCheck()
        {
            var manager = new FeedManager(_feedUrl);
            var date = DateTime.Now.AddHours(-6);
            var check = manager.GetUpdatedItems(date)
                            .FirstOrDefault()?
                            .PublishDate.DateTime >= date;

            Assert.IsTrue(check, "Time given can't be before the item's publish date.");
        }

        [TestMethod]
        public void FeedHasExistingAndValidUrl()
        {
            var manager = new FeedManager(_feedUrl);
            var url = manager.GetFirstUrlFromItem(
                manager.GetUpdatedItems(DateTime.Now.AddDays(-1)).FirstOrDefault()
            );
            Assert.IsNotNull(url);

            var isValidUrl = url.AbsoluteUri.Contains("dumpert.nl/mediabase");
            Assert.IsTrue(isValidUrl, "Url was invalid and won't redirect to video.");
        }
    }
}