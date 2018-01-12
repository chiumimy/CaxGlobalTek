using System;
using System.Collections.Generic;
using System.Linq;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Text;
using System.Windows.Forms;
using DevComponents.DotNetBar.SuperGrid;
using System.Collections;
using System.IO;
using CaxGlobaltek;
using NXOpen;
using NXOpen.UF;
using NXOpen.Utilities;
using NHibernate;
using NHibernate.Criterion;
using Iesi.Collections.Generic;
using DevComponents.DotNetBar;
using DevComponents.AdvTree;


namespace PEGenerateFile
{
    public partial class PEGenerateDlg : DevComponents.DotNetBar.Office2007Form
    {
        private static UFSession theUfSession = UFSession.GetUFSession();
        private static Session theSession = Session.GetSession();
        public static ISession session = MyHibernateHelper.SessionFactory.OpenSession();
        public bool status,Is_OldPart = false;
        public static string Oper1String = "",Oper2String = "",CusName = "",PartNo = "",CusRev = "",OpRev = ""
            , souPartFilePath = "-1", souBilletFilePath = "", CurrentOldCusName = "", CurrentOldPartNo = "", CurrentOldCusRev = "", CurrentOldOpRev = ""
            , souOldPartFilePath = "-1", souOldBilletFilePath = "", PartDes = "", PartMaterial = "", MaterialDataPath = "", tempFolderPath = "";
        public static OperationArray cOperationArray = new OperationArray();
        //public static string[] Oper2StringAry = new string[]{};
        public static GridPanel panel = new GridPanel();
        public static Dictionary<string, PECreateData> DicDataSave = new Dictionary<string, PECreateData>();
        public static METEDownloadData cMETEDownloadData = new METEDownloadData();
        public static int IndexofCusName = -1, IndexofPartNo = -1, IndexofCusRev = -1;
        public static List<string> ListAddOper = new List<string>();
        public static PECreateData cPECreateData = new PECreateData();
        public static Com_PEMain cCom_PEMain = new Com_PEMain();
        public static IList<Sys_Operation2> listSys_Operation2 = new List<Sys_Operation2>();
        public static List<Op2ComboTree> ListOp2ComboTree = new List<Op2ComboTree>();
        public static List<MaterialComboTree> ListMaterialComboTree = new List<MaterialComboTree>();
        public static Com_PartOperation cCom_PartOperation = new Com_PartOperation();
        public static string[] MaterialDataAry = new string[] { };
        public static Com_PEMain cComPEMain = new Com_PEMain();
        public static DirectoryInfo di;
        public static List<string> AssembliesComp = new List<string>();
        public struct OpDataKey
        {
            public int partOpSrNo  { get; set; }
            public string flag { get; set; }
        }
        public struct OpDataValue
        {
            public string op1 { get; set; }
            public string op2 { get; set; }
            public string form { get; set; }
            public string erp { get; set; }
        }
        public struct Op2ComboTree
        {
            public string category { get; set; }
            public string operation2Name { get; set; }
        }
        public struct MaterialComboTree
        {
            public string category { get; set; }
            public string materialName { get; set; }
        }
        public PEGenerateDlg()
        {
            InitializeComponent();
        }

        private void PEGenerateDlg_Load(object sender, EventArgs e)
        {
            //初始設定
            textPartNo.Text = "";
            textCusRev.Text = "";

            #region 舊料號資料填入
            string[] S_Task_CusName = Directory.GetDirectories(CaxEnv.GetGlobaltekTaskDir());
            if (S_Task_CusName.Length == 0)
                comboBoxOldCusName.Items.Add("沒有舊資料");
            else
                foreach (string item in S_Task_CusName)
                    comboBoxOldCusName.Items.Add(Path.GetFileNameWithoutExtension(item));//走訪每個元素只取得目錄名稱(不含路徑)並加入dirlist集合中

            comboBoxOldPartNo.Enabled = false;
            comboBoxOldCusRev.Enabled = false;
            comboBoxOldOpRev.Enabled = false;
            #endregion

            #region 新料號初始化資料

            IList<Sys_Customer> customerName = session.QueryOver<Sys_Customer>().List<Sys_Customer>();
            comboBoxCusName.DisplayMember = "customerName";
            comboBoxCusName.ValueMember = "customerSrNo";

            foreach (Sys_Customer i in customerName)
                comboBoxCusName.Items.Add(i);

            //按順序排序Asc=由小到大
            listSys_Operation2 = session.QueryOver<Sys_Operation2>().OrderBy(x => x.operation2SrNo).Asc
                                                                    .ThenBy(x => x.operation2Name).Asc
                                                                    .List<Sys_Operation2>();
            
            foreach (Sys_Operation2 i in listSys_Operation2)
            {
                Op2ComboTree s = new Op2ComboTree();
                if (i.category == null)
                {
                    s.category = "其他";
                }
                else
                {
                    s.category = i.category;
                }
                s.operation2Name = i.operation2Name;
                ListOp2ComboTree.Add(s);
            }

            //建立GridPanel
            panel = OperSuperGridControl.PrimaryGrid;

            //設定製程別的基礎型態與數據
            panel.Columns["Oper2Ary"].EditorType = typeof(PEComboBox);
            panel.Columns["Oper2Ary"].EditorParams = new object[] { ListOp2ComboTree };

            //設定刪除的基礎型態
            panel.Columns["Delete"].EditorType = typeof(OperDeleteBtn);


            //取得材質資料庫並加入到下拉選單中
            IList<Sys_Material> listSysMaterial = session.QueryOver<Sys_Material>().List<Sys_Material>();
            foreach (Sys_Material i in listSysMaterial)
            {
                MaterialComboTree s = new MaterialComboTree();
                if (i.category == null)
                {
                    s.category = "其他";
                }
                else
                {
                    s.category = i.category;
                }
                s.materialName = i.materialName;
                ListMaterialComboTree.Add(s);
            }
            #endregion

            //將OperationArray配置檔內容塞入製程序&製程別下拉選單中
            //comboOperArray1.Items.AddRange(cOperationArray.OperationArray1.ToArray());
            //comboOperArray2.Items.AddRange(cOperationArray.OperationArray2.ToArray());
        }

        private void OperCreateBtn_Click(object sender, EventArgs e)
        {
            try
            {
                //檢查資料庫，判斷是否有相同的客戶、料號、客戶版次、製程版次
                Sys_Customer customer = session.QueryOver<Sys_Customer>().Where(y => y.customerName == comboBoxCusName.Text).SingleOrDefault<Sys_Customer>();
                Com_PEMain PEMain = session.QueryOver<Com_PEMain>()
                    .Where(x => x.sysCustomer == customer)
                    .And(x => x.partName == textPartNo.Text.ToUpper())
                    .And(x => x.customerVer == textCusRev.Text.ToUpper())
                    .And(x => x.opVer == textOpRev.Text.ToUpper()).SingleOrDefault<Com_PEMain>();
                if (PEMain != null)
                {
                    MessageBox.Show("此【料號】、【客戶版次】、【製程版次】已存在，無法再次建立！");
                    return;
                }

                //取得使用者選取的製程序
                List<string> ListSelectOper = new List<string>();
                status = CheckOper1Status(out ListSelectOper);
                if (!status)
                {
                    MessageBox.Show("檢查製程序失敗");
                    return;
                }

                //2016.12.16
                if (UserDefineProcess.Text != "")
                {
                    ListSelectOper.Add(UserDefineProcess.Text);
                }
                

                //判斷使用者選取的製程序是否已經存在於OperSuperGridControl
                if (!(panel.Rows.Count == 0))
                {
                    for (int i = 0; i < panel.Rows.Count; i++)
                    {
                        status = ListSelectOper.Contains(panel.GridPanel.GetCell(i, 0).Value.ToString());
                        if (status)
                        {
                            MessageBox.Show("已有重複的製程序");
                            //清除使用者選取的製程序
                            ClearSelectOper1();
                            return;
                        }
                    }
                }

                //Tab=新增料號
                if (tabControl1.SelectedTab.Name == "tabItem1")
                {
                    //檢查是否有指定ERP資料
                    if (ERPcode.Text == "")
                    {
                        if (eTaskDialogResult.Yes == CaxPublic.ShowMsgYesNo("尚未產生ERP編號，是否返回操作?\nYes：返回。\nNo：不返回，繼續執行。"))
                            return;
                    }
                    if (ERPcode.Text != "" & CompBillet.Checked == false & CusBillet.Checked == false)
                    {
                        MessageBox.Show("請先指定【自備胚】或【管材/鍛件】");
                        return;
                    }

                    //將製程序填入OperSuperGridControl
                    GridRow row = new GridRow();
                    foreach (var i in ListSelectOper)
                    {
                        row = new GridRow(new object[panel.Columns.Count]);
                        panel.Rows.Add(row);
                        WriteOperRow(i, row);
                       
                        ListAddOper.Add(i);//使用者有可能藉由"加入"來新增舊料號中的製程
                    }
                }
                //Tab=編輯製程，加入IQC/IPQC的功能還沒修改
                else if (tabControl1.SelectedTab.Name == "tabItem2")
                {
                    
                    //將製程序填入OperSuperGridControl
                    GridRow row = new GridRow();
                    
                    foreach (var i in ListSelectOper)
                    {
                        row = new GridRow(new object[panel.Columns.Count]);
                        panel.Rows.Add(row);
                        if (cComPEMain.eRPStd == null)
                        {
                            //row = new GridRow(i, GetOp2(i), "", "刪除");
                            row.Cells["Oper1Ary"].Value = i;
                            row.Cells["Oper2Ary"].Value = GetOp2(i);
                            row.Cells["IQC"].Value = false;
                            row.Cells["IPQC"].Value = false;
                            row.Cells["ERP料號"].Value = "";
                            row.Cells["Delete"].Value = "刪除";
                        }
                        else
                        {
                            //row = new GridRow(i, GetOp2(i), "W" + cComPEMain.materialSource + "-" + cComPEMain.eRPStd + "-" + i, "刪除");
                            row.Cells["Oper1Ary"].Value = i;
                            row.Cells["Oper2Ary"].Value = GetOp2(i);
                            row.Cells["IQC"].Value = false;
                            row.Cells["IPQC"].Value = false;
                            row.Cells["ERP料號"].Value = "W" + cComPEMain.materialSource + "-" + cComPEMain.eRPStd + "-" + i;
                            row.Cells["Delete"].Value = "刪除";
                        }
                        
                        //panel.Rows.Add(row);
                        ListAddOper.Add(i);//使用者有可能藉由"加入"來新增舊料號中的製程
                    }
                }

                //清除使用者選取的製程序
                ClearSelectOper1();

                //清除使用者自定義的製程序
                UserDefineProcess.WatermarkText = "自定義OP";
            }
            catch (System.Exception ex)
            {
                MessageBox.Show(ex.ToString());
            }
            

        }

        private string GetERPcode(string i)
        {
            string ERP = "";
            string Billet = "";
            try
            {
                if (ERPcode.Text == "")
                    return ERP = "";

                if (CompBillet.Checked == false & CusBillet.Checked == false)
                    return ERP = "";

                if (CompBillet.Checked == true)
                    Billet = "1";

                if (CusBillet.Checked == true)
                    Billet = "A";

                if (i == "001")
                    ERP = string.Format("{0}{1}-{2}-{3}", "R", Billet, ERPcode.Text, textCusRev.Text.ToUpper());

                else if (i == "002")
                    return ERP = "";

                else if (i == "003")
                    return ERP = "";

                else if (i == "004")
                    return ERP = "";

                else if (i == "900")
                    ERP = string.Format("{0}{1}-{2}-{3}", "P", Billet, ERPcode.Text, i);
                    
                else
                    ERP = string.Format("{0}{1}-{2}-{3}", "W", Billet, ERPcode.Text, i);

            }
            catch (System.Exception ex)
            {
                return ERP = "";
            }
            return ERP;
        }


        private string GetOp2(string op1)
        {
            string op2 = "";
            try
            {
                if (op1 == "105")
                {
                    op2 = "Wax inject";
                }
                else if (op1 == "110")
                {
                    op2 = "Wax Tree Assembly";
                }
                else if (op1 == "115")
                {
                    op2 = "Ceramic shell";
                }
                else if (op1 == "120")
                {
                    op2 = "De-Wax";
                }
                else if (op1 == "130")
                {
                    op2 = "Sintering&Pouring";
                }
                else if (op1 == "140")
                {
                    op2 = "Shell Removing";
                }
                else if (op1 == "150")
                {
                    op2 = "Heat treatment";
                }
                else if (op1 == "160")
                {
                    op2 = "Machanical properties test";
                }
                else if (op1 == "170")
                {
                    op2 = "Blasting";
                }
                else if (op1 == "180")
                {
                    op2 = "Passivation";
                }
                else if (op1 == "199")
                {
                    op2 = "Final inspection";
                }
            }
            catch (System.Exception ex)
            {
                return op2 = "";
            }
            return op2;
        }
        private void WriteOperRow(string i, GridRow row)
        {
            try
            {

                if (i == "001")
                {
                    //row = new GridRow(i, "", GetERPcode(i), "刪除");
                    //OperSuperGridControl.PrimaryGrid.Rows.Add(row);
                    row.Cells["Oper1Ary"].Value = i;
                    row.Cells["Oper2Ary"].Value = "";
                    row.Cells["IQC"].Value = false;
                    row.Cells["IPQC"].Value = false;
                    row.Cells["ERP料號"].Value = GetERPcode(i);
                    row.Cells["Delete"].Value = "刪除";
                    //panel.Rows.Add(row);
                }
                else if (i == "002")
                {
                    //row = new GridRow(i, "", GetERPcode(i), "刪除");
                    //OperSuperGridControl.PrimaryGrid.Rows.Add(row);
                    row.Cells["Oper1Ary"].Value = i;
                    row.Cells["Oper2Ary"].Value = "";
                    row.Cells["IQC"].Value = false;
                    row.Cells["IPQC"].Value = false;
                    row.Cells["ERP料號"].Value = GetERPcode(i);
                    row.Cells["Delete"].Value = "刪除";
                    //panel.Rows.Add(row);
                }
                else if (i == "003")
                {
                    //row = new GridRow(i, "", GetERPcode(i), "刪除");
                    //OperSuperGridControl.PrimaryGrid.Rows.Add(row);
                    row.Cells["Oper1Ary"].Value = i;
                    row.Cells["Oper2Ary"].Value = "";
                    row.Cells["IQC"].Value = false;
                    row.Cells["IPQC"].Value = false;
                    row.Cells["ERP料號"].Value = GetERPcode(i);
                    row.Cells["Delete"].Value = "刪除";
                    //panel.Rows.Add(row);
                }
                else if (i == "004")
                {
                    //row = new GridRow(i, "", GetERPcode(i), "刪除");
                    //OperSuperGridControl.PrimaryGrid.Rows.Add(row);
                    row.Cells["Oper1Ary"].Value = i;
                    row.Cells["Oper2Ary"].Value = "";
                    row.Cells["IQC"].Value = false;
                    row.Cells["IPQC"].Value = false;
                    row.Cells["ERP料號"].Value = GetERPcode(i);
                    row.Cells["Delete"].Value = "刪除";
                    //panel.Rows.Add(row);
                } 
                else
                {
                    //row = new GridRow(i, GetOp2(i), GetERPcode(i), "刪除");
                    //OperSuperGridControl.PrimaryGrid.Rows.Add(row);
                    row.Cells["Oper1Ary"].Value = i;
                    row.Cells["Oper2Ary"].Value = GetOp2(i);
                    row.Cells["IQC"].Value = false;
                    row.Cells["IPQC"].Value = false;
                    row.Cells["ERP料號"].Value = GetERPcode(i);
                    row.Cells["Delete"].Value = "刪除";
                    //panel.Rows.Add(row);
                }
            }
            catch (System.Exception ex)
            {
                MessageBox.Show(ex.ToString());
            }
        }

        private bool CheckOper1Status(out List<string> ListSelectOper)
        {
            ListSelectOper = new List<string>();
            try
            {
                //一般製程
                if (check001.Checked)
                {
                    ListSelectOper.Add(check001.Text);
                }
                if (check210.Checked)
                {
                    ListSelectOper.Add(check210.Text);
                }
                if (check220.Checked)
                {
                    ListSelectOper.Add(check220.Text);
                }
                if (check230.Checked)
                {
                    ListSelectOper.Add(check230.Text);
                }
                if (check240.Checked)
                {
                    ListSelectOper.Add(check240.Text);
                }
                if (check250.Checked)
                {
                    ListSelectOper.Add(check250.Text);
                }
                if (check260.Checked)
                {
                    ListSelectOper.Add(check260.Text);
                }
                if (check270.Checked)
                {
                    ListSelectOper.Add(check270.Text);
                }
                if (check280.Checked)
                {
                    ListSelectOper.Add(check280.Text);
                }
                if (check290.Checked)
                {
                    ListSelectOper.Add(check290.Text);
                }
                if (check300.Checked)
                {
                    ListSelectOper.Add(check300.Text);
                }
                if (check310.Checked)
                {
                    ListSelectOper.Add(check310.Text);
                }
                if (check320.Checked)
                {
                    ListSelectOper.Add(check320.Text);
                }
                if (check330.Checked)
                {
                    ListSelectOper.Add(check330.Text);
                }
                if (check340.Checked)
                {
                    ListSelectOper.Add(check340.Text);
                }
                if (check350.Checked)
                {
                    ListSelectOper.Add(check350.Text);
                }
                if (check360.Checked)
                {
                    ListSelectOper.Add(check360.Text);
                }
                if (check370.Checked)
                {
                    ListSelectOper.Add(check370.Text);
                }
                if (check380.Checked)
                {
                    ListSelectOper.Add(check380.Text);
                }
                if (check999.Checked)
                {
                    ListSelectOper.Add(check999.Text);
                }

                //精鑄製程
                if (check105.Checked)
                {
                    ListSelectOper.Add(check105.Text);
                }
                if (check110.Checked)
                {
                    ListSelectOper.Add(check110.Text);
                }
                if (check115.Checked)
                {
                    ListSelectOper.Add(check115.Text);
                }
                if (check120.Checked)
                {
                    ListSelectOper.Add(check120.Text);
                }
                if (check130.Checked)
                {
                    ListSelectOper.Add(check130.Text);
                }
                if (check140.Checked)
                {
                    ListSelectOper.Add(check140.Text);
                }
                if (check150.Checked)
                {
                    ListSelectOper.Add(check150.Text);
                }
                if (check160.Checked)
                {
                    ListSelectOper.Add(check160.Text);
                }
                if (check170.Checked)
                {
                    ListSelectOper.Add(check170.Text);
                }
                if (check180.Checked)
                {
                    ListSelectOper.Add(check180.Text);
                }
                if (check199.Checked)
                {
                    ListSelectOper.Add(check199.Text);
                }
            }
            catch (System.Exception ex)
            {
                return false;
            }
            return true;
        }

        private bool ClearSelectOper1()
        {
            try
            {
                //一般製程
                check001.Checked = false; check210.Checked = false; check220.Checked = false; check230.Checked = false;
                check240.Checked = false; check250.Checked = false; check260.Checked = false; check270.Checked = false;
                check280.Checked = false; check290.Checked = false; check300.Checked = false; check310.Checked = false;
                check320.Checked = false; check330.Checked = false; check340.Checked = false; check350.Checked = false;
                check360.Checked = false; check370.Checked = false; check380.Checked = false; check999.Checked = false;
                
                //精鑄製程
                check105.Checked = false; check110.Checked = false; check115.Checked = false; check120.Checked = false;
                check130.Checked = false; check140.Checked = false; check150.Checked = false; check160.Checked = false;
                check170.Checked = false; check180.Checked = false; check199.Checked = false; 
            }
            catch (System.Exception ex)
            {
                return false;
            }
            return true;
        }

        private void SelectPartFileBtn_Click(object sender, EventArgs e)
        {
            try
            {
                openFileDialog1.Filter = "Part Files (*.prt)|*.prt|All Files (*.*)|*.*";
                DialogResult result = openFileDialog1.ShowDialog();
                if (result == DialogResult.OK)
                {
                    //取得檔案名稱(檔名+副檔名)
                    labelPartFileName.Text = openFileDialog1.SafeFileName;
                    //取得檔案完整路徑(路徑+檔名+副檔名)
                    souPartFilePath = openFileDialog1.FileName;
                    //開啟選擇的檔案
                    CaxPart.OpenBaseDisplay(souPartFilePath);
                }
                Part AssemblyPart = theSession.Parts.Display;
                AssembliesComp = new List<string>();
                NXOpen.Assemblies.ComponentAssembly casm = AssemblyPart.ComponentAssembly;
                List<NXOpen.Assemblies.Component> temp = new List<NXOpen.Assemblies.Component>();
                CaxAsm.GetCompChildren(casm.RootComponent, ref temp);
                foreach (NXOpen.Assemblies.Component i in temp)
                {
                    AssembliesComp.Add(i.DisplayName + ".prt");
                }
                
            }
            catch (System.Exception ex)
            {
                MessageBox.Show("選擇成品檔案錯誤，請聯繫開發工程師");
                this.Close();
            }
        }
        private void SeleteBilletFile_Click(object sender, EventArgs e)
        {
            try
            {
                openFileDialog1.Filter = "Part Files (*.prt)|*.prt|All Files (*.*)|*.*";
                DialogResult result = openFileDialog1.ShowDialog();
                if (result == DialogResult.OK)
                {
                    //取得檔案名稱(檔名+副檔名)
                    BilletFileName.Text = openFileDialog1.SafeFileName;
                    //取得檔案完整路徑(路徑+檔名+副檔名)
                    souBilletFilePath = openFileDialog1.FileName;
                }
            }
            catch (System.Exception ex)
            {
                MessageBox.Show("選擇胚料檔案錯誤，請聯繫開發工程師");
                this.Close();
            }
        }

        private void OK_Click(object sender, EventArgs e)
        {
            
            //先關閉所有檔案
            CaxPart.CloseAllParts();

            try
            {
                if (Is_OldPart == true)
                {
                    #region 舊料號開啟
                    //定義總組立檔案、二階檔案、三階檔案名稱
                    string AsmCompFileFullPath = string.Format(@"{0}\{1}\{2}\{3}\{4}\{5}", CaxEnv.GetGlobaltekTaskDir(), CurrentOldCusName, CurrentOldPartNo, CurrentOldCusRev.ToUpper(), CurrentOldOpRev.ToUpper(), CurrentOldPartNo + "_MOT_" + CurrentOldOpRev.ToUpper() + ".prt");
                    string SecondFileFullPath = string.Format(@"{0}\{1}\{2}\{3}\{4}\{5}", CaxEnv.GetGlobaltekTaskDir(), CurrentOldCusName, CurrentOldPartNo, CurrentOldCusRev.ToUpper(), CurrentOldOpRev.ToUpper(), CurrentOldPartNo + "_OP" + "[Oper1]" + ".prt");
                    string ThirdFileFullPath_OIS = string.Format(@"{0}\{1}\{2}\{3}\{4}\{5}", CaxEnv.GetGlobaltekTaskDir(), CurrentOldCusName, CurrentOldPartNo, CurrentOldCusRev.ToUpper(), CurrentOldOpRev.ToUpper(), CurrentOldPartNo + "_OIS" + "[Oper1]" + ".prt");
                    string ThirdFileFullPath_CAM = string.Format(@"{0}\{1}\{2}\{3}\{4}\{5}", CaxEnv.GetGlobaltekTaskDir(), CurrentOldCusName, CurrentOldPartNo, CurrentOldCusRev.ToUpper(), CurrentOldOpRev.ToUpper(), CurrentOldPartNo + "_OP" + "[Oper1]" + "_CAM.prt");
                    string OPFolderPath = string.Format(@"{0}\{1}\{2}\{3}\{4}\{5}", CaxEnv.GetGlobaltekTaskDir(), CurrentOldCusName, CurrentOldPartNo, CurrentOldCusRev.ToUpper(), CurrentOldOpRev.ToUpper(), "OP" + "[Oper1]");
                    string tempSecondFileFullPath = SecondFileFullPath;
                    string tempThirdFileFullPath_OIS = ThirdFileFullPath_OIS;
                    string tempThirdFileFullPath_CAM = ThirdFileFullPath_CAM;
                    string tempOPFolderPath = OPFolderPath;

                    #region 舊料號---將值儲存起來
                    cPECreateData.cusName = comboBoxOldCusName.Text;
                    cPECreateData.partName = comboBoxOldPartNo.Text;
                    cPECreateData.cusRev = comboBoxOldCusRev.Text.ToUpper();
                    cPECreateData.opRev = comboBoxOldOpRev.Text.ToUpper();
                    cPECreateData.listOperation = new List<Operation>();
                    cPECreateData.oper1Ary = new List<string>();
                    cPECreateData.oper2Ary = new List<string>();
                    for (int i = 0; i < panel.Rows.Count; i++)
                    {
                        if (panel.Rows.Count == 0)
                        {
                            MessageBox.Show("尚未選擇製程序與製程別！");
                            return;
                        }

                        if (((GridRow)panel.GetRowFromIndex(i)).Cells["Oper2Ary"].Value.ToString() == "")
                        {
                            MessageBox.Show("製程序" + ((GridRow)panel.GetRowFromIndex(i)).Cells["Oper1Ary"].Value + "尚未選取製程別！");
                            return;
                        }

                        Operation cOperation = new Operation();
                        cOperation.Oper1 = ((GridRow)panel.GetRowFromIndex(i)).Cells["Oper1Ary"].Value.ToString();
                        cOperation.Oper2 = ((GridRow)panel.GetRowFromIndex(i)).Cells["Oper2Ary"].Value.ToString();
                        if ((bool)((GridRow)panel.GetRowFromIndex(i)).Cells["IQC"].Value == true)
                        {
                            cOperation.Form = "IQC";
                        }
                        else if ((bool)((GridRow)panel.GetRowFromIndex(i)).Cells["IPQC"].Value == true)
                        {
                            cOperation.Form = "IPQC";
                        }
                        else
                        {
                            cOperation.Form = null;
                        }
                        cOperation.ERPCode = ((GridRow)panel.GetRowFromIndex(i)).Cells["ERP料號"].Value.ToString();
                        cPECreateData.listOperation.Add(cOperation);
                        cPECreateData.oper1Ary.Add(((GridRow)panel.GetRowFromIndex(i)).Cells["Oper1Ary"].Value.ToString());
                        cPECreateData.oper2Ary.Add(((GridRow)panel.GetRowFromIndex(i)).Cells["Oper2Ary"].Value.ToString());
                    }
                    #endregion

                    #region 舊料號---建立新插入的製程資料夾
                    foreach (string i in ListAddOper)
                    {
                        OPFolderPath = tempOPFolderPath;
                        OPFolderPath = OPFolderPath.Replace("[Oper1]", i);
                        string OISFolderPath = string.Format(@"{0}\{1}", OPFolderPath, "OIS");
                        string CAMFolderPath = string.Format(@"{0}\{1}", OPFolderPath, "CAM");

                        if (!File.Exists(OISFolderPath))
                        {
                            try
                            {
                                System.IO.Directory.CreateDirectory(OISFolderPath);
                            }
                            catch (System.Exception ex)
                            {
                                MessageBox.Show(ex.ToString());
                                return;
                            }
                        }
                        if (!File.Exists(CAMFolderPath))
                        {
                            try
                            {
                                System.IO.Directory.CreateDirectory(CAMFolderPath);
                            }
                            catch (System.Exception ex)
                            {
                                MessageBox.Show(ex.ToString());
                                return;
                            }
                        }
                    }
                    #endregion

                    #region 舊料號---開啟總組立
                    if (File.Exists(AsmCompFileFullPath))
                    {
                        //組件存在，直接開啟任務組立
                        BasePart newAsmPart;
                        status = CaxPart.OpenBaseDisplay(AsmCompFileFullPath, out newAsmPart);
                        if (!status)
                        {
                            MessageBox.Show("組立開啟失敗，料號不可有中文字！");
                            return;
                        }
                    }
                    else
                    {
                        MessageBox.Show("開啟失敗：找不到總組立" + Path.GetFileNameWithoutExtension(AsmCompFileFullPath));
                        return;
                    }
                    #endregion

                    #region 舊料號---建立二階檔案
                    string secondComp = "";
                    foreach (string i in ListAddOper)
                    {
                        //設定一階為WorkComp
                        CaxAsm.SetWorkComponent(null);

                        secondComp = string.Format(@"{0}\{1}", Path.GetDirectoryName(AsmCompFileFullPath), comboBoxOldPartNo.Text + "_OP" + i + ".prt");
                        status = CaxAsm.CreateNewEmptyCompNoReturn(secondComp);
                        if (!status)
                        {
                            MessageBox.Show("建立二階製程檔失敗");
                            return;
                        }
                    }
                    #endregion

                    #region 舊料號---建立三階檔案
                    //先取得所有二階檔案
                    List<NXOpen.Assemblies.Component> secondChildenComp = new List<NXOpen.Assemblies.Component>();
                    CaxAsm.GetCompChildren(out secondChildenComp);

                    string thirdComp_OIS = "", thirdComp_CAM = "";
                    foreach (string i in ListAddOper)
                    {
                        foreach (NXOpen.Assemblies.Component j in secondChildenComp)
                        {
                            if (!j.DisplayName.Split(new string[] { "OP" }, StringSplitOptions.RemoveEmptyEntries)[1].Contains(i))
                                continue;

                            CaxAsm.SetWorkComponent(j);

                            #region 舊料號---組立三階OIS檔
                            thirdComp_OIS = string.Format(@"{0}\{1}", Path.GetDirectoryName(AsmCompFileFullPath), comboBoxOldPartNo.Text + "_OIS" + i + ".prt");
                            status = CaxAsm.CreateNewEmptyCompNoReturn(thirdComp_OIS);
                            if (!status)
                            {
                                CaxLog.ShowListingWindow("組立三階OIS檔失敗");
                                return;
                            }
                            #endregion

                            //001、900不需要組立三階CAM檔
                            if (j.DisplayName.Contains("OP001") || j.DisplayName.Contains("OP900"))
                                continue;

                            #region 舊料號---組立三階CAM檔
                            thirdComp_CAM = string.Format(@"{0}\{1}", Path.GetDirectoryName(AsmCompFileFullPath), comboBoxOldPartNo.Text + "_OP" + i + "_CAM.prt");
                            status = CaxAsm.CreateNewEmptyCompNoReturn(thirdComp_CAM);
                            if (!status)
                            {
                                CaxLog.ShowListingWindow("組立三階CAM檔失敗");
                                return;
                            }
                            #endregion
                        }
                    }
                    #endregion

                    #region 舊料號---建立四階檔案
                    //成品檔案目的地
                    //string desPartFilePath = string.Format(@"{0}\{1}", Path.GetDirectoryName(AsmCompFileFullPath), Path.GetFileName(souOldPartFilePath));
                    string desPartFilePath = cComPEMain.partFilePath;
                    //胚料檔案目的地
                    //string desBilletFilePath = "";
                    string desBilletFilePath = cComPEMain.billetFilePath;
                    //當前ME、TE檔案
                    string CurrentMEPartFilePath = "", CurrentTEPartFilePath = "";

                    foreach (string i in ListAddOper)
                    {
                        foreach (NXOpen.Assemblies.Component j in secondChildenComp)
                        {
                            if (!j.DisplayName.Split(new string[] { "OP" }, StringSplitOptions.RemoveEmptyEntries)[1].Contains(i))
                            {
                                continue;
                            }

                            //由二階檔案取得三階檔案
                            List<NXOpen.Assemblies.Component> thirdChildenComp = new List<NXOpen.Assemblies.Component>();
                            CaxAsm.GetCompChildren(out thirdChildenComp, j);

                            //建立三階與四階的檔案txt，方便ME、TE下載時可讀取要下載哪些檔案
                            List<string> listMEFileName = new List<string>();
                            List<string> listTEFileName = new List<string>();

                            foreach (NXOpen.Assemblies.Component k in thirdChildenComp)
                            {
                                #region (註解)舊料號---重新判斷製程順序關係，如順序改變則重新組立前一工段檔案
                                /*
                                if (!j.Name.Split(new string[] { "OP" }, StringSplitOptions.RemoveEmptyEntries)[1].Contains(i))
                                {
                                    //取得目前OIS
                                    string OIS = j.Name.Split(new string[] { "OP" }, StringSplitOptions.RemoveEmptyEntries)[1];

                                    //取得前一工段OIS
                                    string FrontOIS = "";
                                    for (int ii = 0; ii < cPECreateData.oper1Ary.Count; ii++)
                                    {
                                        if (cPECreateData.oper1Ary[ii] == OIS)
                                            FrontOIS = cPECreateData.oper1Ary[ii - 1];
                                    }

                                    //判斷四階檔案中是否已經有前一工段OIS，由三階檔案取得四階檔案
                                    List<NXOpen.Assemblies.Component> fourthChildenComp = new List<NXOpen.Assemblies.Component>();
                                    CaxAsm.GetCompChildren(out fourthChildenComp, j);


                                    continue;
                                }
                                */
                                #endregion

                                CurrentMEPartFilePath = "";
                                CurrentTEPartFilePath = "";

                                CaxAsm.SetWorkComponent(k);
                                //目前的OIS
                                string CurrentOIS = "";

                                if (k.DisplayName.Contains("OIS"))
                                {
                                    //加入三階OIS檔案名稱
                                    listMEFileName.Add(Path.GetFileName(((Part)k.Prototype).FullPath));

                                    #region 舊料號---組立四階檔案-OIS
                                    if (k.DisplayName.Contains("OIS001"))
                                    {
                                        CurrentOIS = k.DisplayName.Split(new string[] { "OIS" }, StringSplitOptions.RemoveEmptyEntries)[1];

                                        #region 舊料號---組立成品檔案
                                        status = AddEditPartFile(cComPEMain.partFilePath, desPartFilePath);
                                        if (!status)
                                            return;
                                        else
                                            listMEFileName.Add(Path.GetFileName(desPartFilePath));
                                        #endregion

                                        #region 舊料號---組立胚料檔案(有檔案組檔案；沒檔案則組一個空檔)
                                        status = AddEditBilletFile(cComPEMain.billetFilePath, ref desBilletFilePath);
                                        if (!status)
                                            return;
                                        else
                                            listMEFileName.Add(Path.GetFileName(desBilletFilePath));
                                        #endregion

                                        #region 舊料號---組立檢具
                                        status = AddEditInspectionFile(k.DisplayName, ref CurrentMEPartFilePath);
                                        if (!status)
                                            return;
                                        else
                                            listMEFileName.Add(Path.GetFileName(CurrentMEPartFilePath));
                                        #endregion
                                    }
                                    else if (k.DisplayName.Contains("OIS210"))
                                    {
                                        CurrentOIS = k.DisplayName.Split(new string[] { "OIS" }, StringSplitOptions.RemoveEmptyEntries)[1];

                                        #region 舊料號---組立成品檔案
                                        status = AddEditPartFile(cComPEMain.partFilePath, desPartFilePath);
                                        if (!status)
                                            return;
                                        else
                                            listMEFileName.Add(Path.GetFileName(desPartFilePath));
                                        #endregion

                                        #region 舊料號---組立胚料檔案(有檔案組檔案；沒檔案則組一個空檔)
                                        status = AddEditBilletFile(cComPEMain.billetFilePath, ref desBilletFilePath);
                                        if (!status)
                                            return;
                                        else
                                            listMEFileName.Add(Path.GetFileName(desBilletFilePath));
                                        #endregion

                                        #region 舊料號---組立當前ME檔案
                                        status = AddEditCurrentMEFile(CurrentOIS, ref CurrentMEPartFilePath);
                                        if (!status)
                                            return;
                                        else
                                            listMEFileName.Add(Path.GetFileName(CurrentMEPartFilePath));
                                        #endregion

                                        #region 舊料號---組立檢具
                                        status = AddEditInspectionFile(k.DisplayName, ref CurrentMEPartFilePath);
                                        if (!status)
                                            return;
                                        else
                                            listMEFileName.Add(Path.GetFileName(CurrentMEPartFilePath));
                                        #endregion
                                    }
                                    else if (k.DisplayName.Contains("OIS900"))
                                    {
                                        CurrentOIS = k.DisplayName.Split(new string[] { "OIS" }, StringSplitOptions.RemoveEmptyEntries)[1];

                                        #region 舊料號---組立成品檔案
                                        status = AddEditPartFile(cComPEMain.partFilePath, desPartFilePath);
                                        if (!status)
                                            return;
                                        else
                                            listMEFileName.Add(Path.GetFileName(desPartFilePath));
                                        #endregion
                                    }
                                    else
                                    {
                                        CurrentOIS = k.DisplayName.Split(new string[] { "OIS" }, StringSplitOptions.RemoveEmptyEntries)[1];

                                        #region 舊料號---組立成品檔案
                                        status = AddEditPartFile(cComPEMain.partFilePath, desPartFilePath);
                                        if (!status)
                                            return;
                                        else
                                            listMEFileName.Add(Path.GetFileName(desPartFilePath));
                                        #endregion

                                        #region 舊料號---組立胚料檔案(有檔案組檔案；沒檔案則組一個空檔)
                                        status = AddEditBilletFile(cComPEMain.billetFilePath, ref desBilletFilePath);
                                        if (!status)
                                            return;
                                        else
                                            listMEFileName.Add(Path.GetFileName(desBilletFilePath));
                                        #endregion

                                        #region 舊料號---組立前一個ME檔案
                                        status = AddEditFrontMEFile(CurrentOIS, cPECreateData, ref CurrentMEPartFilePath);
                                        if (!status)
                                            return;
                                        else
                                        {
                                            if (CurrentMEPartFilePath != "")
                                            {
                                                listMEFileName.Add(Path.GetFileName(CurrentMEPartFilePath));
                                            }
                                        }
                                        #endregion

                                        #region 舊料號---組立當前ME檔案
                                        status = AddEditCurrentMEFile(CurrentOIS, ref CurrentMEPartFilePath);
                                        if (!status)
                                            return;
                                        else
                                            listMEFileName.Add(Path.GetFileName(CurrentMEPartFilePath));
                                        #endregion

                                        #region 舊料號---組立檢具
                                        status = AddEditInspectionFile(k.DisplayName, ref CurrentMEPartFilePath);
                                        if (!status)
                                            return;
                                        else
                                            listMEFileName.Add(Path.GetFileName(CurrentMEPartFilePath));
                                        #endregion
                                    }
                                    #endregion

                                    #region 舊料號---將三階與四階的檔案txt輸出至每個OP的資料夾內，OIS放在OIS資料夾內；CAM放在CAM資料夾內
                                    string OISFolderPath = string.Format(@"{0}\{1}", Path.GetDirectoryName(AsmCompFileFullPath), "OP" + CurrentOIS);
                                    string[] OISFileNameTxt = listMEFileName.ToArray();
                                    System.IO.File.WriteAllLines(string.Format(@"{0}\{1}", OISFolderPath, "PartNameText_OIS.txt"), OISFileNameTxt);
                                    #endregion
                                }
                                else
                                {
                                    //加入三階CAM檔案名稱
                                    listTEFileName.Add(Path.GetFileName(((Part)k.Prototype).FullPath));

                                    #region 舊料號---組立四階檔案-CAM
                                    if (k.DisplayName.Contains("OP001") || k.DisplayName.Contains("OP900"))
                                    {
                                        continue;
                                    }
                                    else if (k.DisplayName.Contains("OP210"))
                                    {
                                        CurrentOIS = (k.DisplayName.Split(new string[] { "OP" }, StringSplitOptions.RemoveEmptyEntries)[1])
                                                            .Split(new string[] { "_CAM" }, StringSplitOptions.RemoveEmptyEntries)[0];

                                        #region 舊料號---組立成品檔案
                                        status = AddEditPartFile(cComPEMain.partFilePath, desPartFilePath);
                                        if (!status)
                                            return;
                                        else
                                            listTEFileName.Add(Path.GetFileName(desPartFilePath));
                                        #endregion

                                        #region 舊料號---組立胚料檔案(有檔案組檔案；沒檔案則組一個空檔)
                                        status = AddEditBilletFile(cComPEMain.billetFilePath, ref desBilletFilePath);
                                        if (!status)
                                            return;
                                        else
                                            listTEFileName.Add(Path.GetFileName(desBilletFilePath));
                                        #endregion

                                        #region 舊料號---組立當前ME檔案
                                        status = AddEditCurrentMEFile(CurrentOIS, ref CurrentMEPartFilePath);
                                        if (!status)
                                            return;
                                        else
                                            listTEFileName.Add(Path.GetFileName(CurrentMEPartFilePath));
                                        #endregion

                                        #region 舊料號---組立當前TE檔案
                                        status = AddEditCurrentTEFile(CurrentOIS, ref CurrentTEPartFilePath);
                                        if (!status)
                                            return;
                                        else
                                            listTEFileName.Add(Path.GetFileName(CurrentTEPartFilePath));
                                        #endregion

                                        #region 舊料號---組立治具
                                        status = AddEditFixtureFile(k.DisplayName, ref CurrentMEPartFilePath);
                                        if (!status)
                                            return;
                                        else
                                            listTEFileName.Add(Path.GetFileName(CurrentMEPartFilePath));
                                        #endregion
                                    }
                                    else
                                    {
                                        CurrentOIS = (k.DisplayName.Split(new string[] { "OP" }, StringSplitOptions.RemoveEmptyEntries)[1])
                                                            .Split(new string[] { "_CAM" }, StringSplitOptions.RemoveEmptyEntries)[0];

                                        #region 舊料號---組立成品檔案
                                        status = AddEditPartFile(cComPEMain.partFilePath, desPartFilePath);
                                        if (!status)
                                            return;
                                        else
                                            listTEFileName.Add(Path.GetFileName(desPartFilePath));
                                        #endregion

                                        #region 舊料號---組立胚料檔案(有檔案組檔案；沒檔案則組一個空檔)
                                        status = AddEditBilletFile(cComPEMain.billetFilePath, ref desBilletFilePath);
                                        if (!status)
                                            return;
                                        else
                                            listTEFileName.Add(Path.GetFileName(desBilletFilePath));
                                        #endregion

                                        #region 舊料號---組立前一個ME檔案
                                        status = AddEditFrontMEFile(CurrentOIS, cPECreateData, ref CurrentMEPartFilePath);
                                        if (!status)
                                            return;
                                        else
                                        {
                                            if (CurrentMEPartFilePath != "")
                                            {
                                                listTEFileName.Add(Path.GetFileName(CurrentMEPartFilePath));
                                            }
                                        }
                                        #endregion

                                        #region 舊料號---組立當前ME檔案
                                        status = AddEditCurrentMEFile(CurrentOIS, ref CurrentMEPartFilePath);
                                        if (!status)
                                            return;
                                        else
                                            listTEFileName.Add(Path.GetFileName(CurrentMEPartFilePath));
                                        #endregion

                                        #region 舊料號---組立當前TE檔案
                                        status = AddEditCurrentTEFile(CurrentOIS, ref CurrentTEPartFilePath);
                                        if (!status)
                                            return;
                                        else
                                            listTEFileName.Add(Path.GetFileName(CurrentTEPartFilePath));
                                        #endregion

                                        #region 舊料號---組立治具
                                        status = AddEditFixtureFile(k.DisplayName, ref CurrentMEPartFilePath);
                                        if (!status)
                                            return;
                                        else
                                            listTEFileName.Add(Path.GetFileName(CurrentMEPartFilePath));
                                        #endregion
                                    }
                                    #endregion

                                    #region 舊料號---將三階與四階的檔案txt輸出至每個OP的資料夾內，OIS放在OIS資料夾內；CAM放在CAM資料夾內
                                    string CAMFolderPath = string.Format(@"{0}\{1}", Path.GetDirectoryName(AsmCompFileFullPath), "OP" + CurrentOIS);
                                    string[] CAMFileNameTxt = listTEFileName.ToArray();
                                    System.IO.File.WriteAllLines(string.Format(@"{0}\{1}", CAMFolderPath, "PartNameText_CAM.txt"), CAMFileNameTxt);
                                    #endregion
                                }
                            }
                        }
                    }
                    
                    #endregion

                    

                    #region 舊料號---重新判斷製程順序關係，如順序改變則重新組立前一工段檔案
                    foreach (NXOpen.Assemblies.Component singleSecondComp in secondChildenComp)
                    {
                        //當前OIS
                        string CurrentOIS = singleSecondComp.DisplayName.Split(new string[] { "OP" }, StringSplitOptions.RemoveEmptyEntries)[1];

                        //001、210、900不比對
                        if (CurrentOIS == "001" || CurrentOIS == "210" || CurrentOIS == "900")
                            continue;

                        //取得前一工段OIS
                        string FrontOIS = "";
                        for (int i = 0; i < cPECreateData.oper1Ary.Count; i++)
                        {
                            //001、210、900不比對
                            if (cPECreateData.oper1Ary[i] == "001" || cPECreateData.oper1Ary[i] == "210" || cPECreateData.oper1Ary[i] == "900")
                                continue;

                            if (cPECreateData.oper1Ary[i] == CurrentOIS)
                            {
                                if (i==0)
                                    continue;
                                FrontOIS = cPECreateData.oper1Ary[i - 1];
                            }
                        }

                        //比對此製程序下的檔案是否有前一工段的ME檔案，如果有則跳下一個；如果沒有則組立
                        List<NXOpen.Assemblies.Component> ListChildrenComp = new List<NXOpen.Assemblies.Component>();
                        CaxAsm.GetCompChildren(singleSecondComp, ref ListChildrenComp);

                        bool chkFileExist = false;
                        foreach (NXOpen.Assemblies.Component singleComp in ListChildrenComp)
                        {
                            if (singleComp.DisplayName.Contains("_ME_" + FrontOIS))
                                chkFileExist = true;
                        }

                        if (chkFileExist)
                            continue;

                        //建立三階與四階的檔案txt，方便ME、TE下載時可讀取要下載哪些檔案
                        List<string> listMEFileName = new List<string>();
                        string PartNameText_OISPath = string.Format(@"{0}\{1}\{2}", Path.GetDirectoryName(AsmCompFileFullPath), "OP" + CurrentOIS, "PartNameText_OIS.txt");
                        listMEFileName = System.IO.File.ReadAllLines(PartNameText_OISPath).ToList();
                        List<string> listTEFileName = new List<string>();
                        string PartNameText_CAMPath = string.Format(@"{0}\{1}\{2}", Path.GetDirectoryName(AsmCompFileFullPath), "OP" + CurrentOIS, "PartNameText_CAM.txt");
                        listTEFileName = System.IO.File.ReadAllLines(PartNameText_CAMPath).ToList();

                        foreach (NXOpen.Assemblies.Component singleComp in ListChildrenComp)
                        {
                            if (singleComp.DisplayName.Contains("_OIS") & !singleComp.DisplayName.Contains("_INSPECTION"))
                            {
                                CaxAsm.SetWorkComponent(singleComp);

                                status = AddEditFrontMEFile(CurrentOIS, cPECreateData, ref CurrentMEPartFilePath);
                                if (!status)
                                    return;
                                else
                                    listMEFileName.Add(Path.GetFileName(CurrentMEPartFilePath));

                                #region 舊料號---將三階與四階的檔案txt輸出至每個OP的資料夾內，OIS放在OIS資料夾內；CAM放在CAM資料夾內
                                string[] OISFileNameTxt = listMEFileName.ToArray();
                                System.IO.File.WriteAllLines(PartNameText_OISPath, OISFileNameTxt);
                                #endregion
                            }
                            else if (singleComp.DisplayName.Contains("_CAM") & !singleComp.DisplayName.Contains("_CAM_FIXTURE"))
                            {
                                CaxAsm.SetWorkComponent(singleComp);

                                status = AddEditFrontMEFile(CurrentOIS, cPECreateData, ref CurrentMEPartFilePath);
                                if (!status)
                                    return;
                                else
                                    listTEFileName.Add(Path.GetFileName(CurrentMEPartFilePath));

                                #region 舊料號---將三階與四階的檔案txt輸出至每個OP的資料夾內，OIS放在OIS資料夾內；CAM放在CAM資料夾內
                                string[] CAMFileNameTxt = listTEFileName.ToArray();
                                System.IO.File.WriteAllLines(PartNameText_CAMPath, CAMFileNameTxt);
                                #endregion
                            }
                        }

                        //CaxAsm.SetWorkComponent(singleSecondComp);
                        //由二階檔案取得三階檔案
                        //List<NXOpen.Assemblies.Component> thirdChildenComp = new List<NXOpen.Assemblies.Component>();
                        //CaxAsm.GetCompChildren(out thirdChildenComp, singleSecondComp);

                    }
                    #endregion

                    #region 舊料號---刪除檔案(如果有必須刪除的才執行)
                    //取得此料號舊版的所有製程序
                    Dictionary<int, OpDataValue> DicOldOpData = new Dictionary<int, OpDataValue>();
                    List<string> listOldOP = new List<string>();
                    foreach (Com_PartOperation ii in cComPEMain.comPartOperation)
                    {
                        OpDataValue sOldOpDataValue = new OpDataValue();
                        sOldOpDataValue.op1 = ii.operation1;
                        sOldOpDataValue.op2 = session.QueryOver<Sys_Operation2>().Where(x => x.operation2SrNo == ii.sysOperation2.operation2SrNo).SingleOrDefault().operation2Name;
                        sOldOpDataValue.form = ii.form;
                        sOldOpDataValue.erp = ii.erpCode;
                        DicOldOpData.Add(ii.partOperationSrNo, sOldOpDataValue);
                        listOldOP.Add(ii.operation1);
                    }
                    //比對舊版製程，取得本次要【刪除】的製程
                    IEnumerable<string> listDeleteOP = new List<string>();
                    listDeleteOP = listOldOP.Except(cPECreateData.oper1Ary);

                    CaxAsm.SetWorkComponent(null);
                    Part rootPart = theSession.Parts.Work;
                    List<NXOpen.Assemblies.Component> allComp = new List<NXOpen.Assemblies.Component>();
                    CaxAsm.GetCompChildren(rootPart.ComponentAssembly.RootComponent, ref allComp);
                    foreach (string i in listDeleteOP)
                    {
                        try
                        {
                            foreach (NXOpen.Assemblies.Component j in allComp)
                            {
                                if (j.DisplayName.Contains("_OP" + i) || j.DisplayName.Contains("_ME_" + i))
                                {
                                    CaxPublic.DelectObject(j);
                                }
                            }
                        }
                        catch (System.Exception ex)
                        {
                            continue;
                        }
                    }
                    #endregion

                    CaxPart.Save();
                    CaxAsm.SetWorkComponent(null);
                    //CaxAsm.GetCompChildren(theSession.Parts.Work.ComponentAssembly.RootComponent, ref allComp);

                    #region (註解中)舊料號---塞ERP屬性、品名、材質
                    /*
                    PartLoadStatus partLoadStatus1;
                    foreach (NXOpen.Assemblies.Component i in allComp)
                    {
                        try
                        {
                            for (int j = 0; j < panel.Rows.Count; j++)
                            {
                                if (i.DisplayName.Contains("OIS") & i.DisplayName.Contains(panel.GetCell(j, 0).Value.ToString()))
                                {
                                    Part MakeDisplayPart = (Part)i.Prototype;
                                    theSession.Parts.SetDisplay(MakeDisplayPart, true, true, out partLoadStatus1);
                                    MakeDisplayPart.SetAttribute("ERPCODE", panel.GetCell(j, 2).Value.ToString());
                                    MakeDisplayPart.SetAttribute("PartDescriptionPos", PartDesc.Text);
                                    MakeDisplayPart.SetAttribute("MaterialPos", Material.Text);
                                }
                            }
                        }
                        catch (System.Exception ex)
                        {
                        	
                        }
                        
                    }

                    //返回MOT架構
                    theSession.Parts.SetDisplay(rootPart, true, true, out partLoadStatus1);
                    partLoadStatus1.Dispose();
                    */
                    #endregion

                    #region 舊料號---寫出PECreateData.dat
                    string PECreateDataJsonDat = string.Format(@"{0}\{1}\{2}\{3}\{4}\{5}\{6}", CaxEnv.GetGlobaltekTaskDir(), CurrentOldCusName, CurrentOldPartNo, CurrentOldCusRev, CurrentOldOpRev, "MODEL", "PECreateData.dat");
                    status = CaxFile.WriteJsonFileData(PECreateDataJsonDat, cPECreateData);
                    if (!status)
                    {
                        MessageBox.Show("PECreateData.dat 輸出失敗...");
                        return;
                    }
                    #endregion



                    #region 寫Update Datebase
                    
                    using (ISession session = MyHibernateHelper.SessionFactory.OpenSession())
                    {
                        #region (註解)直接刪除全部舊製程，再重新插入
                        
                        ////刪除舊製程
                        //IList<Com_PartOperation> listComPartOperation = session.QueryOver<Com_PartOperation>()
                        //                                          .Where(x => x.comPEMain == oldComPEMain).List<Com_PartOperation>();
                        //using (ITransaction trans = session.BeginTransaction())
                        //{
                        //    foreach (Com_PartOperation i in listComPartOperation)
                        //    {
                        //        session.Delete(i);
                        //    }
                        //    trans.Commit();
                        //}

                        ////重新插入Com_PartOperation
                        //List<Com_PartOperation> ComPartOperation = new List<Com_PartOperation>();
                        //for (int i = 0; i < cPECreateData.listOperation.Count; i++)
                        //{
                        //    cCom_PartOperation = new Com_PartOperation();
                        //    cCom_PartOperation.comPEMain = oldComPEMain;
                        //    cCom_PartOperation.operation1 = cPECreateData.listOperation[i].Oper1;
                        //    cCom_PartOperation.sysOperation2 = session.QueryOver<Sys_Operation2>()
                        //                                        .Where(x => x.operation2Name == cPECreateData.listOperation[i].Oper2)
                        //                                        .SingleOrDefault<Sys_Operation2>();
                        //    cCom_PartOperation.erpCode = cPECreateData.listOperation[i].ERPCode;
                        //    ComPartOperation.Add(cCom_PartOperation);
                        //}

                        //using (ITransaction trans = session.BeginTransaction())
                        //{
                        //    foreach (Com_PartOperation i in ComPartOperation)
                        //    {
                        //        session.Save(i);
                        //    }
                        //    trans.Commit();
                        //}
                        //session.Close();
                        
                        #endregion


                        //取得此料號舊版的所有製程序
                        //Dictionary<int, OpDataValue> DicOldOpData = new Dictionary<int, OpDataValue>();
                        //List<string> listOldOP = new List<string>();
                        //foreach (Com_PartOperation ii in cComPEMain.comPartOperation)
                        //{
                        //    OpDataValue sOldOpDataValue = new OpDataValue();
                        //    sOldOpDataValue.op1 = ii.operation1;
                        //    sOldOpDataValue.op2 = session.QueryOver<Sys_Operation2>().Where(x => x.operation2SrNo == ii.sysOperation2.operation2SrNo).SingleOrDefault().operation2Name;
                        //    sOldOpDataValue.erp = ii.erpCode;
                        //    DicOldOpData.Add(ii.partOperationSrNo, sOldOpDataValue);
                        //    //DicOldOpData.Add(ii.operation1, session.QueryOver<Sys_Operation2>().Where(x => x.operation2SrNo == ii.sysOperation2.operation2SrNo).SingleOrDefault().operation2Name);
                        //    listOldOP.Add(ii.operation1);
                        //}

                        //新Data比對舊Data整理出每個製程需【更新】【新增】的資訊
                        int addcount = 0;
                        Dictionary<OpDataKey, OpDataValue> DicUpdateData = new Dictionary<OpDataKey, OpDataValue>();
                        for (int i = 0; i < cPECreateData.listOperation.Count;i++ )
                        {
                            addcount++;
                            int count = 0;
                            foreach (KeyValuePair<int, OpDataValue> kvp in DicOldOpData)
                            {
                                if (cPECreateData.listOperation[i].Oper1 == kvp.Value.op1)
                                {
                                    if (cPECreateData.listOperation[i].Oper2 != kvp.Value.op2 || cPECreateData.listOperation[i].Form != kvp.Value.form || cPECreateData.listOperation[i].ERPCode != kvp.Value.erp)
                                    {
                                        OpDataKey sOpDataKey = new OpDataKey();
                                        sOpDataKey.partOpSrNo = kvp.Key;
                                        sOpDataKey.flag = "Update";
                                        OpDataValue sOpDataValue = new OpDataValue();
                                        sOpDataValue.op1 = cPECreateData.listOperation[i].Oper1;
                                        sOpDataValue.op2 = cPECreateData.listOperation[i].Oper2;
                                        sOpDataValue.form = cPECreateData.listOperation[i].Form;
                                        sOpDataValue.erp = cPECreateData.listOperation[i].ERPCode;
                                        DicUpdateData.Add(sOpDataKey, sOpDataValue);
                                    }
                                }
                                else
                                {
                                    count++;
                                }
                            }
                            if (count == DicOldOpData.Count)
                            {
                                OpDataKey sOpDataKey = new OpDataKey();
                                sOpDataKey.partOpSrNo = addcount;
                                sOpDataKey.flag = "Add";
                                OpDataValue sOpDataValue = new OpDataValue();
                                sOpDataValue.op1 = cPECreateData.listOperation[i].Oper1;
                                sOpDataValue.op2 = cPECreateData.listOperation[i].Oper2;
                                sOpDataValue.form = cPECreateData.listOperation[i].Form;
                                sOpDataValue.erp = cPECreateData.listOperation[i].ERPCode;
                                //if (cPECreateData.listOperation[i].ERPCode != "")
                                //{
                                //    sOpDataValue.erp = cPECreateData.listOperation[i].ERPCode;
                                //}
                                DicUpdateData.Add(sOpDataKey, sOpDataValue);
                            }
                        }

                        //比對舊版製程，取得本次要【刪除】的製程
                        //IEnumerable<string> listDeleteOP = new List<string>();
                        //listDeleteOP = listOldOP.Except(cPECreateData.oper1Ary);

                        foreach (string i in listDeleteOP)
                        {
                            foreach (KeyValuePair<int, OpDataValue> kvp in DicOldOpData)
                            {
                                if (i == kvp.Value.op1)
                                {
                                    OpDataKey sOpDataKey = new OpDataKey();
                                    sOpDataKey.partOpSrNo = kvp.Key;
                                    sOpDataKey.flag = "Delete";
                                    DicUpdateData.Add(sOpDataKey, kvp.Value);
                                }
                            }
                        }
                        session.Close();
                        ISession session2 = MyHibernateHelper.SessionFactory.OpenSession();
                        
                        foreach (KeyValuePair<OpDataKey, OpDataValue> kvp in DicUpdateData)
                        {
                            using (ITransaction trans = session2.BeginTransaction())
                            {
                                Com_PartOperation comPartOp = new Com_PartOperation();
                                comPartOp.comPEMain = cComPEMain;
                                comPartOp.operation1 = kvp.Value.op1;
                                comPartOp.sysOperation2 = session2.QueryOver<Sys_Operation2>()
                                                            .Where(x => x.operation2Name == kvp.Value.op2)
                                                            .SingleOrDefault<Sys_Operation2>();
                                comPartOp.form = kvp.Value.form;
                                comPartOp.erpCode = kvp.Value.erp;
                                if (kvp.Key.flag == "Update")
                                {
                                    comPartOp.partOperationSrNo = kvp.Key.partOpSrNo;
                                    session2.Update(comPartOp);
                                }
                                else if (kvp.Key.flag == "Add")
                                {
                                    session2.Save(comPartOp);
                                }
                                else if (kvp.Key.flag == "Delete")
                                {
                                    //如果DB是不能為NULL，則刪除的時候要指定否則無法刪除
                                    comPartOp.partOperationSrNo = kvp.Key.partOpSrNo;
                                    session2.Delete(comPartOp);
                                }
                                trans.Commit();
                            }
                        }
                        session2.Close();
                    }
                    
                    #endregion
                    
                    
                    #endregion
                }
                else
                {

                    #region 檢查資訊是否完整
                    status = DataCompleted();
                    if (!status)
                        return;
                    #endregion

                    //CaxLoading.RunDlg();

                    #region 新料號---定義根目錄

                    //定義MODEL資料夾路徑
                    string ModelFolderFullPath = string.Format(@"{0}\{1}\{2}\{3}\{4}\{5}", CaxEnv.GetGlobaltekTaskDir(), CusName, PartNo, CusRev.ToUpper(), OpRev.ToUpper(), "MODEL");

                    //定義總組立檔案名稱
                    string AsmCompFileFullPath = string.Format(@"{0}\{1}\{2}\{3}\{4}\{5}", CaxEnv.GetGlobaltekTaskDir(), CusName, PartNo, CusRev.ToUpper(), OpRev.ToUpper(), PartNo + "_MOT_" + OpRev.ToUpper() + ".prt");

                    //定義CAM資料夾路徑、OIS資料夾路徑、三階檔案路徑
                    string CAMFolderPath = "", OISFolderPath = "", ThridOperPartPath = "";

                    #endregion

                    #region 新料號---建立MODEL資料夾
                    if (!File.Exists(ModelFolderFullPath))
                    {
                        try
                        {
                            System.IO.Directory.CreateDirectory(ModelFolderFullPath);
                        }
                        catch (System.Exception ex)
                        {
                            MessageBox.Show(ex.ToString());
                            return;
                        }
                    }
                    #endregion

                    #region (2016.12.29註解)複製客戶檔案到MODEL資料夾內
                    //判斷客戶的檔案是否存在
                    //status = System.IO.File.Exists(PartPath);
                    //if (!status)
                    //{
                    //    MessageBox.Show("指定的檔案不存在，請再次確認");
                    //    return;
                    //}

                    //建立MODEL資料夾內客戶檔案路徑
                    //string CustomerPartFullPath = string.Format(@"{0}\{1}", ModelFolderFullPath, PartNo + ".prt");

                    //開始複製
                    //if (!System.IO.File.Exists(CustomerPartFullPath))
                    //{
                    //    File.Copy(PartPath, CustomerPartFullPath, true);
                    //}
                    #endregion

                    #region 新料號---將值儲存起來

                    cPECreateData.cusName = CusName;
                    cPECreateData.partName = PartNo;
                    cPECreateData.partDesc = PartDes;
                    cPECreateData.material = PartMaterial;
                    cPECreateData.cusRev = CusRev.ToUpper();
                    cPECreateData.opRev = OpRev.ToUpper();
                    //cPE_OutPutDat.PartPath = PartPath;
                    cPECreateData.listOperation = new List<Operation>();
                    cPECreateData.oper1Ary = new List<string>();
                    cPECreateData.oper2Ary = new List<string>();
                    Operation cOperation = new Operation();
                    for (int i = 0; i < panel.Rows.Count; i++)
                    {
                        if (panel.Rows.Count == 0)
                        {
                            MessageBox.Show("尚未選擇製程序與製程別！");
                            return;
                        }

                        if (((GridRow)panel.GetRowFromIndex(i)).Cells["Oper2Ary"].Value.ToString() == "")
                        {
                            MessageBox.Show("製程序" + ((GridRow)panel.GetRowFromIndex(i)).Cells["Oper1Ary"].Value + "尚未選取製程別！");
                            return;
                        }

                        cOperation = new Operation();
                        cOperation.Oper1 = ((GridRow)panel.GetRowFromIndex(i)).Cells["Oper1Ary"].Value.ToString();
                        cOperation.Oper2 = ((GridRow)panel.GetRowFromIndex(i)).Cells["Oper2Ary"].Value.ToString();
                        //cOperation.Oper1 = panel.GetCell(i, 0).Value.ToString();
                        //cOperation.Oper2 = panel.GetCell(i, 1).Value.ToString();
                        if ((bool)((GridRow)panel.GetRowFromIndex(i)).Cells["IQC"].Value == true)
                        {
                            cOperation.Form = "IQC";
                        }
                        else if ((bool)((GridRow)panel.GetRowFromIndex(i)).Cells["IPQC"].Value == true)
                        {
                            cOperation.Form = "IPQC";
                        }
                        else
                        {
                            cOperation.Form = null;
                        }
                        cOperation.ERPCode = ((GridRow)panel.GetRowFromIndex(i)).Cells["ERP料號"].Value.ToString();
                        //cOperation.ERPCode = panel.GetCell(i, 4).Value.ToString();

                        //建立CAM資料夾路徑
                        CAMFolderPath = string.Format(@"{0}\{1}\{2}", Path.GetDirectoryName(AsmCompFileFullPath), "OP" + ((GridRow)panel.GetRowFromIndex(i)).Cells["Oper1Ary"].Value.ToString(), "CAM");

                        //儲存CAM資料夾路徑
                        //cOperation.CAMFolderPath = CAMFolderPath;

                        //建立CAM資料夾
                        if (!File.Exists(CAMFolderPath))
                        {
                            try
                            {
                                System.IO.Directory.CreateDirectory(CAMFolderPath);
                            }
                            catch (System.Exception ex)
                            {
                                MessageBox.Show(ex.ToString());
                                return;
                            }
                        }

                        //建立OIS資料夾路徑
                        OISFolderPath = string.Format(@"{0}\{1}\{2}", Path.GetDirectoryName(AsmCompFileFullPath), "OP" + ((GridRow)panel.GetRowFromIndex(i)).Cells["Oper1Ary"].Value.ToString(), "OIS");

                        //儲存OIS資料夾路徑
                        //cOperation.OISFolderPath = OISFolderPath;

                        //建立OIS資料夾
                        if (!File.Exists(OISFolderPath))
                        {
                            try
                            {
                                System.IO.Directory.CreateDirectory(OISFolderPath);
                            }
                            catch (System.Exception ex)
                            {
                                MessageBox.Show(ex.ToString());
                                return;
                            }
                        }

                        //建立三階檔案路徑
                        //ThridOperPartPath = Path.GetDirectoryName(AsmCompFileFullPath);

                        cPECreateData.listOperation.Add(cOperation);

                        cPECreateData.oper1Ary.Add(((GridRow)panel.GetRowFromIndex(i)).Cells["Oper1Ary"].Value.ToString());
                        cPECreateData.oper2Ary.Add(((GridRow)panel.GetRowFromIndex(i)).Cells["Oper2Ary"].Value.ToString()); 
                    }

                    #endregion

                    #region (註解中)複製MODEL內的客戶檔案到料號資料夾內，並更名XXX_MOT.prt
                    /*
                    //判斷要複製的檔案是否存在
                    status = System.IO.File.Exists(destFileName_Model);
                    if (!status)
                    {
                        MessageBox.Show("指定的檔案不存在，請再次確認");
                        return;
                    }

                    //建立目的地(客戶版次)檔案全路徑
                    string destFileName_CusRev = string.Format(@"{0}\{1}\{2}\{3}", CaxEnv.GetGlobalTekEnvDir(), PartNo, CusRev.ToUpper(), PartNo + "_MOT.prt");

                    //開始複製
                    File.Copy(destFileName_Model, destFileName_CusRev, true);
                    */
                    #endregion

                    #region 新料號---自動建立總組立檔案架構，並組立相關製程
                    status = CaxAsm.CreateNewAsm(AsmCompFileFullPath);
                    if (!status)
                    {
                        CaxLog.ShowListingWindow("建立一階總組立檔失敗");
                        return;
                    }

                    CaxPart.Save();

                    //NewConstruct---start
                    #region 建立二階檔案
                    string secondComp = "";
                    for (int i = 0; i < cPECreateData.listOperation.Count; i++)
                    {
                        //設定一階為WorkComp
                        CaxAsm.SetWorkComponent(null);

                        //建立二階製程檔
                        secondComp = string.Format(@"{0}\{1}", Path.GetDirectoryName(AsmCompFileFullPath), PartNo + "_OP" + cPECreateData.listOperation[i].Oper1 + ".prt");
                        status = CaxAsm.CreateNewEmptyCompNoReturn(secondComp);
                        if (!status)
                        {
                            CaxLog.ShowListingWindow("建立二階製程檔失敗");
                            return;
                        }
                    }
                    #endregion

                    #region 建立三階檔案
                    //先取得所有二階檔案
                    List<NXOpen.Assemblies.Component> secondChildenComp = new List<NXOpen.Assemblies.Component>();
                    CaxAsm.GetCompChildren(out secondChildenComp);

                    string thirdComp_OIS = "", thirdComp_CAM = "";
                    for (int i = 0; i < secondChildenComp.Count; i++)
                    {
                        CaxAsm.SetWorkComponent(secondChildenComp[i]);

                        

                        string OperStr = secondChildenComp[i].DisplayName.Split(new string[] { "OP" }, StringSplitOptions.RemoveEmptyEntries)[1];

                        #region 組立三階OIS檔
                        thirdComp_OIS = string.Format(@"{0}\{1}", Path.GetDirectoryName(AsmCompFileFullPath), PartNo + "_OIS" + OperStr + ".prt");
                        status = CaxAsm.CreateNewEmptyCompNoReturn(thirdComp_OIS);
                        if (!status)
                        {
                            CaxLog.ShowListingWindow("組立三階OIS檔失敗");
                            return;
                        }
                        #endregion

                        //001、900不需要組立三階CAM檔
                        if (secondChildenComp[i].DisplayName.Contains("OP001") || secondChildenComp[i].DisplayName.Contains("OP900"))
                            continue;

                        #region 組立三階CAM檔
                        thirdComp_CAM = string.Format(@"{0}\{1}", Path.GetDirectoryName(AsmCompFileFullPath), PartNo + "_OP" + OperStr + "_CAM.prt");
                        status = CaxAsm.CreateNewEmptyCompNoReturn(thirdComp_CAM);
                        if (!status)
                        {
                            CaxLog.ShowListingWindow("組立三階CAM檔失敗");
                            return;
                        }
                        #endregion

                    }
                    #endregion

                    #region 建立四階檔案
                    //成品檔案目的地
                    string desPartFilePath = string.Format(@"{0}\{1}", Path.GetDirectoryName(AsmCompFileFullPath), Path.GetFileName(souPartFilePath));
                    //胚料檔案目的地
                    string desBilletFilePath = "";
                    
                    
                    //由二階檔案取得三階檔案
                    foreach (NXOpen.Assemblies.Component i in secondChildenComp)
                    {
                        //由二階檔案取得三階檔案
                        List<NXOpen.Assemblies.Component> thirdChildenComp = new List<NXOpen.Assemblies.Component>();
                        CaxAsm.GetCompChildren(out thirdChildenComp, i);

                        //建立三階與四階的檔案txt，方便ME、TE下載時可讀取要下載哪些檔案
                        List<string> listMEFileName = new List<string>();
                        List<string> listTEFileName = new List<string>();

                        foreach (NXOpen.Assemblies.Component j in thirdChildenComp)
                        {
                            //當前ME、TE檔案
                            string CurrentMEPartFilePath = "", CurrentTEPartFilePath = "";

                            CaxAsm.SetWorkComponent(j);
                            
                            //目前的OIS
                            string CurrentOIS = "";

                            if (j.DisplayName.Contains("OIS"))
                            {
                                //加入三階OIS檔案名稱
                                listMEFileName.Add(Path.GetFileName(((Part)j.Prototype).FullPath));

                                #region 組立四階檔案-OIS
                                if (j.DisplayName.Contains("OIS001"))
                                {
                                    CurrentOIS = j.DisplayName.Split(new string[] { "OIS" }, StringSplitOptions.RemoveEmptyEntries)[1];

                                    #region 組立成品檔案
                                    status = AddPartFile(souPartFilePath, desPartFilePath);
                                    if (!status)
                                        return;
                                    else
                                    {
                                        listMEFileName.Add(Path.GetFileName(desPartFilePath));
                                        //當成品為組立件時，必須加入所有單件的名稱到TXT中，以利ME、TE下載時可以抓
                                        foreach (string assComp in AssembliesComp)
                                        {
                                            string sourassCompPath = string.Format(@"{0}\{1}", Path.GetDirectoryName(souPartFilePath), assComp);
                                            string desassCompPath = string.Format(@"{0}\{1}", Path.GetDirectoryName(AsmCompFileFullPath), assComp);
                                            //複製成品檔案到指定路徑
                                            if (!System.IO.File.Exists(desassCompPath))
                                            {
                                                File.Copy(sourassCompPath, desassCompPath, true);
                                            }
                                            listMEFileName.Add(assComp);
                                        }
                                    }
                                    #endregion

                                    #region 組立胚料檔案(有檔案組檔案；沒檔案則組一個空檔)
                                    status = AddBilletFile(souBilletFilePath, ref desBilletFilePath);
                                    if (!status)
                                        return;
                                    else
                                        listMEFileName.Add(Path.GetFileName(desBilletFilePath));
                                    #endregion

                                    #region 組立檢具
                                    status = AddInspectionFile(j.DisplayName, ref CurrentMEPartFilePath);
                                    if (!status)
                                        return;
                                    else
                                        listMEFileName.Add(Path.GetFileName(CurrentMEPartFilePath));
                                    #endregion
                                }
                                else if (j.DisplayName.Contains("OIS210"))
                                {
                                    CurrentOIS = j.DisplayName.Split(new string[] { "OIS" }, StringSplitOptions.RemoveEmptyEntries)[1];

                                    #region 組立成品檔案
                                    status = AddPartFile(souPartFilePath, desPartFilePath);
                                    if (!status)
                                        return;
                                    else
                                    {
                                        listMEFileName.Add(Path.GetFileName(desPartFilePath));
                                        //當成品為組立件時，必須加入所有單件的名稱到txt&檔案複製到Server，以利ME、TE下載時可以抓
                                        foreach (string assComp in AssembliesComp)
                                        {
                                            string sourassCompPath = string.Format(@"{0}\{1}", Path.GetDirectoryName(souPartFilePath), assComp);
                                            string desassCompPath = string.Format(@"{0}\{1}", Path.GetDirectoryName(AsmCompFileFullPath), assComp);
                                            //複製成品檔案到指定路徑
                                            if (!System.IO.File.Exists(desassCompPath))
                                            {
                                                File.Copy(sourassCompPath, desassCompPath, true);
                                            }
                                            listMEFileName.Add(assComp);
                                        }
                                    }
                                    #endregion

                                    #region 組立胚料檔案(有檔案組檔案；沒檔案則組一個空檔)
                                    status = AddBilletFile(souBilletFilePath, ref desBilletFilePath);
                                    if (!status)
                                        return;
                                    else
                                        listMEFileName.Add(Path.GetFileName(desBilletFilePath));
                                    #endregion

                                    #region 組立當前ME檔案
                                    string OIS = j.DisplayName.Split(new string[] { "OIS" }, StringSplitOptions.RemoveEmptyEntries)[1];
                                    status = AddCurrentMEFile(OIS, ref CurrentMEPartFilePath);
                                    if (!status)
                                        return;
                                    else
                                        listMEFileName.Add(Path.GetFileName(CurrentMEPartFilePath));
                                    #endregion

                                    #region 組立檢具
                                    status = AddInspectionFile(j.DisplayName, ref CurrentMEPartFilePath);
                                    if (!status)
                                        return;
                                    else
                                        listMEFileName.Add(Path.GetFileName(CurrentMEPartFilePath));
                                    #endregion
                                }
                                else if (j.DisplayName.Contains("OIS900"))
                                {
                                    CurrentOIS = j.DisplayName.Split(new string[] { "OIS" }, StringSplitOptions.RemoveEmptyEntries)[1];

                                    #region 組立成品檔案
                                    status = AddPartFile(souPartFilePath, desPartFilePath);
                                    if (!status)
                                        return;
                                    else
                                    {
                                        listMEFileName.Add(Path.GetFileName(desPartFilePath));
                                        //當成品為組立件時，必須加入所有單件的名稱到TXT中，以利ME、TE下載時可以抓
                                        foreach (string assComp in AssembliesComp)
                                        {
                                            string sourassCompPath = string.Format(@"{0}\{1}", Path.GetDirectoryName(souPartFilePath), assComp);
                                            string desassCompPath = string.Format(@"{0}\{1}", Path.GetDirectoryName(AsmCompFileFullPath), assComp);
                                            //複製成品檔案到指定路徑
                                            if (!System.IO.File.Exists(desassCompPath))
                                            {
                                                File.Copy(sourassCompPath, desassCompPath, true);
                                            }
                                            listMEFileName.Add(assComp);
                                        }
                                    }
                                    #endregion
                                }
                                else
                                {
                                    CurrentOIS = j.DisplayName.Split(new string[] { "OIS" }, StringSplitOptions.RemoveEmptyEntries)[1];

                                    #region 組立成品檔案
                                    status = AddPartFile(souPartFilePath, desPartFilePath);
                                    if (!status)
                                        return;
                                    else
                                    {
                                        listMEFileName.Add(Path.GetFileName(desPartFilePath));
                                        //當成品為組立件時，必須加入所有單件的名稱到TXT中，以利ME、TE下載時可以抓
                                        foreach (string assComp in AssembliesComp)
                                        {
                                            string sourassCompPath = string.Format(@"{0}\{1}", Path.GetDirectoryName(souPartFilePath), assComp);
                                            string desassCompPath = string.Format(@"{0}\{1}", Path.GetDirectoryName(AsmCompFileFullPath), assComp);
                                            //複製成品檔案到指定路徑
                                            if (!System.IO.File.Exists(desassCompPath))
                                            {
                                                File.Copy(sourassCompPath, desassCompPath, true);
                                            }
                                            listMEFileName.Add(assComp);
                                        }
                                    }
                                    #endregion

                                    #region 組立胚料檔案(有檔案組檔案；沒檔案則組一個空檔)
                                    status = AddBilletFile(souBilletFilePath, ref desBilletFilePath);
                                    if (!status)
                                        return;
                                    else
                                        listMEFileName.Add(Path.GetFileName(desBilletFilePath));
                                    #endregion

                                    #region 組立前一個ME檔案
                                    status = AddFrontMEFile(CurrentOIS, cPECreateData, ref CurrentMEPartFilePath);
                                    if (!status)
                                        return;
                                    else
                                    {
                                        if (CurrentMEPartFilePath != "")
                                        {
                                            listMEFileName.Add(Path.GetFileName(CurrentMEPartFilePath));
                                        }
                                    }
                                    #endregion

                                    #region 組立當前ME檔案
                                    status = AddCurrentMEFile(CurrentOIS, ref CurrentMEPartFilePath);
                                    if (!status)
                                        return;
                                    else
                                        listMEFileName.Add(Path.GetFileName(CurrentMEPartFilePath));
                                    #endregion

                                    #region 組立檢具
                                    status = AddInspectionFile(j.DisplayName, ref CurrentMEPartFilePath);
                                    if (!status)
                                        return;
                                    else
                                        listMEFileName.Add(Path.GetFileName(CurrentMEPartFilePath));
                                    #endregion
                                }
                                #endregion

                                //將三階與四階的檔案txt輸出至每個OP的資料夾內，OIS放在OIS資料夾內
                                OISFolderPath = string.Format(@"{0}\{1}", Path.GetDirectoryName(AsmCompFileFullPath), "OP" + CurrentOIS);
                                string[] OISFileNameTxt = listMEFileName.ToArray();
                                System.IO.File.WriteAllLines(string.Format(@"{0}\{1}", OISFolderPath, "PartNameText_OIS.txt"), OISFileNameTxt);
                            }
                            else
                            {
                                //加入三階CAM檔案名稱
                                listTEFileName.Add(Path.GetFileName(((Part)j.Prototype).FullPath));

                                #region 組立四階檔案-CAM
                                if (j.DisplayName.Contains("OP001") || j.DisplayName.Contains("OP900"))
                                {
                                    continue;
                                }
                                else if (j.DisplayName.Contains("OP210"))
                                {
                                    CurrentOIS = (j.DisplayName.Split(new string[] { "OP" }, StringSplitOptions.RemoveEmptyEntries)[1])
                                                        .Split(new string[] { "_CAM" }, StringSplitOptions.RemoveEmptyEntries)[0];

                                    #region 組立成品檔案
                                    status = AddPartFile(souPartFilePath, desPartFilePath);
                                    if (!status)
                                        return;
                                    else
                                    {
                                        listTEFileName.Add(Path.GetFileName(desPartFilePath));
                                        //當成品為組立件時，必須加入所有單件的名稱到TXT中，以利ME、TE下載時可以抓
                                        foreach (string assComp in AssembliesComp)
                                        {
                                            string sourassCompPath = string.Format(@"{0}\{1}", Path.GetDirectoryName(souPartFilePath), assComp);
                                            string desassCompPath = string.Format(@"{0}\{1}", Path.GetDirectoryName(AsmCompFileFullPath), assComp);
                                            //複製成品檔案到指定路徑
                                            if (!System.IO.File.Exists(desassCompPath))
                                            {
                                                File.Copy(sourassCompPath, desassCompPath, true);
                                            }
                                            listTEFileName.Add(assComp);
                                        }
                                    }
                                    #endregion

                                    #region 組立胚料檔案(有檔案組檔案；沒檔案則組一個空檔)
                                    status = AddBilletFile(souBilletFilePath, ref desBilletFilePath);
                                    if (!status)
                                        return;
                                    else
                                        listTEFileName.Add(Path.GetFileName(desBilletFilePath));
                                    #endregion

                                    #region 組立當前ME檔案
                                    status = AddCurrentMEFile(CurrentOIS, ref CurrentMEPartFilePath);
                                    if (!status)
                                        return;
                                    else
                                        listTEFileName.Add(Path.GetFileName(CurrentMEPartFilePath));
                                    #endregion

                                    #region 組立當前TE檔案
                                    status = AddCurrentTEFile(CurrentOIS, ref CurrentTEPartFilePath);
                                    if (!status)
                                        return;
                                    else
                                        listTEFileName.Add(Path.GetFileName(CurrentTEPartFilePath));
                                    #endregion

                                    #region 組立治具
                                    status = AddFixtureFile(j.DisplayName, ref CurrentMEPartFilePath);
                                    if (!status)
                                        return;
                                    else
                                        listTEFileName.Add(Path.GetFileName(CurrentMEPartFilePath));
                                    #endregion
                                }
                                else
                                {
                                    CurrentOIS = (j.DisplayName.Split(new string[] { "OP" }, StringSplitOptions.RemoveEmptyEntries)[1])
                                                        .Split(new string[] { "_CAM" }, StringSplitOptions.RemoveEmptyEntries)[0];

                                    #region 組立成品檔案
                                    status = AddPartFile(souPartFilePath, desPartFilePath);
                                    if (!status)
                                        return;
                                    else
                                    {
                                        listTEFileName.Add(Path.GetFileName(desPartFilePath));
                                        //當成品為組立件時，必須加入所有單件的名稱到TXT中，以利ME、TE下載時可以抓
                                        foreach (string assComp in AssembliesComp)
                                        {
                                            string sourassCompPath = string.Format(@"{0}\{1}", Path.GetDirectoryName(souPartFilePath), assComp);
                                            string desassCompPath = string.Format(@"{0}\{1}", Path.GetDirectoryName(AsmCompFileFullPath), assComp);
                                            //複製成品檔案到指定路徑
                                            if (!System.IO.File.Exists(desassCompPath))
                                            {
                                                File.Copy(sourassCompPath, desassCompPath, true);
                                            }
                                            listTEFileName.Add(assComp);
                                        }
                                    }
                                    #endregion

                                    #region 組立胚料檔案(有檔案組檔案；沒檔案則組一個空檔)
                                    status = AddBilletFile(souBilletFilePath, ref desBilletFilePath);
                                    if (!status)
                                        return;
                                    else
                                        listTEFileName.Add(Path.GetFileName(desBilletFilePath));
                                    #endregion

                                    #region 組立前一個ME檔案
                                    status = AddFrontMEFile(CurrentOIS, cPECreateData, ref CurrentMEPartFilePath);
                                    if (!status)
                                        return;
                                    else
                                    {
                                        if (CurrentMEPartFilePath != "")
                                        {
                                            listTEFileName.Add(Path.GetFileName(CurrentMEPartFilePath));
                                        }
                                    }  
                                    #endregion

                                    #region 組立當前ME檔案
                                    status = AddCurrentMEFile(CurrentOIS, ref CurrentMEPartFilePath);
                                    if (!status)
                                        return;
                                    else
                                        listTEFileName.Add(Path.GetFileName(CurrentMEPartFilePath));
                                    #endregion

                                    #region 組立當前TE檔案
                                    status = AddCurrentTEFile(CurrentOIS, ref CurrentTEPartFilePath);
                                    if (!status)
                                        return;
                                    else
                                        listTEFileName.Add(Path.GetFileName(CurrentTEPartFilePath));
                                    #endregion

                                    #region 組立治具
                                    status = AddFixtureFile(j.DisplayName, ref CurrentMEPartFilePath);
                                    if (!status)
                                        return;
                                    else
                                        listTEFileName.Add(Path.GetFileName(CurrentMEPartFilePath));
                                    #endregion
                                }
                                #endregion

                                //將三階與四階的檔案txt輸出至每個OP的資料夾內，CAM放在CAM資料夾內
                                CAMFolderPath = string.Format(@"{0}\{1}", Path.GetDirectoryName(AsmCompFileFullPath), "OP" + CurrentOIS);
                                string[] CAMFileNameTxt = listTEFileName.ToArray();
                                System.IO.File.WriteAllLines(string.Format(@"{0}\{1}", CAMFolderPath, "PartNameText_CAM.txt"), CAMFileNameTxt);
                            }

                            #region 將三階與四階的檔案txt輸出至每個OP的資料夾內，OIS放在OIS資料夾內；CAM放在CAM資料夾內
                            //OISFolderPath = string.Format(@"{0}\{1}", Path.GetDirectoryName(AsmCompFileFullPath), "OP" + CurrentOIS);
                            //string[] OISFileNameTxt = listMEFileName.ToArray();
                            //System.IO.File.WriteAllLines(string.Format(@"{0}\{1}", OISFolderPath, "PartNameText_OIS.txt"), OISFileNameTxt);

                            //CAMFolderPath = string.Format(@"{0}\{1}", Path.GetDirectoryName(AsmCompFileFullPath), "OP" + CurrentOIS);
                            //string[] CAMFileNameTxt = listTEFileName.ToArray();
                            //System.IO.File.WriteAllLines(string.Format(@"{0}\{1}", CAMFolderPath, "PartNameText_CAM.txt"), CAMFileNameTxt);
                            #endregion
                        }
                    }
                    
                    #endregion
                    //NewConstruct---end

                    
                    #endregion

                    #region (註解中)新料號---塞ERP屬性、品名、材質
                    /*
                    Part rootPart = theSession.Parts.Work;
                    NXOpen.Assemblies.ComponentAssembly casm = rootPart.ComponentAssembly;
                    List<NXOpen.Assemblies.Component> allComp = new List<NXOpen.Assemblies.Component>();
                    CaxAsm.GetCompChildren(casm.RootComponent, ref allComp);
                    PartLoadStatus partLoadStatus1;
                    foreach (NXOpen.Assemblies.Component i in allComp)
                    {
                        for (int j = 0; j < panel.Rows.Count; j++)
                        {
                            if (i.DisplayName.Contains("OIS") & i.DisplayName.Contains(panel.GetCell(j, 0).Value.ToString()))
                            {
                                Part MakeDisplayPart = (Part)i.Prototype;
                                theSession.Parts.SetDisplay(MakeDisplayPart, true, true, out partLoadStatus1);
                                MakeDisplayPart.SetAttribute("ERPCODE", panel.GetCell(j, 2).Value.ToString());
                                MakeDisplayPart.SetAttribute("PartDescriptionPos", PartDescription.Text);
                                MakeDisplayPart.SetAttribute("MaterialPos", MaterialCT.Text);
                            }
                        }
                    }

                    //返回MOT架構
                    theSession.Parts.SetDisplay(rootPart, true, true, out partLoadStatus1);
                    partLoadStatus1.Dispose();
                    */
                    #endregion

                    #region 新料號---寫出PECreateData.dat
                    string PECreateDataJsonDat = string.Format(@"{0}\{1}", ModelFolderFullPath, "PECreateData.dat");
                    status = CaxFile.WriteJsonFileData(PECreateDataJsonDat, cPECreateData);
                    if (!status)
                    {
                        MessageBox.Show("PECreateData.dat 輸出失敗...");
                        return;
                    }
                    #endregion

                    #region 新料號---刪除暫存檔案
                    if (Directory.Exists(string.Format(@"{0}\{1}", CaxEnv.GetGlobaltekEnvDir(), "TempFileData")))
                    {
                        di = new DirectoryInfo(string.Format(@"{0}\{1}", CaxEnv.GetGlobaltekEnvDir(), "TempFileData"));
                        foreach (FileInfo i in di.GetFiles())
                        {
                            if (i.Name.Contains(string.Format(@"{0}_{1}_{2}_{3}", CusName, PartNo, CusRev, OpRev)))
                            {
                                File.Delete(i.FullName);
                            }
                        }
                    }
                    #endregion

                    #region (註解中)寫出METEDownloadData.dat

                    //string METEDownloadData = string.Format(@"{0}\{1}", CaxEnv.GetGlobaltekTaskDir(), "METEDownloadData.dat");
                    //METEDownloadData cMETEDownloadData = new METEDownloadData();

                    //if (File.Exists(METEDownloadData))
                    //{
                    //    #region METEDownloadData.dat檔案存在

                    //    status = CaxPublic.ReadMETEDownloadData(METEDownloadData, out cMETEDownloadData);
                    //    if (!status)
                    //    {
                    //        MessageBox.Show("METEDownloadData.dat讀取失敗...");
                    //        return;
                    //    }

                    //    int CusCount = 0, IndexOfCusName = -1;
                    //    for (int i = 0; i < cMETEDownloadData.EntirePartAry.Count; i++)
                    //    {
                    //        if (CusName != cMETEDownloadData.EntirePartAry[i].CusName)
                    //        {
                    //            CusCount++;
                    //        }
                    //        else
                    //        {
                    //            IndexOfCusName = i;
                    //            break;
                    //        }
                    //    }

                    //    //新的客戶且已經有METEDownloadDat.dat
                    //    if (CusCount == cMETEDownloadData.EntirePartAry.Count)
                    //    {
                    //        EntirePartAry cEntirePartAry = new EntirePartAry();
                    //        cEntirePartAry.CusName = CusName;
                    //        cEntirePartAry.CusPart = new List<CusPart>();

                    //        CusPart cCusPart = new CusPart();
                    //        cCusPart.PartNo = PartNo;
                    //        cCusPart.CusRev = new List<CusRev>();

                    //        CusRev cCusRev = new CusRev();
                    //        cCusRev.RevNo = CusRev.ToUpper();
                    //        cCusRev.OperAry1 = new List<string>();
                    //        cCusRev.OperAry2 = new List<string>();
                    //        cCusRev.OperAry1 = cPECreateData.Oper1Ary;
                    //        cCusRev.OperAry2 = cPECreateData.Oper2Ary;

                    //        cCusPart.CusRev.Add(cCusRev);
                    //        cEntirePartAry.CusPart.Add(cCusPart);
                    //        cMETEDownloadData.EntirePartAry.Add(cEntirePartAry);
                    //    }
                    //    //舊的客戶新增料號
                    //    else
                    //    {
                    //        //判斷料號是否已存在
                    //        int PartCount = 0; int IndexOfPartNo = -1;
                    //        for (int i = 0; i < cMETEDownloadData.EntirePartAry[IndexOfCusName].CusPart.Count; i++)
                    //        {
                    //            if (PartNo != cMETEDownloadData.EntirePartAry[IndexOfCusName].CusPart[i].PartNo)
                    //            {
                    //                PartCount++;
                    //            }
                    //            else
                    //            {
                    //                IndexOfPartNo = i;
                    //                break;
                    //            }
                    //        }

                    //        //舊的客戶且新的料號 PartCount == CusPart.Count 表示新的料號
                    //        if (PartCount == cMETEDownloadData.EntirePartAry[IndexOfCusName].CusPart.Count)
                    //        {
                    //            CusPart cCusPart = new CusPart();
                    //            cCusPart.PartNo = PartNo;
                    //            cCusPart.CusRev = new List<CusRev>();

                    //            CusRev cCusRev = new CusRev();
                    //            cCusRev.RevNo = CusRev.ToUpper();
                    //            cCusRev.OperAry1 = new List<string>();
                    //            cCusRev.OperAry2 = new List<string>();
                    //            cCusRev.OperAry1 = cPECreateData.Oper1Ary;
                    //            cCusRev.OperAry2 = cPECreateData.Oper2Ary;

                    //            cCusPart.CusRev.Add(cCusRev);
                    //            cMETEDownloadData.EntirePartAry[IndexOfCusName].CusPart.Add(cCusPart);
                    //        }
                    //        //舊的客戶且舊的料號新增客戶版次
                    //        else
                    //        {
                    //            CusRev cCusRev = new CusRev();
                    //            cCusRev.RevNo = CusRev.ToUpper();
                    //            cCusRev.OperAry1 = new List<string>();
                    //            cCusRev.OperAry1 = cPECreateData.Oper1Ary;

                    //            cMETEDownloadData.EntirePartAry[IndexOfCusName].CusPart[IndexOfPartNo].CusRev.Add(cCusRev);
                    //        }
                    //    }
                    //    /*
                    //    int PartCount = 0; int IndexOfPartNo = -1;
                    //    for (int i = 0; i < cMETEDownloadData.EntirePartAry.Count; i++)
                    //    {
                    //    if (PartNo != cMETEDownloadData.EntirePartAry[i].PartNo)
                    //    {
                    //    PartCount++;
                    //    }
                    //    else
                    //    {
                    //    IndexOfPartNo = i;
                    //    break;
                    //    }
                    //    }

                    //    //新的料號且已經有METEDownloadDat.dat
                    //    if (PartCount == cMETEDownloadData.EntirePartAry.Count)
                    //    {
                    //    EntirePartAry cEntirePartAry = new EntirePartAry();
                    //    cEntirePartAry.CusRev = new List<CusRev>();

                    //    CusRev cCusRev = new CusRev();
                    //    cCusRev.OperAry1 = new List<string>();
                    //    cCusRev.RevNo = CusRev.ToUpper();
                    //    cCusRev.OperAry1 = cPE_OutPutDat.Oper1Ary;

                    //    cEntirePartAry.CusName = CusName;
                    //    cEntirePartAry.PartNo = PartNo;
                    //    cEntirePartAry.CusRev.Add(cCusRev);

                    //    cMETEDownloadData.EntirePartAry.Add(cEntirePartAry);
                    //    }
                    //    //舊的料號新增客戶版次
                    //    else
                    //    {
                    //    CusRev cCusRev = new CusRev();
                    //    cCusRev.OperAry1 = new List<string>();
                    //    cCusRev.RevNo = CusRev.ToUpper();
                    //    cCusRev.OperAry1 = cPE_OutPutDat.Oper1Ary;

                    //    cMETEDownloadData.EntirePartAry[IndexOfPartNo].CusRev.Add(cCusRev);
                    //    }
                    //    */
                    //    #endregion
                    //}
                    //else
                    //{
                    //    #region METEDownloadData.dat檔案不存在

                    //    cMETEDownloadData.EntirePartAry = new List<EntirePartAry>();
                    //    EntirePartAry cEntirePartAry = new EntirePartAry();
                    //    cEntirePartAry.CusName = CusName;
                    //    cEntirePartAry.CusPart = new List<CusPart>();

                    //    CusPart cCusPart = new CusPart();
                    //    cCusPart.PartNo = PartNo;
                    //    cCusPart.CusRev = new List<CusRev>();

                    //    CusRev cCusRev = new CusRev();
                    //    cCusRev.RevNo = CusRev.ToUpper();
                    //    cCusRev.OperAry1 = new List<string>();
                    //    cCusRev.OperAry2 = new List<string>();
                    //    cCusRev.OperAry1 = cPECreateData.Oper1Ary;
                    //    cCusRev.OperAry2 = cPECreateData.Oper2Ary;

                    //    cCusPart.CusRev.Add(cCusRev);
                    //    cEntirePartAry.CusPart.Add(cCusPart);
                    //    cMETEDownloadData.EntirePartAry.Add(cEntirePartAry);

                    //    #endregion
                    //}

                    //status = CaxFile.WriteJsonFileData(METEDownloadData, cMETEDownloadData);
                    //if (!status)
                    //{
                    //    MessageBox.Show("METEDownloadData.dat輸出失敗...");
                    //    return;
                    //}

                    #endregion

                    #region SaveToDatabase
                    
                    using (ISession session = MyHibernateHelper.SessionFactory.OpenSession())
                    {
                        #region 插入Com_PEMain
                        cCom_PEMain.partName = PartNo;
                        cCom_PEMain.partDes = PartDes;
                        cCom_PEMain.partMaterial = PartMaterial;
                        cCom_PEMain.customerVer = CusRev;
                        cCom_PEMain.opVer = OpRev;
                        //插入材料來源(自備胚=1，管材/鍛件=A)
                        if (CompBillet.Checked)
                        {
                            cCom_PEMain.materialSource = "1";
                        }
                        if (CusBillet.Checked)
                        {
                            cCom_PEMain.materialSource = "A";
                        }
                        //插入ERPStd
                        if (ERPcode.Text != "")
                        {
                            cCom_PEMain.eRPStd = ERPcode.Text;
                        }
                        //插入成品檔路徑
                        cCom_PEMain.partFilePath = desPartFilePath;
                        //插入胚料檔路徑
                        cCom_PEMain.billetFilePath = desBilletFilePath;

                        cCom_PEMain.createDate = DateTime.Now.ToString();

                        IList<Com_PartOperation> listComPartOperation = new List<Com_PartOperation>();
                        for (int i = 0; i < panel.Rows.Count;i++ )
                        {
                            cCom_PartOperation = new Com_PartOperation();
                            cCom_PartOperation.operation1 = ((GridRow)panel.GetRowFromIndex(i)).Cells["Oper1Ary"].Value.ToString();
                            cCom_PartOperation.sysOperation2 = session.QueryOver<Sys_Operation2>()
                                                                .Where(x => x.operation2Name == ((GridRow)panel.GetRowFromIndex(i)).Cells["Oper2Ary"].Value.ToString())
                                                                .SingleOrDefault();
                            if ((bool)((GridRow)panel.GetRowFromIndex(i)).Cells["IQC"].Value)
                            {
                                cCom_PartOperation.form = "IQC";
                            }
                            else if ((bool)((GridRow)panel.GetRowFromIndex(i)).Cells["IPQC"].Value)
                            {
                                cCom_PartOperation.form = "IPQC";
                            }
                            else
                            {
                                cCom_PartOperation.form = null;
                            }
                            if (((GridRow)panel.GetRowFromIndex(i)).Cells["ERP料號"].Value.ToString() != "")
                                cCom_PartOperation.erpCode = ((GridRow)panel.GetRowFromIndex(i)).Cells["ERP料號"].Value.ToString();
                            
                            cCom_PartOperation.comPEMain = cCom_PEMain;
                            listComPartOperation.Add(cCom_PartOperation);
                        }
                        
                        //foreach (Operation i in cPECreateData.listOperation)
                        //{
                        //    cCom_PartOperation = new Com_PartOperation();
                        //    cCom_PartOperation.operation1 = i.Oper1;
                        //    cCom_PartOperation.sysOperation2 = session.QueryOver<Sys_Operation2>()
                        //                                        .Where(x => x.operation2Name == i.Oper2).SingleOrDefault();

                        //    cCom_PartOperation.comPEMain = cCom_PEMain;
                        //    listComPartOperation.Add(cCom_PartOperation);
                        //}
                        
                        cCom_PEMain.comPartOperation = listComPartOperation;

                        using (ITransaction trans = session.BeginTransaction())
                        {
                            //session.Save(cCom_PartOperation);
                            session.Save(cCom_PEMain);

                            trans.Commit();
                        }
                        
                        session.Close();
                        #endregion
                    }
                    
                    #endregion

                }
                
                //Console.Read();//暫停畫面用
                //CaxLoading.CloseDlg();
                CaxAsm.SetWorkComponent(null);
                CaxPart.Save();
                this.DialogResult = DialogResult.OK;
                this.Close();
            }
            catch (System.Exception ex)
            {
                MessageBox.Show(ex.ToString());
                this.Close();
            }

        }

        private bool DataCompleted()
        {
            try
            {
                #region 新料號---取得客戶名稱
                CusName = comboBoxCusName.Text;
                if (CusName == "")
                {
                    MessageBox.Show("尚未填寫客戶！");
                    return false;
                }
                #endregion

                #region 新料號---取得料號
                PartNo = textPartNo.Text;
                if (PartNo == "")
                {
                    MessageBox.Show("尚未填寫料號！");
                    return false;
                }
                #endregion

                #region 新料號---取得客戶版次
                CusRev = textCusRev.Text;
                if (CusRev == "")
                {
                    MessageBox.Show("尚未填寫客戶版次！");
                    return false;
                }
                #endregion

                #region 新料號---取得製程版次
                OpRev = textOpRev.Text;
                if (OpRev == "")
                {
                    MessageBox.Show("尚未填寫製程版次！");
                    return false;
                }
                #endregion

                #region 新料號---判斷是否有選擇客戶檔案
                if (souPartFilePath == "-1")
                {
                    MessageBox.Show("尚未選擇客戶檔案！");
                    return false;
                }
                #endregion

                #region 新料號---取得品名
                PartDes = PartDescription.Text;
                if (PartDes == "")
                {
                    MessageBox.Show("尚未填寫品名！");
                    return false;
                }
                #endregion

                #region 新料號---取得材質
                PartMaterial = MaterialCT.Text;
                if (PartDes == "")
                {
                    MessageBox.Show("尚未填寫材質！");
                    return false;
                }
                #endregion
            }
            catch (System.Exception ex)
            {
                return false;
            }
            return true;
        }

        private bool AddPartFile(string souPartFilePath, string desPartFilePath)
        {
            try
            {
                //判斷成品檔案是否存在
                status = System.IO.File.Exists(souPartFilePath);
                if (!status)
                {
                    MessageBox.Show("指定的成品檔案不存在，請再次確認");
                    return false;
                }

                //複製成品檔案到指定路徑
                if (!System.IO.File.Exists(desPartFilePath))
                {
                    File.Copy(souPartFilePath, desPartFilePath, true);
                }

                NXOpen.Assemblies.Component newComponent = null;
                status = CaxAsm.AddComponentToAsmByDefault(desPartFilePath, out newComponent);
                if (!status)
                {
                    MessageBox.Show("成品檔案組裝失敗！");
                    return false;
                }
            }
            catch (System.Exception ex)
            {
                return false;
            }
            return true;
        }
        private bool AddEditPartFile(string souOldPartFilePath, string desPartFilePath)
        {
            try
            {
                //判斷成品檔案是否存在
                status = System.IO.File.Exists(souOldPartFilePath);
                if (!status)
                {
                    MessageBox.Show("指定的成品檔案不存在，請再次確認");
                    return false;
                }

                //複製成品檔案到指定路徑
                if (!System.IO.File.Exists(desPartFilePath))
                {
                    File.Copy(souOldPartFilePath, desPartFilePath, true);
                }

                NXOpen.Assemblies.Component newComponent = null;
                status = CaxAsm.AddComponentToAsmByDefault(desPartFilePath, out newComponent);
                if (!status)
                {
                    MessageBox.Show("成品檔案組裝失敗！");
                    return false;
                }
            }
            catch (System.Exception ex)
            {
                return false;
            }
            return true;
        }

        private bool AddBilletFile(string souBilletFilePath, ref string desBilletFilePath)
        {
            try
            {
                NXOpen.Assemblies.Component newComponent = null;
                if (souBilletFilePath != "")
                {
                    #region 有選擇胚料
                    //判斷胚料檔案是否存在
                    status = System.IO.File.Exists(souBilletFilePath);
                    if (!status)
                    {
                        MessageBox.Show("指定的胚料檔案不存在，請再次確認");
                        return false;
                    }

                    desBilletFilePath = string.Format(@"{0}\{1}\{2}\{3}\{4}\{5}", CaxEnv.GetGlobaltekTaskDir()
                        , CusName, PartNo, CusRev.ToUpper(), OpRev.ToUpper(), Path.GetFileName(souBilletFilePath));
                    
                    //複製胚料檔案到指定路徑
                    if (!System.IO.File.Exists(desBilletFilePath))
                    {
                        File.Copy(souBilletFilePath, desBilletFilePath, true);
                    }

                    status = CaxAsm.AddComponentToAsmByDefault(desBilletFilePath, out newComponent);
                    if (!status)
                    {
                        MessageBox.Show("胚料檔案組裝失敗！");
                        return false;
                    }
                    #endregion
                }
                else
                {
                    #region 沒選擇胚料
                    desBilletFilePath = string.Format(@"{0}\{1}\{2}\{3}\{4}\{5}", CaxEnv.GetGlobaltekTaskDir()
                        , CusName, PartNo, CusRev.ToUpper(), OpRev.ToUpper(), PartNo + "_Blank.prt");
                    if (System.IO.File.Exists(desBilletFilePath))
                    {
                        status = CaxAsm.AddComponentToAsmByDefault(desBilletFilePath, out newComponent);
                        if (!status)
                        {
                            MessageBox.Show("胚料檔案組裝失敗！");
                            return false;
                        }
                    }
                    else
                    {
                        status = CaxAsm.CreateNewEmptyCompNoReturn(desBilletFilePath);
                        if (!status)
                        {
                            MessageBox.Show("胚料檔案組裝失敗！");
                            return false;
                        }
                    }
                    #endregion
                }
            }
            catch (System.Exception ex)
            {
                return false;
            }
            return true;
        }
        private bool AddEditBilletFile(string souOldBilletFilePath, ref string desBilletFilePath)
        {
            try
            {
                NXOpen.Assemblies.Component newComponent = null;
                if (souOldBilletFilePath != "")
                {
                    #region 有選擇胚料
                    //判斷胚料檔案是否存在
                    status = System.IO.File.Exists(souOldBilletFilePath);
                    if (!status)
                    {
                        MessageBox.Show("指定的胚料檔案不存在，請再次確認");
                        return false;
                    }

                    desBilletFilePath = string.Format(@"{0}\{1}\{2}\{3}\{4}\{5}", CaxEnv.GetGlobaltekTaskDir()
                        , comboBoxOldCusName.Text, comboBoxOldPartNo.Text, comboBoxOldCusRev.Text.ToUpper(), comboBoxOldOpRev.Text.ToUpper(), Path.GetFileName(souOldBilletFilePath));

                    //複製胚料檔案到指定路徑
                    if (!System.IO.File.Exists(desBilletFilePath))
                    {
                        File.Copy(souOldBilletFilePath, desBilletFilePath, true);
                    }

                    status = CaxAsm.AddComponentToAsmByDefault(desBilletFilePath, out newComponent);
                    if (!status)
                    {
                        MessageBox.Show("胚料檔案組裝失敗！");
                        return false;
                    }
                    #endregion
                }
                else
                {
                    #region 沒選擇胚料
                    desBilletFilePath = string.Format(@"{0}\{1}\{2}\{3}\{4}\{5}", CaxEnv.GetGlobaltekTaskDir()
                        , comboBoxOldCusName.Text, comboBoxOldPartNo.Text, comboBoxOldCusRev.Text.ToUpper(), comboBoxOldOpRev.Text.ToUpper(), comboBoxOldPartNo.Text + "_Blank.prt");
                    if (System.IO.File.Exists(desBilletFilePath))
                    {
                        status = CaxAsm.AddComponentToAsmByDefault(desBilletFilePath, out newComponent);
                        if (!status)
                        {
                            MessageBox.Show("胚料檔案組裝失敗！");
                            return false;
                        }
                    }
                    else
                    {
                        status = CaxAsm.CreateNewEmptyCompNoReturn(desBilletFilePath);
                        if (!status)
                        {
                            MessageBox.Show("胚料檔案組裝失敗！");
                            return false;
                        }
                    }
                    #endregion
                }
            }
            catch (System.Exception ex)
            {
                return false;
            }
            return true;
        }

        private bool AddCurrentMEFile(string OIS, ref string CurrentMEPartFilePath)
        {
            try
            {
                CurrentMEPartFilePath = string.Format(@"{0}\{1}\{2}\{3}\{4}\{5}", CaxEnv.GetGlobaltekTaskDir()
                    , CusName, PartNo, CusRev.ToUpper(), OpRev.ToUpper(), PartNo + "_ME_" + OIS + ".prt");

                NXOpen.Assemblies.Component newComponent = null;
                if (System.IO.File.Exists(CurrentMEPartFilePath))
                {
                    status = CaxAsm.AddComponentToAsmByDefault(CurrentMEPartFilePath, out newComponent);
                    if (!status)
                    {
                        MessageBox.Show("當前ME檔案(" + OIS + ")組裝失敗！");
                        return false;
                    }
                }
                else
                {
                    status = CaxAsm.CreateNewEmptyCompNoReturn(CurrentMEPartFilePath);
                    if (!status)
                    {
                        MessageBox.Show("當前ME檔案(" + OIS + ")組裝失敗！");
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
        private bool AddEditCurrentMEFile(string OIS, ref string CurrentMEPartFilePath)
        {
            try
            {
                CurrentMEPartFilePath = string.Format(@"{0}\{1}\{2}\{3}\{4}\{5}", CaxEnv.GetGlobaltekTaskDir()
                    , comboBoxOldCusName.Text, comboBoxOldPartNo.Text, comboBoxOldCusRev.Text.ToUpper(), comboBoxOldOpRev.Text.ToUpper(), comboBoxOldPartNo.Text + "_ME_" + OIS + ".prt");

                NXOpen.Assemblies.Component newComponent = null;
                if (System.IO.File.Exists(CurrentMEPartFilePath))
                {
                    status = CaxAsm.AddComponentToAsmByDefault(CurrentMEPartFilePath, out newComponent);
                    if (!status)
                    {
                        MessageBox.Show("當前ME檔案(" + OIS + ")組裝失敗！");
                        return false;
                    }
                }
                else
                {
                    status = CaxAsm.CreateNewEmptyCompNoReturn(CurrentMEPartFilePath);
                    if (!status)
                    {
                        MessageBox.Show("當前ME檔案(" + OIS + ")組裝失敗！");
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

        private bool AddInspectionFile(string partName, ref string CurrentMEPartFilePath)
        {
            try
            {
                CurrentMEPartFilePath = string.Format(@"{0}\{1}\{2}\{3}\{4}\{5}", CaxEnv.GetGlobaltekTaskDir()
                    , CusName, PartNo, CusRev.ToUpper(), OpRev.ToUpper(), partName + "_Inspection" + ".prt");

                NXOpen.Assemblies.Component newComponent = null;
                if (System.IO.File.Exists(CurrentMEPartFilePath))
                {
                    status = CaxAsm.AddComponentToAsmByDefault(CurrentMEPartFilePath, out newComponent);
                    if (!status)
                    {
                        MessageBox.Show("當前檢具檔案組裝失敗！");
                        return false;
                    }
                }
                else
                {
                    status = CaxAsm.CreateNewEmptyCompNoReturn(CurrentMEPartFilePath);
                    if (!status)
                    {
                        MessageBox.Show("當前檢具檔案組裝失敗！");
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
        private bool AddEditInspectionFile(string partName, ref string CurrentMEPartFilePath)
        {
            try
            {
                CurrentMEPartFilePath = string.Format(@"{0}\{1}\{2}\{3}\{4}\{5}", CaxEnv.GetGlobaltekTaskDir()
                    , comboBoxOldCusName.Text, comboBoxOldPartNo.Text, comboBoxOldCusRev.Text.ToUpper(), comboBoxOldOpRev.Text.ToUpper(), partName + "_Inspection" + ".prt");

                NXOpen.Assemblies.Component newComponent = null;
                if (System.IO.File.Exists(CurrentMEPartFilePath))
                {
                    status = CaxAsm.AddComponentToAsmByDefault(CurrentMEPartFilePath, out newComponent);
                    if (!status)
                    {
                        MessageBox.Show("當前檢具檔案組裝失敗！");
                        return false;
                    }
                }
                else
                {
                    status = CaxAsm.CreateNewEmptyCompNoReturn(CurrentMEPartFilePath);
                    if (!status)
                    {
                        MessageBox.Show("當前檢具檔案組裝失敗！");
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

        private bool AddFixtureFile(string partName, ref string CurrentMEPartFilePath)
        {
            try
            {
                CurrentMEPartFilePath = string.Format(@"{0}\{1}\{2}\{3}\{4}\{5}", CaxEnv.GetGlobaltekTaskDir()
                    , CusName, PartNo, CusRev.ToUpper(), OpRev.ToUpper(), partName + "_Fixture" + ".prt");

                NXOpen.Assemblies.Component newComponent = null;
                if (System.IO.File.Exists(CurrentMEPartFilePath))
                {
                    status = CaxAsm.AddComponentToAsmByDefault(CurrentMEPartFilePath, out newComponent);
                    if (!status)
                    {
                        MessageBox.Show(Path.GetFileName(CurrentMEPartFilePath) + "組裝失敗！");
                        return false;
                    }
                }
                else
                {
                    status = CaxAsm.CreateNewEmptyCompNoReturn(CurrentMEPartFilePath);
                    if (!status)
                    {
                        MessageBox.Show(Path.GetFileName(CurrentMEPartFilePath) + "組裝失敗！");
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
        private bool AddEditFixtureFile(string partName, ref string CurrentMEPartFilePath)
        {
            try
            {
                CurrentMEPartFilePath = string.Format(@"{0}\{1}\{2}\{3}\{4}\{5}", CaxEnv.GetGlobaltekTaskDir()
                    , comboBoxOldCusName.Text, comboBoxOldPartNo.Text, comboBoxOldCusRev.Text.ToUpper(), comboBoxOldOpRev.Text.ToUpper(), partName + "_Fixture" + ".prt");

                NXOpen.Assemblies.Component newComponent = null;
                if (System.IO.File.Exists(CurrentMEPartFilePath))
                {
                    status = CaxAsm.AddComponentToAsmByDefault(CurrentMEPartFilePath, out newComponent);
                    if (!status)
                    {
                        MessageBox.Show("當前ME檔案組裝失敗！");
                        return false;
                    }
                }
                else
                {
                    status = CaxAsm.CreateNewEmptyCompNoReturn(CurrentMEPartFilePath);
                    if (!status)
                    {
                        MessageBox.Show("當前ME檔案組裝失敗！");
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

        private bool AddFrontMEFile(string CurrentOIS, PECreateData cPECreateData, ref string CurrentMEPartFilePath)
        {
            try
            {
                string FrontOIS = "";
                for (int i = 0; i < cPECreateData.oper1Ary.Count;i++ )
                {
                    if (cPECreateData.oper1Ary[i] == CurrentOIS & i != 0)
                        FrontOIS = cPECreateData.oper1Ary[i - 1];
                }

                if (FrontOIS != "")
                {
                    status = AddCurrentMEFile(FrontOIS, ref CurrentMEPartFilePath);
                    if (!status)
                    {
                        MessageBox.Show("組立前一工段" + FrontOIS + "失敗");
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
        private bool AddEditFrontMEFile(string CurrentOIS, PECreateData cPECreateData, ref string CurrentMEPartFilePath)
        {
            try
            {
                string FrontOIS = "";
                for (int i = 0; i < cPECreateData.oper1Ary.Count; i++)
                {
                    if (cPECreateData.oper1Ary[i] == CurrentOIS & i != 0)
                        FrontOIS = cPECreateData.oper1Ary[i - 1];
                }

                if (FrontOIS != "")
                {
                    status = AddEditCurrentMEFile(FrontOIS, ref CurrentMEPartFilePath);
                    if (!status)
                    {
                        MessageBox.Show("組立前一工段" + FrontOIS + "失敗");
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

        private bool AddCurrentTEFile(string OIS, ref string CurrentTEPartFilePath)
        {
            try
            {
                CurrentTEPartFilePath = string.Format(@"{0}\{1}\{2}\{3}\{4}\{5}", CaxEnv.GetGlobaltekTaskDir()
                    , CusName, PartNo, CusRev.ToUpper(), OpRev.ToUpper(), PartNo + "_TE_" + OIS + ".prt");
                
                NXOpen.Assemblies.Component newComponent = null;
                if (System.IO.File.Exists(CurrentTEPartFilePath))
                {
                    status = CaxAsm.AddComponentToAsmByDefault(CurrentTEPartFilePath, out newComponent);
                    if (!status)
                    {
                        MessageBox.Show("當前TE檔案組裝失敗！");
                        return false;
                    }
                }
                else
                {
                    status = CaxAsm.CreateNewEmptyCompNoReturn(CurrentTEPartFilePath);
                    if (!status)
                    {
                        MessageBox.Show("當前TE檔案組裝失敗！");
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
        private bool AddEditCurrentTEFile(string OIS, ref string CurrentTEPartFilePath)
        {
            try
            {
                CurrentTEPartFilePath = string.Format(@"{0}\{1}\{2}\{3}\{4}\{5}", CaxEnv.GetGlobaltekTaskDir()
                                                                                , comboBoxOldCusName.Text
                                                                                , comboBoxOldPartNo.Text
                                                                                , comboBoxOldCusRev.Text.ToUpper()
                                                                                , comboBoxOldOpRev.Text.ToUpper()
                                                                                , comboBoxOldPartNo.Text + "_TE_" + OIS + ".prt");

                NXOpen.Assemblies.Component newComponent = null;
                if (System.IO.File.Exists(CurrentTEPartFilePath))
                {
                    status = CaxAsm.AddComponentToAsmByDefault(CurrentTEPartFilePath, out newComponent);
                    if (!status)
                    {
                        MessageBox.Show("當前TE檔案組裝失敗！");
                        return false;
                    }
                }
                else
                {
                    status = CaxAsm.CreateNewEmptyCompNoReturn(CurrentTEPartFilePath);
                    if (!status)
                    {
                        MessageBox.Show("當前TE檔案組裝失敗！");
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

        private void PEGenerateDlg_FormClosing(object sender, FormClosingEventArgs e)
        {
            if (this.DialogResult != DialogResult.OK)
            {
                CaxPart.CloseAllParts();
            }
        }

        private void comboBoxOldCusName_SelectedIndexChanged(object sender, EventArgs e)
        {
            //清空superGrid資料
            panel.Rows.Clear();
            //取得當前選取的客戶
            CurrentOldCusName = comboBoxOldCusName.Text;
            //打開&清空下拉選單-料號
            comboBoxOldPartNo.Enabled = true;
            comboBoxOldPartNo.Items.Clear();
            comboBoxOldPartNo.Text = "";
            //關閉&清空下拉選單-客戶版次
            comboBoxOldCusRev.Enabled = false;
            comboBoxOldCusRev.Items.Clear();
            comboBoxOldCusRev.Text = "";
            //關閉&清空下拉選單-製程版次
            comboBoxOldOpRev.Enabled = false;
            comboBoxOldOpRev.Items.Clear();
            comboBoxOldOpRev.Text = "";

            string S_Task_CusName_Path = string.Format(@"{0}\{1}", CaxEnv.GetGlobaltekTaskDir(), CurrentOldCusName);
            string[] S_Task_PartNo = Directory.GetDirectories(S_Task_CusName_Path);
            foreach (string item in S_Task_PartNo)
                comboBoxOldPartNo.Items.Add(Path.GetFileNameWithoutExtension(item));

            if (comboBoxOldPartNo.Items.Count == 1)
                comboBoxOldPartNo.Text = comboBoxOldPartNo.Items[0].ToString();
        }

        private void comboBoxOldPartNo_SelectedIndexChanged(object sender, EventArgs e)
        {
            //清空superGrid資料
            panel.Rows.Clear();
            //取得當前選取的料號
            CurrentOldPartNo = comboBoxOldPartNo.Text;
            //打開&清空下拉選單-客戶版次
            comboBoxOldCusRev.Enabled = true;
            comboBoxOldCusRev.Items.Clear();
            comboBoxOldCusRev.Text = "";
            //關閉&清空下拉選單-製程版次
            comboBoxOldOpRev.Enabled = false;
            comboBoxOldOpRev.Items.Clear();
            comboBoxOldOpRev.Text = "";

            string S_Task_PartNo_Path = string.Format(@"{0}\{1}\{2}", CaxEnv.GetGlobaltekTaskDir(), CurrentOldCusName, CurrentOldPartNo);
            string[] S_Task_CusRev = Directory.GetDirectories(S_Task_PartNo_Path);
            foreach (string item in S_Task_CusRev)
                comboBoxOldCusRev.Items.Add(Path.GetFileNameWithoutExtension(item));

            if (comboBoxOldCusRev.Items.Count == 1)
                comboBoxOldCusRev.Text = comboBoxOldCusRev.Items[0].ToString();
        }

        private void comboBoxOldCusRev_SelectedIndexChanged(object sender, EventArgs e)
        {
            //清空superGrid資料
            panel.Rows.Clear();
            //取得當前選取的客戶版次
            CurrentOldCusRev = comboBoxOldCusRev.Text;
            //打開&清空下拉選單-製程版次
            comboBoxOldOpRev.Enabled = true;
            comboBoxOldOpRev.Items.Clear();
            comboBoxOldOpRev.Text = "";

            string S_Task_CusRev_Path = string.Format(@"{0}\{1}\{2}\{3}", CaxEnv.GetGlobaltekTaskDir()
                                                                        , CurrentOldCusName
                                                                        , CurrentOldPartNo
                                                                        , CurrentOldCusRev);
            string[] S_Task_OpRev = Directory.GetDirectories(S_Task_CusRev_Path);
            foreach (string item in S_Task_OpRev)
                comboBoxOldOpRev.Items.Add(Path.GetFileNameWithoutExtension(item));

            if (comboBoxOldOpRev.Items.Count == 1)
                comboBoxOldOpRev.Text = comboBoxOldOpRev.Items[0].ToString();
            
        }

        private void comboBoxOldOpRev_SelectedIndexChanged(object sender, EventArgs e)
        {
            try
            {
                //清空superGrid資料
                panel.Rows.Clear();
                //取得當前選取的製程版次
                CurrentOldOpRev = comboBoxOldOpRev.Text;

                //取得PECreateData.dat
                string PECreateData_Path = string.Format(@"{0}\{1}\{2}\{3}\{4}\{5}\{6}", CaxEnv.GetGlobaltekTaskDir()
                                                                                        , CurrentOldCusName
                                                                                        , CurrentOldPartNo
                                                                                        , CurrentOldCusRev
                                                                                        , CurrentOldOpRev
                                                                                        , "MODEL"
                                                                                        , "PECreateData.dat");
                if (!File.Exists(PECreateData_Path))
                {
                    MessageBox.Show("此料號沒有舊資料檔案，請檢查PECreateData.dat");
                    return;
                }
                CaxPE.ReadPECreateData(PECreateData_Path, out cPECreateData);

                //以料號、客版、製版從DB中取得PEMain
                cComPEMain = session.QueryOver<Com_PEMain>().Where(x => x.partName == comboBoxOldPartNo.Text)
                    .And(x => x.customerVer == comboBoxOldCusRev.Text)
                    .And(x => x.opVer == comboBoxOldOpRev.Text)
                    .SingleOrDefault<Com_PEMain>();

                IList<Com_PartOperation> listComPartOp = session.QueryOver<Com_PartOperation>().Where(x => x.comPEMain == cComPEMain)
                    .OrderBy(x => x.operation1).Asc.List();

                //將舊資料填入SuperGridControl
                GridRow row = new GridRow();
                for (int i = 0; i < listComPartOp.Count; i++)
                {
                    //row = new GridRow(cPECreateData.listOperation[i].Oper1, cPECreateData.listOperation[i].Oper2, cPECreateData.listOperation[i].ERPCode, "刪除");
                    row = new GridRow(new object[panel.Columns.Count]);
                    panel.Rows.Add(row);
                    row.Cells["Oper1Ary"].Value = listComPartOp[i].operation1;
                    row.Cells["Oper2Ary"].Value = listComPartOp[i].sysOperation2.operation2Name;
                    if (listComPartOp[i].form == "IQC")
                    {
                        row.Cells["IQC"].Value = true;
                        row.Cells["IPQC"].Value = false;
                    }
                    else if (listComPartOp[i].form == "IPQC")
                    {
                        row.Cells["IQC"].Value = false;
                        row.Cells["IPQC"].Value = true;
                    }
                    else
                    {
                        row.Cells["IQC"].Value = false;
                        row.Cells["IPQC"].Value = false;
                    }
                    row.Cells["ERP料號"].Value = listComPartOp[i].erpCode;
                    row.Cells["Delete"].Value = "刪除";
                    //row.Cells["Delete"].ReadOnly = true;
                }
                Is_OldPart = true;

                PartDesc.Text = cComPEMain.partDes;
                Material.Text = cComPEMain.partMaterial;
                ERP.Text = cComPEMain.eRPStd;
            }
            catch (System.Exception ex)
            {
                MessageBox.Show("舊料號-選擇製程版次時發生錯誤");
            }
        }

        private void comboBoxCusName_SelectedIndexChanged(object sender, EventArgs e)
        {
            cCom_PEMain.sysCustomer = ((Sys_Customer)comboBoxCusName.SelectedItem);
            //CaxLog.ShowListingWindow(((Sys_Customer)comboBoxCusName.SelectedItem).customerSrNo.ToString());
        }

        private bool DeleteText(string HeadorTail, int count, ref string ERPcode)
        {
            try
            {
                if (ERPcode.Length < count)
                {
                    MessageBox.Show("去碼數 > 料號字元，無法去除");
                    return false;
                }

                if (HeadorTail == "Head") ERPcode = ERPcode.Remove(0, count);

                if (HeadorTail == "Tail") ERPcode = ERPcode.Remove(ERPcode.Length - count);
            }
            catch (System.Exception ex)
            {
                return false;
            }
            return true;
        }

        private void CompBillet_CheckedChanged(object sender, EventArgs e)
        {
            if (CompBillet.Checked == true)
            {
                CusBillet.Checked = false;
            }
        }

        private void CusBillet_CheckedChanged(object sender, EventArgs e)
        {
            if (CusBillet.Checked == true)
            {
                CompBillet.Checked = false;
            }
        }

        private void DelHead_CheckedChanged(object sender, EventArgs e)
        {
            if (DelHead.Checked == true)
            {
                DelTail.Checked = false;
            }
        }

        private void DelTail_CheckedChanged(object sender, EventArgs e)
        {
            if (DelTail.Checked == true)
            {
                DelHead.Checked = false;
            }
        }

        private void UpdateBtn_Click(object sender, EventArgs e)
        {
            if (textPartNo.Text == "") return;

            if (DelHead.Checked == false & DelTail.Checked == false)
            {
                MessageBox.Show("請先選擇【去頭】或【去尾】");
                return;
            }

            if (DelCount.Text == "")
            {
                MessageBox.Show("請輸入去碼數！");
                return;
            }

            int DeleteCount = DeleteCount = Convert.ToInt32(DelCount.Text);
            //if (DelCount.Text != "去碼數" || DelCount.Text != "") DeleteCount = Convert.ToInt32(DelCount.Text);

            string ERPcodeText = textPartNo.Text.Replace("-", "");
            if (DelHead.Checked == true)
            {
                status = DeleteText("Head", DeleteCount, ref ERPcodeText);
                if (!status)
                    return;
            }
            if (DelTail.Checked == true)
            {
                status = DeleteText("Tail", DeleteCount, ref ERPcodeText);
                if (!status)
                    return;
            }

            ERPcode.Text = ERPcodeText;
        }

        private void Close_Click(object sender, EventArgs e)
        {
            this.Close();
        }

        

        private void SaveFile_Click(object sender, EventArgs e)
        {
            try
            {
                tempFolderPath = string.Format(@"{0}\{1}", CaxEnv.GetGlobaltekEnvDir(), "TempFileData");
                if (!Directory.Exists(tempFolderPath))
                {
                    Directory.CreateDirectory(tempFolderPath);
                }

                if (comboBoxCusName.Text == "" || textPartNo.Text == "" || PartDescription.Text == "" || MaterialCT.Text == "")
                {
                    MessageBox.Show("資料不全，無法保存");
                    return;
                }

                PECreateData cPECreateData = new PECreateData();
                cPECreateData.cusName = comboBoxCusName.Text;
                cPECreateData.partName = textPartNo.Text;
                cPECreateData.partDesc = PartDescription.Text;
                cPECreateData.material = MaterialCT.Text;
                cPECreateData.cusRev = textCusRev.Text;
                cPECreateData.opRev = textOpRev.Text;
                cPECreateData.partFilePath = souPartFilePath;
                cPECreateData.billetFilePath = souBilletFilePath;
                if (DelHead.Checked)
                {
                    cPECreateData.head_tail = "Head";
                }
                if (DelTail.Checked)
                {
                    cPECreateData.head_tail = "Tail";
                }
                cPECreateData.removeNum = DelCount.Text;
                cPECreateData.erp = ERPcode.Text;
                if (CompBillet.Checked)
                {
                    cPECreateData.materialResource = "1";
                }
                if (CusBillet.Checked)
                {
                    cPECreateData.materialResource = "A";
                }
                cPECreateData.listOperation = new List<Operation>();
                cPECreateData.oper1Ary = new List<string>();
                cPECreateData.oper2Ary = new List<string>();
                Operation cOperation = new Operation();
                for (int i = 0; i < panel.Rows.Count; i++)
                {
                    cOperation = new Operation();
                    cOperation.Oper1 = ((GridRow)panel.GetRowFromIndex(i)).Cells["Oper1Ary"].Value.ToString();
                    cOperation.Oper2 = ((GridRow)panel.GetRowFromIndex(i)).Cells["Oper2Ary"].Value.ToString();
                    if ((bool)((GridRow)panel.GetRowFromIndex(i)).Cells["IQC"].Value == true)
                    {
                        cOperation.Form = "IQC";
                    }
                    else if ((bool)((GridRow)panel.GetRowFromIndex(i)).Cells["IPQC"].Value == true)
                    {
                        cOperation.Form = "IPQC";
                    }
                    else
                    {
                        cOperation.Form = null;
                    }
                    cOperation.ERPCode = ((GridRow)panel.GetRowFromIndex(i)).Cells["ERP料號"].Value.ToString();
                    cPECreateData.listOperation.Add(cOperation);

                    cPECreateData.oper1Ary.Add(((GridRow)panel.GetRowFromIndex(i)).Cells["Oper1Ary"].Value.ToString());
                    cPECreateData.oper2Ary.Add(((GridRow)panel.GetRowFromIndex(i)).Cells["Oper2Ary"].Value.ToString());
                }

                #region 寫出暫存dat
                string PECreateDataJsonDat = string.Format(@"{0}\{1}_{2}_{3}_{4}.dat", tempFolderPath, comboBoxCusName.Text, textPartNo.Text, textCusRev.Text, textOpRev.Text);
                status = CaxFile.WriteJsonFileData(PECreateDataJsonDat, cPECreateData);
                if (!status)
                {
                    MessageBox.Show("暫存檔案輸出失敗...");
                    return;
                }
                #endregion

                MessageBox.Show("保存成功！");
            }
            catch (System.Exception ex)
            {
            	
            }
        }

        private void ImportFile_Click(object sender, EventArgs e)
        {
            try
            {
                tempFolderPath = string.Format(@"{0}\{1}", CaxEnv.GetGlobaltekEnvDir(), "TempFileData");
                if (!Directory.Exists(tempFolderPath))
                {
                    MessageBox.Show("沒有TempFileData資料夾");
                    return;
                }

                di = new DirectoryInfo(tempFolderPath);
                if (di.GetFiles().Length == 0)
                {
                    MessageBox.Show("沒有暫存檔案可以載入");
                    return;
                }

                openFileDialog2.Filter = "dat Files (*.dat)|*.dat";
                openFileDialog2.InitialDirectory = tempFolderPath;
                DialogResult result = openFileDialog2.ShowDialog();
                if (result != DialogResult.OK)
                {
                    return;
                }

                panel.Rows.Clear();

                PECreateData cPECreateData = new PECreateData();
                CaxPublic.ReadTempFileData(openFileDialog2.FileName, out cPECreateData);

                //重新填回表單
                comboBoxCusName.Text = cPECreateData.cusName;
                textPartNo.Text = cPECreateData.partName;
                PartDescription.Text = cPECreateData.partDesc;
                
                MaterialComboTree materialvalue = new MaterialComboTree();
                materialvalue.materialName = cPECreateData.material;
                materialvalue.category = "隨便加一個分類，並不會顯示在下拉選單中";
                List<MaterialComboTree> tempMaterial = new List<MaterialComboTree>();
                tempMaterial.Add(materialvalue);
                AddTemp_Material(tempMaterial);
                
                textCusRev.Text = cPECreateData.cusRev;
                textOpRev.Text = cPECreateData.opRev;
                if (cPECreateData.head_tail == "Head")
                {
                    DelHead.Checked = true;
                }
                if (cPECreateData.head_tail == "Tail")
                {
                    DelTail.Checked = true;
                }
                DelCount.Text = cPECreateData.removeNum;
                ERPcode.Text = cPECreateData.erp;
                if (cPECreateData.materialResource == "1")
                {
                    CompBillet.Checked = true;
                }
                if (cPECreateData.materialResource == "A")
                {
                    CusBillet.Checked = true;
                }

                GridRow row = new GridRow();
                foreach (Operation i in cPECreateData.listOperation)
                {
                    
                    //row = new GridRow(i.Oper1,i.Oper2,i.ERPCode, "刪除");
                    //panel.Rows.Add(row);

                    row = new GridRow(new object[panel.Columns.Count]);
                    panel.Rows.Add(row);
                    row.Cells["Oper1Ary"].Value = i.Oper1;
                    row.Cells["Oper2Ary"].Value = i.Oper2;
                    if (i.Form == "IQC")
                    {
                        row.Cells["IQC"].Value = true;
                        row.Cells["IPQC"].Value = false;
                    }
                    else if (i.Form == "IPQC")
                    {
                        row.Cells["IQC"].Value = false;
                        row.Cells["IPQC"].Value = true;
                    }
                    else
                    {
                        row.Cells["IQC"].Value = false;
                        row.Cells["IPQC"].Value = false;
                    }
                    row.Cells["ERP料號"].Value = i.ERPCode;
                    row.Cells["Delete"].Value = "刪除";
                }

                CaxPart.CloseAllParts();
                CaxPart.OpenBaseDisplay(cPECreateData.partFilePath);
                labelPartFileName.Text = Path.GetFileName(cPECreateData.partFilePath);
                BilletFileName.Text = Path.GetFileName(cPECreateData.billetFilePath);
                souPartFilePath = cPECreateData.partFilePath;
                souBilletFilePath = cPECreateData.billetFilePath;
            }
            catch (System.Exception ex)
            {
                MessageBox.Show(ex.ToString());
            }
        }
        private void AddTemp_Material(List<MaterialComboTree> materialvalue)
        {
            MaterialCT.BackgroundStyle.Class = "TextBoxBorder";
            MaterialCT.BackgroundStyle.CornerType = DevComponents.DotNetBar.eCornerType.Square;
            MaterialCT.ButtonDropDown.Visible = true;
            MaterialCT.DisplayMembers = "materialName";
            MaterialCT.GroupingMembers = "category";
            MaterialCT.DataSource = materialvalue;
        }

        private void tabControl1_SelectedTabChanged(object sender, TabStripTabChangedEventArgs e)
        {
            if (tabControl1.SelectedTab.Name == "tabItem1")
            {
                SaveFile.Enabled = true;
                ImportFile.Enabled = true;
            }
            else if (tabControl1.SelectedTab.Name == "tabItem2")
            {
                SaveFile.Enabled = false;
                ImportFile.Enabled = false;
            }
        }

        private void MaterialCT_ButtonDropDownClick(object sender, CancelEventArgs e)
        {
            MaterialCT.BackgroundStyle.Class = "TextBoxBorder";
            MaterialCT.BackgroundStyle.CornerType = DevComponents.DotNetBar.eCornerType.Square;
            MaterialCT.ButtonDropDown.Visible = true;
            MaterialCT.DisplayMembers = "materialName";
            MaterialCT.GroupingMembers = "category";
            MaterialCT.DataSource = ListMaterialComboTree;
            MaterialCT.DropDownWidth = 350;
        }

        private void OperSuperGridControl_CellClick(object sender, GridCellClickEventArgs e)
        {
            if (e.GridCell.GridColumn == panel.Columns["IQC"])
            {
                if ((bool)e.GridCell.Value)
                {
                    e.GridCell.GridRow.Cells["IPQC"].Value = false;
                }
            }
            else if (e.GridCell.GridColumn == panel.Columns["IPQC"])
            {
                if ((bool)e.GridCell.Value)
                {
                    e.GridCell.GridRow.Cells["IQC"].Value = false;
                }
            }
        }
    }

    public class PEComboBox : GridComboTreeEditControl
    {
        public PEComboBox(IEnumerable Oper2StringAry)
        {
            //DataSource = Oper2StringAry;
            
            //DisplayMember = "operation2Name";
            //ValueMember = "operation2SrNo";
            
            //DropDownStyle = ComboBoxStyle.DropDownList;
            //this.MouseWheel += new MouseEventHandler(Oper2Changed);
            //this.DropDownClosed += new EventHandler(Oper2Changed);

            //new
            this.BackgroundStyle.Class = "TextBoxBorder";
            this.BackgroundStyle.CornerType = DevComponents.DotNetBar.eCornerType.Square;
            this.ButtonDropDown.Visible = true;
            this.DisplayMembers = "operation2Name";
            this.GroupingMembers = "category";
            this.DataSource = Oper2StringAry;
            //this.DoubleClick += new EventHandler(Oper2Changed);
            //old
            //this.DropDown += new EventHandler(Oper2Changed);
            
        }
        /*
        public void Oper2Changed(object sender, EventArgs e)
        {
            try
            {
                IList<Sys_Operation2> listSys_Operation2 = MyHibernateHelper.SessionFactory.OpenSession().QueryOver<Sys_Operation2>().OrderBy(x => x.operation2SrNo).Asc
                                                                    .ThenBy(x => x.operation2Name).Asc
                                                                    .List<Sys_Operation2>();
                DataSource = listSys_Operation2;
                DisplayMember = "operation2Name";
                DropDownStyle = ComboBoxStyle.DropDownList;
            }
            catch (System.Exception ex)
            {
            	
            }
        }
        */
    }

    public class OperDeleteBtn : GridButtonXEditControl
    {
        public OperDeleteBtn()
        {
            Click += DeleteBtnClick;
        }
        public void DeleteBtnClick(object sender, EventArgs e)
        {
            GridPanel panel = PEGenerateDlg.panel;
            
            OperDeleteBtn cOperDelectBtn = (OperDeleteBtn)sender;
            int index = cOperDelectBtn.EditorCell.RowIndex;
            PEGenerateDlg.ListAddOper.Remove(panel.GetCell(index, 0).Value.ToString());
            panel.Rows.RemoveAt(index);
        }
    }
    
}
