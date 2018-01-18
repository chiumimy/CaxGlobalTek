using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using Microsoft.Office.Interop.Excel;
using CaxGlobaltek;
using NHibernate;
using System.Windows.Forms;
using System.IO;

namespace LicenseRecord
{
    public class Function
    {
        public static bool status;
        public struct UserData
        {
            public string Department { get; set; }
            public string ChineseName { get; set; }
            public string EnglishName { get; set; }
        }
        public struct Key
        {
            //public string UserName { get; set; }
            public string Department { get; set; }
            public string ChineseName { get; set; }
            public string EnglishName { get; set; }
            public string Date { get; set; }
        }
        public struct Value
        {
            public string OutTime { get; set; }
            public string InTime { get; set; }
            public string Duration { get; set; }
            public string TotalTime { get; set; }
        }
        public struct OverAllValue
        {
            public string Department { get; set; }
            public string ChineseName { get; set; }
            public string EnglishName { get; set; }
            public string TotalTime { get; set; }
        }
        public static bool GetDBData_AC(DateTime StartDate, int TotalDays, out Dictionary<Key, List<Value>> DicData)
        {
            DicData = new Dictionary<Key, List<Value>>();
            try
            {
                for (int i = 0; i < TotalDays; i++)
                {
                    IList<Sys_ACLicense> listData = new List<Sys_ACLicense>();
                    using (ISession session = MyHibernateHelper.SessionFactory.OpenSession())
                    {
                        listData = session.QueryOver<Sys_ACLicense>().Where(x => x.date == StartDate.AddDays(i).ToLongDateString()).List();
                        session.Close();
                    }
                    if (listData.Count == 0)
                    {
                        continue;
                    }

                    foreach (Sys_ACLicense j in listData)
                    {
                        Key sKey = new Key();
                        sKey.Department = j.department;
                        sKey.ChineseName = j.userChinese;
                        sKey.EnglishName = j.username;
                        sKey.Date = StartDate.AddDays(i).ToLongDateString();

                        List<Value> ListValue = new List<Value>();
                        status = DicData.TryGetValue(sKey, out ListValue);
                        if (!status)
                        {
                            ListValue = new List<Value>();
                        }
                        Value sValue = new Value();
                        sValue.OutTime = j.login;
                        sValue.InTime = j.logout;
                        sValue.Duration = j.workingtime;
                        sValue.TotalTime = j.totaltime;
                        ListValue.Add(sValue);
                        DicData[sKey] = ListValue;
                    }

                    //MessageBox.Show(StartDate.Value.AddDays(i).ToLongDateString());
                }
            }
            catch (System.Exception ex)
            {
                MessageBox.Show("取得資料失敗");
                return false;
            }
            return true;
        }
        public static bool GetDBData_NX9(DateTime StartDate, int TotalDays, out Dictionary<Key, List<Value>> DicData)
        {
            DicData = new Dictionary<Key, List<Value>>();
            try
            {
                for (int i = 0; i < TotalDays; i++)
                {
                    IList<Sys_NX9License> listData = new List<Sys_NX9License>();
                    using (ISession session = MyHibernateHelper.SessionFactory.OpenSession())
                    {
                        listData = session.QueryOver<Sys_NX9License>().Where(x => x.date == StartDate.AddDays(i).ToLongDateString()).List();
                        session.Close();
                    }
                    if (listData.Count == 0)
                    {
                        continue;
                    }

                    foreach (Sys_NX9License j in listData)
                    {
                        Key sKey = new Key();
                        sKey.Department = j.department;
                        sKey.ChineseName = j.userChinese;
                        sKey.EnglishName = j.username;
                        sKey.Date = StartDate.AddDays(i).ToLongDateString();

                        List<Value> ListValue = new List<Value>();
                        status = DicData.TryGetValue(sKey, out ListValue);
                        if (!status)
                        {
                            ListValue = new List<Value>();
                        }
                        Value sValue = new Value();
                        sValue.OutTime = j.login;
                        sValue.InTime = j.logout;
                        sValue.Duration = j.workingtime;
                        sValue.TotalTime = j.totaltime;
                        ListValue.Add(sValue);
                        DicData[sKey] = ListValue;
                    }

                    //MessageBox.Show(StartDate.Value.AddDays(i).ToLongDateString());
                }
            }
            catch (System.Exception ex)
            {
                MessageBox.Show("取得資料失敗");
                return false;
            }
            return true;
        }
        public static bool GetDBData_AVM(DateTime StartDate, int TotalDays, out Dictionary<Key, List<Value>> DicData)
        {
            DicData = new Dictionary<Key, List<Value>>();
            try
            {
                for (int i = 0; i < TotalDays; i++)
                {
                    IList<Sys_AVMLicense> listData = new List<Sys_AVMLicense>();
                    using (ISession session = MyHibernateHelper.SessionFactory.OpenSession())
                    {
                        listData = session.QueryOver<Sys_AVMLicense>().Where(x => x.date == StartDate.AddDays(i).ToLongDateString()).List();
                        session.Close();
                    }
                    if (listData.Count == 0)
                    {
                        continue;
                    }

                    foreach (Sys_AVMLicense j in listData)
                    {
                        Key sKey = new Key();
                        sKey.Department = j.department;
                        sKey.ChineseName = j.userChinese;
                        sKey.EnglishName = j.username;
                        sKey.Date = StartDate.AddDays(i).ToLongDateString();

                        List<Value> ListValue = new List<Value>();
                        status = DicData.TryGetValue(sKey, out ListValue);
                        if (!status)
                        {
                            ListValue = new List<Value>();
                        }
                        Value sValue = new Value();
                        sValue.OutTime = j.login;
                        sValue.InTime = j.logout;
                        sValue.Duration = j.workingtime;
                        sValue.TotalTime = j.totaltime;
                        ListValue.Add(sValue);
                        DicData[sKey] = ListValue;
                    }

                    //MessageBox.Show(StartDate.Value.AddDays(i).ToLongDateString());
                }
            }
            catch (System.Exception ex)
            {
                MessageBox.Show("取得資料失敗");
                return false;
            }
            return true;
        }
        public static bool GetOverAllData_AC(DateTime StartDate, int TotalDays, out Dictionary<string, List<OverAllValue>> DicOverAllData)
        {
            DicOverAllData = new Dictionary<string, List<OverAllValue>>();
            try
            {
                //先整理日期區間有哪些人使用
                List<UserData> ListUserName = new List<UserData>();
                if (!GetUserName_AC(StartDate, TotalDays, out ListUserName))
                {
                    MessageBox.Show("取得【日期區間有哪些人使用】失敗");
                    return false;
                }

                if (ListUserName.Count == 0)
                {
                    MessageBox.Show("此日期區間沒有任何資料");
                    return false;
                }

                for (int i = 0; i < TotalDays; i++)
                {
                    foreach (UserData j in ListUserName)
                    {
                        IList<Sys_ACLicense> ListData = new List<Sys_ACLicense>();
                        using (ISession session = MyHibernateHelper.SessionFactory.OpenSession())
                        {
                            ListData = session.QueryOver<Sys_ACLicense>().Where(x => x.username == j.EnglishName)
                                                                .And(x => x.date == StartDate.AddDays(i).ToLongDateString()).List();
                            session.Close();
                        }

                        Sys_ACLicense lastData = new Sys_ACLicense();
                        
                        List<OverAllValue> ListOverAllValue = new List<OverAllValue>();
                        status = DicOverAllData.TryGetValue(StartDate.AddDays(i).ToLongDateString(), out ListOverAllValue);
                        if (!status)
                        {
                            ListOverAllValue = new List<OverAllValue>();
                        }

                        OverAllValue sOverAllValue = new OverAllValue();
                        sOverAllValue.EnglishName = j.EnglishName;
                        sOverAllValue.Department = j.Department;
                        sOverAllValue.ChineseName = j.ChineseName;
                        if (ListData.Count == 0)
                        {
                            //此人在這個日期沒有用UG
                            sOverAllValue.TotalTime = "";
                        }
                        else
                        {
                            //取最後一筆紀錄
                            lastData = ListData[ListData.Count - 1];
                            if (lastData.totaltime == "使用中或未關閉NX")
                            {
                                sOverAllValue.TotalTime = "1440分0秒";
                            }
                            else
                            {
                                sOverAllValue.TotalTime = lastData.totaltime;
                            }
                        }
                        ListOverAllValue.Add(sOverAllValue);
                        DicOverAllData[StartDate.AddDays(i).ToLongDateString()] = ListOverAllValue;

                        /*
                        if (ListData.Count == 0)
                        {
                            //此人在這個日期沒有用UG
                            List<OverAllValue> ListOverAllValue = new List<OverAllValue>();
                            status = DicOverAllData.TryGetValue(StartDate.AddDays(i).ToLongDateString(), out ListOverAllValue);
                            if (!status)
                            {
                                ListOverAllValue = new List<OverAllValue>();
                            }
                            OverAllValue sOverAllValue = new OverAllValue();
                            sOverAllValue.UserName = j;
                            sOverAllValue.TotalTime = "";
                            ListOverAllValue.Add(sOverAllValue);
                            DicOverAllData[StartDate.AddDays(i).ToLongDateString()] = ListOverAllValue;
                        }
                        else
                        {
                            //取最後一筆紀錄
                            Sys_ACLicense lastData = ListData[ListData.Count - 1];
                            //此人在這個日期沒有用UG
                            List<OverAllValue> ListOverAllValue = new List<OverAllValue>();
                            status = DicOverAllData.TryGetValue(StartDate.AddDays(i).ToLongDateString(), out ListOverAllValue);
                            if (!status)
                            {
                                ListOverAllValue = new List<OverAllValue>();
                            }
                            OverAllValue sOverAllValue = new OverAllValue();
                            sOverAllValue.UserName = j;
                            sOverAllValue.TotalTime = lastData.totaltime;
                            ListOverAllValue.Add(sOverAllValue);
                            DicOverAllData[StartDate.AddDays(i).ToLongDateString()] = ListOverAllValue;
                        }
                        */
                    }
                }

                /*
                for (int i = 0; i < TotalDays; i++)
                {
                    IList<Sys_ACLicense> listData = new List<Sys_ACLicense>();
                    using (ISession session = MyHibernateHelper.SessionFactory.OpenSession())
                    {
                        listData = session.QueryOver<Sys_ACLicense>().Where(x => x.date == StartDate.AddDays(i).ToLongDateString()).List();
                        session.Close();
                    }
                    if (listData.Count == 0)
                    {
                        continue;
                    }

                    foreach (Sys_ACLicense j in listData)
                    {
                        List<OverAllValue> ListOverAllValue = new List<OverAllValue>();
                        status = DicOverAllData.TryGetValue(j.username, out ListOverAllValue);
                        if (!status)
                        {

                        }
                        else
                        {

                        }
                    }
                }
                */
            }
            catch (System.Exception ex)
            {
                return false;
            }
            return true;
        }
        public static bool GetOverAllData_NX9(DateTime StartDate, int TotalDays, out Dictionary<string, List<OverAllValue>> DicOverAllData)
        {
            DicOverAllData = new Dictionary<string, List<OverAllValue>>();
            try
            {
                //先整理日期區間有哪些人使用
                List<UserData> ListUserName = new List<UserData>();
                if (!GetUserName_NX9(StartDate, TotalDays, out ListUserName))
                {
                    MessageBox.Show("取得【日期區間有哪些人使用】失敗");
                    return false;
                }

                if (ListUserName.Count == 0)
                {
                    MessageBox.Show("此日期區間沒有任何資料");
                    return false;
                }

                for (int i = 0; i < TotalDays; i++)
                {
                    foreach (UserData j in ListUserName)
                    {
                        IList<Sys_NX9License> ListData = new List<Sys_NX9License>();
                        using (ISession session = MyHibernateHelper.SessionFactory.OpenSession())
                        {
                            ListData = session.QueryOver<Sys_NX9License>().Where(x => x.username == j.EnglishName)
                                                                .And(x => x.date == StartDate.AddDays(i).ToLongDateString()).List();
                            session.Close();
                        }

                        Sys_NX9License lastData = new Sys_NX9License();

                        List<OverAllValue> ListOverAllValue = new List<OverAllValue>();
                        status = DicOverAllData.TryGetValue(StartDate.AddDays(i).ToLongDateString(), out ListOverAllValue);
                        if (!status)
                        {
                            ListOverAllValue = new List<OverAllValue>();
                        }

                        OverAllValue sOverAllValue = new OverAllValue();
                        sOverAllValue.EnglishName = j.EnglishName;
                        sOverAllValue.Department = j.Department;
                        sOverAllValue.ChineseName = j.ChineseName;
                        if (ListData.Count == 0)
                        {
                            //此人在這個日期沒有用UG
                            sOverAllValue.TotalTime = "";
                        }
                        else
                        {
                            //取最後一筆紀錄
                            lastData = ListData[ListData.Count - 1];
                            if (lastData.totaltime == "使用中或未關閉NX")
                            {
                                sOverAllValue.TotalTime = "1440分0秒";
                            }
                            else
                            {
                                sOverAllValue.TotalTime = lastData.totaltime;
                            }
                        }
                        ListOverAllValue.Add(sOverAllValue);
                        DicOverAllData[StartDate.AddDays(i).ToLongDateString()] = ListOverAllValue;
                    }
                }
            }
            catch (System.Exception ex)
            {
                return false;
            }
            return true;
        }
        public static bool GetOverAllData_AVM(DateTime StartDate, int TotalDays, out Dictionary<string, List<OverAllValue>> DicOverAllData)
        {
            DicOverAllData = new Dictionary<string, List<OverAllValue>>();
            try
            {
                //先整理日期區間有哪些人使用
                List<UserData> ListUserName = new List<UserData>();
                if (!GetUserName_AVM(StartDate, TotalDays, out ListUserName))
                {
                    MessageBox.Show("取得【日期區間有哪些人使用】失敗");
                    return false;
                }

                if (ListUserName.Count == 0)
                {
                    MessageBox.Show("此日期區間沒有任何資料");
                    return false;
                }

                for (int i = 0; i < TotalDays; i++)
                {
                    foreach (UserData j in ListUserName)
                    {
                        IList<Sys_AVMLicense> ListData = new List<Sys_AVMLicense>();
                        using (ISession session = MyHibernateHelper.SessionFactory.OpenSession())
                        {
                            ListData = session.QueryOver<Sys_AVMLicense>().Where(x => x.username == j.EnglishName)
                                                                .And(x => x.date == StartDate.AddDays(i).ToLongDateString()).List();
                            session.Close();
                        }

                        Sys_AVMLicense lastData = new Sys_AVMLicense();

                        List<OverAllValue> ListOverAllValue = new List<OverAllValue>();
                        status = DicOverAllData.TryGetValue(StartDate.AddDays(i).ToLongDateString(), out ListOverAllValue);
                        if (!status)
                        {
                            ListOverAllValue = new List<OverAllValue>();
                        }

                        OverAllValue sOverAllValue = new OverAllValue();
                        sOverAllValue.EnglishName = j.EnglishName;
                        sOverAllValue.Department = j.Department;
                        sOverAllValue.ChineseName = j.ChineseName;
                        if (ListData.Count == 0)
                        {
                            //此人在這個日期沒有用UG
                            sOverAllValue.TotalTime = "";
                        }
                        else
                        {
                            //取最後一筆紀錄
                            lastData = ListData[ListData.Count - 1];
                            if (lastData.totaltime == "使用中或未關閉NX")
                            {
                                sOverAllValue.TotalTime = "1440分0秒";
                            }
                            else
                            {
                                sOverAllValue.TotalTime = lastData.totaltime;
                            }
                        }
                        ListOverAllValue.Add(sOverAllValue);
                        DicOverAllData[StartDate.AddDays(i).ToLongDateString()] = ListOverAllValue;

                        /*
                        if (ListData.Count == 0)
                        {
                            //此人在這個日期沒有用UG
                            List<OverAllValue> ListOverAllValue = new List<OverAllValue>();
                            status = DicOverAllData.TryGetValue(StartDate.AddDays(i).ToLongDateString(), out ListOverAllValue);
                            if (!status)
                            {
                                ListOverAllValue = new List<OverAllValue>();
                            }
                            OverAllValue sOverAllValue = new OverAllValue();
                            sOverAllValue.UserName = j;
                            sOverAllValue.TotalTime = "";
                            ListOverAllValue.Add(sOverAllValue);
                            DicOverAllData[StartDate.AddDays(i).ToLongDateString()] = ListOverAllValue;
                        }
                        else
                        {
                            //取最後一筆紀錄
                            Sys_ACLicense lastData = ListData[ListData.Count - 1];
                            //此人在這個日期沒有用UG
                            List<OverAllValue> ListOverAllValue = new List<OverAllValue>();
                            status = DicOverAllData.TryGetValue(StartDate.AddDays(i).ToLongDateString(), out ListOverAllValue);
                            if (!status)
                            {
                                ListOverAllValue = new List<OverAllValue>();
                            }
                            OverAllValue sOverAllValue = new OverAllValue();
                            sOverAllValue.UserName = j;
                            sOverAllValue.TotalTime = lastData.totaltime;
                            ListOverAllValue.Add(sOverAllValue);
                            DicOverAllData[StartDate.AddDays(i).ToLongDateString()] = ListOverAllValue;
                        }
                        */
                    }
                }

                /*
                for (int i = 0; i < TotalDays; i++)
                {
                    IList<Sys_ACLicense> listData = new List<Sys_ACLicense>();
                    using (ISession session = MyHibernateHelper.SessionFactory.OpenSession())
                    {
                        listData = session.QueryOver<Sys_ACLicense>().Where(x => x.date == StartDate.AddDays(i).ToLongDateString()).List();
                        session.Close();
                    }
                    if (listData.Count == 0)
                    {
                        continue;
                    }

                    foreach (Sys_ACLicense j in listData)
                    {
                        List<OverAllValue> ListOverAllValue = new List<OverAllValue>();
                        status = DicOverAllData.TryGetValue(j.username, out ListOverAllValue);
                        if (!status)
                        {

                        }
                        else
                        {

                        }
                    }
                }
                */
            }
            catch (System.Exception ex)
            {
                return false;
            }
            return true;
        }
        public static bool ExportExcel(Dictionary<string, List<Function.OverAllValue>> DicOverAllData, Dictionary<Key, List<Value>> DicData)
        {
            ApplicationClass excelApp = new ApplicationClass();
            Workbook book = null;
            Worksheet sheet = null;
            Range oRng = null;
            try
            {
                excelApp.Visible = false;
                book = excelApp.Workbooks.Open(string.Format(@"{0}\{1}\{2}", "D:", "LicenseFolder", "LicenseDetection.xls"));
                sheet = (Worksheet)book.Sheets[1];
                oRng = (Range)sheet.Cells;

                #region 處理總表
                Dictionary<string, List<string>> WorkingDayUsedTime = new Dictionary<string, List<string>>(); 
                int DateRow = 1, DateColumn = 4, UserNameRow = 2, DepartmentColumn = 1, ChineseNameColumn = 2, EnglishNameColumn = 3;
                foreach (KeyValuePair<string, List<OverAllValue>> kvp in DicOverAllData)
                {
                    oRng[DateRow, DateColumn] = kvp.Key;
                    List<string> temp = new List<string>();
                    foreach (OverAllValue i in kvp.Value)
                    {
                        oRng[UserNameRow, DepartmentColumn] = i.Department;
                        oRng[UserNameRow, ChineseNameColumn] = i.ChineseName;
                        oRng[UserNameRow, EnglishNameColumn] = i.EnglishName;
                        oRng[UserNameRow, DateColumn] = i.TotalTime;
                        status = WorkingDayUsedTime.TryGetValue(i.EnglishName, out temp);
                        if (!status)
                        {
                            temp = new List<string>();
                        }
                        temp.Add(i.TotalTime);
                        WorkingDayUsedTime[i.EnglishName] = temp;
                        UserNameRow++;
                    }
                   
                    UserNameRow = 2;
                    DateColumn++;
                }

                //插入使用分鐘數(天數加總)
                oRng[1, DateColumn] = "使用分鐘數(天數加總)";

                foreach (KeyValuePair<string, List<string>> kvp in WorkingDayUsedTime)
                {
                    string minute = "0", second = "0";
                    foreach (string i in kvp.Value)
                    {
                        if (i == "")
                        {
                            continue;
                        }

                        string[] splitTime = i.Split(new char[] { '分', '秒' });
                        minute = (Convert.ToInt32(minute) + Convert.ToInt32(splitTime[0])).ToString();
                        second = (Convert.ToInt32(second) + Convert.ToInt32(splitTime[1])).ToString();
                    }
                    minute = (Convert.ToInt32(minute) + (Convert.ToInt32(second) / 60)).ToString();
                    second = (Convert.ToInt32(second) % 60).ToString();
                    string totalTime = string.Format("{0}分{1}秒", minute, second);
                    oRng[UserNameRow, DateColumn] = totalTime;
                    UserNameRow++;
                }
                #endregion

                sheet = (Worksheet)book.Sheets[2];
                //建立Sheet
                string StrDate = "";
                List<string> ListDate = new List<string>();
                foreach (KeyValuePair<Key,List<Value>> kvp in DicData)
                {
                    if (StrDate == "" || StrDate != kvp.Key.Date)
                    {
                        ListDate.Add(kvp.Key.Date);
                    }
                    StrDate = kvp.Key.Date;
                }
                for (int i = 0; i < ListDate.Count - 1; i++ )
                {
                    sheet.Copy(System.Type.Missing, excelApp.Workbooks[1].Worksheets[1]);
                }
                //更改Sheet名稱
                for (int i = 0; i < ListDate.Count; i++ )
                {
                    sheet = (Worksheet)book.Sheets[i + 2];
                    sheet.Name = ListDate[i];
                }

                foreach (string i in ListDate)
                {
                    //取得對應的Sheet
                    for (int j = 1; j < book.Worksheets.Count; j++)
                    {
                        sheet = (Worksheet)book.Sheets[j + 1];
                        if (i != sheet.Name)
                        {
                            continue;
                        }
                        break;
                    }
                    int Row = 2, OutTimeColumn = 4, InTimeColumn = 5, DurationTimeColumn = 6, TotalTimeColumn = 7;
                    foreach (KeyValuePair<Key, List<Value>> kvp in DicData)
                    {
                        if (i != kvp.Key.Date)
                        {
                            continue;
                        }
                        oRng = (Range)sheet.Cells;
                        oRng[Row, DepartmentColumn] = kvp.Key.Department;
                        oRng[Row, ChineseNameColumn] = kvp.Key.ChineseName;
                        oRng[Row, EnglishNameColumn] = kvp.Key.EnglishName;
                        foreach (Value k in kvp.Value)
                        {
                            oRng[Row, OutTimeColumn] = k.OutTime;
                            oRng[Row, InTimeColumn] = k.InTime;
                            oRng[Row, DurationTimeColumn] = k.Duration;
                            oRng[Row, TotalTimeColumn] = k.TotalTime;
                            Row++;
                        }
                    }
                }

                book.SaveAs(Form1.OutputPath, XlFileFormat.xlWorkbookNormal, Type.Missing, Type.Missing, Type.Missing, Type.Missing,
                    XlSaveAsAccessMode.xlNoChange, Type.Missing, Type.Missing, Type.Missing, Type.Missing, Type.Missing);
                book.Close(Type.Missing, Type.Missing, Type.Missing);
                excelApp.Quit();
            }
            catch (System.Exception ex)
            {
                MessageBox.Show(ex.ToString());
                if (File.Exists(Form1.OutputPath))
                {
                    File.Delete(Form1.OutputPath);
                }
                book.SaveAs(Form1.OutputPath, XlFileFormat.xlWorkbookNormal, Type.Missing, Type.Missing, Type.Missing,
                       Type.Missing, XlSaveAsAccessMode.xlNoChange, Type.Missing, Type.Missing, Type.Missing, Type.Missing, Type.Missing);
                book.Close(Type.Missing, Type.Missing, Type.Missing);
                excelApp.Quit();
                File.Delete(Form1.OutputPath);
                return false;
            }
            finally
            {
                Dispose(excelApp, book, sheet, oRng);
            }
            return true;
        }

        #region 小FUN
        public static void Dispose(ApplicationClass excelApp, Workbook workBook, Worksheet workSheet, Range workRange)
        {
            System.Runtime.InteropServices.Marshal.ReleaseComObject(workRange);
            System.Runtime.InteropServices.Marshal.ReleaseComObject(workSheet);
            System.Runtime.InteropServices.Marshal.ReleaseComObject(workBook);
            System.Runtime.InteropServices.Marshal.ReleaseComObject(excelApp);
        }
        public static bool GetUserName_AC(DateTime StartDate, int TotalDays, out List<UserData> ListUserName)
        {
            ListUserName = new List<UserData>();
            try
            {
                for (int i = 0; i < TotalDays; i++)
                {
                    IList<Sys_ACLicense> listData = new List<Sys_ACLicense>();
                    using (ISession session = MyHibernateHelper.SessionFactory.OpenSession())
                    {
                        listData = session.QueryOver<Sys_ACLicense>().Where(x => x.date == StartDate.AddDays(i).ToLongDateString()).List();
                        session.Close();
                    }
                    if (listData.Count == 0)
                    {
                        continue;
                    }

                    foreach (Sys_ACLicense j in listData)
                    {
                        UserData sUserData = new UserData();
                        sUserData.Department = j.department;
                        sUserData.ChineseName = j.userChinese;
                        sUserData.EnglishName = j.username;

                        if (ListUserName.Contains(sUserData))
                        {
                            continue;
                        }
                        ListUserName.Add(sUserData);
                    }
                }
            }
            catch (System.Exception ex)
            {
                return false;
            }
            return true;
        }
        public static bool GetUserName_NX9(DateTime StartDate, int TotalDays, out List<UserData> ListUserName)
        {
            ListUserName = new List<UserData>();
            try
            {
                for (int i = 0; i < TotalDays; i++)
                {
                    IList<Sys_NX9License> listData = new List<Sys_NX9License>();
                    using (ISession session = MyHibernateHelper.SessionFactory.OpenSession())
                    {
                        listData = session.QueryOver<Sys_NX9License>().Where(x => x.date == StartDate.AddDays(i).ToLongDateString()).List();
                        session.Close();
                    }
                    if (listData.Count == 0)
                    {
                        continue;
                    }

                    foreach (Sys_NX9License j in listData)
                    {
                        UserData sUserData = new UserData();
                        sUserData.Department = j.department;
                        sUserData.ChineseName = j.userChinese;
                        sUserData.EnglishName = j.username;

                        if (ListUserName.Contains(sUserData))
                        {
                            continue;
                        }
                        ListUserName.Add(sUserData);
                    }
                }
            }
            catch (System.Exception ex)
            {
                return false;
            }
            return true;
        }
        public static bool GetUserName_AVM(DateTime StartDate, int TotalDays, out List<UserData> ListUserName)
        {
            ListUserName = new List<UserData>();
            try
            {
                for (int i = 0; i < TotalDays; i++)
                {
                    IList<Sys_AVMLicense> listData = new List<Sys_AVMLicense>();
                    using (ISession session = MyHibernateHelper.SessionFactory.OpenSession())
                    {
                        listData = session.QueryOver<Sys_AVMLicense>().Where(x => x.date == StartDate.AddDays(i).ToLongDateString()).List();
                        session.Close();
                    }
                    if (listData.Count == 0)
                    {
                        continue;
                    }

                    foreach (Sys_AVMLicense j in listData)
                    {
                        UserData sUserData = new UserData();
                        sUserData.Department = j.department;
                        sUserData.ChineseName = j.userChinese;
                        sUserData.EnglishName = j.username;

                        if (ListUserName.Contains(sUserData))
                        {
                            continue;
                        }
                        ListUserName.Add(sUserData);
                    }
                }
            }
            catch (System.Exception ex)
            {
                return false;
            }
            return true;
        }
        #endregion
    }
}
