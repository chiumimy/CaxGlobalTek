using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using NXOpen;
using NXOpen.UF;
using NXOpen.Utilities;
using NXOpen.CAM;
using WorldChampion;
using WorldChampion.MRL;

namespace CaxGlobaltek
{
    public class CaxCAM
    {
        public static Session theSession = Session.GetSession();
        public static UI theUI;
        public static UFSession theUfSession = UFSession.GetUFSession();
        public static Part workPart = theSession.Parts.Work;
        public static Part displayPart = theSession.Parts.Display;

        public static List<InsertSize> insertSizeConfig
        {
            get
            {
                List<InsertSize> temp = new List<InsertSize>();

                temp.Add(new InsertSize("C", "03", "3.97"));
                temp.Add(new InsertSize("C", "04", "4.76"));
                temp.Add(new InsertSize("C", "05", "5.56"));
                temp.Add(new InsertSize("C", "06", "6.35"));
                temp.Add(new InsertSize("C", "08", "7.94"));
                temp.Add(new InsertSize("C", "09", "9.525"));
                temp.Add(new InsertSize("C", "11", "11.11"));
                temp.Add(new InsertSize("C", "12", "12.7"));
                temp.Add(new InsertSize("C", "14", "14.29"));
                temp.Add(new InsertSize("C", "16", "15.875"));
                temp.Add(new InsertSize("C", "17", "17.46"));
                temp.Add(new InsertSize("C", "19", "19.05"));
                temp.Add(new InsertSize("C", "22", "22.225"));
                temp.Add(new InsertSize("C", "25", "25.4"));
                temp.Add(new InsertSize("C", "32", "31.75"));

                temp.Add(new InsertSize("D", "04", "3.97"));
                temp.Add(new InsertSize("D", "05", "4.76"));
                temp.Add(new InsertSize("D", "06", "5.56"));
                temp.Add(new InsertSize("D", "07", "6.35"));
                temp.Add(new InsertSize("D", "09", "7.94"));
                temp.Add(new InsertSize("D", "11", "9.525"));
                temp.Add(new InsertSize("D", "12", "10.0")); //KYOCERA
                temp.Add(new InsertSize("D", "13", "11.11"));
                temp.Add(new InsertSize("D", "15", "12.7"));
                temp.Add(new InsertSize("D", "17", "14.29"));
                temp.Add(new InsertSize("D", "19", "15.875"));
                temp.Add(new InsertSize("D", "21", "17.46"));
                temp.Add(new InsertSize("D", "23", "19.05"));
                temp.Add(new InsertSize("D", "27", "22.225"));
                temp.Add(new InsertSize("D", "31", "25.4"));
                temp.Add(new InsertSize("D", "38", "31.75"));

                temp.Add(new InsertSize("S", "03", "3.97"));
                temp.Add(new InsertSize("S", "04", "4.76"));
                temp.Add(new InsertSize("S", "05", "5.56"));
                temp.Add(new InsertSize("S", "06", "6.35"));
                temp.Add(new InsertSize("S", "07", "7.94"));
                temp.Add(new InsertSize("S", "09", "9.525"));
                temp.Add(new InsertSize("S", "11", "11.11"));
                temp.Add(new InsertSize("S", "12", "12.7"));
                temp.Add(new InsertSize("S", "14", "14.29"));
                temp.Add(new InsertSize("S", "15", "15.875"));
                temp.Add(new InsertSize("S", "17", "17.46"));
                temp.Add(new InsertSize("S", "19", "19.05"));
                temp.Add(new InsertSize("S", "22", "22.225"));
                temp.Add(new InsertSize("S", "25", "25.4"));
                temp.Add(new InsertSize("S", "31", "31.75"));

                temp.Add(new InsertSize("T", "06", "3.97"));
                temp.Add(new InsertSize("T", "08", "4.76"));
                temp.Add(new InsertSize("T", "09", "5.56"));
                temp.Add(new InsertSize("T", "11", "6.35"));
                temp.Add(new InsertSize("T", "13", "7.94"));
                temp.Add(new InsertSize("T", "16", "9.525"));
                temp.Add(new InsertSize("T", "19", "11.11"));
                temp.Add(new InsertSize("T", "22", "12.7"));
                temp.Add(new InsertSize("T", "24", "14.29"));
                temp.Add(new InsertSize("T", "27", "15.875"));
                temp.Add(new InsertSize("T", "30", "17.46"));
                temp.Add(new InsertSize("T", "33", "19.05"));
                temp.Add(new InsertSize("T", "38", "22.225"));
                temp.Add(new InsertSize("T", "44", "25.4"));
                temp.Add(new InsertSize("T", "54", "31.75"));

                temp.Add(new InsertSize("R", "03", "3.97"));
                temp.Add(new InsertSize("R", "04", "4.76"));
                temp.Add(new InsertSize("R", "05", "5.56"));
                temp.Add(new InsertSize("R", "06", "6.35"));
                temp.Add(new InsertSize("R", "07", "7.94"));
                temp.Add(new InsertSize("R", "08", "8.00"));
                temp.Add(new InsertSize("R", "09", "9.525"));
                temp.Add(new InsertSize("R", "10", "10.00"));
                temp.Add(new InsertSize("R", "11", "11.11"));
                temp.Add(new InsertSize("R", "12", "12.7"));
                temp.Add(new InsertSize("R", "14", "14.29"));
                temp.Add(new InsertSize("R", "15", "15.875"));
                temp.Add(new InsertSize("R", "16", "16.00"));
                temp.Add(new InsertSize("R", "17", "17.46"));
                temp.Add(new InsertSize("R", "19", "19.05"));
                temp.Add(new InsertSize("R", "20", "20.00"));
                temp.Add(new InsertSize("R", "22", "22.225"));
                temp.Add(new InsertSize("R", "25", "25.40"));
                temp.Add(new InsertSize("R", "31", "31.75"));
                temp.Add(new InsertSize("R", "32", "32.0"));

                temp.Add(new InsertSize("V", "08", "4.76"));
                temp.Add(new InsertSize("V", "09", "5.56"));
                temp.Add(new InsertSize("V", "11", "6.35"));
                temp.Add(new InsertSize("V", "13", "7.94"));
                temp.Add(new InsertSize("V", "16", "9.525"));
                temp.Add(new InsertSize("V", "19", "11.11"));
                temp.Add(new InsertSize("V", "22", "12.7"));
                temp.Add(new InsertSize("V", "24", "14.29"));
                temp.Add(new InsertSize("V", "27", "15.875"));
                temp.Add(new InsertSize("V", "30", "17.46"));
                temp.Add(new InsertSize("V", "33", "19.05"));
                temp.Add(new InsertSize("V", "38", "22.225"));
                temp.Add(new InsertSize("V", "44", "25.4"));
                temp.Add(new InsertSize("V", "54", "31.75"));

                temp.Add(new InsertSize("W", "02", "3.97"));
                temp.Add(new InsertSize("W", "S3", "4.76"));
                temp.Add(new InsertSize("W", "L3", "4.76"));
                temp.Add(new InsertSize("W", "03", "5.56"));
                temp.Add(new InsertSize("W", "04", "6.35"));
                temp.Add(new InsertSize("W", "05", "7.94"));
                temp.Add(new InsertSize("W", "06", "9.525"));
                temp.Add(new InsertSize("W", "07", "11.11"));
                temp.Add(new InsertSize("W", "08", "12.7"));
                temp.Add(new InsertSize("W", "09", "14.29"));
                temp.Add(new InsertSize("W", "10", "15.875"));
                temp.Add(new InsertSize("W", "11", "17.46"));
                temp.Add(new InsertSize("W", "13", "19.05"));
                temp.Add(new InsertSize("W", "15", "22.225"));
                temp.Add(new InsertSize("W", "17", "25.4"));
                temp.Add(new InsertSize("W", "21", "31.75"));

                return temp;
            }
        }

        public static List<InsertOrientAngle> insertOrientAngleConfig
        {
            get
            {
                List<InsertOrientAngle> tempList = new List<InsertOrientAngle>();

                //BS
                tempList.Add(new InsertOrientAngle("B", "S", "53"));
                //CA CB CD CF CG CK CL CM CN CR
                tempList.Add(new InsertOrientAngle("C", "A", "10"));
                tempList.Add(new InsertOrientAngle("C", "B", "25"));
                tempList.Add(new InsertOrientAngle("C", "D", "55"));
                tempList.Add(new InsertOrientAngle("C", "F", "0"));
                tempList.Add(new InsertOrientAngle("C", "G", "10"));
                tempList.Add(new InsertOrientAngle("C", "K", "345"));
                tempList.Add(new InsertOrientAngle("C", "L", "5"));
                tempList.Add(new InsertOrientAngle("C", "M", "50"));
                tempList.Add(new InsertOrientAngle("C", "N", "37"));
                tempList.Add(new InsertOrientAngle("C", "R", "25"));
                //DA DJ DH(X) DN DQ 
                tempList.Add(new InsertOrientAngle("D", "A", "35"));
                tempList.Add(new InsertOrientAngle("D", "J", "32"));
                tempList.Add(new InsertOrientAngle("D", "N", "62"));
                tempList.Add(new InsertOrientAngle("D", "Q", "17.5"));
                //EL(X)
                //KJ KN 
                tempList.Add(new InsertOrientAngle("K", "J", "32"));
                tempList.Add(new InsertOrientAngle("K", "N", "62"));
                //LH(X)
                //SB SD SK SR SS ST SY(X)
                tempList.Add(new InsertOrientAngle("S", "B", "15"));
                tempList.Add(new InsertOrientAngle("S", "D", "45"));
                tempList.Add(new InsertOrientAngle("S", "K", "345"));
                tempList.Add(new InsertOrientAngle("S", "R", "15"));
                tempList.Add(new InsertOrientAngle("S", "S", "45"));
                tempList.Add(new InsertOrientAngle("S", "T", "30"));
                //TA TB TD TE TF TG TJ  TK TL(X) TN TT TU TX(X)
                tempList.Add(new InsertOrientAngle("T", "A", "30"));
                tempList.Add(new InsertOrientAngle("T", "B", "45"));
                tempList.Add(new InsertOrientAngle("T", "D", "75"));
                tempList.Add(new InsertOrientAngle("T", "E", "60"));
                tempList.Add(new InsertOrientAngle("T", "F", "0"));
                tempList.Add(new InsertOrientAngle("T", "G", "30"));
                tempList.Add(new InsertOrientAngle("T", "J", "27"));
                tempList.Add(new InsertOrientAngle("T", "K", "345"));
                tempList.Add(new InsertOrientAngle("T", "N", "57"));
                tempList.Add(new InsertOrientAngle("T", "T", "30"));
                tempList.Add(new InsertOrientAngle("T", "U", "3"));
                //VA VH VJ VL(X) VN(X) VP(X) VQ(X) VU VV
                tempList.Add(new InsertOrientAngle("V", "A", "55"));
                tempList.Add(new InsertOrientAngle("V", "H", "72.5"));
                tempList.Add(new InsertOrientAngle("V", "J", "52"));
                tempList.Add(new InsertOrientAngle("V", "U", "3"));
                tempList.Add(new InsertOrientAngle("V", "V", "72.5"));
                //WA WL 
                tempList.Add(new InsertOrientAngle("W", "A", "10"));
                tempList.Add(new InsertOrientAngle("W", "L", "5"));

                //RA RD RG RX RS 
                //XZ
                return tempList;
            }
        }

        public static List<NameValuePair> insertSsConfig
        {
            get
            {
                List<NameValuePair> tempList = new List<NameValuePair>();
                tempList.Add(new NameValuePair("06", "4.2"));
                tempList.Add(new NameValuePair("09", "6.3"));
                tempList.Add(new NameValuePair("12", "8.3"));
                tempList.Add(new NameValuePair("15", "10.2"));
                tempList.Add(new NameValuePair("19", "12.5"));
                tempList.Add(new NameValuePair("25", "16"));
                return tempList;
            }
        }

        //建立車刀刀具(給各個參數)>>>>>>>>>>>>>>>>>>暫時沒在用
        public static bool CreateTurningToolNxopen(
            int toolNo,
            string insertName, string insertSpec, bool insertOnly, 
            string holderName, int holderType, string holderSpec, string handSide, 
            string holderLength, string holderWidth, string workLength, string workWidth)
        {
            try
            {
                Part workPart = theSession.Parts.Work;

                //取得Machine Tool Root的NCGroup
                NCGroup machineToolRoot = workPart.CAMSetup.GetRoot(CAMSetup.View.MachineTool);

                //建立刀具
                string toolType = "turning";
                string toolSubtype = "OD_80_R";

                //string toolName = insertOnly ? insertName : (insertName + "_" + holderName);
                string toolName = "";
                string preToolName = string.Format("T{0}_{1}", toolNo.ToString(), insertName);
                GetToolName(preToolName, "", out toolName);

                NCGroup createToolGroup = workPart.CAMSetup.CAMGroupCollection.CreateTool(
                    machineToolRoot, toolType, toolSubtype, NCGroupCollection.UseDefaultName.False, toolName);
                Tool createTool = (Tool)createToolGroup;

                

                string shapeCode = insertSpec.Substring(0, 1);
                string relifeAngleCode = insertSpec.Substring(1, 1);
                string toleranceCode = insertSpec.Substring(2, 1);
                string crossSectionCode = insertSpec.Substring(3, 1);
                string sizeCode = insertSpec.Substring(4, 2);
                string thicknessCode = insertSpec.Substring(6, 2);
                string radiusColde = insertSpec.Substring(8, 2);

                //開始設定刀具參數
                NXOpen.CAM.TurnToolBuilder turnToolBuilder;
                turnToolBuilder = workPart.CAMSetup.CAMGroupCollection.CreateTurnToolBuilder(createTool);
                //設定刀號
                turnToolBuilder.TlNumberBuilder.Value = toolNo;
                //設定刀片形狀(Tool>Insert>ISO Insert Shape)
                turnToolBuilder.InsertShape = GetTurningInsertShape(shapeCode);
                //設定刀片位置(Tool>Insert>Insert Position)
                turnToolBuilder.InsertPosition = TurningToolBuilder.InsertPositions.Topside;
                //設定刀片刀尖R角(Tool>Dimensions>Nose Radius)
                turnToolBuilder.NoseRadiusBuilder.Value = GetTurningInsertRadius(radiusColde);
                //設定刀片切削角度(Tool>Dimensions>Orient angle)
                //turnToolBuilder.OrientAngleBuilder.Value = 80;
                //設定刀片刀尖夾角(Tool>Dimensions>Nose Angle，刀片形狀需指定為X)
                if (turnToolBuilder.InsertShape == TurnToolBuilder.InsertShapes.User)
                {
                    turnToolBuilder.NoseAngleBuilder.Value = 20;
                }
                //設定刀片尺寸類型(Tool>Insert Size>Measurement)
                turnToolBuilder.SizeOption = TurnToolBuilder.SizeOptions.InscribedCircle;
                //設定刀片尺寸(Tool>Insert Size>Length)
                turnToolBuilder.SizeBuilder.Value = GetTurningInsertSize(shapeCode, sizeCode);
                //設定刀片後角類型(Tool>More>Relief Angle)
                turnToolBuilder.ReliefAngleType = GetTurningInsertReliefAngle(relifeAngleCode);
                //設定刀片後角角度(Tool>More>Relief Angle，刀片後角類型需指定為O)
                if (turnToolBuilder.ReliefAngleType == TurnToolBuilder.ReliefAngleTypes.OOther)
                {
                    turnToolBuilder.ReliefAngleBuilder.Value = 45;
                }
                //設定刀片厚度(Tool>More>Thickness)
                double thicknessValue = 0.0;
                turnToolBuilder.ThicknessType = GetTurninInsertThickness(thicknessCode, out thicknessValue);
                if (turnToolBuilder.ThicknessType == TurnToolBuilder.ThicknessTypes.ThicknessUserDefined)
                {
                    turnToolBuilder.ThicknessBuilder.Value = thicknessValue;
                }

                #region 刀桿設定
                if (insertOnly)
                {
                    //設定是否使用刀桿(Tool>Holder(Shank)>Use Turn Holder)
                    turnToolBuilder.HolderUse = false;
                }
                else
                {
                    turnToolBuilder.TlDescription = holderName;
                    string holderStyleCode = holderSpec.Substring(2, 1);
                    //設定是否使用刀桿(Tool>Holder(Shank)>Use Turn Holder)
                    turnToolBuilder.HolderUse = true;
                    //設定刀桿類型(Tool>Holder(Shank)>Style)
                    turnToolBuilder.HolderStyle = GetTurningHolderStyle(holderStyleCode);
                    //設定刀桿左右手(Tool>Holder(Shank)>Hand)
                    turnToolBuilder.HolderHand = GetTurningHolderHand(handSide);

                    if (holderType == 1)//外
                    {
                        //設定刀桿方形圓形(Tool>Holder(Shank)>Shank Type)
                        turnToolBuilder.HolderShankType = TurnToolBuilder.HolderShankTypes.Square;
                        //設定刀桿角度(Holder>Dimensions>Holder Angle)
                        turnToolBuilder.HolderAngleBuilder.Value = 90.0;
                    }
                    else//內
                    {
                        //設定刀桿方形圓形(Tool>Holder(Shank)>Shank Type)
                        turnToolBuilder.HolderShankType = TurnToolBuilder.HolderShankTypes.Round;
                        //設定刀桿角度(Holder>Dimensions>Holder Angle)
                        turnToolBuilder.HolderAngleBuilder.Value = 0.0;
                    }

                    //設定刀桿刀片是否固定角度(Tool>Holder(Shank)>Lock Insert and Holder Orientation，刀桿類型需指定為Ud)
                    if (turnToolBuilder.HolderStyle == TurnToolBuilder.HolderStyles.Ud)
                    {
                        turnToolBuilder.HolderLock = false;
                    }

                    //設定刀桿尺寸
                    //設定刀具長度(Holder>Dimensions>Length)
                    turnToolBuilder.HolderLengthBuilder.Value = double.Parse(holderLength);
                    //設定刀尖位置(寬)(Holder>Dimensions>Width)
                    turnToolBuilder.HolderWidthBuilder.Value = double.Parse(workWidth);
                    //設定刀桿寬度(Holder>Dimensions>Shank Width)
                    turnToolBuilder.HolderShankWidthBuilder.Value = double.Parse(holderWidth);
                    //設定刀尖位置(長)(Holder>Dimensions>Shank Line)
                    turnToolBuilder.HolderShankLineBuilder.Value = double.Parse(workLength);
                }
                #endregion
                

                ////設定Tracking Point
                //NXOpen.CAM.TrackingBuilder trackingBuilder = turnToolBuilder.TrackingBuilder;
                ////取得預設的Tracking Point
                //NXObject trackingPoint = trackingBuilder.GetTrackPoint(0);
                ////修改參數
                ////名稱(Tracking>Tracking Points>Name)
                //string tpName = "TP";
                ////位置點(Tracking>Tracking Points>P Number，0-8代表p2, p6, p1, p7, p9, p5, p3, p8, p4)
                //int tpNo = 8;
                //trackingBuilder.Modify(trackingPoint, tpName, 0, tpNo, 0, 0, 0, 0, 0, 0);

                //圓形刀片專用
                //turnToolBuilder.ButtonDiameterBuilder
                //turnToolBuilder.HolderControlAngleBuilder
                //turnToolBuilder.HolderControlWidthBuilder

                turnToolBuilder.Commit();
                turnToolBuilder.Destroy();
            }
            catch (System.Exception ex)
            {
                //Program.ShowListingWindow(ex.Message);
                return false;
            }
            return true;
        }
        //建立車刀刀具(給刀片、刀桿資料類別)>>>>>>>>>>>>>>>>>>暫時沒在用
        public static bool CreateTurningToolNxopen(int toolNo, TurningInsertData insertData, ExternalHolderData exHolder, InternalHolderData inHolder, out Tool turningTool)
        {
            turningTool = null;
            try
            {
                bool status;
                Part workPart = theSession.Parts.Work;

                //取得Machine Tool Root的NCGroup
                NCGroup machineToolRoot = workPart.CAMSetup.GetRoot(CAMSetup.View.MachineTool);

                //建立刀具
                string toolType = "turning";
                string toolSubtype = "OD_80_R";

                string toolName = "";
                string preToolName = string.Format("T{0}_{1}", toolNo.ToString(), insertData.PRODUCT_NO);
                GetToolName(preToolName, "", out toolName);

                NCGroup createToolGroup = workPart.CAMSetup.CAMGroupCollection.CreateTool(
                    machineToolRoot, toolType, toolSubtype, NCGroupCollection.UseDefaultName.False, toolName);
                Tool createTool = (Tool)createToolGroup;
 
                //開始設定刀具參數
                NXOpen.CAM.TurnToolBuilder turnToolBuilder;
                turnToolBuilder = workPart.CAMSetup.CAMGroupCollection.CreateTurnToolBuilder(createTool);
                //設定刀號
                turnToolBuilder.TlNumberBuilder.Value = toolNo;

                //設定刀片參數
                status = SetTurningInsertData(ref turnToolBuilder, insertData);
                if (!status)
                {
                    CaxLog.ShowListingWindow("設定刀片參數錯誤！");
                    return false;
                }

                //設定刀片角度
                turnToolBuilder.OrientAngleBuilder.Value = 0.0;
           
                #region 刀桿設定
                if (exHolder == null && inHolder == null)
                {
                    //設定是否使用刀桿(Tool>Holder(Shank)>Use Turn Holder)
                    turnToolBuilder.HolderUse = false;
                }
                else
                {
                    //設定是否使用刀桿(Tool>Holder(Shank)>Use Turn Holder)
                    turnToolBuilder.HolderUse = true;
                    if (exHolder != null)
                    {
                        //外徑
                        turnToolBuilder.TlDescription = exHolder.PRODUCT_NO;
                        string holderStyleCode = exHolder.ISO_CODE.Substring(2, 1);
                        //設定刀桿類型(Tool>Holder(Shank)>Style)
                        turnToolBuilder.HolderStyle = GetTurningHolderStyle(holderStyleCode);
                        //設定刀桿左右手(Tool>Holder(Shank)>Hand)
                        turnToolBuilder.HolderHand = GetTurningHolderHand(exHolder.HAND_SIDE);
                        //設定刀桿方形圓形(Tool>Holder(Shank)>Shank Type)
                        turnToolBuilder.HolderShankType = TurnToolBuilder.HolderShankTypes.Square;
                        //設定刀桿角度(Holder>Dimensions>Holder Angle)
                        turnToolBuilder.HolderAngleBuilder.Value = 90.0;

                        //設定刀桿尺寸
                        //設定刀具長度(Holder>Dimensions>Length)
                        turnToolBuilder.HolderLengthBuilder.Value = double.Parse(exHolder.SHANK_L_NX);
                        //設定刀尖位置(寬)(Holder>Dimensions>Width)
                        turnToolBuilder.HolderWidthBuilder.Value = double.Parse(exHolder.FUNC_W_NX);
                        //設定刀桿寬度(Holder>Dimensions>Shank Width)
                        turnToolBuilder.HolderShankWidthBuilder.Value = double.Parse(exHolder.SHANK_W);
                        //設定刀尖位置(長)(Holder>Dimensions>Shank Line)
                        turnToolBuilder.HolderShankLineBuilder.Value = double.Parse(exHolder.FUNC_L_NX);
                    }
                    else
                    {
                        //內徑
                        turnToolBuilder.TlDescription = inHolder.PRODUCT_NO;
                        string holderStyleCode = inHolder.ISO_CODE.Substring(2, 1);
                        //設定刀桿類型(Tool>Holder(Shank)>Style)
                        turnToolBuilder.HolderStyle = GetTurningHolderStyle(holderStyleCode);
                        //設定刀桿左右手(Tool>Holder(Shank)>Hand)
                        turnToolBuilder.HolderHand = GetTurningHolderHand(inHolder.HAND_SIDE);
                        //設定刀桿方形圓形(Tool>Holder(Shank)>Shank Type)
                        turnToolBuilder.HolderShankType = TurnToolBuilder.HolderShankTypes.Round;
                        //設定刀桿角度(Holder>Dimensions>Holder Angle)
                        turnToolBuilder.HolderAngleBuilder.Value = 0.0;

                        //設定刀桿尺寸
                        //設定刀具長度(Holder>Dimensions>Length)
                        turnToolBuilder.HolderLengthBuilder.Value = double.Parse(inHolder.SHANK_L_NX);
                        //設定刀尖位置(寬)(Holder>Dimensions>Width)
                        turnToolBuilder.HolderWidthBuilder.Value = double.Parse(inHolder.FUNC_W_NX);
                        //設定刀桿寬度(Holder>Dimensions>Shank Width)
                        turnToolBuilder.HolderShankWidthBuilder.Value = double.Parse(inHolder.SHANK_W);
                        //設定刀尖位置(長)(Holder>Dimensions>Shank Line)
                        turnToolBuilder.HolderShankLineBuilder.Value = double.Parse(inHolder.FUNC_L_NX);
                    }

                    //設定刀桿刀片是否固定角度(Tool>Holder(Shank)>Lock Insert and Holder Orientation，刀桿類型需指定為Ud)
                    if (turnToolBuilder.HolderStyle == TurnToolBuilder.HolderStyles.Ud)
                    {
                        turnToolBuilder.HolderLock = false;
                    }

                }
                #endregion

                //設定Tracking Point
                NXOpen.CAM.TrackingBuilder trackingBuilder = turnToolBuilder.TrackingBuilder;
                //取得預設的Tracking Point
                NXObject trackingPoint = trackingBuilder.GetTrackPoint(0);
                //修改參數
                //名稱(Tracking>Tracking Points>Name)
                string tpName = "TP";
                //位置點(Tracking>Tracking Points>P Number，0-8代表p2, p6, p1, p7, p9, p5, p3, p8, p4)
                int tpNo = 6;
                trackingBuilder.Modify(trackingPoint, tpName, 0, tpNo, 0, 0, 0, 0, 0, 0);

                //圓形刀片專用
                //turnToolBuilder.ButtonDiameterBuilder
                //turnToolBuilder.HolderControlAngleBuilder
                //turnToolBuilder.HolderControlWidthBuilder

                turningTool = (NXOpen.CAM.Tool)turnToolBuilder.Commit();
                turnToolBuilder.Destroy();

                theUfSession.UiOnt.Refresh();
            }
            catch (System.Exception ex)
            {
                CaxLog.ShowListingWindow("CreateTurningToolNxopen()..." + ex.Message);
                return false;
            }
            return true;
        }
        
        //建立外徑車刀刀具(給刀片、刀桿資料類別)
        public static bool CreateExTurningToolNxopen(int toolNo, TurningInsertData insertData, ExternalHolderData exHolder, out Tool turningTool)
        {
            turningTool = null;
            try
            {
                bool status;
                Part workPart = theSession.Parts.Work;

                //取得Machine Tool Root的NCGroup
                NCGroup machineToolRoot = workPart.CAMSetup.GetRoot(CAMSetup.View.MachineTool);

                //建立刀具
                string toolType = "turning";
                string toolSubtype = "OD_80_R";

                string toolName = "";
                string preToolName = string.Format("T{0}_{1}", toolNo.ToString(), insertData.PRODUCT_NO);
                GetToolName(preToolName, "", out toolName);

                NCGroup createToolGroup = workPart.CAMSetup.CAMGroupCollection.CreateTool(
                    machineToolRoot, toolType, toolSubtype, NCGroupCollection.UseDefaultName.False, toolName);
                Tool createTool = (Tool)createToolGroup;


                #region 處理掉ss型刀桿可能會有欄位為null的部分
                //取得刀片尺寸代碼
                string insertSizeCode = exHolder.ISO_SPEC.Substring(exHolder.ISO_SPEC.Length - 2, 2);
                //取得對應的刀片斜對角線一半尺寸
                double insertSizeDiagonal = 0.0;
                NameValuePair nvp = insertSsConfig.Find(qq => qq.NAME == insertSizeCode);
                if (nvp != null)
                {
                    insertSizeDiagonal = double.Parse(nvp.VALUE);
                }

                //修改
                if (exHolder.SHANK_L_NX == null || exHolder.SHANK_L_NX == "")
                {
                    exHolder.SHANK_L_NX = (double.Parse(exHolder.SHANK_L) + insertSizeDiagonal).ToString();
                }
                if (exHolder.FUNC_W_NX == null || exHolder.FUNC_W_NX == "")
                {
                    exHolder.FUNC_W_NX = (double.Parse(exHolder.FUNC_W) - insertSizeDiagonal).ToString();
                }
                if (exHolder.FUNC_L_NX == null || exHolder.FUNC_L_NX == "")
                {
                    exHolder.FUNC_L_NX = (double.Parse(exHolder.FUNC_L) + insertSizeDiagonal).ToString();
                }
                if (exHolder.HEAD_W == null || exHolder.HEAD_W == "")
                {
                    exHolder.HEAD_W = "";
                }
                #endregion

                //開始設定刀具參數
                NXOpen.CAM.TurnToolBuilder turnToolBuilder;
                turnToolBuilder = workPart.CAMSetup.CAMGroupCollection.CreateTurnToolBuilder(createTool);
                //設定刀號
                turnToolBuilder.TlNumberBuilder.Value = toolNo;

                //設定刀片參數
                status = SetTurningInsertData(ref turnToolBuilder, insertData);
                if (!status)
                {
                    CaxLog.ShowListingWindow("設定刀片參數錯誤！");
                    return false;
                }

                #region 刀桿設定
                if (exHolder == null)
                {
                    //設定是否使用刀桿(Tool>Holder(Shank)>Use Turn Holder)
                    turnToolBuilder.HolderUse = false;
                }
                else
                {
                    //設定是否使用刀桿(Tool>Holder(Shank)>Use Turn Holder)
                    turnToolBuilder.HolderUse = true;

                    //外徑
                    turnToolBuilder.TlDescription = exHolder.PRODUCT_NO;
                    string holderStyleCode = exHolder.ISO_CODE.Substring(2, 1);
                    //設定刀桿類型(Tool>Holder(Shank)>Style)
                    turnToolBuilder.HolderStyle = GetTurningHolderStyle(holderStyleCode);
                    //設定刀桿左右手(Tool>Holder(Shank)>Hand)
                    turnToolBuilder.HolderHand = GetTurningHolderHand(exHolder.HAND_SIDE);
                    //設定刀桿方形圓形(Tool>Holder(Shank)>Shank Type)
                    turnToolBuilder.HolderShankType = TurnToolBuilder.HolderShankTypes.Square;
                    //設定刀桿角度(Holder>Dimensions>Holder Angle)
                    turnToolBuilder.HolderAngleBuilder.Value = 90.0;

                    //設定刀桿尺寸
                    //設定刀具長度(Holder>Dimensions>Length)
                    turnToolBuilder.HolderLengthBuilder.Value = double.Parse(exHolder.SHANK_L_NX);
                    //設定刀尖位置(寬)(Holder>Dimensions>Width)
                    turnToolBuilder.HolderWidthBuilder.Value = double.Parse(exHolder.FUNC_W_NX);
                    //設定刀桿寬度(Holder>Dimensions>Shank Width)
                    turnToolBuilder.HolderShankWidthBuilder.Value = double.Parse(exHolder.SHANK_W);
                    //設定刀尖位置(長)(Holder>Dimensions>Shank Line)
                    turnToolBuilder.HolderShankLineBuilder.Value = double.Parse(exHolder.FUNC_L_NX);

                    //設定刀桿刀片是否固定角度(Tool>Holder(Shank)>Lock Insert and Holder Orientation，刀桿類型需指定為Ud)
                    if (turnToolBuilder.HolderStyle == TurnToolBuilder.HolderStyles.Ud)
                    {
                        turnToolBuilder.HolderLock = false;
                    }

                    //設定刀片角度
                    string shapeCode = insertData.ISO_SPEC.Substring(0, 1);
                    turnToolBuilder.OrientAngleBuilder.Value = GetInsertOrientAngle(shapeCode, holderStyleCode);

                }
                #endregion

                //設定Tracking Point
                NXOpen.CAM.TrackingBuilder trackingBuilder = turnToolBuilder.TrackingBuilder;
                //取得預設的Tracking Point
                NXObject trackingPoint = trackingBuilder.GetTrackPoint(0);
                //修改參數
                //名稱(Tracking>Tracking Points>Name)
                string tpName = "TP";
                //位置點(Tracking>Tracking Points>P Number，0-8代表p2, p6, p1, p7, p9, p5, p3, p8, p4)
                int tpNo = 6;
                trackingBuilder.Modify(trackingPoint, tpName, 0, tpNo, 0, 0, 0, 0, 0, 0);

                //圓形刀片專用
                //turnToolBuilder.ButtonDiameterBuilder
                //turnToolBuilder.HolderControlAngleBuilder
                //turnToolBuilder.HolderControlWidthBuilder

                turningTool = (NXOpen.CAM.Tool)turnToolBuilder.Commit();
                turnToolBuilder.Destroy();

                theUfSession.UiOnt.Refresh();
            }
            catch (System.Exception ex)
            {
                CaxLog.ShowListingWindow("CreateTurningToolNxopen()..." + ex.Message);
                return false;
            }
            return true;
        }
        //建立內徑車刀刀具(給刀片、刀桿資料類別)
        public static bool CreateInTurningToolNxopen(int toolNo, TurningInsertData insertData, InternalHolderData inHolder, out Tool turningTool)
        {
            turningTool = null;
            try
            {
                bool status;
                Part workPart = theSession.Parts.Work;

                //取得Machine Tool Root的NCGroup
                NCGroup machineToolRoot = workPart.CAMSetup.GetRoot(CAMSetup.View.MachineTool);

                //建立刀具
                string toolType = "turning";
                string toolSubtype = "OD_80_R";

                string toolName = "";
                string preToolName = string.Format("T{0}_{1}", toolNo.ToString(), insertData.PRODUCT_NO);
                GetToolName(preToolName, "", out toolName);

                NCGroup createToolGroup = workPart.CAMSetup.CAMGroupCollection.CreateTool(
                    machineToolRoot, toolType, toolSubtype, NCGroupCollection.UseDefaultName.False, toolName);
                Tool createTool = (Tool)createToolGroup;


                #region 補足參數缺少的部分，未完成
                string insertShapeAngle = inHolder.ISO_CODE.Substring(1, 2).ToUpper();
                //KORLOY工作長可能沒輸入 DZ、TW頭長也要幫他算給20
                if (inHolder.SUPPLIER.ToUpper() == "KORLOY")
                {
                    if (insertShapeAngle == "DZ" || insertShapeAngle == "TW")
                    {
                        inHolder.HEAD_L = "20.0";
                    }
                }
                //KYOCERA  
                //  DZ(一定是，頭長大部分在L5、313頁的在L4)、JZ(一定是、頭長在L3)、TZ(一定是、頭長在L3)、VZ(一定是、頭長在L5)
                //  注意一下292頁STWP系列 工作長是L2-L3)
                if (inHolder.SUPPLIER.ToUpper() == "KYOCERA")
                {
                    if (insertShapeAngle == "DZ" || insertShapeAngle == "JZ" || 
                        insertShapeAngle == "TZ" || insertShapeAngle == "VZ" || insertShapeAngle == "TW")
                    {
                        if (inHolder.HEAD_L == null || inHolder.HEAD_L == "")
                        {
                            if (inHolder.FUNC_L_B != null)
                            {
                            }
                        }
                    }
                }
                //MITSUBISHI工作長可能沒輸入 
                //  DZ(一定是，但規格沒有頭長，圖片有=25)、CZ(一定是，頭長在規格LH)
                //SANDVIK工作長可能沒輸入 
                //  DU(有l7的才有頭長 = l7 - 柄長) 
                //SECO 工作長可能沒輸入 
                //  DY(一定是，頭長=l7-柄長)
                //SUMITOMO 工作長可能沒輸入 
                //  DZ(一定是、但不一定有頭長資訊10 15 20 26)、VZ(一定是、頭長寫在L2或是15)
                //可能是null的欄位SHANK_B FUNC_L_A FUNC_L_B CLEARANCE_W
                #endregion

                //開始設定刀具參數
                NXOpen.CAM.TurnToolBuilder turnToolBuilder;
                turnToolBuilder = workPart.CAMSetup.CAMGroupCollection.CreateTurnToolBuilder(createTool);
                //設定刀號
                turnToolBuilder.TlNumberBuilder.Value = toolNo;

                //設定刀片參數
                status = SetTurningInsertData(ref turnToolBuilder, insertData);
                if (!status)
                {
                    CaxLog.ShowListingWindow("設定刀片參數錯誤！");
                    return false;
                }

                #region 刀桿設定
                if (inHolder == null)
                {
                    //設定是否使用刀桿(Tool>Holder(Shank)>Use Turn Holder)
                    turnToolBuilder.HolderUse = false;
                }
                else
                {
                    //設定是否使用刀桿(Tool>Holder(Shank)>Use Turn Holder)
                    turnToolBuilder.HolderUse = true;

                    //內徑
                    turnToolBuilder.TlDescription = inHolder.PRODUCT_NO;
                    string holderStyleCode = inHolder.ISO_CODE.Substring(2, 1);
                    //設定刀桿類型(Tool>Holder(Shank)>Style)
                    turnToolBuilder.HolderStyle = GetTurningHolderStyle(holderStyleCode);
                    //設定刀桿左右手(Tool>Holder(Shank)>Hand)
                    turnToolBuilder.HolderHand = GetTurningHolderHand(inHolder.HAND_SIDE);
                    //設定刀桿方形圓形(Tool>Holder(Shank)>Shank Type)
                    turnToolBuilder.HolderShankType = TurnToolBuilder.HolderShankTypes.Round;
                    //設定刀桿角度(Holder>Dimensions>Holder Angle)
                    turnToolBuilder.HolderAngleBuilder.Value = 0.0;

                    //設定刀桿尺寸
                    //設定刀具長度(Holder>Dimensions>Length)
                    turnToolBuilder.HolderLengthBuilder.Value = double.Parse(inHolder.SHANK_L_NX);
                    //設定刀尖位置(寬)(Holder>Dimensions>Width)
                    turnToolBuilder.HolderWidthBuilder.Value = double.Parse(inHolder.FUNC_W_NX);
                    //設定刀桿寬度(Holder>Dimensions>Shank Width)
                    turnToolBuilder.HolderShankWidthBuilder.Value = double.Parse(inHolder.SHANK_W);
                    //設定刀尖位置(長)(Holder>Dimensions>Shank Line)
                    turnToolBuilder.HolderShankLineBuilder.Value = double.Parse(inHolder.FUNC_L_NX);

                    //設定刀桿刀片是否固定角度(Tool>Holder(Shank)>Lock Insert and Holder Orientation，刀桿類型需指定為Ud)
                    if (turnToolBuilder.HolderStyle == TurnToolBuilder.HolderStyles.Ud)
                    {
                        turnToolBuilder.HolderLock = false;
                    }

                    //設定刀片角度
                    string shapeCode = insertData.ISO_SPEC.Substring(0, 1);
                    turnToolBuilder.OrientAngleBuilder.Value = GetInsertOrientAngle(shapeCode, holderStyleCode);
                }
                #endregion

                //設定Tracking Point
                NXOpen.CAM.TrackingBuilder trackingBuilder = turnToolBuilder.TrackingBuilder;
                //取得預設的Tracking Point
                NXObject trackingPoint = trackingBuilder.GetTrackPoint(0);
                //修改參數
                //名稱(Tracking>Tracking Points>Name)
                string tpName = "TP";
                //位置點(Tracking>Tracking Points>P Number，0-8代表p2, p6, p1, p7, p9, p5, p3, p8, p4)
                int tpNo = 0;
                trackingBuilder.Modify(trackingPoint, tpName, 0, tpNo, 0, 0, 0, 0, 0, 0);

                //圓形刀片專用
                //turnToolBuilder.ButtonDiameterBuilder
                //turnToolBuilder.HolderControlAngleBuilder
                //turnToolBuilder.HolderControlWidthBuilder

                turningTool = (NXOpen.CAM.Tool)turnToolBuilder.Commit();
                turnToolBuilder.Destroy();

                theUfSession.UiOnt.Refresh();
            }
            catch (System.Exception ex)
            {
                CaxLog.ShowListingWindow("CreateTurningToolNxopen()..." + ex.Message);
                return false;
            }
            return true;
        }

        //獲得刀具名稱，若有重複的會自動加底線流水號
        public static bool GetToolName(string insertName, string holderName, out string toolName)
        {
            toolName = "";
            try
            {
                List<string> allToolNameList;
                FindAllMachineToolName(out allToolNameList);

                string tempName = (holderName == "") ? insertName : insertName + "_" + holderName;

                if (allToolNameList.Contains(tempName))
                {
                    List<string> sameNameToolList = allToolNameList.FindAll(
                        qq => 
                            qq.ToUpper().Contains(tempName.ToUpper()) && 
                            qq.Length - tempName.Length <= 2);

                    toolName = tempName + "_" + sameNameToolList.Count.ToString();
                }
                else
                {
                    toolName = tempName;
                }

            }
            catch (System.Exception ex)
            {
                CaxLog.ShowListingWindow("GetToolName()..." + ex.Message);
            	return false;
            }
            return true;
        }
        //獲得所有刀具名稱
        public static bool FindAllMachineToolName(out List<string> toolNameList)
        {
            toolNameList = new List<string>();
            try
            {
                Part workPart = theSession.Parts.Work;

                //取得Machine Tool Root的NCGroup
                NCGroup machineToolRoot = workPart.CAMSetup.GetRoot(CAMSetup.View.MachineTool);

                CAMObject[] toolRootMenberAry = machineToolRoot.GetMembers();


                foreach (CAMObject member in toolRootMenberAry)
                {
                    if (member.Name != "NONE")
                    {
                        toolNameList.Add(member.Name);
                    }
                }
            }
            catch (System.Exception ex)
            {
                CaxLog.ShowListingWindow("FindAllMachineToolName()..." + ex.Message);
                return false;
            }
            return true;
        }

        #region 車刀用
        //設定刀片參數
        public static bool SetTurningInsertData(ref NXOpen.CAM.TurnToolBuilder turnToolBuilder, TurningInsertData insertData)
        {
            try
            {
                string shapeCode = insertData.ISO_SPEC.Substring(0, 1);
                string relifeAngleCode = insertData.ISO_SPEC.Substring(1, 1);
                string toleranceCode = insertData.ISO_SPEC.Substring(2, 1);
                string crossSectionCode = insertData.ISO_SPEC.Substring(3, 1);
                string sizeCode = insertData.ISO_SPEC.Substring(4, 2);
                string thicknessCode = insertData.ISO_SPEC.Substring(6, 2);
                string radiusColde = insertData.ISO_SPEC.Substring(8, 2);

                //設定刀片形狀(Tool>Insert>ISO Insert Shape)
                turnToolBuilder.InsertShape = GetTurningInsertShape(shapeCode);
                //設定刀片位置(Tool>Insert>Insert Position)
                turnToolBuilder.InsertPosition = TurningToolBuilder.InsertPositions.Underside;
                //設定刀片刀尖R角(Tool>Dimensions>Nose Radius)
                turnToolBuilder.NoseRadiusBuilder.Value = GetTurningInsertRadius(radiusColde);
                //設定刀片切削角度(Tool>Dimensions>Orient angle)
                //turnToolBuilder.OrientAngleBuilder.Value = 80;
                //設定刀片刀尖夾角(Tool>Dimensions>Nose Angle，刀片形狀需指定為X)
                if (turnToolBuilder.InsertShape == TurnToolBuilder.InsertShapes.User)
                {
                    turnToolBuilder.NoseAngleBuilder.Value = 20;
                }
                //設定刀片尺寸類型(Tool>Insert Size>Measurement)
                turnToolBuilder.SizeOption = TurnToolBuilder.SizeOptions.InscribedCircle;
                //設定刀片尺寸(Tool>Insert Size>Length)
                turnToolBuilder.SizeBuilder.Value = GetTurningInsertSize(shapeCode, sizeCode);
                //設定刀片後角類型(Tool>More>Relief Angle)
                turnToolBuilder.ReliefAngleType = GetTurningInsertReliefAngle(relifeAngleCode);
                //設定刀片後角角度(Tool>More>Relief Angle，刀片後角類型需指定為O)
                if (turnToolBuilder.ReliefAngleType == TurnToolBuilder.ReliefAngleTypes.OOther)
                {
                    turnToolBuilder.ReliefAngleBuilder.Value = 45;
                }
                //設定刀片厚度(Tool>More>Thickness)
                double thicknessValue = 0.0;
                turnToolBuilder.ThicknessType = GetTurninInsertThickness(thicknessCode, out thicknessValue);
                if (turnToolBuilder.ThicknessType == TurnToolBuilder.ThicknessTypes.ThicknessUserDefined)
                {
                    turnToolBuilder.ThicknessBuilder.Value = thicknessValue;
                }

            }
            catch (System.Exception ex)
            {
                CaxLog.ShowListingWindow("SetTurningInsertData()..." + ex.Message);
                return false;
            }
            return true;
        }
        //依刀片形狀代碼獲得刀片形狀設定值
        public static TurnToolBuilder.InsertShapes GetTurningInsertShape(string shapeCode)
        {
            switch (shapeCode.ToUpper())
            {
                case "A":
                    return TurnToolBuilder.InsertShapes.Parallelogram85; //Kyocera編碼有，但實際上型錄沒有此類型刀片
                case "B":
                    return TurnToolBuilder.InsertShapes.Parallelogram82; //Kyocera編碼有，但實際上型錄沒有此類型刀片
                case "C":
                    return TurnToolBuilder.InsertShapes.Diamond80;
                case "D":
                    return TurnToolBuilder.InsertShapes.Diamond55;
                case "E":
                    return TurnToolBuilder.InsertShapes.Diamond75;
                case "F":
                    return TurnToolBuilder.InsertShapes.User;//Kyocera編碼有，但實際上型錄沒有此類型刀片
                case "H":
                    return TurnToolBuilder.InsertShapes.Hexagon; //Kyocera編碼有，但實際上型錄沒有此類型刀片
                case "K":
                    return TurnToolBuilder.InsertShapes.Parallelogram55;
                case "L":
                    return TurnToolBuilder.InsertShapes.Rectangle;
                case "M":
                    return TurnToolBuilder.InsertShapes.Diamond86; //Kyocera編碼有，但實際上型錄沒有此類型刀片
                case "O":
                    return TurnToolBuilder.InsertShapes.Octagon; //Kyocera編碼有，但實際上型錄沒有此類型刀片
                case "P":
                    return TurnToolBuilder.InsertShapes.Pentagon; //Kyocera編碼有，但實際上型錄沒有此類型刀片
                case "R":
                    return TurnToolBuilder.InsertShapes.Round;
                case "S":
                    return TurnToolBuilder.InsertShapes.Square;
                case "T":
                    return TurnToolBuilder.InsertShapes.Triangle;
                case "V":
                    return TurnToolBuilder.InsertShapes.Diamond35;
                case "W":
                    return TurnToolBuilder.InsertShapes.Trigon;
                default:
                    return TurnToolBuilder.InsertShapes.User;
            }
        }
        //依刀片後角代碼獲得刀片後角設定值
        public static TurnToolBuilder.ReliefAngleTypes GetTurningInsertReliefAngle(string reliefAngleCode)
        {
            switch (reliefAngleCode)
            {
                case "A":
                    return TurnToolBuilder.ReliefAngleTypes.A3;
                case "B":
                    return TurnToolBuilder.ReliefAngleTypes.B5;
                case "C":
                    return TurnToolBuilder.ReliefAngleTypes.C7;
                case "D":
                    return TurnToolBuilder.ReliefAngleTypes.D15;
                case "E":
                    return TurnToolBuilder.ReliefAngleTypes.E20;
                case "F":
                    return TurnToolBuilder.ReliefAngleTypes.F25;
                case "G":
                    return TurnToolBuilder.ReliefAngleTypes.G30;
                case "N":
                    return TurnToolBuilder.ReliefAngleTypes.N0;
                case "P":
                    return TurnToolBuilder.ReliefAngleTypes.P11;
                default:
                    return TurnToolBuilder.ReliefAngleTypes.OOther;
            }
        }
        //依刀片尺寸代碼獲得對應刀片尺寸值
        public static double GetTurningInsertSize(string shapeCode, string sizeCode)
        {
            InsertSize insertsize = insertSizeConfig.Find(qq => qq.SHAPE_CODE == shapeCode && qq.SIZE_CODE == sizeCode);

            if (insertsize == null)
            {
                return double.Parse(sizeCode);
            }
            else
            {
                return double.Parse(insertsize.IC_SIZE);
            }
        }
        //依刀片厚度代碼獲得刀片厚度值
        public static TurnToolBuilder.ThicknessTypes GetTurninInsertThickness(string thicknessCode, out double thicknessValue)
        {
            switch (thicknessCode)
            {
                case "S1":
                    thicknessValue = 1.39;
                    return TurnToolBuilder.ThicknessTypes.ThicknessUserDefined; //1.39
                case "X1":
                    thicknessValue = 1.40;
                    return TurnToolBuilder.ThicknessTypes.ThicknessUserDefined; //1.40
                case "01":
                    thicknessValue = 1.59;
                    return TurnToolBuilder.ThicknessTypes.Thickness01; //1.59
                case "T0":
                    thicknessValue = 1.79;
                    return TurnToolBuilder.ThicknessTypes.ThicknessUserDefined; //1.79
                case "T1":
                    thicknessValue = 1.98;
                    return TurnToolBuilder.ThicknessTypes.ThicknessT1; //1.98
                case "02":
                    thicknessValue = 2.38;
                    return TurnToolBuilder.ThicknessTypes.Thickness02; //2.38
                case "T2":
                    thicknessValue = 2.78;
                    return TurnToolBuilder.ThicknessTypes.ThicknessT2; //2.78
                case "03":
                    thicknessValue = 3.18;
                    return TurnToolBuilder.ThicknessTypes.Thickness03; //3.18
                case "T3":
                    thicknessValue = 3.97;
                    return TurnToolBuilder.ThicknessTypes.ThicknessT3; //3.97
                case "04":
                    thicknessValue = 4.76;
                    return TurnToolBuilder.ThicknessTypes.Thickness04; //4.76
                case "05":
                    thicknessValue = 5.56;
                    return TurnToolBuilder.ThicknessTypes.Thickness05; //5.56
                case "06":
                    thicknessValue = 6.35;
                    return TurnToolBuilder.ThicknessTypes.Thickness06; //6.35
                case "07":
                    thicknessValue = 7.97;
                    return TurnToolBuilder.ThicknessTypes.Thickness07; //7.97
                case "09":
                    thicknessValue = 9.52;
                    return TurnToolBuilder.ThicknessTypes.Thickness09; //9.52
                case "10":
                    thicknessValue = 10.0;
                    return TurnToolBuilder.ThicknessTypes.ThicknessUserDefined; //10.00
                case "11":
                    thicknessValue = 11.11;
                    return TurnToolBuilder.ThicknessTypes.Thickness11; //11.11
                case "12":
                    thicknessValue = 12.7;
                    return TurnToolBuilder.ThicknessTypes.Thickness12; //12.70
                default:
                    thicknessValue = 15.0;
                    return TurnToolBuilder.ThicknessTypes.ThicknessUserDefined; //15.0
            }
        }
        //依刀片R角代碼獲得刀片R角值
        public static double GetTurningInsertRadius(string radiusCode)
        {
            switch (radiusCode)
            {
                case "V3":
                    return 0.03;
                case "V5":
                    return 0.05;
                case "01":
                    return 0.1;
                case "02":
                    return 0.2;
                case "04":
                    return 0.4;
                case "08":
                    return 0.8;
                case "10":
                    return 1.0;
                case "12":
                    return 1.2;
                case "16":
                    return 1.6;
                case "20":
                    return 2.0;
                case "24":
                    return 2.4;
                case "28":
                    return 2.8;
                case "32":
                    return 3.2;
                case "00":
                    return 0.0;
                default:
                    return 0.0;
            }
        }
        //依刀桿類型代碼獲得刀桿類型設定值
        public static TurnToolBuilder.HolderStyles GetTurningHolderStyle(string holderStyleCode)
        {
            switch (holderStyleCode)
            {
                case "A":
                    return TurnToolBuilder.HolderStyles.A;
                case "B":
                    return TurnToolBuilder.HolderStyles.B;
                case "C":
                    return TurnToolBuilder.HolderStyles.C;
                case "D":
                    return TurnToolBuilder.HolderStyles.D;
                case "E":
                    return TurnToolBuilder.HolderStyles.E;
                case "F":
                    return TurnToolBuilder.HolderStyles.F;
                case "G":
                    return TurnToolBuilder.HolderStyles.G;
                case "H":
                    return TurnToolBuilder.HolderStyles.Ud;
                case "I":
                    return TurnToolBuilder.HolderStyles.I;
                case "J":
                    return TurnToolBuilder.HolderStyles.J;
                case "K":
                    return TurnToolBuilder.HolderStyles.K;
                case "L":
                    return TurnToolBuilder.HolderStyles.L;
                case "M":
                    return TurnToolBuilder.HolderStyles.Ud;
                case "N":
                    return TurnToolBuilder.HolderStyles.N;
                case "P":
                    return TurnToolBuilder.HolderStyles.P;
                case "Q":
                    return TurnToolBuilder.HolderStyles.Q;
                case "R":
                    return TurnToolBuilder.HolderStyles.R;
                case "S":
                    return TurnToolBuilder.HolderStyles.S;
                case "T":
                    return TurnToolBuilder.HolderStyles.T;
                case "U":
                    return TurnToolBuilder.HolderStyles.Ud;
                case "V":
                    return TurnToolBuilder.HolderStyles.Ud;
                case "W":
                    return TurnToolBuilder.HolderStyles.Ud;
                case "Y":
                    return TurnToolBuilder.HolderStyles.Ud;
                default:
                    return TurnToolBuilder.HolderStyles.Ud;
            }
        }
        //依刀桿左右手代碼獲得刀桿左右手設定值
        public static TurnToolBuilder.HolderHands GetTurningHolderHand(string handSideCode)
        {
            switch (handSideCode)
            {
                case"R":
                    return TurnToolBuilder.HolderHands.Right;
                case"L":
                    return TurnToolBuilder.HolderHands.Left;
                case "N":
                    return TurnToolBuilder.HolderHands.Neutral;
                default:
                    return TurnToolBuilder.HolderHands.Right;
            }
        }
        //獲得刀片角度
        public static double GetInsertOrientAngle(string insertShape, string holderStyle)
        {
            InsertOrientAngle matchOrientAngle = insertOrientAngleConfig.Find(qq => qq.INSERT_SHAPE == insertShape && qq.HOLDER_STYLE == holderStyle);
            if (matchOrientAngle == null)
            {
                return 0.0;
            }
            else
            {
                return double.Parse(matchOrientAngle.ORIENT_ANGLE);
            }
        }
        #endregion


        //建立銑刀
        public static bool CreateMillToolNxopen(int toolNo, MillingToolData millToolData, out Tool millTool)
        {
            millTool = null;
            try
            {
                Part workPart = theSession.Parts.Work;

                //取得Machine Tool Root的NCGroup
                NCGroup machineToolRoot = workPart.CAMSetup.GetRoot(CAMSetup.View.MachineTool);

                //建立刀具
                string toolType = "mill_planar";
                string toolSubtype = "MILL";
                string toolName = "";
                string preToolName = string.Format("T{0}_{1}", toolNo, millToolData.CUTTER_NAME);
                GetToolName(preToolName, "", out toolName);

                NCGroup createToolGroup = workPart.CAMSetup.CAMGroupCollection.CreateTool(
                    machineToolRoot, toolType, toolSubtype, NCGroupCollection.UseDefaultName.False, toolName);
                Tool createTool = (Tool)createToolGroup;

                //設定刀具參數
                NXOpen.CAM.MillToolBuilder millToolBuilder;
                millToolBuilder = workPart.CAMSetup.CAMGroupCollection.CreateMillToolBuilder(createTool);

                //設定刀具直徑(Tool>Dimensions>Diameter)
                millToolBuilder.TlDiameterBuilder.Value = double.Parse(millToolData.DIAMETER);
                //設定刀尖R角(Tool>Dimensions>Lower Radius)
                millToolBuilder.TlCor1RadBuilder.Value = double.Parse(millToolData.RADIUS);
                //設定錐度(Tool>Dimensions>Taper Angle)
                millToolBuilder.TlTaperAngBuilder.Value = double.Parse(millToolData.FLUTE_B_ANGLE);
                //設定刀尖斜度(Tool>Dimensions>Tip Angle)
                millToolBuilder.TlTipAngBuilder.Value = double.Parse(millToolData.FLUTE_A_ANGLE);
                //設定刃長(Tool>Dimensions>Flute Length)
                millToolBuilder.TlFluteLnBuilder.Value = double.Parse(millToolData.FLUTE_LENGTH);
                //設定刃數(Tool>Dimensions>Flutes)
                millToolBuilder.TlNumFlutesBuilder.Value = int.Parse(millToolData.FLUTE_NUM);


                //銜接徑
                double connectDiameter = 0.0;
                //總長
                double totalLength = 0.0;
                //刀把階段
                int sectionIndex = 0;
                double sectionDiameter = 0.0;
                double sectionLength = 0.0;
                double sectionTaper = 0.0;
                double sectionRadius = 0.0;

                if (double.Parse(millToolData.NECK_LENGTH) != 0.0)
                {
                    if (double.Parse(millToolData.NECK_DIAMETER) == 0.0) //基本型
                    {
                        //刀長=NL
                        millToolBuilder.TlHeightBuilder.Value = double.Parse(millToolData.NECK_LENGTH);
                        //記錄刀長
                        totalLength = double.Parse(millToolData.NECK_LENGTH);

                        if (double.Parse(millToolData.FLUTE_B_ANGLE) == 0.0)
                        {
                            //銜接徑=D
                            connectDiameter = double.Parse(millToolData.DIAMETER);
                        }
                        else
                        {
                            //銜接徑要算
                            //刃側斜角(弧度)
                            double fluteAngleB = CommonFunction.DegreeToRadian(double.Parse(millToolData.FLUTE_B_ANGLE));
                            double toolLength = millToolBuilder.TlHeightBuilder.Value;
                            connectDiameter = double.Parse(millToolData.DIAMETER) + toolLength * Math.Tan(fluteAngleB) * 2;
                        }
                    }
                    else //刀頭型
                    {
                        //刀長=刃長
                        millToolBuilder.TlHeightBuilder.Value = double.Parse(millToolData.FLUTE_LENGTH);
                        //記錄刀長
                        totalLength = double.Parse(millToolData.FLUTE_LENGTH);

                        //刃頸銜接階段
                        if (double.Parse(millToolData.BETA_ANGLE) != 90.0) //90度代表沒有銜接階段
                        {
                            //設定直徑(要考慮刃側斜角)
                            double fluteAngleB = CommonFunction.DegreeToRadian(double.Parse(millToolData.FLUTE_B_ANGLE));
                            double toolLength = millToolBuilder.TlHeightBuilder.Value;
                            sectionDiameter = double.Parse(millToolData.DIAMETER) + toolLength * Math.Tan(fluteAngleB) * 2;
                            //設定長度
                            sectionLength = CommonFunction.GetCornHeight(double.Parse(millToolData.NECK_DIAMETER), sectionDiameter, double.Parse(millToolData.BETA_ANGLE));
                            //設定錐度
                            sectionTaper = double.Parse(millToolData.BETA_ANGLE);
                            //設定下端R角
                            sectionRadius = 0.0;
                            //新增階段
                            millToolBuilder.HolderSectionBuilder.Add(sectionIndex, sectionDiameter, sectionLength, sectionTaper, sectionRadius);
                            sectionIndex++;
                            //總長修改
                            totalLength += sectionLength;
                        }

                        //設定刀頭
                        //設定直徑
                        sectionDiameter = double.Parse(millToolData.NECK_DIAMETER);
                        //設定長度
                        sectionLength = double.Parse(millToolData.NECK_LENGTH) - totalLength;
                        //設定錐度
                        sectionTaper = 0.0;
                        //設定下端R角
                        sectionRadius = 0.0;
                        //新增階段
                        millToolBuilder.HolderSectionBuilder.Add(sectionIndex, sectionDiameter, sectionLength, sectionTaper, sectionRadius);
                        sectionIndex++;

                        //修改總長
                        totalLength = double.Parse(millToolData.NECK_LENGTH);
                        //修改銜接徑
                        connectDiameter = double.Parse(millToolData.NECK_DIAMETER);
                    }
                }
                else
                {
                    //簡單型
                    //刀長=刃長
                    millToolBuilder.TlHeightBuilder.Value = double.Parse(millToolData.FLUTE_LENGTH);
                    //記錄刀長
                    totalLength = double.Parse(millToolData.FLUTE_LENGTH);

                    if (double.Parse(millToolData.FLUTE_B_ANGLE) == 0.0)
                    {
                        //銜接徑=D
                        connectDiameter = double.Parse(millToolData.DIAMETER);
                    }
                    else
                    {
                        //銜接徑要算
                        //刃側斜角(弧度)
                        double fluteAngleB = CommonFunction.DegreeToRadian(double.Parse(millToolData.FLUTE_B_ANGLE));
                        double toolLength = millToolBuilder.TlHeightBuilder.Value;
                        connectDiameter = double.Parse(millToolData.DIAMETER) + toolLength * Math.Tan(fluteAngleB) * 2;
                    }
                }

                //頸柄銜接階段
                if (double.Parse(millToolData.ALPHA_ANGLE) != 90.0) //90度代表沒有銜接階段
                {
                    //設定直徑
                    sectionDiameter = connectDiameter;
                    //設定長度
                    sectionLength = CommonFunction.GetCornHeight(double.Parse(millToolData.SHANK_DIAMETER), connectDiameter, double.Parse(millToolData.ALPHA_ANGLE));
                    //設定錐度
                    sectionTaper = double.Parse(millToolData.ALPHA_ANGLE);
                    //設定下端R角
                    sectionRadius = 0.0;
                    //新增階段
                    millToolBuilder.HolderSectionBuilder.Add(sectionIndex, sectionDiameter, sectionLength, sectionTaper, sectionRadius);
                    sectionIndex++;
                    //總長修改
                    totalLength += sectionLength;
                }

                //柄階段
                sectionDiameter = double.Parse(millToolData.SHANK_DIAMETER);
                sectionLength = double.Parse(millToolData.LENGTH) - totalLength;
                sectionTaper = 0.0;
                sectionRadius = 0.0;
                millToolBuilder.HolderSectionBuilder.Add(sectionIndex, sectionDiameter, sectionLength, sectionTaper, sectionRadius);

                #region /////////刀把階段設定方法///////////
                ////設定刀把階段
                ////設定段數(Holder>Holder Steps>Step)
                //int sectionIndex = 0;
                ////設定直徑(Holder>Holder Steps>Diameter)
                //double sectionDiameter = 60.0;
                ////設定長度(Holder>Holder Steps>Length)
                //double sectionLength = 20.0;
                ////設定錐度(Holder>Holder Steps>Taper Angle)
                //double sectionTaper = 2.0;
                ////設定下端R角(Holder>Holder Steps>Corner Radius)
                //double sectionRadius = 1.0;
                ////新增階段
                //millToolBuilder.HolderSectionBuilder.Add(sectionIndex, sectionDiameter, sectionLength, sectionTaper, sectionRadius);
                #endregion

                millTool = (Tool)millToolBuilder.Commit();
                millToolBuilder.Destroy();

            }
            catch (System.Exception ex)
            {
                CaxLog.ShowListingWindow("CreateMillToolNxopen()..." + ex.Message);
                return false;
            }
            return true;
        }

        //建立T型刀
        public static bool CreateTcutterToolNxopen(int toolNo, MillingToolData millToolData, out Tool millTool)
        {
            millTool = null;
            try
            {
                Part workPart = theSession.Parts.Work;

                //取得Machine Tool Root的NCGroup
                NCGroup machineToolRoot = workPart.CAMSetup.GetRoot(CAMSetup.View.MachineTool);

                //建立刀具
                string toolType = "mill_planar";
                string toolSubtype = "T_CUTTER";
        
                string toolName = "";
                string preToolName = string.Format("T{0}_{1}", toolNo, millToolData.CUTTER_NAME);
                GetToolName(preToolName, "", out toolName);

                NCGroup createToolGroup = workPart.CAMSetup.CAMGroupCollection.CreateTool(
                    machineToolRoot, toolType, toolSubtype, NCGroupCollection.UseDefaultName.False, toolName);
                Tool createTool = (Tool)createToolGroup;

                //設定刀具參數
                NXOpen.CAM.TToolBuilder toolBuilder;
                toolBuilder = workPart.CAMSetup.CAMGroupCollection.CreateTToolBuilder(createTool);

                //設定刀具直徑(Tool>Dimensions>Diameter)
                toolBuilder.TlDiameterBuilder.Value = double.Parse(millToolData.DIAMETER);
                //設定刀桿直徑(Tool>Dimensions>Shank Diameter)
                toolBuilder.TlShankDiaBuilder.Value = double.Parse(millToolData.SHANK_DIAMETER);
                //設定刀尖R角(Tool>Dimensions>Lower Radius)
                toolBuilder.TlLowCorRadBuilder.Value = double.Parse(millToolData.RADIUS);
                //設定刀刃上R角(Tool>Dimensions>Up Radius)
                toolBuilder.TlUpCorRadBuilder.Value = double.Parse(millToolData.RADIUS);
                //設定刀長(Tool>Dimensions>Length)
                toolBuilder.TlHeightBuilder.Value = double.Parse(millToolData.LENGTH);
                //設定刃長(Tool>Dimensions>Flute Length)
                toolBuilder.TlFluteLnBuilder.Value = double.Parse(millToolData.FLUTE_LENGTH);
                //設定刃數(Tool>Dimensions>Flutes)
                toolBuilder.TlNumFlutesBuilder.Value = int.Parse(millToolData.FLUTE_NUM);


                ////設定刀把階段
                ////設定段數(Holder>Holder Steps>Step)
                //int sectionIndex = 0;
                ////設定直徑(Holder>Holder Steps>Diameter)
                //double sectionDiameter = 60.0;
                ////設定長度(Holder>Holder Steps>Length)
                //double sectionLength = 20.0;
                ////設定錐度(Holder>Holder Steps>Taper Angle)
                //double sectionTaper = 2.0;
                ////設定下端R角(Holder>Holder Steps>Corner Radius)
                //double sectionRadius = 1.0;
                ////新增階段
                //toolBuilder.HolderSectionBuilder.Add(sectionIndex, sectionDiameter, sectionLength, sectionTaper, sectionRadius);

                toolBuilder.Commit();
                toolBuilder.Destroy();
            }
            catch (System.Exception ex)
            {
                CaxLog.ShowListingWindow("CreateTcutterToolNxopen()..." + ex.Message);
                return false;
            }
            return true;
        }

        //組裝刀把到給定的銑刀類刀具
        public static bool AddHolderToMillTool(Tool millTool, System.Collections.ArrayList holderStepAry, double reduction)
        {
            try
            {
                NXOpen.CAM.TToolBuilder millToolBuilder;
                millToolBuilder = workPart.CAMSetup.CAMGroupCollection.CreateTToolBuilder(millTool);

                //刀把階段
                int sectionIndex = 0;
                double sectionDiameter = 0.0;
                double sectionLength = 0.0;
                double sectionTaper = 0.0;
                double sectionRadius = 0.0;

                //獲得目前刀把的階段數=下一階段的index
                sectionIndex = millToolBuilder.HolderSectionBuilder.NumberOfSections;

                //縮短刀具長度
                double maxHoldLength = 0.0;
                if (sectionIndex == 0)
                {
                    //修改刀具長度
                     maxHoldLength = millToolBuilder.TlHeightBuilder.Value - millToolBuilder.TlFluteLnBuilder.Value;
                    if (maxHoldLength < reduction)
                    {
                        string msg = string.Format("可夾持的長度：刀長({0}) - 刃長({1}) = {2}，小於夾持長度({3})，無法建立刀把！", millToolBuilder.TlHeightBuilder.Value, millToolBuilder.TlFluteLnBuilder.Value, maxHoldLength, reduction);
                        CaxLog.ShowListingWindow(msg);
                        millToolBuilder.Destroy();
                        return false;
                    }

                    millToolBuilder.TlHeightBuilder.Value -= reduction;
                }
                else
                {
                    //修改刀把長度
                    //取得最後一階段的長度
                    NXObject section = millToolBuilder.HolderSectionBuilder.GetSection(sectionIndex - 1);
                    double sDia, sLen, sRad, sAng;
                    millToolBuilder.HolderSectionBuilder.Get(section, out sDia, out sLen, out sAng, out sRad);

                    if (Math.Abs(sLen - reduction) < 0.00001)
                    {
                        //最後一階長度跟夾持長度一樣，刪除最後一階
                        millToolBuilder.HolderSectionBuilder.Delete(sectionIndex - 1);
                        //index減一
                        sectionIndex -= 1;
                    }
                    else if (sLen < reduction)
                    {
                        //最後一階長度太短，沒辦法建立
                        string msg = string.Format("可夾持的長度：柄徑長度({0})，小於夾持長度({1})，無法建立刀把！", sLen, reduction);
                        CaxLog.ShowListingWindow(msg);
                        millToolBuilder.Destroy();
                        return false;
                    }
                    else
                    {
                        //最後一階長度修改，再加入
                        double sNewLength = sLen - reduction;
                        millToolBuilder.HolderSectionBuilder.Modify(sectionIndex - 1, sDia, sNewLength, sAng, sRad);
                    }
                }

                //開始加入刀把階段
                foreach (MillingHolderStep mhs in holderStepAry)
                {
                    sectionDiameter = double.Parse(mhs.LOWER_DIAMETER);
                    sectionLength = double.Parse(mhs.STEP_LENGTH);
                    sectionTaper = double.Parse(mhs.STEP_ANGLE);
                    sectionRadius = double.Parse(mhs.STEP_RADIUS);
                    //加入
                    millToolBuilder.HolderSectionBuilder.Add(sectionIndex, sectionDiameter, sectionLength, sectionTaper, sectionRadius);

                    sectionIndex++;
                }

                millToolBuilder.Commit();
                millToolBuilder.Destroy();
            }
            catch (System.Exception ex)
            {
                CaxLog.ShowListingWindow("AddHolderToMillTool()..." + ex.Message);
                return false;
            }
            return true;
        }

        //刀具清單成員寫到刀具屬性
        public static bool SetToolListMember(Tool tool, ToolListMember listMember)
        {
            try
            {
                string preString = "GTTL_";
                tool.SetAttribute(preString + "TOOL_NO", listMember.TOOL_NO);
                tool.SetAttribute(preString + "ERP_NO", listMember.ERP_NO);
                tool.SetAttribute(preString + "CUTTER_QTY", listMember.CUTTER_QTY);
                tool.SetAttribute(preString + "CUTTER_LIFE", listMember.CUTTER_LIFE);
                tool.SetAttribute(preString + "FLUTE_QTY", listMember.FLUTE_QTY);
                tool.SetAttribute(preString + "TITLE", listMember.TITLE);
                tool.SetAttribute(preString + "SPECIFICATION", listMember.SPECIFICATION);

            }
            catch (System.Exception ex)
            {
                CaxLog.ShowListingWindow("SetToolListMember()..." + ex.Message);
                return false;
            }
            return true;
        }
        //從刀具屬性獲得刀具清單成員
        public static bool GetToolListMember(Tool tool, out ToolListMember listMember)
        {
            listMember = new ToolListMember();
            try
            {
                string preString = "GTTL_";

                try
                {
                    listMember.TOOL_NO = tool.GetStringAttribute(preString + "TOOL_NO");
                }
                catch (System.Exception ex)
                { }

                try
                {
                    listMember.ERP_NO = tool.GetStringAttribute(preString + "ERP_NO");
                }
                catch (System.Exception ex)
                { }

                try
                {
                    listMember.CUTTER_QTY = tool.GetStringAttribute(preString + "CUTTER_QTY");
                }
                catch (System.Exception ex)
                { }

                try
                {
                    listMember.CUTTER_LIFE = tool.GetStringAttribute(preString + "CUTTER_LIFE");
                }
                catch (System.Exception ex)
                { }

                try
                {
                    listMember.FLUTE_QTY = tool.GetStringAttribute(preString + "FLUTE_QTY");
                }
                catch (System.Exception ex)
                { }

                try
                {
                    listMember.TITLE = tool.GetStringAttribute(preString + "TITLE");
                }
                catch (System.Exception ex)
                { }

                try
                {
                    listMember.SPECIFICATION = tool.GetStringAttribute(preString + "SPECIFICATION");
                }
                catch (System.Exception ex)
                { }
           
            }
            catch (System.Exception ex)
            {
                CaxLog.ShowListingWindow("GetToolListMember()..." + ex.Message);
                return false;
            }
            return true;
        }





        public static bool CreateToolUf()
        {
            try
            {
                Tag newToolTag;
                //指定刀具的Type(與所在樣板檔同名)
                string toolType = "turning";
                //指定刀具的Subtype
                string toolSubtype = "OD_80_R";
                //建立刀具
                theUfSession.Cutter.Create(toolType, toolSubtype, out newToolTag);

                //取得CAM Setup
                CAMSetup theCamSetup = theSession.Parts.Work.CAMSetup;

                //取得Machine Tool view的Root
                Tag machineToolRootTag;
                theUfSession.Setup.AskMctRoot(theCamSetup.Tag, out machineToolRootTag);

                //將Tool Tag移到Machine Tool Root下
                theUfSession.Ncgroup.AcceptMember(machineToolRootTag, newToolTag);





                //從toolData(來自DB)設定刀具參數
                //theUfSession.Param.SetStrValue(newToolTag, UFConstants.UF_PARAM_TL_DESCRIPTION, "HAHAHA"); //刀具名稱
                //theUfSession.Param.SetDoubleValue(newToolTag, UFConstants.UF_PARAM_TL_DIAMETER, toolData.diameter); //刀具直徑
                //theUfSession.Param.SetDoubleValue(newToolTag, UFConstants.UF_PARAM_TL_HEIGHT, toolData.fully_length); //刀具全長
                //theUfSession.Param.SetDoubleValue(newToolTag, UFConstants.UF_PARAM_TL_FLUTE_LN, toolData.first_length); //刀具刃長
                //theUfSession.Param.SetDoubleValue(newToolTag, UFConstants.UF_PARAM_TL_COR1_RAD, toolData.tool_point); //刀尖R角
                //theUfSession.Param.SetIntValue(newToolTag, UFConstants.UF_PARAM_TL_NUM_FLUTES, toolData.edge_num); //刃數

            }
            catch (System.Exception ex)
            {
                return false;
            }
            return true;
        }
    }






    public class InsertSize
    {
        public InsertSize(string shape, string size, string ic)
        {
            SHAPE_CODE = shape;
            SIZE_CODE = size;
            IC_SIZE = ic;
        }
        public string SHAPE_CODE { get; set; }
        public string SIZE_CODE { get; set; }
        public string IC_SIZE { get; set; }
    }
    public class InsertOrientAngle
    {
        public InsertOrientAngle(string insertshape, string holderstyle, string orientangle)
        {
            INSERT_SHAPE = insertshape;
            HOLDER_STYLE = holderstyle;
            ORIENT_ANGLE = orientangle;
        }
        public string INSERT_SHAPE { get; set; }
        public string HOLDER_STYLE { get; set; }
        public string ORIENT_ANGLE { get; set; }
    }
}
