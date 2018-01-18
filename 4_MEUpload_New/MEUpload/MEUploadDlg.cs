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
using NHibernate;
using MEUpload.DatabaseClass;
using NXOpen.Utilities;
using DevComponents.DotNetBar;

namespace MEUpload
{
    public partial class MEUploadDlg : DevComponents.DotNetBar.Office2007Form
    {
        public static Session theSession = Session.GetSession();
        public static UI theUI;
        public static UFSession theUfSession = UFSession.GetUFSession();
        public static Part workPart = theSession.Parts.Work;
        public static Part displayPart = theSession.Parts.Display;
        public static string Server_OP_Folder = "", tempLocal_Folder_OIS = "";
        public static METE_Download_Upload_Path cMETE_Download_Upload_Path = new METE_Download_Upload_Path();
        public static Dictionary<string, Function.PartDirData> DicPartDirData = new Dictionary<string, Function.PartDirData>();
        public static ExcelDirData sExcelDirData = new ExcelDirData();
        public static ISession session = MyHibernateHelper.SessionFactory.OpenSession();
        public PartInfo sPartInfo = new PartInfo();
        public static bool status;

        

        public struct ExcelDirData
        {
            public string ExcelIPQCLocalDir { get; set; }
            public string ExcelIPQCServerDir { get; set; }
            public string ExcelSelfCheckLocalDir { get; set; }
            public string ExcelSelfCheckServerDir { get; set; }
            public string ExcelIQCLocalDir { get; set; }
            public string ExcelIQCServerDir { get; set; }
            public string ExcelFAILocalDir { get; set; }
            public string ExcelFAIServerDir { get; set; }
            public string ExcelFQCLocalDir { get; set; }
            public string ExcelFQCServerDir { get; set; }
        }

        //public struct PartInfo 
        //{
        //    public static string CusName { get; set; }
        //    public static string PartNo { get; set; }
        //    public static string CusRev { get; set; }
        //    public static string OpRev { get; set; }
        //    public static string OpNum { get; set; }
        //}

        public MEUploadDlg()
        {
            InitializeComponent();
        }

        private void MEUploadDlg_Load(object sender, EventArgs e)
        {
            //int module_id;
            //theUfSession.UF.AskApplicationModule(out module_id);
            //if (module_id != UFConstants.UF_APP_DRAFTING)
            //{
            //    MessageBox.Show("請先轉換為製圖模組後再執行！");
            //    this.Close();
            //}

            ExportPFD.Checked = true;

            //取得METEDownload_Upload.dat
            CaxGetDatData.GetMETEDownload_Upload(out cMETE_Download_Upload_Path);
            

            //取得料號
            PartNoLabel.Text = Path.GetFileNameWithoutExtension(displayPart.FullPath).Split('_')[0];
            OISLabel.Text = Path.GetFileNameWithoutExtension(displayPart.FullPath).Split('_')[1];

            //將Local_Folder_OIS先暫存起來，然後改變成Server路徑
            //tempLocal_Folder_OIS = cMETE_Download_Upload_Path.Local_Folder_OIS;


            status = CaxPublic.GetAllPath("ME", displayPart.FullPath, out sPartInfo, ref cMETE_Download_Upload_Path);
            if (!status)
            {
                MessageBox.Show("取得路徑或拆分路徑失敗");
                this.Close();
                return;
            }

            


            //處理Part的路徑
            status = Function.GetComponentPath(displayPart, cMETE_Download_Upload_Path, listView1, ref DicPartDirData);
            if (!status)
            {
                MessageBox.Show("零件路徑取得失敗，無法上傳");
                this.Close();
                return;
            }
            

            #region (註解)處理Excel的路徑
            /*
            string[] FolderFile = System.IO.Directory.GetFileSystemEntries(cMETE_Download_Upload_Path.Local_Folder_OIS, "*.xls");
            //篩選出IPQC、SelfCheck
            List<string> List_ExcelIPQC = new List<string>();
            List<string> List_ExcelSelfCheck = new List<string>();
            List<string> List_ExcelIQC = new List<string>();
            List<string> List_ExcelFAI = new List<string>();
            List<string> List_ExcelFQC = new List<string>();
            foreach (string i in FolderFile)
            {
                if (i.Contains("IPQC"))
                {
                    List_ExcelIPQC.Add(i);
                }
                if (i.Contains("SelfCheck"))
                {
                    List_ExcelSelfCheck.Add(i);
                }
                if (i.Contains("IQC"))
                {
                    List_ExcelIQC.Add(i);
                }
                if (i.Contains("FAI"))
                {
                    List_ExcelFAI.Add(i);
                }
                if (i.Contains("FQC"))
                {
                    List_ExcelFQC.Add(i);
                }
            }

            long ExcelIPQCFileTime = new long();
            long ExcelSelfCheckFileTime = new long();
            long ExcelIQCFileTime = new long();
            long ExcelFAIFileTime = new long();
            long ExcelFQCFileTime = new long();

            #region 處理IPQC
            foreach (string i in List_ExcelIPQC)
            {
                System.IO.FileInfo ExcelInfo = new System.IO.FileInfo(i);
                if (ExcelInfo.LastAccessTime.ToFileTime() > ExcelIPQCFileTime)
                {
                    ExcelIPQCFileTime = ExcelInfo.LastAccessTime.ToFileTime();
                    sExcelDirData.ExcelIPQCLocalDir = i;
                    string Server_Folder_OIS = "";
                    Server_Folder_OIS = tempLocal_Folder_OIS.Replace("[Local_ShareStr]", cMETE_Download_Upload_Path.Server_ShareStr);
                    Server_Folder_OIS = Server_Folder_OIS.Replace("[Oper1]", PartInfo.OpNum);
                    sExcelDirData.ExcelIPQCServerDir = string.Format(@"{0}\{1}", Server_Folder_OIS, ExcelInfo.Name);
                }
            }
            if (List_ExcelIPQC.Count != 0)
            {
                listView1.Items.Add(Path.GetFileName(sExcelDirData.ExcelIPQCLocalDir));
            }
            #endregion

            #region 處理SelfCheck
            foreach (string i in List_ExcelSelfCheck)
            {
                System.IO.FileInfo ExcelInfo = new System.IO.FileInfo(i);
                if (ExcelInfo.LastAccessTime.ToFileTime() > ExcelSelfCheckFileTime)
                {
                    ExcelSelfCheckFileTime = ExcelInfo.LastAccessTime.ToFileTime();
                    sExcelDirData.ExcelSelfCheckLocalDir = i;
                    string Server_Folder_OIS = "";
                    Server_Folder_OIS = tempLocal_Folder_OIS.Replace("[Local_ShareStr]", cMETE_Download_Upload_Path.Server_ShareStr);
                    Server_Folder_OIS = Server_Folder_OIS.Replace("[Oper1]", PartInfo.OpNum);
                    sExcelDirData.ExcelSelfCheckServerDir = string.Format(@"{0}\{1}", Server_Folder_OIS, ExcelInfo.Name);
                }
            }
            if (List_ExcelSelfCheck.Count != 0)
            {
                listView1.Items.Add(Path.GetFileName(sExcelDirData.ExcelSelfCheckLocalDir));
            }
            #endregion

            #region 處理IQC
            foreach (string i in List_ExcelIQC)
            {
                System.IO.FileInfo ExcelInfo = new System.IO.FileInfo(i);
                if (ExcelInfo.LastAccessTime.ToFileTime() > ExcelIQCFileTime)
                {
                    ExcelIQCFileTime = ExcelInfo.LastAccessTime.ToFileTime();
                    sExcelDirData.ExcelIQCLocalDir = i;
                    string Server_Folder_OIS = "";
                    Server_Folder_OIS = tempLocal_Folder_OIS.Replace("[Local_ShareStr]", cMETE_Download_Upload_Path.Server_ShareStr);
                    Server_Folder_OIS = Server_Folder_OIS.Replace("[Oper1]", PartInfo.OpNum);
                    sExcelDirData.ExcelIQCServerDir = string.Format(@"{0}\{1}", Server_Folder_OIS, ExcelInfo.Name);
                }
            }
            if (List_ExcelIQC.Count != 0)
            {
                listView1.Items.Add(Path.GetFileName(sExcelDirData.ExcelIQCLocalDir));
            }
            #endregion

            #region 處理FAI
            foreach (string i in List_ExcelFAI)
            {
                System.IO.FileInfo ExcelInfo = new System.IO.FileInfo(i);
                if (ExcelInfo.LastAccessTime.ToFileTime() > ExcelFAIFileTime)
                {
                    ExcelFAIFileTime = ExcelInfo.LastAccessTime.ToFileTime();
                    sExcelDirData.ExcelFAILocalDir = i;
                    string Server_Folder_OIS = "";
                    Server_Folder_OIS = tempLocal_Folder_OIS.Replace("[Local_ShareStr]", cMETE_Download_Upload_Path.Server_ShareStr);
                    Server_Folder_OIS = Server_Folder_OIS.Replace("[Oper1]", PartInfo.OpNum);
                    sExcelDirData.ExcelFAIServerDir = string.Format(@"{0}\{1}", Server_Folder_OIS, ExcelInfo.Name);
                }
            }
            if (List_ExcelFAI.Count != 0)
            {
                listView1.Items.Add(Path.GetFileName(sExcelDirData.ExcelFAILocalDir));
            }
            #endregion

            #region 處理FQC
            foreach (string i in List_ExcelFQC)
            {
                System.IO.FileInfo ExcelInfo = new System.IO.FileInfo(i);
                if (ExcelInfo.LastAccessTime.ToFileTime() > ExcelFQCFileTime)
                {
                    ExcelFQCFileTime = ExcelInfo.LastAccessTime.ToFileTime();
                    sExcelDirData.ExcelFQCLocalDir = i;
                    string Server_Folder_OIS = "";
                    Server_Folder_OIS = tempLocal_Folder_OIS.Replace("[Local_ShareStr]", cMETE_Download_Upload_Path.Server_ShareStr);
                    Server_Folder_OIS = Server_Folder_OIS.Replace("[Oper1]", PartInfo.OpNum);
                    sExcelDirData.ExcelFQCServerDir = string.Format(@"{0}\{1}", Server_Folder_OIS, ExcelInfo.Name);
                }
            }
            if (List_ExcelFQC.Count != 0)
            {
                listView1.Items.Add(Path.GetFileName(sExcelDirData.ExcelFQCLocalDir));
            }
            #endregion
            */
            #endregion

        }

        private void OK_Click(object sender, EventArgs e)
        {
            CaxPart.SaveAll();

            //Part上傳
            List<string> ListPartName = new List<string>();
            status = Function.UploadPart(DicPartDirData, out ListPartName);
            if (!status)
            {
                this.Close();
                return;
            }
            System.IO.File.WriteAllLines(string.Format(@"{0}\{1}\{2}", cMETE_Download_Upload_Path.Server_ShareStr, "OP" + sPartInfo.OpNum, "PartNameText_OIS.txt"), ListPartName.ToArray());
            

            #region (註解)Excel上傳
            /*
            //Excel上傳
            if (File.Exists(sExcelDirData.ExcelIPQCLocalDir))
            {
                try
                {
                    File.Copy(sExcelDirData.ExcelIPQCLocalDir, sExcelDirData.ExcelIPQCServerDir, true);
                }
                catch (System.Exception ex)
                {
                    CaxLog.ShowListingWindow("IPQC.xls上傳失敗");
                    this.Close();
                }
            }

            if (File.Exists(sExcelDirData.ExcelSelfCheckLocalDir))
            {
                try
                {
                    File.Copy(sExcelDirData.ExcelSelfCheckLocalDir, sExcelDirData.ExcelSelfCheckServerDir, true);
                }
                catch (System.Exception ex)
                {
                    CaxLog.ShowListingWindow("SelfCheck.xls上傳失敗");
                    this.Close();
                }
            }

            if (File.Exists(sExcelDirData.ExcelIQCLocalDir))
            {
                try
                {
                    File.Copy(sExcelDirData.ExcelIQCLocalDir, sExcelDirData.ExcelIQCServerDir, true);
                }
                catch (System.Exception ex)
                {
                    CaxLog.ShowListingWindow("IQC.xls上傳失敗");
                    this.Close();
                }
            }

            if (File.Exists(sExcelDirData.ExcelFAILocalDir))
            {
                try
                {
                    File.Copy(sExcelDirData.ExcelFAILocalDir, sExcelDirData.ExcelFAIServerDir, true);
                }
                catch (System.Exception ex)
                {
                    CaxLog.ShowListingWindow("FAI.xls上傳失敗");
                    this.Close();
                }
            }

            if (File.Exists(sExcelDirData.ExcelFQCLocalDir))
            {
                try
                {
                    File.Copy(sExcelDirData.ExcelFQCLocalDir, sExcelDirData.ExcelFQCServerDir, true);
                }
                catch (System.Exception ex)
                {
                    CaxLog.ShowListingWindow("FQC.xls上傳失敗");
                    this.Close();
                }
            }
            */
            #endregion

            #region 資料上傳至Database
            //取得WorkPart資訊並檢查資料是否完整
            CaxME.WorkPartAttribute sWorkPartAttribute = new CaxME.WorkPartAttribute();
            //status = Function.GetWorkPartAttribute(workPart, out sWorkPartAttribute);
            status = CaxME.GetWorkPartAttribute(workPart, out sWorkPartAttribute);
            if (!status)
            {
                MessageBox.Show("workPart屬性取得失敗，無法上傳");
                this.Close();
                return;
            }
            if (sWorkPartAttribute.meExcelType == "" || sWorkPartAttribute.draftingVer == "" || sWorkPartAttribute.draftingDate == "" || 
                sWorkPartAttribute.partDescription == "" || sWorkPartAttribute.material == "")
            {
                MessageBox.Show("量測資訊不足，僅上傳Part檔案到伺服器");
                MessageBox.Show("上傳完成！");
                this.Close();
                return;
            }
            
            #region 取得所有量測尺寸資料
            int SheetCount = 0;
            NXOpen.Tag[] SheetTagAry = null;
            theUfSession.Draw.AskDrawings(out SheetCount, out SheetTagAry);

            List<NXOpen.Drawings.DrawingSheet> listDrawingSheet = new List<NXOpen.Drawings.DrawingSheet>();
            List<CaxME.DimensionData> listDimensionData = new List<CaxME.DimensionData>();
            NXOpen.Drawings.DrawingSheet FirstSheet = null;
            for (int i = 0; i < SheetCount; i++)
            {
                //打開Sheet並記錄所有OBJ
                NXOpen.Drawings.DrawingSheet CurrentSheet = (NXOpen.Drawings.DrawingSheet)NXObjectManager.Get(SheetTagAry[i]);
                if (CurrentSheet.Name == "S1")
                {
                    FirstSheet = CurrentSheet;
                }
                listDrawingSheet.Add(CurrentSheet);
                CurrentSheet.Open();
                CurrentSheet.View.UpdateDisplay();
                DisplayableObject[] SheetObj = CurrentSheet.View.AskVisibleObjects();
                //status = Function.RecordDimension(SheetObj, sWorkPartAttribute, ref Database.listDimensionData);
                status = CaxME.RecordDimension(SheetObj, sWorkPartAttribute, ref listDimensionData);
                if (!status)
                {
                    this.Close();
                    return;
                }
            }
            #endregion

            #region 輸出OIS
            //輸出PDF
            if (ExportPFD.Checked == true)
            {
                //建立PFD資料夾
                string PFDFullPath = string.Format(@"{0}\{1}", cMETE_Download_Upload_Path.Local_Folder_OIS, sPartInfo.PartNo + "_OIS" + sPartInfo.OpNum + ".pdf");
                CaxME.CreateOISPDF(listDrawingSheet, PFDFullPath);
            }
            #endregion

            if (ExportPFD.Checked == true)
            {
                //OIS資料夾上傳
                status = CaxPublic.DirectoryCopy(cMETE_Download_Upload_Path.Local_Folder_OIS, cMETE_Download_Upload_Path.Server_Folder_OIS, true);
                if (!status)
                {
                    MessageBox.Show("OIS資料夾複製失敗，請聯繫開發工程師");
                    this.Close();
                }
            }
            

            //切回首頁
            if (FirstSheet != null)
            {
                FirstSheet.Open();
            }
            

            Com_PEMain comPEMain = new Com_PEMain();
            #region 由料號查peSrNo  
            try
            {
                comPEMain = session.QueryOver<Com_PEMain>().Where(x => x.partName == sPartInfo.PartNo)
                                                            .Where(x => x.customerVer == sPartInfo.CusRev)
                                                            .Where(x => x.opVer == sPartInfo.OpRev)
                                                            .SingleOrDefault<Com_PEMain>();
            }
            catch (System.Exception ex)
            {
                MessageBox.Show("資料庫中沒有此料號的紀錄，故無法上傳量測尺寸，僅成功上傳實體檔案");
                return;
            }
            #endregion

            Com_PartOperation comPartOperation = new Com_PartOperation();
            #region 由peSrNo和Op查partOperationSrNo
            try
            {
                comPartOperation = session.QueryOver<Com_PartOperation>()
                                            .Where(x => x.comPEMain.peSrNo == comPEMain.peSrNo)
                                            .Where(x => x.operation1 == sPartInfo.OpNum)
                                            .SingleOrDefault<Com_PartOperation>();
            }
            catch (System.Exception ex)
            {
                MessageBox.Show("資料庫中沒有此料號的紀錄，故無法上傳量測尺寸，僅成功上傳實體檔案");
                return;
            }
            #endregion

            
            #region (註解)由excelType查meExcelSrNo
            //Sys_MEExcel sysMEExcel = new Sys_MEExcel();
            //try
            //{
            //    sysMEExcel = session.QueryOver<Sys_MEExcel>().Where(x => x.meExcelType == meExcelType).SingleOrDefault<Sys_MEExcel>();
            //}
            //catch (System.Exception ex)
            //{
            //    MessageBox.Show("資料庫中沒有此料號的紀錄，故無法上傳量測尺寸，僅成功上傳實體檔案");
            //    return;
            //}
            #endregion

            #region 比對資料庫MEMain是否有同筆數據
            IList<Com_MEMain> DBData_ComMEMain = new List<Com_MEMain>();
            DBData_ComMEMain = session.QueryOver<Com_MEMain>().List<Com_MEMain>();

            bool Is_Exist = false;
            Com_MEMain currentComMEMain = new Com_MEMain();
            foreach (Com_MEMain i in DBData_ComMEMain)
            {
                if (i.comPartOperation == comPartOperation)
                {
                    Is_Exist = true;
                    currentComMEMain = i;
                    break;
                }
            }
            #endregion

            #region 如果本次上傳的資料不存在於資料庫，則開始上傳資料；如果已存在資料庫，則詢問是否要更新尺寸
            bool Is_Update = true;
            if (Is_Exist)
            {
                if (eTaskDialogResult.Yes == CaxPublic.ShowMsgYesNo("此料號已存在上一次的標註尺寸資料，是否更新?"))
                {
                    #region 刪除Com_Dimension資料表
                    IList<Com_Dimension> DB_ComDimension = session.QueryOver<Com_Dimension>()
                                                                  .Where(x => x.comMEMain == currentComMEMain).List<Com_Dimension>();
                    using (ITransaction trans = session.BeginTransaction())
                    {
                        foreach (Com_Dimension i in DB_ComDimension)
                        {
                            session.Delete(i);
                        }
                        trans.Commit();
                    }
                    #endregion

                    #region 刪除Com_MEMain資料表
                    Com_MEMain DB_ComMEMain = session.QueryOver<Com_MEMain>()
                                                     .Where(x => x.comPartOperation == currentComMEMain.comPartOperation).SingleOrDefault<Com_MEMain>();
                    using (ITransaction trans = session.BeginTransaction())
                    {
                        session.Delete(DB_ComMEMain);
                        trans.Commit();
                    }
                    #endregion
                }
                else
                {
                    Is_Update = false;
                }
            }
            if (Is_Update)
            {
                #region 整理資料並上傳
                try
                {
                    Com_MEMain cCom_MEMain = new Com_MEMain();
                    cCom_MEMain.comPartOperation = comPartOperation;
                    //cCom_MEMain.sysMEExcel = sysMEExcel;
                    cCom_MEMain.partDescription = sWorkPartAttribute.partDescription;
                    cCom_MEMain.createDate = sWorkPartAttribute.createDate;
                    cCom_MEMain.material = sWorkPartAttribute.material;
                    cCom_MEMain.draftingVer = sWorkPartAttribute.draftingVer;

                    IList<Com_Dimension> listCom_Dimension = new List<Com_Dimension>();
                    foreach (CaxME.DimensionData i in listDimensionData)
                    {
                        Com_Dimension cCom_Dimension = new Com_Dimension();
                        cCom_Dimension.comMEMain = cCom_MEMain;
                        //Database.MappingData(i, ref cCom_Dimension);
                        CaxME.MappingData(i, ref cCom_Dimension);
                        listCom_Dimension.Add(cCom_Dimension);
                    }
                    cCom_MEMain.comDimension = listCom_Dimension;

                    using (ITransaction trans = session.BeginTransaction())
                    {
                        session.Save(cCom_MEMain);
                        trans.Commit();
                    }
                }
                catch (System.Exception ex)
                {
                    MessageBox.Show("上傳資料庫時發生錯誤，僅上傳實體檔案");
                }
                #endregion
            }

            #endregion

            

            #endregion

            CaxPart.Save();
            MessageBox.Show("上傳完成！");
            this.Close();
        }
        
        

        private void Cancel_Click(object sender, EventArgs e)
        {
            this.Close();
        }

    }
}
