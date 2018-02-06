using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Windows.Forms;
using DevComponents.DotNetBar;
using NXOpen;
using NXOpen.UF;
using CaxGlobaltek;
using NHibernate;
using NXOpen.Utilities;
using System.IO;
using NXOpen.Annotations;

namespace AssignGauge
{
    public partial class AssignDlg : Office2007Form
    {
        public static Session theSession = Session.GetSession();
        public static UI theUI;
        public static UFSession theUfSession = UFSession.GetUFSession();
        public static Part workPart = theSession.Parts.Work;
        public static Part displayPart = theSession.Parts.Display;
        
        public string Is_Local = "", filePath = "";
        public static bool status;
        public string[] AGData = new string[] { };
        public static CoordinateData cCoordinateData = new CoordinateData();
        public static List<string> listProduct = new List<string>();
        public static Dictionary<string, GaugeData> DicGaugeData = new Dictionary<string, GaugeData>();
        //public static double SheetLength = 0.0, SheetHeight = 0.0;
        public static NXObject[] SelDimensionAry = new NXObject[]{};
        public static Dictionary<NXObject, string> DicSelDimension = new Dictionary<NXObject, string>();
        public static NXOpen.Drawings.DrawingSheet drawingSheet1;
        public class GaugeData
        {
            public string Color { get; set; }
            public string EngName { get; set; }
        }

        public AssignDlg()
        {
            InitializeComponent();
        }

        private void AssignDlg_Load(object sender, EventArgs e)
        {
            try
            {
                #region 模組檢查
                int module_id;
                theUfSession.UF.AskApplicationModule(out module_id);
                if (module_id != UFConstants.UF_APP_DRAFTING)
                {
                    MessageBox.Show("請先轉換為製圖(Drafting)模組後再執行！");
                    this.Close();
                }
                #endregion

                #region 系統環境
                Is_Local = Environment.GetEnvironmentVariable("UGII_ENV_FILE");
                if (Is_Local != null)
                {
                    ISession session = MyHibernateHelper.SessionFactory.OpenSession();

                    //取得AssignGaugeData
                    IList<Sys_Instrument> ListSysInstrument = session.QueryOver<Sys_Instrument>().List();

                    //填檢具到下拉選單中
                    Gauge.Items.Add("");
                    foreach (Sys_Instrument i in ListSysInstrument)
                    {
                        GaugeData cGaugeData = new GaugeData();
                        status = DicGaugeData.TryGetValue(i.instrumentName, out cGaugeData);
                        if (status)
                        {
                            continue;
                        }

                        cGaugeData = new GaugeData();
                        cGaugeData.Color = i.instrumentColor;
                        try
                        {
                            cGaugeData.EngName = i.instrumentEngName;
                        }
                        catch (System.Exception ex)
                        {
                            cGaugeData.EngName = "";
                        }
                        DicGaugeData.Add(i.instrumentName, cGaugeData);
                    }
                    Gauge.Items.AddRange(DicGaugeData.Keys.ToArray());
                    //填檢具到SelfCheck下拉選單中
                    SelfCheck_Gauge.Items.Add("");
                    foreach (KeyValuePair<string, GaugeData> kvp in DicGaugeData)
                    {
                        if (kvp.Key.Contains("T"))
                            continue;

                        SelfCheck_Gauge.Items.Add(kvp.Key);
                    }
                    //status = CaxGetDatData.GetAssignGaugeData(out AGData);
                    //if (!status)
                    //{
                    //    MessageBox.Show("GetAssignGaugeData失敗，請檢查MEConfig是否有檔案");
                    //    return;
                    //}

                    //取得圖紙範圍資料Data
                    status = CaxGetDatData.GetDraftingCoordinateData(out cCoordinateData);

                    //取得PRODUCT資料(未完成，資料庫還沒建立)
                    
                    IList<Sys_Product> sysProduct = session.QueryOver<Sys_Product>().List<Sys_Product>();
                    foreach (Sys_Product i in sysProduct)
                    {
                        listProduct.Add(i.productName);
                    }
                }
                else
                {
                    //取得AssignGaugeData
                    string AssignGaugeData_dat = "AssignGaugeData.dat";
                    string AssignGaugeData_Path = string.Format(@"{0}\{1}", "D:", AssignGaugeData_dat);
                    AGData = System.IO.File.ReadAllLines(AssignGaugeData_Path);

                    #region 存AGData到DicGaugeData中
                    foreach (string Row in AGData)
                    {
                        string[] splitRow = Row.Split(',');
                        if (splitRow.Length == 0)
                        {
                            continue;
                        }

                        GaugeData cGaugeData = new GaugeData();
                        status = DicGaugeData.TryGetValue(splitRow[1], out cGaugeData);
                        if (status)
                        {
                            continue;
                        }

                        cGaugeData = new GaugeData();
                        cGaugeData.Color = splitRow[0];
                        try
                        {
                            cGaugeData.EngName = splitRow[2];
                        }
                        catch (System.Exception ex)
                        {
                            cGaugeData.EngName = "";
                        }
                        DicGaugeData.Add(splitRow[1], cGaugeData);
                    }
                    #endregion

                    #region 初始化AssignGauge
                    //填檢具到下拉選單中
                    Gauge.Items.Add("");
                    Gauge.Items.AddRange(DicGaugeData.Keys.ToArray());
                    //填檢具到SelfCheck下拉選單中
                    SelfCheck_Gauge.Items.Add("");
                    foreach (KeyValuePair<string, GaugeData> kvp in DicGaugeData)
                    {
                        if (kvp.Key.Contains("T"))
                            continue;

                        SelfCheck_Gauge.Items.Add(kvp.Key);
                    }
                    #endregion

                    //取得圖紙範圍資料Data
                    string DraftingCoordinate_dat = "DraftingCoordinate.dat";
                    string DraftingCoordinate_Path = string.Format(@"{0}\{1}", "D:", DraftingCoordinate_dat);
                    if (!System.IO.File.Exists(DraftingCoordinate_Path))
                    {
                        MessageBox.Show("路徑：" + DraftingCoordinate_Path + "不存在");
                        return;
                    }

                    CaxPublic.ReadCoordinateData(DraftingCoordinate_Path, out cCoordinateData);
                }
                #endregion

                #region 初始化控制件
                IQC_Freq_0.Enabled = false; IQC_Freq_1.Enabled = false; IQC_Units.Enabled = false; IQC_UDefine.Enabled = false;
                //IPQC_Freq_0.Enabled = false; IPQC_Freq_1.Enabled = false; IPQC_Units.Enabled = false; IPQC_UDefine.Enabled = false;
                FAI_Freq_0.Enabled = false; FAI_Freq_1.Enabled = false; FAI_Units.Enabled = false; FAI_UDefine.Enabled = false;
                FQC_Freq_0.Enabled = false; FQC_Freq_1.Enabled = false; FQC_Units.Enabled = false; FQC_UDefine.Enabled = false;
                SelfCheck_Gauge.Enabled = false; SelfCheck_Freq_0.Enabled = false; SelfCheck_Freq_1.Enabled = false; SelfCheck_Units.Enabled = false;
                SameData.Enabled = false;
                DiCount.Text = "1";
                #endregion

                #region 取得sheet並填入下拉選單中
                int SheetCount = 0;
                NXOpen.Tag[] SheetTagAry = null;
                theUfSession.Draw.AskDrawings(out SheetCount, out SheetTagAry);
                for (int i = 0; i < SheetCount; i++)
                {
                    NXOpen.Drawings.DrawingSheet CurrentSheet = (NXOpen.Drawings.DrawingSheet)NXObjectManager.Get(SheetTagAry[i]);
                    ListSheet.Items.Add(CurrentSheet.Name);
                }
                //預設開啟sheet1圖紙
                NXOpen.Drawings.DrawingSheet DefaultSheet = (NXOpen.Drawings.DrawingSheet)NXObjectManager.Get(SheetTagAry[0]);
                ListSheet.Text = DefaultSheet.Name;
                //取得圖紙長寬
                //SheetLength = DefaultSheet.Length;
                //SheetHeight = DefaultSheet.Height;
                #endregion

                #region 填入單位
                default_Units.Items.AddRange(new string[] { "", "Lot", "HRS", "PCS", "100%" });
                IQC_Units.Items.AddRange(new string[] { "", "Lot", "HRS", "PCS", "100%" });
                IPQC_Units.Items.AddRange(new string[] { "", "Lot", "HRS", "PCS", "100%" });
                FAI_Units.Items.AddRange(new string[] { "", "Lot", "HRS", "PCS", "100%" });
                FQC_Units.Items.AddRange(new string[] { "", "Lot", "HRS", "PCS", "100%" });
                SelfCheck_Units.Items.AddRange(new string[] { "", "Lot", "HRS", "PCS", "100%" });
                #endregion

                #region 初始化Key Chara.
                KCText.Enabled = false;
                KC_SelPic.Enabled = false;
                pictureBox1.Image = null;
                #endregion

                #region 初始化Product
                ProductData.Items.Add("");
                foreach (string i in listProduct)
                {
                    ProductData.Items.Add(i);
                }
                ProductData.Text = "選擇尺寸類型";
                #endregion

            }
            catch (System.Exception ex)
            {
                MessageBox.Show("載入對話框失敗");
                this.Close();
            }
        }

        private void selDimen_Click(object sender, EventArgs e)
        {
            try
            {
                this.Hide();
                CaxPublic.SelectObjects(out SelDimensionAry);
                DicSelDimension = new Dictionary<NXObject, string>();
                foreach (NXObject single in SelDimensionAry)
                {
                    string DimenType = single.GetType().ToString();
                    DicSelDimension.Add(single, DimenType);
                }
                selDimen.Text = string.Format("選擇尺寸({0})", SelDimensionAry.Length.ToString());
                this.Show();
            }
            catch (System.Exception ex)
            {
                MessageBox.Show("選擇尺寸的對話框載入失敗");
            }
        }

        private void OK_Click(object sender, EventArgs e)
        {
            try
            {
                if (SelDimensionAry.Length == 0)
                {
                    return;
                }

                if (TabControl.SelectedTab.Name == "AssignGauge")
                {
                    #region AssignGauge
                    //資料檢查
                    status = DataChecked();
                    if (!status)
                        return;

                    foreach (KeyValuePair<NXObject, string> kvp in DicSelDimension)
                    {
                        //取得要塞入屬性的ExcelType
                        string AssignExcelType = "";
                        status = GetExcelTypeFromDlg(kvp.Key, out AssignExcelType);
                        if (!status)
                        {
                            MessageBox.Show("取得ExcelType失敗");
                            return;
                        }

                        //取得原始顏色
                        int oldColor = CaxME.GetDimensionColor(kvp.Key);
                        if (oldColor == -1)
                        {
                            oldColor = 125;
                        }
                        
                        //取得檢具顏色
                        GaugeData cGaugeData = new GaugeData();
                        if (Gauge.Text != "")
                        {
                            status = DicGaugeData.TryGetValue(Gauge.Text, out cGaugeData);
                            if (!status)
                            {
                                CaxLog.ShowListingWindow("此檢具資料可能有誤");
                                return;
                            }
                        }

                        //改變標註尺寸顏色
                        CaxME.SetDimensionColor(kvp.Key, Convert.ToInt32(cGaugeData.Color));

                        //塞屬性
                        status = SetAllAttribute(kvp.Key, AssignExcelType, oldColor.ToString(), cGaugeData.EngName);
                        if (!status)
                        {
                            MessageBox.Show("尺寸塞屬性失敗");
                            return;
                        }
                    }

                    //對零件塞上excelType屬性，說明此次尺寸是出哪一張excel(此屬性是給MEUpload時插入資料庫所使用)
                    string excelType = "";
                    try
                    {
                        excelType = workPart.GetStringAttribute("ExcelType");
                    }
                    catch (System.Exception ex)
                    {
                        excelType = "";
                    }
                    if (chk_FAI.Checked == true)
                    {
                        if (excelType != "" && !excelType.Contains("FAI"))
                        {
                            workPart.SetAttribute("ExcelType", excelType + "," + "FAI");
                        }
                        if (excelType == "")
                        {
                            workPart.SetAttribute("ExcelType", "FAI");
                        }
                    }
                    if (chk_IQC.Checked == true)
                    {
                        if (excelType != "" && !excelType.Contains("IQC"))
                        {
                            workPart.SetAttribute("ExcelType", excelType + "," + "IQC");
                        }
                        if (excelType == "")
                        {
                            workPart.SetAttribute("ExcelType", "IQC");
                        }
                    }
                    if (chk_IPQC.Checked == true)
                    {
                        if (excelType != "" && !excelType.Contains("IPQC"))
                        {
                            workPart.SetAttribute("ExcelType", excelType + "," + "IPQC");
                        }
                        if (excelType == "")
                        {
                            workPart.SetAttribute("ExcelType", "IPQC");
                        }
                    }
                    if (chk_FQC.Checked == true)
                    {
                        if (excelType != "" && !excelType.Contains("FQC"))
                        {
                            workPart.SetAttribute("ExcelType", excelType + "," + "FQC");
                        }
                        if (excelType == "")
                        {
                            workPart.SetAttribute("ExcelType", "FQC");
                        }
                    }
                    if (SelfCheck_Gauge.Text != "")
                    {
                        if (excelType != "" && !excelType.Contains("SelfCheck"))
                        {
                            workPart.SetAttribute("ExcelType", excelType + "," + "SelfCheck");
                        }
                        if (excelType == "")
                        {
                            workPart.SetAttribute("ExcelType", "SelfCheck");
                        }
                    }
                    #endregion

                    //將尺寸數量變回1
                    DiCount.Text = "1";
                }

                else if (TabControl.SelectedTab.Name == "KeyChara")
                {
                    if (CpkBox.Text == "" || PpkBox.Text == "")
                        if (eTaskDialogResult.Yes == CaxPublic.ShowMsgYesNo("SPC Control未完成，是否返回操作?\nYes：返回。\nNo：不返回，繼續執行。"))
                            return;

                    #region KeyChara
                    if (chk_KCText.Checked == true)
                    {
                        if (KCText.Text == "")
                        {
                            MessageBox.Show("請先輸入關鍵尺寸代號");
                            return;
                        }

                        //先刪除舊的文字
                        RemoveKCNote(SelDimensionAry);

                        foreach (NXObject i in SelDimensionAry)
                        {
                            i.SetAttribute("KC", KCText.Text);
                            //插入符號文字
                            //計算符號圖片座標並插入
                            CaxME.BoxCoordinate cBoxCoordinate = new CaxME.BoxCoordinate();
                            CaxME.GetTextBoxCoordinate(i.Tag, out cBoxCoordinate);
                            CaxME.DimenData sDimenData = new CaxME.DimenData();
                            CaxME.CalculateBallonCoordinateAfter(cBoxCoordinate, out sDimenData);
                            string datetime = DateTime.Now.TimeOfDay.ToString();
                            Point3d point3d = new Point3d();
                            point3d.X = sDimenData.LocationX;
                            point3d.Y = sDimenData.LocationY;
                            point3d.Z = sDimenData.LocationZ;
                            InsertKCNote(KCText.Text, point3d, datetime);
                            i.SetAttribute(CaxME.DimenAttr.KCInsertTime, datetime);
                        }
                    }
                    if (chk_KCPic.Checked == true)
                    {
                        if (pictureBox1.Image == null)
                        {
                            MessageBox.Show("請先選擇圖片");
                            return;
                        }

                        //先刪除舊的圖片
                        RemoveKC(SelDimensionAry);

                        foreach (NXObject i in SelDimensionAry)
                        {
                            //插入符號圖片路徑屬性
                            i.SetAttribute("KC", filePath);
                            //插入符號圖片
                            //計算符號圖片座標並插入
                            CaxME.BoxCoordinate cBoxCoordinate = new CaxME.BoxCoordinate();
                            CaxME.GetTextBoxCoordinate(i.Tag, out cBoxCoordinate);
                            CaxME.DimenData sDimenData = new CaxME.DimenData();
                            CaxME.CalculateBallonCoordinateAfter(cBoxCoordinate, out sDimenData);
                            string symbolPartFile = string.Format(@"{0}\{1}", Path.GetDirectoryName(filePath), Path.GetFileNameWithoutExtension(filePath) + ".prt");
                            //插入KCinsert時間，用來刪除的時候可以判斷是哪一個KC要刪除
                            string datetime = DateTime.Now.TimeOfDay.ToString();
                            status = symbol(symbolPartFile, sDimenData, datetime);
                            if (!status)
                            {
                                continue;
                            }
                            i.SetAttribute(CaxME.DimenAttr.KCInsertTime, datetime);
                        }
                    }
                    foreach (NXObject i in SelDimensionAry)
                    {
                        if (CpkBox.Text != "" & PpkBox.Text != "")
                        {
                            i.SetAttribute("SPCControl", "Cpk > " + CpkBox.Text + " ; " + "Ppk > " + PpkBox.Text);
                        }
                    }
                    #endregion
                }

                else if (TabControl.SelectedTab.Name == "Product")
                {
                    #region Product
                    if (ProductData.Text == "選擇尺寸類型" || ProductData.Text == "")
                        return;

                    foreach (NXObject i in SelDimensionAry)
                    {
                        i.SetAttribute(CaxME.DimenAttr.Product, ProductData.Text);
                    }
                    #endregion
                }

                else if (TabControl.SelectedTab.Name == "Remove")
                {
                    NXObject[] SheetObj = CaxME.FindObjectsInView(drawingSheet1.View.Tag).ToArray();

                    #region Remove
                    if (chk_Instrument.Checked == true)
                    {
                        status = RemoveInstrument(SelDimensionAry, SheetObj);
                        if (!status)
                        {
                            MessageBox.Show("【檢具/頻率】移除失敗，請聯繫開發工程師");
                            this.Close();
                        }
                    }

                    if (chk_Product.Checked == true)
                    {
                        status = RemoveProduct(SelDimensionAry);
                        if (!status)
                        {
                            MessageBox.Show("【Product】移除失敗，請聯繫開發工程師");
                            this.Close();
                        }
                    }

                    if (chk_KC.Checked == true)
                    {
                        status = RemoveKC(SelDimensionAry);
                        if (!status)
                        {
                            MessageBox.Show("【KC】移除失敗，請聯繫開發工程師");
                            this.Close();
                        }
                    }

                    if (chk_All.Checked == true)
                    {
                        status = RemoveAll(SelDimensionAry, SheetObj);
                        if (!status)
                        {
                            MessageBox.Show("【全部屬性】移除失敗，請聯繫開發工程師");
                            this.Close();
                        }
                    }
                    #endregion
                }

                selDimen.Text = string.Format("選擇尺寸(0)");
                MessageBox.Show("設定成功");
            }
            catch (System.Exception ex)
            {
                MessageBox.Show("執行失敗，請聯繫開發工程師");
                this.Close();
            }
        }

        private bool SetAllAttribute(NXObject singleDimen, string AssignExcelType, string oldColor, string instrumentEng)
        {
            try
            {
                //塞屬性
                singleDimen.SetAttribute(CaxME.DimenAttr.DiCount, DiCount.Text);//尺寸數量
                singleDimen.SetAttribute(CaxME.DimenAttr.OldColor, oldColor);//舊顏色
                singleDimen.SetAttribute(CaxME.DimenAttr.AssignExcelType, AssignExcelType);
                singleDimen.SetAttribute(CaxME.DimenAttr.CheckLevel, CheckLevel.Text);
                if (Gauge.Text != "")
                {
                    singleDimen.SetAttribute(CaxME.DimenAttr.Gauge, Gauge.Text + "，" + instrumentEng);//檢具名稱
                }
                if (SelfCheck_Gauge.Text != "")
                {
                    GaugeData SelfCheckGaugeData = new GaugeData();
                    DicGaugeData.TryGetValue(SelfCheck_Gauge.Text, out SelfCheckGaugeData);
                    singleDimen.SetAttribute(CaxME.DimenAttr.SelfCheck_Gauge, SelfCheck_Gauge.Text + "，" + SelfCheckGaugeData.EngName);//檢具名稱
                }

                if (chk_IPQC.Checked == true)
                {
                    if (IPQC_Units.Text == "100%")
                    {
                        singleDimen.SetAttribute(CaxME.DimenAttr.IPQC_Frequency, "every piece" + "/" + IPQC_Units.Text);//頻率
                        singleDimen.SetAttribute(CaxME.DimenAttr.IPQC_Size, "every piece");
                        singleDimen.SetAttribute(CaxME.DimenAttr.IPQC_Freq, IPQC_Units.Text);
                    }
                    else
                    {
                        singleDimen.SetAttribute(CaxME.DimenAttr.IPQC_Frequency, IPQC_Freq_0.Text + "PC/" + IPQC_Freq_1.Text + IPQC_Units.Text);//頻率
                        singleDimen.SetAttribute(CaxME.DimenAttr.IPQC_Size, IPQC_Freq_0.Text + "PC");
                        singleDimen.SetAttribute(CaxME.DimenAttr.IPQC_Freq, IPQC_Freq_1.Text + IPQC_Units.Text);
                    }
                }
               
                if (chk_SelfCheck.Checked == true)
                {
                    singleDimen.SetAttribute(CaxME.DimenAttr.SelfCheckExcel, "SelfCheck");

                    if (SelfCheck_Units.Text == "100%")
                    {
                        singleDimen.SetAttribute(CaxME.DimenAttr.SelfCheck_Frequency, "every piece" + "/" + SelfCheck_Units.Text);//頻率
                        singleDimen.SetAttribute(CaxME.DimenAttr.SelfCheck_Size, "every piece");
                        singleDimen.SetAttribute(CaxME.DimenAttr.SelfCheck_Freq, SelfCheck_Units.Text);
                    }
                    else
                    {
                        singleDimen.SetAttribute(CaxME.DimenAttr.SelfCheck_Frequency, SelfCheck_Freq_0.Text + "PC/" + SelfCheck_Freq_1.Text + SelfCheck_Units.Text);//SelfCheck
                        singleDimen.SetAttribute(CaxME.DimenAttr.SelfCheck_Size, SelfCheck_Freq_0.Text + "PC");
                        singleDimen.SetAttribute(CaxME.DimenAttr.SelfCheck_Freq, SelfCheck_Freq_1.Text + SelfCheck_Units.Text);
                    }
                }
            }
            catch (System.Exception ex)
            {
                return false;
            }
            return true;
        }

        private bool GetExcelTypeFromDlg(NXObject input, out string AssignExcelType)
        {
            AssignExcelType = "";
            try
            {
                AssignExcelType = input.GetStringAttribute(CaxME.DimenAttr.AssignExcelType);
            }
            catch (System.Exception ex)
            {
                AssignExcelType = "";
            }

            string[] splitAssignExcelType = AssignExcelType.Split(',');

            if (chk_FAI.Checked == true)
            {
                if (AssignExcelType == "")
                {
                    AssignExcelType = chk_FAI.Text;
                }
                else
                {
                    if (!splitAssignExcelType.Contains("FAI"))
                    {
                        AssignExcelType = AssignExcelType + "," + chk_FAI.Text;
                    }
                }
            }

            if (chk_IQC.Checked == true)
            {
                if (AssignExcelType == "")
                {
                    AssignExcelType = chk_IQC.Text;
                }
                else
                {
                    if (!splitAssignExcelType.Contains("IQC"))
                    {
                        AssignExcelType = AssignExcelType + "," + chk_IQC.Text;
                    }
                }
            }

            if (chk_IPQC.Checked == true)
            {
                if (AssignExcelType == "")
                {
                    AssignExcelType = chk_IPQC.Text;
                }
                else
                {
                    if (!splitAssignExcelType.Contains("IPQC"))
                    {
                        AssignExcelType = AssignExcelType + "," + chk_IPQC.Text;
                    }
                }
            }

            if (chk_FQC.Checked == true)
            {
                if (AssignExcelType == "")
                {
                    AssignExcelType = chk_FQC.Text;
                }
                else
                {
                    if (!splitAssignExcelType.Contains("FQC"))
                    {
                        AssignExcelType = AssignExcelType + "," + chk_FQC.Text;
                    }
                }
            }
            return true;
        }

        private bool RemoveInstrument(NXObject[] SelDimensionAry,NXObject[] SheetDimenAry)
        {
            try
            {
                foreach (NXObject i in SelDimensionAry)
                {
                    //恢復原始顏色
                    CaxME.SetDimensionColor(i, 125);
                    //取得泡泡資訊
                    string BallonNum = "";
                    try
                    {
                        BallonNum = i.GetStringAttribute(CaxME.DimenAttr.BallonNum);
                    }
                    catch (System.Exception ex)
                    {
                        BallonNum = "";
                    }
                    if (BallonNum != "")
                    {
                        CaxME.DeleteBallon(BallonNum, SheetDimenAry);
                    }
                    i.DeleteAttributeByTypeAndTitle(NXObject.AttributeType.String, CaxME.DimenAttr.OldColor);
                    i.DeleteAttributeByTypeAndTitle(NXObject.AttributeType.String, CaxME.DimenAttr.AssignExcelType);
                    i.DeleteAttributeByTypeAndTitle(NXObject.AttributeType.String, CaxME.DimenAttr.Gauge);
                    i.DeleteAttributeByTypeAndTitle(NXObject.AttributeType.String, CaxME.DimenAttr.Frequency);
                    i.DeleteAttributeByTypeAndTitle(NXObject.AttributeType.String, CaxME.DimenAttr.SelfCheck_Gauge);
                    i.DeleteAttributeByTypeAndTitle(NXObject.AttributeType.String, CaxME.DimenAttr.SelfCheck_Freq);
                    i.DeleteAttributeByTypeAndTitle(NXObject.AttributeType.String, CaxME.DimenAttr.SelfCheckExcel);
                }
            }
            catch (System.Exception ex)
            {
                return false;
            }
            return true;
        }

        private bool RemoveProduct(NXObject[] SelDimensionAry)
        {
            try
            {
                foreach (NXObject i in SelDimensionAry)
                {
                    i.DeleteAttributeByTypeAndTitle(NXObject.AttributeType.String, "PRODUCT");
                }
            }
            catch (System.Exception ex)
            {
                return false;
            }
            return true;
        }

        private bool RemoveKC(NXObject[] SelDimensionAry)
        {
            try
            {
                foreach (NXObject i in SelDimensionAry)
                {

                    //取得KC資訊
                    string KCValue = "", KCInsertTimeValue = "";
                    try
                    {
                        KCValue = i.GetStringAttribute("KC");
                        KCInsertTimeValue = i.GetStringAttribute("KCInsertTime");
                    }
                    catch (System.Exception ex)
                    {
                        KCValue = "";
                        KCInsertTimeValue = "";
                    }
                    if (KCValue != "" & KCInsertTimeValue != "")
                    {
                        CaxME.DeleteCustomSymbol(KCInsertTimeValue);
                    }
                    i.DeleteAttributeByTypeAndTitle(NXObject.AttributeType.String, "KC");
                    i.DeleteAttributeByTypeAndTitle(NXObject.AttributeType.String, "KCInsertTime");
                    i.DeleteAttributeByTypeAndTitle(NXObject.AttributeType.String, "SPCControl");
                }
            }
            catch (System.Exception ex)
            {
                return false;
            }
            return true;
        }

        private bool RemoveKCNote(NXObject[] SelDimensionAry)
        {
            try
            {
                foreach (NXObject i in SelDimensionAry)
                {

                    //取得KC資訊
                    string KCValue = "", KCInsertTimeValue = "";
                    try
                    {
                        KCValue = i.GetStringAttribute("KC");
                        KCInsertTimeValue = i.GetStringAttribute("KCInsertTime");
                    }
                    catch (System.Exception ex)
                    {
                        KCValue = "";
                        KCInsertTimeValue = "";
                    }
                    if (KCValue != "" & KCInsertTimeValue != "")
                    {
                        CaxME.DeleteKCNote(KCInsertTimeValue);
                    }
                    i.DeleteAttributeByTypeAndTitle(NXObject.AttributeType.String, "KC");
                    i.DeleteAttributeByTypeAndTitle(NXObject.AttributeType.String, "KCInsertTime");
                    i.DeleteAttributeByTypeAndTitle(NXObject.AttributeType.String, "SPCControl");
                }
            }
            catch (System.Exception ex)
            {
                return false;
            }
            return true;
        }

        private bool RemoveAll(NXObject[] SelDimensionAry, NXObject[] SheetDimenAry)
        {
            try
            {
                foreach (NXObject i in SelDimensionAry)
                {
                    //恢復原始顏色
                    CaxME.SetDimensionColor(i, 125);

                    //取得泡泡資訊
                    string BallonNum = "";
                    try
                    {
                        BallonNum = i.GetStringAttribute(CaxME.DimenAttr.BallonNum);
                    }
                    catch (System.Exception ex)
                    {
                        BallonNum = "";
                    }
                    if (BallonNum != "")
                    {
                        CaxME.DeleteBallon(BallonNum, SheetDimenAry);
                    }

                    i.DeleteAllAttributesByType(NXObject.AttributeType.String);
                }
            }
            catch (System.Exception ex)
            {
                return false;
            }
            return true;
        }

        private void Close_Click(object sender, EventArgs e)
        {
            this.Close();
        }

        
        private bool DataChecked()
        {
            try
            {
                if (DicSelDimension.Keys.Count == 0)
                {
                    CaxLog.ShowListingWindow("請使用【選擇尺寸】來選擇標註尺寸！");
                    return false;
                }

                if (Gauge.Text == "" & SelfCheck_Gauge.Text == "")
                {
                    MessageBox.Show("請先【選擇檢具】！");
                    return false;
                }

                if (chk_IQC.Checked == false & chk_IPQC.Checked == false & chk_FAI.Checked == false & 
                    chk_FQC.Checked == false & chk_SelfCheck.Checked == false)
                {
                    MessageBox.Show("請先【指定表單】！");
                    return false;
                }

                if (LessThan5.Checked == false & MoreThan5.Checked == false)
                {
                    MessageBox.Show("請先選擇【加工數量】！");
                    return false;
                }

                if (CheckLevel.Text == "")
                {
                    MessageBox.Show("請先輸入【檢驗等級】！");
                    return false;
                }

                int result = 0;
                if (DiCount.Text.Contains('.') || !int.TryParse(DiCount.Text, out result))
                {
                    MessageBox.Show("請輸入正確【尺寸數量】，必須為正整數！");
                    return false;
                }


                /*
                //如果自訂頻率都沒打開，表示預設頻率要有值
                if (IQC_UDefine.Checked == false & IPQC_UDefine.Checked == false & FAI_UDefine.Checked == false & FQC_UDefine.Checked == false & chk_SelfCheck.Checked == false)
                {
                    if (default_Units.Text != "100%" & (default_Freq_0.Text == "" || default_Freq_1.Text == ""))
                    {
                        MessageBox.Show("【指定預設頻率】不完整！");
                        return false;
                    }
                }

                //自訂頻率打開，則IQC的頻率要有值
                if (IQC_UDefine.Checked == true)
                {
                    if (IQC_Units.Text != "100%" & (IQC_Freq_0.Text == "" || IQC_Freq_1.Text == ""))
                    {
                        MessageBox.Show("IQC檢具/頻率資料不完整！");
                        return false;
                    }
                }

                //自訂頻率打開，則IPQC的頻率要有值
                if (IPQC_UDefine.Checked == true)
                {
                    if (IPQC_Units.Text != "100%" & (IPQC_Freq_0.Text == "" || IPQC_Freq_1.Text == ""))
                    {
                        MessageBox.Show("IPQC檢具/頻率資料不完整！");
                        return false;
                    }
                }

                //自訂頻率打開，則FAI的頻率要有值
                if (FAI_UDefine.Checked == true)
                {
                    if (FAI_Units.Text != "100%" & (FAI_Freq_0.Text == "" || FAI_Freq_1.Text == ""))
                    {
                        MessageBox.Show("FAI檢具/頻率資料不完整！");
                        return false;
                    }
                }

                //自訂頻率打開，則FQC的頻率要有值
                if (FQC_UDefine.Checked == true)
                {
                    if (FQC_Units.Text != "100%" & (FQC_Freq_0.Text == "" || FQC_Freq_1.Text == ""))
                    {
                        MessageBox.Show("FQC檢具/頻率資料不完整！");
                        return false;
                    }
                }
                */

                if (chk_SelfCheck.Checked == true)
                {
                    if (SelfCheck_Gauge.Text == "" || (SelfCheck_Units.Text != "100%" & (SelfCheck_Freq_0.Text == "" || SelfCheck_Freq_1.Text == "")))
                    {
                        MessageBox.Show("SelfCheck檢具/頻率資料不完整！");
                        return false;
                    }
                }
            }
            catch (System.Exception ex)
            {
                return false;
            }
            return true;
        }

        private void chk_All_CheckedChanged(object sender, EventArgs e)
        {
            if (chk_All.Checked == true)
            {
                chk_Instrument.Checked = false;
                chk_KC.Checked = false;
                chk_Product.Checked = false;
            }
        }

        private void chk_Instrument_CheckedChanged(object sender, EventArgs e)
        {
            if (chk_Instrument.Checked == true)
            {
                chk_All.Checked = false;
            }
        }

        private void chk_Product_CheckedChanged(object sender, EventArgs e)
        {
            if (chk_Product.Checked == true)
            {
                chk_All.Checked = false;
            }
        }

        private void chk_KC_CheckedChanged(object sender, EventArgs e)
        {
            if (chk_KC.Checked == true)
            {
                chk_All.Checked = false;
            }
        }

        private void SameData_CheckedChanged(object sender, EventArgs e)
        {
            try
            {
                if (SameData.Checked == true)
                {
                    if (Gauge.Text.Contains("T"))
                    {
                        MessageBox.Show("現場量測檢具不具有【" + Gauge.Text + "】");
                        SelfCheck_Gauge.Text = "";
                        SameData.Checked = false;
                        return;
                    }
                    else
                    {
                        SelfCheck_Gauge.Text = Gauge.Text;
                    }
                }
            }
            catch (System.Exception ex)
            {
                
            }
        }

        private void ListSheet_SelectedIndexChanged(object sender, EventArgs e)
        {
            try
            {
                drawingSheet1 = (NXOpen.Drawings.DrawingSheet)workPart.DrawingSheets.FindObject(ListSheet.Text);
                drawingSheet1.Open();
            }
            catch (System.Exception ex)
            {
            }
        }

        private void KC_SelPic_Click(object sender, EventArgs e)
        {
            try
            {
                string fileName = "", filter = "Image Files (*.bmp;*.jpg;*.gif;*.png;*.ico)|*.bmp;*.jpg;*.gif;*.png;*.ico|All files (*.*)|*.*";
                string defaultPath = string.Format(@"{0}\{1}", Environment.GetEnvironmentVariable("ME_Config"), "CusSymbols");
                CaxPublic.OpenFileDialog(out fileName, out filePath, defaultPath, filter);
                if (filePath == "")
                {
                    return;
                }
                pictureBox1.Image = new Bitmap(filePath);
                
            }
            catch (System.Exception ex)
            {
            }
        }

        private void chk_KCText_CheckedChanged(object sender, EventArgs e)
        {
            if (chk_KCText.Checked == true)
            {
                chk_KCPic.Checked = false;
                KC_SelPic.Enabled = false;
                KCText.Enabled = true;
                pictureBox1.Image = null;
            }
        }

        private void chk_KCPic_CheckedChanged(object sender, EventArgs e)
        {
            if (chk_KCPic.Checked == true)
            {
                chk_KCText.Checked = false;
                KC_SelPic.Enabled = true;
                KCText.Enabled = false;
                KCText.WatermarkText = "請指定代號";
            }
        }

        public static bool symbol(string symbolPartFile, CaxME.DimenData location, string datetime)
        {
            try
            {
                Session theSession = Session.GetSession();
                Part workPart = theSession.Parts.Work;
                Part displayPart = theSession.Parts.Display;
                // ----------------------------------------------
                //   Menu: Insert->Symbol->Custom...
                // ----------------------------------------------

                NXOpen.Annotations.CustomSymbol nullAnnotations_CustomSymbol = null;
                NXOpen.Annotations.DraftingCustomSymbolBuilder draftingCustomSymbolBuilder1;
                draftingCustomSymbolBuilder1 = workPart.Annotations.CustomSymbols.CreateDraftingCustomSymbolBuilder(nullAnnotations_CustomSymbol);

                draftingCustomSymbolBuilder1.Origin.Plane.PlaneMethod = NXOpen.Annotations.PlaneBuilder.PlaneMethodType.XyPlane;

                draftingCustomSymbolBuilder1.Origin.SetInferRelativeToGeometry(true);

                draftingCustomSymbolBuilder1.Origin.SetInferRelativeToGeometry(true);

                draftingCustomSymbolBuilder1.Origin.Anchor = NXOpen.Annotations.OriginBuilder.AlignmentPosition.MidCenter;

                draftingCustomSymbolBuilder1.Scale.RightHandSide = "1";

                draftingCustomSymbolBuilder1.Angle.RightHandSide = "0";

                draftingCustomSymbolBuilder1.Origin.Plane.PlaneMethod = NXOpen.Annotations.PlaneBuilder.PlaneMethodType.XyPlane;

                draftingCustomSymbolBuilder1.Origin.SetInferRelativeToGeometry(true);

                NXOpen.Annotations.LeaderData leaderData1;
                leaderData1 = workPart.Annotations.CreateLeaderData();

                leaderData1.StubSize = 5.0;

                leaderData1.Arrowhead = NXOpen.Annotations.LeaderData.ArrowheadType.FilledArrow;

                draftingCustomSymbolBuilder1.Leader.Leaders.Append(leaderData1);

                leaderData1.StubSide = NXOpen.Annotations.LeaderSide.Inferred;

                draftingCustomSymbolBuilder1.SelectSymbol(symbolPartFile);

                draftingCustomSymbolBuilder1.Origin.SetInferRelativeToGeometry(true);

                draftingCustomSymbolBuilder1.Origin.SetInferRelativeToGeometry(true);

                draftingCustomSymbolBuilder1.AnnotationPreference = NXOpen.Annotations.BaseCustomSymbolBuilder.AnnotationPreferences.UseDefinition;

                NXOpen.Annotations.Annotation.AssociativeOriginData assocOrigin1;
                assocOrigin1.OriginType = NXOpen.Annotations.AssociativeOriginType.Drag;
                NXOpen.View nullView = null;
                assocOrigin1.View = nullView;
                assocOrigin1.ViewOfGeometry = nullView;
                NXOpen.Point nullPoint = null;
                assocOrigin1.PointOnGeometry = nullPoint;
                assocOrigin1.VertAnnotation = null;
                assocOrigin1.VertAlignmentPosition = NXOpen.Annotations.AlignmentPosition.TopLeft;
                assocOrigin1.HorizAnnotation = null;
                assocOrigin1.HorizAlignmentPosition = NXOpen.Annotations.AlignmentPosition.TopLeft;
                assocOrigin1.AlignedAnnotation = null;
                assocOrigin1.DimensionLine = 0;
                assocOrigin1.AssociatedView = nullView;
                assocOrigin1.AssociatedPoint = nullPoint;
                assocOrigin1.OffsetAnnotation = null;
                assocOrigin1.OffsetAlignmentPosition = NXOpen.Annotations.AlignmentPosition.TopLeft;
                assocOrigin1.XOffsetFactor = 0.0;
                assocOrigin1.YOffsetFactor = 0.0;
                assocOrigin1.StackAlignmentPosition = NXOpen.Annotations.StackAlignmentPosition.Above;
                draftingCustomSymbolBuilder1.Origin.SetAssociativeOrigin(assocOrigin1);

                Point3d point1 = new Point3d(location.LocationX, location.LocationY, location.LocationZ);
                draftingCustomSymbolBuilder1.Origin.Origin.SetValue(null, nullView, point1);

                draftingCustomSymbolBuilder1.Origin.SetInferRelativeToGeometry(true);


                NXObject nXObject1;
                nXObject1 = draftingCustomSymbolBuilder1.Commit();
                nXObject1.SetAttribute("KCInsertTime", datetime);


                draftingCustomSymbolBuilder1.Destroy();


            }
            catch (System.Exception ex)
            {
                MessageBox.Show(ex.ToString());
                return false;
            }
            return true;


        }

        public static bool InsertKCNote(string text, Point3d textLocation, string datetime, NXOpen.Annotations.OriginBuilder.AlignmentPosition defaultPosition = NXOpen.Annotations.OriginBuilder.AlignmentPosition.MidLeft)
        {
            try
            {
                Session theSession = Session.GetSession();
                Part workPart = theSession.Parts.Work;
                Part displayPart = theSession.Parts.Display;
                // ----------------------------------------------
                //   Menu: Insert->Annotation->Note...
                // ----------------------------------------------
                NXOpen.Session.UndoMarkId markId1;
                markId1 = theSession.SetUndoMark(NXOpen.Session.MarkVisibility.Visible, "Start");
                NXOpen.Annotations.SimpleDraftingAid nullAnnotations_SimpleDraftingAid = null;
                NXOpen.Annotations.DraftingNoteBuilder draftingNoteBuilder1;
                draftingNoteBuilder1 = workPart.Annotations.CreateDraftingNoteBuilder(nullAnnotations_SimpleDraftingAid);
                draftingNoteBuilder1.Origin.Plane.PlaneMethod = NXOpen.Annotations.PlaneBuilder.PlaneMethodType.XyPlane;
                draftingNoteBuilder1.Origin.SetInferRelativeToGeometry(false);
                draftingNoteBuilder1.Origin.SetInferRelativeToGeometry(true);
                draftingNoteBuilder1.Origin.Anchor = defaultPosition;


                draftingNoteBuilder1.Style.LetteringStyle.GeneralTextSize = 3;

                //text文字
                int a = workPart.Fonts.AddFont("chineset");
                draftingNoteBuilder1.Style.LetteringStyle.GeneralTextFont = a;

                string[] text1 = new string[1];
                text1[0] = text;

                draftingNoteBuilder1.Text.TextBlock.SetText(text1);

                draftingNoteBuilder1.Style.LetteringStyle.GeneralTextSize = 1.5;


                theSession.SetUndoMarkName(markId1, "Note Dialog");
                draftingNoteBuilder1.Origin.Plane.PlaneMethod = NXOpen.Annotations.PlaneBuilder.PlaneMethodType.XyPlane;
                draftingNoteBuilder1.Origin.SetInferRelativeToGeometry(true);
                //NXOpen.Annotations.LeaderData leaderData1;
                //leaderData1 = workPart.Annotations.CreateLeaderData();
                //leaderData1.StubSize = 5.0;
                //leaderData1.Arrowhead = NXOpen.Annotations.LeaderData.ArrowheadType.FilledArrow;
                //draftingNoteBuilder1.Leader.Leaders.Append(leaderData1);
                //leaderData1.StubSide = NXOpen.Annotations.LeaderSide.Inferred;
                double symbolscale1;
                symbolscale1 = draftingNoteBuilder1.Text.TextBlock.SymbolScale;
                double symbolaspectratio1;
                symbolaspectratio1 = draftingNoteBuilder1.Text.TextBlock.SymbolAspectRatio;
                draftingNoteBuilder1.Origin.SetInferRelativeToGeometry(true);
                draftingNoteBuilder1.Origin.SetInferRelativeToGeometry(true);
                NXOpen.Annotations.Annotation.AssociativeOriginData assocOrigin1;
                assocOrigin1.OriginType = NXOpen.Annotations.AssociativeOriginType.Drag;
                NXOpen.View nullView = null;
                assocOrigin1.View = nullView;
                assocOrigin1.ViewOfGeometry = nullView;
                NXOpen.Point nullPoint = null;
                assocOrigin1.PointOnGeometry = nullPoint;
                assocOrigin1.VertAnnotation = null;
                assocOrigin1.VertAlignmentPosition = NXOpen.Annotations.AlignmentPosition.TopLeft;
                assocOrigin1.HorizAnnotation = null;
                assocOrigin1.HorizAlignmentPosition = NXOpen.Annotations.AlignmentPosition.TopLeft;
                assocOrigin1.AlignedAnnotation = null;
                assocOrigin1.DimensionLine = 0;
                assocOrigin1.AssociatedView = nullView;
                assocOrigin1.AssociatedPoint = nullPoint;
                assocOrigin1.OffsetAnnotation = null;
                assocOrigin1.OffsetAlignmentPosition = NXOpen.Annotations.AlignmentPosition.TopLeft;
                assocOrigin1.XOffsetFactor = 0.0;
                assocOrigin1.YOffsetFactor = 0.0;
                assocOrigin1.StackAlignmentPosition = NXOpen.Annotations.StackAlignmentPosition.Above;
                draftingNoteBuilder1.Origin.SetAssociativeOrigin(assocOrigin1);

                //text擺放位置
                draftingNoteBuilder1.Origin.Origin.SetValue(null, nullView, textLocation);

                draftingNoteBuilder1.Origin.SetInferRelativeToGeometry(true);
                //NXOpen.Session.UndoMarkId markId2;
                //markId2 = theSession.SetUndoMark(NXOpen.Session.MarkVisibility.Invisible, "Note");
                NXObject nXObject1;
                nXObject1 = draftingNoteBuilder1.Commit();
                nXObject1.SetAttribute("KCInsertTime", datetime);

                //theSession.DeleteUndoMark(markId2, null);
                theSession.SetUndoMarkName(markId1, "Note");
                theSession.SetUndoMarkVisibility(markId1, null, NXOpen.Session.MarkVisibility.Visible);
                draftingNoteBuilder1.Destroy();


                // ----------------------------------------------
                //   Menu: Tools->Journal->Stop Recording
                // ----------------------------------------------
            }
            catch (System.Exception ex)
            {
                UI.GetUI().NXMessageBox.Show("Message", NXMessageBox.DialogType.Error, ex.Message);
                return false;
            }
            return true;
        }

        #region 表單chk改變處理事件
        private void chk_IQC_CheckedChanged(object sender, EventArgs e)
        {
            if (chk_IQC.Checked == true)
            {
                IQC_UDefine.Enabled = true;
            }
            else
            {
                IQC_Freq_0.Text = "";
                IQC_Freq_1.Text = "";
                IQC_Units.Text = "";
                IQC_UDefine.Checked = false;

                IQC_Freq_0.Enabled = false;
                IQC_Freq_1.Enabled = false;
                IQC_Units.Enabled = false;
                IQC_UDefine.Enabled = false;
            }
        }
        private void chk_IPQC_CheckedChanged(object sender, EventArgs e)
        {
            if (chk_IPQC.Checked == true)
            {
                if (LessThan5.Checked == false & MoreThan5.Checked == false)
                {
                    MessageBox.Show("請先選擇【加工數量】！");
                    chk_IPQC.Checked = false;
                    return;
                }

                if (CheckLevel.Text == "")
                {
                    MessageBox.Show("請先輸入【檢驗等級】！");
                    chk_IPQC.Checked = false;
                    return;
                }

                IPQC_Freq_0.Enabled = true;
                IPQC_Freq_1.Enabled = true;
                IPQC_Units.Enabled = true;

                string checkLevelText = CheckLevel.Text.Substring(0, 1);

                if (LessThan5.Checked == true && checkLevelText == "1")
                {
                    IPQC_Freq_0.Text = "1";
                    IPQC_Freq_1.Text = "1";
                    IPQC_Units.Text = "HRS";
                }
                if (LessThan5.Checked == true && checkLevelText == "2")
                {
                    IPQC_Freq_0.Text = "1";
                    IPQC_Freq_1.Text = "2";
                    IPQC_Units.Text = "HRS";
                }
                if (MoreThan5.Checked == true && checkLevelText == "1")
                {
                    IPQC_Freq_0.Text = "2";
                    IPQC_Freq_1.Text = "1";
                    IPQC_Units.Text = "HRS";
                }
                if (MoreThan5.Checked == true && checkLevelText == "2")
                {
                    IPQC_Freq_0.Text = "1";
                    IPQC_Freq_1.Text = "1";
                    IPQC_Units.Text = "HRS";
                }
                if (LessThan5.Checked == true && checkLevelText == "3" && chk_SelfCheck.Checked == true)
                {
                    IPQC_Freq_0.Text = "1";
                    IPQC_Freq_1.Text = "8";
                    IPQC_Units.Text = "HRS";
                }
                if (MoreThan5.Checked == true && checkLevelText == "3" && chk_SelfCheck.Checked == true)
                {
                    IPQC_Freq_0.Text = "1";
                    IPQC_Freq_1.Text = "4";
                    IPQC_Units.Text = "HRS";
                }
                if (checkLevelText == "3" && chk_SelfCheck.Checked == false)
                {
                    IPQC_Freq_0.Text = "1";
                    IPQC_Freq_1.Text = "4";
                    IPQC_Units.Text = "HRS";
                }

                //#region 小於 5 件
                //if (LessThan5.Checked == true & checkLevelText == "1")
                //{
                //    IPQC_Freq_0.Text = "1";
                //    IPQC_Freq_1.Text = "1";
                //    IPQC_Units.Text = "HRS";
                //}
                //if (LessThan5.Checked == true & checkLevelText == "2")
                //{
                //    IPQC_Freq_0.Text = "1";
                //    IPQC_Freq_1.Text = "2";
                //    IPQC_Units.Text = "HRS";
                //}
                //if (LessThan5.Checked == true & checkLevelText == "3")
                //{
                //    IPQC_Freq_0.Text = "1";
                //    IPQC_Freq_1.Text = "4";
                //    IPQC_Units.Text = "HRS";
                //}
                //if (LessThan5.Checked == true & checkLevelText == "4")
                //{
                //    IPQC_Freq_0.Text = "3";
                //    IPQC_Freq_1.Text = "1";
                //    IPQC_Units.Text = "Lot";
                //}
                //if (LessThan5.Checked == true & checkLevelText == "5")
                //{
                //    IPQC_Freq_0.Text = "1";
                //    IPQC_Freq_1.Text = "1";
                //    IPQC_Units.Text = "Lot";
                //}
                //#endregion
                //#region 大於 5 件
                //if (MoreThan5.Checked == true & checkLevelText == "1")
                //{
                //    IPQC_Freq_0.Text = "2";
                //    IPQC_Freq_1.Text = "1";
                //    IPQC_Units.Text = "HRS";
                //}
                //if (MoreThan5.Checked == true & checkLevelText == "2")
                //{
                //    IPQC_Freq_0.Text = "1";
                //    IPQC_Freq_1.Text = "1";
                //    IPQC_Units.Text = "HRS";
                //}
                //if (MoreThan5.Checked == true & checkLevelText == "3")
                //{
                //    IPQC_Freq_0.Text = "1";
                //    IPQC_Freq_1.Text = "4";
                //    IPQC_Units.Text = "HRS";
                //}
                //if (MoreThan5.Checked == true & checkLevelText == "4")
                //{
                //    IPQC_Freq_0.Text = "3";
                //    IPQC_Freq_1.Text = "1";
                //    IPQC_Units.Text = "Lot";
                //}
                //if (MoreThan5.Checked == true & checkLevelText == "5")
                //{
                //    IPQC_Freq_0.Text = "1";
                //    IPQC_Freq_1.Text = "1";
                //    IPQC_Units.Text = "Lot";
                //}
                //#endregion
            }
            else
            {
                IPQC_Freq_0.Text = "";
                IPQC_Freq_1.Text = "";
                IPQC_Units.Text = "";
                IPQC_Freq_0.Enabled = false;
                IPQC_Freq_1.Enabled = false;
                IPQC_Units.Enabled = false;
            }
        }
        private void chk_FAI_CheckedChanged(object sender, EventArgs e)
        {
            if (chk_FAI.Checked == true)
            {
                FAI_UDefine.Enabled = true;
            }
            else
            {
                FAI_Freq_0.Text = "";
                FAI_Freq_1.Text = "";
                FAI_Units.Text = "";
                FAI_UDefine.Checked = false;

                FAI_Freq_0.Enabled = false;
                FAI_Freq_1.Enabled = false;
                FAI_Units.Enabled = false;
                FAI_UDefine.Enabled = false;
            }
        }
        private void chk_FQC_CheckedChanged(object sender, EventArgs e)
        {
            if (chk_FQC.Checked == true)
            {
                FQC_UDefine.Enabled = true;
            }
            else
            {
                FQC_Freq_0.Text = "";
                FQC_Freq_1.Text = "";
                FQC_Units.Text = "";
                FQC_UDefine.Checked = false;

                FQC_Freq_0.Enabled = false;
                FQC_Freq_1.Enabled = false;
                FQC_Units.Enabled = false;
                FQC_UDefine.Enabled = false;
            }
        }
        private void chk_SelfCheck_CheckedChanged(object sender, EventArgs e)
        {
            if (chk_SelfCheck.Checked == true)
            {
                if (LessThan5.Checked == false & MoreThan5.Checked == false)
                {
                    MessageBox.Show("請先選擇【加工數量】！");
                    chk_IPQC.Checked = false;
                    return;
                }

                if (CheckLevel.Text == "")
                {
                    MessageBox.Show("請先輸入【檢驗等級】！");
                    chk_IPQC.Checked = false;
                    return;
                }

                SameData.Enabled = true;
                SelfCheck_Gauge.Enabled = true;
                SelfCheck_Freq_0.Enabled = true;
                SelfCheck_Freq_1.Enabled = true;
                SelfCheck_Units.Enabled = true;

                string checkLevelText = CheckLevel.Text.Substring(0, 1);
                #region 小於 5 件
                if (checkLevelText == "1")
                {
                    SelfCheck_Freq_0.Text = "";
                    SelfCheck_Freq_1.Text = "";
                    SelfCheck_Units.Text = "100%";
                }
                if (checkLevelText == "2")
                {
                    SelfCheck_Freq_0.Text = "";
                    SelfCheck_Freq_1.Text = "";
                    SelfCheck_Units.Text = "100%";
                }
                if (checkLevelText == "3")
                {
                    SelfCheck_Freq_0.Text = "1";
                    SelfCheck_Freq_1.Text = "2";
                    SelfCheck_Units.Text = "HRS";

                    if (chk_IPQC.Checked == true && LessThan5.Checked == true)
                    {
                        IPQC_Freq_1.Text = "8";
                    }
                }
                if (checkLevelText == "4")
                {
                    SelfCheck_Freq_0.Text = "";
                    SelfCheck_Freq_1.Text = "";
                    SelfCheck_Units.Text = "";
                }
                if (checkLevelText == "5")
                {
                    SelfCheck_Freq_0.Text = "";
                    SelfCheck_Freq_1.Text = "";
                    SelfCheck_Units.Text = "";
                }
                #endregion
               
                //SelfCheck_Gauge.Enabled = true;
                //SelfCheck_Freq_0.Enabled = true;
                //SelfCheck_Freq_1.Enabled = true;
                //SelfCheck_Units.Enabled = true;

                //SameData.Enabled = true;
            }
            else
            {
                SelfCheck_Gauge.Text = "";
                SelfCheck_Freq_0.Text = "";
                SelfCheck_Freq_1.Text = "";
                SelfCheck_Units.Text = "";

                SelfCheck_Gauge.Enabled = false;
                SelfCheck_Freq_0.Enabled = false;
                SelfCheck_Freq_1.Enabled = false;
                SelfCheck_Units.Enabled = false;

                string checkLevelText = CheckLevel.Text.Substring(0, 1);
                if (checkLevelText == "3" & chk_IPQC.Checked == true)
                {
                    IPQC_Freq_1.Text = "4";
                }
                //SelfCheck_Gauge.Text = "";
                //SelfCheck_Freq_0.Text = "";
                //SelfCheck_Freq_1.Text = "";
                //SelfCheck_Units.Text = "";

                //SelfCheck_Gauge.Enabled = false;
                //SelfCheck_Freq_0.Enabled = false;
                //SelfCheck_Freq_1.Enabled = false;
                //SelfCheck_Units.Enabled = false;

                //SameData.Enabled = false;
            }
        }
        #endregion

        #region 單位改變處理事件
        private void IQC_Units_SelectedIndexChanged(object sender, EventArgs e)
        {
            if (IQC_Units.Text == "100%")
            {
                IQC_Freq_0.Text = "";
                IQC_Freq_1.Text = "";
                IQC_Freq_0.Enabled = false;
                IQC_Freq_1.Enabled = false;
            }
            if (IQC_Units.Text == "")
            {
                IQC_Freq_0.Enabled = true;
                IQC_Freq_1.Enabled = true;
                IQC_Freq_0.Text = "";
                IQC_Freq_1.Text = "";
            }
            if (IQC_Units.Text == "Lot")
            {
                IQC_Freq_0.Enabled = true;
                IQC_Freq_1.Enabled = true;
                IQC_Freq_1.Text = "1";
            }
            if (IQC_Units.Text == "PCS")
            {
                IQC_Freq_0.Enabled = true;
                IQC_Freq_1.Enabled = true;
            }
            if (IQC_Units.Text == "HRS")
            {
                IQC_Freq_0.Enabled = true;
                IQC_Freq_1.Enabled = true;
            }
        }
        private void IPQC_Units_SelectedIndexChanged(object sender, EventArgs e)
        {
            if (IPQC_Units.Text == "100%")
            {
                IPQC_Freq_0.Text = "";
                IPQC_Freq_1.Text = "";
                IPQC_Freq_0.Enabled = false;
                IPQC_Freq_1.Enabled = false;
            }
            if (IPQC_Units.Text == "")
            {
                IPQC_Freq_0.Enabled = true;
                IPQC_Freq_1.Enabled = true;
                IPQC_Freq_0.Text = "";
                IPQC_Freq_1.Text = "";
            }
            if (IPQC_Units.Text == "Lot")
            {
                IPQC_Freq_0.Enabled = true;
                IPQC_Freq_1.Enabled = true;
                IPQC_Freq_1.Text = "1";
            }
            if (IPQC_Units.Text == "PCS")
            {
                IPQC_Freq_0.Enabled = true;
                IPQC_Freq_1.Enabled = true;
            }
            if (IPQC_Units.Text == "HRS")
            {
                IPQC_Freq_0.Enabled = true;
                IPQC_Freq_1.Enabled = true;
            }
        }
        private void FAI_Units_SelectedIndexChanged(object sender, EventArgs e)
        {
            if (FAI_Units.Text == "100%")
            {
                FAI_Freq_0.Text = "";
                FAI_Freq_1.Text = "";
                FAI_Freq_0.Enabled = false;
                FAI_Freq_1.Enabled = false;
            }
            if (FAI_Units.Text == "")
            {
                FAI_Freq_0.Enabled = true;
                FAI_Freq_1.Enabled = true;
                IQC_Freq_0.Text = "";
                IQC_Freq_1.Text = "";
            }
            if (FAI_Units.Text == "Lot")
            {
                FAI_Freq_0.Enabled = true;
                FAI_Freq_1.Enabled = true;
                FAI_Freq_1.Text = "1";
            }
            if (FAI_Units.Text == "PCS")
            {
                FAI_Freq_0.Enabled = true;
                FAI_Freq_1.Enabled = true;
            }
            if (FAI_Units.Text == "HRS")
            {
                FAI_Freq_0.Enabled = true;
                FAI_Freq_1.Enabled = true;
            }
        }
        private void FQC_Units_SelectedIndexChanged(object sender, EventArgs e)
        {
            if (FQC_Units.Text == "100%")
            {
                FQC_Freq_0.Text = "";
                FQC_Freq_1.Text = "";
                FQC_Freq_0.Enabled = false;
                FQC_Freq_1.Enabled = false;
            }
            if (FQC_Units.Text == "")
            {
                FQC_Freq_0.Enabled = true;
                FQC_Freq_1.Enabled = true;
                FQC_Freq_0.Text = "";
                FQC_Freq_1.Text = "";
            }
            if (FQC_Units.Text == "Lot")
            {
                FQC_Freq_0.Enabled = true;
                FQC_Freq_1.Enabled = true;
                FQC_Freq_1.Text = "1";
            }
            if (FQC_Units.Text == "PCS")
            {
                FQC_Freq_0.Enabled = true;
                FQC_Freq_1.Enabled = true;
            }
            if (FQC_Units.Text == "HRS")
            {
                FQC_Freq_0.Enabled = true;
                FQC_Freq_1.Enabled = true;
            }
        }
        private void SelfCheck_Units_SelectedIndexChanged(object sender, EventArgs e)
        {
            if (SelfCheck_Units.Text == "100%")
            {
                SelfCheck_Freq_0.Text = "";
                SelfCheck_Freq_1.Text = "";
                SelfCheck_Freq_0.Enabled = false;
                SelfCheck_Freq_1.Enabled = false;
            }
            if (SelfCheck_Units.Text == "")
            {
                SelfCheck_Freq_0.Enabled = true;
                SelfCheck_Freq_1.Enabled = true;
                SelfCheck_Freq_0.Text = "";
                SelfCheck_Freq_1.Text = "";
            }
            if (SelfCheck_Units.Text == "Lot")
            {
                SelfCheck_Freq_0.Enabled = true;
                SelfCheck_Freq_1.Enabled = true;
                SelfCheck_Freq_1.Text = "1";
            }
            if (SelfCheck_Units.Text == "PCS")
            {
                SelfCheck_Freq_0.Enabled = true;
                SelfCheck_Freq_1.Enabled = true;
            }
            if (SelfCheck_Units.Text == "HRS")
            {
                SelfCheck_Freq_0.Enabled = true;
                SelfCheck_Freq_1.Enabled = true;
            }
        }
        private void default_Units_SelectedIndexChanged(object sender, EventArgs e)
        {
            if (default_Units.Text == "100%")
            {
                default_Freq_0.Text = "";
                default_Freq_1.Text = "";
                default_Freq_0.Enabled = false;
                default_Freq_1.Enabled = false;
            }
            if (default_Units.Text == "")
            {
                default_Freq_0.Enabled = true;
                default_Freq_1.Enabled = true;
                default_Freq_0.Text = "";
                default_Freq_1.Text = "";
            }
            if (default_Units.Text == "Lot")
            {
                default_Freq_0.Enabled = true;
                default_Freq_1.Enabled = true;
                default_Freq_1.Text = "1";
            }
            if (default_Units.Text == "PCS")
            {
                default_Freq_0.Enabled = true;
                default_Freq_1.Enabled = true;
            }
            if (default_Units.Text == "HRS")
            {
                default_Freq_0.Enabled = true;
                default_Freq_1.Enabled = true;
            }
        }
        #endregion

        #region 自訂chk改變處理事件
        private void IQC_UDefine_CheckedChanged(object sender, EventArgs e)
        {
            if (IQC_UDefine.Checked == true)
            {
                IQC_Freq_0.Enabled = true;
                IQC_Freq_1.Enabled = true;
                IQC_Units.Enabled = true;
            }
            else
            {
                IQC_Freq_0.Text = "";
                IQC_Freq_1.Text = "";
                IQC_Units.Text = "";

                IQC_Freq_0.Enabled = false;
                IQC_Freq_1.Enabled = false;
                IQC_Units.Enabled = false;
            }
        }
        private void IPQC_UDefine_CheckedChanged(object sender, EventArgs e)
        {
            if (IPQC_UDefine.Checked == true)
            {
                IPQC_Freq_0.Enabled = true;
                IPQC_Freq_1.Enabled = true;
                IPQC_Units.Enabled = true;
            }
            else
            {
                IPQC_Freq_0.Text = "";
                IPQC_Freq_1.Text = "";
                IPQC_Units.Text = "";

                IPQC_Freq_0.Enabled = false;
                IPQC_Freq_1.Enabled = false;
                IPQC_Units.Enabled = false;
            }
        }
        private void FAI_UDefine_CheckedChanged(object sender, EventArgs e)
        {
            if (FAI_UDefine.Checked == true)
            {
                FAI_Freq_0.Enabled = true;
                FAI_Freq_1.Enabled = true;
                FAI_Units.Enabled = true;
            }
            else
            {
                FAI_Freq_0.Text = "";
                FAI_Freq_1.Text = "";
                FAI_Units.Text = "";

                FAI_Freq_0.Enabled = false;
                FAI_Freq_1.Enabled = false;
                FAI_Units.Enabled = false;
            }
        }
        private void FQC_UDefine_CheckedChanged(object sender, EventArgs e)
        {
            if (FQC_UDefine.Checked == true)
            {
                FQC_Freq_0.Enabled = true;
                FQC_Freq_1.Enabled = true;
                FQC_Units.Enabled = true;
            }
            else
            {
                FQC_Freq_0.Text = "";
                FQC_Freq_1.Text = "";
                FQC_Units.Text = "";

                FQC_Freq_0.Enabled = false;
                FQC_Freq_1.Enabled = false;
                FQC_Units.Enabled = false;
            }
        }
        #endregion

        private void LessThan5_CheckedChanged(object sender, EventArgs e)
        {
            if (LessThan5.Checked == true)
            {
                MoreThan5.Checked = false;
            }
        }

        private void MoreThan5_CheckedChanged(object sender, EventArgs e)
        {
            if (MoreThan5.Checked == true)
            {
                LessThan5.Checked = false;
            }
        }


    }
}
