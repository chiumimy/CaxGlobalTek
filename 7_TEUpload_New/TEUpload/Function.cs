using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using NXOpen.CAM;
using NXOpen;
using NXOpen.UF;
using System.Windows.Forms;
using CaxGlobaltek;
using NHibernate;

namespace TEUpload
{
    public class Function
    {
        public static ISession session = MyHibernateHelper.SessionFactory.OpenSession();
        public static Session theSession = Session.GetSession();
        public static UI theUI;
        public static UFSession theUfSession = UFSession.GetUFSession();

        public struct OperData
        {
            public string OperName { get; set; }
            public string ToolName { get; set; }
            public string HolderDescription { get; set; }
            public string CuttingLength { get; set; }
            public string CuttingTime { get; set; }
            public string ToolFeed { get; set; }
            public string ToolNumber { get; set; }
            public string ToolSpeed { get; set; }
            public string PartStock { get; set; }
            public string PartFloorStock { get; set; }
            public string CutterLife { get; set; }
            public string Extension { get; set; }
        }

        public static bool GetNCProgramData(NCGroup[] NCGroupAry, out Dictionary<string, OperData> DicNCData)
        {
            DicNCData = new Dictionary<string, OperData>();
            try
            {
                foreach (NXOpen.CAM.NCGroup ncGroup in NCGroupAry)
                {
                    int type, subtype;
                    theUfSession.Obj.AskTypeAndSubtype(ncGroup.Tag, out type, out subtype);

                    //此處比對是否為Program群組
                    if (type != UFConstants.UF_machining_task_type)
                        continue;

                    if (!ncGroup.Name.Contains("OP"))
                    {
                        MessageBox.Show("請先手動將Group名稱：" + ncGroup.Name + "，改為正確格式，再重新啟動功能！");
                        return false;
                    }

                    //取得此NCGroup下的所有Oper
                    CAMObject[] OperGroup = ncGroup.GetMembers();
                    foreach (NXOpen.CAM.Operation item in OperGroup)
                    {
                        string StockStr = "", FloorstockStr = "";
                        CaxOper.AskOperStock(item, out StockStr, out FloorstockStr);

                        bool cheValue;
                        OperData sOperData = new OperData();
                        cheValue = DicNCData.TryGetValue(ncGroup.Name, out sOperData);
                        if (!cheValue)
                        {
                            sOperData.OperName = item.Name;
                            sOperData.ToolName = CaxOper.AskOperToolNameFromTag(item.Tag);
                            sOperData.HolderDescription = CaxOper.AskOperHolderDescription(item);
                            sOperData.CuttingLength = Convert.ToDouble(CaxOper.AskOperTotalCuttingLength(item)).ToString("f3");
                            sOperData.ToolFeed = Math.Round(Convert.ToDouble(CaxOper.AskOperToolFeed(item)), 3, MidpointRounding.AwayFromZero).ToString();
                            sOperData.CuttingTime = Math.Ceiling((Convert.ToDouble(CaxOper.AskOperTotalCuttingTime(item)) * 60)).ToString();//因為進給單位mmpm，距離單位mm，將進給的60放來這邊乘
                            sOperData.ToolNumber = "T" + CaxOper.AskOperToolNumber(item);
                            sOperData.ToolSpeed = CaxOper.AskOperToolSpeed(item);
                            sOperData.PartStock = StockStr;
                            sOperData.PartFloorStock = FloorstockStr;
                            sOperData.CutterLife = CaxOper.AskOperToolERPNumFromAttribute(item.Tag, "GTTL_CUTTER_LIFE");
                            sOperData.Extension = CaxOper.AskOperToolERPNumFromAttribute(item.Tag, "GTTL_EXTENTION");
                            DicNCData.Add(ncGroup.Name, sOperData);
                        }
                        else
                        {
                            sOperData.OperName = sOperData.OperName + "," + item.Name;
                            sOperData.ToolName = sOperData.ToolName + "," + CaxOper.AskOperToolNameFromTag(item.Tag);
                            sOperData.HolderDescription = sOperData.HolderDescription + "," + CaxOper.AskOperHolderDescription(item);
                            sOperData.CuttingLength = sOperData.CuttingLength + "," + Convert.ToDouble(CaxOper.AskOperTotalCuttingLength(item)).ToString("f3");
                            sOperData.ToolFeed = sOperData.ToolFeed + "," + Math.Round(Convert.ToDouble(CaxOper.AskOperToolFeed(item)), 3, MidpointRounding.AwayFromZero).ToString();
                            sOperData.CuttingTime = sOperData.CuttingTime + "," + Math.Ceiling((Convert.ToDouble(CaxOper.AskOperTotalCuttingTime(item)) * 60)).ToString();//因為進給單位mmpm，距離單位mm，將進給的60放來這邊乘
                            sOperData.ToolNumber = sOperData.ToolNumber + "," + "T" + CaxOper.AskOperToolNumber(item);
                            sOperData.ToolSpeed = sOperData.ToolSpeed + "," + CaxOper.AskOperToolSpeed(item);
                            sOperData.PartStock = sOperData.PartStock + "," + StockStr;
                            sOperData.PartFloorStock = sOperData.PartFloorStock + "," + FloorstockStr;
                            sOperData.CutterLife = sOperData.CutterLife + "," + CaxOper.AskOperToolERPNumFromAttribute(item.Tag, "GTTL_CUTTER_LIFE");
                            sOperData.Extension = sOperData.Extension + "," + CaxOper.AskOperToolERPNumFromAttribute(item.Tag, "GTTL_EXTENTION");
                            DicNCData[ncGroup.Name] = sOperData;
                        }
                    }
                }
            }
            catch (System.Exception ex)
            {
                return false;
            }
            return true;
        }
        public static bool GetCom_PEMain(PartInfo sPartInfo, out Com_PEMain comPEMain)
        {
            comPEMain = new Com_PEMain();
            try
            {
                comPEMain = session.QueryOver<Com_PEMain>().Where(x => x.partName == sPartInfo.PartNo)
                                                            .Where(x => x.customerVer == sPartInfo.CusRev)
                                                            .Where(x => x.opVer == sPartInfo.OpRev)
                                                            .SingleOrDefault<Com_PEMain>();
                if (comPEMain == null)
                {
                    return false;
                }
            }
            catch (System.Exception ex)
            {
                return false;
            }
            return true;
        }
        public static bool GetCom_PartOperation(PartInfo sPartInfo, Com_PEMain comPEMain, out Com_PartOperation comPartOperation)
        {
            comPartOperation = new Com_PartOperation();
            try
            {
                comPartOperation = session.QueryOver<Com_PartOperation>()
                                           .Where(x => x.comPEMain.peSrNo == comPEMain.peSrNo)
                                           .And(x => x.operation1 == sPartInfo.OpNum)
                                           .SingleOrDefault<Com_PartOperation>();
                if (comPartOperation == null)
                {
                    return false;
                }
            }
            catch (System.Exception ex)
            {
                return false;
            }
            return true;
        }
    }
}
