using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using NXOpen;
using CaxGlobaltek;
using NXOpen.UF;
using System.Windows.Forms;

namespace MEUpload.DatabaseClass
{
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

    public class MEMainData
    {
        public string createDate { get; set; }
    }

    public class Database
    {
        public static Session theSession = Session.GetSession();
        public static UI theUI;
        public static UFSession theUfSession = UFSession.GetUFSession();
        public static Dictionary<MEMainData, DimensionData> DicDimenData = new Dictionary<MEMainData, DimensionData>();
        //public static List<CaxME.DimensionData> listDimensionData = new List<CaxME.DimensionData>();
        public static Part workPart = theSession.Parts.Work;
        public static Part displayPart = theSession.Parts.Display;

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
                            SplitMainText(text,out mainText);
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
                            SplitTolerance(text,out upperTol,out lowerTol);
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
        public static bool GetDimensionData(string dimenExcelType, DisplayableObject singleObj, out DimensionData cDimensionData)
        {
            cDimensionData = new DimensionData();
            try
            {

                //string singleObjType = singleObj.GetType().ToString();

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
                        if (cycle != 0)
                        {
                            for (ii = 1; ii <= num_segments; ii++)
                            {
                                string cr3;
                                theUfSession.Drf.AskTextData(ii, ref ann_data[0], out cr3, out size, out length);
                                MappingDimensionData(ann_data_type, ann_data_form, cr3, ref cDimensionData);
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
                                foreach (KeyValuePair<double,double> kvp in DicTolRegion)
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
                if (SplitValue.Length == 1)
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

        public static string GetCharacteristicSymbol(string NXData)
        {
            string ExcelSymbol = "";

            if (NXData == "Straightness")
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
            else if (NXData == "Profile of a Line")
            {
                ExcelSymbol = "k";
            }
            else if (NXData == "Profile of a Surface")
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
            else if (NXData == "Circular Runout")
            {
                ExcelSymbol = "h";
            }
            else if (NXData == "Total Runout")
            {
                ExcelSymbol = "t";
            }

            return ExcelSymbol;
        }

        public static string GetZoneShapeSymbol(string NXData)
        {
            string ExcelSymbol = "";
            if (NXData == "Diameter")
            {
                ExcelSymbol = "n";
            }
            return ExcelSymbol;
        }

        public static string GetGDTWord(string NXData)
        {
            string ExcelSymbol = "";

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
            else if (NXData == "Profile of a Line")
            {
                ExcelSymbol = "k";
            }
            else if (NXData == "Profile of a Surface")
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
            else if (NXData == "Total Runout")
            {
                ExcelSymbol = "t";
            }
            else if (NXData == "Diameter")
            {
                ExcelSymbol = "n";
            }
            else
            {
                ExcelSymbol = NXData.Replace("<#C>", "w");
                ExcelSymbol = ExcelSymbol.Replace("<#B>", "v");
                ExcelSymbol = ExcelSymbol.Replace("<#D>", "x");
                ExcelSymbol = ExcelSymbol.Replace("<#E>", "y");
                ExcelSymbol = ExcelSymbol.Replace("<#G>", "z");
                ExcelSymbol = ExcelSymbol.Replace("<#F>", "o");
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
            return ExcelSymbol;
        }

        public static string GetMaterialModifier(string Value)
        {
            string ValueStr = "";
            if (Value != "" & Value != "None")
            {
                if (Value == "LeastMaterialCondition")
                {
                    ValueStr = "l";
                }
                else if (Value == "MaximumMaterialCondition")
                {
                    ValueStr = "m";
                }
                else if (Value == "RegardlessOfFeatureSize")
                {
                    ValueStr = "s";
                }
            }
            return ValueStr;
        }

        public static bool GetTolerance(DimensionData cDimensionData, ref Com_Dimension cCom_Dimension)
        {
            try
            {
                if (cDimensionData.upperTol != "" & cDimensionData.tolType == "BilateralOneLine")
                {
                    cCom_Dimension.toleranceType = cDimensionData.tolType;
                    cCom_Dimension.upTolerance = cDimensionData.upperTol;
                    cCom_Dimension.lowTolerance = cDimensionData.lowerTol;
                }
                else if (cDimensionData.upperTol != "" & cDimensionData.tolType == "BilateralTwoLines")
                {
                    cCom_Dimension.toleranceType = cDimensionData.tolType;
                    cCom_Dimension.upTolerance = cDimensionData.upperTol;
                    cCom_Dimension.lowTolerance = cDimensionData.lowerTol;
                }
                else if (cDimensionData.upperTol != "" & cDimensionData.tolType == "UnilateralAbove")
                {
                    cCom_Dimension.toleranceType = cDimensionData.tolType;
                    cCom_Dimension.upTolerance = cDimensionData.upperTol;
                    cCom_Dimension.lowTolerance = "0";
                }
                else if (cDimensionData.upperTol != "" & cDimensionData.tolType == "UnilateralBelow")
                {
                    cCom_Dimension.toleranceType = cDimensionData.tolType;
                    cCom_Dimension.upTolerance = "0";
                    cCom_Dimension.lowTolerance = cDimensionData.lowerTol;
                }
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
                //MessageBox.Show(ex.ToString());
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
    }
}
