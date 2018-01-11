using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using Microsoft.Office.Interop.Excel;
using System.Windows.Forms;
using System.Diagnostics;
using NXOpen;
using NXOpen.UF;

namespace CaxGlobaltek
{
    public class CaxExcel
    {
        //public static Session theSession = Session.GetSession();
        //public static UI theUI;
        //public static UFSession theUfSession = UFSession.GetUFSession();

        public class DimensionData
        {
            public string type { get; set; }

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
        public struct IPQCRowColumn
        {
            //表單資訊
            public int PartNoRow { get; set; }
            public int PartNoColumn { get; set; }
            public int OISRow { get; set; }
            public int OISColumn { get; set; }
            public int OISRevRow { get; set; }
            public int OISRevColumn { get; set; }
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
        }
        public struct SelfCheckRowColumn
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

        }
        public struct IQCRowColumn
        {
            //表單資訊
            public int PartNoRow { get; set; }
            public int PartNoColumn { get; set; }
            public int OISRow { get; set; }
            public int OISColumn { get; set; }
            public int DateRow { get; set; }
            public int DateColumn { get; set; }
            public int MaterialRow { get; set; }
            public int MaterialColumn { get; set; }

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
        }
        
        public static void GetIQCRowColumn(int i, out IQCRowColumn sRowColumn)
        {
            sRowColumn = new IQCRowColumn();
            sRowColumn.PartNoRow = 5;
            sRowColumn.PartNoColumn = 4;
            sRowColumn.MaterialRow = 8;
            sRowColumn.MaterialColumn = 8;
            //sRowColumn.OISRow = 5;
            //sRowColumn.OISColumn = 6;
            //sRowColumn.DateRow = 5;
            //sRowColumn.DateColumn = 19;

            int currentNo = (i % 12);

            int RowNo = 0;

            if (currentNo == 0)
            {
                RowNo = 11;
            }
            else if (currentNo == 1)
            {
                RowNo = 12;
            }
            else if (currentNo == 2)
            {
                RowNo = 13;
            }
            else if (currentNo == 3)
            {
                RowNo = 14;
            }
            else if (currentNo == 4)
            {
                RowNo = 15;
            }
            else if (currentNo == 5)
            {
                RowNo = 16;
            }
            else if (currentNo == 6)
            {
                RowNo = 17;
            }
            else if (currentNo == 7)
            {
                RowNo = 18;
            }
            else if (currentNo == 8)
            {
                RowNo = 19;
            }
            else if (currentNo == 9)
            {
                RowNo = 20;
            }
            else if (currentNo == 10)
            {
                RowNo = 21;
            }
            else if (currentNo == 11)
            {
                RowNo = 22;
            }
            //else if (currentNo == 12)
            //{
            //    RowNo = 20;
            //}
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
            sRowColumn.DimensionColumn = 3;

            sRowColumn.GaugeRow = RowNo;
            sRowColumn.GaugeColumn = 5;

            //sRowColumn.FrequencyRow = RowNo;
            //sRowColumn.FrequencyColumn = 7;

            sRowColumn.BallonRow = RowNo;
            sRowColumn.BallonColumn = 1;

            sRowColumn.LocationRow = RowNo;
            sRowColumn.LocationColumn = 2;
        }
        public static void GetSelfCheckRowColumn(int i, out SelfCheckRowColumn sRowColumn)
        {
            sRowColumn = new SelfCheckRowColumn();
            sRowColumn.PartNoRow = 5;
            sRowColumn.PartNoColumn = 4;
            sRowColumn.OISRow = 6;
            sRowColumn.OISColumn = 4;
            sRowColumn.DateRow = 4;
            sRowColumn.DateColumn = 3;

            int currentNo = (i % 11);

            int RowNo = 0;

            if (currentNo == 0)
            {
                RowNo = 9;
            }
            else if (currentNo == 1)
            {
                RowNo = 10;
            }
            else if (currentNo == 2)
            {
                RowNo = 11;
            }
            else if (currentNo == 3)
            {
                RowNo = 12;
            }
            else if (currentNo == 4)
            {
                RowNo = 13;
            }
            else if (currentNo == 5)
            {
                RowNo = 14;
            }
            else if (currentNo == 6)
            {
                RowNo = 15;
            }
            else if (currentNo == 7)
            {
                RowNo = 16;
            }
            else if (currentNo == 8)
            {
                RowNo = 17;
            }
            else if (currentNo == 9)
            {
                RowNo = 18;
            }
            else if (currentNo == 10)
            {
                RowNo = 19;
            }

            sRowColumn.DimensionRow = RowNo;
            sRowColumn.DimensionColumn = 3;

            sRowColumn.GaugeRow = RowNo;
            sRowColumn.GaugeColumn = 4;

            sRowColumn.FrequencyRow = RowNo;
            sRowColumn.FrequencyColumn = 8;

            sRowColumn.BallonRow = RowNo;
            sRowColumn.BallonColumn = 1;

            sRowColumn.LocationRow = RowNo;
            sRowColumn.LocationColumn = 2;

            /*
            sRowColumn.CharacteristicRow = RowNo;
            sRowColumn.CharacteristicColumn = 3;

            sRowColumn.ZoneShapeRow = RowNo;
            sRowColumn.ZoneShapeColumn = 4;
            sRowColumn.BeforeTextRow = RowNo;
            sRowColumn.BeforeTextColumn = 4;

            sRowColumn.ToleranceValueRow = RowNo;
            sRowColumn.ToleranceValueColumn = 5;
            sRowColumn.MainTextRow = RowNo;
            sRowColumn.MainTextColumn = 5;

            sRowColumn.MaterialModifierRow = RowNo;
            sRowColumn.MaterialModifierColumn = 6;
            sRowColumn.ToleranceSymbolRow = RowNo;
            sRowColumn.ToleranceSymbolColumn = 6;

            sRowColumn.UpperTolRow = RowNo;
            sRowColumn.UpperTolColumn = 7;

            sRowColumn.PrimaryDatumRow = RowNo;
            sRowColumn.PrimaryDatumColumn = 8;

            sRowColumn.PrimaryMaterialModifierRow = RowNo;
            sRowColumn.PrimaryMaterialModifierColumn = 9;

            sRowColumn.SecondaryDatumRow = RowNo;
            sRowColumn.SecondaryDatumColumn = 10;

            sRowColumn.SecondaryMaterialModifierRow = RowNo;
            sRowColumn.SecondaryMaterialModifierColumn = 11;

            sRowColumn.TertiaryDatumRow = RowNo;
            sRowColumn.TertiaryDatumColumn = 12;

            sRowColumn.TertiaryMaterialModifierRow = RowNo;
            sRowColumn.TertiaryMaterialModifierColumn = 13;

            sRowColumn.MaxRow = RowNo;
            sRowColumn.MaxColumn = 14;

            sRowColumn.MinRow = RowNo;
            sRowColumn.MinColumn = 15;

            sRowColumn.GaugeRow = RowNo;
            sRowColumn.GaugeColumn = 16;

            sRowColumn.FrequencyRow = RowNo;
            sRowColumn.FrequencyColumn = 20;

            sRowColumn.BallonRow = RowNo;
            sRowColumn.BallonColumn = 1;

            sRowColumn.LocationRow = RowNo;
            sRowColumn.LocationColumn = 2;
            */
        }
        public static void GetIPQCRowColumn(int i, out IPQCRowColumn sRowColumn)
        {
            sRowColumn = new IPQCRowColumn();
            sRowColumn.PartNoRow = 5;
            sRowColumn.PartNoColumn = 4;
            sRowColumn.OISRow = 5;
            sRowColumn.OISColumn = 6;
            sRowColumn.OISRevRow = 5;
            sRowColumn.OISRevColumn = 8;
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
            
        }
        public static bool AddNewSheet(int dataCount, int SheetCountPerOne, ApplicationClass excelApp, Worksheet workSheet)
        {
            try
            {
                int needSheetNum = (dataCount / SheetCountPerOne);
                int needSheetNum_Reserve = (dataCount % SheetCountPerOne);
                if (needSheetNum_Reserve != 0)
                {
                    needSheetNum++;
                }
                for (int i = 1; i < needSheetNum; i++)
                {
                    workSheet.Copy(System.Type.Missing, excelApp.Workbooks[1].Worksheets[1]);
                }
            }
            catch (System.Exception ex)
            {
                return false;
            }
            return true;
        }
        public static bool ModifySheet(string partNo, string section, Workbook workBook, Worksheet workSheet, Range workRange)
        {
            try
            {
                for (int i = 0; i < workBook.Worksheets.Count; i++)
                {
                    workSheet = (Worksheet)workBook.Sheets[i + 1];
                    if (section == "ShopDoc")
                    {
                        workRange = (Range)workSheet.Cells[4, 1];
                        workRange.Value = workRange.Value.ToString().Replace("1/1", (i + 1).ToString() + "/" + (workBook.Worksheets.Count).ToString());
                    }
                    else if (section == "IPQC")
                    {
                        workRange = (Range)workSheet.Cells[5, 17];
                        workRange.Value = workRange.Value.ToString().Replace("1/1", (i + 1).ToString() + "/" + (workBook.Worksheets.Count).ToString());
                    }
                    else if (section == "SelfCheck")
                    {
                        workRange = (Range)workSheet.Cells[4, 5];
                        workRange.Value = workRange.Value.ToString().Replace("1/1", (i + 1).ToString() + "/" + (workBook.Worksheets.Count).ToString());
                    }

                    //Sheet的名稱不得超過31個，否則會錯
                    if (partNo.Length >= 28)
                    {
                        workSheet.Name = string.Format("({0})", (i + 1).ToString());
                    }
                    else
                    {
                        workSheet.Name = string.Format("{0}({1})", partNo, (i + 1).ToString());
                    }
                }
            }
            catch (System.Exception ex)
            {
                return false;
            }
            return true;
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
        public static bool MappingDimenData(Com_Dimension cCom_Dimension, Worksheet worksheet, int currentRow, int dimenColumn)
        {
            try
            {
                Dictionary<int, bool> TranslateWords = new Dictionary<int, bool>();
                int start = 1, length = 0;
                if (cCom_Dimension.characteristic != "" & cCom_Dimension.characteristic != null & cCom_Dimension.characteristic != "None")
                {
                    worksheet.Cells[currentRow, dimenColumn] = cCom_Dimension.characteristic;
                    length = cCom_Dimension.characteristic.Length;
                    for (int i = 0; i < length; i++)
                    {
                        TranslateWords.Add(start, true);
                        start++;
                    }
                    worksheet.Cells[currentRow, dimenColumn] = ((Range)worksheet.Cells[currentRow, dimenColumn]).Value + "|";
                    TranslateWords.Add(start, false);
                    start++;
                    //strLen = ((Range)worksheet.Cells[currentRow, dimenColumn]).Value.ToString().Length;
                    //((Range)worksheet.Cells[currentRow, dimenColumn]).Cells.Characters[start, length].Font.Name = "GDT";
                    //start = start + length;
                }
                if (cCom_Dimension.zoneShape != "" & cCom_Dimension.zoneShape != null & cCom_Dimension.zoneShape != "None")
                {
                    worksheet.Cells[currentRow, dimenColumn] = ((Range)worksheet.Cells[currentRow, dimenColumn]).Value + cCom_Dimension.zoneShape;
                    length = cCom_Dimension.zoneShape.Length;
                    for (int i = 0; i < length; i++)
                    {
                        TranslateWords.Add(start, true);
                        start++;
                    }
                    //int StrLen = ((Range)worksheet.Cells[currentRow, dimenColumn]).Value.ToString().Length;
                    //((Range)worksheet.Cells[currentRow, dimenColumn]).Cells.Characters[start, length].Font.Name = "GDT";
                    //start = start + length;
                }
                if (cCom_Dimension.toleranceValue != "" & cCom_Dimension.toleranceValue != null & cCom_Dimension.toleranceValue != "None")
                {
                    worksheet.Cells[currentRow, dimenColumn] = ((Range)worksheet.Cells[currentRow, dimenColumn]).Value + cCom_Dimension.toleranceValue;
                    length = cCom_Dimension.toleranceValue.Length;
                    for (int i = 0; i < length; i++)
                    {
                        TranslateWords.Add(start, false);
                        start++;
                    }
                    //if (cCom_Dimension.materialModifier != "" & cCom_Dimension.materialModifier != null & cCom_Dimension.materialModifier != "None")
                    //{
                    //}
                    //else
                    //{
                    //    worksheet.Cells[currentRow, dimenColumn] = ((Range)worksheet.Cells[currentRow, dimenColumn]).Value + "|";
                    //    TranslateWords.Add(start, false);
                    //    start++;
                    //}

                    //((Range)worksheet.Cells[currentRow, dimenColumn]).Cells.Characters[start, length].Font.Name = "Arial";
                    //start = start + length;
                }
                if (cCom_Dimension.materialModifier != "" & cCom_Dimension.materialModifier != null & cCom_Dimension.materialModifier != "None")
                {
                    worksheet.Cells[currentRow, dimenColumn] = ((Range)worksheet.Cells[currentRow, dimenColumn]).Value + cCom_Dimension.materialModifier;
                    length = cCom_Dimension.materialModifier.Length;
                    for (int i = 0; i < length; i++)
                    {
                        TranslateWords.Add(start, true);
                        start++;
                    }

                    //((Range)worksheet.Cells[currentRow, dimenColumn]).Cells.Characters[start, length].Font.Name = "GDT";
                    //start = start + length;
                    //workRange[currentRow, dimenColumn] = ((Range)workRange[currentRow, dimenColumn]).Value + cCom_Dimension.materialModifer;
                }
                if (/*cCom_Dimension.primaryDatum != "" &*/ cCom_Dimension.primaryDatum != null /*& cCom_Dimension.primaryDatum != "None"*/)
                {
                    worksheet.Cells[currentRow, dimenColumn] = ((Range)worksheet.Cells[currentRow, dimenColumn]).Value + "|";
                    TranslateWords.Add(start, false);
                    start++;
                    string[] splitPrimaryDatum = cCom_Dimension.primaryDatum.Split(',');
                    string[] splitPrimaryMaterialModifier = cCom_Dimension.primaryMaterialModifier.Split(',');

                    for (int i = 0; i < splitPrimaryDatum.Length; i++)
                    {
                        if (i >= 1)
                        {
                            worksheet.Cells[currentRow, dimenColumn] = ((Range)worksheet.Cells[currentRow, dimenColumn]).Value + "-";
                            TranslateWords.Add(start, false);
                            start++;
                        }

                        if (splitPrimaryDatum[i] == "X")
                        {
                            if (splitPrimaryMaterialModifier[i] == "X")
                            {
                                continue;
                            }
                            else
                            {
                                worksheet.Cells[currentRow, dimenColumn] = ((Range)worksheet.Cells[currentRow, dimenColumn]).Value + splitPrimaryMaterialModifier[i];
                                TranslateWords.Add(start, true);
                                start++;
                                continue;
                            }
                        }
                        else
                        {
                            worksheet.Cells[currentRow, dimenColumn] = ((Range)worksheet.Cells[currentRow, dimenColumn]).Value + splitPrimaryDatum[i];
                            TranslateWords.Add(start, false);
                            start++;
                            if (splitPrimaryMaterialModifier[i] == "X")
                            {
                                continue;
                            }
                            else
                            {
                                worksheet.Cells[currentRow, dimenColumn] = ((Range)worksheet.Cells[currentRow, dimenColumn]).Value + splitPrimaryMaterialModifier[i];
                                TranslateWords.Add(start, true);
                                start++;
                                continue;
                            }
                        }
                    }

                    /*
                    worksheet.Cells[currentRow, dimenColumn] = ((Range)worksheet.Cells[currentRow, dimenColumn]).Value + cCom_Dimension.primaryDatum;
                    length = cCom_Dimension.primaryDatum.Length;
                    for (int i = 0; i < length; i++)
                    {
                        TranslateWords.Add(start, false);
                        start++;
                    }
                    if (cCom_Dimension.primaryMaterialModifier != "" & cCom_Dimension.primaryMaterialModifier != null & cCom_Dimension.primaryMaterialModifier != "None")
                    {
                    }
                    else
                    {
                        worksheet.Cells[currentRow, dimenColumn] = ((Range)worksheet.Cells[currentRow, dimenColumn]).Value + "|";
                        TranslateWords.Add(start, false);
                        start++;
                    }
                    */
                }
                /*
                if (cCom_Dimension.primaryMaterialModifier != "" & cCom_Dimension.primaryMaterialModifier != null & cCom_Dimension.primaryMaterialModifier != "None")
                {
                    worksheet.Cells[currentRow, dimenColumn] = ((Range)worksheet.Cells[currentRow, dimenColumn]).Value + cCom_Dimension.primaryMaterialModifier;
                    length = cCom_Dimension.primaryMaterialModifier.Length;
                    for (int i = 0; i < length; i++)
                    {
                        TranslateWords.Add(start, true);
                        start++;
                    }
                }
                */
                if (/*cCom_Dimension.secondaryDatum != "" &  */cCom_Dimension.secondaryDatum != null /*& cCom_Dimension.secondaryDatum != "None"*/)
                {
                    worksheet.Cells[currentRow, dimenColumn] = ((Range)worksheet.Cells[currentRow, dimenColumn]).Value + "|";
                    TranslateWords.Add(start, false);
                    start++;
                    string[] splitSecondaryDatum = cCom_Dimension.secondaryDatum.Split(',');
                    string[] splitSecondaryMaterialModifier = cCom_Dimension.secondaryMaterialModifier.Split(',');

                    for (int i = 0; i < splitSecondaryDatum.Length; i++)
                    {
                        if (i >= 1)
                        {
                            worksheet.Cells[currentRow, dimenColumn] = ((Range)worksheet.Cells[currentRow, dimenColumn]).Value + "-";
                            TranslateWords.Add(start, false);
                            start++;
                        }

                        if (splitSecondaryDatum[i] == "X")
                        {
                            if (splitSecondaryMaterialModifier[i] == "X")
                            {
                                continue;
                            }
                            else
                            {
                                worksheet.Cells[currentRow, dimenColumn] = ((Range)worksheet.Cells[currentRow, dimenColumn]).Value + splitSecondaryMaterialModifier[i];
                                TranslateWords.Add(start, true);
                                start++;
                                continue;
                            }
                        }
                        else
                        {
                            worksheet.Cells[currentRow, dimenColumn] = ((Range)worksheet.Cells[currentRow, dimenColumn]).Value + splitSecondaryDatum[i];
                            TranslateWords.Add(start, false);
                            start++;
                            if (splitSecondaryMaterialModifier[i] == "X")
                            {
                                continue;
                            }
                            else
                            {
                                worksheet.Cells[currentRow, dimenColumn] = ((Range)worksheet.Cells[currentRow, dimenColumn]).Value + splitSecondaryMaterialModifier[i];
                                TranslateWords.Add(start, true);
                                start++;
                                continue;
                            }
                        }
                    }
                    /*
                    worksheet.Cells[currentRow, dimenColumn] = ((Range)worksheet.Cells[currentRow, dimenColumn]).Value + cCom_Dimension.secondaryDatum;
                    length = cCom_Dimension.secondaryDatum.Length;
                    for (int i = 0; i < length; i++)
                    {
                        TranslateWords.Add(start, false);
                        start++;
                    }
                    if (cCom_Dimension.secondaryMaterialModifier != "" & cCom_Dimension.secondaryMaterialModifier != null & cCom_Dimension.secondaryMaterialModifier != "None")
                    {
                    }
                    else
                    {
                        worksheet.Cells[currentRow, dimenColumn] = ((Range)worksheet.Cells[currentRow, dimenColumn]).Value + "|";
                        TranslateWords.Add(start, false);
                        start++;
                    }
                    */
                }
                /*
                if (cCom_Dimension.secondaryMaterialModifier != "" & cCom_Dimension.secondaryMaterialModifier != null & cCom_Dimension.secondaryMaterialModifier != "None")
                {
                    worksheet.Cells[currentRow, dimenColumn] = ((Range)worksheet.Cells[currentRow, dimenColumn]).Value + cCom_Dimension.secondaryMaterialModifier;
                    length = cCom_Dimension.secondaryMaterialModifier.Length;
                    for (int i = 0; i < length; i++)
                    {
                        TranslateWords.Add(start, true);
                        start++;
                    }
                }
                */
                if ( /*cCom_Dimension.tertiaryDatum != "" & */cCom_Dimension.tertiaryDatum != null  /*& cCom_Dimension.tertiaryDatum != "None"*/)
                {
                    worksheet.Cells[currentRow, dimenColumn] = ((Range)worksheet.Cells[currentRow, dimenColumn]).Value + "|";
                    TranslateWords.Add(start, false);
                    start++;

                    string[] splitTertiaryDatum = cCom_Dimension.tertiaryDatum.Split(',');
                    string[] splitTertiaryMaterialModifier = cCom_Dimension.tertiaryMaterialModifier.Split(',');

                    for (int i = 0; i < splitTertiaryDatum.Length; i++)
                    {
                        if (i >= 1)
                        {
                            worksheet.Cells[currentRow, dimenColumn] = ((Range)worksheet.Cells[currentRow, dimenColumn]).Value + "-";
                            TranslateWords.Add(start, false);
                            start++;
                        }

                        if (splitTertiaryDatum[i] == "X")
                        {
                            if (splitTertiaryMaterialModifier[i] == "X")
                            {
                                continue;
                            }
                            else
                            {
                                worksheet.Cells[currentRow, dimenColumn] = ((Range)worksheet.Cells[currentRow, dimenColumn]).Value + splitTertiaryMaterialModifier[i];
                                TranslateWords.Add(start, true);
                                start++;
                                continue;
                            }
                        }
                        else
                        {
                            worksheet.Cells[currentRow, dimenColumn] = ((Range)worksheet.Cells[currentRow, dimenColumn]).Value + splitTertiaryDatum[i];
                            TranslateWords.Add(start, false);
                            start++;
                            if (splitTertiaryMaterialModifier[i] == "X")
                            {
                                continue;
                            }
                            else
                            {
                                worksheet.Cells[currentRow, dimenColumn] = ((Range)worksheet.Cells[currentRow, dimenColumn]).Value + splitTertiaryMaterialModifier[i];
                                TranslateWords.Add(start, true);
                                start++;
                                continue;
                            }
                        }
                    }
                    /*
                    worksheet.Cells[currentRow, dimenColumn] = ((Range)worksheet.Cells[currentRow, dimenColumn]).Value + cCom_Dimension.tertiaryDatum;
                    length = cCom_Dimension.tertiaryDatum.Length;
                    for (int i = 0; i < length; i++)
                    {
                        TranslateWords.Add(start, false);
                        start++;
                    }
                    if (cCom_Dimension.tertiaryMaterialModifier != "" & cCom_Dimension.tertiaryMaterialModifier != null & cCom_Dimension.tertiaryMaterialModifier != "None")
                    {
                    }
                    else
                    {
                        worksheet.Cells[currentRow, dimenColumn] = ((Range)worksheet.Cells[currentRow, dimenColumn]).Value + "|";
                        TranslateWords.Add(start, false);
                        start++;
                    }
                    */
                }
                /*
                if (cCom_Dimension.tertiaryMaterialModifier != "" & cCom_Dimension.tertiaryMaterialModifier != null & cCom_Dimension.tertiaryMaterialModifier != "None")
                {
                    worksheet.Cells[currentRow, dimenColumn] = ((Range)worksheet.Cells[currentRow, dimenColumn]).Value + cCom_Dimension.tertiaryMaterialModifier;
                    length = cCom_Dimension.tertiaryMaterialModifier.Length;
                    for (int i = 0; i < length; i++)
                    {
                        TranslateWords.Add(start, true);
                        start++;
                    }
                    worksheet.Cells[currentRow, dimenColumn] = ((Range)worksheet.Cells[currentRow, dimenColumn]).Value + "|";
                    TranslateWords.Add(start, false);
                    start++;
                }
                */
                if (cCom_Dimension.aboveText != "" & cCom_Dimension.aboveText != null & cCom_Dimension.aboveText != "None")
                {
                    worksheet.Cells[currentRow, dimenColumn] = ((Range)worksheet.Cells[currentRow, dimenColumn]).Value + cCom_Dimension.aboveText;
                    length = cCom_Dimension.aboveText.Length;
                    for (int i = 0; i < length; i++)
                    {
                        TranslateWords.Add(start, false);
                        start++;
                    }
                    //workRange[currentRow, dimenColumn] = ((Range)workRange[currentRow, dimenColumn]).Value + cCom_Dimension.aboveText;
                }
                if (cCom_Dimension.beforeText != "" & cCom_Dimension.beforeText != null & cCom_Dimension.beforeText != "None")
                {
                    worksheet.Cells[currentRow, dimenColumn] = ((Range)worksheet.Cells[currentRow, dimenColumn]).Value + cCom_Dimension.beforeText;
                    length = cCom_Dimension.beforeText.Length;
                    for (int i = 0; i < length; i++)
                    {
                        TranslateWords.Add(start, false);
                        start++;
                    }
                    //workRange[currentRow, dimenColumn] = ((Range)workRange[currentRow, dimenColumn]).Value + cCom_Dimension.beforeText;
                }
                if (cCom_Dimension.toleranceSymbol != "" & cCom_Dimension.toleranceSymbol != null & cCom_Dimension.toleranceSymbol != "None")
                {
                    worksheet.Cells[currentRow, dimenColumn] = ((Range)worksheet.Cells[currentRow, dimenColumn]).Value + cCom_Dimension.toleranceSymbol;
                    length = cCom_Dimension.toleranceSymbol.Length;
                    if (cCom_Dimension.toleranceSymbol == "R")
                    {
                        for (int i = 0; i < length; i++)
                        {
                            TranslateWords.Add(start, false);
                            start++;
                        }
                    }
                    else
                    {
                        for (int i = 0; i < length; i++)
                        {
                            TranslateWords.Add(start, true);
                            start++;
                        }
                    }
                    
                    
                    //workRange[currentRow, dimenColumn] = ((Range)workRange[currentRow, dimenColumn]).Value + cCom_Dimension.toleranceSymbol;
                }
                if (cCom_Dimension.mainText != "" & cCom_Dimension.mainText != null & cCom_Dimension.mainText != "None")
                {
                    worksheet.Cells[currentRow, dimenColumn] = ((Range)worksheet.Cells[currentRow, dimenColumn]).Value + cCom_Dimension.mainText;
                    length = cCom_Dimension.mainText.Length;
                    char[] splitText = cCom_Dimension.mainText.ToCharArray();
                    for (int i = 0; i < length; i++)
                    {
                        if (splitText[i] == '~')
                        {
                            TranslateWords.Add(start, true);
                        }
                        else
                        {
                            TranslateWords.Add(start, false);
                        }
                        start++;
                    }
                    //workRange[currentRow, dimenColumn] = ((Range)workRange[currentRow, dimenColumn]).Value + cCom_Dimension.mainText;
                }
                if ((cCom_Dimension.upTolerance != "" & cCom_Dimension.upTolerance != null & cCom_Dimension.upTolerance != "None") ||
                    (cCom_Dimension.lowTolerance != "" & cCom_Dimension.upTolerance != null & cCom_Dimension.upTolerance != "None"))
                {
                    if (cCom_Dimension.upTolerance == cCom_Dimension.lowTolerance)
                    {
                        //如果上下公差相同，則加入±到字串中，所以在TranslateWords中必須加一次資訊
                        worksheet.Cells[currentRow, dimenColumn] = ((Range)worksheet.Cells[currentRow, dimenColumn]).Value + "±";
                        TranslateWords.Add(start, false); start++;
                        //加入公差的長度
                        worksheet.Cells[currentRow, dimenColumn] = ((Range)worksheet.Cells[currentRow, dimenColumn]).Value + cCom_Dimension.upTolerance;
                        length = cCom_Dimension.upTolerance.Length;
                        char[] splitText = cCom_Dimension.upTolerance.ToCharArray();
                        for (int i = 0; i < length; i++)
                        {
                            if (splitText[i] == '~')
                            {
                                TranslateWords.Add(start, true);
                            }
                            else
                            {
                                TranslateWords.Add(start, false);
                            }
                            //TranslateWords.Add(start, false);
                            start++;
                        }
                    }
                    else
                    {
                        if (Math.Abs(Convert.ToDouble(cCom_Dimension.upTolerance)) * 10000 > 0)
                        {
                            //表示有上公差，所以加入+到字串中，所以在TranslateWords中必須加一次資訊
                            //如果遇到上公差是-的，則不能加+
                            if (cCom_Dimension.upTolerance.Contains('-'))
                            {
                            }
                            else
                            {
                                worksheet.Cells[currentRow, dimenColumn] = ((Range)worksheet.Cells[currentRow, dimenColumn]).Value + "+";
                                TranslateWords.Add(start, false); start++;
                            }
                            //加入公差的長度
                            worksheet.Cells[currentRow, dimenColumn] = ((Range)worksheet.Cells[currentRow, dimenColumn]).Value + cCom_Dimension.upTolerance;
                            length = cCom_Dimension.upTolerance.Length;
                            char[] splitText = cCom_Dimension.upTolerance.ToCharArray();
                            for (int i = 0; i < length; i++)
                            {
                                if (splitText[i] == '~')
                                {
                                    TranslateWords.Add(start, true);
                                }
                                else
                                {
                                    TranslateWords.Add(start, false);
                                }
                                //TranslateWords.Add(start, false);
                                start++;
                            }
                        }
                        if (Convert.ToDouble(cCom_Dimension.lowTolerance) * 10000 > 0)
                        {
                            worksheet.Cells[currentRow, dimenColumn] = ((Range)worksheet.Cells[currentRow, dimenColumn]).Value + "/";
                            TranslateWords.Add(start, false);
                            start++;
                            //表示有下公差，所以加入-到字串中，所以在TranslateWords中必須加一次資訊
                            //如果遇到下公差是+的，則不能加-
                            if (cCom_Dimension.lowTolerance.Contains('+'))
                            {
                            }
                            else
                            {
                                worksheet.Cells[currentRow, dimenColumn] = ((Range)worksheet.Cells[currentRow, dimenColumn]).Value + "-";
                                TranslateWords.Add(start, false); start++;
                            }
                            //加入公差的長度
                            worksheet.Cells[currentRow, dimenColumn] = ((Range)worksheet.Cells[currentRow, dimenColumn]).Value + cCom_Dimension.lowTolerance;
                            length = cCom_Dimension.lowTolerance.Length;
                            char[] splitText = cCom_Dimension.lowTolerance.ToCharArray();
                            for (int i = 0; i < length; i++)
                            {
                                if (splitText[i] == '~')
                                {
                                    TranslateWords.Add(start, true);
                                }
                                else
                                {
                                    TranslateWords.Add(start, false);
                                }
                                //TranslateWords.Add(start, false);
                                start++;
                            }
                        }
                    }

                    //workRange[currentRow, dimenColumn] = ((Range)workRange[currentRow, dimenColumn]).Value + "+" + cCom_Dimension.upTolerance;
                }
                if (cCom_Dimension.minTolerance != "" & cCom_Dimension.minTolerance != null & cCom_Dimension.minTolerance != "None")
                {
                    //先加入"("符號
                    worksheet.Cells[currentRow, dimenColumn] = ((Range)worksheet.Cells[currentRow, dimenColumn]).Value + "(";
                    TranslateWords.Add(start, false);
                    start++;
                    //再加入最小公差
                    worksheet.Cells[currentRow, dimenColumn] = ((Range)worksheet.Cells[currentRow, dimenColumn]).Value + cCom_Dimension.minTolerance;
                    length = cCom_Dimension.minTolerance.Length;
                    for (int i = 0; i < length; i++)
                    {
                        TranslateWords.Add(start, false);
                        start++;
                    }

                }
                if (cCom_Dimension.maxTolerance != "" & cCom_Dimension.maxTolerance != null & cCom_Dimension.maxTolerance != "None")
                {
                    //先加入"~"符號
                    worksheet.Cells[currentRow, dimenColumn] = ((Range)worksheet.Cells[currentRow, dimenColumn]).Value + "~";
                    TranslateWords.Add(start, false);
                    start++;
                    //再加入最小公差
                    worksheet.Cells[currentRow, dimenColumn] = ((Range)worksheet.Cells[currentRow, dimenColumn]).Value + cCom_Dimension.maxTolerance;
                    length = cCom_Dimension.maxTolerance.Length;
                    for (int i = 0; i < length; i++)
                    {
                        TranslateWords.Add(start, false);
                        start++;
                    }
                    //再加入")"符號
                    worksheet.Cells[currentRow, dimenColumn] = ((Range)worksheet.Cells[currentRow, dimenColumn]).Value + ")";
                    TranslateWords.Add(start, false);
                    start++;
                }
                //if (cCom_Dimension.lowTolerance != "" & cCom_Dimension.lowTolerance != null & cCom_Dimension.lowTolerance != "None")
                //{
                //    worksheet.Cells[currentRow, dimenColumn] = ((Range)worksheet.Cells[currentRow, dimenColumn]).Value + cCom_Dimension.lowTolerance;
                //    length = cCom_Dimension.lowTolerance.Length;
                //    for (int i = 0; i < length; i++)
                //    {
                //        TranslateWords.Add(start, false);
                //        start++;
                //    }
                //}
                if (cCom_Dimension.x != "" & cCom_Dimension.x != null & cCom_Dimension.x != "None")
                {
                    worksheet.Cells[currentRow, dimenColumn] = ((Range)worksheet.Cells[currentRow, dimenColumn]).Value + cCom_Dimension.x;
                    length = cCom_Dimension.x.Length;
                    for (int i = 0; i < length; i++)
                    {
                        TranslateWords.Add(start, false);
                        start++;
                    }
                    //workRange[currentRow, dimenColumn] = ((Range)workRange[currentRow, dimenColumn]).Value + cCom_Dimension.x.ToUpper();
                }
                if (cCom_Dimension.chamferAngle != "" & cCom_Dimension.chamferAngle != null & cCom_Dimension.chamferAngle != "None")
                {
                    worksheet.Cells[currentRow, dimenColumn] = ((Range)worksheet.Cells[currentRow, dimenColumn]).Value + cCom_Dimension.chamferAngle;
                    length = cCom_Dimension.chamferAngle.Length;
                    char[] splitText = cCom_Dimension.chamferAngle.ToCharArray();
                    for (int i = 0; i < length; i++)
                    {
                        if (splitText[i] == '~')
                        {
                            TranslateWords.Add(start, true);
                        }
                        else
                        {
                            TranslateWords.Add(start, false);
                        }
                        start++;
                    }
                    //workRange[currentRow, dimenColumn] = ((Range)workRange[currentRow, dimenColumn]).Value + cCom_Dimension.chamferAngle;
                }
                if (cCom_Dimension.afterText != "" & cCom_Dimension.afterText != null & cCom_Dimension.afterText != "None")
                {
                    worksheet.Cells[currentRow, dimenColumn] = ((Range)worksheet.Cells[currentRow, dimenColumn]).Value + cCom_Dimension.afterText;
                    length = cCom_Dimension.afterText.Length;
                    //MessageBox.Show(length.ToString());
                    char[] splitText = cCom_Dimension.afterText.ToCharArray();
                    //MessageBox.Show(splitText.Length.ToString());
                    for (int i = 0; i < length; i++)
                    {
                        //MessageBox.Show(splitText[i].ToString());
                        if (splitText[i] == '~')
                        {
                            TranslateWords.Add(start, true);
                        }
                        else
                        {
                            TranslateWords.Add(start, false);
                        }
                        start++;
                    }
                    //workRange[currentRow, dimenColumn] = ((Range)workRange[currentRow, dimenColumn]).Value + cCom_Dimension.afterText.ToUpper();
                }

                foreach (KeyValuePair<int, bool> kvp in TranslateWords)
                {
                    if (kvp.Value)
                    {
                        ((Range)worksheet.Cells[currentRow, dimenColumn]).Cells.Characters[kvp.Key, 1].Font.Name = "GDT";
                    }
                    else
                    {
                        ((Range)worksheet.Cells[currentRow, dimenColumn]).Cells.Characters[kvp.Key, 1].Font.Name = "Lucida Bright";
                    }

                }

                /*
                if (cCom_Dimension.dimensionType == "NXOpen.Annotations.DraftingFcf")
                {
                    #region InitialData
                    if (cCom_Dimension.characteristic != "")
                    {
                        //workRange[currentRow, dimenColumn] = GetCharacteristicSymbol(cCom_Dimension.characteristic);
                        workRange[currentRow, dimenColumn] = cCom_Dimension.characteristic;
                    }
                    if (cCom_Dimension.zoneShape != "")
                    {
                        //workRange[currentRow, dimenColumn] = ((Range)workRange[currentRow, dimenColumn]).Value + GetZoneShapeSymbol(cCom_Dimension.zoneShape);
                        workRange[currentRow, dimenColumn] = ((Range)workRange[currentRow, dimenColumn]).Value + cCom_Dimension.zoneShape;
                    }
                    if (cCom_Dimension.toleranceValue != "")
                    {
                        workRange[currentRow, dimenColumn] = ((Range)workRange[currentRow, dimenColumn]).Value + cCom_Dimension.toleranceValue;
                    }
                    if (cCom_Dimension.materialModifer != "" & cCom_Dimension.materialModifer != "None")
                    {
                        string ValueStr = cCom_Dimension.materialModifer;
                        if (ValueStr == "LeastMaterialCondition")
                        {
                            ValueStr = "l";
                        }
                        else if (ValueStr == "MaximumMaterialCondition")
                        {
                            ValueStr = "m";
                        }
                        else if (ValueStr == "RegardlessOfFeatureSize")
                        {
                            ValueStr = "s";
                        }
                        workRange[currentRow, dimenColumn] = ((Range)workRange[currentRow, dimenColumn]).Value + ValueStr;
                    }
                    #endregion

                    #region Primary
                    if (cCom_Dimension.primaryDatum != "")
                    {
                        workRange[currentRow, dimenColumn] = ((Range)workRange[currentRow, dimenColumn]).Value + cCom_Dimension.primaryDatum;
                    }
                    if (cCom_Dimension.primaryMaterialModifier != "" & cCom_Dimension.primaryMaterialModifier != "None")
                    {
                        string ValueStr = cCom_Dimension.primaryMaterialModifier;
                        if (ValueStr == "LeastMaterialCondition")
                        {
                            ValueStr = "l";
                        }
                        else if (ValueStr == "MaximumMaterialCondition")
                        {
                            ValueStr = "m";
                        }
                        else if (ValueStr == "RegardlessOfFeatureSize")
                        {
                            ValueStr = "s";
                        }
                        workRange[currentRow, dimenColumn] = ((Range)workRange[currentRow, dimenColumn]).Value + ValueStr;
                    }
                    #endregion
                    
                    #region Secondary
                    if (cCom_Dimension.secondaryDatum != "")
                    {
                        workRange[currentRow, dimenColumn] = ((Range)workRange[currentRow, dimenColumn]).Value + cCom_Dimension.secondaryDatum;
                    }
                    if (cCom_Dimension.secondaryMaterialModifier != "" & cCom_Dimension.secondaryMaterialModifier != "None")
                    {
                        string ValueStr = cCom_Dimension.secondaryMaterialModifier;
                        if (ValueStr == "LeastMaterialCondition")
                        {
                            ValueStr = "l";
                        }
                        else if (ValueStr == "MaximumMaterialCondition")
                        {
                            ValueStr = "m";
                        }
                        else if (ValueStr == "RegardlessOfFeatureSize")
                        {
                            ValueStr = "s";
                        }
                        workRange[currentRow, dimenColumn] = ((Range)workRange[currentRow, dimenColumn]).Value + ValueStr;
                    }
                    #endregion
                    
                    #region Tertiary
                    if (cCom_Dimension.tertiaryDatum != "")
                    {
                        workRange[currentRow, dimenColumn] = ((Range)workRange[currentRow, dimenColumn]).Value + cCom_Dimension.tertiaryDatum;
                    }
                    if (cCom_Dimension.tertiaryMaterialModifier != "" & cCom_Dimension.tertiaryMaterialModifier != "None")
                    {
                        string ValueStr = cCom_Dimension.tertiaryMaterialModifier;
                        if (ValueStr == "LeastMaterialCondition")
                        {
                            ValueStr = "l";
                        }
                        else if (ValueStr == "MaximumMaterialCondition")
                        {
                            ValueStr = "m";
                        }
                        else if (ValueStr == "RegardlessOfFeatureSize")
                        {
                            ValueStr = "s";
                        }
                        workRange[currentRow, dimenColumn] = ((Range)workRange[currentRow, dimenColumn]).Value + ValueStr;
                    }
                    #endregion
                }
                else if (cCom_Dimension.dimensionType == "NXOpen.Annotations.Label")
                {
                    workRange[currentRow, dimenColumn] = cCom_Dimension.mainText;
                }
                else
                {
                    #region Dimension
                    
                    if (cCom_Dimension.beforeText != null)
                    {
                        //workRange[currentRow, dimenColumn] = ((Range)workRange[currentRow, dimenColumn]).Value + GetGDTWord(cCom_Dimension.beforeText);
                        workRange[currentRow, dimenColumn] = ((Range)workRange[currentRow, dimenColumn]).Value + cCom_Dimension.beforeText;
                    }
                    if (cCom_Dimension.mainText != "")
                    {
                        //workRange[currentRow, dimenColumn] = ((Range)workRange[currentRow, dimenColumn]).Value + GetGDTWord(cCom_Dimension.mainText);
                        workRange[currentRow, dimenColumn] = ((Range)workRange[currentRow, dimenColumn]).Value + cCom_Dimension.mainText;
                    }
                    if (cCom_Dimension.upTolerance != "" & cCom_Dimension.toleranceType == "BilateralOneLine")
                    {
                        workRange[currentRow, dimenColumn] = ((Range)workRange[currentRow, dimenColumn]).Value + "±";
                        workRange[currentRow, dimenColumn] = ((Range)workRange[currentRow, dimenColumn]).Value + cCom_Dimension.upTolerance;
                        string MaxMinStr = "(" + (Convert.ToDouble(cCom_Dimension.mainText) - Convert.ToDouble(cCom_Dimension.upTolerance)).ToString() + "-" + (Convert.ToDouble(cCom_Dimension.mainText) + Convert.ToDouble(cCom_Dimension.upTolerance)).ToString() + ")";
                        workRange[currentRow, dimenColumn] = ((Range)workRange[currentRow, dimenColumn]).Value + MaxMinStr;
                    }
                    else if (cCom_Dimension.upTolerance != "" & cCom_Dimension.toleranceType == "BilateralTwoLines")
                    {
                        workRange[currentRow, dimenColumn] = ((Range)workRange[currentRow, dimenColumn]).Value + "+";
                        workRange[currentRow, dimenColumn] = ((Range)workRange[currentRow, dimenColumn]).Value + cCom_Dimension.upTolerance;
                        workRange[currentRow, dimenColumn] = ((Range)workRange[currentRow, dimenColumn]).Value + "/";
                        workRange[currentRow, dimenColumn] = ((Range)workRange[currentRow, dimenColumn]).Value + cCom_Dimension.lowTolerance;
                        string MaxMinStr = "(" + (Convert.ToDouble(cCom_Dimension.mainText) + Convert.ToDouble(cCom_Dimension.lowTolerance)).ToString() + "-" + (Convert.ToDouble(cCom_Dimension.mainText) + Convert.ToDouble(cCom_Dimension.upTolerance)).ToString() + ")";
                        workRange[currentRow, dimenColumn] = ((Range)workRange[currentRow, dimenColumn]).Value + MaxMinStr;
                    }
                    else if (cCom_Dimension.upTolerance != "" & cCom_Dimension.toleranceType == "UnilateralAbove")
                    {
                        workRange[currentRow, dimenColumn] = ((Range)workRange[currentRow, dimenColumn]).Value + "+";
                        workRange[currentRow, dimenColumn] = ((Range)workRange[currentRow, dimenColumn]).Value + cCom_Dimension.upTolerance;
                    }
                    else if (cCom_Dimension.upTolerance != "" & cCom_Dimension.toleranceType == "UnilateralBelow")
                    {
                        workRange[currentRow, dimenColumn] = ((Range)workRange[currentRow, dimenColumn]).Value + cCom_Dimension.lowTolerance;
                    }

                    #endregion
                }
                */
            }
            catch (System.Exception ex)
            {
                MessageBox.Show(ex.ToString());
                return false;
            }
            return true;
        }
        public static bool CheckExcelProcess()
        {
            try
            {
                //檢查PC有無Excel在執行
                foreach (var item in Process.GetProcesses())
                {
                    if (item.ProcessName == "EXCEL")
                    {
                        return false;
                    }
                    else
                    {
                        continue;
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
        public static bool MappingDimensionData(int ann_type, int ann_form, string text, ref Com_Dimension cDimensionData)
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
                            cDimensionData.upTolerance = upperTol;
                            cDimensionData.lowTolerance = lowerTol;
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
        public static bool GetDimensionData(string excelType, DisplayableObject singleObj, out Com_Dimension cDimensionData)
        {
            Session theSession = Session.GetSession();
            UI theUI;
            UFSession theUfSession = UFSession.GetUFSession();
            cDimensionData = new Com_Dimension();

            try
            {
                if (excelType == "")
                {
                    return false;
                }

                string singleObjType = singleObj.GetType().ToString();

                if (excelType == "SelfCheck")
                {
                    try { cDimensionData.instrument = singleObj.GetStringAttribute(CaxME.DimenAttr.SelfCheck_Gauge); }
                    catch (System.Exception ex) { return false; }
                    try { cDimensionData.frequency = singleObj.GetStringAttribute(CaxME.DimenAttr.SelfCheck_Freq); }
                    catch (System.Exception ex) { }
                }
                else
                {
                    try { cDimensionData.instrument = singleObj.GetStringAttribute(CaxME.DimenAttr.Gauge); }
                    catch (System.Exception ex) { return false; }
                    try { cDimensionData.frequency = singleObj.GetStringAttribute(CaxME.DimenAttr.Frequency); }
                    catch (System.Exception ex) { }
                }
                
                try { cDimensionData.ballon = Convert.ToInt32(singleObj.GetStringAttribute(CaxME.DimenAttr.BallonNum)); }
                catch (System.Exception ex) { return false; }

                try { cDimensionData.location = singleObj.GetStringAttribute(CaxME.DimenAttr.BallonLocation); }
                catch (System.Exception ex) { return false; }


                if (singleObj is NXOpen.Annotations.Annotation)
                {
                    if (singleObj is NXOpen.Annotations.DraftingFcf)
                    {
                        #region DraftingFcf取Text

                        NXOpen.Annotations.DraftingFcf temp = (NXOpen.Annotations.DraftingFcf)singleObj;
                        CaxME.FcfData sFcfData = new CaxME.FcfData();
                        CaxME.GetFcfData(temp, out sFcfData);
                        cDimensionData.dimensionType = "NXOpen.Annotations.DraftingFcf";
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
                        //CaxLog.ShowListingWindow("----");
                    }
                    try
                    {
                        if (cDimensionData.upTolerance != null)
                        {
                            cDimensionData.maxTolerance = (Convert.ToDouble(cDimensionData.mainText) + Convert.ToDouble(cDimensionData.upTolerance)).ToString();
                        }
                        if (cDimensionData.lowTolerance != null)
                        {
                            cDimensionData.minTolerance = (Convert.ToDouble(cDimensionData.mainText) - Convert.ToDouble(cDimensionData.lowTolerance)).ToString();
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
        public static bool MappingGDTWord(ref Com_Dimension input)
        {
            try
            {
                if (input.characteristic != null)
                {
                    input.characteristic = GetGDTWord(input.characteristic);
                }
                if (input.zoneShape != null)
                {
                    input.zoneShape = GetGDTWord(input.zoneShape);
                }
                if (input.toleranceValue != null)
                {
                    input.toleranceValue = input.toleranceValue;
                }
                if (input.materialModifier != null)
                {
                    input.materialModifier = GetGDTWord(input.materialModifier);
                }
                if (input.primaryDatum != null)
                {
                    input.primaryDatum = input.primaryDatum;
                }
                if (input.primaryMaterialModifier != null)
                {
                    input.primaryMaterialModifier = GetGDTWord(input.primaryMaterialModifier);
                }
                if (input.secondaryDatum != null)
                {
                    input.secondaryDatum = input.secondaryDatum;
                }
                if (input.secondaryMaterialModifier != null)
                {
                    input.secondaryMaterialModifier = GetGDTWord(input.secondaryMaterialModifier);
                }
                if (input.tertiaryDatum != null)
                {
                    input.tertiaryDatum = input.tertiaryDatum;
                }
                if (input.tertiaryMaterialModifier != null)
                {
                    input.tertiaryMaterialModifier = GetGDTWord(input.tertiaryMaterialModifier);
                }
                if (input.aboveText != null)
                {
                    input.aboveText = GetGDTWord(input.aboveText);
                }
                if (input.belowText != null)
                {
                    input.belowText = GetGDTWord(input.belowText);
                }
                if (input.beforeText != null)
                {
                    input.beforeText = GetGDTWord(input.beforeText);
                }
                if (input.afterText != null)
                {
                    input.afterText = GetGDTWord(input.afterText);
                }
                if (input.toleranceSymbol != null)
                {
                    input.toleranceSymbol = GetGDTWord(input.toleranceSymbol);
                }
                if (input.mainText != null)
                {
                    input.mainText = input.mainText;
                }
                if (input.upTolerance != null)
                {
                    input.upTolerance = input.upTolerance;
                }
                if (input.lowTolerance != null)
                {
                    input.lowTolerance = input.lowTolerance;
                }
                if (input.x != null)
                {
                    input.x = input.x;
                }
                if (input.chamferAngle != null)
                {
                    input.chamferAngle = GetGDTWord(input.chamferAngle);
                }
                if (input.maxTolerance != null)
                {
                    input.maxTolerance = input.maxTolerance;
                }
                if (input.minTolerance != null)
                {
                    input.minTolerance = input.minTolerance;
                }
            }
            catch (System.Exception ex)
            {
                return false;
            }
            return true;
        }
        public static bool CreateHTML(Com_Dimension singleDimen, out string htmlPath)
        {
            htmlPath = "";
            try
            {
                string descriptionText = "<html><body style='margin:0px;background-color:rgb(255,255,192)'><font color='black' size='3'>"
                    + CaxExcel.GetDimenUnicode(singleDimen) +
                    "</font></body></html>";
                htmlPath = string.Format(@"{0}temp.html", System.AppDomain.CurrentDomain.BaseDirectory);
                System.IO.File.WriteAllText(htmlPath, descriptionText, Encoding.Unicode);
            }
            catch (System.Exception ex)
            {
                return false;
            }
            return true;
        }
        public static string GetDimenUnicode(Com_Dimension singleDimen)
        {
            string dimenUnicode = "";
            try
            {
                #region 幾何公差
                if (singleDimen.characteristic != "" & singleDimen.characteristic != null & singleDimen.characteristic != "None")
                {
                    dimenUnicode = TransUni(singleDimen.characteristic);
                    dimenUnicode = dimenUnicode + "|";
                }
                if (singleDimen.zoneShape != "" & singleDimen.zoneShape != null & singleDimen.zoneShape != "None")
                {
                    dimenUnicode = dimenUnicode + TransUni(singleDimen.zoneShape);
                }
                if (singleDimen.toleranceValue != "" & singleDimen.toleranceValue != null & singleDimen.toleranceValue != "None")
                {
                    dimenUnicode = dimenUnicode + singleDimen.toleranceValue;
                }
                if (singleDimen.materialModifier != "" & singleDimen.materialModifier != null & singleDimen.materialModifier != "None")
                {
                    dimenUnicode = dimenUnicode + TransUni(singleDimen.materialModifier);
                }
                if (singleDimen.primaryDatum != "" & singleDimen.primaryDatum != null & singleDimen.primaryDatum != "None")
                {
                    dimenUnicode = dimenUnicode + "|";
                    dimenUnicode = dimenUnicode + singleDimen.primaryDatum;
                    if (singleDimen.primaryMaterialModifier != "" & singleDimen.primaryMaterialModifier != null & singleDimen.primaryMaterialModifier != "None")
                    {
                    }
                    else
                    {
                        dimenUnicode = dimenUnicode + "|";
                    }
                }
                if (singleDimen.primaryMaterialModifier != "" & singleDimen.primaryMaterialModifier != null & singleDimen.primaryMaterialModifier != "None")
                {
                    dimenUnicode = dimenUnicode + TransUni(singleDimen.primaryMaterialModifier);
                }
                if (singleDimen.secondaryDatum != "" & singleDimen.secondaryDatum != null & singleDimen.secondaryDatum != "None")
                {
                    dimenUnicode = dimenUnicode + "|";
                    dimenUnicode = dimenUnicode + singleDimen.secondaryDatum;
                    if (singleDimen.secondaryMaterialModifier != "" & singleDimen.secondaryMaterialModifier != null & singleDimen.secondaryMaterialModifier != "None")
                    {
                    }
                    else
                    {
                        dimenUnicode = dimenUnicode + "|";
                    }
                }
                if (singleDimen.secondaryMaterialModifier != "" & singleDimen.secondaryMaterialModifier != null & singleDimen.secondaryMaterialModifier != "None")
                {
                    dimenUnicode = dimenUnicode + TransUni(singleDimen.secondaryMaterialModifier);
                }
                if (singleDimen.tertiaryDatum != "" & singleDimen.tertiaryDatum != null & singleDimen.tertiaryDatum != "None")
                {
                    dimenUnicode = dimenUnicode + "|";
                    dimenUnicode = dimenUnicode + singleDimen.tertiaryDatum;
                    if (singleDimen.tertiaryMaterialModifier != "" & singleDimen.tertiaryMaterialModifier != null & singleDimen.tertiaryMaterialModifier != "None")
                    {
                    }
                    else
                    {
                        dimenUnicode = dimenUnicode + "|";
                    }
                }
                if (singleDimen.tertiaryMaterialModifier != "" & singleDimen.tertiaryMaterialModifier != null & singleDimen.tertiaryMaterialModifier != "None")
                {
                    dimenUnicode = dimenUnicode + TransUni(singleDimen.tertiaryMaterialModifier);
                }
                #endregion

                #region 尺寸公差
                if (singleDimen.aboveText != "" & singleDimen.aboveText != null & singleDimen.aboveText != "None")
                {
                    dimenUnicode = dimenUnicode + singleDimen.aboveText;
                }
                if (singleDimen.beforeText != "" & singleDimen.beforeText != null & singleDimen.beforeText != "None")
                {
                    dimenUnicode = dimenUnicode + TransUni(singleDimen.beforeText);
                }
                if (singleDimen.toleranceSymbol != "" & singleDimen.toleranceSymbol != null & singleDimen.toleranceSymbol != "None")
                {

                    if (singleDimen.toleranceSymbol == "R")
                    {
                        dimenUnicode = dimenUnicode + singleDimen.toleranceSymbol;
                    }
                    else
                    {
                        dimenUnicode = dimenUnicode + TransUni(singleDimen.toleranceSymbol);
                    }
                }
                if (singleDimen.mainText != "" & singleDimen.mainText != null & singleDimen.mainText != "None")
                {
                    char[] splitText = singleDimen.mainText.ToCharArray();
                    for (int i = 0; i < splitText.Length; i++)
                    {
                        if (splitText[i] == '~')
                        {
                            dimenUnicode = dimenUnicode + TransUni(splitText[i].ToString());
                        }
                        else
                        {
                            dimenUnicode = dimenUnicode + splitText[i].ToString();
                        }
                    }
                    //dimenUnicode = dimenUnicode + singleDimen.mainText;
                }
                if ((singleDimen.upTolerance != "" & singleDimen.upTolerance != null & singleDimen.upTolerance != "None") ||
                    (singleDimen.lowTolerance != "" & singleDimen.upTolerance != null & singleDimen.upTolerance != "None"))
                {
                    if (singleDimen.upTolerance == singleDimen.lowTolerance)
                    {
                        //如果上下公差相同，則加入±到字串中
                        dimenUnicode = dimenUnicode + "±";
                        //加入公差的長度
                        char[] splitText = singleDimen.upTolerance.ToCharArray();
                        for (int i = 0; i < splitText.Length; i++)
                        {
                            if (splitText[i] == '~')
                            {
                                dimenUnicode = dimenUnicode + TransUni(splitText[i].ToString());
                            }
                            else
                            {
                                dimenUnicode = dimenUnicode + splitText[i].ToString();
                            }
                        }
                        //dimenUnicode = dimenUnicode + singleDimen.upTolerance;
                    }
                    else
                    {
                        if (Math.Abs(Convert.ToDouble(singleDimen.upTolerance)) * 10000 > 0)
                        {
                            //表示有上公差，所以加入+到字串中，如果上公差為-，則不加+
                            if (singleDimen.upTolerance.Contains('-'))
                            {
                            }
                            else
                            {
                                dimenUnicode = dimenUnicode + "+";
                            }

                            //加入公差的長度
                            char[] splitText = singleDimen.upTolerance.ToCharArray();
                            for (int i = 0; i < splitText.Length; i++)
                            {
                                if (splitText[i] == '~')
                                {
                                    dimenUnicode = dimenUnicode + TransUni(splitText[i].ToString());
                                }
                                else
                                {
                                    dimenUnicode = dimenUnicode + splitText[i].ToString();
                                }
                            }
                            //dimenUnicode = dimenUnicode + singleDimen.upTolerance;
                        }
                        if (Convert.ToDouble(singleDimen.lowTolerance) * 10000 > 0)
                        {
                            dimenUnicode = dimenUnicode + "/";
                            //表示有下公差，所以加入-到字串中，如果下公差為+，則不加-
                            if (singleDimen.lowTolerance.Contains('+'))
                            {
                            }
                            else
                            {
                                dimenUnicode = dimenUnicode + "-";
                            }

                            //加入下公差的長度
                            char[] splitText = singleDimen.lowTolerance.ToCharArray();
                            for (int i = 0; i < splitText.Length; i++)
                            {
                                if (splitText[i] == '~')
                                {
                                    dimenUnicode = dimenUnicode + TransUni(splitText[i].ToString());
                                }
                                else
                                {
                                    dimenUnicode = dimenUnicode + splitText[i].ToString();
                                }
                            }
                            //dimenUnicode = dimenUnicode + singleDimen.lowTolerance;
                        }
                    }
                }
                if (singleDimen.x != "" & singleDimen.x != null & singleDimen.x != "None")
                {
                    dimenUnicode = dimenUnicode + singleDimen.x;
                }
                if (singleDimen.chamferAngle != "" & singleDimen.chamferAngle != null & singleDimen.chamferAngle != "None")
                {
                    char[] splitText = singleDimen.chamferAngle.ToCharArray();
                    for (int i = 0; i < splitText.Length; i++)
                    {
                        if (splitText[i] == '~')
                        {
                            dimenUnicode = dimenUnicode + TransUni(splitText[i].ToString());
                        }
                        else
                        {
                            dimenUnicode = dimenUnicode + splitText[i].ToString();
                        }
                    }
                }
                if (singleDimen.afterText != "" & singleDimen.afterText != null & singleDimen.afterText != "None")
                {
                    char[] splitText = singleDimen.afterText.ToCharArray();
                    for (int i = 0; i < splitText.Length; i++)
                    {
                        if (splitText[i] == '~')
                        {
                            dimenUnicode = dimenUnicode + TransUni(splitText[i].ToString());
                        }
                        else
                        {
                            dimenUnicode = dimenUnicode + splitText[i].ToString();
                        }
                    }
                }
                #endregion
                return dimenUnicode;
            }
            catch (System.Exception ex)
            {
                return dimenUnicode = "";
            }
        }
        public static string TransUni(string inputStr)
        {
            string outputStr = "";
            try
            {
                if (inputStr == "a")
                {
                    outputStr = "\u2220";
                }
                else if (inputStr == "b")
                {
                    outputStr = "\u27C2";
                }
                else if (inputStr == "c")
                {
                    outputStr = "\u23E4";
                }
                else if (inputStr == "d")
                {
                    outputStr = "\u2313";
                }
                else if (inputStr == "e")
                {
                    outputStr = "\u25EF";
                }
                else if (inputStr == "f")
                {
                    outputStr = "\u2225";
                }
                else if (inputStr == "g")
                {
                    outputStr = "\u232D";
                }
                else if (inputStr == "h")
                {
                    outputStr = "\u2197";
                }
                else if (inputStr == "i")
                {
                    outputStr = "\u232F";
                }
                else if (inputStr == "j")
                {
                    outputStr = "\u2316";
                }
                else if (inputStr == "k")
                {
                    outputStr = "\u2312";
                }
                else if (inputStr == "l")
                {
                    outputStr = "\u24C1";
                }
                else if (inputStr == "m")
                {
                    outputStr = "\u24C2";
                }
                else if (inputStr == "n")
                {
                    outputStr = "\u00D8";
                }
                else if (inputStr == "o")
                {
                    outputStr = "\u25A1";
                }
                else if (inputStr == "p")
                {
                    outputStr = "\u24C5";
                }
                else if (inputStr == "q")
                {
                    outputStr = "";
                }
                else if (inputStr == "r")
                {
                    outputStr = "\u25CE";
                }
                else if (inputStr == "s")
                {
                    outputStr = "\u24C8";
                }
                else if (inputStr == "t")
                {
                    outputStr = "\u2330";
                }
                else if (inputStr == "u")
                {
                    outputStr = "\u23E5";
                }
                else if (inputStr == "v")
                {
                    outputStr = "\u2334";
                }
                else if (inputStr == "w")
                {
                    outputStr = "\u2335";
                }
                else if (inputStr == "x")
                {
                    outputStr = "\u21A7";
                }
                else if (inputStr == "y")
                {
                    outputStr = "\u2332";
                }
                else if (inputStr == "z")
                {
                    outputStr = "\u2333";
                }
                else if (inputStr == "~")
                {
                    outputStr = "\u00B0";
                }

                if (outputStr == "")
                {
                    outputStr = inputStr;
                }
                return outputStr;
            }
            catch (System.Exception ex)
            {
                return outputStr = "";
            }
        }
    }
}
