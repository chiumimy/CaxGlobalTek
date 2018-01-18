using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Text;
using System.Windows.Forms;
using DevComponents.DotNetBar;
using DevComponents.DotNetBar.SuperGrid;
using NXOpen;
using NXOpen.UF;
using NXOpen.Utilities;
using CaxGlobaltek;

namespace RelationCustomerBalloon
{
    public partial class BalloonDlg : Form
    {
        public static Session theSession = Session.GetSession();
        public static Part workPart = theSession.Parts.Work;
        public static UFSession theUfSession = UFSession.GetUFSession();
        public bool status;
        public static int CurrentRowIndex = -1;
        private static GridPanel panel = new GridPanel();
        public List<DisplayableObject> listDisplayObj = new List<DisplayableObject>();
        public static NXOpen.Tag[] SheetTagAry = null;

        public BalloonDlg()
        {
            InitializeComponent();
            panel = SGC_Panel.PrimaryGrid;
            status = InitialDraftingData();
            if (!status)
            {
                MessageBox.Show("初始化圖紙資料失敗");
                return;
            }
        }
        private bool InitialDraftingData()
        {
            try
            {
                int SheetCount = 0;
                //NXOpen.Tag[] SheetTagAry = null;
                theUfSession.Draw.AskDrawings(out SheetCount, out SheetTagAry);
                for (int i = 0; i < SheetCount; i++)
                {
                    NXOpen.Drawings.DrawingSheet CurrentSheet = (NXOpen.Drawings.DrawingSheet)NXObjectManager.Get(SheetTagAry[i]);
                    DraftingBox.Items.Add(CurrentSheet.Name);
                }
                //預設開啟sheet1圖紙
                NXOpen.Drawings.DrawingSheet DefaultSheet = (NXOpen.Drawings.DrawingSheet)NXObjectManager.Get(SheetTagAry[0]);
                DraftingBox.Text = DefaultSheet.Name;
                DefaultSheet.Open();
            }
            catch (System.Exception ex)
            {
                return false;	
            }
            return true;
        }

        private void DraftingBox_SelectedIndexChanged(object sender, EventArgs e)
        {
            try
            {
                //清空Panel資訊
                panel.Rows.Clear();
                //取得試圖上的所有尺寸
                NXOpen.Drawings.DrawingSheet CurrentSheet = (NXOpen.Drawings.DrawingSheet)workPart.DrawingSheets.FindObject(DraftingBox.Text);
                CurrentSheet.Open();
                CurrentSheet.View.UpdateDisplay();
                DisplayableObject[] SheetObj = CurrentSheet.View.AskVisibleObjects();

                GridRow row = new GridRow();
                int count = -1;
                foreach (DisplayableObject singleObj in SheetObj)
                {
                    //取得泡泡，如果沒泡泡表示不是尺寸的資訊
                    string balloon = "";
                    try
                    {
                        balloon = singleObj.GetStringAttribute(CaxME.DimenAttr.BallonNum);
                    }
                    catch (System.Exception ex)
                    {
                        continue;
                    }
                    string customerBalloon = "";
                    try
                    {
                        customerBalloon = singleObj.GetStringAttribute("CustomerBalloon");
                    }
                    catch (System.Exception ex)
                    {
                        customerBalloon = "";
                    }
                    listDisplayObj.Add(singleObj);
                    count++; //singleObj.Highlight(); 
                    row = new GridRow(balloon, customerBalloon);
                    panel.Rows.Add(row);
                    panel.GetCell(count, 0).Tag = singleObj.Tag;
                }
            }
            catch (System.Exception ex)
            {
            	
            }
        }

        private void SGC_Panel_CellClick(object sender, GridCellClickEventArgs e)
        {
            try
            {
                if (CurrentRowIndex != -1)
                {
                    DisplayableObject unhighlight = (DisplayableObject)NXObjectManager.Get((Tag)panel.GetCell(CurrentRowIndex, 0).Tag);
                    unhighlight.Unhighlight();
                }

                CurrentRowIndex = e.GridCell.GridRow.Index;

                DisplayableObject highlight = (DisplayableObject)NXObjectManager.Get((Tag)panel.GetCell(CurrentRowIndex, 0).Tag);
                highlight.Highlight();

                workPart.Views.Refresh();
            }
            catch (System.Exception ex)
            {

            }
        }

        private void OK_Click(object sender, EventArgs e)
        {
            try
            {
                for (int i = 0; i < panel.Rows.Count;i++ )
                {
                    DisplayableObject singleObj = (DisplayableObject)NXObjectManager.Get((Tag)panel.GetCell(i, 0).Tag);
                    singleObj.SetAttribute("CustomerBalloon", panel.GetCell(i, 1).Value.ToString());
                }
                MessageBox.Show("設定完成！");
            }
            catch (System.Exception ex)
            {
            	
            }
        }

        private void Cancel_Click(object sender, EventArgs e)
        {
            try
            {
                foreach (DisplayableObject i in listDisplayObj)
                {
                    i.Unhighlight();
                }
                workPart.Views.Refresh();
                this.Close();
            }
            catch (System.Exception ex)
            {
            	
            }
        }

        

    }
}
