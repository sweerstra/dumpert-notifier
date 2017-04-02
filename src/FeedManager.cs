using System;
using System.Collections.Generic;
using System.Linq;
using System.ServiceModel.Syndication;
using System.Xml;

namespace DumpertNotifier
{
    public class FeedManager
    {
        public Uri Url { get; set; }

        public FeedManager(Uri url)
        {
            Url = url;
        }

        public Uri GetFirstUrlFromItem(SyndicationItem item)
        {
            return item.Links
                .Select(link => link.Uri)
                .FirstOrDefault();
        }

        public IEnumerable<SyndicationItem> GetUpdatedItems(DateTime lastUpdate)
        {
            return GetFeed().Items
                .OrderByDescending(date => date.PublishDate)
                .Where(item => item.PublishDate.DateTime > lastUpdate);
        }

        public SyndicationFeed GetFeed()
        {
            try
            {
                return SyndicationFeed.Load(XmlReader.Create(Url.AbsoluteUri));
            }
            catch (Exception)
            {
                return new SyndicationFeed();
            }
        }
    }
}