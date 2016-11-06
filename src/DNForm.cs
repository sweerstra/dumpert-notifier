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

        private void _notifyIcon_BalloonTipClicked(object sender, EventArgs e)
        {
            Process.Start((_manager.GetFirstUrl() ?? _homepage).ToString());
        }

        private void _timer_Tick(object sender, EventArgs e)
        {
            var item = _manager.GetFirstItemFromFeed();
            if (item == null)
            {
                _notifyIcon.ShowBalloonTip(8000, "Verbinding verbroken!",
                    "Dumpert Notifier heeft geen internet verbinding kunnen vinden. Probeer opnieuw verbinding te maken en herstart de applicatie.",
                    ToolTipIcon.Error);
                Close();
                return;
            }

            var lastUpdated = item.PublishDate.DateTime;
            if (lastUpdated <= _startTime) return;

            var amount = _manager.GetNewItemAmount(_startTime);

            _startTime = lastUpdated;

            _notifyIcon.ShowBalloonTip(5000, (amount == 1) ? "Nieuw filmpje!" : $"{amount} nieuwe filmpjes!",
                $"{item.Title.Text}\n{item.Summary.Text}\n{_startTime.ToShortTimeString()}",
                ToolTipIcon.Info);
        }

        private void quitMenuItem_Click(object sender, EventArgs e)
        {
            Close();
        }
    }
}