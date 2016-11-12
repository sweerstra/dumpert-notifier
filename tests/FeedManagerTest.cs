using System;
using System.Linq;
using Microsoft.VisualStudio.TestTools.UnitTesting;

namespace DumpertNotifier.Tests
{
    [TestClass]
    public class FeedManagerTest
    {
        private readonly FeedManager _manager = new FeedManager();

        [TestMethod]
        public void GetFeedFromValidUrl()
        {
            var feed = _manager.GetFeed(_manager.RssFeedUrl);
            Assert.IsNotNull(feed.Items);
        }

        [TestMethod]
        public void GetItemsWithCorrectDateCheck()
        {
            var date = DateTime.Now.AddHours(-6);

            var check = _manager.GetNewItems(date)
                            .FirstOrDefault()
                            ?.PublishDate.DateTime >= date;

            Assert.IsTrue(check, "Time given can't be before the item's publish date.");
        }

        [TestMethod]
        public void FeedHasExistingAndValidUrl()
        {
            var url = _manager.GetFirstUrlFromItem(
                _manager.GetNewItems(DateTime.Now.AddDays(-1))
                    .FirstOrDefault());

            Assert.IsNotNull(url);

            var isValidUrl = url.AbsoluteUri.Contains("dumpert.nl/mediabase");

            Assert.IsTrue(isValidUrl, "Url was invalid and won't redirect to video.");
        }
    }
}