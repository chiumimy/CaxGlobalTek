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
    public class Excel_IPQC
    {
        private static bool status;
        private static string OutputPath = "";

        public struct FactoyName
        {
            public static string XinWu_IPQC = "XinWu_IPQC";
            public static string XiAn_IPQC = "XiAn_IPQC";
            public static string WuXi_IPQC = "WuXi_IPQC";
        }

        public struct RowColumn
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

            //ToolNoControl
            public int ToolNoControlRow { get; set; }
            public int ToolNoControlColumn { get; set; }

            //CheckLevel
            public int CheckLevelRow { get; set; }
            public int CheckLevelColumn { get; set; }
        }

        public static void GetExcelRowColumn(int i, string factory, out RowColumn sRowColumn)
        {

            sRowColumn = new RowColumn();

            if (factory == FactoyName.XinWu_IPQC)
            {
                sRowColumn.PartNoRow = 5;
                sRowColumn.PartNoColumn = 4;
                sRowColumn.OISRow = 5;
                sRowColumn.OISColumn = 6;
                sRowColumn.OISRevRow = 5;
                sRowColumn.OISRevColumn = 8;
                sRowColumn.DateRow = 5;
                sRowColumn.DateColumn = 19;

                int currentNo = (i % 12);

                int RowNo = 9;

                RowNo = RowNo + currentNo;

                sRowColumn.BallonRow = RowNo;
                sRowColumn.BallonColumn = 2;

                sRowColumn.LocationRow = RowNo;
                sRowColumn.LocationColumn = 3;

                sRowColumn.DimensionRow = RowNo;
                sRowColumn.DimensionColumn = 4;

                sRowColumn.GaugeRow = RowNo;
                sRowColumn.GaugeColumn = 5;

                sRowColumn.FrequencyRow = RowNo;
                sRowColumn.FrequencyColumn = 7;

                sRowColumn.CheckLevelRow = RowNo;
                sRowColumn.CheckLevelColumn = 18;
            }
            else if (factory == FactoyName.XiAn_IPQC)
            {
                sRowColumn.PartNoRow = 4;
                sRowColumn.PartNoColumn = 2;
                sRowColumn.OISRow = 4;
                sRowColumn.OISColumn = 6;
                sRowColumn.OISRevRow = 4;
                sRowColumn.OISRevColumn = 4;
                sRowColumn.DateRow = 4;
                sRowColumn.DateColumn = 12;

                int currentNo = (i % 15);

                int RowNo = 7;

                RowNo = RowNo + currentNo;

                sRowColumn.BallonRow = RowNo;
                sRowColumn.BallonColumn = 1;

                sRowColumn.LocationRow = RowNo;
                sRowColumn.LocationColumn = 2;

                sRowColumn.DimensionRow = RowNo;
                sRowColumn.DimensionColumn = 3;

                sRowColumn.GaugeRow = RowNo;
                sRowColumn.GaugeColumn = 4;

                sRowColumn.ToolNoControlRow = RowNo;
                sRowColumn.ToolNoControlColumn = 5;

                sRowColumn.FrequencyRow = RowNo;
                sRowColumn.FrequencyColumn = 6;
            }
            
        }
        public static bool CreateIPQCExcel_XinWu(string cusName, string partNo, string cusVer, string opVer, string op1, DB_MEMain sDB_MEMain, IList<Com_Dimension> cCom_Dimension)
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

                //判斷Server的Template是否存在
                if (!File.Exists(sDB_MEMain.excelTemplateFilePath))
                {
                    return false;
                }

                //1.開啟Excel
                //2.將Excel設為不顯示
                //3.取得第一頁Sheet
                workBook = (Workbook)excelApp.Workbooks.Open(sDB_MEMain.excelTemplateFilePath);
                excelApp.Visible = false;
                workSheet = (Worksheet)workBook.Sheets[1];

                //建立Sheet頁數符合所有的Dimension，因舊資料沒有balloonCount的資訊，所以要防呆
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
                status = Excel_CommonFun.AddNewSheet(Dicount, 12, excelApp, workSheet);
                if (!status)
                {
                    MessageBox.Show("建立Sheet頁失敗，請聯繫開發工程師");
                    workBook.SaveAs(sDB_MEMain.excelTemplateFilePath, XlFileFormat.xlWorkbookNormal, Type.Missing, Type.Missing, Type.Missing, Type.Missing
                        , XlSaveAsAccessMode.xlNoChange, Type.Missing, Type.Missing, Type.Missing, Type.Missing, Type.Missing);
                    workBook.Close(Type.Missing, Type.Missing, Type.Missing);
                    excelApp.Quit();
                    return false;
                }

                //修改每一個Sheet名字和頁數
                status = Excel_CommonFun.ModifySheet(partNo, "IPQC", workBook, workSheet, workRange, sDB_MEMain.factory);
                if (!status)
                {
                    MessageBox.Show("修改Sheet名字和頁數失敗，請聯繫開發工程師");
                    //workBook.SaveAs(sDB_MEMain.excelTemplateFilePath, XlFileFormat.xlWorkbookNormal, Type.Missing, Type.Missing, Type.Missing, Type.Missing
                    //    , XlSaveAsAccessMode.xlNoChange, Type.Missing, Type.Missing, Type.Missing, Type.Missing, Type.Missing);
                    workBook.Close(Type.Missing, Type.Missing, Type.Missing);
                    excelApp.Quit();
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
                        GetExcelRowColumn(count, sDB_MEMain.factory, out sRowColumn);
                        currentSheet_Value = (count / 12);
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
                        if (!status)
                        {
                            MessageBox.Show("MappingDimenData時發生錯誤，請聯繫開發工程師");
                            workBook.Close(Type.Missing, Type.Missing, Type.Missing);
                            excelApp.Quit();
                            return false;
                        }

                        #region 檢具、頻率、Max、Min、泡泡、泡泡位置、料號、日期、檢驗等級
                        workRange[sRowColumn.GaugeRow, sRowColumn.GaugeColumn] = "=" + "\"" + i.instrument.Split('，')[0] + "\"" + "&char(10)&" + "\"" + i.instrument.Split('，')[1] + "\"";
                        workRange[sRowColumn.FrequencyRow, sRowColumn.FrequencyColumn] = i.frequency;
                        workRange[sRowColumn.BallonRow, sRowColumn.BallonColumn] = i.ballon.ToString();
                        workRange[sRowColumn.LocationRow, sRowColumn.LocationColumn] = i.location;
                        workRange[sRowColumn.PartNoRow, sRowColumn.PartNoColumn] = partNo + string.Format("({0})", cusVer);
                        workRange[sRowColumn.OISRow, sRowColumn.OISColumn] = op1;
                        workRange[sRowColumn.OISRevRow, sRowColumn.OISRevColumn] = sDB_MEMain.comMEMain.draftingVer;
                        //workRange[sRowColumn.DateRow, sRowColumn.DateColumn] = DateTime.Now.ToShortDateString();
                        workRange[sRowColumn.CheckLevelRow, sRowColumn.CheckLevelColumn] = i.checkLevel;
                        #endregion
                    }
                    else
                    {
                        for (int j = 0; j < Convert.ToInt32(i.balloonCount); j++)
                        {
                            count++;
                            GetExcelRowColumn(count, sDB_MEMain.factory, out sRowColumn);
                            currentSheet_Value = (count / 12);
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
                            if (!status)
                            {
                                MessageBox.Show("MappingDimenData時發生錯誤，請聯繫開發工程師");
                                workBook.Close(Type.Missing, Type.Missing, Type.Missing);
                                excelApp.Quit();
                                return false;
                            }

                            #region 檢具、頻率、Max、Min、泡泡、泡泡位置、料號、日期、檢驗等級
                            //MessageBox.Show(i.instrument.Split('，')[0] + "&chr(10)&" + i.instrument.Split('，')[1]);
                            workRange[sRowColumn.GaugeRow, sRowColumn.GaugeColumn] = "=" + "\"" + i.instrument.Split('，')[0] + "\"" + "&char(10)&" + "\"" + i.instrument.Split('，')[1] + "\"";
                            //workRange[sRowColumn.GaugeRow, sRowColumn.GaugeColumn] = i.instrument;
                            workRange[sRowColumn.FrequencyRow, sRowColumn.FrequencyColumn] = i.frequency;
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
                            workRange[sRowColumn.OISRow, sRowColumn.OISColumn] = op1;
                            workRange[sRowColumn.OISRevRow, sRowColumn.OISRevColumn] = sDB_MEMain.comMEMain.draftingVer;
                            //workRange[sRowColumn.DateRow, sRowColumn.DateColumn] = DateTime.Now.ToShortDateString();
                            workRange[sRowColumn.CheckLevelRow, sRowColumn.CheckLevelColumn] = i.checkLevel;
                            #endregion
                        }
                    }
                }
                
                /*
                for (int i = 0; i < cCom_Dimension.Count; i++)
                {
                    GetExcelRowColumn(i, sDB_MEMain.factory, out sRowColumn);
                    currentSheet_Value = (i / 13);
                    if (currentSheet_Value == 0)
                    {
                        workSheet = (Worksheet)workBook.Sheets[1];
                    }
                    else
                    {
                        workSheet = (Worksheet)workBook.Sheets[currentSheet_Value + 1];
                    }
                    workRange = (Range)workSheet.Cells;
                    
                    status = Excel_CommonFun.MappingDimenData(cCom_Dimension[i], workSheet, sRowColumn.DimensionRow, sRowColumn.DimensionColumn);
                    if (!status)
                    {
                        MessageBox.Show("MappingDimenData時發生錯誤，請聯繫開發工程師");
                        workBook.Close(Type.Missing, Type.Missing, Type.Missing);
                        excelApp.Quit();
                        return false;
                    }

                    #region 檢具、頻率、Max、Min、泡泡、泡泡位置、料號、日期、檢驗等級
                    workRange[sRowColumn.GaugeRow, sRowColumn.GaugeColumn] = cCom_Dimension[i].instrument;
                    workRange[sRowColumn.FrequencyRow, sRowColumn.FrequencyColumn] = cCom_Dimension[i].frequency;
                    workRange[sRowColumn.BallonRow, sRowColumn.BallonColumn] = cCom_Dimension[i].ballon;
                    workRange[sRowColumn.LocationRow, sRowColumn.LocationColumn] = cCom_Dimension[i].location;
                    workRange[sRowColumn.PartNoRow, sRowColumn.PartNoColumn] = partNo + string.Format("({0})", cusVer);
                    workRange[sRowColumn.OISRow, sRowColumn.OISColumn] = op1;
                    workRange[sRowColumn.OISRevRow, sRowColumn.OISRevColumn] = sDB_MEMain.comMEMain.draftingVer;
                    //workRange[sRowColumn.DateRow, sRowColumn.DateColumn] = DateTime.Now.ToShortDateString();
                    workRange[sRowColumn.CheckLevelRow, sRowColumn.CheckLevelColumn] = cCom_Dimension[i].checkLevel;
                    #endregion
                }
                */

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
        public static bool CreateIPQCExcel_XiAn(string cusName, string partNo, string cusVer, string opVer, string op1, DB_MEMain sDB_MEMain, IList<Com_Dimension> cCom_Dimension)
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

                //判斷Server的Template是否存在
                if (!File.Exists(sDB_MEMain.excelTemplateFilePath))
                {
                    return false;
                }

                //1.開啟Excel
                //2.將Excel設為不顯示
                //3.取得第一頁Sheet
                workBook = excelApp.Workbooks.Open(sDB_MEMain.excelTemplateFilePath);
                excelApp.Visible = false;
                workSheet = (Worksheet)workBook.Sheets[1];

                //建立Sheet頁數符合所有的Dimension
                status = Excel_CommonFun.AddNewSheet(cCom_Dimension.Count, 15, excelApp, workSheet);
                if (!status)
                {
                    MessageBox.Show("建立Sheet頁失敗，請聯繫開發工程師");
                    workBook.SaveAs(sDB_MEMain.excelTemplateFilePath, XlFileFormat.xlWorkbookNormal, Type.Missing, Type.Missing, Type.Missing, Type.Missing
                        , XlSaveAsAccessMode.xlNoChange, Type.Missing, Type.Missing, Type.Missing, Type.Missing, Type.Missing);
                    workBook.Close(Type.Missing, Type.Missing, Type.Missing);
                    excelApp.Quit();
                    return false;
                }

                //修改每一個Sheet名字和頁數
                status = Excel_CommonFun.ModifySheet(partNo, "IPQC", workBook, workSheet, workRange, sDB_MEMain.factory);
                if (!status)
                {
                    MessageBox.Show("修改Sheet名字和頁數失敗，請聯繫開發工程師");
                    //workBook.SaveAs(sDB_MEMain.excelTemplateFilePath, XlFileFormat.xlWorkbookNormal, Type.Missing, Type.Missing, Type.Missing, Type.Missing
                    //    , XlSaveAsAccessMode.xlNoChange, Type.Missing, Type.Missing, Type.Missing, Type.Missing, Type.Missing);
                    workBook.Close(Type.Missing, Type.Missing, Type.Missing);
                    excelApp.Quit();
                    return false;
                }

                RowColumn sRowColumn = new RowColumn();
                int currentSheet_Value;
                for (int i = 0; i < cCom_Dimension.Count; i++)
                {
                    GetExcelRowColumn(i, sDB_MEMain.factory, out sRowColumn);
                    currentSheet_Value = (i / 15);
                    if (currentSheet_Value == 0)
                    {
                        workSheet = (Worksheet)workBook.Sheets[1];
                    }
                    else
                    {
                        workSheet = (Worksheet)workBook.Sheets[currentSheet_Value + 1];
                    }
                    workRange = (Range)workSheet.Cells; /*workSheet.Range[sRowColumn.DimensionRow, sRowColumn.DimensionColumn].Characters[1]*/

                    status = Excel_CommonFun.MappingDimenData(cCom_Dimension[i], workSheet, sRowColumn.DimensionRow, sRowColumn.DimensionColumn);
                    if (!status)
                    {
                        MessageBox.Show("MappingDimenData時發生錯誤，請聯繫開發工程師");
                        //workBook.SaveAs(sDB_MEMain.excelTemplateFilePath, XlFileFormat.xlWorkbookNormal, Type.Missing, Type.Missing, Type.Missing, Type.Missing
                        //, XlSaveAsAccessMode.xlNoChange, Type.Missing, Type.Missing, Type.Missing, Type.Missing, Type.Missing);
                        workBook.Close(Type.Missing, Type.Missing, Type.Missing);
                        excelApp.Quit();
                        return false;
                    }

                    #region 泡泡、泡泡位置、檢具、刀號、頻率、料號、製程序、客戶版次、日期
                    workRange[sRowColumn.BallonRow, sRowColumn.BallonColumn] = cCom_Dimension[i].ballon;
                    workRange[sRowColumn.LocationRow, sRowColumn.LocationColumn] = cCom_Dimension[i].location;
                    workRange[sRowColumn.GaugeRow, sRowColumn.GaugeColumn] = cCom_Dimension[i].instrument;
                    workRange[sRowColumn.ToolNoControlRow, sRowColumn.ToolNoControlColumn] = cCom_Dimension[i].toolNoControl;
                    workRange[sRowColumn.FrequencyRow, sRowColumn.FrequencyColumn] = cCom_Dimension[i].frequency;
                    workRange[sRowColumn.PartNoRow, sRowColumn.PartNoColumn] = partNo;
                    workRange[sRowColumn.OISRow, sRowColumn.OISColumn] = op1;
                    workRange[sRowColumn.OISRevRow, sRowColumn.OISRevColumn] = cusVer;
                    workRange[sRowColumn.DateRow, sRowColumn.DateColumn] = DateTime.Now.ToShortDateString();
                    #endregion
                }

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
            System.Runtime.InteropServices.Marshal.FinalReleaseComObject(workRange);
            System.Runtime.InteropServices.Marshal.FinalReleaseComObject(workSheet);
            System.Runtime.InteropServices.Marshal.FinalReleaseComObject(workBook);
            System.Runtime.InteropServices.Marshal.FinalReleaseComObject(excelApp);
        }
    }
}
