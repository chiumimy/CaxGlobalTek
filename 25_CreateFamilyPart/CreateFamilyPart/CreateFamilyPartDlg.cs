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
using System.IO;
using NXOpen;
using NXOpen.UF;

namespace CreateFamilyPart
{
    public partial class CreateFamilyPartDlg : DevComponents.DotNetBar.Office2007Form
    {
        private static UFSession theUfSession = UFSession.GetUFSession();
        private static Session theSession = Session.GetSession();
        public static ISession session = MyHibernateHelper.SessionFactory.OpenSession();
        public static IList<Com_PartOperation> listComPartOperation = new List<Com_PartOperation>();
        public static string souPartFilePath = "-1", souBilletFilePath = "", CusName = "", PartNo = "", CusRev = "", OpRev = "",
            PartDesc = "", PartMaterial = "", ERPCode = "", MaterialSource = "", desPartFilePath = "", desBilletFilePath = "", oldPartFilePath = "",  oldBilletFilePath = "";
        public bool status;
        public static PECreateData cPECreateData = new PECreateData();
        public static Com_PEMain cCom_PEMain = new Com_PEMain();
        public static Com_PEMain oldPEMain = new Com_PEMain();

        public CreateFamilyPartDlg()
        {
            InitializeComponent();
            InitializeDlgData();
        }
        public void InitializeDlgData()
        {
            //舊客戶資料填入舊料號(從Server取)
            string[] S_Task_CusName = Directory.GetDirectories(CaxEnv.GetGlobaltekTaskDir());
            foreach (string item in S_Task_CusName)
            {
                Old_Cus.Items.Add(Path.GetFileNameWithoutExtension(item));//走訪每個元素只取得目錄名稱(不含路徑)並加入dirlist集合中
            }

            //客戶資料填入新料號(從DB取)
            IList<Sys_Customer> customerName = session.QueryOver<Sys_Customer>().List<Sys_Customer>();
            New_Cus.DisplayMember = "customerName"; New_Cus.ValueMember = "customerSrNo";
            foreach (Sys_Customer i in customerName)
            {
                New_Cus.Items.Add(i);
            }

            //取得材質資料庫並加入到下拉選單中
            IList<Sys_Material> listSysMaterial = session.QueryOver<Sys_Material>().List<Sys_Material>();
            foreach (Sys_Material i in listSysMaterial)
                New_Material.Items.Add(i.materialName);

            #region 預設
            Old_PartNo.Enabled = false;
            Old_CusRev.Enabled = false;
            Old_OpRev.Enabled = false;

            New_Cus.Enabled = false; 
            New_PartNo.Enabled = false;
            New_CusRev.Enabled = false; 
            New_OpRev.Enabled = false; 
            New_PartDesc.Enabled = false; PartDescSame.Enabled = false;
            New_Material.Enabled = false; MaterialSame.Enabled = false;
            New_ERP.Enabled = false;
            New_CompBillet.Enabled = false; MaterialSourceSame.Enabled = false;
            New_CusBillet.Enabled = false;
            PartFileSame.Enabled = false; BilletFileSame.Enabled = false;
            #endregion
            
        }

        private void Old_Cus_SelectedIndexChanged(object sender, EventArgs e)
        {
            //關閉新料號下拉選單-客戶
            New_Cus.Text = ""; New_Cus.Enabled = false; 
            //關閉&清空下拉選單-料號
            Old_PartNo.Enabled = true; Old_PartNo.Items.Clear(); Old_PartNo.Text = "";
            New_PartNo.Enabled = false; New_PartNo.Text = "";
            //關閉&清空下拉選單-客戶版次
            Old_CusRev.Enabled = false; Old_CusRev.Items.Clear(); Old_CusRev.Text = "";
            New_CusRev.Enabled = false; New_CusRev.Text = ""; 
            //關閉&清空下拉選單-製程版次
            Old_OpRev.Enabled = false; Old_OpRev.Items.Clear(); Old_OpRev.Text = "";
            New_OpRev.Enabled = false; New_OpRev.Text = ""; 
            //關閉&清空-品名
            Old_PartDesc.Text = "";
            PartDescSame.Checked = false; New_PartDesc.Text = ""; New_PartDesc.Enabled = false; PartDescSame.Enabled = false; 
            //關閉&清空-材質
            Old_Material.Text = "";
            MaterialSame.Checked = false; New_Material.Text = ""; New_Material.Enabled = false; MaterialSame.Enabled = false; 
            //關閉&清空-ERP
            Old_ERP.Text = "";
            New_ERP.Text = ""; New_ERP.Enabled = false;
            //關閉&清空-材料來源
            Old_MaterialSource.Text = "";
            MaterialSourceSame.Checked = false; MaterialSourceSame.Enabled = false;
            New_CompBillet.Checked = false; New_CompBillet.Enabled = false;
            New_CusBillet.Checked = false; New_CusBillet.Enabled = false; 
            //關閉&清空-成品檔案
            PartFileSame.Checked = false; PartFileSame.Enabled = false;
            Old_PartFile.Text = "";
            New_PartFile.Text = "";
            //關閉&清空-胚料檔案
            BilletFileSame.Checked = false; BilletFileSame.Enabled = false;
            Old_BilletFile.Text = "";
            New_BilletFile.Text = "";
            

            string S_Task_CusName_Path = string.Format(@"{0}\{1}", CaxEnv.GetGlobaltekTaskDir(), Old_Cus.Text);
            string[] S_Task_PartNo = Directory.GetDirectories(S_Task_CusName_Path);
            foreach (string item in S_Task_PartNo)
            {
                Old_PartNo.Items.Add(Path.GetFileNameWithoutExtension(item));//走訪每個元素只取得目錄名稱(不含路徑)並加入dirlist集合中
            }
        }

        private void Old_PartNo_SelectedIndexChanged(object sender, EventArgs e)
        {
            //打開&清空下拉選單-客戶版次
            Old_CusRev.Enabled = true; Old_CusRev.Items.Clear(); Old_CusRev.Text = "";
            
            
            //關閉&清空下拉選單-製程版次
            Old_OpRev.Enabled = false; Old_OpRev.Items.Clear(); Old_OpRev.Text = "";
           
            

            string S_Task_PartNo_Path = string.Format(@"{0}\{1}\{2}", CaxEnv.GetGlobaltekTaskDir(), Old_Cus.Text, Old_PartNo.Text);
            string[] S_Task_CusRev = Directory.GetDirectories(S_Task_PartNo_Path);
            foreach (string item in S_Task_CusRev)
            {
                Old_CusRev.Items.Add(Path.GetFileNameWithoutExtension(item));//走訪每個元素只取得目錄名稱(不含路徑)並加入dirlist集合中
            }
            if (Old_CusRev.Items.Count == 1)
            {
                Old_CusRev.Text = Old_CusRev.Items[0].ToString();
            }
        }

        private void Old_CusRev_SelectedIndexChanged(object sender, EventArgs e)
        {
            
            //打開&清空下拉選單-製程版次
            Old_OpRev.Enabled = true; Old_OpRev.Items.Clear(); Old_OpRev.Text = "";
            
            

            string S_Task_OpRev_Path = string.Format(@"{0}\{1}\{2}\{3}", CaxEnv.GetGlobaltekTaskDir(), Old_Cus.Text, Old_PartNo.Text, Old_CusRev.Text);
            string[] S_Task_OpRev = Directory.GetDirectories(S_Task_OpRev_Path);
            foreach (string item in S_Task_OpRev)
            {
                Old_OpRev.Items.Add(Path.GetFileNameWithoutExtension(item));//走訪每個元素只取得目錄名稱(不含路徑)並加入dirlist集合中
            }
            if (Old_OpRev.Items.Count == 1)
            {
                Old_OpRev.Text = Old_OpRev.Items[0].ToString();
            }
        }

        private void Old_OpRev_SelectedIndexChanged(object sender, EventArgs e)
        {
            try
            {
                //打開新料號下拉選單-客戶、料號、客戶版次、製程版次、品名、材質、ERP、材料來源、成品、胚料
                New_Cus.Text = Old_Cus.Text;
                New_Cus.Enabled = true; New_Cus.Text = "";
                New_PartNo.Enabled = true; New_PartNo.Text = "";
                New_CusRev.Enabled = true;
                New_OpRev.Enabled = true;
                PartDescSame.Checked = false; PartDescSame.Enabled = true; New_PartDesc.Enabled = true;
                MaterialSame.Checked = false; MaterialSame.Enabled = true; New_Material.Enabled = true;
                PartFileSame.Checked = false; PartFileSame.Enabled = true;
                BilletFileSame.Checked = false; BilletFileSame.Enabled = true; 
                New_ERP.Enabled = true;
                MaterialSourceSame.Checked = false; MaterialSourceSame.Enabled = true; New_CompBillet.Enabled = true; New_CusBillet.Enabled = true;


                oldPEMain = session.QueryOver<Com_PEMain>().Where(x => x.partName == Old_PartNo.Text)
                                                                      .Where(x => x.customerVer == Old_CusRev.Text)
                                                                      .Where(x => x.opVer == Old_OpRev.Text).SingleOrDefault();
                Old_PartDesc.Text = oldPEMain.partDes;
                Old_Material.Text = oldPEMain.partMaterial;
                Old_ERP.Text = oldPEMain.eRPStd;
                if (oldPEMain.partFilePath != null)
                {
                    Old_PartFile.Text = Path.GetFileNameWithoutExtension(oldPEMain.partFilePath);
                    //先把舊的零件路徑加入，如果遇到料號一樣(表示產品一樣)，DB才能有partFilePath&billetFilePath可以插入
                    desPartFilePath = oldPEMain.partFilePath;
                }
                else
                {
                    Old_PartFile.Text = "";
                    desPartFilePath = "";
                }
                if (oldPEMain.billetFilePath != null)
                {
                    Old_BilletFile.Text = Path.GetFileNameWithoutExtension(oldPEMain.billetFilePath);
                    //先把舊的零件路徑加入，如果遇到料號一樣(表示產品一樣)，DB才能有partFilePath&billetFilePath可以插入
                    desBilletFilePath = oldPEMain.billetFilePath;
                }
                else
                {
                    Old_BilletFile.Text = "";
                    desBilletFilePath = "";
                }
                //插入材料來源(自備胚=1，管材/鍛件=A)
                if (oldPEMain.materialSource == "1")
                {
                    Old_MaterialSource.Text = "自備胚";
                }
                else
                {
                    Old_MaterialSource.Text = "管材/鍛件";
                }
                //取得此料號所排的製程資訊
                listComPartOperation = session.QueryOver<Com_PartOperation>().Where(x => x.comPEMain == oldPEMain).List();
            }
            catch (System.Exception ex)
            {
                MessageBox.Show("取得此舊料號資訊失敗");
                return;
            }
            

        }

        private void PartDescSame_CheckedChanged(object sender, EventArgs e)
        {
            if (PartDescSame.Checked == true)
            {
                New_PartDesc.Text = Old_PartDesc.Text;
                New_PartDesc.Enabled = false;
            }
            else
            {
                New_PartDesc.Enabled = true;
                New_PartDesc.Text = "";
            }
        }

        private void MaterialSame_CheckedChanged(object sender, EventArgs e)
        {
            if (MaterialSame.Checked == true)
            {
                New_Material.Text = Old_Material.Text;
                New_Material.Enabled = false;
            }
            else
            {
                New_Material.Enabled = true;
                New_Material.Text = "";
            }
        }

        private void SeeProcess_Click(object sender, EventArgs e)
        {
            if (Old_OpRev.Text == "")
            {
                MessageBox.Show("請先完成舊料號資料");
                return;
            }
            ProcessDlg cProcessDlg = new ProcessDlg(listComPartOperation);
            cProcessDlg.ShowDialog();
        }

        private void MaterialSourceSame_CheckedChanged(object sender, EventArgs e)
        {
            if (MaterialSourceSame.Checked == true)
            {
                if (Old_MaterialSource.Text == "自備胚")
                {
                    New_CompBillet.Checked = true;
                }
                else
                {
                    New_CusBillet.Checked = true;
                }
            }
            else
            {
                New_CompBillet.Checked = false;
                New_CusBillet.Checked = false;
            }
        }

        private void New_CompBillet_CheckedChanged(object sender, EventArgs e)
        {
            if (New_CompBillet.Checked == true)
            {
                New_CusBillet.Checked = false;
            }
        }

        private void New_CusBillet_CheckedChanged(object sender, EventArgs e)
        {
            if (New_CusBillet.Checked == true)
            {
                New_CompBillet.Checked = false;
            }
        }

        private void Close_Click(object sender, EventArgs e)
        {
            CaxPart.CloseAllParts();
            this.Close();
        }

        private void SelectPartFile_Click(object sender, EventArgs e)
        {
            try
            {
                openFileDialog1.Filter = "Part Files (*.prt)|*.prt|All Files (*.*)|*.*";
                DialogResult result = openFileDialog1.ShowDialog();
                if (result == DialogResult.OK)
                {
                    //取得檔案名稱(檔名+副檔名)
                    New_PartFile.Text = openFileDialog1.SafeFileName;
                    //取得檔案完整路徑(路徑+檔名+副檔名)
                    souPartFilePath = openFileDialog1.FileName;
                    //開啟選擇的檔案
                    CaxPart.OpenBaseDisplay(souPartFilePath);
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
                    New_BilletFile.Text = openFileDialog1.SafeFileName;
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
                #region 檢查資訊是否完整
                status = DataCompleted();
                if (!status)
                    return;
                #endregion
     
                #region 新料號---定義根目錄
                string S_NewPart_Folder = string.Format(@"{0}\{1}\{2}\{3}\{4}",
                                                        CaxEnv.GetGlobaltekTaskDir(), CusName, PartNo, CusRev.ToUpper(), OpRev.ToUpper());
                //定義MODEL資料夾路徑
                string ModelFolderFullPath = string.Format(@"{0}\{1}", S_NewPart_Folder, "MODEL");
                //定義總組立檔案名稱
                string AsmCompFileFullPath = string.Format(@"{0}\{1}", S_NewPart_Folder, PartNo + "_MOT_" + OpRev.ToUpper() + ".prt");
                //定義CAM資料夾路徑、OIS資料夾路徑、三階檔案路徑
                string CAMFolderPath = "", OISFolderPath = "";
                #endregion
        
                #region 檢查舊料號總組立檔案是否存在
                string S_OldPart_Path = string.Format(@"{0}\{1}\{2}\{3}\{4}", CaxEnv.GetGlobaltekTaskDir(), Old_Cus.Text, Old_PartNo.Text, Old_CusRev.Text, Old_OpRev.Text);
                string OldPartPath = string.Format(@"{0}\{1}_MOT_{2}.prt", S_OldPart_Path, Old_PartNo.Text, Old_OpRev.Text);
                if (!File.Exists(OldPartPath))
                {
                    MessageBox.Show("舊料號總組立檔案不存在，無法建立FamilyPart");
                    return;
                }
                #endregion
  
                #region 建立MODEL資料夾
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
 
                #region 將值儲存起來
                cPECreateData.cusName = CusName;
                cPECreateData.partName = PartNo;
                cPECreateData.cusRev = CusRev.ToUpper();
                cPECreateData.opRev = OpRev.ToUpper();
                cPECreateData.listOperation = new List<Operation>();
                cPECreateData.oper1Ary = new List<string>();
                cPECreateData.oper2Ary = new List<string>();
                Operation cOperation = new Operation();
                foreach (Com_PartOperation i in listComPartOperation)
                {
                    cOperation = new Operation();
                    cOperation.Oper1 = i.operation1.ToString();
                    cOperation.Oper2 = session.QueryOver<Sys_Operation2>().Where(x => x.operation2SrNo == i.sysOperation2.operation2SrNo)
                                        .SingleOrDefault().operation2Name;
                    cOperation.ERPCode = GetERPcode(i.operation1);

                    //建立CAM資料夾
                    CAMFolderPath = string.Format(@"{0}\{1}\{2}", Path.GetDirectoryName(AsmCompFileFullPath), "OP" + i.operation1, "CAM");
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

                    //建立OIS資料夾
                    OISFolderPath = string.Format(@"{0}\{1}\{2}", Path.GetDirectoryName(AsmCompFileFullPath), "OP" + i.operation1, "OIS");
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

                    cPECreateData.listOperation.Add(cOperation);
                    cPECreateData.oper1Ary.Add(i.operation1);
                    cPECreateData.oper2Ary.Add(session.QueryOver<Sys_Operation2>().Where(x => x.operation2SrNo == i.sysOperation2.operation2SrNo)
                                                .SingleOrDefault().operation2Name); 
                }
                #endregion
         
                #region 複製舊料號所有檔案到新料號資料夾
                string[] OldParts = System.IO.Directory.GetFileSystemEntries(S_OldPart_Path,"*.prt");
                foreach (string item in OldParts)
                {
                    if (!File.Exists(item))
                    {
                        continue;
                    }

                    //複製檔案到新料號資料夾
                    string NewPartName = string.Format(@"{0}\{1}", S_NewPart_Folder, Path.GetFileName(item));
                    File.Copy(item, NewPartName, true);
                }
                #endregion
       
                #region 開啟總組立
                string OldPartPathAtNewFolder = string.Format(@"{0}\{1}", S_NewPart_Folder, Path.GetFileName(OldPartPath));
                BasePart newAsmPart;
                if (!CaxPart.OpenBaseDisplay(OldPartPathAtNewFolder, out newAsmPart))
                {
                    CaxLog.ShowListingWindow("MOT開啟失敗！");
                    return;
                }
                #endregion
                
                #region 取得所有Comp檔案
                Part displayPart = theSession.Parts.Display;
                if (displayPart == null)
                {
                    MessageBox.Show("找不到Root檔案，無法建立FamilyPart");
                    return;
                }
                List<NXOpen.Assemblies.Component> ListChildrenComp = new List<NXOpen.Assemblies.Component>();
                CaxAsm.GetCompChildren(displayPart.ComponentAssembly.RootComponent, ref ListChildrenComp);
                ListChildrenComp.Add(displayPart.ComponentAssembly.RootComponent);
                #endregion
                
                foreach (NXOpen.Assemblies.Component i in ListChildrenComp)
                {
                    //CaxLog.ShowListingWindow(i.DisplayName);
                    if (i.IsSuppressed == true)
                    {
                        //CaxLog.ShowListingWindow(i.DisplayName);
                        NXOpen.Assemblies.Component[] components1 = new NXOpen.Assemblies.Component[1];
                        components1[0] = i;
                        theSession.Parts.Work.ComponentAssembly.UnsuppressComponents(components1);
                    }
                    //CaxLog.ShowListingWindow(i.DisplayName);
                    //CaxLog.ShowListingWindow("----------");
                }
                //CaxLog.ShowListingWindow("0");


                //新邏輯測試
                //1.料號一樣，製程版次一樣，成品、胚料檔名一樣，客戶版次(品名、材質可能不同)不一樣->整套模型複製其餘不動
                //2.料號一樣，製程版次一樣，成品、胚料檔名不一樣，客戶版次(品名、材質可能不同)不一樣->不換MOT名稱，成品變更，胚料變更，其餘不動
                //3.料號一樣，製程版次不一樣，成品、胚料檔名一樣，客戶版次隨意->更換MOT名稱，成品不換，胚料不換
                //4.料號一樣，製程版次不一樣，成品、胚料檔名不一樣，客戶版次隨意->更換MOT名稱，成品變更，胚料變更
                //5.料號不一樣，製程版次一樣，成品、胚料檔名一樣，客戶版次隨意->整套檔案變更不換MOT名稱，成品不換，胚料不換
                //6.料號不一樣，製程版次一樣，成品、胚料檔名不一樣，客戶版次隨意->整套檔案變更不換MOT名稱，成品變更，胚料變更
                //7.料號不一樣，製程版次不一樣，成品、胚料檔名一樣，客戶版次隨意->整套檔案變更，成品不換，胚料不換
                //8.料號不一樣，製程版次不一樣，成品、胚料檔名不一樣，客戶版次隨意->整套檔案變更，成品變更，胚料變更

                #region 1.料號一樣，製程版次一樣，成品、胚料檔名一樣，客戶版次(品名、材質可能不同)不一樣->整套模型複製其餘不動
                if (New_PartNo.Text.ToUpper() == Old_PartNo.Text.ToUpper() && 
                    Old_OpRev.Text.ToUpper() == New_OpRev.Text.ToUpper() && 
                    Old_PartFile.Text.ToUpper() == Path.GetFileNameWithoutExtension(New_PartFile.Text).ToUpper() &&
                    Old_BilletFile.Text.ToUpper() == Path.GetFileNameWithoutExtension(New_BilletFile.Text).ToUpper())
                {
                    goto SaveMOT;
                }
                #endregion
                #region 2.料號一樣，製程版次一樣，成品、胚料檔名不一樣，客戶版次(品名、材質可能不同)不一樣->不換MOT名稱，成品變更，胚料變更，其餘不動
                else if (New_PartNo.Text.ToUpper() == Old_PartNo.Text.ToUpper() &&
                         Old_OpRev.Text.ToUpper() == New_OpRev.Text.ToUpper() &&
                         Old_PartFile.Text.ToUpper() != Path.GetFileNameWithoutExtension(New_PartFile.Text).ToUpper() &&
                         Old_BilletFile.Text.ToUpper() != Path.GetFileNameWithoutExtension(New_BilletFile.Text).ToUpper())
                {
                    //紀錄OwningComponent
                    Dictionary<NXOpen.Assemblies.Component, List<string>> DicOwningCompME = new Dictionary<NXOpen.Assemblies.Component, List<string>>();
                    Dictionary<NXOpen.Assemblies.Component, List<string>> DicOwningCompTE = new Dictionary<NXOpen.Assemblies.Component, List<string>>();
                    foreach (NXOpen.Assemblies.Component i in ListChildrenComp)
                    {
                        string PartName = "";
                        PartName = i.DisplayName;
                        if (keepMETE.Checked == true)
                        {
                            //料號相同且保留，不做事
                        }
                        else
                        {
                            //料號相同且不保留，須刪除檔案再save，再組空檔案
                            if (PartName.Contains(Old_PartNo.Text + "_TE"))
                            {
                                #region 紀錄TE的OwningComponent，並刪除TE檔案
                                PartName = PartName.Replace(Old_PartNo.Text, New_PartNo.Text);
                                string NewPartFilePath = string.Format(@"{0}\{1}.prt", S_NewPart_Folder, PartName);

                                List<string> listTEName = new List<string>();
                                status = DicOwningCompTE.TryGetValue(i.OwningComponent, out listTEName);
                                if (!status)
                                {
                                    listTEName = new List<string>();
                                }
                                listTEName.Add(NewPartFilePath);
                                DicOwningCompTE[i.OwningComponent] = listTEName;

                                try
                                {
                                    if (File.Exists(((Part)i.Prototype).FullPath))
                                        File.Delete(((Part)i.Prototype).FullPath);
                                }
                                catch (System.Exception ex)
                                {

                                }
                                CaxPublic.DelectObject(i);
                                CaxPart.ClosePartMemory(PartName);

                                //AddEmptyComponent(NewPartFilePath);
                                continue;
                                #endregion
                            }
                            if (PartName.Contains(Old_PartNo.Text + "_ME"))
                            {
                                #region 紀錄ME的OwningComponent，並刪除ME檔案
                                PartName = PartName.Replace(Old_PartNo.Text, New_PartNo.Text);
                                string NewPartFilePath = string.Format(@"{0}\{1}.prt", S_NewPart_Folder, PartName);

                                List<string> listMEName = new List<string>();
                                status = DicOwningCompME.TryGetValue(i.OwningComponent, out listMEName);
                                if (!status)
                                {
                                    listMEName = new List<string>();
                                }
                                listMEName.Add(NewPartFilePath);
                                DicOwningCompME[i.OwningComponent] = listMEName;

                                try
                                {
                                    if (File.Exists(((Part)i.Prototype).FullPath))
                                        File.Delete(((Part)i.Prototype).FullPath);
                                }
                                catch (System.Exception ex)
                                {
                                	
                                }
                                
                                CaxPublic.DelectObject(i);
                                CaxPart.ClosePartMemory(PartName);

                                //AddEmptyComponent(NewPartFilePath);
                                continue;
                                #endregion
                            }
                        }
                        if (PartName.ToUpper() == Old_PartFile.Text.ToUpper() && Old_PartFile.Text != Path.GetFileNameWithoutExtension(New_PartFile.Text))
                        {
                            #region 建立新的成品檔案名稱
                            PartName = PartName.Replace(Old_PartFile.Text, New_PartFile.Text);
                            string NewPartFilePath = string.Format(@"{0}\{1}", S_NewPart_Folder, PartName);
                            desPartFilePath = NewPartFilePath;
                            CaxAsm.SetWorkComponent(i.OwningComponent);
                            if (File.Exists(((Part)i.Prototype).FullPath))
                            {
                                File.Delete(((Part)i.Prototype).FullPath);
                            }
                            CaxPublic.DelectObject(i);
                            AddExistComponent(souPartFilePath, NewPartFilePath);
                            continue;
                            #endregion
                        }
                        if (PartName.ToUpper() == Old_BilletFile.Text.ToUpper() && Old_BilletFile.Text != Path.GetFileNameWithoutExtension(New_BilletFile.Text))
                        {
                            #region 料號沒變的話，如果沒選胚料檔要跳出
                            if (New_BilletFile.Text == "")
                            {
                                continue;
                            }
                            #endregion
                            #region 建立新的胚料檔案名稱
                            if (New_BilletFile.Text != "")
                            {
                                PartName = PartName.Replace(Old_BilletFile.Text, New_BilletFile.Text);
                            }
                            else
                            {
                                PartName = New_PartNo.Text + "_Blank.prt";
                            }

                            string NewPartFilePath = string.Format(@"{0}\{1}", S_NewPart_Folder, PartName);
                            desBilletFilePath = NewPartFilePath;
                            CaxAsm.SetWorkComponent(i.OwningComponent);
                            if (File.Exists(((Part)i.Prototype).FullPath))
                            {
                                File.Delete(((Part)i.Prototype).FullPath);
                            }
                            CaxPublic.DelectObject(i);
                            AddBilletFile(souBilletFilePath, NewPartFilePath);
                            continue;
                            #endregion
                        }
                    }
                    //開始組裝TE、ME檔案
                    foreach (KeyValuePair<NXOpen.Assemblies.Component, List<string>> kvp in DicOwningCompME)
                    {
                        CaxAsm.SetWorkComponent(kvp.Key);
                        foreach (string i in kvp.Value)
                        {
                            AddEmptyComponent(i);
                        }
                    }
                    foreach (KeyValuePair<NXOpen.Assemblies.Component, List<string>> kvp in DicOwningCompTE)
                    {
                        CaxAsm.SetWorkComponent(kvp.Key);
                        foreach (string i in kvp.Value)
                        {
                            AddEmptyComponent(i);
                        }
                    }
                }
                #endregion
                #region 3.料號一樣，製程版次不一樣，成品、胚料檔名一樣，客戶版次隨意->更換MOT的版次名稱，成品不換，胚料不換
                else if (New_PartNo.Text.ToUpper() == Old_PartNo.Text.ToUpper() &&
                         Old_OpRev.Text.ToUpper() != New_OpRev.Text.ToUpper() &&
                         Old_PartFile.Text.ToUpper() == Path.GetFileNameWithoutExtension(New_PartFile.Text).ToUpper() &&
                         Old_BilletFile.Text.ToUpper() == Path.GetFileNameWithoutExtension(New_BilletFile.Text).ToUpper())
                {
                    //紀錄OwningComponent
                    Dictionary<NXOpen.Assemblies.Component, List<string>> DicOwningCompME = new Dictionary<NXOpen.Assemblies.Component, List<string>>();
                    Dictionary<NXOpen.Assemblies.Component, List<string>> DicOwningCompTE = new Dictionary<NXOpen.Assemblies.Component, List<string>>();
                    foreach (NXOpen.Assemblies.Component i in ListChildrenComp)
                    {
                        string PartName = "";
                        PartName = i.DisplayName;
                        if (keepMETE.Checked == true)
                        {
                            //料號相同且保留，不做事
                        }
                        else
                        {
                            //料號相同且不保留，須刪除檔案再save，再組空檔案
                            if (PartName.Contains(Old_PartNo.Text + "_TE"))
                            {
                                #region 紀錄TE的OwningComponent，並刪除TE檔案
                                PartName = PartName.Replace(Old_PartNo.Text, New_PartNo.Text);
                                string NewPartFilePath = string.Format(@"{0}\{1}.prt", S_NewPart_Folder, PartName);

                                List<string> listTEName = new List<string>();
                                status = DicOwningCompTE.TryGetValue(i.OwningComponent, out listTEName);
                                if (!status)
                                {
                                    listTEName = new List<string>();
                                }
                                listTEName.Add(NewPartFilePath);
                                DicOwningCompTE[i.OwningComponent] = listTEName;

                                try
                                {
                                    if (File.Exists(((Part)i.Prototype).FullPath))
                                        File.Delete(((Part)i.Prototype).FullPath);
                                }
                                catch (System.Exception ex)
                                {

                                }
                                CaxPublic.DelectObject(i);
                                CaxPart.ClosePartMemory(PartName);

                                //AddEmptyComponent(NewPartFilePath);
                                continue;
                                #endregion
                            }
                            if (PartName.Contains(Old_PartNo.Text + "_ME"))
                            {
                                #region 紀錄ME的OwningComponent，並刪除ME檔案
                                PartName = PartName.Replace(Old_PartNo.Text, New_PartNo.Text);
                                string NewPartFilePath = string.Format(@"{0}\{1}.prt", S_NewPart_Folder, PartName);

                                List<string> listMEName = new List<string>();
                                status = DicOwningCompME.TryGetValue(i.OwningComponent, out listMEName);
                                if (!status)
                                {
                                    listMEName = new List<string>();
                                }
                                listMEName.Add(NewPartFilePath);
                                DicOwningCompME[i.OwningComponent] = listMEName;

                                try
                                {
                                    if (File.Exists(((Part)i.Prototype).FullPath))
                                        File.Delete(((Part)i.Prototype).FullPath);
                                }
                                catch (System.Exception ex)
                                {

                                }

                                CaxPublic.DelectObject(i);
                                CaxPart.ClosePartMemory(PartName);

                                //AddEmptyComponent(NewPartFilePath);
                                continue;
                                #endregion
                            }
                        }
                        if (PartName.Contains("_MOT_"))
                        {
                            //建立新的檔案名稱
                            CaxAsm.RenamePart(((Part)displayPart.ComponentAssembly.RootComponent.Prototype), Path.GetFileNameWithoutExtension(AsmCompFileFullPath));
                        }
                    }
                    //開始組裝TE、ME檔案
                    foreach (KeyValuePair<NXOpen.Assemblies.Component, List<string>> kvp in DicOwningCompME)
                    {
                        CaxAsm.SetWorkComponent(kvp.Key);
                        foreach (string i in kvp.Value)
                        {
                            AddEmptyComponent(i);
                        }
                    }
                    foreach (KeyValuePair<NXOpen.Assemblies.Component, List<string>> kvp in DicOwningCompTE)
                    {
                        CaxAsm.SetWorkComponent(kvp.Key);
                        foreach (string i in kvp.Value)
                        {
                            AddEmptyComponent(i);
                        }
                    }
                }
                #endregion
                #region 4.料號一樣，製程版次不一樣，成品、胚料檔名不一樣，客戶版次隨意->更換MOT的版次名稱，成品變更，胚料變更
                else if (New_PartNo.Text.ToUpper() == Old_PartNo.Text.ToUpper() &&
                         Old_OpRev.Text.ToUpper() != New_OpRev.Text.ToUpper() &&
                         Old_PartFile.Text.ToUpper() != Path.GetFileNameWithoutExtension(New_PartFile.Text).ToUpper() &&
                         Old_BilletFile.Text.ToUpper() != Path.GetFileNameWithoutExtension(New_BilletFile.Text).ToUpper())
                {
                    //紀錄OwningComponent
                    Dictionary<NXOpen.Assemblies.Component, List<string>> DicOwningCompME = new Dictionary<NXOpen.Assemblies.Component, List<string>>();
                    Dictionary<NXOpen.Assemblies.Component, List<string>> DicOwningCompTE = new Dictionary<NXOpen.Assemblies.Component, List<string>>();
                    foreach (NXOpen.Assemblies.Component i in ListChildrenComp)
                    {
                        string PartName = "";
                        PartName = i.DisplayName;
                        if (keepMETE.Checked == true)
                        {
                            //料號相同且保留，不做事
                        }
                        else
                        {
                            //料號相同且不保留，須刪除檔案再save，再組空檔案
                            if (PartName.Contains(Old_PartNo.Text + "_TE"))
                            {
                                #region 紀錄TE的OwningComponent，並刪除TE檔案
                                PartName = PartName.Replace(Old_PartNo.Text, New_PartNo.Text);
                                string NewPartFilePath = string.Format(@"{0}\{1}.prt", S_NewPart_Folder, PartName);

                                List<string> listTEName = new List<string>();
                                status = DicOwningCompTE.TryGetValue(i.OwningComponent, out listTEName);
                                if (!status)
                                {
                                    listTEName = new List<string>();
                                }
                                listTEName.Add(NewPartFilePath);
                                DicOwningCompTE[i.OwningComponent] = listTEName;

                                try
                                {
                                    if (File.Exists(((Part)i.Prototype).FullPath))
                                        File.Delete(((Part)i.Prototype).FullPath);
                                }
                                catch (System.Exception ex)
                                {

                                }
                                CaxPublic.DelectObject(i);
                                CaxPart.ClosePartMemory(PartName);

                                //AddEmptyComponent(NewPartFilePath);
                                continue;
                                #endregion
                            }
                            if (PartName.Contains(Old_PartNo.Text + "_ME"))
                            {
                                #region 紀錄ME的OwningComponent，並刪除ME檔案
                                PartName = PartName.Replace(Old_PartNo.Text, New_PartNo.Text);
                                string NewPartFilePath = string.Format(@"{0}\{1}.prt", S_NewPart_Folder, PartName);

                                List<string> listMEName = new List<string>();
                                status = DicOwningCompME.TryGetValue(i.OwningComponent, out listMEName);
                                if (!status)
                                {
                                    listMEName = new List<string>();
                                }
                                listMEName.Add(NewPartFilePath);
                                DicOwningCompME[i.OwningComponent] = listMEName;

                                try
                                {
                                    if (File.Exists(((Part)i.Prototype).FullPath))
                                        File.Delete(((Part)i.Prototype).FullPath);
                                }
                                catch (System.Exception ex)
                                {

                                }

                                CaxPublic.DelectObject(i);
                                CaxPart.ClosePartMemory(PartName);

                                //AddEmptyComponent(NewPartFilePath);
                                continue;
                                #endregion
                            }
                        }
                        if (PartName.ToUpper() == Old_PartFile.Text.ToUpper() && Old_PartFile.Text != Path.GetFileNameWithoutExtension(New_PartFile.Text))
                        {
                            #region 建立新的成品檔案名稱
                            PartName = PartName.Replace(Old_PartFile.Text, New_PartFile.Text);
                            string NewPartFilePath = string.Format(@"{0}\{1}", S_NewPart_Folder, PartName);
                            desPartFilePath = NewPartFilePath;
                            CaxAsm.SetWorkComponent(i.OwningComponent);
                            if (File.Exists(((Part)i.Prototype).FullPath))
                            {
                                File.Delete(((Part)i.Prototype).FullPath);
                            }
                            CaxPublic.DelectObject(i);
                            AddExistComponent(souPartFilePath, NewPartFilePath);
                            continue;
                            #endregion
                        }
                        if (PartName.ToUpper() == Old_BilletFile.Text.ToUpper() && Old_BilletFile.Text != Path.GetFileNameWithoutExtension(New_BilletFile.Text))
                        {
                            #region 料號沒變的話，如果沒選胚料檔要跳出
                            if (New_BilletFile.Text == "")
                            {
                                continue;
                            }
                            #endregion
                            #region 建立新的胚料檔案名稱
                            if (New_BilletFile.Text != "")
                            {
                                PartName = PartName.Replace(Old_BilletFile.Text, New_BilletFile.Text);
                            }
                            else
                            {
                                PartName = New_PartNo.Text + "_Blank.prt";
                            }

                            string NewPartFilePath = string.Format(@"{0}\{1}", S_NewPart_Folder, PartName);
                            desBilletFilePath = NewPartFilePath;
                            CaxAsm.SetWorkComponent(i.OwningComponent);
                            if (File.Exists(((Part)i.Prototype).FullPath))
                            {
                                File.Delete(((Part)i.Prototype).FullPath);
                            }
                            CaxPublic.DelectObject(i);
                            AddBilletFile(souBilletFilePath, NewPartFilePath);
                            continue;
                            #endregion
                        }
                        if (PartName.Contains("_MOT_"))
                        {
                            //建立新的檔案名稱
                            CaxAsm.RenamePart(((Part)displayPart.ComponentAssembly.RootComponent.Prototype), Path.GetFileNameWithoutExtension(AsmCompFileFullPath));
                        }
                    }
                    //開始組裝TE、ME檔案
                    foreach (KeyValuePair<NXOpen.Assemblies.Component, List<string>> kvp in DicOwningCompME)
                    {
                        CaxAsm.SetWorkComponent(kvp.Key);
                        foreach (string i in kvp.Value)
                        {
                            AddEmptyComponent(i);
                        }
                    }
                    foreach (KeyValuePair<NXOpen.Assemblies.Component, List<string>> kvp in DicOwningCompTE)
                    {
                        CaxAsm.SetWorkComponent(kvp.Key);
                        foreach (string i in kvp.Value)
                        {
                            AddEmptyComponent(i);
                        }
                    }
                }
                #endregion
                #region 5.料號不一樣，製程版次一樣，成品、胚料檔名一樣，客戶版次隨意->整套檔名變更，成品不換，胚料不換
                else if (New_PartNo.Text.ToUpper() != Old_PartNo.Text.ToUpper() &&
                         Old_OpRev.Text.ToUpper() == New_OpRev.Text.ToUpper() &&
                         Old_PartFile.Text.ToUpper() == Path.GetFileNameWithoutExtension(New_PartFile.Text).ToUpper() &&
                         Old_BilletFile.Text.ToUpper() == Path.GetFileNameWithoutExtension(New_BilletFile.Text).ToUpper())
                {
                    foreach (NXOpen.Assemblies.Component i in ListChildrenComp)
                    {
                        string PartName = "";
                        PartName = i.DisplayName;
                        if (keepMETE.Checked == true)
                        {
                            //料號不同且保留，改檔名
                            if (PartName.Contains(Old_PartNo.Text + "_TE") || PartName.Contains(Old_PartNo.Text + "_ME"))
                            {
                                #region 建立新的檔案名稱
                                PartName = PartName.Replace(Old_PartNo.Text, New_PartNo.Text);
                                CaxAsm.RenamePart(((Part)i.Prototype), PartName);
                                continue;
                                #endregion
                            }
                        }
                        else
                        {
                            //料號不同且不保留，刪除後再新增空檔案
                            if (PartName.Contains(Old_PartNo.Text + "_TE") || PartName.Contains(Old_PartNo.Text + "_ME"))
                            {
                                #region 建立新的TE、ME檔案名稱
                                PartName = PartName.Replace(Old_PartNo.Text, New_PartNo.Text);
                                string NewPartFilePath = string.Format(@"{0}\{1}.prt", S_NewPart_Folder, PartName);
                                CaxAsm.SetWorkComponent(i.OwningComponent);

                                if (File.Exists(((Part)i.Prototype).FullPath))
                                {
                                    File.Delete(((Part)i.Prototype).FullPath);
                                }
                                CaxPublic.DelectObject(i);
                                AddEmptyComponent(NewPartFilePath);
                                continue;
                                #endregion
                            }
                        }
                        if (PartName.Contains(Old_PartNo.Text) && PartName != Old_PartFile.Text && !PartName.Contains("_MOT_"))
                        {
                            #region 建立新的檔案名稱
                            PartName = PartName.Replace(Old_PartNo.Text, New_PartNo.Text);
                            CaxAsm.RenamePart(((Part)i.Prototype), PartName);
                            continue;
                            #endregion
                        }
                        if (PartName.Contains("_MOT_"))
                        {
                            //建立新的檔案名稱
                            CaxAsm.RenamePart(((Part)displayPart.ComponentAssembly.RootComponent.Prototype), Path.GetFileNameWithoutExtension(AsmCompFileFullPath));
                        }
                    }
                }
                #endregion
                #region 6.料號不一樣，製程版次一樣，成品、胚料檔名不一樣，客戶版次隨意->整套檔名變更，成品變更，胚料變更
                else if (New_PartNo.Text.ToUpper() != Old_PartNo.Text.ToUpper() &&
                         Old_OpRev.Text.ToUpper() == New_OpRev.Text.ToUpper() &&
                         Old_PartFile.Text.ToUpper() != Path.GetFileNameWithoutExtension(New_PartFile.Text).ToUpper() &&
                         Old_BilletFile.Text.ToUpper() != Path.GetFileNameWithoutExtension(New_BilletFile.Text).ToUpper())
                {
                    foreach (NXOpen.Assemblies.Component i in ListChildrenComp)
                    {
                        string PartName = "";
                        PartName = i.DisplayName;
                        if (keepMETE.Checked == true)
                        {
                            //料號不同且保留，改檔名
                            if (PartName.Contains(Old_PartNo.Text + "_TE") || PartName.Contains(Old_PartNo.Text + "_ME"))
                            {
                                #region 建立新的檔案名稱
                                PartName = PartName.Replace(Old_PartNo.Text, New_PartNo.Text);
                                CaxAsm.RenamePart(((Part)i.Prototype), PartName);
                                continue;
                                #endregion
                            }
                        }
                        else
                        {
                            //料號不同且不保留，刪除後再新增空檔案
                            if (PartName.Contains(Old_PartNo.Text + "_TE") || PartName.Contains(Old_PartNo.Text + "_ME"))
                            {
                                #region 建立新的TE、ME檔案名稱
                                PartName = PartName.Replace(Old_PartNo.Text, New_PartNo.Text);
                                string NewPartFilePath = string.Format(@"{0}\{1}.prt", S_NewPart_Folder, PartName);
                                CaxAsm.SetWorkComponent(i.OwningComponent);

                                if (File.Exists(((Part)i.Prototype).FullPath))
                                {
                                    File.Delete(((Part)i.Prototype).FullPath);
                                }
                                CaxPublic.DelectObject(i);
                                AddEmptyComponent(NewPartFilePath);
                                continue;
                                #endregion
                            }
                        }
                        if (PartName.Contains(Old_PartNo.Text + "_TE") || PartName.Contains(Old_PartNo.Text + "_ME"))
                        {
                            #region 建立新的TE、ME檔案名稱
                            PartName = PartName.Replace(Old_PartNo.Text, New_PartNo.Text);
                            string NewPartFilePath = string.Format(@"{0}\{1}.prt", S_NewPart_Folder, PartName);
                            CaxAsm.SetWorkComponent(i.OwningComponent);

                            if (File.Exists(((Part)i.Prototype).FullPath))
                            {
                                File.Delete(((Part)i.Prototype).FullPath);
                            }
                            CaxPublic.DelectObject(i);
                            AddEmptyComponent(NewPartFilePath);
                            continue;
                            #endregion
                        }
                        if (PartName.ToUpper() == Old_PartFile.Text.ToUpper() && Old_PartFile.Text != Path.GetFileNameWithoutExtension(New_PartFile.Text))
                        {
                            #region 建立新的成品檔案名稱
                            PartName = PartName.Replace(Old_PartFile.Text, New_PartFile.Text);
                            string NewPartFilePath = string.Format(@"{0}\{1}", S_NewPart_Folder, PartName);
                            desPartFilePath = NewPartFilePath;
                            CaxAsm.SetWorkComponent(i.OwningComponent);
                            if (File.Exists(((Part)i.Prototype).FullPath))
                            {
                                File.Delete(((Part)i.Prototype).FullPath);
                            }
                            CaxPublic.DelectObject(i);
                            AddExistComponent(souPartFilePath, NewPartFilePath);
                            continue;
                            #endregion
                        }
                        if (PartName.ToUpper() == Old_BilletFile.Text.ToUpper() && Old_BilletFile.Text != Path.GetFileNameWithoutExtension(New_BilletFile.Text))
                        {
                            #region 建立新的胚料檔案名稱
                            if (New_BilletFile.Text != "")
                            {
                                PartName = PartName.Replace(Old_BilletFile.Text, New_BilletFile.Text);
                            }
                            else
                            {
                                PartName = New_PartNo.Text + "_Blank.prt";
                            }

                            string NewPartFilePath = string.Format(@"{0}\{1}", S_NewPart_Folder, PartName);
                            desBilletFilePath = NewPartFilePath;
                            CaxAsm.SetWorkComponent(i.OwningComponent);
                            if (File.Exists(((Part)i.Prototype).FullPath))
                            {
                                File.Delete(((Part)i.Prototype).FullPath);
                            }
                            CaxPublic.DelectObject(i);
                            AddBilletFile(souBilletFilePath, NewPartFilePath);
                            continue;
                            #endregion
                        }
                        if (PartName.Contains(Old_PartNo.Text) && PartName != Old_PartFile.Text && !PartName.Contains("_MOT_"))
                        {
                            #region 建立新的檔案名稱
                            PartName = PartName.Replace(Old_PartNo.Text, New_PartNo.Text);
                            CaxAsm.RenamePart(((Part)i.Prototype), PartName);
                            continue;
                            #endregion
                        }
                        if (PartName.Contains("_MOT_"))
                        {
                            //建立新的檔案名稱
                            CaxAsm.RenamePart(((Part)displayPart.ComponentAssembly.RootComponent.Prototype), Path.GetFileNameWithoutExtension(AsmCompFileFullPath));
                        }
                    }
                }
                #endregion
                #region 7.料號不一樣，製程版次不一樣，成品、胚料檔名一樣，客戶版次隨意->整套檔名變更，成品不換，胚料不換
                else if (New_PartNo.Text.ToUpper() != Old_PartNo.Text.ToUpper() &&
                         Old_OpRev.Text.ToUpper() != New_OpRev.Text.ToUpper() &&
                         Old_PartFile.Text.ToUpper() == Path.GetFileNameWithoutExtension(New_PartFile.Text).ToUpper() &&
                         Old_BilletFile.Text.ToUpper() == Path.GetFileNameWithoutExtension(New_BilletFile.Text).ToUpper())
                {
                    foreach (NXOpen.Assemblies.Component i in ListChildrenComp)
                    {
                        string PartName = "";
                        PartName = i.DisplayName;
                        if (keepMETE.Checked == true)
                        {
                            //料號不同且保留，改檔名
                            if (PartName.Contains(Old_PartNo.Text + "_TE") || PartName.Contains(Old_PartNo.Text + "_ME"))
                            {
                                #region 建立新的檔案名稱
                                PartName = PartName.Replace(Old_PartNo.Text, New_PartNo.Text);
                                CaxAsm.RenamePart(((Part)i.Prototype), PartName);
                                continue;
                                #endregion
                            }
                        }
                        else
                        {
                            //料號不同且不保留，刪除後再新增空檔案
                            if (PartName.Contains(Old_PartNo.Text + "_TE") || PartName.Contains(Old_PartNo.Text + "_ME"))
                            {
                                #region 建立新的TE、ME檔案名稱
                                PartName = PartName.Replace(Old_PartNo.Text, New_PartNo.Text);
                                string NewPartFilePath = string.Format(@"{0}\{1}.prt", S_NewPart_Folder, PartName);
                                CaxAsm.SetWorkComponent(i.OwningComponent);

                                if (File.Exists(((Part)i.Prototype).FullPath))
                                {
                                    File.Delete(((Part)i.Prototype).FullPath);
                                }
                                CaxPublic.DelectObject(i);
                                AddEmptyComponent(NewPartFilePath);
                                continue;
                                #endregion
                            }
                        }
                        if (PartName.Contains(Old_PartNo.Text + "_TE") || PartName.Contains(Old_PartNo.Text + "_ME"))
                        {
                            #region 建立新的TE、ME檔案名稱
                            PartName = PartName.Replace(Old_PartNo.Text, New_PartNo.Text);
                            string NewPartFilePath = string.Format(@"{0}\{1}.prt", S_NewPart_Folder, PartName);
                            CaxAsm.SetWorkComponent(i.OwningComponent);

                            if (File.Exists(((Part)i.Prototype).FullPath))
                            {
                                File.Delete(((Part)i.Prototype).FullPath);
                            }
                            CaxPublic.DelectObject(i);
                            AddEmptyComponent(NewPartFilePath);
                            continue;
                            #endregion
                        }
                        if (PartName.Contains(Old_PartNo.Text) && PartName != Old_PartFile.Text & !PartName.Contains("_MOT_"))
                        {
                            #region 建立新的檔案名稱
                            PartName = PartName.Replace(Old_PartNo.Text, New_PartNo.Text);
                            CaxAsm.RenamePart(((Part)i.Prototype), PartName);
                            continue;
                            #endregion
                        }
                        if (PartName.Contains("_MOT_"))
                        {
                            //建立新的檔案名稱
                            CaxAsm.RenamePart(((Part)displayPart.ComponentAssembly.RootComponent.Prototype), Path.GetFileNameWithoutExtension(AsmCompFileFullPath));
                        }
                    }
                }
                #endregion
                #region 8.料號不一樣，製程版次不一樣，成品、胚料檔名不一樣，客戶版次隨意->整套檔名變更，成品變更，胚料變更
                else if (New_PartNo.Text.ToUpper() != Old_PartNo.Text.ToUpper() &&
                         Old_OpRev.Text.ToUpper() != New_OpRev.Text.ToUpper() &&
                         Old_PartFile.Text.ToUpper() != Path.GetFileNameWithoutExtension(New_PartFile.Text).ToUpper() &&
                         Old_BilletFile.Text.ToUpper() != Path.GetFileNameWithoutExtension(New_BilletFile.Text).ToUpper())
                {
                    foreach (NXOpen.Assemblies.Component i in ListChildrenComp)
                    {
                        string PartName = "";
                        PartName = i.DisplayName;
                        if (keepMETE.Checked == true)
                        {
                            //料號不同且保留，改檔名
                            if (PartName.Contains(Old_PartNo.Text + "_TE") || PartName.Contains(Old_PartNo.Text + "_ME"))
                            {
                                #region 建立新的檔案名稱
                                PartName = PartName.Replace(Old_PartNo.Text, New_PartNo.Text);
                                CaxAsm.RenamePart(((Part)i.Prototype), PartName);
                                continue;
                                #endregion
                            }
                        }
                        else
                        {
                            //料號不同且不保留，刪除後再新增空檔案
                            if (PartName.Contains(Old_PartNo.Text + "_TE") || PartName.Contains(Old_PartNo.Text + "_ME"))
                            {
                                #region 建立新的TE、ME檔案名稱
                                PartName = PartName.Replace(Old_PartNo.Text, New_PartNo.Text);
                                string NewPartFilePath = string.Format(@"{0}\{1}.prt", S_NewPart_Folder, PartName);
                                CaxAsm.SetWorkComponent(i.OwningComponent);

                                if (File.Exists(((Part)i.Prototype).FullPath))
                                {
                                    File.Delete(((Part)i.Prototype).FullPath);
                                }
                                CaxPublic.DelectObject(i);
                                AddEmptyComponent(NewPartFilePath);
                                continue;
                                #endregion
                            }
                        }
                        if (PartName.Contains(Old_PartNo.Text + "_TE") || PartName.Contains(Old_PartNo.Text + "_ME"))
                        {
                            #region 建立新的TE、ME檔案名稱
                            PartName = PartName.Replace(Old_PartNo.Text, New_PartNo.Text);
                            string NewPartFilePath = string.Format(@"{0}\{1}.prt", S_NewPart_Folder, PartName);
                            CaxAsm.SetWorkComponent(i.OwningComponent);

                            if (File.Exists(((Part)i.Prototype).FullPath))
                            {
                                File.Delete(((Part)i.Prototype).FullPath);
                            }
                            CaxPublic.DelectObject(i);
                            AddEmptyComponent(NewPartFilePath);
                            continue;
                            #endregion
                        }
                        if (PartName.ToUpper() == Old_PartFile.Text.ToUpper() && Old_PartFile.Text != Path.GetFileNameWithoutExtension(New_PartFile.Text))
                        {
                            #region 建立新的成品檔案名稱
                            PartName = PartName.Replace(Old_PartFile.Text, New_PartFile.Text);
                            string NewPartFilePath = string.Format(@"{0}\{1}", S_NewPart_Folder, PartName);
                            desPartFilePath = NewPartFilePath;
                            CaxAsm.SetWorkComponent(i.OwningComponent);
                            if (File.Exists(((Part)i.Prototype).FullPath))
                            {
                                File.Delete(((Part)i.Prototype).FullPath);
                            }
                            CaxPublic.DelectObject(i);
                            AddExistComponent(souPartFilePath, NewPartFilePath);
                            continue;
                            #endregion
                        }
                        if (PartName.ToUpper() == Old_BilletFile.Text.ToUpper() && Old_BilletFile.Text != Path.GetFileNameWithoutExtension(New_BilletFile.Text))
                        {
                            #region 建立新的胚料檔案名稱
                            if (New_BilletFile.Text != "")
                            {
                                PartName = PartName.Replace(Old_BilletFile.Text, New_BilletFile.Text);
                            }
                            else
                            {
                                PartName = New_PartNo.Text + "_Blank.prt";
                            }

                            string NewPartFilePath = string.Format(@"{0}\{1}", S_NewPart_Folder, PartName);
                            desBilletFilePath = NewPartFilePath;
                            CaxAsm.SetWorkComponent(i.OwningComponent);
                            if (File.Exists(((Part)i.Prototype).FullPath))
                            {
                                File.Delete(((Part)i.Prototype).FullPath);
                            }
                            CaxPublic.DelectObject(i);
                            AddBilletFile(souBilletFilePath, NewPartFilePath);
                            continue;
                            #endregion
                        }
                        if (PartName.Contains(Old_PartNo.Text) && PartName != Old_PartFile.Text & !PartName.Contains("_MOT_"))
                        {
                            #region 建立新的檔案名稱
                            PartName = PartName.Replace(Old_PartNo.Text, New_PartNo.Text);
                            CaxAsm.RenamePart(((Part)i.Prototype), PartName);
                            continue;
                            #endregion
                        }
                        if (PartName.Contains("_MOT_"))
                        {
                            //建立新的檔案名稱
                            CaxAsm.RenamePart(((Part)displayPart.ComponentAssembly.RootComponent.Prototype), Path.GetFileNameWithoutExtension(AsmCompFileFullPath));
                        }
                    }
                }
                #endregion

            SaveMOT:
                //CaxLog.ShowListingWindow("3");
                CaxPart.Save();
                CaxPart.CloseAllParts();
                //CaxLog.ShowListingWindow("4");
                if (!CaxPart.OpenBaseDisplay(AsmCompFileFullPath, out newAsmPart))
                {
                    CaxLog.ShowListingWindow("MOT開啟失敗！");
                    return;
                }
                //CaxLog.ShowListingWindow("5");
                #region 建立下載的txt檔案放到OPXXX資料夾內
                //先取得所有二階檔案
                List<NXOpen.Assemblies.Component> secondChildenComp = new List<NXOpen.Assemblies.Component>();
                CaxAsm.GetCompChildren(out secondChildenComp);

                foreach (NXOpen.Assemblies.Component i in secondChildenComp)
                {
                    //取得抓到的製程序
                    string CurrentOIS = i.DisplayName.Split(new string[] { "_OP" }, StringSplitOptions.RemoveEmptyEntries)[1];
                    //由二階檔案取得三階檔案
                    List<NXOpen.Assemblies.Component> thirdChildenComp = new List<NXOpen.Assemblies.Component>();
                    CaxAsm.GetCompChildren(out thirdChildenComp, i);
                    //建立三階與四階的檔案txt，方便ME、TE下載時可讀取要下載哪些檔案
                    List<string> listMEFileName = new List<string>();
                    List<string> listTEFileName = new List<string>();
                    foreach (NXOpen.Assemblies.Component j in thirdChildenComp)
                    {
                        if (j.DisplayName.Contains("_OIS"))
                        {
                            //加入三階OIS檔案
                            listMEFileName.Add(j.DisplayName + ".prt");
                            //由三階檔案取得四階檔案
                            List<NXOpen.Assemblies.Component> fourthChildenComp = new List<NXOpen.Assemblies.Component>();
                            CaxAsm.GetCompChildren(out fourthChildenComp, j);
                            foreach (NXOpen.Assemblies.Component k in fourthChildenComp)
                            {
                                //加入四階檔案
                                listMEFileName.Add(k.DisplayName + ".prt");
                            }
                        }
                        else
                        {
                            //加入三階CAM檔案
                            listTEFileName.Add(j.DisplayName + ".prt");
                            //由三階檔案取得四階檔案
                            List<NXOpen.Assemblies.Component> fourthChildenComp = new List<NXOpen.Assemblies.Component>();
                            CaxAsm.GetCompChildren(out fourthChildenComp, j);
                            foreach (NXOpen.Assemblies.Component k in fourthChildenComp)
                            {
                                //加入四階檔案
                                listTEFileName.Add(k.DisplayName + ".prt");
                            }
                        }

                        #region 將三階與四階的檔案txt輸出至每個OP的資料夾內，OIS放在OIS資料夾內；CAM放在CAM資料夾內
                        OISFolderPath = string.Format(@"{0}\{1}", Path.GetDirectoryName(AsmCompFileFullPath), "OP" + CurrentOIS);
                        string[] OISFileNameTxt = listMEFileName.ToArray();
                        System.IO.File.WriteAllLines(string.Format(@"{0}\{1}", OISFolderPath, "PartNameText_OIS.txt"), OISFileNameTxt);

                        CAMFolderPath = string.Format(@"{0}\{1}", Path.GetDirectoryName(AsmCompFileFullPath), "OP" + CurrentOIS);
                        string[] CAMFileNameTxt = listTEFileName.ToArray();
                        System.IO.File.WriteAllLines(string.Format(@"{0}\{1}", CAMFolderPath, "PartNameText_CAM.txt"), CAMFileNameTxt);
                        #endregion

                    }
                }
                #endregion
                //CaxLog.ShowListingWindow("6");
                #region 塞ERP屬性、品名、材質
                CaxAsm.SetWorkComponent(null);
                Part rootPart = theSession.Parts.Work;
                List<NXOpen.Assemblies.Component> allComp = new List<NXOpen.Assemblies.Component>();
                CaxAsm.GetCompChildren(rootPart.ComponentAssembly.RootComponent, ref allComp);
                PartLoadStatus partLoadStatus1;
                foreach (NXOpen.Assemblies.Component i in allComp)
                {
                    foreach (Operation j in cPECreateData.listOperation)
                    {
                        if (i.DisplayName.Contains("_OIS" + j.Oper1))
                        {
                            Part MakeDisplayPart = (Part)i.Prototype;
                            theSession.Parts.SetDisplay(MakeDisplayPart, true, true, out partLoadStatus1);
                            string excelType = "";//將EXCELTYPE抓出來後重新填回檔案中，如此ME即可不用修改直接上傳
                            try
                            {
                                excelType = MakeDisplayPart.GetStringAttribute("ExcelType");
                            }
                            catch (System.Exception ex)
                            {
                                excelType = "";
                            }
                            string RevStartPos = "";//將【製圖版次】抓出來後重新填回檔案中，如此ME重新出PartInformation時才不會被刪除
                            try
                            {
                                RevStartPos = MakeDisplayPart.GetStringAttribute(CaxME.TablePosi.RevStartPos);
                            }
                            catch (System.Exception ex)
                            {
                                RevStartPos = "";
                            }
                            string RevDateStartPos = "";//將【製圖日期】抓出來後重新填回檔案中，如此ME重新出PartInformation時才不會被刪除
                            try
                            {
                                RevDateStartPos = MakeDisplayPart.GetStringAttribute(CaxME.TablePosi.RevDateStartPos);
                            }
                            catch (System.Exception ex)
                            {
                                RevDateStartPos = "";
                            }
                            string InstructionPos = "";//將【製圖說明】抓出來後重新填回檔案中，如此ME重新出PartInformation時才不會被刪除
                            try
                            {
                                InstructionPos = MakeDisplayPart.GetStringAttribute(CaxME.TablePosi.InstructionPos);
                            }
                            catch (System.Exception ex)
                            {
                                InstructionPos = "";
                            }
                            string InstApprovedPos = "";//將【製圖審核人員】抓出來後重新填回檔案中，如此ME重新出PartInformation時才不會被刪除
                            try
                            {
                                InstApprovedPos = MakeDisplayPart.GetStringAttribute(CaxME.TablePosi.InstApprovedPos);
                            }
                            catch (System.Exception ex)
                            {
                                InstApprovedPos = "";
                            }
                            string RevCount = "";//將【RevCount】抓出來後重新填回檔案中，如此ME重新出PartInformation時才不會被刪除
                            try
                            {
                                RevCount = MakeDisplayPart.GetStringAttribute("RevCount");
                            }
                            catch (System.Exception ex)
                            {
                                RevCount = "";
                            }

                            MakeDisplayPart.DeleteAllAttributesByType(NXObject.AttributeType.Any);
                            MakeDisplayPart.SetAttribute("ERPCODE", j.ERPCode);
                            MakeDisplayPart.SetAttribute(CaxME.TablePosi.PartDescriptionPos, New_PartDesc.Text);
                            MakeDisplayPart.SetAttribute(CaxME.TablePosi.MaterialPos, New_Material.Text);
                            MakeDisplayPart.SetAttribute("IsFamilyPart", "IsFamilyPart");
                            MakeDisplayPart.SetAttribute("ExcelType", excelType);
                            MakeDisplayPart.SetAttribute(CaxME.TablePosi.RevStartPos, RevStartPos);
                            MakeDisplayPart.SetAttribute(CaxME.TablePosi.RevDateStartPos, RevDateStartPos);
                            MakeDisplayPart.SetAttribute(CaxME.TablePosi.InstructionPos, InstructionPos);
                            MakeDisplayPart.SetAttribute(CaxME.TablePosi.InstApprovedPos, InstApprovedPos);
                            MakeDisplayPart.SetAttribute("RevCount", RevCount);
                        }
                    }
                }

                //返回MOT架構
                theSession.Parts.SetDisplay(rootPart, true, true, out partLoadStatus1);
                partLoadStatus1.Dispose();
                #endregion
                //CaxLog.ShowListingWindow("7");
                #region 寫出PECreateData.dat
                string PECreateDataJsonDat = string.Format(@"{0}\{1}", ModelFolderFullPath, "PECreateData.dat");
                status = CaxFile.WriteJsonFileData(PECreateDataJsonDat, cPECreateData);
                if (!status)
                {
                    MessageBox.Show("PECreateData.dat 輸出失敗...");
                    return;
                }
                #endregion
      
                using (ISession session = MyHibernateHelper.SessionFactory.OpenSession())
                {
                    #region 插入Com_PEMain
                    cCom_PEMain.partName = PartNo;
                    cCom_PEMain.partDes = PartDesc;
                    cCom_PEMain.partMaterial = PartMaterial;
                    cCom_PEMain.customerVer = CusRev;
                    cCom_PEMain.opVer = OpRev;
                    //插入材料來源(自備胚=1，管材/鍛件=A)
                    if (New_CompBillet.Checked)
                    {
                        cCom_PEMain.materialSource = "1";
                    }
                    if (New_CusBillet.Checked)
                    {
                        cCom_PEMain.materialSource = "A";
                    }
                    //插入ERPStd
                    if (New_ERP.Text != "")
                    {
                        cCom_PEMain.eRPStd = New_ERP.Text;
                    }
                    //插入成品檔路徑
                    cCom_PEMain.partFilePath = string.Format(@"{0}\{1}", S_NewPart_Folder, New_PartFile.Text);
                    //插入胚料檔路徑
                    if (New_BilletFile.Text != "")
                    {
                        cCom_PEMain.billetFilePath = string.Format(@"{0}\{1}", S_NewPart_Folder, New_BilletFile.Text);
                    }
                    

                    cCom_PEMain.createDate = DateTime.Now.ToString();

                    IList<Com_PartOperation> updateData = new List<Com_PartOperation>();
                    Com_PartOperation cCom_PartOperation = new Com_PartOperation();
                    foreach (Com_PartOperation i in listComPartOperation)
                    {
                        cCom_PartOperation = new Com_PartOperation();
                        cCom_PartOperation.operation1 = i.operation1;
                        cCom_PartOperation.sysOperation2 = i.sysOperation2;
                        cCom_PartOperation.erpCode = GetERPcode(i.operation1);

                        cCom_PartOperation.comPEMain = cCom_PEMain;
                        updateData.Add(cCom_PartOperation);
                    }

                    cCom_PEMain.comPartOperation = updateData;

                    using (ITransaction trans = session.BeginTransaction())
                    {
                        //session.Save(cCom_PartOperation);
                        session.Save(cCom_PEMain);

                        trans.Commit();
                    }

                    session.Close();
                    #endregion
                }
                //CaxLog.ShowListingWindow("8");

                CaxAsm.SetWorkComponent(null);

                CaxPart.Save();
                //CaxLog.ShowListingWindow("9");
                this.Close();

                #region (註解)建立一階總組立檔案
                /*
                status = CaxAsm.CreateNewAsm(AsmCompFileFullPath);
                if (!status)
                {
                    CaxLog.ShowListingWindow("建立一階總組立檔失敗");
                    return;
                }
                CaxPart.Save();
                */
                #endregion

                #region (註解)建立二階檔案
                /*
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
                */
                #endregion

                #region (註解)先取得舊料號中上傳的檔案資訊(PartNameText_OIS.txt、PartNameText_CAM.txt)

                #endregion



            }
            catch (System.Exception ex)
            {
                MessageBox.Show(ex.ToString());
            }
        }

        private bool DataCompleted()
        {
            try
            {
                
                #region 資料比對---料號、客戶版次、製程版次不能相同
                //檢查資料庫，判斷是否有相同的客戶、料號、客戶版次、製程版次
                Sys_Customer customer = session.QueryOver<Sys_Customer>().Where(y => y.customerName == New_Cus.Text).SingleOrDefault<Sys_Customer>();
                Com_PEMain PEMain = session.QueryOver<Com_PEMain>()
                    .Where(x => x.sysCustomer == customer)
                    .And(x => x.partName == New_PartNo.Text.ToUpper())
                    .And(x => x.customerVer == New_CusRev.Text.ToUpper())
                    .And(x => x.opVer == New_OpRev.Text.ToUpper()).SingleOrDefault<Com_PEMain>();
                if (PEMain != null)
                {
                    MessageBox.Show("此【料號】、【客戶版次】、【製程版次】已存在，無法再次建立！");
                    return false;
                }
                //if (New_PartNo.Text.ToUpper() == Old_PartNo.Text.ToUpper() &
                //    New_CusRev.Text.ToUpper() == Old_CusRev.Text.ToUpper() &
                //    New_OpRev.Text.ToUpper() == Old_OpRev.Text.ToUpper())
                //{
                //    MessageBox.Show("【料號、客戶版次、製程版次】不可全部相同");
                //    return false;
                //}
                #endregion

                #region 新料號---取得客戶名稱
                CusName = New_Cus.Text;
                if (CusName == "")
                {
                    MessageBox.Show("尚未填寫客戶！");
                    return false;
                }
                #endregion

                #region 新料號---取得料號
                PartNo = New_PartNo.Text;
                if (PartNo == "")
                {
                    MessageBox.Show("尚未填寫料號！");
                    return false;
                }
                #endregion

                #region 新料號---取得客戶版次
                CusRev = New_CusRev.Text;
                if (CusRev == "")
                {
                    MessageBox.Show("尚未填寫客戶版次！");
                    return false;
                }
                #endregion

                #region 新料號---取得製程版次
                OpRev = New_OpRev.Text;
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
                PartDesc = New_PartDesc.Text;
                if (PartDesc == "")
                {
                    MessageBox.Show("尚未填寫品名！");
                    return false;
                }
                #endregion

                #region 新料號---取得材質
                PartMaterial = New_Material.Text;
                if (PartMaterial == "")
                {
                    MessageBox.Show("尚未填寫材質！");
                    return false;
                }
                #endregion

                #region 新料號---取得ERP
                ERPCode = New_ERP.Text;
                if (ERPCode == "")
                {
                    MessageBox.Show("尚未填寫ERP！");
                    return false;
                }
                #endregion

                #region 新料號---取得材料來源
                if (New_CompBillet.Checked == false & New_CusBillet.Checked == false)
                {
                    MessageBox.Show("尚未選擇材料來源！");
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

        private string GetERPcode(string i)
        {
            string ERP = "";
            string Billet = "";
            try
            {
                if (New_ERP.Text == "")
                    return ERP = "";

                if (New_CompBillet.Checked == true)
                    Billet = "1";

                if (New_CusBillet.Checked == true)
                    Billet = "A";

                if (i == "001")
                    ERP = string.Format("{0}{1}-{2}-{3}", "R", Billet, New_ERP.Text, New_CusRev.Text.ToUpper());

                else if (i == "002")
                    return ERP = "";

                else if (i == "003")
                    return ERP = "";

                else if (i == "004")
                    return ERP = "";

                else
                    ERP = string.Format("{0}{1}-{2}-{3}", "W", Billet, New_ERP.Text, i);

            }
            catch (System.Exception ex)
            {
                return ERP = "";
            }
            return ERP;
        }

        private bool AddEmptyComponent(string PartFilePath)
        {
            try
            {
                NXOpen.Assemblies.Component newComponent = null;
                if (System.IO.File.Exists(PartFilePath))
                {
                    status = CaxAsm.AddComponentToAsmByDefault(PartFilePath, out newComponent);
                    if (!status)
                    {
                        MessageBox.Show("組裝已存在的ME檔案失敗！");
                        return false;
                    }
                }
                else
                {
                    status = CaxAsm.CreateNewEmptyCompNoReturn(PartFilePath);
                    if (!status)
                    {
                        MessageBox.Show("新建ME檔案失敗！");
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

        private bool AddExistComponent(string souPartFilePath, string desPartFilePath)
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
                //if (!status)
                //{
                //    MessageBox.Show("成品檔案組裝失敗！");
                //    return false;
                //}
            }
            catch (System.Exception ex)
            {
                MessageBox.Show(ex.ToString());
                return false;
            }
            return true;
        }
        
        private bool AddBilletFile(string souBilletFilePath, string desBilletFilePath)
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

                    //desBilletFilePath = string.Format(@"{0}\{1}\{2}\{3}\{4}\{5}", CaxEnv.GetGlobaltekTaskDir()
                    //    , CusName, PartNo, CusRev.ToUpper(), OpRev.ToUpper(), Path.GetFileName(souBilletFilePath));

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
                    //desBilletFilePath = string.Format(@"{0}\{1}\{2}\{3}\{4}\{5}", CaxEnv.GetGlobaltekTaskDir()
                    //    , CusName, PartNo, CusRev.ToUpper(), OpRev.ToUpper(), PartNo + "_Blank.prt");
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

        private void DeletePartFile_Click(object sender, EventArgs e)
        {
            New_PartFile.Text = "";
            souPartFilePath = "-1";
            CaxPart.CloseAllParts();
            PartFileSame.Checked = false;
        }

        private void DeleteBilletFile_Click(object sender, EventArgs e)
        {
            New_BilletFile.Text = "";
            souBilletFilePath = "";
            BilletFileSame.Checked = false;
        }

        private void New_Cus_SelectedIndexChanged(object sender, EventArgs e)
        {
            cCom_PEMain.sysCustomer = ((Sys_Customer)New_Cus.SelectedItem);
        }

        private void PartFileSame_CheckedChanged(object sender, EventArgs e)
        {
            if (PartFileSame.Checked == true)
            {
                CaxPart.CloseAllParts();
                New_PartFile.Text = Old_PartFile.Text + ".prt";
                souPartFilePath = oldPEMain.partFilePath;
                //開啟選擇的檔案
                CaxPart.OpenBaseDisplay(oldPEMain.partFilePath);
            }
            else
            {
                New_PartFile.Text = "";
            }
        }

        private void BilletFileSame_CheckedChanged(object sender, EventArgs e)
        {
            if (BilletFileSame.Checked == true)
            {
                New_BilletFile.Text = Old_BilletFile.Text + ".prt";
                souBilletFilePath = oldPEMain.billetFilePath;
            }
            else
            {
                New_BilletFile.Text = "";
            }
        }

    }
}
