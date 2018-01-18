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
using DevComponents.DotNetBar.SuperGrid;
using NXOpen.CAM;
using System.Text.RegularExpressions;
using CaxGlobaltek;
using System.IO;
using DevComponents.DotNetBar.Controls;


namespace PostProcessor
{
    public partial class PostProcessorDlg : DevComponents.DotNetBar.Office2007Form
    {
        public static Session theSession = Session.GetSession();
        public static UI theUI;
        public static UFSession theUfSession = UFSession.GetUFSession();
        public static Part workPart = theSession.Parts.Work;
        public static Part displayPart = theSession.Parts.Display;

        public static bool status;
        public static METE_Download_Upload_Path cMETE_Download_Upload_Path = new METE_Download_Upload_Path();
        public static GridPanel OperPanel = new GridPanel();
        public static GridPanel PostPanel = new GridPanel();
        public static NXOpen.CAM.NCGroup[] NCGroupAry = new NXOpen.CAM.NCGroup[] { };
        public static NXOpen.CAM.Operation[] OperationAry = new NXOpen.CAM.Operation[] { };
        public static Dictionary<string, string> DicNCData = new Dictionary<string, string>();
        public static string CurrentNCGroup = "", CurrentSelPostName = "";
        public static int CurrentRowIndexofPostPanel = -1;
        public static string NCFolderPath = "", postprocessor = "";
        public static NXOpen.CAM.NCGroup SelectNCGroup;
        public static NXOpen.CAM.CAMObject[] OperationObj = new NXOpen.CAM.CAMObject[] { };
        public static List<string> ListSelOper = new List<string>();
        public PartInfo sPartInfo = new PartInfo();
        public static string[] TemplatePostData;

        public PostProcessorDlg()
        {
            InitializeComponent();

            //建立panel物件
            OperPanel = SuperGridOperPanel.PrimaryGrid;
            PostPanel = SuperGridPostPanel.PrimaryGrid;
            //OperPanel.Columns["挑選"].EditorType = typeof(CheckBox1);
            //SelectAll.Checked = true;
        }

        private void PostProcessorDlg_Load(object sender, EventArgs e)
        {
            int module_id;
            theUfSession.UF.AskApplicationModule(out module_id);
            if (module_id != UFConstants.UF_APP_CAM)
            {
                MessageBox.Show("請先轉換為加工模組後再執行！");
                this.Close();
            }

            //取得METEDownload_Upload資料
            status = CaxGetDatData.GetMETEDownload_Upload(out cMETE_Download_Upload_Path);
            if (!status)
            {
                MessageBox.Show("取得METEDownload_Upload失敗");
                return;
            }

            //取得所有GroupAry，用來判斷Group的Type決定是NC、Tool、Geometry
            NCGroupAry = displayPart.CAMSetup.CAMGroupCollection.ToArray();
            //取得所有operationAry
            OperationAry = displayPart.CAMSetup.CAMOperationCollection.ToArray();

            #region 取得相關資訊，填入Dic
            DicNCData = new Dictionary<string, string>();
            foreach (NXOpen.CAM.NCGroup ncGroup in NCGroupAry)
            {
                int type;
                int subtype;
                theUfSession.Obj.AskTypeAndSubtype(ncGroup.Tag, out type, out subtype);
                //判斷是否為Program群組
                if (type != UFConstants.UF_machining_task_type)
                {
                    continue;
                }
                
                foreach (NXOpen.CAM.Operation item in OperationAry)
                {
                    //取得父層的群組(回傳：NCGroup XXXX)
                    string NCProgramTag = item.GetParent(CAMSetup.View.ProgramOrder).ToString();
                    NCProgramTag = Regex.Replace(NCProgramTag, "[^0-9]", "");
                    if (NCProgramTag == ncGroup.Tag.ToString())
                    {
                        bool cheValue;
                        string OperName = "";
                        cheValue = DicNCData.TryGetValue(ncGroup.Name, out OperName);
                        if (!cheValue)
                        {
                            OperName = item.Name;
                            DicNCData.Add(ncGroup.Name, OperName);
                        }
                        else
                        {
                            OperName = OperName + "," + item.Name;
                            DicNCData[ncGroup.Name] = OperName;
                        }
                    }
                }
            }
            #endregion

            //將DicProgName的key存入程式群組下拉選單中
            comboBoxNCgroup.Items.AddRange(DicNCData.Keys.ToArray());

            #region 將控制器名稱填入SuperGridPostPanel

            GridRow row = new GridRow();
            //-----暫時使用版本(路徑指向UG)
            //string TemplateRoot = string.Format(@"{0}\{1}\{2}\{3}", Environment.GetEnvironmentVariable("UGII_BASE_DIR"), "MACH", "resource", "postprocessor");
            //string TemplatePostPath = string.Format(@"{0}\{1}", TemplateRoot, "template_post.dat");
            //TemplatePostData = System.IO.File.ReadAllLines(TemplatePostPath);
            //-----發佈使用版本(Server需有MACH檔案)
            TemplatePostData = CaxGetDatData.GetTemplatePostData();
            if (TemplatePostData.Length == 0)
            {
                CaxLog.ShowListingWindow("template_post.dat為空，請檢查！");
            }
            //string empty = "";
            //row = new GridRow(empty);
            //PostPanel.Rows.Add(row);
            //for (int i = 0; i < TemplatePostData.Length;i++ )
            //{
            //    if (i>6)
            //    {
            //        string PostName = TemplatePostData[i].Split(',')[0];
            //        row = new GridRow(PostName);
            //        PostPanel.Rows.Add(row);
            //    }
            //}

            #endregion
        }

        private bool GetMachineDataToMachineNo()
        {
            try
            {
                //曾經選過的機台資訊寫到MachineNo
                foreach (NXOpen.CAM.NCGroup ncGroup in NCGroupAry)
                {
                    int type;
                    int subtype;
                    theUfSession.Obj.AskTypeAndSubtype(ncGroup.Tag, out type, out subtype);

                    if (type != UFConstants.UF_machining_task_type)//此處比對是否為Program群組
                    {
                        continue;
                    }
                    if (CurrentNCGroup != ncGroup.Name || ncGroup.GetStringAttribute("MachineNo") == null)
                    {
                        continue;
                    }

                    MachineNo.Text = ncGroup.GetStringAttribute("MachineNo");
                }
            }
            catch (System.Exception ex)
            {
                return false;
            }
            return true;
        }


        private void comboBoxNCgroup_SelectedIndexChanged(object sender, EventArgs e)
        {
            PostPanel.Rows.Clear();
            MachineNo.Text = "";
            //清空superGrid資料&機台資訊
            OperPanel.Rows.Clear();
            MachineNo.Text = "";
            //取得comboBox資料
            CurrentNCGroup = comboBoxNCgroup.Text;

            #region 建立NC資料夾
            string Is_Local = "";
            Is_Local = Environment.GetEnvironmentVariable("UGII_ENV_FILE");
            if (Is_Local != null)
            {
                CaxPublic.GetAllPath("TE", displayPart.FullPath, out sPartInfo, ref cMETE_Download_Upload_Path);
                if (!cMETE_Download_Upload_Path.Local_Folder_CAM.Contains("Oper1"))
                {
                    NCFolderPath = string.Format(@"{0}\{1}_{2}", cMETE_Download_Upload_Path.Local_Folder_CAM, CurrentNCGroup, "NC");
                }
                else
                {
                    NCFolderPath = string.Format(@"{0}\{1}_{2}", Path.GetDirectoryName(displayPart.FullPath), CurrentNCGroup, "NC");
                }
            }
            else
            {
                NCFolderPath = string.Format(@"{0}\{1}_{2}", Path.GetDirectoryName(displayPart.FullPath), CurrentNCGroup, "NC");
            }


            if (!Directory.Exists(NCFolderPath))
            {
                System.IO.Directory.CreateDirectory(NCFolderPath);
            }
            #endregion

            //取得選擇的NC
            foreach (NXOpen.CAM.NCGroup ncGroup in NCGroupAry)
            {
                if (CurrentNCGroup != ncGroup.Name)
                {
                    continue;
                }
                SelectNCGroup = ncGroup;
            }

            //取得Group下的所有OP
            OperationObj = SelectNCGroup.GetMembers();

            #region 填值到SuperGridOperPanel
            
            GridRow row = new GridRow();
            foreach (KeyValuePair<string, string> kvp in DicNCData)
            {
                if (CurrentNCGroup != kvp.Key)
                {
                    continue;
                }
                
                string[] splitOperName = kvp.Value.Split(',');
                
                for (int i = 0; i < splitOperName.Length; i++)
                {
                    //CheckBox1 aaa = new CheckBox1();
                    

                    row = new GridRow(false, splitOperName[i], "");
                    
                    //OperPanel.Columns["挑選"].EditorType = typeof(CheckBox1);
                    OperPanel.Rows.Add(row);
                    
                }
            }
            
            #endregion

            //填機台到MachineNo欄位並取得對應後處理器
            try
            {
                MachineNo.Text = SelectNCGroup.GetStringAttribute("MachineNo");
                postprocessor = SelectNCGroup.GetStringAttribute("postprocessor");
            }
            catch (System.Exception ex)
            {
                MachineNo.Text = "";
                postprocessor = "";
            }

            if (postprocessor != "")
            {
                string[] splitPostProcessor = postprocessor.Split(',');
                string empty = "";
                row = new GridRow(empty);
                PostPanel.Rows.Add(row);
                for (int i = 0; i < splitPostProcessor.Length; i++)
                {
                    row = new GridRow(splitPostProcessor[i]);
                    PostPanel.Rows.Add(row);
                }
                #region 在ShopDoc中有指定機台並且有後處理
                if (splitPostProcessor.Length == 1)
                {
                    //如果只有一個後處理，則自動對應
                    for (int i = 0; i < OperPanel.Rows.Count;i++ )
                    {
                        OperPanel.GetCell(i, 2).Value = splitPostProcessor[0];
                    }
                }
                #endregion
            }
            else
            {
                #region 在ShopDoc中沒指定機台or有指定機台但沒有後處理，則顯示全部後處理
                string empty = "";
                row = new GridRow(empty);
                PostPanel.Rows.Add(row);
                for (int i = 0; i < TemplatePostData.Length; i++)
                {
                    if (i > 6)
                    {
                        string PostName = TemplatePostData[i].Split(',')[0];
                        row = new GridRow(PostName);
                        PostPanel.Rows.Add(row);
                    }
                }
                #endregion
            }
        }

        public class CheckBox1 : GridCheckBoxXEditControl
        {
            public CheckBox1()
            {
                try
                {
                    CaxLog.ShowListingWindow("220");
                    
                    //Checked = false;
                    CheckedChanged += new System.EventHandler(aabb);

                    //CaxLog.ShowListingWindow(OperPanel.GetCell(0,0).Value.ToString());
                }
                catch (System.Exception ex)
                {
                }
            }
            public void aabb(object sender, EventArgs e)
            {
                CaxLog.ShowListingWindow("5656");
            }

        }

        private void SelectAll_CheckedChanged(object sender, EventArgs e)
        {
            //清空superGrid資料
            //OperPanel.Rows.Clear();
            /*
            GridRow row = new GridRow();
            for (int i = 0; i < OperPanel.Rows.Count;i++ )
            {
                if (SelectAll.Checked == false)
                {
                    OperPanel.GetCell(i, 0).Value = false;
                }
                else
                {
                    OperPanel.GetCell(i, 0).Value = true;
                }
            }
            */

            #region (註解中)填值到SuperGridOperPanel
            /*
            GridRow row = new GridRow();
            if (SelectAll.Checked == false)
            {
                foreach (KeyValuePair<string, string> kvp in DicNCData)
                {
                    if (CurrentNCGroup != kvp.Key)
                    {
                        continue;
                    }
                     
                    string[] splitOperName = kvp.Value.Split(',');
                    for (int i = 0; i < splitOperName.Length; i++)
                    {
                        row = new GridRow(false, splitOperName[i], "");
                        OperPanel.Rows.Add(row);
                    }
                }
            }
            else
            {
                foreach (KeyValuePair<string, string> kvp in DicNCData)
                {
                    if (CurrentNCGroup != kvp.Key)
                    {
                        continue;
                    }

                    string[] splitOperName = kvp.Value.Split(',');
                    for (int i = 0; i < splitOperName.Length; i++)
                    {
                        row = new GridRow(true, splitOperName[i], "");
                        OperPanel.Rows.Add(row);
                    }
                }
            }
            */
            #endregion
            
        }

        private void SuperGridPostPanel_RowClick(object sender, GridRowClickEventArgs e)
        {
            //取得點選的RowIndex
            //CurrentRowIndexofPostPanel = e.GridRow.Index; 
            //CurrentSelPostName = PostPanel.GetCell(CurrentRowIndexofPostPanel, 0).Value.ToString();
        }

        private void Output_Click(object sender, EventArgs e)
        {
            NXOpen.CAM.Preferences preferences1 = theSession.CAMSession.CreateCamPreferences();
            preferences1.ReplayRefreshBeforeEachPath = true;
            preferences1.Commit();
            preferences1.Destroy();

            for (int i = 0; i < OperPanel.Rows.Count; i++)
            {
                string PostName = OperPanel.GetCell(i, 2).Value.ToString();
                if (PostName == "")
                {
                    continue;
                }
                string OperName = OperPanel.GetCell(i, 1).Value.ToString();
                for (int y = 0; y < OperationObj.Length;y++ )
                {
                    if (OperName != OperationObj[y].Name)
                    {
                        continue;
                    }
                    string outputpath = "";
                    
                    //if (PostName == "Globaltek_DMU_40_eVo_Mill_turn_20151214")
                    //{
                    //    outputpath = string.Format(@"{0}\{1}", NCFolderPath, OperationObj[y].Name + ".mpf");
                    //}
                    //else if (PostName == "GLOBALTEK_DMU50_NEW_20120924" || PostName == "GLOBALTEK_DMU50_NEW_VERICUT")
                    //{
                    //    outputpath = string.Format(@"{0}\{1}", NCFolderPath, OperationObj[y].Name + ".h");
                    //}
                    //else
                    //{
                    //    outputpath = string.Format(@"{0}\{1}", NCFolderPath, OperationObj[y].Name);
                    //}

                    NCFolderPath = Path.GetFullPath(NCFolderPath);
                    if (ExtensionName.Text.Contains('.'))
                        outputpath = string.Format(@"{0}\{1}", NCFolderPath, OperationObj[y].Name + ExtensionName.Text);
                    else if (ExtensionName.Text != "")
                        outputpath = string.Format(@"{0}\{1}", NCFolderPath, OperationObj[y].Name + "." + ExtensionName.Text);
                    else
                        outputpath = string.Format(@"{0}\{1}", NCFolderPath, OperationObj[y].Name);
                    
                    bool checkSucess = CaxOper.CreatePost(OperationObj[y], PostName, outputpath);
                    if (!checkSucess)
                    {
                        CaxLog.ShowListingWindow("程式：" + OperationObj[y].Name + "可能尚未完成，導致輸出的後處理不完全");
                    }
                    break;
                }
            }

            
            MessageBox.Show("後處理輸出完成！");
            //CaxLog.ShowListingWindow("後處理輸出完成！");

            /*
            if (CurrentSelPostName == "")
            {
                UI.GetUI().NXMessageBox.Show("注意", NXMessageBox.DialogType.Error, "先指定一個後處理器名稱！");
                return;
            }
            
            foreach (NXOpen.CAM.NCGroup ncGroup in NCGroupAry)
            {
                int type;
                int subtype;
                theUfSession.Obj.AskTypeAndSubtype(ncGroup.Tag, out type, out subtype);
                //判斷是否為Program群組
                if (type != UFConstants.UF_machining_task_type)
                {
                    continue;
                }

                //判斷是否已更名為OPXXX
                if (!ncGroup.Name.Contains("OP"))
                {
                    UI.GetUI().NXMessageBox.Show("注意", NXMessageBox.DialogType.Error, "請先手動將Group名稱：" + ncGroup.Name + "，改為正確格式，再重新啟動功能！");
                    this.Close();
                    return;
                }

                //選到的OP與Collection做比對
                if (CurrentNCGroup != ncGroup.Name)
                {
                    continue;
                }
                
                //記錄checkbox為true的OP
                List<string> ListSelectOP = new List<string>();
                for (int i = 0; i < OperPanel.Rows.Count; i++)
                {
                    bool check_sel = false;
                    check_sel = (bool)OperPanel.GetCell(i, 0).Value;
                    if (check_sel)
                    {
                        ListSelectOP.Add(OperPanel.GetCell(i, 1).Value.ToString());
                    }
                }

                //取得此NC下的Operation
                CAMObject[] OperGroup = ncGroup.GetMembers();

                //開始輸出後處理
                foreach (string i in ListSelectOP)
                {
                    foreach (CAMObject y in OperGroup)
                    {
                        if (i != y.Name)
                        {
                            continue;
                        }
                        string outputpath = string.Format(@"{0}\{1}", NCFolderPath, y.Name);
                        bool checkSucess = CaxOper.CreatePost(y, CurrentSelPostName, outputpath);
                        if (!checkSucess)
                        {
                            CaxLog.ShowListingWindow("程式：" + y.Name + "可能尚未完成，導致輸出的後處理不完全");
                        }
                    }
                }
            }
            */
        }
        private void CloseDlg_Click(object sender, EventArgs e)
        {
            NXOpen.CAM.Preferences preferences1 = theSession.CAMSession.CreateCamPreferences();
            preferences1.ReplayRefreshBeforeEachPath = true;
            preferences1.Commit();
            preferences1.Destroy();
            this.Close();
        }

        private void SuperGridPostPanel_RowDoubleClick(object sender, GridRowDoubleClickEventArgs e)
        {
            //取得點選的RowIndex
            CurrentRowIndexofPostPanel = e.GridRow.Index;
            CurrentSelPostName = PostPanel.GetCell(CurrentRowIndexofPostPanel, 0).Value.ToString();

            //將選到的後處理器填入OperPanel中
            foreach (string SelectOper in ListSelOper)
            {
                for (int i = 0; i < OperationObj.Length; i++)
                {
                    if (SelectOper == OperationObj[i].Name)
                    {
                        OperPanel.GetCell(i, 2).Value = CurrentSelPostName;
                    }
                }
            }
        }

        private void SuperGridOperPanel_RowClick(object sender, GridRowClickEventArgs e)
        {
            //取得使用者選取的OP名稱
            ListSelOper = new List<string>();
            SelectedElementCollection OperSelCollection = OperPanel.GetSelectedElements();
            foreach (GridRow item in OperSelCollection)
            {
                ListSelOper.Add(item.Cells[1].Value.ToString());
                //CaxLog.ShowListingWindow(item.Cells[0].Value.ToString());//可以看出debug中item的值會顯示GridRow的型態
            }

            //這邊設定NX中是否顯示加工路徑為->單條顯示or多條顯示
            NXOpen.CAM.Preferences preferences1 = theSession.CAMSession.CreateCamPreferences();
            if (ListSelOper.Count == 1)
            {
                preferences1.ReplayRefreshBeforeEachPath = true;
                preferences1.Commit();
                preferences1.Destroy();
                
            }
            else if (ListSelOper.Count > 1)
            {
                preferences1.ReplayRefreshBeforeEachPath = false;
                preferences1.Commit();
                preferences1.Destroy();
            }

            foreach (string SelectOper in ListSelOper)
            {
                for (int i = 0; i < OperationObj.Length; i++)
                {
                    if (SelectOper == OperationObj[i].Name)
                    {
                        NXOpen.CAM.CAMObject[] tempObjToCreateImg = new CAMObject[1];
                        tempObjToCreateImg[0] = (NXOpen.CAM.CAMObject)OperationObj[i];
                        workPart.CAMSetup.ReplayToolPath(tempObjToCreateImg);
                    }
                }
            }
        }
    }

    
}
