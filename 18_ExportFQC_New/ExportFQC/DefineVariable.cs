using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace ExportFQC
{
    public class TextData
    {
        //共用變數
        public string Gauge { get; set; }
        public string Type { get; set; }
        public string Location { get; set; }
        public string Frequency { get; set; }
        public string BallonNum { get; set; }

        public string MainText { get; set; }
        public string BeforeText { get; set; }
        public string AfterText { get; set; }
        public string AboveText { get; set; }
        public string BelowText { get; set; }
        public string UpperTol { get; set; }
        public string LowerTol { get; set; }
        public string TolType { get; set; }

        //FcfData
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

    public struct RowColumn
    {
        //表單資訊
        public int PartNoRow { get; set; }
        public int PartNoColumn { get; set; }
        public int OISRow { get; set; }
        public int OISColumn { get; set; }
        public int DateRow { get; set; }
        public int DateColumn { get; set; }

        //泡泡位置
        public int BallonRow { get; set; }
        public int BallonColumn { get; set; }

        //泡泡相對圖表位置
        public int LocationRow { get; set; }
        public int LocationColumn { get; set; }

        //檢具
        public int GaugeRow { get; set; }
        public int GaugeColumn { get; set; }

        //頻率
        public int FrequencyRow { get; set; }
        public int FrequencyColumn { get; set; }

        //Dimension
        public int DimensionRow { get; set; }
        public int DimensionColumn { get; set; }

        /*
        //第1格
        public int CharacteristicRow { get; set; }
        public int CharacteristicColumn { get; set; }

        //第2格
        public int ZoneShapeRow { get; set; }
        public int ZoneShapeColumn { get; set; }
        public int BeforeTextRow { get; set; }
        public int BeforeTextColumn { get; set; }

        //第3格
        public int ToleranceValueRow { get; set; }
        public int ToleranceValueColumn { get; set; }
        public int MainTextRow { get; set; }
        public int MainTextColumn { get; set; }

        //第4格
        public int MaterialModifierRow { get; set; }
        public int MaterialModifierColumn { get; set; }
        public int ToleranceSymbolRow { get; set; }
        public int ToleranceSymbolColumn { get; set; }

        //第5格
        public int UpperTolRow { get; set; }
        public int UpperTolColumn { get; set; }

        //第6格
        public int PrimaryDatumRow { get; set; }
        public int PrimaryDatumColumn { get; set; }

        //第7格
        public int PrimaryMaterialModifierRow { get; set; }
        public int PrimaryMaterialModifierColumn { get; set; }

        //第8格
        public int SecondaryDatumRow { get; set; }
        public int SecondaryDatumColumn { get; set; }

        //第9格
        public int SecondaryMaterialModifierRow { get; set; }
        public int SecondaryMaterialModifierColumn { get; set; }

        //第10格
        public int TertiaryDatumRow { get; set; }
        public int TertiaryDatumColumn { get; set; }

        //第11格
        public int TertiaryMaterialModifierRow { get; set; }
        public int TertiaryMaterialModifierColumn { get; set; }

        //Max
        public int MaxRow { get; set; }
        public int MaxColumn { get; set; }

        //Min
        public int MinRow { get; set; }
        public int MinColumn { get; set; }
        */

    }

    public class DefineVariable
    {
        public static int BallonCount = 0;
        public static Dictionary<string, TextData> DicDimenData = new Dictionary<string, TextData>();
        public static List<TextData> ListTextData = new List<TextData>();
        public static NXOpen.Drawings.DrawingSheet FirstDrawingSheet;
        public static string FQCPath = "";
        public static string Is_Local = "";

        public static void GetExcelRowColumn(int i, out RowColumn sRowColumn)
        {
            sRowColumn = new RowColumn();
            sRowColumn.PartNoRow = 5;
            sRowColumn.PartNoColumn = 4;
            sRowColumn.OISRow = 5;
            sRowColumn.OISColumn = 6;
            sRowColumn.DateRow = 5;
            sRowColumn.DateColumn = 19;

            int currentNo = (i % 13);

            int RowNo = 0;

            if (currentNo == 0)
            {
                RowNo = 8;
            }
            else if (currentNo == 1)
            {
                RowNo = 9;
            }
            else if (currentNo == 2)
            {
                RowNo = 10;
            }
            else if (currentNo == 3)
            {
                RowNo = 11;
            }
            else if (currentNo == 4)
            {
                RowNo = 12;
            }
            else if (currentNo == 5)
            {
                RowNo = 13;
            }
            else if (currentNo == 6)
            {
                RowNo = 14;
            }
            else if (currentNo == 7)
            {
                RowNo = 15;
            }
            else if (currentNo == 8)
            {
                RowNo = 16;
            }
            else if (currentNo == 9)
            {
                RowNo = 17;
            }
            else if (currentNo == 10)
            {
                RowNo = 18;
            }
            else if (currentNo == 11)
            {
                RowNo = 19;
            }
            else if (currentNo == 12)
            {
                RowNo = 20;
            }
            //else if (currentNo == 13)
            //{
            //    RowNo = 21;
            //}
            //else if (currentNo == 14)
            //{
            //    RowNo = 22;
            //}
            //else if (currentNo == 15)
            //{
            //    RowNo = 23;
            //}
            //else if (currentNo == 16)
            //{
            //    RowNo = 24;
            //}
            //else if (currentNo == 17)
            //{
            //    RowNo = 25;
            //}

            sRowColumn.DimensionRow = RowNo;
            sRowColumn.DimensionColumn = 4;

            sRowColumn.GaugeRow = RowNo;
            sRowColumn.GaugeColumn = 5;

            sRowColumn.FrequencyRow = RowNo;
            sRowColumn.FrequencyColumn = 7;

            sRowColumn.BallonRow = RowNo;
            sRowColumn.BallonColumn = 2;

            sRowColumn.LocationRow = RowNo;
            sRowColumn.LocationColumn = 3;
            //sRowColumn.CharacteristicRow = RowNo;
            //sRowColumn.CharacteristicColumn = 4;

            //sRowColumn.ZoneShapeRow = RowNo;
            //sRowColumn.ZoneShapeColumn = 5;
            //sRowColumn.BeforeTextRow = RowNo;
            //sRowColumn.BeforeTextColumn = 5;

            //sRowColumn.ToleranceValueRow = RowNo;
            //sRowColumn.ToleranceValueColumn = 6;
            //sRowColumn.MainTextRow = RowNo;
            //sRowColumn.MainTextColumn = 6;

            //sRowColumn.MaterialModifierRow = RowNo;
            //sRowColumn.MaterialModifierColumn = 7;
            //sRowColumn.ToleranceSymbolRow = RowNo;
            //sRowColumn.ToleranceSymbolColumn = 7;

            //sRowColumn.UpperTolRow = RowNo;
            //sRowColumn.UpperTolColumn = 8;

            //sRowColumn.PrimaryDatumRow = RowNo;
            //sRowColumn.PrimaryDatumColumn = 9;

            //sRowColumn.PrimaryMaterialModifierRow = RowNo;
            //sRowColumn.PrimaryMaterialModifierColumn = 10;

            //sRowColumn.SecondaryDatumRow = RowNo;
            //sRowColumn.SecondaryDatumColumn = 11;

            //sRowColumn.SecondaryMaterialModifierRow = RowNo;
            //sRowColumn.SecondaryMaterialModifierColumn = 12;

            //sRowColumn.TertiaryDatumRow = RowNo;
            //sRowColumn.TertiaryDatumColumn = 13;

            //sRowColumn.TertiaryMaterialModifierRow = RowNo;
            //sRowColumn.TertiaryMaterialModifierColumn = 14;

            //sRowColumn.MaxRow = RowNo;
            //sRowColumn.MaxColumn = 15;

            //sRowColumn.MinRow = RowNo;
            //sRowColumn.MinColumn = 16;

            //sRowColumn.GaugeRow = RowNo;
            //sRowColumn.GaugeColumn = 17;

            //sRowColumn.FrequencyRow = RowNo;
            //sRowColumn.FrequencyColumn = 19;

            //sRowColumn.BallonRow = RowNo;
            //sRowColumn.BallonColumn = 2;

            //sRowColumn.LocationRow = RowNo;
            //sRowColumn.LocationColumn = 3;
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

            /*
            if (NXData.Contains("<#C>"))
            {
                ExcelSymbol = NXData.Replace("<#C>", "w");
            }
            if (NXData.Contains("<#B>"))
            {
                ExcelSymbol = NXData.Replace("<#B>", "v");
            }
            if (NXData.Contains("<#D>"))
            {
                ExcelSymbol = NXData.Replace("<#D>", "x");
            }
            if (NXData.Contains("<#E>"))
            {
                ExcelSymbol = NXData.Replace("<#E>", "y");
            }
            if (NXData.Contains("<#G>"))
            {
                ExcelSymbol = NXData.Replace("<#G>", "z");
            }
            if (NXData.Contains("<#F>"))
            {
                ExcelSymbol = NXData.Replace("<#F>", "o");
            }
            if (NXData.Contains("<$s>"))
            {
                ExcelSymbol = NXData.Replace("<$s>", "~");
            }
            if (NXData.Contains("<O>"))
            {
                ExcelSymbol = NXData.Replace("<O>", "n");
            }
            if (NXData.Contains("S<O>"))
            {
                ExcelSymbol = NXData.Replace("S<O>", "Sn");
            }
            if (NXData.Contains("&1"))
            {
                ExcelSymbol = NXData.Replace("&1", "u");
            }
            if (NXData.Contains("&2"))
            {
                ExcelSymbol = NXData.Replace("&2", "c");
            }
            if (NXData.Contains("&3"))
            {
                ExcelSymbol = NXData.Replace("&3", "e");
            }
            if (NXData.Contains("&4"))
            {
                ExcelSymbol = NXData.Replace("&4", "g");
            }
            if (NXData.Contains("&5"))
            {
                ExcelSymbol = NXData.Replace("&5", "k");
            }
            if (NXData.Contains("&6"))
            {
                ExcelSymbol = NXData.Replace("&6", "d");
            }
            if (NXData.Contains("&7"))
            {
                ExcelSymbol = NXData.Replace("&7", "a");
            }
            if (NXData.Contains("&8"))
            {
                ExcelSymbol = NXData.Replace("&8", "b");
            }
            if (NXData.Contains("&9"))
            {
                ExcelSymbol = NXData.Replace("&9", "f");
            }
            if (NXData.Contains("&10"))
            {
                ExcelSymbol = NXData.Replace("&10", "j");
            }
            if (NXData.Contains("&11"))
            {
                ExcelSymbol = NXData.Replace("&11", "r");
            }
            if (NXData.Contains("&12"))
            {
                ExcelSymbol = NXData.Replace("&12", "i");
            }
            if (NXData.Contains("&13"))
            {
                ExcelSymbol = NXData.Replace("&13", "h");
            }
            if (NXData.Contains("&15"))
            {
                ExcelSymbol = NXData.Replace("&15", "t");
            }
            if (NXData.Contains("<M>"))
            {
                ExcelSymbol = NXData.Replace("<M>", "m");
            }
            if (NXData.Contains("<E>"))
            {
                ExcelSymbol = NXData.Replace("<E>", "l");
            }
            if (NXData.Contains("<S>"))
            {
                ExcelSymbol = NXData.Replace("<S>", "s");
            }
            if (NXData.Contains("<P>"))
            {
                ExcelSymbol = NXData.Replace("<P>", "p");
            }
            if (NXData.Contains("<$t>"))
            {
                ExcelSymbol = NXData.Replace("<$t>", "±");
            }
            */

            return ExcelSymbol;
        }
    }
}
