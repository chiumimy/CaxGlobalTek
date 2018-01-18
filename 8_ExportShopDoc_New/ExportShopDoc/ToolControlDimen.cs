using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Windows.Forms;
using DevComponents.DotNetBar.SuperGrid;
using CaxGlobaltek;
using NHibernate;
using DevComponents.AdvTree;
using NXOpen;

namespace ExportShopDoc
{
    public partial class ToolControlDimen : DevComponents.DotNetBar.Office2007Form
    {
        public static ISession session = MyHibernateHelper.SessionFactory.OpenSession();
        public static Session theSession = Session.GetSession();
        public static Com_PartOperation comPartOperation;
        public static IList<Com_MEMain> comMEMain;
        public static IList<Com_Dimension> comDimension;
        public static GridPanel panel;
        public NodeCollection selNodes;
        public Com_PEMain comPEMain = new Com_PEMain();
        public static Form parentForm = new Form();
        public static bool Is_BigDlg = false;
        public static int CurrentRowIndex = -1;
        public static string CurrentSelOperName = "";
        public static List<string> ListSelOper = new List<string>();
        public ToolControlDimen(Form pForm, PartInfo partInfo, Dictionary<string, List<string>> DicToolNoControl)
        {
            InitializeComponent();
            //將父層的Form傳進來
            parentForm = pForm;
            panel = SGC_ToolPath.PrimaryGrid;

            //初始對話框大小
            Size DlgSize = new Size(367, 491);
            this.Size = DlgSize;

            InitialLabel(partInfo, DicToolNoControl);
        }

        private void InitialLabel(PartInfo sPartInfo, Dictionary<string, List<string>> DicToolNoControl)
        {
            try
            {
                //設定基礎資料資訊
                CusName.Text = sPartInfo.CusName;
                PartNo.Text = sPartInfo.PartNo;
                CusRev.Text = sPartInfo.CusRev;
                OpRev.Text = sPartInfo.OpRev;
                OIS.Text = sPartInfo.OpNum;
                ToolNoCombo.Items.AddRange(DicToolNoControl.Keys.ToArray());

                //由料號取得DB中PEMain的資訊
                comPEMain = session.QueryOver<Com_PEMain>()
                    .Where(x => x.partName == sPartInfo.PartNo)
                    .And(x => x.customerVer == sPartInfo.CusRev)
                    .And(x => x.opVer == sPartInfo.OpRev)
                    .SingleOrDefault<Com_PEMain>();

                //由PEMain與OIS取得DB中PartOperation的資訊
                comPartOperation = session.QueryOver<Com_PartOperation>()
                    .Where(x => x.comPEMain == comPEMain)
                    .And(x => x.operation1 == sPartInfo.OpNum)
                    .SingleOrDefault<Com_PartOperation>();

                //由PartOperation取得DB中DraftingVer的資訊
                comMEMain = session.QueryOver<Com_MEMain>()
                    .Where(x => x.comPartOperation == comPartOperation)
                    .List<Com_MEMain>();

                //設定圖版
                foreach (Com_MEMain i in comMEMain)
                    DraftingRev.Items.Add(i.draftingVer);

                if (DraftingRev.Items.Count == 1)
                    DraftingRev.Text = DraftingRev.Items[0].ToString();

                //設定ToolPathPanel
                foreach (KeyValuePair<string,List<string>> kvp in DicToolNoControl)
                {
                    foreach (string i in kvp.Value)
                    {
                        GridRow row = new GridRow(kvp.Key, i);
                        panel.Rows.Add(row);
                    }
                }
            }
            catch (System.Exception ex)
            {

            }
        }
        
        private void DraftingRev_SelectedIndexChanged(object sender, EventArgs e)
        {
            try
            {
                OISTree.Nodes.Clear();
                foreach (Com_MEMain i in comMEMain)
                {
                    if (DraftingRev.Text != i.draftingVer)
                        continue;

                    comDimension = session.QueryOver<Com_Dimension>().Where(x => x.comMEMain == i).OrderBy(x => x.ballon).Asc.List<Com_Dimension>();
                    break;
                }

                foreach (Com_Dimension i in comDimension)
                {
                    if (!InitializeOISTree(i, OISTree))
                    {
                        MessageBox.Show("插入尺寸資訊失敗");
                    }
                }
            }
            catch (System.Exception ex)
            {
                MessageBox.Show(ex.ToString());
            }
            
        }

        public bool InitializeOISTree(Com_Dimension inputDimen, AdvTree OISTree)
        {
            try
            {
                OISTree.BeginUpdate();

                //泡泡
                Node singleDimen = new Node();
                singleDimen.Text = inputDimen.ballon.ToString();

                foreach (Node i in OISTree.Nodes)
                {
                    if (i.Cells[0].Text == inputDimen.ballon.ToString())
                    {
                        OISTree.EndUpdate();
                        return true;
                    }
                }

                //Dimension
                Cell dimension = new Cell();
                WebBrowser tempWebBrowser = new WebBrowser();
                tempWebBrowser.Size = new Size(200, 25);
                tempWebBrowser.ScrollBarsEnabled = false;
                string htmlPath = "";
                CaxExcel.CreateHTML(inputDimen, out htmlPath);
                tempWebBrowser.Url = new Uri(htmlPath);
                dimension.HostedControl = tempWebBrowser;
                singleDimen.Cells.Add(dimension);

                //刀號 
                Cell toolNo = new Cell();
                toolNo.Text = inputDimen.toolNoControl;
                singleDimen.Cells.Add(toolNo);

                OISTree.Nodes.Add(singleDimen);

                OISTree.EndUpdate();
            }
            catch (System.Exception ex)
            {
                return false;
            }
            return true;
        }

        private void buttonX1_Click(object sender, EventArgs e)
        {
            try
            {
                foreach (Node i in OISTree.Nodes)
                {
                    foreach (Com_Dimension j in comDimension)
                    {
                        if (j.ballon.ToString() != i.Cells[0].Text)
                        {
                            continue;
                        }
                        j.toolNoControl = i.Cells[2].Text;
                        using (ITransaction trans = session.BeginTransaction())
                        {
                            session.Update(j);
                            trans.Commit();
                        }
                    }
                }

                this.Close();
            }
            catch (System.Exception ex)
            {
                
            }
        }

        private void ToolControlDimen_FormClosed(object sender, FormClosedEventArgs e)
        {
            try
            {
                parentForm.Show();
            }
            catch (System.Exception ex)
            {
                MessageBox.Show("開啟主畫面失敗");
            }
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

        private void ToolNoCombo_SelectedIndexChanged(object sender, EventArgs e)
        {
            try
            {
                foreach (Node i in selNodes)
                {
                    i.Cells[2].Text = ToolNoCombo.Text;
                }
            }
            catch (System.Exception ex)
            {
            	
            }
        }

        private void LookPath_Click(object sender, EventArgs e)
        {
            if (!Is_BigDlg)
            {
                //Dlg變大
                Size DlgSize = new Size(555, 491);
                this.Size = DlgSize;

                Is_BigDlg = true;
            }
            else
            {
                //Dlg變小
                Size DlgSize = new Size(367, 491);
                this.Size = DlgSize;

                Is_BigDlg = false;
            }
        }

        private void SGC_ToolPath_RowClick(object sender, GridRowClickEventArgs e)
        {
            //取得點選的RowIndex
            CurrentRowIndex = e.GridRow.Index;
            CurrentSelOperName = panel.GetCell(CurrentRowIndex, 0).Value.ToString();
            SelectedElementCollection selRow = panel.GetSelectedElements();
            ListSelOper = new List<string>();
            foreach (GridRow item in selRow)
            {
                ListSelOper.Add(item.Cells[1].Value.ToString());
            }

            NXOpen.CAM.Preferences preferences1 = theSession.CAMSession.CreateCamPreferences();
            if (ListSelOper.Count == 1)
            {
                preferences1.ReplayRefreshBeforeEachPath = true;
            }
            else if (ListSelOper.Count > 1)
            {
                preferences1.ReplayRefreshBeforeEachPath = false;
            }
            preferences1.Commit();
            preferences1.Destroy();
            //UG顯示刀具路徑
            ExportShopDocDlg cExportShopDocDlg = new ExportShopDocDlg();
            cExportShopDocDlg.DisplayOpPath(ListSelOper);
        }

        
    }
}
