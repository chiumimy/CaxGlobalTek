using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace LicenseDetection
{
    public class Variable
    {
        public static string AVMLogFilePath { get { return string.Format(@"{0}\{1}", "\\\\192.168.3.101\\PLMLicenseServer", "splm_ugslmd.log"); } }
        public static string ACLogFilePath { get { return string.Format(@"{0}\{1}", "\\\\192.168.3.17\\UGSLicensing", "ugslicensing.log"); } }
        public static string NX9LogFilePath { get { return string.Format(@"{0}\{1}", "\\\\192.168.3.78\\PLMLicenseServer", "splm_ugslmd.log"); } }
        public static string TodayDate { get {return DateTime.Now.ToString("M/d/yyyy"); } }
        public struct UserData
        {
            public string Department { get; set; }
            public string ChineseName { get; set; }
            public string EnglishName { get; set; }
        }
        public struct OutInTime
        {
            public string OutTime { get; set; }
            public string InTime { get; set; }
            public string Duration { get; set; }
            public string TotalTime { get; set; }
            
        }
    }
}
