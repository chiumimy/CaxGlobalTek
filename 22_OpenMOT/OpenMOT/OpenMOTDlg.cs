using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Windows.Forms;
using DevComponents.DotNetBar.SuperGrid;
using System.IO;
using CaxGlobaltek;
using NXOpen;

namespace OpenMOT
{
    public partial class OpenMOTDlg : DevComponents.DotNetBar.Office2007Form
    {
        public static string[] Server_CusPathAry = new string[] { };//伺服器Task下的所有客戶(全路徑)
        public static string[] Server_PartNoPathAry = new string[] { };//伺服器客戶的所有料號(全路徑)
        public static string[] Server_CusVerPathAry = new string[] { };//伺服器料號的所有客戶版次(全路徑)
        public static string[] Server_OpVerPathAry = new string[] { };//伺服器客戶版次的所有製程版次(全路徑)
        public static string CurrentCusName = "", CurrentPartNo = "", CurrentCusVer = "", CurrentOpVer = "", Server_OpVerDir = "", MOT_File = "";
        public OpenMOTDlg()
        {
            InitializeComponent();
            //更新客戶資料
            IniSearchCus();
            //關閉下拉選單-料號&客戶版次&製程版次
            comboBoxPartNo.Enabled = false;
            comboBoxCusVer.Enabled = false;
            comboBoxOpVer.Enabled = false;
        }
        public void IniSearchCus()
        {
            /*目錄(含路徑)的陣列*/
            Server_CusPathAry = Directory.GetDirectories(CaxEnv.GetGlobaltekTaskDir());

            foreach (string item in Server_CusPathAry)
            {
                comboBoxCus.Items.Add(Path.GetFileNameWithoutExtension(item));
            }
        }

        private void comboBoxCus_SelectedIndexChanged(object sender, EventArgs e)
        {
            try
            {
                MOT_File = "";
                //取得當前選取的客戶
                CurrentCusName = comboBoxCus.Text;
                //開啟&清空下拉選單-料號
                comboBoxPartNo.Enabled = true;
                comboBoxPartNo.Items.Clear();
                comboBoxPartNo.Text = "";
                //關閉&清空下拉選單-客戶版次
                comboBoxCusVer.Enabled = false;
                comboBoxCusVer.Items.Clear();
                comboBoxCusVer.Text = "";
                //關閉&清空下拉選單-製程版次
                comboBoxOpVer.Enabled = false;
                comboBoxOpVer.Items.Clear();
                comboBoxOpVer.Text = "";

                //取得Server客戶資料夾目錄
                string Server_CusDir = string.Format(@"{0}\{1}", CaxEnv.GetGlobaltekTaskDir(), CurrentCusName);
                Server_PartNoPathAry = Directory.GetDirectories(Server_CusDir);/*目錄(含路徑)的陣列*/

                foreach (string item in Server_PartNoPathAry)
                {
                    comboBoxPartNo.Items.Add(Path.GetFileNameWithoutExtension(item));
                }
                if (comboBoxPartNo.Items.Count == 1)
                {
                    comboBoxPartNo.Text = comboBoxPartNo.Items[0].ToString();
                }
            }
            catch (System.Exception ex)
            {
            	
            }
        }

        private void comboBoxPartNo_SelectedIndexChanged(object sender, EventArgs e)
        {
            try
            {
                MOT_File = "";
                //取得當前選取的料號
                CurrentPartNo = comboBoxPartNo.Text;
                //開啟&清空下拉選單-客戶版次
                comboBoxCusVer.Enabled = true;
                comboBoxCusVer.Items.Clear();
                comboBoxCusVer.Text = "";
                //關閉&清空下拉選單-製程版次
                comboBoxOpVer.Enabled = false;
                comboBoxOpVer.Items.Clear();
                comboBoxOpVer.Text = "";

                //取得Server料號資料夾目錄
                string Server_PartNoDir = string.Format(@"{0}\{1}\{2}", CaxEnv.GetGlobaltekTaskDir(), CurrentCusName, CurrentPartNo);
                Server_CusVerPathAry = Directory.GetDirectories(Server_PartNoDir);

                foreach (string item in Server_CusVerPathAry)
                {
                    comboBoxCusVer.Items.Add(Path.GetFileNameWithoutExtension(item));
                }
                if (comboBoxCusVer.Items.Count == 1)
                {
                    comboBoxCusVer.Text = comboBoxCusVer.Items[0].ToString();
                }
            }
            catch (System.Exception ex)
            {
            	
            }
        }

        private void comboBoxCusVer_SelectedIndexChanged(object sender, EventArgs e)
        {
            try
            {
                MOT_File = "";
                //取得當前選取的客戶
                CurrentCusVer = comboBoxCusVer.Text;
                //開啟&清空下拉選單-製程版次
                comboBoxOpVer.Enabled = true;
                comboBoxOpVer.Items.Clear();
                comboBoxOpVer.Text = "";

                //取得Server客戶版次資料夾目錄
                string Server_CusVerDir = string.Format(@"{0}\{1}\{2}\{3}", CaxEnv.GetGlobaltekTaskDir(), CurrentCusName, CurrentPartNo, CurrentCusVer);
                Server_OpVerPathAry = Directory.GetDirectories(Server_CusVerDir);/*目錄(含路徑)的陣列*/

                foreach (string item in Server_OpVerPathAry)
                {
                    comboBoxOpVer.Items.Add(Path.GetFileNameWithoutExtension(item));
                }
                if (comboBoxOpVer.Items.Count == 1)
                {
                    comboBoxOpVer.Text = comboBoxOpVer.Items[0].ToString();
                }
            }
            catch (System.Exception ex)
            {
            	
            }
        }

        private void comboBoxOpVer_SelectedIndexChanged(object sender, EventArgs e)
        {
            try
            {
                MOTName.Text = "";

                //取得當前選取的版次
                CurrentOpVer = comboBoxOpVer.Text;

                //取得Server製程版次目錄
                Server_OpVerDir = string.Format(@"{0}\{1}\{2}\{3}\{4}", CaxEnv.GetGlobaltekTaskDir(), CurrentCusName, CurrentPartNo, CurrentCusVer, CurrentOpVer);

                //取得MOT檔案
                MOT_File = string.Format(@"{0}\{1}_MOT_{2}.prt", Server_OpVerDir, CurrentPartNo, CurrentOpVer);

                if (!File.Exists(MOT_File))
                {
                    MessageBox.Show("檔案：" + Path.GetFileNameWithoutExtension(MOT_File) + "不存在，無法開啟");
                    return;
                }

                MOTName.Text = Path.GetFileNameWithoutExtension(MOT_File);
            }
            catch (System.Exception ex)
            {
            	
            }
        }

        private void OK_Click(object sender, EventArgs e)
        {
            try
            {
                if (MOT_File == "")
                {
                    return;
                }

                BasePart newAsmPart;
                if (!CaxPart.OpenBaseDisplay(MOT_File, out newAsmPart))
                {
                    CaxLog.ShowListingWindow("MOT開啟失敗！");
                    return;
                }

                this.Close();
            }
            catch (System.Exception ex)
            {
            	
            }
        }

        private void Close_Click(object sender, EventArgs e)
        {
            try
            {
                this.Close();
            }
            catch (System.Exception ex)
            {
            	
            }
        }
    }
}
