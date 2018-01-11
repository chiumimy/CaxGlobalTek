using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using NXOpen.UF;
using NXOpen;

namespace CaxGlobaltek
{
    public class CaxLog
    {
        private static UFSession theUfSession = UFSession.GetUFSession();
        private static Session theSession = Session.GetSession();

        //顯示訊息在頁面上
        public static void ShowListingWindow(string msgStr)
        {
            DateTime myDate = DateTime.Now;
            string myDateString = myDate.ToString("yyyy-MM-dd HH:mm:ss");
            string logStr = string.Format("Globaltek_LOG\t{0}\t{1}", myDateString, msgStr);

            theSession.ListingWindow.Open();
            theSession.ListingWindow.WriteLine(logStr);
            theSession.LogFile.WriteLine(logStr);
        }
    }
}
