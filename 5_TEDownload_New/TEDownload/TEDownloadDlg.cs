using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Windows.Forms;
using CaxGlobaltek;
using System.IO;

namespace TEDownload
{
    public partial class TEDownloadDlg : DevComponents.DotNetBar.Office2007Form
    {
        public static bool status;
        public static METEDownloadData cMETEDownloadData = new METEDownloadData();
        public static METE_Download_Upload_Path cMETE_Download_Upload_Path = new METE_Download_Upload_Path();
        public static string CurrentCusName = "", CurrentPartNo = "", CurrentCusRev = "", CurrentOper1 = "", CurrentOpRev = "";
        public static string Server_Folder_MODEL = "", Server_TEDownloadPart = "", Server_ShopDoc = "", Server_Folder_CAM = "";
        public static string Local_Folder_MODEL = "", Local_Folder_CAM = "", Local_Folder_OIS = "";
        public static Dictionary<string, string> DicSeleOper1 = new Dictionary<string, string>();
        public static List<string> ListSeleOper1 = new List<string>();
        public static string tempServer_TEDownloadPart = "", tempServer_Folder_CAM = "", tempLocal_Folder_CAM = "", tempLocal_Folder_OIS = "";
        public static int IndexofCusName = -1, IndexofPartNo = -1;
        public static List<string> ListDownloadPartPath = new List<string>();
        public static List<CAMPath> ListDownloadCAMPath = new List<CAMPath>();
        public static PECreateData cPECreateData = new PECreateData();

        public struct CAMPath
        {
            public string S_CAM_Path { get; set; }
            public string L_CAM_Path { get; set; }
        }

        public TEDownloadDlg()
        {
            InitializeComponent();

            #region 客戶資料填入
            string[] S_Task_CusName = Directory.GetDirectories(CaxEnv.GetGlobaltekTaskDir());
            foreach (string item in S_Task_CusName)
            {
                comboBoxCusName.Items.Add(Path.GetFileNameWithoutExtension(item));//走訪每個元素只取得目錄名稱(不含路徑)並加入dirlist集合中
            }
            PartNocomboBox.Enabled = false;
            CusRevcomboBox.Enabled = false;
            OpRevcomboBox.Enabled = false;
            Oper1comboBox.Enabled = false;
            #endregion

            /*
            //取得METEDownloadData資料
            status = CaxGetDatData.GetMETEDownloadData(out cMETEDownloadData);
            if (!status)
            {
                MessageBox.Show("取得METEDownloadData失敗");
                return;
            }
            //存入下拉選單-客戶
            for (int i = 0; i < cMETEDownloadData.EntirePartAry.Count; i++)
            {
                comboBoxCusName.Items.Add(cMETEDownloadData.EntirePartAry[i].CusName);
            }

            PartNocomboBox.Enabled = false;
            CusRevcomboBox.Enabled = false;
            Oper1comboBox.Enabled = false;
            */

            //取得METEDownload_Upload資料
            status = CaxGetDatData.GetMETEDownload_Upload(out cMETE_Download_Upload_Path);
            if (!status)
            {
                MessageBox.Show("取得METEDownload_Upload失敗");
                return;
            }
        }

        private void comboBoxCusName_SelectedIndexChanged(object sender, EventArgs e)
        {
            //清空ListView資訊
            listView.Items.Clear();
            //取得當前選取的客戶
            CurrentCusName = comboBoxCusName.Text;
            //打開&清空下拉選單-料號
            PartNocomboBox.Enabled = true;
            PartNocomboBox.Items.Clear();
            PartNocomboBox.Text = "";
            //關閉&清空下拉選單-客戶版次
            CusRevcomboBox.Enabled = false;
            CusRevcomboBox.Items.Clear();
            CusRevcomboBox.Text = "";
            //關閉&清空下拉選單-製程版次
            OpRevcomboBox.Enabled = false;
            OpRevcomboBox.Items.Clear();
            OpRevcomboBox.Text = "";
            //關閉&清空下拉選單-製程序
            Oper1comboBox.Enabled = false;
            Oper1comboBox.Items.Clear();
            Oper1comboBox.Text = "";

            string S_Task_CusName_Path = string.Format(@"{0}\{1}", CaxEnv.GetGlobaltekTaskDir(), CurrentCusName);
            string[] S_Task_PartNo = Directory.GetDirectories(S_Task_CusName_Path);
            foreach (string item in S_Task_PartNo)
            {
                PartNocomboBox.Items.Add(Path.GetFileNameWithoutExtension(item));//走訪每個元素只取得目錄名稱(不含路徑)並加入dirlist集合中
            }
        }

        private void PartNocomboBox_SelectedIndexChanged(object sender, EventArgs e)
        {
            //清空ListView資訊
            listView.Items.Clear();
            //取得當前選取的料號
            CurrentPartNo = PartNocomboBox.Text;
            //打開&清空下拉選單-客戶版次
            CusRevcomboBox.Enabled = true;
            CusRevcomboBox.Items.Clear();
            CusRevcomboBox.Text = "";
            //關閉&清空下拉選單-製程版次
            OpRevcomboBox.Enabled = false;
            OpRevcomboBox.Items.Clear();
            OpRevcomboBox.Text = "";
            //關閉&清空下拉選單-製程序
            Oper1comboBox.Enabled = false;
            Oper1comboBox.Items.Clear();
            Oper1comboBox.Text = "";

            string S_Task_PartNo_Path = string.Format(@"{0}\{1}\{2}", CaxEnv.GetGlobaltekTaskDir(), CurrentCusName, CurrentPartNo);
            string[] S_Task_CusRev = Directory.GetDirectories(S_Task_PartNo_Path);
            foreach (string item in S_Task_CusRev)
            {
                CusRevcomboBox.Items.Add(Path.GetFileNameWithoutExtension(item));//走訪每個元素只取得目錄名稱(不含路徑)並加入dirlist集合中
            }
            if (CusRevcomboBox.Items.Count == 1)
            {
                CusRevcomboBox.Text = CusRevcomboBox.Items[0].ToString();
            }
        }

        private void CusRevcomboBox_SelectedIndexChanged(object sender, EventArgs e)
        {
            //清空ListView資訊
            listView.Items.Clear();
            //取得當前選取的客戶版次
            CurrentCusRev = CusRevcomboBox.Text;
            //打開&清空下拉選單-製程版次
            OpRevcomboBox.Enabled = true;
            OpRevcomboBox.Items.Clear();
            OpRevcomboBox.Text = "";
            //關閉&清空下拉選單-製程序
            Oper1comboBox.Enabled = false;
            Oper1comboBox.Items.Clear();
            Oper1comboBox.Text = "";

            string S_Task_OpRev_Path = string.Format(@"{0}\{1}\{2}\{3}", CaxEnv.GetGlobaltekTaskDir(), CurrentCusName, CurrentPartNo, CurrentCusRev);
            string[] S_Task_OpRev = Directory.GetDirectories(S_Task_OpRev_Path);
            foreach (string item in S_Task_OpRev)
            {
                OpRevcomboBox.Items.Add(Path.GetFileNameWithoutExtension(item));//走訪每個元素只取得目錄名稱(不含路徑)並加入dirlist集合中
            }
            if (OpRevcomboBox.Items.Count == 1)
            {
                OpRevcomboBox.Text = OpRevcomboBox.Items[0].ToString();
            }
        }

        private void OpRevcomboBox_SelectedIndexChanged(object sender, EventArgs e)
        {
            //清空ListView資訊
            listView.Items.Clear();
            //取得當前選取的製程版次
            CurrentOpRev = OpRevcomboBox.Text;
            //打開&清空下拉選單-製程序
            Oper1comboBox.Enabled = true;
            Oper1comboBox.Items.Clear();
            Oper1comboBox.Text = "";

            //取得PECreateData.dat
            string PECreateData_Path = string.Format(@"{0}\{1}\{2}\{3}\{4}\{5}\{6}", CaxEnv.GetGlobaltekTaskDir(), CurrentCusName, CurrentPartNo, CurrentCusRev, CurrentOpRev, "MODEL", "PECreateData.dat");
            if (!File.Exists(PECreateData_Path))
            {
                CaxLog.ShowListingWindow("此料號沒有舊資料檔案，請檢查PECreateData.dat");
                return;
            }
            CaxPE.ReadPECreateData(PECreateData_Path, out cPECreateData);

            Oper1comboBox.Items.AddRange(cPECreateData.oper1Ary.ToArray());
            Oper1comboBox.Items.Remove("001");
            Oper1comboBox.Items.Remove("900");
            Oper1comboBox.Items.Add("全部下載");
        }

        private void Oper1comboBox_SelectedIndexChanged(object sender, EventArgs e)
        {
            //清空ListView資訊
            listView.Items.Clear();
            //取得當前選取的製程序
            CurrentOper1 = Oper1comboBox.Text;

            //建立Server路徑資料
            string Server_IP = cMETE_Download_Upload_Path.Server_IP;
            string Server_ShareStr = cMETE_Download_Upload_Path.Server_ShareStr;
            Server_Folder_MODEL = cMETE_Download_Upload_Path.Server_Folder_MODEL;
            Server_TEDownloadPart = cMETE_Download_Upload_Path.Server_TEDownloadPart;
            Server_ShopDoc = cMETE_Download_Upload_Path.Server_ShopDoc;
            Server_Folder_CAM = cMETE_Download_Upload_Path.Server_Folder_CAM;

            //取代字串成正確路徑
            Server_ShareStr = Server_ShareStr.Replace("[Server_IP]", Server_IP);
            Server_ShareStr = Server_ShareStr.Replace("[CusName]", CurrentCusName);
            Server_ShareStr = Server_ShareStr.Replace("[PartNo]", CurrentPartNo);
            Server_ShareStr = Server_ShareStr.Replace("[CusRev]", CurrentCusRev);
            Server_ShareStr = Server_ShareStr.Replace("[OpRev]", CurrentOpRev);
            Server_Folder_MODEL = Server_Folder_MODEL.Replace("[Server_ShareStr]", Server_ShareStr);
            Server_Folder_CAM = Server_Folder_CAM.Replace("[Server_ShareStr]", Server_ShareStr);
            Server_TEDownloadPart = Server_TEDownloadPart.Replace("[Server_ShareStr]", Server_ShareStr);
            Server_TEDownloadPart = Server_TEDownloadPart.Replace("[PartNo]", CurrentPartNo);
            Server_ShopDoc = Server_ShopDoc.Replace("[Server_IP]", Server_IP);
            

            #region (註解)判斷ShopDoc.xls是否存在
            /*
            if (!File.Exists(Server_ShopDoc))
            {
                listView.Items.Add("刀具路徑與清單樣板(ShopDoc.xls)不存在，無法下載");
                return;
            }
            listView.Items.Add("刀具路徑與清單樣板：" + Path.GetFileName(Server_ShopDoc));
            */
            #endregion

            #region (2016.12.30註解)判斷客戶檔案是否存在
            //Server_Folder_MODEL = string.Format(@"{0}\{1}", Server_Folder_MODEL, CurrentPartNo + ".prt");
            //if (!File.Exists(Server_Folder_MODEL))
            //{
            //    listView.Items.Add("客戶檔案不存在，無法下載");
            //    buttonDownload.Enabled = false;
            //    return;
            //}
            //listView.Items.Add("客戶檔案：" + Path.GetFileName(Server_Folder_MODEL));
            #endregion
            
            //暫存一個Server_MEDownloadPart，目的要讓程式每次都能有[Oper1]可取代
            tempServer_TEDownloadPart = Server_TEDownloadPart;
            tempServer_Folder_CAM = Server_Folder_CAM;

            #region 將選取到的Oper1紀錄成DicSeleOper1(Key = 製程序,Value = ServerPartPath)

            DicSeleOper1 = new Dictionary<string, string>();
            ListDownloadPartPath = new List<string>();
            

            if (CurrentOper1 == "全部下載")
            {
                for (int i = 0; i < Oper1comboBox.Items.Count; i++)
                {
                    if (Oper1comboBox.Items[i].ToString() == "全部下載")
                    {
                        continue;
                    }
                    //判斷OP資料夾內是否有PartNameText_CAM.txt，如果有，表示有上傳過，則讀取裡面檔案資料進行下載
                    string PartNameText_CAMPath = string.Format(@"{0}\{1}\{2}", Server_ShareStr, "OP" + Oper1comboBox.Items[i].ToString(), "PartNameText_CAM.txt");
                    if (File.Exists(PartNameText_CAMPath))
                    {
                        //取得已上傳過的檔案名稱
                        string[] PartNameText_CAMData = System.IO.File.ReadAllLines(PartNameText_CAMPath);
                        //開始記錄每個零件的路徑
                        foreach (string ii in PartNameText_CAMData)
                        {
                            Server_TEDownloadPart = string.Format(@"{0}\{1}", Server_ShareStr, ii);
                            ListDownloadPartPath.Add(Server_TEDownloadPart);
                        }
                        //紀錄ServerCAM資料夾路徑
                        //Server_Folder_CAM = tempServer_Folder_CAM;
                        //Server_Folder_CAM = Server_Folder_CAM.Replace("[Oper1]", Oper1comboBox.Items[i].ToString());
                        //ListDownloadCAMPath.Add(Server_Folder_CAM);
                    }
                    else
                    {
                        Server_TEDownloadPart = tempServer_TEDownloadPart;
                        Server_TEDownloadPart = Server_TEDownloadPart.Replace("[Oper1]", Oper1comboBox.Items[i].ToString());
                        ListDownloadPartPath.Add(Server_TEDownloadPart);
                    }
                }
            }
            else
            {
                //判斷OP資料夾內是否有PartNameText_CAM.txt，如果有，表示有上傳過，則讀取裡面檔案資料進行下載
                string PartNameText_CAMPath = string.Format(@"{0}\{1}\{2}", Server_ShareStr, "OP" + CurrentOper1, "PartNameText_CAM.txt");
                if (File.Exists(PartNameText_CAMPath))
                {
                    //取得已上傳過的檔案名稱
                    string[] PartNameText_CAMData = System.IO.File.ReadAllLines(PartNameText_CAMPath);
                    //開始記錄每個零件的路徑
                    foreach (string i in PartNameText_CAMData)
                    {
                        Server_TEDownloadPart = string.Format(@"{0}\{1}", Server_ShareStr, i);
                        ListDownloadPartPath.Add(Server_TEDownloadPart);
                    }
                    //紀錄ServerCAM資料夾路徑
                    //Server_Folder_CAM = tempServer_Folder_CAM;
                    //Server_Folder_CAM = Server_Folder_CAM.Replace("[Oper1]", CurrentOper1);
                    //ListDownloadCAMPath.Add(Server_Folder_CAM);
                }
                else
                {
                    Server_TEDownloadPart = tempServer_TEDownloadPart;
                    Server_TEDownloadPart = Server_TEDownloadPart.Replace("[Oper1]", CurrentOper1);
                    ListDownloadPartPath.Add(Server_TEDownloadPart);
                }
            }


            /*
            if (CurrentOper1 == "全部下載")
            {
                for (int i = 0; i < Oper1comboBox.Items.Count; i++)
                {
                    if (Oper1comboBox.Items[i].ToString() == "全部下載")
                    {
                        continue;
                    }
                    Server_TEDownloadPart = tempServer_TEDownloadPart;
                    Server_TEDownloadPart = Server_TEDownloadPart.Replace("[Oper1]", Oper1comboBox.Items[i].ToString());

                    string ServerPartPath = "";
                    status = DicSeleOper1.TryGetValue(Oper1comboBox.Items[i].ToString(), out ServerPartPath);
                    if (!status)
                    {
                        DicSeleOper1.Add(Oper1comboBox.Items[i].ToString(), Server_TEDownloadPart);
                    }
                }
            }
            else
            {
                Server_TEDownloadPart = tempServer_TEDownloadPart;
                Server_TEDownloadPart = Server_TEDownloadPart.Replace("[Oper1]", CurrentOper1);
                DicSeleOper1.Add(CurrentOper1, Server_TEDownloadPart);
            }
            */

            #endregion


            #region 判斷製程檔案是否存在
            foreach (string i in ListDownloadPartPath)
            {
                //判斷Part檔案是否存在
                if (!File.Exists(i))
                {
                    listView.Items.Add("Part檔案：");
                    listView.Items.Add(Path.GetFileName(i) + "不存在，請再次確認");
                    buttonDownload.Enabled = false;
                    return;
                }
                listView.Items.Add("Part檔案：" + Path.GetFileName(i));
            }
            #endregion

            buttonDownload.Enabled = true;



        }

        private void buttonDownload_Click(object sender, EventArgs e)
        {
            //建立Local路徑資料
            string Local_IP = cMETE_Download_Upload_Path.Local_IP;
            string Local_ShareStr = cMETE_Download_Upload_Path.Local_ShareStr;
            Local_Folder_MODEL = cMETE_Download_Upload_Path.Local_Folder_MODEL;
            Local_Folder_CAM = cMETE_Download_Upload_Path.Local_Folder_CAM;
            Local_Folder_OIS = cMETE_Download_Upload_Path.Local_Folder_OIS;

            //取代字串成正確路徑
            Local_ShareStr = Local_ShareStr.Replace("[Local_IP]", Local_IP);
            Local_ShareStr = Local_ShareStr.Replace("[CusName]", CurrentCusName);
            Local_ShareStr = Local_ShareStr.Replace("[PartNo]", CurrentPartNo);
            Local_ShareStr = Local_ShareStr.Replace("[CusRev]", CurrentCusRev);
            Local_ShareStr = Local_ShareStr.Replace("[OpRev]", CurrentOpRev);
            Local_Folder_MODEL = Local_Folder_MODEL.Replace("[Local_ShareStr]", Local_ShareStr);
            Local_Folder_CAM = Local_Folder_CAM.Replace("[Local_ShareStr]", Local_ShareStr);
            Local_Folder_OIS = Local_Folder_OIS.Replace("[Local_ShareStr]", Local_ShareStr);

            #region (2016.12.30註解)建立Local_Folder_MODEL資料夾

            //if (!File.Exists(Local_Folder_MODEL))
            //{
            //    try
            //    {
            //        System.IO.Directory.CreateDirectory(Local_Folder_MODEL);
            //    }
            //    catch (System.Exception ex)
            //    {
            //        MessageBox.Show(ex.ToString());
            //        return;
            //    }
            //}

            #endregion

            #region (2016.12.30註解)複製Server客戶檔案到Local_Folder_MODEL資料夾內
            /*
            //判斷客戶檔案是否存在
            status = System.IO.File.Exists(Server_Folder_MODEL);
            if (!status)
            {
                MessageBox.Show("指定的檔案不存在，請再次確認");
                return;
            }

            //建立Local_Folder_MODEL資料夾內客戶檔案路徑
            string Local_CusPartFullPath = string.Format(@"{0}\{1}", Local_Folder_MODEL, Path.GetFileName(Server_Folder_MODEL));

            //判斷是否存在，不存在則開始複製
            if (!File.Exists(Local_CusPartFullPath))
            {
                try
                {
                    File.Copy(Server_Folder_MODEL, Local_CusPartFullPath, true);
                }
                catch (System.Exception ex)
                {
                    CaxLog.ShowListingWindow("客戶檔案複製失敗");
                    this.Close();
                }
            }
            */
            #endregion

            #region (註解)複製ShopDoc.xls到Local_Folder_MODEL資料夾內
            /*
            //判斷ShopDoc.xls是否存在
            if (!File.Exists(Server_ShopDoc))
            {
                listView.Items.Add("刀具路徑與清單樣板(ShopDoc.xls)不存在，無法下載");
                return;
            }

            //建立Local_Folder_MODEL資料夾內客戶檔案路徑
            string Local_ShopDocFullPath = string.Format(@"{0}\{1}", Local_Folder_MODEL, Path.GetFileName(Server_ShopDoc));

            //判斷是否存在，不存在則開始複製
            if (!File.Exists(Local_ShopDocFullPath))
            {
                try
                {
                    File.Copy(Server_ShopDoc, Local_ShopDocFullPath, true);
                }
                catch (System.Exception ex)
                {
                    CaxLog.ShowListingWindow("ShopDoc.xls下載失敗");
                    this.Close();
                }
            }
            */
            #endregion

            #region 先刪除本機中該製程的OPxxx->CAM資料夾、TEpart檔
            //刪除本機OPxxx資料夾路徑
            for (int i = 0; i < Oper1comboBox.Items.Count; i++)
            {
                if (CurrentOper1 != Oper1comboBox.Items[i].ToString() & CurrentOper1 != "全部下載")
                {
                    continue;
                }
                string Local_OPxxxPath = string.Format(@"{0}\{1}\{2}", Local_ShareStr, "OP" + Oper1comboBox.Items[i].ToString(), "CAM");
                if (Directory.Exists(Local_OPxxxPath))
                {
                    Directory.Delete(Local_OPxxxPath, true);
                }
                string Local_TEpartPath = string.Format(@"{0}\{1}", Local_ShareStr, CurrentPartNo + "_TE_" + Oper1comboBox.Items[i].ToString() + ".prt");
                if (File.Exists(Local_TEpartPath))
                {
                    File.Delete(Local_TEpartPath);
                }
            }
            /*
            string Local_OPxxxPath = string.Format(@"{0}\{1}\{2}", Local_ShareStr, "OP" + CurrentOper1, "CAM");
            if (Directory.Exists(Local_OPxxxPath))
            {
                Directory.Delete(Local_OPxxxPath, true);
            }
            string Local_TEpartPath = string.Format(@"{0}\{1}", Local_ShareStr, CurrentPartNo + "_TE_" + CurrentOper1 + ".prt");
            if (File.Exists(Local_TEpartPath))
            {
                File.Delete(Local_TEpartPath);
            }
            */
            #endregion

            #region 建立Local_Folder_CAM、Local_Folder_OIS資料夾

            //暫存一個tempLocal_Folder_CAM、Local_Folder_OIS，目的要讓程式每次都能有[Oper1]可取代
            tempLocal_Folder_CAM = Local_Folder_CAM;
            tempLocal_Folder_OIS = Local_Folder_OIS;
            tempServer_Folder_CAM = Server_Folder_CAM;

            ListDownloadCAMPath = new List<CAMPath>();

            if (CurrentOper1 == "全部下載")
            {
                for (int i = 0; i < Oper1comboBox.Items.Count; i++)
                {
                    if (Oper1comboBox.Items[i].ToString() == "全部下載")
                    {
                        continue;
                    }
                    Local_Folder_CAM = tempLocal_Folder_CAM;
                    Local_Folder_OIS = tempLocal_Folder_OIS;
                    Local_Folder_CAM = Local_Folder_CAM.Replace("[Oper1]", Oper1comboBox.Items[i].ToString());
                    Local_Folder_OIS = Local_Folder_OIS.Replace("[Oper1]", Oper1comboBox.Items[i].ToString());
                    if (!File.Exists(Local_Folder_CAM))
                    {
                        try
                        {
                            System.IO.Directory.CreateDirectory(Local_Folder_CAM);
                        }
                        catch (System.Exception ex)
                        {
                            MessageBox.Show(ex.ToString());
                            return;
                        }
                    }
                    if (!File.Exists(Local_Folder_OIS))
                    {
                        try
                        {
                            System.IO.Directory.CreateDirectory(Local_Folder_OIS);
                        }
                        catch (System.Exception ex)
                        {
                            MessageBox.Show(ex.ToString());
                            return;
                        }
                    }
                    Server_Folder_CAM = tempServer_Folder_CAM;
                    Server_Folder_CAM = Server_Folder_CAM.Replace("[Oper1]", Oper1comboBox.Items[i].ToString());
                    CAMPath sCAMPath = new CAMPath();
                    sCAMPath.L_CAM_Path = Local_Folder_CAM;
                    sCAMPath.S_CAM_Path = Server_Folder_CAM;
                    ListDownloadCAMPath.Add(sCAMPath);
                }
            }
            else
            {
                Local_Folder_CAM = tempLocal_Folder_CAM;
                Local_Folder_OIS = tempLocal_Folder_OIS;
                Local_Folder_CAM = Local_Folder_CAM.Replace("[Oper1]", CurrentOper1);
                Local_Folder_OIS = Local_Folder_OIS.Replace("[Oper1]", CurrentOper1);
                if (!File.Exists(Local_Folder_CAM))
                {
                    try
                    {
                        System.IO.Directory.CreateDirectory(Local_Folder_CAM);
                    }
                    catch (System.Exception ex)
                    {
                        MessageBox.Show(ex.ToString());
                        return;
                    }
                }
                if (!File.Exists(Local_Folder_OIS))
                {
                    try
                    {
                        System.IO.Directory.CreateDirectory(Local_Folder_OIS);
                    }
                    catch (System.Exception ex)
                    {
                        MessageBox.Show(ex.ToString());
                        return;
                    }
                }
                Server_Folder_CAM = tempServer_Folder_CAM;
                Server_Folder_CAM = Server_Folder_CAM.Replace("[Oper1]", CurrentOper1);
                CAMPath sCAMPath = new CAMPath();
                sCAMPath.L_CAM_Path = Local_Folder_CAM;
                sCAMPath.S_CAM_Path = Server_Folder_CAM;
                ListDownloadCAMPath.Add(sCAMPath);
            }
            /*
            //DicSeleOper1(Key = 製程序,Value = ServerPartPath)
            foreach (KeyValuePair<string, string> kvp in DicSeleOper1)
            {
                Local_Folder_CAM = tempLocal_Folder_CAM;
                Local_Folder_OIS = tempLocal_Folder_OIS;
                Local_Folder_CAM = Local_Folder_CAM.Replace("[Oper1]", kvp.Key);
                Local_Folder_OIS = Local_Folder_OIS.Replace("[Oper1]", kvp.Key);
                if (!File.Exists(Local_Folder_CAM))
                {
                    try
                    {
                        System.IO.Directory.CreateDirectory(Local_Folder_CAM);
                    }
                    catch (System.Exception ex)
                    {
                        MessageBox.Show(ex.ToString());
                        return;
                    }
                }
                if (!File.Exists(Local_Folder_OIS))
                {
                    try
                    {
                        System.IO.Directory.CreateDirectory(Local_Folder_OIS);
                    }
                    catch (System.Exception ex)
                    {
                        MessageBox.Show(ex.ToString());
                        return;
                    }
                }
            }
            */
            #endregion

            #region 複製Server製程序檔案到Local資料夾內

            foreach (string i in ListDownloadPartPath)
            {
                //刪除本機TEpart
                string Local_TEpart = string.Format(@"{0}\{1}", Local_ShareStr, Path.GetFileName(i));
                if (File.Exists(Local_TEpart))
                {
                    File.Delete(Local_TEpart);
                }

                //判斷Part檔案是否存在
                if (!File.Exists(i))
                {
                    CaxLog.ShowListingWindow("製程序檔案" + Path.GetFileName(i) + "不存在，請再次確認");
                    //MessageBox.Show("製程序檔案" + Path.GetFileName(i) + "不存在，請再次確認");
                    this.Close();
                }
                //建立Local_ShareStr資料夾內製程序檔案路徑
                string Local_Oper1PartFullPath = string.Format(@"{0}\{1}", Local_ShareStr, Path.GetFileName(i));
                //開始複製
                try
                {
                    File.Copy(i, Local_Oper1PartFullPath, true);
                }
                catch (System.Exception ex)
                {
                    CaxLog.ShowListingWindow(Path.GetFileName(i) + "下載失敗");
                    this.Close();
                }
            }

            /*
            foreach (KeyValuePair<string, string> kvp in DicSeleOper1)
            {
                //判斷製程序檔案是否存在
                Server_TEDownloadPart = tempServer_TEDownloadPart;
                Server_TEDownloadPart = Server_TEDownloadPart.Replace("[Oper1]", kvp.Key);
                status = System.IO.File.Exists(Server_TEDownloadPart);
                if (!status)
                {
                    MessageBox.Show("製程序檔案" + Path.GetFileName(Server_TEDownloadPart) + "不存在，請再次確認");
                    return;
                }

                //建立Local_ShareStr資料夾內製程序檔案路徑
                string Local_Oper1PartFullPath = string.Format(@"{0}\{1}", Local_ShareStr, Path.GetFileName(Server_TEDownloadPart));

                //開始複製
                File.Copy(Server_TEDownloadPart, Local_Oper1PartFullPath, true);
            }
            */

            #endregion

            #region 複製ServerCAM資料夾到Local資料夾內
            foreach (CAMPath i in ListDownloadCAMPath)
            {
                status = CaxPublic.DirectoryCopy(i.S_CAM_Path, i.L_CAM_Path, true);
                if (!status)
                {
                    MessageBox.Show("CAM資料夾複製失敗，請聯繫開發工程師");
                    this.Close();
                }
            }
            #endregion

            MessageBox.Show("下載完成！");


            this.Close();
        }

        
    }
}
