using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using DevComponents.AdvTree;
using System.Windows.Forms;
using Microsoft.Office.Interop.Excel;
using CaxGlobaltek;
using NHibernate;
using System.IO;
using DevComponents.DotNetBar.SuperGrid;

namespace PFMEA
{
    public class Fun_Common
    {
        public static ISession session = MyHibernateHelper.SessionFactory.OpenSession();
        public static ApplicationClass excelApp = new ApplicationClass();
        public static Workbook workBook = null;
        public static Worksheet workSheet = null;
        public static Range workRange = null;
        public static string OutputPath = "";

        public struct DimenData
        {
            public Node node { get; set; }
            public string dimensionSrNo { get; set; }
        }

        public static bool GetAllNodes(AdvTree OISTree, out Dictionary<Node, List<DimenData>> dicAllNode)
        {
            try
            {
                dicAllNode = new Dictionary<Node, List<DimenData>>();

                foreach (Node i in OISTree.Nodes)
                {
                    List<DimenData> childNode = new List<DimenData>();
                    if (i.HasChildNodes)
                    {
                        DimenData sDimenData = new DimenData();
                        foreach (Node j in i.Nodes)
                        {
                            sDimenData.node = j;
                            sDimenData.dimensionSrNo = j.Tag.ToString();
                            childNode.Add(sDimenData);
                        }
                    }
                    else
                    {
                        continue;
                    }
                    dicAllNode.Add(i, childNode);
                }
            }
            catch (System.Exception ex)
            {
                dicAllNode = new Dictionary<Node, List<DimenData>>();
                return false;
            }
            return true;
        }

        public static bool GetSelNodes(AdvTree OISTree, out Dictionary<Node, List<Node>> DicSelNodes)
        {
            DicSelNodes = new Dictionary<Node, List<Node>>();
            try
            {
                foreach (Node i in OISTree.Nodes)
                {
                    List<Node> childNode = new List<Node>();
                    if (i.HasChildNodes)
                    {
                        foreach (Node j in i.Nodes)
                        {
                            if (j.Checked == false)
                            {
                                continue;
                            }
                            childNode.Add(j);
                        }
                        if (childNode.Count > 0)
                        {
                            DicSelNodes.Add(i, childNode);
                        }
                    }
                    else
                    {
                        continue;
                    }
                }
            }
            catch (System.Exception ex)
            {
                return false;
            }
            return true;
        }

        public static bool ManualAdd(GridPanel panel, string manualstr)
        {
            try
            {
                object[] obj = new object[] { false, manualstr };
                panel.Rows.Add(new GridRow(obj));
            }
            catch (System.Exception ex)
            {
                return false;
            }
            return true;
        }

        public static bool DeleteDataBase(IList<Com_PFMEA> listComPFMEA, Dictionary<Node, List<Node>> DicSelNodes)
        {
            try
            {
                foreach (KeyValuePair<Node, List<Node>> kvp in DicSelNodes)
                {
                    foreach (Node i in kvp.Value)
                    {
                        //刪除舊資料
                        foreach (Com_PFMEA j in listComPFMEA)
                        {
                            if (j.comDimension.dimensionSrNo == ((Com_Dimension)i.Tag).dimensionSrNo)
                                session.Delete(j);

                            session.BeginTransaction().Commit();
                        }
                        Com_PFMEA cCom_PFMEA = new Com_PFMEA();
                        cCom_PFMEA.comDimension = (Com_Dimension)i.Tag;
                        cCom_PFMEA.pFMData = i.Cells[3].Text;
                        cCom_PFMEA.pEoFData = i.Cells[4].Text;
                        cCom_PFMEA.sevData = i.Cells[5].Text;
                        cCom_PFMEA.classData = i.Cells[6].Text;
                        cCom_PFMEA.pCoFData = i.Cells[7].Text;
                        cCom_PFMEA.occurrenceData = i.Cells[8].Text;
                        cCom_PFMEA.preventionData = i.Cells[9].Text;
                        cCom_PFMEA.detectionData = i.Cells[10].Text;
                        cCom_PFMEA.detData = i.Cells[11].Text;
                        cCom_PFMEA.rpnData = i.Cells[12].Text;
                        using (ITransaction trans = session.BeginTransaction())
                        {
                            session.Save(cCom_PFMEA);
                            trans.Commit();
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
        public static bool CreatePFMEA(string partNo, string cusRev, string opRev, Dictionary<Node, List<Node>> DicSelNodes)
        {
            try
            {
                //設定輸出路徑
                OutputPath = string.Format(@"{0}\{1}_{2}_{3}\{4}_{5}_{6}"
                                                    , Environment.GetFolderPath(Environment.SpecialFolder.Desktop)
                                                    , partNo
                                                    , cusRev
                                                    , opRev
                                                    , partNo
                                                    , cusRev
                                                    , opRev + "_" + "PFMEA" + ".xls");
                //設定Template路徑
                string excelTemplatePath = string.Format(@"{0}\{1}\{2}\{3}\{4}.xls"
                                                                    , CaxEnv.GetGlobaltekEnvDir()
                                                                    , "PE_Config"
                                                                    , "Config"
                                                                    , "PFMEA"
                                                                    , "PFMEA");
                //1.開啟Excel
                //2.將Excel設為不顯示
                //3.取得第一頁Sheet
                workBook = excelApp.Workbooks.Open(excelTemplatePath);
                excelApp.Visible = false;
                workSheet = (Worksheet)workBook.Sheets[1];

                //Insert所需欄位
                int dimenCount = 0;
                foreach (KeyValuePair<Node, List<Node>> kvp in DicSelNodes)
                {
                    foreach (Node i in kvp.Value)
                    {
                        dimenCount++;
                    }
                }
                while (dimenCount - 1 != 0 && dimenCount > 1)
                {
                    workRange = (Range)workSheet.Range["A16"].EntireRow;
                    workRange.Insert(XlInsertShiftDirection.xlShiftDown, workRange.Copy(Type.Missing));
                    dimenCount--;
                }
                //設定欄位的Row,Column
                int
                    currentRow = 16, op1Column = 1, PFMColumn = 2, PEoFColumn = 3, SevColumn = 4, ClassChara = 5,
                    PC_MoFColumn = 6, OccColumn = 7, CPCPColumn = 8, CPCDColumn = 9, DetColumn = 10, RPNColumn = 11;
                foreach (KeyValuePair<Node,List<Node>> kvp in DicSelNodes)
                {
                    workRange = (Range)workSheet.Cells;
                    workRange[currentRow, op1Column] = kvp.Key.Cells[0].Text;
                    string OP1MergeRow = currentRow.ToString();
                    foreach (Node j in kvp.Value)
                    {
                        workRange[currentRow, PFMColumn] = j.Cells[3].Text;
                        workRange[currentRow, PEoFColumn] = j.Cells[4].Text;
                        workRange[currentRow, SevColumn] = j.Cells[5].Text;
                        workRange[currentRow, ClassChara] = j.Cells[6].Text;
                        workRange[currentRow, PC_MoFColumn] = j.Cells[7].Text;
                        workRange[currentRow, OccColumn] = j.Cells[8].Text;
                        workRange[currentRow, CPCPColumn] = j.Cells[9].Text;
                        workRange[currentRow, CPCDColumn] = j.Cells[10].Text;
                        workRange[currentRow, DetColumn] = j.Cells[11].Text;
                        workRange[currentRow, RPNColumn] = j.Cells[12].Text;
                        currentRow++;
                    }
                    //合併儲存格-OP1
                    workSheet.get_Range("A" + OP1MergeRow, "A" + (currentRow - 1).ToString()).Merge(false);
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
                Dispose();
            }
            return true;
        }
        public static void Dispose()
        {
            System.Runtime.InteropServices.Marshal.ReleaseComObject(workRange);
            System.Runtime.InteropServices.Marshal.ReleaseComObject(workSheet);
            System.Runtime.InteropServices.Marshal.ReleaseComObject(workBook);
            System.Runtime.InteropServices.Marshal.ReleaseComObject(excelApp);
        }
    }
}
