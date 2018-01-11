using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Windows.Forms;
using DevComponents.DotNetBar;

namespace CaxGlobaltek
{
    public partial class LoadingDlg : Office2007Form
    {
        public LoadingDlg()
        {
            InitializeComponent();
            circularProgress1.IsRunning = true;
            
            //this.Activate();
        }

        private void LoadingDlg_Load(object sender, EventArgs e)
        {
             circularProgress1.IsRunning = true;
        }

        private void LoadingDlg_FormClosing(object sender, FormClosingEventArgs e)
        {
//             circularProgress1.IsRunning = false;
//             circularProgress1.Dispose();
        }
    }
}
