using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Windows.Forms;
using System.IO;
using DevComponents.DotNetBar.SuperGrid;
using System.Collections;
using CaxGlobaltek;
using OutputExcelForm.Excel;
using System.Drawing;
using System.Drawing.Drawing2D;

namespace OutputExcelForm
{

    public class GetExcelForm
    {
        
        private static bool status;
        private static string ServerEnvVari = "Globaltek_Server_Env";
        //字體初始化設定
        public static Font myFont = new System.Drawing.Font("新細明體", 9);
        public static Brush myBrush = new SolidBrush(System.Drawing.Color.Blue);
        public static StringFormat stringFormat = new StringFormat();
        public struct Point8
        {
            public int LeftUpPointX { get; set; }
            public int LeftUpPointY { get; set; }
            public int UpPointX { get; set; }
            public int UpPointY { get; set; }
            public int RightUpPointX { get; set; }
            public int RightUpPointY { get; set; }
            public int LeftPointX { get; set; }
            public int LeftPointY { get; set; }
            public int RightPointX { get; set; }
            public int RightPointY { get; set; }
            public int LeftDownPointX { get; set; }
            public int LeftDownPointY { get; set; }
            public int DownPointX { get; set; }
            public int DownPointY { get; set; }
            public int RightDownPointX { get; set; }
            public int RightDownPointY { get; set; }
        }
        public struct ArrowPoint
        {
            public int StartPointX { get; set; }
            public int StartPointY { get; set; }
            public int EndPointX { get; set; }
            public int EndPointY { get; set; }
        }




        public static bool GetMEExcelForm(string ExcelType, out List<string> ExcelData)
        {
            ExcelData = new List<string>();
            try
            {
                //取得Server->ME_Config->Config內的ExcelForm資料
                //string serverMEConfig = string.Format(@"{0}\{1}\{2}\{3}", OutputForm.EnvVariables.env, "ME_Config", "Config", ExcelType);
                string serverMEConfig = string.Format(@"{0}\{1}\{2}\{3}", CaxEnv.GetGlobaltekEnvDir(), "ME_Config", "Config", ExcelType);
                if (!Directory.Exists(serverMEConfig))
                {
                    MessageBox.Show(string.Format(@"{0}{1}{2}", "路徑：", serverMEConfig, "不存在，請確定ServerIP是否正確"));
                    return false;
                }
                string[] FolderFile = System.IO.Directory.GetFileSystemEntries(serverMEConfig, "*.xls");
                //List<string> ExcelData = new List<string>();
                for (int i = 0; i < FolderFile.Length;i++ )
                {
                    ExcelData.Add(Path.GetFileNameWithoutExtension(FolderFile[i]));
                }
                //foreach (string i in FolderFile)
                //{
                //    ExcelData.Add(Path.GetFileNameWithoutExtension(i));
                //}
                //ExcelComboBox ExcelData1 = new ExcelComboBox(ExcelData.ToArray());
                
            }
            catch (System.Exception ex)
            {
                MessageBox.Show(ex.ToString());
                return false;
            }
            return true;
        }
        public static bool GetTEExcelForm(string ExcelType, out List<string> ExcelData)
        {
            ExcelData = new List<string>();
            try
            {
                //取得Server->TE_Config->Config內的ExcelForm資料
                //string serverTEConfig = string.Format(@"{0}\{1}\{2}\{3}", OutputForm.EnvVariables.env, "TE_Config", "Config", ExcelType);
                string serverTEConfig = string.Format(@"{0}\{1}\{2}\{3}", CaxEnv.GetGlobaltekEnvDir(), "TE_Config", "Config", ExcelType);
                if (!Directory.Exists(serverTEConfig))
                {
                    MessageBox.Show(string.Format(@"{0}{1}{2}", "路徑：", serverTEConfig, "不存在，請確定ServerIP是否正確"));
                    return false;
                }
                string[] FolderFile = System.IO.Directory.GetFileSystemEntries(serverTEConfig, "*.xls");
                //List<string> ExcelData = new List<string>();
                for (int i = 0; i < FolderFile.Length; i++)
                {
                    ExcelData.Add(Path.GetFileNameWithoutExtension(FolderFile[i]));
                }
            }
            catch (System.Exception ex)
            {
                return false;
            }
            return true;
        }
        public static bool GetToolListExcelForm(string ExcelType, out List<string> ExcelData)
        {
            ExcelData = new List<string>();
            try
            {
                //取得Server->TE_Config->Config內的ExcelForm資料
                //string serverTEConfig = string.Format(@"{0}\{1}\{2}\{3}", OutputForm.EnvVariables.env, "TE_Config", "Config", ExcelType);
                string serverTEConfig = string.Format(@"{0}\{1}\{2}\{3}", CaxEnv.GetGlobaltekEnvDir(), "TE_Config", "Config", ExcelType);
                if (!Directory.Exists(serverTEConfig))
                {
                    MessageBox.Show(string.Format(@"{0}{1}{2}", "路徑：", serverTEConfig, "不存在，請確定ServerIP是否正確"));
                    return false;
                }
                string[] FolderFile = System.IO.Directory.GetFileSystemEntries(serverTEConfig, "*.xls");
                //List<string> ExcelData = new List<string>();
                for (int i = 0; i < FolderFile.Length; i++)
                {
                    ExcelData.Add(Path.GetFileNameWithoutExtension(FolderFile[i]));
                }
            }
            catch (System.Exception ex)
            {
                return false;
            }
            return true;
        }
        public static bool InsertDataToMEExcel(Dictionary<DB_MEMain, IList<Com_Dimension>> DicDimensionData, string cusName, string partNo, string cusVer, string opVer, string op1)
        {
            try
            {
                foreach (KeyValuePair<DB_MEMain, IList<Com_Dimension>> kvp in DicDimensionData)
                {
                    if (kvp.Key.excelType == "FAI")
                    {
                        #region FAI
                        if (kvp.Key.factory == "XinWu_FAI")
                        {
                            status = Excel_FAI.CreateFAIExcel_XinWu(cusName, partNo, cusVer, opVer, op1, kvp.Key, kvp.Value);
                            if (!status)
                            {
                                MessageBox.Show("輸出XinWu_FAI失敗，請聯繫開發工程師");
                                return false;
                            }
                        }
                        else if (kvp.Key.factory == "WuXi_FAI")
                        {

                        }
                        else if (kvp.Key.factory == "XiAn_FAI")
                        {
                        }
                        #endregion
                    }
                    else if (kvp.Key.excelType == "FQC")
                    {
                        #region FQC
                        if (kvp.Key.factory == "XinWu_FQC")
                        {
                            status = Excel_FQC.CreateFQCExcel_XinWu(cusName, partNo, cusVer, opVer, op1, kvp.Key, kvp.Value);
                            if (!status)
                            {
                                MessageBox.Show("輸出XinWu_FQC失敗，請聯繫開發工程師");
                                return false;
                            }
                        }
                        else if (kvp.Key.factory == "WuXi_FQC")
                        {

                        }
                        else if (kvp.Key.factory == "XiAn_FQC")
                        {
                            status = Excel_FQC.CreateFQCExcel_XiAn(cusName, partNo, cusVer, opVer, op1, kvp.Key, kvp.Value);
                            if (!status)
                            {
                                MessageBox.Show("輸出XiAn_FQC失敗，請聯繫開發工程師");
                                return false;
                            }
                        }
                        #endregion
                    }
                    else if (kvp.Key.excelType == "IQC")
                    {
                        #region IQC
                        if (kvp.Key.factory == "XinWu_IQC")
                        {
                            status = Excel_IQC.CreateIQCExcel_XinWu(cusName, partNo, cusVer, opVer, op1, kvp.Key, kvp.Value);
                            if (!status)
                            {
                                MessageBox.Show("輸出XinWu_IQC失敗，請聯繫開發工程師");
                                return false;
                            }
                        }
                        else if (kvp.Key.factory == "WuXi_IQC")
                        {

                        }
                        else if (kvp.Key.factory == "XiAn_IQC")
                        {
                        }
                        #endregion
                    }
                    else if (kvp.Key.excelType == "IPQC")
                    {
                        #region IPQC
                        if (kvp.Key.factory == "XinWu_IPQC")
                        {
                            status = Excel_IPQC.CreateIPQCExcel_XinWu(cusName, partNo, cusVer, opVer, op1, kvp.Key, kvp.Value);
                            if (!status)
                            {
                                MessageBox.Show("輸出XinWu_IPQC失敗，請聯繫開發工程師");
                                return false;
                            }
                        }
                        else if (kvp.Key.factory == "WuXi_IPQC")
                        {

                        }
                        else if (kvp.Key.factory == "XiAn_IPQC")
                        {
                            status = Excel_IPQC.CreateIPQCExcel_XiAn(cusName, partNo, cusVer, opVer, op1, kvp.Key, kvp.Value);
                            if (!status)
                            {
                                MessageBox.Show("輸出XiAn_IPQC失敗，請聯繫開發工程師");
                                return false;
                            }
                        }
                        #endregion
                    }
                    else if (kvp.Key.excelType == "SelfCheck")
                    {
                        #region SelfCheck
                        if (kvp.Key.factory == "XinWu_SelfCheck")
                        {
                            status = Excel_SelfCheck.CreateSelfCheckExcel_XinWu(cusName, partNo, cusVer, opVer, op1, kvp.Key, kvp.Value);
                            if (!status)
                            {
                                MessageBox.Show("輸出XinWu_SelfCheck失敗，請聯繫開發工程師");
                                return false;
                            }
                        }
                        else if (kvp.Key.factory == "WuXi_SelfCheck")
                        {

                        }
                        else if (kvp.Key.factory == "XiAn_SelfCheck")
                        {
                            status = Excel_SelfCheck.CreateSelfCheckExcel_XiAn(cusName, partNo, cusVer, opVer, op1, kvp.Key, kvp.Value);
                            if (!status)
                            {
                                MessageBox.Show("輸出XiAn_SelfCheck失敗，請聯繫開發工程師");
                                return false;
                            }
                        }
                        #endregion
                    }
                }
            }
            catch (System.Exception ex)
            {
                MessageBox.Show(ex.ToString());
                return false;
            }
            return true;
        }
        public static bool InsertDataToShopDocExcel(Dictionary<DB_TEMain, IList<Com_ShopDoc>> DicShopDocData, string cusName, string partNo, string cusVer, string opVer, string op1)
        {
            try
            {
                foreach (KeyValuePair<DB_TEMain, IList<Com_ShopDoc>> kvp in DicShopDocData)
                {
                    if (kvp.Key.comTEMain.sysTEExcel.teExcelType == "ShopDoc")
                    {
                        if (kvp.Key.factory == "XinWu_ShopDoc")
                        {
                            status = Excel_ShopDoc.CreateShopDocExcel_XinWu(cusName, partNo, cusVer, opVer, op1, kvp.Key, kvp.Value);
                            if (!status)
                            {
                                MessageBox.Show("輸出XinWu_ShopDoc失敗，請聯繫開發工程師");
                                return false;
                            }
                        }
                    }
                }
            }
            catch (System.Exception ex)
            {
                return false;
            }
            return true;
        }
        public static bool InsertDataToTLExcel(Dictionary<DB_TEMain, IList<Com_ToolList>> DicToolListData, string cusName, string partNo, string cusVer, string opVer, string op1)
        {
            try
            {
                foreach (KeyValuePair<DB_TEMain, IList<Com_ToolList>> kvp in DicToolListData)
                {
                    if (kvp.Key.factory == "XinWu_ToolList")
                    {
                        status = Excel_ToolList.CreateToolListExcel_XinWu(cusName, partNo, cusVer, opVer, op1, kvp.Key, kvp.Value);
                        if (!status)
                        {
                            MessageBox.Show("輸出XinWu_ShopDoc失敗，請聯繫開發工程師");
                            return false;
                        }
                    }
                }
            }
            catch (System.Exception ex)
            {
                return false;
            }
            return true;
        }
        public static bool InsertDataToPFDExcel(string partNo, Dictionary<DB_PEMain, List<DB_PartOperation>> DicPFDData)
        {
            try
            {
                if (DicPFDData.Count == 0)
                    return true;

                //製作每個OP的圖片
                //status = CreateOpImg(partNo, ref DicPFDData);
                //if (!status)
                //    return false;
                

                //插入Excel
                foreach (KeyValuePair<DB_PEMain, List<DB_PartOperation>> kvp in DicPFDData)
                {
                    //status = Excel_PFD.CreatePFDExcel(kvp.Key, kvp.Value);
                    //if (!status)
                    //{
                    //    MessageBox.Show("輸出PFD失敗，請聯繫開發工程師");
                    //    return false;
                    //}
                    status = Excel_PFD.NewCreatePFDExcel(kvp.Key, kvp.Value);
                    if (!status)
                    {
                        MessageBox.Show("輸出PFD失敗，請聯繫開發工程師");
                        return false;
                    }
                }
                
            }
            catch (System.Exception ex)
            {
                return false;
            }
            return true;
        }
        public static bool InsertDataToCPExcel(Dictionary<DB_CPKey, List<DB_CPValue>> DicCPData)
        {
            try
            {
                foreach (KeyValuePair<DB_CPKey, List<DB_CPValue>> kvp in DicCPData)
                {
                    status = Excel_ControlPlan.CreateCPExcel(kvp.Key, kvp.Value);
                    if (!status)
                    {
                        MessageBox.Show("輸出CP失敗，請聯繫開發工程師");
                        return false;
                    }
                }
                
            }
            catch (System.Exception ex)
            {
                return false;
            }
            return true;
        }
        public static bool InsertDataToFixInsExcel(Dictionary<DB_FixInspection, IList<Com_FixDimension>> DicFixDimensionData, string cusName, string partNo, string cusVer, string opVer, string op1)
        {
            try
            {
                if (DicFixDimensionData.Count == 0)
                    return true;
                foreach (KeyValuePair<DB_FixInspection, IList<Com_FixDimension>> kvp in DicFixDimensionData)
                {
                    status = Excel_FixtureInspection.CreateFixInsExcel(cusName, partNo, cusVer, opVer, op1, kvp.Key, kvp.Value);
                    if (!status)
                    {
                        MessageBox.Show("輸出FixInsExcel失敗，請聯繫開發工程師");
                        return false;
                    }
                }

            }
            catch (System.Exception ex)
            {
                return false;
            }
            return true;
        }
        public static bool CreateOpImg(string partNo, ref Dictionary<DB_PEMain, List<DB_PartOperation>> DicPFDData)
        {
            try
            {
                string imgPath = "";
                Bitmap bitmap = null;
                Graphics graphicsObj;

                DB_PEMain sDB_PEMain = new DB_PEMain();
                List<DB_PartOperation> ListDB_PartOperation_OLD = new List<DB_PartOperation>();
                List<DB_PartOperation> ListDB_PartOperation_New = new List<DB_PartOperation>();
                DB_PartOperation sDB_PartOperation = new DB_PartOperation();
                for (int i = 0; i < OutputForm.PEPanel.Rows.Count; i++)
                {
                    if (((bool)OutputForm.PEPanel.GetCell(i, 0).Value) == false || OutputForm.PEPanel.GetCell(i,1).Value.ToString() != "PFD")
                        continue;

                    sDB_PEMain.PartNo = partNo;
                    sDB_PEMain.CusVer = OutputForm.PEPanel.GetCell(i, 2).Value.ToString();
                    sDB_PEMain.OpVer = OutputForm.PEPanel.GetCell(i, 3).Value.ToString();
                    //sDB_PEMain.excelTemplateFilePath = string.Format(@"{0}\{1}\{2}\{3}\{4}\{5}\{6}.xls"
                    //                                                    , "\\\\192.168.35.1"
                    //                                                    , "cax"
                    //                                                    , "Globaltek"
                    //                                                    , "PE_Config"
                    //                                                    , "Config"
                    //                                                    , "PFD"
                    //                                                    , "PFD");
                    sDB_PEMain.excelTemplateFilePath = string.Format(@"{0}\{1}\{2}\{3}\{4}.xls"
                                                                        , CaxEnv.GetGlobaltekEnvDir()
                                                                        , "PE_Config"
                                                                        , "Config"
                                                                        , "PFD"
                                                                        , "PFD");
                    //sDB_PEMain.excelTemplateFilePath = string.Format(@"{0}\{1}\{2}\{3}\{4}.xls"
                    //                                                    , OutputForm.EnvVariables.env
                    //                                                    , "PE_Config"
                    //                                                    , "Config"
                    //                                                    , "PFD"
                    //                                                    , "PFD");
                    
                    
                    //foreach (KeyValuePair<DB_PEMain, List<DB_PartOperation>> kvp in DicPFDData)
                    //{
                    //    if (kvp.Key.PartNo == sDB_PEMain.PartNo)
                    //    {
                    //        MessageBox.Show("料號一樣");
                    //    }
                    //    if (kvp.Key.CusVer == sDB_PEMain.CusVer)
                    //    {
                    //        MessageBox.Show("客版一樣");
                    //    }
                    //    if (kvp.Key.OpVer == sDB_PEMain.OpVer)
                    //    {
                    //        MessageBox.Show("製版一樣");
                    //    }
                    //    if (kvp.Key.excelTemplateFilePath == sDB_PEMain.excelTemplateFilePath)
                    //    {
                    //        MessageBox.Show("PFD一樣");
                    //    }

                    //}
                    

                    status = DicPFDData.TryGetValue(sDB_PEMain, out ListDB_PartOperation_OLD);
                    if (!status)
                        return false;


                    //製作C
                    if (ListDB_PartOperation_OLD.Count > 6)
                    {
                        #region C1
                        bitmap = new Bitmap(155, 21);
                        graphicsObj = Graphics.FromImage(bitmap);
                        //畫圓
                        Point8 sPoint8 = new Point8();
                        int startX = 90;
                        int startY = 0;
                        status = DrawCircle(graphicsObj, startX, startY, 10, out sPoint8);
                        if (!status)
                            return false;

                        //寫A
                        startX = 95;
                        startY = 5;
                        status = WriteString(graphicsObj, startX, startY, "C", 8);
                        if (!status)
                            return false;

                        //存成圖片
                        imgPath = string.Format(@"{0}\{1}_{2}_{3}\{4}_{5}_{6}.Jpeg"
                                                    , Environment.GetFolderPath(Environment.SpecialFolder.Desktop)
                                                    , sDB_PEMain.PartNo
                                                    , sDB_PEMain.CusVer
                                                    , sDB_PEMain.OpVer
                                                    , "C1"
                                                    , 155
                                                    , 21);
                        bitmap.Save(imgPath);
                        #endregion


                        #region C2
                        bitmap = new Bitmap(155, 46);
                        graphicsObj = Graphics.FromImage(bitmap);
                        //畫圓
                        startX = 90;
                        startY = 0;
                        status = DrawCircle(graphicsObj, startX, startY, 10, out sPoint8);
                        if (!status)
                            return false;

                        //寫C
                        startX = 95;
                        startY = 5;
                        status = WriteString(graphicsObj, startX, startY, "C", 8);
                        if (!status)
                            return false;

                        //向下箭頭
                        startX = sPoint8.DownPointX;
                        startY = sPoint8.DownPointY;
                        ArrowPoint sArrowPoint = new ArrowPoint();
                        status = DrawDownArrow(graphicsObj, startX, startY, 20, out sArrowPoint);
                        if (!status)
                            return false;

                        //存成圖片
                        imgPath = string.Format(@"{0}\{1}_{2}_{3}\{4}_{5}_{6}.Jpeg"
                                                    , Environment.GetFolderPath(Environment.SpecialFolder.Desktop)
                                                    , sDB_PEMain.PartNo
                                                    , sDB_PEMain.CusVer
                                                    , sDB_PEMain.OpVer
                                                    , "C2"
                                                    , 155
                                                    , 46);
                        bitmap.Save(imgPath);
                        #endregion
                    }

                    foreach (DB_PartOperation j in ListDB_PartOperation_OLD)
                    {
                        
                        if (j.Op2 == "Blank" || j.Op2 == "Forging" || j.Op2 == "Anodize")
                        {
                            #region Blank

                            bitmap = new Bitmap(155, 131);
                            graphicsObj = Graphics.FromImage(bitmap);

                            //畫倒三角
                            Point8 sPoint8 = new Point8();
                            status = DrawReverseTriangle(graphicsObj, out sPoint8);
                            if (!status)
                                return false;

                            //畫透明方框
                            int rectangleWidth = sPoint8.RightPointX - sPoint8.LeftUpPointX;
                            int rectangleheight = sPoint8.DownPointY - sPoint8.UpPointY;
                            status = DrawRectangle(graphicsObj, Color.Transparent, sPoint8.LeftUpPointX, sPoint8.LeftUpPointY, rectangleWidth, rectangleheight, j.Op1 + "\n" + j.Op2, out sPoint8);
                            if (!status)
                                return false;

                            //向下箭頭
                            int startX = sPoint8.DownPointX;
                            int startY = sPoint8.DownPointY;
                            ArrowPoint sArrowPoint = new ArrowPoint();
                            status = DrawDownArrow(graphicsObj, startX, startY, 20, out sArrowPoint);
                            if (!status)
                                return false;

                            //畫菱形
                            startX = sArrowPoint.EndPointX;
                            startY = sArrowPoint.EndPointY;
                            status = DrawDiamond(graphicsObj, startX, startY, 40, 20, out sPoint8);
                            if (!status)
                                return false;

                            //畫透明方框
                            startX = sPoint8.LeftUpPointX;
                            startY = sPoint8.LeftUpPointY;
                            rectangleWidth = sPoint8.RightPointX - sPoint8.LeftUpPointX;
                            rectangleheight = sPoint8.DownPointY - sPoint8.UpPointY;
                            status = DrawRectangle(graphicsObj, Color.Transparent, startX, startY, rectangleWidth, rectangleheight, "IQC", out sPoint8);
                            if (!status)
                                return false;

                            //向下箭頭
                            startX = sPoint8.DownPointX;
                            startY = sPoint8.DownPointY;
                            status = DrawDownArrow(graphicsObj, startX, startY, 20, out sArrowPoint);
                            if (!status)
                                return false;

                            //寫OK
                            startX = sArrowPoint.StartPointX + 3;
                            startY = sArrowPoint.StartPointY + 3;
                            status = WriteString(graphicsObj, startX, startY, "OK", 7);
                            if (!status)
                                return false;

                            //向左箭頭
                            startX = sPoint8.LeftPointX;
                            startY = sPoint8.LeftPointY;
                            status = DrawLeftArrow(graphicsObj, startX, startY, 20, out sArrowPoint);
                            if (!status)
                                return false;

                            //寫NG
                            startX = sArrowPoint.StartPointX - 15;
                            startY = sArrowPoint.StartPointY - 10;
                            status = WriteString(graphicsObj, startX, startY, "NG", 7);
                            if (!status)
                                return false;

                            //畫圓
                            startX = sArrowPoint.EndPointX - 20;
                            startY = sArrowPoint.EndPointY - 10;
                            status = DrawCircle(graphicsObj, startX, startY, 10, out sPoint8);
                            if (!status)
                                return false;

                            //寫A
                            startX = sArrowPoint.EndPointX - 15;
                            startY = sArrowPoint.EndPointY - 5;
                            status = WriteString(graphicsObj, startX, startY, "A", 8);
                            if (!status)
                                return false;

                            //畫A下面的線
                            startX = sPoint8.DownPointX;
                            startY = sPoint8.DownPointY;
                            status = DrawDownLine(graphicsObj, startX, startY, 20, out sArrowPoint);
                            if (!status)
                                return false;

                            //寫OK
                            startX = sArrowPoint.StartPointX + 5;
                            startY = sArrowPoint.StartPointY + 2;
                            status = WriteString(graphicsObj, startX, startY, "OK", 7);
                            if (!status)
                                return false;

                            //向右箭頭
                            startX = sArrowPoint.EndPointX;
                            startY = sArrowPoint.EndPointY;
                            status = DrawRightArrow(graphicsObj, startX, startY, 69, out sArrowPoint);
                            if (!status)
                                return false;

                            //存成圖片
                            imgPath = string.Format(@"{0}\{1}_{2}_{3}\{4}_{5}_{6}.Jpeg"
                                                        , Environment.GetFolderPath(Environment.SpecialFolder.Desktop)
                                                        , sDB_PEMain.PartNo
                                                        , sDB_PEMain.CusVer
                                                        , sDB_PEMain.OpVer
                                                        , j.Op1
                                                        , 155
                                                        , 131);
                            bitmap.Save(imgPath);
                            sDB_PartOperation.Op1 = j.Op1;
                            sDB_PartOperation.Op2 = j.Op2;
                            sDB_PartOperation.ImgPath = imgPath;
                            ListDB_PartOperation_New.Add(sDB_PartOperation);
                            #endregion
                        }
                        else if (j.Op2 == "Appearance check & Packaging")
                        {
                            #region Appearance check & Packaging

                            bitmap = new Bitmap(155, 66);
                            graphicsObj = Graphics.FromImage(bitmap);

                            //畫方框
                            Point8 sPoint8 = new Point8();
                            status = DrawRectangle(graphicsObj, Color.Black, 50, 0, 100, 40, j.Op1 + "\n" + j.Op2, out sPoint8);
                            if (!status)
                                return false;

                            //向下箭頭
                            ArrowPoint sArrowPoint = new ArrowPoint();
                            int startX = sPoint8.DownPointX;
                            int startY = sPoint8.DownPointY;
                            status = DrawDownArrow(graphicsObj, startX, startY, 20, out sArrowPoint);
                            if (!status)
                                return false;

                            //寫OK
                            startX = sArrowPoint.StartPointX + 3;
                            startY = sArrowPoint.StartPointY + 3;
                            status = WriteString(graphicsObj, startX, startY, "OK", 7);
                            if (!status)
                                return false;

                            //向左箭頭
                            startX = sPoint8.LeftPointX;
                            startY = sPoint8.LeftPointY;
                            status = DrawLeftArrow(graphicsObj, startX, startY, 20, out sArrowPoint);
                            if (!status)
                                return false;

                            //寫NG
                            startX = sArrowPoint.StartPointX - 15;
                            startY = sArrowPoint.StartPointY - 10;
                            status = WriteString(graphicsObj, startX, startY, "NG", 7);
                            if (!status)
                                return false;

                            //畫圓
                            startX = sArrowPoint.EndPointX - 20;
                            startY = sArrowPoint.EndPointY - 10;
                            status = DrawCircle(graphicsObj, startX, startY, 10, out sPoint8);
                            if (!status)
                                return false;

                            //寫A
                            startX = sArrowPoint.EndPointX - 15;
                            startY = sArrowPoint.EndPointY - 5;
                            status = WriteString(graphicsObj, startX, startY, "A", 8);
                            if (!status)
                                return false;

                            //畫A下面的線
                            startX = sPoint8.DownPointX;
                            startY = sPoint8.DownPointY;
                            status = DrawDownLine(graphicsObj, startX, startY, 20, out sArrowPoint);
                            if (!status)
                                return false;

                            //寫OK
                            startX = sArrowPoint.StartPointX + 5;
                            startY = sArrowPoint.StartPointY + 2;
                            status = WriteString(graphicsObj, startX, startY, "OK", 7);
                            if (!status)
                                return false;

                            //向右箭頭
                            startX = sArrowPoint.EndPointX;
                            startY = sArrowPoint.EndPointY;
                            status = DrawRightArrow(graphicsObj, startX, startY, 77, out sArrowPoint);
                            if (!status)
                                return false;

                            //存成圖片
                            imgPath = string.Format(@"{0}\{1}_{2}_{3}\{4}_{5}_{6}.Jpeg"
                                                        , Environment.GetFolderPath(Environment.SpecialFolder.Desktop)
                                                        , sDB_PEMain.PartNo
                                                        , sDB_PEMain.CusVer
                                                        , sDB_PEMain.OpVer
                                                        , j.Op1
                                                        , 155
                                                        , 66);
                            bitmap.Save(imgPath);
                            sDB_PartOperation.Op1 = j.Op1;
                            sDB_PartOperation.Op2 = j.Op2;
                            sDB_PartOperation.ImgPath = imgPath;
                            ListDB_PartOperation_New.Add(sDB_PartOperation);
                            #endregion
                        }
                        else if (j.Op2 == "Flaw Detect")
                        {
                            #region Flaw Detect

                            bitmap = new Bitmap(155, 66);
                            graphicsObj = Graphics.FromImage(bitmap);

                            //畫方框
                            Point8 sPoint8 = new Point8();
                            status = DrawRectangle(graphicsObj, Color.Black, 50, 0, 100, 40, j.Op1 + "\n" + j.Op2, out sPoint8);
                            if (!status)
                                return false;

                            //向下箭頭
                            int startX = sPoint8.DownPointX;
                            int startY = sPoint8.DownPointY;
                            ArrowPoint sArrowPoint = new ArrowPoint();
                            status = DrawDownArrow(graphicsObj, startX, startY, 20, out sArrowPoint);
                            if (!status)
                                return false;

                            //存成圖片
                            imgPath = string.Format(@"{0}\{1}_{2}_{3}\{4}_{5}_{6}.Jpeg"
                                                        , Environment.GetFolderPath(Environment.SpecialFolder.Desktop)
                                                        , sDB_PEMain.PartNo
                                                        , sDB_PEMain.CusVer
                                                        , sDB_PEMain.OpVer
                                                        , j.Op1
                                                        , 155
                                                        , 66);
                            bitmap.Save(imgPath);
                            sDB_PartOperation.Op1 = j.Op1;
                            sDB_PartOperation.Op2 = j.Op2;
                            sDB_PartOperation.ImgPath = imgPath;
                            ListDB_PartOperation_New.Add(sDB_PartOperation);
                            #endregion
                        }
                        else
                        {
                            #region else

                            bitmap = new Bitmap(155, 131);
                            graphicsObj = Graphics.FromImage(bitmap);

                            //畫方框
                            Point8 sPoint8 = new Point8();
                            status = DrawRectangle(graphicsObj, Color.Black, 50, 0, 100, 40, j.Op1 + "\n" + j.Op2, out sPoint8);
                            if (!status)
                                return false;

                            //向下箭頭
                            int startX = sPoint8.DownPointX;
                            int startY = sPoint8.DownPointY;
                            ArrowPoint sArrowPoint = new ArrowPoint();
                            status = DrawDownArrow(graphicsObj, startX, startY, 20, out sArrowPoint);
                            if (!status)
                                return false;

                            //畫菱形
                            startX = sArrowPoint.EndPointX;
                            startY = sArrowPoint.EndPointY;
                            status = DrawDiamond(graphicsObj, startX, startY, 40, 20, out sPoint8);
                            if (!status)
                                return false;

                            //畫透明方框
                            startX = sPoint8.LeftUpPointX;
                            startY = sPoint8.LeftUpPointY;
                            int rectangleWidth = sPoint8.RightPointX - sPoint8.LeftUpPointX;
                            int rectangleheight = sPoint8.DownPointY - sPoint8.UpPointY;
                            status = DrawRectangle(graphicsObj, Color.Transparent, startX, startY, rectangleWidth, rectangleheight, "IPQC", out sPoint8);
                            if (!status)
                                return false;

                            //向下箭頭
                            startX = sPoint8.DownPointX;
                            startY = sPoint8.DownPointY;
                            status = DrawDownArrow(graphicsObj, startX, startY, 20, out sArrowPoint);
                            if (!status)
                                return false;

                            //寫OK
                            startX = sArrowPoint.StartPointX + 3;
                            startY = sArrowPoint.StartPointY + 3;
                            status = WriteString(graphicsObj, startX, startY, "OK", 7);
                            if (!status)
                                return false;

                            //向左箭頭
                            startX = sPoint8.LeftPointX;
                            startY = sPoint8.LeftPointY;
                            status = DrawLeftArrow(graphicsObj, startX, startY, 20, out sArrowPoint);
                            if (!status)
                                return false;

                            //寫NG
                            startX = sArrowPoint.StartPointX - 15;
                            startY = sArrowPoint.StartPointY - 10;
                            status = WriteString(graphicsObj, startX, startY, "NG", 7);
                            if (!status)
                                return false;

                            //畫圓
                            startX = sArrowPoint.EndPointX - 20;
                            startY = sArrowPoint.EndPointY - 10;
                            status = DrawCircle(graphicsObj, startX, startY, 10, out sPoint8);
                            if (!status)
                                return false;

                            //寫A
                            startX = sArrowPoint.EndPointX - 15;
                            startY = sArrowPoint.EndPointY - 5;
                            status = WriteString(graphicsObj, startX, startY, "A", 8);
                            if (!status)
                                return false;

                            //畫A下面的線
                            startX = sPoint8.DownPointX;
                            startY = sPoint8.DownPointY;
                            status = DrawDownLine(graphicsObj, startX, startY, 20, out sArrowPoint);
                            if (!status)
                                return false;

                            //寫OK
                            startX = sArrowPoint.StartPointX + 5;
                            startY = sArrowPoint.StartPointY + 2;
                            status = WriteString(graphicsObj, startX, startY, "OK", 7);
                            if (!status)
                                return false;

                            //向右箭頭
                            startX = sArrowPoint.EndPointX;
                            startY = sArrowPoint.EndPointY;
                            status = DrawRightArrow(graphicsObj, startX, startY, 69, out sArrowPoint);
                            if (!status)
                                return false;

                            //存成圖片
                            imgPath = string.Format(@"{0}\{1}_{2}_{3}\{4}_{5}_{6}.Jpeg"
                                                        , Environment.GetFolderPath(Environment.SpecialFolder.Desktop)
                                                        , sDB_PEMain.PartNo
                                                        , sDB_PEMain.CusVer
                                                        , sDB_PEMain.OpVer
                                                        , j.Op1
                                                        , 155
                                                        , 131);
                            bitmap.Save(imgPath);
                            sDB_PartOperation.Op1 = j.Op1;
                            sDB_PartOperation.Op2 = j.Op2;
                            sDB_PartOperation.ImgPath = imgPath;
                            ListDB_PartOperation_New.Add(sDB_PartOperation);
                            #endregion
                        }
                    }

                    #region 製作Shipping
                    bitmap = new Bitmap(155, 31);
                    graphicsObj = Graphics.FromImage(bitmap);

                    //畫透明方框
                    Point8 sPoint = new Point8();
                    status = DrawRectangle(graphicsObj, Color.Transparent, 55, 0, 70, 30, "Shipping", out sPoint);
                    if (!status)
                        return false;

                    //畫平行四邊形
                    status = DrawParallelogram(graphicsObj);
                    if (!status)
                        return false;

                    //存成圖片
                    imgPath = string.Format(@"{0}\{1}_{2}_{3}\{4}_{5}_{6}.Jpeg"
                                                , Environment.GetFolderPath(Environment.SpecialFolder.Desktop)
                                                , sDB_PEMain.PartNo
                                                , sDB_PEMain.CusVer
                                                , sDB_PEMain.OpVer
                                                , "Shipping"
                                                , 155
                                                , 31);
                    bitmap.Save(imgPath);
                    #endregion


                    //重新加回DicPFDData
                    DicPFDData[sDB_PEMain] = ListDB_PartOperation_New;
                }
                #region 註解中
                
                /*
                foreach (KeyValuePair<DB_PEMain, List<DB_PartOperation>> kvp in DicPFDData)
                {
                    foreach (DB_PartOperation i in kvp.Value)
                    {
                        if (i.Op2 == "Blank")
                        {
                            #region Blank

                            bitmap = new Bitmap(155, 131);
                            Graphics graphicsObj = Graphics.FromImage(bitmap);

                            //畫倒三角
                            Point8 sPoint8 = new Point8();
                            status = DrawReverseTriangle(graphicsObj, out sPoint8);
                            if (!status)
                                return false;

                            //畫透明方框
                            int rectangleWidth = sPoint8.RightPointX - sPoint8.LeftUpPointX;
                            int rectangleheight = sPoint8.DownPointY - sPoint8.UpPointY;
                            status = DrawRectangle(graphicsObj, Color.Transparent, sPoint8.LeftUpPointX, sPoint8.LeftUpPointY, rectangleWidth, rectangleheight, i.Op1 + "\n" + i.Op2, out sPoint8);
                            if (!status)
                                return false;

                            //向下箭頭
                            int startX = sPoint8.DownPointX;
                            int startY = sPoint8.DownPointY;
                            ArrowPoint sArrowPoint = new ArrowPoint();
                            status = DrawDownArrow(graphicsObj, startX, startY, 20, out sArrowPoint);
                            if (!status)
                                return false;

                            //畫菱形
                            startX = sArrowPoint.EndPointX;
                            startY = sArrowPoint.EndPointY;
                            status = DrawDiamond(graphicsObj, startX, startY, 40, 20, out sPoint8);
                            if (!status)
                                return false;

                            //畫透明方框
                            startX = sPoint8.LeftUpPointX;
                            startY = sPoint8.LeftUpPointY;
                            rectangleWidth = sPoint8.RightPointX - sPoint8.LeftUpPointX;
                            rectangleheight = sPoint8.DownPointY - sPoint8.UpPointY;
                            status = DrawRectangle(graphicsObj, Color.Transparent, startX, startY, rectangleWidth, rectangleheight, "IQC", out sPoint8);
                            if (!status)
                                return false;

                            //向下箭頭
                            startX = sPoint8.DownPointX;
                            startY = sPoint8.DownPointY;
                            status = DrawDownArrow(graphicsObj, startX, startY, 20, out sArrowPoint);
                            if (!status)
                                return false;

                            //寫OK
                            startX = sArrowPoint.StartPointX + 3;
                            startY = sArrowPoint.StartPointY + 3;
                            status = WriteString(graphicsObj, startX, startY, "OK", 7);
                            if (!status)
                                return false;

                            //向左箭頭
                            startX = sPoint8.LeftPointX;
                            startY = sPoint8.LeftPointY;
                            status = DrawLeftArrow(graphicsObj, startX, startY, 20, out sArrowPoint);
                            if (!status)
                                return false;

                            //寫NG
                            startX = sArrowPoint.StartPointX - 15;
                            startY = sArrowPoint.StartPointY - 10;
                            status = WriteString(graphicsObj, startX, startY, "NG", 7);
                            if (!status)
                                return false;

                            //畫圓
                            startX = sArrowPoint.EndPointX - 20;
                            startY = sArrowPoint.EndPointY - 10;
                            status = DrawCircle(graphicsObj, startX, startY, 10, out sPoint8);
                            if (!status)
                                return false;

                            //寫A
                            startX = sArrowPoint.EndPointX - 15;
                            startY = sArrowPoint.EndPointY - 5;
                            status = WriteString(graphicsObj, startX, startY, "A", 8);
                            if (!status)
                                return false;

                            //畫A下面的線
                            startX = sPoint8.DownPointX;
                            startY = sPoint8.DownPointY;
                            status = DrawDownLine(graphicsObj, startX, startY, 20, out sArrowPoint);
                            if (!status)
                                return false;

                            //寫OK
                            startX = sArrowPoint.StartPointX + 5;
                            startY = sArrowPoint.StartPointY + 2;
                            status = WriteString(graphicsObj, startX, startY, "OK", 7);
                            if (!status)
                                return false;

                            //向右箭頭
                            startX = sArrowPoint.EndPointX;
                            startY = sArrowPoint.EndPointY;
                            status = DrawRightArrow(graphicsObj, startX, startY, 69, out sArrowPoint);
                            if (!status)
                                return false;

                            //存成圖片
                            imgPath = string.Format(@"{0}\{1}_{2}_{3}\{4}_{5}_{6}.Jpeg"
                                                        , Environment.GetFolderPath(Environment.SpecialFolder.Desktop)
                                                        , kvp.Key.PartNo
                                                        , kvp.Key.CusVer
                                                        , kvp.Key.OpVer
                                                        , i.Op1
                                                        , 151
                                                        , 131);
                            bitmap.Save(imgPath);
                            //sDB_PartOperation.Op1 = i.Op1;
                            //sDB_PartOperation.Op2 = i.Op2;
                            //sDB_PartOperation.ImgPath = imgPath;
                            //kvp.Value[index] = sDB_PartOperation;
                            //DicPFDData[kvp.Key] = kvp.Value;
                            #endregion
                        }
                        else if (i.Op2 == "Appearance check & Packaging")
                        {
                            #region Appearance check & Packaging

                            bitmap = new Bitmap(155, 66);
                            Graphics graphicsObj = Graphics.FromImage(bitmap);

                            //畫方框
                            Point8 sPoint8 = new Point8();
                            status = DrawRectangle(graphicsObj, Color.Black, 50, 0, 100, 40, i.Op1 + "\n" + i.Op2, out sPoint8);
                            if (!status)
                                return false;

                            //向下箭頭
                            ArrowPoint sArrowPoint = new ArrowPoint();
                            int startX = sPoint8.DownPointX;
                            int startY = sPoint8.DownPointY;
                            status = DrawDownArrow(graphicsObj, startX, startY, 20, out sArrowPoint);
                            if (!status)
                                return false;

                            //寫OK
                            startX = sArrowPoint.StartPointX + 3;
                            startY = sArrowPoint.StartPointY + 3;
                            status = WriteString(graphicsObj, startX, startY, "OK", 7);
                            if (!status)
                                return false;

                            //向左箭頭
                            startX = sPoint8.LeftPointX;
                            startY = sPoint8.LeftPointY;
                            status = DrawLeftArrow(graphicsObj, startX, startY, 20, out sArrowPoint);
                            if (!status)
                                return false;

                            //寫NG
                            startX = sArrowPoint.StartPointX - 15;
                            startY = sArrowPoint.StartPointY - 10;
                            status = WriteString(graphicsObj, startX, startY, "NG", 7);
                            if (!status)
                                return false;

                            //畫圓
                            startX = sArrowPoint.EndPointX - 20;
                            startY = sArrowPoint.EndPointY - 10;
                            status = DrawCircle(graphicsObj, startX, startY, 10, out sPoint8);
                            if (!status)
                                return false;

                            //寫A
                            startX = sArrowPoint.EndPointX - 15;
                            startY = sArrowPoint.EndPointY - 5;
                            status = WriteString(graphicsObj, startX, startY, "A", 8);
                            if (!status)
                                return false;

                            //畫A下面的線
                            startX = sPoint8.DownPointX;
                            startY = sPoint8.DownPointY;
                            status = DrawDownLine(graphicsObj, startX, startY, 20, out sArrowPoint);
                            if (!status)
                                return false;

                            //寫OK
                            startX = sArrowPoint.StartPointX + 5;
                            startY = sArrowPoint.StartPointY + 2;
                            status = WriteString(graphicsObj, startX, startY, "OK", 7);
                            if (!status)
                                return false;

                            //向右箭頭
                            startX = sArrowPoint.EndPointX;
                            startY = sArrowPoint.EndPointY;
                            status = DrawRightArrow(graphicsObj, startX, startY, 64, out sArrowPoint);
                            if (!status)
                                return false;

                            //存成圖片
                            imgPath = string.Format(@"{0}\{1}_{2}_{3}\{4}_{5}_{6}.Jpeg"
                                                        , Environment.GetFolderPath(Environment.SpecialFolder.Desktop)
                                                        , kvp.Key.PartNo
                                                        , kvp.Key.CusVer
                                                        , kvp.Key.OpVer
                                                        , i.Op1
                                                        , 151
                                                        , 66);
                            bitmap.Save(imgPath);
                            #endregion
                        }
                        else if (i.Op2 == "Flaw Detect")
                        {
                            #region Flaw Detect

                            bitmap = new Bitmap(155, 66);
                            Graphics graphicsObj = Graphics.FromImage(bitmap);

                            //畫方框
                            Point8 sPoint8 = new Point8();
                            status = DrawRectangle(graphicsObj, Color.Black, 50, 0, 100, 40, i.Op1 + "\n" + i.Op2, out sPoint8);
                            if (!status)
                                return false;

                            //向下箭頭
                            int startX = sPoint8.DownPointX;
                            int startY = sPoint8.DownPointY;
                            ArrowPoint sArrowPoint = new ArrowPoint();
                            status = DrawDownArrow(graphicsObj, startX, startY, 20, out sArrowPoint);
                            if (!status)
                                return false;

                            //存成圖片
                            imgPath = string.Format(@"{0}\{1}_{2}_{3}\{4}_{5}_{6}.Jpeg"
                                                        , Environment.GetFolderPath(Environment.SpecialFolder.Desktop)
                                                        , kvp.Key.PartNo
                                                        , kvp.Key.CusVer
                                                        , kvp.Key.OpVer
                                                        , i.Op1
                                                        , 151
                                                        , 66);
                            bitmap.Save(imgPath);
                            #endregion
                        }
                        else
                        {
                            #region else

                            bitmap = new Bitmap(155, 131);
                            Graphics graphicsObj = Graphics.FromImage(bitmap);

                            //畫方框
                            Point8 sPoint8 = new Point8();
                            status = DrawRectangle(graphicsObj, Color.Black, 50, 0, 100, 40, i.Op1 + "\n" + i.Op2, out sPoint8);
                            if (!status)
                                return false;

                            //向下箭頭
                            int startX = sPoint8.DownPointX;
                            int startY = sPoint8.DownPointY;
                            ArrowPoint sArrowPoint = new ArrowPoint();
                            status = DrawDownArrow(graphicsObj, startX, startY, 20, out sArrowPoint);
                            if (!status)
                                return false;

                            //畫菱形
                            startX = sArrowPoint.EndPointX;
                            startY = sArrowPoint.EndPointY;
                            status = DrawDiamond(graphicsObj, startX, startY, 40, 20, out sPoint8);
                            if (!status)
                                return false;

                            //畫透明方框
                            startX = sPoint8.LeftUpPointX;
                            startY = sPoint8.LeftUpPointY;
                            int rectangleWidth = sPoint8.RightPointX - sPoint8.LeftUpPointX;
                            int rectangleheight = sPoint8.DownPointY - sPoint8.UpPointY;
                            status = DrawRectangle(graphicsObj, Color.Transparent, startX, startY, rectangleWidth, rectangleheight, "IPQC", out sPoint8);
                            if (!status)
                                return false;

                            //向下箭頭
                            startX = sPoint8.DownPointX;
                            startY = sPoint8.DownPointY;
                            status = DrawDownArrow(graphicsObj, startX, startY, 20, out sArrowPoint);
                            if (!status)
                                return false;

                            //寫OK
                            startX = sArrowPoint.StartPointX + 3;
                            startY = sArrowPoint.StartPointY + 3;
                            status = WriteString(graphicsObj, startX, startY, "OK", 7);
                            if (!status)
                                return false;

                            //向左箭頭
                            startX = sPoint8.LeftPointX;
                            startY = sPoint8.LeftPointY;
                            status = DrawLeftArrow(graphicsObj, startX, startY, 20, out sArrowPoint);
                            if (!status)
                                return false;

                            //寫NG
                            startX = sArrowPoint.StartPointX - 15;
                            startY = sArrowPoint.StartPointY - 10;
                            status = WriteString(graphicsObj, startX, startY, "NG", 7);
                            if (!status)
                                return false;

                            //畫圓
                            startX = sArrowPoint.EndPointX - 20;
                            startY = sArrowPoint.EndPointY - 10;
                            status = DrawCircle(graphicsObj, startX, startY, 10, out sPoint8);
                            if (!status)
                                return false;

                            //寫A
                            startX = sArrowPoint.EndPointX - 15;
                            startY = sArrowPoint.EndPointY - 5;
                            status = WriteString(graphicsObj, startX, startY, "A", 8);
                            if (!status)
                                return false;

                            //畫A下面的線
                            startX = sPoint8.DownPointX;
                            startY = sPoint8.DownPointY;
                            status = DrawDownLine(graphicsObj, startX, startY, 20, out sArrowPoint);
                            if (!status)
                                return false;

                            //寫OK
                            startX = sArrowPoint.StartPointX + 5;
                            startY = sArrowPoint.StartPointY + 2;
                            status = WriteString(graphicsObj, startX, startY, "OK", 7);
                            if (!status)
                                return false;

                            //向右箭頭
                            startX = sArrowPoint.EndPointX;
                            startY = sArrowPoint.EndPointY;
                            status = DrawRightArrow(graphicsObj, startX, startY, 69, out sArrowPoint);
                            if (!status)
                                return false;

                            //存成圖片
                            imgPath = string.Format(@"{0}\{1}_{2}_{3}\{4}_{5}_{6}.Jpeg"
                                                        , Environment.GetFolderPath(Environment.SpecialFolder.Desktop)
                                                        , kvp.Key.PartNo
                                                        , kvp.Key.CusVer
                                                        , kvp.Key.OpVer
                                                        , i.Op1
                                                        , 151
                                                        , 131);
                            bitmap.Save(imgPath);
                            #endregion
                        }
                    }
                }
                */
                #endregion
            }
            catch (System.Exception ex)
            {
                MessageBox.Show(ex.ToString());
                return false;
            }
            return true;
        }

        public static bool DrawParallelogram(Graphics graphicsObj)
        {
            try
            {
                Pen myPen = new Pen(Color.Black, 1);
                Point[] myPoints = new Point[] { new Point(70, 0), new Point(40, 30), new Point(110, 30), new Point(140, 0) };
                graphicsObj.DrawPolygon(myPen, myPoints);
            }
            catch (System.Exception ex)
            {
                return false;
            }
            return true;
        }
        public static bool DrawReverseTriangle(Graphics graphicsObj, out Point8 sPont8)
        {
            sPont8 = new Point8();
            try
            {
                Pen myPen = new Pen(Color.Black, 1);
                Point[] myPoints = new Point[] { new Point(55, 0), new Point(100, 40), new Point(145, 0) };
                graphicsObj.DrawPolygon(myPen, myPoints);

                sPont8.LeftUpPointX = 55; sPont8.LeftUpPointY = 0;
                sPont8.UpPointX = 100; sPont8.UpPointY = 0;
                sPont8.RightUpPointX = 145; sPont8.RightUpPointY = 0;
                sPont8.LeftPointX = 55; sPont8.LeftPointY = 25;
                sPont8.RightPointX = 145; sPont8.RightPointY = 25;
                sPont8.LeftDownPointX = 55; sPont8.LeftDownPointY = 40;
                sPont8.DownPointX = 100; sPont8.DownPointY = 40;
                sPont8.RightDownPointX = 145; sPont8.RightDownPointY = 40;
            }
            catch (System.Exception ex)
            {
                return false;
            }
            return true;
        }
        public static bool DrawRectangle(Graphics graphicsObj, Color rectangleColor, int startX, int startY, int width, int height, string text, out Point8 sPont8)
        {
            sPont8 = new Point8();
            try
            {
                Rectangle myRectangle = new Rectangle(startX, startY, width, height);
                Pen myPen = new Pen(rectangleColor, 1);
                graphicsObj.DrawRectangle(myPen, myRectangle);

                myFont = new System.Drawing.Font("新細明體", 8);
                myPen = new Pen(Color.Blue, 1);
                stringFormat.Alignment = StringAlignment.Center;
                stringFormat.LineAlignment = StringAlignment.Center;
                graphicsObj.DrawString(text, myFont, myBrush, myRectangle, stringFormat);

                sPont8.LeftUpPointX = startX; sPont8.LeftUpPointY = startY;
                sPont8.UpPointX = startX + Convert.ToInt32(width / 2); sPont8.UpPointY = startY;
                sPont8.RightUpPointX = startX + width; sPont8.RightUpPointY = startY;

                sPont8.LeftPointX = startX; sPont8.LeftPointY = startY + Convert.ToInt32(height / 2);
                sPont8.RightPointX = startX + width; sPont8.RightPointY = startY + Convert.ToInt32(height / 2);

                sPont8.LeftDownPointX = startX; sPont8.LeftDownPointY = startY + height;
                sPont8.DownPointX = startX + Convert.ToInt32(width / 2); sPont8.DownPointY = startY + height;
                sPont8.RightDownPointX = startX + width; sPont8.RightDownPointY = startY + height;
            }
            catch (System.Exception ex)
            {
                return false;
            }
            return true;
        }
        public static bool DrawDiamond(Graphics graphicsObj, int startX, int startY, int longLen, int shortLen, out Point8 sPont8)
        {
            sPont8 = new Point8();
            try
            {
                Pen myPen = new Pen(System.Drawing.Color.Black, 1);
                Point point1 = new Point(startX, startY);
                Point point2 = new Point(startX - longLen, startY + shortLen);
                Point point3 = new Point(startX, startY + shortLen * 2);
                Point point4 = new Point(startX + longLen, startY + shortLen);
                Point[] myPoints = new Point[] { point1, point2, point3, point4 };
                graphicsObj.DrawPolygon(myPen, myPoints);

                sPont8.LeftUpPointX = startX - longLen; sPont8.LeftUpPointY = startY;
                sPont8.UpPointX = startX; sPont8.UpPointY = startY;
                sPont8.RightUpPointX = startX + longLen; sPont8.RightUpPointY = startY;

                sPont8.LeftPointX = startX - longLen; sPont8.LeftPointY = startY + shortLen;
                sPont8.RightPointX = startX + longLen; sPont8.RightPointY = startY + shortLen;

                sPont8.LeftDownPointX = startX - longLen; sPont8.LeftDownPointY = startY + shortLen * 2;
                sPont8.DownPointX = startX; sPont8.DownPointY = startY + shortLen * 2;
                sPont8.RightDownPointX = startX + longLen; sPont8.RightDownPointY = startY + shortLen * 2;
            }
            catch (System.Exception ex)
            {
                return false;
            }
            return true;
        }
        public static bool DrawDownArrow(Graphics graphicsObj, int startX, int startY, int length, out ArrowPoint sArrowPoint)
        {
            sArrowPoint = new ArrowPoint();
            try
            {
                Pen myPen = new Pen(System.Drawing.Color.Black, 1);
                GraphicsPath capPath = new GraphicsPath();
                capPath.AddLine(-5, 0, 5, 0);
                capPath.AddLine(-5, 0, 0, 5);
                capPath.AddLine(0, 5, 5, 0);
                myPen.CustomEndCap = new System.Drawing.Drawing2D.CustomLineCap(null, capPath);
                graphicsObj.DrawLine(myPen, startX, startY, startX, startY + length);
                sArrowPoint.StartPointX = startX; sArrowPoint.StartPointY = startY;
                sArrowPoint.EndPointX = startX; sArrowPoint.EndPointY = startY + length + 5;
            }
            catch (System.Exception ex)
            {
                return false;
            }
            return true;   
        }
        public static bool DrawLeftArrow(Graphics graphicsObj, int startX, int startY, int length, out ArrowPoint sArrowPoint)
        {
            sArrowPoint = new ArrowPoint();
            try
            {
                Pen myPen = new Pen(System.Drawing.Color.Black, 1);
                GraphicsPath capPath = new GraphicsPath();
                capPath.AddLine(-5, 0, 5, 0);
                capPath.AddLine(-5, 0, 0, 5);
                capPath.AddLine(0, 5, 5, 0);
                myPen.CustomEndCap = new System.Drawing.Drawing2D.CustomLineCap(null, capPath);
                graphicsObj.DrawLine(myPen, startX, startY, startX - length, startY);
                sArrowPoint.StartPointX = startX; sArrowPoint.StartPointY = startY;
                sArrowPoint.EndPointX = startX - length - 5; sArrowPoint.EndPointY = startY;
            }
            catch (System.Exception ex)
            {
                return false;
            }
            return true;
        }
        public static bool DrawRightArrow(Graphics graphicsObj, int startX, int startY, int length, out ArrowPoint sArrowPoint)
        {
            sArrowPoint = new ArrowPoint();
            try
            {
                Pen myPen = new Pen(System.Drawing.Color.Black, 1);
                GraphicsPath capPath = new GraphicsPath();
                capPath.AddLine(-5, 0, 5, 0);
                capPath.AddLine(-5, 0, 0, 5);
                capPath.AddLine(0, 5, 5, 0);
                myPen.CustomEndCap = new System.Drawing.Drawing2D.CustomLineCap(null, capPath);
                graphicsObj.DrawLine(myPen, startX, startY, startX + length, startY);
                sArrowPoint.StartPointX = startX; sArrowPoint.StartPointY = startY;
                sArrowPoint.EndPointX = startX + length + 5; sArrowPoint.EndPointY = startY;
            }
            catch (System.Exception ex)
            {
                return false;
            }
            return true;
        }
        public static bool DrawDownLine(Graphics graphicsObj, int startX, int startY, int length, out ArrowPoint sArrowPoint)
        {
            sArrowPoint = new ArrowPoint();
            try
            {
                Pen myPen = new Pen(System.Drawing.Color.Black, 1);
                graphicsObj.DrawLine(myPen, startX, startY, startX, startY + length);
                sArrowPoint.StartPointX = startX; sArrowPoint.StartPointY = startY;
                sArrowPoint.EndPointX = startX; sArrowPoint.EndPointY = startY + length;
            }
            catch (System.Exception ex)
            {
                return false;
            }
            return true;
        }
        public static bool DrawLeftLine(Graphics graphicsObj, int startX, int startY, int length, out ArrowPoint sArrowPoint)
        {
            sArrowPoint = new ArrowPoint();
            try
            {
                Pen myPen = new Pen(System.Drawing.Color.Black, 1);
                graphicsObj.DrawLine(myPen, startX, startY, startX - length, startY);
                sArrowPoint.StartPointX = startX; sArrowPoint.StartPointY = startY;
                sArrowPoint.EndPointX = startX - length - 5; sArrowPoint.EndPointY = startY;
            }
            catch (System.Exception ex)
            {
                return false;
            }
            return true;
        }
        public static bool DrawRightLine(Graphics graphicsObj, int startX, int startY, int length, out ArrowPoint sArrowPoint)
        {
            sArrowPoint = new ArrowPoint();
            try
            {
                Pen myPen = new Pen(System.Drawing.Color.Black, 1);
                graphicsObj.DrawLine(myPen, startX, startY, startX + length, startY);
                sArrowPoint.StartPointX = startX; sArrowPoint.StartPointY = startY;
                sArrowPoint.EndPointX = startX + length + 5; sArrowPoint.EndPointY = startY;
            }
            catch (System.Exception ex)
            {
                return false;
            }
            return true;
        }
        public static bool DrawCircle(Graphics graphicsObj, int startX, int startY, int radius, out Point8 sPont8)
        {
            sPont8 = new Point8();
            try
            {
                Pen myPen = new Pen(System.Drawing.Color.Black, 1);
                graphicsObj.DrawEllipse(myPen, startX, startY, radius * 2, radius * 2);
                SolidBrush circleBrush = new SolidBrush(System.Drawing.Color.HotPink);
                graphicsObj.FillEllipse(circleBrush, startX, startY, radius * 2, radius * 2);

                sPont8.LeftUpPointX = startX; sPont8.LeftUpPointY = startY;
                sPont8.UpPointX = startX + radius; sPont8.UpPointY = startY;
                sPont8.RightUpPointX = startX + radius * 2; sPont8.RightUpPointY = startY;

                sPont8.LeftPointX = startX; sPont8.LeftPointY = startY + radius;
                sPont8.RightPointX = startX + radius * 2; sPont8.RightPointY = startY + radius;

                sPont8.LeftDownPointX = startX; sPont8.LeftDownPointY = startY + radius * 2;
                sPont8.DownPointX = startX + radius; sPont8.DownPointY = startY + radius * 2;
                sPont8.RightDownPointX = startX + radius * 2; sPont8.RightDownPointY = startY + radius * 2;
            }
            catch (System.Exception ex)
            {
                return false;
            }
            return true;
        }
        public static bool WriteString(Graphics graphicsObj, int startX, int startY, string text, int textSize)
        {
            try
            {
                myFont = new System.Drawing.Font("新細明體", textSize);
                PointF strPoint = new PointF(startX, startY);
                graphicsObj.DrawString(text, myFont, myBrush, strPoint);
            }
            catch (System.Exception ex)
            {
                return false;
            }
            return true;
        }
        
    }
}
