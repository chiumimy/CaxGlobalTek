using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Windows.Forms;
using DevComponents.AdvTree;

namespace AddDeleteDB
{
    public class CommonFun
    {
        public static bool CheckData(string AddText, List<string> DBdata)
        {
            try
            {
                if (AddText == "")
                {
                    return false;
                }
                foreach (string i in DBdata)
                {
                    if (i.ToUpper() == AddText.ToUpper())
                    {
                        MessageBox.Show("已存在此資料名稱");
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
        public static bool CheckMachineData(string MachineTypeText, string MachineNoText, Dictionary<string, List<AddDeleteDB.Form1.MachineNoData>> DicMachine)
        {
            try
            {
                if (MachineTypeText == "" || MachineNoText == "")
                {
                    MessageBox.Show("填寫資料不完全，無法新增");
                    return false;
                }
                foreach (KeyValuePair<string, List<AddDeleteDB.Form1.MachineNoData>> kvp in DicMachine)
                {
                    if (MachineTypeText.ToUpper() != kvp.Key)
                    {
                        continue;
                    }
                    foreach (AddDeleteDB.Form1.MachineNoData i in kvp.Value)
                    {
                        if (i.MachineNo == MachineNoText)
                        {
                            MessageBox.Show("編號：" + i.MachineNo + "已重複，請重新填寫");
                            return false;
                        }
                    }
                }
            }
            catch (System.Exception ex)
            {
                return false;
            }
            return true;
        }
        public static bool CheckInstData(string InstColorText, string InstNameText, List<AddDeleteDB.Form1.InstrumentData> DBdata)
        {
            try
            {
                if (InstColorText == "" || InstNameText == "")
                {
                    MessageBox.Show("填寫資料不完全，無法新增");
                    return false;
                }
                foreach (AddDeleteDB.Form1.InstrumentData i in DBdata)
                {
                    if (i.instrumentColor.ToUpper() == InstColorText.ToUpper() & i.instrumentName.ToUpper() == InstNameText.ToUpper())
                    {
                        MessageBox.Show("已存在此量具名稱");
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
        public static bool RenewAdvTree(Dictionary<string, List<string>> DicData, AdvTree AdvTree)
        {
            try
            {
                foreach (KeyValuePair<string, List<string>> kvp in DicData)
                {
                    Node node1 = new Node();
                    node1.Text = kvp.Key;
                    node1.ExpandVisibility = eNodeExpandVisibility.Visible;
                    foreach (string i in kvp.Value)
                    {
                        Node node2 = new Node();
                        node2.Text = i;
                        node2.CheckBoxVisible = true;
                        node1.Nodes.Add(node2);
                    }
                    AdvTree.Nodes.Add(node1);
                }
            }
            catch (System.Exception ex)
            {
                return false;
            }
            return true;
        }
    }
}
