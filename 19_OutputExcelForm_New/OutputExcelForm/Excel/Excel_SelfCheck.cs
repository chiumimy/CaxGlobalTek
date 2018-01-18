using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using Microsoft.Office.Interop.Excel;
using System.IO;
using CaxGlobaltek;
using System.Windows.Forms;

namespace OutputExcelForm.Excel
{
    public class Excel_SelfCheck
    {
        private static bool status;
        private static string OutputPath = "";

        public struct FactoyName
        {
            public static string XinWu_SelfCheck = "XinWu_SelfCheck";
            public static string XiAn_SelfCheck = "XiAn_SelfCheck";
            public static string WuXi_SelfCheck = "WuXi_SelfCheck";
        }

        public struct RowColumn
        {
            //表單資訊
            public int PartNoRow { get; set; }
            public int PartNoColumn { get; set; }
            public int PartDescRow { get; set; }
            public int PartDescColumn { get; set; }
            public int OISRow { get; set; }
            public int OISColumn { get; set; }
            public int CusRevRow { get; set; }
            public int CusRevColumn { get; set; }
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
        public static void GetExcelRowColumn(int i, string factory, out RowColumn sRowColumn)
        {
            sRowColumn = new RowColumn();

            if (factory == FactoyName.XinWu_SelfCheck)
            {
                #region 新屋格式
                sRowColumn.PartNoRow = 5;
                sRowColumn.PartNoColumn = 4;

                sRowColumn.PartDescRow = 5;
                sRowColumn.PartDescColumn = 9;

                sRowColumn.OISRow = 6;
                sRowColumn.OISColumn = 4;

                sRowColumn.CusRevRow = 6;
                sRowColumn.CusRevColumn = 9;

                sRowColumn.DateRow = 4;
                sRowColumn.DateColumn = 3;

                int currentNo = (i % 12);

                int RowNo = 9;

                RowNo = RowNo + currentNo;

                sRowColumn.BallonRow = RowNo;
                sRowColumn.BallonColumn = 1;

                sRowColumn.LocationRow = RowNo;
                sRowColumn.LocationColumn = 2;

                sRowColumn.DimensionRow = RowNo;
                sRowColumn.DimensionColumn = 3;

                sRowColumn.GaugeRow = RowNo;
                sRowColumn.GaugeColumn = 4;

                sRowColumn.FrequencyRow = RowNo;
                sRowColumn.FrequencyColumn = 8;
                #endregion
            }
            else if (factory == FactoyName.XiAn_SelfCheck)
            {
                #region 西安格式
                sRowColumn.PartNoRow = 4;
                sRowColumn.PartNoColumn = 2;

                //sRowColumn.PartDescRow = 5;
                //sRowColumn.PartDescColumn = 9;

                sRowColumn.OISRow = 4;
                sRowColumn.OISColumn = 8;

                sRowColumn.CusRevRow = 4;
                sRowColumn.CusRevColumn = 5;

                sRowColumn.DateRow = 4;
                sRowColumn.DateColumn = 12;

                int currentNo = (i % 15);

                int RowNo = 0;

                if (currentNo == 0)
                {
                    RowNo = 7;
                }
                else if (currentNo == 1)
                {
                    RowNo = 8;
                }
                else if (currentNo == 2)
                {
                    RowNo = 9;
                }
                else if (currentNo == 3)
                {
                    RowNo = 10;
                }
                else if (currentNo == 4)
                {
                    RowNo = 11;
                }
                else if (currentNo == 5)
                {
                    RowNo = 12;
                }
                else if (currentNo == 6)
                {
                    RowNo = 13;
                }
                else if (currentNo == 7)
                {
                    RowNo = 14;
                }
                else if (currentNo == 8)
                {
                    RowNo = 15;
                }
                else if (currentNo == 9)
                {
                    RowNo = 16;
                }
                else if (currentNo == 10)
                {
                    RowNo = 17;
                }
                else if (currentNo == 11)
                {
                    RowNo = 18;
                }
                else if (currentNo == 12)
                {
                    RowNo = 19;
                }
                else if (currentNo == 13)
                {
                    RowNo = 20;
                }
                else if (currentNo == 14)
                {
                    RowNo = 21;
                }

                sRowColumn.BallonRow = RowNo;
                sRowColumn.BallonColumn = 1;

                sRowColumn.LocationRow = RowNo;
                sRowColumn.LocationColumn = 2;

                sRowColumn.DimensionRow = RowNo;
                sRowColumn.DimensionColumn = 3;

                sRowColumn.GaugeRow = RowNo;
                sRowColumn.GaugeColumn = 4;

                sRowColumn.FrequencyRow = RowNo;
                sRowColumn.FrequencyColumn = 8;
                #endregion
            }
            

        }
        
        public static bool CreateSelfCheckExcel_XinWu(string cusName, string partNo, string cusVer, string opVer, string op1, DB_MEMain sDB_MEMain, IList<Com_Dimension> cCom_Dimension)
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
                    return false;
                }

                //修改每一個Sheet名字和頁數
                status = Excel_CommonFun.ModifySheet(partNo, "SelfCheck", workBook, workSheet, workRange, sDB_MEMain.factory);
                if (!status)
                {
                    MessageBox.Show("修改Sheet名字和頁數失敗，請聯繫開發工程師");
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
                        //status = Excel_CommonFun.MappingDimenData(cCom_Dimension[i], workRange, sRowColumn.DimensionRow, sRowColumn.DimensionColumn);
                        if (!status)
                        {
                            MessageBox.Show("MappingDimenData時發生錯誤，請聯繫開發工程師");
                            return false;
                        }

                        #region 檢具、頻率、Max、Min、泡泡、泡泡位置、料號、日期
                        workRange[sRowColumn.GaugeRow, sRowColumn.GaugeColumn] = "=" + "\"" + i.instrument.Split('，')[0] + "\"" + "&char(10)&" + "\"" + i.instrument.Split('，')[1] + "\"";
                        workRange[sRowColumn.FrequencyRow, sRowColumn.FrequencyColumn] = i.frequency;
                        workRange[sRowColumn.BallonRow, sRowColumn.BallonColumn] = i.ballon;
                        workRange[sRowColumn.LocationRow, sRowColumn.LocationColumn] = i.location;
                        workRange[sRowColumn.PartNoRow, sRowColumn.PartNoColumn] = partNo + string.Format("({0})", cusVer);
                        //workRange[sRowColumn.DateRow, sRowColumn.DateColumn] = DateTime.Now.ToShortDateString();
                        workRange[sRowColumn.PartDescRow, sRowColumn.PartDescColumn] = sDB_MEMain.comMEMain.partDescription;
                        workRange[sRowColumn.OISRow, sRowColumn.OISColumn] = op1;
                        workRange[sRowColumn.CusRevRow, sRowColumn.CusRevColumn] = sDB_MEMain.comMEMain.draftingVer;
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
                            //status = Excel_CommonFun.MappingDimenData(cCom_Dimension[i], workRange, sRowColumn.DimensionRow, sRowColumn.DimensionColumn);
                            if (!status)
                            {
                                MessageBox.Show("MappingDimenData時發生錯誤，請聯繫開發工程師");
                                return false;
                            }

                            #region 檢具、頻率、Max、Min、泡泡、泡泡位置、料號、日期
                            workRange[sRowColumn.GaugeRow, sRowColumn.GaugeColumn] = "=" + "\"" + i.instrument.Split('，')[0] + "\"" + "&char(10)&" + "\"" + i.instrument.Split('，')[1] + "\"";
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
                            //workRange[sRowColumn.DateRow, sRowColumn.DateColumn] = DateTime.Now.ToShortDateString();
                            workRange[sRowColumn.PartDescRow, sRowColumn.PartDescColumn] = sDB_MEMain.comMEMain.partDescription;
                            workRange[sRowColumn.OISRow, sRowColumn.OISColumn] = op1;
                            workRange[sRowColumn.CusRevRow, sRowColumn.CusRevColumn] = sDB_MEMain.comMEMain.draftingVer;
                            #endregion
                        }
                        
                    }
                }

                //for (int i = 0; i < cCom_Dimension.Count; i++)
                //{
                //    GetExcelRowColumn(i, sDB_MEMain.factory, out sRowColumn);
                //    currentSheet_Value = (i / 12);
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
                //    workRange[sRowColumn.FrequencyRow, sRowColumn.FrequencyColumn] = cCom_Dimension[i].frequency;
                //    workRange[sRowColumn.BallonRow, sRowColumn.BallonColumn] = cCom_Dimension[i].ballon;
                //    workRange[sRowColumn.LocationRow, sRowColumn.LocationColumn] = cCom_Dimension[i].location;
                //    workRange[sRowColumn.PartNoRow, sRowColumn.PartNoColumn] = partNo + string.Format("({0})", cusVer);
                //    //workRange[sRowColumn.DateRow, sRowColumn.DateColumn] = DateTime.Now.ToShortDateString();
                //    workRange[sRowColumn.PartDescRow, sRowColumn.PartDescColumn] = sDB_MEMain.comMEMain.partDescription;
                //    workRange[sRowColumn.OISRow, sRowColumn.OISColumn] = op1;
                //    workRange[sRowColumn.CusRevRow, sRowColumn.CusRevColumn] = sDB_MEMain.comMEMain.draftingVer;
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
        public static bool CreateSelfCheckExcel_XiAn(string cusName, string partNo, string cusVer, string opVer, string op1, DB_MEMain sDB_MEMain, IList<Com_Dimension> cCom_Dimension)
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
                workBook = excelApp.Workbooks.Open(sDB_MEMain.excelTemplateFilePath);
                excelApp.Visible = false;
                workSheet = (Worksheet)workBook.Sheets[1];

                //建立Sheet頁數符合所有的Dimension
                status = Excel_CommonFun.AddNewSheet(cCom_Dimension.Count, 12, excelApp, workSheet);
                if (!status)
                {
                    MessageBox.Show("建立Sheet頁失敗，請聯繫開發工程師");
                    return false;
                }

                //修改每一個Sheet名字和頁數
                status = Excel_CommonFun.ModifySheet(partNo, "SelfCheck", workBook, workSheet, workRange, sDB_MEMain.factory);
                if (!status)
                {
                    MessageBox.Show("修改Sheet名字和頁數失敗，請聯繫開發工程師");
                    return false;
                }

                RowColumn sRowColumn = new RowColumn();
                int currentSheet_Value;
                for (int i = 0; i < cCom_Dimension.Count; i++)
                {
                    GetExcelRowColumn(i, sDB_MEMain.factory, out sRowColumn);
                    currentSheet_Value = (i / 12);
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
                    //status = Excel_CommonFun.MappingDimenData(cCom_Dimension[i], workRange, sRowColumn.DimensionRow, sRowColumn.DimensionColumn);
                    if (!status)
                    {
                        MessageBox.Show("MappingDimenData時發生錯誤，請聯繫開發工程師");
                        return false;
                    }

                    #region 檢具、頻率、Max、Min、泡泡、泡泡位置、料號、日期
                    workRange[sRowColumn.GaugeRow, sRowColumn.GaugeColumn] = cCom_Dimension[i].instrument;
                    workRange[sRowColumn.FrequencyRow, sRowColumn.FrequencyColumn] = cCom_Dimension[i].frequency;
                    workRange[sRowColumn.BallonRow, sRowColumn.BallonColumn] = cCom_Dimension[i].ballon;
                    workRange[sRowColumn.LocationRow, sRowColumn.LocationColumn] = cCom_Dimension[i].location;
                    workRange[sRowColumn.PartNoRow, sRowColumn.PartNoColumn] = partNo;
                    workRange[sRowColumn.DateRow, sRowColumn.DateColumn] = DateTime.Now.ToShortDateString();
                    workRange[sRowColumn.OISRow, sRowColumn.OISColumn] = op1;
                    workRange[sRowColumn.CusRevRow, sRowColumn.CusRevColumn] = cusVer;
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
            System.Runtime.InteropServices.Marshal.ReleaseComObject(workRange);
            System.Runtime.InteropServices.Marshal.ReleaseComObject(workSheet);
            System.Runtime.InteropServices.Marshal.ReleaseComObject(workBook);
            System.Runtime.InteropServices.Marshal.ReleaseComObject(excelApp);
        }
    }
}
