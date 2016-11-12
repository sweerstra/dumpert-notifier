﻿using System;
using System.Collections.Generic;
using System.Linq;
using System.ServiceModel.Syndication;
using System.Xml;

namespace DumpertNotifier
{
    public class FeedManager
    {
        public readonly Uri RssFeedUrl = new Uri("http://www.dumpert.nl/rss.xml.php");

        public Uri GetFirstUrlFromItem(SyndicationItem item)
        {
            return item.Links
                .Select(link => link.Uri)
                .FirstOrDefault();
        }

        public List<SyndicationItem> GetNewItems(DateTime currentTime)
        {
            return GetFeed(RssFeedUrl).Items
                .OrderByDescending(date => date.PublishDate)
                .Where(item => item.PublishDate.DateTime > currentTime)
                .ToList();
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