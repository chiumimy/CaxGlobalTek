using System;
using System.Collections.Generic;
using System.Linq;
using System.Windows.Forms;
using CaxGlobaltek;
using NHibernate;

namespace LicenseDetection
{
    static class Program
    {
        /// <summary>
        /// 應用程式的主要進入點。
        /// </summary>
        [STAThread]
        static void Main()
        {
//             Application.EnableVisualStyles();
//             Application.SetCompatibleTextRenderingDefault(false);
//             Application.Run(new Form1());


            try
            {
                //建立資料夾
                bool status;
                status = Function.CreateFolder();
                if (!status)
                {
                    //MessageBox.Show("建立資料夾失敗");
                    return;
                }

                //寫出Log
                List<string> ListMessage = new List<string>();
                if (!Function.SaveListMessage(Function.FileLocationFolder, out ListMessage))
                {
                    //MessageBox.Show("今天沒有人使用NX");
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

                #region SaveDB
                //先刪除今日的資料再儲存表格
                if (Function.Department == "AVM")
                {
                    IList<Sys_AVMLicense> listData = new List<Sys_AVMLicense>();
                    using (ISession session = MyHibernateHelper.SessionFactory.OpenSession())
                    {
                        listData = session.QueryOver<Sys_AVMLicense>().Where(x => x.date == DateTime.Today.ToLongDateString()).List();
                    }
                    Function.DelDB<Sys_AVMLicense>(listData);
                    Function.SaveDB_AVM(DicUser);
                }
                else if (Function.Department == "AC")
                {
                    IList<Sys_ACLicense> listData = new List<Sys_ACLicense>();
                    using (ISession session = MyHibernateHelper.SessionFactory.OpenSession())
                    {
                        listData = session.QueryOver<Sys_ACLicense>().Where(x => x.date == DateTime.Today.ToLongDateString()).List();
                    }
                    Function.DelDB<Sys_ACLicense>(listData);
                    Function.SaveDB_AC(DicUser);
                }
                else if (Function.Department == "NX9")
                {
                    IList<Sys_NX9License> listData = new List<Sys_NX9License>();
                    using (ISession session = MyHibernateHelper.SessionFactory.OpenSession())
                    {
                        listData = session.QueryOver<Sys_NX9License>().Where(x => x.date == DateTime.Today.ToLongDateString()).List();
                    }
                    Function.DelDB<Sys_NX9License>(listData);
                    Function.SaveDB_NX9(DicUser);
                }

                
                

                #endregion
            }
            catch (System.Exception ex)
            {
                //MessageBox.Show(ex.ToString());
            }
            
        }
    }
}
