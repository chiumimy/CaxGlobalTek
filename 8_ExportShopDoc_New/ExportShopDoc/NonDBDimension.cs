using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Windows.Forms;
using NXOpen;
using NXOpen.UF;
using DevComponents.DotNetBar.SuperGrid;

namespace ExportShopDoc
{
    public partial class NonDBDimension : DevComponents.DotNetBar.Office2007Form
    {
        public static Form parentForm = new Form();
        public static Session theSession = Session.GetSession();
        public static UI theUI;
        public static UFSession theUfSession = UFSession.GetUFSession();
        public static Part displayPart = theSession.Parts.Display;
        //public static Dictionary<ExportShopDocDlg.ControlDimen_Key, ExportShopDocDlg.ControlDimen_Value> DicControlDimen = new Dictionary<ExportShopDocDlg.ControlDimen_Key, ExportShopDocDlg.ControlDimen_Value>();
        public static bool status;
        public static GridPanel panel = new GridPanel();

        public NonDBDimension(Form pForm, string OpName)
        {
            InitializeComponent();

            //將父層的Form傳進來
            parentForm = pForm;

            panel = SGCControlPanel.PrimaryGrid;
            panel.Columns["Delete"].EditorType = typeof(DeleteControlData);

            InitializeLabel(OpName);
        }


        private void InitializeLabel(string OpName)
        {
            try
            {
                OperationName.Text = OpName;
                foreach (KeyValuePair<ExportShopDocDlg.ControlDimen_Key, List<ExportShopDocDlg.ControlDimen_Value>> kvp in ExportShopDocDlg.DicControlDimen)
                {
                    if (kvp.Key.OperationName != OpName)
                    {
                        continue;
                    }
                    ToolNo.Text = kvp.Key.ToolNo;
                    ToolName.Text = kvp.Key.ToolName;
                    ToolHolder.Text = kvp.Key.ToolHolder;

                    foreach (ExportShopDocDlg.ControlDimen_Value i in kvp.Value)
                    {
                        if (i.ControlBallon == "" || i.TheoryTolRange == "" || i.ControlTolRange == "") 
                            break;

                        GridRow row = new GridRow(i.ControlBallon, i.TheoryTolRange, i.ControlTolRange, "刪除");
                        panel.Rows.Add(row);
                    }
                }
            }
            catch (System.Exception ex)
            {
            	
            }
        }

        private void CloseNonDBDlg_Click(object sender, EventArgs e)
        {
            this.Close();
        }

        private void NonDBDimension_FormClosed(object sender, FormClosedEventArgs e)
        {
            parentForm.Show();
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
                panel = NonDBDimension.panel;

                DeleteControlData cDeleteControlData = (DeleteControlData)sender;
                int index = cDeleteControlData.EditorCell.RowIndex;
                panel.Rows.RemoveAt(index);
            }
        }

        private void AddControlData_Click(object sender, EventArgs e)
        {
            try
            {
                if (TheoryDimen.Text == "" || BallonBox.Text == "" || ControlUpperTol.Text == "" || ControlLowerTol.Text == "" || ControlMaxTol.Text == "" || ControlMinTol.Text == "")
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


                GridRow row = new GridRow(BallonBox.Text, theroryTolRange, controlTolRange, "刪除");
                panel.Rows.Add(row);

                TheoryDimen.Text = "";
                BallonBox.Text = "";
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
