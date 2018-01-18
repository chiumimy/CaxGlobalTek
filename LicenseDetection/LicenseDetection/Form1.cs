using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Windows.Forms;
using System.IO;
using System.Globalization;

namespace LicenseDetection
{


    
    public partial class Form1 : Form
    {
        public bool status;
        public Form1()
        {
            InitializeComponent();
            ConnectStatus();
        }

        private void ConnectStatus()
        {
            try
            {
                if (!File.Exists(Variable.AVMLogFilePath))
                {
                    ConnectStr.Text = "連線失敗";
                }
                else
                {
                    ConnectStr.Text = "連線成功";
                }
            }
            catch (System.Exception ex)
            {
            	
            }
        }

        private void OK_Click(object sender, EventArgs e)
        {
            try
            {
                //建立資料夾
                bool status;
                status = Function.CreateFolder();
                if (!status)
                {
                    MessageBox.Show("建立資料夾失敗");
                    return;
                }

                //寫出Log
                List<string> ListMessage = new List<string>();
                if (!Function.SaveListMessage(Function.FileLocationFolder, out ListMessage))
                {
                    MessageBox.Show("今天沒有人使用NX");
                    return;
                }

                Dictionary<Variable.UserData, List<Variable.OutInTime>> DicUser = new Dictionary<Variable.UserData, List<Variable.OutInTime>>();
                foreach (string i in ListMessage)
                {
                    string EnglishName = i.Split('@')[1].Trim();
                    string StrDepartment = "", StrUserChinese = "";
                    Function.GetDepartmentUser(EnglishName, out StrDepartment, out StrUserChinese);
                    Variable.UserData sUserData = new Variable.UserData();
                    sUserData.Department = StrDepartment;
                    sUserData.ChineseName = StrUserChinese;
                    sUserData.EnglishName = EnglishName;
                    List<Variable.OutInTime> ListOutInTime = new List<Variable.OutInTime>();
                    status = DicUser.TryGetValue(sUserData, out ListOutInTime);
                    if (i.Contains("OUT:") & i.Contains("gateway"))
                    {
                        if (!status)
                        {
                            ListOutInTime = new List<Variable.OutInTime>();
                        }
                        else
                        {
                            //判斷此人是否關閉過NX，如果沒關閉過，表示可能開2個以上的NX，則第二筆登入的資訊不紀錄；反之則表示關閉過NX後再次開啟NX
                            bool flag = false;
                            foreach (Variable.OutInTime j in ListOutInTime)
                            {
                                if (j.InTime == null)
                                {
                                    flag = true;
                                }
                            }
                            if (flag)
                            {
                                continue;
                            }
                            //表示關閉過NX後再次開啟NX(已記錄前一次登出時間)
                        }
                        Variable.OutInTime sOutInTime = new Variable.OutInTime();
                        sOutInTime.OutTime = i.Split(new string[] { " (ugslmd)" }, StringSplitOptions.RemoveEmptyEntries)[0];
                        ListOutInTime.Add(sOutInTime);
                        DicUser[sUserData] = ListOutInTime;
                    }
                    if (i.Contains("IN:") & i.Contains("gateway"))
                    {
                        if (!status)
                        {
                            continue;
                        }
                        else
                        {
                            string InTime = i.Split(new string[] { " (ugslmd)" }, StringSplitOptions.RemoveEmptyEntries)[0];
                            Variable.OutInTime temp = new Variable.OutInTime();
                            int no = -1;
                            for (int j = 0; j < ListOutInTime.Count; j++)
                            {
                                if (ListOutInTime[j].InTime == null)
                                {
                                    no = j;
                                    temp.OutTime = ListOutInTime[j].OutTime;
                                    temp.InTime = InTime;
                                    TimeSpan dur = Convert.ToDateTime(temp.InTime) - Convert.ToDateTime(temp.OutTime);
                                    temp.Duration = dur.ToString();
                                }
                            }
                            if (no == -1)
                            {
                                continue;
                            }
                            ListOutInTime[no] = temp;

                        }
                    }
                }

                Function.CalculateTotalTime(ref DicUser);

                Function.ExportExcel(DicUser);


                //MessageBox.Show("success");
                this.Close();
            }
            catch (System.Exception ex)
            {
                MessageBox.Show(ex.ToString());
            }
        }
        
    }
}
