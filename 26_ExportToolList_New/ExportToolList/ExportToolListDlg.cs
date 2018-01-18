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
using NXOpen.CAM;
using DevComponents.DotNetBar.SuperGrid;
using System.Text.RegularExpressions;
using Microsoft.Office.Interop.Excel;
using System.Diagnostics;
using System.Data.OleDb;
using NHibernate;

namespace ExportToolList
{
    public partial class ExportToolListDlg : DevComponents.DotNetBar.Office2007Form
    {
        public static Session theSession = Session.GetSession();
        public static UI theUI;
        public static UFSession theUfSession = UFSession.GetUFSession();
        public static bool status;
        public static string ToolListPath = "", Is_Local = "";
        public static METE_Download_Upload_Path cMETE_Download_Upload_Path = new METE_Download_Upload_Path();
        public static Part workPart = theSession.Parts.Work;
        public static Part displayPart = theSession.Parts.Display;
        public static PartInfo sPartInfo = new PartInfo();
        public static Dictionary<string, List<OperData>> DicNCData = new Dictionary<string, List<OperData>>();
        public static GridPanel panel = new GridPanel();
        public static string CurrentNCName = "", PartNo = "", ToolListFolder = "", OutputPath = "";
        public ApplicationClass excelApp = null;
        public Workbook book = null;
        public Worksheet sheet = null;
        public Range oRng = null;

        public struct OperData
        {
            public string OperName { get; set; }
            public string ToolName { get; set; }
            public string HolderDescription { get; set; }
            //public string CuttingLength { get; set; }
            //public string CuttingTime { get; set; }
            //public string ToolFeed { get; set; }
            public string ToolNumber { get; set; }
            public string ToolERP { get; set; }
            public string ShankERP { get; set; }
            public string ExtensionShankERP { get; set; }
            public string HolderERP { get; set; }

            public string TOOL_NO { get; set; }
            public string ERP_NO { get; set; }
            public string CUTTER_QTY { get; set; }
            public string CUTTER_LIFE { get; set; }
            public string FLUTE_QTY { get; set; }
            public string TITLE { get; set; }
            public string SPECIFICATION { get; set; }
            public string NOTE { get; set; }
            public string ACCESSORY { get; set; }
            //public string ToolSpeed { get; set; }
            //public string PartStock { get; set; }
            //public string PartFloorStock { get; set; }
        }

        public class ToolListMember
        {
            public static string TOOL_NO = "GTTL_TOOL_NO";
            public static string ERP_NO = "GTTL_ERP_NO";
            public static string CUTTER_QTY = "GTTL_CUTTER_QTY";
            public static string CUTTER_LIFE = "GTTL_CUTTER_LIFE";
            public static string FLUTE_QTY = "GTTL_FLUTE_QTY";
            public static string TITLE = "GTTL_TITLE";
            public static string SPECIFICATION = "GTTL_SPECIFICATION";
            public static string NOTE = "GTTL_NOTE";
            public static string ACCESSORY = "GTTL_ACCESSORY";
        }

        public ExportToolListDlg()
        {
            InitializeComponent();
            ois.Visible = false;
            productNo.Visible = false;
            //建立panel物件
            panel = SGC.PrimaryGrid;
        }


        private static bool IsNC(NXOpen.CAM.NCGroup ncGroup)
        {
            try
            {
                int type;
                int subtype;
                theUfSession.Obj.AskTypeAndSubtype(ncGroup.Tag, out type, out subtype);

                //比對是否為Program群組
                if (type != UFConstants.UF_machining_task_type)
                {
                    return false;
                }
                //過濾PROGRAM
                if (ncGroup.Name == "PROGRAM")
                {
                    return false;
                }
            }
            catch (System.Exception ex)
            {
                return false;
            }
            return true;

        }
        private static void CheckRepeat(NXOpen.CAM.Operation item, ref OperData sOperData)
        {
            try
            {
                string toolNumber = "T" + CaxOper.AskOperToolNumber(item);
                //取得現有的刀號
                string[] toolNumbers = sOperData.ToolNumber.Split(',');

                string toolName = CaxOper.AskOperToolNameFromTag(item.Tag);
                string holder = CaxOper.AskOperHolderDescription(item);
                //string toolERP = CaxOper.AskOperToolERPNumFromAttribute(item.Tag, "ToolERP");
                //string shankERP = CaxOper.AskOperToolERPNumFromAttribute(item.Tag, "ShankERP");
                //string extensionShankERP = CaxOper.AskOperToolERPNumFromAttribute(item.Tag, "ExtensionShankERP");
                //string holderERP = CaxOper.AskOperToolERPNumFromAttribute(item.Tag, "HolderERP");
                string specification = "";
                if (CaxOper.AskOperToolERPNumFromAttribute(item.Tag, ToolListMember.SPECIFICATION) != "")
                {
                    specification = CaxOper.AskOperToolERPNumFromAttribute(item.Tag, ToolListMember.SPECIFICATION);
                }
                else
                {
                    specification = CaxOper.AskOperToolNameFromTag(item.Tag);
                }

                string tool_NO = CaxOper.AskOperToolERPNumFromAttribute(item.Tag, ToolListMember.TOOL_NO);
                string erp_NO = CaxOper.AskOperToolERPNumFromAttribute(item.Tag, ToolListMember.ERP_NO);
                string cutter_qty = CaxOper.AskOperToolERPNumFromAttribute(item.Tag, ToolListMember.CUTTER_QTY);
                string cutter_life = CaxOper.AskOperToolERPNumFromAttribute(item.Tag, ToolListMember.CUTTER_LIFE);
                string flute_qty = CaxOper.AskOperToolERPNumFromAttribute(item.Tag, ToolListMember.FLUTE_QTY);
                string title = CaxOper.AskOperToolERPNumFromAttribute(item.Tag, ToolListMember.TITLE);
                string note = CaxOper.AskOperToolERPNumFromAttribute(item.Tag, ToolListMember.NOTE);
                //string specification = CaxOper.AskOperToolERPNumFromAttribute(item.Tag, ToolListMember.SPECIFICATION);


                #region toolNumber檢查
                bool toolNumberChk = false;
                foreach (string i in toolNumbers)
                {
                    if (toolNumber == i)
                    {
                        toolNumberChk = true;
                        break;
                    }
                }
                if (toolNumberChk)
                {
                    sOperData.ToolNumber = sOperData.ToolNumber + "," + toolNumber;
                    sOperData.ToolName = sOperData.ToolName + "," + "";
                    sOperData.HolderDescription = sOperData.HolderDescription + "," + "";
                    //sOperData.ToolERP = sOperData.ToolERP + "," + "";
                    //sOperData.ShankERP = sOperData.ShankERP + "," + "";
                    //sOperData.ExtensionShankERP = sOperData.ExtensionShankERP + "," + "";
                    //sOperData.HolderERP = sOperData.HolderERP + "," + "";
                    //attribute
                    sOperData.TOOL_NO = sOperData.TOOL_NO + "," + "";
                    sOperData.ERP_NO = sOperData.ERP_NO + "," + "";
                    sOperData.CUTTER_QTY = sOperData.CUTTER_QTY + "," + "";
                    sOperData.CUTTER_LIFE = sOperData.CUTTER_LIFE + "," + "";
                    sOperData.FLUTE_QTY = sOperData.FLUTE_QTY + "," + "";
                    sOperData.TITLE = sOperData.TITLE + "," + "";
                    sOperData.SPECIFICATION = sOperData.SPECIFICATION + "," + "";
                    sOperData.NOTE = sOperData.NOTE + "," + "";
                }
                else
                {
                    sOperData.ToolNumber = sOperData.ToolNumber + "," + toolNumber;
                    sOperData.ToolName = sOperData.ToolName + "," + toolName;
                    sOperData.HolderDescription = sOperData.HolderDescription + "," + holder;
                    //sOperData.ToolERP = sOperData.ToolERP + "," + toolERP;
                    //sOperData.ShankERP = sOperData.ShankERP + "," + shankERP;
                    //sOperData.ExtensionShankERP = sOperData.ExtensionShankERP + "," + extensionShankERP;
                    //sOperData.HolderERP = sOperData.HolderERP + "," + holderERP;
                    //attribute
                    sOperData.TOOL_NO = sOperData.TOOL_NO + "," + tool_NO;
                    sOperData.ERP_NO = sOperData.ERP_NO + "," + erp_NO;
                    sOperData.CUTTER_QTY = sOperData.CUTTER_QTY + "," + cutter_qty;
                    sOperData.CUTTER_LIFE = sOperData.CUTTER_LIFE + "," + cutter_life;
                    sOperData.FLUTE_QTY = sOperData.FLUTE_QTY + "," + flute_qty;
                    sOperData.TITLE = sOperData.TITLE + "," + title;
                    sOperData.SPECIFICATION = sOperData.SPECIFICATION + "," + specification;
                    sOperData.NOTE = sOperData.NOTE + "," + note;
                }
                #endregion
                //string toolName = CaxOper.AskOperToolNameFromTag(item.Tag);
                //string holder = CaxOper.AskOperHolderDescription(item);
                //string[] toolNames = sOperData.ToolName.Split(',');
                //string[] holders = sOperData.HolderDescription.Split(',');

                #region (註解)toolName檢查
                /*
                bool toolNameChk = false;
                foreach (string i in toolNames)
                {
                    if (toolName == i)
                    {
                        toolNameChk = true;
                    }
                }
                if (toolNameChk)
                {
                    sOperData.ToolName = sOperData.ToolName + "," + "";
                }
                else
                {
                    sOperData.ToolName = sOperData.ToolName + "," + toolName;
                }
                */
                #endregion

                #region (註解)holder檢查
                /*
                bool holderChk = false;
                foreach (string i in holders)
                {
                    if (holder == i)
                    {
                        holderChk = true;
                    }
                }
                if (holderChk)
                {
                    sOperData.HolderDescription = sOperData.HolderDescription + "," + "";
                }
                else
                {
                    sOperData.HolderDescription = sOperData.HolderDescription + "," + holder;
                }
                */
                #endregion
            }
            catch (System.Exception ex)
            {
                MessageBox.Show("判斷是否重複的刀具與刀柄失敗");
            }
        }
        public bool Doit()
        {
            try
            {
                int module_id;
                theUfSession.UF.AskApplicationModule(out module_id);
                if (module_id != UFConstants.UF_APP_CAM)
                {
                    MessageBox.Show("請先轉換為加工模組後再執行！");
                    return false;
                }

                if (!GetToolListPath(out ToolListPath))
                {
                    MessageBox.Show("取得ToolList.xls失敗");
                    return false;
                }



                //取得正確路徑，拆零件路徑字串取得客戶名稱、料號、版本
                status = CaxPublic.GetAllPath("TE", displayPart.FullPath, out sPartInfo, ref cMETE_Download_Upload_Path);
                if (!status)
                {
                    Is_Local = null;
                }

                //此條件判斷是否為走系統的零件
                if (!displayPart.FullPath.Contains("Task"))
                {
                    Is_Local = null;
                }

                if (Is_Local == null)
                {
                    PartNo = Path.GetFileNameWithoutExtension(displayPart.FullPath);
                }
                else
                {
                    PartNo = sPartInfo.PartNo;
                }

                //取得所有GroupAry，用來判斷Group的Type決定是NC、Tool、Geometry
                NXOpen.CAM.NCGroup[] NCGroupAry = displayPart.CAMSetup.CAMGroupCollection.ToArray();
                //取得所有OperationAry
                NXOpen.CAM.Operation[] OperationAry = displayPart.CAMSetup.CAMOperationCollection.ToArray();

                #region 取得相關資訊，填入DIC
                DicNCData = new Dictionary<string, List<OperData>>();
                foreach (NXOpen.CAM.NCGroup ncGroup in NCGroupAry)
                {
                    if (!IsNC(ncGroup))
                    {
                        continue;
                    }

                    if (!ncGroup.Name.Contains("OP"))
                    {
                        MessageBox.Show("請先手動將Group名稱：" + ncGroup.Name + "，改為正確格式，再重新啟動功能！");
                        return false;
                    }

                    //取得此NCGroup下的所有Oper
                    CAMObject[] OperGroup = ncGroup.GetMembers();

                    foreach (NXOpen.CAM.Operation item in OperGroup)
                    {
                        bool cheValue;
                        List<OperData> listOperData = new List<OperData>();
                        cheValue = DicNCData.TryGetValue(ncGroup.Name, out listOperData);
                        if (!cheValue)
                        {
                            listOperData = new List<OperData>();
                        }
                        bool chk = true;
                        foreach (OperData i in listOperData)
                        {
                            if (i.ToolNumber == "T" + CaxOper.AskOperToolNumber(item))
                            {
                                chk = false;
                                break;
                            }
                        }
                        if (!chk)
                        {
                            continue;
                        }
                        OperData sOperData = new OperData();
                        GetOperToolAttr(item, ref sOperData);
                        listOperData.Add(sOperData);
                        DicNCData[ncGroup.Name] = listOperData;
                    }
                }

                //將DicNCData的key存入程式群組下拉選單中
                foreach (KeyValuePair<string, List<OperData>> kvp in DicNCData)
                {
                    comboBoxNCName.Items.Add(kvp.Key);
                }
                if (comboBoxNCName.Items.Count == 1)
                {
                    comboBoxNCName.Text = comboBoxNCName.Items[0].ToString();
                }
                #endregion
            }
            catch (System.Exception ex)
            {
                return false;
            }
            return true;
        }
        private static bool GetOperToolAttr(NXOpen.CAM.Operation item, ref OperData sOperData)
        {
            try
            {
                sOperData.OperName = item.Name;
                sOperData.ToolNumber = "T" + CaxOper.AskOperToolNumber(item);
                sOperData.ToolName = CaxOper.AskOperToolNameFromTag(item.Tag);
                sOperData.HolderDescription = CaxOper.AskOperHolderDescription(item);

                sOperData.TOOL_NO = "T" + CaxOper.AskOperToolERPNumFromAttribute(item.Tag, ToolListMember.TOOL_NO);
                sOperData.ERP_NO = CaxOper.AskOperToolERPNumFromAttribute(item.Tag, ToolListMember.ERP_NO);
                sOperData.CUTTER_QTY = CaxOper.AskOperToolERPNumFromAttribute(item.Tag, ToolListMember.CUTTER_QTY);
                sOperData.CUTTER_LIFE = CaxOper.AskOperToolERPNumFromAttribute(item.Tag, ToolListMember.CUTTER_LIFE);
                sOperData.FLUTE_QTY = CaxOper.AskOperToolERPNumFromAttribute(item.Tag, ToolListMember.FLUTE_QTY);
                sOperData.TITLE = CaxOper.AskOperToolERPNumFromAttribute(item.Tag, ToolListMember.TITLE);
                if (CaxOper.AskOperToolERPNumFromAttribute(item.Tag, ToolListMember.SPECIFICATION) != "")
                {
                    sOperData.SPECIFICATION = CaxOper.AskOperToolERPNumFromAttribute(item.Tag, ToolListMember.SPECIFICATION);
                }
                else
                {
                    sOperData.SPECIFICATION = CaxOper.AskOperToolNameFromTag(item.Tag);
                }
                sOperData.NOTE = CaxOper.AskOperToolERPNumFromAttribute(item.Tag, ToolListMember.NOTE);
                sOperData.ACCESSORY = CaxOper.AskOperToolERPNumFromAttribute(item.Tag, ToolListMember.ACCESSORY);
            }
            catch (System.Exception ex)
            {
                return false;
            }
            return true;
        }
        private static bool GetToolListPath(out string ToolListPath)
        {
            ToolListPath = "";
            try
            {
                cMETE_Download_Upload_Path = new METE_Download_Upload_Path();
                Is_Local = Environment.GetEnvironmentVariable("UGII_ENV_FILE");
                if (Is_Local != null)
                {
                    //取得Server的ToolListPath.xls路徑
                    ToolListPath = string.Format(@"{0}\{1}\{2}\{3}", CaxEnv.GetGlobaltekEnvDir(), "TE_Config", "Config", "ToolList.xls");

                    //取得METEDownload_Upload.dat
                    status = CaxGetDatData.GetMETEDownload_Upload(out cMETE_Download_Upload_Path);
                    if (!status)
                    {
                        MessageBox.Show("取得METEDownload_Upload.dat失敗");
                        return false;
                    }
                }
                else
                {
                    ToolListPath = string.Format(@"{0}\{1}", "D:", "ToolList.xls");
                }
            }
            catch (System.Exception ex)
            {
                return false;
            }
            return true;
            
        }
        public void ExportToolListDlg_Load(object sender, EventArgs e)
        {
            if (!Doit())
            {
                MessageBox.Show("取得資料失敗，請聯繫開發工程師");
                this.Close();
                return;
            }
        }
        private void comboBoxNCName_SelectedIndexChanged(object sender, EventArgs e)
        {
            //清空superGrid資料
            panel.Rows.Clear();
            //取得comboBox資料
            CurrentNCName = comboBoxNCName.Text;

            #region 建立ToolListFolder資料夾
            ToolListFolder = string.Format(@"{0}\{1}_ToolList", Path.GetDirectoryName(displayPart.FullPath), CurrentNCName);
            if (!Directory.Exists(ToolListFolder))
            {
                System.IO.Directory.CreateDirectory(ToolListFolder);
            }
            #endregion

            //變更路徑
            string[] FolderFile = System.IO.Directory.GetFileSystemEntries(ToolListFolder, "*.xls");
            OutputPath = string.Format(@"{0}\{1}", ToolListFolder
                                                      , PartNo + "_" + CurrentNCName + "_" + (FolderFile.Length + 1) + ".xls");

            //拆群組名稱字串取得製程序(EX：OP210=>210)
            //string[] splitCurrentNCName = CurrentNCName.Split('_');
            //string OperNum = Regex.Replace(splitCurrentNCName[0], "[^0-9]", "");

            #region 填值到SuperGridPanel

            GridRow row = new GridRow();
            foreach (KeyValuePair<string, List<OperData>> kvp in DicNCData)
            {
                if (CurrentNCName != kvp.Key)
                {
                    continue;
                }

                foreach (OperData sOperData in kvp.Value)
                {
                    row = new GridRow(sOperData.TOOL_NO, sOperData.ERP_NO, sOperData.CUTTER_QTY, sOperData.CUTTER_LIFE,
                        sOperData.FLUTE_QTY, sOperData.TITLE, sOperData.SPECIFICATION, sOperData.NOTE);
                    panel.Rows.Add(row);
                    if (sOperData.ACCESSORY != "")
                    {
                        //  xxx!QQ!TC10D20FL5R2SD10L40 ? zxb!zxv!zxc ? 34!23!12
                        string[] SplitAccessory = sOperData.ACCESSORY.Split('?');
                        foreach (string i in SplitAccessory)
                        {
                            string[] Spliti = i.Split('!');
                            row = new GridRow("", Spliti[0], "", "",
                                        "", Spliti[1], Spliti[2], "");
                            panel.Rows.Add(row);
                        }
                    }
                }




                //string[] splitOperName = kvp.Value.OperName.Split(',');
                //string[] splitOperToolNumber = kvp.Value.ToolNumber.Split(',');
                //string[] splitToolName = kvp.Value.ToolName.Split(',');
                //string[] splitHolderDescription = kvp.Value.HolderDescription.Split(',');
                //string[] splitToolERP = kvp.Value.ToolERP.Split(',');
                //string[] splitShankERP = kvp.Value.ShankERP.Split(',');
                //string[] splitExtensionShankERP = kvp.Value.ExtensionShankERP.Split(',');
                //string[] splitHolderERP = kvp.Value.HolderERP.Split(',');
                //string[] splitToolNo = kvp.Value.TOOL_NO.Split(',');
                //string[] splitERPNo = kvp.Value.ERP_NO.Split(',');
                //string[] splitCutterQty = kvp.Value.CUTTER_QTY.Split(',');
                //string[] splitCutterLife = kvp.Value.CUTTER_LIFE.Split(',');
                //string[] splitFluteQty = kvp.Value.FLUTE_QTY.Split(',');
                //string[] splitTitle = kvp.Value.TITLE.Split(',');
                //string[] splitSpecification = kvp.Value.SPECIFICATION.Split(',');
                //string[] splitNote = kvp.Value.NOTE.Split(',');
                //string[] splitOperCuttingLength = kvp.Value.CuttingLength.Split(',');
                //string[] splitOperToolFeed = kvp.Value.ToolFeed.Split(',');
                //string[] splitOperCuttingTime = kvp.Value.CuttingTime.Split(',');
                //string[] splitOperToolSpeed = kvp.Value.ToolSpeed.Split(',');

                //for (int i = 0; i < splitOperName.Length; i++)
                //{
                //    //row = new GridRow(splitOperName[i], splitOperToolNumber[i], splitToolName[i], splitHolderDescription[i], splitToolERP[i]
                //    //                    , splitShankERP[i], splitExtensionShankERP[i], splitHolderERP[i]);
                //    row = new GridRow(splitOperName[i], splitOperToolNumber[i], splitSpecification[i], splitERPNo[i], splitCutterQty[i]
                //                        , splitCutterLife[i], splitFluteQty[i], splitTitle[i], splitNote[i]);
                //    panel.Rows.Add(row);
                //}
            }

            #endregion
        }
        private void OK_Click(object sender, EventArgs e)
        {
            try
            {
                #region (註解中)檢查PC有無Excel在執行
                //bool flag = false;
                //foreach (var item in Process.GetProcesses())
                //{
                //    if (item.ProcessName == "EXCEL")
                //    {
                //        flag = true;
                //        break;
                //    }
                //}
                //if (flag)
                //{
                //    MessageBox.Show("請先關閉所有Excel再重新執行輸出，如沒有EXCEL在執行，請開啟工作管理員關閉背景EXCEL");
                //    return;
                //}
                #endregion

                bool flag = false;
                Dictionary<string, List<Sys_TipTop>> DicToolERP = new Dictionary<string, List<Sys_TipTop>>();
                if (productNo.Text != "")
                {
                    if (!File.Exists("D:\\Knife.xls"))
                    {
                        MessageBox.Show("TipTop的文件不存在，無法比對資料，僅由UG資料輸出");
                        flag = true;
                        goto ExportFromUG;
                    }
                    string strCon = "Provider=Microsoft.Jet.OLEDB.4.0;" + "Data Source=D:\\Knife.xls;" + "Extended Properties='Excel 8.0;" + "HDR=YES;" + "IMEX=1'";
                    
                    OleDbConnection GetXLS = new OleDbConnection(strCon);
                    GetXLS.Open();
                    System.Data.DataTable Table = GetXLS.GetOleDbSchemaTable(OleDbSchemaGuid.Tables, null);

                    //查詢此Excel所有的工作表名稱
                    
                    //List<Sys_TipTop> ListSheetA = new List<Sys_TipTop>();
                    string SelectSheetName = "";
                    foreach (DataRow row in Table.Rows)
                    {
                        //抓取Xls各個Sheet的名稱(+'$')-有的名稱需要加名稱''，有的不用
                        SelectSheetName = (string)row["TABLE_NAME"];

                        //工作表名稱有特殊字元、空格，需加'工作表名稱$'，ex：'Sheet_A$'
                        //工作表名稱沒有特殊字元、空格，需加工作表名稱$，ex：SheetA$
                        //所有工作表名稱為Sheet1，讀取此工作表的內容
                        if (SelectSheetName == "工作表1$")
                        {
                            //select 工作表名稱
                            OleDbCommand cmSheetA = new OleDbCommand(" SELECT * FROM [工作表1$] ", GetXLS);
                            OleDbDataReader drSheetA = cmSheetA.ExecuteReader();

                            //讀取工作表SheetA資料
                            //List<string> ListSheetA = new List<string>();

                            while (drSheetA.Read())
                            {
                                if (drSheetA[0].ToString() == "")
                                {
                                    break;
                                }
                                List<Sys_TipTop> ListSheetA = new List<Sys_TipTop>();
                                status = DicToolERP.TryGetValue(drSheetA[0].ToString(), out ListSheetA);
                                if (!status)
                                {
                                    ListSheetA = new List<Sys_TipTop>();
                                    Sys_TipTop sSys_TipTop = new Sys_TipTop();
                                    sSys_TipTop.productNo = drSheetA[0].ToString();
                                    sSys_TipTop.stepNo = drSheetA[1].ToString();
                                    sSys_TipTop.partNo = drSheetA[2].ToString();
                                    sSys_TipTop.ois = drSheetA[3].ToString();
                                    sSys_TipTop.erpNo = drSheetA[4].ToString();
                                    sSys_TipTop.toolNo = drSheetA[5].ToString();
                                    sSys_TipTop.usedCount = drSheetA[6].ToString();
                                    sSys_TipTop.toolLife = drSheetA[7].ToString();
                                    sSys_TipTop.toolChangeTime = drSheetA[8].ToString();
                                    sSys_TipTop.toolSpec = drSheetA[9].ToString();
                                    ListSheetA.Add(sSys_TipTop);
                                    DicToolERP.Add(drSheetA[0].ToString(), ListSheetA);
                                }
                                else
                                {
                                    Sys_TipTop sSys_TipTop = new Sys_TipTop();
                                    sSys_TipTop.productNo = drSheetA[0].ToString();
                                    sSys_TipTop.stepNo = drSheetA[1].ToString();
                                    sSys_TipTop.partNo = drSheetA[2].ToString();
                                    sSys_TipTop.ois = drSheetA[3].ToString();
                                    sSys_TipTop.erpNo = drSheetA[4].ToString();
                                    sSys_TipTop.toolNo = drSheetA[5].ToString();
                                    sSys_TipTop.usedCount = drSheetA[6].ToString();
                                    sSys_TipTop.toolLife = drSheetA[7].ToString();
                                    sSys_TipTop.toolChangeTime = drSheetA[8].ToString();
                                    sSys_TipTop.toolSpec = drSheetA[9].ToString();
                                    ListSheetA.Add(sSys_TipTop);
                                    DicToolERP[drSheetA[0].ToString()] = ListSheetA;
                                }
                            }

                            /*步驟4：關閉檔案*/

                            //結束關閉讀檔(必要，不關會有error)
                            drSheetA.Close();
                            GetXLS.Close();
                        }
                    }
                }
                else
                {
                    flag = true;
                }
            ExportFromUG:

                excelApp = new ApplicationClass();
                book = null;
                sheet = null;
                oRng = null;

                excelApp.Visible = false;
                book = excelApp.Workbooks.Open(ToolListPath);
                sheet = (Worksheet)book.Sheets[1];
                oRng = (Range)sheet.Cells;
                oRng[52, 10] = PartNo;

            
                //Insert所需欄位並填入資料
                //新版CurrentRow從7開始、ToolNumberColumn從1開始、ToolNameColumn從8開始
                //舊版CurrentRow從8開始、ToolNumberColumn從2開始、ToolNameColumn從3開始
                int
                    CurrentRow = 6,
                    ToolNumberColumn = 1,
                    ToolERPColumn = 2,
                    ToolCutterQtyColumn = 3,
                    ToolCutterLifeColumn = 4,
                    ToolFluteQtyColumn = 5,
                    ToolTitleColumn = 6,
                    ToolSpecColumn = 8,
                    ToolNoteColumn = 10;
                   
                //將相同刀號記錄起來，只輸出一筆資料
                Dictionary<string, OperData> DicToolData = new Dictionary<string, OperData>();
                //for (int i = 0; i < panel.Rows.Count; i++)
                //{
                //    OperData toolData = new OperData();
                //    status = DicToolData.TryGetValue(ReplaceToolNumber(panel.GetCell(i, 0).Value.ToString()), out toolData);
                //    if (!status)
                //    {
                //        toolData.ERP_NO = panel.GetCell(i, 1).Value.ToString();
                //        toolData.CUTTER_QTY = panel.GetCell(i, 2).Value.ToString();
                //        toolData.CUTTER_LIFE = panel.GetCell(i, 3).Value.ToString();
                //        toolData.FLUTE_QTY = panel.GetCell(i, 4).Value.ToString();
                //        toolData.TITLE = panel.GetCell(i, 5).Value.ToString();
                //        toolData.SPECIFICATION = panel.GetCell(i, 6).Value.ToString();
                //        toolData.NOTE = panel.GetCell(i, 7).Value.ToString();
                //        DicToolData.Add(ReplaceToolNumber(panel.GetCell(i, 0).Value.ToString()), toolData);
                //    }
                //}

                if (flag)
                {
                    int StartRow = 0, EndRow = 0;
                    oRng = (Range)sheet.Cells;
                    foreach (KeyValuePair<string, List<OperData>> kvp in DicNCData)
                    {
                        if (CurrentNCName != kvp.Key)
                        {
                            continue;
                        }

                        foreach (OperData i in kvp.Value)
                        {
                            CurrentRow = CurrentRow + 1;
                            StartRow = CurrentRow;
                            oRng[CurrentRow, ToolNumberColumn] = i.ToolNumber;//暫時不寫TOOL_NO，原因是有些刀子不是用系統掉進來會沒有屬性
                            oRng[CurrentRow, ToolERPColumn] = i.ERP_NO;
                            oRng[CurrentRow, ToolCutterQtyColumn] = i.CUTTER_QTY;
                            oRng[CurrentRow, ToolCutterLifeColumn] = i.CUTTER_LIFE;
                            oRng[CurrentRow, ToolFluteQtyColumn] = i.FLUTE_QTY;
                            oRng[CurrentRow, ToolTitleColumn] = i.TITLE;
                            oRng[CurrentRow, ToolSpecColumn] = i.SPECIFICATION;
                            oRng[CurrentRow, ToolNoteColumn] = i.NOTE;
                            if (i.ACCESSORY != "")
                            {
                                //  xxx!QQ!TC10D20FL5R2SD10L40 ? zxb!zxv!zxc ? 34!23!12
                                string[] SplitAccessory = i.ACCESSORY.Split('?');
                                foreach (string j in SplitAccessory)
                                {
                                    CurrentRow = CurrentRow + 1;
                                    string[] Spliti = j.Split('!');
                                    oRng[CurrentRow, ToolERPColumn] = Spliti[0];
                                    oRng[CurrentRow, ToolTitleColumn] = Spliti[1];
                                    oRng[CurrentRow, ToolSpecColumn] = Spliti[2];
                                }
                            }
                            EndRow = CurrentRow;
                            //合併儲存格
                            sheet.get_Range("A" + StartRow, "A" + EndRow).Merge(false);
                        }
                        
                    }
                    //foreach (KeyValuePair<string, OperData> kvp in DicToolData)
                    //{
                    //    CurrentRow = CurrentRow + 1;
                    //    oRng[CurrentRow, ToolNumberColumn] = kvp.Key;
                    //    oRng[CurrentRow, ToolERPColumn] = kvp.Value.ERP_NO;
                    //    oRng[CurrentRow, ToolCutterQtyColumn] = kvp.Value.CUTTER_QTY;
                    //    oRng[CurrentRow, ToolCutterLifeColumn] = kvp.Value.CUTTER_LIFE;
                    //    oRng[CurrentRow, ToolFluteQtyColumn] = kvp.Value.FLUTE_QTY;
                    //    oRng[CurrentRow, ToolTitleColumn] = kvp.Value.TITLE;
                    //    oRng[CurrentRow, ToolSpecColumn] = kvp.Value.SPECIFICATION;
                    //    oRng[CurrentRow, ToolNoteColumn] = kvp.Value.NOTE;
                    //}
                    oRng[51, 10] = "OIS-" + comboBoxNCName.Text.Split('P')[1].Split('_')[0];
                }
                else
                {
                    //判斷TipTopExcel與UG的資料整理出要輸出的資料
                    Dictionary<string, List<Sys_TipTop>> DicExportData = new Dictionary<string, List<Sys_TipTop>>();
                    foreach (KeyValuePair<string, List<Sys_TipTop>> i in DicToolERP)
                    {
                        if (i.Key != productNo.Text)
                        {
                            continue;
                        }

                        foreach (KeyValuePair<string, OperData> j in DicToolData)
                        {
                            foreach (Sys_TipTop k in i.Value)
                            {
                                if (k.ois != ois.Text)
                                {
                                    continue;
                                }

                                if (k.toolNo != j.Key)
                                {
                                    continue;
                                }

                                List<Sys_TipTop> temp = new List<Sys_TipTop>();
                                status = DicExportData.TryGetValue(j.Key, out temp);
                                if (!status)
                                {
                                    temp = new List<Sys_TipTop>();
                                    Sys_TipTop sSys_TipTop = new Sys_TipTop();
                                    sSys_TipTop.productNo = k.productNo;
                                    sSys_TipTop.stepNo = k.stepNo;
                                    sSys_TipTop.partNo = k.partNo;
                                    sSys_TipTop.ois = k.ois;
                                    sSys_TipTop.erpNo = k.erpNo;
                                    sSys_TipTop.toolNo = k.toolNo;
                                    sSys_TipTop.usedCount = k.usedCount;
                                    sSys_TipTop.toolLife = k.toolLife;
                                    sSys_TipTop.toolChangeTime = k.toolChangeTime;
                                    sSys_TipTop.toolSpec = k.toolSpec;
                                    sSys_TipTop.toolUGSpec = j.Value.SPECIFICATION;
                                    temp.Add(sSys_TipTop);
                                    DicExportData.Add(j.Key, temp);
                                }
                                else
                                {
                                    Sys_TipTop sSys_TipTop = new Sys_TipTop();
                                    sSys_TipTop.productNo = k.productNo;
                                    sSys_TipTop.stepNo = k.stepNo;
                                    sSys_TipTop.partNo = k.partNo;
                                    sSys_TipTop.ois = k.ois;
                                    sSys_TipTop.erpNo = k.erpNo;
                                    sSys_TipTop.toolNo = k.toolNo;
                                    sSys_TipTop.usedCount = k.usedCount;
                                    sSys_TipTop.toolLife = k.toolLife;
                                    sSys_TipTop.toolChangeTime = k.toolChangeTime;
                                    sSys_TipTop.toolSpec = k.toolSpec;
                                    sSys_TipTop.toolUGSpec = j.Value.SPECIFICATION;
                                    temp.Add(sSys_TipTop);
                                    DicExportData[j.Key] = temp;
                                }
                            }
                        }
                    }

                    oRng = (Range)sheet.Cells;
                    foreach (KeyValuePair<string, List<Sys_TipTop>> kvp in DicExportData)
                    {
                        foreach (Sys_TipTop i in kvp.Value)
                        {
                            CurrentRow = CurrentRow + 1;
                            oRng[CurrentRow, ToolNumberColumn] = i.toolNo;
                            oRng[CurrentRow, ToolERPColumn] = i.erpNo;
                            oRng[CurrentRow, ToolCutterQtyColumn] = "1";
                            oRng[CurrentRow, ToolCutterLifeColumn] = i.toolLife;
                            oRng[CurrentRow, ToolFluteQtyColumn] = i.usedCount;
                            oRng[CurrentRow, ToolTitleColumn] = i.toolSpec;
                            oRng[CurrentRow, ToolSpecColumn] = i.toolUGSpec;
                        }
                    }
                }

                if (Is_Local != null)
                {
                    ISession session = MyHibernateHelper.SessionFactory.OpenSession();
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
                        MessageBox.Show("1.資料庫中沒有此料號的紀錄，無法上傳刀具清單資料");
                        return;
                    }
                    #endregion

                    Com_PartOperation comPartOperation = new Com_PartOperation();
                    #region 由peSrNo和OpNum查partOperationSrNo
                    try
                    {
                        comPartOperation = session.QueryOver<Com_PartOperation>()
                                                             .Where(x => x.comPEMain.peSrNo == comPEMain.peSrNo)
                                                             .Where(x => x.operation1 == sPartInfo.OpNum)
                                                             .SingleOrDefault<Com_PartOperation>();
                    }
                    catch (System.Exception ex)
                    {
                        MessageBox.Show(ex.ToString());
                        MessageBox.Show("2.資料庫中沒有此料號的紀錄，無法上傳刀具清單資料");
                        return;
                    }
                    #endregion

                    #region 比對資料庫TEMain是否有同筆數據
                    IList<Com_TEMain> DBData_ComTEMain = session.QueryOver<Com_TEMain>().List<Com_TEMain>();

                    bool Is_Exist = false;
                    Com_TEMain currentComTEMain = new Com_TEMain();
                    foreach (Com_TEMain i in DBData_ComTEMain)
                    {
                        if (i.comPartOperation == comPartOperation && i.ncGroupName == CurrentNCName)
                        {
                            Is_Exist = true;
                            currentComTEMain = i;
                            break;
                        }
                    }
                    #endregion

                    if (Is_Exist)
                    {
                        #region 刪除Com_ToolList
                        IList<Com_ToolList> DB_ToolList = session.QueryOver<Com_ToolList>()
                                                 .Where(x => x.comTEMain.teSrNo == currentComTEMain.teSrNo).List<Com_ToolList>();
                        using (ITransaction trans = session.BeginTransaction())
                        {
                            foreach (Com_ToolList i in DB_ToolList)
                            {
                                session.Delete(i);
                            }
                            trans.Commit();
                        }
                        #endregion

                        foreach (KeyValuePair<string, List<OperData>> kvp in DicNCData)
                        {
                            if (CurrentNCName != kvp.Key)
                            {
                                continue;
                            }

                            foreach (OperData i in kvp.Value)
                            {
                                Com_ToolList comToolList = new Com_ToolList();
                                comToolList.comTEMain = currentComTEMain;
                                comToolList.toolNumber = i.TOOL_NO;
                                comToolList.erpNumber = i.ERP_NO;
                                comToolList.cutterQty = i.CUTTER_QTY;
                                comToolList.cutterLife = i.CUTTER_LIFE;
                                comToolList.fluteQty = i.FLUTE_QTY;
                                comToolList.title = i.TITLE;
                                comToolList.specification = i.SPECIFICATION;
                                comToolList.note = i.NOTE;
                                comToolList.accessory = i.ACCESSORY;
                                using (ITransaction trans = session.BeginTransaction())
                                {
                                    session.Save(comToolList);
                                    trans.Commit();
                                }
                            }
                        }

                        //foreach (KeyValuePair<string, OperData> kvp in DicToolData)
                        //{
                        //    Com_ToolList comToolList = new Com_ToolList();
                        //    comToolList.comTEMain = currentComTEMain;
                        //    comToolList.toolNumber = kvp.Key;
                        //    comToolList.erpNumber = kvp.Value.ERP_NO;
                        //    comToolList.cutterQty = kvp.Value.CUTTER_QTY;
                        //    comToolList.cutterLife = kvp.Value.CUTTER_LIFE;
                        //    comToolList.fluteQty = kvp.Value.FLUTE_QTY;
                        //    comToolList.title = kvp.Value.TITLE;
                        //    comToolList.specification = kvp.Value.SPECIFICATION;
                        //    comToolList.note = kvp.Value.NOTE;
                        //    using (ITransaction trans = session.BeginTransaction())
                        //    {
                        //        session.Save(comToolList);
                        //        trans.Commit();
                                
                        //    }
                        //}
                    }
                }



                //oRng = (Range)sheet.Cells;
                //foreach (KeyValuePair<string,string> kvp in DicToolData)
                //{
                //    CurrentRow = CurrentRow + 1;
                //    oRng[CurrentRow, ToolNumberColumn] = kvp.Key;
                //    oRng[CurrentRow, ToolNameColumn] = kvp.Value;
                //}




                //oRng = (Range)sheet.Cells;
                //for (int i = 0; i < panel.Rows.Count; i++)
                //{
                //    //取得Row,Column
                //    CurrentRow = CurrentRow + 1;

                //    //新版
                //    oRng[CurrentRow, ToolNumberColumn] = panel.GetCell(i, 1).Value.ToString();
                //    oRng[CurrentRow, ToolNameColumn] = panel.GetCell(i, 2).Value.ToString();

                //    //舊版
                //    //oRng[CurrentRow, OpNameColumn] = panel.GetCell(i, 0).Value.ToString();
                //    //oRng[CurrentRow, ToolNumberColumn] = panel.GetCell(i, 1).Value.ToString();
                //    //oRng[CurrentRow, ToolNameColumn] = panel.GetCell(i, 2).Value.ToString();
                //    //oRng[CurrentRow, ToolDescColumn] = panel.GetCell(i, 3).Value.ToString();
                //}

                MessageBox.Show("刀具清單【完成】");
            }
            catch (System.Exception ex)
            {
                MessageBox.Show(ex.ToString());
                MessageBox.Show("刀具清單【失敗】");
            }
            finally
            {
                book.SaveAs(OutputPath, XlFileFormat.xlWorkbookNormal, Type.Missing, Type.Missing, Type.Missing,
                    Type.Missing, XlSaveAsAccessMode.xlNoChange, Type.Missing, Type.Missing, Type.Missing, Type.Missing, Type.Missing);
                book.Close(Type.Missing, Type.Missing, Type.Missing);
                excelApp.Quit();
            }
            
            this.Close();
        }
        private void Cancel_Click(object sender, EventArgs e)
        {
            this.Close();
        }
        private string ReplaceToolNumber(string input)
        {
            string output = input;
            if (input == "T1")
            {
                return output = "T01";
            }
            if (input == "T2")
            {
                return output = "T02";
            }
            if (input == "T3")
            {
                return output = "T03";
            }
            if (input == "T4")
            {
                return output = "T04";
            }
            if (input == "T5")
            {
                return output = "T05";
            }
            if (input == "T6")
            {
                return output = "T06";
            }
            if (input == "T7")
            {
                return output = "T07";
            }
            if (input == "T8")
            {
                return output = "T08";
            }
            if (input == "T9")
            {
                return output = "T09";
            }
            return output;
        }

        

    }
}
