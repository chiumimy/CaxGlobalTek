using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using NHibernate;
using CaxGlobaltek;
using DevComponents.DotNetBar.Controls;
using System.Windows.Forms;
using DevComponents.DotNetBar.SuperGrid;
using System.IO;

namespace OutputExcelForm
{
    //public class Com_MEMain
    //{
    //    public virtual Int32 meSrNo { get; set; }
    //    public virtual Com_PartOperation comPartOperation { get; set; }
    //    public virtual Sys_MEExcel sysMEExcel { get; set; }
    //    public virtual IList<Com_Dimension> comDimension { get; set; }
    //    public virtual string createDate { get; set; }
    //}


    public struct DB_PEMain
    {
        public string PartNo { get; set; }
        public string CusVer { get; set; }
        public string OpVer { get; set; }
        public string excelTemplateFilePath { get; set; }
    }
    public struct DB_PartOperation
    {
        public string Op1 { get; set; }
        public string Op2 { get; set; }
        public string Form { get; set; }
        public string ImgPath { get; set; }
    }

    public struct DB_CPKey
    {
        public string PartNo { get; set; }
        public string CusVer { get; set; }
        public string OpVer { get; set; }
        public string PartDesc { get; set; }
        public string excelTemplateFilePath { get; set; }
    }
    public struct DB_CPValue
    {
        public string Op1 { get; set; }
        public string Op2 { get; set; }
        public IList<Com_Dimension> comDimension { get; set; }
        public Dictionary<int, IList<Com_Dimension>> DicBalloonData { get; set; }
        //public List<BalloonData> sBalloonData { get; set; }
    }
    //public struct BalloonData
    //{
    //    public string balloon { get; set; }
    //    public IList<Com_Dimension> comDimension { get; set; } 
    //}
    
    public struct DB_MEMain
    {
        public Com_MEMain comMEMain { get; set; }
        public string excelType { get; set; }
        public string excelTemplateFilePath { get; set; }
        public string factory { get; set; }
    }
    public struct DB_TEMain
    {
        public Com_TEMain comTEMain { get; set; }
        public string excelTemplateFilePath { get; set; }
        public string ncGroupName { get; set; }
        public string factory { get; set; }
        public string partDesc { get; set; }
    }
    public struct DB_FixInspection
    {
        public Com_FixInspection comFixInspection { get; set; }
        public string excelTemplateFilePath { get; set; }
    }
    public struct DB_ShopDoc
    {
        public IList<Com_ShopDoc> comShopDoc { get; set; }
    }

    public class GetDataFromDatabase
    {
        public static bool status;
        public static ISession session = MyHibernateHelper.SessionFactory.OpenSession();
//         public static OutputForm aa;
// 
//         public GetDataFromDatabase(OutputForm bb)
//         {
//             aa = bb;
//         }


        public static bool SetCustomerData(ComboBoxEx CusComboBox)
        {
            try
            {
                IList<Sys_Customer> customerName = session.QueryOver<Sys_Customer>().List<Sys_Customer>();
                CusComboBox.DisplayMember = "customerName";
                //CusComboBox.ValueMember = "customerSrNo";
                foreach (Sys_Customer i in customerName)
                {
                    CusComboBox.Items.Add(i);
                }
            }
            catch (System.Exception ex)
            {
                return false;
            }
            return true;
        }
        public static bool SetPartNoData(Sys_Customer customerSrNo, ComboBoxEx PartNoCombobox)
        {
            try
            {
                //MessageBox.Show(customerSrNo.customerSrNo.ToString());
                IList<Com_PEMain> comPEMain = session.QueryOver<Com_PEMain>()
                                              .Where(x => x.sysCustomer == customerSrNo)
                                              .List<Com_PEMain>();

                foreach (Com_PEMain i in comPEMain)
                {
                    if (PartNoCombobox.Items.Contains(i.partName))
                    {
                        continue;
                    }
                    PartNoCombobox.Items.Add(i.partName);
                }
            }
            catch (System.Exception ex)
            {
                return false;
            }
            return true;
        }
        public static bool SetCusVerData(Sys_Customer customerSrNo, string partNo, ComboBoxEx CusVerCombobox)
        {
            try
            {
                IList<Com_PEMain> comPEMain = session.QueryOver<Com_PEMain>()
                                              .Where(x => x.sysCustomer == customerSrNo)
                                              .Where(x => x.partName == partNo)
                                              .List<Com_PEMain>();
                //CusVerCombobox.DisplayMember = "customerVer";
                //CusVerCombobox.ValueMember = "peSrNo";
                foreach (Com_PEMain i in comPEMain)
                {
                    if (CusVerCombobox.Items.Contains(i.customerVer))
                    {
                        continue;
                    }
                    CusVerCombobox.Items.Add(i.customerVer);
                }
                //CusVerCombobox.Items.Add(comPEMain);

            }
            catch (System.Exception ex)
            {
                return false;
            }
            return true;
        }
        public static bool SetOpVerData(Sys_Customer customerSrNo, string partNo, string cusVer, ComboBoxEx OpVerCombobox)
        {
            try
            {
                IList<Com_PEMain> comPEMain = session.QueryOver<Com_PEMain>()
                                              .Where(x => x.sysCustomer == customerSrNo)
                                              .Where(x => x.partName == partNo)
                                              .Where(x => x.customerVer == cusVer)
                                              .List<Com_PEMain>();
                //CusVerCombobox.DisplayMember = "customerVer";
                //CusVerCombobox.ValueMember = "peSrNo";
                foreach (Com_PEMain i in comPEMain)
                {
                    if (OpVerCombobox.Items.Contains(i.opVer))
                    {
                        continue;
                    }
                    OpVerCombobox.Items.Add(i.opVer);
                }
                //CusVerCombobox.Items.Add(comPEMain);
            }
            catch (System.Exception ex)
            {
                return false;
            }
            return true;
        }
        public static bool SetOp1Data(Sys_Customer customerSrNo, string partNo, string cusVer, string opVer, ComboBoxEx Op1Combobox)
        {
            try
            {
                Com_PEMain comPEMain = session.QueryOver<Com_PEMain>()
                                              .Where(x => x.sysCustomer == customerSrNo)
                                              .Where(x => x.partName == partNo)
                                              .Where(x => x.customerVer == cusVer)
                                              .Where(x => x.opVer == opVer)
                                              .SingleOrDefault<Com_PEMain>();
                IList<Com_PartOperation> comPartOperation = session.QueryOver<Com_PartOperation>()
                                                            .Where(x => x.comPEMain == comPEMain)
                                                            .List<Com_PartOperation>();
                Op1Combobox.DisplayMember = "operation1";
                Op1Combobox.ValueMember = "partOperationSrNo";
                //Op1Combobox.DataSource = comPartOperation;
                foreach (Com_PartOperation i in comPartOperation)
                {
                    Op1Combobox.Items.Add(i);
                }
            }
            catch (System.Exception ex)
            {
                return false;
            }
            return true;
        }
        public static bool SetOp1Data(Com_PEMain comPEMain, ComboBoxEx Op1Combobox)
        {
            try
            {
                IList<Com_PartOperation> comPartOperation = session.QueryOver<Com_PartOperation>()
                                                            .Where(x => x.comPEMain == comPEMain)
                                                            .OrderBy(x => x.operation1).Asc
                                                            .List<Com_PartOperation>();
                Op1Combobox.DisplayMember = "operation1";
                Op1Combobox.ValueMember = "partOperationSrNo";
                //Op1Combobox.DataSource = comPartOperation;
                foreach (Com_PartOperation i in comPartOperation)
                {
                    Op1Combobox.Items.Add(i);
                }
            }
            catch (System.Exception ex)
            {
                return false;
            }
            return true;
        }
        public static bool SetPEPanelData(Sys_Customer customerSrNo, string partNo, string cusVer, string opVer, ref GridPanel PEPanel)
        {
            try
            {
                Com_PEMain comPEMain = session.QueryOver<Com_PEMain>()
                                              .Where(x => x.sysCustomer == customerSrNo)
                                              .Where(x => x.partName == partNo)
                                              .Where(x => x.customerVer == cusVer)
                                              .Where(x => x.opVer == opVer)
                                              .SingleOrDefault<Com_PEMain>();
                
                #region 插入Panel
                object[] o = new object[] { false, "PFD", cusVer, opVer
                                                , string.Format("{0}_{1}_{2}資料夾"
                                                , partNo
                                                , comPEMain.customerVer
                                                , comPEMain.opVer) };
                PEPanel.Rows.Add(new GridRow(o));
                PEPanel.GetCell(0, 0).Value = false;

                #region 判斷所有製程是否都有上傳MEMain，如果有上傳才顯示Control Plan
                IList<Com_PartOperation> listComPartOperation = session.QueryOver<Com_PartOperation>()
                                                                       .Where(x => x.comPEMain == comPEMain).List<Com_PartOperation>();

                bool IsComplete = true;
                foreach (Com_PartOperation i in listComPartOperation)
                {
                    IList<Com_MEMain> comMEMain = session.QueryOver<Com_MEMain>()
                                                         .Where(x => x.comPartOperation == i).List<Com_MEMain>();
                    if (comMEMain.Count > 0)
                        continue;
                    else
                    {
                        IsComplete = false;
                        break;
                    }
                }

                if (IsComplete)
                {
                    o = new object[] { false, "Control Plan", cusVer, opVer
                                                , string.Format("{0}_{1}_{2}資料夾"
                                                , partNo
                                                , comPEMain.customerVer
                                                , comPEMain.opVer) };
                    PEPanel.Rows.Add(new GridRow(o));
                    PEPanel.GetCell(1, 0).Value = false;
                }

                #endregion


                #endregion
            }
            catch (System.Exception ex)
            {
                MessageBox.Show(ex.ToString());
                return false;
            }
            return true;
        }
        public static bool SetPEPanelData(Com_PEMain comPEMain, ref GridPanel PEPanel)
        {
            try
            {
                #region 插入PFD
                object[] o = new object[] { false, "PFD", comPEMain.customerVer, comPEMain.opVer
                                                , string.Format("{0}_{1}_{2}資料夾"
                                                , comPEMain.partName
                                                , comPEMain.customerVer
                                                , comPEMain.opVer) };
                PEPanel.Rows.Add(new GridRow(o));
                PEPanel.GetCell(0, 0).Value = false;
                #endregion

                #region (註解)判斷所有製程是否都有上傳MEMain，如果有上傳才顯示Control Plan
                /*
                IList<Com_PartOperation> listComPartOperation = session.QueryOver<Com_PartOperation>()
                                                                       .Where(x => x.comPEMain == comPEMain).List<Com_PartOperation>();

                bool IsComplete = true;
                foreach (Com_PartOperation i in listComPartOperation)
                {
                    IList<Com_MEMain> comMEMain = session.QueryOver<Com_MEMain>()
                                                         .Where(x => x.comPartOperation == i).List<Com_MEMain>();
                    if (comMEMain.Count > 0)
                        continue;
                    else
                    {
                        IsComplete = false;
                        break;
                    }
                }

                if (IsComplete)
                {
                    o = new object[] { false, "Control Plan", comPEMain.customerVer, comPEMain.opVer
                                                , string.Format("{0}_{1}_{2}資料夾"
                                                , comPEMain.partName
                                                , comPEMain.customerVer
                                                , comPEMain.opVer) };
                    PEPanel.Rows.Add(new GridRow(o));
                    PEPanel.GetCell(1, 0).Value = false;
                }
                */
                #endregion

                #region 插入Control Plan
                o = new object[] { false, "Control Plan", comPEMain.customerVer, comPEMain.opVer
                                                        , string.Format("{0}_{1}_{2}資料夾"
                                                        , comPEMain.partName
                                                        , comPEMain.customerVer
                                                        , comPEMain.opVer) };
                PEPanel.Rows.Add(new GridRow(o));
                PEPanel.GetCell(1, 0).Value = false;
                #endregion

            }
            catch (System.Exception ex)
            {
                return false;
            }
            return true;
        }
        public static bool SetMEPanelData(Com_PartOperation comPartOperation, string cus, string partNo, string cusVer, string opVer, string op1, ref GridPanel MEPanel)
        {
            try
            {
                Com_MEMain comMEMain = session.QueryOver<Com_MEMain>()
                                              .Where(x => x.comPartOperation == comPartOperation).SingleOrDefault<Com_MEMain>();

                IList<Com_Dimension> listComDimension = session.QueryOver<Com_Dimension>()
                                              .Where(x => x.comMEMain == comMEMain).List<Com_Dimension>();

                int MECount = -1;
                foreach (Com_Dimension i in listComDimension)
                {
                    //2017.02.14判斷是否已經有插入過
                    bool IsExist = false;
                    for (int y = 0; y < MEPanel.Rows.Count;y++ )
                    {
                        if (i.excelType == MEPanel.GetCell(y, 1).Value.ToString())
                            IsExist = true;
                    }
                    if (IsExist)
                        continue;

                    MECount++;
                    //由excelType取得廠區專用的Excel路徑
                    List<string> ExcelData = new List<string>();
                    status = GetExcelForm.GetMEExcelForm(i.excelType, out ExcelData);
                    if (!status)
                        return false;

                    object[] o = new object[] { false, i.excelType, i.draftingVer, ""
                                                , string.Format("{0}_{1}_{2}資料夾"
                                                , partNo
                                                , cusVer
                                                , opVer) };
                    MEPanel.Rows.Add(new GridRow(o));
                    MEPanel.GetCell(MECount, 0).Value = false;
                    MEPanel.GetCell(MECount, 3).EditorType = typeof(GridComboBoxExEditControl);
                    GridComboBoxExEditControl singleCell = MEPanel.GetCell(MECount, 3).EditControl as GridComboBoxExEditControl;
                    //singleCell.Items.Add("");
                    foreach (string tempStr in ExcelData)
                    {
                        singleCell.Items.Add(tempStr);
                    }


                    if (singleCell.Items.Count == 1)
                    {
                        MEPanel.GetCell(MECount, 3).Value = singleCell.Items[0].ToString();
                    }
                    else
                    {
                        MEPanel.GetCell(MECount, 3).Value = "(雙擊)選擇表單";
                    }
                }

                #region 找OIS資料夾，並插入Panel
                //string OISFolderPath = string.Format(@"{0}\{1}\{2}\{3}\{4}\{5}\{6}", OutputForm.EnvVariables.env + "\\Task", cus, partNo, cusVer, opVer, "OP" + op1, "OIS");
                string OISFolderPath = string.Format(@"{0}\{1}\{2}\{3}\{4}\{5}\{6}", CaxEnv.GetGlobaltekEnvDir() + "\\Task", cus, partNo, cusVer, opVer, "OP" + op1, "OIS");
                string[] OISFolder = Directory.GetFileSystemEntries(OISFolderPath, "*.pdf");
                foreach (string item in OISFolder)
                {
                    if (!item.Contains(".pdf"))
                    {
                        continue;
                    }
                    MECount++;
                    object[] o = new object[] { false, "PDF", Path.GetFileNameWithoutExtension(item), "", string.Format("{0}_{1}_{2}資料夾", partNo, cusVer, opVer) };
                    MEPanel.Rows.Add(new GridRow(o));
                }
                #endregion                
            }
            catch (System.Exception ex)
            {
                MessageBox.Show(ex.ToString());
                return false;
            }
            return true;
        }
        public static bool SetTEPanelData(Com_PartOperation comPartOperation, string cus, string partNo, string cusVer, string opVer, string op1, ref GridPanel TEPanel)
        {
            try
            {
                List<string> ExcelData = new List<string>();
                GridComboBoxExEditControl singleCell;
                IList<Com_TEMain> comTEMain = session.QueryOver<Com_TEMain>()
                                              .Where(x => x.comPartOperation == comPartOperation).List<Com_TEMain>();
                int TECount = -1;
                foreach (Com_TEMain i in comTEMain)
                {
                    #region 由teExcelSrNo取得對應的ExcelType
                    Sys_TEExcel sysTEExcel = session.QueryOver<Sys_TEExcel>()
                                             .Where(x => x.teExcelSrNo == i.sysTEExcel.teExcelSrNo).SingleOrDefault<Sys_TEExcel>();
                    ExcelData = new List<string>();
                    status = GetExcelForm.GetTEExcelForm(sysTEExcel.teExcelType, out ExcelData);
                    if (!status)
                    {
                        return false;
                    }
                    #endregion

                    #region 插入Panel
                    TECount++;
                    object[] o = new object[] { false, sysTEExcel.teExcelType, i.ncGroupName, ""
                                                , string.Format("{0}_{1}_{2}資料夾"
                                                , partNo
                                                , cusVer
                                                , opVer) };
                    TEPanel.Rows.Add(new GridRow(o));
                    TEPanel.GetCell(TECount, 0).Value = false;
                    TEPanel.GetCell(TECount, 3).EditorType = typeof(GridComboBoxExEditControl);
                    singleCell = TEPanel.GetCell(TECount, 3).EditControl as GridComboBoxExEditControl;
                    //singleCell.Items.Add("");
                    foreach (string tempStr in ExcelData)
                    {
                        singleCell.Items.Add(tempStr);
                    }

                    if (singleCell.Items.Count == 1)
                    {
                        TEPanel.GetCell(TECount, 3).Value = singleCell.Items[0].ToString();
                    }
                    else
                    {
                        TEPanel.GetCell(TECount, 3).Value = "(雙擊)選擇表單";
                    }
                    #endregion
                }

                #region 取得ToolList的表單格式
                ExcelData = new List<string>();
                status = GetExcelForm.GetTEExcelForm("ToolList", out ExcelData);
                if (!status)
                {
                    return false;
                }
                #endregion

                foreach (Com_TEMain i in comTEMain)
                {
                    #region 找ToolList資料，並插入Panel
                    IList<Com_ToolList> comToolList = session.QueryOver<Com_ToolList>().Where(x => x.comTEMain == i).List();
                    if (comToolList.Count == 0)
                    {
                        continue;
                    }
                    else
                    {
                        TECount++;
                        object[] o = new object[] { false, "ToolList", i.ncGroupName, ""
                                                , string.Format("{0}_{1}_{2}資料夾"
                                                , partNo
                                                , cusVer
                                                , opVer) };
                        TEPanel.Rows.Add(new GridRow(o));
                        TEPanel.GetCell(TECount, 0).Value = false;
                        TEPanel.GetCell(TECount, 3).EditorType = typeof(GridComboBoxExEditControl);
                        singleCell = TEPanel.GetCell(TECount, 3).EditControl as GridComboBoxExEditControl;
                        //singleCell.Items.Add("");
                        foreach (string tempStr in ExcelData)
                        {
                            singleCell.Items.Add(tempStr);
                        }

                        if (singleCell.Items.Count == 1)
                        {
                            TEPanel.GetCell(TECount, 3).Value = singleCell.Items[0].ToString();
                        }
                        else
                        {
                            TEPanel.GetCell(TECount, 3).Value = "(雙擊)選擇表單";
                        }
                        
                    }
                    #endregion
                }


                #region 找NC資料夾，並插入Panel
                //string CAMFolderPath = string.Format(@"{0}\{1}\{2}\{3}\{4}\{5}\{6}", OutputForm.EnvVariables.env + "\\Task", cus, partNo, cusVer, opVer, "OP" + op1, "CAM");
                string CAMFolderPath = string.Format(@"{0}\{1}\{2}\{3}\{4}\{5}\{6}", CaxEnv.GetGlobaltekEnvDir() + "\\Task", cus, partNo, cusVer, opVer, "OP" + op1, "CAM");
                string[] NCFolder = Directory.GetDirectories(CAMFolderPath);
                foreach (string item in NCFolder)
                {
                    if (!item.Contains("NC"))
                    {
                        continue;
                    }
                    TECount++;
                    object[] o = new object[] { false, "NC程式", Path.GetFileNameWithoutExtension(item), ""
                                                , string.Format("{0}_{1}_{2}資料夾"
                                                , partNo
                                                , cusVer
                                                , opVer) };
                    TEPanel.Rows.Add(new GridRow(o));
                }
                #endregion
                    
                

            }
            catch (System.Exception ex)
            {
                MessageBox.Show(ex.ToString());
                return false;
            }
            return true;
        }
        public static bool SetFixInsPanelData(Com_PartOperation comPartOperation, string cus, string partNo, string cusVer, string opVer, string op1, ref GridPanel FixInsPanel)
        {
            try
            {
                IList<Com_FixInspection> listComFixIns = session.QueryOver<Com_FixInspection>().Where(x => x.comPartOperation == comPartOperation).List();
                int FixInsCount = -1;
                foreach (Com_FixInspection i in listComFixIns)
                {
                    FixInsCount++;
                    object[] o = new object[] { false, i.fixPartName, i.fixinsDescription, i.fixinsNo, i.fixinsERP};
                    FixInsPanel.Rows.Add(new GridRow(o));
                    FixInsPanel.GetCell(FixInsCount, 0).Value = false;
                }
            }
            catch (System.Exception ex)
            {
                return false;
            }
            return true;
        }
        public static bool GetPFDData(Com_PEMain comPEMain, out Dictionary<DB_PEMain, List<DB_PartOperation>> DicPFDData)
        {
            DicPFDData = new Dictionary<DB_PEMain, List<DB_PartOperation>>();
            try
            {
                for (int i = 0; i < OutputForm.PEPanel.Rows.Count; i++)
                {
                    if (((bool)OutputForm.PEPanel.GetCell(i, 0).Value) == false)
                        continue;

                    if (OutputForm.PEPanel.GetCell(i, 1).Value.ToString() != "PFD")
                        continue;

                    DB_PEMain sDB_PEMain = new DB_PEMain();
                    sDB_PEMain.PartNo = comPEMain.partName;
                    sDB_PEMain.CusVer = comPEMain.customerVer;
                    sDB_PEMain.OpVer = comPEMain.opVer;
                    //sDB_PEMain.excelTemplateFilePath = string.Format(@"{0}\{1}\{2}\{3}\{4}.xls"
                    //                                                    , OutputForm.EnvVariables.env
                    //                                                    , "PE_Config"
                    //                                                    , "Config"
                    //                                                    , "PFD"
                    //                                                    , "PFD");
                    sDB_PEMain.excelTemplateFilePath = string.Format(@"{0}\{1}\{2}\{3}\{4}.xls"
                                                                        , CaxEnv.GetGlobaltekEnvDir()
                                                                        , "PE_Config"
                                                                        , "Config"
                                                                        , "PFD"
                                                                        , "PFD");
                    //sDB_PEMain.excelTemplateFilePath = string.Format(@"{0}\{1}\{2}\{3}\{4}\{5}\{6}.xls"
                    //                                                    , "\\\\192.168.35.1"
                    //                                                    , "cax"
                    //                                                    , "Globaltek"
                    //                                                    , "PE_Config"
                    //                                                    , "Config"
                    //                                                    , "PFD"
                    //                                                    , "PFD");

                    IList<Com_PartOperation> listComPartOperation = session.QueryOver<Com_PartOperation>()
                                                                    .Where(x => x.comPEMain == comPEMain)
                                                                    .OrderBy(x => x.operation1).Asc
                                                                    .List<Com_PartOperation>();

                    List<DB_PartOperation> listDBPartOperation = new List<DB_PartOperation>();
                    foreach (Com_PartOperation j in listComPartOperation)
                    {
                        DB_PartOperation sDB_PartOperation = new DB_PartOperation();
                        sDB_PartOperation.Op1 = j.operation1;
                        sDB_PartOperation.Op2 = j.sysOperation2.operation2Name;
                        sDB_PartOperation.Form = j.form;
                        listDBPartOperation.Add(sDB_PartOperation);
                    }
                    DicPFDData.Add(sDB_PEMain, listDBPartOperation);
                }
            }
            catch (System.Exception ex)
            {
                return false;
            }
            return true;
        }
        public static bool GetDimensionData(ComboBoxEx Op1Combobox, out Dictionary<DB_MEMain, IList<Com_Dimension>> DicDimensionData)
        {
            DicDimensionData = new Dictionary<DB_MEMain, IList<Com_Dimension>>();
            try
            {
                for (int i = 0; i < OutputForm.MEPanel.Rows.Count; i++)
                {
                    if (((bool)OutputForm.MEPanel.GetCell(i, 0).Value) == false || OutputForm.MEPanel.GetCell(i, 1).Value.ToString() == "PDF")
                        continue;

                    Com_MEMain comMEMain = session.QueryOver<Com_MEMain>()
                                           .Where(x => x.comPartOperation == (Com_PartOperation)Op1Combobox.SelectedItem)
                                           .Where(x => x.draftingVer == OutputForm.MEPanel.GetCell(i, 2).Value.ToString())
                                           .SingleOrDefault<Com_MEMain>();
                    IList<Com_Dimension> listComDimension = session.QueryOver<Com_Dimension>()
                                                        .Where(x => x.comMEMain == comMEMain)
                                                        .Where(x => x.excelType == OutputForm.MEPanel.GetCell(i, 1).Value.ToString())
                                                        .OrderBy(x => x.ballon).Asc
                                                        .List<Com_Dimension>();

                    //填key的值
                    DB_MEMain sDB_MEMain = new DB_MEMain();
                    sDB_MEMain.comMEMain = comMEMain;
                    sDB_MEMain.excelType = OutputForm.MEPanel.GetCell(i, 1).Value.ToString();
                    sDB_MEMain.excelTemplateFilePath = string.Format(@"{0}\{1}\{2}\{3}\{4}.xls"
                                                                        , CaxEnv.GetGlobaltekEnvDir()
                                                                        , "ME_Config"
                                                                        , "Config"
                                                                        , OutputForm.MEPanel.GetCell(i, 1).Value.ToString()
                                                                        , OutputForm.MEPanel.GetCell(i, 3).Value.ToString());
                    sDB_MEMain.factory = OutputForm.MEPanel.GetCell(i, 3).Value.ToString();

                    //填值
                    DicDimensionData.Add(sDB_MEMain, listComDimension);

                    /*
                    DB_MEMain sDB_MEMain = new DB_MEMain();
                    Sys_MEExcel meExcelSrNo = session.QueryOver<Sys_MEExcel>()
                                              .Where(x => x.meExcelType == OutputForm.MEPanel.GetCell(i, 1).Value.ToString())
                                              .SingleOrDefault<Sys_MEExcel>();
                    if (meExcelSrNo == null)
                    {
                        continue;
                    }
                    Com_MEMain comMEMain = session.QueryOver<Com_MEMain>()
                                           .Where(x => x.comPartOperation == (Com_PartOperation)Op1Combobox.SelectedItem)
                                           .Where(x => x.sysMEExcel == meExcelSrNo)
                                           .Where(x => x.draftingVer == OutputForm.MEPanel.GetCell(i, 2).Value.ToString())
                                           .SingleOrDefault<Com_MEMain>();

                    IList<Com_Dimension> comDimension = session.QueryOver<Com_Dimension>()
                                                        .Where(x => x.comMEMain == comMEMain)
                                                        .OrderBy(x => x.ballon).Asc
                                                        .List<Com_Dimension>();
                    sDB_MEMain.comMEMain = comMEMain;
                    sDB_MEMain.excelTemplateFilePath = string.Format(@"{0}\{1}\{2}\{3}\{4}\{5}\{6}.xls"
                                                                        , "\\\\192.168.31.55"
                                                                        , "cax"
                                                                        , "Globaltek"
                                                                        , "ME_Config"
                                                                        , "Config"
                                                                        , OutputForm.MEPanel.GetCell(i, 1).Value.ToString()
                                                                        , OutputForm.MEPanel.GetCell(i, 3).Value.ToString());
                    sDB_MEMain.factory = OutputForm.MEPanel.GetCell(i, 3).Value.ToString();
                    DicDimensionData.Add(sDB_MEMain, comDimension);
                    */
                }
            }
            catch (System.Exception ex)
            {
                MessageBox.Show(ex.ToString());
                return false;
            }
            return true;
        }
        public static bool GetShopDocData(ComboBoxEx Op1Combobox, out Dictionary<DB_TEMain, IList<Com_ShopDoc>> DicShopDocData)
        {
            DicShopDocData = new Dictionary<DB_TEMain, IList<Com_ShopDoc>>();
            try
            {
                for (int i = 0; i < OutputForm.TEPanel.Rows.Count; i++)
                {
                    if (((bool)OutputForm.TEPanel.GetCell(i, 0).Value) == false || OutputForm.TEPanel.GetCell(i, 1).Value.ToString() != "ShopDoc")
                    {
                        continue;
                    }
                    
                    Sys_TEExcel teExcelSrNo = session.QueryOver<Sys_TEExcel>()
                                              .Where(x => x.teExcelType == OutputForm.TEPanel.GetCell(i, 1).Value.ToString())
                                              .SingleOrDefault<Sys_TEExcel>();
                    if (teExcelSrNo == null)
                    {
                        continue;
                    }
                    Com_TEMain comTEMain = session.QueryOver<Com_TEMain>()
                                                  .Where(x => x.comPartOperation == (Com_PartOperation)Op1Combobox.SelectedItem)
                                                  .Where(x => x.sysTEExcel == teExcelSrNo)
                                                  .Where(x => x.ncGroupName == OutputForm.TEPanel.GetCell(i, 2).Value.ToString())
                                                  .SingleOrDefault<Com_TEMain>();

                    DB_TEMain sDB_TEMain = new DB_TEMain();
                    IList<Com_ShopDoc> comDimension = session.QueryOver<Com_ShopDoc>()
                                                        .Where(x => x.comTEMain == comTEMain)
                                                        .List<Com_ShopDoc>();
                    sDB_TEMain.comTEMain = comTEMain;
                    sDB_TEMain.excelTemplateFilePath = string.Format(@"{0}\{1}\{2}\{3}\{4}.xls"
                                                                        , CaxEnv.GetGlobaltekEnvDir()
                                                                        , "TE_Config"
                                                                        , "Config"
                                                                        , OutputForm.TEPanel.GetCell(i, 1).Value.ToString()
                                                                        , OutputForm.TEPanel.GetCell(i, 3).Value.ToString());
                    sDB_TEMain.ncGroupName = comTEMain.ncGroupName;
                    sDB_TEMain.factory = OutputForm.TEPanel.GetCell(i, 3).Value.ToString();
                    sDB_TEMain.partDesc = (session.QueryOver<Com_PEMain>()
                                                    .Where(x => x.peSrNo == ((Com_PartOperation)Op1Combobox.SelectedItem).comPEMain.peSrNo)
                                                    .SingleOrDefault<Com_PEMain>()).partDes;
                    DicShopDocData.Add(sDB_TEMain, comDimension);
                    
                    
                    //sDB_TEMain.comTEMain = comTEMain;
                    //sDB_TEMain.excelTemplateFilePath = string.Format(@"{0}\{1}.xls", OutputForm.serverTEConfig, OutputForm.TEPanel.GetCell(i, 3).Value.ToString());
                    //sDB_TEMain.factory = OutputForm.TEPanel.GetCell(i, 3).Value.ToString();
                    //DicShopDocData.Add(sDB_TEMain, comDimension);
                }
            }
            catch (System.Exception ex)
            {
                MessageBox.Show(ex.ToString());
                return false;
            }
            return true;
        }
        public static bool GetToolListData(ComboBoxEx Op1Combobox, out Dictionary<DB_TEMain, IList<Com_ToolList>> DicToolListData)
        {
            DicToolListData = new Dictionary<DB_TEMain, IList<Com_ToolList>>();
            try
            {
                for (int i = 0; i < OutputForm.TEPanel.Rows.Count; i++)
                {
                    if (((bool)OutputForm.TEPanel.GetCell(i, 0).Value) == false || OutputForm.TEPanel.GetCell(i, 1).Value.ToString() != "ToolList")
                    {
                        continue;
                    }

                    Com_TEMain comTEMain = session.QueryOver<Com_TEMain>()
                                                  .Where(x => x.comPartOperation == (Com_PartOperation)Op1Combobox.SelectedItem)
                                                  .Where(x => x.ncGroupName == OutputForm.TEPanel.GetCell(i, 2).Value.ToString())
                                                  .SingleOrDefault<Com_TEMain>();

                    DB_TEMain sDB_TEMain = new DB_TEMain();
                    IList<Com_ToolList> comToolList = session.QueryOver<Com_ToolList>()
                                                        .Where(x => x.comTEMain == comTEMain)
                                                        .List();
                    sDB_TEMain.comTEMain = comTEMain;
                    sDB_TEMain.excelTemplateFilePath = string.Format(@"{0}\{1}\{2}\{3}\{4}.xls"
                                                                        , CaxEnv.GetGlobaltekEnvDir()
                                                                        , "TE_Config"
                                                                        , "Config"
                                                                        , OutputForm.TEPanel.GetCell(i, 1).Value.ToString()
                                                                        , OutputForm.TEPanel.GetCell(i, 3).Value.ToString());
                    sDB_TEMain.ncGroupName = comTEMain.ncGroupName;
                    sDB_TEMain.factory = OutputForm.TEPanel.GetCell(i, 3).Value.ToString();
                    sDB_TEMain.partDesc = (session.QueryOver<Com_PEMain>()
                                                    .Where(x => x.peSrNo == ((Com_PartOperation)Op1Combobox.SelectedItem).comPEMain.peSrNo)
                                                    .SingleOrDefault<Com_PEMain>()).partDes;
                    DicToolListData.Add(sDB_TEMain, comToolList);

                }
            }
            catch (System.Exception ex)
            {
                MessageBox.Show(ex.ToString());
                return false;
            }
            return true;
        }
        public static bool GetControlPlanData(Com_PEMain comPEMain, out Dictionary<DB_CPKey, List<DB_CPValue>> DicCPData)
        {
            DicCPData = new Dictionary<DB_CPKey, List<DB_CPValue>>();
            try
            {
                bool IsCP = false;
                for (int i = 0; i < OutputForm.PEPanel.Rows.Count; i++)
                {
                    if (((bool)OutputForm.PEPanel.GetCell(i, 0).Value) == false || OutputForm.PEPanel.GetCell(i, 1).Value.ToString() != "Control Plan")
                        continue;

                    IsCP = true;
                }

                if (!IsCP)
                    return true;

                IList<Com_PartOperation> listComPartOperation = session.QueryOver<Com_PartOperation>()
                                                                    .Where(x => x.comPEMain == comPEMain)
                                                                    .OrderBy(x => x.operation1).Asc
                                                                    .List<Com_PartOperation>();

                foreach (Com_PartOperation i in listComPartOperation)
                {
                    DB_CPKey sDB_CPKey = new DB_CPKey();
                    sDB_CPKey.PartNo = comPEMain.partName;
                    sDB_CPKey.CusVer = comPEMain.customerVer;
                    sDB_CPKey.OpVer = comPEMain.opVer;
                    sDB_CPKey.PartDesc = comPEMain.partDes;
                    sDB_CPKey.excelTemplateFilePath = string.Format(@"{0}\{1}\{2}\{3}\{4}.xls"
                                                                    //, OutputForm.EnvVariables.env
                                                                    , CaxEnv.GetGlobaltekEnvDir()
                                                                    , "PE_Config"
                                                                    , "Config"
                                                                    , "ControlPlan"
                                                                    , "ControlPlan");

                    DB_CPValue sDB_CPValue = new DB_CPValue();
                    sDB_CPValue.Op1 = i.operation1;
                    sDB_CPValue.Op2 = session.QueryOver<Sys_Operation2>().Where(x => x.operation2SrNo == i.sysOperation2.operation2SrNo)
                                                                    .SingleOrDefault().operation2Name;
                    Com_MEMain comMEMain = session.QueryOver<Com_MEMain>().Where(x => x.comPartOperation == i).SingleOrDefault<Com_MEMain>();
                    //過濾製程中有外包的尺寸，不可顯示在CP上避免客戶稽核，EX：250有外包則回來的IQC不能顯示在CP上
                    sDB_CPValue.comDimension = session.QueryOver<Com_Dimension>()
                                                .Where(x => x.comMEMain == comMEMain)
                                                .OrderBy(x => x.ballon).Asc.List<Com_Dimension>();
                    foreach (var o in sDB_CPValue.comDimension.ToArray())
                    {
                        if (sDB_CPValue.Op1 != "001" & o.excelType == "IQC") 
                            sDB_CPValue.comDimension.Remove(o);
                    }
                    
                    Dictionary<int, IList<Com_Dimension>> DicBalloonData = new Dictionary<int, IList<Com_Dimension>>();
                    foreach (Com_Dimension ii in sDB_CPValue.comDimension)
                    {
                        IList<Com_Dimension> dimension = new List<Com_Dimension>();
                        status = DicBalloonData.TryGetValue(ii.ballon, out dimension);
                        if (!status)
                        {
                            dimension = new List<Com_Dimension>();
                            dimension.Add(ii);
                            DicBalloonData.Add(ii.ballon, dimension);
                        }
                        else
                        {
                            dimension.Add(ii);
                            DicBalloonData[ii.ballon] = dimension;
                        }
                    }
                    sDB_CPValue.DicBalloonData = DicBalloonData;
                    


                    List<DB_CPValue> listDB_CPValue = new List<DB_CPValue>();
                    status = DicCPData.TryGetValue(sDB_CPKey, out listDB_CPValue);
                    if (!status)
                    {
                        listDB_CPValue = new List<DB_CPValue>();
                        listDB_CPValue.Add(sDB_CPValue);
                        DicCPData.Add(sDB_CPKey, listDB_CPValue);
                    }
                    else
                    {
                        listDB_CPValue.Add(sDB_CPValue);
                        DicCPData[sDB_CPKey] = listDB_CPValue;
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
        public static bool GetFixInsData(ComboBoxEx Op1Combobox, string fixPartName, out Dictionary<DB_FixInspection, IList<Com_FixDimension>> DicFixDimensionData)
        {
            DicFixDimensionData = new Dictionary<DB_FixInspection, IList<Com_FixDimension>>();
            try
            {
                Com_FixInspection comFixInspection = session.QueryOver<Com_FixInspection>()
                                                        .Where(x => x.comPartOperation == (Com_PartOperation)Op1Combobox.SelectedItem)
                                                        .And(x => x.fixPartName == fixPartName).SingleOrDefault();
                IList<Com_FixDimension> listComFixDimension = session.QueryOver<Com_FixDimension>()
                    .Where(x => x.comFixInspection == comFixInspection).OrderBy(x => x.ballon).Asc.List();

                DB_FixInspection sDB_FixInspection = new DB_FixInspection();
                sDB_FixInspection.comFixInspection = comFixInspection;
                sDB_FixInspection.excelTemplateFilePath = string.Format(@"{0}\{1}\{2}\{3}\{4}.xls"
                                                                        , CaxEnv.GetGlobaltekEnvDir()
                                                                        , "ME_Config"
                                                                        , "Config"
                                                                        , "FixtureInspection"
                                                                        , "FixtureInspection");
                DicFixDimensionData.Add(sDB_FixInspection, listComFixDimension);
            }
            catch (System.Exception ex)
            {
                return false;
            }
            return true;
        }
    }
}
