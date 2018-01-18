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
    public class Excel_FAI
    {
        private static bool status;
        private static string OutputPath = "";

        public static bool CreateFAIExcel_XinWu(string cusName, string partNo, string cusVer, string opVer, string op1, DB_MEMain sDB_MEMain, IList<Com_Dimension> cCom_Dimension)
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
                #region 處理第一頁
                workRange = (Range)workSheet.Cells;
                workRange[10, 1] = partNo;
                workRange[10, 2] = sDB_MEMain.comMEMain.partDescription;
                #endregion

                //取得第二頁sheet
                workSheet = (Worksheet)workBook.Sheets[2];
                #region 處理第二頁
                workRange = (Range)workSheet.Cells;
                workRange[10, 1] = partNo;
                workRange[10, 2] = sDB_MEMain.comMEMain.partDescription;
                #endregion

                //取得第三頁sheet
                workSheet = (Worksheet)workBook.Sheets[3];
                #region 處理第三頁
                workRange = (Range)workSheet.Cells;
                workRange[10, 1] = partNo;
                workRange[10, 5] = sDB_MEMain.comMEMain.partDescription;

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
                    workRange = (Range)workSheet.Range["A17"].EntireRow;
                    workRange.Insert(XlInsertShiftDirection.xlShiftDown, workRange.Copy(Type.Missing));
                }

                //設定欄位的Row,Column
                int currentRow = 16, ballonColumn = 1, locationColumn = 2, dimenColumn = 4, instrumentColumn = 6;
                foreach (Com_Dimension i in cCom_Dimension)
                {
                    if (i.balloonCount == null)
                    {
                        workRange = (Range)workSheet.Cells;

                        currentRow = currentRow + 1;

                        status = CaxExcel.MappingDimenData(i, workSheet, currentRow, dimenColumn);
                        //status = Excel_CommonFun.MappingDimenData(cCom_Dimension[i], workRange, currentRow, dimenColumn);
                        if (!status)
                        {
                            MessageBox.Show("MappingDimenData時發生錯誤，請聯繫開發工程師");
                            return false;
                        }

                        #region 檢具、頻率、Max、Min、泡泡、泡泡位置、料號、日期
                        workRange[currentRow, instrumentColumn] = "=" + "\"" + i.instrument.Split('，')[0] + "\"" + "&char(10)&" + "\"" + i.instrument.Split('，')[1] + "\"";
                        workRange[currentRow, ballonColumn] = i.ballon;
                        workRange[currentRow, locationColumn] = i.location;
                        #endregion  
                    }
                    else
                    {
                        for (int j = 0; j < Convert.ToInt32(i.balloonCount); j++)
                        {
                            workRange = (Range)workSheet.Cells;

                            currentRow = currentRow + 1;

                            status = CaxExcel.MappingDimenData(i, workSheet, currentRow, dimenColumn);
                            //status = Excel_CommonFun.MappingDimenData(cCom_Dimension[i], workRange, currentRow, dimenColumn);
                            if (!status)
                            {
                                MessageBox.Show("MappingDimenData時發生錯誤，請聯繫開發工程師");
                                return false;
                            }

                            #region 檢具、頻率、Max、Min、泡泡、泡泡位置、料號、日期
                            workRange[currentRow, instrumentColumn] = "=" + "\"" + i.instrument.Split('，')[0] + "\"" + "&char(10)&" + "\"" + i.instrument.Split('，')[1] + "\"";
                            if (i.balloonCount != null && Convert.ToInt32(i.balloonCount) > 1)
                            {
                                workRange[currentRow, ballonColumn] = i.ballon.ToString() + "." + Convert.ToChar(65 + j).ToString().ToLower();
                            }
                            else
                            {
                                workRange[currentRow, ballonColumn] = i.ballon.ToString();
                            }
                            workRange[currentRow, locationColumn] = i.location;
                            #endregion    
                        }
                    }
                }

                //for (int i = 0; i < cCom_Dimension.Count; i++)
                //{
                //    workRange = (Range)workSheet.Cells;

                //    currentRow = currentRow + 1;

                //    status = CaxExcel.MappingDimenData(cCom_Dimension[i], workSheet, currentRow, dimenColumn);
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
                //    #endregion                    
                //}

                #endregion



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
        public static bool CreateFAIExcel_WuXi(string cusName, string partNo, string cusVer, string opVer, string op1, DB_MEMain sDB_MEMain, IList<Com_Dimension> cCom_Dimension)
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






            }
            catch (System.Exception ex)
            {
                return false;
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
