using System.Windows.Forms;

namespace DumpertNotifier
{
    partial class DnForm
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
            System.ComponentModel.ComponentResourceManager resources = new System.ComponentModel.ComponentResourceManager(typeof(DnForm));
            this.timer = new System.Windows.Forms.Timer(this.components);
            this.notifier = new System.Windows.Forms.NotifyIcon(this.components);
            this.contextMenu = new System.Windows.Forms.ContextMenuStrip(this.components);
            this.quitToolStripItem = new System.Windows.Forms.ToolStripMenuItem();
            this.contextMenu.SuspendLayout();
            this.SuspendLayout();
            // 
            // timer
            // 
            this.timer.Enabled = true;
            this.timer.Interval = 10000;
            this.timer.Tick += new System.EventHandler(this.timer_Tick);
            // 
            // notifier
            // 
            this.notifier.ContextMenuStrip = this.contextMenu;
            this.notifier.Icon = ((System.Drawing.Icon)(resources.GetObject("notifier.Icon")));
            this.notifier.Visible = true;
            this.notifier.BalloonTipClicked += new System.EventHandler(this.notifier_BalloonTipClicked);
            // 
            // contextMenu
            // 
            this.contextMenu.Items.AddRange(new System.Windows.Forms.ToolStripItem[] {
            this.quitToolStripItem});
            this.contextMenu.Name = "contextMenuStrip1";
            this.contextMenu.Size = new System.Drawing.Size(153, 48);
            // 
            // quitToolStripItem
            // 
            this.quitToolStripItem.Name = "quitToolStripItem";
            this.quitToolStripItem.Size = new System.Drawing.Size(152, 22);
            this.quitToolStripItem.Text = "Afsluiten";
            this.quitToolStripItem.Click += new System.EventHandler(this.quitMenuItem_Click);
            // 
            // DnForm
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 13F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.ClientSize = new System.Drawing.Size(143, 57);
            this.FormBorderStyle = System.Windows.Forms.FormBorderStyle.FixedDialog;
            this.Icon = ((System.Drawing.Icon)(resources.GetObject("$this.Icon")));
            this.MaximizeBox = false;
            this.Name = "DnForm";
            this.ShowInTaskbar = false;
            this.StartPosition = System.Windows.Forms.FormStartPosition.CenterParent;
            this.WindowState = System.Windows.Forms.FormWindowState.Minimized;
            this.contextMenu.ResumeLayout(false);
            this.ResumeLayout(false);

        }

        #endregion

        private System.Windows.Forms.Timer timer;
        private System.Windows.Forms.NotifyIcon notifier;
        private ContextMenuStrip contextMenu;
        private ToolStripMenuItem quitToolStripItem;
    }
}

