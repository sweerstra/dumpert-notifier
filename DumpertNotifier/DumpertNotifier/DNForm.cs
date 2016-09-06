using System;
using System.Diagnostics;
using System.Linq;
using System.ServiceModel.Syndication;
using System.Windows.Forms;
using System.Xml;

namespace DumpertNotifier
{
    public partial class DnForm : Form
    {
        private readonly Uri _homepage = new Uri("http://www.dumpert.nl");
        private readonly FeedManager _manager;
        private DateTime _startTime = DateTime.Now;

        public DnForm()
        {
            _manager = new FeedManager();
            InitializeComponent();
        }

        //On balloon tip click, start default browser with new item link
        private void _notifyIcon_BalloonTipClicked(object sender, EventArgs e)
        {
            Process.Start((_manager.GetFirstUrl() ?? _homepage).ToString());
        }

        //Every 10 seconds, if item has a new publish date, show a balloon tip with information
        private void _timer_Tick(object sender, EventArgs e)
        {
            var item = _manager.GetFirstItemFromFeed();
            if (item != null)
            {
                var lastUpdated = item.PublishDate.DateTime;
                if (lastUpdated <= _startTime) return;

                _startTime = lastUpdated;
                _notifyIcon.ShowBalloonTip(5000, "Nieuw filmpje!",
                    string.Format("{0}\n{1}\n{2}", item.Title.Text, item.Summary.Text,
                        lastUpdated.ToShortTimeString()), ToolTipIcon.Info);
            }
            else
            {
                ConnectionInterrupted();
            }
        }

        private void ConnectionInterrupted()
        {
            _notifyIcon.ShowBalloonTip(8000, "Verbinding verbroken!",
                "Dumpert Notifier heeft geen internet verbinding kunnen vinden. Probeer opnieuw verbinding te maken en herstart de applicatie.",
                ToolTipIcon.Error);
            Close();
        }

        private void quitMenuItem_Click(object sender, EventArgs e)
        {
            Close();
        }
    }
}