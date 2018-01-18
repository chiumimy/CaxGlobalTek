using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Windows.Forms;
using CaxGlobaltek;
using DevComponents.DotNetBar.SuperGrid;
using System.Collections;
using NHibernate;
using System.IO;
using DevComponents.DotNetBar;
using OutputExcelForm.Excel;
using System.Runtime.InteropServices;

namespace OutputExcelForm
{
    public partial class OutputForm : Form
    {
        #region 全域變數
        bool status;
        public static GridPanel PEPanel = new GridPanel();
        public static GridPanel MEPanel = new GridPanel();
        public static GridPanel TEPanel = new GridPanel();
        public static GridPanel CPPanel = new GridPanel();
        public static GridPanel FixInsPanel = new GridPanel();
        public static ISession session = MyHibernateHelper.SessionFactory.OpenSession();
        public static Com_PEMain comPEMain = new Com_PEMain();
        public static Dictionary<DB_PEMain, List<DB_PartOperation>> DicPFDData = new Dictionary<DB_PEMain, List<DB_PartOperation>>();
        public static Dictionary<DB_CPKey, List<DB_CPValue>> DicCPData = new Dictionary<DB_CPKey, List<DB_CPValue>>();
        public static Dictionary<DB_MEMain, IList<Com_Dimension>> DicDimensionData = new Dictionary<DB_MEMain, IList<Com_Dimension>>();
        public static Dictionary<DB_TEMain, IList<Com_ShopDoc>> DicShopDocData = new Dictionary<DB_TEMain, IList<Com_ShopDoc>>();
        public static Dictionary<DB_TEMain, IList<Com_ToolList>> DicToolListData = new Dictionary<DB_TEMain, IList<Com_ToolList>>();
        public static Dictionary<DB_FixInspection, IList<Com_FixDimension>> DicFixDimensionData = new Dictionary<DB_FixInspection, IList<Com_FixDimension>>();
        public struct EnvVariables
        {
            //public static string env = "D:\\cax\\Globaltek";

            //public static string env = "\\\\192.168.31.129\\cax\\Globaltek";
            //public static string env = "\\\\192.168.35.1\\cax\\Globaltek";


        }
//         public string partNoComboboxText
//         {
//             get { return PartNoCombobox.Text; }
//         }
        #endregion
        
        public OutputForm()
        {
            InitializeComponent();
            InitializeGrid();
            InitializeHideControlItems();
            GetCustomerFromDatabase();
        }

        private void InitializeGrid()
        {
            PEPanel = SGC_PEPanel.PrimaryGrid;
            MEPanel = SGC_MEPanel.PrimaryGrid;
            TEPanel = SGC_TEPanel.PrimaryGrid;
            FixInsPanel = SGC_FixInsPanel.PrimaryGrid;
            CusComboBox.DropDownWidth = 150;
            //CPPanel = SGC_CPPanel.PrimaryGrid;
            //CPCharPanel = SGC_Characteristics.PrimaryGrid;
            //GetDataFromDatabase aaa = new GetDataFromDatabase(this);
            //string[] orderArray = new string[]{};            
        }

        private void InitializeHideControlItems()
        {
            PartNoCombobox.Enabled = false;
            CusVerCombobox.Enabled = false;
            OpVerCombobox.Enabled = false;
            Op1Combobox.Enabled = false;
        }

        private void GetCustomerFromDatabase()
        {
            status = GetDataFromDatabase.SetCustomerData(CusComboBox);
            if (!status)
            {
                MessageBox.Show("客戶資料取得失敗，請聯繫開發工程師");
                this.Close();
            }
        }

        private void CusComboBox_SelectedIndexChanged(object sender, EventArgs e)
        {
            PartNoCombobox.Enabled = true;
            CusVerCombobox.Enabled = false;
            OpVerCombobox.Enabled = false;
            Op1Combobox.Enabled = false;

            PartNoCombobox.Items.Clear();
            CusVerCombobox.Items.Clear();
            OpVerCombobox.Items.Clear();
            Op1Combobox.Items.Clear();

            PartNoCombobox.Text = "";
            CusVerCombobox.Text = "";
            OpVerCombobox.Text = "";
            Op1Combobox.Text = "";
            Op2Text.Text = "";

            PEPanel.Rows.Clear();
            MEPanel.Rows.Clear();
            TEPanel.Rows.Clear();
            CPPanel.Rows.Clear();
            FixInsPanel.Rows.Clear();

            status = GetDataFromDatabase.SetPartNoData(((Sys_Customer)CusComboBox.SelectedItem), PartNoCombobox);
            if (!status)
            {
                MessageBox.Show("料號資料取得失敗，請聯繫開發工程師");
                this.Close();
            }
        }

        private void PartNoCombobox_SelectedIndexChanged(object sender, EventArgs e)
        {
            CusVerCombobox.Enabled = true;
            OpVerCombobox.Enabled = false;
            Op1Combobox.Enabled = false;

            CusVerCombobox.Items.Clear();
            OpVerCombobox.Items.Clear();
            Op1Combobox.Items.Clear();

            CusVerCombobox.Text = "";
            OpVerCombobox.Text = "";
            Op1Combobox.Text = "";
            Op2Text.Text = "";

            PEPanel.Rows.Clear();
            MEPanel.Rows.Clear();
            TEPanel.Rows.Clear();
            CPPanel.Rows.Clear();
            FixInsPanel.Rows.Clear();

            status = GetDataFromDatabase.SetCusVerData(((Sys_Customer)CusComboBox.SelectedItem), PartNoCombobox.Text, CusVerCombobox);
            if (!status)
            {
                MessageBox.Show("版次資料取得失敗，請聯繫開發工程師");
                this.Close();
            }
            if (CusVerCombobox.Items.Count == 1)
            {
                CusVerCombobox.Text = CusVerCombobox.Items[0].ToString();
            }
        }

        private void CusVerCombobox_SelectedIndexChanged(object sender, EventArgs e)
        {
            //MessageBox.Show(((Com_PEMain)CusVerCombobox.SelectedItem).peSrNo.ToString());
            OpVerCombobox.Enabled = true;
            Op1Combobox.Enabled = false;

            OpVerCombobox.Items.Clear();
            Op1Combobox.Items.Clear();
            Op2Text.Text = "";

            OpVerCombobox.Text = "";
            Op1Combobox.Text = "";

            PEPanel.Rows.Clear();
            MEPanel.Rows.Clear();
            TEPanel.Rows.Clear();
            CPPanel.Rows.Clear();
            FixInsPanel.Rows.Clear();

            status = GetDataFromDatabase.SetOpVerData(((Sys_Customer)CusComboBox.SelectedItem), 
                                                        PartNoCombobox.Text, 
                                                        CusVerCombobox.Text, 
                                                        OpVerCombobox);
            if (!status)
            {
                MessageBox.Show("製程資料取得失敗，請聯繫開發工程師");
                this.Close();
            }
            if (OpVerCombobox.Items.Count == 1)
            {
                OpVerCombobox.Text = OpVerCombobox.Items[0].ToString();
            }
        }

        private void OpVerCombobox_SelectedIndexChanged(object sender, EventArgs e)
        {
            Op1Combobox.Enabled = true;
            Op1Combobox.Items.Clear();
            Op1Combobox.Text = "";
            Op2Text.Text = "";

            PEPanel.Rows.Clear();
            MEPanel.Rows.Clear();
            TEPanel.Rows.Clear();
            CPPanel.Rows.Clear();
            FixInsPanel.Rows.Clear();

            comPEMain = session.QueryOver<Com_PEMain>()
                               .Where(x => x.partName == PartNoCombobox.Text)
                               .Where(x => x.customerVer == CusVerCombobox.Text)
                               .Where(x => x.opVer == OpVerCombobox.Text)
                               .SingleOrDefault<Com_PEMain>();

            status = GetDataFromDatabase.SetPEPanelData(comPEMain, ref PEPanel);
            if (!status)
            {
                MessageBox.Show("PFD資料取得失敗，請聯繫開發工程師");
                this.Close();
            }
            //status = GetDataFromDatabase.SetPEPanelData(((Sys_Customer)CusComboBox.SelectedItem), PartNoCombobox.Text, CusVerCombobox.Text, OpVerCombobox.Text, ref PEPanel);
            //if (!status)
            //{
            //    MessageBox.Show("PFD資料取得失敗，請聯繫開發工程師");
            //    this.Close();
            //}

            status = GetDataFromDatabase.SetOp1Data(comPEMain, Op1Combobox);
            if (!status)
            {
                MessageBox.Show("製程資料取得失敗，請聯繫開發工程師");
                this.Close();
            }
        }

        private void Op1Combobox_SelectedIndexChanged(object sender, EventArgs e)
        {
            MEPanel.Rows.Clear();
            TEPanel.Rows.Clear();
            FixInsPanel.Rows.Clear();
            Op2Text.Text = ((Com_PartOperation)Op1Combobox.SelectedItem).sysOperation2.operation2Name;
            

            //從資料庫中取得有關ME的表單資料
            status = GetDataFromDatabase.SetMEPanelData(((Com_PartOperation)Op1Combobox.SelectedItem)
                                                        , CusComboBox.Text
                                                        , PartNoCombobox.Text
                                                        , CusVerCombobox.Text
                                                        , OpVerCombobox.Text
                                                        , Op1Combobox.Text
                                                        , ref MEPanel);
            if (!status)
            {
                MessageBox.Show("SetMEExcelData資料取得失敗，請聯繫開發工程師");
                this.Close();
            }

            //從資料庫中取得有關TE的表單資料
            status = GetDataFromDatabase.SetTEPanelData(((Com_PartOperation)Op1Combobox.SelectedItem)
                                                        , CusComboBox.Text
                                                        , PartNoCombobox.Text
                                                        , CusVerCombobox.Text
                                                        , OpVerCombobox.Text
                                                        , Op1Combobox.Text
                                                        , ref TEPanel);
            if (!status)
            {
                MessageBox.Show("SetTEExcelData資料取得失敗，請聯繫開發工程師");
                this.Close();
            }

            //從資料庫中取得有關FixIns的表單資料
            status = GetDataFromDatabase.SetFixInsPanelData(((Com_PartOperation)Op1Combobox.SelectedItem)
                                                            , CusComboBox.Text
                                                            , PartNoCombobox.Text
                                                            , CusVerCombobox.Text
                                                            , OpVerCombobox.Text
                                                            , Op1Combobox.Text
                                                            , ref FixInsPanel);
            if (!status)
            {
                MessageBox.Show("SetFixInsPanelData資料取得失敗，請聯繫開發工程師");
                this.Close();
            }
        }

        private void OK_Click(object sender, EventArgs e)
        {
            //string ExcelFolder = string.Format(@"{0}\{1}_{2}_{3}_OP{4}"
            //                                    , Environment.GetFolderPath(Environment.SpecialFolder.Desktop)
            //                                    , PartNoCombobox.Text
            //                                    , CusVerCombobox.Text
            //                                    , OpVerCombobox.Text
            //                                    , Op1Combobox.Text);

            CaxLoading.RunDlg();
            string ExcelFolder = string.Format(@"{0}\{1}_{2}_{3}"
                                                , Environment.GetFolderPath(Environment.SpecialFolder.Desktop)
                                                , PartNoCombobox.Text
                                                , CusVerCombobox.Text
                                                , OpVerCombobox.Text);

            //status = Excel_CommonFun.CheckExcelProcess();
            //if (!status)
            //{
            //    MessageBox.Show("請先關閉所有Excel再重新執行輸出，如沒有EXCEL在執行，請開啟工作管理員關閉背景EXCEL");
            //    return;
            //}

            #region ME表單
            if (SuperTabControl.SelectedTab.Text == "ME")
            {
                //ME_檢查是否有選取表單格式
                status = CheckFun.CheckAll("ME", MEPanel);
                if (!status)
                {
                    CaxLoading.CloseDlg();
                    return;
                }

                //ME_建立桌面資料夾存放產生的Excel
                if (!Directory.Exists(ExcelFolder))
                    Directory.CreateDirectory(ExcelFolder);

                foreach (GridRow i in MEPanel.Rows)
                {
                    if (i.GetCell(1).Value.ToString() == "PDF" & ((bool)i.GetCell(0).Value) == true)
                    {
                        CopyNC.CopyOISPFDToDesktop(CusComboBox.Text, PartNoCombobox.Text, CusVerCombobox.Text, OpVerCombobox.Text, Op1Combobox.Text, i.GetCell(2).Value.ToString());
                    }
                }

                
                //ME_由選取的Op1與Excel表單，查出資料庫的Com_Dimension
                status = GetDataFromDatabase.GetDimensionData(Op1Combobox, out DicDimensionData);
                if (!status)
                {
                    CaxLoading.CloseDlg();
                    MessageBox.Show("由Panel資料查DicDimensionData時發生錯誤，請聯繫開發工程師");
                    this.Close();
                } 
                //ME_開始輸出ME的Excel
                status = GetExcelForm.InsertDataToMEExcel(DicDimensionData, CusComboBox.Text, PartNoCombobox.Text, CusVerCombobox.Text, OpVerCombobox.Text, Op1Combobox.Text);
                if (!status)
                {
                    CaxLoading.CloseDlg();
                    MessageBox.Show("輸出ME的Excel時發生錯誤，請聯繫開發工程師");
                    this.Close();
                }
                
            }
            #endregion
            #region TE表單
            else if (SuperTabControl.SelectedTab.Text == "TE")
            {
                //TE_檢查是否有選取表單格式
                status = CheckFun.CheckAll("TE", TEPanel);
                if (!status)
                {
                    return;
                }
                //TE_建立桌面資料夾存放產生的Excel
                if (!Directory.Exists(ExcelFolder))
                    Directory.CreateDirectory(ExcelFolder);

                foreach (GridRow i in TEPanel.Rows)
                {
                    if (i.GetCell(1).Value.ToString() == "ShopDoc" & ((bool)i.GetCell(0).Value) == true)
                    {
                        
                        #region ShopDoc
                        //TE_由選取的Op1與Excel表單，查出資料庫的Com_ShopDoc
                        status = GetDataFromDatabase.GetShopDocData(Op1Combobox, out DicShopDocData);
                        if (!status)
                        {
                            CaxLoading.CloseDlg();
                            MessageBox.Show("由Panel資料查DicShopDocData時發生錯誤，請聯繫開發工程師");
                            this.Close();
                        }
                        //TE_開始輸出TE的ShopDoc
                        status = GetExcelForm.InsertDataToShopDocExcel(DicShopDocData, CusComboBox.Text, PartNoCombobox.Text, CusVerCombobox.Text, OpVerCombobox.Text, Op1Combobox.Text);
                        if (!status)
                        {
                            CaxLoading.CloseDlg();
                            MessageBox.Show("輸出TE的ShopDoc時發生錯誤，請聯繫開發工程師");
                            this.Close();
                        }
                        #endregion
                        
                    }
                    else if (i.GetCell(1).Value.ToString() == "ToolList" & ((bool)i.GetCell(0).Value) == true)
                    {
                        #region ToolList
                        //TE_由選取的Op1與Excel表單，查出資料庫的Com_ToolList
                        status = GetDataFromDatabase.GetToolListData(Op1Combobox, out DicToolListData);
                        if (!status)
                        {
                            CaxLoading.CloseDlg();
                            MessageBox.Show("由Panel資料查DicToolListData時發生錯誤，請聯繫開發工程師");
                            this.Close();
                        }
                        //TE_開始輸出TE的ToolList
                        status = GetExcelForm.InsertDataToTLExcel(DicToolListData, CusComboBox.Text, PartNoCombobox.Text, CusVerCombobox.Text, OpVerCombobox.Text, Op1Combobox.Text);
                        if (!status)
                        {
                            CaxLoading.CloseDlg();
                            MessageBox.Show("輸出TE的ToolList時發生錯誤，請聯繫開發工程師");
                            this.Close();
                        }
                        #endregion
                        
                    }
                    else if (i.GetCell(1).Value.ToString() == "NC程式" & ((bool)i.GetCell(0).Value) == true)
                    {
                        #region NC程式
                        status = CopyNC.CopyNCToDesktop(CusComboBox.Text, PartNoCombobox.Text, CusVerCombobox.Text, OpVerCombobox.Text, Op1Combobox.Text, i.GetCell(2).Value.ToString());
                        if (!status)
                        {
                            MessageBox.Show("下載NC資料夾時發生錯誤，請聯繫開發工程師");
                            this.Close();
                        }
                        #endregion
                    }
                }
            }
            #endregion
            #region PE表單
            else if (SuperTabControl.SelectedTab.Text == "PE")
            {
                //PE_檢查是否有選取表單格式
                status = CheckFun.CheckAll("PE", PEPanel);
                if (!status)
                {
                    return;
                }
                //PE_建立桌面資料夾存放產生的Excel
                if (!Directory.Exists(ExcelFolder))
                    Directory.CreateDirectory(ExcelFolder);

                foreach (GridRow i in PEPanel.Rows)
                {
                    if (i.GetCell(1).Value.ToString() == "PFD" & ((bool)i.GetCell(0).Value) == true)
                    {
                       
                        //PE_由選取的Row查出PFD資料
                        status = GetDataFromDatabase.GetPFDData(comPEMain, out DicPFDData);
                        if (!status)
                        {
                            CaxLoading.CloseDlg();
                            MessageBox.Show("由Panel資料查PFD Data時發生錯誤，請聯繫開發工程師");
                            this.Close();
                        }

                        //PE_開始輸出PFD的Excel
                        status = GetExcelForm.InsertDataToPFDExcel(PartNoCombobox.Text, DicPFDData);
                        if (!status)
                        {
                            CaxLoading.CloseDlg();
                            MessageBox.Show("輸出PFD的Excel時發生錯誤，請聯繫開發工程師");
                            this.Close();
                        }
                        
                    }
                    else if (i.GetCell(1).Value.ToString() == "Control Plan" & ((bool)i.GetCell(0).Value) == true)
                    {
                        
                        //PE_由選取的Row查出Control Plan資料
                        status = GetDataFromDatabase.GetControlPlanData(comPEMain, out DicCPData);
                        if (!status)
                        {
                            CaxLoading.CloseDlg();
                            MessageBox.Show("由Panel資料查Control Plan Data時發生錯誤，請聯繫開發工程師");
                            this.Close();
                        }

                        //PE_開始輸出Control Plan的Excel
                        status = GetExcelForm.InsertDataToCPExcel(DicCPData);
                        if (!status)
                        {
                            CaxLoading.CloseDlg();
                            MessageBox.Show("輸出Control Plan的Excel時發生錯誤，請聯繫開發工程師");
                            this.Close();
                        }
                        
                    }
                }
            }
            #endregion
            #region 模.檢.治
            else if (SuperTabControl.SelectedTab.Text == "模.檢.治")
            {
                //模.檢.治_檢查是否有選取表單格式
                status = CheckFun.CheckAll("模.檢.治", FixInsPanel);
                if (!status)
                {
                    return;
                }
                //建立桌面資料夾存放產生的Excel
                if (!Directory.Exists(ExcelFolder))
                    Directory.CreateDirectory(ExcelFolder);

                foreach (GridRow i in FixInsPanel.Rows)
                {
                    if (((bool)i.GetCell(0).Value) != true)
                    {
                        continue;
                    }
                    status = GetDataFromDatabase.GetFixInsData(Op1Combobox, i.GetCell(1).Value.ToString(), out DicFixDimensionData);
                    if (!status)
                    {
                        CaxLoading.CloseDlg();
                        MessageBox.Show("由Panel資料查DicFixDimensionData時發生錯誤");
                        this.Close();
                    }

                    status = GetExcelForm.InsertDataToFixInsExcel(DicFixDimensionData, CusComboBox.Text, PartNoCombobox.Text, CusVerCombobox.Text, OpVerCombobox.Text, Op1Combobox.Text);
                    if (!status)
                    {
                        CaxLoading.CloseDlg();
                        MessageBox.Show("輸出模.檢.治的Excel時發生錯誤，請聯繫開發工程師");
                        this.Close();
                    }
                }

            }
            #endregion
            CaxLoading.CloseDlg();
            MessageBox.Show("表單輸出完成！");
            //this.Close();
        }

        private void Close_Click(object sender, EventArgs e)
        {
            this.Close();
        }

        private void OutputForm_FormClosing(object sender, FormClosingEventArgs e)
        {
            //this.Close();
        }
    }
}
