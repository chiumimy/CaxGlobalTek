using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using NXOpen;
using NXOpen.UF;
using NXOpen.Annotations;
using NXOpen.Drawings;
using NXOpen.Utilities;


namespace CaxGlobaltek
{
    public class CaxME
    {
        public static Session theSession = Session.GetSession();
        public static UI theUI;
        public static UFSession theUfSession = UFSession.GetUFSession();
        public static Part workPart = theSession.Parts.Work;
        public static Part displayPart = theSession.Parts.Display;
        public static bool status;
        
        public struct TablePosi
        {
            public static string PartNumberPos = "PartNumberPos";
            public static string ERPcodePos = "ERPcodePos";
            public static string ERPRevPos = "ERPRevPos";
            public static string CusRevPos = "CusRevPos";
            public static string PartDescriptionPos = "PartDescriptionPos";
            public static string RevStartPos = "RevStartPos";
            public static string PartUnitPos = "PartUnitPos";
            public static string TolTitle0Pos = "TolTitle0Pos";
            public static string TolTitle1Pos = "TolTitle1Pos";
            public static string TolTitle2Pos = "TolTitle2Pos";
            public static string TolTitle3Pos = "TolTitle3Pos";
            public static string TolTitle4Pos = "TolTitle4Pos";
            public static string AngleTitlePos = "AngleTitlePos";
            public static string TolValue0Pos = "TolValue0Pos";
            public static string TolValue1Pos = "TolValue1Pos";
            public static string TolValue2Pos = "TolValue2Pos";
            public static string TolValue3Pos = "TolValue3Pos";
            public static string TolValue4Pos = "TolValue4Pos";
            public static string AngleValuePos = "AngleValuePos";
            public static string RevDateStartPos = "RevDateStartPos";
            public static string AuthDatePos = "AuthDatePos";
            public static string MaterialPos = "MaterialPos";
            public static string ProcNamePos = "ProcNamePos";
            public static string PageNumberPos = "PageNumberPos";
            public static string PreparedPos = "PreparedPos";
            public static string ReviewedPos = "ReviewedPos";
            public static string ApprovedPos = "ApprovedPos";
            public static string InstructionPos = "InstructionPos";
            public static string InstApprovedPos = "InstApprovedPos";
            public static string SecondPartNumberPos = "SecondPartNumberPos";
            public static string SecondPageNumberPos = "SecondPageNumberPos";
        }

        public class DimenAttr
        {
            public static string Gauge = "Gauge";
            public static string SelfCheck_Gauge = "SelfCheck_Gauge";
            public static string Frequency = "Frequency";

            public static string CheckLevel = "CheckLevel";

            public static string FixDimension = "FixDimension";
            public static string DiCount = "DiCount";
            
            public static string OldColor = "OldColor";

            public static string IPQC_Frequency = "IPQC_Frequency";
            public static string IPQC_Freq_0 = "IPQC_Freq_0";
            public static string IPQC_Freq_1 = "IPQC_Freq_1";
            public static string IPQC_Units = "IPQC_Units";
            public static string IPQC_Size = "IPQC_Size";
            public static string IPQC_Freq = "IPQC_Freq";

            public static string SelfCheck_Frequency = "SelfCheck_Frequency";
            public static string SelfCheck_Freq_0 = "SelfCheck_Freq_0";
            public static string SelfCheck_Freq_1 = "SelfCheck_Freq_1";
            public static string SelfCheck_Units = "SelfCheck_Units";

            public static string IQC_Frequency = "IQC_Frequency";
            public static string IQC_Freq_0 = "IQC_Freq_0";
            public static string IQC_Freq_1 = "IQC_Freq_1";
            public static string IQC_Units = "IQC_Units";
            public static string IQC_Size = "IQC_Size";
            public static string IQC_Freq = "IQC_Freq";

            public static string FAI_Frequency = "FAI_Frequency";
            public static string FAI_Freq_0 = "FAI_Freq_0";
            public static string FAI_Freq_1 = "FAI_Freq_1";
            public static string FAI_Units = "FAI_Units";
            public static string FAI_Size = "FAI_Size";
            public static string FAI_Freq = "FAI_Freq";

            public static string FQC_Frequency = "FQC_Frequency";
            public static string FQC_Freq_0 = "FQC_Freq_0";
            public static string FQC_Freq_1 = "FQC_Freq_1";
            public static string FQC_Units = "FQC_Units";
            public static string FQC_Size = "FQC_Size";
            public static string FQC_Freq = "FQC_Freq";

            public static string SheetName = "SheetName";
            public static string BallonNum = "BallonNum";
            public static string BallonLocation = "BallonLocation";
            public static string AssignExcelType = "AssignExcelType";
            public static string SelfCheckExcel = "SelfCheckExcel";
            public static string KC = "KC";
            public static string Product = "PRODUCT";
            public static string CustomerBalloon = "CUSTOMERBALLOON";
            public static string SpcControl = "SPCCONTROL";
            public static string KCInsertTime = "KCInsertTime";
            public static string Size = "Size";
            public static string Freq = "Freq";
            public static string SelfCheck_Size = "SelfCheck_Size";
            public static string SelfCheck_Freq = "SelfCheck_Freq";
        }

        public class BoxCoordinate
        {
            public double[] upper_left = new double[3] { 0, 0, 0 };
            public double[] lower_left = new double[3] { 0, 0, 0 };
            public double[] lower_right = new double[3] { 0, 0, 0 };
            public double[] upper_right = new double[3] { 0, 0, 0 };
        }

        public struct DimenData
        {
            public NXObject Obj { get; set; }
            public DrawingSheet CurrentSheet { get; set; }
            public double LocationX { get; set; }
            public double LocationY { get; set; }
            public double LocationZ { get; set; }
        }

        public struct FcfData
        {
            public string Characteristic { get; set; }
            public string ZoneShape { get; set; }
            public string ToleranceValue { get; set; }
            public string MaterialModifier { get; set; }
            public string PrimaryDatum { get; set; }
            public string PrimaryMaterialModifier { get; set; }
            public string SecondaryDatum { get; set; }
            public string SecondaryMaterialModifier { get; set; }
            public string TertiaryDatum { get; set; }
            public string TertiaryMaterialModifier { get; set; }
        }

        public struct ObjectAttribute
        {
            public string singleObjExcel { get; set; }
            public string singleSelfCheckExcel { get; set; }
            public string keyChara { get; set; }
            public string productName { get; set; }
            public string customerBalloon { get; set; }
            public string spcControl { get; set; }
        }

        public struct FixDimenAttr
        {
            public string FixDimension { get; set; }
            public string DiCount { get; set; }
            public string BallonNum { get; set; }
        }

        public class DimensionData
        {
            public string type { get; set; }
            public string keyChara { get; set; }
            public string productName { get; set; }
            public string excelType { get; set; }
            public string spcControl { get; set; }
            public string size { get; set; }
            public string freq { get; set; }
            public string selfCheck_Size { get; set; }
            public string selfCheck_Freq { get; set; }
            public string checkLevel { get; set; }
            public string balloonCount { get; set; }

            public string aboveText { get; set; }
            public string belowText { get; set; }
            public string beforeText { get; set; }
            public string afterText { get; set; }
            public string toleranceSymbol { get; set; }
            public string mainText { get; set; }
            public string upperTol { get; set; }
            public string lowerTol { get; set; }
            public string x { get; set; }
            public string chamferAngle { get; set; }
            public string maxTolerance { get; set; }
            public string minTolerance { get; set; }
            public string tolType { get; set; }
            public string instrument { get; set; }
            public string location { get; set; }
            public string frequency { get; set; }
            public int ballonNum { get; set; }
            public int customerBalloon { get; set; }
            public string draftingVer { get; set; }
            public string draftingDate { get; set; }

            //FcfData
            public string characteristic { get; set; }
            public string zoneShape { get; set; }
            public string toleranceValue { get; set; }
            public string materialModifier { get; set; }
            public string primaryDatum { get; set; }
            public string primaryMaterialModifier { get; set; }
            public string secondaryDatum { get; set; }
            public string secondaryMaterialModifier { get; set; }
            public string tertiaryDatum { get; set; }
            public string tertiaryMaterialModifier { get; set; }
        }

        public struct WorkPartAttribute
        {
            public string meExcelType { get; set; }
            public string draftingVer { get; set; }
            public string draftingDate { get; set; }
            public string partDescription { get; set; }
            public string material { get; set; }
            public string createDate { get; set; }
        }

        /// <summary>
        /// 回傳標註尺寸的顏色
        /// </summary>
        /// <param name="nNXObject"></param>
        /// <returns></returns>
        public static int GetDimensionColor(NXObject nNXObject)
        {
            int oldColor = -1;
            try
            {
                string DimenType = nNXObject.GetType().ToString();
                if (DimenType == "NXOpen.Annotations.VerticalDimension")
                {
                    NXOpen.Annotations.VerticalDimension dimension = (NXOpen.Annotations.VerticalDimension)nNXObject;
                    return oldColor = dimension.Color;
                }
                else if (DimenType == "NXOpen.Annotations.PerpendicularDimension")
                {
                    NXOpen.Annotations.PerpendicularDimension dimension = (NXOpen.Annotations.PerpendicularDimension)nNXObject;
                    return oldColor = dimension.Color;
                }
                else if (DimenType == "NXOpen.Annotations.MinorAngularDimension")
                {
                    NXOpen.Annotations.MinorAngularDimension dimension = (NXOpen.Annotations.MinorAngularDimension)nNXObject;
                    return oldColor = dimension.Color;
                }
                else if (DimenType == "NXOpen.Annotations.ChamferDimension")
                {
                    NXOpen.Annotations.ChamferDimension dimension = (NXOpen.Annotations.ChamferDimension)nNXObject;
                    return oldColor = dimension.Color;
                }
                else if (DimenType == "NXOpen.Annotations.HorizontalDimension")
                {
                    NXOpen.Annotations.HorizontalDimension dimension = (NXOpen.Annotations.HorizontalDimension)nNXObject;
                    return oldColor = dimension.Color;
                }
                else if (DimenType == "NXOpen.Annotations.HoleDimension")
                {
                    NXOpen.Annotations.HoleDimension dimension = (NXOpen.Annotations.HoleDimension)nNXObject;
                    return oldColor = dimension.Color;
                }
                else if (DimenType == "NXOpen.Annotations.DiameterDimension")
                {
                    NXOpen.Annotations.DiameterDimension dimension = (NXOpen.Annotations.DiameterDimension)nNXObject;
                    return oldColor = dimension.Color;
                }
                else if (DimenType == "NXOpen.Annotations.CylindricalDimension")
                {
                    NXOpen.Annotations.CylindricalDimension dimension = (NXOpen.Annotations.CylindricalDimension)nNXObject;
                    return oldColor = dimension.Color;
                }
                else if (DimenType == "NXOpen.Annotations.RadiusDimension")
                {
                    NXOpen.Annotations.RadiusDimension dimension = (NXOpen.Annotations.RadiusDimension)nNXObject;
                    return oldColor = dimension.Color;
                }
                else if (DimenType == "NXOpen.Annotations.ArcLengthDimension")
                {
                    NXOpen.Annotations.ArcLengthDimension dimension = (NXOpen.Annotations.ArcLengthDimension)nNXObject;
                    return oldColor = dimension.Color;
                }
                else if (DimenType == "NXOpen.Annotations.AngularDimension")
                {
                    NXOpen.Annotations.AngularDimension dimension = (NXOpen.Annotations.AngularDimension)nNXObject;
                    return oldColor = dimension.Color;
                }
                else if (DimenType == "NXOpen.Annotations.ParallelDimension")
                {
                    NXOpen.Annotations.ParallelDimension dimension = (NXOpen.Annotations.ParallelDimension)nNXObject;
                    return oldColor = dimension.Color;
                }
            }
            catch (System.Exception ex)
            {
                
            }
            return oldColor;
        }

        /// <summary>
        /// 設定標註尺寸顏色
        /// </summary>
        /// <param name="nNXObject"></param>
        /// <param name="color"></param>
        /// <returns></returns>
        public static bool SetDimensionColor(NXObject nNXObject, int color)
        {
            try
            {
                DisplayModification displayModification1;
                displayModification1 = theSession.DisplayManager.NewDisplayModification();
                displayModification1.ApplyToAllFaces = false;
                displayModification1.ApplyToOwningParts = false;
                displayModification1.NewColor = color;
                DisplayableObject[] objects1 = new DisplayableObject[1];

                string DimenType = nNXObject.GetType().ToString();
                if (DimenType == "NXOpen.Annotations.VerticalDimension")
                {
                    NXOpen.Annotations.VerticalDimension dimension = (NXOpen.Annotations.VerticalDimension)nNXObject;
                    objects1[0] = dimension;
                    displayModification1.Apply(objects1);
                }
                else if (DimenType == "NXOpen.Annotations.PerpendicularDimension")
                {
                    NXOpen.Annotations.PerpendicularDimension dimension = (NXOpen.Annotations.PerpendicularDimension)nNXObject;
                    objects1[0] = dimension;
                    displayModification1.Apply(objects1);
                }
                else if (DimenType == "NXOpen.Annotations.MinorAngularDimension")
                {
                    NXOpen.Annotations.MinorAngularDimension dimension = (NXOpen.Annotations.MinorAngularDimension)nNXObject;
                    objects1[0] = dimension;
                    displayModification1.Apply(objects1);
                }
                else if (DimenType == "NXOpen.Annotations.MajorAngularDimension")
                {
                    NXOpen.Annotations.MajorAngularDimension dimension = (NXOpen.Annotations.MajorAngularDimension)nNXObject;
                    objects1[0] = dimension;
                    displayModification1.Apply(objects1);
                }
                else if (DimenType == "NXOpen.Annotations.ChamferDimension")
                {
                    NXOpen.Annotations.ChamferDimension dimension = (NXOpen.Annotations.ChamferDimension)nNXObject;
                    objects1[0] = dimension;
                    displayModification1.Apply(objects1);
                }
                else if (DimenType == "NXOpen.Annotations.HorizontalDimension")
                {
                    NXOpen.Annotations.HorizontalDimension dimension = (NXOpen.Annotations.HorizontalDimension)nNXObject;
                    objects1[0] = dimension;
                    displayModification1.Apply(objects1);
                }
                else if (DimenType == "NXOpen.Annotations.HoleDimension")
                {
                    NXOpen.Annotations.HoleDimension dimension = (NXOpen.Annotations.HoleDimension)nNXObject;
                    objects1[0] = dimension;
                    displayModification1.Apply(objects1);
                }
                else if (DimenType == "NXOpen.Annotations.DiameterDimension")
                {
                    NXOpen.Annotations.DiameterDimension dimension = (NXOpen.Annotations.DiameterDimension)nNXObject;
                    objects1[0] = dimension;
                    displayModification1.Apply(objects1);
                }
                else if (DimenType == "NXOpen.Annotations.CylindricalDimension")
                {
                    NXOpen.Annotations.CylindricalDimension dimension = (NXOpen.Annotations.CylindricalDimension)nNXObject;
                    objects1[0] = dimension;
                    displayModification1.Apply(objects1);
                }
                else if (DimenType == "NXOpen.Annotations.RadiusDimension")
                {
                    NXOpen.Annotations.RadiusDimension dimension = (NXOpen.Annotations.RadiusDimension)nNXObject;
                    objects1[0] = dimension;
                    displayModification1.Apply(objects1);
                }
                else if (DimenType == "NXOpen.Annotations.ArcLengthDimension")
                {
                    NXOpen.Annotations.ArcLengthDimension dimension = (NXOpen.Annotations.ArcLengthDimension)nNXObject;
                    objects1[0] = dimension;
                    displayModification1.Apply(objects1);
                }
                else if (DimenType == "NXOpen.Annotations.AngularDimension")
                {
                    NXOpen.Annotations.AngularDimension dimension = (NXOpen.Annotations.AngularDimension)nNXObject;
                    objects1[0] = dimension;
                    displayModification1.Apply(objects1);
                }
                else if (DimenType == "NXOpen.Annotations.ParallelDimension")
                {
                    NXOpen.Annotations.ParallelDimension dimension = (NXOpen.Annotations.ParallelDimension)nNXObject;
                    objects1[0] = dimension;
                    displayModification1.Apply(objects1);
                }
                else if (DimenType == "NXOpen.Annotations.DraftingFcf")
                {
                    NXOpen.Annotations.DraftingFcf dimension = (NXOpen.Annotations.DraftingFcf)nNXObject;
                    objects1[0] = dimension;
                    displayModification1.Apply(objects1);
                }
            }
            catch (System.Exception ex)
            {
                return false;
            }
            return true;
        }

        /// <summary>
        /// 畫Annotation的方框
        /// </summary>
        /// <param name="AnnotationTag"></param>
        /// <returns></returns>
        public static bool DrawTextBox(Tag AnnotationTag)
        {
            try
            {
                int[] mpi = new int[100];
                double[] mpr = new double[70], orientation = new double[9];
                string rad, dia;

                theUfSession.Drf.AskObjectPreferences(AnnotationTag, mpi, mpr, out rad, out dia);
                double angle = mpr[3];
                double angRad = angle * Math.PI / 180.0;
                double[] upper_left = new double[3] { 0, 0, 0 };
                double[] lower_left = new double[3] { 0, 0, 0 };
                double[] lower_right = new double[3] { 0, 0, 0 };
                double[] upper_right = new double[3] { 0, 0, 0 };
                double length, height;

                theUfSession.Drf.AskAnnotationTextBox(AnnotationTag, upper_left, out length, out height);

                //左下角
                lower_left[0] = upper_left[0] + height * Math.Sin(angRad); //CaxLog.ShowListingWindow(lower_left[0].ToString());
                lower_left[1] = upper_left[1] - height * Math.Cos(angRad); //CaxLog.ShowListingWindow(lower_left[1].ToString());
                //右下角
                lower_right[0] = lower_left[0] + length * Math.Cos(angRad); //CaxLog.ShowListingWindow(lower_right[0].ToString());
                lower_right[1] = lower_left[1] + length * Math.Sin(angRad); //CaxLog.ShowListingWindow(lower_right[1].ToString());
                //右上角
                upper_right[0] = upper_left[0] + length * Math.Cos(angRad); //CaxLog.ShowListingWindow(upper_right[0].ToString());
                upper_right[1] = upper_left[1] + length * Math.Sin(angRad); //CaxLog.ShowListingWindow(upper_right[1].ToString());

                UFObj.DispProps props = new UFObj.DispProps();
                props.color = 0; // only color is needed, use system default color

                theUfSession.Disp.DisplayTemporaryLine(Tag.Null, UFDisp.ViewType.UseWorkView, upper_left, lower_left, ref props);
                theUfSession.Disp.DisplayTemporaryLine(Tag.Null, UFDisp.ViewType.UseWorkView, lower_left, lower_right, ref props);
                theUfSession.Disp.DisplayTemporaryLine(Tag.Null, UFDisp.ViewType.UseWorkView, lower_right, upper_right, ref props);
                theUfSession.Disp.DisplayTemporaryLine(Tag.Null, UFDisp.ViewType.UseWorkView, upper_right, upper_left, ref props);
            }
            catch (System.Exception ex)
            {
                return false;
            }
            return true;
        }

        /// <summary>
        /// 取得TextBox的四個角落座標
        /// </summary>
        /// <param name="AnnotationTag"></param>
        /// <param name="cBoxCoordinate"></param>
        /// <returns></returns>
        public static bool GetTextBoxCoordinate(Tag AnnotationTag, out BoxCoordinate cBoxCoordinate)
        {
            cBoxCoordinate = new BoxCoordinate();
            try
            {
                int[] mpi = new int[100];
                double[] mpr = new double[70], orientation = new double[9];
                string rad, dia;

                theUfSession.Drf.AskObjectPreferences(AnnotationTag, mpi, mpr, out rad, out dia);
                double angle = mpr[3];
                double angRad = angle * Math.PI / 180.0;
                //double[] upper_left = new double[3] { 0, 0, 0 };
                //double[] lower_left = new double[3] { 0, 0, 0 };
                //double[] lower_right = new double[3] { 0, 0, 0 };
                //double[] upper_right = new double[3] { 0, 0, 0 };
                double length, height;

                theUfSession.Drf.AskAnnotationTextBox(AnnotationTag, cBoxCoordinate.upper_left, out length, out height);
                
                //左下角
                cBoxCoordinate.lower_left[0] = cBoxCoordinate.upper_left[0] + height * Math.Sin(angRad);
                cBoxCoordinate.lower_left[1] = cBoxCoordinate.upper_left[1] - height * Math.Cos(angRad);
                //右下角
                cBoxCoordinate.lower_right[0] = cBoxCoordinate.lower_left[0] + length * Math.Cos(angRad);
                cBoxCoordinate.lower_right[1] = cBoxCoordinate.lower_left[1] + length * Math.Sin(angRad);
                //右上角
                cBoxCoordinate.upper_right[0] = cBoxCoordinate.upper_left[0] + length * Math.Cos(angRad);
                cBoxCoordinate.upper_right[1] = cBoxCoordinate.upper_left[1] + length * Math.Sin(angRad);
            }
            catch (System.Exception ex)
            {
                return false;
            }
            return true;
        }

        public static bool CalculateBallonCoordinate(CaxME.BoxCoordinate cBoxCoordinate, ref DimenData sDimenData)
        {
            try
            {
                //先取得欲偏移的方向向量
                double X2_X1 = (cBoxCoordinate.lower_left[0] - cBoxCoordinate.lower_right[0]);
                double Y2_Y1 = (cBoxCoordinate.lower_left[1] - cBoxCoordinate.lower_right[1]);
                double shift_X = (X2_X1) / Math.Sqrt(X2_X1 * X2_X1 + Y2_Y1 * Y2_Y1);
                double shift_Y = (Y2_Y1) / Math.Sqrt(X2_X1 * X2_X1 + Y2_Y1 * Y2_Y1);

                sDimenData.LocationX = (cBoxCoordinate.upper_left[0] + cBoxCoordinate.lower_left[0]) / 2;
                sDimenData.LocationY = (cBoxCoordinate.upper_left[1] + cBoxCoordinate.lower_left[1]) / 2;

                sDimenData.LocationX = sDimenData.LocationX + shift_X * 3;
                sDimenData.LocationY = sDimenData.LocationY + shift_Y * 3;

                sDimenData.LocationZ = 0;
            }
            catch (System.Exception ex)
            {
                return false;
            }
            return true;
        }

        public static bool CalculateBallonCoordinateAfter(CaxME.BoxCoordinate cBoxCoordinate,out DimenData sDimenData)
        {
            sDimenData = new DimenData();
            try
            {
                //先取得欲偏移的方向向量
                double X2_X1 = (cBoxCoordinate.lower_right[0] - cBoxCoordinate.lower_left[0]);
                double Y2_Y1 = (cBoxCoordinate.lower_right[1] - cBoxCoordinate.lower_left[1]);
                double shift_X = (cBoxCoordinate.lower_right[0] - cBoxCoordinate.lower_left[0]) / Math.Sqrt(X2_X1 * X2_X1 + Y2_Y1 * Y2_Y1);
                double shift_Y = (cBoxCoordinate.lower_right[1] - cBoxCoordinate.lower_left[1]) / Math.Sqrt(X2_X1 * X2_X1 + Y2_Y1 * Y2_Y1);

                sDimenData.LocationX = (cBoxCoordinate.upper_right[0] + cBoxCoordinate.lower_right[0]) / 2;
                sDimenData.LocationY = (cBoxCoordinate.upper_right[1] + cBoxCoordinate.lower_right[1]) / 2;

                sDimenData.LocationX = sDimenData.LocationX + shift_X * 3;
                sDimenData.LocationY = sDimenData.LocationY + shift_Y * 3;

                sDimenData.LocationZ = 0;
            }
            catch (System.Exception ex)
            {
                return false;
            }
            return true;
        }

        /// <summary>
        /// 建立泡泡在圖紙上
        /// </summary>
        /// <param name="Number"></param>
        /// <param name="BallonPt"></param>
        /// <returns></returns>
        public static bool CreateBallonOnSheet(string Number, Point3d BallonPt, double textSize, string BalloonAtt, out NXObject nxobj)
        {
            nxobj = null;
            try
            {// ----------------------------------------------
                //   Menu: Insert->Annotation->Identification Symbol...
                // ----------------------------------------------
                NXOpen.Session.UndoMarkId markId1;
                markId1 = theSession.SetUndoMark(NXOpen.Session.MarkVisibility.Visible, "Start");

                NXOpen.Annotations.IdSymbol nullAnnotations_IdSymbol = null;
                NXOpen.Annotations.IdSymbolBuilder idSymbolBuilder1;
                idSymbolBuilder1 = workPart.Annotations.IdSymbols.CreateIdSymbolBuilder(nullAnnotations_IdSymbol);

                idSymbolBuilder1.Origin.Plane.PlaneMethod = NXOpen.Annotations.PlaneBuilder.PlaneMethodType.XyPlane;

                idSymbolBuilder1.Origin.SetInferRelativeToGeometry(false);

                idSymbolBuilder1.Origin.Anchor = NXOpen.Annotations.OriginBuilder.AlignmentPosition.MidCenter;

                // 給字體大小
                idSymbolBuilder1.Style.LetteringStyle.GeneralTextSize = textSize;

                idSymbolBuilder1.UpperText = Number;
                //idSymbolBuilder1.UpperText = "<C0.7>" + Number + "<C>";

                idSymbolBuilder1.Size = 4;

                theSession.SetUndoMarkName(markId1, "Identification Symbol Dialog");

                idSymbolBuilder1.Origin.Plane.PlaneMethod = NXOpen.Annotations.PlaneBuilder.PlaneMethodType.XyPlane;

                idSymbolBuilder1.Origin.SetInferRelativeToGeometry(false);

                NXOpen.Annotations.LeaderData leaderData1;
                leaderData1 = workPart.Annotations.CreateLeaderData();

                leaderData1.StubSize = 5.0;

                leaderData1.Arrowhead = NXOpen.Annotations.LeaderData.ArrowheadType.FilledArrow;

                idSymbolBuilder1.Leader.Leaders.Append(leaderData1);

                leaderData1.StubSide = NXOpen.Annotations.LeaderSide.Inferred;

                idSymbolBuilder1.Origin.SetInferRelativeToGeometry(false);

                idSymbolBuilder1.Origin.SetInferRelativeToGeometry(false);

                NXOpen.Annotations.Annotation.AssociativeOriginData assocOrigin1;
                assocOrigin1.OriginType = NXOpen.Annotations.AssociativeOriginType.Drag;
                View nullView = null;
                assocOrigin1.View = nullView;
                assocOrigin1.ViewOfGeometry = nullView;
                Point nullPoint = null;
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
                idSymbolBuilder1.Origin.SetAssociativeOrigin(assocOrigin1);

                Point3d point1 = new Point3d(BallonPt.X, BallonPt.Y, 0.0);
                idSymbolBuilder1.Origin.Origin.SetValue(null, nullView, point1);

                idSymbolBuilder1.Origin.SetInferRelativeToGeometry(false);

                NXOpen.Session.UndoMarkId markId2;
                markId2 = theSession.SetUndoMark(NXOpen.Session.MarkVisibility.Invisible, "Identification Symbol");

                NXObject nXObject1;
                nXObject1 = idSymbolBuilder1.Commit();
                nxobj = nXObject1;
                nXObject1.SetAttribute(BalloonAtt, "CAXCreate");

                theSession.DeleteUndoMark(markId2, null);

                theSession.SetUndoMarkName(markId1, "Identification Symbol");

                theSession.SetUndoMarkVisibility(markId1, null, NXOpen.Session.MarkVisibility.Visible);

                idSymbolBuilder1.Destroy();

            }
            catch (System.Exception ex)
            {
                return false;
            }
            return true;
        }

        /// <summary>
        /// 取得Fcf的資料
        /// Characteristic
        /// ZoneShape
        /// ToleranceValue
        /// MaterialModifier
        /// PrimaryDatum/PrimaryMaterialModifier
        /// SecondaryDatum/SecondaryMaterialModifier
        /// TertiaryDatum/TertiaryMaterialModifier
        /// </summary>
        /// <param name="temp"></param>
        /// <param name="Characteristic"></param>
        /// <param name="ToleranceValue"></param>
        /// <returns></returns>
        public static bool GetFcfData(NXOpen.Annotations.DraftingFcf temp, out FcfData sFcfData)
        {
            sFcfData = new FcfData();
            try
            {
                NXOpen.Annotations.DraftingFeatureControlFrameBuilder draftingFeatureControlFrameBuilder1;
                draftingFeatureControlFrameBuilder1 = workPart.Annotations.CreateDraftingFeatureControlFrameBuilder(temp);
                #region Characteristic
                if (draftingFeatureControlFrameBuilder1.Characteristic.ToString() == "")
                {
                    sFcfData.Characteristic = null;
                }
                else
                {
                    sFcfData.Characteristic = draftingFeatureControlFrameBuilder1.Characteristic.ToString();
                }
                #endregion

                TaggedObject taggedObject2;
                taggedObject2 = draftingFeatureControlFrameBuilder1.FeatureControlFrameDataList.FindItem(0);
                NXOpen.Annotations.FeatureControlFrameDataBuilder featureControlFrameDataBuilder1 = (NXOpen.Annotations.FeatureControlFrameDataBuilder)taggedObject2;
                #region ZoneShape
                if (featureControlFrameDataBuilder1.ZoneShape.ToString() == "")
                {
                    sFcfData.ZoneShape = null;
                }
                else
                {
                    sFcfData.ZoneShape = featureControlFrameDataBuilder1.ZoneShape.ToString();
                }
                #endregion

                #region ToleranceValue
                if (featureControlFrameDataBuilder1.ToleranceValue == "")
                {
                    sFcfData.ToleranceValue = null;
                }
                else
                {
                    sFcfData.ToleranceValue = featureControlFrameDataBuilder1.ToleranceValue;
                }
                #endregion

                #region MaterialModifier
                if (featureControlFrameDataBuilder1.MaterialModifier.ToString() == "" ||
                    featureControlFrameDataBuilder1.MaterialModifier.ToString() == "None")
                {
                    sFcfData.MaterialModifier = null;
                }
                else
                {
                    sFcfData.MaterialModifier = featureControlFrameDataBuilder1.MaterialModifier.ToString();
                }
                #endregion

                #region PrimaryDatumData
                if (featureControlFrameDataBuilder1.PrimaryDatumReference.Letter == "Compound")
                {
                    DatumReferenceBuilder[] aaa;
                    featureControlFrameDataBuilder1.PrimaryCompoundDatumReference.DatumReferenceList(out aaa);
                    foreach (DatumReferenceBuilder i in aaa)
                    {
                        //Letter
                        if (sFcfData.PrimaryDatum == null)
                        {
                            if (i.Letter == "")
                            {
                                sFcfData.PrimaryDatum = "X";
                            }
                            else
                            {
                                sFcfData.PrimaryDatum = i.Letter;
                            }
                        }
                        else
                        {
                            if (i.Letter == "")
                            {
                                sFcfData.PrimaryDatum = sFcfData.PrimaryDatum + "," + "X";
                            }
                            else
                            {
                                sFcfData.PrimaryDatum = sFcfData.PrimaryDatum + "," + i.Letter;
                            }
                        }
                        //MaterialCondition
                        if (sFcfData.PrimaryMaterialModifier == null)
                        {
                            if (i.MaterialCondition.ToString() == "None")
                            {
                                sFcfData.PrimaryMaterialModifier = "X";
                            }
                            else
                            {
                                sFcfData.PrimaryMaterialModifier = i.MaterialCondition.ToString();
                            }
                        }
                        else
                        {
                            if (i.MaterialCondition.ToString() == "None")
                            {
                                sFcfData.PrimaryMaterialModifier = sFcfData.PrimaryMaterialModifier + "," + "X";
                            }
                            else
                            {
                                sFcfData.PrimaryMaterialModifier = sFcfData.PrimaryMaterialModifier + "," + i.MaterialCondition.ToString();
                            }
                        }
                    }
                }
                else
                {
                    //Letter
                    if (featureControlFrameDataBuilder1.PrimaryDatumReference.Letter == "")
                    {
                        sFcfData.PrimaryDatum = "X";
                    }
                    else
                    {
                        sFcfData.PrimaryDatum = featureControlFrameDataBuilder1.PrimaryDatumReference.Letter;
                    }
                    //MaterialCondition
                    if (featureControlFrameDataBuilder1.PrimaryDatumReference.MaterialCondition.ToString() == "None")
                    {
                        sFcfData.PrimaryMaterialModifier = "X";
                    }
                    else
                    {
                        sFcfData.PrimaryMaterialModifier = featureControlFrameDataBuilder1.PrimaryDatumReference.MaterialCondition.ToString();
                    }
                }
                #endregion

                #region (註解)PrimaryDatumReference
                /*
                if (featureControlFrameDataBuilder1.PrimaryDatumReference.Letter == "Compound")
                {
                    DatumReferenceBuilder[] aaa;
                    featureControlFrameDataBuilder1.PrimaryCompoundDatumReference.DatumReferenceList(out aaa);
                    foreach (DatumReferenceBuilder i in aaa)
                    {
                        if (sFcfData.PrimaryDatum == null)
                        {
                            if (i.Letter == "None" || i.Letter == null)
                            {
                                sFcfData.PrimaryDatum = "X";
                            }
                            else
                            {
                                sFcfData.PrimaryDatum = i.Letter;
                            }
                        }
                        else
                        {
                            if (i.Letter == "None" || i.Letter == null)
                            {
                                sFcfData.PrimaryDatum = sFcfData.PrimaryDatum + "," + "X";
                            }
                            else
                            {
                                sFcfData.PrimaryDatum = sFcfData.PrimaryDatum + "," + i.Letter;
                            }
                        }
                    }
                }
                else
                {
                    if (featureControlFrameDataBuilder1.PrimaryDatumReference.Letter == "")
                    {
                        sFcfData.PrimaryDatum = "X";
                    }
                    else
                    {
                        sFcfData.PrimaryDatum = featureControlFrameDataBuilder1.PrimaryDatumReference.Letter;
                    }
                }
                */
                /*
                if (featureControlFrameDataBuilder1.PrimaryDatumReference.Letter == "")
                {
                    sFcfData.PrimaryDatum = null;
                }
                else
                {
                    if (featureControlFrameDataBuilder1.PrimaryDatumReference.Letter == "Compound")
                    {
                        DatumReferenceBuilder[] aaa;
                        featureControlFrameDataBuilder1.PrimaryCompoundDatumReference.DatumReferenceList(out aaa);
                        foreach (DatumReferenceBuilder i in aaa)
                        {
                            if (sFcfData.PrimaryDatum == null)
                            {
                                sFcfData.PrimaryDatum = i.Letter;
                            }
                            else
                            {
                                sFcfData.PrimaryDatum = sFcfData.PrimaryDatum + "," + i.Letter;
                            }
                        }
                    }
                    else
                    {
                        sFcfData.PrimaryDatum = featureControlFrameDataBuilder1.PrimaryDatumReference.Letter;
                    }
                }
                */
                #endregion

                #region (註解)PrimaryDatumReference.MaterialCondition
                /*
                if (featureControlFrameDataBuilder1.PrimaryDatumReference.Letter == "Compound")
                {
                    DatumReferenceBuilder[] aaa;
                    featureControlFrameDataBuilder1.PrimaryCompoundDatumReference.DatumReferenceList(out aaa);
                    foreach (DatumReferenceBuilder i in aaa)
                    {
                        if (sFcfData.PrimaryMaterialModifier == null)
                        {
                            if (i.MaterialCondition.ToString() == "None")
                            {
                                sFcfData.PrimaryMaterialModifier = "X";
                            }
                            else
                            {
                                sFcfData.PrimaryMaterialModifier = i.MaterialCondition.ToString();
                            }
                        }
                        else
                        {
                            if (i.MaterialCondition.ToString() == "None")
                            {
                                sFcfData.PrimaryMaterialModifier = sFcfData.PrimaryMaterialModifier + "," + "X";
                            }
                            else
                            {
                                sFcfData.PrimaryMaterialModifier = sFcfData.PrimaryMaterialModifier + "," + i.MaterialCondition.ToString();
                            }
                        }
                    }
                }
                else
                {
                    if (featureControlFrameDataBuilder1.PrimaryDatumReference.MaterialCondition.ToString() == "" ||
                    featureControlFrameDataBuilder1.PrimaryDatumReference.MaterialCondition.ToString() == "None")
                    {
                        sFcfData.PrimaryMaterialModifier = "X";
                    }
                    else
                    {
                        sFcfData.PrimaryMaterialModifier = featureControlFrameDataBuilder1.PrimaryDatumReference.MaterialCondition.ToString();
                    }
                }
                */
                /*
                if (featureControlFrameDataBuilder1.PrimaryDatumReference.MaterialCondition.ToString() == "" ||
                    featureControlFrameDataBuilder1.PrimaryDatumReference.MaterialCondition.ToString() == "None")
                {
                    sFcfData.PrimaryMaterialModifier = null;
                }
                else
                {
                    if (featureControlFrameDataBuilder1.PrimaryDatumReference.Letter == "Compound")
                    {
                        DatumReferenceBuilder[] aaa;
                        featureControlFrameDataBuilder1.PrimaryCompoundDatumReference.DatumReferenceList(out aaa);
                        foreach (DatumReferenceBuilder i in aaa)
                        {
                            if (sFcfData.PrimaryMaterialModifier == null)
                            {
                                sFcfData.PrimaryMaterialModifier = i.MaterialCondition.ToString();
                            }
                            else
                            {
                                sFcfData.PrimaryMaterialModifier = sFcfData.PrimaryMaterialModifier + "," + i.MaterialCondition.ToString();
                            }
                        }
                    }
                    else
                    {
                        sFcfData.PrimaryMaterialModifier = featureControlFrameDataBuilder1.PrimaryDatumReference.MaterialCondition.ToString();
                    }
                }
                */
                #endregion

                #region SecondaryDatumData
                if (featureControlFrameDataBuilder1.SecondaryDatumReference.Letter == "Compound")
                {
                    DatumReferenceBuilder[] aaa;
                    featureControlFrameDataBuilder1.SecondaryCompoundDatumReference.DatumReferenceList(out aaa);
                    foreach (DatumReferenceBuilder i in aaa)
                    {
                        //Letter
                        if (sFcfData.SecondaryDatum == null)
                        {
                            if (i.Letter == "")
                            {
                                sFcfData.SecondaryDatum = "X";
                            }
                            else
                            {
                                sFcfData.SecondaryDatum = i.Letter;
                            }
                        }
                        else
                        {
                            if (i.Letter == "")
                            {
                                sFcfData.SecondaryDatum = sFcfData.SecondaryDatum + "," + "X";
                            }
                            else
                            {
                                sFcfData.SecondaryDatum = sFcfData.SecondaryDatum + "," + i.Letter;
                            }
                        }
                        //MaterialCondition
                        if (sFcfData.SecondaryMaterialModifier == null)
                        {
                            if (i.MaterialCondition.ToString() == "None")
                            {
                                sFcfData.SecondaryMaterialModifier = "X";
                            }
                            else
                            {
                                sFcfData.SecondaryMaterialModifier = i.MaterialCondition.ToString();
                            }
                        }
                        else
                        {
                            if (i.MaterialCondition.ToString() == "None")
                            {
                                sFcfData.SecondaryMaterialModifier = sFcfData.SecondaryMaterialModifier + "," + "X";
                            }
                            else
                            {
                                sFcfData.SecondaryMaterialModifier = sFcfData.SecondaryMaterialModifier + "," + i.MaterialCondition.ToString();
                            }
                        }
                    }
                }
                else
                {
                    //Letter
                    if (featureControlFrameDataBuilder1.SecondaryDatumReference.Letter == "")
                    {
                        sFcfData.SecondaryDatum = "X";
                    }
                    else
                    {
                        sFcfData.SecondaryDatum = featureControlFrameDataBuilder1.SecondaryDatumReference.Letter;
                    }
                    //MaterialCondition
                    if (featureControlFrameDataBuilder1.SecondaryDatumReference.MaterialCondition.ToString() == "None")
                    {
                        sFcfData.SecondaryMaterialModifier = "X";
                    }
                    else
                    {
                        sFcfData.SecondaryMaterialModifier = featureControlFrameDataBuilder1.SecondaryDatumReference.MaterialCondition.ToString();
                    }
                }
                #endregion

                #region (註解)SecondaryDatumReference
                /*
                if (featureControlFrameDataBuilder1.SecondaryDatumReference.Letter == "Compound")
                {
                    DatumReferenceBuilder[] aaa;
                    featureControlFrameDataBuilder1.SecondaryCompoundDatumReference.DatumReferenceList(out aaa);
                    foreach (DatumReferenceBuilder i in aaa)
                    {
                        if (sFcfData.SecondaryDatum == null)
                        {
                            sFcfData.SecondaryDatum = i.Letter;
                        }
                        else
                        {
                            sFcfData.SecondaryDatum = sFcfData.SecondaryDatum + "," + i.Letter;
                        }
                    }
                }
                else
                {
                    if (featureControlFrameDataBuilder1.SecondaryDatumReference.Letter == "")
                    {
                        sFcfData.SecondaryDatum = null;
                    }
                    else
                    {
                        sFcfData.SecondaryDatum = featureControlFrameDataBuilder1.SecondaryDatumReference.Letter;
                    }
                }
                */
                /*
                if (featureControlFrameDataBuilder1.SecondaryDatumReference.Letter == "")
                {
                    sFcfData.SecondaryDatum = null;
                }
                else
                {
                    if (featureControlFrameDataBuilder1.SecondaryDatumReference.Letter == "Compound")
                    {
                        DatumReferenceBuilder[] aaa;
                        featureControlFrameDataBuilder1.SecondaryCompoundDatumReference.DatumReferenceList(out aaa);
                        foreach (DatumReferenceBuilder i in aaa)
                        {
                            if (sFcfData.SecondaryDatum == null)
                            {
                                sFcfData.SecondaryDatum = i.Letter;
                            }
                            else
                            {
                                sFcfData.SecondaryDatum = sFcfData.SecondaryDatum + "," + i.Letter;
                            }
                        }
                    }
                    else
                    {
                        sFcfData.SecondaryDatum = featureControlFrameDataBuilder1.SecondaryDatumReference.Letter;
                    }
                }
                */
                #endregion

                #region (註解)SecondaryDatumReference.MaterialCondition
                /*
                if (featureControlFrameDataBuilder1.SecondaryDatumReference.Letter == "Compound")
                {
                    DatumReferenceBuilder[] aaa;
                    featureControlFrameDataBuilder1.SecondaryCompoundDatumReference.DatumReferenceList(out aaa);
                    foreach (DatumReferenceBuilder i in aaa)
                    {
                        if (sFcfData.SecondaryMaterialModifier == null)
                        {
                            sFcfData.SecondaryMaterialModifier = i.MaterialCondition.ToString();
                        }
                        else
                        {
                            sFcfData.SecondaryMaterialModifier = sFcfData.SecondaryMaterialModifier + "," + i.MaterialCondition.ToString();
                        }
                    }
                }
                else
                {
                    if (featureControlFrameDataBuilder1.SecondaryDatumReference.MaterialCondition.ToString() == "" ||
                    featureControlFrameDataBuilder1.SecondaryDatumReference.MaterialCondition.ToString() == "None")
                    {
                        sFcfData.SecondaryMaterialModifier = "X";
                    }
                    else
                    {
                        sFcfData.SecondaryMaterialModifier = featureControlFrameDataBuilder1.SecondaryDatumReference.MaterialCondition.ToString();
                    }
                }
                */
                /*
                if (featureControlFrameDataBuilder1.SecondaryDatumReference.MaterialCondition.ToString() == "" ||
                    featureControlFrameDataBuilder1.SecondaryDatumReference.MaterialCondition.ToString() == "None")
                {
                    sFcfData.SecondaryMaterialModifier = null;
                }
                else
                {
                    if (featureControlFrameDataBuilder1.SecondaryDatumReference.Letter == "Compound")
                    {
                        DatumReferenceBuilder[] aaa;
                        featureControlFrameDataBuilder1.SecondaryCompoundDatumReference.DatumReferenceList(out aaa);
                        foreach (DatumReferenceBuilder i in aaa)
                        {
                            if (sFcfData.SecondaryMaterialModifier == null)
                            {
                                sFcfData.SecondaryMaterialModifier = i.MaterialCondition.ToString();
                            }
                            else
                            {
                                sFcfData.SecondaryMaterialModifier = sFcfData.SecondaryMaterialModifier + "," + i.MaterialCondition.ToString();
                            }
                        }
                    }
                    else
                    {
                        sFcfData.SecondaryMaterialModifier = featureControlFrameDataBuilder1.SecondaryDatumReference.MaterialCondition.ToString();
                    }
                }
                */
                #endregion

                #region TertiaryDatumData
                if (featureControlFrameDataBuilder1.TertiaryDatumReference.Letter == "Compound")
                {
                    DatumReferenceBuilder[] aaa;
                    featureControlFrameDataBuilder1.TertiaryCompoundDatumReference.DatumReferenceList(out aaa);
                    foreach (DatumReferenceBuilder i in aaa)
                    {
                        //Letter
                        if (sFcfData.TertiaryDatum == null)
                        {
                            if (i.Letter == "")
                            {
                                sFcfData.TertiaryDatum = "X";
                            }
                            else
                            {
                                sFcfData.TertiaryDatum = i.Letter;
                            }
                        }
                        else
                        {
                            if (i.Letter == "")
                            {
                                sFcfData.TertiaryDatum = sFcfData.TertiaryDatum + "," + "X";
                            }
                            else
                            {
                                sFcfData.TertiaryDatum = sFcfData.TertiaryDatum + "," + i.Letter;
                            }
                        }
                        //MaterialCondition
                        if (sFcfData.TertiaryMaterialModifier == null)
                        {
                            if (i.MaterialCondition.ToString() == "None")
                            {
                                sFcfData.TertiaryMaterialModifier = "X";
                            }
                            else
                            {
                                sFcfData.TertiaryMaterialModifier = i.MaterialCondition.ToString();
                            }
                        }
                        else
                        {
                            if (i.MaterialCondition.ToString() == "None")
                            {
                                sFcfData.TertiaryMaterialModifier = sFcfData.TertiaryMaterialModifier + "," + "X";
                            }
                            else
                            {
                                sFcfData.TertiaryMaterialModifier = sFcfData.TertiaryMaterialModifier + "," + i.MaterialCondition.ToString();
                            }
                        }
                    }
                }
                else
                {
                    //Letter
                    if (featureControlFrameDataBuilder1.TertiaryDatumReference.Letter == "")
                    {
                        sFcfData.TertiaryDatum = "X";
                    }
                    else
                    {
                        sFcfData.TertiaryDatum = featureControlFrameDataBuilder1.TertiaryDatumReference.Letter;
                    }
                    //MaterialCondition
                    if (featureControlFrameDataBuilder1.TertiaryDatumReference.MaterialCondition.ToString() == "None")
                    {
                        sFcfData.TertiaryMaterialModifier = "X";
                    }
                    else
                    {
                        sFcfData.TertiaryMaterialModifier = featureControlFrameDataBuilder1.TertiaryDatumReference.MaterialCondition.ToString();
                    }
                }
                #endregion

                #region (註解)TertiaryDatumReference
                /*
                if (featureControlFrameDataBuilder1.TertiaryDatumReference.Letter == "Compound")
                {
                    DatumReferenceBuilder[] aaa;
                    featureControlFrameDataBuilder1.TertiaryCompoundDatumReference.DatumReferenceList(out aaa);
                    foreach (DatumReferenceBuilder i in aaa)
                    {
                        if (sFcfData.TertiaryDatum == null)
                        {
                            sFcfData.TertiaryDatum = i.Letter;
                        }
                        else
                        {
                            sFcfData.TertiaryDatum = sFcfData.TertiaryDatum + "," + i.Letter;
                        }
                    }
                }
                else
                {
                    if (featureControlFrameDataBuilder1.TertiaryDatumReference.Letter == "")
                    {
                        sFcfData.TertiaryDatum = null;
                    }
                    else
                    {
                        sFcfData.TertiaryDatum = featureControlFrameDataBuilder1.TertiaryDatumReference.Letter;
                    }
                }
                */
                /*
                if (featureControlFrameDataBuilder1.TertiaryDatumReference.Letter == "")
                {
                    sFcfData.TertiaryDatum = null;
                }
                else
                {
                    if (featureControlFrameDataBuilder1.TertiaryDatumReference.Letter == "Compound")
                    {
                        DatumReferenceBuilder[] aaa;
                        featureControlFrameDataBuilder1.TertiaryCompoundDatumReference.DatumReferenceList(out aaa);
                        foreach (DatumReferenceBuilder i in aaa)
                        {
                            if (sFcfData.TertiaryDatum == null)
                            {
                                sFcfData.TertiaryDatum = i.Letter;
                            }
                            else
                            {
                                sFcfData.TertiaryDatum = sFcfData.TertiaryDatum + "," + i.Letter;
                            }
                        }
                    }
                    else
                    {
                        sFcfData.TertiaryDatum = featureControlFrameDataBuilder1.TertiaryDatumReference.Letter;
                    }
                }
                */
                #endregion

                #region (註解)TertiaryDatumReference.MaterialCondition
                /*
                if (featureControlFrameDataBuilder1.TertiaryDatumReference.Letter == "Compound")
                {
                    DatumReferenceBuilder[] aaa;
                    featureControlFrameDataBuilder1.TertiaryCompoundDatumReference.DatumReferenceList(out aaa);
                    foreach (DatumReferenceBuilder i in aaa)
                    {
                        if (sFcfData.TertiaryMaterialModifier == null)
                        {
                            sFcfData.TertiaryMaterialModifier = i.MaterialCondition.ToString();
                        }
                        else
                        {
                            sFcfData.TertiaryMaterialModifier = sFcfData.TertiaryMaterialModifier + "," + i.MaterialCondition.ToString();
                        }
                    }
                }
                else
                {
                    if (featureControlFrameDataBuilder1.TertiaryDatumReference.MaterialCondition.ToString() == "" ||
                    featureControlFrameDataBuilder1.TertiaryDatumReference.MaterialCondition.ToString() == "None")
                    {
                        sFcfData.TertiaryMaterialModifier = "X";
                    }
                    else
                    {
                        sFcfData.TertiaryMaterialModifier = featureControlFrameDataBuilder1.TertiaryDatumReference.MaterialCondition.ToString();
                    }
                }
                */
                /*
                if (featureControlFrameDataBuilder1.TertiaryDatumReference.MaterialCondition.ToString() == "" ||
                    featureControlFrameDataBuilder1.TertiaryDatumReference.MaterialCondition.ToString() == "None")
                {
                    sFcfData.TertiaryMaterialModifier = null;
                }
                else
                {
                    if (featureControlFrameDataBuilder1.TertiaryDatumReference.Letter == "Compound")
                    {
                        DatumReferenceBuilder[] aaa;
                        featureControlFrameDataBuilder1.TertiaryCompoundDatumReference.DatumReferenceList(out aaa);
                        foreach (DatumReferenceBuilder i in aaa)
                        {
                            if (sFcfData.TertiaryMaterialModifier == null)
                            {
                                sFcfData.TertiaryMaterialModifier = i.MaterialCondition.ToString();
                            }
                            else
                            {
                                sFcfData.TertiaryMaterialModifier = sFcfData.TertiaryMaterialModifier + "," + i.MaterialCondition.ToString();
                            }
                        }
                    }
                    else
                    {
                        sFcfData.TertiaryMaterialModifier = featureControlFrameDataBuilder1.TertiaryDatumReference.MaterialCondition.ToString();
                    }
                }
                */
                #endregion
                
                //sFcfData.PrimaryDatum = featureControlFrameDataBuilder1.PrimaryDatumReference.Letter;
                //sFcfData.PrimaryMaterialModifier = featureControlFrameDataBuilder1.PrimaryDatumReference.MaterialCondition.ToString();
                //sFcfData.SecondaryDatum = featureControlFrameDataBuilder1.SecondaryDatumReference.Letter;
                //sFcfData.SecondaryMaterialModifier = featureControlFrameDataBuilder1.SecondaryDatumReference.MaterialCondition.ToString();
                //sFcfData.TertiaryDatum = featureControlFrameDataBuilder1.TertiaryDatumReference.Letter;
                //sFcfData.TertiaryMaterialModifier = featureControlFrameDataBuilder1.TertiaryDatumReference.MaterialCondition.ToString();
            }
            catch (System.Exception ex)
            {
                return false;
            }
            return true;
        }

        /// <summary>
        /// 刪除指定的泡泡
        /// </summary>
        /// <param name="BallonNum"></param>
        /// <returns></returns>
        public static bool DeleteBallon(string BallonNum, NXObject[] DimensionAry)
        {
            try
            {
                IdSymbolCollection aa = workPart.Annotations.IdSymbols;
                IdSymbol[] bb = aa.ToArray();
                foreach (NXOpen.Annotations.IdSymbol i in bb)
                {
                    NXOpen.Annotations.IdSymbolBuilder idSymbolBuilder1;
                    idSymbolBuilder1 = workPart.Annotations.IdSymbols.CreateIdSymbolBuilder(i);
                    string a = idSymbolBuilder1.UpperText;
                    idSymbolBuilder1.Destroy();
                    if (BallonNum == a)
                    {
                        CaxPublic.DelectObject(i);
                    }
                }
                foreach (NXObject i in DimensionAry)
                {
                    string dicount = "", chekcLevel = "", balloonNum = "";
                    try
                    {
                        dicount = i.GetStringAttribute(CaxME.DimenAttr.DiCount);
                        balloonNum = i.GetStringAttribute(CaxME.DimenAttr.BallonNum);
                    }
                    catch (System.Exception ex)
                    {
                        dicount = "";
                        balloonNum = "";
                        continue;
                    }
                    try
                    {
                        chekcLevel = i.GetStringAttribute(CaxME.DimenAttr.CheckLevel);
                    }
                    catch (System.Exception ex)
                    {
                        chekcLevel = "";
                    }
                    if (dicount != "" && chekcLevel == "" && balloonNum == BallonNum)
                    {
                        CaxPublic.DelectObject(i);
                    }
                }
            }
            catch (System.Exception ex)
            {
                return false;
            }
            return true;
        }

        public static bool DeleteFixBallon(string BallonNum, NXObject[] DimensionAry)
        {
            try
            {
                IdSymbolCollection aa = workPart.Annotations.IdSymbols;
                IdSymbol[] bb = aa.ToArray();
                foreach (NXOpen.Annotations.IdSymbol i in bb)
                {
                    NXOpen.Annotations.IdSymbolBuilder idSymbolBuilder1;
                    idSymbolBuilder1 = workPart.Annotations.IdSymbols.CreateIdSymbolBuilder(i);
                    string a = idSymbolBuilder1.UpperText;
                    idSymbolBuilder1.Destroy();
                    if (BallonNum == a)
                    {
                        CaxPublic.DelectObject(i);
                    }
                }
                foreach (NXObject i in DimensionAry)
                {
                    string dicount = "", fixDimension = "";
                    try
                    {
                        dicount = i.GetStringAttribute(CaxME.DimenAttr.DiCount);
                    }
                    catch (System.Exception ex)
                    {
                        dicount = "";
                        continue;
                    }
                    try
                    {
                        fixDimension = i.GetStringAttribute(CaxME.DimenAttr.FixDimension);
                    }
                    catch (System.Exception ex)
                    {
                        fixDimension = "";
                    }
                    if (dicount != "" && fixDimension == "")
                    {
                        CaxPublic.DelectObject(i);
                    }
                }
            }
            catch (System.Exception ex)
            {
                return false;
            }
            return true;
        }

        public static bool DeleteCustomSymbol(string insertTime)
        {
            try
            {
                CustomSymbolCollection cusSymCollection = workPart.Annotations.CustomSymbols;
                CustomSymbol[] cusSymAry = cusSymCollection.ToArray();
                foreach (CustomSymbol i in cusSymAry)
                {
                    string symbolTime = "";
                    try
                    {
                        symbolTime = i.GetStringAttribute("KCInsertTime");
                    }
                    catch (System.Exception ex)
                    {
                        symbolTime = "";
                        continue;
                    }
                    if (symbolTime == insertTime)
                    {
                        CaxPublic.DelectObject(i);
                    }
                }
            }
            catch (System.Exception ex)
            {
                return false;
            }
            return true;
        }

        public static bool DeleteKCNote(string insertTime)
        {
            try
            {
                NXOpen.Annotations.Note[] NotesAry = workPart.Notes.ToArray();
                foreach (Note i in NotesAry)
                {
                    string symbolTime = "";
                    try
                    {
                        symbolTime = i.GetStringAttribute("KCInsertTime");
                    }
                    catch (System.Exception ex)
                    {
                        symbolTime = "";
                        continue;
                    }
                    if (symbolTime == insertTime)
                    {
                        CaxPublic.DelectObject(i);
                    }
                }
            }
            catch (System.Exception ex)
            {
                return false;
            }
            return true;
        }

        /// <summary>
        /// 修改Sheet名稱
        /// </summary>
        /// <param name="TargetSheet"></param>
        /// <param name="SheetName"></param>
        /// <returns></returns>
        public static bool SheetRename(DrawingSheet TargetSheet, string SheetName)
        {
            try
            {
                NXOpen.Drawings.DrawingSheetBuilder drawingSheetBuilder1;
                drawingSheetBuilder1 = workPart.DrawingSheets.DrawingSheetBuilder(TargetSheet);
                drawingSheetBuilder1.Name = SheetName;
                drawingSheetBuilder1.Commit();
                drawingSheetBuilder1.Destroy();
            }
            catch (System.Exception ex)
            {
                return false;
            }
            return true;
        }

        /// <summary>
        /// 取得圖紙的所有NXObject，原本的DisplayableObject型態會因為NXOpen.Preferences.WorkPlane失敗，故改成NXObject。
        /// 也可使用這種方式DisplayableObject[] SheetObj = CurrentSheet.View.AskVisibleObjects();
        /// </summary>
        /// <param name="view"></param>
        /// <returns></returns>
        public static List<NXObject> FindObjectsInView(Tag view)
        {
            List<NXObject> result = new List<NXObject>();
            Tag dimObj = Tag.Null;
            UFView.CycleObjectsEnum cycleType = UFView.CycleObjectsEnum.DependentObjects;

            theUfSession.View.CycleObjects(view, cycleType, ref dimObj);
            while (dimObj != Tag.Null)
            {
                if (theUfSession.Obj.AskStatus(dimObj) == UFConstants.UF_OBJ_ALIVE)
                {
                    NXObject theObj = (NXObject)NXObjectManager.Get(dimObj);
                    result.Add(theObj);
                }

                theUfSession.View.CycleObjects(view, cycleType, ref dimObj);
            }
            return result;
        }

        public static bool CreateOISPDF(List<NXOpen.Drawings.DrawingSheet> Sheet, string OutputFullPath)
        {
            try
            {
                Session theSession = Session.GetSession();
                Part workPart = theSession.Parts.Work;
                Part displayPart = theSession.Parts.Display;
                // ----------------------------------------------
                //   Menu: File->Export->PDF...
                // ----------------------------------------------

                PrintPDFBuilder printPDFBuilder1;
                printPDFBuilder1 = workPart.PlotManager.CreatePrintPdfbuilder();

                printPDFBuilder1.Scale = 1.0;

                printPDFBuilder1.Size = NXOpen.PrintPDFBuilder.SizeOption.ScaleFactor;

                printPDFBuilder1.OutputText = NXOpen.PrintPDFBuilder.OutputTextOption.Polylines;

                printPDFBuilder1.Units = NXOpen.PrintPDFBuilder.UnitsOption.English;

                printPDFBuilder1.XDimension = 8.5;

                printPDFBuilder1.YDimension = 11.0;

                printPDFBuilder1.RasterImages = true;



                printPDFBuilder1.Watermark = "";

                NXObject[] sheets1 = new NXObject[Sheet.Count];
                //NXOpen.Drawings.DrawingSheet drawingSheet1 = (NXOpen.Drawings.DrawingSheet)workPart.DrawingSheets.FindObject("S1");
                for (int i = 0; i < Sheet.Count;i++ )
                {
                    sheets1[i] = Sheet[i];
                }
                printPDFBuilder1.SourceBuilder.SetSheets(sheets1);

                printPDFBuilder1.Filename = OutputFullPath;

                //NXOpen.Sketch.ConstraintVisibility visibility1;
                //visibility1 = theSession.ActiveSketch.VisibilityOfConstraints;

                //theSession.ActiveSketch.VisibilityOfConstraints = NXOpen.Sketch.ConstraintVisibility.None;

                //theSession.ActiveSketch.VisibilityOfConstraints = NXOpen.Sketch.ConstraintVisibility.Some;

                NXObject nXObject1;
                nXObject1 = printPDFBuilder1.Commit();


                printPDFBuilder1.Destroy();


                // ----------------------------------------------
                //   Menu: Tools->Journal->Stop Recording
                // ----------------------------------------------
            }
            catch (System.Exception ex)
            {
                return false;
            }
            return true;
        }

        public static bool GetObjectAttribute(DisplayableObject singleObj, out ObjectAttribute sObjectAttribute)
        {
            sObjectAttribute = new ObjectAttribute();
            try
            {
                //取得Non-SelfCheck的Excel屬性
                try { sObjectAttribute.singleObjExcel = singleObj.GetStringAttribute(CaxME.DimenAttr.AssignExcelType); }
                catch (System.Exception ex) { sObjectAttribute.singleObjExcel = ""; }

                //取得SelfCheck的Excel屬性
                try { sObjectAttribute.singleSelfCheckExcel = singleObj.GetStringAttribute(CaxME.DimenAttr.SelfCheckExcel); }
                catch (System.Exception ex) { sObjectAttribute.singleSelfCheckExcel = ""; }

                //取得keyChara、productName、customerBalloon、SPCControl
                try { sObjectAttribute.keyChara = singleObj.GetStringAttribute(CaxME.DimenAttr.KC); }
                catch (System.Exception ex) { sObjectAttribute.keyChara = ""; }

                try { sObjectAttribute.productName = singleObj.GetStringAttribute(CaxME.DimenAttr.Product); }
                catch (System.Exception ex) { sObjectAttribute.productName = ""; }

                try { sObjectAttribute.customerBalloon = singleObj.GetStringAttribute(CaxME.DimenAttr.CustomerBalloon); }
                catch (System.Exception ex) { sObjectAttribute.customerBalloon = ""; }

                try { sObjectAttribute.spcControl = singleObj.GetStringAttribute(CaxME.DimenAttr.SpcControl); }
                catch (System.Exception ex) { sObjectAttribute.spcControl = ""; }
            }
            catch (System.Exception ex)
            {
                return false;
            }
            return true;
        }

        public static bool GetFixDimenAttribute(DisplayableObject singleObj, out FixDimenAttr sFixDimenAttr)
        {
            sFixDimenAttr = new FixDimenAttr();
            try
            {
                //取得Non-SelfCheck的Excel屬性
                try { sFixDimenAttr.FixDimension = singleObj.GetStringAttribute(CaxME.DimenAttr.FixDimension); }
                catch (System.Exception ex) { sFixDimenAttr.FixDimension = ""; }

                //取得SelfCheck的Excel屬性
                try { sFixDimenAttr.DiCount = singleObj.GetStringAttribute(CaxME.DimenAttr.DiCount); }
                catch (System.Exception ex) { sFixDimenAttr.DiCount = ""; }

                //取得keyChara、productName、customerBalloon、SPCControl
                try { sFixDimenAttr.BallonNum = singleObj.GetStringAttribute(CaxME.DimenAttr.BallonNum); }
                catch (System.Exception ex) { sFixDimenAttr.BallonNum = ""; }
            }
            catch (System.Exception ex)
            {
                return false;
            }
            return true;
        }

        public static bool GetDimensionData(string dimenExcelType, DisplayableObject singleObj, out DimensionData cDimensionData)
        {
            cDimensionData = new DimensionData();
            try
            {
                if (dimenExcelType.ToUpper() == "SELFCHECK")
                {
                    try { cDimensionData.instrument = singleObj.GetStringAttribute(CaxME.DimenAttr.SelfCheck_Gauge); }
                    catch (System.Exception ex) { return false; }
                    try { cDimensionData.frequency = singleObj.GetStringAttribute(CaxME.DimenAttr.SelfCheck_Frequency); }
                    catch (System.Exception ex) { }
                    try { cDimensionData.selfCheck_Size = singleObj.GetStringAttribute(CaxME.DimenAttr.SelfCheck_Size); }
                    catch (System.Exception ex) { }
                    try { cDimensionData.selfCheck_Freq = singleObj.GetStringAttribute(CaxME.DimenAttr.SelfCheck_Freq); }
                    catch (System.Exception ex) { }
                }
                else
                {
                    if (dimenExcelType.ToUpper() == "IQC")
                    {
                        try { cDimensionData.instrument = singleObj.GetStringAttribute(CaxME.DimenAttr.Gauge); }
                        catch (System.Exception ex) { return false; }
                        try { cDimensionData.frequency = singleObj.GetStringAttribute(CaxME.DimenAttr.IQC_Frequency); }
                        catch (System.Exception ex) { }
                        try { cDimensionData.size = singleObj.GetStringAttribute(CaxME.DimenAttr.IQC_Size); }
                        catch (System.Exception ex) { }
                        try { cDimensionData.freq = singleObj.GetStringAttribute(CaxME.DimenAttr.IQC_Freq); }
                        catch (System.Exception ex) { }
                    }
                    if (dimenExcelType.ToUpper() == "IPQC")
                    {
                        try { cDimensionData.instrument = singleObj.GetStringAttribute(CaxME.DimenAttr.Gauge); }
                        catch (System.Exception ex) { return false; }
                        try { cDimensionData.frequency = singleObj.GetStringAttribute(CaxME.DimenAttr.IPQC_Frequency); }
                        catch (System.Exception ex) { }
                        try { cDimensionData.size = singleObj.GetStringAttribute(CaxME.DimenAttr.IPQC_Size); }
                        catch (System.Exception ex) { }
                        try { cDimensionData.freq = singleObj.GetStringAttribute(CaxME.DimenAttr.IPQC_Freq); }
                        catch (System.Exception ex) { }
                    }
                    if (dimenExcelType.ToUpper() == "FAI")
                    {
                        try { cDimensionData.instrument = singleObj.GetStringAttribute(CaxME.DimenAttr.Gauge); }
                        catch (System.Exception ex) { return false; }
                        try { cDimensionData.frequency = singleObj.GetStringAttribute(CaxME.DimenAttr.FAI_Frequency); }
                        catch (System.Exception ex) { }
                        try { cDimensionData.size = singleObj.GetStringAttribute(CaxME.DimenAttr.FAI_Size); }
                        catch (System.Exception ex) { }
                        try { cDimensionData.freq = singleObj.GetStringAttribute(CaxME.DimenAttr.FAI_Freq); }
                        catch (System.Exception ex) { }
                    }
                    if (dimenExcelType.ToUpper() == "FQC")
                    {
                        try { cDimensionData.instrument = singleObj.GetStringAttribute(CaxME.DimenAttr.Gauge); }
                        catch (System.Exception ex) { return false; }
                        try { cDimensionData.frequency = singleObj.GetStringAttribute(CaxME.DimenAttr.FQC_Frequency); }
                        catch (System.Exception ex) { }
                        try { cDimensionData.size = singleObj.GetStringAttribute(CaxME.DimenAttr.FQC_Size); }
                        catch (System.Exception ex) { }
                        try { cDimensionData.freq = singleObj.GetStringAttribute(CaxME.DimenAttr.FQC_Freq); }
                        catch (System.Exception ex) { }
                    }
                }

                try { cDimensionData.ballonNum = Convert.ToInt32(singleObj.GetStringAttribute(CaxME.DimenAttr.BallonNum)); }
                catch (System.Exception ex) { return false; }

                try { cDimensionData.location = singleObj.GetStringAttribute(CaxME.DimenAttr.BallonLocation); }
                catch (System.Exception ex) { return false; }

                try { cDimensionData.checkLevel = singleObj.GetStringAttribute(CaxME.DimenAttr.CheckLevel); }
                catch (System.Exception ex) { return false; }

                try { cDimensionData.balloonCount = singleObj.GetStringAttribute(CaxME.DimenAttr.DiCount); }
                catch (System.Exception ex) { }

                if (singleObj is NXOpen.Annotations.Annotation)
                {
                    if (singleObj is NXOpen.Annotations.DraftingFcf)
                    {
                        #region DraftingFcf取Text

                        NXOpen.Annotations.DraftingFcf temp = (NXOpen.Annotations.DraftingFcf)singleObj;
                        CaxME.FcfData sFcfData = new CaxME.FcfData();
                        CaxME.GetFcfData(temp, out sFcfData);
                        cDimensionData.type = "NXOpen.Annotations.DraftingFcf";
                        cDimensionData.characteristic = sFcfData.Characteristic;
                        cDimensionData.zoneShape = sFcfData.ZoneShape;
                        cDimensionData.toleranceValue = sFcfData.ToleranceValue;
                        cDimensionData.materialModifier = sFcfData.MaterialModifier;
                        cDimensionData.primaryDatum = sFcfData.PrimaryDatum;
                        cDimensionData.primaryMaterialModifier = sFcfData.PrimaryMaterialModifier;
                        cDimensionData.secondaryDatum = sFcfData.SecondaryDatum;
                        cDimensionData.secondaryMaterialModifier = sFcfData.SecondaryMaterialModifier;
                        cDimensionData.tertiaryDatum = sFcfData.TertiaryDatum;
                        cDimensionData.tertiaryMaterialModifier = sFcfData.TertiaryMaterialModifier;

                        #endregion
                    }
                    else
                    {
                        #region 尺寸公差
                        Tag annTag = singleObj.Tag;
                        
                        
                        
                        int
                            ann_data_type,
                            ann_data_form,
                            num_segments,
                            cycle = 0,
                            ii,
                            length,
                            size;
                        double
                            radius_angle;
                        int[]
                            ann_data = new int[10],
                            mask = new int[4] { 0, 0, 1, 0 };
                        double[]
                            ann_origin = new double[2];


                    AskAnnData:
                        theUfSession.Drf.AskAnnData(ref annTag,
                                                           mask,
                                                           ref cycle,
                                                           ann_data,
                                                           out ann_data_type,
                                                           out ann_data_form,
                                                           out num_segments,
                                                           ann_origin,
                                                           out radius_angle);
                        //CaxLog.ShowListingWindow("ann_data_type：" + ann_data_type.ToString());
                        //CaxLog.ShowListingWindow("ann_data_form：" + ann_data_form.ToString());
                        if (cycle != 0)
                        {
                            for (ii = 1; ii <= num_segments; ii++)
                            {
                                string cr3;
                                theUfSession.Drf.AskTextData(ii, ref ann_data[0], out cr3, out size, out length);
                                MappingDimensionData(ann_data_type, ann_data_form, cr3, ref cDimensionData);
                                //CaxLog.ShowListingWindow("cr3：" + cr3);
                                //CaxLog.ShowListingWindow(size.ToString());
                                //CaxLog.ShowListingWindow(length.ToString());
                            }
                            goto AskAnnData;
                        }
                        //如果ToleranceType=Basic.Reference...等等不需要公差的則跳出
                        if (singleObj is NXOpen.Annotations.Dimension)
                        {
                            NXOpen.Annotations.Dimension a = (NXOpen.Annotations.Dimension)NXObjectManager.Get(singleObj.Tag);
                            cDimensionData.tolType = a.ToleranceType.ToString();
                            if (a.ToleranceType.ToString() == "Basic" || a.ToleranceType.ToString() == "Reference")
                            {
                                return true;
                            }
                        }
                        #endregion
                    }
                    try
                    {
                        if (cDimensionData.upperTol != null)
                        {
                            if (cDimensionData.mainText.Contains("<$s>"))
                            {
                                string tempmainText = cDimensionData.mainText.Replace("<$s>", "");
                                string tempupperTol = cDimensionData.upperTol.Replace("<$s>", "");
                                cDimensionData.maxTolerance = (Convert.ToDouble(tempmainText) + Convert.ToDouble(tempupperTol)).ToString() + "~";
                            }
                            else
                            {
                                cDimensionData.maxTolerance = (Convert.ToDouble(cDimensionData.mainText) + Convert.ToDouble(cDimensionData.upperTol)).ToString();
                            }

                        }
                        if (cDimensionData.lowerTol != null)
                        {
                            if (cDimensionData.mainText.Contains("<$s>"))
                            {
                                string tempmainText = cDimensionData.mainText.Replace("<$s>", "");
                                string templowerTol = cDimensionData.lowerTol.Replace("<$s>", "");
                                cDimensionData.minTolerance = (Convert.ToDouble(tempmainText) - Convert.ToDouble(templowerTol)).ToString() + "~";
                            }
                            else
                            {
                                cDimensionData.minTolerance = (Convert.ToDouble(cDimensionData.mainText) - Convert.ToDouble(cDimensionData.lowerTol)).ToString();
                            }
                        }
                        //此處加入沒有設定公差時，用一般公差計算，判斷afterText也null才進入是怕使用者用afterText功能去加入公差
                        if (cDimensionData.lowerTol == null && cDimensionData.upperTol == null && cDimensionData.afterText == null)
                        {
                            string temp = "";
                            //判斷是否純數字
                            temp = cDimensionData.mainText;
                            temp = temp.Replace(".", "");
                            int n;
                            int type = -1;
                            if (int.TryParse(temp, out n))
                            {
                                type = 0;
                            }
                            else
                            {
                                temp = temp.Replace("<$s>", "");
                                if (int.TryParse(temp, out n))
                                {
                                    type = 1;
                                }
                                else
                                {
                                    return true;
                                }
                            }

                            if (WhichGeneralTol(workPart) == 0)
                            {
                                if (type == 0)
                                {
                                    #region 小數點
                                    Dictionary<string, string> DicTolPoint = new Dictionary<string, string>();
                                    GetTolPoint(workPart, out DicTolPoint);
                                    //判斷是小數點幾位
                                    temp = cDimensionData.mainText;
                                    string[] SplitStr = temp.Split('.');
                                    //由小數點幾位來取得一般公差
                                    string TolValue = "";
                                    if (SplitStr.Length == 1)
                                    {
                                        //TolValue = workPart.GetStringAttribute(CaxME.TablePosi.TolValue0Pos);
                                        DicTolPoint.TryGetValue("0", out TolValue);
                                    }
                                    else
                                    {
                                        switch (SplitStr[1].Length)
                                        {
                                            case 1:
                                                DicTolPoint.TryGetValue("1", out TolValue);
                                                break;
                                            case 2:
                                                DicTolPoint.TryGetValue("2", out TolValue);
                                                break;
                                            case 3:
                                                DicTolPoint.TryGetValue("3", out TolValue);
                                                break;
                                            case 4:
                                                DicTolPoint.TryGetValue("4", out TolValue);
                                                break;
                                        }
                                    }
                                    cDimensionData.maxTolerance = (Convert.ToDouble(cDimensionData.mainText) + Convert.ToDouble(TolValue)).ToString();
                                    cDimensionData.minTolerance = (Convert.ToDouble(cDimensionData.mainText) - Convert.ToDouble(TolValue)).ToString();
                                    cDimensionData.lowerTol = TolValue;
                                    cDimensionData.upperTol = TolValue;
                                    #endregion
                                }
                                else if (type == 1)
                                {
                                    #region 角度
                                    string angleTol = "";
                                    GetTolAngle(workPart, out angleTol);
                                    temp = cDimensionData.mainText;
                                    temp = temp.Replace("<$s>", "");
                                    cDimensionData.maxTolerance = (Convert.ToDouble(temp) + Convert.ToDouble(angleTol)).ToString() + "~";
                                    cDimensionData.minTolerance = (Convert.ToDouble(temp) - Convert.ToDouble(angleTol)).ToString() + "~";
                                    cDimensionData.lowerTol = angleTol + "<$s>";
                                    cDimensionData.upperTol = angleTol + "<$s>";
                                    #endregion
                                }
                            }
                            else if (WhichGeneralTol(workPart) == 1)
                            {
                                #region 範圍
                                Dictionary<double, double> DicTolRegion = new Dictionary<double, double>();
                                GetTolRegion(workPart, out DicTolRegion);
                                foreach (KeyValuePair<double, double> kvp in DicTolRegion)
                                {
                                    if (Convert.ToDouble(cDimensionData.mainText) > kvp.Key)
                                    {
                                        continue;
                                    }
                                    else
                                    {
                                        cDimensionData.maxTolerance = (Convert.ToDouble(cDimensionData.mainText) + kvp.Value).ToString();
                                        cDimensionData.minTolerance = (Convert.ToDouble(cDimensionData.mainText) - kvp.Value).ToString();
                                        cDimensionData.lowerTol = kvp.Value.ToString();
                                        cDimensionData.upperTol = kvp.Value.ToString();
                                        break;
                                    }
                                }
                                #endregion
                            }
                        }
                    }
                    catch (System.Exception ex)
                    {

                    }
                }
            }
            catch (System.Exception ex)
            {
                return false;
            }
            return true;
        }

        public static bool GetFixDimensionData(DisplayableObject singleObj, out DimensionData cDimensionData)
        {
            cDimensionData = new DimensionData();
            try
            {
                try { cDimensionData.ballonNum = Convert.ToInt32(singleObj.GetStringAttribute(CaxME.DimenAttr.BallonNum)); }
                catch (System.Exception ex) { return false; }

                try { cDimensionData.balloonCount = singleObj.GetStringAttribute(CaxME.DimenAttr.DiCount); }
                catch (System.Exception ex) { }

                if (singleObj is NXOpen.Annotations.Annotation)
                {
                    if (singleObj is NXOpen.Annotations.DraftingFcf)
                    {
                        #region DraftingFcf取Text

                        NXOpen.Annotations.DraftingFcf temp = (NXOpen.Annotations.DraftingFcf)singleObj;
                        CaxME.FcfData sFcfData = new CaxME.FcfData();
                        CaxME.GetFcfData(temp, out sFcfData);
                        cDimensionData.type = "NXOpen.Annotations.DraftingFcf";
                        cDimensionData.characteristic = sFcfData.Characteristic;
                        cDimensionData.zoneShape = sFcfData.ZoneShape;
                        cDimensionData.toleranceValue = sFcfData.ToleranceValue;
                        cDimensionData.materialModifier = sFcfData.MaterialModifier;
                        cDimensionData.primaryDatum = sFcfData.PrimaryDatum;
                        cDimensionData.primaryMaterialModifier = sFcfData.PrimaryMaterialModifier;
                        cDimensionData.secondaryDatum = sFcfData.SecondaryDatum;
                        cDimensionData.secondaryMaterialModifier = sFcfData.SecondaryMaterialModifier;
                        cDimensionData.tertiaryDatum = sFcfData.TertiaryDatum;
                        cDimensionData.tertiaryMaterialModifier = sFcfData.TertiaryMaterialModifier;

                        #endregion
                    }
                    else
                    {
                        #region 尺寸公差
                        Tag annTag = singleObj.Tag;

                        int
                            ann_data_type,
                            ann_data_form,
                            num_segments,
                            cycle = 0,
                            ii,
                            length,
                            size;
                        double
                            radius_angle;
                        int[]
                            ann_data = new int[10],
                            mask = new int[4] { 0, 0, 1, 0 };
                        double[]
                            ann_origin = new double[2];


                    AskAnnData:
                        theUfSession.Drf.AskAnnData(ref annTag,
                                                           mask,
                                                           ref cycle,
                                                           ann_data,
                                                           out ann_data_type,
                                                           out ann_data_form,
                                                           out num_segments,
                                                           ann_origin,
                                                           out radius_angle);
                        //CaxLog.ShowListingWindow("ann_data_type：" + ann_data_type.ToString());
                        //CaxLog.ShowListingWindow("ann_data_form：" + ann_data_form.ToString());
                        if (cycle != 0)
                        {
                            for (ii = 1; ii <= num_segments; ii++)
                            {
                                string cr3;
                                theUfSession.Drf.AskTextData(ii, ref ann_data[0], out cr3, out size, out length);
                                MappingDimensionData(ann_data_type, ann_data_form, cr3, ref cDimensionData);
                                //CaxLog.ShowListingWindow("cr3：" + cr3);
                                //CaxLog.ShowListingWindow(size.ToString());
                                //CaxLog.ShowListingWindow(length.ToString());
                            }
                            goto AskAnnData;
                        }
                        #endregion
                    }
                    try
                    {
                        if (cDimensionData.upperTol != null)
                        {
                            if (cDimensionData.mainText.Contains("<$s>"))
                            {
                                string tempmainText = cDimensionData.mainText.Replace("<$s>", "");
                                string tempupperTol = cDimensionData.upperTol.Replace("<$s>", "");
                                cDimensionData.maxTolerance = (Convert.ToDouble(tempmainText) + Convert.ToDouble(tempupperTol)).ToString() + "~";
                            }
                            else
                            {
                                cDimensionData.maxTolerance = (Convert.ToDouble(cDimensionData.mainText) + Convert.ToDouble(cDimensionData.upperTol)).ToString();
                            }

                        }
                        if (cDimensionData.lowerTol != null)
                        {
                            if (cDimensionData.mainText.Contains("<$s>"))
                            {
                                string tempmainText = cDimensionData.mainText.Replace("<$s>", "");
                                string templowerTol = cDimensionData.lowerTol.Replace("<$s>", "");
                                cDimensionData.minTolerance = (Convert.ToDouble(tempmainText) - Convert.ToDouble(templowerTol)).ToString() + "~";
                            }
                            else
                            {
                                cDimensionData.minTolerance = (Convert.ToDouble(cDimensionData.mainText) - Convert.ToDouble(cDimensionData.lowerTol)).ToString();
                            }
                        }
                        //此處加入沒有設定公差時，用一般公差計算，判斷afterText也null才進入是怕使用者用afterText功能去加入公差
                        if (cDimensionData.lowerTol == null && cDimensionData.upperTol == null && cDimensionData.afterText == null)
                        {
                            string temp = "";
                            //判斷是否純數字
                            temp = cDimensionData.mainText;
                            temp = temp.Replace(".", "");
                            int n;
                            int type = -1;
                            if (int.TryParse(temp, out n))
                            {
                                type = 0;
                            }
                            else
                            {
                                temp = temp.Replace("<$s>", "");
                                if (int.TryParse(temp, out n))
                                {
                                    type = 1;
                                }
                                else
                                {
                                    return true;
                                }
                            }

                            if (WhichGeneralTol(workPart) == 0)
                            {
                                if (type == 0)
                                {
                                    #region 小數點
                                    Dictionary<string, string> DicTolPoint = new Dictionary<string, string>();
                                    GetTolPoint(workPart, out DicTolPoint);
                                    //判斷是小數點幾位
                                    temp = cDimensionData.mainText;
                                    string[] SplitStr = temp.Split('.');
                                    //由小數點幾位來取得一般公差
                                    string TolValue = "";
                                    if (SplitStr.Length == 1)
                                    {
                                        //TolValue = workPart.GetStringAttribute(CaxME.TablePosi.TolValue0Pos);
                                        DicTolPoint.TryGetValue("0", out TolValue);
                                    }
                                    else
                                    {
                                        switch (SplitStr[1].Length)
                                        {
                                            case 1:
                                                DicTolPoint.TryGetValue("1", out TolValue);
                                                break;
                                            case 2:
                                                DicTolPoint.TryGetValue("2", out TolValue);
                                                break;
                                            case 3:
                                                DicTolPoint.TryGetValue("3", out TolValue);
                                                break;
                                            case 4:
                                                DicTolPoint.TryGetValue("4", out TolValue);
                                                break;
                                        }
                                    }
                                    cDimensionData.maxTolerance = (Convert.ToDouble(cDimensionData.mainText) + Convert.ToDouble(TolValue)).ToString();
                                    cDimensionData.minTolerance = (Convert.ToDouble(cDimensionData.mainText) - Convert.ToDouble(TolValue)).ToString();
                                    cDimensionData.lowerTol = TolValue;
                                    cDimensionData.upperTol = TolValue;
                                    #endregion
                                }
                                else if (type == 1)
                                {
                                    #region 角度
                                    string angleTol = "";
                                    GetTolAngle(workPart, out angleTol);
                                    temp = cDimensionData.mainText;
                                    temp = temp.Replace("<$s>", "");
                                    cDimensionData.maxTolerance = (Convert.ToDouble(temp) + Convert.ToDouble(angleTol)).ToString() + "~";
                                    cDimensionData.minTolerance = (Convert.ToDouble(temp) - Convert.ToDouble(angleTol)).ToString() + "~";
                                    cDimensionData.lowerTol = angleTol + "~";
                                    cDimensionData.upperTol = angleTol + "~";
                                    #endregion
                                }
                            }
                            else if (WhichGeneralTol(workPart) == 1)
                            {
                                #region 範圍
                                Dictionary<double, double> DicTolRegion = new Dictionary<double, double>();
                                GetTolRegion(workPart, out DicTolRegion);
                                foreach (KeyValuePair<double, double> kvp in DicTolRegion)
                                {
                                    if (Convert.ToDouble(cDimensionData.mainText) > kvp.Key)
                                    {
                                        continue;
                                    }
                                    else
                                    {
                                        cDimensionData.maxTolerance = (Convert.ToDouble(cDimensionData.mainText) + kvp.Value).ToString();
                                        cDimensionData.minTolerance = (Convert.ToDouble(cDimensionData.mainText) - kvp.Value).ToString();
                                        cDimensionData.lowerTol = kvp.Value.ToString();
                                        cDimensionData.upperTol = kvp.Value.ToString();
                                        break;
                                    }
                                }
                                #endregion
                            }
                        }
                    }
                    catch (System.Exception ex)
                    {

                    }
                }
            }
            catch (System.Exception ex)
            {
                return false;
            }
            return true;
        }

        public static int WhichGeneralTol(Part workPart)
        {
            string x = "";
            try
            {
                x = workPart.GetStringAttribute(CaxME.TablePosi.TolTitle0Pos);
                if (x.Contains("X"))
                {
                    return 0;
                }
                else
                {
                    return 1;
                }
            }
            catch (System.Exception ex)
            {

            }
            try
            {
                x = workPart.GetStringAttribute(CaxME.TablePosi.TolTitle1Pos);
                if (x.Contains("X"))
                {
                    return 0;
                }
                else
                {
                    return 1;
                }
            }
            catch (System.Exception ex)
            {

            }
            try
            {
                x = workPart.GetStringAttribute(CaxME.TablePosi.TolTitle2Pos);
                if (x.Contains("X"))
                {
                    return 0;
                }
                else
                {
                    return 1;
                }
            }
            catch (System.Exception ex)
            {

            }
            try
            {
                x = workPart.GetStringAttribute(CaxME.TablePosi.TolTitle3Pos);
                if (x.Contains("X"))
                {
                    return 0;
                }
                else
                {
                    return 1;
                }
            }
            catch (System.Exception ex)
            {

            }
            try
            {
                x = workPart.GetStringAttribute(CaxME.TablePosi.TolTitle4Pos);
                if (x.Contains("X"))
                {
                    return 0;
                }
                else
                {
                    return 1;
                }
            }
            catch (System.Exception ex)
            {

            }
            return 2;
        }

        public static bool GetTolRegion(Part workPart, out Dictionary<double, double> DicTolRegion)
        {
            DicTolRegion = new Dictionary<double, double>();
            string x = "", value = "", y = "";
            try
            {
                x = workPart.GetStringAttribute(CaxME.TablePosi.TolTitle0Pos);
                value = x.Split('-')[1];
                y = workPart.GetStringAttribute(CaxME.TablePosi.TolValue0Pos);
                DicTolRegion[Convert.ToDouble(value)] = Convert.ToDouble(y);
            }
            catch (System.Exception ex)
            {
                x = "";
                value = "";
                y = "";
            }
            try
            {
                x = workPart.GetStringAttribute(CaxME.TablePosi.TolTitle1Pos);
                value = x.Split('-')[1];
                y = workPart.GetStringAttribute(CaxME.TablePosi.TolValue1Pos);
                DicTolRegion[Convert.ToDouble(value)] = Convert.ToDouble(y);
            }
            catch (System.Exception ex)
            {
                x = "";
                value = "";
                y = "";
            }
            try
            {
                x = workPart.GetStringAttribute(CaxME.TablePosi.TolTitle2Pos);
                value = x.Split('-')[1];
                y = workPart.GetStringAttribute(CaxME.TablePosi.TolValue2Pos);
                DicTolRegion[Convert.ToDouble(value)] = Convert.ToDouble(y);
            }
            catch (System.Exception ex)
            {
                x = "";
                value = "";
                y = "";
            }
            try
            {
                x = workPart.GetStringAttribute(CaxME.TablePosi.TolTitle3Pos);
                value = x.Split('-')[1];
                y = workPart.GetStringAttribute(CaxME.TablePosi.TolValue3Pos);
                DicTolRegion[Convert.ToDouble(value)] = Convert.ToDouble(y);
            }
            catch (System.Exception ex)
            {
                x = "";
                value = "";
                y = "";
            }
            try
            {
                x = workPart.GetStringAttribute(CaxME.TablePosi.TolTitle4Pos);
                value = x.Split('-')[1];
                y = workPart.GetStringAttribute(CaxME.TablePosi.TolValue4Pos);
                DicTolRegion[Convert.ToDouble(value)] = Convert.ToDouble(y);
            }
            catch (System.Exception ex)
            {
                x = "";
                value = "";
                y = "";
            }
            return true;
        }

        public static bool GetTolPoint(Part workPart, out Dictionary<string, string> DicTolPoint)
        {
            DicTolPoint = new Dictionary<string, string>();
            string x = "";
            string[] SplitValue;
            try
            {
                x = workPart.GetStringAttribute(CaxME.TablePosi.TolTitle0Pos);
                SplitValue = x.Split('.');
                if (SplitValue.Length == 1 || (SplitValue.Length == 2 && SplitValue[1] == ""))
                {
                    DicTolPoint["0"] = workPart.GetStringAttribute(CaxME.TablePosi.TolValue0Pos);
                }
                else
                {
                    switch (SplitValue[1].Length)
                    {
                        case 1:
                            DicTolPoint["1"] = workPart.GetStringAttribute(CaxME.TablePosi.TolValue0Pos);
                            break;
                        case 2:
                            DicTolPoint["2"] = workPart.GetStringAttribute(CaxME.TablePosi.TolValue0Pos);
                            break;
                        case 3:
                            DicTolPoint["3"] = workPart.GetStringAttribute(CaxME.TablePosi.TolValue0Pos);
                            break;
                        case 4:
                            DicTolPoint["4"] = workPart.GetStringAttribute(CaxME.TablePosi.TolValue0Pos);
                            break;
                    }
                }
            }
            catch (System.Exception ex)
            {
                x = "";
                SplitValue = new string[] { };
            }
            try
            {
                x = workPart.GetStringAttribute(CaxME.TablePosi.TolTitle1Pos);
                SplitValue = x.Split('.');
                if (SplitValue.Length == 1)
                {
                    DicTolPoint["0"] = workPart.GetStringAttribute(CaxME.TablePosi.TolValue1Pos);
                }
                else
                {
                    switch (SplitValue[1].Length)
                    {
                        case 1:
                            DicTolPoint["1"] = workPart.GetStringAttribute(CaxME.TablePosi.TolValue1Pos);
                            break;
                        case 2:
                            DicTolPoint["2"] = workPart.GetStringAttribute(CaxME.TablePosi.TolValue1Pos);
                            break;
                        case 3:
                            DicTolPoint["3"] = workPart.GetStringAttribute(CaxME.TablePosi.TolValue1Pos);
                            break;
                        case 4:
                            DicTolPoint["4"] = workPart.GetStringAttribute(CaxME.TablePosi.TolValue1Pos);
                            break;
                    }
                }
            }
            catch (System.Exception ex)
            {
                x = "";
                SplitValue = new string[] { };
            }
            try
            {
                x = workPart.GetStringAttribute(CaxME.TablePosi.TolTitle2Pos);
                SplitValue = x.Split('.');
                if (SplitValue.Length == 1)
                {
                    DicTolPoint["0"] = workPart.GetStringAttribute(CaxME.TablePosi.TolValue2Pos);
                }
                else
                {
                    switch (SplitValue[1].Length)
                    {
                        case 1:
                            DicTolPoint["1"] = workPart.GetStringAttribute(CaxME.TablePosi.TolValue2Pos);
                            break;
                        case 2:
                            DicTolPoint["2"] = workPart.GetStringAttribute(CaxME.TablePosi.TolValue2Pos);
                            break;
                        case 3:
                            DicTolPoint["3"] = workPart.GetStringAttribute(CaxME.TablePosi.TolValue2Pos);
                            break;
                        case 4:
                            DicTolPoint["4"] = workPart.GetStringAttribute(CaxME.TablePosi.TolValue2Pos);
                            break;
                    }
                }
            }
            catch (System.Exception ex)
            {
                x = "";
                SplitValue = new string[] { };
            }
            try
            {
                x = workPart.GetStringAttribute(CaxME.TablePosi.TolTitle3Pos);
                SplitValue = x.Split('.');
                if (SplitValue.Length == 1)
                {
                    DicTolPoint["0"] = workPart.GetStringAttribute(CaxME.TablePosi.TolValue3Pos);
                }
                else
                {
                    switch (SplitValue[1].Length)
                    {
                        case 1:
                            DicTolPoint["1"] = workPart.GetStringAttribute(CaxME.TablePosi.TolValue3Pos);
                            break;
                        case 2:
                            DicTolPoint["2"] = workPart.GetStringAttribute(CaxME.TablePosi.TolValue3Pos);
                            break;
                        case 3:
                            DicTolPoint["3"] = workPart.GetStringAttribute(CaxME.TablePosi.TolValue3Pos);
                            break;
                        case 4:
                            DicTolPoint["4"] = workPart.GetStringAttribute(CaxME.TablePosi.TolValue3Pos);
                            break;
                    }
                }
            }
            catch (System.Exception ex)
            {
                x = "";
                SplitValue = new string[] { };
            }
            try
            {
                x = workPart.GetStringAttribute(CaxME.TablePosi.TolTitle4Pos);
                SplitValue = x.Split('.');
                if (SplitValue.Length == 1)
                {
                    DicTolPoint["0"] = workPart.GetStringAttribute(CaxME.TablePosi.TolValue4Pos);
                }
                else
                {
                    switch (SplitValue[1].Length)
                    {
                        case 1:
                            DicTolPoint["1"] = workPart.GetStringAttribute(CaxME.TablePosi.TolValue4Pos);
                            break;
                        case 2:
                            DicTolPoint["2"] = workPart.GetStringAttribute(CaxME.TablePosi.TolValue4Pos);
                            break;
                        case 3:
                            DicTolPoint["3"] = workPart.GetStringAttribute(CaxME.TablePosi.TolValue4Pos);
                            break;
                        case 4:
                            DicTolPoint["4"] = workPart.GetStringAttribute(CaxME.TablePosi.TolValue4Pos);
                            break;
                    }
                }
            }
            catch (System.Exception ex)
            {
                x = "";
                SplitValue = new string[] { };
            }
            return true;
        }

        public static bool GetTolAngle(Part workPart, out string angle)
        {
            angle = "";
            try
            {
                angle = workPart.GetStringAttribute(CaxME.TablePosi.AngleValuePos);
            }
            catch (System.Exception ex)
            {
                angle = "";
                return false;
            }
            return true;
        }

        public static bool MappingDimensionData(int ann_type, int ann_form, string text, ref DimensionData cDimensionData)
        {
            try
            {
                if (ann_type == 3)
                {
                    switch (ann_form)
                    {
                        case 1:
                            string mainText = "";
                            SplitMainText(text, out mainText);
                            if (cDimensionData.mainText == null)
                            {
                                cDimensionData.mainText = mainText;
                            }
                            else
                            {
                                cDimensionData.mainText = cDimensionData.mainText + "\r\n" + mainText;
                            }
                            break;
                        case 3:
                            string upperTol = "", lowerTol = "";
                            SplitTolerance(text, out upperTol, out lowerTol);
                            cDimensionData.upperTol = upperTol;
                            cDimensionData.lowerTol = lowerTol;
                            break;
                        case 5:
                            cDimensionData.toleranceSymbol = text;
                            break;
                        case 50:
                            cDimensionData.aboveText = text;
                            break;
                        case 51:
                            cDimensionData.belowText = text;
                            break;
                        case 52:
                            cDimensionData.beforeText = text;
                            break;
                        case 53:
                            cDimensionData.afterText = text;
                            break;
                        case 63:
                            cDimensionData.x = text;
                            break;
                        case 64:
                            cDimensionData.chamferAngle = text;
                            break;
                    }
                }
            }
            catch (System.Exception ex)
            {
                return false;
            }
            return true;
        }

        public static bool SplitMainText(string text, out string mainText)
        {
            mainText = "";
            try
            {
                string[] splitTol = text.Split('!');
                if (splitTol.Length > 1)
                {
                    splitTol[0] = splitTol[0].Remove(0, 2);
                    splitTol[1] = splitTol[1].Remove(splitTol[1].Length - 1);
                    mainText = splitTol[0] + "-" + splitTol[1];
                }
                else
                {
                    mainText = text;
                }
            }
            catch (System.Exception ex)
            {
                return false;
            }
            return true;
        }

        public static bool SplitTolerance(string tolerance, out string upperTol, out string lowerTol)
        {
            upperTol = "";
            lowerTol = "";
            try
            {
                string[] splitTol = tolerance.Split('!');
                if (splitTol.Length > 1)
                {
                    upperTol = splitTol[0].Remove(0, 2);
                    upperTol = upperTol.Replace("+", "");
                    lowerTol = splitTol[1].Remove(splitTol[1].Length - 1);
                    lowerTol = lowerTol.Replace("-", "");
                }
                else if (splitTol[0].Contains("<$t>"))
                {
                    upperTol = splitTol[0].Remove(0, 4);
                    lowerTol = upperTol;
                }
                else
                {
                    upperTol = tolerance;
                    lowerTol = "";
                }
            }
            catch (System.Exception ex)
            {
                return false;
            }
            return true;
        }

        public static bool GetWorkPartAttribute(Part workPart, out WorkPartAttribute sWorkPartAttribute)
        {
            sWorkPartAttribute = new WorkPartAttribute();
            try
            {
                try { sWorkPartAttribute.meExcelType = workPart.GetStringAttribute("EXCELTYPE"); }
                catch (System.Exception ex) { sWorkPartAttribute.meExcelType = ""; }

                try
                {
                    sWorkPartAttribute.draftingVer = workPart.GetStringAttribute("REVSTARTPOS");
                    sWorkPartAttribute.draftingVer = sWorkPartAttribute.draftingVer.Split(',')[0];
                }
                catch (System.Exception ex) { sWorkPartAttribute.draftingVer = ""; }

                try { sWorkPartAttribute.partDescription = workPart.GetStringAttribute("PARTDESCRIPTIONPOS"); }
                catch (System.Exception ex) { sWorkPartAttribute.partDescription = ""; }

                try { sWorkPartAttribute.draftingDate = workPart.GetStringAttribute("REVDATESTARTPOS"); }
                catch (System.Exception ex) { sWorkPartAttribute.draftingDate = ""; }

                try { sWorkPartAttribute.material = workPart.GetStringAttribute("MATERIALPOS"); }
                catch (System.Exception ex) { sWorkPartAttribute.material = ""; }

                sWorkPartAttribute.createDate = DateTime.Now.ToString();
            }
            catch (System.Exception ex)
            {
                return false;
            }
            return true;
        }

        public static bool RecordDimension(DisplayableObject[] SheetObj, WorkPartAttribute sWorkPartAttribute, ref List<DimensionData> listDimensionData)
        {
            try
            {
                foreach (DisplayableObject singleObj in SheetObj)
                {
                    //取得尺寸的屬性
                    ObjectAttribute sObjectAttribute = new ObjectAttribute();
                    status = GetObjectAttribute(singleObj, out sObjectAttribute);
                    if (!status)
                    {
                        System.Windows.Forms.MessageBox.Show("singleObj屬性取得失敗，無法上傳");
                        return false;
                    }

                    if (sObjectAttribute.singleObjExcel == "" & sObjectAttribute.singleSelfCheckExcel == "")
                    {
                        continue;
                    }

                    //如果有Non-SelfCheck，則記錄一筆資料
                    if (sObjectAttribute.singleObjExcel != "")
                    {
                        string[] splitExcel = sObjectAttribute.singleObjExcel.Split(',');
                        foreach (string dimenExcelType in splitExcel)
                        {
                            DimensionData cDimensionData = new DimensionData();
                            status = GetDimensionData(dimenExcelType, singleObj, out cDimensionData);
                            if (!status)
                            {
                                continue;
                            }
                            cDimensionData.draftingVer = sWorkPartAttribute.draftingVer;
                            cDimensionData.draftingDate = sWorkPartAttribute.draftingDate;
                            cDimensionData.keyChara = sObjectAttribute.keyChara;
                            cDimensionData.productName = sObjectAttribute.productName;
                            cDimensionData.excelType = dimenExcelType;
                            cDimensionData.spcControl = sObjectAttribute.spcControl;
                            if (sObjectAttribute.customerBalloon != "")
                            {
                                cDimensionData.customerBalloon = Convert.ToInt32(sObjectAttribute.customerBalloon);
                            }
                            listDimensionData.Add(cDimensionData);
                        }
                    }
                    //如果有SelfCheck，則記錄一筆資料
                    if (sObjectAttribute.singleSelfCheckExcel != "")
                    {
                        DimensionData cDimensionData = new DimensionData();
                        status = GetDimensionData(sObjectAttribute.singleSelfCheckExcel, singleObj, out cDimensionData);
                        if (!status)
                        {
                            continue;
                        }
                        cDimensionData.draftingVer = sWorkPartAttribute.draftingVer;
                        cDimensionData.draftingDate = sWorkPartAttribute.draftingDate;
                        cDimensionData.keyChara = sObjectAttribute.keyChara;
                        cDimensionData.productName = sObjectAttribute.productName;
                        cDimensionData.excelType = sObjectAttribute.singleSelfCheckExcel;
                        cDimensionData.spcControl = sObjectAttribute.spcControl;
                        if (sObjectAttribute.customerBalloon != "")
                        {
                            cDimensionData.customerBalloon = Convert.ToInt32(sObjectAttribute.customerBalloon);
                        }
                        listDimensionData.Add(cDimensionData);
                    }
                }
            }
            catch (System.Exception ex)
            {
                System.Windows.Forms.MessageBox.Show("singleObj屬性取得失敗，無法上傳");
                return false;
            }
            return true;
        }

        public static bool RecordFixDimension(DisplayableObject[] SheetObj, WorkPartAttribute sWorkPartAttribute, ref List<DimensionData> listDimensionData)
        {
            try
            {
                foreach (DisplayableObject singleObj in SheetObj)
                {
                    //取得尺寸的屬性
                    FixDimenAttr sFixDimenAttr = new FixDimenAttr();
                    status = GetFixDimenAttribute(singleObj, out sFixDimenAttr);
                    if (!status)
                    {
                        System.Windows.Forms.MessageBox.Show("singleObj屬性取得失敗，無法上傳");
                        return false;
                    }

                    if (sFixDimenAttr.FixDimension == "")
                    {
                        continue;
                    }

                  DimensionData cDimensionData = new DimensionData();
                    status = GetFixDimensionData(singleObj, out cDimensionData);
                    if (!status)
                    {
                        continue;
                    }
                    cDimensionData.draftingVer = sWorkPartAttribute.draftingVer;
                    cDimensionData.draftingDate = sWorkPartAttribute.draftingDate;
                    listDimensionData.Add(cDimensionData);
                }
            }
            catch (System.Exception ex)
            {
                System.Windows.Forms.MessageBox.Show("singleObj屬性取得失敗，無法上傳");
                return false;
            }
            return true;
        }

        public static string GetGDTWord(string NXData)
        {
            string ExcelSymbol = "";
            ExcelSymbol = NXData.Replace("LeastMaterialCondition", "l");
            ExcelSymbol = ExcelSymbol.Replace("MaximumMaterialCondition", "m");
            ExcelSymbol = ExcelSymbol.Replace("RegardlessOfFeatureSize", "s");
            ExcelSymbol = ExcelSymbol.Replace("Straightness", "u");
            ExcelSymbol = ExcelSymbol.Replace("Flatness", "c");
            ExcelSymbol = ExcelSymbol.Replace("Circularity", "e");
            ExcelSymbol = ExcelSymbol.Replace("Cylindricity", "g");
            ExcelSymbol = ExcelSymbol.Replace("ProfileOfALine", "k");
            ExcelSymbol = ExcelSymbol.Replace("ProfileOfASurface", "d");
            ExcelSymbol = ExcelSymbol.Replace("Angularity", "a");
            ExcelSymbol = ExcelSymbol.Replace("Perpendicularity", "b");
            ExcelSymbol = ExcelSymbol.Replace("Parallelism", "f");
            ExcelSymbol = ExcelSymbol.Replace("Position", "j");
            ExcelSymbol = ExcelSymbol.Replace("Concentricity", "r");
            ExcelSymbol = ExcelSymbol.Replace("Symmetry", "i");
            ExcelSymbol = ExcelSymbol.Replace("CircularRunout", "h");
            ExcelSymbol = ExcelSymbol.Replace("TotalRunout", "t");
            ExcelSymbol = ExcelSymbol.Replace("Diameter", "n");
            ExcelSymbol = ExcelSymbol.Replace("SphericalDiameter", "Sn");
            ExcelSymbol = ExcelSymbol.Replace("Square", "o");
            ExcelSymbol = ExcelSymbol.Replace("<#C>", "w");
            ExcelSymbol = ExcelSymbol.Replace("<#A>", "X");
            ExcelSymbol = ExcelSymbol.Replace("<#B>", "v");
            ExcelSymbol = ExcelSymbol.Replace("<#D>", "x");
            ExcelSymbol = ExcelSymbol.Replace("<#E>", "y");
            ExcelSymbol = ExcelSymbol.Replace("<#G>", "z");
            ExcelSymbol = ExcelSymbol.Replace("<#F>", "o");
            ExcelSymbol = ExcelSymbol.Replace("~", "-");
            ExcelSymbol = ExcelSymbol.Replace("<$s>", "~");
            ExcelSymbol = ExcelSymbol.Replace("<O>", "n");
            ExcelSymbol = ExcelSymbol.Replace("S<O>", "Sn");
            ExcelSymbol = ExcelSymbol.Replace("&1", "u");
            ExcelSymbol = ExcelSymbol.Replace("&2", "c");
            ExcelSymbol = ExcelSymbol.Replace("&3", "e");
            ExcelSymbol = ExcelSymbol.Replace("&4", "g");
            ExcelSymbol = ExcelSymbol.Replace("&5", "k");
            ExcelSymbol = ExcelSymbol.Replace("&6", "d");
            ExcelSymbol = ExcelSymbol.Replace("&7", "a");
            ExcelSymbol = ExcelSymbol.Replace("&8", "b");
            ExcelSymbol = ExcelSymbol.Replace("&9", "f");
            ExcelSymbol = ExcelSymbol.Replace("&10", "j");
            ExcelSymbol = ExcelSymbol.Replace("&11", "r");
            ExcelSymbol = ExcelSymbol.Replace("&12", "i");
            ExcelSymbol = ExcelSymbol.Replace("&13", "h");
            ExcelSymbol = ExcelSymbol.Replace("&15", "t");
            ExcelSymbol = ExcelSymbol.Replace("<M>", "m");
            ExcelSymbol = ExcelSymbol.Replace("<E>", "l");
            ExcelSymbol = ExcelSymbol.Replace("<S>", "s");
            ExcelSymbol = ExcelSymbol.Replace("<P>", "p");
            ExcelSymbol = ExcelSymbol.Replace("<$t>", "±");
            /*
            if (NXData == "LeastMaterialCondition")
            {
                ExcelSymbol = "l";
            }
            else if (NXData == "MaximumMaterialCondition")
            {
                ExcelSymbol = "m";
            }
            else if (NXData == "RegardlessOfFeatureSize")
            {
                ExcelSymbol = "s";
            }
            else if (NXData == "Straightness")
            {
                ExcelSymbol = "u";
            }
            else if (NXData == "Flatness")
            {
                ExcelSymbol = "c";
            }
            else if (NXData == "Circularity")
            {
                ExcelSymbol = "e";
            }
            else if (NXData == "Cylindricity")
            {
                ExcelSymbol = "g";
            }
            else if (NXData == "ProfileOfALine")
            {
                ExcelSymbol = "k";
            }
            else if (NXData == "ProfileOfASurface")
            {
                ExcelSymbol = "d";
            }
            else if (NXData == "Angularity")
            {
                ExcelSymbol = "a";
            }
            else if (NXData == "Perpendicularity")
            {
                ExcelSymbol = "b";
            }
            else if (NXData == "Parallelism")
            {
                ExcelSymbol = "f";
            }
            else if (NXData == "Position")
            {
                ExcelSymbol = "j";
            }
            else if (NXData == "Concentricity")
            {
                ExcelSymbol = "r";
            }
            else if (NXData == "Symmetry")
            {
                ExcelSymbol = "i";
            }
            else if (NXData == "CircularRunout")
            {
                ExcelSymbol = "h";
            }
            else if (NXData == "TotalRunout")
            {
                ExcelSymbol = "t";
            }
            else if (NXData == "Diameter")
            {
                ExcelSymbol = "n";
            }
            else if (NXData == "SphericalDiameter")
            {
                ExcelSymbol = "Sn";
            }
            else if (NXData == "Square")
            {
                ExcelSymbol = "o";
            }
            else
            {
                ExcelSymbol = NXData.Replace("LeastMaterialCondition", "l");
                ExcelSymbol = ExcelSymbol.Replace("MaximumMaterialCondition", "m");
                ExcelSymbol = ExcelSymbol.Replace("RegardlessOfFeatureSize", "s");
                ExcelSymbol = ExcelSymbol.Replace("Straightness", "u");
                ExcelSymbol = ExcelSymbol.Replace("Flatness", "c");
                ExcelSymbol = ExcelSymbol.Replace("Circularity", "e");
                ExcelSymbol = ExcelSymbol.Replace("Cylindricity", "g");
                ExcelSymbol = ExcelSymbol.Replace("ProfileOfALine", "k");
                ExcelSymbol = ExcelSymbol.Replace("ProfileOfASurface", "d");
                ExcelSymbol = ExcelSymbol.Replace("Angularity", "a");
                ExcelSymbol = ExcelSymbol.Replace("Perpendicularity", "b");
                ExcelSymbol = ExcelSymbol.Replace("Parallelism", "f");
                ExcelSymbol = ExcelSymbol.Replace("Position", "j");
                ExcelSymbol = ExcelSymbol.Replace("Concentricity", "r");
                ExcelSymbol = ExcelSymbol.Replace("Symmetry", "i");
                ExcelSymbol = ExcelSymbol.Replace("CircularRunout", "h");
                ExcelSymbol = ExcelSymbol.Replace("TotalRunout", "t");
                ExcelSymbol = ExcelSymbol.Replace("Diameter", "n");
                ExcelSymbol = ExcelSymbol.Replace("SphericalDiameter", "Sn");
                ExcelSymbol = ExcelSymbol.Replace("Square", "o");
                ExcelSymbol = ExcelSymbol.Replace("<#C>", "w");
                ExcelSymbol = ExcelSymbol.Replace("<#A>", "X");
                ExcelSymbol = ExcelSymbol.Replace("<#B>", "v");
                ExcelSymbol = ExcelSymbol.Replace("<#D>", "x");
                ExcelSymbol = ExcelSymbol.Replace("<#E>", "y");
                ExcelSymbol = ExcelSymbol.Replace("<#G>", "z");
                ExcelSymbol = ExcelSymbol.Replace("<#F>", "o");
                ExcelSymbol = ExcelSymbol.Replace("~", "-");
                ExcelSymbol = ExcelSymbol.Replace("<$s>", "~");
                ExcelSymbol = ExcelSymbol.Replace("<O>", "n");
                ExcelSymbol = ExcelSymbol.Replace("S<O>", "Sn");
                ExcelSymbol = ExcelSymbol.Replace("&1", "u");
                ExcelSymbol = ExcelSymbol.Replace("&2", "c");
                ExcelSymbol = ExcelSymbol.Replace("&3", "e");
                ExcelSymbol = ExcelSymbol.Replace("&4", "g");
                ExcelSymbol = ExcelSymbol.Replace("&5", "k");
                ExcelSymbol = ExcelSymbol.Replace("&6", "d");
                ExcelSymbol = ExcelSymbol.Replace("&7", "a");
                ExcelSymbol = ExcelSymbol.Replace("&8", "b");
                ExcelSymbol = ExcelSymbol.Replace("&9", "f");
                ExcelSymbol = ExcelSymbol.Replace("&10", "j");
                ExcelSymbol = ExcelSymbol.Replace("&11", "r");
                ExcelSymbol = ExcelSymbol.Replace("&12", "i");
                ExcelSymbol = ExcelSymbol.Replace("&13", "h");
                ExcelSymbol = ExcelSymbol.Replace("&15", "t");
                ExcelSymbol = ExcelSymbol.Replace("<M>", "m");
                ExcelSymbol = ExcelSymbol.Replace("<E>", "l");
                ExcelSymbol = ExcelSymbol.Replace("<S>", "s");
                ExcelSymbol = ExcelSymbol.Replace("<P>", "p");
                ExcelSymbol = ExcelSymbol.Replace("<$t>", "±");
            }
            */
            return ExcelSymbol;
        }

        public static bool MappingData(DimensionData input, ref Com_FixDimension cCom_FixDimension)
        {
            try
            {
                string renewStr = "";
                if (input.characteristic != null)
                {
                    cCom_FixDimension.characteristic = GetGDTWord(input.characteristic);
                }
                if (input.zoneShape != null)
                {
                    cCom_FixDimension.zoneShape = GetGDTWord(input.zoneShape);
                }
                if (input.toleranceValue != null)
                {
                    cCom_FixDimension.toleranceValue = input.toleranceValue;
                }
                if (input.materialModifier != null)
                {
                    cCom_FixDimension.materialModifier = GetGDTWord(input.materialModifier);
                }
                if (input.primaryDatum != null)
                {
                    cCom_FixDimension.primaryDatum = input.primaryDatum;
                }
                if (input.primaryMaterialModifier != null)
                {
                    cCom_FixDimension.primaryMaterialModifier = GetGDTWord(input.primaryMaterialModifier);
                }
                if (input.secondaryDatum != null)
                {
                    cCom_FixDimension.secondaryDatum = input.secondaryDatum;
                }
                if (input.secondaryMaterialModifier != null)
                {
                    cCom_FixDimension.secondaryMaterialModifier = GetGDTWord(input.secondaryMaterialModifier);
                }
                if (input.tertiaryDatum != null)
                {
                    cCom_FixDimension.tertiaryDatum = input.tertiaryDatum;
                }
                if (input.tertiaryMaterialModifier != null)
                {
                    cCom_FixDimension.tertiaryMaterialModifier = GetGDTWord(input.tertiaryMaterialModifier);
                }
                if (input.aboveText != null)
                {
                    renewStr = GetGDTWord(input.aboveText);
                    ModifyText(ref renewStr);
                    cCom_FixDimension.aboveText = renewStr;
                }
                if (input.belowText != null)
                {
                    renewStr = GetGDTWord(input.belowText);
                    ModifyText(ref renewStr);
                    cCom_FixDimension.belowText = renewStr;
                }
                if (input.beforeText != null)
                {
                    renewStr = GetGDTWord(input.beforeText);
                    ModifyText(ref renewStr);
                    cCom_FixDimension.beforeText = renewStr;
                }
                if (input.afterText != null)
                {
                    renewStr = GetGDTWord(input.afterText);
                    ModifyText(ref renewStr);
                    cCom_FixDimension.afterText = renewStr;
                }
                if (input.toleranceSymbol != null)
                {
                    renewStr = GetGDTWord(input.toleranceSymbol);
                    ModifyText(ref renewStr);
                    cCom_FixDimension.toleranceSymbol = renewStr;
                }
                if (input.mainText != null)
                {
                    renewStr = GetGDTWord(input.mainText);
                    ModifyText(ref renewStr);
                    cCom_FixDimension.mainText = renewStr;
                }
                if (input.upperTol != null)
                {
                    renewStr = GetGDTWord(input.upperTol);
                    ModifyText(ref renewStr);
                    cCom_FixDimension.upTolerance = renewStr;
                }
                if (input.lowerTol != null)
                {
                    renewStr = GetGDTWord(input.lowerTol);
                    ModifyText(ref renewStr);
                    cCom_FixDimension.lowTolerance = renewStr;
                }
                if (input.x != null)
                {
                    cCom_FixDimension.x = input.x;
                }
                if (input.chamferAngle != null)
                {
                    renewStr = GetGDTWord(input.chamferAngle);
                    ModifyText(ref renewStr);
                    cCom_FixDimension.chamferAngle = renewStr;
                }
                if (input.maxTolerance != null)
                {
                    cCom_FixDimension.maxTolerance = input.maxTolerance;
                }
                if (input.minTolerance != null)
                {
                    cCom_FixDimension.minTolerance = input.minTolerance;
                }
                if (input.balloonCount != null)
                {
                    cCom_FixDimension.balloonCount = input.balloonCount;
                }
                cCom_FixDimension.draftingVer = input.draftingVer;
                cCom_FixDimension.draftingDate = input.draftingDate;
                cCom_FixDimension.ballon = input.ballonNum;
                cCom_FixDimension.location = input.location;
                cCom_FixDimension.frequency = input.frequency;
                cCom_FixDimension.instrument = input.instrument;
                cCom_FixDimension.excelType = input.excelType;
                cCom_FixDimension.keyChara = input.keyChara;
                cCom_FixDimension.productName = input.productName;
                cCom_FixDimension.customerBalloon = input.customerBalloon;
                cCom_FixDimension.spcControl = input.spcControl;
                cCom_FixDimension.size = input.size;
                cCom_FixDimension.freq = input.freq;
                cCom_FixDimension.selfCheck_Size = input.selfCheck_Size;
                cCom_FixDimension.selfCheck_Freq = input.selfCheck_Freq;
                cCom_FixDimension.checkLevel = input.checkLevel;
                /*
                if (input.type == "NXOpen.Annotations.DraftingFcf")
                {
                    cCom_Dimension.dimensionType = input.type;
                    #region DraftingFcf
                    if (input.characteristic != "")
                    {
                        cCom_Dimension.characteristic = GetCharacteristicSymbol(input.characteristic);
                    }
                    if (input.zoneShape != "")
                    {
                        cCom_Dimension.zoneShape = GetZoneShapeSymbol(input.zoneShape);
                    }
                    if (input.toleranceValue != "")
                    {
                        cCom_Dimension.toleranceValue = input.toleranceValue;
                    }
                    if (input.materialModifier != "")
                    {
                        cCom_Dimension.materialModifer = GetMaterialModifier(input.materialModifier);
                    }
                    if (input.primaryDatum != "")
                    {
                        cCom_Dimension.primaryDatum = input.primaryDatum;
                    }
                    if (input.primaryMaterialModifier != "")
                    {
                        cCom_Dimension.primaryMaterialModifier = GetMaterialModifier(input.primaryMaterialModifier);
                    }
                    if (input.secondaryDatum != "")
                    {
                        cCom_Dimension.secondaryDatum = input.secondaryDatum;
                    }
                    if (input.secondaryMaterialModifier != "")
                    {
                        cCom_Dimension.secondaryMaterialModifier = GetMaterialModifier(input.secondaryMaterialModifier);
                    }
                    if (input.tertiaryDatum != "")
                    {
                        cCom_Dimension.tertiaryDatum = input.tertiaryDatum;
                    }
                    if (input.tertiaryMaterialModifier != "")
                    {
                        cCom_Dimension.tertiaryMaterialModifier = GetMaterialModifier(input.tertiaryMaterialModifier);
                    }
                    #endregion
                }
                else if (input.type == "NXOpen.Annotations.Label")
                {
                    cCom_Dimension.dimensionType = input.type;
                    #region Label
                    if (input.mainText != "")
                    {
                        cCom_Dimension.mainText = input.mainText;
                    }
                    #endregion
                }
                else
                {
                    cCom_Dimension.dimensionType = input.type;
                    #region Dimension
                    if (input.beforeText != null)
                    {
                        cCom_Dimension.beforeText = GetGDTWord(input.beforeText);
                    }
                    if (input.mainText != "")
                    {
                        cCom_Dimension.mainText = GetGDTWord(input.mainText);
                    }
                    if (input.upperTol != "")
                    {
                        Database.GetTolerance(input, ref cCom_Dimension);
                    }
                    #endregion
                }
                */
            }
            catch (System.Exception ex)
            {
                return false;
            }
            return true;
        }

        public static bool MappingData(DimensionData input, ref Com_Dimension cCom_Dimension)
        {
            try
            {
                string renewStr = "";
                if (input.characteristic != null)
                {
                    cCom_Dimension.characteristic = GetGDTWord(input.characteristic);
                }
                if (input.zoneShape != null)
                {
                    cCom_Dimension.zoneShape = GetGDTWord(input.zoneShape);
                }
                if (input.toleranceValue != null)
                {
                    cCom_Dimension.toleranceValue = input.toleranceValue;
                }
                if (input.materialModifier != null)
                {
                    cCom_Dimension.materialModifier = GetGDTWord(input.materialModifier);
                }
                if (input.primaryDatum != null)
                {
                    cCom_Dimension.primaryDatum = input.primaryDatum;
                }
                if (input.primaryMaterialModifier != null)
                {
                    cCom_Dimension.primaryMaterialModifier = GetGDTWord(input.primaryMaterialModifier);
                }
                if (input.secondaryDatum != null)
                {
                    cCom_Dimension.secondaryDatum = input.secondaryDatum;
                }
                if (input.secondaryMaterialModifier != null)
                {
                    cCom_Dimension.secondaryMaterialModifier = GetGDTWord(input.secondaryMaterialModifier);
                }
                if (input.tertiaryDatum != null)
                {
                    cCom_Dimension.tertiaryDatum = input.tertiaryDatum;
                }
                if (input.tertiaryMaterialModifier != null)
                {
                    cCom_Dimension.tertiaryMaterialModifier = GetGDTWord(input.tertiaryMaterialModifier);
                }
                if (input.aboveText != null)
                {
                    renewStr = GetGDTWord(input.aboveText);
                    ModifyText(ref renewStr);
                    cCom_Dimension.aboveText = renewStr;
                }
                if (input.belowText != null)
                {
                    renewStr = GetGDTWord(input.belowText);
                    ModifyText(ref renewStr);
                    cCom_Dimension.belowText = renewStr;
                }
                if (input.beforeText != null)
                {
                    renewStr = GetGDTWord(input.beforeText);
                    ModifyText(ref renewStr);
                    cCom_Dimension.beforeText = renewStr;
                }
                if (input.afterText != null)
                {
                    renewStr = GetGDTWord(input.afterText);
                    ModifyText(ref renewStr);
                    cCom_Dimension.afterText = renewStr;
                }
                if (input.toleranceSymbol != null)
                {
                    renewStr = GetGDTWord(input.toleranceSymbol);
                    ModifyText(ref renewStr);
                    cCom_Dimension.toleranceSymbol = renewStr;
                }
                if (input.mainText != null)
                {
                    renewStr = GetGDTWord(input.mainText);
                    ModifyText(ref renewStr);
                    cCom_Dimension.mainText = renewStr;
                }
                if (input.upperTol != null)
                {
                    renewStr = GetGDTWord(input.upperTol);
                    ModifyText(ref renewStr);
                    cCom_Dimension.upTolerance = renewStr;
                }
                if (input.lowerTol != null)
                {
                    renewStr = GetGDTWord(input.lowerTol);
                    ModifyText(ref renewStr);
                    cCom_Dimension.lowTolerance = renewStr;
                }
                if (input.x != null)
                {
                    cCom_Dimension.x = input.x;
                }
                if (input.chamferAngle != null)
                {
                    renewStr = GetGDTWord(input.chamferAngle);
                    ModifyText(ref renewStr);
                    cCom_Dimension.chamferAngle = renewStr;
                }
                if (input.maxTolerance != null)
                {
                    cCom_Dimension.maxTolerance = input.maxTolerance;
                }
                if (input.minTolerance != null)
                {
                    cCom_Dimension.minTolerance = input.minTolerance;
                }
                if (input.balloonCount != null)
                {
                    cCom_Dimension.balloonCount = input.balloonCount;
                }
                cCom_Dimension.draftingVer = input.draftingVer;
                cCom_Dimension.draftingDate = input.draftingDate;
                cCom_Dimension.ballon = input.ballonNum;
                cCom_Dimension.location = input.location;
                cCom_Dimension.frequency = input.frequency;
                cCom_Dimension.toleranceType = input.tolType;
                cCom_Dimension.instrument = input.instrument;
                cCom_Dimension.excelType = input.excelType;
                cCom_Dimension.keyChara = input.keyChara;
                cCom_Dimension.productName = input.productName;
                cCom_Dimension.customerBalloon = input.customerBalloon;
                cCom_Dimension.spcControl = input.spcControl;
                cCom_Dimension.size = input.size;
                cCom_Dimension.freq = input.freq;
                cCom_Dimension.selfCheck_Size = input.selfCheck_Size;
                cCom_Dimension.selfCheck_Freq = input.selfCheck_Freq;
                cCom_Dimension.checkLevel = input.checkLevel;

            }
            catch (System.Exception ex)
            {
                return false;
            }
            return true;
        }
        
        public static void ModifyText(ref string NXData)
        {
            try
            {
                int startIndes, endIndex;
                startIndes = NXData.IndexOf("<");
                endIndex = NXData.IndexOf(">");
                if (startIndes != -1)
                {
                    NXData = NXData.Remove(startIndes, endIndex - startIndes + 1);
                    ModifyText(ref NXData);
                }
            }
            catch (System.Exception ex)
            {

            }
        }   

        public static bool InsertDicountNote(string balloonNum, string attrStr, string text, string FontSize, Point3d textLocation, string RevCount = "", NXOpen.Annotations.OriginBuilder.AlignmentPosition defult = NXOpen.Annotations.OriginBuilder.AlignmentPosition.MidCenter)
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
                draftingNoteBuilder1.Origin.Anchor = defult;


                draftingNoteBuilder1.Style.LetteringStyle.GeneralTextSize = Convert.ToDouble(FontSize);

                //text文字
                int a = workPart.Fonts.AddFont("chineset");
                draftingNoteBuilder1.Style.LetteringStyle.GeneralTextFont = a;

                string[] text1;
                if (attrStr == TablePosi.MaterialPos)
                {
                    string[] splitText = text.Split(' ');
                    text1 = new string[splitText.Length];
                    for (int i = 0; i < splitText.Length; i++)
                    {
                        text1[i] = splitText[i];
                        //text1[i] = "<F2>" + text[i] + "<F>";
                    }
                }
                else
                {
                    text1 = new string[1];
                    text1[0] = text;
                }
                //string[] text1 = new string[1];
                //text1[0] = "<C2>" + text + "<C>";
                //text1[0] = "<F2>" + text + "<F>";

                draftingNoteBuilder1.Text.TextBlock.SetText(text1);

                draftingNoteBuilder1.Style.LetteringStyle.GeneralTextSize = Convert.ToDouble(FontSize);


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
                NXOpen.Session.UndoMarkId markId2;
                markId2 = theSession.SetUndoMark(NXOpen.Session.MarkVisibility.Invisible, "Note");
                NXObject nXObject1;
                nXObject1 = draftingNoteBuilder1.Commit();
                //塞屬性，以利重複執行時抓取資料，加入RevCount是給製圖版次、製圖日期、說明  使用
                if (RevCount != "")
                {
                    text = text + "," + RevCount;
                }
                nXObject1.SetAttribute(attrStr, text);
                nXObject1.SetAttribute(CaxME.DimenAttr.BallonNum, balloonNum);
                theSession.DeleteUndoMark(markId2, null);
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
    }
}
