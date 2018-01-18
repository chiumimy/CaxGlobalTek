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
using CaxGlobaltek;
using NXOpen.UF;

namespace AssignGauge
{
    public partial class Inspection : Office2007Form
    {
        public static Session theSession = Session.GetSession();
        public static UI theUI;
        public static UFSession theUfSession = UFSession.GetUFSession();
        public static Part workPart = theSession.Parts.Work;
        public static Part displayPart = theSession.Parts.Display;

        private bool status;
        public static Form parentForm = new Form();
        public static Dictionary<NXObject, string> DicSelDimension = new Dictionary<NXObject, string>();
        public static Dictionary<string, AssignGaugeDlg.GaugeData> DicGaugeData = new Dictionary<string, AssignGaugeDlg.GaugeData>();


        public Inspection(Form pForm, Dictionary<string, AssignGaugeDlg.GaugeData> cDicGaugeData)
        {
            InitializeComponent();
            
            parentForm = pForm;
            DicGaugeData = cDicGaugeData;
        }

        private void Ins_OK_Click(object sender, EventArgs e)
        {
            try
            {
                status = DataChecked();
                if (!status)
                {
                    MessageBox.Show("Data檢查失敗");
                    return;
                }

                string AssignExcelType = "";

                if (FAIcheckBox.Checked == true)
                    AssignExcelType = FAIcheckBox.Text;

                if (IQCcheckBox.Checked == true)
                    AssignExcelType = IQCcheckBox.Text;

                if (IPQCcheckBox.Checked == true)
                    AssignExcelType = IPQCcheckBox.Text;

                if (FQCcheckBox.Checked == true)
                    AssignExcelType = FQCcheckBox.Text;



                foreach (KeyValuePair<NXObject, string> kvp in DicSelDimension)
                {
                    //取得原始顏色
                    int oldColor = CaxME.GetDimensionColor(kvp.Key);
                    if (oldColor == -1)
                    {
                        oldColor = 125;
                    }
                    //取得檢具顏色
                    AssignGaugeDlg.GaugeData cGaugeData = new AssignGaugeDlg.GaugeData();
                    if (SelfCheckGauge.Text != "")
                    {
                        status = DicGaugeData.TryGetValue(SelfCheckGauge.Text, out cGaugeData);
                        if (!status)
                        {
                            CaxLog.ShowListingWindow("此檢具資料可能有誤");
                            return;
                        }
                    }
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
                    kvp.Key.SetAttribute(CaxME.DimenAttr.OldColor, oldColor.ToString());//舊顏色
                    kvp.Key.SetAttribute(CaxME.DimenAttr.AssignExcelType, AssignExcelType);
                    if (Gauge.Text != "")
                    {
                        kvp.Key.SetAttribute(CaxME.DimenAttr.Gauge, Gauge.Text);//檢具名稱
                    }
                    if (Freq_0.Text != "" & Freq_1.Text != "" & Freq_Units.Text != "")
                    {
                        kvp.Key.SetAttribute(CaxME.DimenAttr.Frequency, Freq_0.Text + "PC/" + Freq_1.Text + Freq_Units.Text);//頻率
                    }
                    if (SelfCheckGauge.Text != "")
                    {
                        kvp.Key.SetAttribute("SelfCheckExcel", "SelfCheck");
                        kvp.Key.SetAttribute(CaxME.DimenAttr.SelfCheck_Gauge, SelfCheckGauge.Text);//檢具名稱
                        kvp.Key.SetAttribute(CaxME.DimenAttr.SelfCheck_Freq, SelfCheck_0.Text + "PC/" + SelfCheck_1.Text + SelfCheck_Units.Text);//SelfCheck
                    }
                }
                //對零件塞上excelType屬性，說明此次尺寸是出哪一張excel(此屬性是給MEUpload時插入資料庫所使用)
                string excelType = "";
                if (FAIcheckBox.Checked == true)
                {
                    try
                    {
                        excelType = workPart.GetStringAttribute("ExcelType");
                    }
                    catch (System.Exception ex)
                    {
                        excelType = "";
                    }
                    if (excelType != "" && !excelType.Contains("FAI"))
                    {
                        excelType = excelType + "," + "FAI";
                        workPart.SetAttribute("ExcelType", excelType);
                    }
                    if (excelType == "")
                    {
                        workPart.SetAttribute("ExcelType", "FAI");
                    }
                }
                if (IQCcheckBox.Checked == true)
                {
                    try
                    {
                        excelType = workPart.GetStringAttribute("ExcelType");
                    }
                    catch (System.Exception ex)
                    {
                        excelType = "";
                    }
                    if (excelType != "" && !excelType.Contains("IQC"))
                    {
                        excelType = excelType + "," + "IQC";
                        workPart.SetAttribute("ExcelType", excelType);
                    }
                    if (excelType == "")
                    {
                        workPart.SetAttribute("ExcelType", "IQC");
                    }
                }
                if (IPQCcheckBox.Checked == true)
                {
                    try
                    {
                        excelType = workPart.GetStringAttribute("ExcelType");
                    }
                    catch (System.Exception ex)
                    {
                        excelType = "";
                    }
                    if (excelType != "" && !excelType.Contains("IPQC"))
                    {
                        excelType = excelType + "," + "IPQC";
                        workPart.SetAttribute("ExcelType", excelType);
                    }
                    if (excelType == "")
                    {
                        workPart.SetAttribute("ExcelType", "IPQC");
                    }
                }
                if (FQCcheckBox.Checked == true)
                {
                    try
                    {
                        excelType = workPart.GetStringAttribute("ExcelType");
                    }
                    catch (System.Exception ex)
                    {
                        excelType = "";
                    }
                    if (excelType != "" && !excelType.Contains("FQC"))
                    {
                        excelType = excelType + "," + "FQC";
                        workPart.SetAttribute("ExcelType", excelType);
                    }
                    if (excelType == "")
                    {
                        workPart.SetAttribute("ExcelType", "FQC");
                    }
                }
                if (SelfCheckGauge.Text != "")
                {
                    try
                    {
                        excelType = workPart.GetStringAttribute("ExcelType");
                    }
                    catch (System.Exception ex)
                    {
                        excelType = "";
                    }
                    if (excelType != "" && !excelType.Contains("SelfCheck"))
                    {
                        excelType = excelType + "," + "SelfCheck";
                        workPart.SetAttribute("ExcelType", excelType);
                    }
                    if (excelType == "")
                    {
                        workPart.SetAttribute("ExcelType", "SelfCheck");
                    }
                }

            }
            catch (System.Exception ex)
            {
                return;
            }
            MessageBox.Show("Assign成功");
            SelDimen.Text = "選擇物件(0)";
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

                if (FAIcheckBox.Checked == false & IQCcheckBox.Checked == false & IPQCcheckBox.Checked == false & FQCcheckBox.Checked == false)
                {
                    MessageBox.Show("請先選擇一種檢驗報告格式！");
                    return false;
                }
                if (Gauge.Text == "")
                {
                    MessageBox.Show("請先選擇一種量具！");
                    return false;
                }
                if (IPQCcheckBox.Checked == true)
                {
                    if (Freq_0.Text == "" || Freq_1.Text == "" || Freq_Units.Text == "")
                    {
                        MessageBox.Show("IPQC需指定檢驗頻率！");
                        return false;
                    }
                }
                if (SameData.Checked == true)
                {
                    if (SelfCheckGauge.Text == "" || SelfCheck_0.Text == "" || SelfCheck_1.Text == "" || SelfCheck_Units.Text == "")
                    {
                        MessageBox.Show("SelfCheck量測資料不足！");
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

        private void Inspection_FormClosed(object sender, FormClosedEventArgs e)
        {
            parentForm.Show();
        }

        private void Ins_Close_Click(object sender, EventArgs e)
        {
            this.Close();
        }

        private void SelDimen_Click(object sender, EventArgs e)
        {
            try
            {
                this.Hide();
                NXObject[] SelDimensionAry;
                CaxPublic.SelectObjects(out SelDimensionAry);
                DicSelDimension = new Dictionary<NXObject, string>();
                foreach (NXObject single in SelDimensionAry)
                {
                    string DimenType = single.GetType().ToString();
                    DicSelDimension.Add(single, DimenType);
                }
                this.Show();
                SelDimen.Text = string.Format("選擇物件({0})", SelDimensionAry.Length.ToString());
            }
            catch (System.Exception ex)
            {
                return;
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

        private void Inspection_Load(object sender, EventArgs e)
        {
            //填檢具到下拉選單中
            Gauge.Items.Add("");
            Gauge.Items.AddRange(DicGaugeData.Keys.ToArray());
            //填檢具到SelfCheck下拉選單中
            SelfCheckGauge.Items.Add("");
            foreach (KeyValuePair<string, AssignGaugeDlg.GaugeData> kvp in DicGaugeData)
            {
                if (kvp.Key.Contains("T"))
                {
                    continue;
                }
                SelfCheckGauge.Items.Add(kvp.Key);
            }

            //填入單位
            string[] CheckUnits = new string[] { "HRS", "PCS", "100%" };
            Freq_Units.Items.AddRange(CheckUnits.ToArray());
            SelfCheck_Units.Items.AddRange(CheckUnits.ToArray());
        }
    }
}
