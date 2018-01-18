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
using NXOpen.Annotations;
using NXOpen.Utilities;
using NXOpen.Drawings;

namespace FixCreateBalloon
{
    public partial class FixCreateBalloonDlg : Office2007Form
    {
        public static Session theSession = Session.GetSession();
        public static UI theUI;
        public static UFSession theUfSession = UFSession.GetUFSession();
        public static Part workPart = theSession.Parts.Work;
        public static Part displayPart = theSession.Parts.Display;

        public FixCreateBalloonDlg()
        {
            InitializeComponent();
        }

        private void FixCreateBalloonDlg_Load(object sender, EventArgs e)
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
        }

        private void OK_Click(object sender, EventArgs e)
        {
            //抓取目前圖紙數量和Tag
            int SheetCount = 0;
            NXOpen.Tag[] SheetTagAry = null;
            theUfSession.Draw.AskDrawings(out SheetCount, out SheetTagAry);
            //取得最後一顆泡泡的數字
            int MaxBallonNum;
            try
            {
                MaxBallonNum = Convert.ToInt32(workPart.GetStringAttribute(CaxME.DimenAttr.BallonNum));
            }
            catch (System.Exception ex)
            {
                MaxBallonNum = 0;
            }

            if (chb_Regeneration.Checked == true)
            {
                #region 刪除全部泡泡
                IdSymbolCollection BallonCollection = workPart.Annotations.IdSymbols;
                IdSymbol[] BallonAry = BallonCollection.ToArray();
                foreach (IdSymbol i in BallonAry)
                {
                    try
                    {
                        i.GetStringAttribute("BalloonAtt");
                    }
                    catch (System.Exception ex)
                    {
                        continue;
                    }
                    CaxPublic.DelectObject(i);
                }
                workPart.DeleteAttributeByTypeAndTitle(NXObject.AttributeType.String, "BALLONNUM");
                #endregion

                for (int i = 0; i < SheetCount; i++)
                {
                    //打開Sheet並記錄所有OBJ
                    DrawingSheet CurrentSheet = (NXOpen.Drawings.DrawingSheet)NXObjectManager.Get(SheetTagAry[i]);
                    CurrentSheet.Open();
                    if (i == 0)
                    {
                        Variables.FirstDrawingSheet = CurrentSheet;
                    }

                    int BallonNum = 0;
                    NXObject[] SheetObj = CaxME.FindObjectsInView(CurrentSheet.View.Tag).ToArray();
                    foreach (NXObject singleObj in SheetObj)
                    {
                        string diCount = "", fixDiemnsion = "";
                        #region 刪除尺寸數量產生的文字(ex:a-c)
                        try
                        {
                            diCount = singleObj.GetStringAttribute(CaxME.DimenAttr.DiCount);
                        }
                        catch (System.Exception ex)
                        {
                            diCount = "";
                        }
                        try
                        {
                            fixDiemnsion = singleObj.GetStringAttribute(CaxME.DimenAttr.FixDimension);
                        }
                        catch (System.Exception ex)
                        {
                            fixDiemnsion = "";
                        }
                        if (diCount != "" && fixDiemnsion == "")
                        {
                            CaxPublic.DelectObject(singleObj);
                        }
                        #endregion

                        string AssignExcelType = "";
                        #region 判斷是否有屬性，沒有屬性就跳下一個
                        try { AssignExcelType = singleObj.GetStringAttribute(CaxME.DimenAttr.FixDimension); }
                        catch (System.Exception ex) { continue; }
                        #endregion


                        //事先塞入該尺寸所在Sheet
                        singleObj.SetAttribute("SheetName", CurrentSheet.Name);

                        CaxME.BoxCoordinate cBoxCoordinate = new CaxME.BoxCoordinate();
                        CaxME.GetTextBoxCoordinate(singleObj.Tag, out cBoxCoordinate);

                        #region 計算泡泡座標
                        CaxME.DimenData sDimenData = new CaxME.DimenData();
                        sDimenData.Obj = singleObj;
                        sDimenData.CurrentSheet = CurrentSheet;
                        CaxME.CalculateBallonCoordinate(cBoxCoordinate, ref sDimenData);
                        #endregion

                        sDimenData.CurrentSheet.Open();

                        Point3d BallonLocation = new Point3d();
                        BallonLocation.X = sDimenData.LocationX;
                        BallonLocation.Y = sDimenData.LocationY;

                        BallonNum++;
                        InsertBalloon(BallonNum, diCount, BallonLocation);
                        singleObj.SetAttribute(CaxME.DimenAttr.BallonNum, BallonNum.ToString());
                    }


                    //將最後一顆泡泡的數字插入零件中
                    workPart.SetAttribute(CaxME.DimenAttr.BallonNum, BallonNum.ToString());
                }
            }
            else if (chb_keepOrigination.Checked == true)
            {
                for (int i = 0; i < SheetCount; i++)
                {
                    //打開Sheet並記錄所有OBJ
                    DrawingSheet CurrentSheet = (NXOpen.Drawings.DrawingSheet)NXObjectManager.Get(SheetTagAry[i]);
                    CurrentSheet.Open();
                    if (i == 0)
                    {
                        Variables.FirstDrawingSheet = CurrentSheet;
                    }

                    NXObject[] SheetObj = CaxME.FindObjectsInView(CurrentSheet.View.Tag).ToArray();
                    foreach (NXObject singleObj in SheetObj)
                    {
                        //判斷是否取到舊的尺寸，如果是就跳下一個
                        string OldBallonNum = "";
                        try
                        {
                            OldBallonNum = singleObj.GetStringAttribute(CaxME.DimenAttr.BallonNum);
                            continue;
                        }
                        catch (System.Exception ex) { }

                        string AssignExcelType = "";
                        #region 判斷是否有屬性，沒有屬性就跳下一個
                        try { AssignExcelType = singleObj.GetStringAttribute(CaxME.DimenAttr.FixDimension); }
                        catch (System.Exception ex) { continue; }
                        #endregion

                        //事先塞入該尺寸所在Sheet
                        singleObj.SetAttribute("SheetName", CurrentSheet.Name);

                        CaxME.BoxCoordinate cBoxCoordinate = new CaxME.BoxCoordinate();
                        CaxME.GetTextBoxCoordinate(singleObj.Tag, out cBoxCoordinate);

                        #region 計算泡泡座標
                        CaxME.DimenData sDimenData = new CaxME.DimenData();
                        sDimenData.Obj = singleObj;
                        sDimenData.CurrentSheet = CurrentSheet;
                        CaxME.CalculateBallonCoordinate(cBoxCoordinate, ref sDimenData);
                        #endregion

                        sDimenData.CurrentSheet.Open();

                        Point3d BallonLocation = new Point3d();
                        BallonLocation.X = sDimenData.LocationX;
                        BallonLocation.Y = sDimenData.LocationY;

                        MaxBallonNum++;
                        string diCount = "";
                        try
                        {
                            diCount = singleObj.GetStringAttribute(CaxME.DimenAttr.DiCount);
                        }
                        catch (System.Exception ex)
                        {
                            diCount = "1";
                        }

                        InsertBalloon(MaxBallonNum, diCount, BallonLocation);
                        singleObj.SetAttribute(CaxME.DimenAttr.BallonNum, MaxBallonNum.ToString());
                    }
                    //將最後一顆泡泡的數字插入零件中
                    workPart.SetAttribute(CaxME.DimenAttr.BallonNum, MaxBallonNum.ToString());
                }
            }

            MessageBox.Show("完成！");
        }

        private bool InsertBalloon(int balloonNum, string diCount, Point3d BallonLocation)
        {
            try
            {
                double BallonNumSize = 0;
                
                //決定數字的大小
                if (balloonNum <= 9)
                {
                    BallonNumSize = 2.5;
                }
                else if (balloonNum > 9 && balloonNum <= 99)
                {
                    BallonNumSize = 1.5;
                }
                else
                {
                    BallonNumSize = 1;
                }
                NXObject balloonObj = null;
                CaxME.CreateBallonOnSheet(balloonNum.ToString(), BallonLocation, BallonNumSize, "BalloonAtt", out balloonObj);
                //如果大於1表示要插入a.b.c.....
                if (diCount != "1")
                {
                    //文字座標
                    CaxME.BoxCoordinate sBoxCoordinate = new CaxME.BoxCoordinate();
                    CaxME.GetTextBoxCoordinate(balloonObj.Tag, out sBoxCoordinate);
                    Point3d textCoord = new Point3d(sBoxCoordinate.lower_left[0] + 1.5, sBoxCoordinate.lower_left[1] - 1.5, 0);
                    string countText = Convert.ToChar(65 + 0).ToString().ToLower() + "-" + Convert.ToChar(65 + Convert.ToInt32(diCount) - 1).ToString().ToLower();
                    CaxME.InsertDicountNote(balloonNum.ToString(), CaxME.DimenAttr.DiCount, countText, "1.8", textCoord);
                }

            }
            catch (System.Exception ex)
            {
                return false;
            }
            return true;
        }

        private void Cancel_Click(object sender, EventArgs e)
        {
            this.Close();
            return;
        }

        private void chb_Regeneration_CheckedChanged(object sender, EventArgs e)
        {
            if (chb_Regeneration.Checked == true)
            {
                chb_keepOrigination.Checked = false;
            }
        }

        private void chb_keepOrigination_CheckedChanged(object sender, EventArgs e)
        {
            if (chb_keepOrigination.Checked == true)
            {
                chb_Regeneration.Checked = false;
            }
        }
    }
}
