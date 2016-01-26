using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace DumpertNotifier
{
    public partial class Dialog : Form
    {
        public Dialog()
        {
            InitializeComponent();
            var workingArea = Screen.GetWorkingArea(this);
            Location = new Point(workingArea.Right - Size.Width - (int)(Screen.PrimaryScreen.Bounds.Width*0.15),
                                      workingArea.Bottom - Size.Height - (int)(Screen.PrimaryScreen.Bounds.Height*0.5));
        }
    }
}