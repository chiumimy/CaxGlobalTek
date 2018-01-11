using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Windows.Forms;
using System.Threading;

namespace CaxGlobaltek
{
    public class CaxLoading
    {
        public static LoadingDlg cLoadingDlg = new LoadingDlg();
        private static Thread showDlg = null;

        public static void RunDlg()
        {
            showDlg = new Thread(Doit);
            showDlg.IsBackground = true;
            showDlg.Start();
        }

        private static void Doit()
        {
            //System.Windows.Forms.Application.EnableVisualStyles();
            cLoadingDlg.ShowDialog();
            //System.Windows.Forms.Application.DoEvents();
        }

        public static void CloseDlg()
        {
            CloseWindows();
            if (showDlg.ThreadState == ThreadState.Background)
            {
                Thread.Sleep(100);
            }
            showDlg.Abort();
            showDlg = null;
            //cLoadingDlg.Close();
        }

        private delegate void CloseWindowsCallBack();
        private static void CloseWindows()
        {
            try
            {
                if (cLoadingDlg.InvokeRequired)
                {
                    CloseWindowsCallBack callback = new CloseWindowsCallBack(CloseWindows);
                    cLoadingDlg.Invoke(callback);
                }
                else
                {
                    while (cLoadingDlg.Visible == false)
                    {
                        Thread.Sleep(10);
                    }
                    cLoadingDlg.Close();
                }
            }
            catch (System.Exception ex)
            {
                MessageBox.Show(ex.ToString());
            }
        }
    }
}
