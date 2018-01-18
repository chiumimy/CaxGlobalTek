using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Windows.Forms;
using DevComponents.DotNetBar;
using NHibernate;
using CaxGlobaltek;
using DevComponents.AdvTree;
using Microsoft.Office.Interop.Excel;
using System.IO;

namespace PFMEA
{
    public partial class Form1 : Office2007Form
    {
        public bool status;
        public ISession session = MyHibernateHelper.SessionFactory.OpenSession();
        public Com_PEMain comPEMain = new Com_PEMain();
        public IList<Com_PartOperation> listComPartOperation = new List<Com_PartOperation>();
        public GetOISData cGetOISData = new GetOISData();
        public NodeCollection selNodes;
        public List<string> listSysPFM = new List<string>();
        public List<string> listSysPEoF = new List<string>();
        public List<string> listSysPCoF = new List<string>();
        public List<string> listSysPrevention = new List<string>();
        public List<string> listSysDetection = new List<string>();
        //public Dictionary<Node, List<DimenData>> dicAllNode = new Dictionary<Node, List<DimenData>>();
        public static IList<Com_PFMEA> listComPFMEA = new List<Com_PFMEA>();
        

        public Form1()
        {
            InitializeComponent();
            InitializeCombo();
            
            //cGetOISData = new GetOISData(this.FindForm());

            //取得Com_PFMEA所有資料
            try
            {
                listComPFMEA = session.QueryOver<Com_PFMEA>().List();
            }
            catch (System.Exception ex)
            {
                MessageBox.Show(ex.ToString());
            }
            

            //初始化客戶名稱
            IList<Sys_Customer> listSysCustomer = session.QueryOver<Sys_Customer>().List();
            CustCombo.DisplayMember = "customerName";
            foreach (Sys_Customer i in listSysCustomer)
            {
                CustCombo.Items.Add(i);
            }

            GetDBData();
        }
        private void InitializeCombo()
        {
            try
            {
                PartNoCombo.Enabled = false;
                CusRevCombo.Enabled = false;
                OpRevCombo.Enabled = false;

                string[] num = new string[] { "", "1", "2", "3", "4", "5", "6", "7", "8", "9", "10" };
                SevCombo.Items.AddRange(num);
                OccCombo.Items.AddRange(num);
                DetCombo.Items.AddRange(num);
            }
            catch (System.Exception ex)
            {
            	
            }
        }

        private void GetDBData()
        {
            try
            {
                foreach (Sys_PFM i in session.QueryOver<Sys_PFM>().List())
                    listSysPFM.Add(i.pFMName);

                foreach (Sys_PEoF i in session.QueryOver<Sys_PEoF>().List())
                    listSysPEoF.Add(i.pEoFName);

                foreach (Sys_PCoF i in session.QueryOver<Sys_PCoF>().List())
                    listSysPCoF.Add(i.pCoFName);

                foreach (Sys_Prevention i in session.QueryOver<Sys_Prevention>().List())
                    listSysPrevention.Add(i.preventionName);

                foreach (Sys_Detection i in session.QueryOver<Sys_Detection>().List())
                    listSysDetection.Add(i.detectionName);
            }
            catch (System.Exception ex)
            {
            	
            }
        }

        private void CustCombo_SelectedIndexChanged(object sender, EventArgs e)
        {
            PartNoCombo.Enabled = true;
            PartNoCombo.Text = "";
            PartNoCombo.Items.Clear();

            CusRevCombo.Enabled = false;
            CusRevCombo.Text = "";
            CusRevCombo.Items.Clear();

            OpRevCombo.Enabled = false;
            OpRevCombo.Text = "";
            OpRevCombo.Items.Clear();


            status = GetPartNoData.SetPartNoData(((Sys_Customer)CustCombo.SelectedItem), PartNoCombo);
            if (!status)
            {
                MessageBox.Show("料號資料取得失敗，請聯繫開發工程師");
                this.Close();
            }
        }

        private void PartNoCombo_SelectedIndexChanged(object sender, EventArgs e)
        {
            CusRevCombo.Enabled = true;
            CusRevCombo.Text = "";
            CusRevCombo.Items.Clear();

            OpRevCombo.Enabled = false;
            OpRevCombo.Text = "";
            OpRevCombo.Items.Clear();


            status = GetCusRevData.SetCusVerData(((Sys_Customer)CustCombo.SelectedItem), PartNoCombo.Text, CusRevCombo);
            if (!status)
            {
                MessageBox.Show("版次資料取得失敗，請聯繫開發工程師");
                this.Close();
            }

            if (CusRevCombo.Items.Count == 1)
                CusRevCombo.Text = CusRevCombo.Items[0].ToString();
        }

        private void CusRevCombo_SelectedIndexChanged(object sender, EventArgs e)
        {
            OpRevCombo.Enabled = true;
            OpRevCombo.Text = "";
            OpRevCombo.Items.Clear();

            status = GetOpRevData.SetOpVerData(((Sys_Customer)CustCombo.SelectedItem),
                                                        PartNoCombo.Text,
                                                        CusRevCombo.Text,
                                                        OpRevCombo);
            if (!status)
            {
                MessageBox.Show("製程資料取得失敗，請聯繫開發工程師");
                this.Close();
            }

            if (OpRevCombo.Items.Count == 1)
                OpRevCombo.Text = OpRevCombo.Items[0].ToString();
        }

        private void OpRevCombo_SelectedIndexChanged(object sender, EventArgs e)
        {
            CaxLoading.RunDlg();
            OISTree.Nodes.Clear();
            comPEMain = session.QueryOver<Com_PEMain>()
                              .Where(x => x.partName == PartNoCombo.Text)
                              .Where(x => x.customerVer == CusRevCombo.Text)
                              .Where(x => x.opVer == OpRevCombo.Text)
                              .SingleOrDefault<Com_PEMain>();

            listComPartOperation = session.QueryOver<Com_PartOperation>().Where(x => x.comPEMain == comPEMain).List();

            foreach (Com_PartOperation i in listComPartOperation)
            {
                status = cGetOISData.InitializeOISTree(i, OISTree);
                if (!status)
                {
                    MessageBox.Show("製程資料取得失敗，請聯繫開發工程師");
                    CaxLoading.CloseDlg();
                    this.Close();
                    return;
                }
            }
            CaxLoading.CloseDlg();
        }

        private void OISTree_BeforeExpand(object sender, DevComponents.AdvTree.AdvTreeNodeCancelEventArgs e)
        {
            /*
            Node parent = e.Node;
            if (parent.Nodes.Count > 0) 
                return;

            //取得選取的OIS資料
            Com_PartOperation singleComPartOperation = new Com_PartOperation();
            foreach (Com_PartOperation i in listComPartOperation)
            {
                if (i.operation1 != parent.Cells[0].Text)
                    continue;

                singleComPartOperation = i;
            }

            //填入此OIS相關資料
            status = cGetOISData.InsertDimenData(singleComPartOperation, parent);
            if (!status)
            {
                MessageBox.Show("製程資料取得失敗，請聯繫開發工程師");
                this.Close();
            }
            */
        }

        private void OISTree_NodeClick(object sender, TreeNodeMouseEventArgs e)
        {
            try
            {
                //取得點選check的Node
                Node selNode = e.Node;
                //判斷此Node是否有子Node，如果有表示點選的是第一層Node；反之為第二層的Node
                if (selNode.HasChildNodes)
                {
                    if (selNode.Checked)
                        foreach (Node j in selNode.Nodes)
                            j.Checked = true;
                    else
                        foreach (Node j in selNode.Nodes)
                            j.Checked = false;
                }
            }
            catch (System.Exception ex)
            {
            	
            }
        }

        private void OK_Click(object sender, EventArgs e)
        {
            try
            {
                Dictionary<Node, List<Node>> DicSelNodes = new Dictionary<Node, List<Node>>();
                status = Fun_Common.GetSelNodes(OISTree, out DicSelNodes);
                if (!status)
                {
                    MessageBox.Show("取得對話框資料失敗，請聯繫開發工程師");
                    this.Close();
                }

                IList<Com_PFMEA> listComPFMEA = session.QueryOver<Com_PFMEA>().List();

                status = Fun_Common.DeleteDataBase(listComPFMEA, DicSelNodes);
                if (!status)
                {
                    MessageBox.Show("刪除資料庫發生錯誤，請聯繫開發工程師");
                    this.Close();
                }

                string ExcelFolder = string.Format(@"{0}\{1}_{2}_{3}"
                                                , Environment.GetFolderPath(Environment.SpecialFolder.Desktop)
                                                , PartNoCombo.Text
                                                , CusRevCombo.Text
                                                , OpRevCombo.Text);
                //建立桌面資料夾存放產生的Excel
                if (!Directory.Exists(ExcelFolder))
                    Directory.CreateDirectory(ExcelFolder);
                
                status = Fun_Common.CreatePFMEA(PartNoCombo.Text, CusRevCombo.Text, OpRevCombo.Text, DicSelNodes);
                if (!status)
                {
                    MessageBox.Show("輸出P-FMEA發生錯誤，請聯繫開發工程師");
                    this.Close();
                }

                MessageBox.Show("輸出成功");
                this.Close();

                /*
                status = GetAllNodes(out dicAllNode);
                if (!status)
                {
                    MessageBox.Show("取得對話框資料失敗，請聯繫開發工程師");
                    this.Close();
                }

                IList<Com_PFMEA> listComPFMEA = session.QueryOver<Com_PFMEA>().List();

                foreach (KeyValuePair<Node,List<DimenData>> kvp in dicAllNode)
                {
                    foreach (DimenData i in kvp.Value)
                    {
                        //刪除舊資料
                        foreach (Com_PFMEA j in listComPFMEA)
                        {
                            if (j.comDimension.dimensionSrNo == ((Com_Dimension)i.node.Tag).dimensionSrNo)
                                session.Delete(j);

                            session.BeginTransaction().Commit();
                        }
                        Com_PFMEA cCom_PFMEA = new Com_PFMEA();
                        cCom_PFMEA.comDimension = (Com_Dimension)i.node.Tag;
                        cCom_PFMEA.pFMData = i.node.Cells[2].Text;
                        cCom_PFMEA.pEoFData = i.node.Cells[3].Text;
                        cCom_PFMEA.sevData = i.node.Cells[4].Text;
                        cCom_PFMEA.classData = i.node.Cells[5].Text;
                        cCom_PFMEA.pCoFData = i.node.Cells[6].Text;
                        cCom_PFMEA.occurrenceData = i.node.Cells[7].Text;
                        cCom_PFMEA.preventionData = i.node.Cells[8].Text;
                        cCom_PFMEA.detectionData = i.node.Cells[9].Text;
                        cCom_PFMEA.detData = i.node.Cells[10].Text;
                        cCom_PFMEA.rpnData = i.node.Cells[11].Text;
                        using (ITransaction trans = session.BeginTransaction())
                        {
                            session.Save(cCom_PFMEA);
                            trans.Commit();
                        }
                    }
                }
                this.Close();
                */
            }
            catch (System.Exception ex)
            {
                MessageBox.Show(ex.ToString());
                //MessageBox.Show("取得對話框資料失敗，請聯繫開發工程師");
                this.Close();
            }
        }

        private void Cancel_Click(object sender, EventArgs e)
        {
            this.Close();
        }

        private void OISTree_NodeMouseUp(object sender, TreeNodeMouseEventArgs e)
        {
            try
            {
                selNodes = OISTree.SelectedNodes;
            }
            catch (System.Exception ex)
            {
            	
            }
        }

        /*
        private bool GetAllNodes(out Dictionary<Node, List<DimenData>> dicAllNode)
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
        */

        //潛在失效模式
        private void PFM_Click(object sender, EventArgs e)
        {
            try
            {
                if (selNodes.Count == 0)
                    return;

                PotentialFailureMode cPotentialFailureMode = new PotentialFailureMode(listSysPFM, selNodes);
                cPotentialFailureMode.ShowDialog();
                
            }
            catch (System.Exception ex)
            {
            	
            }
        }

        //潛在失效影響
        private void PEoF_Click(object sender, EventArgs e)
        {
            try
            {
                if (selNodes.Count == 0)
                    return;

                PotentialEffectOfFailure cPotentialEffectOfFailure = new PotentialEffectOfFailure(listSysPEoF, selNodes);
                cPotentialEffectOfFailure.ShowDialog();

            }
            catch (System.Exception ex)
            {

            }
        }

        //Class
        private void Class_Click(object sender, EventArgs e)
        {
            try
            {
                if (selNodes.Count == 0)
                    return;

                ClassDlg cClassDlg = new ClassDlg();
                cClassDlg.ShowDialog();
            }
            catch (System.Exception ex)
            {
            	
            }
        }

        //潛在失效原因
        private void PCoF_Click(object sender, EventArgs e)
        {
            try
            {
                if (selNodes.Count == 0)
                    return;

                PotentialCauseOfFailure cPotentialCauseOfFailure = new PotentialCauseOfFailure(listSysPCoF, selNodes);
                cPotentialCauseOfFailure.ShowDialog();
            }
            catch (System.Exception ex)
            {
            	
            }
        }

        //預防措施
        private void CPCP_Click(object sender, EventArgs e)
        {
            try
            {
                if (selNodes.Count == 0)
                    return;

                Prevention cPrevention = new Prevention(listSysPrevention, selNodes);
                cPrevention.ShowDialog();
            }
            catch (System.Exception ex)
            {
            	
            }
        }

        //預防措施檢測
        private void CPCD_Click(object sender, EventArgs e)
        {
            try
            {
                if (selNodes.Count == 0)
                    return;

                Detection cDetection = new Detection(listSysDetection, selNodes);
                cDetection.ShowDialog();
            }
            catch (System.Exception ex)
            {
            	
            }
        }

        //指數設定
        private void IndexSetting_Click(object sender, EventArgs e)
        {
            try
            {
                foreach (Node i in selNodes)
                {
                    //填Sev
                    if (SevCombo.Text != "")
                    {
                        i.Cells[5].Text = SevCombo.Text;
                    }
                    //填Occ
                    if (OccCombo.Text != "")
                    {
                        i.Cells[8].Text = OccCombo.Text;
                    }
                    //填Det
                    if (DetCombo.Text != "")
                    {
                        i.Cells[11].Text = DetCombo.Text;
                    }

                    //計算R.P.N.
                    if (SevCombo.Text != "" & OccCombo.Text != "" & DetCombo.Text != "")
                    {
                        i.Cells[12].Text = (Convert.ToInt32(SevCombo.Text) * Convert.ToInt32(OccCombo.Text) * Convert.ToInt32(DetCombo.Text)).ToString();
                    }
                }

                SevCombo.Text = "";
                OccCombo.Text = "";
                DetCombo.Text = "";

            }
            catch (System.Exception ex)
            {
            	
            }
        }
        
    }
}
