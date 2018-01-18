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
    public class Excel_PFD
    {
        private static bool status;
        public static ISession session = MyHibernateHelper.SessionFactory.OpenSession();
        private static string OutputPath = "";
        public struct Point8
        {
            public LeftUp sLeftUp;
            public Left sLeft;
            public LeftLower sLeftLower;

            public MiddleUp sMiddleUp;
            public MiddleLower sMiddleLower;

            public RightUp sRightUp;
            public Right sRight;
            public RightLower sRightLower;
        }
        public struct LeftUp
        {
            public double X { get; set; }
            public double Y { get; set; }
        }
        public struct Left
        {
            public double X { get; set; }
            public double Y { get; set; }
        }
        public struct LeftLower
        {
            public double X { get; set; }
            public double Y { get; set; }
        }
        public struct MiddleUp
        {
            public double X { get; set; }
            public double Y { get; set; }
        }
        public struct MiddleLower
        {
            public double X { get; set; }
            public double Y { get; set; }
        }
        public struct RightUp
        {
            public double X { get; set; }
            public double Y { get; set; }
        }
        public struct Right
        {
            public double X { get; set; }
            public double Y { get; set; }
        }
        public struct RightLower
        {
            public double X { get; set; }
            public double Y { get; set; }
        }
        


        public static bool CreatePFDExcel(DB_PEMain sDB_PEMain, List<DB_PartOperation> sDB_PartOperation)
        {
            //判斷Server的Template是否存在
            if (!File.Exists(sDB_PEMain.excelTemplateFilePath))
                return false;

            ApplicationClass excelApp = new ApplicationClass();
            Workbook workBook = null;
            Worksheet workSheet = null;
            Range workRange = null;
            try
            {
                OutputPath = string.Format(@"{0}\{1}_{2}_{3}\{4}_{5}_{6}_{7}"
                                                    , Environment.GetFolderPath(Environment.SpecialFolder.Desktop)
                                                    , sDB_PEMain.PartNo
                                                    , sDB_PEMain.CusVer
                                                    , sDB_PEMain.OpVer
                                                    , sDB_PEMain.PartNo
                                                    , sDB_PEMain.CusVer
                                                    , sDB_PEMain.OpVer
                                                    , "PFD" + ".xls");

                

                //1.開啟Excel
                //2.將Excel設為不顯示
                //3.取得第一頁Sheet
                workBook = (Workbook)excelApp.Workbooks.Open(sDB_PEMain.excelTemplateFilePath);
                excelApp.Visible = false;
                workSheet = (Worksheet)workBook.Sheets[1];

                //初始高度 
                double topDistance = 0, initialTopDistance = 0;
                for (int i = 1; i < 9; i++)
                {
                    workRange = (Range)workSheet.Cells[i, 1];
                    topDistance = topDistance + Convert.ToDouble(workRange.Height);
                    initialTopDistance = topDistance;
                }

                //初始寬度
                float initialLeftDistance = 20;

                int imgCount = 0;
                foreach (DB_PartOperation i in sDB_PartOperation)
                {
                    imgCount++;
                    string imgPath = "";
                    double imgWidthDouble, imgHeightDouble;
                    float imgWidth, imgHeight;

                    //當圖片超過7張，先加入C2
                    if (imgCount == 7)
                    {
                        imgPath = string.Format(@"{0}\{1}", Path.GetDirectoryName(i.ImgPath), "C2_155_46.Jpeg");

                        //計算Excel中的寬、高
                        imgWidthDouble = Convert.ToInt32(155) * 0.75;
                        imgHeightDouble = Convert.ToInt32(46) * 0.75;
                        imgWidth = (float)imgWidthDouble;
                        imgHeight = (float)imgHeightDouble;

                        initialLeftDistance = initialLeftDistance + imgWidth + 15;
                        topDistance = initialTopDistance;

                        //插入圖片
                        workSheet.Shapes.AddPicture(imgPath, Microsoft.Office.Core.MsoTriState.msoFalse,
                                    Microsoft.Office.Core.MsoTriState.msoTrue, initialLeftDistance,
                                    (float)topDistance, imgWidth, imgHeight);
                        File.Delete(imgPath);
                        topDistance = topDistance + imgHeight;
                    }

                    //拆解圖片的寬、高
                    string imgWidthStr = Path.GetFileNameWithoutExtension(i.ImgPath).Split('_')[1];
                    string imgHeightStr = Path.GetFileNameWithoutExtension(i.ImgPath).Split('_')[2];

                    //計算Excel中的寬、高
                    imgWidthDouble = Convert.ToInt32(imgWidthStr) * 0.75;
                    imgHeightDouble = Convert.ToInt32(imgHeightStr) * 0.75;
                    imgWidth = (float)imgWidthDouble;
                    imgHeight = (float)imgHeightDouble;

                    //取得圖片路徑
                    imgPath = i.ImgPath;

                    //插入圖片
                    workSheet.Shapes.AddPicture(imgPath, Microsoft.Office.Core.MsoTriState.msoFalse,
                                Microsoft.Office.Core.MsoTriState.msoTrue, initialLeftDistance,
                                (float)topDistance, imgWidth, imgHeight);
                    File.Delete(imgPath);
                    topDistance = topDistance + imgHeight;

                    if (i.Op1 == "900")
                    {
                        //取得Shipping圖片路徑
                        imgPath = string.Format(@"{0}\{1}", Path.GetDirectoryName(i.ImgPath), "Shipping_155_31.Jpeg");

                        //計算Excel中的寬、高
                        imgWidthDouble = Convert.ToInt32(155) * 0.75;
                        imgHeightDouble = Convert.ToInt32(31) * 0.75;
                        imgWidth = (float)imgWidthDouble;
                        imgHeight = (float)imgHeightDouble;

                        //插入圖片
                        workSheet.Shapes.AddPicture(imgPath, Microsoft.Office.Core.MsoTriState.msoFalse,
                                    Microsoft.Office.Core.MsoTriState.msoTrue, initialLeftDistance,
                                    (float)topDistance, imgWidth, imgHeight);
                        File.Delete(imgPath);
                    }

                    if (imgCount == 6 & i.Op1 != "900")
                    {
                        //取得C1圖片路徑
                        imgPath = string.Format(@"{0}\{1}", Path.GetDirectoryName(i.ImgPath), "C1_155_21.Jpeg");

                        //計算Excel中的寬、高
                        imgWidthDouble = Convert.ToInt32(155) * 0.75;
                        imgHeightDouble = Convert.ToInt32(21) * 0.75;
                        imgWidth = (float)imgWidthDouble;
                        imgHeight = (float)imgHeightDouble;

                        //插入圖片
                        workSheet.Shapes.AddPicture(imgPath, Microsoft.Office.Core.MsoTriState.msoFalse,
                                    Microsoft.Office.Core.MsoTriState.msoTrue, initialLeftDistance,
                                    (float)topDistance, imgWidth, imgHeight);
                        File.Delete(imgPath);
                    }

                    
                }
                
                //Common.png
                //string commonImgPath = string.Format(@"{0}\{1}\{2}\{3}\{4}\{5}.png"
                //                                    , "\\\\192.168.35.1"
                //                                    , "cax"
                //                                    , "Globaltek"
                //                                    , "PE_Config"
                //                                    , "Config"
                //                                    , "common");
                string commonImgPath = string.Format(@"{0}\{1}\{2}\{3}.png"
                                                    , CaxEnv.GetGlobaltekEnvDir()
                                                    , "PE_Config"
                                                    , "Config"
                                                    , "common");

                //插入圖片
                workSheet.Shapes.AddPicture(commonImgPath, Microsoft.Office.Core.MsoTriState.msoFalse,
                            Microsoft.Office.Core.MsoTriState.msoTrue, 320,
                            300, 220, 350);
                //File.Delete(commonImgPath);

                //插入料號、客戶版次、品名
                workRange = (Range)workSheet.Cells;
                workRange[5, 2] = sDB_PEMain.PartNo;
                workRange[5, 4] = sDB_PEMain.CusVer;
                workRange[6, 2] = session.QueryOver<Com_PEMain>()
                                    .Where(x => x.partName == sDB_PEMain.PartNo)
                                    .Where(x => x.customerVer == sDB_PEMain.CusVer)
                                    .Where(x => x.opVer == sDB_PEMain.OpVer).SingleOrDefault<Com_PEMain>().partDes;


                //workSheet.Shapes.AddPicture(imgPath, Microsoft.Office.Core.MsoTriState.msoFalse,
                //                Microsoft.Office.Core.MsoTriState.msoTrue, 20 + imgWidth + 10,
                //                (float)initialTopDistance, imgWidth, imgHeight);
                if (File.Exists(OutputPath))
                {
                    File.Delete(OutputPath);
                }
                workBook.SaveAs(OutputPath, XlFileFormat.xlWorkbookNormal, Type.Missing, Type.Missing, Type.Missing, Type.Missing,
                    XlSaveAsAccessMode.xlNoChange, Type.Missing, Type.Missing, Type.Missing, Type.Missing, Type.Missing);
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
        public static bool NewCreatePFDExcel(DB_PEMain sDB_PEMain, List<DB_PartOperation> sDB_PartOperation)
        {
            //判斷Server的Template是否存在
            if (!File.Exists(sDB_PEMain.excelTemplateFilePath))
                return false;

            ApplicationClass excelApp = new ApplicationClass();
            Workbook workBook = null;
            Worksheet workSheet = null;
            Range workRange = null;
            try
            {
                OutputPath = string.Format(@"{0}\{1}_{2}_{3}\{4}_{5}_{6}_{7}"
                                                    , Environment.GetFolderPath(Environment.SpecialFolder.Desktop)
                                                    , sDB_PEMain.PartNo
                                                    , sDB_PEMain.CusVer
                                                    , sDB_PEMain.OpVer
                                                    , sDB_PEMain.PartNo
                                                    , sDB_PEMain.CusVer
                                                    , sDB_PEMain.OpVer
                                                    , "PFD" + ".xls");



                //1.開啟Excel
                //2.將Excel設為不顯示
                //3.取得第一頁Sheet
                workBook = excelApp.Workbooks.Open(sDB_PEMain.excelTemplateFilePath);
                excelApp.Visible = false;
                workSheet = (Worksheet)workBook.Sheets[1];

                //初始高度 
                double topDistance = 0, initialTopDistance = 0;
                for (int i = 1; i < 9; i++)
                {
                    workRange = (Range)workSheet.Cells[i, 1];
                    topDistance = topDistance + Convert.ToDouble(workRange.Height);
                    initialTopDistance = topDistance;
                }

                //初始值
                double initialLeftDistance = 70;
                double frame_Height = 33;
                double frame_Width = 76;
                double arrow_Length = 19.6;

                Point8 rtPoint8 = new Point8();
                Point8 downPoint8 = new Point8();
                Point8 diamondPoint8 = new Point8();
                Point8 circlePoint8 = new Point8();
                Point8 leftPoint8 = new Point8();
                Point8 rightPoint8 = new Point8();
                Point8 linePoint8 = new Point8(); 

                int opCount = 0;
                foreach (DB_PartOperation i in sDB_PartOperation)
                {
                    opCount++;
                    if (opCount == 7)
                    {
                        //先加入C1
                        CreateCircleWithText(workSheet, "C", (downPoint8.sMiddleLower.X - 8.2), topDistance, 16.5, 16.5, out circlePoint8);

                        initialLeftDistance = 210;
                        topDistance = initialTopDistance;

                        //再加入C2
                        CreateCircleWithText(workSheet, "C", 239.18, topDistance, 16.5, 16.5, out circlePoint8);
                        CreateDownArrow(workSheet, circlePoint8.sMiddleLower.X, circlePoint8.sMiddleLower.Y, circlePoint8.sMiddleLower.X, (circlePoint8.sMiddleLower.Y + arrow_Length), out downPoint8);
                        topDistance = downPoint8.sMiddleLower.Y;
                    }

                    //接IQC
                    if (i.Form == "IQC")
                    {
                        if (i.Op1 == "001")
                        {
                            CreateReverseTriangle(workSheet, initialLeftDistance, topDistance, frame_Width, frame_Height, out rtPoint8);
                            WriteTextBox(workSheet, "Top", i.Op1, i.Op2, rtPoint8.sLeftUp.X, rtPoint8.sLeftUp.Y, frame_Width, frame_Height);
                        }
                        else
                        {
                            CreateRectangle(workSheet, initialLeftDistance, topDistance, frame_Width, frame_Height, out rtPoint8);
                            WriteTextBox(workSheet, "Middle", i.Op1, i.Op2, rtPoint8.sLeftUp.X, rtPoint8.sLeftUp.Y, (rtPoint8.sRightUp.X - rtPoint8.sLeftUp.X), (rtPoint8.sRightLower.Y - rtPoint8.sRightUp.Y));
                        }
                        CreateDownArrow(workSheet, rtPoint8.sMiddleLower.X, rtPoint8.sMiddleLower.Y, rtPoint8.sMiddleLower.X, (rtPoint8.sMiddleLower.Y + arrow_Length), out downPoint8);
                        CreateDiamond(workSheet, initialLeftDistance, downPoint8.sMiddleLower.Y, frame_Width, frame_Height, out diamondPoint8);
                        WriteTextBox(workSheet, "Middle", "IQC", "", diamondPoint8.sLeftUp.X, diamondPoint8.sLeftUp.Y, frame_Width, frame_Height);
                        CreateDownArrowWithOK(workSheet, diamondPoint8.sMiddleLower.X, diamondPoint8.sMiddleLower.Y, diamondPoint8.sMiddleLower.X, (diamondPoint8.sMiddleLower.Y + arrow_Length), out downPoint8);
                        CreateLeftArrowWithNG(workSheet, diamondPoint8.sLeft.X, diamondPoint8.sLeft.Y, (diamondPoint8.sLeft.X - arrow_Length), diamondPoint8.sLeft.Y, out leftPoint8);
                        CreateCircleWithText(workSheet, "A", (leftPoint8.sLeft.X - 16.5), (leftPoint8.sLeft.Y - 8.2), 16.5, 16.5, out circlePoint8);
                        CreateStraightLine(workSheet, circlePoint8.sMiddleLower.X, circlePoint8.sMiddleLower.Y, circlePoint8.sMiddleLower.X, (circlePoint8.sMiddleLower.Y + 15), out linePoint8);
                        CreateRightArrowWithOK(workSheet, linePoint8.sMiddleLower.X, linePoint8.sMiddleLower.Y, (linePoint8.sMiddleLower.X + 65), linePoint8.sMiddleLower.Y, out rightPoint8);
                        topDistance = downPoint8.sMiddleLower.Y;
                        continue;
                    }
                    //接IPQC
                    else if (i.Form == "IPQC")
                    {
                        CreateRectangle(workSheet, initialLeftDistance, topDistance, frame_Width, frame_Height, out rtPoint8);
                        WriteTextBox(workSheet, "Middle", i.Op1, i.Op2, rtPoint8.sLeftUp.X, rtPoint8.sLeftUp.Y, (rtPoint8.sRightUp.X - rtPoint8.sLeftUp.X), (rtPoint8.sRightLower.Y - rtPoint8.sRightUp.Y));
                        CreateDownArrow(workSheet, rtPoint8.sMiddleLower.X, rtPoint8.sMiddleLower.Y, rtPoint8.sMiddleLower.X, (rtPoint8.sMiddleLower.Y + arrow_Length), out downPoint8);
                        CreateDiamond(workSheet, initialLeftDistance, downPoint8.sMiddleLower.Y, frame_Width, frame_Height, out diamondPoint8);
                        WriteTextBox(workSheet, "Middle", "IPQC", "", diamondPoint8.sLeftUp.X, diamondPoint8.sLeftUp.Y, (diamondPoint8.sRightUp.X - diamondPoint8.sLeftUp.X), (diamondPoint8.sRightLower.Y - diamondPoint8.sRightUp.Y));
                        CreateDownArrowWithOK(workSheet, diamondPoint8.sMiddleLower.X, diamondPoint8.sMiddleLower.Y, diamondPoint8.sMiddleLower.X, (diamondPoint8.sMiddleLower.Y + arrow_Length), out downPoint8);
                        CreateLeftArrowWithNG(workSheet, diamondPoint8.sLeft.X, diamondPoint8.sLeft.Y, (diamondPoint8.sLeft.X - arrow_Length), diamondPoint8.sLeft.Y, out leftPoint8);
                        CreateCircleWithText(workSheet, "A", (leftPoint8.sLeft.X - 16.5), (leftPoint8.sLeft.Y - 8.2), 16.5, 16.5, out circlePoint8);
                        CreateStraightLine(workSheet, circlePoint8.sMiddleLower.X, circlePoint8.sMiddleLower.Y, circlePoint8.sMiddleLower.X, (circlePoint8.sMiddleLower.Y + 15), out linePoint8);
                        CreateRightArrowWithOK(workSheet, linePoint8.sMiddleLower.X, linePoint8.sMiddleLower.Y, (linePoint8.sMiddleLower.X + 65), linePoint8.sMiddleLower.Y, out rightPoint8);
                        topDistance = downPoint8.sMiddleLower.Y;
                        continue;
                    }
                    //都不接
                    else if (i.Form == null && i.Op2 != "Appearance check & Packaging")
                    {
                        CreateRectangle(workSheet, initialLeftDistance, topDistance, frame_Width, frame_Height, out rtPoint8);
                        WriteTextBox(workSheet, "Middle", i.Op1, i.Op2, rtPoint8.sLeftUp.X, rtPoint8.sLeftUp.Y, (rtPoint8.sRightUp.X - rtPoint8.sLeftUp.X), (rtPoint8.sRightLower.Y - rtPoint8.sRightUp.Y));
                        CreateDownArrow(workSheet, rtPoint8.sMiddleLower.X, rtPoint8.sMiddleLower.Y, rtPoint8.sMiddleLower.X, (rtPoint8.sMiddleLower.Y + arrow_Length), out downPoint8);
                        topDistance = downPoint8.sMiddleLower.Y;
                        continue;
                    }
                    //接出貨
                    else if (i.Form == null && i.Op2 == "Appearance check & Packaging")
                    {
                        CreateRectangle(workSheet, initialLeftDistance, topDistance, frame_Width, frame_Height + 6, out rtPoint8);
                        WriteTextBox(workSheet, "Middle", i.Op1, i.Op2, rtPoint8.sLeftUp.X, rtPoint8.sLeftUp.Y, (rtPoint8.sRightUp.X - rtPoint8.sLeftUp.X), (rtPoint8.sRightLower.Y - rtPoint8.sRightUp.Y));
                        CreateDownArrowWithOK(workSheet, rtPoint8.sMiddleLower.X, rtPoint8.sMiddleLower.Y, rtPoint8.sMiddleLower.X, (rtPoint8.sMiddleLower.Y + arrow_Length), out downPoint8);
                        CreateLeftArrowWithNG(workSheet, rtPoint8.sLeft.X, rtPoint8.sLeft.Y, (rtPoint8.sLeft.X - arrow_Length), rtPoint8.sLeft.Y, out leftPoint8);
                        CreateCircleWithText(workSheet, "A", (leftPoint8.sLeft.X - 16.5), (leftPoint8.sLeft.Y - 8.2), 16.5, 16.5, out circlePoint8);
                        CreateStraightLine(workSheet, circlePoint8.sMiddleLower.X, circlePoint8.sMiddleLower.Y, circlePoint8.sMiddleLower.X, (circlePoint8.sMiddleLower.Y + 18), out linePoint8);
                        CreateRightArrowWithOK(workSheet, linePoint8.sMiddleLower.X, linePoint8.sMiddleLower.Y, (linePoint8.sMiddleLower.X + 65), linePoint8.sMiddleLower.Y, out rightPoint8);
                        topDistance = downPoint8.sMiddleLower.Y;
                        if (i.Op1 == "900")
                        {
                            CreateShipping(workSheet, initialLeftDistance, topDistance, frame_Width, frame_Height - 5, out rtPoint8);
                            WriteTextBox(workSheet, "Middle", "Shipping", "", rtPoint8.sLeftUp.X, rtPoint8.sLeftUp.Y, (rtPoint8.sRightUp.X - rtPoint8.sLeftUp.X), (rtPoint8.sRightLower.Y - rtPoint8.sRightUp.Y));
                        }
                        continue;
                    }









                    /*
                    if (i.Op2 == "Blank" || i.Op2 == "Forging" || i.Op2 == "Anodize" || i.Op2 == "Casting" || i.Op2 == "Cold drawing")
                    {
                        CreateReverseTriangle(workSheet, initialLeftDistance, topDistance, frame_Width, frame_Height, out rtPoint8);
                        WriteTextBox(workSheet, "Top", i.Op1, i.Op2, rtPoint8.sLeftUp.X, rtPoint8.sLeftUp.Y, frame_Width, frame_Height);
                        CreateDownArrow(workSheet, rtPoint8.sMiddleLower.X, rtPoint8.sMiddleLower.Y, rtPoint8.sMiddleLower.X, (rtPoint8.sMiddleLower.Y + arrow_Length), out downPoint8);
                        CreateDiamond(workSheet, initialLeftDistance, downPoint8.sMiddleLower.Y, frame_Width, frame_Height, out diamondPoint8);
                        WriteTextBox(workSheet, "Middle", "IQC", "", diamondPoint8.sLeftUp.X, diamondPoint8.sLeftUp.Y, frame_Width, frame_Height);
                        CreateDownArrowWithOK(workSheet, diamondPoint8.sMiddleLower.X, diamondPoint8.sMiddleLower.Y, diamondPoint8.sMiddleLower.X, (diamondPoint8.sMiddleLower.Y + arrow_Length), out downPoint8);
                        CreateLeftArrowWithNG(workSheet, diamondPoint8.sLeft.X, diamondPoint8.sLeft.Y, (diamondPoint8.sLeft.X - arrow_Length), diamondPoint8.sLeft.Y, out leftPoint8);
                        CreateCircleWithText(workSheet, "A", (leftPoint8.sLeft.X - 16.5), (leftPoint8.sLeft.Y - 8.2), 16.5, 16.5, out circlePoint8);
                        CreateStraightLine(workSheet, circlePoint8.sMiddleLower.X, circlePoint8.sMiddleLower.Y, circlePoint8.sMiddleLower.X, (circlePoint8.sMiddleLower.Y + 15), out linePoint8);
                        CreateRightArrowWithOK(workSheet, linePoint8.sMiddleLower.X, linePoint8.sMiddleLower.Y, (linePoint8.sMiddleLower.X + 65), linePoint8.sMiddleLower.Y, out rightPoint8);
                        topDistance = downPoint8.sMiddleLower.Y;
                        continue;
                    }
                    else if (i.Op2 == "Appearance check & Packaging")
                    {
                        CreateRectangle(workSheet, initialLeftDistance, topDistance, frame_Width, frame_Height + 6, out rtPoint8);
                        WriteTextBox(workSheet, "Middle", i.Op1, i.Op2, rtPoint8.sLeftUp.X, rtPoint8.sLeftUp.Y, (rtPoint8.sRightUp.X - rtPoint8.sLeftUp.X), (rtPoint8.sRightLower.Y - rtPoint8.sRightUp.Y));
                        CreateDownArrowWithOK(workSheet, rtPoint8.sMiddleLower.X, rtPoint8.sMiddleLower.Y, rtPoint8.sMiddleLower.X, (rtPoint8.sMiddleLower.Y + arrow_Length), out downPoint8);
                        CreateLeftArrowWithNG(workSheet, rtPoint8.sLeft.X, rtPoint8.sLeft.Y, (rtPoint8.sLeft.X - arrow_Length), rtPoint8.sLeft.Y, out leftPoint8);
                        CreateCircleWithText(workSheet, "A", (leftPoint8.sLeft.X - 16.5), (leftPoint8.sLeft.Y - 8.2), 16.5, 16.5, out circlePoint8);
                        CreateStraightLine(workSheet, circlePoint8.sMiddleLower.X, circlePoint8.sMiddleLower.Y, circlePoint8.sMiddleLower.X, (circlePoint8.sMiddleLower.Y + 18), out linePoint8);
                        CreateRightArrowWithOK(workSheet, linePoint8.sMiddleLower.X, linePoint8.sMiddleLower.Y, (linePoint8.sMiddleLower.X + 65), linePoint8.sMiddleLower.Y, out rightPoint8);
                        topDistance = downPoint8.sMiddleLower.Y;
                        if (i.Op1 == "900")
                        {
                            CreateShipping(workSheet, initialLeftDistance, topDistance, frame_Width, frame_Height - 5, out rtPoint8);
                            WriteTextBox(workSheet, "Middle", "Shipping", "", rtPoint8.sLeftUp.X, rtPoint8.sLeftUp.Y, (rtPoint8.sRightUp.X - rtPoint8.sLeftUp.X), (rtPoint8.sRightLower.Y - rtPoint8.sRightUp.Y));
                        }
                        continue;
                    }
                    else if (i.Op2 == "Flaw Detect")
                    {
                        CreateRectangle(workSheet, initialLeftDistance, topDistance, frame_Width, frame_Height, out rtPoint8);
                        WriteTextBox(workSheet, "Middle", i.Op1, i.Op2, rtPoint8.sLeftUp.X, rtPoint8.sLeftUp.Y, (rtPoint8.sRightUp.X - rtPoint8.sLeftUp.X), (rtPoint8.sRightLower.Y - rtPoint8.sRightUp.Y));
                        CreateDownArrow(workSheet, rtPoint8.sMiddleLower.X, rtPoint8.sMiddleLower.Y, rtPoint8.sMiddleLower.X, (rtPoint8.sMiddleLower.Y + arrow_Length), out downPoint8);
                        topDistance = downPoint8.sMiddleLower.Y;
                        continue;
                    }
                    else
                    {
                        CreateRectangle(workSheet, initialLeftDistance, topDistance, frame_Width, frame_Height, out rtPoint8);
                        WriteTextBox(workSheet, "Middle", i.Op1, i.Op2, rtPoint8.sLeftUp.X, rtPoint8.sLeftUp.Y, (rtPoint8.sRightUp.X - rtPoint8.sLeftUp.X), (rtPoint8.sRightLower.Y - rtPoint8.sRightUp.Y));
                        CreateDownArrow(workSheet, rtPoint8.sMiddleLower.X, rtPoint8.sMiddleLower.Y, rtPoint8.sMiddleLower.X, (rtPoint8.sMiddleLower.Y + arrow_Length), out downPoint8);
                        CreateDiamond(workSheet, initialLeftDistance, downPoint8.sMiddleLower.Y, frame_Width, frame_Height, out diamondPoint8);
                        WriteTextBox(workSheet, "Middle", "IPQC", "", diamondPoint8.sLeftUp.X, diamondPoint8.sLeftUp.Y, (diamondPoint8.sRightUp.X - diamondPoint8.sLeftUp.X), (diamondPoint8.sRightLower.Y - diamondPoint8.sRightUp.Y));
                        CreateDownArrowWithOK(workSheet, diamondPoint8.sMiddleLower.X, diamondPoint8.sMiddleLower.Y, diamondPoint8.sMiddleLower.X, (diamondPoint8.sMiddleLower.Y + arrow_Length), out downPoint8);
                        CreateLeftArrowWithNG(workSheet, diamondPoint8.sLeft.X, diamondPoint8.sLeft.Y, (diamondPoint8.sLeft.X - arrow_Length), diamondPoint8.sLeft.Y, out leftPoint8);
                        CreateCircleWithText(workSheet, "A", (leftPoint8.sLeft.X - 16.5), (leftPoint8.sLeft.Y - 8.2), 16.5, 16.5, out circlePoint8);
                        CreateStraightLine(workSheet, circlePoint8.sMiddleLower.X, circlePoint8.sMiddleLower.Y, circlePoint8.sMiddleLower.X, (circlePoint8.sMiddleLower.Y + 15), out linePoint8);
                        CreateRightArrowWithOK(workSheet, linePoint8.sMiddleLower.X, linePoint8.sMiddleLower.Y, (linePoint8.sMiddleLower.X + 65), linePoint8.sMiddleLower.Y, out rightPoint8);
                        topDistance = downPoint8.sMiddleLower.Y;
                        continue;
                    }
                    */
                }
                //(downPoint8.sMiddleLower.X - frame_Width / 2)

                //string commonImgPath = string.Format(@"{0}\{1}\{2}\{3}.png"
                //                                    , OutputForm.EnvVariables.env
                //                                    , "PE_Config"
                //                                    , "Config"
                //                                    , "common");
                string commonImgPath = string.Format(@"{0}\{1}\{2}\{3}.png"
                                                    , CaxEnv.GetGlobaltekEnvDir()
                                                    , "PE_Config"
                                                    , "Config"
                                                    , "common");

                //插入圖片
                workSheet.Shapes.AddPicture(commonImgPath, Microsoft.Office.Core.MsoTriState.msoTrue,
                            Microsoft.Office.Core.MsoTriState.msoTrue, 320,
                            300, 220, 350);

                //插入料號、客戶版次、品名
                workRange = (Range)workSheet.Cells;
                workRange[5, 2] = sDB_PEMain.PartNo;
                workRange[5, 4] = sDB_PEMain.CusVer;
                workRange[6, 2] = session.QueryOver<Com_PEMain>()
                                    .Where(x => x.partName == sDB_PEMain.PartNo)
                                    .Where(x => x.customerVer == sDB_PEMain.CusVer)
                                    .Where(x => x.opVer == sDB_PEMain.OpVer).SingleOrDefault<Com_PEMain>().partDes;


                if (File.Exists(OutputPath))
                {
                    File.Delete(OutputPath);
                }
                workBook.SaveAs(OutputPath, XlFileFormat.xlWorkbookNormal, Type.Missing, Type.Missing, Type.Missing, Type.Missing,
                    XlSaveAsAccessMode.xlNoChange, Type.Missing, Type.Missing, Type.Missing, Type.Missing, Type.Missing);
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

        public static bool CreateReverseTriangle(Worksheet workSheet, double left, double top, double width, double height, out Point8 sPoint8)
        {
            Microsoft.Office.Interop.Excel.Shape b = null; sPoint8 = new Point8();
            try
            {
                b = workSheet.Shapes.AddShape(MsoAutoShapeType.msoShapeFlowchartMerge, (float)left, (float)top, (float)width, (float)height);
                b.Fill.Visible = MsoTriState.msoFalse;
                int oleColor = ColorTranslator.ToOle(Color.FromArgb(0, 0, 0));
                b.Line.ForeColor.RGB = oleColor;
                b.Line.Weight = (float)0.75;

                GetPoint8(left, top, width, height, out sPoint8);
            }
            catch (System.Exception ex)
            {
                return false;
            }
            return true;
        }
        public static bool CreateDiamond(Worksheet workSheet, double left, double top, double width, double height, out Point8 sPoint8)
        {
            Microsoft.Office.Interop.Excel.Shape b = null; sPoint8 = new Point8();
            try
            {
                b = workSheet.Shapes.AddShape(MsoAutoShapeType.msoShapeFlowchartDecision, (float)left, (float)top, (float)width, (float)height);
                b.Fill.Visible = MsoTriState.msoFalse;
                int oleColor = ColorTranslator.ToOle(Color.FromArgb(0, 0, 0));
                b.Line.ForeColor.RGB = oleColor;
                b.Line.Weight = (float)0.75;

                GetPoint8(left, top, width, height, out sPoint8);
            }
            catch (System.Exception ex)
            {
                return false;
            }
            return true;
        }
        public static bool CreateRectangle(Worksheet workSheet, double left, double top, double width, double height, out Point8 sPoint8)
        {
            Microsoft.Office.Interop.Excel.Shape b = null; sPoint8 = new Point8();
            try
            {
                b = workSheet.Shapes.AddShape(MsoAutoShapeType.msoShapeFlowchartProcess, (float)left, (float)top, (float)width, (float)height);
                b.Fill.Visible = MsoTriState.msoFalse;
                int oleColor = ColorTranslator.ToOle(Color.FromArgb(0, 0, 0));
                b.Line.ForeColor.RGB = oleColor;
                b.Line.Weight = (float)0.75;
                GetPoint8(left, top, width, height, out sPoint8);
            }
            catch (System.Exception ex)
            {
                return false;
            }
            return true;
        }
        public static bool CreateShipping(Worksheet workSheet, double left, double top, double width, double height, out Point8 sPoint8)
        {
            Microsoft.Office.Interop.Excel.Shape b = null; sPoint8 = new Point8();
            try
            {
                b = workSheet.Shapes.AddShape(MsoAutoShapeType.msoShapeFlowchartData, (float)left, (float)top, (float)width, (float)height);
                b.Fill.Visible = MsoTriState.msoFalse;
                int oleColor = ColorTranslator.ToOle(Color.FromArgb(0, 0, 0));
                b.Line.ForeColor.RGB = oleColor;
                b.Line.Weight = (float)0.75;
                GetPoint8(left, top, width, height, out sPoint8);
            }
            catch (System.Exception ex)
            {
                return false;
            }
            return true;
        }
        public static bool CreateCircle(Worksheet workSheet, double left, double top, double width, double height, out Point8 sPoint8)
        {
            Microsoft.Office.Interop.Excel.Shape b = null; sPoint8 = new Point8();
            try
            {
                b = workSheet.Shapes.AddShape(MsoAutoShapeType.msoShapeFlowchartConnector, (float)left, (float)top, (float)width, (float)height);
                b.Fill.Visible = MsoTriState.msoTrue;
                b.Fill.ForeColor.RGB = ColorTranslator.ToOle(Color.FromArgb(255, 0, 151));
                b.Line.ForeColor.RGB = ColorTranslator.ToOle(Color.FromArgb(0, 0, 0));
                b.Line.Weight = (float)0.75;

                GetPoint8(left, top, width, height, out sPoint8);
            }
            catch (System.Exception ex)
            {
                return false;
            }
            return true;
        }
        public static bool CreateCircleWithText(Worksheet workSheet, string text, double left, double top, double width, double height, out Point8 sPoint8)
        {
            Microsoft.Office.Interop.Excel.Shape b = null; sPoint8 = new Point8();
            try
            {
                b = workSheet.Shapes.AddShape(MsoAutoShapeType.msoShapeFlowchartConnector, (float)left, (float)top, (float)width, (float)height);
                b.Fill.Visible = MsoTriState.msoTrue;
                b.Fill.ForeColor.RGB = ColorTranslator.ToOle(Color.FromArgb(255, 0, 151));
                b.Line.ForeColor.RGB = ColorTranslator.ToOle(Color.FromArgb(0, 0, 0));
                b.Line.Weight = (float)0.75;

                GetPoint8(left, top, width, height, out sPoint8);

                WriteTextBox(workSheet, "Middle", text, "", sPoint8.sLeftUp.X, sPoint8.sLeftUp.Y, 16.5, 16.5);
            }
            catch (System.Exception ex)
            {
                return false;
            }
            return true;
        }
        public static bool CreateParallelogram(Worksheet workSheet, double left, double top, double width, double height)
        {
            Microsoft.Office.Interop.Excel.Shape b = null;
            try
            {
                b = workSheet.Shapes.AddShape(MsoAutoShapeType.msoShapeFlowchartData, (float)left, (float)top, (float)width, (float)height);
                b.Fill.Visible = MsoTriState.msoFalse;
                int oleColor = ColorTranslator.ToOle(Color.FromArgb(0, 0, 0));
                b.Line.ForeColor.RGB = oleColor;
                b.Line.Weight = (float)0.75;
            }
            catch (System.Exception ex)
            {
                return false;
            }
            return true;
        }
        public static bool WriteTextBox(Worksheet workSheet, string alignStatus, string op1, string op2, double left, double top, double width, double height)
        {
            Microsoft.Office.Interop.Excel.Shape b = null;
            try
            {
                b = workSheet.Shapes.AddTextbox(MsoTextOrientation.msoTextOrientationHorizontal, (float)left, (float)top, (float)width, (float)height);
                b.Fill.Visible = MsoTriState.msoFalse;
                b.Line.Visible = MsoTriState.msoFalse;
                b.TextFrame2.TextRange.ParagraphFormat.Alignment = MsoParagraphAlignment.msoAlignCenter;
                if (alignStatus == "Middle")
                {
                    b.TextFrame2.VerticalAnchor = MsoVerticalAnchor.msoAnchorMiddle;
                }
                else if (alignStatus == "Top")
                {
                    b.TextFrame2.VerticalAnchor = MsoVerticalAnchor.msoAnchorTop;
                }
                int oleColor = ColorTranslator.ToOle(Color.FromArgb(0, 0, 0));
                b.TextFrame2.TextRange.Font.Fill.ForeColor.RGB = oleColor;
                b.TextFrame2.TextRange.Font.Fill.Visible = MsoTriState.msoTrue;
                b.TextFrame2.TextRange.Font.Size = 8;
                if (op2 == "")
                {
                    b.TextFrame2.VerticalAnchor = MsoVerticalAnchor.msoAnchorMiddle;
                    b.TextFrame2.TextRange.Characters.Text = op1;
                }
                else
                {
                    b.TextFrame2.TextRange.Characters.Text = op1 + "\r\n" + op2;
                }

            }
            catch (System.Exception ex)
            {
                return false;
            }
            return true;
        }
        public static bool CreateDownArrow(Worksheet workSheet, double beginX, double beginY, double endX, double endY, out Point8 sPoint8)
        {
            Microsoft.Office.Interop.Excel.Shape b = null; sPoint8 = new Point8();
            try
            {
                b = workSheet.Shapes.AddConnector(MsoConnectorType.msoConnectorStraight, (float)beginX, (float)beginY, (float)endX, (float)endY);
                b.Line.EndArrowheadStyle = MsoArrowheadStyle.msoArrowheadTriangle;
                int oleColor = ColorTranslator.ToOle(Color.FromArgb(0, 0, 0));
                b.Line.ForeColor.RGB = oleColor;

                //左上
                sPoint8.sLeftUp.X = beginX;
                sPoint8.sLeftUp.Y = beginY;
                //左中
                sPoint8.sLeft.X = beginX;
                sPoint8.sLeft.Y = beginY + ((endY - beginY) / 2);
                //左下
                sPoint8.sLeftLower.X = beginX;
                sPoint8.sLeftLower.Y = endY;
                //中上
                sPoint8.sMiddleUp.X = beginX;
                sPoint8.sMiddleUp.Y = beginY;
                //中下
                sPoint8.sMiddleLower.X = beginX;
                sPoint8.sMiddleLower.Y = endY;
                //右上
                sPoint8.sRightUp.X = beginX;
                sPoint8.sRightUp.Y = beginY;
                //右中
                sPoint8.sRight.X = beginX;
                sPoint8.sRight.Y = beginY + ((endY - beginY) / 2);
                //右下
                sPoint8.sRightLower.X = beginX;
                sPoint8.sRightLower.Y = endY;
            }
            catch (System.Exception ex)
            {
                return false;
            }
            return true;
        }
        public static bool CreateLeftArrow(Worksheet workSheet, double beginX, double beginY, double endX, double endY, out Point8 sPoint8)
        {
            Microsoft.Office.Interop.Excel.Shape b = null; sPoint8 = new Point8();
            try
            {
                b = workSheet.Shapes.AddConnector(MsoConnectorType.msoConnectorStraight, (float)beginX, (float)beginY, (float)(endX - 30), (float)endY);
                b.Line.EndArrowheadStyle = MsoArrowheadStyle.msoArrowheadTriangle;
                int oleColor = ColorTranslator.ToOle(Color.FromArgb(0, 0, 0));
                b.Line.ForeColor.RGB = oleColor;

                //左上
                sPoint8.sLeftUp.X = endX;
                sPoint8.sLeftUp.Y = endY;
                //左中
                sPoint8.sLeft.X = endX;
                sPoint8.sLeft.Y = endY;
                //左下
                sPoint8.sLeftLower.X = endX;
                sPoint8.sLeftLower.Y = endY;
                //中上
                sPoint8.sMiddleUp.X = beginX + ((endY - beginY) / 2);
                sPoint8.sMiddleUp.Y = beginY;
                //中下
                sPoint8.sMiddleLower.X = beginX + ((endY - beginY) / 2);
                sPoint8.sMiddleLower.Y = beginY;
                //右上
                sPoint8.sRightUp.X = beginX;
                sPoint8.sRightUp.Y = beginY;
                //右中
                sPoint8.sRight.X = beginX;
                sPoint8.sRight.Y = beginY;
                //右下
                sPoint8.sRightLower.X = beginX;
                sPoint8.sRightLower.Y = beginY;
            }
            catch (System.Exception ex)
            {
                return false;
            }
            return true;
        }
        public static bool CreateRightArrow(Worksheet workSheet, double beginX, double beginY, double endX, double endY, out Point8 sPoint8)
        {
            Microsoft.Office.Interop.Excel.Shape b = null; sPoint8 = new Point8();
            try
            {
                b = workSheet.Shapes.AddConnector(MsoConnectorType.msoConnectorStraight, (float)beginX, (float)beginY, (float)endX, (float)endY);
                b.Line.EndArrowheadStyle = MsoArrowheadStyle.msoArrowheadTriangle;
                int oleColor = ColorTranslator.ToOle(Color.FromArgb(0, 0, 0));
                b.Line.ForeColor.RGB = oleColor;

                //左上
                sPoint8.sLeftUp.X = beginX;
                sPoint8.sLeftUp.Y = beginY;
                //左中
                sPoint8.sLeft.X = beginX;
                sPoint8.sLeft.Y = beginY;
                //左下
                sPoint8.sLeftLower.X = beginX;
                sPoint8.sLeftLower.Y = beginY;
                //中上
                sPoint8.sMiddleUp.X = beginX + ((endY - beginY) / 2);
                sPoint8.sMiddleUp.Y = beginY;
                //中下
                sPoint8.sMiddleLower.X = beginX + ((endY - beginY) / 2);
                sPoint8.sMiddleLower.Y = beginY;
                //右上
                sPoint8.sRightUp.X = endX;
                sPoint8.sRightUp.Y = endY;
                //右中
                sPoint8.sRight.X = endX;
                sPoint8.sRight.Y = endY;
                //右下
                sPoint8.sRightLower.X = endX;
                sPoint8.sRightLower.Y = endY;
            }
            catch (System.Exception ex)
            {
                return false;
            }
            return true;
        }
        public static bool CreateDownArrowWithOK(Worksheet workSheet, double beginX, double beginY, double endX, double endY, out Point8 sPoint8)
        {
            Microsoft.Office.Interop.Excel.Shape b = null; sPoint8 = new Point8();
            try
            {
                b = workSheet.Shapes.AddConnector(MsoConnectorType.msoConnectorStraight, (float)beginX, (float)beginY, (float)endX, (float)endY);
                b.Line.EndArrowheadStyle = MsoArrowheadStyle.msoArrowheadTriangle;
                int oleColor = ColorTranslator.ToOle(Color.FromArgb(0, 0, 0));
                b.Line.ForeColor.RGB = oleColor;

                //左上
                sPoint8.sLeftUp.X = beginX;
                sPoint8.sLeftUp.Y = beginY;
                //左中
                sPoint8.sLeft.X = beginX;
                sPoint8.sLeft.Y = beginY + ((endY - beginY) / 2);
                //左下
                sPoint8.sLeftLower.X = beginX;
                sPoint8.sLeftLower.Y = endY;
                //中上
                sPoint8.sMiddleUp.X = beginX;
                sPoint8.sMiddleUp.Y = beginY;
                //中下
                sPoint8.sMiddleLower.X = beginX;
                sPoint8.sMiddleLower.Y = endY;
                //右上
                sPoint8.sRightUp.X = beginX;
                sPoint8.sRightUp.Y = beginY;
                //右中
                sPoint8.sRight.X = beginX;
                sPoint8.sRight.Y = beginY + ((endY - beginY) / 2);
                //右下
                sPoint8.sRightLower.X = beginX;
                sPoint8.sRightLower.Y = endY;

                WriteTextBox(workSheet, "Middle", "OK", "", (sPoint8.sRightUp.X - 8), (sPoint8.sRightUp.Y - 3), 33, 16.5);

            }
            catch (System.Exception ex)
            {
                return false;
            }
            return true;
        }
        public static bool CreateLeftArrowWithNG(Worksheet workSheet, double beginX, double beginY, double endX, double endY, out Point8 sPoint8)
        {
            Microsoft.Office.Interop.Excel.Shape b = null; sPoint8 = new Point8();
            try
            {
                b = workSheet.Shapes.AddConnector(MsoConnectorType.msoConnectorStraight, (float)beginX, (float)beginY, (float)endX, (float)endY);
                b.Line.EndArrowheadStyle = MsoArrowheadStyle.msoArrowheadTriangle;
                int oleColor = ColorTranslator.ToOle(Color.FromArgb(0, 0, 0));
                b.Line.ForeColor.RGB = oleColor;

                //左上
                sPoint8.sLeftUp.X = endX;
                sPoint8.sLeftUp.Y = endY;
                //左中
                sPoint8.sLeft.X = endX;
                sPoint8.sLeft.Y = endY;
                //左下
                sPoint8.sLeftLower.X = endX;
                sPoint8.sLeftLower.Y = endY;
                //中上
                sPoint8.sMiddleUp.X = beginX + ((endY - beginY) / 2);
                sPoint8.sMiddleUp.Y = beginY;
                //中下
                sPoint8.sMiddleLower.X = beginX + ((endY - beginY) / 2);
                sPoint8.sMiddleLower.Y = beginY;
                //右上
                sPoint8.sRightUp.X = beginX;
                sPoint8.sRightUp.Y = beginY;
                //右中
                sPoint8.sRight.X = beginX;
                sPoint8.sRight.Y = beginY;
                //右下
                sPoint8.sRightLower.X = beginX;
                sPoint8.sRightLower.Y = beginY;

                WriteTextBox(workSheet, "Middle", "NG", "", (sPoint8.sLeftUp.X - 5), (sPoint8.sLeftUp.Y - 16.5), 33, 16.5);
            }
            catch (System.Exception ex)
            {
                return false;
            }
            return true;
        }
        public static bool CreateRightArrowWithOK(Worksheet workSheet, double beginX, double beginY, double endX, double endY, out Point8 sPoint8)
        {
            Microsoft.Office.Interop.Excel.Shape b = null; sPoint8 = new Point8();
            try
            {
                b = workSheet.Shapes.AddConnector(MsoConnectorType.msoConnectorStraight, (float)beginX, (float)beginY, (float)endX, (float)endY);
                b.Line.EndArrowheadStyle = MsoArrowheadStyle.msoArrowheadTriangle;
                int oleColor = ColorTranslator.ToOle(Color.FromArgb(0, 0, 0));
                b.Line.ForeColor.RGB = oleColor;

                //左上
                sPoint8.sLeftUp.X = beginX;
                sPoint8.sLeftUp.Y = beginY;
                //左中
                sPoint8.sLeft.X = beginX;
                sPoint8.sLeft.Y = beginY;
                //左下
                sPoint8.sLeftLower.X = beginX;
                sPoint8.sLeftLower.Y = beginY;
                //中上
                sPoint8.sMiddleUp.X = beginX + ((endY - beginY) / 2);
                sPoint8.sMiddleUp.Y = beginY;
                //中下
                sPoint8.sMiddleLower.X = beginX + ((endY - beginY) / 2);
                sPoint8.sMiddleLower.Y = beginY;
                //右上
                sPoint8.sRightUp.X = endX;
                sPoint8.sRightUp.Y = endY;
                //右中
                sPoint8.sRight.X = endX;
                sPoint8.sRight.Y = endY;
                //右下
                sPoint8.sRightLower.X = endX;
                sPoint8.sRightLower.Y = endY;

                WriteTextBox(workSheet, "Middle", "OK", "", (sPoint8.sLeftUp.X - 8), (sPoint8.sLeftUp.Y - 16.5), 33, 16.5);
            }
            catch (System.Exception ex)
            {
                return false;
            }
            return true;
        }
        public static bool CreateStraightLine(Worksheet workSheet, double beginX, double beginY, double endX, double endY, out Point8 sPoint8)
        {
            Microsoft.Office.Interop.Excel.Shape b = null; sPoint8 = new Point8();
            try
            {
                b = workSheet.Shapes.AddConnector(MsoConnectorType.msoConnectorStraight, (float)beginX, (float)beginY, (float)endX, (float)endY);
                b.Line.EndArrowheadStyle = MsoArrowheadStyle.msoArrowheadNone;
                b.Fill.Visible = MsoTriState.msoFalse;
                int oleColor = ColorTranslator.ToOle(Color.FromArgb(0, 0, 0));
                b.Line.ForeColor.RGB = oleColor;
                b.Line.Weight = (float)0.75;

                //左上
                sPoint8.sLeftUp.X = beginX;
                sPoint8.sLeftUp.Y = beginY;
                //左中
                sPoint8.sLeft.X = beginX;
                sPoint8.sLeft.Y = beginY + ((endY - beginY) / 2);
                //左下
                sPoint8.sLeftLower.X = beginX;
                sPoint8.sLeftLower.Y = endY;
                //中上
                sPoint8.sMiddleUp.X = beginX;
                sPoint8.sMiddleUp.Y = beginY;
                //中下
                sPoint8.sMiddleLower.X = beginX;
                sPoint8.sMiddleLower.Y = endY;
                //右上
                sPoint8.sRightUp.X = beginX;
                sPoint8.sRightUp.Y = beginY;
                //右中
                sPoint8.sRight.X = beginX;
                sPoint8.sRight.Y = beginY + ((endY - beginY) / 2);
                //右下
                sPoint8.sRightLower.X = beginX;
                sPoint8.sRightLower.Y = endY;
            }
            catch (System.Exception ex)
            {
                return false;
            }
            return true;
        }
        public static bool GetExcelCoordinate(int count, out double leftDist, out double topDist)
        {
            leftDist = 0;
            topDist = 0;
            try
            {
                if (count == 7)
                {
                }
            }
            catch (System.Exception ex)
            {
                return false;
            }
            return true;
        }

        public static bool GetPoint8(double left, double top, double width, double height, out Point8 sPoint8)
        {
            sPoint8 = new Point8();
            try
            {
                //左上
                sPoint8.sLeftUp.X = left;
                sPoint8.sLeftUp.Y = top;
                //左中
                sPoint8.sLeft.X = left;
                sPoint8.sLeft.Y = top + (height / 2);
                //左下
                sPoint8.sLeftLower.X = left;
                sPoint8.sLeftLower.Y = top + height;
                //中上
                sPoint8.sMiddleUp.X = left + (width / 2);
                sPoint8.sMiddleUp.Y = top;
                //中下
                sPoint8.sMiddleLower.X = left + (width / 2);
                sPoint8.sMiddleLower.Y = top + height;
                //右上
                sPoint8.sRightUp.X = left + width;
                sPoint8.sRightUp.Y = top;
                //右中
                sPoint8.sRight.X = left + width;
                sPoint8.sRight.Y = top + (height / 2);
                //右下
                sPoint8.sRightLower.X = left + width;
                sPoint8.sRightLower.Y = top + height;
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
