using System;
using System.Collections.Generic;
using System.Linq;
using System.Windows.Forms;

namespace Server_PFMEA_NX75
{
    static class Program
    {
        /// <summary>
        /// 應用程式的主要進入點。
        /// </summary>
        [STAThread]
        static void Main()
        {
            //Application.EnableVisualStyles();
            //Application.SetCompatibleTextRenderingDefault(false);
            //Application.Run(new Form1());

            try
            {
                System.Diagnostics.Process.Start(@"\\192.168.35.1\cax\Globaltek\Server_NX75\PFMEA_S_NX75.bat");
            }
            catch (System.Exception ex)
            {
                MessageBox.Show("請檢查網路連線狀態！");
            }
        }
    }
}
