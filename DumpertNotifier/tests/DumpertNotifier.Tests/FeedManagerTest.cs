using System;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using NUnit.Framework;
using Assert = NUnit.Framework.Assert;

namespace DumpertNotifier.Tests
{
    [TestClass]
    public class FeedManagerTest
    {
        private readonly FeedManager _manager = new FeedManager();

        [Test]
        public void ExistingAndValidUrl()
        {
            var url = _manager.GetFirstUrl();
            var correctUrl = url.AbsoluteUri.Contains("dumpert.nl/mediabase");
            Assert.IsNotNull(url);
            Assert.That(correctUrl, "Url was invalid and won't redirect to video.");
        }

        [Test]
        public void GetFirstItemFromValidFeed()
        {
            var item = _manager.GetFirstItemFromFeed();
            Assert.IsNotNull(item.PublishDate);
            Assert.That(item.PublishDate.DateTime < DateTime.Now,
                "DateTime was incorrectly formatted, item's publish date can't be after the current time.");
        }

        [Test]
        public void GetFeedFromValidUrl()
        {
            var feed = _manager.GetFeed(_manager.RssFeedUrl);
            Assert.IsNotNull(feed.Items);
        }

        [Test]
        public void NonexistingFeedUrl()
        {
            var exception = Assert.Catch(() => _manager.GetFeed(new Uri("www.invalidfeed.com")));
            Assert.IsInstanceOf<UriFormatException>(exception);
        }
    }
}