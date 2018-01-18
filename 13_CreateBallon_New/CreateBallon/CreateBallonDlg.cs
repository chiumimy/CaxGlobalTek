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
using NXOpen.Utilities;
using CaxGlobaltek;
using DevComponents.DotNetBar.SuperGrid;

namespace CreateBallon
{
    public partial class CreateBallonDlg : Office2007Form
    {
        public static Session theSession = Session.GetSession();
        public static UI theUI;
        public static UFSession theUfSession = UFSession.GetUFSession();
        public static Part workPart = theSession.Parts.Work;
        public static Part displayPart = theSession.Parts.Display;
        public int SheetCount = 0;
        public NXOpen.Tag[] SheetTagAry = null;
        public static Dictionary<NXObject, Sheet_DefineNum> DicUserDefineData = new Dictionary<NXObject, Sheet_DefineNum>();
        public static string CurrentSheetName = "";
        public static DisplayableObject exOnj;
        public CreateBallonDlg(Dictionary<NXObject, Sheet_DefineNum> DicUserDefine)
        {
            InitializeComponent();
            DicUserDefineData = DicUserDefine;
            InsertUserDefineDataToPanel(DicUserDefineData);
        }

        private void InsertUserDefineDataToPanel(Dictionary<NXObject, Sheet_DefineNum> DicUserDefineData)
        {
            int count = 0;
            GridRow row = new GridRow();
            foreach (KeyValuePair<NXObject, Sheet_DefineNum> kvp in DicUserDefineData)
            {
                row = new GridRow(new object[SGC.PrimaryGrid.Columns.Count]);
                SGC.PrimaryGrid.Rows.Add(row);
                count = count + 1;
                row.Cells["序號"].Value = count;
                row.Cells["尺寸位置"].Value = kvp.Value.sheet;
                row.Cells["自定泡泡號"].Value = kvp.Value.defineNum;
                row.Cells["Dimension"].Value = kvp.Key;
            }
        }


        private void chb_keepOrigination_CheckedChanged(object sender, EventArgs e)
        {
            if (chb_keepOrigination.Checked == true)
            {
                chb_Regeneration.Checked = false;
                chb_UserDefine.Checked = false;
            }
        }

        private void chb_Regeneration_CheckedChanged(object sender, EventArgs e)
        {
            if (chb_Regeneration.Checked == true)
            {
                chb_keepOrigination.Checked = false;
                chb_UserDefine.Checked = false;
            }
        }

        private void OK_Click(object sender, EventArgs e)
        {
            if (chb_keepOrigination.Checked == false && chb_Regeneration.Checked == false && chb_UserDefine.Checked == false)
            {
                this.Hide();
                UI.GetUI().NXMessageBox.Show("Message", NXMessageBox.DialogType.Information, "請先選擇一個選項");
                this.Show();
                return;
            }
            if (chb_keepOrigination.Checked == true)
            {
                Is_Keep = true;
                this.DialogResult = DialogResult.Yes;
                this.Close();
            }
            if (chb_Regeneration.Checked == true)
            {
                Is_Keep = false;
                this.DialogResult = DialogResult.Yes;
                this.Close();
            }
            if (chb_UserDefine.Checked == true)
            {
                //判斷所有的泡泡是否有重複
                List<string> ListIsRepeat = new List<string>();
                foreach (GridRow i in SGC.PrimaryGrid.Rows)
                {
                    if (i.Cells["自定泡泡號"].Value.ToString() == "")
                    {
                        continue;
                    }
                    if (ListIsRepeat.Contains(i.Cells["自定泡泡號"].Value.ToString()))
                    {
                        MessageBox.Show("泡泡號【" + i.Cells["自定泡泡號"].Value.ToString() + "】重複，請重新檢查");
                        return;
                    }
                    else
                    {
                        ListIsRepeat.Add(i.Cells["自定泡泡號"].Value.ToString());
                    }
                }
                CoordinateData cCoordinateData = new CoordinateData();
                CaxGetDatData.GetDraftingCoordinateData(out cCoordinateData);

                //開始插入自定義泡泡
                foreach (GridRow i in SGC.PrimaryGrid.Rows)
                {
                    //判斷是否有舊的泡泡，如果舊泡泡與自定的相同，則跳下一個
                    //判斷是否有舊的泡泡，如果舊泡泡與自定的不相同，則先刪除泡泡再重新產生
                    //如有Dicount也要加入一起生成


                    //如果沒有自定泡泡就跳下一個row
                    if (i.Cells["自定泡泡號"].Value.ToString() == "")
                    {
                        //判斷是否需刪除已存在的泡泡
                        NXOpen.Drawings.DrawingSheet drawingSheet1 = (NXOpen.Drawings.DrawingSheet)workPart.DrawingSheets.FindObject(i.Cells["尺寸位置"].Value.ToString());
                        string oldBalloon = "";
                        try
                        {
                            oldBalloon = ((NXObject)i.Cells["Dimension"].Value).GetStringAttribute(CaxME.DimenAttr.BallonNum);
                        }
                        catch (System.Exception ex)
                        {
                            continue;
                        }
                        NXObject[] SheetObj = CaxME.FindObjectsInView(drawingSheet1.View.Tag).ToArray();
                        CaxME.DeleteBallon(oldBalloon, SheetObj);
                        workPart.Views.Refresh();
                        //刪除屬性
                        ((NXObject)i.Cells["Dimension"].Value).DeleteAttributeByTypeAndTitle(NXObject.AttributeType.String, CaxME.DimenAttr.BallonNum);
                        ((NXObject)i.Cells["Dimension"].Value).DeleteAttributeByTypeAndTitle(NXObject.AttributeType.String, CaxME.DimenAttr.BallonLocation);
                        ((NXObject)i.Cells["Dimension"].Value).DeleteAttributeByTypeAndTitle(NXObject.AttributeType.String, CaxME.DimenAttr.SheetName);
                        continue;
                    }
                    //如果回傳False，表示舊泡泡=自定泡泡；如果回傳True，表示舊泡泡=\=自定泡泡
                    if (!JudgmentBalloon(i))
                    {
                        continue;
                    }
                    InsertBalloon(i, cCoordinateData);
                }
                if (exOnj != null)
                {
                    exOnj.Unhighlight();
                    workPart.Views.Refresh();
                }
                
                //(NXOpen.Drawings.DrawingSheet)workPart.DrawingSheets.FindObject(ListSheet.Text)
                //((NXOpen.Drawings.DrawingSheet)workPart.DrawingSheets.FindObject("S1")).Open();
                this.Close();
            }
            
        }

        private void InsertBalloon(GridRow row, CoordinateData cCoordinateData)
        {
            //取得圖紙長、高
            NXOpen.Drawings.DrawingSheet drawingSheet1 = (NXOpen.Drawings.DrawingSheet)workPart.DrawingSheets.FindObject(row.Cells["尺寸位置"].Value.ToString());
            drawingSheet1.Open();
            double SheetLength = drawingSheet1.Length;
            double SheetHeight = drawingSheet1.Height;

            //事先塞入該尺寸所在Sheet
            ((NXObject)row.Cells["Dimension"].Value).SetAttribute(CaxME.DimenAttr.SheetName, row.Cells["尺寸位置"].Value.ToString());
            //計算泡泡座標
            CaxME.BoxCoordinate cBoxCoordinate = new CaxME.BoxCoordinate();
            CaxME.GetTextBoxCoordinate(((NXOpen.Annotations.Annotation)row.Cells["Dimension"].Value).Tag, out cBoxCoordinate);
            DimenData sDimenData = new DimenData();
            Functions.CalculateBallonCoordinate(cBoxCoordinate, ref sDimenData);
            Point3d BallonLocation = new Point3d();
            BallonLocation.X = sDimenData.LocationX;
            BallonLocation.Y = sDimenData.LocationY;
            //決定數字的大小
            double BallonNumSize = 0;
            if (Convert.ToInt32(row.Cells["自定泡泡號"].Value) <= 9)
            {
                BallonNumSize = 2.5;
            }
            else if (Convert.ToInt32(row.Cells["自定泡泡號"].Value) > 9 && Convert.ToInt32(row.Cells["自定泡泡號"].Value) <= 99)
            {
                BallonNumSize = 1.5;
            }
            else
            {
                BallonNumSize = 1;
            }
            NXObject balloonObj = null;
            CaxME.CreateBallonOnSheet(row.Cells["自定泡泡號"].Value.ToString(), BallonLocation, BallonNumSize, "BalloonAtt", out balloonObj);

            //取得該尺寸數量
            string diCount = "";
            try
            {
                diCount = ((NXObject)row.Cells["Dimension"].Value).GetStringAttribute(CaxME.DimenAttr.DiCount);
            }
            catch (System.Exception ex)
            {
                //當遇到舊料號沒有Dicount的屬性時，在這邊補上
                ((NXObject)row.Cells["Dimension"].Value).SetAttribute(CaxME.DimenAttr.DiCount, "1");
                diCount = "1";
            }
            //如果大於1表示要插入a.b.c.....
            if (diCount != "1")
            {
                //文字座標
                CaxME.BoxCoordinate sBoxCoordinate = new CaxME.BoxCoordinate();
                CaxME.GetTextBoxCoordinate(balloonObj.Tag, out sBoxCoordinate);
                Point3d textCoord = new Point3d(sBoxCoordinate.lower_left[0] + 1.5, sBoxCoordinate.lower_left[1] - 1.5, 0);
                string countText = Convert.ToChar(65 + 0).ToString().ToLower() + "-" + Convert.ToChar(65 + Convert.ToInt32(diCount) - 1).ToString().ToLower();
                CaxME.InsertDicountNote(row.Cells["自定泡泡號"].Value.ToString(), CaxME.DimenAttr.DiCount, countText, "1.8", textCoord);
            }

            #region 計算泡泡相對位置
            string RegionX = "", RegionY = "";
            for (int ii = 0; ii < cCoordinateData.DraftingCoordinate.Count; ii++)
            {
                string SheetSize = cCoordinateData.DraftingCoordinate[ii].SheetSize;
                if (Math.Ceiling(SheetHeight) != Convert.ToDouble(SheetSize.Split(',')[0]) || Math.Ceiling(SheetLength) != Convert.ToDouble(SheetSize.Split(',')[1]))
                {
                    continue;
                }
                //比對X
                for (int j = 0; j < cCoordinateData.DraftingCoordinate[ii].RegionX.Count; j++)
                {
                    string X0 = cCoordinateData.DraftingCoordinate[ii].RegionX[j].X0;
                    string X1 = cCoordinateData.DraftingCoordinate[ii].RegionX[j].X1;
                    if (BallonLocation.X >= Convert.ToDouble(X0) && BallonLocation.X <= Convert.ToDouble(X1))
                    {
                        RegionX = cCoordinateData.DraftingCoordinate[ii].RegionX[j].Zone;
                    }
                }
                //比對Y
                for (int j = 0; j < cCoordinateData.DraftingCoordinate[ii].RegionY.Count; j++)
                {
                    string Y0 = cCoordinateData.DraftingCoordinate[ii].RegionY[j].Y0;
                    string Y1 = cCoordinateData.DraftingCoordinate[ii].RegionY[j].Y1;
                    if (BallonLocation.Y >= Convert.ToDouble(Y0) && BallonLocation.Y <= Convert.ToDouble(Y1))
                    {
                        RegionY = cCoordinateData.DraftingCoordinate[ii].RegionY[j].Zone;
                    }
                }
            }
            #endregion

            ((NXObject)row.Cells["Dimension"].Value).SetAttribute(CaxME.DimenAttr.BallonNum, row.Cells["自定泡泡號"].Value.ToString());
            ((NXObject)row.Cells["Dimension"].Value).SetAttribute(CaxME.DimenAttr.BallonLocation, row.Cells["尺寸位置"].Value.ToString() + "-" + RegionY + RegionX);
            workPart.Views.Refresh();
        }

        private bool JudgmentBalloon(GridRow row)
        {
            string oldBalloon = "";
            try
            {
                oldBalloon = ((NXObject)row.Cells["Dimension"].Value).GetStringAttribute(CaxME.DimenAttr.BallonNum);
                if (oldBalloon == row.Cells["自定泡泡號"].Value.ToString())
                {
                    return false;
                }
                NXOpen.Drawings.DrawingSheet drawingSheet1 = (NXOpen.Drawings.DrawingSheet)workPart.DrawingSheets.FindObject(row.Cells["尺寸位置"].Value.ToString());
                NXObject[] SheetObj = CaxME.FindObjectsInView(drawingSheet1.View.Tag).ToArray();
                CaxME.DeleteBallon(oldBalloon, SheetObj);
                workPart.Views.Refresh();
            }
            catch (System.Exception ex)
            {
                oldBalloon = "";
            }
            return true;
        }

        private void Cancel_Click(object sender, EventArgs e)
        {
            if (exOnj != null)
            {
                exOnj.Unhighlight();
                workPart.Views.Refresh();
            }

            this.DialogResult = DialogResult.No;
            this.Close();
        }

        private void chb_UserDefine_CheckedChanged(object sender, EventArgs e)
        {
            if (chb_UserDefine.Checked == true)
            {
                chb_keepOrigination.Checked = false;
                chb_Regeneration.Checked = false;

                //Dlg變大
                this.Size = new Size(350, 655);

                OK.Location = new System.Drawing.Point(89, 570);
                Cancel.Location = new System.Drawing.Point(170, 570);

                SGC.Size = new Size(250, 355);
                SGC.Location = new System.Drawing.Point(39, 198);
            }
            else
            {
                //Dlg變小
                this.Size = new Size(350, 270);

                OK.Location = new System.Drawing.Point(89, 193);
                Cancel.Location = new System.Drawing.Point(170, 193);

                SGC.Size = new Size(250, 300);
                SGC.Location = new System.Drawing.Point(39, 248);

                if (exOnj != null)
                {
                    exOnj.Unhighlight();
                    workPart.Views.Refresh();
                }
            }
        }

        private void GetDicUserDefine(NXObject[] SheetObj, string SheetName, ref Dictionary<NXObject, Sheet_DefineNum> DicUserDefine)
        {
            try
            {
                foreach (NXObject singleObj in SheetObj)
                {
                    try
                    { singleObj.GetStringAttribute(CaxME.DimenAttr.Gauge); }
                    catch (System.Exception ex)
                    { continue; }

                    Sheet_DefineNum sSheet_DefineNum = new Sheet_DefineNum();
                    sSheet_DefineNum.sheet = SheetName;
                    try
                    { sSheet_DefineNum.defineNum = singleObj.GetStringAttribute(CaxME.DimenAttr.BallonNum); }
                    catch (System.Exception ex)
                    { sSheet_DefineNum.defineNum = ""; }
                    DicUserDefine[singleObj] = sSheet_DefineNum;
                }
            }
            catch (System.Exception ex)
            {
            	
            }
        }

        private void SGC_CellClick(object sender, GridCellClickEventArgs e)
        {
            #region 尺寸高亮
            if (e.GridCell.GridColumn.Name == "尺寸位置")
            {
                if (exOnj != null)
                {
                    exOnj.Unhighlight();
                }
                
                ((NXOpen.Drawings.DrawingSheet)workPart.DrawingSheets.FindObject(e.GridCell.Value.ToString())).Open();
                ((DisplayableObject)e.GridCell.GridRow.Cells["Dimension"].Value).Highlight();
                exOnj = ((DisplayableObject)e.GridCell.GridRow.Cells["Dimension"].Value);
                workPart.Views.Refresh();
            }
            #endregion
        }

        private void SGC_CellValueChanged(object sender, GridCellValueChangedEventArgs e)
        {
            #region 判斷僅能輸入正整數
            try
            {
                int n;
                string temp = e.GridCell.GridRow.Cells["自定泡泡號"].Value.ToString();
                if (!int.TryParse(temp, out n) && temp != "")
                {
                    MessageBox.Show("請輸入正整數");
                    e.GridCell.GridRow.Cells["自定泡泡號"].Value = "";
                    return;
                }

                #region 判斷是否重複數字，可能要移到按OK才判斷
                //foreach (GridRow i in SGC.PrimaryGrid.GridPanel.Rows)
                //{
                //    if (temp == "")
                //    {
                //        break;
                //    }
                //    if (temp == i.Cells["自定泡泡號"].Value)
                //    {
                //        MessageBox.Show("數字重複，請重心指定");
                //        break;
                //    }
                //}
                #endregion
            }
            catch (System.Exception ex)
            {

            }
            #endregion
        }

        
    }
}
