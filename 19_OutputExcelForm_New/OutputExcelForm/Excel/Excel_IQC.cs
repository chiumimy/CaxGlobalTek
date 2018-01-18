using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using Microsoft.Office.Interop.Excel;
using CaxGlobaltek;
using System.IO;
using System.Windows.Forms;

namespace OutputExcelForm.Excel
{
    public class Excel_IQC
    {
        private static bool status;
        private static string OutputPath = "";

        public struct RowColumn
        {
            //表單資訊
            public int PartNoRow { get; set; }
            public int PartNoColumn { get; set; }
            public int PartDescRow { get; set; }
            public int PartDescColumn { get; set; }
            public int OISRow { get; set; }
            public int OISColumn { get; set; }
            public int DateRow { get; set; }
            public int DateColumn { get; set; }
            public int MaterialRow { get; set; }
            public int MaterialColumn { get; set; }
            public int OISRevRow { get; set; }
            public int OISRevColumn { get; set; }

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
        public static void GetExcelRowColumn(int i, out RowColumn sRowColumn)
        {
            sRowColumn = new RowColumn();
            sRowColumn.PartNoRow = 4;
            sRowColumn.PartNoColumn = 4;
            sRowColumn.PartDescRow = 5;
            sRowColumn.PartDescColumn = 4;
            sRowColumn.MaterialRow = 7;
            sRowColumn.MaterialColumn = 8;
            sRowColumn.OISRow = 2;
            sRowColumn.OISColumn = 18;
            sRowColumn.OISRevRow = 3;
            sRowColumn.OISRevColumn = 18;

            int currentNo = (i % 8);

            int RowNo = 10;

            RowNo = RowNo + currentNo;

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
        public static bool CreateIQCExcel_XinWu(string cusName, string partNo, string cusVer, string opVer, string op1, DB_MEMain sDB_MEMain, IList<Com_Dimension> cCom_Dimension)
        {
            //判斷Server的Template是否存在
            if (!File.Exists(sDB_MEMain.excelTemplateFilePath))
                return false;

            ApplicationClass excelApp = new ApplicationClass();
            Workbook workBook = null;
            Worksheet workSheet = null;
            Range workRange = null;
            try
            {
                //設定輸出路徑
                OutputPath = string.Format(@"{0}\{1}_{2}_{3}\{4}_{5}_{6}_{7}"
                                                    , Environment.GetFolderPath(Environment.SpecialFolder.Desktop)
                                                    , partNo
                                                    , cusVer
                                                    , opVer
                                                    , partNo
                                                    , cusVer
                                                    , opVer
                                                    , op1 + "_" + sDB_MEMain.factory + ".xls");

                
                
                //1.開啟Excel
                //2.將Excel設為不顯示
                //3.取得第一頁Sheet
                workBook = (Workbook)excelApp.Workbooks.Open(sDB_MEMain.excelTemplateFilePath);
                excelApp.Visible = false;
                workSheet = (Worksheet)workBook.Sheets[1];
                
                //建立Sheet頁數符合所有的Dimension
                int Dicount = 0;
                foreach (Com_Dimension i in cCom_Dimension)
                {
                    if (i.balloonCount != null)
                    {
                        Dicount = Convert.ToInt32(i.balloonCount) + Dicount;
                    }
                    else
                    {
                        Dicount++;
                    }
                }
                status = Excel_CommonFun.AddNewSheet(Dicount, 8, excelApp, workSheet);
                if (!status)
                {
                    MessageBox.Show("建立Sheet頁失敗，請聯繫開發工程師");
                    return false;
                }
                
                //修改每一個Sheet名字和頁數
                status = Excel_CommonFun.ModifySheet(partNo, "IQC", workBook, workSheet, workRange, sDB_MEMain.factory);
                if (!status)
                {
                    MessageBox.Show("修改Sheet名字失敗，請聯繫開發工程師");
                    return false;
                }
                
                RowColumn sRowColumn = new RowColumn();
                int currentSheet_Value;
                int count = -1;
                foreach (Com_Dimension i in cCom_Dimension)
                {
                    if (i.balloonCount == null)
                    {
                        count++;
                        GetExcelRowColumn(count, out sRowColumn);
                        currentSheet_Value = (count / 8);
                        if (currentSheet_Value == 0)
                        {
                            workSheet = (Worksheet)workBook.Sheets[1];
                        }
                        else
                        {
                            workSheet = (Worksheet)workBook.Sheets[currentSheet_Value + 1];
                        }
                        workRange = (Range)workSheet.Cells;

                        status = Excel_CommonFun.MappingDimenData(i, workSheet, sRowColumn.DimensionRow, sRowColumn.DimensionColumn);
                        //status = Excel_CommonFun.MappingDimenData(cCom_Dimension[i], workRange, sRowColumn.DimensionRow, sRowColumn.DimensionColumn);
                        if (!status)
                        {
                            MessageBox.Show("MappingDimenData時發生錯誤，請聯繫開發工程師");
                            return false;
                        }

                        #region 檢具、頻率、Max、Min、泡泡、泡泡位置、料號、日期
                        workRange[sRowColumn.GaugeRow, sRowColumn.GaugeColumn] = "=" + "\"" + i.instrument.Split('，')[0] + "\"" + "&char(10)&" + "\"" + i.instrument.Split('，')[1] + "\"";
                        workRange[sRowColumn.BallonRow, sRowColumn.BallonColumn] = i.ballon;
                        workRange[sRowColumn.LocationRow, sRowColumn.LocationColumn] = i.location;
                        workRange[sRowColumn.PartNoRow, sRowColumn.PartNoColumn] = partNo + string.Format("({0})", cusVer);
                        workRange[sRowColumn.PartDescRow, sRowColumn.PartDescColumn] = sDB_MEMain.comMEMain.partDescription;
                        workRange[sRowColumn.MaterialRow, sRowColumn.MaterialColumn] = sDB_MEMain.comMEMain.material;
                        workRange[sRowColumn.OISRow, sRowColumn.OISColumn] = op1;
                        workRange[sRowColumn.OISRevRow, sRowColumn.OISRevColumn] = sDB_MEMain.comMEMain.draftingVer;
                        #endregion
                    }
                    else
                    {
                        for (int j = 0; j < Convert.ToInt32(i.balloonCount); j++)
                        {
                            count++;
                            GetExcelRowColumn(count, out sRowColumn);
                            currentSheet_Value = (count / 8);
                            if (currentSheet_Value == 0)
                            {
                                workSheet = (Worksheet)workBook.Sheets[1];
                            }
                            else
                            {
                                workSheet = (Worksheet)workBook.Sheets[currentSheet_Value + 1];
                            }
                            workRange = (Range)workSheet.Cells;

                            status = Excel_CommonFun.MappingDimenData(i, workSheet, sRowColumn.DimensionRow, sRowColumn.DimensionColumn);
                            //status = Excel_CommonFun.MappingDimenData(cCom_Dimension[i], workRange, sRowColumn.DimensionRow, sRowColumn.DimensionColumn);
                            if (!status)
                            {
                                MessageBox.Show("MappingDimenData時發生錯誤，請聯繫開發工程師");
                                return false;
                            }

                            #region 檢具、頻率、Max、Min、泡泡、泡泡位置、料號、日期
                            workRange[sRowColumn.GaugeRow, sRowColumn.GaugeColumn] = "=" + "\"" + i.instrument.Split('，')[0] + "\"" + "&char(10)&" + "\"" + i.instrument.Split('，')[1] + "\"";
                            if (i.balloonCount != null && Convert.ToInt32(i.balloonCount) > 1)
                            {
                                workRange[sRowColumn.BallonRow, sRowColumn.BallonColumn] = i.ballon.ToString() + "." + Convert.ToChar(65 + j).ToString().ToLower();
                            }
                            else
                            {
                                workRange[sRowColumn.BallonRow, sRowColumn.BallonColumn] = i.ballon.ToString();
                            }
                            workRange[sRowColumn.LocationRow, sRowColumn.LocationColumn] = i.location;
                            workRange[sRowColumn.PartNoRow, sRowColumn.PartNoColumn] = partNo + string.Format("({0})", cusVer);
                            workRange[sRowColumn.PartDescRow, sRowColumn.PartDescColumn] = sDB_MEMain.comMEMain.partDescription;
                            workRange[sRowColumn.MaterialRow, sRowColumn.MaterialColumn] = sDB_MEMain.comMEMain.material;
                            workRange[sRowColumn.OISRow, sRowColumn.OISColumn] = op1;
                            workRange[sRowColumn.OISRevRow, sRowColumn.OISRevColumn] = sDB_MEMain.comMEMain.draftingVer;
                            #endregion
                        }
                    }
                }
                //for (int i = 0; i < cCom_Dimension.Count; i++)
                //{
                //    GetExcelRowColumn(i, out sRowColumn);
                //    currentSheet_Value = (i / 8);
                //    if (currentSheet_Value == 0)
                //    {
                //        workSheet = (Worksheet)workBook.Sheets[1];
                //    }
                //    else
                //    {
                //        workSheet = (Worksheet)workBook.Sheets[currentSheet_Value + 1];
                //    }
                //    workRange = (Range)workSheet.Cells;

                //    status = Excel_CommonFun.MappingDimenData(cCom_Dimension[i], workSheet, sRowColumn.DimensionRow, sRowColumn.DimensionColumn);
                //    //status = Excel_CommonFun.MappingDimenData(cCom_Dimension[i], workRange, sRowColumn.DimensionRow, sRowColumn.DimensionColumn);
                //    if (!status)
                //    {
                //        MessageBox.Show("MappingDimenData時發生錯誤，請聯繫開發工程師");
                //        return false;
                //    }

                //    #region 檢具、頻率、Max、Min、泡泡、泡泡位置、料號、日期
                //    workRange[sRowColumn.GaugeRow, sRowColumn.GaugeColumn] = cCom_Dimension[i].instrument;
                //    workRange[sRowColumn.BallonRow, sRowColumn.BallonColumn] = cCom_Dimension[i].ballon;
                //    workRange[sRowColumn.LocationRow, sRowColumn.LocationColumn] = cCom_Dimension[i].location;
                //    workRange[sRowColumn.PartNoRow, sRowColumn.PartNoColumn] = partNo + string.Format("({0})", cusVer);
                //    workRange[sRowColumn.PartDescRow, sRowColumn.PartDescColumn] = sDB_MEMain.comMEMain.partDescription;
                //    workRange[sRowColumn.MaterialRow, sRowColumn.MaterialColumn] = sDB_MEMain.comMEMain.material;
                //    workRange[sRowColumn.OISRow, sRowColumn.OISColumn] = op1;
                //    workRange[sRowColumn.OISRevRow, sRowColumn.OISRevColumn] = sDB_MEMain.comMEMain.draftingVer;
                //    #endregion
                //}


                if (File.Exists(OutputPath))
                {
                    File.Delete(OutputPath);
                }
                workBook.SaveAs(OutputPath, XlFileFormat.xlWorkbookNormal, Type.Missing, Type.Missing, Type.Missing, Type.Missing
                    , XlSaveAsAccessMode.xlNoChange, Type.Missing, Type.Missing, Type.Missing, Type.Missing, Type.Missing);
                workBook.Close(Type.Missing, Type.Missing, Type.Missing);
                excelApp.Quit();
            }
            catch (System.Exception ex)
            {
                if (File.Exists(OutputPath))
                {
                    File.Delete(OutputPath);
                }
                workBook.SaveAs(OutputPath, XlFileFormat.xlWorkbookNormal, Type.Missing, Type.Missing, Type.Missing,
                       Type.Missing, XlSaveAsAccessMode.xlNoChange, Type.Missing, Type.Missing, Type.Missing, Type.Missing, Type.Missing);
                workBook.Close(Type.Missing, Type.Missing, Type.Missing);
                excelApp.Quit();
                File.Delete(OutputPath);
                return false;
            }
            finally
            {
                Dispose(excelApp, workBook, workSheet, workRange);
            }
            return true;
        }
        public static void Dispose(ApplicationClass excelApp, Workbook workBook, Worksheet workSheet, Range workRange)
        {
            System.Runtime.InteropServices.Marshal.ReleaseComObject(workRange);
            System.Runtime.InteropServices.Marshal.ReleaseComObject(workSheet);
            System.Runtime.InteropServices.Marshal.ReleaseComObject(workBook);
            System.Runtime.InteropServices.Marshal.ReleaseComObject(excelApp);
        }
    }
}
