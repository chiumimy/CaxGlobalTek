using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Text;
using System.Windows.Forms;
using DevComponents.AdvTree;
using NXOpen.UF;
using NXOpen;
using System.IO;
using DevComponents.DotNetBar;
using CaxGlobaltek;
using NXOpen.Utilities;

namespace BomList
{
    public partial class BomListDlg : Form
    {
        private static bool status;
        public static Session theSession = Session.GetSession();
        public static UI theUI;
        public static UFSession theUfSession = UFSession.GetUFSession();
        public static Part workPart = theSession.Parts.Work;
        public static Part displayPart = theSession.Parts.Display;
        private ElementStyle _RightAlignFileSizeStyle = null;
        public BomListDlg()
        {
            InitializeComponent();
        }

        private void BomListDlg_Load(object sender, EventArgs e)
        {
            try
            {
                //advTree1.BeginUpdate();

                //加入MOT總組立檔案
                Node node = new Node();
                node.Tag = (NXOpen.Assemblies.Component)displayPart.ComponentAssembly.RootComponent;
                node.Text = Path.GetFileNameWithoutExtension(displayPart.FullPath);
                advTree1.Nodes.Add(node);
                node.ExpandVisibility = eNodeExpandVisibility.Visible;

                //加入所有子Comp
                NXOpen.Assemblies.Component clickComp = (NXOpen.Assemblies.Component)node.Tag;
                List<NXOpen.Assemblies.Component> compAry = new List<NXOpen.Assemblies.Component>();
                status = CaxAsm.GetCompChildren(clickComp, ref compAry, false);
                if (!status)
                {
                    MessageBox.Show("尋找子Component失敗，請聯繫開發工程師");
                    this.Close();
                }
               
                foreach (NXOpen.Assemblies.Component i in compAry)
                {
                    status = InsertSubComponent(node, i);
                    if (!status)
                    {
                        MessageBox.Show("插入子Component失敗，請聯繫開發工程師");
                        this.Close();
                    }
                }

                //比對歷史BomList，並將新增的零件上紅色










                //advTree1.EndUpdate();
            }
            catch (System.Exception ex)
            {
                MessageBox.Show(ex.ToString());
            }
        }

        //(註解)點選"+"的時候
        private void advTree1_BeforeExpand(object sender, AdvTreeNodeCancelEventArgs e)
        {
            /*
            try
            {
                Node parent = e.Node;
                if (parent.Nodes.Count > 0) return;

                NXOpen.Assemblies.Component clickComp = (NXOpen.Assemblies.Component)parent.Tag;
                List<NXOpen.Assemblies.Component> compAry = new List<NXOpen.Assemblies.Component>();
                status = CaxAsm.GetCompChildren(clickComp, ref compAry, false);
                if (!status)
                {
                    MessageBox.Show("尋找子Component失敗，請聯繫開發工程師");
                    this.Close();
                }

                //判斷搜出來的子comp是否還有他的子comp，如果有就加入"+"符號，如果沒有表示最後一階層
                List<NXOpen.Assemblies.Component> childrenCompAry = new List<NXOpen.Assemblies.Component>();
                foreach (NXOpen.Assemblies.Component i in compAry)
                {
                    childrenCompAry = new List<NXOpen.Assemblies.Component>();
                    CaxAsm.GetCompChildren(i, ref childrenCompAry, false);
                    if (childrenCompAry.Count > 0)
                    {
                        status = InsertSubComponent(parent, i, true);
                        if (!status)
                        {
                            MessageBox.Show("插入子Component失敗，請聯繫開發工程師");
                            this.Close();
                        }
                    }
                    else
                    {
                        status = InsertSubComponent(parent, i, false);
                        if (!status)
                        {
                            MessageBox.Show("插入子Component失敗，請聯繫開發工程師");
                            this.Close();
                        }
                    }
                }
            }
            catch (System.Exception ex)
            {
                MessageBox.Show(ex.ToString());
            }
            */
        }

        //插入子Comp，如果
        private bool InsertSubComponent(Node parent, NXOpen.Assemblies.Component comp)
        {
            try
            {
                List<NXOpen.Assemblies.Component> childrenCompAry = new List<NXOpen.Assemblies.Component>();
                CaxAsm.GetCompChildren(comp, ref childrenCompAry, false);

                Node node = new Node();
                node.Tag = (NXOpen.Assemblies.Component)NXObjectManager.Get(comp.Tag);
                node.Text = Path.GetFileNameWithoutExtension(comp.DisplayName);
                //node.Style = new ElementStyle();
                //node.Style.TextColor = Color.Red;
                node.CheckBoxVisible = true;

                //判斷搜出來的子comp是否還有他的子comp，如果有就加入"+"符號，如果沒有表示最後一階層
                if (childrenCompAry.Count > 0)
                {
                    node.ExpandVisibility = eNodeExpandVisibility.Visible;
                }
                parent.Nodes.Add(node);
                
                foreach (NXOpen.Assemblies.Component i in childrenCompAry)
                {
                    status = InsertSubComponent(node, i);
                    if (!status)
                    {
                        MessageBox.Show("插入子Component失敗，請聯繫開發工程師");
                        this.Close();
                    }
                }
            }
            catch (System.Exception ex)
            {
                MessageBox.Show(ex.ToString());
                return false;
            }
            return true;
        }

        private static bool NodeCheckBox(Node parent)
        {
            try
            {
                NodeCollection nodes = parent.Nodes;

                foreach (Node i in nodes)
                {
                    if (parent.Checked == true) i.Checked = true;
                    else i.Checked = false;
                    NodeCheckBox(i);
                }
                
            }
            catch (System.Exception ex)
            {
                return false;
            }
            return true;
        }

        private void advTree1_NodeClick(object sender, TreeNodeMouseEventArgs e)
        {
            try
            {
                Node parent = e.Node;

                NodeCheckBox(parent);

                
                //NXOpen.Assemblies.Component clickComp = (NXOpen.Assemblies.Component)parent.Tag;
                //MessageBox.Show(parent.Nodes.Count.ToString());
                
            }
            catch (System.Exception ex)
            {
            	
            }
        }

        private static bool GetSelectNode(Node parent, ref List<string> listBomData)
        {
            try
            {
                NodeCollection nodes = parent.Nodes;
                foreach (Node i in nodes)
                {
                    if (i.Checked == true)
                    {
                        listBomData.Add(i.ToString());
                    }
                    GetSelectNode(i, ref listBomData);
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
                string[] bomData;
                List<string> listBomData = new List<string>();
                List<Node> listNodes = new List<Node>();
                NodeCollection nodes = advTree1.Nodes;
                foreach (Node i in nodes)
                {
                    status = GetSelectNode(i, ref listBomData);
                    if (!status)
                    {
                        MessageBox.Show("取得選擇的Node失敗");
                        return;
                    }
                }
                bomData = listBomData.ToArray();
                System.IO.File.WriteAllLines(string.Format(@"{0}\{1}", Path.GetDirectoryName(displayPart.FullPath)
                                                                     , Path.GetFileNameWithoutExtension(displayPart.FullPath) + ".txt")
                                                                     , bomData);
                MessageBox.Show("BomList清單輸出完成");
            }
            catch (System.Exception ex)
            {
                MessageBox.Show(ex.ToString());
            }
        }

        private void Close_Click(object sender, EventArgs e)
        {
            this.Close();
        }

        private void Update_Click(object sender, EventArgs e)
        {
            Size BomListDlgSize = new Size(510,457);
            this.Size = BomListDlgSize;

            Size advTreeSize = new Size(470,310);
            advTree1.Size = advTreeSize;

            System.Drawing.Point OKBtnLocation = new System.Drawing.Point(163, 382);
            OK.Location = OKBtnLocation;

            System.Drawing.Point CloseBtnLocation = new System.Drawing.Point(258, 382);
            Close.Location = CloseBtnLocation;


        }

        private void RemoveHead_CheckedChanged(object sender, EventArgs e)
        {
            if (RemoveHead.Checked == true)
            {
                RemoveTail.Checked = false;
            }
        }

        private void RemoveTail_CheckedChanged(object sender, EventArgs e)
        {
            if (RemoveTail.Checked == true)
            {
                RemoveHead.Checked = false;
            }
        }

        private void RemoveSymbol_CheckedChanged(object sender, EventArgs e)
        {

        }

    }
}
