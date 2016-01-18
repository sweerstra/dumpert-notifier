using System;
using System.Diagnostics;
using System.Drawing;
using System.Linq;
using System.ServiceModel.Syndication;
using System.Timers;
using System.Windows.Forms;
using System.Xml;
using DumpertNotifier.Properties;
using Timer = System.Timers.Timer;

namespace DumpertNotifier
{
    public partial class DNForm : Form
    {
        private readonly Timer _timer = new Timer(10000);
        private DateTime _startTime;
        private readonly NotifyIcon _notifyIcon;
        private readonly ToolStripComboBox _refreshComboBox;

        public DNForm()
        {
            InitializeComponent();

            _notifyIcon = new NotifyIcon {Icon = new Icon("dumpert-favicon.ico"), Visible = true};

            _refreshComboBox = new ToolStripComboBox
            {
                AutoSize = false,
                ToolTipText = @"Dumpert wordt ververst om dit aantal seconden",
                DropDownStyle = ComboBoxStyle.DropDownList
            };

            for (var i = 10; i <= 60; i += 10)
            {
                _refreshComboBox.Items.Add(i);
            }
            _refreshComboBox.Text = @"10";

            var preferencesItem = new ToolStripMenuItem("Instellingen")
            {
                Image = Resources.preferences_drawn.ToBitmap(),
                DropDownItems = {_refreshComboBox}
            };

            var quitItem = new ToolStripMenuItem("Rot op")
            {
                Image = Resources.cross.ToBitmap()
            };

            _notifyIcon.ContextMenuStrip = new ContextMenuStrip
            {
                Items = {preferencesItem, new ToolStripSeparator(), quitItem}
            };

            _refreshComboBox.Width = 40;

            _notifyIcon.MouseDoubleClick += _notifyIcon_BalloonTipClicked;
            _refreshComboBox.TextChanged += refreshComboBox_TextChanged;
            quitItem.Click += quitMenuItem_Click;
            _notifyIcon.BalloonTipClicked += _notifyIcon_BalloonTipClicked;

            WindowState = FormWindowState.Minimized;
            ShowInTaskbar = false;

            _startTime = DateTime.Now;

            _timer.Elapsed += OnDumpertTimedEvent;
            _timer.Enabled = true;
        }

        private void refreshComboBox_TextChanged(object sender, EventArgs e)
        {
            _timer.Interval = Convert.ToInt32(_refreshComboBox.SelectedText)*1000;
        }

        private static void _notifyIcon_BalloonTipClicked(object sender, EventArgs e)
        {
            var dumpert = new Uri("http://www.dumpert.nl/");
            var dumpertSource = new Uri("http://www.dumpert.nl/rss.xml.php");
            Process.Start(GetSource(dumpertSource, dumpert));
        }

        public static SyndicationFeed GetFeed(Uri uri)
        {
            Uri uriResult;
            if (!Uri.TryCreate(uri.AbsoluteUri, UriKind.Absolute, out uriResult) ||
                uriResult.Scheme != Uri.UriSchemeHttp)
            {
                return null;
            }
            try
            {
                var reader = XmlReader.Create(uri.AbsoluteUri);
                var feed = SyndicationFeed.Load(reader);
                reader.Close();
                return feed;
            }
            catch (ArgumentNullException)
            {
                MessageBox.Show(string.Format(@"Not able to get data from '{0}'", uri.AbsoluteUri));
                return null;
            }
        }

        private static string GetSource(Uri feedUri, Uri standard)
        {
            var feed = GetFeed(feedUri);
            var first = feed.Items.FirstOrDefault();
            if (first == null || first.Links.Count <= 0) return standard.AbsoluteUri;
            return first.Links.First().Uri.ToString();
        }

        private void OnDumpertTimedEvent(object source, ElapsedEventArgs e)
        {
            var lastItem = GetItem();
            var lastUpdated = lastItem.PublishDate.DateTime;
            if (lastUpdated <= _startTime)
            {
                return;
            }
            _startTime = lastUpdated;
            _notifyIcon.ShowBalloonTip(5000, ""
                , string.Format("{0}\n{1}\n{2}"
                    , lastItem.Title.Text
                    , lastItem.Summary.Text
                    , lastUpdated.ToShortTimeString())
                , ToolTipIcon.Info);
        }

        public SyndicationItem GetItem()
        {
            var dumpertSource = new Uri("http://www.dumpert.nl/rss.xml.php");
            var feed = GetFeed(dumpertSource);
            return feed
                .Items
                .OrderByDescending(date => date.PublishDate)
                .FirstOrDefault();
        }

        private void quitMenuItem_Click(object sender, EventArgs e)
        {
            _notifyIcon.Dispose();
            Process.GetCurrentProcess().Kill();
            Application.Exit();
        }
    }
}