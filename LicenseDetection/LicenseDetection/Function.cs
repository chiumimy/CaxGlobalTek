using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.IO;
using System.Windows.Forms;
using Microsoft.Office.Interop.Excel;
using CaxGlobaltek;
using NHibernate;

namespace LicenseDetection
{
    public class Function
    {
        //public static string FileLocationFolder = string.Format(@"{0}\{1}\{2}", "D:", "LicenseFolder", DateTime.Now.ToString("M_d_yyyy"));
        public static string OutputPath = "";
        public static string Department = Environment.GetEnvironmentVariable("Department");
        //public static string Department = "NX9";
        //public static string Department = "AC";
        public static string FileLocationFolder { get 
                                                  {
                                                      return string.Format(@"{0}\{1}\{2}\{3}",
                                                          "D:",
                                                          "LicenseFolder",
                                                          Department,
                                                          DateTime.Now.ToString("M_d_yyyy"));
                                                  } 
                                                }


        public static bool CreateFolder()
        {
            try
            {
                if (!Directory.Exists(FileLocationFolder))
                {
                    System.IO.Directory.CreateDirectory(FileLocationFolder);
                }
                if (File.Exists(string.Format(@"{0}\{1}", FileLocationFolder, DateTime.Now.ToString("M_d_yyyy") + ".log")))
                {
                    File.Delete(string.Format(@"{0}\{1}", FileLocationFolder, DateTime.Now.ToString("M_d_yyyy") + ".log"));
                }
                if (Department == "AVM")
                {
                    File.Copy(Variable.AVMLogFilePath, string.Format(@"{0}\{1}", FileLocationFolder, DateTime.Now.ToString("M_d_yyyy") + ".log"), true);
                }
                else if (Department == "AC")
                {
                    File.Copy(Variable.ACLogFilePath, string.Format(@"{0}\{1}", FileLocationFolder, DateTime.Now.ToString("M_d_yyyy") + ".log"), true);
                }
                else if (Department == "NX9")
                {
                    File.Copy(Variable.NX9LogFilePath, string.Format(@"{0}\{1}", FileLocationFolder, DateTime.Now.ToString("M_d_yyyy") + ".log"), true);
                }
            }
            catch (System.Exception ex)
            {
                return false;
            }
            return true;
        }
        public static bool SaveListMessage(string pathString, out List<string> ListMessage)
        {
            ListMessage = new List<string>();
            try
            {
                int lineCount = 0, timestamp = 1000000000;
                string line;
                using(System.IO.StreamReader file = new System.IO.StreamReader(string.Format(@"{0}\{1}", pathString, DateTime.Now.ToString("M_d_yyyy") + ".log")))
                {
                    while ((line = file.ReadLine()) != null)
                    {
                        lineCount++;
                        if (line.Contains(Variable.TodayDate))
                        {
                            timestamp = lineCount;
                        }
                        if (lineCount < timestamp)
                        {
                            continue;
                        }
                        if (!((line.Contains("OUT:") & line.Contains("gateway")) || (line.Contains("IN:") & line.Contains("gateway"))))
                        {
                            continue;
                        }
                        ListMessage.Add(line);
                    }
                }
                
                if (ListMessage.Count == 0)
                {
                    return false;
                }
                Show(pathString, ListMessage.ToArray());
            }
            catch (System.Exception ex)
            {
                return false;
            }
            return true;
        }
        public static void Show(string pathString, string[] i)
        {
            try
            {
                string TxtPath = string.Format(@"{0}\{1}", FileLocationFolder, "LogTxt" + ".txt");
                
                System.IO.File.WriteAllLines(TxtPath, i);
            }
            catch (System.Exception ex)
            {
                //MessageBox.Show(ex.ToString());
            }
        }
        public static bool ExportExcel(Dictionary<Variable.UserData, List<Variable.OutInTime>> DicUser)
        {
            ApplicationClass excelApp = new ApplicationClass();
            Workbook book = null;
            Worksheet sheet = null;
            Range oRng = null;
            try
            {
                OutputPath = string.Format(@"{0}\{1}", FileLocationFolder, DateTime.Today.ToLongDateString() + ".xls");
                if (File.Exists(OutputPath))
                {
                    File.Delete(OutputPath);
                }
                excelApp.Visible = false;
                book = excelApp.Workbooks.Open(string.Format(@"{0}\{1}\{2}", "D:", "LicenseFolder", "LicenseDetection_Today.xls"));
                sheet = (Worksheet)book.Sheets[1];
                oRng = (Range)sheet.Cells;

                int Row = 2, DepartmentColumn = 1, ChineseNameColumn = 2, EnglishNameColumn = 3, OutTimeColumn = 4, InTimeColumn = 5, DurationTimeColumn = 6, TotalTimeColumn = 7;
                foreach (KeyValuePair<Variable.UserData, List<Variable.OutInTime>> kvp in DicUser)
                {
                    oRng[Row, DepartmentColumn] = kvp.Key.Department;
                    oRng[Row, ChineseNameColumn] = kvp.Key.ChineseName;
                    oRng[Row, EnglishNameColumn] = kvp.Key.EnglishName;
                    foreach (Variable.OutInTime i in kvp.Value)
                    {
                        oRng[Row, OutTimeColumn] = i.OutTime;
                        oRng[Row, InTimeColumn] = i.InTime;
                        oRng[Row, DurationTimeColumn] = i.Duration;
                        oRng[Row, TotalTimeColumn] = i.TotalTime;
                        Row++;
                    }
                }
                book.SaveAs(OutputPath, XlFileFormat.xlWorkbookNormal, Type.Missing, Type.Missing, Type.Missing, Type.Missing,
                    XlSaveAsAccessMode.xlNoChange, Type.Missing, Type.Missing, Type.Missing, Type.Missing, Type.Missing);
                book.Close(Type.Missing, Type.Missing, Type.Missing);
                excelApp.Quit();
            }
            catch (System.Exception ex)
            {
                if (File.Exists(OutputPath))
                {
                    File.Delete(OutputPath);
                }
                book.SaveAs(OutputPath, XlFileFormat.xlWorkbookNormal, Type.Missing, Type.Missing, Type.Missing,
                       Type.Missing, XlSaveAsAccessMode.xlNoChange, Type.Missing, Type.Missing, Type.Missing, Type.Missing, Type.Missing);
                book.Close(Type.Missing, Type.Missing, Type.Missing);
                excelApp.Quit();
                File.Delete(OutputPath);
                return false;
            }

            finally
            {
                Dispose(excelApp, book, sheet, oRng);
            }
            return true;
        }
        public static void Dispose(ApplicationClass excelApp, Workbook workBook, Worksheet workSheet, Range workRange)
        {
            System.Runtime.InteropServices.Marshal.ReleaseComObject(workRange);
            System.Runtime.InteropServices.Marshal.ReleaseComObject(workSheet);
            System.Runtime.InteropServices.Marshal.ReleaseComObject(workBook);
            System.Runtime.InteropServices.Marshal.ReleaseComObject(excelApp);
        }
        public static bool CalculateTotalTime(ref Dictionary<Variable.UserData, List<Variable.OutInTime>> DicUser)
        {
            try
            {

                foreach (KeyValuePair<Variable.UserData, List<Variable.OutInTime>> kvp in DicUser.ToArray())
                {
                    string totalTimes = "";
                    int overalltime = 0;
                    List<Variable.OutInTime> temp = new List<Variable.OutInTime>();
                    foreach (Variable.OutInTime i in kvp.Value)
                    {
                        Variable.OutInTime stemp = new Variable.OutInTime();
                        if (i.Duration == null)
                        {
                            totalTimes = "使用中或未關閉NX";
                            stemp.OutTime = i.OutTime;
                            stemp.TotalTime = totalTimes;
                            temp.Add(stemp);
                            continue;
                        }
                        string[] total = i.Duration.Split(':');
                        int HrstoSecond = Convert.ToInt32(total[0]) * 60 * 60;
                        int MinstoSecond = Convert.ToInt32(total[1]) * 60;
                        int totalTime = HrstoSecond + MinstoSecond + Convert.ToInt32(total[2]);
                        
                        if (totalTimes == "")
                        {
                            overalltime = totalTime;
                        }
                        else
                        {
                            overalltime = overalltime + totalTime;
                        }

                        totalTimes = string.Format("{0}分{1}秒", (overalltime / 60).ToString(), (overalltime % 60).ToString());
                        stemp.OutTime = i.OutTime;
                        stemp.InTime = i.InTime;
                        stemp.Duration = i.Duration;
                        stemp.TotalTime = totalTimes;
                        temp.Add(stemp);
                    }
                    DicUser[kvp.Key] = temp;
                }
            }
            catch (System.Exception ex)
            {
                return false;
            }
            return true;
        }
        /*
        public static bool DeleteDatabase(string department)
        {
            try
            {
                if (department == "AVM")
                {
                    IList<Sys_AVMLicense> listData = new List<Sys_AVMLicense>();
                    using (ISession session = MyHibernateHelper.SessionFactory.OpenSession())
                    {
                        listData = session.QueryOver<Sys_AVMLicense>().Where(x => x.date == DateTime.Today.ToLongDateString()).List();
                        using (ITransaction trans = session.BeginTransaction())
                        {
                            foreach (Sys_AVMLicense i in listData)
                            {
                                session.Delete(i);
                            }
                            trans.Commit();
                        }
                    }
                }
                else if (department == "AC")
                {
                    IList<Sys_ACLicense> listData = new List<Sys_ACLicense>();
                    using (ISession session = MyHibernateHelper.SessionFactory.OpenSession())
                    {
                        listData = session.QueryOver<Sys_ACLicense>().Where(x => x.date == DateTime.Today.ToLongDateString()).List();
                        using (ITransaction trans = session.BeginTransaction())
                        {
                            foreach (Sys_ACLicense i in listData)
                            {
                                session.Delete(i);
                            }
                            trans.Commit();
                        }
                    }
                }
                
            }
            catch (System.Exception ex)
            {
                return false;
            }
            return true;
        }
        */
        public static bool DelDB<T>(IList<T> listData)
        {
            try
            {
                using (ISession session = MyHibernateHelper.SessionFactory.OpenSession())
                {
                    using (ITransaction trans = session.BeginTransaction())
                    {
                        foreach (T i in listData)
                        {
                            session.Delete(i);
                        }
                        trans.Commit();
                    }
                    session.Close();
                }
            }
            catch (System.Exception ex)
            {
                return false;
            }
            return true;
        }
        public static bool SaveDB_AVM(Dictionary<Variable.UserData, List<Variable.OutInTime>> DicUser)
        {
            try
            {
                Sys_AVMLicense sysLicenseRecord = new Sys_AVMLicense();
                foreach (KeyValuePair<Variable.UserData, List<Variable.OutInTime>> kvp in DicUser)
                {
                    foreach (Variable.OutInTime i in kvp.Value)
                    {
                        sysLicenseRecord.department = kvp.Key.Department;
                        sysLicenseRecord.userChinese = kvp.Key.ChineseName;
                        sysLicenseRecord.username = kvp.Key.EnglishName;
                        sysLicenseRecord.login = i.OutTime;
                        sysLicenseRecord.logout = i.InTime;
                        sysLicenseRecord.workingtime = i.Duration;
                        sysLicenseRecord.totaltime = i.TotalTime;
                        sysLicenseRecord.date = DateTime.Today.ToLongDateString();
                        if (!SaveDB<Sys_AVMLicense>(sysLicenseRecord))
                        {
                            return false;
                        }
                        
                    }
                }
            }
            catch (System.Exception ex)
            {
                return false;
            }
            return true;
        }
        public static bool SaveDB_AC(Dictionary<Variable.UserData, List<Variable.OutInTime>> DicUser)
        {
            try
            {
                Sys_ACLicense sysLicenseRecord = new Sys_ACLicense();
                foreach (KeyValuePair<Variable.UserData, List<Variable.OutInTime>> kvp in DicUser)
                {
                    foreach (Variable.OutInTime i in kvp.Value)
                    {
                        sysLicenseRecord.department = kvp.Key.Department;
                        sysLicenseRecord.userChinese = kvp.Key.ChineseName;
                        sysLicenseRecord.username = kvp.Key.EnglishName;
                        sysLicenseRecord.login = i.OutTime;
                        sysLicenseRecord.logout = i.InTime;
                        sysLicenseRecord.workingtime = i.Duration;
                        sysLicenseRecord.totaltime = i.TotalTime;
                        sysLicenseRecord.date = DateTime.Today.ToLongDateString();
                        if (!SaveDB<Sys_ACLicense>(sysLicenseRecord))
                        {
                            return false;
                        }
                    }
                }
            }
            catch (System.Exception ex)
            {
                return false;
            }
            return true;
        }
        public static bool SaveDB_NX9(Dictionary<Variable.UserData, List<Variable.OutInTime>> DicUser)
        {
            try
            {
                Sys_NX9License sysLicenseRecord = new Sys_NX9License();
                foreach (KeyValuePair<Variable.UserData, List<Variable.OutInTime>> kvp in DicUser)
                {
                    foreach (Variable.OutInTime i in kvp.Value)
                    {
                        sysLicenseRecord.department = kvp.Key.Department;
                        sysLicenseRecord.userChinese = kvp.Key.ChineseName;
                        sysLicenseRecord.username = kvp.Key.EnglishName;
                        sysLicenseRecord.login = i.OutTime;
                        sysLicenseRecord.logout = i.InTime;
                        sysLicenseRecord.workingtime = i.Duration;
                        sysLicenseRecord.totaltime = i.TotalTime;
                        sysLicenseRecord.date = DateTime.Today.ToLongDateString();
                        if (!SaveDB<Sys_NX9License>(sysLicenseRecord))
                        {
                            return false;
                        }
                    }
                }
            }
            catch (System.Exception ex)
            {
                return false;
            }
            return true;
        }
        public static bool SaveDB<T>(T table)
        {
            try
            {
                using (ISession session = MyHibernateHelper.SessionFactory.OpenSession())
                {
                    using (ITransaction trans = session.BeginTransaction())
                    {
                        session.Save(table);
                        trans.Commit();
                    }
                    session.Close();
                }
            }
            catch (System.Exception ex)
            {
                return false;
            }
            return true;
        }
        public static bool GetDepartmentUser(string UserName, out string Department, out string UserChinese)
        {
            Department = "";
            UserChinese = "";
            try
            {
                using (ISession session = MyHibernateHelper.SessionFactory.OpenSession())
                {
                    IList<Sys_User> ListSysUser = session.QueryOver<Sys_User>().List();
                    foreach (Sys_User i in ListSysUser)
                    {
                        if ((i.userEnglish == null) || (i.userEnglish.ToUpper() != UserName.ToUpper()))
                        {
                            continue;
                        }
                        Department = i.department;
                        UserChinese = i.userChinese;
                    }
                }
            }
            catch (System.Exception ex)
            {
                return false;
            }
            return true;
        }
        
    }
}
