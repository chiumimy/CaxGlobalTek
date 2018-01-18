using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using NHibernate;
using CaxGlobaltek;
using System.Windows.Forms;
using DevComponents.AdvTree;
using DevComponents.DotNetBar;
using System.Drawing;

namespace PFMEA
{
    public class GetOISData
    {
        public static bool status;
        public static ISession session = MyHibernateHelper.SessionFactory.OpenSession();
        //public static Form MainForm;
        public static ElementStyle styleAligned = new ElementStyle();

        //public GetOISData(Form mainForm)
        //{
        //    MainForm = mainForm;
        //}
        public struct OIS_Key
        {
            public string Op1 { get; set; }
            public string Op2 { get; set; }
        }
        public struct OIS_Value
        {
            public string ProductName { get; set; }
            public string Balloon { get; set; }
        }

        public bool InitializeOISTree(Com_PartOperation ComPartOperation, AdvTree OISTree)
        {
            try
            {
                OISTree.BeginUpdate();
                Node Op1Node = new Node();
                Op1Node.Expanded = false;
                Op1Node.ExpandVisibility = eNodeExpandVisibility.Visible;
                Op1Node.CheckBoxVisible = true;

                //設定製程序、把PK值填入Tag中
                Op1Node.Text = ComPartOperation.operation1;
                Op1Node.Tag = ComPartOperation;

                //設定製程別
                Cell Op2Cell = new Cell();
                Op2Cell.Text = session.QueryOver<Sys_Operation2>().Where(x => x.operation2SrNo == ComPartOperation.sysOperation2.operation2SrNo)
                                                                .SingleOrDefault().operation2Name;
                Op1Node.Cells.Add(Op2Cell);

                OISTree.Nodes.Add(Op1Node);

                //填入此OIS相關資料
                status = InsertDimenData(ComPartOperation, Op1Node);
                if (!status)
                {
                    return false;
                }


                OISTree.EndUpdate();
            }
            catch (System.Exception ex)
            {
                return false;
            }
            return true;
        }
        public bool InsertDimenData(Com_PartOperation ComPartOperation, Node node1)
        {
            try
            {
                Com_MEMain comMEMain = session.QueryOver<Com_MEMain>().Where(x => x.comPartOperation == ComPartOperation).SingleOrDefault();
                IList<Com_Dimension> listComDimension = session.QueryOver<Com_Dimension>().Where(x => x.comMEMain == comMEMain).OrderBy(x => x.ballon).Asc.List();

                if (listComDimension.Count > 0)
                {
                    status = InsertColumns(node1);
                    if (!status)
                        return false;
                }

                foreach (Com_Dimension i in listComDimension)
                {
                    status = InsertSingleDimen(i, node1);
                    if (!status)
                    {
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
        public bool InsertColumns(Node node)
        {
            try
            {
                //泡泡
                DevComponents.AdvTree.ColumnHeader cColumnHeader = new DevComponents.AdvTree.ColumnHeader();
                //cColumnHeader.Name = "Balloon";
                cColumnHeader.Text = "Balloon";
                cColumnHeader.Width.Absolute = 50;
                cColumnHeader.SortingEnabled = false;
                node.NodesColumns.Add(cColumnHeader);
                //Dimension
                DevComponents.AdvTree.ColumnHeader cColumnHeader12 = new DevComponents.AdvTree.ColumnHeader();
                cColumnHeader12.Text = "Dimension";
                cColumnHeader12.Width.Absolute = 200;
                cColumnHeader12.SortingEnabled = false;
                node.NodesColumns.Add(cColumnHeader12);
                //產品名稱
                DevComponents.AdvTree.ColumnHeader cColumnHeader1 = new DevComponents.AdvTree.ColumnHeader();
                //cColumnHeader1.Name = "ProductName";
                cColumnHeader1.Text = "ProductName";
                cColumnHeader1.Width.Absolute = 80;
                cColumnHeader1.SortingEnabled = false;
                node.NodesColumns.Add(cColumnHeader1);
                //潛在失效模式
                DevComponents.AdvTree.ColumnHeader cColumnHeader2 = new DevComponents.AdvTree.ColumnHeader();
                //cColumnHeader2.Name = "Potential Failure Mode";
                cColumnHeader2.Text = "Potential Failure Mode";
                cColumnHeader2.Width.Absolute = 130;
                cColumnHeader2.SortingEnabled = false;
                node.NodesColumns.Add(cColumnHeader2);
                //潛在失效模式的後果
                DevComponents.AdvTree.ColumnHeader cColumnHeader3 = new DevComponents.AdvTree.ColumnHeader();
                //cColumnHeader3.Name = "Potential Effect of Failure";
                cColumnHeader3.Text = "Potential Effect of Failure";
                cColumnHeader3.Width.Absolute = 150;
                cColumnHeader3.SortingEnabled = false;
                node.NodesColumns.Add(cColumnHeader3);
                //嚴重度
                DevComponents.AdvTree.ColumnHeader cColumnHeader4 = new DevComponents.AdvTree.ColumnHeader();
                //cColumnHeader4.Name = "Sev";
                cColumnHeader4.Text = "Sev";
                cColumnHeader4.Width.Absolute = 30;
                cColumnHeader4.SortingEnabled = false;
                node.NodesColumns.Add(cColumnHeader4);
                //Class
                DevComponents.AdvTree.ColumnHeader cColumnHeader5 = new DevComponents.AdvTree.ColumnHeader();
                //cColumnHeader5.Name = "Class";
                cColumnHeader5.Text = "Class";
                cColumnHeader5.Width.Absolute = 40;
                cColumnHeader5.SortingEnabled = false;
                node.NodesColumns.Add(cColumnHeader5);
                //造成潛在失效原因
                DevComponents.AdvTree.ColumnHeader cColumnHeader6 = new DevComponents.AdvTree.ColumnHeader();
                //cColumnHeader6.Name = "Potential Cause/Mechanism of Failure";
                cColumnHeader6.Text = "Potential Cause/Mechanism of Failure";
                cColumnHeader6.Width.Absolute = 200;
                cColumnHeader6.SortingEnabled = false;
                node.NodesColumns.Add(cColumnHeader6);
                //Occ
                DevComponents.AdvTree.ColumnHeader cColumnHeader7 = new DevComponents.AdvTree.ColumnHeader();
                //cColumnHeader7.Name = "Occ";
                cColumnHeader7.Text = "Occ";
                cColumnHeader7.Width.Absolute = 30;
                cColumnHeader7.SortingEnabled = false;
                node.NodesColumns.Add(cColumnHeader7);
                //預防措施
                DevComponents.AdvTree.ColumnHeader cColumnHeader8 = new DevComponents.AdvTree.ColumnHeader();
                //cColumnHeader8.Name = "Current Process Controls Prevention";
                cColumnHeader8.Text = "Current Process Controls Prevention";
                cColumnHeader8.Width.Absolute = 200;
                cColumnHeader8.SortingEnabled = false;
                node.NodesColumns.Add(cColumnHeader8);
                //預防措施檢查
                DevComponents.AdvTree.ColumnHeader cColumnHeader9 = new DevComponents.AdvTree.ColumnHeader();
                //cColumnHeader9.Name = "Current Process Controls Detection";
                cColumnHeader9.Text = "Current Process Controls Detection";
                cColumnHeader9.Width.Absolute = 200;
                cColumnHeader9.SortingEnabled = false;
                node.NodesColumns.Add(cColumnHeader9);
                //Det
                DevComponents.AdvTree.ColumnHeader cColumnHeader10 = new DevComponents.AdvTree.ColumnHeader();
                //cColumnHeader10.Name = "Det";
                cColumnHeader10.Text = "Det";
                cColumnHeader10.Width.Absolute = 30;
                cColumnHeader10.SortingEnabled = false;
                node.NodesColumns.Add(cColumnHeader10);
                //R.P.N.
                DevComponents.AdvTree.ColumnHeader cColumnHeader11 = new DevComponents.AdvTree.ColumnHeader();
                //cColumnHeader11.Name = "R.P.N.";
                cColumnHeader11.Text = "R.P.N.";
                cColumnHeader11.Width.Absolute = 40;
                cColumnHeader11.SortingEnabled = false;
                node.NodesColumns.Add(cColumnHeader11);
            }
            catch (System.Exception ex)
            {
                return false;
            }
            return true;
        }
        public bool InsertSingleDimen(Com_Dimension singleDimen, Node node1)
        {
            try
            {
                //如有重複泡泡就不再插入
                foreach (Node i in node1.Nodes)
                {
                    if (i.Cells[0].Text == singleDimen.ballon.ToString())
                    {
                        return true;
                    }
                }

                //泡泡、把PK值填入Tag中
                Node secondNode = new Node();
                secondNode.Text = singleDimen.ballon.ToString();
                secondNode.CheckBoxVisible = true;
                secondNode.Tag = singleDimen;

                //Dimension
                Cell dimension = new Cell();
                WebBrowser tempWebBrowser = new WebBrowser();
                tempWebBrowser.Size = new Size(200, 25);
                tempWebBrowser.ScrollBarsEnabled = false;
                string htmlPath = "";
                status = CreateHTML(singleDimen, out htmlPath);
                if (!status)
                {
                    return false;
                }
                tempWebBrowser.Url = new Uri(htmlPath);
                dimension.HostedControl = tempWebBrowser;
                secondNode.Cells.Add(dimension);

                //產品名稱
                Cell productName = new Cell();
                //styleAligned.TextAlignment = eStyleTextAlignment.Center;
                //productName.StyleNormal = styleAligned;
                if (singleDimen.productName != null)
                    productName.Text = singleDimen.productName.ToString();
                else
                    productName.Text = "N/A";

                secondNode.Cells.Add(productName);

                Com_PFMEA singleData = new Com_PFMEA();
                bool isExist = false;
                foreach (Com_PFMEA i in Form1.listComPFMEA)
                {
                    if (i.comDimension.dimensionSrNo == singleDimen.dimensionSrNo)
                    {
                        isExist = true;
                        singleData = i;
                        break;
                    }
                }
                
                if (isExist)
                {
                    //singleData = Form1.listComPFMEA.Single(x => x.comDimension == singleDimen);
                    //潛在失效模式 Potential Failure Mode
                    Cell Potential_Failure_Mode = new Cell();
                    Potential_Failure_Mode.Images.Image = Properties.Resources.list_24px;
                    Potential_Failure_Mode.Text = singleData.pFMData;
                    secondNode.Cells.Add(Potential_Failure_Mode);

                    //潛在失效模式的後果 Potential Effect of Failure
                    Cell Potential_Effect_of_Failure = new Cell();
                    Potential_Effect_of_Failure.Images.Image = Properties.Resources.list_24px;
                    Potential_Effect_of_Failure.Text = singleData.pEoFData;
                    secondNode.Cells.Add(Potential_Effect_of_Failure);

                    //嚴重度 Severity
                    Cell Severity = new Cell();
                    Severity.Text = singleData.sevData;
                    secondNode.Cells.Add(Severity);

                    //分類 Class
                    Cell Class = new Cell();
                    Class.Text = singleData.classData;
                    secondNode.Cells.Add(Class);

                    //造成潛在失效原因 Potential Cause/Mechanism of Failure
                    Cell Potential_Cause_Mechanism_of_Failure = new Cell();
                    Potential_Cause_Mechanism_of_Failure.Images.Image = Properties.Resources.list_24px;
                    Potential_Cause_Mechanism_of_Failure.Text = singleData.pCoFData;
                    secondNode.Cells.Add(Potential_Cause_Mechanism_of_Failure);

                    //發生度 Occ
                    Cell Occ = new Cell();
                    Occ.Text = singleData.occurrenceData;
                    secondNode.Cells.Add(Occ);

                    //預防措施 Current Process Controls Prevention
                    Cell Current_Process_Controls_Prevention = new Cell();
                    Current_Process_Controls_Prevention.Images.Image = Properties.Resources.list_24px;
                    Current_Process_Controls_Prevention.Text = singleData.preventionData;
                    secondNode.Cells.Add(Current_Process_Controls_Prevention);

                    //預防措施檢查 Current Process Controls Detection
                    Cell Current_Process_Controls_Detection = new Cell();
                    Current_Process_Controls_Detection.Images.Image = Properties.Resources.list_24px;
                    Current_Process_Controls_Detection.Text = singleData.detectionData;
                    secondNode.Cells.Add(Current_Process_Controls_Detection);

                    //檢測度 Det
                    Cell Det = new Cell();
                    Det.Text = singleData.detData;
                    secondNode.Cells.Add(Det);

                    //風險指數 R.P.N.
                    Cell RPN = new Cell();
                    RPN.Text = singleData.rpnData;
                    secondNode.Cells.Add(RPN);

                }
                else
                {
                    //潛在失效模式 Potential Failure Mode
                    Cell Potential_Failure_Mode = new Cell();
                    Potential_Failure_Mode.Images.Image = Properties.Resources.list_24px;
                    //Potential_Failure_Mode.ImageAlignment = eCellPartAlignment.CenterTop;
                    //Potential_Failure_Mode.Layout = eCellPartLayout.Vertical;
                    Potential_Failure_Mode.Text = "N/A";
                    secondNode.Cells.Add(Potential_Failure_Mode);

                    //潛在失效模式的後果 Potential Effect of Failure
                    Cell Potential_Effect_of_Failure = new Cell();
                    Potential_Effect_of_Failure.Images.Image = Properties.Resources.list_24px;
                    Potential_Effect_of_Failure.Text = "N/A";
                    secondNode.Cells.Add(Potential_Effect_of_Failure);

                    //嚴重度 Severity
                    Cell Severity = new Cell();
                    Severity.Text = "N/A";
                    secondNode.Cells.Add(Severity);

                    //分類 Class
                    Cell Class = new Cell();
                    Class.Text = "N/A";
                    secondNode.Cells.Add(Class);

                    //造成潛在失效原因 Potential Cause/Mechanism of Failure
                    Cell Potential_Cause_Mechanism_of_Failure = new Cell();
                    Potential_Cause_Mechanism_of_Failure.Images.Image = Properties.Resources.list_24px;
                    Potential_Cause_Mechanism_of_Failure.Text = "N/A";
                    secondNode.Cells.Add(Potential_Cause_Mechanism_of_Failure);

                    //發生度 Occ
                    Cell Occ = new Cell();
                    Occ.Text = "N/A";
                    secondNode.Cells.Add(Occ);

                    //預防措施 Current Process Controls Prevention
                    Cell Current_Process_Controls_Prevention = new Cell();
                    Current_Process_Controls_Prevention.Images.Image = Properties.Resources.list_24px;
                    Current_Process_Controls_Prevention.Text = "N/A";
                    secondNode.Cells.Add(Current_Process_Controls_Prevention);

                    //預防措施檢查 Current Process Controls Detection
                    Cell Current_Process_Controls_Detection = new Cell();
                    Current_Process_Controls_Detection.Images.Image = Properties.Resources.list_24px;
                    Current_Process_Controls_Detection.Text = "N/A";
                    secondNode.Cells.Add(Current_Process_Controls_Detection);

                    //檢測度 Det
                    Cell Det = new Cell();
                    Det.Text = "N/A";
                    secondNode.Cells.Add(Det);

                    //風險指數 R.P.N.
                    Cell RPN = new Cell();
                    RPN.Text = "N/A";
                    secondNode.Cells.Add(RPN);
                }


                

                /*
                //潛在失效模式 Potential Failure Mode
                Cell Potential_Failure_Mode = new Cell();
                Potential_Failure_Mode.HostedItem = new ButtonItem();
                Potential_Failure_Mode.HostedItem.Name = "Potential Failure Mode";
                Potential_Failure_Mode.HostedItem.Text = "潛在失效模式";
                //在Tag中加入Op1、Op2、Balloon方便點擊按鈕時，有資料可以填入對話框
                Potential_Failure_Mode.HostedItem.Tag = node1.Cells[0].Text + "," + node1.Cells[1].Text + "," + singleDimen.ballon.ToString();
                Potential_Failure_Mode.HostedItem.Click += new EventHandler(Cell_Button_Click);
                secondNode.Cells.Add(Potential_Failure_Mode);

                //潛在失效模式的後果 Potential Effect of Failure
                Cell Potential_Effect_of_Failure = new Cell();
                Potential_Effect_of_Failure.HostedItem = new ButtonItem();
                Potential_Effect_of_Failure.HostedItem.Name = "Potential Effect of Failure";
                Potential_Effect_of_Failure.HostedItem.Text = "潛在失效影響";
                //在Tag中加入Op1、Op2、Balloon方便點擊按鈕時，有資料可以填入對話框
                Potential_Effect_of_Failure.HostedItem.Tag = node1.Cells[0].Text + "," + node1.Cells[1].Text + "," + singleDimen.ballon.ToString();
                Potential_Effect_of_Failure.HostedItem.Click += new EventHandler(Cell_Button_Click);
                secondNode.Cells.Add(Potential_Effect_of_Failure);

                //嚴重度 Severity
                Cell Severity = new Cell();
                Severity.HostedItem = new ButtonItem();
                Severity.HostedItem.Name = "Severity";
                Severity.HostedItem.Text = "嚴重度";
                //在Tag中加入Op1、Op2、Balloon方便點擊按鈕時，有資料可以填入對話框
                Severity.HostedItem.Tag = node1.Cells[0].Text + "," + node1.Cells[1].Text + "," + singleDimen.ballon.ToString();
                Severity.HostedItem.Click += new EventHandler(Cell_Button_Click);
                secondNode.Cells.Add(Severity);

                //分類 Class
                Cell Class = new Cell();
                Class.HostedItem = new ButtonItem();
                Class.HostedItem.Name = "Class";
                Class.HostedItem.Text = "分類";
                //在Tag中加入Op1、Op2、Balloon方便點擊按鈕時，有資料可以填入對話框
                Class.HostedItem.Tag = node1.Cells[0].Text + "," + node1.Cells[1].Text + "," + singleDimen.ballon.ToString();
                Class.HostedItem.Click += new EventHandler(Cell_Button_Click);
                secondNode.Cells.Add(Class);

                //造成潛在失效原因 Potential Cause/Mechanism of Failure
                Cell Potential_Cause_Mechanism_of_Failure = new Cell();
                Potential_Cause_Mechanism_of_Failure.HostedItem = new ButtonItem();
                Potential_Cause_Mechanism_of_Failure.HostedItem.Name = "Potential Cause/Mechanism of Failure";
                Potential_Cause_Mechanism_of_Failure.HostedItem.Text = "造成潛在失效原因";
                //在Tag中加入Op1、Op2、Balloon方便點擊按鈕時，有資料可以填入對話框
                Potential_Cause_Mechanism_of_Failure.HostedItem.Tag = node1.Cells[0].Text + "," + node1.Cells[1].Text + "," + singleDimen.ballon.ToString();
                Potential_Cause_Mechanism_of_Failure.HostedItem.Click += new EventHandler(Cell_Button_Click);
                secondNode.Cells.Add(Potential_Cause_Mechanism_of_Failure);

                //發生度 Occ
                Cell Occ = new Cell();
                Occ.HostedItem = new ButtonItem();
                Occ.HostedItem.Name = "Occ";
                Occ.HostedItem.Text = "發生度";
                //在Tag中加入Op1、Op2、Balloon方便點擊按鈕時，有資料可以填入對話框
                Occ.HostedItem.Tag = node1.Cells[0].Text + "," + node1.Cells[1].Text + "," + singleDimen.ballon.ToString();
                Occ.HostedItem.Click += new EventHandler(Cell_Button_Click);
                secondNode.Cells.Add(Occ);

                //預防措施 Current Process Controls Prevention
                Cell Current_Process_Controls_Prevention = new Cell();
                Current_Process_Controls_Prevention.HostedItem = new ButtonItem();
                Current_Process_Controls_Prevention.HostedItem.Name = "Current Process Controls Prevention";
                Current_Process_Controls_Prevention.HostedItem.Text = "預防措施";
                //在Tag中加入Op1、Op2、Balloon方便點擊按鈕時，有資料可以填入對話框
                Current_Process_Controls_Prevention.HostedItem.Tag = node1.Cells[0].Text + "," + node1.Cells[1].Text + "," + singleDimen.ballon.ToString();
                Current_Process_Controls_Prevention.HostedItem.Click += new EventHandler(Cell_Button_Click);
                secondNode.Cells.Add(Current_Process_Controls_Prevention);

                //預防措施檢查 Current Process Controls Detection
                Cell Current_Process_Controls_Detection = new Cell();
                Current_Process_Controls_Detection.HostedItem = new ButtonItem();
                Current_Process_Controls_Detection.HostedItem.Name = "Current Process Controls Detection";
                Current_Process_Controls_Detection.HostedItem.Text = "預防措施檢查";
                //在Tag中加入Op1、Op2、Balloon方便點擊按鈕時，有資料可以填入對話框
                Current_Process_Controls_Detection.HostedItem.Tag = node1.Cells[0].Text + "," + node1.Cells[1].Text + "," + singleDimen.ballon.ToString();
                Current_Process_Controls_Detection.HostedItem.Click += new EventHandler(Cell_Button_Click);
                secondNode.Cells.Add(Current_Process_Controls_Detection);

                //檢測度 Det
                Cell Det = new Cell();
                Det.HostedItem = new ButtonItem();
                Det.HostedItem.Name = "Det";
                Det.HostedItem.Text = "檢測度";
                //在Tag中加入Op1、Op2、Balloon方便點擊按鈕時，有資料可以填入對話框
                Det.HostedItem.Tag = node1.Cells[0].Text + "," + node1.Cells[1].Text + "," + singleDimen.ballon.ToString();
                Det.HostedItem.Click += new EventHandler(Cell_Button_Click);
                secondNode.Cells.Add(Det);

                //風險指數 R.P.N.
                Cell RPN = new Cell();
                RPN.HostedItem = new ButtonItem();
                RPN.HostedItem.Name = "R.P.N.";
                RPN.HostedItem.Text = "風險指數";
                //在Tag中加入Op1、Op2、Balloon方便點擊按鈕時，有資料可以填入對話框
                RPN.HostedItem.Tag = node1.Cells[0].Text + "," + node1.Cells[1].Text + "," + singleDimen.ballon.ToString();
                RPN.HostedItem.Click += new EventHandler(Cell_Button_Click);
                secondNode.Cells.Add(RPN);
                */

                node1.Nodes.Add(secondNode);
            }
            catch (System.Exception ex)
            {
                MessageBox.Show(ex.ToString());
                return false;
            }
            return true;
        }
        public bool CreateHTML(Com_Dimension singleDimen, out string htmlPath)
        {
            htmlPath = "";
            try
            {
                string descriptionText = "<html><body style='margin:0px;background-color:rgb(255,255,192)'><font color='black' size='2.5'>"
                    + CaxExcel.GetDimenUnicode(singleDimen) + 
                    "</font></body></html>";
                htmlPath = string.Format(@"{0}temp.html", System.AppDomain.CurrentDomain.BaseDirectory);
                System.IO.File.WriteAllText(htmlPath, descriptionText, Encoding.Unicode);
            }
            catch (System.Exception ex)
            {
                MessageBox.Show(ex.ToString());
                return false;
            }
            return true;
        }
        public static string GetDimenUnicode(Com_Dimension singleDimen)
        {
            string dimenUnicode = "";
            try
            {
                #region 幾何公差
                if (singleDimen.characteristic != "" & singleDimen.characteristic != null & singleDimen.characteristic != "None")
                {
                    dimenUnicode = TransUni(singleDimen.characteristic);
                    dimenUnicode = dimenUnicode + "|";
                }
                if (singleDimen.zoneShape != "" & singleDimen.zoneShape != null & singleDimen.zoneShape != "None")
                {
                    dimenUnicode = dimenUnicode + TransUni(singleDimen.zoneShape);
                }
                if (singleDimen.toleranceValue != "" & singleDimen.toleranceValue != null & singleDimen.toleranceValue != "None")
                {
                    dimenUnicode = dimenUnicode + singleDimen.toleranceValue;
                }
                if (singleDimen.materialModifier != "" & singleDimen.materialModifier != null & singleDimen.materialModifier != "None")
                {
                    dimenUnicode = dimenUnicode + TransUni(singleDimen.materialModifier);
                }
                if (singleDimen.primaryDatum != "" & singleDimen.primaryDatum != null & singleDimen.primaryDatum != "None")
                {
                    dimenUnicode = dimenUnicode + "|";
                    dimenUnicode = dimenUnicode + singleDimen.primaryDatum;
                    if (singleDimen.primaryMaterialModifier != "" & singleDimen.primaryMaterialModifier != null & singleDimen.primaryMaterialModifier != "None")
                    {
                    }
                    else
                    {
                        dimenUnicode = dimenUnicode + "|";
                    }
                }
                if (singleDimen.primaryMaterialModifier != "" & singleDimen.primaryMaterialModifier != null & singleDimen.primaryMaterialModifier != "None")
                {
                    dimenUnicode = dimenUnicode + TransUni(singleDimen.primaryMaterialModifier);
                }
                if (singleDimen.secondaryDatum != "" & singleDimen.secondaryDatum != null & singleDimen.secondaryDatum != "None")
                {
                    dimenUnicode = dimenUnicode + "|";
                    dimenUnicode = dimenUnicode + singleDimen.secondaryDatum;
                    if (singleDimen.secondaryMaterialModifier != "" & singleDimen.secondaryMaterialModifier != null & singleDimen.secondaryMaterialModifier != "None")
                    {
                    }
                    else
                    {
                        dimenUnicode = dimenUnicode + "|";
                    }
                }
                if (singleDimen.secondaryMaterialModifier != "" & singleDimen.secondaryMaterialModifier != null & singleDimen.secondaryMaterialModifier != "None")
                {
                    dimenUnicode = dimenUnicode + TransUni(singleDimen.secondaryMaterialModifier);
                }
                if (singleDimen.tertiaryDatum != "" & singleDimen.tertiaryDatum != null & singleDimen.tertiaryDatum != "None")
                {
                    dimenUnicode = dimenUnicode + "|";
                    dimenUnicode = dimenUnicode + singleDimen.tertiaryDatum;
                    if (singleDimen.tertiaryMaterialModifier != "" & singleDimen.tertiaryMaterialModifier != null & singleDimen.tertiaryMaterialModifier != "None")
                    {
                    }
                    else
                    {
                        dimenUnicode = dimenUnicode + "|";
                    }
                }
                if (singleDimen.tertiaryMaterialModifier != "" & singleDimen.tertiaryMaterialModifier != null & singleDimen.tertiaryMaterialModifier != "None")
                {
                    dimenUnicode = dimenUnicode + TransUni(singleDimen.tertiaryMaterialModifier);
                }
                #endregion

                #region 尺寸公差
                if (singleDimen.aboveText != "" & singleDimen.aboveText != null & singleDimen.aboveText != "None")
                {
                    dimenUnicode = dimenUnicode + singleDimen.aboveText;
                }
                if (singleDimen.beforeText != "" & singleDimen.beforeText != null & singleDimen.beforeText != "None")
                {
                    dimenUnicode = dimenUnicode + singleDimen.beforeText;
                }
                if (singleDimen.toleranceSymbol != "" & singleDimen.toleranceSymbol != null & singleDimen.toleranceSymbol != "None")
                {

                    if (singleDimen.toleranceSymbol == "R")
                    {
                        dimenUnicode = dimenUnicode + singleDimen.toleranceSymbol;
                    }
                    else
                    {
                        dimenUnicode = dimenUnicode + TransUni(singleDimen.toleranceSymbol);
                    }
                }
                if (singleDimen.mainText != "" & singleDimen.mainText != null & singleDimen.mainText != "None")
                {
                    char[] splitText = singleDimen.mainText.ToCharArray();
                    for (int i = 0; i < splitText.Length; i++)
                    {
                        if (splitText[i] == '~')
                        {
                            dimenUnicode = dimenUnicode + TransUni(splitText[i].ToString());
                        }
                        else
                        {
                            dimenUnicode = dimenUnicode + splitText[i].ToString();
                        }
                    }
                    //dimenUnicode = dimenUnicode + singleDimen.mainText;
                }
                if ((singleDimen.upTolerance != "" & singleDimen.upTolerance != null & singleDimen.upTolerance != "None") ||
                    (singleDimen.lowTolerance != "" & singleDimen.upTolerance != null & singleDimen.upTolerance != "None"))
                {
                    if (singleDimen.upTolerance == singleDimen.lowTolerance)
                    {
                        //如果上下公差相同，則加入±到字串中
                        dimenUnicode = dimenUnicode + "±";
                        //加入公差的長度
                        char[] splitText = singleDimen.upTolerance.ToCharArray();
                        for (int i = 0; i < splitText.Length; i++)
                        {
                            if (splitText[i] == '~')
                            {
                                dimenUnicode = dimenUnicode + TransUni(splitText[i].ToString());
                            }
                            else
                            {
                                dimenUnicode = dimenUnicode + splitText[i].ToString();
                            }
                        }
                        //dimenUnicode = dimenUnicode + singleDimen.upTolerance;
                    }
                    else
                    {
                        if (Math.Abs(Convert.ToDouble(singleDimen.upTolerance)) * 10000 > 0)
                        {
                            //表示有上公差，所以加入+到字串中，如果上公差為-，則不加+
                            if (singleDimen.upTolerance.Contains('-'))
                            {
                            }
                            else
                            {
                                dimenUnicode = dimenUnicode + "+";
                            }

                            //加入公差的長度
                            char[] splitText = singleDimen.upTolerance.ToCharArray();
                            for (int i = 0; i < splitText.Length; i++)
                            {
                                if (splitText[i] == '~')
                                {
                                    dimenUnicode = dimenUnicode + TransUni(splitText[i].ToString());
                                }
                                else
                                {
                                    dimenUnicode = dimenUnicode + splitText[i].ToString();
                                }
                            }
                            //dimenUnicode = dimenUnicode + singleDimen.upTolerance;
                        }
                        if (Convert.ToDouble(singleDimen.lowTolerance) * 10000 > 0)
                        {
                            dimenUnicode = dimenUnicode + "/";
                            //表示有下公差，所以加入-到字串中，如果下公差為+，則不加-
                            if (singleDimen.lowTolerance.Contains('+'))
                            {
                            }
                            else
                            {
                                dimenUnicode = dimenUnicode + "-";
                            }

                            //加入下公差的長度
                            char[] splitText = singleDimen.lowTolerance.ToCharArray();
                            for (int i = 0; i < splitText.Length; i++)
                            {
                                if (splitText[i] == '~')
                                {
                                    dimenUnicode = dimenUnicode + TransUni(splitText[i].ToString());
                                }
                                else
                                {
                                    dimenUnicode = dimenUnicode + splitText[i].ToString();
                                }
                            }
                            //dimenUnicode = dimenUnicode + singleDimen.lowTolerance;
                        }
                    }
                }
                if (singleDimen.x != "" & singleDimen.x != null & singleDimen.x != "None")
                {
                    dimenUnicode = dimenUnicode + singleDimen.x;
                }
                if (singleDimen.chamferAngle != "" & singleDimen.chamferAngle != null & singleDimen.chamferAngle != "None")
                {
                    char[] splitText = singleDimen.chamferAngle.ToCharArray();
                    for (int i = 0; i < splitText.Length; i++)
                    {
                        if (splitText[i] == '~')
                        {
                            dimenUnicode = dimenUnicode + TransUni(splitText[i].ToString());
                        }
                        else
                        {
                            dimenUnicode = dimenUnicode + splitText[i].ToString();
                        }
                    }
                }
                if (singleDimen.afterText != "" & singleDimen.afterText != null & singleDimen.afterText != "None")
                {
                    char[] splitText = singleDimen.afterText.ToCharArray();
                    for (int i = 0; i < splitText.Length; i++)
                    {
                        if (splitText[i] == '~')
                        {
                            dimenUnicode = dimenUnicode + TransUni(splitText[i].ToString());
                        }
                        else
                        {
                            dimenUnicode = dimenUnicode + splitText[i].ToString();
                        }
                    }
                }
                #endregion
                return dimenUnicode;
            }
            catch (System.Exception ex)
            {
                return dimenUnicode = "";
            }
        }
        /*
        public string GetDimenUnicode(Com_Dimension singleDimen)
        {
            string dimenUnicode = "";
            try
            {
                #region 幾何公差
                if (singleDimen.characteristic != "" & singleDimen.characteristic != null & singleDimen.characteristic != "None")
                {
                    dimenUnicode = TransUni(singleDimen.characteristic);
                    dimenUnicode = dimenUnicode + "|";
                }
                if (singleDimen.zoneShape != "" & singleDimen.zoneShape != null & singleDimen.zoneShape != "None")
                {
                    dimenUnicode = dimenUnicode + TransUni(singleDimen.zoneShape);
                }
                if (singleDimen.toleranceValue != "" & singleDimen.toleranceValue != null & singleDimen.toleranceValue != "None")
                {
                    dimenUnicode = dimenUnicode + singleDimen.toleranceValue;
                }
                if (singleDimen.materialModifier != "" & singleDimen.materialModifier != null & singleDimen.materialModifier != "None")
                {
                    dimenUnicode = dimenUnicode + TransUni(singleDimen.materialModifier);
                }
                if (singleDimen.primaryDatum != "" & singleDimen.primaryDatum != null & singleDimen.primaryDatum != "None")
                {
                    dimenUnicode = dimenUnicode + "|";
                    dimenUnicode = dimenUnicode + singleDimen.primaryDatum;
                    if (singleDimen.primaryMaterialModifier != "" & singleDimen.primaryMaterialModifier != null & singleDimen.primaryMaterialModifier != "None")
                    {
                    }
                    else
                    {
                        dimenUnicode = dimenUnicode + "|";
                    }
                }
                if (singleDimen.primaryMaterialModifier != "" & singleDimen.primaryMaterialModifier != null & singleDimen.primaryMaterialModifier != "None")
                {
                    dimenUnicode = dimenUnicode + TransUni(singleDimen.primaryMaterialModifier);
                }
                if (singleDimen.secondaryDatum != "" & singleDimen.secondaryDatum != null & singleDimen.secondaryDatum != "None")
                {
                    dimenUnicode = dimenUnicode + "|";
                    dimenUnicode = dimenUnicode + singleDimen.secondaryDatum;
                    if (singleDimen.secondaryMaterialModifier != "" & singleDimen.secondaryMaterialModifier != null & singleDimen.secondaryMaterialModifier != "None")
                    {
                    }
                    else
                    {
                        dimenUnicode = dimenUnicode + "|";
                    }
                }
                if (singleDimen.secondaryMaterialModifier != "" & singleDimen.secondaryMaterialModifier != null & singleDimen.secondaryMaterialModifier != "None")
                {
                    dimenUnicode = dimenUnicode + TransUni(singleDimen.secondaryMaterialModifier);
                }
                if (singleDimen.tertiaryDatum != "" & singleDimen.tertiaryDatum != null & singleDimen.tertiaryDatum != "None")
                {
                    dimenUnicode = dimenUnicode + "|";
                    dimenUnicode = dimenUnicode + singleDimen.tertiaryDatum;
                    if (singleDimen.tertiaryMaterialModifier != "" & singleDimen.tertiaryMaterialModifier != null & singleDimen.tertiaryMaterialModifier != "None")
                    {
                    }
                    else
                    {
                        dimenUnicode = dimenUnicode + "|";
                    }
                }
                if (singleDimen.tertiaryMaterialModifier != "" & singleDimen.tertiaryMaterialModifier != null & singleDimen.tertiaryMaterialModifier != "None")
                {
                    dimenUnicode = dimenUnicode + TransUni(singleDimen.tertiaryMaterialModifier);
                }
                #endregion

                #region 尺寸公差
                if (singleDimen.aboveText != "" & singleDimen.aboveText != null & singleDimen.aboveText != "None")
                {
                    dimenUnicode = dimenUnicode + singleDimen.aboveText;
                }
                if (singleDimen.beforeText != "" & singleDimen.beforeText != null & singleDimen.beforeText != "None")
                {
                    dimenUnicode = dimenUnicode + singleDimen.beforeText;
                }
                if (singleDimen.toleranceSymbol != "" & singleDimen.toleranceSymbol != null & singleDimen.toleranceSymbol != "None")
                {
                    
                    if (singleDimen.toleranceSymbol == "R")
                    {
                        dimenUnicode = dimenUnicode + singleDimen.toleranceSymbol;
                    }
                    else
                    {
                        dimenUnicode = dimenUnicode + TransUni(singleDimen.toleranceSymbol);
                    }
                }
                if (singleDimen.mainText != "" & singleDimen.mainText != null & singleDimen.mainText != "None")
                {
                    char[] splitText = singleDimen.mainText.ToCharArray();
                    for (int i = 0; i < splitText.Length; i++)
                    {
                        if (splitText[i] == '~')
                        {
                            dimenUnicode = dimenUnicode + TransUni(splitText[i].ToString());
                        }
                        else
                        {
                            dimenUnicode = dimenUnicode + splitText[i].ToString();
                        }
                    }
                    //dimenUnicode = dimenUnicode + singleDimen.mainText;
                }
                if ((singleDimen.upTolerance != "" & singleDimen.upTolerance != null & singleDimen.upTolerance != "None") ||
                    (singleDimen.lowTolerance != "" & singleDimen.upTolerance != null & singleDimen.upTolerance != "None"))
                {
                    if (singleDimen.upTolerance == singleDimen.lowTolerance)
                    {
                        //如果上下公差相同，則加入±到字串中
                        dimenUnicode = dimenUnicode + "±";
                        //加入公差的長度
                        char[] splitText = singleDimen.upTolerance.ToCharArray();
                        for (int i = 0; i < splitText.Length; i++)
                        {
                            if (splitText[i] == '~')
                            {
                                dimenUnicode = dimenUnicode + TransUni(splitText[i].ToString());
                            }
                            else
                            {
                                dimenUnicode = dimenUnicode + splitText[i].ToString();
                            }
                        }
                        //dimenUnicode = dimenUnicode + singleDimen.upTolerance;
                    }
                    else
                    {
                        if (Math.Abs(Convert.ToDouble(singleDimen.upTolerance)) * 10000 > 0)
                        {
                            //表示有上公差，所以加入+到字串中，如果上公差為-，則不加+
                            if (singleDimen.upTolerance.Contains('-'))
                            {
                            }
                            else
                            {
                                dimenUnicode = dimenUnicode + "+";
                            }
                            
                            //加入公差的長度
                            char[] splitText = singleDimen.upTolerance.ToCharArray();
                            for (int i = 0; i < splitText.Length; i++)
                            {
                                if (splitText[i] == '~')
                                {
                                    dimenUnicode = dimenUnicode + TransUni(splitText[i].ToString());
                                }
                                else
                                {
                                    dimenUnicode = dimenUnicode + splitText[i].ToString();
                                }
                            }
                            //dimenUnicode = dimenUnicode + singleDimen.upTolerance;
                        }
                        if (Convert.ToDouble(singleDimen.lowTolerance) * 10000 > 0)
                        {
                            dimenUnicode = dimenUnicode + "/";
                            //表示有下公差，所以加入-到字串中，如果下公差為+，則不加-
                            if (singleDimen.lowTolerance.Contains('+'))
                            {
                            }
                            else
                            {
                                dimenUnicode = dimenUnicode + "-";
                            }
                            
                            //加入下公差的長度
                            char[] splitText = singleDimen.lowTolerance.ToCharArray();
                            for (int i = 0; i < splitText.Length; i++)
                            {
                                if (splitText[i] == '~')
                                {
                                    dimenUnicode = dimenUnicode + TransUni(splitText[i].ToString());
                                }
                                else
                                {
                                    dimenUnicode = dimenUnicode + splitText[i].ToString();
                                }
                            }
                            //dimenUnicode = dimenUnicode + singleDimen.lowTolerance;
                        }
                    }
                }
                if (singleDimen.x != "" & singleDimen.x != null & singleDimen.x != "None")
                {
                    dimenUnicode = dimenUnicode + singleDimen.x;
                }
                if (singleDimen.chamferAngle != "" & singleDimen.chamferAngle != null & singleDimen.chamferAngle != "None")
                {
                    char[] splitText = singleDimen.chamferAngle.ToCharArray();
                    for (int i = 0; i < splitText.Length; i++)
                    {
                        if (splitText[i] == '~')
                        {
                            dimenUnicode = dimenUnicode + TransUni(splitText[i].ToString());
                        }
                        else
                        {
                            dimenUnicode = dimenUnicode + splitText[i].ToString();
                        }
                    }
                }
                if (singleDimen.afterText != "" & singleDimen.afterText != null & singleDimen.afterText != "None")
                {
                    char[] splitText = singleDimen.afterText.ToCharArray();
                    for (int i = 0; i < splitText.Length; i++)
                    {
                        if (splitText[i] == '~')
                        {
                            dimenUnicode = dimenUnicode + TransUni(splitText[i].ToString());
                        }
                        else
                        {
                            dimenUnicode = dimenUnicode + splitText[i].ToString();
                        }
                    }
                }
                #endregion
                return dimenUnicode;
            }
            catch (System.Exception ex)
            {
                return dimenUnicode = "";
            }
        }
        */
        public static string TransUni(string inputStr)
        {
            string outputStr = "";
            try
            {
                if (inputStr == "a")
                {
                    outputStr = "\u2220";
                }
                else if (inputStr == "b")
                {
                    outputStr = "\u27C2";
                }
                else if (inputStr == "c")
                {
                    outputStr = "\u23E4";
                }
                else if (inputStr == "d")
                {
                    outputStr = "\u2313";
                }
                else if (inputStr == "e")
                {
                    outputStr = "\u25EF";
                }
                else if (inputStr == "f")
                {
                    outputStr = "\u2225";
                }
                else if (inputStr == "g")
                {
                    outputStr = "\u232D";
                }
                else if (inputStr == "h")
                {
                    outputStr = "\u2197";
                }
                else if (inputStr == "i")
                {
                    outputStr = "\u232F";
                }
                else if (inputStr == "j")
                {
                    outputStr = "\u2316";
                }
                else if (inputStr == "k")
                {
                    outputStr = "\u2312";
                }
                else if (inputStr == "l")
                {
                    outputStr = "\u24C1";
                }
                else if (inputStr == "m")
                {
                    outputStr = "\u24C2";
                }
                else if (inputStr == "n")
                {
                    outputStr = "\u00D8";
                }
                else if (inputStr == "o")
                {
                    outputStr = "\u25A1";
                }
                else if (inputStr == "p")
                {
                    outputStr = "\u24C5";
                }
                else if (inputStr == "q")
                {
                    outputStr = "";
                }
                else if (inputStr == "r")
                {
                    outputStr = "\u25CE";
                }
                else if (inputStr == "s")
                {
                    outputStr = "\u24C8";
                }
                else if (inputStr == "t")
                {
                    outputStr = "\u2330";
                }
                else if (inputStr == "u")
                {
                    outputStr = "\u23E5";
                }
                else if (inputStr == "v")
                {
                    outputStr = "\u2334";
                }
                else if (inputStr == "w")
                {
                    outputStr = "\u2335";
                }
                else if (inputStr == "x")
                {
                    outputStr = "\u21A7";
                }
                else if (inputStr == "y")
                {
                    outputStr = "\u2332";
                }
                else if (inputStr == "z")
                {
                    outputStr = "\u2333";
                }
                else if (inputStr == "~")
                {
                    outputStr = "\u00B0";
                }

                if (outputStr == "")
                {
                    outputStr = inputStr;
                }
                return outputStr;
            }
            catch (System.Exception ex)
            {
                return outputStr = "";
            }
        }
        /*
        public void Cell_Button_Click(object sender, EventArgs e)
        {
            MainForm.Hide();
            
            if (((ButtonItem)sender).Name == "Potential Failure Mode")
            {
                PotentialFailureMode cPotentialFailureMode = new PotentialFailureMode();
                cPotentialFailureMode.ShowDialog();
            }
            else if (((ButtonItem)sender).Name == "Potential Effect of Failure")
            {
                
            }
            else if (((ButtonItem)sender).Name == "Severity")
            {
                
            }
            else if (((ButtonItem)sender).Name == "Class")
            {

            }
            else if (((ButtonItem)sender).Name == "Potential Cause/Mechanism of Failure")
            {

            }
            else if (((ButtonItem)sender).Name == "Occ")
            {

            }
            else if (((ButtonItem)sender).Name == "Current Process Controls Prevention")
            {

            }
            else if (((ButtonItem)sender).Name == "Current Process Controls Detection")
            {

            }
            else if (((ButtonItem)sender).Name == "Det")
            {

            }
            else if (((ButtonItem)sender).Name == "R.P.N.")
            {

            }
            
            MainForm.Show();
        }
        */
        
    }
}
