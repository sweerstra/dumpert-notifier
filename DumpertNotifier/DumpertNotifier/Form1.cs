using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.Drawing;
using System.IO;
using System.Linq;
using System.Net;
using System.ServiceModel.Syndication;
using System.Timers;
using System.Windows.Forms;
using System.Xml;
using DumpertNotifier.Properties;
using HtmlAgilityPack;
using Timer = System.Timers.Timer;

namespace DumpertNotifier
{
    public partial class Form1 : Form
    {
        private static readonly Uri DumpertSource = new Uri("http://www.dumpert.nl/rss.xml.php");
        private static readonly Uri Dumpert = new Uri("http://www.dumpert.nl/");
        private static string lastLink;
        private readonly Timer dumpertTimer = new Timer(10000);
        private DateTime startTimeDumpert;
        private readonly NotifyIcon notifyIcon;
        private readonly ToolStripComboBox refreshComboBox = new ToolStripComboBox
        {
            AutoSize = false,
            ToolTipText = @"Amount of seconds Dumpert is refreshed",
            DropDownStyle = ComboBoxStyle.DropDownList
        };

        public Form1()
        {
            InitializeComponent();

            //create notify icon
            notifyIcon = new NotifyIcon {Icon = new Icon("dumpert-favicon.ico"), Visible = true};

            for (var i = 10; i <= 60; i += 10)
            {
                refreshComboBox.Items.Add(i);
            }
            refreshComboBox.Text = @"10";

            //create context menu items
            var preferencesItem = new ToolStripMenuItem("Preferences...")
            {
                Image = Resources.preferences_drawn.ToBitmap(),
                DropDownItems = {refreshComboBox}
            };

            var kudosItem = new ToolStripMenuItem("Kudos")
            {
                Image = Resources.kudo.ToBitmap()
            };

            var quitItem = new ToolStripMenuItem("Quit")
            {
                Image = Resources.cross.ToBitmap()
            };

            notifyIcon.ContextMenuStrip = new ContextMenuStrip
            {
                Items = {preferencesItem, kudosItem, new ToolStripSeparator(), quitItem}
            };

            refreshComboBox.Width = 40;

            //make click events
            notifyIcon.MouseDoubleClick += _notifyIcon_BalloonTipClicked;
            refreshComboBox.TextChanged += refreshComboBox_TextChanged;
            kudosItem.Click += kudosItem_Click;
            quitItem.Click += quitMenuItem_Click;
            notifyIcon.BalloonTipClicked += _notifyIcon_BalloonTipClicked;

            //minimize window
            WindowState = FormWindowState.Minimized;
            ShowInTaskbar = false;

            startTimeDumpert = DateTime.Now;

            //initialize timer set interval to 10 seconds
            dumpertTimer.Elapsed += OnDumpertTimedEvent;
            dumpertTimer.Enabled = true;

        }

        void kudosItem_Click(object sender, EventArgs e)
        {
            Request();
        }

        private void refreshComboBox_TextChanged(object sender, EventArgs e)
        {
            dumpertTimer.Interval = Convert.ToInt32(refreshComboBox.SelectedText)*1000;
        }

        private static void _notifyIcon_BalloonTipClicked(object sender, EventArgs e)
        {
            Process.Start(GetSource(DumpertSource, Dumpert));
        }

        public void GetKudos()
        {
            //if (lastLink == null) return;
            var web = new HtmlWeb {UseCookies = true};

            //var doc = web.Load(lastLink);
            var doc = web.Load("http://www.dumpert.nl");

            String value = String.Empty;

            var firstOrDefault = doc.DocumentNode.SelectNodes("//section[@id='comments']").FirstOrDefault();
            if (firstOrDefault != null)
            {
                var node = firstOrDefault.InnerHtml;
                MessageBox.Show(node);
            }

            //notifyIcon.ShowBalloonTip(5000, String.Empty, string.Format("You have {0} kudos", value), ToolTipIcon.Info);
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
            lastLink = first.Links.First().Uri.ToString();
            return lastLink;
        }

        private void OnDumpertTimedEvent(object source, ElapsedEventArgs e)
        {
            var lastItem = GetItem();
            var lastUpdated = lastItem.PublishDate.DateTime;
            if (lastUpdated <= startTimeDumpert)
            {
                return;
            }
            startTimeDumpert = lastUpdated;
            notifyIcon.ShowBalloonTip(5000, "Dumpert"
                , string.Format("{0}\n{1}\n{2}"
                    , lastItem.Title.Text
                    , lastItem.Summary.Text
                    , lastUpdated.ToShortTimeString())
                , ToolTipIcon.Info);
        }

        public SyndicationItem GetItem()
        {
            var feed = GetFeed(DumpertSource);
            return feed
                .Items
                .OrderByDescending(date => date.PublishDate)
                .FirstOrDefault();
        }

        private void quitMenuItem_Click(object sender, EventArgs e)
        {
            notifyIcon.Dispose();
            Process.GetCurrentProcess().Kill();
            Application.Exit();
        }
    }
}