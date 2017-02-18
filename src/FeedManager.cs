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

        public List<SyndicationItem> GetUpdatedItems(DateTime currentTime)
        {
            return GetFeed().Items
                .OrderByDescending(date => date.PublishDate)
                .Where(item => item.PublishDate.DateTime > currentTime)
                .ToList();
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