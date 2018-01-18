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
    public class Excel_ControlPlan
    {
        private static bool status;
        public static string OutputPath = "";

        public static bool CreateCPExcel(DB_CPKey sDB_CP, List<DB_CPValue> sDB_CPValue)
        {
            //判斷Server的Template是否存在
            if (!File.Exists(sDB_CP.excelTemplateFilePath))
                return false;

            ApplicationClass excelApp = new ApplicationClass();
            Workbook workBook = null;
            Worksheet workSheet = null;
            Range workRange = null;
            try
            {
                //設定輸出路徑
                OutputPath = string.Format(@"{0}\{1}_{2}_{3}\{4}_{5}_{6}"
                                                    , Environment.GetFolderPath(Environment.SpecialFolder.Desktop)
                                                    , sDB_CP.PartNo
                                                    , sDB_CP.CusVer
                                                    , sDB_CP.OpVer
                                                    , sDB_CP.PartNo
                                                    , sDB_CP.CusVer
                                                    , sDB_CP.OpVer + "_" + "ControlPlan" + ".xls");

                

                //1.開啟Excel
                //2.將Excel設為不顯示
                //3.取得第一頁Sheet

                workBook = (Workbook)excelApp.Workbooks.Open(sDB_CP.excelTemplateFilePath);
                excelApp.Visible = false;
                workSheet = (Worksheet)workBook.Sheets[1];
                workRange = (Range)workSheet.Cells;
                workRange[6, 1] = sDB_CP.PartNo;
                workRange[6, 4] = sDB_CP.CusVer;
                workRange[8, 1] = sDB_CP.PartDesc;

                //Insert所需欄位，此處有判斷是否有SPC欄位需要增加
                int dimenCount = 0;
                foreach (DB_CPValue i in sDB_CPValue)
                    foreach (KeyValuePair<int, IList<Com_Dimension>> y in i.DicBalloonData)
                    {
                        int balloonDimenCount = 0;
                        foreach (Com_Dimension k in y.Value)
                        {
                            dimenCount++;
                            balloonDimenCount++;
                            //當該泡泡的尺寸數量輪巡到最後一筆的時候，判斷是否有SPC，如果有則再加一欄
                            if (y.Value.Count == balloonDimenCount & (k.spcControl != null & k.spcControl != ""))
                            {
                                dimenCount++;
                            }
                        }
                    }
                
                //之後可以試看看先紀錄第16行，要插入資料的時候再新增就好
                while (dimenCount - 1 != 0 && dimenCount > 1)
                {
                    workRange = (Range)workSheet.Range["A16"].EntireRow;
                    workRange.Insert(XlInsertShiftDirection.xlShiftDown, XlInsertFormatOrigin.xlFormatFromRightOrBelow);
                    dimenCount--;
                }
                
                //設定欄位的Row,Column
                int
                    currentRow = 16, dimenColumn = 8, op1Column = 1, op2Column = 2, ballonColumn = 4, productColumn = 5, keyChara = 7,
                    instrumentColumn = 9, methodColumn = 12, spcControlColumn = 9, sizeColumn = 10, freqColumn = 11;
                foreach (DB_CPValue i in sDB_CPValue)
                {
                    if (i.DicBalloonData.Count == 0)
                    {
                        continue;
                    }
                    workRange = (Range)workSheet.Cells;
                    workRange[currentRow, op1Column] = i.Op1;
                    workRange[currentRow, op2Column] = i.Op2;
                    string OP1MergeRow = currentRow.ToString();
                    foreach (KeyValuePair<int,IList<Com_Dimension>> j in i.DicBalloonData)
                    {
                        workRange[currentRow, ballonColumn] = j.Key;

                        //判斷是否有SPC，如果有則多合併一個欄位
                        if (j.Value[0].spcControl != null & j.Value[0].spcControl != "")
                        {
                            //合併儲存格-泡泡
                            workSheet.get_Range("D" + currentRow.ToString(), "D" + (currentRow + j.Value.Count).ToString()).Merge(false);
                            //workSheet.get_Range("D:D", Type.Missing).HorizontalAlignment = XlVAlign.xlVAlignCenter;
                            //workSheet.get_Range("D:D", Type.Missing).VerticalAlignment = XlVAlign.xlVAlignDistributed;
                            //合併儲存格-Product
                            workSheet.get_Range("E" + currentRow.ToString(), "E" + (currentRow + j.Value.Count).ToString()).Merge(false);
                            //合併儲存格-KC
                            workSheet.get_Range("G" + currentRow.ToString(), "G" + (currentRow + j.Value.Count).ToString()).Merge(false);
                            //合併儲存格-尺寸
                            workSheet.get_Range("H" + currentRow.ToString(), "H" + (currentRow + j.Value.Count).ToString()).Merge(false);
                        }
                        else
                        {
                            //合併儲存格-泡泡
                            workSheet.get_Range("D" + currentRow.ToString(), "D" + (currentRow + j.Value.Count - 1).ToString()).Merge(false);
                            //workSheet.get_Range("D:D", Type.Missing).HorizontalAlignment = XlVAlign.xlVAlignCenter;
                            //workSheet.get_Range("D:D", Type.Missing).VerticalAlignment = XlVAlign.xlVAlignDistributed;
                            //合併儲存格-Product
                            workSheet.get_Range("E" + currentRow.ToString(), "E" + (currentRow + j.Value.Count - 1).ToString()).Merge(false);
                            //合併儲存格-KC
                            workSheet.get_Range("G" + currentRow.ToString(), "G" + (currentRow + j.Value.Count - 1).ToString()).Merge(false);
                            //合併儲存格-尺寸
                            workSheet.get_Range("H" + currentRow.ToString(), "H" + (currentRow + j.Value.Count - 1).ToString()).Merge(false);
                        }
                        

                        //紀錄泡泡拿來判斷，當出現相同泡泡又要插圖片時只差一個圖片
                        int tempBalloon = 0, count = 0;
                        foreach (Com_Dimension k in j.Value)
                        {
                            //紀錄泡泡拿來判斷，當出現相同泡泡又要插圖片時只插一個圖片
                            tempBalloon = k.ballon;
                            
                            status = Excel_CommonFun.MappingDimenData(k, workSheet, currentRow, dimenColumn);
                            if (!status)
                            {
                                //MessageBox.Show("MappingDimenData時發生錯誤，請聯繫開發工程師");
                                return false;
                            }
                            workRange[currentRow, instrumentColumn] = k.instrument;
                            workRange[currentRow, productColumn] = k.productName;
                            workRange[currentRow, methodColumn] = k.excelType;
                            if (k.excelType != "SelfCheck")
                            {
                                workRange[currentRow, sizeColumn] = k.size;
                                workRange[currentRow, freqColumn] = k.freq;
                            }
                            else
                            {
                                workRange[currentRow, sizeColumn] = k.selfCheck_Size;
                                workRange[currentRow, freqColumn] = k.selfCheck_Freq;
                            }

                            count++;

                            if (count == j.Value.Count)
                            {
                                //當量具資料都插好了之後再插入SPC
                                if (j.Value[0].spcControl != null & j.Value[0].spcControl != "")
                                {
                                    currentRow = currentRow + 1;
                                    workRange[currentRow, spcControlColumn] = k.spcControl;
                                    workRange[currentRow, methodColumn] = "Software";
                                    //合併儲存格-SPC
                                    workSheet.get_Range("I" + currentRow, "K" + currentRow).Merge(false);
                                }
                                if (k.keyChara.Contains("cax"))
                                {
                                    Range tempRange = (Range)workSheet.get_Range("G" + (currentRow - j.Value.Count + 1).ToString());
                                    float left, top, rowHeight;
                                    rowHeight = Convert.ToSingle(tempRange.RowHeight); 
                                    left = Convert.ToSingle(tempRange.Left) + 10;
                                    if (j.Value.Count == 1)
                                    {
                                        top = Convert.ToSingle(tempRange.Top) + Convert.ToSingle(rowHeight / 2 - 5);
                                    }
                                    else
                                    {
                                        top = Convert.ToSingle(tempRange.Top) + Convert.ToSingle((rowHeight * j.Value.Count) / 3);
                                    }

                                    //MessageBox.Show(workRange.Column.ToString());//表示第幾欄
                                    //MessageBox.Show(workRange.ColumnWidth.ToString());//該欄的寬度
                                    workSheet.Shapes.AddPicture(k.keyChara, Microsoft.Office.Core.MsoTriState.msoFalse,
                                       Microsoft.Office.Core.MsoTriState.msoTrue, left,
                                       top, 10, 10);
                                }
                                else
                                {
                                    workRange[currentRow, keyChara] = k.keyChara;
                                }
                            }
                            currentRow = currentRow + 1;
                        }
                    }
                    //合併儲存格-OP1
                    workSheet.get_Range("A" + OP1MergeRow, "A" + (currentRow - 1).ToString()).Merge(false);
                    //合併儲存格-OP2
                    workSheet.get_Range("B" + OP1MergeRow, "B" + (currentRow - 1).ToString()).Merge(false);

                }


                /*
                foreach (DB_CPValue i in sDB_CPValue)
                {
                    workRange = (Range)workSheet.Cells;
                    workRange[currentRow, op1Column] = i.Op1;
                    workRange[currentRow, op2Column] = i.Op2;
                    foreach (Com_Dimension y in i.comDimension)
                    {
                        workRange[currentRow, ballonColumn] = y.ballon;
                        workRange[currentRow, productColumn] = y.productName;
                        workRange[currentRow, keyChara] = y.keyChara;
                        status = Excel_CommonFun.MappingDimenData(y, workSheet, currentRow, dimenColumn);
                        if (!status)
                        {
                            MessageBox.Show("MappingDimenData時發生錯誤，請聯繫開發工程師");
                            return false;
                        }
                        workRange[currentRow, instrumentColumn] = y.instrument;
                        workRange[currentRow, methodColumn] = y.excelType;
                        currentRow = currentRow + 1;
                        int balloonNum = y.ballon;
                        if (y.ballon == balloonNum)
                        {
                            workSheet.get_Range("D16", "D17").Merge(false);
                            workSheet.get_Range("D:D", Type.Missing).HorizontalAlignment = XlVAlign.xlVAlignCenter;
                            workSheet.get_Range("D:D", Type.Missing).VerticalAlignment = XlVAlign.xlVAlignDistributed;
                        }
                    }
                }
                */

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
