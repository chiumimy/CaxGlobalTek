using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using NXOpen;
using NXOpen.UF;
using NXOpen.Utilities;
using NXOpen.CAM;
using System.Windows.Forms;
using MRL_OperationWizard;

namespace CaxGlobaltek
{

    public class CaxOper
    {
        public static Session theSession = Session.GetSession();
        public static UI theUI;
        public static UFSession theUfSession = UFSession.GetUFSession();
        public static Part workPart = theSession.Parts.Work;
        public static Part displayPart = theSession.Parts.Display;

        /// <summary>
        /// 取得Operatrion程式名
        /// </summary>
        /// <param name="operTag"></param>
        /// <param name="operName"></param>
        /// <returns></returns>
        public static string AskOperNameFromTag(Tag operTag)
        {
            string operName = "";
            try
            {
                theUfSession.Oper.AskNameFromTag(operTag, out operName);
                return operName;
            }
            catch (System.Exception ex)
            {
                CaxLog.ShowListingWindow("使用AskOperNameFromTag出錯：" + ex.ToString());
                return operName;
            }
        }

        /// <summary>
        /// 取得Operation加工方法
        /// </summary>
        /// <param name="operTag"></param>
        /// <param name="operMethodName"></param>
        /// <returns></returns>
        public static bool AskOperMethodNameFromTag(Tag operTag, out string operMethodName)
        {
            operMethodName = "";
            try
            {
                Tag bb;
                theUfSession.Oper.AskMethodGroup(operTag, out bb);
                NXOpen.CAM.Method cc = (NXOpen.CAM.Method)NXObjectManager.Get(bb);
                operMethodName = cc.Name;
            }
            catch (System.Exception ex)
            {
                CaxLog.ShowListingWindow("使用AskOperMethodNameFromTag出錯：" + ex.ToString());
                return false;
            }
            return true;
        }

        /// <summary>
        /// 取得Operation刀具名稱
        /// </summary>
        /// <param name="operTag"></param>
        /// <param name="operToolName"></param>
        /// <returns></returns>
        public static string AskOperToolNameFromTag(Tag operTag)
        {
            string operToolName = "0";
            try
            {
                Tag bb;
                theUfSession.Oper.AskCutterGroup(operTag, out bb);
                NXOpen.CAM.Tool dd = (NXOpen.CAM.Tool)NXObjectManager.Get(bb);
                operToolName = dd.Name;
                //return operToolName;
            }
            catch (System.Exception ex)
            {
                operToolName = "0";
                //return operToolName;
            }
            return operToolName;
        }

        /// <summary>
        /// 取得此Operation的刀子
        /// </summary>
        /// <returns></returns>
        public static bool AskOperTool(Tag operTag, out Tool tool)
        {
            tool = null;
            try
            {
                Tag bb;
                theUfSession.Oper.AskCutterGroup(operTag, out bb);
                tool = (Tool)NXObjectManager.Get(bb);
            }
            catch (System.Exception ex)
            {
                return false;
            }
            return true;
        }

        /// <summary>
        /// 傳入Op的Tag取得該Op使用的刀子內的ERP
        /// </summary>
        /// <param name="title"></param>
        /// <returns></returns>
        public static string AskOperToolERPNumFromAttribute(Tag operTag, string title)
        {
            string toolERPNum = "";
            try
            {
                Tool tool;
                if (!AskOperTool(operTag, out tool))
                {
                    return toolERPNum = "";
                }
                toolERPNum = tool.GetStringAttribute(title);
            }
            catch (System.Exception ex)
            {
                return toolERPNum = "";
            }
            return toolERPNum;
        }

        /// <summary>
        /// 取得Operation父層名稱
        /// </summary>
        /// <param name="operTag"></param>
        /// <param name="operProgramName"></param>
        /// <returns></returns>
        public static bool AskOperProgramNameFromTag(Tag operTag, out string operProgramName)
        {
            operProgramName = "";
            try
            {
                Tag bb;
                theUfSession.Oper.AskProgramGroup(operTag, out bb);
                NXOpen.CAM.NCGroup gg = (NXOpen.CAM.NCGroup)NXObjectManager.Get(bb);
                operProgramName = gg.Name;
            }
            catch (System.Exception ex)
            {
                return false;
            }
            return true;
        }

        /// <summary>
        /// 取得Operation座標系名稱
        /// </summary>
        /// <param name="operTag"></param>
        /// <param name="operGeometryName"></param>
        /// <returns></returns>
        public static bool AskOperGeometryNameFromTag(Tag operTag, out string operGeometryName)
        {
            operGeometryName = "";
            try
            {
                Tag bb;
                theUfSession.Oper.AskGeomGroup(operTag, out bb);
                NXOpen.CAM.OrientGeometry gg = (NXOpen.CAM.OrientGeometry)NXObjectManager.Get(bb);
                operGeometryName = gg.Name;
            }
            catch (System.Exception ex)
            {
                return false;
            }
            return true;
        }

        /// <summary>
        /// 取得Operation的HolderDescription名稱
        /// </summary>
        /// <param name="singleOper"></param>
        /// <param name="operHolderDescription"></param>
        /// <returns></returns>
        public static string AskOperHolderDescription(NXOpen.CAM.Operation singleOper)
        {
            string operHolderDescription = "";
            try
            {
                NCGroup singleOperParent = singleOper.GetParent(CAMSetup.View.MachineTool);//由Oper取得刀子
                NXOpen.CAM.Tool tool = (NXOpen.CAM.Tool)NXObjectManager.Get(singleOperParent.Tag);//取得Oper的刀子名稱
                Tool.Types type;
                Tool.Subtypes subtype;
                tool.GetTypeAndSubtype(out type, out subtype);
                //CaxLog.ShowListingWindow(type.ToString());
                //CaxLog.ShowListingWindow(subtype.ToString());
                //CaxLog.ShowListingWindow("---");
                //CaxLog.ShowListingWindow(singleOper.Name.ToString());
                if (type == Tool.Types.Barrel)
                {
                    NXOpen.CAM.BarrelToolBuilder ToolBuilder1;
                    ToolBuilder1 = workPart.CAMSetup.CAMGroupCollection.CreateBarrelToolBuilder(tool);
                    operHolderDescription = ToolBuilder1.TlHolderDescription;
                    return operHolderDescription;
                }
                else if (type == Tool.Types.Drill)
                {
                    NXOpen.CAM.DrillStdToolBuilder ToolBuilder1;
                    ToolBuilder1 = workPart.CAMSetup.CAMGroupCollection.CreateDrillStdToolBuilder(tool);
                    operHolderDescription = ToolBuilder1.TlHolderDescription;
                    //CaxLog.ShowListingWindow(operHolderDescription);
                    return operHolderDescription;
                }
                else if (type == Tool.Types.DrillSpcGroove)
                {
                    NXOpen.CAM.MillingToolBuilder ToolBuilder1;
                    ToolBuilder1 = workPart.CAMSetup.CAMGroupCollection.CreateMillToolBuilder(tool);
                    operHolderDescription = ToolBuilder1.TlHolderDescription;
                    return operHolderDescription;
                }
                else if (type == Tool.Types.Mill)
                {
                    NXOpen.CAM.MillingToolBuilder ToolBuilder1;
                    ToolBuilder1 = workPart.CAMSetup.CAMGroupCollection.CreateMillToolBuilder(tool);
                    operHolderDescription = ToolBuilder1.TlHolderDescription;
                    return operHolderDescription;
                }
                else if (type == Tool.Types.MillForm)
                {
                    NXOpen.CAM.MillingToolBuilder ToolBuilder1;
                    ToolBuilder1 = workPart.CAMSetup.CAMGroupCollection.CreateMillToolBuilder(tool);
                    operHolderDescription = ToolBuilder1.TlHolderDescription;
                    return operHolderDescription;
                }
                else if (type == Tool.Types.Solid)
                {
                    
                }
                else if (type == Tool.Types.Tcutter)
                {
                    NXOpen.CAM.MillingToolBuilder ToolBuilder1;
                    ToolBuilder1 = workPart.CAMSetup.CAMGroupCollection.CreateMillToolBuilder(tool);
                    operHolderDescription = ToolBuilder1.TlHolderDescription;
                    return operHolderDescription;
                }
                else if (type == Tool.Types.Thread)
                {
                    NXOpen.CAM.ThreadToolBuilder ToolBuilder1;
                    ToolBuilder1 = workPart.CAMSetup.CAMGroupCollection.CreateThreadToolBuilder(tool);
                    operHolderDescription = ToolBuilder1.TlDescription; //CaxLog.ShowListingWindow(operHolderDescription);
                    //operHolderDescription = ToolBuilder1.HolderHand.ToString(); CaxLog.ShowListingWindow(operHolderDescription);
                    //operHolderDescription = ToolBuilder1.HolderGrooveHand.ToString(); CaxLog.ShowListingWindow(operHolderDescription);
                    //CaxLog.ShowListingWindow("---");
                    return operHolderDescription;
                }
                else if (type == Tool.Types.Turn)
                {
                    NXOpen.CAM.TurningToolBuilder ToolBuilder1;
                    ToolBuilder1 = workPart.CAMSetup.CAMGroupCollection.CreateTurnToolBuilder(tool);
                    operHolderDescription = ToolBuilder1.TlDescription; //CaxLog.ShowListingWindow(operHolderDescription);
                    //operHolderDescription = ToolBuilder1.HolderHand.ToString(); CaxLog.ShowListingWindow(operHolderDescription);
                    //operHolderDescription = ToolBuilder1.HolderGrooveHand.ToString(); CaxLog.ShowListingWindow(operHolderDescription);
                    //CaxLog.ShowListingWindow("---");
                    return operHolderDescription;
                }
                else if (type == Tool.Types.Wedm)
                {
                    
                }
                else if (type == Tool.Types.Groove)
                {
                    NXOpen.CAM.GrooveToolBuilder ToolBuilder1;
                    ToolBuilder1 = workPart.CAMSetup.CAMGroupCollection.CreateGrooveToolBuilder(tool);
                    operHolderDescription = ToolBuilder1.TlDescription; //CaxLog.ShowListingWindow(operHolderDescription);
                    //operHolderDescription = ToolBuilder1.HolderHand.ToString(); CaxLog.ShowListingWindow(operHolderDescription);
                    //operHolderDescription = ToolBuilder1.HolderGrooveHand.ToString(); CaxLog.ShowListingWindow(operHolderDescription);
                    //CaxLog.ShowListingWindow("---");
                    return operHolderDescription;
                }
                
            }
            catch (System.Exception ex)
            {
                operHolderDescription = "尚未建立Builder存取資料";
                return operHolderDescription;
            }
            return operHolderDescription;
        }

        /// <summary>
        /// 取得ToolNumber
        /// </summary>
        /// <param name="singleOper"></param>
        /// <param name="operHolderDescription"></param>
        /// <returns></returns>
        public static string AskOperToolNumber(NXOpen.CAM.Operation singleOper)
        {
            string operToolNumber = "0";
            try
            {
                NCGroup singleOperParent = singleOper.GetParent(CAMSetup.View.MachineTool);//由Oper取得刀子
                NXOpen.CAM.Tool tool = (NXOpen.CAM.Tool)NXObjectManager.Get(singleOperParent.Tag);//取得Oper的刀子名稱

                return operToolNumber = CaxOper.AskToolNumber(tool);

                /*註解(如果AskToolNumber測試失敗，則回覆程式碼)
                Tool.Types type;
                Tool.Subtypes subtype;
                tool.GetTypeAndSubtype(out type, out subtype);
                
                if (type == Tool.Types.Barrel)
                {
                    NXOpen.CAM.BarrelToolBuilder ToolBuilder1;
                    ToolBuilder1 = workPart.CAMSetup.CAMGroupCollection.CreateBarrelToolBuilder(tool);
                    operToolNumber = ToolBuilder1.TlNumberBuilder.Value.ToString();
                    return operToolNumber;
                }
                else if (type == Tool.Types.Drill)
                {
                    NXOpen.CAM.DrillStdToolBuilder ToolBuilder1;
                    ToolBuilder1 = workPart.CAMSetup.CAMGroupCollection.CreateDrillStdToolBuilder(tool);
                    operToolNumber = ToolBuilder1.TlNumberBuilder.Value.ToString();
                    return operToolNumber;
                }
                else if (type == Tool.Types.DrillSpcGroove)
                {
                    NXOpen.CAM.MillingToolBuilder ToolBuilder1;
                    ToolBuilder1 = workPart.CAMSetup.CAMGroupCollection.CreateMillToolBuilder(tool);
                    operToolNumber = ToolBuilder1.TlNumberBuilder.Value.ToString();
                    return operToolNumber;
                }
                else if (type == Tool.Types.Mill)
                {
                    NXOpen.CAM.MillingToolBuilder ToolBuilder1;
                    ToolBuilder1 = workPart.CAMSetup.CAMGroupCollection.CreateMillToolBuilder(tool);
                    operToolNumber = ToolBuilder1.TlNumberBuilder.Value.ToString();
                    return operToolNumber;
                }
                else if (type == Tool.Types.MillForm)
                {
                    NXOpen.CAM.MillingToolBuilder ToolBuilder1;
                    ToolBuilder1 = workPart.CAMSetup.CAMGroupCollection.CreateMillToolBuilder(tool);
                    operToolNumber = ToolBuilder1.TlNumberBuilder.Value.ToString();
                    return operToolNumber;
                }
                else if (type == Tool.Types.Solid)
                {

                }
                else if (type == Tool.Types.Tcutter)
                {
                    NXOpen.CAM.MillingToolBuilder ToolBuilder1;
                    ToolBuilder1 = workPart.CAMSetup.CAMGroupCollection.CreateMillToolBuilder(tool);
                    operToolNumber = ToolBuilder1.TlNumberBuilder.Value.ToString();
                    return operToolNumber;
                }
                else if (type == Tool.Types.Thread)
                {
                    NXOpen.CAM.ThreadToolBuilder ToolBuilder1;
                    ToolBuilder1 = workPart.CAMSetup.CAMGroupCollection.CreateThreadToolBuilder(tool);
                    operToolNumber = ToolBuilder1.TlNumberBuilder.Value.ToString();
                    return operToolNumber;
                }
                else if (type == Tool.Types.Turn)
                {
                    NXOpen.CAM.TurningToolBuilder ToolBuilder1;
                    ToolBuilder1 = workPart.CAMSetup.CAMGroupCollection.CreateTurnToolBuilder(tool);
                    operToolNumber = ToolBuilder1.TlNumberBuilder.Value.ToString(); 
                    return operToolNumber;
                }
                else if (type == Tool.Types.Wedm)
                {

                }
                else if (type == Tool.Types.Groove)
                {
                    NXOpen.CAM.GrooveToolBuilder ToolBuilder1;
                    ToolBuilder1 = workPart.CAMSetup.CAMGroupCollection.CreateGrooveToolBuilder(tool);
                    operToolNumber = ToolBuilder1.TlNumberBuilder.Value.ToString();
                    return operToolNumber;
                }
                */
            }
            catch (System.Exception ex)
            {
                operToolNumber = "尚未建立Builder存取資料";
                return operToolNumber;
            }
            return operToolNumber;
        }

        /// <summary>
        /// 傳入型別為NXOpen.CAM.Tool的物件，取得該Tool的刀號
        /// </summary>
        /// <param name="tool"></param>
        /// <returns></returns>
        public static string AskToolNumber(CAMObject tool)
        {
            string ToolNumber = "";
            try
            {
                if (tool is NXOpen.CAM.Tool)
                {
                    ToolBuilder toolBuilder = displayPart.CAMSetup.CAMGroupCollection.CreateDrillStepToolBuilder(tool);
                    ToolNumber = toolBuilder.TlNumberBuilder.Value.ToString();
                    return ToolNumber;
                }
                else
                {
                    return ToolNumber = "";
                }
            }
            catch (System.Exception ex)
            {
                return ToolNumber = "";
            }
        }

        /// <summary>
        /// 取得Operation的加工長度
        /// </summary>
        /// <param name="singleOper"></param>
        /// <returns></returns>
        public static string AskOperCuttingLength(NXOpen.CAM.Operation singleOper)
        {
            string CuttingLength = "";
            try
            {
                return CuttingLength = singleOper.GetToolpathCuttingLength().ToString();
            }
            catch (System.Exception ex)
            {
                return CuttingLength;
            }
        }

        /// <summary>
        /// 取得Operation的加工時間
        /// </summary>
        /// <param name="singleOper"></param>
        /// <returns></returns>
        public static string AskOperCuttingTime(NXOpen.CAM.Operation singleOper)
        {
            string CuttingTime = "";
            try
            {
                return CuttingTime = singleOper.GetToolpathCuttingTime().ToString();
            }
            catch (System.Exception ex)
            {
                return CuttingTime;
            }
        }

        /// <summary>
        /// 取得Operation的刀具進給
        /// </summary>
        /// <param name="singleOper"></param>
        /// <returns></returns>
        public static string AskOperToolFeed(NXOpen.CAM.Operation singleOper)
        {
            string OperToolFeed = "";
            try
            {
                NXOpen.CAM.CAMObject[] params1 = new NXOpen.CAM.CAMObject[1];
                params1[0] = singleOper;
                NXOpen.CAM.ObjectsFeedsBuilder objectsFeedsBuilder1;
                objectsFeedsBuilder1 = workPart.CAMSetup.CreateFeedsBuilder(params1);
                OperToolFeed = objectsFeedsBuilder1.FeedsBuilder.FeedCutBuilder.Value.ToString(); 
                //CaxLog.ShowListingWindow(OperToolFeed);
                //OperToolFeed = (Convert.ToDouble(AskOperCuttingLength(singleOper)) / Convert.ToDouble(AskOperCuttingTime(singleOper))).ToString();
                return OperToolFeed;
            }
            catch (System.Exception ex)
            {
                return OperToolFeed;
            }
        }

        /// <summary>
        /// 取得ToolSpeed
        /// </summary>
        /// <param name="singleOper"></param>
        /// <returns></returns>
        public static string AskOperToolSpeed(NXOpen.CAM.Operation singleOper)
        {
            string OperToolSpeed = "";
            //double maxSpindleSpeed;
            try
            {
                NXOpen.CAM.CAMObject[] params1 = new NXOpen.CAM.CAMObject[1];
                params1[0] = singleOper;
                NXOpen.CAM.ObjectsFeedsBuilder objectsFeedsBuilder1;
                objectsFeedsBuilder1 = workPart.CAMSetup.CreateFeedsBuilder(params1);
                string OutPutMode = objectsFeedsBuilder1.FeedsBuilder.SpindleModeBuilder.Value.ToString();
                if (OutPutMode == "0")
                {
                    OperToolSpeed = "S：" + objectsFeedsBuilder1.FeedsBuilder.SpindleRpmBuilder.Value.ToString();
                }
                else if (OutPutMode == "1" || OutPutMode == "2")
                {
                    OperToolSpeed = "V：" + objectsFeedsBuilder1.FeedsBuilder.SurfaceSpeedBuilder.Value.ToString();
                    //JOperation jOper = JOperationFactory.CreateJOperation(singleOper);
                    //maxSpindleSpeed = jOper.GetMaximunSpindleSpeed();
                }
                return OperToolSpeed;

                
            }
            catch (System.Exception ex)
            {
                return OperToolSpeed;
            }
        }

        /// <summary>
        /// 取得Operation的預留量
        /// </summary>
        /// <param name="singleOper"></param>
        /// <param name="StockStr"></param>
        /// <param name="FloorStockStr"></param>
        /// <returns></returns>
        public static bool AskOperStock(NXOpen.CAM.Operation singleOper, out string StockStr, out string FloorStockStr)
        {
            StockStr = "0";
            FloorStockStr = "0";
            try
            {
                //CaxLog.ShowListingWindow(singleOper.GetType().ToString());
                if (singleOper.GetType().ToString() == "NXOpen.CAM.PlanarMilling")
                {
                    NXOpen.CAM.PlanarMillingBuilder Builder1; 
                    Builder1 = workPart.CAMSetup.CAMOperationCollection.CreatePlanarMillingBuilder(singleOper);
                    StockStr = Builder1.CutParameters.PartStock.Value.ToString();
                    FloorStockStr = Builder1.CutParameters.FloorStock.Value.ToString();
                }
                else if (singleOper.GetType().ToString() == "NXOpen.CAM.PlungeMilling")
                {
                    NXOpen.CAM.PlungeMillingBuilder Builder1; 
                    Builder1 = workPart.CAMSetup.CAMOperationCollection.CreatePlungeMillingBuilder(singleOper);
                    StockStr = Builder1.CutParameters.PartStock.Value.ToString();
                    FloorStockStr = Builder1.CutParameters.FloorStock.Value.ToString();
                }
                else if (singleOper.GetType().ToString() == "NXOpen.CAM.RoughTurning")
                {
                    NXOpen.CAM.RoughTurningBuilder Builder1; 
                    Builder1 = workPart.CAMSetup.CAMOperationCollection.CreateRoughTurningBuilder(singleOper);
                    StockStr = Builder1.CutParameters.PartStock.Value.ToString(); 
                }
                else if (singleOper.GetType().ToString() == "NXOpen.CAM.FinishTurning")
                {
                    NXOpen.CAM.FinishTurningBuilder Builder1; 
                    Builder1 = workPart.CAMSetup.CAMOperationCollection.CreateFinishTurningBuilder(singleOper);
                    StockStr = Builder1.CutParameters.PartStock.Value.ToString(); 
                }
                else if (singleOper.GetType().ToString() == "NXOpen.CavityMilling")
                {
                    NXOpen.CAM.CavityMillingBuilder Builder1;
                    Builder1 = workPart.CAMSetup.CAMOperationCollection.CreateCavityMillingBuilder(singleOper);
                    StockStr = Builder1.CutParameters.PartStock.Value.ToString();
                    FloorStockStr = Builder1.CutParameters.FloorStock.Value.ToString();
                }
                else if (singleOper.GetType().ToString() == "NXOpen.FaceMilling")
                {
                    NXOpen.CAM.FaceMillingBuilder Builder1;
                    Builder1 = workPart.CAMSetup.CAMOperationCollection.CreateFaceMillingBuilder(singleOper);
                    StockStr = Builder1.CutParameters.PartStock.Value.ToString();
                    FloorStockStr = Builder1.CutParameters.FloorStock.Value.ToString();
                }
                else if (singleOper.GetType().ToString() == "NXOpen.HoleMaking")
                {
                    NXOpen.CAM.HoleMakingBuilder Builder1;
                    Builder1 = workPart.CAMSetup.CAMOperationCollection.CreateHoleMakingBuilder(singleOper);
                    StockStr = Builder1.CutParameters.PartStock.Value.ToString();
                }
                
                
            }
            catch (System.Exception ex)
            {
                return false;
            }
            return true;
        }

        /// <summary>
        /// 取得Operation總加工時間
        /// </summary>
        /// <param name="singleOper"></param>
        /// <returns></returns>
        public static string AskOperTotalCuttingTime(NXOpen.CAM.Operation singleOper)
        {
            string TotalCuttingTime = "";
            try
            {
                return TotalCuttingTime = singleOper.GetToolpathTime().ToString();
            }
            catch (System.Exception ex)
            {
                return TotalCuttingTime;
            }
        }

        /// <summary>
        /// 取得Operation總加工長度
        /// </summary>
        /// <param name="singleOper"></param>
        /// <returns></returns>
        public static string AskOperTotalCuttingLength(NXOpen.CAM.Operation singleOper)
        {
            string TotalCuttingLength = "";
            try
            {
                return TotalCuttingLength = singleOper.GetToolpathLength().ToString();
            }
            catch (System.Exception ex)
            {
                return TotalCuttingLength;
            }
        }

        /// <summary>
        /// 輸出Post
        /// </summary>
        /// <param name="OperObject">程式物件</param>
        /// <param name="PostName">後處理器名稱</param>
        /// <param name="OutputPath">輸出路徑</param>
        public static bool CreatePost(NXOpen.CAM.CAMObject OperObject, string PostName, string OutputPath)
        {
            try
            {

                NXOpen.CAM.CAMObject[] objects1 = new NXOpen.CAM.CAMObject[1];
                objects1[0] = OperObject;
                workPart.CAMSetup.Postprocess(objects1, PostName, OutputPath, NXOpen.CAM.CAMSetup.OutputUnits.PostDefined);
            }
            catch (NXOpen.NXException NXex)
            {
                if (NXex.ErrorCode == 11)
                {
                    return true;
                }
            }
            catch (System.Exception ex)
            {
                return false;
            }
            return true;
        }

        /// <summary>
        /// 取得NCGroupMessage
        /// </summary>
        /// <param name="nCGroup"></param>
        /// <returns></returns>
        public static string AskNCGroupMessage(NXOpen.CAM.NCGroup nCGroup)
        {
            string NCGroupMessage = "";
            try
            {
                NXOpen.CAM.ProgramOrderGroupBuilder programOrderGroupBuilder1;
                programOrderGroupBuilder1 = workPart.CAMSetup.CAMGroupCollection.CreateProgramOrderGroupBuilder(nCGroup);
                TaggedObject taggedObject1;
                taggedObject1 = programOrderGroupBuilder1.StartUdeSet.UdeList.FindItem(0);
                NXOpen.CAM.Ude ude1 = (NXOpen.CAM.Ude)taggedObject1;
                NXOpen.CAM.UdeParameter udeParameter1;
                udeParameter1 = ude1.GetParameter("operator_message");
                NCGroupMessage = udeParameter1.StringValue;
                programOrderGroupBuilder1.Destroy();

                return NCGroupMessage;
            }
            catch (System.Exception ex)
            {
                return NCGroupMessage;
            }
        }
    }
}
