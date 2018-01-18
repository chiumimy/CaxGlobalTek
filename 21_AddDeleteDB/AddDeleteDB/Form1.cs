using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Windows.Forms;
using DevComponents.DotNetBar;
using CaxGlobaltek;
using NHibernate;
using DevComponents.AdvTree;
using System.IO;
using DevComponents.DotNetBar.SuperGrid;

namespace AddDeleteDB
{
    public partial class Form1 : Form
    {
        private bool status;
        private static ISession session = MyHibernateHelper.SessionFactory.OpenSession();
        private List<string> listCustomerName = new List<string>();
        private List<string> listOperation2 = new List<string>();
        private List<string> listMaterial = new List<string>();
        private List<string> listProduct = new List<string>();
        private List<InstrumentData> listInstrument = new List<InstrumentData>();
        private Dictionary<string, List<MachineNoData>> DicMachine = new Dictionary<string, List<MachineNoData>>();
        private Dictionary<string, List<string>> DicOperation2 = new Dictionary<string, List<string>>();
        private Dictionary<string, List<string>> DicMaterial = new Dictionary<string, List<string>>();
        private List<string> listSelItems = new List<string>();
        private List<string> listSelInstItems = new List<string>();
        private List<string> listSelMachineItems = new List<string>();
        private List<string> listSelOp2Items = new List<string>();
        private List<string> listSelMaterialItems = new List<string>();
        private List<RenewERP.PartData> listPartData = new List<RenewERP.PartData>();
        private Com_PEMain ComPEMain = new Com_PEMain();
        private string TaskPath = "";
        public struct MachineNoData
        {
            public string MachineNo;
            public string MachineID;
            public string MachineName;
        }
        public struct InstrumentData
        {
            public string instrumentColor;
            public string instrumentName;
            public string instrumentEngName;
        }
        private SuperTabStripSelectedTabChangedEventArgs CurrentE;
        public Form1()
        {
            InitializeComponent();
        }

        private void Form1_Load(object sender, EventArgs e)
        {
            SelNum.Text = "0";
            SelMachineNum.Text = "0";
            SelInstNum.Text = "0";

            #region (註解)新增製程別
            /*
            SuperTabItem newOne = new DevComponents.DotNetBar.SuperTabItem();
            newOne.Text = "製程別";
            //newOne.Image = this.imageList1.Images[6];
            newOne.Image = Properties.Resources.file_24px;
            newOne.AttachedControl = STC_Panel;
            newOne.ImagePadding = superTabItem1.ImagePadding;
            newOne.ImageAlignment = superTabItem1.ImageAlignment;
            newOne.FixedTabSize = superTabItem1.FixedTabSize;
            newOne.TextAlignment = superTabItem1.TextAlignment;

            TabControl.Tabs.Add(newOne);
            */
            #endregion

            #region (註解)新增材質
            /*
            newOne = new DevComponents.DotNetBar.SuperTabItem();
            newOne.Text = "材質";
            //newOne.Image = this.imageList1.Images[6];
            newOne.Image = Properties.Resources.material_24px;
            newOne.AttachedControl = STC_Panel;
            newOne.ImagePadding = superTabItem1.ImagePadding;
            newOne.ImageAlignment = superTabItem1.ImageAlignment;
            newOne.FixedTabSize = superTabItem1.FixedTabSize;
            newOne.TextAlignment = superTabItem1.TextAlignment;

            TabControl.Tabs.Add(newOne);
            */
            #endregion

            if (TabControl.SelectedTab.Text == "客戶")
            {
                listCustomerName = new List<string>();
                status = Customer.GetCustomerData(out listCustomerName);
                if (!status)
                {
                    MessageBox.Show("取得目前客戶名稱資料失敗");
                    return;
                }
                for (int i = 0; i < listCustomerName.Count; i++)
                {
                    ListViewItem tempViewItem = new ListViewItem(listCustomerName[i]);
                    listView1.Items.Add(tempViewItem);
                }
            }
            
        }

        private void TabControl_SelectedTabChanged(object sender, SuperTabStripSelectedTabChangedEventArgs e)
        {
            session = MyHibernateHelper.SessionFactory.OpenSession();
            try
            {
                //客戶
                listView1.Items.Clear();
                SelNum.Text = "0";
                AddText.Text = "";

                //機台
                MachineTree.Nodes.Clear();
                MachineType.Text = "";
                MachineNo.Text = "";
                MachineName.Text = "";
                MachineID.Text = "";
                SelMachineNum.Text = "0";

                //量具
                listView2.Items.Clear();
                instColor.Text = "";
                instName.Text = "";
                instEngName.Text = "";
                SelInstNum.Text = "0";

                //製程別
                Op2Tree.Nodes.Clear();
                Op2_Category.Items.Clear();
                SelOp2Num.Text = "0";
                Op2Text.Text = "";

                //材質
                MaterialTree.Nodes.Clear();
                Material_Category.Items.Clear();
                SelMaterialNum.Text = "0";
                MaterialText.Text = "";

                //尺寸類型
                listView3.Items.Clear();
                SelProductNum.Text = "0";
                AddProduct.Text = "";

                //修改ERP
                SGC_RenewERP.PrimaryGrid.Rows.Clear();
                Renew_CusRev.Items.Clear();
                Renew_OpRev.Items.Clear();

                if (TabControl.SelectedTab.Text == "客戶")
                {
                    #region 取得客戶名稱
                    listCustomerName = new List<string>();
                    status = Customer.GetCustomerData(out listCustomerName);
                    if (!status)
                    {
                        MessageBox.Show("取得客戶名稱資料失敗");
                        return;
                    }
                    for (int i = 0; i < listCustomerName.Count; i++)
                    {
                        ListViewItem tempViewItem = new ListViewItem(listCustomerName[i]);
                        listView1.Items.Add(tempViewItem);
                    }
                    #endregion
                }
                else if (TabControl.SelectedTab.Text == "製程別")
                {
                    #region 取得製程別
                    DicOperation2 = new Dictionary<string, List<string>>();
                    Op2Tree.BeginUpdate();
                    status = Operation2.GetOperation2Data(out DicOperation2);
                    if (!status)
                    {
                        MessageBox.Show("取得製程別資料失敗");
                        return;
                    }
                    if (DicOperation2.Count == 0)
                    {
                        return;
                    }
                    Op2_Category.Items.AddRange(DicOperation2.Keys.ToArray());

                    status = CommonFun.RenewAdvTree(DicOperation2, Op2Tree);
                    if (!status)
                    {
                        MessageBox.Show("填寫製程別資料失敗");
                        return;
                    }

                    Op2Tree.EndUpdate();
                    #endregion
                }
                else if (TabControl.SelectedTab.Text == "材質")
                {
                    #region 取得材質
                    DicMaterial = new Dictionary<string, List<string>>();
                    MaterialTree.BeginUpdate();
                    status = Material.GetMaterialData(out DicMaterial);
                    if (!status)
                    {
                        MessageBox.Show("取得材質資料失敗");
                        return;
                    }
                    if (DicMaterial.Count == 0)
                    {
                        return;
                    }
                    Material_Category.Items.AddRange(DicMaterial.Keys.ToArray());

                    status = CommonFun.RenewAdvTree(DicMaterial, MaterialTree);
                    if (!status)
                    {
                        MessageBox.Show("填寫材質資料失敗");
                        return;
                    }

                    MaterialTree.EndUpdate();
                    #endregion
                }
                else if (TabControl.SelectedTab.Text == "量具")
                {
                    #region 取得量具
                    listInstrument = new List<InstrumentData>();
                    status = Instrument.GetInstrumentData(out listInstrument);
                    if (!status)
                    {
                        MessageBox.Show("取得量具資料失敗");
                        return;
                    }
                    for (int i = 0; i < listInstrument.Count; i++)
                    {
                        ListViewItem tempViewItem = new ListViewItem(listInstrument[i].instrumentName + "，" + listInstrument[i].instrumentEngName);
                        listView2.Items.Add(tempViewItem);
                    }
                    #endregion
                }
                else if (TabControl.SelectedTab.Text == "機台")
                {
                    #region 取得機台型號
                    DicMachine = new Dictionary<string, List<MachineNoData>>();
                    MachineTree.BeginUpdate();
                    status = Machine.GetMachineData(out DicMachine);
                    if (!status)
                    {
                        MessageBox.Show("取得機台型號資料失敗");
                        return;
                    }
                    foreach (KeyValuePair<string, List<MachineNoData>> kvp in DicMachine)
                    {
                        Node node1 = new Node();
                        node1.Text = kvp.Key;
                        node1.ExpandVisibility = eNodeExpandVisibility.Visible;
                        foreach (MachineNoData i in kvp.Value)
                        {
                            Node node2 = new Node();
                            node2.Text = i.MachineNo;
                            node2.CheckBoxVisible = true;
                            Cell cell1 = new Cell();
                            cell1.Text = i.MachineID;
                            Cell cell2 = new Cell();
                            cell2.Text = i.MachineName;
                            node2.Cells.Add(cell1);
                            node2.Cells.Add(cell2);
                            node1.Nodes.Add(node2);
                        }
                        MachineTree.Nodes.Add(node1);
                    }
                    MachineTree.EndUpdate();
                    #endregion
                }
                else if (TabControl.SelectedTab.Text == "尺寸類型")
                {
                    #region 取得尺寸類型
                    listProduct = new List<string>();
                    status = Product.GetProductData(out listProduct);
                    if (!status)
                    {
                        MessageBox.Show("取得尺寸類型資料失敗");
                        return;
                    }
                    for (int i = 0; i < listProduct.Count; i++)
                    {
                        ListViewItem tempViewItem = new ListViewItem(listProduct[i]);
                        listView3.Items.Add(tempViewItem);
                    }
                    #endregion
                }
                else if (TabControl.SelectedTab.Text == "修改ERP")
                {
                    #region 取得料號資訊
                    listPartData = new List<RenewERP.PartData>();
                    status = RenewERP.GetPartData(out listPartData);
                    if (!status)
                    {
                        MessageBox.Show("取得料號資訊失敗");
                        return;
                    }
                    #endregion
                }
                //儲存目前操作的對象
                CurrentE = e;
            }
            catch (System.Exception ex)
            {
                MessageBox.Show(ex.ToString());
            }
        }

        #region 客戶
        private void listView1_MouseUp(object sender, MouseEventArgs e)
        {
            int selCount = 0;
            listSelItems = new List<string>();
            foreach (ListViewItem tmpLstView in listView1.Items)
            {
                if (tmpLstView.Selected == true)
                {
                    selCount++;
                    listSelItems.Add(tmpLstView.Text);
                }
            }
            SelNum.Text = selCount.ToString();
        }
        private void AddButton_Click(object sender, EventArgs e)
        {
            try
            {
                if (TabControl.SelectedTab.Text == "客戶")
                {
                    status = CommonFun.CheckData(AddText.Text, listCustomerName);
                    if (!status)
                    {
                        AddText.Text = "";
                        return;
                    }

                    Sys_Customer sysCustomer = new Sys_Customer();
                    sysCustomer.customerName = AddText.Text;
                    session.Save(sysCustomer);
                    ITransaction trans = session.BeginTransaction();
                    trans.Commit();

                    //將目前操作對象重新更新列表
                    TabControl_SelectedTabChanged(sender, CurrentE);
                }
                else if (TabControl.SelectedTab.Text == "製程別")
                {
                    status = CommonFun.CheckData(AddText.Text, listOperation2);
                    if (!status)
                    {
                        AddText.Text = "";
                        return;
                    }

                    Sys_Operation2 sysOperation2 = new Sys_Operation2();
                    sysOperation2.operation2Name = AddText.Text;
                    session.Save(sysOperation2);
                    ITransaction trans = session.BeginTransaction();
                    trans.Commit();

                    //將目前操作對象重新更新列表
                    TabControl_SelectedTabChanged(sender, CurrentE);
                }
                else if (TabControl.SelectedTab.Text == "材質")
                {
                    status = CommonFun.CheckData(AddText.Text, listMaterial);
                    if (!status)
                    {
                        AddText.Text = "";
                        return;
                    }

                    Sys_Material sysMaterial = new Sys_Material();
                    sysMaterial.materialName = AddText.Text;
                    session.Save(sysMaterial);
                    ITransaction trans = session.BeginTransaction();
                    trans.Commit();

                    //將目前操作對象重新更新列表
                    TabControl_SelectedTabChanged(sender, CurrentE);

                }
                AddText.Text = "";
            }
            catch (System.Exception ex)
            {
                MessageBox.Show(ex.ToString());
            }
        }
        private void DelButton_Click(object sender, EventArgs e)
        {
            try
            {
                if (TabControl.SelectedTab.Text == "客戶")
                {
                    #region 客戶
                    IList<Sys_Customer> sysCustomer = session.QueryOver<Sys_Customer>().List<Sys_Customer>();
                    IList<Com_PEMain> comPEMain = session.QueryOver<Com_PEMain>().List<Com_PEMain>();

                    List<string> cantDelData = new List<string>();
                    foreach (string i in listSelItems)
                    {
                        foreach (Sys_Customer ii in sysCustomer)
                        {
                            if (i != ii.customerName)
                            {
                                continue;
                            }
                            foreach (Com_PEMain y in comPEMain)
                            {
                                if (y.sysCustomer == ii)
                                {
                                    cantDelData.Add(i);
                                    break;
                                }
                                else
                                {
                                    continue;
                                }
                            }
                        }
                    }

                    if (cantDelData.Count > 0)
                    {
                        string cantDelStr = "";
                        for (int i = 0; i < cantDelData.Count; i++)
                        {
                            if (i == 0)
                            {
                                cantDelStr = cantDelData[i];
                            }
                            else
                            {
                                cantDelStr = cantDelStr + "、" + cantDelData[i];
                            }
                        }
                        MessageBox.Show("選項：" + cantDelStr + "已使用中，無法刪除，請重新操作！");
                        SelNum.Text = "0";
                        listSelItems = new List<string>();
                        return;
                    }

                    foreach (string i in listSelItems)
                    {
                        foreach (Sys_Customer ii in sysCustomer)
                        {
                            if (i == ii.customerName)
                            {
                                session.Delete(ii);
                                break;
                            }
                        }
                    }
                    ITransaction trans = session.BeginTransaction();
                    trans.Commit();

                    TabControl_SelectedTabChanged(sender, CurrentE);
                    #endregion
                }
                else if (TabControl.SelectedTab.Text == "製程別")
                {
                    #region 製程別
                    IList<Sys_Operation2> sysOperation2 = session.QueryOver<Sys_Operation2>().List<Sys_Operation2>();
                    IList<Com_PartOperation> comPartOperation = session.QueryOver<Com_PartOperation>().List<Com_PartOperation>();

                    List<string> cantDelData = new List<string>();
                    foreach (string i in listSelItems)
                    {
                        foreach (Sys_Operation2 ii in sysOperation2)
                        {
                            if (i != ii.operation2Name)
                            {
                                continue;
                            }
                            foreach (Com_PartOperation y in comPartOperation)
                            {
                                if (y.sysOperation2 == ii)
                                {
                                    cantDelData.Add(i);
                                    break;
                                }
                                else
                                {
                                    continue;
                                }
                            }
                        }
                    }

                    if (cantDelData.Count > 0)
                    {
                        string cantDelStr = "";
                        for (int i = 0; i < cantDelData.Count; i++)
                        {
                            if (i == 0)
                            {
                                cantDelStr = cantDelData[i];
                            }
                            else
                            {
                                cantDelStr = cantDelStr + "、" + cantDelData[i];
                            }
                        }
                        MessageBox.Show("選項：" + cantDelStr + "已使用中，無法刪除，請重新操作！");
                        SelNum.Text = "0";
                        listSelItems = new List<string>();
                        return;
                    }

                    foreach (string i in listSelItems)
                    {
                        foreach (Sys_Operation2 ii in sysOperation2)
                        {
                            if (i == ii.operation2Name)
                            {
                                session.Delete(ii);
                                break;
                            }
                        }
                    }
                    ITransaction trans = session.BeginTransaction();
                    trans.Commit();

                    TabControl_SelectedTabChanged(sender, CurrentE);
                    #endregion
                }
                else if (TabControl.SelectedTab.Text == "材質")
                {
                    #region 材質
                    IList<Sys_Material> sysMaterial = session.QueryOver<Sys_Material>().List<Sys_Material>();
                    IList<Com_MEMain> comMEMain = session.QueryOver<Com_MEMain>().List<Com_MEMain>();

                    List<string> cantDelData = new List<string>();
                    foreach (string i in listSelItems)
                    {
                        foreach (Sys_Material ii in sysMaterial)
                        {
                            if (i != ii.materialName)
                            {
                                continue;
                            }
                            foreach (Com_MEMain y in comMEMain)
                            {
                                if (y.material == ii.materialName)
                                {
                                    cantDelData.Add(i);
                                    break;
                                }
                                else
                                {
                                    continue;
                                }
                            }
                        }
                    }

                    if (cantDelData.Count > 0)
                    {
                        string cantDelStr = "";
                        for (int i = 0; i < cantDelData.Count; i++)
                        {
                            if (i == 0)
                            {
                                cantDelStr = cantDelData[i];
                            }
                            else
                            {
                                cantDelStr = cantDelStr + "、" + cantDelData[i];
                            }
                        }
                        MessageBox.Show("選項：" + cantDelStr + "已使用中，無法刪除，請重新操作！");
                        SelNum.Text = "0";
                        listSelItems = new List<string>();
                        return;
                    }

                    foreach (string i in listSelItems)
                    {
                        foreach (Sys_Material ii in sysMaterial)
                        {
                            if (i == ii.materialName)
                            {
                                session.Delete(ii);
                                break;
                            }
                        }
                    }
                    ITransaction trans = session.BeginTransaction();
                    trans.Commit();

                    TabControl_SelectedTabChanged(sender, CurrentE);
                    #endregion
                }
            }
            catch (System.Exception ex)
            {
                MessageBox.Show(ex.ToString());
            }
        }
        #endregion

        #region 材質
        private void AddMaterial_Click(object sender, EventArgs e)
        {
            try
            {
                status = Material.CheckData(Material_Category.Text, MaterialText.Text, DicMaterial);
                if (!status)
                {
                    MaterialText.Text = "";
                    return;
                }

                Sys_Material sysMaterial = new Sys_Material();
                sysMaterial.materialName = MaterialText.Text;
                sysMaterial.category = Material_Category.Text;
                session.Save(sysMaterial);
                ITransaction trans = session.BeginTransaction();
                trans.Commit();

                //將目前操作對象重新更新列表
                TabControl_SelectedTabChanged(sender, CurrentE);
                MaterialText.Text = "";
                Material_Category.Text = "";
            }
            catch (System.Exception ex)
            {

            }
        }
        private void DelMaterial_Click(object sender, EventArgs e)
        {
            try
            {
                if (SelMaterialNum.Text == "0")
                {
                    return;
                }

                #region 材質
                IList<Sys_Material> sysMaterial = session.QueryOver<Sys_Material>().List<Sys_Material>();
                IList<Com_MEMain> comMEMain = session.QueryOver<Com_MEMain>().List<Com_MEMain>();

                List<string> cantDelData = new List<string>();
                foreach (string i in listSelMaterialItems)
                {
                    foreach (Sys_Material ii in sysMaterial)
                    {
                        if (i != ii.materialName)
                        {
                            continue;
                        }
                        foreach (Com_MEMain y in comMEMain)
                        {
                            if (y.material == ii.materialName)
                            {
                                cantDelData.Add(i);
                                break;
                            }
                            else
                            {
                                continue;
                            }
                        }
                    }
                }

                if (cantDelData.Count > 0)
                {
                    string cantDelStr = "";
                    for (int i = 0; i < cantDelData.Count; i++)
                    {
                        if (i == 0)
                        {
                            cantDelStr = cantDelData[i];
                        }
                        else
                        {
                            cantDelStr = cantDelStr + "、" + cantDelData[i];
                        }
                    }
                    MessageBox.Show("選項：" + cantDelStr + "已使用中，無法刪除，請重新操作！");
                    SelMaterialNum.Text = "0";
                    listSelMaterialItems = new List<string>();
                    return;
                }

                foreach (string i in listSelMaterialItems)
                {
                    foreach (Sys_Material ii in sysMaterial)
                    {
                        if (i == ii.materialName)
                        {
                            session.Delete(ii);
                            break;
                        }
                    }
                }
                ITransaction trans = session.BeginTransaction();
                trans.Commit();

                TabControl_SelectedTabChanged(sender, CurrentE);
                #endregion
            }
            catch (System.Exception ex)
            {

                throw;
            }
        }
        private void MaterialTree_NodeClick(object sender, TreeNodeMouseEventArgs e)
        {
            try
            {
                int selCount = 0;
                listSelMaterialItems = new List<string>();
                for (int i = 0; i < MaterialTree.Nodes.Count; i++)
                {
                    for (int j = 0; j < MaterialTree.Nodes[i].Nodes.Count; j++)
                    {
                        if (MaterialTree.Nodes[i].Nodes[j].Checked)
                        {
                            selCount++; //MessageBox.Show(MachineTree.Nodes[i].Nodes[j].Text);
                            listSelMaterialItems.Add(MaterialTree.Nodes[i].Nodes[j].Text);
                        }
                    }
                }
                SelMaterialNum.Text = selCount.ToString();
            }
            catch (System.Exception ex)
            {

            }
        }
        #endregion

        #region 製程別
        private void Op2Tree_NodeClick(object sender, TreeNodeMouseEventArgs e)
        {
            try
            {
                int selCount = 0;
                listSelOp2Items = new List<string>();
                for (int i = 0; i < Op2Tree.Nodes.Count; i++)
                {
                    for (int j = 0; j < Op2Tree.Nodes[i].Nodes.Count; j++)
                    {
                        if (Op2Tree.Nodes[i].Nodes[j].Checked)
                        {
                            selCount++; //MessageBox.Show(MachineTree.Nodes[i].Nodes[j].Text);
                            listSelOp2Items.Add(Op2Tree.Nodes[i].Nodes[j].Text);
                        }
                    }
                }
                SelOp2Num.Text = selCount.ToString();
            }
            catch (System.Exception ex)
            {

            }
        }
        private void AddOp2_Click(object sender, EventArgs e)
        {
            try
            {
                status = Operation2.CheckData(Op2_Category.Text, Op2Text.Text, DicOperation2);
                if (!status)
                {
                    Op2Text.Text = "";
                    return;
                }

                Sys_Operation2 sysOperation2 = new Sys_Operation2();
                sysOperation2.operation2Name = Op2Text.Text;
                sysOperation2.category = Op2_Category.Text;
                session.Save(sysOperation2);
                ITransaction trans = session.BeginTransaction();
                trans.Commit();

                //將目前操作對象重新更新列表
                TabControl_SelectedTabChanged(sender, CurrentE);
                Op2Text.Text = "";
                Op2_Category.Text = "";
            }
            catch (System.Exception ex)
            {

            }
        }
        private void DelOp2_Click(object sender, EventArgs e)
        {
            try
            {
                if (SelOp2Num.Text == "0")
                {
                    return;
                }

                IList<Sys_Operation2> sysOperation2 = session.QueryOver<Sys_Operation2>().List();
                IList<Com_PartOperation> comPartOperation = session.QueryOver<Com_PartOperation>().List();

                //比對目前零件中有那些料號已使用要刪除的資料
                List<string> cantDelData = new List<string>();
                foreach (string i in listSelOp2Items)
                {
                    foreach (Sys_Operation2 ii in sysOperation2)
                    {
                        if (i != ii.operation2Name)
                        {
                            continue;
                        }
                        foreach (Com_PartOperation y in comPartOperation)
                        {
                            if (y.sysOperation2 == ii)
                            {
                                cantDelData.Add(i);
                                break;
                            }
                            else
                            {
                                continue;
                            }
                        }
                    }
                }

                if (cantDelData.Count > 0)
                {
                    string cantDelStr = "";
                    for (int i = 0; i < cantDelData.Count; i++)
                    {
                        if (i == 0)
                        {
                            cantDelStr = cantDelData[i];
                        }
                        else
                        {
                            cantDelStr = cantDelStr + "、" + cantDelData[i];
                        }
                    }
                    MessageBox.Show("選項：" + cantDelStr + "已使用中，無法刪除，請重新操作！");
                    SelOp2Num.Text = "0";
                    listSelOp2Items = new List<string>();
                    return;
                }

                foreach (string i in listSelOp2Items)
                {
                    foreach (Sys_Operation2 ii in sysOperation2)
                    {
                        if (i == ii.operation2Name)
                        {
                            session.Delete(ii);
                            break;
                        }
                    }
                }
                ITransaction trans = session.BeginTransaction();
                trans.Commit();

                TabControl_SelectedTabChanged(sender, CurrentE);
            }
            catch (System.Exception ex)
            {

            }
        }
        #endregion

        #region 機台
        private void MachineTree_NodeClick(object sender, TreeNodeMouseEventArgs e)
        {
            try
            {
                int selCount = 0;
                listSelMachineItems = new List<string>();
                for (int i = 0; i < MachineTree.Nodes.Count; i++)
                {
                    for (int j = 0; j < MachineTree.Nodes[i].Nodes.Count; j++)
                    {
                        if (MachineTree.Nodes[i].Nodes[j].Checked)
                        {
                            selCount++; //MessageBox.Show(MachineTree.Nodes[i].Nodes[j].Text);
                            listSelMachineItems.Add(MachineTree.Nodes[i].Nodes[j].Text);
                        }
                    }
                }
                SelMachineNum.Text = selCount.ToString();
            }
            catch (System.Exception ex)
            {

            }
        }
        private void AddMachineBtn_Click(object sender, EventArgs e)
        {
            try
            {
                status = CommonFun.CheckMachineData(MachineType.Text.ToUpper(), MachineNo.Text.ToUpper(), DicMachine);
                if (!status)
                {
                    return;
                }

                if (DicMachine.Keys.Contains(MachineType.Text.ToUpper()))
                {
                    Sys_MachineType sysMachineType = session.QueryOver<Sys_MachineType>()
                        .Where(x => x.machineType == MachineType.Text.ToUpper())
                        .SingleOrDefault<Sys_MachineType>();

                    Sys_MachineNo sysMachineNo = new Sys_MachineNo();
                    sysMachineNo.machineNo = MachineType.Text.ToUpper() + MachineNo.Text.ToUpper();
                    sysMachineNo.machineName = MachineName.Text.ToUpper();
                    sysMachineNo.machineID = MachineID.Text.ToUpper();
                    sysMachineNo.sysMachineType = sysMachineType;
                    session.Save(sysMachineNo);
                    ITransaction trans = session.BeginTransaction();
                    trans.Commit();
                }
                else
                {
                    Sys_MachineType sysMachineType = new Sys_MachineType();
                    sysMachineType.machineType = MachineType.Text.ToUpper();

                    IList<Sys_MachineNo> ListSysMachineNo = new List<Sys_MachineNo>();
                    Sys_MachineNo sysMachineNo = new Sys_MachineNo();
                    sysMachineNo.machineNo = MachineType.Text.ToUpper() + MachineNo.Text.ToUpper();
                    sysMachineNo.machineName = MachineName.Text.ToUpper();
                    sysMachineNo.machineID = MachineID.Text.ToUpper();
                    sysMachineNo.sysMachineType = sysMachineType;
                    ListSysMachineNo.Add(sysMachineNo);

                    sysMachineType.sysMachineNo = ListSysMachineNo;
                    session.Save(sysMachineType);
                    ITransaction trans = session.BeginTransaction();
                    trans.Commit();
                }

                //將目前操作對象重新更新列表
                TabControl_SelectedTabChanged(sender, CurrentE);
            }
            catch (System.Exception ex)
            {

            }
        }
        private void DelMachineBtn_Click(object sender, EventArgs e)
        {
            try
            {
                if (SelMachineNum.Text == "0")
                {
                    return;
                }

                IList<Sys_MachineNo> sysMachineNo = session.QueryOver<Sys_MachineNo>().List<Sys_MachineNo>();
                IList<Com_TEMain> comTEMain = session.QueryOver<Com_TEMain>().List<Com_TEMain>();

                //比對目前零件中有那些料號已使用要刪除的資料
                List<string> cantDelData = new List<string>();
                foreach (string i in listSelMachineItems)
                {
                    foreach (Sys_MachineNo ii in sysMachineNo)
                    {
                        if (i != ii.machineNo)
                        {
                            continue;
                        }
                        foreach (Com_TEMain y in comTEMain)
                        {
                            if (y.machineNo.Contains(ii.machineNo))
                            {
                                cantDelData.Add(i);
                                break;
                            }
                            else
                            {
                                continue;
                            }
                        }
                    }
                }

                if (cantDelData.Count > 0)
                {
                    string cantDelStr = "";
                    for (int i = 0; i < cantDelData.Count; i++)
                    {
                        if (i == 0)
                        {
                            cantDelStr = cantDelData[i];
                        }
                        else
                        {
                            cantDelStr = cantDelStr + "、" + cantDelData[i];
                        }
                    }
                    MessageBox.Show("選項：" + cantDelStr + "已使用中，無法刪除，請重新操作！");
                    SelMachineNum.Text = "0";
                    listSelMachineItems = new List<string>();
                    return;
                }

                foreach (string i in listSelMachineItems)
                {
                    foreach (Sys_MachineNo ii in sysMachineNo)
                    {
                        if (i == ii.machineNo)
                        {
                            session.Delete(ii);
                            break;
                        }
                    }
                }
                ITransaction trans = session.BeginTransaction();
                trans.Commit();

                TabControl_SelectedTabChanged(sender, CurrentE);
            }
            catch (System.Exception ex)
            {

            }
        }
        #endregion

        #region 量具
        private void listView2_MouseUp(object sender, MouseEventArgs e)
        {
            int selCount = 0;
            listSelInstItems = new List<string>();
            foreach (ListViewItem tmpLstView in listView2.Items)
            {
                if (tmpLstView.Selected == true)
                {
                    selCount++;
                    listSelInstItems.Add(tmpLstView.Text);
                }
            }
            SelInstNum.Text = selCount.ToString();
        }
        private void AddInstBtn_Click(object sender, EventArgs e)
        {
            status = CommonFun.CheckInstData(instColor.Text, instName.Text, listInstrument);
            if (!status)
            {
                return;
            }

            Sys_Instrument sysInstrument = new Sys_Instrument();
            sysInstrument.instrumentColor = instColor.Text;
            sysInstrument.instrumentName = instName.Text;
            if (instEngName.Text != "") sysInstrument.instrumentEngName = instEngName.Text;

            session.Save(sysInstrument);
            ITransaction trans = session.BeginTransaction();
            trans.Commit();

            //將目前操作對象重新更新列表
            TabControl_SelectedTabChanged(sender, CurrentE);
        }
        private void DelInstBtn_Click(object sender, EventArgs e)
        {
            IList<Sys_Instrument> sysInstrument = session.QueryOver<Sys_Instrument>().List<Sys_Instrument>();
            IList<Com_Dimension> comDimension = session.QueryOver<Com_Dimension>().List<Com_Dimension>();

            List<string> cantDelData = new List<string>();
            foreach (string i in listSelInstItems)
            {
                foreach (Sys_Instrument ii in sysInstrument)
                {
                    if (i != ii.instrumentName)
                    {
                        continue;
                    }
                    foreach (Com_Dimension y in comDimension)
                    {
                        if (y.instrument == ii.instrumentName)
                        {
                            cantDelData.Add(i);
                            break;
                        }
                        else
                        {
                            continue;
                        }
                    }
                }
            }

            if (cantDelData.Count > 0)
            {
                string cantDelStr = "";
                for (int i = 0; i < cantDelData.Count; i++)
                {
                    if (i == 0)
                    {
                        cantDelStr = cantDelData[i];
                    }
                    else
                    {
                        cantDelStr = cantDelStr + "、" + cantDelData[i];
                    }
                }
                MessageBox.Show("選項：" + cantDelStr + "已使用中，無法刪除，請重新操作！");
                SelInstNum.Text = "0";
                listSelInstItems = new List<string>();
                return;
            }

            foreach (string i in listSelInstItems)
            {
                foreach (Sys_Instrument ii in sysInstrument)
                {
                    if (i == ii.instrumentName + "，" + ii.instrumentEngName)
                    {
                        session.Delete(ii);
                        break;
                    }
                }
            }
            ITransaction trans = session.BeginTransaction();
            trans.Commit();

            TabControl_SelectedTabChanged(sender, CurrentE);
        }
        #endregion

        #region 刪料號
        private void Del_Cus_SelectedIndexChanged(object sender, EventArgs e)
        {
            try
            {
                SGC_DelPanel.PrimaryGrid.Rows.Clear();
                Del_PartNo.Items.Clear();
                Del_PartNo.Text = "";
                Del_CusRev.Items.Clear();
                Del_CusRev.Text = "";
                Del_OpRev.Items.Clear();
                Del_OpRev.Text = "";

                string S_Task_CusName_Path = string.Format(@"{0}\{1}", TaskPath, Del_Cus.Text);
                string[] S_Task_PartNo = Directory.GetDirectories(S_Task_CusName_Path);
                foreach (string item in S_Task_PartNo)
                {
                    Del_PartNo.Items.Add(Path.GetFileNameWithoutExtension(item));//走訪每個元素只取得目錄名稱(不含路徑)並加入dirlist集合中
                }
                if (Del_PartNo.Items.Count == 1)
                {
                    Del_PartNo.Text = Del_PartNo.Items[0].ToString();
                }
            }
            catch (System.Exception ex)
            {

            }

        }
        private void Del_PartNo_SelectedIndexChanged(object sender, EventArgs e)
        {
            try
            {
                SGC_DelPanel.PrimaryGrid.Rows.Clear();
                Del_CusRev.Items.Clear();
                Del_CusRev.Text = "";
                Del_OpRev.Items.Clear();
                Del_OpRev.Text = "";

                string S_Task_PartNo_Path = string.Format(@"{0}\{1}\{2}", TaskPath, Del_Cus.Text, Del_PartNo.Text);
                string[] S_Task_CusRev = Directory.GetDirectories(S_Task_PartNo_Path);
                foreach (string item in S_Task_CusRev)
                {
                    Del_CusRev.Items.Add(Path.GetFileNameWithoutExtension(item));//走訪每個元素只取得目錄名稱(不含路徑)並加入dirlist集合中
                }
                if (Del_CusRev.Items.Count == 1)
                {
                    Del_CusRev.Text = Del_CusRev.Items[0].ToString();
                }
            }
            catch (System.Exception ex)
            {

            }

        }
        private void Del_CusRev_SelectedIndexChanged(object sender, EventArgs e)
        {
            try
            {
                SGC_DelPanel.PrimaryGrid.Rows.Clear();
                Del_OpRev.Items.Clear();
                Del_OpRev.Text = "";

                string S_Task_OpRev_Path = string.Format(@"{0}\{1}\{2}\{3}", TaskPath, Del_Cus.Text, Del_PartNo.Text, Del_CusRev.Text);
                string[] S_Task_OpRev = Directory.GetDirectories(S_Task_OpRev_Path);
                foreach (string item in S_Task_OpRev)
                {
                    Del_OpRev.Items.Add(Path.GetFileNameWithoutExtension(item));//走訪每個元素只取得目錄名稱(不含路徑)並加入dirlist集合中
                }
                if (Del_OpRev.Items.Count == 1)
                {
                    Del_OpRev.Text = Del_OpRev.Items[0].ToString();
                }
            }
            catch (System.Exception ex)
            {

            }

        }
        private void Del_OpRev_SelectedIndexChanged(object sender, EventArgs e)
        {
            try
            {
                SGC_DelPanel.PrimaryGrid.Rows.Clear();

                Com_PEMain comPEMain = session.QueryOver<Com_PEMain>().Where(x => x.partName == Del_PartNo.Text)
                                                                          .Where(x => x.customerVer == Del_CusRev.Text)
                                                                          .Where(x => x.opVer == Del_OpRev.Text).SingleOrDefault();
                //取得此料號所排的製程資訊
                IList<Com_PartOperation> listComPartOperation = session.QueryOver<Com_PartOperation>().Where(x => x.comPEMain == comPEMain).List();
                foreach (Com_PartOperation i in listComPartOperation)
                {
                    SGC_DelPanel.PrimaryGrid.Rows.Add(new GridRow(i.operation1,
                        session.QueryOver<Sys_Operation2>().Where(x => x.operation2SrNo == i.sysOperation2.operation2SrNo)
                        .SingleOrDefault().operation2Name));
                }
            }
            catch (System.Exception ex)
            {

            }

        }
        private void Chk_NX75_CheckedChanged(object sender, EventArgs e)
        {
            if (Chk_NX75.Checked)
            {
                SGC_DelPanel.PrimaryGrid.Rows.Clear();
                Del_Cus.Items.Clear();
                Del_PartNo.Items.Clear();
                Del_CusRev.Items.Clear();
                Del_OpRev.Items.Clear();
                Chk_NX10.Checked = false;
                //TaskPath = string.Format(@"{0}\{1}\{2}\{3}", "D:", "CAX", "Globaltek", "Task");
                TaskPath = string.Format(@"{0}\{1}", CaxEnv.GetGlobaltekEnvDir(), "Task");
                string[] S_Task_CusName = Directory.GetDirectories(TaskPath);
                foreach (string item in S_Task_CusName)
                {
                    Del_Cus.Items.Add(Path.GetFileNameWithoutExtension(item));//走訪每個元素只取得目錄名稱(不含路徑)並加入dirlist集合中
                }
            }
            else
            {
                SGC_DelPanel.PrimaryGrid.Rows.Clear();
                Del_Cus.Items.Clear();
                Del_PartNo.Items.Clear();
                Del_CusRev.Items.Clear();
                Del_OpRev.Items.Clear();
            }
        }
        private void Chk_NX10_CheckedChanged(object sender, EventArgs e)
        {
            if (Chk_NX10.Checked)
            {
                Chk_NX75.Checked = false;
                TaskPath = string.Format(@"{0}\{1}", CaxEnv.GetGlobaltekEnvDir(), "Task_NX10");
                
                string[] S_Task_CusName = Directory.GetDirectories(TaskPath);
                foreach (string item in S_Task_CusName)
                {
                    Del_Cus.Items.Add(Path.GetFileNameWithoutExtension(item));//走訪每個元素只取得目錄名稱(不含路徑)並加入dirlist集合中
                }
            }
            else
            {
                SGC_DelPanel.PrimaryGrid.Rows.Clear();
                Del_Cus.Items.Clear();
                Del_PartNo.Items.Clear();
                Del_CusRev.Items.Clear();
                Del_OpRev.Items.Clear();
            }
        }
        private void Del_OK_Click(object sender, EventArgs e)
        {
            try
            {
                if (Del_Cus.Text == "" || Del_PartNo.Text == "" || Del_CusRev.Text == "" || Del_OpRev.Text == "")
                {
                    MessageBox.Show("請指定料號才能刪除");
                    return;
                }
                if (eTaskDialogResult.Yes != CaxPublic.ShowMsgYesNo(string.Format(@"{0}客戶：{1}料號：{2}客戶版次：{3}製程版次：{4}"
                    , "確定刪除此筆資料嗎？一經刪除，無法復原\r\n", Del_Cus.Text + "\r\n", Del_PartNo.Text + "\r\n", Del_CusRev.Text + "\r\n", Del_OpRev.Text + "\r\n")))
                {
                    return;
                }

                Sys_Customer SysCustomer = session.QueryOver<Sys_Customer>().Where(x => x.customerName == Del_Cus.Text).SingleOrDefault();
                Com_PEMain ComPEMain = session.QueryOver<Com_PEMain>().Where(x => x.sysCustomer == SysCustomer)
                                                                        .And(x => x.partName == Del_PartNo.Text)
                                                                        .And(x => x.customerVer == Del_CusRev.Text)
                                                                        .And(x => x.opVer == Del_OpRev.Text).SingleOrDefault();
                if (ComPEMain != null)
                {
                    using (ITransaction trans = session.BeginTransaction())
                    {
                        session.Delete(ComPEMain);
                        trans.Commit();
                    }
                }

                string FolderPath = string.Format(@"{0}\{1}\{2}\{3}", TaskPath, Del_Cus.Text, Del_PartNo.Text, Del_CusRev.Text);
                string[] S_Task_OpRev = Directory.GetDirectories(FolderPath);
                if (S_Task_OpRev.Length > 1)
                {
                    FolderPath = string.Format(@"{0}\{1}", FolderPath, Del_OpRev.Text);
                }
                else
                {
                    FolderPath = string.Format(@"{0}\{1}\{2}", TaskPath, Del_Cus.Text, Del_PartNo.Text);
                    string[] S_Task_CusRev = Directory.GetDirectories(FolderPath);
                    if (S_Task_CusRev.Length > 1)
                    {
                        FolderPath = string.Format(@"{0}\{1}", FolderPath, Del_CusRev.Text);
                    }
                }
                if (Directory.Exists(FolderPath))
                {
                    Directory.Delete(FolderPath, true);
                }

                Chk_NX75.Checked = false;
                Chk_NX10.Checked = false;

                MessageBox.Show("刪除成功");
                //IList<Com_PartOperation> ListComPartOperation = session.QueryOver<Com_PartOperation>().Where(x => x.comPEMain == ComPEMain).List();
                ////ME相關表
                //IList<Com_MEMain> ListComMEMain = new List<Com_MEMain>();
                //IList<Com_Dimension> ListComDimension = new List<Com_Dimension>();
                //IList<Com_PFMEA> ListComPFMEA = new List<Com_PFMEA>();
                ////TE相關表
                //IList<Com_TEMain> ListComTEMain = new List<Com_TEMain>();
                //IList<Com_ShopDoc> ListComShopDoc = new List<Com_ShopDoc>();
                //IList<Com_ToolList> ListComToolList = new List<Com_ToolList>();
                //IList<Com_ControlDimen> ListComControlDimen = new List<Com_ControlDimen>();
                //foreach (Com_PartOperation i in ListComPartOperation)
                //{
                //    ListComMEMain = session.QueryOver<Com_MEMain>().Where(x => x.comPartOperation == i).List();
                //    ListComTEMain = session.QueryOver<Com_TEMain>().Where(x => x.comPartOperation == i).List();
                //}
            }
            catch (System.Exception ex)
            {
                MessageBox.Show(ex.ToString());
            }
        }
        #endregion

        #region 尺寸類型
        private void AddProductBtn_Click(object sender, EventArgs e)
        {
            try
            {
                status = CommonFun.CheckData(AddProduct.Text, listProduct);
                if (!status)
                {
                    AddProduct.Text = "";
                    return;
                }

                Sys_Product sysProduct = new Sys_Product();
                sysProduct.productName = AddProduct.Text;
                using (ISession session1 = MyHibernateHelper.SessionFactory.OpenSession())
                {
                    session1.Save(sysProduct);
                    using (ITransaction trans = session1.BeginTransaction())
                    {
                        trans.Commit();
                    }
                    session1.Close();
                }
                //將目前操作對象重新更新列表
                TabControl_SelectedTabChanged(sender, CurrentE);
                AddProduct.Text = "";
            }
            catch (System.Exception ex)
            {
                MessageBox.Show(ex.ToString());
            }
        }
        private void DelProduct_Click(object sender, EventArgs e)
        {
            try
            {
                if (SelProductNum.Text == "0")
                {
                    return;
                }

                #region 尺寸類型
                IList<Sys_Product> sysProduct = session.QueryOver<Sys_Product>().List();

                foreach (string i in listSelItems)
                {
                    foreach (Sys_Product ii in sysProduct)
                    {
                        if (i == ii.productName)
                        {
                            session.Delete(ii);
                            break;
                        }
                    }
                }
                ITransaction trans = session.BeginTransaction();
                trans.Commit();

                TabControl_SelectedTabChanged(sender, CurrentE);
                #endregion
            }
            catch (System.Exception ex)
            {
                MessageBox.Show(ex.ToString());
            }
        }
        private void listView3_MouseUp(object sender, MouseEventArgs e)
        {
            int selCount = 0;
            listSelItems = new List<string>();
            foreach (ListViewItem tmpLstView in listView3.Items)
            {
                if (tmpLstView.Selected == true)
                {
                    selCount++;
                    listSelItems.Add(tmpLstView.Text);
                }
            }
            SelProductNum.Text = selCount.ToString();
        }
        #endregion

        #region 修改ERP
        private void Renew_PartNo_ButtonDropDownClick(object sender, CancelEventArgs e)
        {
            Renew_PartNo.BackgroundStyle.Class = "TextBoxBorder";
            Renew_PartNo.BackgroundStyle.CornerType = DevComponents.DotNetBar.eCornerType.Square;
            Renew_PartNo.ButtonDropDown.Visible = true;
            Renew_PartNo.DisplayMembers = "PartNo";
            Renew_PartNo.GroupingMembers = "CustomerName";
            Renew_PartNo.DataSource = listPartData;
        }
        private void Renew_PartNo_TextChanged(object sender, EventArgs e)
        {
            Renew_CusRev.Items.Clear();
            Renew_OpRev.Items.Clear();
            SGC_RenewERP.PrimaryGrid.Rows.Clear();

            IList<Com_PEMain> listComPEMain = session.QueryOver<Com_PEMain>().Where(x => x.partName == Renew_PartNo.Text).List();
            foreach (Com_PEMain i in listComPEMain)
            {
                if (Renew_CusRev.Items.Contains(i.customerVer))
                {
                    continue;
                }
                Renew_CusRev.Items.Add(i.customerVer);
            }
            if (Renew_CusRev.Items.Count == 1)
            {
                Renew_CusRev.Text = Renew_CusRev.Items[0].ToString();
            }
        }
        private void Renew_CusRev_SelectedIndexChanged(object sender, EventArgs e)
        {
            Renew_OpRev.Items.Clear();
            SGC_RenewERP.PrimaryGrid.Rows.Clear();

            IList<Com_PEMain> listComPEMain = session.QueryOver<Com_PEMain>().Where(x => x.partName == Renew_PartNo.Text).And(x => x.customerVer == Renew_CusRev.Text).List();
            foreach (Com_PEMain i in listComPEMain)
            {
                Renew_OpRev.Items.Add(i.opVer);
            }
            if (Renew_OpRev.Items.Count == 1)
            {
                Renew_OpRev.Text = Renew_OpRev.Items[0].ToString();
            }
        }
        private void Renew_OpRev_SelectedIndexChanged(object sender, EventArgs e)
        {
            SGC_RenewERP.PrimaryGrid.Rows.Clear();

            ComPEMain = session.QueryOver<Com_PEMain>().Where(x => x.partName == Renew_PartNo.Text).And(x => x.customerVer == Renew_CusRev.Text).And(x => x.opVer == Renew_OpRev.Text).SingleOrDefault();
            IList<Com_PartOperation> listComPartOperation = session.QueryOver<Com_PartOperation>().Where(x => x.comPEMain == ComPEMain).List();
            foreach (Com_PartOperation item in listComPartOperation)
            {
                object[] rowdata = new object[] { item.operation1, 
                    session.QueryOver<Sys_Operation2>().Where(x => x.operation2SrNo == item.sysOperation2.operation2SrNo).SingleOrDefault().operation2Name,
                    item.erpCode,
                    item.partOperationSrNo};
                SGC_RenewERP.PrimaryGrid.Rows.Add(new GridRow(rowdata));
            }
        }
        private void RenewERPBtn_Click(object sender, EventArgs e)
        {
            using (ISession session2 = MyHibernateHelper.SessionFactory.OpenSession())
            {
                using (ITransaction trans = session2.BeginTransaction())
                {
                    for (int i = 0; i < SGC_RenewERP.PrimaryGrid.Rows.Count; i++)
                    {
                        Com_PartOperation comPartOp = new Com_PartOperation();
                        comPartOp.comPEMain = ComPEMain;
                        comPartOp.partOperationSrNo = Convert.ToInt32(SGC_RenewERP.PrimaryGrid.GetCell(i, 3).Value.ToString());
                        comPartOp.operation1 = SGC_RenewERP.PrimaryGrid.GetCell(i, 0).Value.ToString();
                        comPartOp.sysOperation2 = session2.QueryOver<Sys_Operation2>()
                                                    .Where(x => x.operation2Name == SGC_RenewERP.PrimaryGrid.GetCell(i, 1).Value.ToString())
                                                    .SingleOrDefault<Sys_Operation2>();

                        comPartOp.erpCode = SGC_RenewERP.PrimaryGrid.GetCell(i, 2).Value.ToString();
                        session2.Update(comPartOp);
                    }
                    trans.Commit();
                }
            }
            MessageBox.Show("更新完成");
        }
        #endregion
        
    }
}
