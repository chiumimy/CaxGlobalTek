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
using NXOpen.Utilities;
using NXOpen.CAM;
using System.Text.RegularExpressions;
using DevComponents.DotNetBar.SuperGrid;
using System.Data.OleDb;
using System.Diagnostics;
using Excel = Microsoft.Office.Interop.Excel;
using Microsoft.Office.Interop.Excel;
using System.Runtime.InteropServices;
using System.IO;
using NHibernate;
using DevComponents.DotNetBar;
using DevComponents.AdvTree;
using System.Collections;

namespace ExportShopDoc
{
    public partial class ExportShopDocDlg : DevComponents.DotNetBar.Office2007Form
    {
        
        //public static ISession session = MyHibernateHelper.SessionFactory.OpenSession();
        public static Session theSession = Session.GetSession();
        public static UI theUI;
        public static UFSession theUfSession = UFSession.GetUFSession();
        public static Dictionary<string, OperData> DicNCData = new Dictionary<string, OperData>();
        public static GridPanel panel = new GridPanel();
        public static string CurrentNCGroup = "", PartNo = "";
        public static NXOpen.CAM.Operation[] OperationAry = new NXOpen.CAM.Operation[] { };
        public static NXOpen.CAM.NCGroup[] NCGroupAry = new NXOpen.CAM.NCGroup[] { };
        public static Part workPart = theSession.Parts.Work;
        public static Part displayPart = theSession.Parts.Display;
        public static bool status;
        public static METE_Download_Upload_Path cMETE_Download_Upload_Path = new METE_Download_Upload_Path();
        public static int CurrentRowIndex = -1;
        public static string CurrentSelOperName = "";
        public static List<string> ListSelOper = new List<string>();
        public static string FixturePathStr = "", 
            FixtureNameStr = "", 
            PhotoFolderPath = "", 
            ShopDocPath = "", 
            Is_Local = "", 
            ShopDocFolderPath = "", 
            Local_Folder_CAM = "",
            OutputPath = "",
            PostProcessor = "";
        public static bool Is_Click_Rename = false, Is_BigDlg = false;
        public PartInfo sPartInfo = new PartInfo();
        public static Dictionary<string, List<string>> Dic_MachineNo = new Dictionary<string, List<string>>();
        public static NXOpen.CAM.NCGroup CurrentNC = null;
        public static Dictionary<ControlDimen_Key, List<ControlDimen_Value>> DicControlDimen = new Dictionary<ControlDimen_Key, List<ControlDimen_Value>>();
        public static GridRow singleRow = new GridRow();
        public static Dictionary<string, List<BallonDimen>> DicToolNoBallon = new Dictionary<string, List<BallonDimen>>();
        //public static NXOpen.CAM.Operation singleOperation;

        public struct ProgramName
        {
            public string OldOperName { get; set; }
            public string NewOperName { get; set; }
        }

        public struct OperData
        {
            public string OperName { get; set; }
            public string ToolName { get; set; }
            public string HolderDescription { get; set; }
            public string CuttingLength { get; set; }
            public string CuttingTime { get; set; }
            public string ToolFeed { get; set; }
            public string ToolNumber { get; set; }
            public string ToolSpeed { get; set; }
            public string PartStock { get; set; }
            public string PartFloorStock { get; set; }
            public string CutterLife { get; set; }
            public string Extension { get; set; }
        }

        public struct BallonDimen
        {
            public string Ballon { get; set; }
            public string Dimen { get; set; }
        }

        //2016.12.23新增管制尺寸的資訊
        public struct ControlDimen_Key
        {
            public string NcGroupName { get; set; }
            //public NXOpen.CAM.Operation singleOp { get; set; }
            public string OperationName { get; set; }
            
            public string ToolNo { get; set; }
            public string ToolName { get; set; }
            public string ToolHolder { get; set; }
        }

        //2016.12.23新增管制尺寸的資訊
        public struct ControlDimen_Value
        {
            public string DraftingRev { get; set; }
            public string ControlBallon { get; set; }
            public string TheoryTolRange { get; set; }
            public string ControlTolRange { get; set; }
            //public string ControlMaxTol { get; set; }
            //public string ControlMinTol { get; set; }
        }

        public struct RowColumn
        {
            //刀號
            public int ToolNumberRow { get; set; }
            public int ToolNumberColumn { get; set; }
            //刀具名稱
            public int ToolNameRow { get; set; }
            public int ToolNameColumn { get; set; }
            //程式名稱
            public int OperNameRow { get; set; }
            public int OperNameColumn { get; set; }
            //刀柄名稱
            public int HolderRow { get; set; }
            public int HolderColumn { get; set; }
            //刀具壽命
            public int CutterLifeRow { get; set; }
            public int CutterLifeColumn { get; set; }
            //刀具伸長量
            public int ExtensionRow { get; set; }
            public int ExtensionColumn { get; set; }
            //加工時間
            public int CuttingTimeRow { get; set; }
            public int CuttingTimeColumn { get; set; }
            //進給
            public int ToolFeedRow { get; set; }
            public int ToolFeedColumn { get; set; }
            //速度
            public int ToolSpeedRow { get; set; }
            public int ToolSpeedColumn { get; set; }
            //加工路徑圖片
            public int OperImgToolRow { get; set; }
            public int OperImgToolColumn { get; set; }
            //留料
            public int PartStockRow { get; set; }
            public int PartStockColumn { get; set; }
            //循環時間
            public int TotalCuttingTimeRow { get; set; }
            public int TotalCuttingTimeColumn { get; set; }
            //料號
            public int PartNoRow { get; set; }
            public int PartNoColumn { get; set; }
            //品名
            public int PartDescRow { get; set; }
            public int PartDescColumn { get; set; }
            //機台型號
            public int MachineNoRow { get; set; }
            public int MachineNoColumn { get; set; }
            //設計
            public int DesignedRow { get; set; }
            public int DesignedColumn { get; set; }
            //審核
            public int ReviewedRow { get; set; }
            public int ReviewedColumn { get; set; }
            //批准
            public int ApprovedRow { get; set; }
            public int ApprovedColumn { get; set; }
            //管控刀號
            public int ControlToolNoRow { get; set; }
            public int ControlToolNoColumn { get; set; }
            //管控泡泡
            public int ControlBallonRow { get; set; }
            public int ControlBallonColumn { get; set; }
            //管控尺寸
            public int ControlDimenRow { get; set; }
            public int ControlDimenColumn { get; set; }
        }

        public struct OperImgPosiSize
        {
            public float OperPosiLeft { get; set; }
            public float OperPosiTop { get; set; }
            public float OperImgWidth { get; set; }
            public float OperImgHeight { get; set; }
        }

        public struct FixImgPosiSize
        {
            public float FixPosiLeft { get; set; }
            public float FixPosiTop { get; set; }
            public float FixImgWidth { get; set; }
            public float FixImgHeight { get; set; }
        }

        //public struct PartInfo
        //{
        //    public static string CusName { get; set; }
        //    public static string PartNo { get; set; }
        //    public static string CusRev { get; set; }
        //    public static string OpRev { get; set; }
        //    public static string OpNum { get; set; }
        //}

        public ExportShopDocDlg()
        {
            InitializeComponent();
            //建立panel物件
            panel = superGridProg.PrimaryGrid;

            panel.Columns["拍照"].EditorType = typeof(SetView);

            //預設關閉群組拍照、更名
            GroupSaveView.Enabled = false;
            ConfirmRename.Enabled = false;

            //初始對話框大小
            Size DlgSize = new Size(301, 629);
            this.Size = DlgSize;
            //取得METEDownload_Upload資料
            /*
            status = CaxGetDatData.GetMETEDownload_Upload(out cMETE_Download_Upload_Path);
            if (!status)
            {
                MessageBox.Show("取得METEDownload_Upload失敗");
                return;
            }
            */
            #region 註解中，驗證的資料
            
            /*取得刀具名稱&修改程式名稱
            IntPtr[] b = new IntPtr[] { };
            int a=0;
            foreach (NXOpen.CAM.NCGroup ncGroup in NCGroupAry)
            {
                int type;
                int subtype;
                
                theUfSession.Obj.AskTypeAndSubtype(ncGroup.Tag, out type, out subtype);
                
                if (type == UFConstants.UF_machining_tool_type)
                {
                    CaxLog.ShowListingWindow(ncGroup.Name);
                }
            }
            NXOpen.CAM.Operation[] aaa = displayPart.CAMSetup.CAMOperationCollection.ToArray();//取得operationName
            for (int i = 0; i < aaa.Length; i++)
            {
                aaa[i].SetName(600 + i.ToString());
            }
            */
            

            //this.Close();
//             int count = 0;
//             string[] type_names;
//             theUfSession.Cam.OptAskTypes(out count, out type_names);//取得Create Tool中的Type(count=總數、type_names=參數名稱)
//             CaxLog.ShowListingWindow("count：" + count);
//             for (int i = 0; i < type_names.Length;i++ )
//             {
//                 CaxLog.ShowListingWindow("type_names[i]："+type_names[i].ToString());
//             }
//             int sub_count = 0;
//             string[] subtype_names;
//             UFCam.OptStypeCls subtype_class = UFCam.OptStypeCls.OptStypeClsOper;
//             theUfSession.Cam.OptAskSubtypes(type_names[0], subtype_class, out sub_count, out subtype_names);
//             CaxLog.ShowListingWindow("sub_count：" + sub_count);
//             for (int i = 0; i < subtype_names.Length; i++)
//             {
//                 CaxLog.ShowListingWindow("subtype_names[i]：" + subtype_names[i].ToString());
//             }
//             try
//             {
//                 string a = "";
//                 theUfSession.Part.AskPartName(displayPart.Tag, out a);
//                 CaxLog.ShowListingWindow(a.ToString());
//             }
//             catch (System.Exception ex)
//             {
//                 CaxLog.ShowListingWindow("沒有displayPart.Name");
//             }
//             try
//             {
//                 CaxLog.ShowListingWindow(displayPart.FullPath.ToString());
//             }
//             catch (System.Exception ex)
//             {
//                 CaxLog.ShowListingWindow("沒有displayPart.FullPath");
//             }
//             try
//             {
//                 NXOpen.Tag tagRootPart = NXOpen.Tag.Null;
//                 tagRootPart = theUfSession.Assem.AskRootPartOcc(displayPart.Tag);
//                 CaxLog.ShowListingWindow(displayPart.Tag.ToString());
//                 CaxLog.ShowListingWindow(tagRootPart.ToString());
//             }
//             catch (System.Exception ex)
//             {
//                 CaxLog.ShowListingWindow("沒有displayPart.Tag");
//             }

            #endregion
        }

        
        private void InitialzeMachineTree()
        {
            MachineTree.Nodes.Clear();
            MachineTree.Enabled = false;

            #region 加入機台型號
            MachineTree.BeginUpdate();
            ISession session = MyHibernateHelper.SessionFactory.OpenSession();
            IList<Sys_MachineType> sysMachineType = session.QueryOver<Sys_MachineType>().List<Sys_MachineType>();
            foreach (Sys_MachineType i in sysMachineType)
            {
                Node node1 = new Node();
                node1.Tag = i.machineTypeSrNo;
                node1.Text = i.machineType;
                node1.ExpandVisibility = eNodeExpandVisibility.Visible;
                
                IList<Sys_MachineNo> sysMachineNo = session.QueryOver<Sys_MachineNo>()
                                                            .Where(x => x.sysMachineType.machineTypeSrNo == i.machineTypeSrNo)
                                                            .List<Sys_MachineNo>();
                foreach (Sys_MachineNo ii in sysMachineNo)
                {
                    Node node2 = new Node();
                    node2.Text = ii.machineNo;
                    node2.CheckBoxVisible = true;
                    Cell cell1 = new Cell();
                    cell1.Text = ii.machineID;
                    Cell cell2 = new Cell();
                    cell2.Text = ii.machineName;
                    Cell cell3 = new Cell();
                    cell3.Tag = ii.postprocessor;
                    node2.Cells.Add(cell1);
                    node2.Cells.Add(cell2);
                    node2.Cells.Add(cell3);
                    //node.ExpandVisibility = eNodeExpandVisibility.Visible;//此條為在文字前面加入可展開的符號(+)
                    node1.Nodes.Add(node2);
                }
                MachineTree.Nodes.Add(node1);
            }
            MachineTree.EndUpdate();
            #endregion
        }
        
        
        private void MachineTree_BeforeExpand(object sender, AdvTreeNodeCancelEventArgs e)
        {
            //Node parent = e.Node;
            //if (parent.Nodes.Count > 0) return;

            //status = GetMachineNo(parent);
            //if (!status)
            //{
            //    MessageBox.Show("機台型號資料錯誤！");
            //}
        }
        
        
        private bool GetMachineNo(Node parent)
        {
            try
            {
                //表示沒有資料可以再搜尋了
                if (parent.Tag == null)
                {
                    return true;
                }
                ISession session = MyHibernateHelper.SessionFactory.OpenSession();
                IList<Sys_MachineNo> sysMachineNo = session.QueryOver<Sys_MachineNo>()
                                                            .Where(x => x.sysMachineType.machineTypeSrNo == (int)parent.Tag)
                                                            .List<Sys_MachineNo>();
                foreach (Sys_MachineNo i in sysMachineNo)
                {
                    Node node = new Node();
                    //node.Text = i.machineNo + "," + i.machineID + "," + i.machineName;
                    node.Text = i.machineNo;
                    node.CheckBoxVisible = true;
                    //Node supNode1 = new Node();
                    //supNode1.Text = i.machineID;
                    //Node supNode2 = new Node();
                    //supNode2.Text = i.machineName;
                    //supNode1.Nodes.Add(supNode2);
                    //node.Nodes.Add(supNode1);
                    //node.ExpandVisibility = eNodeExpandVisibility.Visible;//此條為在文字前面加入可展開的符號(+)
                    parent.Nodes.Add(node);
                }

                
                
            }
            catch (System.Exception ex)
            {
                MessageBox.Show(ex.ToString());
            	return false;
            }
            return true;
        }
        
        /*
        public void InitializeRadialMenu()
        {
            radialMenu.ItemClick += new EventHandler(RadialMenuItemClick);

            #region 加入L機型
            RadialMenuItem item = new RadialMenuItem();
            item.Text = "L";
            radialMenu.Items.Add(item);
            foreach (KeyValuePair<string,List<string>> kvp in Dic_MachineNo)
            {
                foreach (string i in kvp.Value)
                {
                    RadialMenuItem childItem1 = new RadialMenuItem();
                    childItem1.Text = i;
                    item.SubItems.Add(childItem1);
                }
            }
            #endregion

            // Create spacer item
            item = new RadialMenuItem();
            radialMenu.Items.Add(item);

            #region 加入M機型
            item = new RadialMenuItem();
            item.Text = "M";
            radialMenu.Items.Add(item);
            #endregion
            
            // Create spacer item
            item = new RadialMenuItem();
            radialMenu.Items.Add(item);

            #region 加入LM機型
            item = new RadialMenuItem();
            item.Text = "LM";
            radialMenu.Items.Add(item);
            #endregion

            // Create spacer item
            item = new RadialMenuItem();
            radialMenu.Items.Add(item);

            #region 加入E機型
            item = new RadialMenuItem();
            item.Text = "E";
            radialMenu.Items.Add(item);
            RadialMenuItem childItem = new RadialMenuItem();
            childItem.Text = "Sub menu 1";
            item.SubItems.Add(childItem);
            #endregion

            // Create spacer item
            item = new RadialMenuItem();
            radialMenu.Items.Add(item);
        }
        */

        //private void RadialMenuItemClick(object sender, EventArgs e)
        //{
        //    RadialMenuItem item = sender as RadialMenuItem;
        //    if (item != null && !string.IsNullOrEmpty(item.Text))
        //    {
        //        MessageBox.Show(item.Text);
        //    }
        //}

        //private BaseItem CreateItem(string text)
        //{
        //    RadialMenuItem item = new RadialMenuItem();
        //    item.Text = text;
        //    return item;
        //}

        private void ExportShopDocDlg_Load(object sender, EventArgs e)
        {
            Is_Local = Environment.GetEnvironmentVariable("UGII_ENV_FILE");
            #region 取得ShopDoc.xls路徑
            
            if (Is_Local != null)
            {
                //取得Server的ShopDoc.xls路徑
                ShopDocPath = string.Format(@"{0}\{1}\{2}\{3}", CaxEnv.GetGlobaltekEnvDir(), "TE_Config", "Config", "ShopDoc.xls");
                
                //取得METEDownload_Upload.dat
                CaxGetDatData.GetMETEDownload_Upload(out cMETE_Download_Upload_Path);

                //加入機台資訊
                InitialzeMachineTree();
            }
            else
            {
                ShopDocPath = string.Format(@"{0}\{1}", "D:", "ShopDoc.xls");
            }
            
            #endregion

            //取得正確路徑，拆零件路徑字串取得客戶名稱、料號、版本
            status = CaxPublic.GetAllPath("TE", displayPart.FullPath, out sPartInfo, ref cMETE_Download_Upload_Path);
            if (!status && !displayPart.FullPath.Contains("Task"))
            {
                Is_Local = null;
                PartNo = Path.GetFileNameWithoutExtension(displayPart.FullPath);
            }
            else
            {
                PartNo = sPartInfo.PartNo;
            }


            //取得所有GroupAry，用來判斷Group的Type決定是NC、Tool、Geometry
            NCGroupAry = displayPart.CAMSetup.CAMGroupCollection.ToArray();
            //取得所有OperationAry
            OperationAry = displayPart.CAMSetup.CAMOperationCollection.ToArray();

            #region (註解中)test
            /*
            foreach (NXOpen.CAM.NCGroup ncGroup in NCGroupAry)
            {
                int type;
                int subtype;

                theUfSession.Obj.AskTypeAndSubtype(ncGroup.Tag, out type, out subtype);
                
                if (type == UFConstants.UF_machining_tool_type)
                {
                    NXOpen.CAM.Tool tool1 = (NXOpen.CAM.Tool)NXObjectManager.Get(ncGroup.Tag);
                    
                    Tool.Types type1;
                    Tool.Subtypes subtype1;
                    tool1.GetTypeAndSubtype(out type1, out subtype1);
                    if (type1 == Tool.Types.Drill)
                    {
                        NXOpen.CAM.DrillStdToolBuilder drillStdToolBuilder1;
                        drillStdToolBuilder1 = workPart.CAMSetup.CAMGroupCollection.CreateDrillStdToolBuilder(tool1);
                        string aaaaa = drillStdToolBuilder1.TlHolderDescription;
                        //CaxLog.ShowListingWindow(aaaaa);
                    }
                    else if (type1 == Tool.Types.Mill)
                    {
                        NXOpen.CAM.MillingToolBuilder drillStdToolBuilder1;
                        drillStdToolBuilder1 = workPart.CAMSetup.CAMGroupCollection.CreateMillToolBuilder(tool1);
                        string aaaaa = drillStdToolBuilder1.TlHolderDescription;
                        //CaxLog.ShowListingWindow(aaaaa);
                    }
                    else if (type1 == Tool.Types.MillForm)
                    {
                        NXOpen.CAM.MillingToolBuilder drillStdToolBuilder1;
                        drillStdToolBuilder1 = workPart.CAMSetup.CAMGroupCollection.CreateMillToolBuilder(tool1);
                        string aaaaa = drillStdToolBuilder1.TlHolderDescription;
                        //CaxLog.ShowListingWindow(aaaaa);
                    }
                }
                else if (type == UFConstants.UF_machining_task_type)
                {
                    //取得NCProgram名稱
                    NXOpen.CAM.NCGroup tool1 = (NXOpen.CAM.NCGroup)NXObjectManager.Get(ncGroup.Tag);

                }



                if (type == UFConstants.UF_machining_tool_type)
                {
                    NXOpen.CAM.Tool tool1 = (NXOpen.CAM.Tool)NXObjectManager.Get(ncGroup.Tag);
                    Tool.Types type1;
                    Tool.Subtypes subtype1;
                    tool1.GetTypeAndSubtype(out type1, out subtype1);
                    
                    //                     for (int i = 0; i < type1.Length; i++)
                    //                     {
                    //                         CaxLog.ShowListingWindow(a[i].Name);
                    //                     }

                    //NXOpen.CAM.MillingToolBuilder drillStdToolBuilder1;
                    //drillStdToolBuilder1 = workPart.CAMSetup.CAMGroupCollection.CreateMillToolBuilder(tool1);
                    //drillStdToolBuilder1 = workPart.CAMSetup.CAMGroupCollection.CreateDrillStdToolBuilder(tool1);
                    //string aaaaa = drillStdToolBuilder1.TlHolderDescription;
                    //CaxLog.ShowListingWindow(aaaaa);
                    //drillStdToolBuilder1.TlHolderDescription = "123";//設定或取得Description數值
                    //drillStdToolBuilder1.Commit();
                    //drillStdToolBuilder1.Destroy();
                }
            }
            */
            #endregion

            #region 取得相關資訊，填入DIC
            //string ncGroupName = "";
            DicNCData = new Dictionary<string, OperData>();
            foreach (NXOpen.CAM.NCGroup ncGroup in NCGroupAry)
            {
                int type,subtype;
                theUfSession.Obj.AskTypeAndSubtype(ncGroup.Tag, out type, out subtype);

                //此處比對是否為Program群組
                if (type != UFConstants.UF_machining_task_type) 
                    continue;
                
                if (!ncGroup.Name.Contains("OP"))
                {
                    MessageBox.Show("請先手動將Group名稱：" + ncGroup.Name + "，改為正確格式，再重新啟動功能！");
                    this.Close();
                }

                //取得此NCGroup下的所有Oper
                CAMObject[] OperGroup = ncGroup.GetMembers();
                try
                {
                    foreach (NXOpen.CAM.Operation item in OperGroup)
                    {
                        string StockStr = "", FloorstockStr = "";
                        CaxOper.AskOperStock(item, out StockStr, out FloorstockStr);

                        bool cheValue;
                        OperData sOperData = new OperData();
                        cheValue = DicNCData.TryGetValue(ncGroup.Name, out sOperData);
                        if (!cheValue)
                        {
                            sOperData.OperName = item.Name;
                            sOperData.ToolName = CaxOper.AskOperToolNameFromTag(item.Tag);
                            sOperData.HolderDescription = CaxOper.AskOperHolderDescription(item);
                            sOperData.CuttingLength = Convert.ToDouble(CaxOper.AskOperTotalCuttingLength(item)).ToString("f3");
                            sOperData.ToolFeed = Math.Round(Convert.ToDouble(CaxOper.AskOperToolFeed(item)), 3, MidpointRounding.AwayFromZero).ToString();
                            sOperData.CuttingTime = Math.Ceiling((Convert.ToDouble(CaxOper.AskOperTotalCuttingTime(item)) * 60)).ToString();//因為進給單位mmpm，距離單位mm，將進給的60放來這邊乘
                            sOperData.ToolNumber = "T" + CaxOper.AskOperToolNumber(item);
                            sOperData.ToolSpeed = CaxOper.AskOperToolSpeed(item);
                            sOperData.PartStock = StockStr;
                            sOperData.PartFloorStock = FloorstockStr;
                            sOperData.CutterLife = CaxOper.AskOperToolERPNumFromAttribute(item.Tag, "GTTL_CUTTER_LIFE");
                            sOperData.Extension = CaxOper.AskOperToolERPNumFromAttribute(item.Tag, "GTTL_EXTENTION");
                            DicNCData.Add(ncGroup.Name, sOperData);
                        }
                        else
                        {
                            sOperData.OperName = sOperData.OperName + "," + item.Name;
                            sOperData.ToolName = sOperData.ToolName + "," + CaxOper.AskOperToolNameFromTag(item.Tag);
                            sOperData.HolderDescription = sOperData.HolderDescription + "," + CaxOper.AskOperHolderDescription(item);
                            sOperData.CuttingLength = sOperData.CuttingLength + "," + Convert.ToDouble(CaxOper.AskOperTotalCuttingLength(item)).ToString("f3");
                            sOperData.ToolFeed = sOperData.ToolFeed + "," + Math.Round(Convert.ToDouble(CaxOper.AskOperToolFeed(item)), 3, MidpointRounding.AwayFromZero).ToString();
                            sOperData.CuttingTime = sOperData.CuttingTime + "," + Math.Ceiling((Convert.ToDouble(CaxOper.AskOperTotalCuttingTime(item)) * 60)).ToString();//因為進給單位mmpm，距離單位mm，將進給的60放來這邊乘
                            sOperData.ToolNumber = sOperData.ToolNumber + "," + "T" + CaxOper.AskOperToolNumber(item);
                            sOperData.ToolSpeed = sOperData.ToolSpeed + "," + CaxOper.AskOperToolSpeed(item);
                            sOperData.PartStock = sOperData.PartStock + "," + StockStr;
                            sOperData.PartFloorStock = sOperData.PartFloorStock + "," + FloorstockStr;
                            sOperData.CutterLife = sOperData.CutterLife + "," + CaxOper.AskOperToolERPNumFromAttribute(item.Tag, "GTTL_CUTTER_LIFE");
                            sOperData.Extension = sOperData.Extension + "," + CaxOper.AskOperToolERPNumFromAttribute(item.Tag, "GTTL_EXTENTION");
                            DicNCData[ncGroup.Name] = sOperData;
                        }
                    }
                }
                catch (System.Exception ex)
                {

                }
            }

            //將DicProgName的key存入程式群組下拉選單中
            foreach (KeyValuePair<string, OperData> kvp in DicNCData) 
                comboBoxNCgroup.Items.Add(kvp.Key);

            #endregion

            #region (註解中)設定輸出路徑

            //暫時使用的路徑
            //string[] FolderFile = System.IO.Directory.GetFileSystemEntries(Path.GetDirectoryName(displayPart.FullPath), "*.xls");
            //OutputPath.Text = string.Format(@"{0}\{1}", Path.GetDirectoryName(displayPart.FullPath), 
            //                                            Path.GetFileNameWithoutExtension(displayPart.FullPath) + "_" + (FolderFile.Length + 1) + ".xls");
            
            

            /*-------以下發布版本
            //取得總組立名稱與全路徑
            string PartNoFullPath = Path.GetDirectoryName(displayPart.FullPath);//回傳：IP:\Globaltek\Task\廠商\料號\版次
            string[] splitPartNoFullPath = PartNoFullPath.Split('\\');
            if (splitPartNoFullPath.Length<5)
            {
                CaxLog.ShowListingWindow("未使用下載檔案工具，請手動建立資料架構！");
                this.Close();
            }

            
            string Local_IP = cMETE_Download_Upload_Path.Local_IP;
            string Local_ShareStr = cMETE_Download_Upload_Path.Local_ShareStr;
            Local_Folder_CAM = cMETE_Download_Upload_Path.Local_Folder_CAM;

            Local_ShareStr = Local_ShareStr.Replace("[Local_IP]", Local_IP);
            Local_ShareStr = Local_ShareStr.Replace("[CusName]", splitPartNoFullPath[3]);
            Local_ShareStr = Local_ShareStr.Replace("[PartNo]", splitPartNoFullPath[4]);
            Local_ShareStr = Local_ShareStr.Replace("[CusRev]", splitPartNoFullPath[5]);
            Local_Folder_CAM = Local_Folder_CAM.Replace("[Local_ShareStr]", Local_ShareStr);
            Local_Folder_CAM = Local_Folder_CAM.Replace("[Oper1]", Regex.Replace(ncGroupName, "[^0-9]", ""));
            
            //取得資料夾內所有檔案
            if (!Directory.Exists(Local_Folder_CAM))
            {
                CaxLog.ShowListingWindow("資料夾架構建立有誤，請聯繫開發人員！");
                this.Close();
            }
            string[] FolderFile = System.IO.Directory.GetFileSystemEntries(Local_Folder_CAM, "*.xls");
            //設定輸出路徑與檔名
            OutputPath.Text = string.Format(@"{0}\{1}", Local_Folder_CAM, PartNo + "_" + (FolderFile.Length + 1) + ".xls");
            */

            #endregion

            

        }

        /*
        public static string AskOperHolderDescription(NXOpen.CAM.Operation singleOper)
        {
            string operHolderDescription = "";
            try
            {
                NCGroup singleOperParent = singleOper.GetParent(CAMSetup.View.MachineTool);//由Oper取得刀子
                NXOpen.CAM.Tool tool = (NXOpen.CAM.Tool)NXObjectManager.Get(singleOperParent.Tag);//取得Oper的刀子名稱
                Tool.Types type;
                Tool.Subtypes subtype;
                tool.GetTypeAndSubtype(out type, out subtype);
                if (type == Tool.Types.Barrel)
                {
                    NXOpen.CAM.BarrelToolBuilder ToolBuilder1;
                    ToolBuilder1 = workPart.CAMSetup.CAMGroupCollection.CreateBarrelToolBuilder(tool);
                    operHolderDescription = ToolBuilder1.TlHolderDescription;
                    return operHolderDescription;
                }
                else if (type == Tool.Types.Drill)
                {
                    NXOpen.CAM.DrillStdToolBuilder ToolBuilder1;
                    ToolBuilder1 = workPart.CAMSetup.CAMGroupCollection.CreateDrillStdToolBuilder(tool);
                    operHolderDescription = ToolBuilder1.TlHolderDescription;
                    //CaxLog.ShowListingWindow(operHolderDescription);
                    return operHolderDescription;
                }
                else if (type == Tool.Types.DrillSpcGroove)
                {
                    NXOpen.CAM.MillingToolBuilder ToolBuilder1;
                    ToolBuilder1 = workPart.CAMSetup.CAMGroupCollection.CreateMillToolBuilder(tool);
                    operHolderDescription = ToolBuilder1.TlHolderDescription;
                    return operHolderDescription;
                }
                else if (type == Tool.Types.Mill)
                {
                    NXOpen.CAM.MillingToolBuilder ToolBuilder1;
                    ToolBuilder1 = workPart.CAMSetup.CAMGroupCollection.CreateMillToolBuilder(tool);
                    operHolderDescription = ToolBuilder1.TlHolderDescription;
                    return operHolderDescription;
                }
                else if (type == Tool.Types.MillForm)
                {
                    NXOpen.CAM.MillingToolBuilder ToolBuilder1;
                    ToolBuilder1 = workPart.CAMSetup.CAMGroupCollection.CreateMillToolBuilder(tool);
                    operHolderDescription = ToolBuilder1.TlHolderDescription;
                    return operHolderDescription;
                }
                else if (type == Tool.Types.Solid)
                {

                }
                else if (type == Tool.Types.Tcutter)
                {
                    NXOpen.CAM.MillingToolBuilder ToolBuilder1;
                    ToolBuilder1 = workPart.CAMSetup.CAMGroupCollection.CreateMillToolBuilder(tool);
                    operHolderDescription = ToolBuilder1.TlHolderDescription;
                    return operHolderDescription;
                }
                else if (type == Tool.Types.Thread)
                {
                    NXOpen.CAM.ThreadToolBuilder ToolBuilder1;
                    ToolBuilder1 = workPart.CAMSetup.CAMGroupCollection.CreateThreadToolBuilder(tool);
                    operHolderDescription = ToolBuilder1.TlDescription; 
                    return operHolderDescription;
                }
                else if (type == Tool.Types.Turn)
                {
                    NXOpen.CAM.TurningToolBuilder ToolBuilder1;
                    ToolBuilder1 = workPart.CAMSetup.CAMGroupCollection.CreateTurnToolBuilder(tool);
                    operHolderDescription = ToolBuilder1.TlDescription; 
                    return operHolderDescription;
                }
                else if (type == Tool.Types.Wedm)
                {

                }
                else if (type == Tool.Types.Groove)
                {
                    NXOpen.CAM.GrooveToolBuilder ToolBuilder1;
                    ToolBuilder1 = workPart.CAMSetup.CAMGroupCollection.CreateGrooveToolBuilder(tool);
                    operHolderDescription = ToolBuilder1.TlDescription; 
                    return operHolderDescription;
                }

            }
            catch (System.Exception ex)
            {
                operHolderDescription = "尚未建立Builder存取資料";
                return operHolderDescription;
            }
            return operHolderDescription;
        }
        */

        #region (註解)刀具路徑與清單的輸出路徑
        //private void buttonSelePath_Click(object sender, EventArgs e)
        //{
        //    string selectDir = "";
        //    CaxPublic.SaveFileDialog(OutputPath.Text, out selectDir);
        //    OutputPath.Text = selectDir;
        //}
        #endregion
        

        private void comboBoxNCgroup_SelectedIndexChanged(object sender, EventArgs e)
        {
            if (Is_Local != null)
            {
                InitialzeMachineTree();
            }
            
            MachineTree.Enabled = true;
            //清空superGrid資料&機台資訊
            panel.Rows.Clear();
            MachineNo.Text = "";
            //取得comboBox資料
            CurrentNCGroup = comboBoxNCgroup.Text;
            //關閉群組拍照(原因：使用者在前一個NC是多選所以被打開，換到現在NC後都還沒開始選所以要關閉)
            GroupSaveView.Enabled = false;
            //打開拍照(原因：使用者在前一個NC是多選所以被關閉，換到現在NC後都還沒開始選所以要打開)
            panel.Columns["拍照"].ReadOnly = false;

            #region 建立ImageFolder、ShopDocFolder資料夾
            if (Is_Local != null)
            {
                PhotoFolderPath = string.Format(@"{0}\{1}_Image", cMETE_Download_Upload_Path.Local_Folder_CAM, CurrentNCGroup);
                //ShopDocFolderPath = string.Format(@"{0}\{1}_ShopDoc", cMETE_Download_Upload_Path.Local_Folder_CAM, CurrentNCGroup);
            }
            else
            {
                PhotoFolderPath = string.Format(@"{0}\{1}_Image", Path.GetDirectoryName(displayPart.FullPath), CurrentNCGroup);
                //ShopDocFolderPath = string.Format(@"{0}\{1}_ShopDoc", Path.GetDirectoryName(displayPart.FullPath), CurrentNCGroup);
            }
            if (!Directory.Exists(PhotoFolderPath))
            {
                System.IO.Directory.CreateDirectory(PhotoFolderPath);
            }
            //因為路徑太長(超過260字元)，無法將shopDoc檔案存到CAM裡面
            ShopDocFolderPath = string.Format(@"{0}\{1}_ShopDoc", Path.GetDirectoryName(displayPart.FullPath), CurrentNCGroup);
            if (!Directory.Exists(ShopDocFolderPath))
            {
                System.IO.Directory.CreateDirectory(ShopDocFolderPath);
            }
            #endregion


            string[] FolderFile = System.IO.Directory.GetFileSystemEntries(ShopDocFolderPath, "*.xls");
            OutputPath = string.Format(@"{0}\{1}_{2}_{3}", ShopDocFolderPath
                                                              , Path.GetFileNameWithoutExtension(displayPart.FullPath)
                                                              , CurrentNCGroup
                                                              , (FolderFile.Length + 1) + ".xls");
            //OutputPath.Text = string.Format(@"{0}\{1}\{2}", Path.GetDirectoryName(displayPart.FullPath)
            //                                                , CurrentNCGroup
            //                                                , Path.GetFileNameWithoutExtension(displayPart.FullPath) + "_" + CurrentNCGroup + "_" + (FolderFile.Length + 1) + ".xls");

            //拆群組名稱字串取得製程序(EX：OP210=>210)
            string[] splitCurrentNCGroup = CurrentNCGroup.Split('_');
            string OperNum = Regex.Replace(splitCurrentNCGroup[0], "[^0-9]", "");
            
            #region (註解中)拍Oper刀具路徑圖
            //foreach (NXOpen.CAM.NCGroup ncGroup in NCGroupAry)
            //{
            //    if (CurrentNCGroup == ncGroup.Name)
            //    {
            //        for (int i = 0; i < OperationAry.Length; i++)
            //        {
            //            //取得父層的群組(回傳：NCGroup XXXX)
            //            string NCProgramTag = OperationAry[i].GetParent(CAMSetup.View.ProgramOrder).ToString();
            //            NCProgramTag = Regex.Replace(NCProgramTag, "[^0-9]", "");
            //            NXOpen.CAM.CAMObject[] tempObjToCreateImg = new CAMObject[1];
            //            string ImagePath = "";
            //            if (NCProgramTag == ncGroup.Tag.ToString())
            //            {
            //                tempObjToCreateImg[0] = (NXOpen.CAM.CAMObject)OperationAry[i];
            //                workPart.CAMSetup.ReplayToolPath(tempObjToCreateImg);
            //                workPart.ModelingViews.WorkView.Orient(NXOpen.View.Canned.Isometric, NXOpen.View.ScaleAdjustment.Fit);
            //                ImagePath = string.Format(@"{0}\{1}", Local_Folder_CAM, OperationAry[i].Name);
            //                theUfSession.Disp.CreateImage(ImagePath, UFDisp.ImageFormat.Jpeg, UFDisp.BackgroundColor.White);
            //            }
            //        }
            //    }
            //}

            #endregion

            #region 取得選擇的NCGroup
            foreach (NXOpen.CAM.NCGroup ncGroup in NCGroupAry)
            {
                int type;
                int subtype;
                theUfSession.Obj.AskTypeAndSubtype(ncGroup.Tag, out type, out subtype);

                if (type != UFConstants.UF_machining_task_type)//此處比對是否為Program群組
                {
                    continue;
                }
                if (CurrentNCGroup != ncGroup.Name)
                {
                    continue;
                }

                CurrentNC = ncGroup;
                break;
            }
            #endregion

            #region 填程式條數、並請使用者輸入起始值

            foreach (KeyValuePair<string, OperData> kvp in DicNCData)
            {
                if (CurrentNCGroup != kvp.Key)
                    continue;

                string[] splitOperName = kvp.Value.OperName.Split(',');
                ProgramCount.Text = splitOperName.Length.ToString();
            }

            /*
            GridRow row = new GridRow();
            foreach (KeyValuePair<string, OperData> kvp in DicNCData)
            {
                if (CurrentNCGroup != kvp.Key)
                {
                    continue;
                }
                string[] splitOperName = kvp.Value.OperName.Split(',');
                string[] splitToolName = kvp.Value.ToolName.Split(',');
                string[] splitHolderDescription = kvp.Value.HolderDescription.Split(',');
                string[] splitOperCuttingLength = kvp.Value.CuttingLength.Split(',');
                string[] splitOperToolFeed = kvp.Value.ToolFeed.Split(',');
                string[] splitOperCuttingTime = kvp.Value.CuttingTime.Split(',');
                string[] splitOperToolNumber = kvp.Value.ToolNumber.Split(',');
                string[] splitOperToolSpeed = kvp.Value.ToolSpeed.Split(',');

                for (int i = 0; i < splitOperName.Length; i++)
                {
                    //處理單主軸or多主軸中的第一主軸OPxxx_1
                    if (splitCurrentNCGroup.Length == 1 || (splitCurrentNCGroup.Length == 2 && splitCurrentNCGroup[1] == "1"))
                    {
                        int y = i + 1;
                        if (i < 9)
                        {
                            row = new GridRow(splitOperName[i], "O" + OperNum + y, splitOperToolNumber[i], splitToolName[i],
                                splitHolderDescription[i], splitOperCuttingLength[i], splitOperToolFeed[i], splitOperToolSpeed[i], splitOperCuttingTime[i], "拍照");
                        }
                        else
                        {
                            string tempOperNum = (Convert.ToDouble(OperNum) * 0.1).ToString();
                            row = new GridRow(splitOperName[i], "O" + tempOperNum + y, splitOperToolNumber[i], splitToolName[i],
                                splitHolderDescription[i], splitOperCuttingLength[i], splitOperToolFeed[i], splitOperToolSpeed[i], splitOperCuttingTime[i], "拍照");
                        }
                    }
                    else//處理多主軸中的第二主軸
                    {
                        int y = 50 + (i + 1);
                        string tempOperNum = (Convert.ToDouble(OperNum) * 0.1).ToString();
                        row = new GridRow(splitOperName[i], "O" + tempOperNum + y, splitOperToolNumber[i], splitToolName[i],
                            splitHolderDescription[i], splitOperCuttingLength[i], splitOperToolFeed[i], splitOperToolSpeed[i], splitOperCuttingTime[i], "拍照");
                    }
                    panel.Rows.Add(row);
                }
            }
            */
            #endregion

            #region 如果先前已更名，再次開啟時直接載入
            try
            {
                ProgramStart.Text = CurrentNC.GetStringAttribute("ProgramStart");
                SetProgName_Click(sender, e);
            }
            catch (System.Exception ex)
            {
                ProgramStart.Text = "";
            }
            #endregion

            if (Is_Local != null)
            {
                //填機台到MachineNo欄位
                GetMachineDataToMachineNo(CurrentNC);

                //填舊的尺寸控制資訊
                try
                {
                    ISession session = MyHibernateHelper.SessionFactory.OpenSession();
                    Com_PEMain comPEMain = session.QueryOver<Com_PEMain>().Where(x => x.partName == sPartInfo.PartNo)
                                                                      .Where(x => x.customerVer == sPartInfo.CusRev)
                                                                      .Where(x => x.opVer == sPartInfo.OpRev).SingleOrDefault<Com_PEMain>();
                    Com_PartOperation comPartOperation = session.QueryOver<Com_PartOperation>().Where(x => x.comPEMain == comPEMain)
                                                                                           .Where(x => x.operation1 == OperNum)
                                                                                           .SingleOrDefault<Com_PartOperation>();
                    Com_TEMain comTEMain = session.QueryOver<Com_TEMain>().Where(x => x.comPartOperation == comPartOperation)
                                                                      .Where(x => x.ncGroupName == CurrentNCGroup)
                                                                      .SingleOrDefault<Com_TEMain>();
                    IList<Com_ControlDimen> listComControlDimen = session.QueryOver<Com_ControlDimen>().Where(x => x.comTEMain == comTEMain).List<Com_ControlDimen>();


                    foreach (Com_ControlDimen i in listComControlDimen)
                    {
                        List<BallonDimen> listBallonDimen = new List<BallonDimen>();
                        status = DicToolNoBallon.TryGetValue(i.toolNo, out listBallonDimen);
                        if (!status)
                        {
                            listBallonDimen = new List<BallonDimen>();
                            BallonDimen sBallonDimen = new BallonDimen();
                            sBallonDimen.Ballon = i.controlBallon.ToString();
                            sBallonDimen.Dimen = i.controlDimen;
                            listBallonDimen.Add(sBallonDimen);
                            DicToolNoBallon.Add(i.toolNo, listBallonDimen);
                        }
                        else
                        {
                            BallonDimen sBallonDimen = new BallonDimen();
                            sBallonDimen.Ballon = i.controlBallon.ToString();
                            sBallonDimen.Dimen = i.controlDimen;
                            listBallonDimen.Add(sBallonDimen);
                            DicToolNoBallon[i.toolNo] = listBallonDimen;
                        }
                    }
                }
                catch (System.Exception ex)
                {
                	
                }

                
                
            }

            
        }

        private void ConfirmRename_Click(object sender, EventArgs e)
        {
            if (CurrentNCGroup == "") 
                return;

            string RenameStr = "";
            
            foreach (NXOpen.CAM.NCGroup ncGroup in NCGroupAry)
            {
                if (CurrentNCGroup == ncGroup.Name)
                {
                    //取得此NCGroup下的所有Oper
                    CAMObject[] OperGroup = ncGroup.GetMembers();

                    //先將Oper更名成與新Oper名稱完全不衝突(防止前一條程式的名稱與之後的舊Oper名稱相同)
                    
                    try
                    {
                        int count1 = 0, count2 = 0;
                        foreach (NXOpen.CAM.Operation item in OperGroup)
                        {
                            item.SetName(item.Name + count1 + count2);
                            count1++;
                            count2++;
                        }
                    }
                    catch (System.Exception ex)
                    {
                    	
                    }

                    theUfSession.UiOnt.Refresh();

                    //真正更名成新的Oper名稱
                    try
                    {
                        //int count = 0;
                        for (int i = 0; i < OperGroup.Length;i++ )
                        {
                            int type;
                            int subtype;
                            theUfSession.Obj.AskTypeAndSubtype(OperGroup[i].Tag, out type, out subtype);
                            if (type != 100)
                            {
                                continue;
                            }
                            RenameStr = panel.GetCell(i, 1).Value.ToString();
                            OperGroup[i].SetName(RenameStr);
                        }
                    }
                    catch (System.Exception ex)
                    {
                        MessageBox.Show("注意：" + RenameStr + " 程式名重複，請手動檢查並更改！");
                    }
                    

                    theUfSession.UiOnt.Refresh();
                }
            }

            theUfSession.UiOnt.Refresh();

            #region 重新將更名後的Oper名稱寫回Dic中

            string NewOperName = "";
            for (int i = 0; i < panel.Rows.Count;i++ )
            {
                string tempOperName = panel.GetCell(i, 1).Value.ToString();
                if (i==0)
                {
                    NewOperName = tempOperName;
                }
                else
                {
                    NewOperName = NewOperName + "," + tempOperName;
                }
            }
            
            OperData NewOperData = new OperData();
            DicNCData.TryGetValue(CurrentNCGroup, out NewOperData);
            NewOperData.OperName = NewOperName;
            DicNCData[CurrentNCGroup] = NewOperData;

            #endregion

            Is_Click_Rename = true;

            //塞屬性表示已更名過
            CurrentNC.SetAttribute("ProgramStart", ProgramStart.Text);
            

        }

        private void MachineTree_NodeClick(object sender, TreeNodeMouseEventArgs e)
        {
            MachineNo.Clear();
            SelectedMachineNo();
        }

        private void SelectedMachineNo()
        {
            try
            {
                for (int i = 0; i < MachineTree.Nodes.Count; i++)
                {
                    for (int j = 0; j < MachineTree.Nodes[i].Nodes.Count; j++)
                    {
                        if (MachineTree.Nodes[i].Nodes[j].Checked)
                        {
                            if (MachineNo.Text == "")
                            {
                                MachineNo.Text = MachineTree.Nodes[i].Nodes[j].Text;
                                //PostProcessor = MachineTree.Nodes[i].Nodes[j].Cells[3].Tag.ToString();
                            }
                            else
                            {
                                MachineNo.Text = MachineNo.Text + "," + MachineTree.Nodes[i].Nodes[j].Text;
                                //PostProcessor = PostProcessor + "," + MachineTree.Nodes[i].Nodes[j].Cells[3].Tag.ToString();
                            }
                        }
                    }
                }
            }
            catch (System.Exception ex)
            {
            }
        }

        private bool GetMachineDataToMachineNo(NXOpen.CAM.NCGroup ncGroup)
        {
            try
            {
                //曾經選過的機台資訊寫到MachineNo
                try
                {
                    MachineNo.Text = ncGroup.GetStringAttribute("MachineNo");
                }
                catch (System.Exception ex)
                {
                    MachineNo.Text = "";
                }
                //foreach (NXOpen.CAM.NCGroup ncGroup in NCGroupAry)
                //{
                //    int type;
                //    int subtype;
                //    theUfSession.Obj.AskTypeAndSubtype(ncGroup.Tag, out type, out subtype);

                //    if (type != UFConstants.UF_machining_task_type)//此處比對是否為Program群組
                //    {
                //        continue;
                //    }
                //    if (CurrentNCGroup != ncGroup.Name)
                //    {
                //        continue;
                //    }
                    
                //    CurrentNC = ncGroup;
                //    try
                //    {
                //        MachineNo.Text = ncGroup.GetStringAttribute("MachineNo");
                //    }
                //    catch (System.Exception ex)
                //    {
                //        MachineNo.Text = "";
                //    }
                //}

                //打勾
                string[] splitMachineNo = MachineNo.Text.Split(',');
                if (splitMachineNo.Length == 0)
                {
                    for (int i = 0; i < MachineTree.Nodes.Count; i++)
                    {
                        for (int j = 0; j < MachineTree.Nodes[i].Nodes.Count; j++)
                        {
                            MachineTree.Nodes[i].Nodes[j].Checked = false;
                        }
                    }
                }
                else
                {
                    for (int i = 0; i < MachineTree.Nodes.Count; i++)
                    {
                        foreach (string ii in splitMachineNo)
                        {
                            if (MachineTree.Nodes[i].Text != Regex.Replace(ii, "[^A-Z]", ""))
                            {
                                continue;
                            }
                            for (int j = 0; j < MachineTree.Nodes[i].Nodes.Count; j++)
                            {
                                if (MachineTree.Nodes[i].Nodes[j].Text == ii & MachineTree.Nodes[i].Nodes[j].Checked == false)
                                {
                                    MachineTree.Nodes[i].Nodes[j].Checked = true;
                                }
                            }
                        }
                    }
                }
            }
            catch (System.Exception ex)
            {
                MessageBox.Show(ex.ToString());
                return false;
            }
            return true;
        }

        private bool SetAttributeToNcGroup(string MachineNoText)
        {
            try
            {
                if (MachineNo.Text != "")
                {
                    CurrentNC.SetAttribute("MachineNo", MachineNo.Text);
                }
                else
                {
                    return false;
                }
                if (Is_Local == null)
                {
                    return true;
                }
                ISession session = MyHibernateHelper.SessionFactory.OpenSession();
                string[] splitMachineNoText = MachineNoText.Split(',');
                foreach (string i in splitMachineNoText)
                {
                    Sys_MachineNo sysMachineNo = new Sys_MachineNo();
                    sysMachineNo = session.QueryOver<Sys_MachineNo>().Where(x => x.machineNo == i).SingleOrDefault<Sys_MachineNo>();
                    if (PostProcessor == "")
                    {
                        PostProcessor = sysMachineNo.postprocessor;
                    }
                    else
                    {
                        if (!PostProcessor.Contains(sysMachineNo.postprocessor))
                        {
                            PostProcessor = PostProcessor + "," + sysMachineNo.postprocessor;
                        }
                    }
                }
                CurrentNC.SetAttribute("PostProcessor", PostProcessor);
            }
            catch (System.Exception ex)
            {
                return false;
            }
            return true;
        }
        
        
        private void ExportExcel_Click(object sender, EventArgs e)
        {
            if (CurrentNCGroup == "")
            {
                MessageBox.Show("尚未選取程式！", "", MessageBoxButtons.OK, MessageBoxIcon.Exclamation);
                return;
            }

            
            //將機台寫入該群組物件內，以利下次開啟時，有舊機台顯示
            SetAttributeToNcGroup(MachineNo.Text);
            
            NXOpen.CAM.Preferences preferences1 = theSession.CAMSession.CreateCamPreferences();
            preferences1.ReplayRefreshBeforeEachPath = true;
            preferences1.Commit();
            preferences1.Destroy();
            
            #region 拍OperToolPath圖片
            string[] FolderImageAry = System.IO.Directory.GetFileSystemEntries(PhotoFolderPath, "*.jpg");
            OperationAry = displayPart.CAMSetup.CAMOperationCollection.ToArray();
            status = CreateOpImg(FolderImageAry);
            if (!status)
            {
                MessageBox.Show("拍照失敗");
                return;
            }
            #endregion

            
            #region 開始插入excel
            //檢查PC有無Excel在執行
            bool flag = false;
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
            //    MessageBox.Show("請先關閉所有Excel再重新執行輸出");
            //    return;
            //}

            ApplicationClass excelApp = new ApplicationClass();
            Workbook book = null;
            Worksheet sheet = null;
            Range oRng = null;
            try
            {
                //判斷是否已經指定路徑
                if (OutputPath == "")
                {
                    UI.GetUI().NXMessageBox.Show("注意", NXMessageBox.DialogType.Error, "ShopDoc輸出路徑錯誤，請聯繫開發工程師！");
                    return;
                }

                //ApplicationClass excelApp = new ApplicationClass();
                //Workbook book = null;
                //Worksheet sheet = null;
                //Range oRng = null;

                excelApp.Visible = false;
                //CaxLog.ShowListingWindow(ShopDocPath);
                book = excelApp.Workbooks.Open(ShopDocPath);

                sheet = (Excel.Worksheet)book.Sheets[1];
                
                foreach (KeyValuePair<string, OperData> kvp in DicNCData)
                {
                    if (CurrentNCGroup != kvp.Key) continue;

                    string[] splitOperName = kvp.Value.OperName.Split(',');

                    int needSheetNo = (splitOperName.Length / 8);
                    int needSheetNo_Reserve = (splitOperName.Length % 8);
                    if (needSheetNo_Reserve != 0)
                    {
                        needSheetNo++;
                    }
                    for (int i = 1; i < needSheetNo; i++)
                    {
                        sheet.Copy(System.Type.Missing, excelApp.Workbooks[1].Worksheets[1]);
                    }
                    break;
                }
                
                for (int i = 0; i < book.Worksheets.Count; i++)
                {
                    sheet = (Excel.Worksheet)book.Sheets[i + 1];
                    oRng = (Excel.Range)sheet.Cells[4, 1];
                    oRng.Value = oRng.Value.ToString().Replace("1/1", (i + 1).ToString() + "/" + (book.Worksheets.Count).ToString());
                    //Sheet的名稱不得超過31個，否則會錯
                    sheet.Name = string.Format(@"{0}({1})", CurrentNCGroup, (i + 1).ToString());
                    //if (PartNo.Length >= 28)
                    //{
                    //    sheet.Name = string.Format("({0})", (i + 1).ToString());
                    //}
                    //else
                    //{ 
                    //    sheet.Name = string.Format("{0}({1})", PartNo, (i + 1).ToString());
                    //}
                }
                
                #region 註解中，計算欄位寬高
                //sheet = (Excel.Worksheet)book.Sheets[1];
                //double abc = 0;
                //計算欄位的高
                double de = 0;
                for (int i = 1; i < 23; i++)
                {
                    oRng = (Excel.Range)sheet.Cells[i, 1];
                    de = de + Convert.ToDouble(oRng.Height);
                    //CaxLog.ShowListingWindow(oRng.Height.ToString());
                }
                //CaxLog.ShowListingWindow(de.ToString());

                //計算欄位的寬
                double abc = 0;
                for (int i = 1; i < 9; i++)
                {
                    oRng = (Excel.Range)sheet.Cells[23, i];
                    abc = abc + Convert.ToDouble(oRng.Width.ToString());
                }
                //CaxLog.ShowListingWindow(abc.ToString());
                #endregion

                //填表
                foreach (KeyValuePair<string, OperData> kvp in DicNCData)
                {
                    if (CurrentNCGroup != kvp.Key)
                    {
                        continue;
                    }
                    
                    Database.SplitData sSplitData = new Database.SplitData();
                    Database.GetSplitData(kvp.Value, out sSplitData);
                    string CuttingTimeStr = "";
                    string TotalCuttingTimeStr = "";
                    double ToTalCuttingTime = 0;

                    //取得所有加工時間
                    foreach (string i in sSplitData.OperCuttingTime)
                    {
                        ToTalCuttingTime = ToTalCuttingTime + Convert.ToDouble(i);
                    }
                    //CaxLog.ShowListingWindow(book.Worksheets.Count.ToString());
                    for (int j = 0; j < sSplitData.OperName.Length; j++)
                    {
                        RowColumn sRowColumn;
                        GetExcelRowColumn(j, out sRowColumn);
                        int currentSheet_Value = (j / 8);
                        //int currentSheet_Reserve = (j % 8);
                        if (currentSheet_Value == 0) sheet = (Excel.Worksheet)book.Sheets[1];
                        else sheet = (Excel.Worksheet)book.Sheets[currentSheet_Value + 1];

                        oRng = (Excel.Range)sheet.Cells;
                        oRng[sRowColumn.OperImgToolRow, sRowColumn.OperImgToolColumn] = sSplitData.OperToolNo[j] + "_" + sSplitData.OperName[j];
                        //oRng[sRowColumn.ToolNumberRow, sRowColumn.ToolNumberColumn] = sSplitData.OperToolNo[j];
                        oRng[sRowColumn.ToolNameRow, sRowColumn.ToolNameColumn] = sSplitData.OperToolID[j];
                        oRng[sRowColumn.OperNameRow, sRowColumn.OperNameColumn] = sSplitData.OperName[j];
                        oRng[sRowColumn.HolderRow, sRowColumn.HolderColumn] = sSplitData.OperHolderID[j];
                        oRng[sRowColumn.ToolFeedRow, sRowColumn.ToolFeedColumn] = "F：" + sSplitData.OperToolFeed[j];
                        oRng[sRowColumn.ToolSpeedRow, sRowColumn.ToolSpeedColumn] = sSplitData.OperToolSpeed[j];
                        oRng[sRowColumn.PartStockRow, sRowColumn.PartStockColumn] = sSplitData.OperPartStock[j] + "/" + sSplitData.OperPartFloorStock[j];
                        oRng[sRowColumn.CutterLifeRow, sRowColumn.CutterLifeColumn] = sSplitData.OperCutterLife[j];
                        oRng[sRowColumn.ExtensionRow, sRowColumn.ExtensionColumn] = sSplitData.OperExtension[j];

                        CuttingTimeStr = string.Format("{0}m {1}s", Math.Truncate((Convert.ToDouble(sSplitData.OperCuttingTime[j]) / 60)), (Convert.ToDouble(sSplitData.OperCuttingTime[j]) % 60));
                        oRng[sRowColumn.CuttingTimeRow, sRowColumn.CuttingTimeColumn] = CuttingTimeStr;

                        //料號
                        oRng[sRowColumn.PartNoRow, sRowColumn.PartNoColumn] = PartNo;
                        //循環時間
                        TotalCuttingTimeStr = string.Format("{0}m {1}s", Math.Truncate((ToTalCuttingTime / 60)), (ToTalCuttingTime % 60));
                        oRng[sRowColumn.TotalCuttingTimeRow, sRowColumn.TotalCuttingTimeColumn] = TotalCuttingTimeStr;
                        //機台型號
                        oRng[sRowColumn.MachineNoRow, sRowColumn.MachineNoColumn] = MachineNo.Text;
                        //設計
                        oRng[sRowColumn.DesignedRow, sRowColumn.DesignedColumn] = Designed.Text;
                        //審核
                        oRng[sRowColumn.ReviewedRow, sRowColumn.ReviewedColumn] = Reviewed.Text;
                        //批准
                        oRng[sRowColumn.ApprovedRow, sRowColumn.ApprovedColumn] = Approved.Text;
                        //品名
                        oRng[sRowColumn.PartDescRow, sRowColumn.PartDescColumn] = "OIS-" + CurrentNCGroup.Split('P')[1];

                        OperImgPosiSize sImgPosiSize = new OperImgPosiSize();
                        GetOperImgPosiAndSize(j, sheet, oRng, out sImgPosiSize);

                        //OperImg暫時使用版本
                        string OperImagePath = string.Format(@"{0}\{1}", PhotoFolderPath, sSplitData.OperName[j] + ".jpg");
                        
                        //發布使用版本
                        //string OperImagePath = string.Format(@"{0}\{1}", Local_Folder_CAM, splitOperName[j] + ".jpg");

                        sheet.Shapes.AddPicture(OperImagePath, Microsoft.Office.Core.MsoTriState.msoFalse,
                            Microsoft.Office.Core.MsoTriState.msoTrue, sImgPosiSize.OperPosiLeft,
                            sImgPosiSize.OperPosiTop, sImgPosiSize.OperImgWidth, sImgPosiSize.OperImgHeight);

                        //System.IO.File.Delete(OperImagePath);
                    }

                    //貼治具圖片
                    if (FixturePath.Text != "")
                    {
                        FixImgPosiSize sFixImgPosiSize = new FixImgPosiSize();
                        GetFixImgPosiAndSize(out sFixImgPosiSize);
                        for (int i = 0; i < book.Sheets.Count; i++)
                        {
                            sheet = (Excel.Worksheet)book.Sheets[i + 1];

                            sheet.Shapes.AddPicture(FixturePath.Text, Microsoft.Office.Core.MsoTriState.msoFalse,
                                Microsoft.Office.Core.MsoTriState.msoTrue, (float)abc,
                                (float)de, sFixImgPosiSize.FixImgWidth, sFixImgPosiSize.FixImgHeight);
                        }
                    }
                }
                
                #region 插入尺寸控制資訊(先整理刀號與泡泡)
                DicToolNoBallon = new Dictionary<string, List<BallonDimen>>();
                foreach (KeyValuePair<ControlDimen_Key, List<ControlDimen_Value>> kvp in DicControlDimen)
                {
                    if (kvp.Value.Count == 0) 
                        continue;
                    

                    foreach (ControlDimen_Value i in kvp.Value)
                    {
                        if (i.ControlBallon == "" || i.ControlTolRange == "" || i.DraftingRev == "" || i.TheoryTolRange == "")
                        {
                            continue;
                        }

                        List<BallonDimen> listBallonDimen = new List<BallonDimen>();
                        status = DicToolNoBallon.TryGetValue(kvp.Key.ToolNo, out listBallonDimen);
                        if (!status)
                        {
                            listBallonDimen = new List<BallonDimen>();
                            BallonDimen sBallonDimen = new BallonDimen();
                            sBallonDimen.Ballon = i.ControlBallon;
                            sBallonDimen.Dimen = i.ControlTolRange;
                            listBallonDimen.Add(sBallonDimen);
                            DicToolNoBallon.Add(kvp.Key.ToolNo, listBallonDimen);
                        }
                        else
                        {
                            BallonDimen sBallonDimen = new BallonDimen();
                            sBallonDimen.Ballon = i.ControlBallon;
                            sBallonDimen.Dimen = i.ControlTolRange;
                            listBallonDimen.Add(sBallonDimen);
                            DicToolNoBallon[kvp.Key.ToolNo] = listBallonDimen;
                        }
                    }
                }
                
                int count = -1;
                foreach (KeyValuePair<string, List<BallonDimen>> kvp in DicToolNoBallon)
                {
                    bool sameToolNo = false;
                    foreach (BallonDimen i in kvp.Value)
                    {
                        count++;
                        RowColumn sRowColumn;
                        GetExcelRowColumn(count, out sRowColumn);
                        int currentSheet_Value = (count / 8);
                        if (currentSheet_Value == 0) 
                            sheet = (Excel.Worksheet)book.Sheets[1];
                        else 
                            sheet = (Excel.Worksheet)book.Sheets[currentSheet_Value + 1];

                        oRng = (Excel.Range)sheet.Cells;

                        if (!sameToolNo)
                            oRng[sRowColumn.ControlToolNoRow, sRowColumn.ControlToolNoColumn] = kvp.Key;
                        
                        oRng[sRowColumn.ControlBallonRow, sRowColumn.ControlBallonColumn] = i.Ballon;
                        oRng[sRowColumn.ControlDimenRow, sRowColumn.ControlDimenColumn] = i.Dimen;
                        sameToolNo = true;
                    }

                    /*
                    count++;
                    RowColumn sRowColumn;
                    GetExcelRowColumn(count, out sRowColumn);
                    int currentSheet_Value = (count / 8);
                    if (currentSheet_Value == 0) sheet = (Excel.Worksheet)book.Sheets[1];
                    else sheet = (Excel.Worksheet)book.Sheets[currentSheet_Value + 1];

                    oRng = (Excel.Range)sheet.Cells;
                    oRng[sRowColumn.ControlToolNoRow, sRowColumn.ControlToolNoColumn] = kvp.Key;
                    oRng[sRowColumn.ControlBallonRow, sRowColumn.ControlBallonColumn] = kvp.Value;
                    */
                }
                #endregion


                //輸出XLSX
                //book.SaveAs(OutputPath, Excel.XlFileFormat.xlOpenXMLWorkbook, Type.Missing, Type.Missing, Type.Missing, Type.Missing,
                //    Excel.XlSaveAsAccessMode.xlNoChange, Type.Missing, Type.Missing, Type.Missing, Type.Missing, Type.Missing);
                //輸出XLS
                book.SaveAs(OutputPath, Excel.XlFileFormat.xlWorkbookNormal, Type.Missing, Type.Missing, Type.Missing, Type.Missing,
                    Excel.XlSaveAsAccessMode.xlNoChange, Type.Missing, Type.Missing, Type.Missing, Type.Missing, Type.Missing);
                book.Close(Type.Missing, Type.Missing, Type.Missing);
                excelApp.Quit();
            }
            catch (System.Exception ex)
            {
                MessageBox.Show(ex.ToString());
                if (File.Exists(OutputPath))
                {
                    File.Delete(OutputPath);
                }
                book.SaveAs(OutputPath, XlFileFormat.xlWorkbookNormal, Type.Missing, Type.Missing, Type.Missing,
                       Type.Missing, XlSaveAsAccessMode.xlNoChange, Type.Missing, Type.Missing, Type.Missing, Type.Missing, Type.Missing);
                book.Close(Type.Missing, Type.Missing, Type.Missing);
                excelApp.Quit();
                File.Delete(OutputPath);
                flag = true;
            }
            finally
            {
                Dispose(excelApp, book, sheet, oRng);
            }

            
            
            #endregion

            //表示輸出失敗
            if (flag)
            {
                MessageBox.Show("刀具路徑與清單輸出【失敗】！");
                this.Close();
                return;
            }

            #region 上傳數據至Database

            if (Is_Local != null & flag == false)
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
                    MessageBox.Show("資料庫中沒有此料號的紀錄，無法上傳");
                    return;
                }
                #endregion

                if (comPEMain == null)
                {
                    MessageBox.Show("資料庫中沒有此料號的紀錄，無法上傳\r\n刀具路徑與清單輸出【完成】！");
                    //MessageBox.Show("刀具路徑與清單輸出【完成】！");
                    this.Close();
                }

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
                    MessageBox.Show("資料庫中沒有此料號的紀錄，無法上傳");
                    return;
                }
                #endregion

                #region 比對資料庫TEMain是否有同筆數據
                IList<Com_TEMain> DBData_ComTEMain = session.QueryOver<Com_TEMain>().List<Com_TEMain>();

                //可優化成這樣
                //session.QueryOver<Com_TEMain>().Where(x => x.comPartOperation == comPartOperation).And(x => x.ncGroupName == CurrentNCGroup).SingleOrDefault();

                bool Is_Exist = false;
                Com_TEMain currentComTEMain = new Com_TEMain();
                foreach (Com_TEMain i in DBData_ComTEMain)
                {
                    if (i.comPartOperation == comPartOperation && i.ncGroupName == CurrentNCGroup)
                    {
                        Is_Exist = true;
                        currentComTEMain = i;
                        break;
                    }
                }
                #endregion

                #region 如果本次上傳的資料不存在於資料庫，則開始上傳資料；如果已存在資料庫，則詢問是否要更新尺寸
                bool Is_Update = true;
                if (Is_Exist)
                {
                    if (eTaskDialogResult.Yes == CaxPublic.ShowMsgYesNo("此CAM已存在上一筆資料，是否更新?"))
                    {
                        #region 刪除Com_ShocDoc資料表
                        IList<Com_ShopDoc> DB_ShopDoc = session.QueryOver<Com_ShopDoc>()
                                                 .Where(x => x.comTEMain.teSrNo == currentComTEMain.teSrNo).List<Com_ShopDoc>();
                        using (ITransaction trans = session.BeginTransaction())
                        {
                            foreach (Com_ShopDoc i in DB_ShopDoc)
                            {
                                session.Delete(i);
                            }
                            trans.Commit();
                        }
                        #endregion

                        #region 刪除Com_ControlDimen
                        IList<Com_ControlDimen> DB_ControlDimen = session.QueryOver<Com_ControlDimen>()
                                                 .Where(x => x.comTEMain.teSrNo == currentComTEMain.teSrNo).List<Com_ControlDimen>();
                        using (ITransaction trans = session.BeginTransaction())
                        {
                            foreach (Com_ControlDimen i in DB_ControlDimen)
                            {
                                session.Delete(i);
                            }
                            trans.Commit();
                        }
                        #endregion

                        #region 刪除Com_TEMain
                        Com_TEMain DB_ComTEMain = session.QueryOver<Com_TEMain>()
                                                 .Where(x => x.comPartOperation == currentComTEMain.comPartOperation)
                                                 .And(x => x.ncGroupName == CurrentNCGroup)
                                                 .SingleOrDefault<Com_TEMain>();
                        using (ITransaction trans = session.BeginTransaction())
                        {
                            session.Delete(DB_ComTEMain);
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
                    #region 重新插入所有程式

                    Com_TEMain cCom_TEMain = new Com_TEMain();
                    cCom_TEMain.comPartOperation = comPartOperation;
                    cCom_TEMain.fixtureImgPath = string.Format(@"{0}\{1}_Image\{2}", cMETE_Download_Upload_Path.Server_Folder_CAM, CurrentNCGroup, FixtureNameStr);
                    cCom_TEMain.sysTEExcel = session.QueryOver<Sys_TEExcel>().Where(x => x.teExcelType == "ShopDoc").SingleOrDefault<Sys_TEExcel>();
                    cCom_TEMain.createDate = DateTime.Now.ToString();
                    cCom_TEMain.machineNo = MachineNo.Text;
                    cCom_TEMain.designed = Designed.Text;
                    cCom_TEMain.reviewed = Reviewed.Text;
                    cCom_TEMain.approved = Approved.Text;

                    //會在ShopDoc就上傳資料是因為可能有兩個以上的群組名稱，如果在上傳的時候才傳資料，則會因為抓不到程式而沒資料填寫
                    OperData sOperData = new OperData();
                    foreach (KeyValuePair<string, OperData> kvp in DicNCData)
                    {
                        if (CurrentNCGroup != kvp.Key)
                        {
                            continue;
                        }
                        cCom_TEMain.ncGroupName = CurrentNCGroup;
                        sOperData = kvp.Value;
                    }

                    Database.SplitData sSplitData = new Database.SplitData();
                    Database.GetSplitData(sOperData, out sSplitData);

                    //取得所有加工時間
                    double ToTalCuttingTime = 0;
                    foreach (string i in sSplitData.OperCuttingTime)
                    {
                        ToTalCuttingTime = ToTalCuttingTime + Convert.ToDouble(i);
                    }
                    //循環時間
                    cCom_TEMain.totalCuttingTime = string.Format("{0}m {1}s", Math.Truncate((ToTalCuttingTime / 60)), (ToTalCuttingTime % 60));

                    Database.comShopDoc = new List<Com_ShopDoc>();

                    for (int i = 0; i < sSplitData.OperName.Length; i++)
                    {
                        Com_ShopDoc cCom_ShopDoc = new Com_ShopDoc();
                        cCom_ShopDoc.comTEMain = cCom_TEMain;
                        cCom_ShopDoc.operationName = sSplitData.OperName[i];
                        cCom_ShopDoc.toolID = sSplitData.OperToolID[i];
                        cCom_ShopDoc.toolNo = sSplitData.OperToolNo[i];
                        cCom_ShopDoc.holderID = sSplitData.OperHolderID[i];
                        cCom_ShopDoc.opImagePath = string.Format(@"{0}\{1}_Image\{2}.jpg", cMETE_Download_Upload_Path.Server_Folder_CAM, CurrentNCGroup, sSplitData.OperName[i]);
                        cCom_ShopDoc.machiningtime = string.Format("{0}m {1}s", Math.Truncate((Convert.ToDouble(sSplitData.OperCuttingTime[i]) / 60))
                                                                                            , (Convert.ToDouble(sSplitData.OperCuttingTime[i]) % 60));
                        cCom_ShopDoc.feed = sSplitData.OperToolFeed[i];
                        cCom_ShopDoc.speed = sSplitData.OperToolSpeed[i];
                        cCom_ShopDoc.partStock = sSplitData.OperPartStock[i] + "/" + sSplitData.OperPartFloorStock[i];
                        cCom_ShopDoc.cutterLife = sSplitData.OperCutterLife[i];
                        cCom_ShopDoc.extension = sSplitData.OperExtension[i];

                        Database.comShopDoc.Add(cCom_ShopDoc);
                    }
                    cCom_TEMain.comShopDoc = Database.comShopDoc;

                    //加入ControlDimen資訊
                    Database.comControlDimen = new List<Com_ControlDimen>();
                    foreach (KeyValuePair<string, List<BallonDimen>> kvp in DicToolNoBallon)
                    {
                        foreach (BallonDimen i in kvp.Value)
                        {
                            Com_ControlDimen cCom_ControlDimen = new Com_ControlDimen();
                            cCom_ControlDimen.comTEMain = cCom_TEMain;
                            cCom_ControlDimen.toolNo = kvp.Key;
                            cCom_ControlDimen.controlBallon = Convert.ToInt32(i.Ballon);
                            cCom_ControlDimen.controlDimen = i.Dimen;
                            Database.comControlDimen.Add(cCom_ControlDimen);
                        }
                    }
                    cCom_TEMain.comControlDimen = Database.comControlDimen;


                    using (ITransaction trans = session.BeginTransaction())
                    {
                        session.Save(cCom_TEMain);
                        trans.Commit();
                    }
                    #endregion
                }
                #endregion
            }
            
            #endregion

            MessageBox.Show("刀具路徑與清單輸出【完成】！");
            this.Close();
        }

        public static void Dispose(ApplicationClass excelApp, Workbook workBook, Worksheet workSheet, Range workRange)
        {
            System.Runtime.InteropServices.Marshal.ReleaseComObject(workRange);
            System.Runtime.InteropServices.Marshal.ReleaseComObject(workSheet);
            System.Runtime.InteropServices.Marshal.ReleaseComObject(workBook);
            System.Runtime.InteropServices.Marshal.ReleaseComObject(excelApp);
        }

        private bool CreateOpImg(string[] FolderImageAry)
        {
            try
            {
                foreach (NXOpen.CAM.NCGroup ncGroup in NCGroupAry)
                {
                    if (CurrentNCGroup != ncGroup.Name)
                    {
                        continue;
                    }
                    for (int i = 0; i < OperationAry.Length; i++)
                    {
                        //取得父層的群組(回傳：NCGroup XXXX)
                        string NCProgramTag = OperationAry[i].GetParent(CAMSetup.View.ProgramOrder).ToString();
                        NCProgramTag = Regex.Replace(NCProgramTag, "[^0-9]", "");
                        NXOpen.CAM.CAMObject[] tempObjToCreateImg = new CAMObject[1];
                        string ImagePath = "";
                        if (NCProgramTag == ncGroup.Tag.ToString())
                        {
                            //判斷是否已手動拍攝，如拍攝過就不再拍攝
                            bool checkStatus = false;
                            foreach (string single in FolderImageAry)
                            {
                                if (Path.GetFileNameWithoutExtension(single) == CaxOper.AskOperNameFromTag(OperationAry[i].Tag))
                                {
                                    checkStatus = true;
                                    break;
                                }
                            }

                            if (!checkStatus)
                            {
                                tempObjToCreateImg[0] = (NXOpen.CAM.CAMObject)OperationAry[i];
                                workPart.ModelingViews.WorkView.Orient(NXOpen.View.Canned.Isometric, NXOpen.View.ScaleAdjustment.Fit);
                                workPart.CAMSetup.ReplayToolPath(tempObjToCreateImg);

                                ImagePath = string.Format(@"{0}\{1}", PhotoFolderPath, OperationAry[i].Name);
                                theUfSession.Disp.CreateImage(ImagePath, UFDisp.ImageFormat.Jpeg, UFDisp.BackgroundColor.White);
                                //ImagePath = string.Format(@"{0}\{1}", PhotoFolderPath, panel.GetCell(CurrentRowIndex, 0).Value.ToString());
                                //theUfSession.Disp.CreateImage(ImagePath, UFDisp.ImageFormat.Jpeg, UFDisp.BackgroundColor.White);
                                //ImagePath = string.Format(@"{0}\{1}", PhotoFolderPath, panel.GetCell(CurrentRowIndex, 1).Value.ToString());
                                //theUfSession.Disp.CreateImage(ImagePath, UFDisp.ImageFormat.Jpeg, UFDisp.BackgroundColor.White);
                            }
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

        private void CloseDlg_Click(object sender, EventArgs e)
        {
            NXOpen.CAM.Preferences preferences1 = theSession.CAMSession.CreateCamPreferences();
            preferences1.ReplayRefreshBeforeEachPath = true;
            preferences1.Commit();
            preferences1.Destroy();

            this.Close();
        }

        private void GetExcelRowColumn(int i,out RowColumn sRowColumn)
        {
            sRowColumn = new RowColumn();
            //料號
            sRowColumn.PartNoRow = 52;
            sRowColumn.PartNoColumn = 11;
            //循環時間
            sRowColumn.TotalCuttingTimeRow = 51;
            sRowColumn.TotalCuttingTimeColumn = 2;
            //機台型號
            sRowColumn.MachineNoRow = 52;
            sRowColumn.MachineNoColumn = 4;
            //設計
            sRowColumn.DesignedRow = 52;
            sRowColumn.DesignedColumn = 6;
            //審核
            sRowColumn.ReviewedRow = 52;
            sRowColumn.ReviewedColumn = 7;
            //批准
            sRowColumn.ApprovedRow = 52;
            sRowColumn.ApprovedColumn = 8;
            //品名
            sRowColumn.PartDescRow = 51;
            sRowColumn.PartDescColumn = 11;

            int currentNo = (i % 8);

            if (currentNo == 0)
            {
                sRowColumn.PartStockRow = 27;
                sRowColumn.PartStockColumn = 4;

                sRowColumn.OperImgToolRow = 5;
                sRowColumn.OperImgToolColumn = 1;

                //ShopDoc_C版改成刀號拿掉只放刀具名稱(名稱已包含刀號)
                sRowColumn.ToolNumberRow = 23;
                sRowColumn.ToolNumberColumn = 2;
                sRowColumn.ToolNameRow = 23;
                sRowColumn.ToolNameColumn = 2;

                sRowColumn.OperNameRow = 24;
                sRowColumn.OperNameColumn = 2;

                sRowColumn.HolderRow = 25;
                sRowColumn.HolderColumn = 2;

                sRowColumn.CutterLifeRow = 26;
                sRowColumn.CutterLifeColumn = 2;

                sRowColumn.ExtensionRow = 26;
                sRowColumn.ExtensionColumn = 4;

                sRowColumn.ToolFeedRow = 27;
                sRowColumn.ToolFeedColumn = 2;

                sRowColumn.ToolSpeedRow = 27;
                sRowColumn.ToolSpeedColumn = 3;

                sRowColumn.CuttingTimeRow = 28;
                sRowColumn.CuttingTimeColumn = 2;

                sRowColumn.ControlToolNoRow = 39;
                sRowColumn.ControlToolNoColumn = 9;

                sRowColumn.ControlBallonRow = 39;
                sRowColumn.ControlBallonColumn = 10;

                sRowColumn.ControlDimenRow = 39;
                sRowColumn.ControlDimenColumn = 11;
            }
            else if (currentNo == 1)
            {
                sRowColumn.PartStockRow = 33;
                sRowColumn.PartStockColumn = 4;

                sRowColumn.OperImgToolRow = 5;
                sRowColumn.OperImgToolColumn = 4;

                //ShopDoc_C版改成刀號拿掉只放刀具名稱(名稱已包含刀號)
                sRowColumn.ToolNumberRow = 29;
                sRowColumn.ToolNumberColumn = 2;
                sRowColumn.ToolNameRow = 29;
                sRowColumn.ToolNameColumn = 2;

                sRowColumn.OperNameRow = 30;
                sRowColumn.OperNameColumn = 2;

                sRowColumn.HolderRow = 31;
                sRowColumn.HolderColumn = 2;

                sRowColumn.CutterLifeRow = 32;
                sRowColumn.CutterLifeColumn = 2;

                sRowColumn.ExtensionRow = 32;
                sRowColumn.ExtensionColumn = 4;

                sRowColumn.ToolFeedRow = 33;
                sRowColumn.ToolFeedColumn = 2;

                sRowColumn.ToolSpeedRow = 33;
                sRowColumn.ToolSpeedColumn = 3;

                sRowColumn.CuttingTimeRow = 34;
                sRowColumn.CuttingTimeColumn = 2;

                sRowColumn.ControlToolNoRow = 40;
                sRowColumn.ControlToolNoColumn = 9;

                sRowColumn.ControlBallonRow = 40;
                sRowColumn.ControlBallonColumn = 10;

                sRowColumn.ControlDimenRow = 40;
                sRowColumn.ControlDimenColumn = 11;
            }
            else if (currentNo == 2)
            {
                sRowColumn.PartStockRow = 39;
                sRowColumn.PartStockColumn = 4;

                sRowColumn.OperImgToolRow = 5;
                sRowColumn.OperImgToolColumn = 7;

                //ShopDoc_C版改成刀號拿掉只放刀具名稱(名稱已包含刀號)
                sRowColumn.ToolNumberRow = 35;
                sRowColumn.ToolNumberColumn = 2;
                sRowColumn.ToolNameRow = 35;
                sRowColumn.ToolNameColumn = 2;

                sRowColumn.OperNameRow = 36;
                sRowColumn.OperNameColumn = 2;

                sRowColumn.HolderRow = 37;
                sRowColumn.HolderColumn = 2;

                sRowColumn.CutterLifeRow = 38;
                sRowColumn.CutterLifeColumn = 2;

                sRowColumn.ExtensionRow = 38;
                sRowColumn.ExtensionColumn = 4;

                sRowColumn.ToolFeedRow = 39;
                sRowColumn.ToolFeedColumn = 2;

                sRowColumn.ToolSpeedRow = 39;
                sRowColumn.ToolSpeedColumn = 3;

                sRowColumn.CuttingTimeRow = 40;
                sRowColumn.CuttingTimeColumn = 2;

                sRowColumn.ControlToolNoRow = 41;
                sRowColumn.ControlToolNoColumn = 9;

                sRowColumn.ControlBallonRow = 41;
                sRowColumn.ControlBallonColumn = 10;

                sRowColumn.ControlDimenRow = 41;
                sRowColumn.ControlDimenColumn = 11;
            }
            else if (currentNo == 3)
            {
                sRowColumn.PartStockRow = 45;
                sRowColumn.PartStockColumn = 4;

                sRowColumn.OperImgToolRow = 5;
                sRowColumn.OperImgToolColumn = 10;

                //ShopDoc_C版改成刀號拿掉只放刀具名稱(名稱已包含刀號)
                sRowColumn.ToolNumberRow = 41;
                sRowColumn.ToolNumberColumn = 2;
                sRowColumn.ToolNameRow = 41;
                sRowColumn.ToolNameColumn = 2;

                sRowColumn.OperNameRow = 42;
                sRowColumn.OperNameColumn = 2;

                sRowColumn.HolderRow = 43;
                sRowColumn.HolderColumn = 2;

                sRowColumn.CutterLifeRow = 44;
                sRowColumn.CutterLifeColumn = 2;

                sRowColumn.ExtensionRow = 44;
                sRowColumn.ExtensionColumn = 4;

                sRowColumn.ToolFeedRow = 45;
                sRowColumn.ToolFeedColumn = 2;

                sRowColumn.ToolSpeedRow = 45;
                sRowColumn.ToolSpeedColumn = 3;

                sRowColumn.CuttingTimeRow = 46;
                sRowColumn.CuttingTimeColumn = 2;

                sRowColumn.ControlToolNoRow = 42;
                sRowColumn.ControlToolNoColumn = 9;

                sRowColumn.ControlBallonRow = 42;
                sRowColumn.ControlBallonColumn = 10;

                sRowColumn.ControlDimenRow = 42;
                sRowColumn.ControlDimenColumn = 11;
            }
            else if (currentNo == 4)
            {
                sRowColumn.PartStockRow = 27;
                sRowColumn.PartStockColumn = 8;

                sRowColumn.OperImgToolRow = 13;
                sRowColumn.OperImgToolColumn = 1;

                //ShopDoc_C版改成刀號拿掉只放刀具名稱(名稱已包含刀號)
                sRowColumn.ToolNumberRow = 23;
                sRowColumn.ToolNumberColumn = 6;
                sRowColumn.ToolNameRow = 23;
                sRowColumn.ToolNameColumn = 6;

                sRowColumn.OperNameRow = 24;
                sRowColumn.OperNameColumn = 6;

                sRowColumn.HolderRow = 25;
                sRowColumn.HolderColumn = 6;

                sRowColumn.CutterLifeRow = 26;
                sRowColumn.CutterLifeColumn = 6;

                sRowColumn.ExtensionRow = 26;
                sRowColumn.ExtensionColumn = 8;

                sRowColumn.ToolFeedRow = 27;
                sRowColumn.ToolFeedColumn = 6;

                sRowColumn.ToolSpeedRow = 27;
                sRowColumn.ToolSpeedColumn = 7;

                sRowColumn.CuttingTimeRow = 28;
                sRowColumn.CuttingTimeColumn = 6;

                sRowColumn.ControlToolNoRow = 43;
                sRowColumn.ControlToolNoColumn = 9;

                sRowColumn.ControlBallonRow = 43;
                sRowColumn.ControlBallonColumn = 10;

                sRowColumn.ControlDimenRow = 43;
                sRowColumn.ControlDimenColumn = 11;
            }
            else if (currentNo == 5)
            {
                sRowColumn.PartStockRow = 33;
                sRowColumn.PartStockColumn = 8;

                sRowColumn.OperImgToolRow = 13;
                sRowColumn.OperImgToolColumn = 4;

                //ShopDoc_C版改成刀號拿掉只放刀具名稱(名稱已包含刀號)
                sRowColumn.ToolNumberRow = 29;
                sRowColumn.ToolNumberColumn = 6;
                sRowColumn.ToolNameRow = 29;
                sRowColumn.ToolNameColumn = 6;

                sRowColumn.OperNameRow = 30;
                sRowColumn.OperNameColumn = 6;

                sRowColumn.HolderRow = 31;
                sRowColumn.HolderColumn = 6;

                sRowColumn.CutterLifeRow = 32;
                sRowColumn.CutterLifeColumn = 6;

                sRowColumn.ExtensionRow = 32;
                sRowColumn.ExtensionColumn = 8;

                sRowColumn.ToolFeedRow = 33;
                sRowColumn.ToolFeedColumn = 6;

                sRowColumn.ToolSpeedRow = 33;
                sRowColumn.ToolSpeedColumn = 7;

                sRowColumn.CuttingTimeRow = 34;
                sRowColumn.CuttingTimeColumn = 6;

                sRowColumn.ControlToolNoRow = 44;
                sRowColumn.ControlToolNoColumn = 9;

                sRowColumn.ControlBallonRow = 44;
                sRowColumn.ControlBallonColumn = 10;

                sRowColumn.ControlDimenRow = 44;
                sRowColumn.ControlDimenColumn = 11;
            }
            else if (currentNo == 6)
            {
                sRowColumn.PartStockRow = 39;
                sRowColumn.PartStockColumn = 8;

                sRowColumn.OperImgToolRow = 13;
                sRowColumn.OperImgToolColumn = 7;

                //ShopDoc_C版改成刀號拿掉只放刀具名稱(名稱已包含刀號)
                sRowColumn.ToolNumberRow = 35;
                sRowColumn.ToolNumberColumn = 6;
                sRowColumn.ToolNameRow = 35;
                sRowColumn.ToolNameColumn = 6;

                sRowColumn.OperNameRow = 36;
                sRowColumn.OperNameColumn = 6;

                sRowColumn.HolderRow = 37;
                sRowColumn.HolderColumn = 6;

                sRowColumn.CutterLifeRow = 38;
                sRowColumn.CutterLifeColumn = 6;

                sRowColumn.ExtensionRow = 38;
                sRowColumn.ExtensionColumn = 8;

                sRowColumn.ToolFeedRow = 39;
                sRowColumn.ToolFeedColumn = 6;

                sRowColumn.ToolSpeedRow = 39;
                sRowColumn.ToolSpeedColumn = 7;

                sRowColumn.CuttingTimeRow = 40;
                sRowColumn.CuttingTimeColumn = 6;

                sRowColumn.ControlToolNoRow = 45;
                sRowColumn.ControlToolNoColumn = 9;

                sRowColumn.ControlBallonRow = 45;
                sRowColumn.ControlBallonColumn = 10;

                sRowColumn.ControlDimenRow = 45;
                sRowColumn.ControlDimenColumn = 11;
            }
            else if (currentNo == 7)
            {
                sRowColumn.PartStockRow = 45;
                sRowColumn.PartStockColumn = 8;

                sRowColumn.OperImgToolRow = 13;
                sRowColumn.OperImgToolColumn = 10;

                //ShopDoc_C版改成刀號拿掉只放刀具名稱(名稱已包含刀號)
                sRowColumn.ToolNumberRow = 41;
                sRowColumn.ToolNumberColumn = 6;
                sRowColumn.ToolNameRow = 41;
                sRowColumn.ToolNameColumn = 6;

                sRowColumn.OperNameRow = 42;
                sRowColumn.OperNameColumn = 6;

                sRowColumn.HolderRow = 43;
                sRowColumn.HolderColumn = 6;

                sRowColumn.CutterLifeRow = 44;
                sRowColumn.CutterLifeColumn = 6;

                sRowColumn.ExtensionRow = 44;
                sRowColumn.ExtensionColumn = 8;

                sRowColumn.ToolFeedRow = 45;
                sRowColumn.ToolFeedColumn = 6;

                sRowColumn.ToolSpeedRow = 45;
                sRowColumn.ToolSpeedColumn = 7;

                sRowColumn.CuttingTimeRow = 46;
                sRowColumn.CuttingTimeColumn = 6;

                sRowColumn.ControlToolNoRow = 46;
                sRowColumn.ControlToolNoColumn = 9;

                sRowColumn.ControlBallonRow = 46;
                sRowColumn.ControlBallonColumn = 10;

                sRowColumn.ControlDimenRow = 46;
                sRowColumn.ControlDimenColumn = 11;
            }
        }

        private void GetOperImgPosiAndSize(int i, Excel.Worksheet sheet, Excel.Range oRng, out OperImgPosiSize sOperImgPosiSize)
        {
            sOperImgPosiSize = new OperImgPosiSize();
            int currentNo = (i % 8);

            if (currentNo == 0)
            {
                sOperImgPosiSize.OperPosiLeft = 5;
                sOperImgPosiSize.OperPosiTop = 118;
                sOperImgPosiSize.OperImgWidth = 160;
                sOperImgPosiSize.OperImgHeight = 115;
            }
            else if (currentNo == 1)
            {
                sOperImgPosiSize.OperPosiLeft = 185;
                sOperImgPosiSize.OperPosiTop = 118;
                sOperImgPosiSize.OperImgWidth = 160;
                sOperImgPosiSize.OperImgHeight = 115;
            }
            else if (currentNo == 2)
            {
                sOperImgPosiSize.OperPosiLeft = 365;
                sOperImgPosiSize.OperPosiTop = 118;
                sOperImgPosiSize.OperImgWidth = 160;
                sOperImgPosiSize.OperImgHeight = 115;
            }
            else if (currentNo == 3)
            {
                sOperImgPosiSize.OperPosiLeft = 540;
                sOperImgPosiSize.OperPosiTop = 118;
                sOperImgPosiSize.OperImgWidth = 160;
                sOperImgPosiSize.OperImgHeight = 115;
            }
            else if (currentNo == 4)
            {
                sOperImgPosiSize.OperPosiLeft = 10;
                sOperImgPosiSize.OperPosiTop = 265;
                sOperImgPosiSize.OperImgWidth = 160;
                sOperImgPosiSize.OperImgHeight = 115;
            }
            else if (currentNo == 5)
            {
                sOperImgPosiSize.OperPosiLeft = 190;
                sOperImgPosiSize.OperPosiTop = 265;
                sOperImgPosiSize.OperImgWidth = 160;
                sOperImgPosiSize.OperImgHeight = 115;
            }
            else if (currentNo == 6)
            {
                sOperImgPosiSize.OperPosiLeft = 370;
                sOperImgPosiSize.OperPosiTop = 265;
                sOperImgPosiSize.OperImgWidth = 160;
                sOperImgPosiSize.OperImgHeight = 115;
            }
            else if (currentNo == 7)
            {
                sOperImgPosiSize.OperPosiLeft = 545;
                sOperImgPosiSize.OperPosiTop = 265;
                sOperImgPosiSize.OperImgWidth = 160;
                sOperImgPosiSize.OperImgHeight = 115;
            }
        }

        private void GetFixImgPosiAndSize(out FixImgPosiSize sFixImgPosiSize)
        {
            sFixImgPosiSize = new FixImgPosiSize();
            sFixImgPosiSize.FixPosiLeft = 485;
            sFixImgPosiSize.FixPosiTop = 423;
            sFixImgPosiSize.FixImgWidth = 225;
            sFixImgPosiSize.FixImgHeight = 198;
        }

        public class SetView : GridButtonXEditControl
        {
            //public static Matrix3x3 VIEW_MATRIX;
            //public static double VIEW_SCALE;

            public SetView()
            {
                try
                {
                    Click += SetViewClick;
                }
                catch (System.Exception ex)
                {
                }
            }
            public void SetViewClick(object sender, EventArgs e)
            {
                SetView cSetView = (SetView)sender;
                CurrentRowIndex = cSetView.EditorCell.RowIndex;
                if (Is_Click_Rename)
                {
                    CurrentSelOperName = panel.GetCell(CurrentRowIndex, 1).Value.ToString();
                }
                else
                {
                    CurrentSelOperName = panel.GetCell(CurrentRowIndex, 0).Value.ToString();
                }
                //SelectedElementCollection a = panel.GetSelectedElements();
                //ListSelOper = new List<string>();
                //foreach (GridRow item in a)
                //{
                //    ListSelOper.Add(item.Cells[0].Value.ToString());
                //    //CaxLog.ShowListingWindow(item.Cells[0].Value.ToString());//可以看出debug中item的值會顯示GridRow的型態
                //}
                NXOpen.CAM.Preferences preferences1 = theSession.CAMSession.CreateCamPreferences();
                preferences1.ReplayRefreshBeforeEachPath = true;
                preferences1.Commit();
                preferences1.Destroy();

                foreach (NXOpen.CAM.NCGroup ncGroup in NCGroupAry)
                {
                    if (CurrentNCGroup != ncGroup.Name)
                    {
                        continue;
                    }

                    for (int i = 0; i < OperationAry.Length; i++)
                    {
                        //取得父層的群組(回傳：NCGroup XXXX)
                        string NCProgramTag = OperationAry[i].GetParent(CAMSetup.View.ProgramOrder).ToString();
                        NCProgramTag = Regex.Replace(NCProgramTag, "[^0-9]", "");
                        if (NCProgramTag != ncGroup.Tag.ToString())
                        {
                            continue;
                        }

                        NXOpen.CAM.CAMObject[] tempObjToCreateImg = new CAMObject[1];
                        string ImagePath = "";
                        if (CurrentSelOperName == CaxOper.AskOperNameFromTag(OperationAry[i].Tag))
                        {
                            //暫時使用版本
                            tempObjToCreateImg[0] = (NXOpen.CAM.CAMObject)OperationAry[i];
                            workPart.CAMSetup.ReplayToolPath(tempObjToCreateImg);

                            //ImagePath = string.Format(@"{0}\{1}", PhotoFolderPath, OperationAry[i].Name);
                            ImagePath = string.Format(@"{0}\{1}", PhotoFolderPath, panel.GetCell(CurrentRowIndex, 0).Value.ToString());
                            theUfSession.Disp.CreateImage(ImagePath, UFDisp.ImageFormat.Jpeg, UFDisp.BackgroundColor.White);
                            ImagePath = string.Format(@"{0}\{1}", PhotoFolderPath, panel.GetCell(CurrentRowIndex, 1).Value.ToString());
                            theUfSession.Disp.CreateImage(ImagePath, UFDisp.ImageFormat.Jpeg, UFDisp.BackgroundColor.White);
                            panel.GetCell(CurrentRowIndex, 9).Value = "已拍照";
                        }

                        /*
                        NXOpen.CAM.CAMObject[] tempObjToCreateImg = new CAMObject[1];
                        string ImagePath = "";
                        if (CurrentSelOperName == CaxOper.AskOperNameFromTag(OperationAry[i].Tag))
                        {
                            //暫時使用版本
                            tempObjToCreateImg[0] = (NXOpen.CAM.CAMObject)OperationAry[i];
                            workPart.CAMSetup.ReplayToolPath(tempObjToCreateImg);
                            ImagePath = string.Format(@"{0}\{1}", Path.GetDirectoryName(displayPart.FullPath), OperationAry[i].Name);
                            theUfSession.Disp.CreateImage(ImagePath, UFDisp.ImageFormat.Jpeg, UFDisp.BackgroundColor.White);

                            //------發布使用版本
                            tempObjToCreateImg[0] = (NXOpen.CAM.CAMObject)OperationAry[i];
                            workPart.CAMSetup.ReplayToolPath(tempObjToCreateImg);
                            ImagePath = string.Format(@"{0}\{1}", Local_Folder_CAM, OperationAry[i].Name);
                            theUfSession.Disp.CreateImage(ImagePath, UFDisp.ImageFormat.Jpeg, UFDisp.BackgroundColor.White);
                            
                        }
                        */
                    }
                   
                }


                //VIEW_MATRIX = workPart.ModelingViews.WorkView.Matrix;
                //VIEW_SCALE = workPart.ModelingViews.WorkView.Scale;
                

                //string ImagePath = string.Format(@"{0}\{1}", Local_Folder_CAM, CurrentOperName + ".jpg");
                //theUfSession.Disp.CreateImage(ImagePath, UFDisp.ImageFormat.Jpeg, UFDisp.BackgroundColor.White);

            }
        }

        public void superGridProg_RowClick(object sender, GridRowClickEventArgs e)
        {
            //取得點選的RowIndex
            CurrentRowIndex = e.GridRow.Index;
            CurrentSelOperName = panel.GetCell(CurrentRowIndex, 0).Value.ToString();
            SelectedElementCollection selRow = panel.GetSelectedElements();
            ListSelOper = new List<string>();
            foreach (GridRow item in selRow)
            {
                //判斷是否已按更名，已按->存新的名字；未按->存舊的名字
                if (Is_Click_Rename)
                {
                    ListSelOper.Add(item.Cells[1].Value.ToString());
                }
                else
                {
                    ListSelOper.Add(item.Cells[0].Value.ToString());
                }
                //當只有選擇一條程式時，記錄起來，以備管制尺寸時可以使用
                if (selRow.Count == 1)
                {
                    singleRow = item;
                    //singleOperation = GetSingleOperation(ListSelOper[0]);
                }
                //CaxLog.ShowListingWindow(item.Cells[0].Value.ToString());//可以看出debug中item的值會顯示GridRow的型態
            }

            NXOpen.CAM.Preferences preferences1 = theSession.CAMSession.CreateCamPreferences();
            if (ListSelOper.Count == 1)
            {
                preferences1.ReplayRefreshBeforeEachPath = true;
                preferences1.Commit();
                preferences1.Destroy();
                GroupSaveView.Enabled = false;
                panel.Columns["拍照"].ReadOnly = false;
            }
            else if (ListSelOper.Count > 1)
            {
                preferences1.ReplayRefreshBeforeEachPath = false;
                preferences1.Commit();
                preferences1.Destroy();
                GroupSaveView.Enabled = true;
                panel.Columns["拍照"].ReadOnly = true;
            }

            //UG顯示刀具路徑
            DisplayOpPath(ListSelOper);

            
        }

        public bool DisplayOpPath(List<string> ListSelOper)
        {
            try
            {
                for (int i = 0; i < OperationAry.Length; i++)
                {
                    NXOpen.CAM.CAMObject[] tempObjToCreateImg = new CAMObject[1];
                    foreach (string singleOper in ListSelOper)
                    {
                        if (singleOper == CaxOper.AskOperNameFromTag(OperationAry[i].Tag))
                        {
                            tempObjToCreateImg[0] = (NXOpen.CAM.CAMObject)OperationAry[i];
                            workPart.CAMSetup.ReplayToolPath(tempObjToCreateImg);
                        }
                    }
                }
                /*
                foreach (NXOpen.CAM.NCGroup ncGroup in NCGroupAry)
                {
                    if (CurrentNCGroup != ncGroup.Name)
                    {
                        continue;
                    }
                    for (int i = 0; i < OperationAry.Length; i++)
                    {
                        //取得父層的群組(回傳：NCGroup XXXX)
                        string NCProgramTag = OperationAry[i].GetParent(CAMSetup.View.ProgramOrder).ToString();
                        NCProgramTag = Regex.Replace(NCProgramTag, "[^0-9]", "");
                        NXOpen.CAM.CAMObject[] tempObjToCreateImg = new CAMObject[1];
                        foreach (string singleOper in ListSelOper)
                        {
                            if (NCProgramTag == ncGroup.Tag.ToString() && singleOper == CaxOper.AskOperNameFromTag(OperationAry[i].Tag))
                            {
                                tempObjToCreateImg[0] = (NXOpen.CAM.CAMObject)OperationAry[i];
                                workPart.CAMSetup.ReplayToolPath(tempObjToCreateImg);
                            }
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

        private NXOpen.CAM.Operation GetSingleOperation(string OpName)
        {
            NXOpen.CAM.Operation singleOp = null;
            try
            {
                for (int i = 0; i < OperationAry.Length; i++)
                {
                    if (OpName == CaxOper.AskOperNameFromTag(OperationAry[i].Tag))
                    {
                        singleOp = OperationAry[i];
                        break;
                    }
                }
                return singleOp;
            }
            catch (System.Exception ex)
            {
                return singleOp = null;
            }
        }

        private void GroupSaveView_Click(object sender, EventArgs e)
        {
            NXOpen.CAM.Preferences preferences1 = theSession.CAMSession.CreateCamPreferences();
            preferences1.ReplayRefreshBeforeEachPath = true;
            preferences1.Commit();
            preferences1.Destroy();

            foreach (NXOpen.CAM.NCGroup ncGroup in NCGroupAry)
            {
                if (CurrentNCGroup != ncGroup.Name)
                {
                    continue;
                }

                CAMObject[] OperGroup = ncGroup.GetMembers();

                for (int i = 0; i < OperGroup.Length; i++)
                {
                    NXOpen.CAM.CAMObject[] tempObjToCreateImg = new CAMObject[1];
                    string ImagePath = "";
                    foreach (string item in ListSelOper)
                    {
                        if (item == OperGroup[i].Name)
                        {
                            //暫時使用版本
                            tempObjToCreateImg[0] = OperGroup[i];
                            workPart.CAMSetup.ReplayToolPath(tempObjToCreateImg);
                            ImagePath = string.Format(@"{0}\{1}", PhotoFolderPath, OperGroup[i].Name);
                            theUfSession.Disp.CreateImage(ImagePath, UFDisp.ImageFormat.Jpeg, UFDisp.BackgroundColor.White);
                        }
                    }
                }


                for (int i = 0; i < panel.Rows.Count;i++ )
                {
                    string TempOperName = panel.GetCell(i, 0).Value.ToString();
                    foreach (string item in ListSelOper)
                    {
                        if (item != TempOperName)
                        {
                            continue;
                        }
                        panel.GetCell(i, 9).Value = "已拍照";
                        //GridRow a = new GridRow();a.GetCell(i,9).
                        //panel.GetCell(i, 9).CellStyles.Default.Background.Color1 = System.Drawing.Color.Red;
                        //panel.GetCell(i, 9).CellStyles.Default.Background.Color2 = System.Drawing.Color.Red;
                    }
                }
                
                /*
                for (int i = 0; i < OperationAry.Length; i++)
                {
                    //取得父層的群組(回傳：NCGroup XXXX)
                    //string NCProgramTag = OperationAry[i].GetParent(CAMSetup.View.ProgramOrder).ToString();
                    //NCProgramTag = Regex.Replace(NCProgramTag, "[^0-9]", "");
                    //if (NCProgramTag != ncGroup.Tag.ToString())
                    //{
                    //    continue;
                    //}

                    NXOpen.CAM.CAMObject[] tempObjToCreateImg = new CAMObject[1];
                    string ImagePath = "";
                    foreach (string item in ListSelOper)
                    {
                        if (item == OperationAry[i].Name)
                        {
                            //暫時使用版本
                            tempObjToCreateImg[0] = (NXOpen.CAM.CAMObject)OperationAry[i];
                            workPart.CAMSetup.ReplayToolPath(tempObjToCreateImg);
                            ImagePath = string.Format(@"{0}\{1}", PhotoFolderPath, OperationAry[i].Name);
                            theUfSession.Disp.CreateImage(ImagePath, UFDisp.ImageFormat.Jpeg, UFDisp.BackgroundColor.White);
                        }
                    }
                }
                */
            }
        }

        private void SelFixtuePath_Click(object sender, EventArgs e)
        {
            //判斷OP圖片資料夾是否已建立
            if (!Directory.Exists(PhotoFolderPath))
            {
                MessageBox.Show("請先選擇程式群組名稱");
                return;
            }

            string FixtureFilter = "jpg Files (*.jpg)|*.jpg|eps Files (*.eps)|*.eps|gif Files (*.gif)|*.gif|bmp Files (*.bmp)|*.bmp|png Files (*.png)|*.png|All Files (*.*)|*.*";
            status = CaxPublic.OpenFileDialog(out FixtureNameStr, out FixturePathStr, "", FixtureFilter);
            if (!status)
            {
                MessageBox.Show("治具圖片選擇失敗，系統將持續進行，請手動將治具圖片貼至Excel內");
                return;
            }

            FixturePath.Text = FixturePathStr;

            if (FixtureNameStr != "")
            {
                //將治具圖片放到Op圖片資料夾
                string destFileName = string.Format(@"{0}\{1}", PhotoFolderPath, FixtureNameStr);
                File.Copy(FixturePath.Text, destFileName, true);
            }
        }

        private bool ClearnMachineNoOfNCGroup()
        {
            try
            {
                foreach (NXOpen.CAM.NCGroup ncGroup in NCGroupAry)
                {
                    int type;
                    int subtype;
                    theUfSession.Obj.AskTypeAndSubtype(ncGroup.Tag, out type, out subtype);

                    if (type != UFConstants.UF_machining_task_type)//此處比對是否為Program群組
                    {
                        continue;
                    }
                    if (CurrentNCGroup == ncGroup.Name & MachineNo.Text != "")
                    {
                        ncGroup.DeleteAttributeByTypeAndTitle(NXObject.AttributeType.String, "MachineNo");
                    }
                }
            }
            catch (System.Exception ex)
            {
                return false;
            }
            return true;
        }
        private void ClearnMachineNo_Click(object sender, EventArgs e)
        {
            if (CurrentNCGroup == "") return;

            InitialzeMachineTree();
            //刪除屬性
            status = ClearnMachineNoOfNCGroup();
            if (!status)
            {
                MessageBox.Show("清空失敗");
            }
            MachineNo.Text = "";
            MachineTree.Enabled = true;
        }

        private void ControlDimen_Click(object sender, EventArgs e)
        {
            if (CurrentNCGroup == "") 
                return;

            //利用ListSelOper判斷是否有沒有選擇程式
            if (ListSelOper.Count == 0)
            {
                MessageBox.Show("請先選擇一條程式！");
                return;
            }
            if (ListSelOper.Count > 1)
            {
                MessageBox.Show("一次僅能選擇一條程式！");
                return;
            }

            #region 將選到的程式紀錄資訊，以利開啟對話框時可以填資料
            ControlDimen_Key sControlDimen_Key = new ControlDimen_Key();
            sControlDimen_Key.NcGroupName = CurrentNCGroup;
            sControlDimen_Key.ToolNo = singleRow.Cells[2].Value.ToString();
            sControlDimen_Key.ToolName = singleRow.Cells[3].Value.ToString();
            sControlDimen_Key.ToolHolder = singleRow.Cells[4].Value.ToString();
            //判斷是否已按更名，已按->存新的名字；未按->存舊的名字
            if (Is_Click_Rename) sControlDimen_Key.OperationName = singleRow.Cells[1].Value.ToString();
            else sControlDimen_Key.OperationName = singleRow.Cells[0].Value.ToString();
            
            List<ControlDimen_Value> ListControlDimen_Value = new List<ControlDimen_Value>();
            status = DicControlDimen.TryGetValue(sControlDimen_Key, out ListControlDimen_Value);
            if (!status)
            {
                ListControlDimen_Value = new List<ControlDimen_Value>();
                ControlDimen_Value sControlDimen_Value = new ControlDimen_Value();
                sControlDimen_Value.DraftingRev = "";
                sControlDimen_Value.ControlBallon = "";
                sControlDimen_Value.TheoryTolRange = "";
                sControlDimen_Value.ControlTolRange = "";
                ListControlDimen_Value.Add(sControlDimen_Value);
                DicControlDimen.Add(sControlDimen_Key, ListControlDimen_Value);
            }
            #endregion
           

            //判斷是否為系統流程
            if (Is_Local == null) 
            {
                this.Hide();
                NonDBDimension sNonDBDimensionDlg = new NonDBDimension(this.FindForm(), sControlDimen_Key.OperationName);
                sNonDBDimensionDlg.Show();
            }
            else
            {
                this.Hide();
                Dimension sDimensionDlg = new Dimension(this.FindForm(), sPartInfo, sControlDimen_Key.OperationName);
                sDimensionDlg.Show();
            }

            
        }

        private void SetProgName_Click(object sender, EventArgs e)
        {
            try
            {
                if (ProgramStart.Text == "")
                {
                    MessageBox.Show("請先輸入程式起始值");
                    return;
                }

                panel.Rows.Clear();

                #region 填入SGCPanel

                string initialProgValue = ProgramStart.Text;
                GridRow row = new GridRow();
                foreach (KeyValuePair<string, OperData> kvp in DicNCData)
                {
                    if (CurrentNCGroup != kvp.Key)
                    {
                        continue;
                    }
                    string[] splitOperName = kvp.Value.OperName.Split(',');
                    string[] splitToolName = kvp.Value.ToolName.Split(',');
                    string[] splitHolderDescription = kvp.Value.HolderDescription.Split(',');
                    string[] splitOperCuttingLength = kvp.Value.CuttingLength.Split(',');
                    string[] splitOperToolFeed = kvp.Value.ToolFeed.Split(',');
                    string[] splitOperCuttingTime = kvp.Value.CuttingTime.Split(',');
                    string[] splitOperToolNumber = kvp.Value.ToolNumber.Split(',');
                    string[] splitOperToolSpeed = kvp.Value.ToolSpeed.Split(',');

                    for (int i = 0; i < splitOperName.Length; i++)
                    {
                        row = new GridRow(splitOperName[i], "O" + initialProgValue, splitOperToolNumber[i], splitToolName[i],
                            splitHolderDescription[i], splitOperCuttingLength[i], splitOperToolFeed[i], splitOperToolSpeed[i], splitOperCuttingTime[i], "拍照");
                        panel.Rows.Add(row);

                        initialProgValue = (Convert.ToInt32(initialProgValue) + 1).ToString();
                    }
                }

                #endregion

                ConfirmRename.Enabled = true;


            }
            catch (System.Exception ex)
            {
                MessageBox.Show("設定失敗");
                return;
            }
        }

        private void SelectMachine_Click(object sender, EventArgs e)
        {
            if (!Is_BigDlg)
            {
                //Dlg變大
                Size DlgSize = new Size(598, 629);
                this.Size = DlgSize;

                Is_BigDlg = true;
            }
            else
            {
                //Dlg變小
                Size DlgSize = new Size(301, 629);
                this.Size = DlgSize;

                Is_BigDlg = false;
            }
        }

        private void ProgramStart_KeyDown(object sender, KeyEventArgs e)
        {
            if (e.KeyCode != Keys.Enter)
            {
                return;
            }

            try
            {
                if (ProgramStart.Text == "")
                {
                    MessageBox.Show("請先輸入程式起始值");
                    return;
                }

                panel.Rows.Clear();

                #region 填入SGCPanel

                string initialProgValue = ProgramStart.Text;
                GridRow row = new GridRow();
                foreach (KeyValuePair<string, OperData> kvp in DicNCData)
                {
                    if (CurrentNCGroup != kvp.Key)
                    {
                        continue;
                    }
                    string[] splitOperName = kvp.Value.OperName.Split(',');
                    string[] splitToolName = kvp.Value.ToolName.Split(',');
                    string[] splitHolderDescription = kvp.Value.HolderDescription.Split(',');
                    string[] splitOperCuttingLength = kvp.Value.CuttingLength.Split(',');
                    string[] splitOperToolFeed = kvp.Value.ToolFeed.Split(',');
                    string[] splitOperCuttingTime = kvp.Value.CuttingTime.Split(',');
                    string[] splitOperToolNumber = kvp.Value.ToolNumber.Split(',');
                    string[] splitOperToolSpeed = kvp.Value.ToolSpeed.Split(',');

                    for (int i = 0; i < splitOperName.Length; i++)
                    {
                        row = new GridRow(splitOperName[i], "O" + initialProgValue, splitOperToolNumber[i], splitToolName[i],
                            splitHolderDescription[i], splitOperCuttingLength[i], splitOperToolFeed[i], splitOperToolSpeed[i], splitOperCuttingTime[i], "拍照");
                        panel.Rows.Add(row);

                        initialProgValue = (Convert.ToInt32(initialProgValue) + 1).ToString();
                    }
                }

                #endregion

                ConfirmRename.Enabled = true;


            }
            catch (System.Exception ex)
            {
                MessageBox.Show("設定失敗");
                return;
            }
        }

        private void ToolControlDimenBtn_Click(object sender, EventArgs e)
        {
            if (CurrentNCGroup == "")
                return;

            //取得所有刀號
            Dictionary<string, List<string>> DicToolNoControl = new Dictionary<string, List<string>>();
            foreach (GridRow i in panel.Rows)
            {
                List<string> tempListOper = new List<string>();
                status = DicToolNoControl.TryGetValue(i.Cells[2].Value.ToString(), out tempListOper);
                if (!status)
                {
                    tempListOper = new List<string>();
                    if (Is_Click_Rename)
                    {
                        tempListOper.Add(i.Cells[1].Value.ToString());
                    }
                    else
                    {
                        tempListOper.Add(i.Cells[0].Value.ToString());
                    }
                    DicToolNoControl.Add(i.Cells[2].Value.ToString(), tempListOper);
                }
                else
                {
                    if (Is_Click_Rename)
                    {
                        tempListOper.Add(i.Cells[1].Value.ToString());
                    }
                    else
                    {
                        tempListOper.Add(i.Cells[0].Value.ToString());
                    }
                    DicToolNoControl[i.Cells[2].Value.ToString()] = tempListOper;
                }
            }
            //List<string> AllToolNo = new List<string>();
            //foreach (GridRow i in panel.Rows)
            //{
            //    if (AllToolNo.ToArray().Contains(i.Cells[2].Value))
            //    {
            //        continue;
            //    }
            //    AllToolNo.Add(i.Cells[2].Value.ToString());
            //}
            this.Hide();
            ToolControlDimen cToolControlDimen = new ToolControlDimen(this.FindForm(), sPartInfo, DicToolNoControl);
            cToolControlDimen.Show();

        }

        
    }
}
