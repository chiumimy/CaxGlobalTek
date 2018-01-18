using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Windows.Forms;
using System.Drawing.Drawing2D;
using NXOpen;
using NXOpen.UF;
using DevComponents.DotNetBar.SuperGrid;
using NXOpen.CAM;
using System.Text.RegularExpressions;
using CaxGlobaltek;
using System.IO;

namespace MainProgram
{
    public partial class MainProgramDlg : DevComponents.DotNetBar.Office2007Form
    {
        public static Session theSession = Session.GetSession();
        public static UI theUI;
        public static UFSession theUfSession = UFSession.GetUFSession();
        public static Part workPart = theSession.Parts.Work;
        public static Part displayPart = theSession.Parts.Display;

        public static bool status;
        public static METE_Download_Upload_Path cMETE_Download_Upload_Path = new METE_Download_Upload_Path();
        public static NXOpen.CAM.NCGroup[] NCGroupAry = new NXOpen.CAM.NCGroup[] { };
        public static NXOpen.CAM.Operation[] OperationAry = new NXOpen.CAM.Operation[] { };
        public static Dictionary<string, string> DicNCData = new Dictionary<string, string>();
        public static string CurrentNCGroup = "";
        public static NXOpen.CAM.NCGroup SelectNCGroup;
        public static NXOpen.CAM.CAMObject[] OperObjAry = new NXOpen.CAM.CAMObject[] { };
        public static List<string> ListSelOper = new List<string>();
        public static Dictionary<int, ListViewItem> DicSelLV5Oper = new Dictionary<int, ListViewItem>();
        public static Dictionary<int, ListViewItem> DicSelLV1Oper = new Dictionary<int, ListViewItem>();
        public static string NCGroupMessage = "";
        public static ControlerConfig cControlerConfig = new ControlerConfig();
        public static string NewGroupName = "", UserDefineTxtPath = "", NCFolderPath = "", Is_Local = "";
        public PartInfo sPartInfo = new PartInfo();

        public struct SelectOper
        {
            public string OperName { get; set; }
            public int OperIndex { get; set; }
        }
        
        public MainProgramDlg()
        {
            InitializeComponent();
            chb_M98.Enabled = false;
            chb_M198.Enabled = false;
            chb_CALL.Enabled = false;
            chb_EXTCALL.Enabled = false;
            chb_SelPath.Enabled = false;
            SiePath.Enabled = false;
            NCGroup.Enabled = false;
            AddCondition.Enabled = false;

            //取得控制器配置資料(暫時註解，發布使用)
            
            Is_Local = Environment.GetEnvironmentVariable("UGII_ENV_FILE");
            if (Is_Local != null)
            {
                CaxGetDatData.GetControlerConfigData(out cControlerConfig);
            }
            else
            {
                string ControlerConfig_dat = "ControlerConfig.dat";
                string ControlerConfigPath = string.Format(@"{0}\{1}", "D:", ControlerConfig_dat);
                CaxPublic.ReadControlerConfig(ControlerConfigPath, out cControlerConfig);
            }
            

            //取得使用者自定義的txt
            UserDefineTxtPath = string.Format(@"{0}\{1}\{2}", "D:", "CaxUserDefine", "MainProgram");
            if (!Directory.Exists(UserDefineTxtPath))
            {
                UserDefineTxt.Items.Add("尚未定義");
            }
            else
            {
                string[] UserDefineTxtAry = System.IO.Directory.GetFileSystemEntries(UserDefineTxtPath, "*.txt");
                if (UserDefineTxtAry.Length == 0)
                {
                    UserDefineTxt.Items.Add("尚未定義");
                }
                else
                {
                    foreach (string i in UserDefineTxtAry)
                    {
                        UserDefineTxt.Items.Add(Path.GetFileNameWithoutExtension(i));
                    }
                }
            }
            
           
        }

        private class ListViewIndexComparer : System.Collections.IComparer
        {
            public int Compare(object x, object y)
            {
                return ((ListViewItem)x).Index - ((ListViewItem)y).Index;
            }
        }

        private void MainProgramDlg_Load(object sender, EventArgs e)
        {
            //AddButton.BackgroundImage = MainProgram.Properties.Resources.圖片1;
            //取得所有GroupAry，用來判斷Group的Type決定是NC、Tool、Geometry
            NCGroupAry = displayPart.CAMSetup.CAMGroupCollection.ToArray();
            //取得所有operationAry
            OperationAry = displayPart.CAMSetup.CAMOperationCollection.ToArray();

            #region 取得相關資訊，填入Dic
            DicNCData = new Dictionary<string, string>();
            foreach (NXOpen.CAM.NCGroup ncGroup in NCGroupAry)
            {
                int type, subtype;
                theUfSession.Obj.AskTypeAndSubtype(ncGroup.Tag, out type, out subtype);
                //判斷是否為Program群組
                if (type != UFConstants.UF_machining_task_type)
                {
                    continue;
                }

                foreach (NXOpen.CAM.Operation item in OperationAry)
                {
                    //取得父層的群組(回傳：NCGroup XXXX)
                    string NCProgramTag = item.GetParent(CAMSetup.View.ProgramOrder).ToString();
                    NCProgramTag = Regex.Replace(NCProgramTag, "[^0-9]", "");
                    if (NCProgramTag == ncGroup.Tag.ToString())
                    {
                        bool cheValue;
                        string OperName = "";
                        cheValue = DicNCData.TryGetValue(ncGroup.Name, out OperName);
                        if (!cheValue)
                        {
                            OperName = item.Name;
                            DicNCData.Add(ncGroup.Name, OperName);
                        }
                        else
                        {
                            OperName = OperName + "," + item.Name;
                            DicNCData[ncGroup.Name] = OperName;
                        }
                    }
                }
            }
            #endregion

            //將DicProgName的key存入程式群組下拉選單中
            comboBoxNCgroup.Items.AddRange(DicNCData.Keys.ToArray());

            //取得METEDownload_Upload資料
            status = CaxGetDatData.GetMETEDownload_Upload(out cMETE_Download_Upload_Path);
            if (!status)
            {
                MessageBox.Show("取得METEDownload_Upload失敗");
                return;
            }
        }

        private void comboBoxNCgroup_SelectedIndexChanged(object sender, EventArgs e)
        {
            //清空listView資料
            listView1.Items.Clear();
            listView3.Items.Clear();
            listView4.Items.Clear();
            listView5.Items.Clear();
            //取得comboBox資料
            CurrentNCGroup = comboBoxNCgroup.Text;

            #region 建立NC資料夾
            
            if (Is_Local != null)
            {
                CaxPublic.GetAllPath("TE", displayPart.FullPath, out sPartInfo, ref cMETE_Download_Upload_Path);
                if (!cMETE_Download_Upload_Path.Local_Folder_CAM.Contains("Oper1"))
                {
                    NCFolderPath = string.Format(@"{0}\{1}_{2}", cMETE_Download_Upload_Path.Local_Folder_CAM, CurrentNCGroup, "NC");
                }
                else
                {
                    NCFolderPath = string.Format(@"{0}\{1}_{2}", Path.GetDirectoryName(displayPart.FullPath), CurrentNCGroup, "NC");
                }
            }
            else
            {
                NCFolderPath = string.Format(@"{0}\{1}_{2}", Path.GetDirectoryName(displayPart.FullPath), CurrentNCGroup, "NC");
            }


            if (!Directory.Exists(NCFolderPath))
            {
                System.IO.Directory.CreateDirectory(NCFolderPath);
            }
            #endregion

            //取得選擇的NC
            foreach (NXOpen.CAM.NCGroup ncGroup in NCGroupAry)
            {
                if (CurrentNCGroup != ncGroup.Name)
                {
                    continue;
                }
                NCGroupMessage = CaxOper.AskNCGroupMessage(ncGroup);
                SelectNCGroup = ncGroup;
            }

            //當選擇海德漢控制器時，修改檔頭檔尾
            string[] splitCurrentNCGroup = CurrentNCGroup.Split('_');
            string outputName = Regex.Replace(splitCurrentNCGroup[0], "[^0-9]", "");
            NewGroupName = "O" + outputName + "0";
            if (splitCurrentNCGroup.Length == 2)
            {
                NewGroupName = NewGroupName + "_" + splitCurrentNCGroup[1];
            }



            if (chb_Heidenhain.Checked == true)
            {
                for (int i = 0; i < cControlerConfig.Controler.Count; i++)
                {
                    if (cControlerConfig.Controler[i].CompanyName == chb_Heidenhain.Text)
                    {
                        if (cControlerConfig.Controler[i].MasterValue1 != "")
                        {
                            listView3.Items.Add(cControlerConfig.Controler[i].MasterValue1);
                        }
                        if (cControlerConfig.Controler[i].MasterValue2 != "")
                        {
                            listView4.Items.Add(cControlerConfig.Controler[i].MasterValue2);
                        }
                        if (cControlerConfig.Controler[i].MasterValue3 != "")
                        {
                            listView4.Items.Add(cControlerConfig.Controler[i].MasterValue3);
                        }
                        break;
                    }
                }

                listView3.Items[0].Text = listView3.Items[0].Text.Replace("[NCGroupName]", NewGroupName);
                listView3.Items.Add(";(" + NCGroupMessage + ")");
                listView4.Items[1].Text = listView4.Items[1].Text.Replace("[NCGroupName]", NewGroupName);
            }
            else if (chb_Fanuc.Checked == true)
            {
                for (int i = 0; i < cControlerConfig.Controler.Count; i++)
                {
                    if (cControlerConfig.Controler[i].CompanyName == chb_Fanuc.Text)
                    {
                        if (cControlerConfig.Controler[i].MasterValue1 != "")
                        {
                            ListViewItem tempViewItem = new ListViewItem(cControlerConfig.Controler[i].MasterValue1);
                            listView3.Items.Add(tempViewItem);
                        }
                        if (cControlerConfig.Controler[i].MasterValue2 != "")
                        {
                            ListViewItem tempViewItem = new ListViewItem(cControlerConfig.Controler[i].MasterValue2);
                            listView4.Items.Add(tempViewItem);
                        }
                        if (cControlerConfig.Controler[i].MasterValue3 != "")
                        {
                            ListViewItem tempViewItem = new ListViewItem(cControlerConfig.Controler[i].MasterValue3);
                            listView4.Items.Add(tempViewItem);
                        }
                        break;
                    }
                }
                listView3.Items.Add(NewGroupName + "(" + NCGroupMessage + ")");
            }

            //取得Group下的所有OP
            OperObjAry = SelectNCGroup.GetMembers();

            #region 填值到ListView5

            foreach (KeyValuePair<string, string> kvp in DicNCData)
            {
                if (CurrentNCGroup != kvp.Key)
                {
                    continue;
                }

                string[] splitOperName = kvp.Value.Split(',');

                for (int i = 0; i < splitOperName.Length; i++)
                {
                    ListViewItem tempViewItem = new ListViewItem(splitOperName[i]);
                    ListViewItem.ListViewSubItem tempViewSubItem = new ListViewItem.ListViewSubItem();
                    foreach (CAMObject tempOper in OperObjAry)
                    {
                        if (splitOperName[i] == tempOper.Name)
                        {
                            tempViewSubItem = new ListViewItem.ListViewSubItem(tempViewItem, CaxOper.AskOperToolNumber((NXOpen.CAM.Operation)tempOper));
                        }
                    }
                    
                    tempViewItem.SubItems.Add(tempViewSubItem);
                    listView5.Items.Add(tempViewItem);
                }
                
            }

            #endregion

        }

        private void chb_Simens_CheckedChanged(object sender, EventArgs e)
        {
            if (chb_Siemens.Checked == true)
            {
                chb_Heidenhain.Checked = false;
                chb_Fanuc.Checked = false;
                chb_M98.Checked = false;
                chb_M198.Checked = false;
                chb_SelPath.Enabled = true;
                //SiePath.Enabled = true;
                //chb_CALL.Enabled = true;
                //chb_EXTCALL.Enabled = true;
                NCGroup.Enabled = true;
                AddCondition.Enabled = true;

                listView1.Items.Clear();
                listView3.Items.Clear();
                listView4.Items.Clear();
                listView5.Items.Clear();
                comboBoxNCgroup.Items.Clear();
                comboBoxNCgroup.Items.AddRange(DicNCData.Keys.ToArray());

                /*
                for (int i = 0; i < cControlerConfig.Controler.Count; i++ )
                {
                    if (cControlerConfig.Controler[i].CompanyName == chb_Simens.Text)
                    {
                        if (cControlerConfig.Controler[i].MasterValue1 != "")
                        {
                            listView3.Items.Add(cControlerConfig.Controler[i].MasterValue1);
                        }
                        if (cControlerConfig.Controler[i].MasterValue2 != "")
                        {
                            listView4.Items.Add(cControlerConfig.Controler[i].MasterValue2);
                        } 
                        if (cControlerConfig.Controler[i].MasterValue3 != "")
                        {
                            listView4.Items.Add(cControlerConfig.Controler[i].MasterValue3);
                        }
                        break; 
                    }
                }
                */
            }
            else
            {
                chb_SelPath.Checked = false;
                chb_SelPath.Enabled = false;
                SiePath.Text = "";
                SiePath.Enabled = false;
                //chb_CALL.Checked = false;
                //chb_CALL.Enabled = false;
                //chb_EXTCALL.Checked = false;
                //chb_EXTCALL.Enabled = false;
            }
        }

        private void chb_Heidenhain_CheckedChanged(object sender, EventArgs e)
        {
            if (chb_Heidenhain.Checked == true)
            {
                chb_Siemens.Checked = false;
                chb_Fanuc.Checked = false;
                chb_M98.Checked = false;
                chb_M198.Checked = false;
                NCGroup.Enabled = true;
                AddCondition.Enabled = true;

                listView1.Items.Clear();
                listView3.Items.Clear();
                listView4.Items.Clear();
                listView5.Items.Clear();
                comboBoxNCgroup.Items.Clear();
                comboBoxNCgroup.Items.AddRange(DicNCData.Keys.ToArray());
            }
        }

        private void chb_Fanuc_CheckedChanged(object sender, EventArgs e)
        {
            if (chb_Fanuc.Checked == true)
            {
                chb_Siemens.Checked = false;
                chb_Heidenhain.Checked = false;
                chb_M98.Enabled = true;
                chb_M198.Enabled = true;
                NCGroup.Enabled = true;
                AddCondition.Enabled = true;

                listView1.Items.Clear();
                listView3.Items.Clear();
                listView4.Items.Clear();
                listView5.Items.Clear();
                comboBoxNCgroup.Items.Clear();
                comboBoxNCgroup.Items.AddRange(DicNCData.Keys.ToArray());
            }
            else
            {
                chb_M98.Checked = false;
                chb_M198.Checked = false;
                chb_M98.Enabled = false;
                chb_M198.Enabled = false;
            }
        }

        private void chb_M98_CheckedChanged(object sender, EventArgs e)
        {
            if (chb_M98.Checked == true)
            {
                chb_M198.Checked = false;
                NCGroup.Enabled = true;
            }
        }

        private void chb_M198_CheckedChanged(object sender, EventArgs e)
        {
            if (chb_M198.Checked == true)
            {
                chb_M98.Checked = false;
                NCGroup.Enabled = true;
            }
        }
        /*
        private void AddButton_Click(object sender, EventArgs e)
        {

            //CaxLog.ShowListingWindow(ListSelOper.Count.ToString());
            if (DicSelOper.Count == 0)
            {
                this.Hide();
                UI.GetUI().NXMessageBox.Show("注意", NXMessageBox.DialogType.Warning, "請選擇程式");
                this.Show();
                return;
            }

            //判斷選到的Oper只能是listView5裡面的Oper
            foreach (KeyValuePair<int, ListViewItem> kvp in DicSelOper)
            {
                if (kvp.Value.ListView.Name == listView1.Name)
                {
                    return;
                }
            }

            if (chb_Fanuc.Checked == true)
            {
                if (chb_M198.Checked == false && chb_M98.Checked == false)
                {
                    this.Hide();
                    UI.GetUI().NXMessageBox.Show("注意", NXMessageBox.DialogType.Warning, "請選擇M98或M198");
                    this.Show();
                    return;
                }
            }

            foreach (KeyValuePair<int, ListViewItem> kvp in DicSelOper)
            {
                ListViewItem tempViewItem = new ListViewItem(kvp.Value.Text);

                //如果選擇發那科控制器，則取代O為P
                if (chb_Fanuc.Checked == true)
                {
                    tempViewItem.Text = kvp.Value.Text.Replace("O", "P");
                    if (chb_M198.Checked == true)
                    {
                        tempViewItem.Text = chb_M198.Text + " " + tempViewItem.Text;
                    }
                    if (chb_M98.Checked == true)
                    {
                        tempViewItem.Text = chb_M98.Text + " " + tempViewItem.Text;
                    }
                }
                else if (chb_Heidenhain.Checked == true)
                {
                    tempViewItem.Text = "CALL PGM" + " " + tempViewItem.Text;
                }
                else if (chb_Simens.Checked == true)
                {
                    tempViewItem.Text = "extcall" + " " + tempViewItem.Text;
                }
                listView1.Items.Add(tempViewItem);
            }

            foreach (KeyValuePair<int, ListViewItem> kvp in DicSelOper)
            {
                foreach (ListViewItem tmpLstView in listView5.Items)
                {
                    if (kvp.Value.Text == tmpLstView.Text)
                    {
                        listView5.Items.Remove(tmpLstView);
                    }
                    listView5.Update();
                }
            }
            DicSelOper = new Dictionary<int, ListViewItem>();

            //如果選擇海德漢控制器，則寫入順序

            
            //if (chb_Heidenhain.Checked == true)
            //{
            //    UpdateSeqHeidenhain();
            //}


        }
        */
        private void UpdateSeqHeidenhain()
        {
            int countNum = 1;
            for (int i = 0; i < listView1.Items.Count; i++)
            {
                if (listView1.Items[i].Text.Split().Length == 1)
                {
                    listView1.Items[i].Text = countNum.ToString() + " " + listView1.Items[i].Text;
                }
                else
                {
                    string SplitListView1Items = listView1.Items[i].Text.Split()[0];
                    listView1.Items[i].Text = listView1.Items[i].Text.Replace(SplitListView1Items, countNum.ToString());
                }
                countNum++;
            }

            for (int i = 0; i < listView4.Items.Count; i++)
            {
                string SplitListView4Items = listView4.Items[i].Text.Split()[0];
                listView4.Items[i].Text = listView4.Items[i].Text.Replace(SplitListView4Items, countNum.ToString());
                countNum++;
            }
        }

        private void listView1_MouseUp(object sender, MouseEventArgs e)
        {
            foreach (ListViewItem tmpLstView in listView1.Items)
            {
                tmpLstView.BackColor = Color.White;
            }

            DicSelLV1Oper = new Dictionary<int, ListViewItem>();
            foreach (ListViewItem tmpLstView in listView1.Items)
            {
                if (tmpLstView.Selected == true)
                {
                    DicSelLV1Oper.Add(tmpLstView.Index, tmpLstView);
                }
            }

            //顯示加工路徑
            ShowToolPath();

        }

        private void ShowToolPath()
        {
            NXOpen.CAM.Preferences preferences1 = theSession.CAMSession.CreateCamPreferences();
            if (DicSelLV1Oper.Count == 1 || DicSelLV5Oper.Count == 1)
            {
                preferences1.ReplayRefreshBeforeEachPath = true;
                preferences1.Commit();
                preferences1.Destroy();
            }
            else if (DicSelLV1Oper.Count > 1 || DicSelLV5Oper.Count > 1)
            {
                preferences1.ReplayRefreshBeforeEachPath = false;
                preferences1.Commit();
                preferences1.Destroy();
            }

            foreach (NXOpen.CAM.NCGroup ncGroup in NCGroupAry)
            {
                if (CurrentNCGroup != ncGroup.Name)
                {
                    continue;
                }
                for (int i = 0; i < OperationAry.Length; i++)
                {
                    //取得父層的群組(回傳：NCGroup XXXX)
                    string NCProgramTag = OperationAry[i].GetParent(CAMSetup.View.ProgramOrder).ToString();
                    NCProgramTag = Regex.Replace(NCProgramTag, "[^0-9]", "");
                    NXOpen.CAM.CAMObject[] tempObjToCreateImg = new CAMObject[1];

                    foreach (KeyValuePair<int, ListViewItem> kvp in DicSelLV1Oper)
                    {
                        string tempOper = kvp.Value.Text;
                        //如果選擇發那科控制器，則取代P為O才能看路徑
                        if (chb_Fanuc.Checked == true)
                        {
                            tempOper = tempOper.Replace("P", "O");
                        }
                        if (chb_Siemens.Checked == true)
                        {
                            if (chb_CALL.Checked == true)
                            {
                                tempOper = tempOper.Replace("\"", "");
                            }
                            else if (chb_EXTCALL.Checked == true)
                            {
                                tempOper = tempOper.Replace("\"", "");
                                tempOper = tempOper.Replace(".mpf", "");
                            }
                        }
                        if (tempOper.Split().Length > 1)
                        {
                            tempOper = tempOper.Split()[tempOper.Split().Length - 1];
                        }
                        if (NCProgramTag == ncGroup.Tag.ToString() && tempOper == CaxOper.AskOperNameFromTag(OperationAry[i].Tag))
                        {
                            tempObjToCreateImg[0] = (NXOpen.CAM.CAMObject)OperationAry[i];
                            workPart.CAMSetup.ReplayToolPath(tempObjToCreateImg);
                        }
                    }

                    foreach (KeyValuePair<int, ListViewItem> kvp in DicSelLV5Oper)
                    {
                        string tempOper = kvp.Value.Text;
                        //如果選擇發那科控制器，則取代P為O才能看路徑
                        if (chb_Fanuc.Checked == true)
                        {
                            tempOper = tempOper.Replace("P", "O");
                        }
                        if (tempOper.Split().Length > 1)
                        {
                            tempOper = tempOper.Split()[tempOper.Split().Length - 1];
                        }
                        if (NCProgramTag == ncGroup.Tag.ToString() && tempOper == CaxOper.AskOperNameFromTag(OperationAry[i].Tag))
                        {
                            tempObjToCreateImg[0] = (NXOpen.CAM.CAMObject)OperationAry[i];
                            workPart.CAMSetup.ReplayToolPath(tempObjToCreateImg);
                        }
                    }
                }
            }
        }
        /*
        private void RemoveButton_Click(object sender, EventArgs e)
        {
            if (DicSelLV1Oper.Count == 0)
            {
                MessageBox.Show("請選擇程式");
                return;
            }

            //判斷選到的Oper只能是listView1裡面的Oper
            foreach (KeyValuePair<int, ListViewItem> kvp in DicSelLV1Oper)
            {
                if (kvp.Value.ListView.Name == listView5.Name)
                {
                    return;
                }
                if (!kvp.Value.Text.Contains(chb_M198.Text) &
                    !kvp.Value.Text.Contains(chb_M98.Text) &
                    !kvp.Value.Text.Contains("extcall") &
                    !kvp.Value.Text.Contains("CALL PGM"))
                {
                    this.Hide();
                    UI.GetUI().NXMessageBox.Show("注意", NXMessageBox.DialogType.Warning, "如欲移除" + kvp.Value.Text + "請使用刪除功能");
                    this.Show();
                    return;
                }
            }

            #region 將listView1選到的Oper填回listView5中，並重新取得刀號

            foreach (KeyValuePair<int, ListViewItem> kvp in DicSelOper)
            {
                ListViewItem tempViewItem = new ListViewItem();
                tempViewItem.Text = kvp.Value.Text.Split()[kvp.Value.Text.Split().Length - 1];
                if (chb_Fanuc.Checked == true)
                {
                    tempViewItem.Text = tempViewItem.Text.Replace("P", "O");
                }

                ListViewItem.ListViewSubItem tempViewSubItem = new ListViewItem.ListViewSubItem();
                foreach (CAMObject tempOper in OperObjAry)
                {
                    if (tempViewItem.Text == tempOper.Name)
                    {
                        tempViewSubItem = new ListViewItem.ListViewSubItem(tempViewItem, CaxOper.AskOperToolNumber((NXOpen.CAM.Operation)tempOper));
                    }
                }

                tempViewItem.SubItems.Add(tempViewSubItem);
                listView5.Items.Add(tempViewItem);
            }

            #endregion

            int count = 0;
            foreach (KeyValuePair<int, ListViewItem> kvp in DicSelOper)
            {
                foreach (ListViewItem tmpLstView in listView1.Items)
                {
                    if (kvp.Value.Text != tmpLstView.Text || kvp.Key != (tmpLstView.Index + count))
                    {
                        continue;
                    }
                    count++;
                    listView1.Items.Remove(tmpLstView);
                    listView1.Update();
                    break;
                }
            }
            DicSelOper = new Dictionary<int, ListViewItem>();
        }
        */
        private void listView1_ItemDrag(object sender, ItemDragEventArgs e)
        {
            //啟始拖放作業
            //listView1.DoDragDrop(e.Item, DragDropEffects.Move);
        }

        private void listView1_DragEnter(object sender, DragEventArgs e)
        {
            //e.Effect = e.AllowedEffect;
        }

        private void listView1_DragDrop(object sender, DragEventArgs e)
        {   
            /*
            //取得要拖曳的資料所在的Index
            int targetIndex = listView1.InsertionMark.Index;
            
            if (targetIndex == -1)
            {
                return;
            }
            if (listView1.InsertionMark.AppearsAfterItem)
            {
                targetIndex++;
            }
            
            ListViewItem draggedItem = (ListViewItem)e.Data.GetData(typeof(ListViewItem));
            listView1.BeginUpdate();

            listView1.Items.Insert(targetIndex, (ListViewItem)draggedItem.Clone());
            listView1.Items.Remove(draggedItem);
            listView1.EndUpdate();
            */
        }

        private void listView1_DragOver(object sender, DragEventArgs e)
        {
            /*
            System.Drawing.Point ptScreen = new System.Drawing.Point(e.X, e.Y);
            System.Drawing.Point pt = listView1.PointToClient(ptScreen);
            ListViewItem item = listView1.GetItemAt(pt.X, pt.Y);

            int targetIndex = listView1.InsertionMark.NearestIndex(pt);
            if (targetIndex > -1)
            {
                Rectangle itemBounds = listView1.GetItemRect(targetIndex);
                if (pt.X > itemBounds.Left + (itemBounds.Width / 2))
                {
                    listView1.InsertionMark.AppearsAfterItem = true;
                }
                else
                {
                    listView1.InsertionMark.AppearsAfterItem = false;
                }
            }
            listView1.InsertionMark.Index = targetIndex;
            */
        }

        private void listView1_DragLeave(object sender, EventArgs e)
        {
            //listView1.InsertionMark.Index = -1;
        }

        private void CopyItem_Click(object sender, EventArgs e)
        {
            if (DicSelLV1Oper.Count == 0)
            {
                MessageBox.Show("請選擇程式");
                return;
            }

            if (DicSelLV1Oper.Count > 1)
            {
                MessageBox.Show("僅能選擇一條程式做複製");
                return;
            }

            //判斷選到的Oper只能是listView1裡面的Oper
            //foreach (KeyValuePair<int, ListViewItem> kvp in DicSelLV1Oper)
            //{
            //    if (kvp.Value.ListView.Name == listView5.Name)
            //    {
            //        return;
            //    }
            //}

            foreach (KeyValuePair<int, ListViewItem> kvp in DicSelLV1Oper)
            {
                if (kvp.Value.ListView.Name != listView1.Name)
                {
                    MessageBox.Show("僅能選擇右邊主視窗中的程式做複製");
                    return;
                }
                listView1.Items.Insert(listView1.Items.Count, kvp.Value.Text);
            }
            listView1.Update();
        }

        private void UpButton_Click(object sender, EventArgs e)
        {

            if (DicSelLV1Oper.Count == 0)
                return;

            List<int> ListSeleIndex = new List<int>();
            foreach (KeyValuePair<int, ListViewItem> kvp in DicSelLV1Oper)
            {
                ListSeleIndex.Add(kvp.Key);
            }
            for (int i = 0; i < ListSeleIndex.Count; i++)
            {
                if (i + 1 == ListSeleIndex.Count)
                {
                    break;
                }
                if (ListSeleIndex[i + 1] - ListSeleIndex[i] > 1)
                {
                    MessageBox.Show("不可跳著挑選程式");
                    DicSelLV1Oper = new Dictionary<int, ListViewItem>();
                    return;
                }
            }

            foreach (KeyValuePair<int, ListViewItem> kvp in DicSelLV1Oper)
            {
                if (kvp.Key == 0)
                {
                    return;
                }
            }
            
            List<ListViewItem> ListTempListViewItem = new List<ListViewItem>();
            foreach (KeyValuePair<int, ListViewItem> kvp in DicSelLV1Oper)
            {
                //ListViewItem tempItem = listView1.Items.Insert(kvp.Key - 1, kvp.Value.Text);
                ListViewItem tempItem = listView1.Items.Insert(kvp.Value.Index - 1, kvp.Value.Text);
                ListTempListViewItem.Add(tempItem);
                tempItem.BackColor = Color.Bisque;
                //listView1.Items.RemoveAt(kvp.Key + 1);
                listView1.Items.RemoveAt(kvp.Value.Index);
            }

            DicSelLV1Oper = new Dictionary<int, ListViewItem>();
            foreach (ListViewItem i in ListTempListViewItem)
            {
                DicSelLV1Oper[i.Index] = i;
            }
            

            

        }

        private void DownButton_Click(object sender, EventArgs e)
        {
            if (DicSelLV1Oper.Count == 0)
                return;

            List<int> ListSeleIndex = new List<int>();
            foreach (KeyValuePair<int, ListViewItem> kvp in DicSelLV1Oper)
            {
                ListSeleIndex.Add(kvp.Key);
            }
            for (int i = 0; i < ListSeleIndex.Count; i++)
            {
                if (i + 1 == ListSeleIndex.Count)
                {
                    break;
                }
                if (ListSeleIndex[i + 1] - ListSeleIndex[i] > 1)
                {
                    MessageBox.Show("不可跳著挑選程式");
                    DicSelLV1Oper = new Dictionary<int, ListViewItem>();
                    return;
                }
            }


            foreach (KeyValuePair<int, ListViewItem> kvp in DicSelLV1Oper)
            {
                if (kvp.Key == listView1.Items.Count-1)
                {
                    return;
                }
            }

            int SelectCount = DicSelLV1Oper.Count;
            int smallestIndex = -1;
            foreach (KeyValuePair<int, ListViewItem> kvp in DicSelLV1Oper)
            {
                smallestIndex = kvp.Key;
                break;
            }

            List<ListViewItem> ListTempListViewItem = new List<ListViewItem>();
            foreach (KeyValuePair<int, ListViewItem> kvp in DicSelLV1Oper)
            {
                //當執行往下移動時，需要插入的index為(目前所在index + 選擇幾條程式做移動 + 1)
                ListViewItem tempItem = listView1.Items.Insert(kvp.Value.Index + SelectCount + 1, kvp.Value.Text);
                ListTempListViewItem.Add(tempItem);
                tempItem.BackColor = Color.Bisque;
                listView1.Items.RemoveAt(smallestIndex);
            }

            DicSelLV1Oper = new Dictionary<int, ListViewItem>();
            foreach (ListViewItem i in ListTempListViewItem)
            {
                DicSelLV1Oper[i.Index] = i;
            }
                     
            /*
            int index = -1;
            ListViewItem OperStr = new ListViewItem();

            foreach (KeyValuePair<int, ListViewItem> kvp in DicSelOper)
            {
                index = kvp.Key + 1;
                OperStr = kvp.Value;
                ListViewItem tempItem = listView1.Items.Insert(kvp.Key + 2, kvp.Value.Text);
                tempItem.BackColor = Color.Bisque;
                listView1.Items.RemoveAt(kvp.Key);
            }

            DicSelOper = new Dictionary<int, ListViewItem>();
            DicSelOper.Add(index, OperStr);
            */


        }

        private void CloseDlg_Click(object sender, EventArgs e)
        {
            NXOpen.CAM.Preferences preferences1 = theSession.CAMSession.CreateCamPreferences();
            preferences1.ReplayRefreshBeforeEachPath = true;
            preferences1.Commit();
            preferences1.Destroy();
            this.Close();
        }

        private void listView5_MouseUp(object sender, MouseEventArgs e)
        {
            DicSelLV5Oper = new Dictionary<int, ListViewItem>();
            foreach (ListViewItem tmpLstView in listView5.Items)
            {
                if (tmpLstView.Selected == true)
                {
                    DicSelLV5Oper.Add(tmpLstView.Index, tmpLstView);
                }
            }

            //顯示加工路徑
            ShowToolPath();
        }

        private void UserAddCondition_Click(object sender, EventArgs e)
        {
            if (UserCondition.Text == "")
                return;

            if (DicSelLV1Oper.Count > 1)
            {
                MessageBox.Show("請選擇一條程式當參考");
                return;
            }

            //將UserCondition中的訊息拆分字串
            string[] SplitCondition = UserCondition.Text.Split(new string[] { "\r\n" }, StringSplitOptions.None);

            if (DicSelLV1Oper.Count == 0)
            {
                foreach (string i in SplitCondition)
                {
                    if (i == "")
                    {
                        continue;
                    }
                    ListViewItem tempViewItem = new ListViewItem(i);
                    listView1.Items.Add(tempViewItem);
                }
            }
            else
            {
                int selectindex = -1;
                foreach (KeyValuePair<int, ListViewItem> kvp in DicSelLV1Oper)
                {
                    selectindex = kvp.Key;
                    //ListViewItem tempItem = listView1.Items.Insert(kvp.Key + 1, UserCondition.Text);
                }
                foreach (string i in SplitCondition)
                {
                    if (i == "")
                    {
                        continue;
                    }
                    selectindex++;
                    ListViewItem tempItem = listView1.Items.Insert(selectindex, i);
                }
            }
            UserCondition.Text = "";
        }

        private void DeleteSel_Click(object sender, EventArgs e)
        {
            if (DicSelLV1Oper.Count == 0)
            {
                MessageBox.Show("請選擇程式");
                return;
            }

            //判斷選到的Oper只能是listView1裡面的Oper
            //foreach (KeyValuePair<int, ListViewItem> kvp in DicSelLV1Oper)
            //{
            //    if (kvp.Value.ListView.Name == listView5.Name)
            //    {
            //        return;
            //    }
            //}

            int count = 0;
            foreach (KeyValuePair<int, ListViewItem> kvp in DicSelLV1Oper)
            {
                foreach (ListViewItem tmpLstView in listView1.Items)
                {
                    if (kvp.Value.Text != tmpLstView.Text || kvp.Key != (tmpLstView.Index + count))
                    {
                        continue;
                    }
                    count++;
                    listView1.Items.Remove(tmpLstView);
                    listView1.Update();
                    break;
                }
            }
            DicSelLV1Oper = new Dictionary<int, ListViewItem>();
        }

        private void ExportMainProg_Click(object sender, EventArgs e)
        {
            List<string> ExportData = new List<string>();
            for (int i = 0; i < listView3.Items.Count; i++)
            {
                ExportData.Add(listView3.Items[i].Text);
            }
            for (int i = 0; i < listView1.Items.Count; i++)
            {
                ExportData.Add(listView1.Items[i].Text);
            }
            for (int i = 0; i < listView4.Items.Count; i++)
            {
                ExportData.Add(listView4.Items[i].Text);
            }

            
            //建立NC資料夾
            /*
            string Is_Local = "", NCFolderPath = "";
            Is_Local = Environment.GetEnvironmentVariable("UGII_ENV_FILE");
            if (Is_Local != "")
            {
                CaxPublic.GetAllPath("TE", displayPart.FullPath, ref cMETE_Download_Upload_Path);
                if (!cMETE_Download_Upload_Path.Local_Folder_CAM.Contains("Oper1"))
                {
                    NCFolderPath = string.Format(@"{0}\{1}", cMETE_Download_Upload_Path.Local_Folder_CAM, "NC");
                }
                else
                {
                    NCFolderPath = string.Format(@"{0}\{1}", Path.GetDirectoryName(displayPart.FullPath), "NC");
                }
            }
            else
            {
                NCFolderPath = string.Format(@"{0}\{1}", Path.GetDirectoryName(displayPart.FullPath), "NC");
            }
            if (!Directory.Exists(NCFolderPath))
            {
                System.IO.Directory.CreateDirectory(NCFolderPath);
            }
            */

            //設定輸出路徑
            string ExportPath = string.Format(@"{0}\{1}", NCFolderPath, NewGroupName);

            //string NCFolderPath = string.Format(@"{0}\{1}", Path.GetDirectoryName(displayPart.FullPath), "NC");
            //if (!Directory.Exists(NCFolderPath))
            //{
            //    System.IO.Directory.CreateDirectory(NCFolderPath);
            //}
            //string ExportPath = string.Format(@"{0}\{1}", Path.GetDirectoryName(displayPart.FullPath), NewGroupName);

            if (chb_Heidenhain.Checked == true)
            {
                for (int i = 0; i < ExportData.Count; i++)
                {
                    ExportData[i] = i.ToString() + " " + ExportData[i];
                }
                ExportPath = ExportPath + ".h";
            }
            else if (chb_Siemens.Checked == true)
            {
                //for (int i = 0; i < ExportData.Count; i++)
                //{
                //    ExportData[i] = "N" + (i + 1).ToString() + " " + ExportData[i];
                //}
                ExportPath = ExportPath + ".MPF";
            }
            
            System.IO.StreamWriter file = new System.IO.StreamWriter(ExportPath);
            for (int i = 0; i < ExportData.Count; i++) 
            {
                file.Write(ExportData[i]+"\r\n");
            }
            file.Close();

            MessageBox.Show("主程式編輯完成！");
        }

        private void UserDefineTxt_SelectedIndexChanged(object sender, EventArgs e)
        {
            UserCondition.Clear();

            string SelDefineTxt = string.Format(@"{0}\{1}", UserDefineTxtPath, UserDefineTxt.Text + ".txt");
            string[] TxtData = System.IO.File.ReadAllLines(SelDefineTxt);
            for (int i = 0; i < TxtData.Length;i++ )
            {
                if (i == 0)
                {
                    UserCondition.Text = TxtData[i];
                }
                else
                {
                    UserCondition.Text = UserCondition.Text + "\r\n" + TxtData[i];
                }
            }
        }

        private void ClearTextBox_Click(object sender, EventArgs e)
        {
            UserCondition.Clear();
        }

        private void right_arrow_Click(object sender, EventArgs e)
        {
            //CaxLog.ShowListingWindow(ListSelOper.Count.ToString());
            if (DicSelLV5Oper.Count == 0)
            {
                MessageBox.Show("請選擇程式");
                return;
            }

            //判斷選到的Oper只能是listView5裡面的Oper
            //foreach (KeyValuePair<int, ListViewItem> kvp in DicSelLV5Oper)
            //{
            //    if (kvp.Value.ListView.Name == listView1.Name)
            //    {
            //        return;
            //    }
            //}

            if (chb_Fanuc.Checked == true)
            {
                if (chb_M198.Checked == false && chb_M98.Checked == false)
                {
                    MessageBox.Show("請選擇M98或M198");
                    return;
                }
            }

            //if (chb_Siemens.Checked == true)
            //{
            //    if (chb_CALL.Checked == false && chb_EXTCALL.Checked == false)
            //    {
            //        MessageBox.Show("請選擇CALL或EXTCALL");
            //        return;
            //    }
            //}

            foreach (KeyValuePair<int, ListViewItem> kvp in DicSelLV5Oper)
            {
                ListViewItem tempViewItem = new ListViewItem(kvp.Value.Text);

                //如果選擇發那科控制器，則取代O為P
                if (chb_Fanuc.Checked == true)
                {
                    tempViewItem.Text = kvp.Value.Text.Replace("O", "P");
                    if (chb_M198.Checked == true)
                    {
                        tempViewItem.Text = chb_M198.Text + " " + tempViewItem.Text;
                    }
                    if (chb_M98.Checked == true)
                    {
                        tempViewItem.Text = chb_M98.Text + " " + tempViewItem.Text;
                    }
                }
                else if (chb_Heidenhain.Checked == true)
                {
                    tempViewItem.Text = "CALL PGM" + " " + tempViewItem.Text;
                }
                else if (chb_Siemens.Checked == true)
                {
                    if (SiePath.Text != "")
                    {
                        tempViewItem.Text = string.Format("{0} {1}", chb_EXTCALL.Text, "(\"" + SiePath.Text + tempViewItem.Text + ".mpf" + "\")");
                    }
                    else
                    {
                        tempViewItem.Text = string.Format("{0} {1}", chb_EXTCALL.Text, "\"" + tempViewItem.Text + ".mpf" + "\"");
                    }
                    //if (chb_CALL.Checked == true)
                    //{
                    //    tempViewItem.Text = string.Format("{0} {1}", chb_CALL.Text, "\"" + tempViewItem.Text + "\"");
                    //}
                    //if (chb_EXTCALL.Checked == true)
                    //{
                    //    tempViewItem.Text = string.Format("{0} {1}", chb_EXTCALL.Text, "\"" + tempViewItem.Text + ".mpf" + "\"");
                    //}
                }
                listView1.Items.Add(tempViewItem);
            }

            foreach (KeyValuePair<int, ListViewItem> kvp in DicSelLV5Oper)
            {
                foreach (ListViewItem tmpLstView in listView5.Items)
                {
                    if (kvp.Value.Text == tmpLstView.Text)
                    {
                        listView5.Items.Remove(tmpLstView);
                    }
                    listView5.Update();
                }
            }
            DicSelLV5Oper = new Dictionary<int, ListViewItem>();

            //如果選擇海德漢控制器，則寫入順序


            //if (chb_Heidenhain.Checked == true)
            //{
            //    UpdateSeqHeidenhain();
            //}
        }

        private void left_arrow_Click(object sender, EventArgs e)
        {
            if (DicSelLV1Oper.Count == 0)
            {
                MessageBox.Show("請選擇程式");
                return;
            }

            //判斷選到的Oper只能是listView1裡面的Oper
            foreach (KeyValuePair<int, ListViewItem> kvp in DicSelLV1Oper)
            {
                if (kvp.Value.ListView.Name == listView5.Name)
                {
                    return;
                }
                if (!kvp.Value.Text.Contains(chb_M198.Text) &
                    !kvp.Value.Text.Contains(chb_M98.Text) &
                    !kvp.Value.Text.Contains(chb_CALL.Text) &
                    !kvp.Value.Text.Contains(chb_EXTCALL.Text) &
                    !kvp.Value.Text.Contains("CALL PGM"))
                {
                    MessageBox.Show("如欲移除" + kvp.Value.Text + "請使用刪除功能");
                    return;
                }
            }

            #region 將listView1選到的Oper填回listView5中，並重新取得刀號

            foreach (KeyValuePair<int, ListViewItem> kvp in DicSelLV1Oper)
            {
                ListViewItem tempViewItem = new ListViewItem();
                tempViewItem.Text = kvp.Value.Text.Split()[kvp.Value.Text.Split().Length - 1];
                if (chb_Fanuc.Checked == true)
                {
                    tempViewItem.Text = tempViewItem.Text.Replace("P", "O");
                }
                else if (chb_Siemens.Checked == true)
                {

                    
                    tempViewItem.Text = tempViewItem.Text.Replace("\"", "");
                    tempViewItem.Text = tempViewItem.Text.Replace(".mpf", "");
                    if (SiePath.Text != "")
                    {
                        tempViewItem.Text = tempViewItem.Text.Replace("(", "");
                        tempViewItem.Text = tempViewItem.Text.Replace(")", "");
                        tempViewItem.Text = tempViewItem.Text.Replace(SiePath.Text, "");
                    }
                    //if (chb_CALL.Checked == true)
                    //{
                    //    tempViewItem.Text = tempViewItem.Text.Replace("\"", "");
                    //}
                    //else if (chb_EXTCALL.Checked == true)
                    //{
                    //    tempViewItem.Text = tempViewItem.Text.Replace("\"", "");
                    //    tempViewItem.Text = tempViewItem.Text.Replace(".mpf", "");
                    //}
                }

                ListViewItem.ListViewSubItem tempViewSubItem = new ListViewItem.ListViewSubItem();
                foreach (CAMObject tempOper in OperObjAry)
                {
                    if (tempViewItem.Text == tempOper.Name)
                    {
                        tempViewSubItem = new ListViewItem.ListViewSubItem(tempViewItem, CaxOper.AskOperToolNumber((NXOpen.CAM.Operation)tempOper));
                    }
                }

                tempViewItem.SubItems.Add(tempViewSubItem);
                listView5.Items.Add(tempViewItem);
            }

            #endregion

            int count = 0;
            foreach (KeyValuePair<int, ListViewItem> kvp in DicSelLV1Oper)
            {
                foreach (ListViewItem tmpLstView in listView1.Items)
                {
                    if (kvp.Value.Text != tmpLstView.Text || kvp.Key != (tmpLstView.Index + count))
                    {
                        continue;
                    }
                    count++;
                    listView1.Items.Remove(tmpLstView);
                    listView1.Update();
                    break;
                }
            }
            DicSelLV1Oper = new Dictionary<int, ListViewItem>();
        }

        private void up_arrow_Click(object sender, EventArgs e)
        {

            if (DicSelLV1Oper.Count == 0)
            {
                MessageBox.Show("請選擇程式");
                return;
            }

            List<int> ListSeleIndex = new List<int>();
            foreach (KeyValuePair<int, ListViewItem> kvp in DicSelLV1Oper)
            {
                ListSeleIndex.Add(kvp.Key);
            }
            for (int i = 0; i < ListSeleIndex.Count; i++)
            {
                if (i + 1 == ListSeleIndex.Count)
                {
                    break;
                }
                if (ListSeleIndex[i + 1] - ListSeleIndex[i] > 1)
                {
                    MessageBox.Show("不可跳著挑選程式");
                    DicSelLV1Oper = new Dictionary<int, ListViewItem>();
                    return;
                }
            }

            foreach (KeyValuePair<int, ListViewItem> kvp in DicSelLV1Oper)
            {
                if (kvp.Key == 0)
                {
                    return;
                }
            }

            List<ListViewItem> ListTempListViewItem = new List<ListViewItem>();
            foreach (KeyValuePair<int, ListViewItem> kvp in DicSelLV1Oper)
            {
                //ListViewItem tempItem = listView1.Items.Insert(kvp.Key - 1, kvp.Value.Text);
                ListViewItem tempItem = listView1.Items.Insert(kvp.Value.Index - 1, kvp.Value.Text);
                ListTempListViewItem.Add(tempItem);
                tempItem.BackColor = Color.Bisque;
                //listView1.Items.RemoveAt(kvp.Key + 1);
                listView1.Items.RemoveAt(kvp.Value.Index);
            }

            DicSelLV1Oper = new Dictionary<int, ListViewItem>();
            foreach (ListViewItem i in ListTempListViewItem)
            {
                DicSelLV1Oper[i.Index] = i;
            }
        }

        private void down_arrow_Click(object sender, EventArgs e)
        {
            if (DicSelLV1Oper.Count == 0)
            {
                MessageBox.Show("請選擇程式");
                return;
            }

            List<int> ListSeleIndex = new List<int>();
            foreach (KeyValuePair<int, ListViewItem> kvp in DicSelLV1Oper)
            {
                ListSeleIndex.Add(kvp.Key);
            }
            for (int i = 0; i < ListSeleIndex.Count; i++)
            {
                if (i + 1 == ListSeleIndex.Count)
                {
                    break;
                }
                if (ListSeleIndex[i + 1] - ListSeleIndex[i] > 1)
                {
                    MessageBox.Show("不可跳著挑選程式");
                    DicSelLV1Oper = new Dictionary<int, ListViewItem>();
                    return;
                }
            }


            foreach (KeyValuePair<int, ListViewItem> kvp in DicSelLV1Oper)
            {
                if (kvp.Key == listView1.Items.Count - 1)
                {
                    return;
                }
            }

            int SelectCount = DicSelLV1Oper.Count;
            int smallestIndex = -1;
            foreach (KeyValuePair<int, ListViewItem> kvp in DicSelLV1Oper)
            {
                smallestIndex = kvp.Key;
                break;
            }

            List<ListViewItem> ListTempListViewItem = new List<ListViewItem>();
            foreach (KeyValuePair<int, ListViewItem> kvp in DicSelLV1Oper)
            {
                //當執行往下移動時，需要插入的index為(目前所在index + 選擇幾條程式做移動 + 1)
                ListViewItem tempItem = listView1.Items.Insert(kvp.Value.Index + SelectCount + 1, kvp.Value.Text);
                ListTempListViewItem.Add(tempItem);
                tempItem.BackColor = Color.Bisque;
                listView1.Items.RemoveAt(smallestIndex);
            }

            DicSelLV1Oper = new Dictionary<int, ListViewItem>();
            foreach (ListViewItem i in ListTempListViewItem)
            {
                DicSelLV1Oper[i.Index] = i;
            }

            /*
            int index = -1;
            ListViewItem OperStr = new ListViewItem();

            foreach (KeyValuePair<int, ListViewItem> kvp in DicSelOper)
            {
                index = kvp.Key + 1;
                OperStr = kvp.Value;
                ListViewItem tempItem = listView1.Items.Insert(kvp.Key + 2, kvp.Value.Text);
                tempItem.BackColor = Color.Bisque;
                listView1.Items.RemoveAt(kvp.Key);
            }

            DicSelOper = new Dictionary<int, ListViewItem>();
            DicSelOper.Add(index, OperStr);
            */
        }

        private void chb_CALL_CheckedChanged(object sender, EventArgs e)
        {
            if (chb_CALL.Checked == true)
            {
                chb_EXTCALL.Checked = false;
                NCGroup.Enabled = true;
            }
        }

        private void chb_EXTCALL_CheckedChanged(object sender, EventArgs e)
        {
            if (chb_EXTCALL.Checked == true)
            {
                chb_CALL.Checked = false;
                NCGroup.Enabled = true;
            }
        }

        private void chb_SelPath_CheckedChanged(object sender, EventArgs e)
        {
            if (chb_SelPath.Checked)
            {
                SiePath.Enabled = true;
            }
            else
            {
                SiePath.Text = "";
                SiePath.Enabled = false;
            }
        }

        


        


    }
}
