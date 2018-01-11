using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

using DevComponents.DotNetBar;
using DevComponents.DotNetBar.SuperGrid;

namespace WorldChampion
{
    public class DotnetbarFunction
    {
        //建立一個GridColumn物件
        public static bool GetMyTextBoxXGridColumn(string columnName, out GridColumn gridColumn)
        {
            gridColumn = new GridColumn();
            try
            {
                gridColumn.AutoSizeMode = DevComponents.DotNetBar.SuperGrid.ColumnAutoSizeMode.AllCells;
                gridColumn.CellStyles.Default.Alignment = DevComponents.DotNetBar.SuperGrid.Style.Alignment.MiddleCenter;
                gridColumn.ColumnSortMode = DevComponents.DotNetBar.SuperGrid.ColumnSortMode.None;
                gridColumn.EditorType = typeof(DevComponents.DotNetBar.SuperGrid.GridTextBoxXEditControl);
                gridColumn.HeaderText = columnName;
                gridColumn.Name = columnName;
                gridColumn.ResizeMode = DevComponents.DotNetBar.SuperGrid.ColumnResizeMode.None;
                gridColumn.SortIndicator = DevComponents.DotNetBar.SuperGrid.SortIndicator.None;
            }
            catch (System.Exception ex)
            {
                //MessageBox.Show(ex.Message);
                return false;
            }
            return true;
        }

    }
}
