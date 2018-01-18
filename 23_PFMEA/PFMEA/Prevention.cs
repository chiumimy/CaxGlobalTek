using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Windows.Forms;
using DevComponents.DotNetBar;
using DevComponents.AdvTree;
using DevComponents.DotNetBar.SuperGrid;

namespace PFMEA
{
    public partial class Prevention : Office2007Form
    {
        private GridPanel panel = new GridPanel();
        private NodeCollection nodes;
        public Prevention(List<string> input, NodeCollection inputNodes)
        {
            InitializeComponent();

            panel = SGC_Prevention.PrimaryGrid;
            nodes = inputNodes;
            InitializePanel(input);
        }

        private bool InitializePanel(List<string> listSysPrevention)
        {
            try
            {
                foreach (string i in listSysPrevention)
                {
                    object[] obj = new object[] { false, i };
                    panel.Rows.Add(new GridRow(obj));
                }
            }
            catch (System.Exception ex)
            {
                return false;
            }
            return true;
        }

        private void OK_Click(object sender, EventArgs e)
        {
            try
            {
                int cou = 0;
                string selString = "";
                foreach (GridRow i in panel.Rows)
                {
                    if ((bool)(i.Cells[0].Value) != true)
                        continue;

                    cou++;
                    if (selString == "")
                        selString = cou.ToString() + "." + i.Cells[1].Value.ToString();
                    else
                        selString = selString + "\n" + cou.ToString() + "." + i.Cells[1].Value.ToString();
                }
                foreach (Node i in nodes)
                {
                    i.Cells[9].Text = selString;
                    i.Cells[9].Tooltip = selString;
                }


                //MessageBox.Show(selString);
                this.Close();
            }
            catch (System.Exception ex)
            {
                MessageBox.Show(ex.ToString());
                this.Close();
            }
        }

        private void AddBtn_Click(object sender, EventArgs e)
        {
            Fun_Common.ManualAdd(panel, Manual.Text);
        }
    }
}
