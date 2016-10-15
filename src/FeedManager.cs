using System;
using System.Linq;
using System.ServiceModel.Syndication;
using System.Xml;

namespace DumpertNotifier
{
    public class FeedManager
    {
        public readonly Uri RssFeedUrl = new Uri("http://www.dumpert.nl/rss.xml.php");

        public Uri GetFirstUrl()
        {
            return GetFirstItemFromFeed().Links
                .Select(link => link.Uri)
                .FirstOrDefault();
        }

        public SyndicationItem GetFirstItemFromFeed()
        {
            return GetFeed(RssFeedUrl).Items
                .OrderByDescending(date => date.PublishDate)
                .FirstOrDefault();
        }

        public SyndicationFeed GetFeed(Uri uri)
        {
            try
            {
                return SyndicationFeed.Load(XmlReader.Create(uri.AbsoluteUri));
            }
            catch (Exception)
            {
                return new SyndicationFeed();
            }
        }
    }
}