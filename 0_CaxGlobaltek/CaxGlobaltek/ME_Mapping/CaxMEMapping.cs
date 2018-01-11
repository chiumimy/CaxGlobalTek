using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace CaxGlobaltek
{

    public class Com_MEMain
    {
        public virtual Int32 meSrNo { get; set; }
        public virtual Com_PartOperation comPartOperation { get; set; }
        //public virtual Sys_MEExcel sysMEExcel { get; set; }
        public virtual IList<Com_Dimension> comDimension { get; set; }
        public virtual string partDescription { get; set; }
        public virtual string createDate { get; set; }
        public virtual string material { get; set; }
        public virtual string draftingVer { get; set; }
    }

    public class Com_Dimension
    {
        public virtual Int32 dimensionSrNo { get; set; }
        public virtual Com_MEMain comMEMain { get; set; }
        public virtual IList<Com_PFMEA> comPFMEA { get; set; }
        public virtual string keyChara { get; set; }
        public virtual string productName { get; set; }
        public virtual string excelType { get; set; }
        public virtual string toolNoControl { get; set; }
        public virtual string checkLevel { get; set; }
        public virtual string balloonCount { get; set; }

        //Type=FcfData
        public virtual string characteristic { get; set; }
        public virtual string zoneShape { get; set; }
        public virtual string toleranceValue { get; set; }
        public virtual string materialModifier { get; set; }
        public virtual string primaryDatum { get; set; }
        public virtual string primaryMaterialModifier { get; set; }
        public virtual string secondaryDatum { get; set; }
        public virtual string secondaryMaterialModifier { get; set; }
        public virtual string tertiaryDatum { get; set; }
        public virtual string tertiaryMaterialModifier { get; set; }


        public virtual string dimensionType { get; set; }
        public virtual string aboveText { get; set; }
        public virtual string belowText { get; set; }
        public virtual string beforeText { get; set; }
        public virtual string afterText { get; set; }
        public virtual string toleranceSymbol { get; set; }
        public virtual string mainText { get; set; }
        public virtual string upTolerance { get; set; }
        public virtual string lowTolerance { get; set; }
        public virtual string x { get; set; }
        public virtual string chamferAngle { get; set; }
        public virtual string maxTolerance { get; set; }
        public virtual string minTolerance { get; set; }
        public virtual string draftingVer { get; set; }
        public virtual string draftingDate { get; set; }
        public virtual Int32 ballon { get; set; }
        public virtual string location { get; set; }
        public virtual string instrument { get; set; }
        public virtual string frequency { get; set; }
        public virtual string toleranceType { get; set; }
        public virtual Int32 customerBalloon { get; set; }
        public virtual string spcControl { get; set; }
        public virtual string size { get; set; }
        public virtual string freq { get; set; }
        public virtual string selfCheck_Size { get; set; }
        public virtual string selfCheck_Freq { get; set; }
    }

    public class Com_FixInspection
    {
        public virtual Int32 fixinsSrNo { get; set; }
        public virtual Com_PartOperation comPartOperation { get; set; }
        public virtual IList<Com_FixDimension> comFixDimension { get; set; }
        public virtual string fixinsDescription { get; set; }
        public virtual string fixinsERP { get; set; }
        public virtual string fixinsNo { get; set; }
        public virtual string fixPicPath { get; set; }
        public virtual string fixPartName { get; set; }
    }

    public class Com_FixDimension
    {
        public virtual Int32 fixDimensionSrNo { get; set; }
        public virtual Com_FixInspection comFixInspection { get; set; }
        public virtual string keyChara { get; set; }
        public virtual string productName { get; set; }
        public virtual string excelType { get; set; }
        public virtual string toolNoControl { get; set; }
        public virtual string checkLevel { get; set; }
        public virtual string balloonCount { get; set; }

        //Type=FcfData
        public virtual string characteristic { get; set; }
        public virtual string zoneShape { get; set; }
        public virtual string toleranceValue { get; set; }
        public virtual string materialModifier { get; set; }
        public virtual string primaryDatum { get; set; }
        public virtual string primaryMaterialModifier { get; set; }
        public virtual string secondaryDatum { get; set; }
        public virtual string secondaryMaterialModifier { get; set; }
        public virtual string tertiaryDatum { get; set; }
        public virtual string tertiaryMaterialModifier { get; set; }


        public virtual string dimensionType { get; set; }
        public virtual string aboveText { get; set; }
        public virtual string belowText { get; set; }
        public virtual string beforeText { get; set; }
        public virtual string afterText { get; set; }
        public virtual string toleranceSymbol { get; set; }
        public virtual string mainText { get; set; }
        public virtual string upTolerance { get; set; }
        public virtual string lowTolerance { get; set; }
        public virtual string x { get; set; }
        public virtual string chamferAngle { get; set; }
        public virtual string maxTolerance { get; set; }
        public virtual string minTolerance { get; set; }
        public virtual string draftingVer { get; set; }
        public virtual string draftingDate { get; set; }
        public virtual Int32 ballon { get; set; }
        public virtual string location { get; set; }
        public virtual string instrument { get; set; }
        public virtual string frequency { get; set; }
        public virtual string toleranceType { get; set; }
        public virtual Int32 customerBalloon { get; set; }
        public virtual string spcControl { get; set; }
        public virtual string size { get; set; }
        public virtual string freq { get; set; }
        public virtual string selfCheck_Size { get; set; }
        public virtual string selfCheck_Freq { get; set; }
    }

    



    public class Com_PFMEA
    {
        public virtual Int32 pFMEASrNo { get; set; }
        public virtual Com_Dimension comDimension { get; set; }
        public virtual string pFMData { get; set; }
        public virtual string pEoFData { get; set; }
        public virtual string sevData { get; set; }
        public virtual string classData { get; set; }
        public virtual string pCoFData { get; set; }
        public virtual string occurrenceData { get; set; }
        public virtual string preventionData { get; set; }
        public virtual string detectionData { get; set; }
        public virtual string detData { get; set; }
        public virtual string rpnData { get; set; }
    }

    public class Sys_MEExcel
    {
        public virtual Int32 meExcelSrNo { get; set; }
        public virtual string meExcelType { get; set; }
        public virtual IList<Com_MEMain> comMEMain { get; set; }
    }

    public class Sys_Material
    {
        public virtual Int32 materialSrNo { get; set; }
        public virtual string materialName { get; set; }
        public virtual string category { get; set; }
    }

    public class Sys_Instrument
    {
        public virtual Int32 instrumentSrNo { get; set; }
        public virtual string instrumentColor { get; set; }
        public virtual string instrumentName { get; set; }
        public virtual string instrumentEngName { get; set; }
    }

    public class Sys_Product
    {
        public virtual Int32 productSrNo { get; set; }
        public virtual string productName { get; set; }
    }
}
