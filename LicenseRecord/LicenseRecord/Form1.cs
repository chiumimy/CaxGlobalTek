using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Windows.Forms;
using CaxGlobaltek;
using NHibernate;
using System.IO;

namespace LicenseRecord
{
    public partial class Form1 : Form
    {
        public static string OutputPath = "";
        public static string Department = Environment.GetEnvironmentVariable("Department");
        //public static string Department = "NX9";
        public Form1()
        {
            InitializeComponent();
            StartDate.Text = DateTime.Now.ToLongDateString();
            EndDate.Text = DateTime.Now.ToLongDateString();
        }

        private void OK_Click(object sender, EventArgs e)
        {
            //2017/XX/XX => ToShortDateString()
            //2017年XX月XX日 => ToLongDateString()
            //StartDate.Format = DateTimePickerFormat.Custom;
            //StartDate.CustomFormat = "yyyy/M/d";
            //EndDate.Format = DateTimePickerFormat.Custom;
            //EndDate.CustomFormat = "yyyy/M/d";
            //MessageBox.Show(StartDate.Value.ToShortDateString());
            //MessageBox.Show(EndDate.Value.ToShortDateString());

            

            bool status;
            if (EndDate.Value.CompareTo(StartDate.Value).ToString() == "-1")
            {
                MessageBox.Show("結束時間不能晚於開始時間");
                return;
            }

            int totalDays = (EndDate.Value.Subtract(StartDate.Value)).Days + 1;

            //取得資料庫資料
            Dictionary<Function.Key, List<Function.Value>> DicData = new Dictionary<Function.Key, List<Function.Value>>();
            if (Department == "AVM")
            {
                status = Function.GetDBData_AVM(StartDate.Value, totalDays, out DicData);
                if (!status)
                {
                    this.Close();
                }
            }
            else if (Department == "AC")
            {
                status = Function.GetDBData_AC(StartDate.Value, totalDays, out DicData);
                if (!status)
                {
                    this.Close();
                }
            }
            else if (Department == "NX9")
            {
                status = Function.GetDBData_NX9(StartDate.Value, totalDays, out DicData);
                if (!status)
                {
                    this.Close();
                }
            }
            

            //取得總表資料格式
            Dictionary<string, List<Function.OverAllValue>> DicOverAllData = new Dictionary<string, List<Function.OverAllValue>>();
            if (Department == "AVM")
            {
                status = Function.GetOverAllData_AVM(StartDate.Value, totalDays, out DicOverAllData);
                if (!status)
                {
                    this.Close();
                }
            }
            else if (Department == "AC")
            {
                status = Function.GetOverAllData_AC(StartDate.Value, totalDays, out DicOverAllData);
                if (!status)
                {
                    this.Close();
                }
            }
            else if (Department == "NX9")
            {
                status = Function.GetOverAllData_NX9(StartDate.Value, totalDays, out DicOverAllData);
                if (!status)
                {
                    this.Close();
                }
            }

            //建立輸出檔案名稱
            OutputPath = string.Format(@"{0}\{1}\{2}\{3}_To_{4}", "D:", "LicenseFolder", Department, StartDate.Value.ToLongDateString(), EndDate.Value.ToLongDateString() + ".xls");
            if (File.Exists(OutputPath))
            {
                File.Delete(OutputPath);
            }

            //輸出Excel
            status = Function.ExportExcel(DicOverAllData, DicData);
            if (!status)
            {
                MessageBox.Show("輸出Excel失敗");
                this.Close();
            }
            MessageBox.Show("輸出完成");
            this.Close();
        }

        private void Closed_Click(object sender, EventArgs e)
        {
            this.Close();
        }
    }
}
