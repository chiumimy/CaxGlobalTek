using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Windows.Forms;
using DevComponents.DotNetBar.SuperGrid;

namespace OutputExcelForm
{
    public class CheckFun
    {
        public static bool status;
        public static bool CheckAll(string section, GridPanel panel)
        {
            try
            {
                //檢查是否有選擇
                status = Is_Select(panel);
                if (!status)
                {
                    return false;
                }

                //檢查是否有選擇Excel格式
                status = Is_SelectForm(section, panel);
                if (!status)
                {
                    return false;
                }
            }
            catch (System.Exception ex)
            {
                return false;
            }
            return true;
        }
        public static bool Is_Select(GridPanel panel)
        {
            try
            {
                int count = 0;
                for (int i = 0; i < panel.Rows.Count; i++)
                {
                    if (((bool)panel.GetCell(i, 0).Value) == false)
                    {
                        count++;
                    }
                }
                if (count == panel.Rows.Count)
                {
                    MessageBox.Show("請先選擇要輸出的資料");
                    return false;
                }
            }
            catch (System.Exception ex)
            {
                return false;
            }
            return true;
        }
        public static bool Is_SelectForm(string section, GridPanel panel)
        {
            try
            {
                for (int i = 0; i < panel.Rows.Count; i++)
                {
                    if (((bool)panel.GetCell(i, 0).Value) == false)
                    {
                        continue;
                    }
                    if (section == "ME")
                    {
                        if (panel.GetCell(i, 1).Value == "PDF")
                        {
                            continue;
                        }

                        if (panel.GetCell(i, 3).Value == "" || panel.GetCell(i, 3).Value == "(雙擊)選擇表單")
                        {
                            panel.GetCell(i, 3).SetActive(((bool)panel.GetCell(i, 0).Value));
                            panel.GetCell(i, 3).CellStyles.Selected.Background.Color1 = System.Drawing.Color.Red;
                            panel.GetCell(i, 3).CellStyles.Selected.Background.Color2 = System.Drawing.Color.Red;
                            MessageBox.Show("表單 " + panel.GetCell(i, 1).Value.ToString() + " 尚未選擇廠區");
                            return false;
                        }
                    }
                    else if (section == "TE")
                    {
                        if (panel.GetCell(i, 1).Value == "NC程式")
                        {
                            continue;
                        }

                        if (panel.GetCell(i, 3).Value == "" || panel.GetCell(i, 3).Value == "(雙擊)選擇表單")
                        {
                            panel.GetCell(i, 3).SetActive(((bool)panel.GetCell(i, 0).Value));
                            panel.GetCell(i, 3).CellStyles.Selected.Background.Color1 = System.Drawing.Color.Red;
                            panel.GetCell(i, 3).CellStyles.Selected.Background.Color2 = System.Drawing.Color.Red;
                            MessageBox.Show("表單 " + panel.GetCell(i, 1).Value.ToString() + " 尚未選擇廠區");
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
    }
}
