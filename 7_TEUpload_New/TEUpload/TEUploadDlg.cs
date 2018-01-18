using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Windows.Forms;
using NXOpen;
using NXOpen.UF;
using CaxGlobaltek;
using System.IO;
using System.Text.RegularExpressions;
using NXOpen.CAM;
using NHibernate;

namespace TEUpload
{
    public partial class TEUploadDlg : DevComponents.DotNetBar.Office2007Form
    {
        public static ISession session = MyHibernateHelper.SessionFactory.OpenSession();
        public static Session theSession = Session.GetSession();
        public static UI theUI;
        public static UFSession theUfSession = UFSession.GetUFSession();
        public static Part workPart = theSession.Parts.Work;
        public static Part displayPart = theSession.Parts.Display;
        public static METE_Download_Upload_Path cMETE_Download_Upload_Path = new METE_Download_Upload_Path();
        public static string Server_OP_Folder = "", tempLocal_Folder_CAM = "";
        public static Dictionary<string, PartDirData> DicPartDirData = new Dictionary<string, PartDirData>();
        public static ExcelDirData sExcelDirData = new ExcelDirData();
        public static NCProgramDirData sNCProgramDirData = new NCProgramDirData();
        public PartInfo sPartInfo = new PartInfo();
        public static bool status;

        public struct NCProgramDirData
        {
            public string NCProgramLocalDir { get; set; }
            public string NCProgramServerDir { get; set; }
        }

        public struct ExcelDirData
        {
            public string ExcelShopDocLocalDir { get; set; }
            public string ExcelShopDocServerDir { get; set; }
        }

        public struct PartDirData
        {
            public string PartLocalDir { get; set; }
            public string PartServer1Dir { get; set; }
            //public string PartServer2Dir { get; set; }
        }

        

        //public struct PartInfo
        //{
        //    public string CusName { get; set; }
        //    public string PartNo { get; set; }
        //    public string CusRev { get; set; }
        //    public string OpNum { get; set; }
        //}

        public TEUploadDlg()
        {
            InitializeComponent();
        }

        private void TEUploadDlg_Load(object sender, EventArgs e)
        {
            //取得METEDownload_Upload.dat
            CaxGetDatData.GetMETEDownload_Upload(out cMETE_Download_Upload_Path);

            //取得料號
            PartNoLabel.Text = Path.GetFileNameWithoutExtension(displayPart.FullPath).Split('_')[0];
            OISLabel.Text = Path.GetFileNameWithoutExtension(displayPart.FullPath).Split('_')[1];

            //將Local_Folder_CAM先暫存起來，然後改變成Server路徑
            //tempLocal_Folder_CAM = cMETE_Download_Upload_Path.Local_Folder_CAM;

            status = CaxPublic.GetAllPath("TE", displayPart.FullPath, out sPartInfo, ref cMETE_Download_Upload_Path);
            if (!status)
            {
                MessageBox.Show("路徑取代錯誤，無法上傳，請聯繫開發工程師");
                this.Close();
            }
            
            //拆零件路徑字串取得客戶名稱、料號、版本
            //status = PartExcelNC.GetPartData(displayPart,out sPartInfo);
            //if (!status)
            //{
            //    MessageBox.Show("取得客戶名稱、料號、版本失敗，請聯繫開發工程師");
            //    this.Close();
            //}

            
            #region (註解中)取代路徑字串
            /*
            cMETE_Download_Upload_Path.Server_ShareStr = cMETE_Download_Upload_Path.Server_ShareStr.Replace("[Server_IP]", cMETE_Download_Upload_Path.Server_IP);
            cMETE_Download_Upload_Path.Server_ShareStr = cMETE_Download_Upload_Path.Server_ShareStr.Replace("[CusName]", sPartInfo.CusName);
            cMETE_Download_Upload_Path.Server_ShareStr = cMETE_Download_Upload_Path.Server_ShareStr.Replace("[PartNo]", sPartInfo.PartNo);
            cMETE_Download_Upload_Path.Server_ShareStr = cMETE_Download_Upload_Path.Server_ShareStr.Replace("[CusRev]", sPartInfo.CusRev);
            Server_OP_Folder = string.Format(@"{0}\{1}", cMETE_Download_Upload_Path.Server_ShareStr, "OP" + sPartInfo.OpNum);
            
            //將Local_Folder_OIS先暫存起來，然後改變成Server路徑
            tempLocal_Folder_CAM = cMETE_Download_Upload_Path.Local_Folder_CAM;
            cMETE_Download_Upload_Path.Local_ShareStr = cMETE_Download_Upload_Path.Local_ShareStr.Replace("[Local_IP]", cMETE_Download_Upload_Path.Local_IP);
            cMETE_Download_Upload_Path.Local_ShareStr = cMETE_Download_Upload_Path.Local_ShareStr.Replace("[CusName]", sPartInfo.CusName);
            cMETE_Download_Upload_Path.Local_ShareStr = cMETE_Download_Upload_Path.Local_ShareStr.Replace("[PartNo]", sPartInfo.PartNo);
            cMETE_Download_Upload_Path.Local_ShareStr = cMETE_Download_Upload_Path.Local_ShareStr.Replace("[CusRev]", sPartInfo.CusRev);
            cMETE_Download_Upload_Path.Local_Folder_CAM = cMETE_Download_Upload_Path.Local_Folder_CAM.Replace("[Local_ShareStr]", cMETE_Download_Upload_Path.Local_ShareStr);
            cMETE_Download_Upload_Path.Local_Folder_CAM = cMETE_Download_Upload_Path.Local_Folder_CAM.Replace("[Oper1]", sPartInfo.OpNum);
            */
            #endregion
            
            //處理Part的路徑
            status = PartExcelNC.PartFilePath(displayPart, sPartInfo, listView1, out DicPartDirData);
            if (!status)
            {
                MessageBox.Show("Part路徑處理失敗，請聯繫開發工程師");
                this.Close();
            }

            //string Server_Folder_CAM = "";
            //Server_Folder_CAM = tempLocal_Folder_CAM.Replace("[Local_ShareStr]", cMETE_Download_Upload_Path.Server_ShareStr);
            //Server_Folder_CAM = Server_Folder_CAM.Replace("[Oper1]", sPartInfo.OpNum);

            //整個CAM資料夾上傳
            listView1.Items.Add("資料夾：");
            listView1.Items.Add(cMETE_Download_Upload_Path.Local_Folder_CAM);

            #region (註解)處理Excel的路徑
            /*
            string[] FolderFile = System.IO.Directory.GetFileSystemEntries(cMETE_Download_Upload_Path.Local_Folder_CAM, "*.xls");
            long FileTime = new long();
            for (int i = 0; i < FolderFile.Length; i++)
            {
                System.IO.FileInfo ExcelInfo = new System.IO.FileInfo(FolderFile[i]);
                if (ExcelInfo.LastAccessTime.ToFileTime() > FileTime)
                {
                    FileTime = ExcelInfo.LastAccessTime.ToFileTime();
                    sExcelDirData.ExcelShopDocLocalDir = FolderFile[i];
                    sExcelDirData.ExcelShopDocServerDir = string.Format(@"{0}\{1}", Server_Folder_CAM, ExcelInfo.Name);
                }
            }
            if (File.Exists(sExcelDirData.ExcelShopDocLocalDir))
            {
                listView1.Items.Add(Path.GetFileName(sExcelDirData.ExcelShopDocLocalDir));
            }
            */
            #endregion

            #region (註解)處理NC程式的路徑
            /*
            string Local_NC_Folder = string.Format(@"{0}\{1}", cMETE_Download_Upload_Path.Local_Folder_CAM, "NC");
            if (Directory.Exists(Local_NC_Folder))
            {
                sNCProgramDirData.NCProgramLocalDir = Local_NC_Folder;
                sNCProgramDirData.NCProgramServerDir = string.Format(@"{0}\{1}", Server_Folder_CAM, "NC");
                listView1.Items.Add("NC資料夾");
            }
            */
            #endregion
        }

        private void OK_Click(object sender, EventArgs e)
        {
            #region Part上傳
            
            List<string> ListPartName = new List<string>();
            string[] PartText;
            foreach (KeyValuePair<string, PartDirData> kvp in DicPartDirData)
            {
                //判斷Part是否存在
                if (!File.Exists(kvp.Value.PartLocalDir))
                {
                    MessageBox.Show("Part不存在，無法上傳");
                    return;
                }
                try
                {
                    File.Copy(kvp.Value.PartLocalDir, kvp.Value.PartServer1Dir, true);
                }
                catch (System.Exception ex)
                {
                    MessageBox.Show(ex.ToString());
                    MessageBox.Show(Path.GetFileName(kvp.Value.PartLocalDir) + "上傳失敗");
                    return;
                }

                ListPartName.Add(kvp.Key + ".prt");
            }
            PartText = ListPartName.ToArray();
            System.IO.File.WriteAllLines(string.Format(@"{0}\{1}\{2}", cMETE_Download_Upload_Path.Server_ShareStr, "OP" + sPartInfo.OpNum, "PartNameText_CAM.txt"), PartText);
            
            #endregion

            //CAM資料夾上傳
            status = CaxPublic.DirectoryCopy(cMETE_Download_Upload_Path.Local_Folder_CAM, cMETE_Download_Upload_Path.Server_Folder_CAM, true);
            if (!status)
            {
                MessageBox.Show("CAM資料夾複製失敗，請聯繫開發工程師");
                this.Close();
            }
            #region (註解中)Excel上傳
            /*
            if (File.Exists(sExcelDirData.ExcelShopDocLocalDir))
            {
                try
                {
                    File.Copy(sExcelDirData.ExcelShopDocLocalDir, sExcelDirData.ExcelShopDocServerDir, true);
                }
                catch (System.Exception ex)
                {
                    CaxLog.ShowListingWindow("ShopDoc.xls上傳失敗");
                    this.Close();
                }
            }
            */
            #endregion

            #region (註解)NC上傳
            /*
            if (Directory.Exists(sNCProgramDirData.NCProgramLocalDir))
            {
                try
                {
                    CaxPublic.DirectoryCopy(sNCProgramDirData.NCProgramLocalDir, sNCProgramDirData.NCProgramServerDir, true);
                }
                catch (System.Exception ex)
                {
                    CaxLog.ShowListingWindow("NC上傳失敗");
                    this.Close();
                }
            }
            */
            #endregion
            
            #region 上傳至資料庫
            /*
            NCGroup[] NCGroupAry = displayPart.CAMSetup.CAMGroupCollection.ToArray();
            Dictionary<string, Function.OperData> DicNCData = new Dictionary<string, Function.OperData>();
            status = Function.GetNCProgramData(NCGroupAry, out DicNCData);
            if (!status)
            {
                MessageBox.Show("取得NC資料失敗，無法上傳至數據庫，僅上傳NX檔案");
                goto finish;
            }
            
            Com_PEMain comPEMain = new Com_PEMain(); 
            status = Function.GetCom_PEMain(sPartInfo, out comPEMain);
            if (!status)
            {
                MessageBox.Show("資料庫無此筆料號，僅上傳NX檔案");
                goto finish;
            }

            Com_PartOperation comPartOperation = new Com_PartOperation();
            status = Function.GetCom_PartOperation(sPartInfo, comPEMain, out comPartOperation);
            if (!status)
            {
                MessageBox.Show("資料庫無此筆料號，僅上傳NX檔案");
                goto finish;
            }

            foreach (KeyValuePair<string,Function.OperData> kvp in DicNCData)
            {
                Com_TEMain ComTEMain = session.QueryOver<Com_TEMain>()
                                               .Where(x => x.comPartOperation == comPartOperation)
                                               .And(x => x.ncGroupName == kvp.Key)
                                               .SingleOrDefault();
                if (ComTEMain == null)
                {
                    //表示未插入過此NC資料，直接插入

                }
            }
            */
            #endregion

            //finish:
            MessageBox.Show("上傳完成！");
            this.Close();
        }

        

        private void Cancel_Click(object sender, EventArgs e)
        {
            this.Close();
        }
    }
}
