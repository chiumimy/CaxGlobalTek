using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using CaxGlobaltek;

namespace ExportShopDoc
{
    public class Database
    {
        public static IList<Com_ShopDoc> comShopDoc = new List<Com_ShopDoc>();
        public static IList<Com_ControlDimen> comControlDimen = new List<Com_ControlDimen>();

        public struct SplitData
        {
            public string[] OperName { get; set; }
            public string[] OperToolNo { get; set; }
            public string[] OperToolID { get; set; }
            public string[] OperHolderID { get; set; }
            public string[] OperToolFeed { get; set; }
            public string[] OperCuttingTime { get; set; }
            public string[] OperToolSpeed { get; set; }
            public string[] OperPartStock { get; set; }
            public string[] OperPartFloorStock { get; set; }
            public string[] OperCuttingLength { get; set; }
            public string[] OperCutterLife { get; set; }
            public string[] OperExtension { get; set; }
        }

        public static bool GetSplitData(ExportShopDoc.ExportShopDocDlg.OperData input, out SplitData sSplitData)
        {
            sSplitData = new SplitData();
            try
            {
                sSplitData.OperName = input.OperName.Split(',');
                sSplitData.OperToolID = input.ToolName.Split(',');
                sSplitData.OperHolderID = input.HolderDescription.Split(',');
                sSplitData.OperToolFeed = input.ToolFeed.Split(',');
                sSplitData.OperCuttingTime = input.CuttingTime.Split(',');
                sSplitData.OperToolNo = input.ToolNumber.Split(',');
                sSplitData.OperToolSpeed = input.ToolSpeed.Split(',');
                sSplitData.OperPartStock = input.PartStock.Split(',');
                sSplitData.OperPartFloorStock = input.PartFloorStock.Split(',');
                sSplitData.OperCuttingLength = input.CuttingLength.Split(',');
                sSplitData.OperCutterLife = input.CutterLife.Split(',');
                sSplitData.OperExtension = input.Extension.Split(',');
            }
            catch (System.Exception ex)
            {
                return false;
            }
            return true;
        }
    }
}
