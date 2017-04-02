using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.Linq;
using System.ServiceModel.Syndication;
using System.Windows.Forms;

namespace DumpertNotifier
{
    public partial class DnForm : Form
    {
        private readonly FeedManager _manager;
        private DateTime _lastUpdate = DateTime.Now;
        private Uri _navigationUrl;

        public DnForm()
        {
            _manager = new FeedManager(new Uri("http://www.dumpert.nl/rss.xml.php"));
            InitializeComponent();
        }

        private void notifier_BalloonTipClicked(object sender, EventArgs e)
        {
            Process.Start(_navigationUrl.AbsoluteUri);
        }

        private void timer_Tick(object sender, EventArgs e)
        {
            IEnumerable<SyndicationItem> items = _manager.GetUpdatedItems(_lastUpdate).ToList();

            int count = items.Count();
            if (count == 0) return;

            SyndicationItem first = items.First();
            _lastUpdate = first.PublishDate.DateTime;
            _navigationUrl = _manager.GetFirstUrlFromItem(first);

            notifier.ShowBalloonTip(5000, count == 1 ? "Nieuw filmpje!" : $"{count} nieuwe filmpjes!",
                $"{first.Title.Text}\n{first.Summary.Text}\n{_lastUpdate.ToShortTimeString()}",
                ToolTipIcon.Info);

            notifier.Text = $@"Laatste filmpje: {_lastUpdate.ToShortTimeString()}";
        }

        private void quitMenuItem_Click(object sender, EventArgs e)
        {
            Close();
        }
    }
}