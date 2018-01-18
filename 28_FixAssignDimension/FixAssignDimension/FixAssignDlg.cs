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
using NXOpen.Utilities;
using CaxGlobaltek;

namespace FixAssignDimension
{
    public partial class FixAssignDlg : DevComponents.DotNetBar.Office2007Form
    {
        public static Session theSession = Session.GetSession();
        public static UI theUI;
        public static UFSession theUfSession = UFSession.GetUFSession();
        public static Part workPart = theSession.Parts.Work;

        public FixAssignDlg()
        {
            InitializeComponent();
        }

        private void FixAssignDlg_Load(object sender, EventArgs e)
        {
            #region 系統環境
            Variables.Is_Local = Environment.GetEnvironmentVariable("UGII_ENV_FILE");
            if (Variables.Is_Local == null)
            {
                MessageBox.Show("請先使用系統後再執行！");
                this.Close();
                return;
            }
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
            #endregion

            DiCount.Text = "1";
        }

        private void selDimen_Click(object sender, EventArgs e)
        {
            try
            {
                this.Hide();
                CaxPublic.SelectObjects(out Variables.SelDimensionAry);
                Variables.DicSelDimension = new Dictionary<NXObject, string>();
                foreach (NXObject single in Variables.SelDimensionAry)
                {
                    string DimenType = single.GetType().ToString();
                    Variables.DicSelDimension.Add(single, DimenType);
                }
                selDimen.Text = string.Format("選擇尺寸({0})", Variables.SelDimensionAry.Length.ToString());
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
                if (Variables.SelDimensionAry.Length != 0)
                {
                    int value;
                    if (DiCount.Text == "" || !int.TryParse(DiCount.Text, out value))
                    {
                        MessageBox.Show("尺寸數量不正確，請指定正整數");
                        return;
                    }

                    foreach (KeyValuePair<NXObject, string> kvp in Variables.DicSelDimension)
                    {
                        //改變標註尺寸顏色
                        CaxME.SetDimensionColor(kvp.Key, 186);
                        //塞屬性
                        kvp.Key.SetAttribute(CaxME.DimenAttr.FixDimension, "1");
                        kvp.Key.SetAttribute(CaxME.DimenAttr.DiCount, DiCount.Text);
                    }
                    DiCount.Text = "1";
                }
                if (Variables.CleanDimensionAry.Length != 0)
                {
                    NXObject[] SheetObj = CaxME.FindObjectsInView(Variables.drawingSheet1.View.Tag).ToArray();

                    foreach (NXObject i in Variables.CleanDimensionAry)
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
                            CaxME.DeleteFixBallon(BallonNum, SheetObj);
                        }

                        i.DeleteAllAttributesByType(NXObject.AttributeType.String);
                    }
                }

                MessageBox.Show("設定完成！");
                
            }
            catch (System.Exception ex)
            {
                MessageBox.Show("執行失敗，請聯繫開發工程師");
                this.Close();
                return;
            }
        }

        private void Close_Click(object sender, EventArgs e)
        {
            this.Close();
            return;
        }

        private void CleaningAttr_Click(object sender, EventArgs e)
        {
            try
            {
                this.Hide();
                CaxPublic.SelectObjects(out Variables.CleanDimensionAry);
                Variables.DicCleanDimension = new Dictionary<NXObject, string>();
                foreach (NXObject single in Variables.CleanDimensionAry)
                {
                    string DimenType = single.GetType().ToString();
                    Variables.DicCleanDimension.Add(single, DimenType);
                }
                CleaningAttr.Text = string.Format("移除屬性({0})", Variables.CleanDimensionAry.Length.ToString());
                this.Show();
            }
            catch (System.Exception ex)
            {
                MessageBox.Show("選擇尺寸的對話框載入失敗");
            }
        }

        private void ListSheet_SelectedIndexChanged(object sender, EventArgs e)
        {
            try
            {
                Variables.drawingSheet1 = (NXOpen.Drawings.DrawingSheet)workPart.DrawingSheets.FindObject(ListSheet.Text);
                Variables.drawingSheet1.Open();
            }
            catch (System.Exception ex)
            {
            }
        }

       
    }
}
