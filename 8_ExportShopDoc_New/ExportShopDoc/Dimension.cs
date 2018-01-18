using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Windows.Forms;
using CaxGlobaltek;
using NHibernate;
using DevComponents.DotNetBar.SuperGrid;
using System.Collections;
using NXOpen;
using NXOpen.UF;
using NXOpen.CAM;
using NXOpen.Utilities;

namespace ExportShopDoc
{
    public partial class Dimension : DevComponents.DotNetBar.Office2007Form
    {
        public static ISession session = MyHibernateHelper.SessionFactory.OpenSession();
        public static Session theSession = Session.GetSession();
        public static UI theUI;
        public static UFSession theUfSession = UFSession.GetUFSession();
        public static Part displayPart = theSession.Parts.Display;
        public static Form parentForm = new Form();
        //public static PartInfo sPartInfo = new PartInfo();
        public static Com_PEMain comPEMain;
        public static Com_PartOperation comPartOperation;
        public static IList<Com_MEMain> comMEMain;
        public static IList<Com_Dimension> comDimension;
        public static List<string> ListToolNo = new List<string>();
        public static GridPanel panel = new GridPanel();
        public bool status;

        public Dimension(Form pForm, PartInfo partInfo, string OpName)
        {
            InitializeComponent();

            //將父層的Form傳進來
            parentForm = pForm;
            
            panel = SGCControlPanel.PrimaryGrid;
            panel.Columns["Delete"].EditorType = typeof(DeleteControlData);
            InitialLabel(partInfo, OpName);


            /*
            try
            {
                //取得所有刀號資料
                NCGroup ToolGroup = displayPart.CAMSetup.GetRoot(CAMSetup.View.MachineTool);
                CAMObject[] ListTool = ToolGroup.GetMembers();
                ListToolNo.Add("");
                foreach (CAMObject i in ListTool)
                {
                    if (i is NXOpen.CAM.Tool) ListToolNo.Add("T" + CaxOper.AskToolNumber(i));
                }
            }
            catch (System.Exception ex)
            {
                MessageBox.Show("取得刀號失敗");
                return;
            }
            */

        }

        private void InitialLabel(PartInfo sPartInfo, string OpName)
        {
            try
            {
                //設定基礎資料資訊
                CusName.Text = sPartInfo.CusName;
                PartNo.Text = sPartInfo.PartNo;
                CusRev.Text = sPartInfo.CusRev;
                OpRev.Text = sPartInfo.OpRev;
                OIS.Text = sPartInfo.OpNum;

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

                foreach (Com_MEMain i in comMEMain)
                    DraftingRev.Items.Add(i.draftingVer);

                if (DraftingRev.Items.Count == 1)
                    DraftingRev.Text = DraftingRev.Items[0].ToString();
                

                //設定程式資訊
                OperationName.Text = OpName;
                foreach (KeyValuePair<ExportShopDocDlg.ControlDimen_Key, List<ExportShopDocDlg.ControlDimen_Value>> kvp in ExportShopDocDlg.DicControlDimen)
                {
                    if (kvp.Key.OperationName != OpName)
                        continue;

                    ToolNo.Text = kvp.Key.ToolNo;
                    ToolName.Text = kvp.Key.ToolName;
                    ToolHolder.Text = kvp.Key.ToolHolder;

                    foreach (ExportShopDocDlg.ControlDimen_Value i in kvp.Value)
                    {
                        if (i.ControlBallon == "" || i.TheoryTolRange == "" || i.ControlTolRange == "")
                            break;

                        DraftingRev.Text = i.DraftingRev;
                        GridRow row = new GridRow(i.ControlBallon, i.TheoryTolRange, i.ControlTolRange, "刪除");
                        panel.Rows.Add(row);
                    }
                }
            }
            catch (System.Exception ex)
            {
            	
            }
        }

        

        private void CloseDimen_Click(object sender, EventArgs e)
        {
            try
            {
                this.Close();
            }
            catch (System.Exception ex)
            {
                MessageBox.Show("關閉副畫面失敗");
            }
        }

        private void Dimension_FormClosed(object sender, FormClosedEventArgs e)
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

        private void DraftingRev_SelectedIndexChanged(object sender, EventArgs e)
        {
            //清空BallonBox資訊
            BalloonBox.Items.Clear();

            foreach (Com_MEMain i in comMEMain)
            {
                if (DraftingRev.Text != i.draftingVer)
                    continue;
                    
                comDimension = session.QueryOver<Com_Dimension>().Where(x => x.comMEMain == i).OrderBy(x => x.ballon).Asc.List<Com_Dimension>();
                break;
            }
            InsertBalloonToBalloonBox(comDimension);
        }

        private void InsertBalloonToBalloonBox(IList<Com_Dimension> comDimension)
        {
            try
            {
                int balloonNo = 0;
                foreach (Com_Dimension i in comDimension)
                {
                    if (i.upTolerance == "" || i.upTolerance == null || i.lowTolerance == "" || i.lowTolerance == null ||
                        balloonNo == i.ballon)
                    {
                        continue;
                    }
                    BalloonBox.Items.Add(i.ballon);
                    balloonNo = i.ballon;
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
                if (panel.Rows.Count == 0)
                {
                    this.Close();
                    return;
                }

                ExportShopDocDlg.ControlDimen_Key sControlDimen_Key = new ExportShopDocDlg.ControlDimen_Key();
                sControlDimen_Key.NcGroupName = ExportShopDocDlg.CurrentNCGroup;
                sControlDimen_Key.OperationName = OperationName.Text;
                sControlDimen_Key.ToolNo = ToolNo.Text;
                sControlDimen_Key.ToolName = ToolName.Text;
                sControlDimen_Key.ToolHolder = ToolHolder.Text;

                List<ExportShopDocDlg.ControlDimen_Value> ListControlDimen_Value = new List<ExportShopDocDlg.ControlDimen_Value>();
                for (int i = 0; i < panel.Rows.Count; i++)
                {
                    ExportShopDocDlg.ControlDimen_Value sControlDimen_Value = new ExportShopDocDlg.ControlDimen_Value();
                    sControlDimen_Value.DraftingRev = DraftingRev.Text;
                    sControlDimen_Value.ControlBallon = panel.GetCell(i, 0).Value.ToString();
                    sControlDimen_Value.TheoryTolRange = panel.GetCell(i, 1).Value.ToString();
                    sControlDimen_Value.ControlTolRange = panel.GetCell(i, 2).Value.ToString();

                    ListControlDimen_Value.Add(sControlDimen_Value);
                }

                status = ExportShopDocDlg.DicControlDimen.ContainsKey(sControlDimen_Key);
                if (!status)
                    ExportShopDocDlg.DicControlDimen.Add(sControlDimen_Key, ListControlDimen_Value);
                else
                    ExportShopDocDlg.DicControlDimen[sControlDimen_Key] = ListControlDimen_Value;
            }
            catch (System.Exception ex)
            {
                MessageBox.Show("存入管制尺寸資料失敗");
                return;
            }
            this.Close();
        }

        public class DeleteControlData : GridButtonXEditControl
        {
            public DeleteControlData()
            {
                try
                {
                    Click += DeleteControlDataClick;
                }
                catch (System.Exception ex)
                {
                }
            }
            public void DeleteControlDataClick(object sender, EventArgs e)
            {
                GridPanel panel = new GridPanel();
                panel = Dimension.panel;

                DeleteControlData cDeleteControlData = (DeleteControlData)sender;
                int index = cDeleteControlData.EditorCell.RowIndex;
                panel.Rows.RemoveAt(index);
            }
        }

        private void BallonBox_SelectedIndexChanged(object sender, EventArgs e)
        {
            try
            {
                foreach (Com_Dimension i in comDimension)
                {
                    if (i.ballon.ToString() != BalloonBox.Text)
                        continue;

                    TheoryDimen.Text = i.mainText;
                    ControlUpperTol.Text = i.upTolerance;
                    ControlLowerTol.Text = i.lowTolerance;
                }
            }
            catch (System.Exception ex)
            {
            	
            }
        }

        private void AddControlData_Click(object sender, EventArgs e)
        {
            try
            {
                if (TheoryDimen.Text == "" || BalloonBox.Text == "" || ControlUpperTol.Text == "" || ControlLowerTol.Text == "" || ControlMaxTol.Text == "" || ControlMinTol.Text == "")
                {
                    MessageBox.Show("請填寫完整再新增！");
                    return;
                }

                //理論公差範圍
                string theroryTolLower = (Convert.ToDouble(TheoryDimen.Text) - Convert.ToDouble(ControlLowerTol.Text)).ToString();
                string theroryTolUpper = (Convert.ToDouble(TheoryDimen.Text) + Convert.ToDouble(ControlUpperTol.Text)).ToString();
                string theroryTolRange = theroryTolLower + "~" + theroryTolUpper;

                //管制公差範圍
                string controlTolLower = (Convert.ToDouble(TheoryDimen.Text) - Convert.ToDouble(ControlMinTol.Text)).ToString();
                string controlTolUpper = (Convert.ToDouble(TheoryDimen.Text) + Convert.ToDouble(ControlMaxTol.Text)).ToString();
                string controlTolRange = controlTolLower + "~" + controlTolUpper;


                GridRow row = new GridRow(BalloonBox.Text, theroryTolRange, controlTolRange, "刪除");
                panel.Rows.Add(row);

                TheoryDimen.Text = "";
                BalloonBox.Text = "";
                ControlUpperTol.Text = "";
                ControlLowerTol.Text = "";
                ControlMaxTol.Text = "";
                ControlMinTol.Text = "";
            }
            catch (System.Exception ex)
            {

            }
        }
    }
}
