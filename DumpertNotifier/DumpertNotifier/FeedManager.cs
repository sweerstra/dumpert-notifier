using System;
using System.Linq;
using System.ServiceModel.Syndication;
using System.Xml;

namespace DumpertNotifier
{
    public class FeedManager
    {
        public readonly Uri RssFeedUrl = new Uri("http://www.dumpert.nl/rss.xml.php");

        //Returns the first available Uri from a item or null if no valid Uri was found
        public Uri GetFirstUrl()
        {
            return GetFirstItemFromFeed().Links
                .Select(link => link.Uri)
                .FirstOrDefault();
        }

        //Returns the latest syndication item
        public SyndicationItem GetFirstItemFromFeed()
        {
            return GetFeed(RssFeedUrl).Items
                .OrderByDescending(date => date.PublishDate)
                .FirstOrDefault();
        }

        //Reads the feed from url and returns it, else spoof
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