using System;
using System.Collections.Generic;
using System.Linq;
using System.Windows.Forms;

namespace Local_AddDeleteDB_NX75
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
                System.Diagnostics.Process.Start(@"D:\cax\Globaltek\Local_NX75\AddDeleteDB_L_NX75.bat");
            }
            catch (System.Exception ex)
            {
                MessageBox.Show("請檢查網路連線狀態！");
            }
        }
    }
}
