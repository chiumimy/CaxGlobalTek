using System;
using NXOpen;
using NXOpen.UF;
using NXOpen.Annotations;
using CaxGlobaltek;
using System.Collections.Generic;
using CreateBallon;
using NXOpen.Utilities;
using NXOpen.Drawings;
using System.Windows.Forms;
using NXOpenUI;

public class Program
{
    // class members
    private static Session theSession;
    private static UI theUI;
    private static UFSession theUfSession;
    public static Program theProgram;
    public static bool isDisposeCalled;

    //------------------------------------------------------------------------------
    // Constructor
    //------------------------------------------------------------------------------
    public Program()
    {
        try
        {
            theSession = Session.GetSession();
            theUI = UI.GetUI();
            theUfSession = UFSession.GetUFSession();
            isDisposeCalled = false;
        }
        catch (NXOpen.NXException ex)
        {
            // ---- Enter your exception handling code here -----
            // UI.GetUI().NXMessageBox.Show("Message", NXMessageBox.DialogType.Error, ex.Message);
        }
    }

    //------------------------------------------------------------------------------
    //  Explicit Activation
    //      This entry point is used to activate the application explicitly
    //------------------------------------------------------------------------------
    public static int Main(string[] args)
    {
        int retValue = 0;
        try
        {
            theProgram = new Program();

            Session theSession = Session.GetSession();
            Part workPart = theSession.Parts.Work;
            Part displayPart = theSession.Parts.Display;

            int module_id;
            theUfSession.UF.AskApplicationModule(out module_id);
            if (module_id != UFConstants.UF_APP_DRAFTING)
            {
                MessageBox.Show("請先轉換為製圖模組後再執行！");
                return retValue;
            }

            bool status,Is_Keep;

            //抓取目前圖紙數量和Tag
            //取得全部尺寸資料，並整理出尺寸落在的圖紙&尺寸設定的自定義泡泡再填入Panel中(當使用者點自定義時使用)
            int SheetCount = 0;
            NXOpen.Tag[] SheetTagAry = null;
            theUfSession.Draw.AskDrawings(out SheetCount, out SheetTagAry);
            Dictionary<NXObject, Sheet_DefineNum> DicUserDefine = new Dictionary<NXObject, Sheet_DefineNum>();
            for (int i = 0; i < SheetCount; i++)
            {
                NXOpen.Drawings.DrawingSheet CurrentSheet = (NXOpen.Drawings.DrawingSheet)NXObjectManager.Get(SheetTagAry[i]);
                CurrentSheet.Open();
                CurrentSheet.View.UpdateDisplay();
                NXObject[] SheetObj = CaxME.FindObjectsInView(CurrentSheet.View.Tag).ToArray();
                GetUserDefineData(SheetObj, CurrentSheet.Name, ref DicUserDefine);
            }

            NXOpen.Drawings.DrawingSheet DefaultSheet = (NXOpen.Drawings.DrawingSheet)NXObjectManager.Get(SheetTagAry[0]);
            DefaultSheet.Open();

            Application.EnableVisualStyles();
            CreateBallonDlg cCreateBallonDlg = new CreateBallonDlg(DicUserDefine);
            FormUtilities.ReparentForm(cCreateBallonDlg);
            System.Windows.Forms.Application.Run(cCreateBallonDlg);
            if (cCreateBallonDlg.DialogResult == DialogResult.Yes)
            {
                Is_Keep = cCreateBallonDlg.Is_Keep;
                cCreateBallonDlg.Dispose();
            }
            else
            {
                ((NXOpen.Drawings.DrawingSheet)NXObjectManager.Get(SheetTagAry[0])).Open();
                cCreateBallonDlg.Dispose();
                theProgram.Dispose();
                return retValue;
            }

            
            #region 前置處理
            string Is_Local = Environment.GetEnvironmentVariable("UGII_ENV_FILE");
            CoordinateData cCoordinateData = new CoordinateData();
            if (Is_Local != null)
            {
                //取得圖紙範圍資料Data
                status = CaxGetDatData.GetDraftingCoordinateData(out cCoordinateData);
                if (!status)
                    return retValue;
            }
            else
            {
                string DraftingCoordinate_dat = "DraftingCoordinate.dat";
                string DraftingCoordinate_Path = string.Format(@"{0}\{1}", "D:", DraftingCoordinate_dat);
                CaxPublic.ReadCoordinateData(DraftingCoordinate_Path, out cCoordinateData);
            }
                        
            //圖紙長、高
            double SheetLength = 0;
            double SheetHeight = 0;

            
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
            #endregion

            //重新產生泡泡
            if (Is_Keep == false)
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
                

                #region 存DicDimenData(string=檢具名稱,DimenData=尺寸物件、泡泡座標)
                DefineParam.DicDimenData = new Dictionary<string, List<DimenData>>();
                for (int i = 0; i < SheetCount; i++)
                {
                    //打開Sheet並記錄所有OBJ
                    NXOpen.Drawings.DrawingSheet CurrentSheet = (NXOpen.Drawings.DrawingSheet)NXObjectManager.Get(SheetTagAry[i]);
                    //string SheetName = "S" + (i + 1).ToString();
                    //CaxME.SheetRename(CurrentSheet, SheetName);
                    CurrentSheet.Open();

                    if (i == 0)
                    {
                        DefineParam.FirstDrawingSheet = CurrentSheet;
                    }

                    //取得圖紙長、高
                    SheetLength = CurrentSheet.Length;
                    SheetHeight = CurrentSheet.Height;

                    //DisplayableObject[] SheetObj = CurrentSheet.View.AskVisibleObjects();
                    NXObject[] SheetObj = CaxME.FindObjectsInView(CurrentSheet.View.Tag).ToArray();
                    foreach (NXObject singleObj in SheetObj)
                    {
                        #region 刪除尺寸數量產生的文字(ex:a-c)
                        string dicount = "", chekcLevel = "";
                        try
                        {
                            dicount = singleObj.GetStringAttribute(CaxME.DimenAttr.DiCount);
                        }
                        catch (System.Exception ex)
                        {
                            dicount = "";
                        }
                        try
                        {
                            chekcLevel = singleObj.GetStringAttribute(CaxME.DimenAttr.CheckLevel);
                        }
                        catch (System.Exception ex)
                        {
                            chekcLevel = "";
                        }
                        if (dicount != "" && chekcLevel == "")
                        {
                            CaxPublic.DelectObject(singleObj);
                        }
                        #endregion

                        string Gauge = "", AssignExcelType = "";
                        #region 判斷是否有屬性，沒有屬性就跳下一個
                        try{ AssignExcelType = singleObj.GetStringAttribute(CaxME.DimenAttr.AssignExcelType); }
                        catch (System.Exception ex){ continue; }
                        try{ Gauge = singleObj.GetStringAttribute(CaxME.DimenAttr.Gauge); }
                        catch (System.Exception ex){ }
                        #endregion

                        //事先塞入該尺寸所在Sheet
                        singleObj.SetAttribute("SheetName", CurrentSheet.Name);

                        //string DimeType = singleObj.GetType().ToString();
                        CaxME.BoxCoordinate cBoxCoordinate = new CaxME.BoxCoordinate();
                        //GetTextBoxCoordinate(DimeType, singleObj, out cBoxCoordinate);
                        
                        //可以將NXOpen直接轉型成Annotation
                        CaxME.GetTextBoxCoordinate(((NXOpen.Annotations.Annotation)singleObj).Tag, out cBoxCoordinate);
                        
                        #region 計算泡泡座標
                        DimenData sDimenData = new DimenData();
                        sDimenData.Obj = singleObj;
                        sDimenData.CurrentSheet = CurrentSheet;
                        Functions.CalculateBallonCoordinate(cBoxCoordinate, ref sDimenData);
                        #endregion

                        if (Gauge != "")
                        {
                            List<DimenData> ListDimenData = new List<DimenData>();
                            status = DefineParam.DicDimenData.TryGetValue(Gauge, out ListDimenData);
                            if (!status)
                            {
                                ListDimenData = new List<DimenData>();
                            }
                            ListDimenData.Add(sDimenData);
                            DefineParam.DicDimenData[Gauge] = ListDimenData;
                        }
                    }
                }
                #endregion

                //插入泡泡
                int BallonNum = 0;
                InsertBallon(ref BallonNum, cCoordinateData, SheetHeight, SheetLength, "BalloonAtt");

                //將最後一顆泡泡的數字插入零件中
                workPart.SetAttribute(CaxME.DimenAttr.BallonNum, BallonNum.ToString());
            }
            //保留泡泡
            else
            {
                #region 存DicDimenData(string=檢具名稱,DimenData=尺寸物件、泡泡座標)
                DefineParam.DicDimenData = new Dictionary<string, List<DimenData>>();
                for (int i = 0; i < SheetCount; i++)
                {
                    //打開Sheet並記錄所有OBJ
                    NXOpen.Drawings.DrawingSheet CurrentSheet = (NXOpen.Drawings.DrawingSheet)NXObjectManager.Get(SheetTagAry[i]);
                    CurrentSheet.Open();

                    if (i == 0)
                    {
                        DefineParam.FirstDrawingSheet = CurrentSheet;
                    }

                    //取得圖紙長、高
                    SheetLength = CurrentSheet.Length;
                    SheetHeight = CurrentSheet.Height;

                    //DisplayableObject[] SheetObj = CurrentSheet.View.AskVisibleObjects();
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
                        catch (System.Exception ex) {  }
                         
                        //判斷是否有屬性，沒有屬性就跳下一個
                        string Gauge = "", AssignExcelType = "";
                        try { AssignExcelType = singleObj.GetStringAttribute(CaxME.DimenAttr.AssignExcelType); }
                        catch (System.Exception ex) { continue; }
                        
                        try { Gauge = singleObj.GetStringAttribute(CaxME.DimenAttr.Gauge); }
                        catch (System.Exception ex) { }
                        
                        //事先塞入該尺寸所在Sheet
                        singleObj.SetAttribute("SheetName", CurrentSheet.Name);

                        //string DimeType = "";
                        //DimeType = singleObj.GetType().ToString();
                        CaxME.BoxCoordinate cBoxCoordinate = new CaxME.BoxCoordinate();

                        //GetTextBoxCoordinate(DimeType, singleObj, out cBoxCoordinate);
                        //CaxLog.ShowListingWindow(cBoxCoordinate.lower_left[0].ToString());
                        CaxME.GetTextBoxCoordinate(((NXOpen.Annotations.Annotation)singleObj).Tag, out cBoxCoordinate);
                        //CaxLog.ShowListingWindow(cBoxCoordinate.lower_left[0].ToString());
                        #region 計算泡泡座標
                        DimenData sDimenData = new DimenData();
                        sDimenData.Obj = singleObj;
                        sDimenData.CurrentSheet = CurrentSheet;
                        Functions.CalculateBallonCoordinate(cBoxCoordinate, ref sDimenData);
                        #endregion

                        if (Gauge != "")
                        {
                            List<DimenData> ListDimenData = new List<DimenData>();
                            status = DefineParam.DicDimenData.TryGetValue(Gauge, out ListDimenData);
                            if (!status)
                            {
                                ListDimenData = new List<DimenData>();
                            }
                            ListDimenData.Add(sDimenData);
                            DefineParam.DicDimenData[Gauge] = ListDimenData;
                        }
                    }
                }
                #endregion

                if (DefineParam.DicDimenData.Count != 0)
                {
                    //插入泡泡
                    InsertBallon(ref MaxBallonNum, cCoordinateData, SheetHeight, SheetLength, "BalloonAtt");

                    //將最後一顆泡泡的數字插入零件中
                    workPart.SetAttribute(CaxME.DimenAttr.BallonNum, MaxBallonNum.ToString());
                }
            }

            //切回第一張Sheet
            DefineParam.FirstDrawingSheet.Open();

            MessageBox.Show("完成");
            theProgram.Dispose();
        }
        catch (NXOpen.NXException ex)
        {
            // ---- Enter your exception handling code here -----
            CaxLog.ShowListingWindow(ex.ToString());
        }
        return retValue;
    }

    private static void GetUserDefineData(NXObject[] SheetObj, string SheetName, ref Dictionary<NXObject, Sheet_DefineNum> DicUserDefine)
    {
        try
        {
            int count = -1;
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

    private static void InsertBallon(ref int MaxBallonNum, CoordinateData cCoordinateData, double SheetHeight, double SheetLength, string BalloonAtt)
    {
        double BallonNumSize = 0;
        foreach (KeyValuePair<string, List<DimenData>> kvp in DefineParam.DicDimenData)
        {
            List<DimenData> tempListDimenData = new List<DimenData>();
            DefineParam.DicDimenData.TryGetValue(kvp.Key, out tempListDimenData);
            for (int i = 0; i < tempListDimenData.Count; i++)
            {
                tempListDimenData[i].CurrentSheet.Open();
                MaxBallonNum++;
                Point3d BallonLocation = new Point3d();
                BallonLocation.X = tempListDimenData[i].LocationX;
                BallonLocation.Y = tempListDimenData[i].LocationY;
                //決定數字的大小
                if (MaxBallonNum <= 9)
                {
                    BallonNumSize = 2.5;
                }
                else if (MaxBallonNum > 9 && MaxBallonNum <= 99)
                {
                    BallonNumSize = 1.5;
                }
                else
                {
                    BallonNumSize = 1;
                }
                NXObject balloonObj = null;
                CaxME.CreateBallonOnSheet(MaxBallonNum.ToString(), BallonLocation, BallonNumSize, BalloonAtt, out balloonObj);

                //取得該尺寸數量
                string diCount = "";
                try
                {
                    diCount = tempListDimenData[i].Obj.GetStringAttribute(CaxME.DimenAttr.DiCount);
                }
                catch (System.Exception ex)
                {
                    //當遇到舊料號沒有Dicount的屬性時，在這邊補上
                    tempListDimenData[i].Obj.SetAttribute(CaxME.DimenAttr.DiCount, "1");
                    diCount = "1";
                }
                //如果大於1表示要插入a.b.c.....
                if (diCount != "1")
                {
                    //文字座標
                    CaxME.BoxCoordinate sBoxCoordinate = new CaxME.BoxCoordinate();
                    CaxME.GetTextBoxCoordinate(balloonObj.Tag, out sBoxCoordinate);
                    Point3d textCoord = new Point3d ( sBoxCoordinate.lower_left[0] + 1.5, sBoxCoordinate.lower_left[1] -1.5, 0 );
                    string countText = Convert.ToChar(65 + 0).ToString().ToLower() + "-" + Convert.ToChar(65 + Convert.ToInt32(diCount) - 1).ToString().ToLower();
                    CaxME.InsertDicountNote(MaxBallonNum.ToString(), CaxME.DimenAttr.DiCount, countText, "1.8", textCoord);
                }


                //取得該尺寸所在圖紙
                string SheetNum = tempListDimenData[i].Obj.GetStringAttribute("SheetName");
                #region 計算泡泡相對位置
                //計算泡泡相對位置
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
                tempListDimenData[i].Obj.SetAttribute(CaxME.DimenAttr.BallonNum, MaxBallonNum.ToString());
                tempListDimenData[i].Obj.SetAttribute(CaxME.DimenAttr.BallonLocation, SheetNum + "-" + RegionY + RegionX);
            }
        }
    }
    

    private static void GetTextBoxCoordinate(string DimeType, NXObject singleObj, out CaxME.BoxCoordinate cBoxCoordinate)
    {
        cBoxCoordinate = new CaxME.BoxCoordinate();
        if (DimeType == "NXOpen.Annotations.VerticalDimension")
        {
            #region VerticalDimension取Location
            NXOpen.Annotations.VerticalDimension temp = (NXOpen.Annotations.VerticalDimension)singleObj;
            CaxME.GetTextBoxCoordinate(temp.Tag, out cBoxCoordinate);
            #endregion
        }
        else if (DimeType == "NXOpen.Annotations.PerpendicularDimension")
        {
            #region PerpendicularDimension取Location
            NXOpen.Annotations.PerpendicularDimension temp = (NXOpen.Annotations.PerpendicularDimension)singleObj;
            CaxME.GetTextBoxCoordinate(temp.Tag, out cBoxCoordinate);
            #endregion
        }
        else if (DimeType == "NXOpen.Annotations.MinorAngularDimension")
        {
            #region MinorAngularDimension取Location
            NXOpen.Annotations.MinorAngularDimension temp = (NXOpen.Annotations.MinorAngularDimension)singleObj;
            CaxME.GetTextBoxCoordinate(temp.Tag, out cBoxCoordinate);
            #endregion
        }
        else if (DimeType == "NXOpen.Annotations.MajorAngularDimension")
        {
            #region MajorAngularDimension取Location
            NXOpen.Annotations.MajorAngularDimension temp = (NXOpen.Annotations.MajorAngularDimension)singleObj;
            CaxME.GetTextBoxCoordinate(temp.Tag, out cBoxCoordinate);
            #endregion
        }
        else if (DimeType == "NXOpen.Annotations.RadiusDimension")
        {
            #region MinorAngularDimension取Location
            NXOpen.Annotations.RadiusDimension temp = (NXOpen.Annotations.RadiusDimension)singleObj;
            CaxME.GetTextBoxCoordinate(temp.Tag, out cBoxCoordinate);
            #endregion
        }
        else if (DimeType == "NXOpen.Annotations.HorizontalDimension")
        {
            #region HorizontalDimension取Location
            NXOpen.Annotations.HorizontalDimension temp = (NXOpen.Annotations.HorizontalDimension)singleObj;
            CaxME.GetTextBoxCoordinate(temp.Tag, out cBoxCoordinate);
            #endregion
        }
        else if (DimeType == "NXOpen.Annotations.IdSymbol")
        {
            #region IdSymbol取Location
            NXOpen.Annotations.IdSymbol temp = (NXOpen.Annotations.IdSymbol)singleObj;
            CaxME.GetTextBoxCoordinate(temp.Tag, out cBoxCoordinate);
            #endregion
        }
        else if (DimeType == "NXOpen.Annotations.Note")
        {
            #region Note取Location
            NXOpen.Annotations.Note temp = (NXOpen.Annotations.Note)singleObj;
            CaxME.GetTextBoxCoordinate(temp.Tag, out cBoxCoordinate);
            #endregion
        }
        else if (DimeType == "NXOpen.Annotations.DraftingFcf")
        {
            #region DraftingFcf取Location
            NXOpen.Annotations.DraftingFcf temp = (NXOpen.Annotations.DraftingFcf)singleObj;
            CaxME.GetTextBoxCoordinate(temp.Tag, out cBoxCoordinate);
            #endregion
        }
        else if (DimeType == "NXOpen.Annotations.Label")
        {
            #region Label取Location
            NXOpen.Annotations.Label temp = (NXOpen.Annotations.Label)singleObj;
            CaxME.GetTextBoxCoordinate(temp.Tag, out cBoxCoordinate);
            #endregion
        }
        else if (DimeType == "NXOpen.Annotations.DraftingDatum")
        {
            #region DraftingDatum取Location
            NXOpen.Annotations.DraftingDatum temp = (NXOpen.Annotations.DraftingDatum)singleObj;
            CaxME.GetTextBoxCoordinate(temp.Tag, out cBoxCoordinate);
            #endregion
        }
        else if (DimeType == "NXOpen.Annotations.DiameterDimension")
        {
            #region DiameterDimension取Location
            NXOpen.Annotations.DiameterDimension temp = (NXOpen.Annotations.DiameterDimension)singleObj;
            CaxME.GetTextBoxCoordinate(temp.Tag, out cBoxCoordinate);
            #endregion
        }
        else if (DimeType == "NXOpen.Annotations.AngularDimension")
        {
            #region AngularDimension取Location
            NXOpen.Annotations.AngularDimension temp = (NXOpen.Annotations.AngularDimension)singleObj;
            CaxME.GetTextBoxCoordinate(temp.Tag, out cBoxCoordinate);
            #endregion
        }
        else if (DimeType == "NXOpen.Annotations.CylindricalDimension")
        {
            #region CylindricalDimension取Location
            NXOpen.Annotations.CylindricalDimension temp = (NXOpen.Annotations.CylindricalDimension)singleObj;
            CaxME.GetTextBoxCoordinate(temp.Tag, out cBoxCoordinate);
            #endregion
        }
        else if (DimeType == "NXOpen.Annotations.ChamferDimension")
        {
            #region ChamferDimension取Location
            NXOpen.Annotations.ChamferDimension temp = (NXOpen.Annotations.ChamferDimension)singleObj;
            CaxME.GetTextBoxCoordinate(temp.Tag, out cBoxCoordinate);
            #endregion
        }
        else if (DimeType == "NXOpen.Annotations.HoleDimension")
        {
            #region HoleDimension取Location
            NXOpen.Annotations.HoleDimension temp = (NXOpen.Annotations.HoleDimension)singleObj;
            CaxME.GetTextBoxCoordinate(temp.Tag, out cBoxCoordinate);
            //CaxLog.ShowListingWindow(cBoxCoordinate.lower_left[0].ToString());
            //CaxLog.ShowListingWindow(cBoxCoordinate.lower_left[1].ToString());
            //CaxLog.ShowListingWindow(cBoxCoordinate.lower_right[0].ToString());
            //CaxLog.ShowListingWindow(cBoxCoordinate.lower_right[1].ToString());
            //CaxLog.ShowListingWindow(cBoxCoordinate.upper_right[0].ToString());
            //CaxLog.ShowListingWindow(cBoxCoordinate.upper_right[1].ToString());
            //CaxLog.ShowListingWindow(cBoxCoordinate.upper_left[0].ToString());
            //CaxLog.ShowListingWindow(cBoxCoordinate.upper_left[1].ToString());
            //CaxLog.ShowListingWindow("----");
            //CaxME.DrawTextBox(temp.Tag);
            #endregion
        }
        else if (DimeType == "NXOpen.Annotations.ParallelDimension")
        {
            #region ParallelDimension取Location
            NXOpen.Annotations.ParallelDimension temp = (NXOpen.Annotations.ParallelDimension)singleObj;
            CaxME.GetTextBoxCoordinate(temp.Tag, out cBoxCoordinate);
            #endregion
        }
        else if (DimeType == "NXOpen.Annotations.FoldedRadiusDimension")
        {
            #region FoldedRadiusDimension取Location
            NXOpen.Annotations.FoldedRadiusDimension temp = (NXOpen.Annotations.FoldedRadiusDimension)singleObj;
            CaxME.GetTextBoxCoordinate(temp.Tag, out cBoxCoordinate);
            #endregion
        }
        else if (DimeType == "NXOpen.Annotations.ArcLengthDimension")
        {
            #region ArcLengthDimension取Location
            NXOpen.Annotations.ArcLengthDimension temp = (NXOpen.Annotations.ArcLengthDimension)singleObj;
            CaxME.GetTextBoxCoordinate(temp.Tag, out cBoxCoordinate);
            #endregion
        }
        else if (DimeType == "NXOpen.Annotations.DraftingSurfaceFinish")
        {
            #region SurfaceFinish取Location
            NXOpen.Annotations.DraftingSurfaceFinish temp = (NXOpen.Annotations.DraftingSurfaceFinish)singleObj;
            CaxME.GetTextBoxCoordinate(temp.Tag, out cBoxCoordinate);
            #endregion
        }
        else if (DimeType == "NXOpen.Annotations.ConcentricCircleDimension")
        {
            #region ConcentricCircle取Location
            NXOpen.Annotations.ConcentricCircleDimension temp = (NXOpen.Annotations.ConcentricCircleDimension)singleObj;
            CaxME.GetTextBoxCoordinate(temp.Tag, out cBoxCoordinate);
            #endregion
        }
        else if (DimeType == "NXOpen.Annotations.Gdt")
        {
            #region GDT Symbol取Location
            NXOpen.Annotations.Gdt temp = (NXOpen.Annotations.Gdt)singleObj;
            CaxME.GetTextBoxCoordinate(temp.Tag, out cBoxCoordinate);
            #endregion
        }
    }
    //------------------------------------------------------------------------------
    // Following method disposes all the class members
    //------------------------------------------------------------------------------
    public void Dispose()
    {
        try
        {
            if (isDisposeCalled == false)
            {
                //TODO: Add your application code here 
            }
            isDisposeCalled = true;
        }
        catch (NXOpen.NXException ex)
        {
            // ---- Enter your exception handling code here -----

        }
    }

    public static int GetUnloadOption(string arg)
    {
        //Unloads the image explicitly, via an unload dialog
        //return System.Convert.ToInt32(Session.LibraryUnloadOption.Explicitly);

        //Unloads the image immediately after execution within NX
        return System.Convert.ToInt32(Session.LibraryUnloadOption.Immediately);

        //Unloads the image when the NX session terminates
        // return System.Convert.ToInt32(Session.LibraryUnloadOption.AtTermination);
    }

}

