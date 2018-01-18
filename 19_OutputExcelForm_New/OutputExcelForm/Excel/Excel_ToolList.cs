using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using Microsoft.Office.Interop.Excel;
using System.Windows.Forms;
using System.IO;
using CaxGlobaltek;

namespace OutputExcelForm.Excel
{
    public class Excel_ToolList
    {
        private static bool status;
        private static string OutputPath = "";
        public struct RowColumn
        {
            //刀號
            public int ToolNumberRow { get; set; }
            public int ToolNumberColumn { get; set; }
            //ERP
            public int ERPNumberRow { get; set; }
            public int ERPNumberColumn { get; set; }
            //刀具用量
            public int CutterQtyRow { get; set; }
            public int CutterQtyColumn { get; set; }
            //刀具壽命
            public int CutterLifeRow { get; set; }
            public int CutterLifeColumn { get; set; }
            //可用刃數
            public int FluteQtyRow { get; set; }
            public int FluteQtyColumn { get; set; }
            //刀具品名
            public int TitleRow { get; set; }
            public int TitleColumn { get; set; }
            //規格
            public int SpecificationRow { get; set; }
            public int SpecificationColumn { get; set; }
            //備註
            public int NoteRow { get; set; }
            public int NoteColumn { get; set; }
            //刀具名稱
            //public int ToolNameRow { get; set; }
            //public int ToolNameColumn { get; set; }
            //程式名稱
            //public int OperNameRow { get; set; }
            //public int OperNameColumn { get; set; }
            //刀柄名稱
            public int HolderRow { get; set; }
            public int HolderColumn { get; set; }
            //加工時間
            //public int CuttingTimeRow { get; set; }
            //public int CuttingTimeColumn { get; set; }
            //進給
            //public int ToolFeedRow { get; set; }
            //public int ToolFeedColumn { get; set; }
            //速度
            //public int ToolSpeedRow { get; set; }
            //public int ToolSpeedColumn { get; set; }
            //加工路徑圖片
            //public int OperImgToolRow { get; set; }
            //public int OperImgToolColumn { get; set; }
            //留料
            //public int PartStockRow { get; set; }
            //public int PartStockColumn { get; set; }
            //循環時間
            //public int TotalCuttingTimeRow { get; set; }
            //public int TotalCuttingTimeColumn { get; set; }
            //料號
            public int PartNoRow { get; set; }
            public int PartNoColumn { get; set; }
            //品名
            public int PartDescRow { get; set; }
            public int PartDescColumn { get; set; }
            //機台型號
            //public int MachineNoRow { get; set; }
            //public int MachineNoColumn { get; set; }
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
        public static void GetExcelRowColumn(int i, out RowColumn sRowColumn)
        {
            sRowColumn = new RowColumn();
            //料號
            sRowColumn.PartNoRow = 52;
            sRowColumn.PartNoColumn = 10;
            //品名
            sRowColumn.PartDescRow = 51;
            sRowColumn.PartDescColumn = 10;
            //設計
            sRowColumn.DesignedRow = 52;
            sRowColumn.DesignedColumn = 5;
            //審核
            sRowColumn.ReviewedRow = 52;
            sRowColumn.ReviewedColumn = 6;
            //批准
            sRowColumn.ApprovedRow = 52;
            sRowColumn.ApprovedColumn = 7;


            int currentNo = (i % 40);
            int RowNo = 7;

            RowNo = RowNo + currentNo;
            
            sRowColumn.ToolNumberRow = RowNo;
            sRowColumn.ToolNumberColumn = 1;

            sRowColumn.ERPNumberRow = RowNo;
            sRowColumn.ERPNumberColumn = 2;

            sRowColumn.CutterQtyRow = RowNo;
            sRowColumn.CutterQtyColumn = 3;

            sRowColumn.CutterLifeRow = RowNo;
            sRowColumn.CutterLifeColumn = 4;

            sRowColumn.FluteQtyRow = RowNo;
            sRowColumn.FluteQtyColumn = 5;

            sRowColumn.TitleRow = RowNo;
            sRowColumn.TitleColumn = 6;

            sRowColumn.SpecificationRow = RowNo;
            sRowColumn.SpecificationColumn = 8;

            sRowColumn.NoteRow = RowNo;
            sRowColumn.NoteColumn = 10;

            sRowColumn.HolderRow = RowNo;
            sRowColumn.HolderColumn = 12;
        }
        public static bool CreateToolListExcel_XinWu(string cusName, string partNo, string cusVer, string opVer, string op1, DB_TEMain sDB_TEMain, IList<Com_ToolList> cCom_ToolList)
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
                                                    , sDB_TEMain.comTEMain.ncGroupName + "_" + "ToolList" + ".xls");



                //1.開啟Excel
                //2.將Excel設為不顯示
                //3.取得第一頁Sheet
                workBook = (Workbook)excelApp.Workbooks.Open(sDB_TEMain.excelTemplateFilePath);
                excelApp.Visible = false;
                workSheet = (Worksheet)workBook.Sheets[1];

                //建立Sheet頁數符合所有的Operation
                //之後應該要改成同一頁然後一直新增ROW
                status = Excel_CommonFun.AddNewSheet(cCom_ToolList.Count, 40, excelApp, workSheet);
                if (!status)
                {
                    MessageBox.Show("建立Sheet頁失敗，請聯繫開發工程師");
                    return false;
                }

                //修改每一個Sheet名字和頁數
                status = Excel_CommonFun.ModifySheet(partNo, "ToolList", workBook, workSheet, workRange, sDB_TEMain.factory);
                if (!status)
                {
                    MessageBox.Show("修改Sheet名字和頁數失敗，請聯繫開發工程師");
                    return false;
                }

                //開始填表
                int
                    CurrentRow = 6,
                    ToolNumberColumn = 1,
                    ToolERPColumn = 2,
                    ToolCutterQtyColumn = 3,
                    ToolCutterLifeColumn = 4,
                    ToolFluteQtyColumn = 5,
                    ToolTitleColumn = 6,
                    ToolSpecColumn = 8,
                    ToolNoteColumn = 10;
                RowColumn sRowColumn; int StartRow = 0, EndRow = 0;
                for (int i = 0; i < cCom_ToolList.Count; i++)
                {
                    //GetExcelRowColumn(i, out sRowColumn);
                    //取得當前Operation該放置的Sheet
                    int currentSheet_Value = (i / 40);
                    if (currentSheet_Value == 0)
                    {
                        workSheet = (Worksheet)workBook.Sheets[1];
                    }
                    else
                    {
                        workSheet = (Worksheet)workBook.Sheets[currentSheet_Value + 1];
                    }
                    CurrentRow = CurrentRow + 1;
                    StartRow = CurrentRow;
                    workRange = (Range)workSheet.Cells;
                    workRange[CurrentRow, ToolNumberColumn] = cCom_ToolList[i].toolNumber;
                    workRange[CurrentRow, ToolERPColumn] = cCom_ToolList[i].erpNumber;
                    workRange[CurrentRow, ToolCutterQtyColumn] = cCom_ToolList[i].cutterQty;
                    workRange[CurrentRow, ToolCutterLifeColumn] = cCom_ToolList[i].cutterLife;
                    workRange[CurrentRow, ToolFluteQtyColumn] = cCom_ToolList[i].fluteQty;
                    workRange[CurrentRow, ToolTitleColumn] = cCom_ToolList[i].title;
                    workRange[CurrentRow, ToolSpecColumn] = cCom_ToolList[i].specification;
                    workRange[CurrentRow, ToolNoteColumn] = cCom_ToolList[i].note;

                    if (cCom_ToolList[i].accessory != "")
                    {
                        string[] SplitAccessory = cCom_ToolList[i].accessory.Split('?');
                        foreach (string j in SplitAccessory)
                        {
                            CurrentRow = CurrentRow + 1;
                            string[] Spliti = j.Split('!');
                            workRange[CurrentRow, ToolERPColumn] = Spliti[0];
                            workRange[CurrentRow, ToolTitleColumn] = Spliti[1];
                            workRange[CurrentRow, ToolSpecColumn] = Spliti[2];
                        }
                    }
                    EndRow = CurrentRow;
                    //合併儲存格
                    workSheet.get_Range("A" + StartRow, "A" + EndRow).Merge(false);
                    
                }
                //加入 料號.品名.設計.審核.批准
                workRange[52, 10] = partNo;
                workRange[51, 10] = "OIS-" + sDB_TEMain.ncGroupName.Split('P')[1].Split('_')[0];
                workRange[52, 5] = sDB_TEMain.comTEMain.designed;
                workRange[52, 6] = sDB_TEMain.comTEMain.reviewed;
                workRange[52, 7] = sDB_TEMain.comTEMain.approved;

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
