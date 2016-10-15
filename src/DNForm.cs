using System;
using System.Diagnostics;
using System.Windows.Forms;

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

        //Start default browser using the latest item from the feed, go to Dumpert homepage if not available
        private void _notifyIcon_BalloonTipClicked(object sender, EventArgs e)
        {
            Process.Start((_manager.GetFirstUrl() ?? _homepage).ToString());
        }

        //Shows a balloon tip with information when a new item arrives
        private void _timer_Tick(object sender, EventArgs e)
        {
            var item = _manager.GetFirstItemFromFeed();
            if (item == null)
            {
                ShowConnectionInterruptedAlert();
                return;
            }

            var lastUpdated = item.PublishDate.DateTime;
            if (lastUpdated > _startTime) return;
            _startTime = lastUpdated;

            _notifyIcon.ShowBalloonTip(5000, "Nieuw filmpje!",
                string.Format("{0}\n{1}\n{2}", item.Title.Text, item.Summary.Text, lastUpdated.ToShortTimeString()),
                ToolTipIcon.Info);
        }

        private void ShowConnectionInterruptedAlert()
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