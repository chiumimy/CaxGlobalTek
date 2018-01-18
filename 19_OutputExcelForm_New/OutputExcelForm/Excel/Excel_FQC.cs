using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using CaxGlobaltek;
using System.IO;
using Microsoft.Office.Interop.Excel;
using System.Windows.Forms;

namespace OutputExcelForm.Excel
{
    public class Excel_FQC
    {
        private static bool status;
        private static string OutputPath = "";

        public static bool CreateFQCExcel_XinWu(string cusName, string partNo, string cusVer, string opVer, string op1, DB_MEMain sDB_MEMain, IList<Com_Dimension> cCom_Dimension)
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
                workRange = (Range)workSheet.Cells;
                workRange[5, 1] = cusName;
                workRange[5, 5] = partNo;
                workRange[5, 6] = cusVer;
                workRange[5, 7] = sDB_MEMain.comMEMain.partDescription;

                //Insert所需欄位
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
                for (int i = 1; i < Dicount; i++)
                {
                    workRange = (Range)workSheet.Range["A8"].EntireRow;
                    workRange.Insert(XlInsertShiftDirection.xlShiftDown, workRange.Copy(Type.Missing));
                }

                //設定欄位的Row,Column
                int currentRow = 7, ballonColumn = 1, locationColumn = 2, dimenColumn = 3, instrumentColumn = 4, checkColumn = 18;
                foreach (Com_Dimension i in cCom_Dimension)
                {
                    if (i.balloonCount == null)
                    {
                        workRange = (Range)workSheet.Cells;
                        //取得Row,Column
                        currentRow = currentRow + 1;

                        status = Excel_CommonFun.MappingDimenData(i, workSheet, currentRow, dimenColumn);
                        //status = Excel_CommonFun.MappingDimenData(cCom_Dimension[i], workRange, currentRow, dimenColumn);
                        if (!status)
                        {
                            MessageBox.Show("MappingDimenData時發生錯誤，請聯繫開發工程師");
                            return false;
                        }

                        #region 檢具、頻率、Max、Min、泡泡、泡泡位置、料號、日期
                        string[] splitInstrument = i.instrument.Split('，');
                        if (splitInstrument[1] != null)
                        {
                            workRange[currentRow, instrumentColumn] = splitInstrument[1];
                        }
                        else
                        {
                            workRange[currentRow, instrumentColumn] = splitInstrument[0];
                        }
                        workRange[currentRow, ballonColumn] = i.ballon;
                        workRange[currentRow, locationColumn] = i.location;
                        workRange[currentRow, checkColumn] = i.checkLevel;
                        #endregion
                    }
                    else
                    {
                        for (int j = 0; j < Convert.ToInt32(i.balloonCount); j++)
                        {
                            workRange = (Range)workSheet.Cells;
                            //取得Row,Column
                            currentRow = currentRow + 1;

                            status = Excel_CommonFun.MappingDimenData(i, workSheet, currentRow, dimenColumn);
                            //status = Excel_CommonFun.MappingDimenData(cCom_Dimension[i], workRange, currentRow, dimenColumn);
                            if (!status)
                            {
                                MessageBox.Show("MappingDimenData時發生錯誤，請聯繫開發工程師");
                                return false;
                            }

                            #region 檢具、頻率、Max、Min、泡泡、泡泡位置、料號、日期
                            string[] splitInstrument = i.instrument.Split('，');
                            if (splitInstrument[1] != null)
                            {
                                workRange[currentRow, instrumentColumn] = splitInstrument[1];
                            }
                            else
                            {
                                workRange[currentRow, instrumentColumn] = splitInstrument[0];
                            }
                            if (i.balloonCount != null && Convert.ToInt32(i.balloonCount) > 1)
                            {
                                workRange[currentRow, ballonColumn] = i.ballon.ToString() + "." + Convert.ToChar(65 + j).ToString().ToLower();
                            }
                            else
                            {
                                workRange[currentRow, ballonColumn] = i.ballon.ToString();
                            }
                            workRange[currentRow, locationColumn] = i.location;
                            workRange[currentRow, checkColumn] = i.checkLevel;
                            #endregion
                        }
                    }
                }

                //for (int i = 0; i < cCom_Dimension.Count; i++)
                //{
                //    workRange = (Range)workSheet.Cells;
                //    //取得Row,Column
                //    currentRow = currentRow + 1;

                //    status = Excel_CommonFun.MappingDimenData(cCom_Dimension[i], workSheet, currentRow, dimenColumn);
                //    //status = Excel_CommonFun.MappingDimenData(cCom_Dimension[i], workRange, currentRow, dimenColumn);
                //    if (!status)
                //    {
                //        MessageBox.Show("MappingDimenData時發生錯誤，請聯繫開發工程師");
                //        return false;
                //    }

                //    #region 檢具、頻率、Max、Min、泡泡、泡泡位置、料號、日期
                //    workRange[currentRow, instrumentColumn] = cCom_Dimension[i].instrument;
                //    workRange[currentRow, ballonColumn] = cCom_Dimension[i].ballon;
                //    workRange[currentRow, locationColumn] = cCom_Dimension[i].location;
                //    workRange[currentRow, checkColumn] = cCom_Dimension[i].checkLevel;
                //    #endregion
                //}



                if (File.Exists(OutputPath))
                {
                    File.Delete(OutputPath);
                }
                workBook.SaveAs(OutputPath, XlFileFormat.xlWorkbookNormal, Type.Missing, Type.Missing, Type.Missing,
                    Type.Missing, XlSaveAsAccessMode.xlNoChange, Type.Missing, Type.Missing, Type.Missing, Type.Missing, Type.Missing);
                workBook.Close(Type.Missing, Type.Missing, Type.Missing);
                excelApp.Quit();

            }
            catch (System.Exception ex)
            {
                MessageBox.Show(ex.ToString());
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
        public static bool CreateFQCExcel_XiAn(string cusName, string partNo, string cusVer, string opVer, string op1, DB_MEMain sDB_MEMain, IList<Com_Dimension> cCom_Dimension)
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
                workRange = (Range)workSheet.Cells;
                workRange[5, 1] = cusName;
                workRange[5, 5] = partNo;
                workRange[5, 8] = sDB_MEMain.comMEMain.partDescription;
                workRange[5, 10] = cusVer;
                

                //設定欄位的Row,Column
                int currentRow = 7, ballonColumn = 1, locationColumn = 2, dimenColumn = 3, instrumentColumn = 4;

                //Insert所需欄位
                for (int i = 1; i < cCom_Dimension.Count; i++)
                {
                    workRange = (Range)workSheet.Range["A8"].EntireRow;
                    workRange.Insert(XlInsertShiftDirection.xlShiftDown, XlInsertFormatOrigin.xlFormatFromRightOrBelow);
                }


                for (int i = 0; i < cCom_Dimension.Count; i++)
                {
                    workRange = (Range)workSheet.Cells;
                    //取得Row,Column
                    currentRow = currentRow + 1;

                    status = Excel_CommonFun.MappingDimenData(cCom_Dimension[i], workSheet, currentRow, dimenColumn);
                    if (!status)
                    {
                        MessageBox.Show("MappingDimenData時發生錯誤，請聯繫開發工程師");
                        return false;
                    }

                    #region 檢具、泡泡、泡泡位置
                    workRange[currentRow, instrumentColumn] = cCom_Dimension[i].instrument;
                    workRange[currentRow, ballonColumn] = cCom_Dimension[i].ballon;
                    workRange[currentRow, locationColumn] = cCom_Dimension[i].location;
                    #endregion
                }

                if (File.Exists(OutputPath))
                {
                    File.Delete(OutputPath);
                }
                workBook.SaveAs(OutputPath, XlFileFormat.xlWorkbookNormal, Type.Missing, Type.Missing, Type.Missing,
                    Type.Missing, XlSaveAsAccessMode.xlNoChange, Type.Missing, Type.Missing, Type.Missing, Type.Missing, Type.Missing);
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
