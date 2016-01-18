namespace DumpertNotifier
{
    partial class DNForm
    {
        /// <summary>
        /// Required designer variable.
        /// </summary>
        private System.ComponentModel.IContainer components = null;

        /// <summary>
        /// Clean up any resources being used.
        /// </summary>
        /// <param name="disposing">true if managed resources should be disposed; otherwise, false.</param>
        protected override void Dispose(bool disposing)
        {
            if (disposing && (components != null))
            {
                components.Dispose();
            }
            base.Dispose(disposing);
        }

        #region Windows Form Designer generated code

        /// <summary>
        /// Required method for Designer support - do not modify
        /// the contents of this method with the code editor.
        /// </summary>
        private void InitializeComponent()
        {
            this.components = new System.ComponentModel.Container();
            System.ComponentModel.ComponentResourceManager resources = new System.ComponentModel.ComponentResourceManager(typeof(DNForm));
            this.comboBox1 = new System.Windows.Forms.ComboBox();
            this._timer = new System.Windows.Forms.Timer(this.components);
            this._notifyIcon = new System.Windows.Forms.NotifyIcon(this.components);
            this.contextMenu = new System.Windows.Forms.ContextMenuStrip(this.components);
            this.instellingenToolStripMenuItem = new System.Windows.Forms.ToolStripMenuItem();
            this._refreshComboBox = new System.Windows.Forms.ToolStripComboBox();
            this.toolStripSeparator1 = new System.Windows.Forms.ToolStripSeparator();
            this.rotOpToolStripMenuItem = new System.Windows.Forms.ToolStripMenuItem();
            this.contextMenu.SuspendLayout();
            this.SuspendLayout();
            // 
            // comboBox1
            // 
            this.comboBox1.FormattingEnabled = true;
            this.comboBox1.Location = new System.Drawing.Point(44, 30);
            this.comboBox1.Name = "comboBox1";
            this.comboBox1.Size = new System.Drawing.Size(121, 21);
            this.comboBox1.TabIndex = 0;
            // 
            // _timer
            // 
            this._timer.Enabled = true;
            this._timer.Interval = 10000;
            this._timer.Tick += new System.EventHandler(this._timer_Tick);
            // 
            // _notifyIcon
            // 
            this._notifyIcon.ContextMenuStrip = this.contextMenu;
            this._notifyIcon.Icon = ((System.Drawing.Icon)(resources.GetObject("_notifyIcon.Icon")));
            this._notifyIcon.Text = "notifyIcon1";
            this._notifyIcon.Visible = true;
            this._notifyIcon.BalloonTipClicked += new System.EventHandler(this._notifyIcon_BalloonTipClicked);
            this._notifyIcon.MouseDoubleClick += new System.Windows.Forms.MouseEventHandler(this._notifyIcon_BalloonTipClicked);
            // 
            // contextMenu
            // 
            this.contextMenu.Items.AddRange(new System.Windows.Forms.ToolStripItem[] {
            this.instellingenToolStripMenuItem,
            this.toolStripSeparator1,
            this.rotOpToolStripMenuItem});
            this.contextMenu.Name = "contextMenuStrip1";
            this.contextMenu.Size = new System.Drawing.Size(153, 76);
            // 
            // instellingenToolStripMenuItem
            // 
            this.instellingenToolStripMenuItem.DropDownItems.AddRange(new System.Windows.Forms.ToolStripItem[] {
            this._refreshComboBox});
            this.instellingenToolStripMenuItem.Image = global::DumpertNotifier.Properties.Resources.preferences;
            this.instellingenToolStripMenuItem.Name = "instellingenToolStripMenuItem";
            this.instellingenToolStripMenuItem.Size = new System.Drawing.Size(152, 22);
            this.instellingenToolStripMenuItem.Text = "Instellingen";
            // 
            // _refreshComboBox
            // 
            this._refreshComboBox.AutoSize = false;
            this._refreshComboBox.DropDownStyle = System.Windows.Forms.ComboBoxStyle.DropDownList;
            this._refreshComboBox.Name = "_refreshComboBox";
            this._refreshComboBox.Size = new System.Drawing.Size(40, 23);
            this._refreshComboBox.ToolTipText = "Dumpert wordt ververst om dit aantal seconden";
            this._refreshComboBox.SelectedIndexChanged += new System.EventHandler(this._refreshComboBox_SelectedIndexChanged);
            // 
            // toolStripSeparator1
            // 
            this.toolStripSeparator1.Name = "toolStripSeparator1";
            this.toolStripSeparator1.Size = new System.Drawing.Size(149, 6);
            // 
            // rotOpToolStripMenuItem
            // 
            this.rotOpToolStripMenuItem.Image = global::DumpertNotifier.Properties.Resources.cross;
            this.rotOpToolStripMenuItem.Name = "rotOpToolStripMenuItem";
            this.rotOpToolStripMenuItem.Size = new System.Drawing.Size(152, 22);
            this.rotOpToolStripMenuItem.Text = "Rot op";
            this.rotOpToolStripMenuItem.Click += new System.EventHandler(this.quitMenuItem_Click);
            // 
            // DNForm
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 13F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.ClientSize = new System.Drawing.Size(234, 108);
            this.Controls.Add(this.comboBox1);
            this.Icon = ((System.Drawing.Icon)(resources.GetObject("$this.Icon")));
            this.MaximizeBox = false;
            this.Name = "DNForm";
            this.ShowInTaskbar = false;
            this.StartPosition = System.Windows.Forms.FormStartPosition.CenterScreen;
            this.Text = "Dumpert Notifier 1.0";
            this.WindowState = System.Windows.Forms.FormWindowState.Minimized;
            this.contextMenu.ResumeLayout(false);
            this.ResumeLayout(false);

        }

        #endregion

        private System.Windows.Forms.ComboBox comboBox1;
        private System.Windows.Forms.Timer _timer;
        private System.Windows.Forms.NotifyIcon _notifyIcon;
        private System.Windows.Forms.ContextMenuStrip contextMenu;
        private System.Windows.Forms.ToolStripMenuItem instellingenToolStripMenuItem;
        private System.Windows.Forms.ToolStripSeparator toolStripSeparator1;
        private System.Windows.Forms.ToolStripMenuItem rotOpToolStripMenuItem;
        private System.Windows.Forms.ToolStripComboBox _refreshComboBox;
    }
}

