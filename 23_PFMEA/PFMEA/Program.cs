using System;
using System.Collections.Generic;
using System.Linq;
using System.Windows.Forms;
using NHibernate;
using CaxGlobaltek;

namespace PFMEA
{
    static class Program
    {
        /// <summary>
        /// 應用程式的主要進入點。
        /// </summary>
        [STAThread]
        static void Main()
        {
            //保護裝置
            //if (!CaxPublic.CheckLicense())
            //{
            //    MessageBox.Show("產品過期，請購買License延長功能使用權限！");
            //    return;
            //}
            
            Application.EnableVisualStyles();
            Application.SetCompatibleTextRenderingDefault(false);
            Application.Run(new Form1());
        }
    }
}
