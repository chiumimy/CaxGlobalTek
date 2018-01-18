using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Windows.Forms;
using CaxGlobaltek;
using System.IO;
using System.Collections;
using DevComponents.DotNetBar.SuperGrid;
using NXOpen;

namespace TEOpenTask
{
    public partial class TEOpenTaskDlg : DevComponents.DotNetBar.Office2007Form
    {
        public static METE_Download_Upload_Path cMETE_Download_Upload_Path = new METE_Download_Upload_Path();
        public static string[] Local_CusPathAry = new string[] { };//本機Task下的所有客戶(全路徑)
        public static string[] Local_PartNoPathAry = new string[] { };//本機客戶的所有料號(全路徑)
        public static string[] Local_CusVerPathAry = new string[] { };//本機料號的所有客戶版次(全路徑)
        public static string[] Local_OpVerPathAry = new string[] { };//本機客戶版次的所有製程版次(全路徑)
        //public static ArrayList Local_CusAry = new ArrayList();//本機Task下的所有客戶(資料夾名稱)
        //public static ArrayList Local_PartNoAry = new ArrayList();//本機客戶下的所有料號(資料夾名稱)
        //public static ArrayList Local_CusVerAry = new ArrayList();//本機料號下的所有版次(資料夾名稱)
        public static string CurrentCusName = "", CurrentPartNo = "", CurrentCusVer = "", CurrentOpVer = "";
        public static GridPanel panel = new GridPanel();
        private bool IS_PROGRAM = false;
        public static string SelePartName = "", SeleOperValue = "";
        public static bool status;
        public static string label001BilletPath = "";//001胚料檔路徑
        public static string labelWBilletPath = "";//W胚料檔路徑
        public static string label900BilletPath = "";//900胚料檔路徑

        public TEOpenTaskDlg()
        {
            InitializeComponent();

            panel = superGridPanel.PrimaryGrid;

            //取得METEDownload_Upload.dat資料
            status = CaxGetDatData.GetMETEDownload_Upload(out cMETE_Download_Upload_Path);
            if (!status)
            {
                MessageBox.Show("取得METEDownload_Upload.dat資料失敗");
                return;
            }

            //更新客戶資料
            IniSearchCus();

            //關閉下拉選單-料號&客戶版次&製程版次
            comboBoxPartNo.Enabled = false;
            comboBoxCusVer.Enabled = false;
            comboBoxOpVer.Enabled = false;
            //關閉groupBox
            groupBox001.Enabled = false;
            groupBoxW.Enabled = false;
            groupBox900.Enabled = false;
            button001SelPart.Enabled = false;
            buttonWSelPart.Enabled = false;

            //初始化labelBox
            label001.Text = "";
            labelW.Text = "";
        }

        public void IniSearchCus()
        {
            Local_CusPathAry = Directory.GetDirectories(CaxEnv.GetLocalTaskDir());

            foreach (string item in Local_CusPathAry)
            {
                comboBoxCus.Items.Add(Path.GetFileNameWithoutExtension(item));
            }
        }

        private void comboBoxCus_SelectedIndexChanged(object sender, EventArgs e)
        {
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
            //清空superGridPanel
            panel.Rows.Clear();

            //取得本機客戶資料夾目錄
            string Local_CusDir = string.Format(@"{0}\{1}", CaxEnv.GetLocalTaskDir(), CurrentCusName);
            Local_PartNoPathAry = Directory.GetDirectories(Local_CusDir);/*目錄(含路徑)的陣列*/

            foreach (string item in Local_PartNoPathAry)
            {
                comboBoxPartNo.Items.Add(Path.GetFileNameWithoutExtension(item));
            }
            if (comboBoxPartNo.Items.Count == 1)
            {
                comboBoxPartNo.Text = comboBoxPartNo.Items[0].ToString();
            }
            /*
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
            //清空superGridPanel
            panel.Rows.Clear();

            //取得本機客戶資料夾目錄
            Local_PartNoPathAry = new string[] { };
            Local_PartNoAry = new ArrayList();
            string Local_CusDir = string.Format(@"{0}\{1}", CaxEnv.GetLocalTaskDir(), CurrentCusName);
            Local_PartNoPathAry = Directory.GetDirectories(Local_CusDir);

            foreach (string item in Local_PartNoPathAry)
            {
                Local_PartNoAry.Add(Path.GetFileNameWithoutExtension(item));
            }

            comboBoxPartNo.Items.AddRange(Local_PartNoAry.ToArray());
            */
        }

        private void comboBoxPartNo_SelectedIndexChanged(object sender, EventArgs e)
        {
            //取得當前選取的客戶
            CurrentPartNo = comboBoxPartNo.Text;
            //開啟&清空下拉選單-客戶版次
            comboBoxCusVer.Enabled = true;
            comboBoxCusVer.Items.Clear();
            comboBoxCusVer.Text = "";
            //關閉&清空下拉選單-製程版次
            comboBoxOpVer.Enabled = false;
            comboBoxOpVer.Items.Clear();
            comboBoxOpVer.Text = "";
            //清空superGridPanel
            panel.Rows.Clear();

            //取得本機料號資料夾目錄
            string Local_PartNoDir = string.Format(@"{0}\{1}\{2}", CaxEnv.GetLocalTaskDir(), CurrentCusName, CurrentPartNo);
            Local_CusVerPathAry = Directory.GetDirectories(Local_PartNoDir);

            foreach (string item in Local_CusVerPathAry)
            {
                comboBoxCusVer.Items.Add(Path.GetFileNameWithoutExtension(item));
            }
            if (comboBoxCusVer.Items.Count == 1)
            {
                comboBoxCusVer.Text = comboBoxCusVer.Items[0].ToString();
            }
            /*
            //取得當前選取的客戶
            CurrentPartNo = comboBoxPartNo.Text;
            //開啟&清空下拉選單-客戶版次
            comboBoxCusVer.Enabled = true;
            comboBoxCusVer.Items.Clear();
            comboBoxCusVer.Text = "";
            //清空superGridPanel
            panel.Rows.Clear();

            //取得本機客戶下料號資料夾目錄
            Local_CusVerPathAry = new string[] { };
            Local_CusVerAry = new ArrayList();
            string Local_PartNoDir = string.Format(@"{0}\{1}\{2}", CaxEnv.GetLocalTaskDir(), CurrentCusName, CurrentPartNo);
            Local_CusVerPathAry = Directory.GetDirectories(Local_PartNoDir);

            foreach (string item in Local_CusVerPathAry)
            {
                Local_CusVerAry.Add(Path.GetFileNameWithoutExtension(item));
            }

            comboBoxCusVer.Items.AddRange(Local_CusVerAry.ToArray());
            */
        }

        private void comboBoxCusVer_SelectedIndexChanged(object sender, EventArgs e)
        {
            //取得當前選取的客戶
            CurrentCusVer = comboBoxCusVer.Text;
            //開啟&清空下拉選單-製程版次
            comboBoxOpVer.Enabled = true;
            comboBoxOpVer.Items.Clear();
            comboBoxOpVer.Text = "";
            //清空superGridPanel
            panel.Rows.Clear();

            //取得本機客戶版次資料夾目錄
            string Local_CusVerDir = string.Format(@"{0}\{1}\{2}\{3}", CaxEnv.GetLocalTaskDir(), CurrentCusName, CurrentPartNo, CurrentCusVer);
            Local_OpVerPathAry = Directory.GetDirectories(Local_CusVerDir);/*目錄(含路徑)的陣列*/

            foreach (string item in Local_OpVerPathAry)
            {
                comboBoxOpVer.Items.Add(Path.GetFileNameWithoutExtension(item));
            }
            if (comboBoxOpVer.Items.Count == 1)
            {
                comboBoxOpVer.Text = comboBoxOpVer.Items[0].ToString();
            }
            /*
            //取得當前選取的客戶
            CurrentCusVer = comboBoxCusVer.Text;
            //清空superGridPanel
            panel.Rows.Clear();

            //取得本機當前版次目錄
            string Local_CusVerDir = string.Format(@"{0}\{1}\{2}\{3}", CaxEnv.GetLocalTaskDir(), CurrentCusName, CurrentPartNo, CurrentCusVer);

            string[] Local_PartAry = System.IO.Directory.GetFiles(Local_CusVerDir);

            List<string> ListPartName = new List<string>();

            foreach (string item in Local_PartAry)
            {
                if (!item.Contains("CAM"))
                {
                    continue;
                }
                ListPartName.Add(item);
            }

            GridRow row = new GridRow();
            foreach (string item in ListPartName)
            {
                row = new GridRow(false, Path.GetFileName(item));
                panel.Rows.Add(row);
            }
            */
        }

        private void comboBoxOpVer_SelectedIndexChanged(object sender, EventArgs e)
        {
            //取得當前選取的客戶
            CurrentOpVer = comboBoxOpVer.Text;
            //清空superGridPanel
            panel.Rows.Clear();

            //取得本機製程版次目錄
            string Local_OpVerDir = string.Format(@"{0}\{1}\{2}\{3}\{4}", CaxEnv.GetLocalTaskDir(), CurrentCusName, CurrentPartNo, CurrentCusVer, CurrentOpVer);

            string[] Local_PartAry = System.IO.Directory.GetFiles(Local_OpVerDir);

            List<string> ListPartName = new List<string>();

            foreach (string item in Local_PartAry)
            {
                if (!item.Contains("CAM.prt") || item.ToUpper().Contains("FIXTURE") || item.Contains(".pdf"))
                {
                    continue;
                }
                ListPartName.Add(item);
            }

            GridRow row = new GridRow();
            foreach (string item in ListPartName)
            {
                row = new GridRow(false, Path.GetFileName(item));
                panel.Rows.Add(row);
            }
        }

        private void superGridPanel_CellValueChanged(object sender, GridCellValueChangedEventArgs e)
        {
            GridCell cell = e.GridCell;
            if (cell.GridColumn.Name.Equals("製程序") == true && !IS_PROGRAM)
            {
                IS_PROGRAM = true;

                GridRow row = cell.GridRow;

                for (int i = 0; i < panel.Rows.Count; i++)
                {
                    if (i == row.Index)
                    {
                        continue;
                    }
                    bool check = (bool)panel.GridPanel.GetCell(i, 0).Value;
                    if (check)
                    {
                        panel.GridPanel.GetCell(i, 0).Value = (bool)false;
                    }
                }

                bool cur_check = (bool)panel.GridPanel.GetCell(row.Index, 0).Value;
                if (!cur_check)
                {
                    panel.GridPanel.GetCell(row.Index, 0).Value = (bool)false;
                }
                else
                {
                    panel.GridPanel.GetCell(row.Index, 0).Value = (bool)true;
                }
                //CaxLog.ShowListingWindow(row.Index.ToString());

                IS_PROGRAM = false;
            }

            //清空所有checkBox&labelBox
            ClearAllCheck();

            //判斷選取到的製程序對應開啟的groupBox
            bool check_sel = (bool)panel.GridPanel.GetCell(cell.GridRow.Index, 0).Value;
            if (check_sel)
            {
                SelePartName = panel.GridPanel.GetCell(cell.GridRow.Index, 1).Value.ToString();
                string[] SplitSelePartName = SelePartName.Split(new string[] { "OP" }, StringSplitOptions.RemoveEmptyEntries);
                string DoubleSplitSelePartName = (SplitSelePartName[1].Split(new string[] { "_CAM" }, StringSplitOptions.RemoveEmptyEntries))[0];
                if (DoubleSplitSelePartName == "001")
                {
                    groupBox001.Enabled = true;
                    groupBox900.Enabled = false;
                    groupBoxW.Enabled = false;
                }
                else if (DoubleSplitSelePartName == "900")
                {
                    groupBox900.Enabled = true;
                    groupBox001.Enabled = false;
                    groupBoxW.Enabled = false;
                }
                else 
                {
                    groupBoxW.Enabled = true;
                    groupBox001.Enabled = false;
                    groupBox900.Enabled = false;
                }
                SeleOperValue = DoubleSplitSelePartName;
            }
        }

        private void ClearAllCheck()
        {
            check001HasBillet.Checked = false;
            check001NoBillet.Checked = false;
            checkWHasBillet.Checked = false;
            checkWNoBillet.Checked = false;
            label001.Text = "";
            labelW.Text = "";
            label900.Text = "";
        }

        private void check001HasBillet_CheckedChanged(object sender, EventArgs e)
        {
            if (check001HasBillet.Checked == true)
            {
                check001NoBillet.Checked = false;
                button001SelPart.Enabled = true;
            }
        }

        private void check001NoBillet_CheckedChanged(object sender, EventArgs e)
        {
            if (check001NoBillet.Checked == true)
            {
                check001HasBillet.Checked = false;
                button001SelPart.Enabled = false;
                label001.Text = "";
            }
        }

        private void checkWHasBillet_CheckedChanged(object sender, EventArgs e)
        {
            if (checkWHasBillet.Checked == true)
            {
                checkWNoBillet.Checked = false;
                buttonWSelPart.Enabled = true;
            }
        }

        private void checkWNoBillet_CheckedChanged(object sender, EventArgs e)
        {
            if (checkWNoBillet.Checked == true)
            {
                checkWHasBillet.Checked = false;
                buttonWSelPart.Enabled = false;
                labelW.Text = "";
            }
        }

        private void button001SelPart_Click(object sender, EventArgs e)
        {
            string ServerPartPath = string.Format(@"{0}\{1}\{2}\{3}", CaxEnv.GetGlobaltekTaskDir(),
                                                                      CurrentCusName,
                                                                      CurrentPartNo,
                                                                      CurrentCusVer);
            string tempFileName = "";
            status = CaxPublic.OpenFileDialog(out tempFileName, out label001BilletPath, ServerPartPath);
            if (!status)
            {
                MessageBox.Show("選擇檔案失敗，請聯繫開發工程師");
                return;
            }
            label001.Text = tempFileName;
        }

        private void buttonWSelPart_Click(object sender, EventArgs e)
        {
            string ServerPartPath = string.Format(@"{0}\{1}\{2}\{3}", CaxEnv.GetGlobaltekTaskDir(),
                                                                      CurrentCusName,
                                                                      CurrentPartNo,
                                                                      CurrentCusVer);
            string tempFileName = "";
            status = CaxPublic.OpenFileDialog(out tempFileName, out labelWBilletPath, ServerPartPath);
            if (!status)
            {
                MessageBox.Show("選擇檔案失敗，請聯繫開發工程師");
                return;
            }
            labelW.Text = tempFileName;
        }

        private void button900SelPart_Click(object sender, EventArgs e)
        {
            string tempFileName = "";
            status = CaxPublic.OpenFileDialog(out tempFileName, out label900BilletPath);
            if (!status)
            {
                MessageBox.Show("選擇檔案失敗，請聯繫開發工程師");
                return;
            }
            label900.Text = tempFileName;
        }

        private void buttonOpenTask_Click(object sender, EventArgs e)
        {
            #region (註解)檢查是否有選擇組裝方式
            /*
            if (SeleOperValue == "001")
            {
                if (check001HasBillet.Checked == false && check001NoBillet.Checked == false)
                {
                    MessageBox.Show("請選擇一種組裝方式");
                    return;
                }
                if (check001HasBillet.Checked == true && check001NoBillet.Checked == false)
                {
                    if (label001.Text == "")
                    {
                        MessageBox.Show("請選擇一個零件當作胚料檔，或勾選無胚料檔");
                        return;
                    }
                }
            }
            else if (SeleOperValue == "900")
            {

            }
            else
            {
                if (checkWHasBillet.Checked == false && checkWNoBillet.Checked == false)
                {
                    MessageBox.Show("請選擇一種組裝方式");
                    return;
                }
                if (checkWHasBillet.Checked == true && checkWHasBillet.Checked == false)
                {
                    if (labelW.Text == "")
                    {
                        MessageBox.Show("請選擇一個前段工序檔當作此工序零件檔，或勾選無前段工序檔");
                        return;
                    }
                }
            }
            */
            #endregion

            string Local_CAMPartFullPath = string.Format(@"{0}\{1}\{2}\{3}\{4}\{5}", CaxEnv.GetLocalTaskDir(),
                                                                                 CurrentCusName,
                                                                                 CurrentPartNo,
                                                                                 CurrentCusVer,
                                                                                 CurrentOpVer,
                                                                                 SelePartName);

            string Local_ModelPartFullPath = string.Format(@"{0}\{1}\{2}\{3}\{4}\{5}\{6}", CaxEnv.GetLocalTaskDir(),
                                                                                       CurrentCusName,
                                                                                       CurrentPartNo,
                                                                                       CurrentCusVer,
                                                                                       CurrentOpVer,
                                                                                       "MODEL",
                                                                                       CurrentPartNo + ".prt");

            string Local_ShareStrPartFullPath = string.Format(@"{0}\{1}\{2}\{3}\{4}", CaxEnv.GetLocalTaskDir(),
                                                                                         CurrentCusName,
                                                                                         CurrentPartNo,
                                                                                         CurrentCusVer,
                                                                                         CurrentOpVer);

            //組件存在，直接開啟任務組立
            BasePart newAsmPart;
            status = CaxPart.OpenBaseDisplay(Local_CAMPartFullPath, out newAsmPart);
            if (!status)
            {
                CaxLog.ShowListingWindow("組立開啟失敗！");
                return;
            }

            if (check001HasBillet.Checked == true || check001NoBillet.Checked == true ||
                checkWHasBillet.Checked == true || checkWNoBillet.Checked == true)
            {
                #region 組裝客戶檔案
                NXOpen.Assemblies.Component newComponent = null;
                string Local_NewModelPartFullPath = string.Format(@"{0}\{1}\{2}\{3}\{4}\{5}", CaxEnv.GetLocalTaskDir(),
                                                                                          CurrentCusName,
                                                                                          CurrentPartNo,
                                                                                          CurrentCusVer,
                                                                                          CurrentOpVer,
                                                                                          CurrentPartNo + ".prt");
                //判斷是否已經有客戶檔案，如果沒有則複製過來
                if (!System.IO.File.Exists(Local_NewModelPartFullPath))
                {
                    File.Copy(Local_ModelPartFullPath, Local_NewModelPartFullPath, true);
                }

                status = CaxAsm.AddComponentToAsmByDefault(Local_NewModelPartFullPath, out newComponent);
                if (!status)
                {
                    MessageBox.Show("客戶檔案組裝失敗！");
                    this.Close();
                    return;
                }
                #endregion

                CaxAsm.SetWorkComponent(null);

                #region 建立三階製程檔

                string Local_CurrentOperPartFullPath = "";
                if (groupBox001.Enabled == true)
                {
                    #region 001
                    //組立"胚料檔"
                    if (check001HasBillet.Checked == true & System.IO.File.Exists(label001BilletPath))
                    {
                        //複製檔案到任務資料夾
                        string tempBilletPath = label001BilletPath;
                        tempBilletPath = tempBilletPath.Replace(Path.GetDirectoryName(tempBilletPath), Path.GetDirectoryName(Local_NewModelPartFullPath));
                        File.Copy(label001BilletPath, tempBilletPath, true);

                        status = CaxAsm.AddComponentToAsmByDefault(tempBilletPath, out newComponent);
                        if (!status)
                        {
                            CaxLog.ShowListingWindow("001胚料檔組裝失敗");
                            return;
                        }
                    }

                    //組立"料號_TE_xxx"
                    Local_CurrentOperPartFullPath = string.Format(@"{0}\{1}", Local_ShareStrPartFullPath, CurrentPartNo + "_TE_" + SeleOperValue + ".prt");
                    if (!System.IO.File.Exists(Local_CurrentOperPartFullPath))
                    {
                        status = CaxAsm.CreateNewEmptyComp(Local_CurrentOperPartFullPath, out newComponent);
                        if (!status)
                        {
                            CaxLog.ShowListingWindow("建立三階製程檔失敗");
                            return;
                        }
                    }
                    #endregion
                }
                else if (groupBox900.Enabled == true)
                {
                }
                else
                {
                    #region W階
                    //組立"前段製程檔"
                    if (checkWHasBillet.Checked == true & System.IO.File.Exists(labelWBilletPath))
                    {
                        //複製檔案到任務資料夾
                        string tempBilletPath = labelWBilletPath;
                        tempBilletPath = tempBilletPath.Replace(Path.GetDirectoryName(tempBilletPath), Path.GetDirectoryName(Local_NewModelPartFullPath));
                        File.Copy(labelWBilletPath, tempBilletPath, true);

                        status = CaxAsm.AddComponentToAsmByDefault(tempBilletPath, out newComponent);
                        if (!status)
                        {
                            CaxLog.ShowListingWindow("W階胚料檔組裝失敗");
                            return;
                        }
                    }

                    //組立"料號_ME_OISxxx"
                    Local_CurrentOperPartFullPath = string.Format(@"{0}\{1}", Local_ShareStrPartFullPath, CurrentPartNo + "_TE_" + SeleOperValue + ".prt");
                    if (!System.IO.File.Exists(Local_CurrentOperPartFullPath))
                    {
                        status = CaxAsm.CreateNewEmptyComp(Local_CurrentOperPartFullPath, out newComponent);
                        if (!status)
                        {
                            CaxLog.ShowListingWindow("建立三階製程檔失敗");
                            return;
                        }
                    }
                    #endregion
                }

                #endregion
            }

            /*
            //判斷組件是否曾經被開啟。如已被開啟過，則開啟檔案後離開；反之則塞上屬性(Title=IsOpend，Value=Y)並組裝客戶零件與胚料零件
            string checkIsOpened = "";
            try
            {
                checkIsOpened = newAsmPart.GetStringAttribute("ISOPENED");
                if (checkIsOpened == "Y")
                {
                    CaxPart.Save();
                    this.Close();
                    return;
                }
            }
            catch (System.Exception ex)
            {
                newAsmPart.SetAttribute("ISOPENED", "Y");
            }

            NXOpen.Assemblies.Component newComponent = null;

            #region 組裝客戶檔案

            string Local_NewModelPartFullPath = string.Format(@"{0}\{1}\{2}\{3}\{4}", CaxEnv.GetLocalTaskDir(),
                                                                                       CurrentCusName,
                                                                                       CurrentPartNo,
                                                                                       CurrentCusVer,
                                                                                       CurrentPartNo + ".prt");
            //判斷是否已經有客戶檔案，如果沒有則複製過來
            if (!System.IO.File.Exists(Local_NewModelPartFullPath))
            {
                File.Copy(Local_ModelPartFullPath, Local_NewModelPartFullPath, true);
            }

            status = CaxAsm.AddComponentToAsmByDefault(Local_NewModelPartFullPath, out newComponent);
            if (!status)
            {
                CaxLog.ShowListingWindow("ERROR,組裝失敗！");
                return;
            }

            #endregion

            CaxAsm.SetWorkComponent(null);

            #region 建立三階製程檔

            string Local_CurrentOperPartFullPath = "";
            if (groupBox001.Enabled == true)
            {
                if (check001NoBillet.Checked == true)
                {
                    //選擇無胚料檔--新建檔案(檔名：料號_TE_製程序)
                    Local_CurrentOperPartFullPath = string.Format(@"{0}\{1}", Local_ShareStrPartFullPath, CurrentPartNo + "_TE_" + SeleOperValue + ".prt");
                    if (!System.IO.File.Exists(Local_CurrentOperPartFullPath))
                    {
                        status = CaxAsm.CreateNewEmptyComp(Local_CurrentOperPartFullPath, out newComponent);
                        if (!status)
                        {
                            CaxLog.ShowListingWindow("建立二階製程檔失敗");
                            return;
                        }
                    }
                }
                else
                {
                    //選擇有胚料檔--組裝胚料檔(檔名：胚料檔原始名稱)
                    Local_CurrentOperPartFullPath = string.Format(@"{0}\{1}", Local_ShareStrPartFullPath, label001.Text);
                    if (!System.IO.File.Exists(Local_CurrentOperPartFullPath))
                    {
                        status = CaxAsm.AddComponentToAsmByDefault(Local_CurrentOperPartFullPath, out newComponent);
                        if (!status)
                        {
                            CaxLog.ShowListingWindow("001胚料檔組裝失敗");
                            return;
                        }
                    }
                }
            }
            else if (groupBox900.Enabled == true)
            {
            }
            else
            {
                #region W階
                //組立"前段製程檔"
                if (checkWHasBillet.Checked == true & System.IO.File.Exists(labelWBilletPath))
                {
                    //複製檔案到任務資料夾
                    string tempBilletPath = labelWBilletPath;
                    tempBilletPath = tempBilletPath.Replace(Path.GetDirectoryName(tempBilletPath), Path.GetDirectoryName(Local_NewModelPartFullPath));
                    File.Copy(labelWBilletPath, tempBilletPath, true);

                    status = CaxAsm.AddComponentToAsmByDefault(tempBilletPath, out newComponent);
                    if (!status)
                    {
                        CaxLog.ShowListingWindow("W階胚料檔組裝失敗");
                        return;
                    }
                }

                //選擇無前段製程檔案--新建檔案(檔名：料號_TE_製程序)
                Local_CurrentOperPartFullPath = string.Format(@"{0}\{1}", Local_ShareStrPartFullPath, CurrentPartNo + "_TE_" + SeleOperValue + ".prt");
                if (!System.IO.File.Exists(Local_CurrentOperPartFullPath))
                {
                    status = CaxAsm.CreateNewEmptyComp(Local_CurrentOperPartFullPath, out newComponent);
                    if (!status)
                    {
                        CaxLog.ShowListingWindow("建立三階製程檔失敗");
                        return;
                    }
                }
                #endregion
            }

            #endregion
            */

            CaxPart.Save();
            this.Close();
            
        }

        private void Close_Click(object sender, EventArgs e)
        {
            this.Close();
        }

        

        
    }
}
