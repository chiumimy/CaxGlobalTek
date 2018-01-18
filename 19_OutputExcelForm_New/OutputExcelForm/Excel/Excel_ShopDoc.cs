using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using CaxGlobaltek;
using Microsoft.Office.Interop.Excel;
using System.IO;
using System.Windows.Forms;
using NHibernate;

namespace OutputExcelForm.Excel
{
    public class Excel_ShopDoc
    {
        private static bool status;
        private static string OutputPath = "";
        public struct RowColumn
        {
            //刀號
            public int ToolNumberRow { get; set; }
            public int ToolNumberColumn { get; set; }
            //刀具名稱
            public int ToolNameRow { get; set; }
            public int ToolNameColumn { get; set; }
            //程式名稱
            public int OperNameRow { get; set; }
            public int OperNameColumn { get; set; }
            //刀柄名稱
            public int HolderRow { get; set; }
            public int HolderColumn { get; set; }
            //刀具壽命
            public int CutterLifeRow { get; set; }
            public int CutterLifeColumn { get; set; }
            //刀具伸長量
            public int ExtensionRow { get; set; }
            public int ExtensionColumn { get; set; }
            //加工時間
            public int CuttingTimeRow { get; set; }
            public int CuttingTimeColumn { get; set; }
            //進給
            public int ToolFeedRow { get; set; }
            public int ToolFeedColumn { get; set; }
            //速度
            public int ToolSpeedRow { get; set; }
            public int ToolSpeedColumn { get; set; }
            //加工路徑圖片
            public int OperImgToolRow { get; set; }
            public int OperImgToolColumn { get; set; }
            //留料
            public int PartStockRow { get; set; }
            public int PartStockColumn { get; set; }
            //循環時間
            public int TotalCuttingTimeRow { get; set; }
            public int TotalCuttingTimeColumn { get; set; }
            //料號
            public int PartNoRow { get; set; }
            public int PartNoColumn { get; set; }
            //品名
            public int PartDescRow { get; set; }
            public int PartDescColumn { get; set; }
            //機台型號
            public int MachineNoRow { get; set; }
            public int MachineNoColumn { get; set; }
            //設計
            public int DesignedRow { get; set; }
            public int DesignedColumn { get; set; }
            //審核
            public int ReviewedRow { get; set; }
            public int ReviewedColumn { get; set; }
            //批准
            public int ApprovedRow { get; set; }
            public int ApprovedColumn { get; set; }
        }
        public struct OperImgPosiSize
        {
            public float OperPosiLeft { get; set; }
            public float OperPosiTop { get; set; }
            public float OperImgWidth { get; set; }
            public float OperImgHeight { get; set; }
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
            //料號
            sRowColumn.PartNoRow = 52;
            sRowColumn.PartNoColumn = 11;
            //品名
            sRowColumn.PartDescRow = 51;
            sRowColumn.PartDescColumn = 11;
            //循環時間
            sRowColumn.TotalCuttingTimeRow = 51;
            sRowColumn.TotalCuttingTimeColumn = 2;
            //機台型號
            sRowColumn.MachineNoRow = 52;
            sRowColumn.MachineNoColumn = 4;
            //設計
            sRowColumn.DesignedRow = 52;
            sRowColumn.DesignedColumn = 6;
            //審核
            sRowColumn.ReviewedRow = 52;
            sRowColumn.ReviewedColumn = 7;
            //批准
            sRowColumn.ApprovedRow = 52;
            sRowColumn.ApprovedColumn = 8;


            int currentNo = (i % 8);

            if (currentNo == 0)
            {
                sRowColumn.PartStockRow = 27;
                sRowColumn.PartStockColumn = 4;

                sRowColumn.OperImgToolRow = 5;
                sRowColumn.OperImgToolColumn = 1;

                //ShopDoc_C版改成刀號拿掉只放刀具名稱(名稱已包含刀號)
                sRowColumn.ToolNumberRow = 23;
                sRowColumn.ToolNumberColumn = 2;
                sRowColumn.ToolNameRow = 23;
                sRowColumn.ToolNameColumn = 2;

                sRowColumn.OperNameRow = 24;
                sRowColumn.OperNameColumn = 2;

                sRowColumn.HolderRow = 25;
                sRowColumn.HolderColumn = 2;

                sRowColumn.CutterLifeRow = 26;
                sRowColumn.CutterLifeColumn = 2;

                sRowColumn.ExtensionRow = 26;
                sRowColumn.ExtensionColumn = 4;

                sRowColumn.ToolFeedRow = 27;
                sRowColumn.ToolFeedColumn = 2;

                sRowColumn.ToolSpeedRow = 27;
                sRowColumn.ToolSpeedColumn = 3;

                sRowColumn.CuttingTimeRow = 28;
                sRowColumn.CuttingTimeColumn = 2;
            }
            else if (currentNo == 1)
            {
                sRowColumn.PartStockRow = 33;
                sRowColumn.PartStockColumn = 4;

                sRowColumn.OperImgToolRow = 5;
                sRowColumn.OperImgToolColumn = 4;

                //ShopDoc_C版改成刀號拿掉只放刀具名稱(名稱已包含刀號)
                sRowColumn.ToolNumberRow = 29;
                sRowColumn.ToolNumberColumn = 2;
                sRowColumn.ToolNameRow = 29;
                sRowColumn.ToolNameColumn = 2;

                sRowColumn.OperNameRow = 30;
                sRowColumn.OperNameColumn = 2;

                sRowColumn.HolderRow = 31;
                sRowColumn.HolderColumn = 2;

                sRowColumn.CutterLifeRow = 32;
                sRowColumn.CutterLifeColumn = 2;

                sRowColumn.ExtensionRow = 32;
                sRowColumn.ExtensionColumn = 4;

                sRowColumn.ToolFeedRow = 33;
                sRowColumn.ToolFeedColumn = 2;

                sRowColumn.ToolSpeedRow = 33;
                sRowColumn.ToolSpeedColumn = 3;

                sRowColumn.CuttingTimeRow = 34;
                sRowColumn.CuttingTimeColumn = 2;
            }
            else if (currentNo == 2)
            {
                sRowColumn.PartStockRow = 39;
                sRowColumn.PartStockColumn = 4;

                sRowColumn.OperImgToolRow = 5;
                sRowColumn.OperImgToolColumn = 7;

                //ShopDoc_C版改成刀號拿掉只放刀具名稱(名稱已包含刀號)
                sRowColumn.ToolNumberRow = 35;
                sRowColumn.ToolNumberColumn = 2;
                sRowColumn.ToolNameRow = 35;
                sRowColumn.ToolNameColumn = 2;

                sRowColumn.OperNameRow = 36;
                sRowColumn.OperNameColumn = 2;

                sRowColumn.HolderRow = 37;
                sRowColumn.HolderColumn = 2;

                sRowColumn.CutterLifeRow = 38;
                sRowColumn.CutterLifeColumn = 2;

                sRowColumn.ExtensionRow = 38;
                sRowColumn.ExtensionColumn = 4;

                sRowColumn.ToolFeedRow = 39;
                sRowColumn.ToolFeedColumn = 2;

                sRowColumn.ToolSpeedRow = 39;
                sRowColumn.ToolSpeedColumn = 3;

                sRowColumn.CuttingTimeRow = 40;
                sRowColumn.CuttingTimeColumn = 2;
            }
            else if (currentNo == 3)
            {
                sRowColumn.PartStockRow = 45;
                sRowColumn.PartStockColumn = 4;

                sRowColumn.OperImgToolRow = 5;
                sRowColumn.OperImgToolColumn = 10;

                //ShopDoc_C版改成刀號拿掉只放刀具名稱(名稱已包含刀號)
                sRowColumn.ToolNumberRow = 41;
                sRowColumn.ToolNumberColumn = 2;
                sRowColumn.ToolNameRow = 41;
                sRowColumn.ToolNameColumn = 2;

                sRowColumn.OperNameRow = 42;
                sRowColumn.OperNameColumn = 2;

                sRowColumn.HolderRow = 43;
                sRowColumn.HolderColumn = 2;

                sRowColumn.CutterLifeRow = 44;
                sRowColumn.CutterLifeColumn = 2;

                sRowColumn.ExtensionRow = 44;
                sRowColumn.ExtensionColumn = 4;

                sRowColumn.ToolFeedRow = 45;
                sRowColumn.ToolFeedColumn = 2;

                sRowColumn.ToolSpeedRow = 45;
                sRowColumn.ToolSpeedColumn = 3;

                sRowColumn.CuttingTimeRow = 46;
                sRowColumn.CuttingTimeColumn = 2;
            }
            else if (currentNo == 4)
            {
                sRowColumn.PartStockRow = 27;
                sRowColumn.PartStockColumn = 8;

                sRowColumn.OperImgToolRow = 13;
                sRowColumn.OperImgToolColumn = 1;

                //ShopDoc_C版改成刀號拿掉只放刀具名稱(名稱已包含刀號)
                sRowColumn.ToolNumberRow = 23;
                sRowColumn.ToolNumberColumn = 6;
                sRowColumn.ToolNameRow = 23;
                sRowColumn.ToolNameColumn = 6;

                sRowColumn.OperNameRow = 24;
                sRowColumn.OperNameColumn = 6;

                sRowColumn.HolderRow = 25;
                sRowColumn.HolderColumn = 6;

                sRowColumn.CutterLifeRow = 26;
                sRowColumn.CutterLifeColumn = 6;

                sRowColumn.ExtensionRow = 26;
                sRowColumn.ExtensionColumn = 8;

                sRowColumn.ToolFeedRow = 27;
                sRowColumn.ToolFeedColumn = 6;

                sRowColumn.ToolSpeedRow = 27;
                sRowColumn.ToolSpeedColumn = 7;

                sRowColumn.CuttingTimeRow = 28;
                sRowColumn.CuttingTimeColumn = 6;
            }
            else if (currentNo == 5)
            {
                sRowColumn.PartStockRow = 33;
                sRowColumn.PartStockColumn = 8;

                sRowColumn.OperImgToolRow = 13;
                sRowColumn.OperImgToolColumn = 4;

                //ShopDoc_C版改成刀號拿掉只放刀具名稱(名稱已包含刀號)
                sRowColumn.ToolNumberRow = 29;
                sRowColumn.ToolNumberColumn = 6;
                sRowColumn.ToolNameRow = 29;
                sRowColumn.ToolNameColumn = 6;

                sRowColumn.OperNameRow = 30;
                sRowColumn.OperNameColumn = 6;

                sRowColumn.HolderRow = 31;
                sRowColumn.HolderColumn = 6;

                sRowColumn.CutterLifeRow = 32;
                sRowColumn.CutterLifeColumn = 6;

                sRowColumn.ExtensionRow = 32;
                sRowColumn.ExtensionColumn = 8;

                sRowColumn.ToolFeedRow = 33;
                sRowColumn.ToolFeedColumn = 6;

                sRowColumn.ToolSpeedRow = 33;
                sRowColumn.ToolSpeedColumn = 7;

                sRowColumn.CuttingTimeRow = 34;
                sRowColumn.CuttingTimeColumn = 6;
            }
            else if (currentNo == 6)
            {
                sRowColumn.PartStockRow = 39;
                sRowColumn.PartStockColumn = 8;

                sRowColumn.OperImgToolRow = 13;
                sRowColumn.OperImgToolColumn = 7;

                //ShopDoc_C版改成刀號拿掉只放刀具名稱(名稱已包含刀號)
                sRowColumn.ToolNumberRow = 35;
                sRowColumn.ToolNumberColumn = 6;
                sRowColumn.ToolNameRow = 35;
                sRowColumn.ToolNameColumn = 6;

                sRowColumn.OperNameRow = 36;
                sRowColumn.OperNameColumn = 6;

                sRowColumn.HolderRow = 37;
                sRowColumn.HolderColumn = 6;

                sRowColumn.CutterLifeRow = 38;
                sRowColumn.CutterLifeColumn = 6;

                sRowColumn.ExtensionRow = 38;
                sRowColumn.ExtensionColumn = 8;

                sRowColumn.ToolFeedRow = 39;
                sRowColumn.ToolFeedColumn = 6;

                sRowColumn.ToolSpeedRow = 39;
                sRowColumn.ToolSpeedColumn = 7;

                sRowColumn.CuttingTimeRow = 40;
                sRowColumn.CuttingTimeColumn = 6;
            }
            else if (currentNo == 7)
            {
                sRowColumn.PartStockRow = 45;
                sRowColumn.PartStockColumn = 8;

                sRowColumn.OperImgToolRow = 13;
                sRowColumn.OperImgToolColumn = 10;

                //ShopDoc_C版改成刀號拿掉只放刀具名稱(名稱已包含刀號)
                sRowColumn.ToolNumberRow = 41;
                sRowColumn.ToolNumberColumn = 6;
                sRowColumn.ToolNameRow = 41;
                sRowColumn.ToolNameColumn = 6;

                sRowColumn.OperNameRow = 42;
                sRowColumn.OperNameColumn = 6;

                sRowColumn.HolderRow = 43;
                sRowColumn.HolderColumn = 6;

                sRowColumn.CutterLifeRow = 44;
                sRowColumn.CutterLifeColumn = 6;

                sRowColumn.ExtensionRow = 44;
                sRowColumn.ExtensionColumn = 8;

                sRowColumn.ToolFeedRow = 45;
                sRowColumn.ToolFeedColumn = 6;

                sRowColumn.ToolSpeedRow = 45;
                sRowColumn.ToolSpeedColumn = 7;

                sRowColumn.CuttingTimeRow = 46;
                sRowColumn.CuttingTimeColumn = 6;
            }
        }
        public static void GetOperImgPosiAndSize(int i, out OperImgPosiSize sOperImgPosiSize)
        {
            string ScreenWidth = SystemInformation.PrimaryMonitorSize.Width.ToString();
            string ScreenHeight = SystemInformation.PrimaryMonitorSize.Height.ToString();

            sOperImgPosiSize = new OperImgPosiSize();
            int currentNo = (i % 8);

            if (currentNo == 0)
            {
                sOperImgPosiSize.OperPosiLeft = 5;
                sOperImgPosiSize.OperPosiTop = 118;
            }
            else if (currentNo == 1)
            {
                sOperImgPosiSize.OperPosiLeft = 5 + (float)(180 * 1366 / Convert.ToDouble(ScreenWidth));
                sOperImgPosiSize.OperPosiTop = 118;
            }
            else if (currentNo == 2)
            {
                sOperImgPosiSize.OperPosiLeft = 5 + (float)(360 * 1366 / Convert.ToDouble(ScreenWidth));
                sOperImgPosiSize.OperPosiTop = 118;
            }
            else if (currentNo == 3)
            {
                sOperImgPosiSize.OperPosiLeft = (float)(540 * 1366 / Convert.ToDouble(ScreenWidth));
                sOperImgPosiSize.OperPosiTop = 118;
            }
            else if (currentNo == 4)
            {
                sOperImgPosiSize.OperPosiLeft = 5;
                sOperImgPosiSize.OperPosiTop = 265;
            }
            else if (currentNo == 5)
            {
                sOperImgPosiSize.OperPosiLeft = 5 + (float)(180 * 1366 / Convert.ToDouble(ScreenWidth));
                sOperImgPosiSize.OperPosiTop = 265;
            }
            else if (currentNo == 6)
            {
                sOperImgPosiSize.OperPosiLeft = 5 + (float)(360 * 1366 / Convert.ToDouble(ScreenWidth));
                sOperImgPosiSize.OperPosiTop = 265;
            }
            else if (currentNo == 7)
            {
                sOperImgPosiSize.OperPosiLeft = (float)(540 * 1366 / Convert.ToDouble(ScreenWidth));
                sOperImgPosiSize.OperPosiTop = 265;
            }
            sOperImgPosiSize.OperImgWidth = 160;
            sOperImgPosiSize.OperImgHeight = 115;


            float width = sOperImgPosiSize.OperImgWidth;
            float height = sOperImgPosiSize.OperImgHeight;
            Excel_CommonFun.GetScreenResolution(ref width, ref height);
            sOperImgPosiSize.OperImgWidth = width;
            sOperImgPosiSize.OperImgHeight = height;
        }
        
        public static void GetFixImgPosiAndSize(out FixImgPosiSize sFixImgPosiSize)
        {
            string ScreenWidth = SystemInformation.PrimaryMonitorSize.Width.ToString();
            string ScreenHeight = SystemInformation.PrimaryMonitorSize.Height.ToString();

            sFixImgPosiSize = new FixImgPosiSize();
            //sFixImgPosiSize.FixPosiLeft = 485;
            sFixImgPosiSize.FixPosiLeft = (float)(485 * 1366 / Convert.ToDouble(ScreenWidth));
            sFixImgPosiSize.FixPosiTop = 423;
            //sFixImgPosiSize.FixPosiTop = (float)(423 * 768 / Convert.ToDouble(ScreenHeight));
            sFixImgPosiSize.FixImgWidth = 225;
            sFixImgPosiSize.FixImgHeight = 198;

            float width = sFixImgPosiSize.FixImgWidth;
            float height = sFixImgPosiSize.FixImgHeight;
            Excel_CommonFun.GetScreenResolution(ref width, ref height);
            sFixImgPosiSize.FixImgWidth = width;
            sFixImgPosiSize.FixImgHeight = height;

        }
        public static bool CreateShopDocExcel_XinWu(string cusName, string partNo, string cusVer, string opVer, string op1, DB_TEMain sDB_TEMain, IList<Com_ShopDoc> cCom_ShopDoc)
        {
            //判斷Server的Template是否存在
            if (!File.Exists(sDB_TEMain.excelTemplateFilePath))
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
                                                    , sDB_TEMain.comTEMain.ncGroupName + "_" + "ShopDoc" + ".xls");


                //MessageBox.Show("0");
                //1.開啟Excel
                //2.將Excel設為不顯示
                //3.取得第一頁Sheet
                workBook = (Workbook)excelApp.Workbooks.Open(sDB_TEMain.excelTemplateFilePath);
                //MessageBox.Show("1");
                excelApp.Visible = false;
                //MessageBox.Show("2");
                workSheet = (Worksheet)workBook.Sheets[1];
                //MessageBox.Show("3");
                //建立Sheet頁數符合所有的Operation
                status = Excel_CommonFun.AddNewSheet(cCom_ShopDoc.Count, 8, excelApp, workSheet);
                if (!status)
                {
                    MessageBox.Show("建立Sheet頁失敗，請聯繫開發工程師");
                    return false;
                }
 
                //修改每一個Sheet名字和頁數
                status = Excel_CommonFun.ModifySheet(sDB_TEMain.ncGroupName, "ShopDoc", workBook, workSheet, workRange, sDB_TEMain.factory);
                if (!status)
                {
                    MessageBox.Show("修改Sheet名字和頁數失敗，請聯繫開發工程師");
                    return false;
                }
             
                //計算欄位的初始TOP
                double de = 0;
                for (int i = 1; i < 23; i++)
                {
                    workRange = (Range)workSheet.Cells[i, 1];
                    de = de + Convert.ToDouble(workRange.Height);
                    //CaxLog.ShowListingWindow(oRng.Height.ToString());
                }
                //CaxLog.ShowListingWindow(de.ToString());

                //計算欄位的初始LEFT
                double abc = 0;
                for (int i = 1; i < 9; i++)
                {
                    workRange = (Range)workSheet.Cells[23, i];
                    abc = abc + Convert.ToDouble(workRange.Width.ToString());
                }

                //開始填表
                RowColumn sRowColumn;
                for (int i = 0; i < cCom_ShopDoc.Count; i++)
                {
                    GetExcelRowColumn(i, out sRowColumn);
                    //取得當前Operation該放置的Sheet
                    int currentSheet_Value = (i / 8);
                    //int currentSheet_Reserve = (i % 8);
                    if (currentSheet_Value == 0)
                    {
                        workSheet = (Worksheet)workBook.Sheets[1];
                    }
                    else
                    {
                        workSheet = (Worksheet)workBook.Sheets[currentSheet_Value + 1];
                    }

                    workRange = (Range)workSheet.Cells;
                    workRange[sRowColumn.OperImgToolRow, sRowColumn.OperImgToolColumn] = cCom_ShopDoc[i].toolNo + "_" + cCom_ShopDoc[i].operationName;
                    //workRange[sRowColumn.ToolNumberRow, sRowColumn.ToolNumberColumn] = cCom_ShopDoc[i].toolNo;
                    workRange[sRowColumn.ToolNameRow, sRowColumn.ToolNameColumn] = cCom_ShopDoc[i].toolID;
                    workRange[sRowColumn.OperNameRow, sRowColumn.OperNameColumn] = cCom_ShopDoc[i].operationName;
                    workRange[sRowColumn.HolderRow, sRowColumn.HolderColumn] = cCom_ShopDoc[i].holderID;
                    workRange[sRowColumn.ToolFeedRow, sRowColumn.ToolFeedColumn] = "F：" + cCom_ShopDoc[i].feed;
                    workRange[sRowColumn.ToolSpeedRow, sRowColumn.ToolSpeedColumn] = cCom_ShopDoc[i].speed;
                    workRange[sRowColumn.PartStockRow, sRowColumn.PartStockColumn] = cCom_ShopDoc[i].partStock;
                    workRange[sRowColumn.CutterLifeRow, sRowColumn.CutterLifeColumn] = cCom_ShopDoc[i].cutterLife;
                    workRange[sRowColumn.ExtensionRow, sRowColumn.ExtensionColumn] = cCom_ShopDoc[i].extension;
                    workRange[sRowColumn.CuttingTimeRow, sRowColumn.CuttingTimeColumn] = cCom_ShopDoc[i].machiningtime;
                    workRange[sRowColumn.PartNoRow, sRowColumn.PartNoColumn] = partNo;
                    workRange[sRowColumn.TotalCuttingTimeRow, sRowColumn.TotalCuttingTimeColumn] = sDB_TEMain.comTEMain.totalCuttingTime;
                    workRange[sRowColumn.PartDescRow, sRowColumn.PartDescColumn] = "OIS-" + sDB_TEMain.ncGroupName.Split('P')[1];
                    //加入 機台型號.設計.審核.批准
                    workRange[sRowColumn.MachineNoRow, sRowColumn.MachineNoColumn] = sDB_TEMain.comTEMain.machineNo;
                    workRange[sRowColumn.DesignedRow, sRowColumn.DesignedColumn] = sDB_TEMain.comTEMain.designed;
                    workRange[sRowColumn.ReviewedRow, sRowColumn.ReviewedColumn] = sDB_TEMain.comTEMain.reviewed;
                    workRange[sRowColumn.ApprovedRow, sRowColumn.ApprovedColumn] = sDB_TEMain.comTEMain.approved;


                    OperImgPosiSize sImgPosiSize = new OperImgPosiSize();
                    GetOperImgPosiAndSize(i, out sImgPosiSize);
                    if (File.Exists(cCom_ShopDoc[i].opImagePath))
                    {
                        workSheet.Shapes.AddPicture(cCom_ShopDoc[i].opImagePath, Microsoft.Office.Core.MsoTriState.msoFalse,
                                   Microsoft.Office.Core.MsoTriState.msoTrue, sImgPosiSize.OperPosiLeft,
                                   sImgPosiSize.OperPosiTop, sImgPosiSize.OperImgWidth, sImgPosiSize.OperImgHeight);
                    }
                }
           
                //加入控制尺寸資訊(未開發)
                ISession session = MyHibernateHelper.SessionFactory.OpenSession();
                IList<Com_ControlDimen> ListComControlDimen = session.QueryOver<Com_ControlDimen>()
                                                                .Where(x => x.comTEMain == sDB_TEMain.comTEMain).List();
                int j = 0,initialControlDimenRow = 39, ControlDimenToolNoColumn = 9, ControlDimenBalloonColumn = 10, ControlDimenColumn = 11;
                string toolNo = "";
                foreach (Com_ControlDimen i in ListComControlDimen)
                {
                    workSheet = (Worksheet)workBook.Sheets[1 + j];
                    workRange = (Range)workSheet.Cells;

                    if (toolNo == "" || toolNo != i.toolNo)
                    {
                        workRange[initialControlDimenRow, ControlDimenToolNoColumn] = i.toolNo;
                        toolNo = i.toolNo;
                    }
                    workRange[initialControlDimenRow, ControlDimenBalloonColumn] = i.controlBallon;
                    workRange[initialControlDimenRow, ControlDimenColumn] = i.controlDimen;
                    initialControlDimenRow++;
                    if (initialControlDimenRow == 47)
                    {
                        j++;
                    }
                }
          

                //加入治具圖片
                if (sDB_TEMain.comTEMain.fixtureImgPath != "")
                {
                    FixImgPosiSize sFixImgPosiSize = new FixImgPosiSize();
                    GetFixImgPosiAndSize(out sFixImgPosiSize);
                    for (int i = 0; i < workBook.Sheets.Count; i++)
                    {
                        workSheet = (Worksheet)workBook.Sheets[i + 1];

                        if (File.Exists(sDB_TEMain.comTEMain.fixtureImgPath))
                        {
                            workSheet.Shapes.AddPicture(sDB_TEMain.comTEMain.fixtureImgPath, Microsoft.Office.Core.MsoTriState.msoFalse,
                            Microsoft.Office.Core.MsoTriState.msoTrue, (float)(abc+3),
                            (float)(de+5), sFixImgPosiSize.FixImgWidth, sFixImgPosiSize.FixImgHeight);
                        }
                    }
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
