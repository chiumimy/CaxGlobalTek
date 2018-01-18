using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using Excel = Microsoft.Office.Interop.Excel;
using Microsoft.Office.Interop.Excel;
using System.Windows.Forms;
using Microsoft.Office.Core;
using System.IO;
using NHibernate;
using CaxGlobaltek;
using System.Drawing;

namespace OutputExcelForm.Excel
{
    public class Excel_FixtureInspection
    {
        private static bool status;
        public static ISession session = MyHibernateHelper.SessionFactory.OpenSession();
        private static string OutputPath = "";
        public struct RowColumn
        {
            //模檢治編號
            public int FixInsNoRow { get; set; }
            public int FixInsNoColumn { get; set; }
            //ERP編號
            public int ERPRow { get; set; }
            public int ERPColumn { get; set; }
            //品名
            public int FixInsDescRow { get; set; }
            public int FixInsDescColumn { get; set; }
            //泡泡位置
            public int BallonRow { get; set; }
            public int BallonColumn { get; set; }
            //Dimension
            public int FixInsDimensionRow { get; set; }
            public int FixInsDimensionColumn { get; set; }
        }
        public struct FixImgPosiSize
        {
            public float FixPosiLeft { get; set; }
            public float FixPosiTop { get; set; }
            public float FixImgWidth { get; set; }
            public float FixImgHeight { get; set; }
        }
        public static void GetExcelRowColumn(int i, out RowColumn sRowColumn)
        {
            sRowColumn = new RowColumn();

            sRowColumn.FixInsNoRow = 32;
            sRowColumn.FixInsNoColumn = 1;
            sRowColumn.ERPRow = 31;
            sRowColumn.ERPColumn = 21;
            sRowColumn.FixInsDescRow = 34;
            sRowColumn.FixInsDescColumn = 1;

            int currentNo = (i % 15);
            int RowNo = 16;
            RowNo = RowNo + currentNo;

            sRowColumn.BallonRow = RowNo;
            sRowColumn.BallonColumn = 1;

            sRowColumn.FixInsDimensionRow = RowNo;
            sRowColumn.FixInsDimensionColumn = 2;

        }
        public static void GetFixImgPosiAndSize(double left, double top, double width, double height, out FixImgPosiSize sFixImgPosiSize)
        {
            string ScreenWidth = SystemInformation.PrimaryMonitorSize.Width.ToString();
            string ScreenHeight = SystemInformation.PrimaryMonitorSize.Height.ToString();

            sFixImgPosiSize = new FixImgPosiSize();
            //sFixImgPosiSize.FixPosiLeft = 485;
            sFixImgPosiSize.FixPosiLeft = (float)(left * 1366 / Convert.ToDouble(ScreenWidth));
            sFixImgPosiSize.FixPosiTop = (float)top;
            //sFixImgPosiSize.FixPosiTop = (float)(423 * 768 / Convert.ToDouble(ScreenHeight));
            //sFixImgPosiSize.FixImgWidth = 225;
            //sFixImgPosiSize.FixImgHeight = 198;

            float widthfloat = (float)width;
            float heightfloat = (float)height;
            Excel_CommonFun.GetScreenResolution(ref widthfloat, ref heightfloat);
            sFixImgPosiSize.FixImgWidth = widthfloat;
            sFixImgPosiSize.FixImgHeight = heightfloat;

        }
        public static bool CreateFixInsExcel(string cusName, string partNo, string cusVer, string opVer, string op1, DB_FixInspection sDB_FixInspection, IList<Com_FixDimension> cCom_FixDimension)
        {
            //判斷Server的Template是否存在
            if (!File.Exists(sDB_FixInspection.excelTemplateFilePath))
                return false;

            ApplicationClass excelApp = new ApplicationClass();
            Workbook workBook = null;
            Worksheet workSheet = null;
            Range workRange = null;

            try
            {
                OutputPath = string.Format(@"{0}\{1}_{2}_{3}\{4}_{5}_{6}_{7}"
                                                       , Environment.GetFolderPath(Environment.SpecialFolder.Desktop)
                                                       , partNo
                                                       , cusVer
                                                       , opVer
                                                       , partNo
                                                       , cusVer
                                                       , opVer
                                                       , sDB_FixInspection.comFixInspection.fixPartName + "模檢治" + ".xls");
                //1.開啟Excel
                //2.將Excel設為不顯示
                //3.取得第一頁Sheet
                workBook = (Workbook)excelApp.Workbooks.Open(sDB_FixInspection.excelTemplateFilePath);
                excelApp.Visible = false;
                workSheet = (Worksheet)workBook.Sheets[1];

                //建立Sheet頁數符合所有的Dimension
                int Dicount = 0;
                foreach (Com_FixDimension i in cCom_FixDimension)
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
                status = Excel_CommonFun.AddNewSheet(Dicount, 15, excelApp, workSheet);
                if (!status)
                {
                    MessageBox.Show("建立Sheet頁失敗，請聯繫開發工程師");
                    workBook.SaveAs(sDB_FixInspection.excelTemplateFilePath, XlFileFormat.xlWorkbookNormal, Type.Missing, Type.Missing, Type.Missing, Type.Missing
                        , XlSaveAsAccessMode.xlNoChange, Type.Missing, Type.Missing, Type.Missing, Type.Missing, Type.Missing);
                    workBook.Close(Type.Missing, Type.Missing, Type.Missing);
                    excelApp.Quit();
                    return false;
                }

                RowColumn sRowColumn = new RowColumn();
                int currentSheet_Value;
                int count = -1;
                foreach (Com_FixDimension i in cCom_FixDimension)
                {
                    if (i.balloonCount == null)
                    {
                        count++;
                        GetExcelRowColumn(count, out sRowColumn);
                        currentSheet_Value = (count / 15);
                        if (currentSheet_Value == 0)
                        {
                            workSheet = (Worksheet)workBook.Sheets[1];
                        }
                        else
                        {
                            workSheet = (Worksheet)workBook.Sheets[currentSheet_Value + 1];
                        }
                        workRange = (Range)workSheet.Cells;

                        status = Excel_CommonFun.MappingFixInsDimenData(i, workSheet, sRowColumn.FixInsDimensionRow, sRowColumn.FixInsDimensionColumn);
                        if (!status)
                        {
                            MessageBox.Show("MappingDimenData時發生錯誤，請聯繫開發工程師");
                            workBook.Close(Type.Missing, Type.Missing, Type.Missing);
                            excelApp.Quit();
                            return false;
                        }

                        #region 泡泡、模檢治編號、ERP編號、品名
                        workRange[sRowColumn.BallonRow, sRowColumn.BallonColumn] = i.ballon.ToString();
                        workRange[sRowColumn.FixInsNoRow, sRowColumn.FixInsNoColumn] = sDB_FixInspection.comFixInspection.fixinsNo.ToString();
                        workRange[sRowColumn.FixInsDescRow, sRowColumn.FixInsDescColumn] = sDB_FixInspection.comFixInspection.fixinsDescription.ToString();
                        workRange[sRowColumn.ERPRow, sRowColumn.ERPColumn] = sDB_FixInspection.comFixInspection.fixinsERP.ToString();
                        #endregion
                    }
                    else
                    {
                        for (int j = 0; j < Convert.ToInt32(i.balloonCount); j++)
                        {
                            count++;
                            GetExcelRowColumn(count, out sRowColumn);
                            currentSheet_Value = (count / 15);
                            if (currentSheet_Value == 0)
                            {
                                workSheet = (Worksheet)workBook.Sheets[1];
                            }
                            else
                            {
                                workSheet = (Worksheet)workBook.Sheets[currentSheet_Value + 1];
                            }
                            workRange = (Range)workSheet.Cells;

                            status = Excel_CommonFun.MappingFixInsDimenData(i, workSheet, sRowColumn.FixInsDimensionRow, sRowColumn.FixInsDimensionColumn);
                            if (!status)
                            {
                                MessageBox.Show("MappingDimenData時發生錯誤，請聯繫開發工程師");
                                workBook.Close(Type.Missing, Type.Missing, Type.Missing);
                                excelApp.Quit();
                                return false;
                            }

                            #region 泡泡、模檢治編號、ERP編號、品名
                            if (i.balloonCount != null && Convert.ToInt32(i.balloonCount) > 1)
                            {
                                workRange[sRowColumn.BallonRow, sRowColumn.BallonColumn] = i.ballon.ToString() + Convert.ToChar(65 + j).ToString().ToLower();
                            }
                            else
                            {
                                workRange[sRowColumn.BallonRow, sRowColumn.BallonColumn] = i.ballon.ToString();
                            }
                            workRange[sRowColumn.FixInsNoRow, sRowColumn.FixInsNoColumn] = sDB_FixInspection.comFixInspection.fixinsNo.ToString();
                            workRange[sRowColumn.FixInsDescRow, sRowColumn.FixInsDescColumn] = sDB_FixInspection.comFixInspection.fixinsDescription.ToString();
                            workRange[sRowColumn.ERPRow, sRowColumn.ERPColumn] = sDB_FixInspection.comFixInspection.fixinsERP.ToString();
                            #endregion
                        }
                    }
                }

                //計算欄位的初始top
                double topDouble = 0;
                for (int i = 1; i < 5; i++)
                {
                    workRange = (Range)workSheet.Cells[i, 1];
                    topDouble = topDouble + Convert.ToDouble(workRange.Height);
                    //CaxLog.ShowListingWindow(oRng.Height.ToString());
                }
                //CaxLog.ShowListingWindow(de.ToString());

                //計算欄位的初始left
                double leftDouble = 0;
                for (int i = 1; i < 2; i++)
                {
                    workRange = (Range)workSheet.Cells[1, i];
                    leftDouble = leftDouble + Convert.ToDouble(workRange.Width.ToString());
                }

                //計算圖片的width
                double widthDouble = 0;
                for (int i = 2; i < 34; i++)
                {
                    workRange = (Range)workSheet.Cells[1, i];
                    widthDouble = widthDouble + Convert.ToDouble(workRange.Width.ToString());
                }

                //計算圖片的height
                double heightDouble = 0;
                for (int i = 5; i < 14; i++)
                {
                    workRange = (Range)workSheet.Cells[i, 1];
                    heightDouble = heightDouble + Convert.ToDouble(workRange.Height);
                }

                //加入圖片
                FixImgPosiSize sFixImgPosiSize = new FixImgPosiSize();
                GetFixImgPosiAndSize(leftDouble, topDouble, widthDouble, heightDouble, out sFixImgPosiSize);
                for (int i = 0; i < workBook.Sheets.Count; i++)
                {
                    workSheet = (Worksheet)workBook.Sheets[i + 1];

                    string[] splitFixImgPath = sDB_FixInspection.comFixInspection.fixPicPath.Split(',');
                    foreach (string j in splitFixImgPath)
                    {
                        workSheet.Shapes.AddPicture(j, Microsoft.Office.Core.MsoTriState.msoFalse,
                        Microsoft.Office.Core.MsoTriState.msoTrue, sFixImgPosiSize.FixPosiLeft,
                        sFixImgPosiSize.FixPosiTop, sFixImgPosiSize.FixImgWidth, sFixImgPosiSize.FixImgHeight);
                    }
                    //if (File.Exists(sDB_TEMain.comTEMain.fixtureImgPath))
                    //{
                    //    workSheet.Shapes.AddPicture(sDB_TEMain.comTEMain.fixtureImgPath, Microsoft.Office.Core.MsoTriState.msoFalse,
                    //    Microsoft.Office.Core.MsoTriState.msoTrue, (float)(abc + 3),
                    //    (float)(de + 5), sFixImgPosiSize.FixImgWidth, sFixImgPosiSize.FixImgHeight);
                    //}
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
        public static void Dispose(ApplicationClass excelApp, Workbook workBook, Worksheet workSheet, Range workRange)
        {
            System.Runtime.InteropServices.Marshal.ReleaseComObject(workRange);
            System.Runtime.InteropServices.Marshal.ReleaseComObject(workSheet);
            System.Runtime.InteropServices.Marshal.ReleaseComObject(workBook);
            System.Runtime.InteropServices.Marshal.ReleaseComObject(excelApp);
        }
    }
}
