using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Windows.Forms;
using DevComponents.DotNetBar;
using CaxGlobaltek;
using NXOpen;
using NXOpen.UF;
using NXOpen.Utilities;
using NXOpen.Annotations;
using NHibernate;

namespace AssignGauge
{
    public partial class AssignGaugeDlg : Office2007Form
    {
        public static Session theSession = Session.GetSession();
        public static UI theUI;
        public static UFSession theUfSession = UFSession.GetUFSession();
        public static Part workPart = theSession.Parts.Work;
        public static Part displayPart = theSession.Parts.Display;
        

        public static bool status;
        public static Dictionary<string, GaugeData> DicGaugeData = new Dictionary<string, GaugeData>();
        public static Dictionary<NXObject, string> DicSelDimension = new Dictionary<NXObject, string>();
        public static int BallonNum = 0;
        public static CoordinateData cCoordinateData = new CoordinateData();
        public static double SheetLength = 0.0, SheetHeight = 0.0;
        public string Is_Local = "";
        public string[] AGData = new string[] { };
        public static NXObject[] SelDimensionAry;
        public bool IsAbsCheck = false, IsRemove = false;
        public static List<string> listProduct = new List<string>();

        public class GaugeData
        {
            public string Color { get; set; }
            public string EngName { get; set; }
        }

        public AssignGaugeDlg()
        {
            InitializeComponent();
        }

        private void AssignGaugeDlg_Load(object sender, EventArgs e)
        {
            try
            {
                int module_id;
                theUfSession.UF.AskApplicationModule(out module_id);
                if (module_id != UFConstants.UF_APP_DRAFTING)
                {
                    MessageBox.Show("請先轉換為製圖模組後再執行！");
                    this.Close();
                }

                Is_Local = Environment.GetEnvironmentVariable("UGII_ENV_FILE");
                if (Is_Local != null)
                {
                    //取得AssignGaugeData
                    status = CaxGetDatData.GetAssignGaugeData(out AGData);
                    if (!status)
                    {
                        CaxLog.ShowListingWindow("GetAssignGaugeData失敗，請檢查MEConfig是否有檔案");
                        return;
                    }

                    //取得圖紙範圍資料Data
                    status = CaxGetDatData.GetDraftingCoordinateData(out cCoordinateData);

                    //取得PRODUCT資料(未完成，資料庫還沒建立)
                    ISession session = MyHibernateHelper.SessionFactory.OpenSession();
                    IList<Sys_Product> sysProduct = session.QueryOver<Sys_Product>().List<Sys_Product>();
                    foreach (Sys_Product i in sysProduct)
                        listProduct.Add(i.productName);

                }
                else
                {
                    //取得AssignGaugeData
                    string AssignGaugeData_dat = "AssignGaugeData.dat";
                    string AssignGaugeData_Path = string.Format(@"{0}\{1}", "D:", AssignGaugeData_dat);
                    AGData = System.IO.File.ReadAllLines(AssignGaugeData_Path);

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
                //預設關閉選擇物件
                //SelectDimen.Enabled = false;

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

                //填檢具到下拉選單中
                //Gauge.Items.Add("");
                //Gauge.Items.AddRange(DicGaugeData.Keys.ToArray());
                //填檢具到SelfCheck下拉選單中
                //SelfCheckGauge.Items.Add("");
                //foreach (KeyValuePair<string,GaugeData> kvp in DicGaugeData)
                //{
                //    if (kvp.Key.Contains("T"))
                //    {
                //        continue;
                //    }
                //    SelfCheckGauge.Items.Add(kvp.Key);
                //}

                //取得sheet並填入下拉選單中
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
                SheetLength = DefaultSheet.Length;
                SheetHeight = DefaultSheet.Height;

                //填入IQC、IPQC與SelfCheck的單位
                //string[] CheckUnits = new string[] { "HRS", "PCS", "100%" };
                //Freq_Units.Items.AddRange(CheckUnits.ToArray());
                //SelfCheck_Units.Items.AddRange(CheckUnits.ToArray());
            }
            catch (System.Exception ex)
            {
                MessageBox.Show(ex.ToString());
            }
            

            
        }

        private void ListSheet_SelectedIndexChanged(object sender, EventArgs e)
        {
            NXOpen.Drawings.DrawingSheet drawingSheet1 = (NXOpen.Drawings.DrawingSheet)workPart.DrawingSheets.FindObject(ListSheet.Text);
            drawingSheet1.Open();
        }

        private void SelectDimen_Click(object sender, EventArgs e)
        {
            this.Hide();
            Inspection cInspection = new Inspection(this.FindForm(), DicGaugeData);
            cInspection.Show();
            //NXObject[] SelDimensionAry;
            //CaxPublic.SelectObjects(out SelDimensionAry);
            //DicSelDimension = new Dictionary<NXObject, string>();
            //foreach (NXObject single in SelDimensionAry)
            //{
            //    string DimenType = single.GetType().ToString();
            //    DicSelDimension.Add(single, DimenType);
            //}
            //this.Show();
            //SelectDimen.Text = string.Format("選擇物件({0})", SelDimensionAry.Length.ToString());
        }

        private void chb_Assign_CheckedChanged(object sender, EventArgs e)
        {
            if (chb_Assign.Checked == true)
            {
                chb_Remove.Checked = false;
                SelectDimen.Enabled = true;
            }
        }

        private void chb_Remove_CheckedChanged(object sender, EventArgs e)
        {
            if (chb_Remove.Checked == true)
            {
                chb_Assign.Checked = false;
                SelectDimen.Enabled = true;
            }
        }

        private void OK_Click(object sender, EventArgs e)
        {
            if (IsAbsCheck)
            {
                foreach (NXObject i in SelDimensionAry)
                    i.SetAttribute("KC", "KeyChara.");

                AbsCheDimen.Text = "Key Chara.(0)";
            }
            else if (IsRemove)
            {
                foreach (KeyValuePair<NXObject, string> kvp in DicSelDimension)
                {
                    //恢復原始顏色
                    string oldColor = "125";
                    CaxME.SetDimensionColor(kvp.Key, Convert.ToInt32(oldColor));

                    //取得泡泡資訊
                    string BallonNum = "";
                    try
                    {
                        BallonNum = kvp.Key.GetStringAttribute(CaxME.DimenAttr.BallonNum);
                    }
                    catch (System.Exception ex)
                    {
                        BallonNum = "";
                    }
                    if (BallonNum != "")
                    {
                        //CaxME.DeleteBallon(BallonNum);
                    }

                    kvp.Key.DeleteAllAttributesByType(NXObject.AttributeType.String);
                }
                RepairDimen.Text = "Remove(0)";
            }

            MessageBox.Show("設定成功");
        }

        private void Cancel_Click(object sender, EventArgs e)
        {
            this.Close();
        }

        private void FAIcheckBox_CheckedChanged(object sender, EventArgs e)
        {
            if (FAIcheckBox.Checked == true)
            {
                IQCcheckBox.Checked = false;
                IPQCcheckBox.Checked = false;
                FQCcheckBox.Checked = false;
            }
        }

        private void IQCcheckBox_CheckedChanged(object sender, EventArgs e)
        {
            if (IQCcheckBox.Checked == true)
            {
                FAIcheckBox.Checked = false;
                IPQCcheckBox.Checked = false;
                FQCcheckBox.Checked = false;
            }
        }

        private void IPQCcheckBox_CheckedChanged(object sender, EventArgs e)
        {
            if (IPQCcheckBox.Checked == true)
            {
                FAIcheckBox.Checked = false;
                IQCcheckBox.Checked = false;
                FQCcheckBox.Checked = false;
            }
        }

        private void FQCcheckBox_CheckedChanged(object sender, EventArgs e)
        {
            if (FQCcheckBox.Checked == true)
            {
                FAIcheckBox.Checked = false;
                IQCcheckBox.Checked = false;
                IPQCcheckBox.Checked = false;
            }
        }

        private void Gauge_SelectedIndexChanged(object sender, EventArgs e)
        {
            if (SameData.Checked == true)
            {
                if (Gauge.Text.Contains("T"))
                {
                    CaxLog.ShowListingWindow("現場量測檢具沒有【" + Gauge.Text + "】");
                    return;
                }
                SelfCheckGauge.Text = Gauge.Text;
            }
        }

        private void Freq_0_TextChanged(object sender, EventArgs e)
        {
            if (SameData.Checked == true)
            {
                SelfCheck_0.Text = Freq_0.Text;
            }
        }

        private void Freq_1_TextChanged(object sender, EventArgs e)
        {
            if (SameData.Checked == true)
            {
                SelfCheck_1.Text = Freq_1.Text;
            }
        }

        private void Freq_Units_TextChanged(object sender, EventArgs e)
        {
            if (SameData.Checked == true)
            {
                SelfCheck_Units.Text = Freq_Units.Text;
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
                        SelfCheck_0.Text = Freq_0.Text;
                        SelfCheck_1.Text = Freq_1.Text;
                        SelfCheck_Units.Text = Freq_Units.Text;
                        return;
                    }
                    else
                    {
                        SelfCheckGauge.Text = Gauge.Text;
                        SelfCheck_0.Text = Freq_0.Text;
                        SelfCheck_1.Text = Freq_1.Text;
                        SelfCheck_Units.Text = Freq_Units.Text;
                    }
                }
                else
                {
                    SelfCheckGauge.Text = "";
                    SelfCheck_0.Text = "";
                    SelfCheck_1.Text = "";
                    SelfCheck_Units.Text = "";
                }
            }
            catch (System.Exception ex)
            {
                return;
            }
        }

        private void AbsCheDimen_Click(object sender, EventArgs e)
        {
            try
            {
                IsAbsCheck = false;
                AbsCheDimen.Text = "Key Chara.(0)";
                IsRemove = false;
                RepairDimen.Text = "Remove(0)";

                CaxPublic.SelectObjects(out SelDimensionAry);
                if (SelDimensionAry.Length > 0)
                    IsAbsCheck = true;

                AbsCheDimen.Text = string.Format(@"Key Chara.({0})", SelDimensionAry.Length);

                //foreach (NXObject i in SelDimensionAry)
                //    i.SetAttribute("KC", "KeyChara");

                //MessageBox.Show("指定成功");
            }
            catch (System.Exception ex)
            {
                return;
            }
        }

        private void RepairDimen_Click(object sender, EventArgs e)
        {
            try
            {
                IsRemove = false;
                RepairDimen.Text = "Remove(0)";
                IsAbsCheck = false;
                AbsCheDimen.Text = "Key Chara.(0)";

                CaxPublic.SelectObjects(out SelDimensionAry);
                if (SelDimensionAry.Length > 0)
                {
                    IsRemove = true;
                    DicSelDimension = new Dictionary<NXObject, string>();
                    foreach (NXObject single in SelDimensionAry)
                        DicSelDimension.Add(single, single.GetType().ToString());
                }
                    

                RepairDimen.Text = string.Format(@"Remove({0})", SelDimensionAry.Length);
                //foreach (KeyValuePair<NXObject, string> kvp in DicSelDimension)
                //{
                //    //恢復原始顏色
                //    string oldColor = "";
                //    try
                //    {
                //        //第二次以上指定顏色的話，抓出來的顏色就不是內建顏色EX:125->108->186，抓到的是108
                //        oldColor = kvp.Key.GetStringAttribute(CaxME.DimenAttr.OldColor);
                //        //內建原始顏色
                //        oldColor = "125";
                //    }
                //    catch (System.Exception ex)
                //    {
                //        oldColor = "125";
                //    }
                //    CaxME.SetDimensionColor(kvp.Key, Convert.ToInt32(oldColor));

                //    //取得泡泡資訊
                //    string BallonNum = "";
                //    try
                //    {
                //        BallonNum = kvp.Key.GetStringAttribute(CaxME.DimenAttr.BallonNum);
                //    }
                //    catch (System.Exception ex)
                //    {
                //        BallonNum = "";
                //    }
                //    if (BallonNum != "")
                //    {
                //        CaxME.DeleteBallon(BallonNum);
                //    }

                //    kvp.Key.DeleteAllAttributesByType(NXObject.AttributeType.String);
                //}
                //MessageBox.Show("移除成功");
            }
            catch (System.Exception ex)
            {
                return;
            }
        }

        private void DimenChara_Click(object sender, EventArgs e)
        {
            try
            {
                this.Hide();
                Product cProduct = new Product(this.FindForm(), listProduct);
                cProduct.Show();
            }
            catch (System.Exception ex)
            {
                return;
            }
        }

        


    }
}
