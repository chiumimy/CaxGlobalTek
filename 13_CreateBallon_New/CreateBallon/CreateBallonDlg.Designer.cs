namespace CreateBallon
{
    partial class CreateBallonDlg
    {
        /// <summary>
        /// 全域變數
        /// </summary>
        public bool Is_Keep;



        /// <summary>
        /// Required designer variable.
        /// </summary>
        private System.ComponentModel.IContainer components = null;

        /// <summary>
        /// Clean up any resources being used.
        /// </summary>
        /// <param name="disposing">true if managed resources should be disposed; otherwise, false.</param>
        protected override void Dispose(bool disposing)
        {
            if (disposing && (components != null))
            {
                components.Dispose();
            }
            base.Dispose(disposing);
        }

        #region Windows Form Designer generated code

        /// <summary>
        /// Required method for Designer support - do not modify
        /// the contents of this method with the code editor.
        /// </summary>
        private void InitializeComponent()
        {
            this.components = new System.ComponentModel.Container();
            this.styleManager1 = new DevComponents.DotNetBar.StyleManager(this.components);
            this.chb_keepOrigination = new DevComponents.DotNetBar.Controls.CheckBoxX();
            this.chb_Regeneration = new DevComponents.DotNetBar.Controls.CheckBoxX();
            this.Cancel = new DevComponents.DotNetBar.ButtonX();
            this.OK = new DevComponents.DotNetBar.ButtonX();
            this.chb_UserDefine = new DevComponents.DotNetBar.Controls.CheckBoxX();
            this.SGC = new DevComponents.DotNetBar.SuperGrid.SuperGridControl();
            this.gridColumn1 = new DevComponents.DotNetBar.SuperGrid.GridColumn();
            this.gridColumn3 = new DevComponents.DotNetBar.SuperGrid.GridColumn();
            this.gridColumn2 = new DevComponents.DotNetBar.SuperGrid.GridColumn();
            this.gridColumn4 = new DevComponents.DotNetBar.SuperGrid.GridColumn();
            this.SuspendLayout();
            // 
            // styleManager1
            // 
            this.styleManager1.ManagerStyle = DevComponents.DotNetBar.eStyle.Windows7Blue;
            this.styleManager1.MetroColorParameters = new DevComponents.DotNetBar.Metro.ColorTables.MetroColorGeneratorParameters(System.Drawing.Color.White, System.Drawing.Color.FromArgb(((int)(((byte)(43)))), ((int)(((byte)(87)))), ((int)(((byte)(154))))));
            // 
            // chb_keepOrigination
            // 
            // 
            // 
            // 
            this.chb_keepOrigination.BackgroundStyle.CornerType = DevComponents.DotNetBar.eCornerType.Square;
            this.chb_keepOrigination.Font = new System.Drawing.Font("標楷體", 12F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(136)));
            this.chb_keepOrigination.Location = new System.Drawing.Point(12, 9);
            this.chb_keepOrigination.Name = "chb_keepOrigination";
            this.chb_keepOrigination.Size = new System.Drawing.Size(310, 57);
            this.chb_keepOrigination.Style = DevComponents.DotNetBar.eDotNetBarStyle.StyleManagerControlled;
            this.chb_keepOrigination.TabIndex = 2;
            this.chb_keepOrigination.Text = "保留原始泡泡圖\r\n(已產生泡泡且有新增或修改量測檢具)";
            this.chb_keepOrigination.CheckedChanged += new System.EventHandler(this.chb_keepOrigination_CheckedChanged);
            // 
            // chb_Regeneration
            // 
            // 
            // 
            // 
            this.chb_Regeneration.BackgroundStyle.CornerType = DevComponents.DotNetBar.eCornerType.Square;
            this.chb_Regeneration.Font = new System.Drawing.Font("標楷體", 12F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(136)));
            this.chb_Regeneration.Location = new System.Drawing.Point(12, 72);
            this.chb_Regeneration.Name = "chb_Regeneration";
            this.chb_Regeneration.Size = new System.Drawing.Size(310, 57);
            this.chb_Regeneration.Style = DevComponents.DotNetBar.eDotNetBarStyle.StyleManagerControlled;
            this.chb_Regeneration.TabIndex = 3;
            this.chb_Regeneration.Text = "新產生泡泡圖\r\n(尚未產生過泡泡或想重新更新泡泡)";
            this.chb_Regeneration.CheckedChanged += new System.EventHandler(this.chb_Regeneration_CheckedChanged);
            // 
            // Cancel
            // 
            this.Cancel.AccessibleRole = System.Windows.Forms.AccessibleRole.PushButton;
            this.Cancel.ColorTable = DevComponents.DotNetBar.eButtonColor.Flat;
            this.Cancel.Font = new System.Drawing.Font("標楷體", 12F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(136)));
            this.Cancel.Image = global::CreateBallon.Properties.Resources.Error_24px;
            this.Cancel.Location = new System.Drawing.Point(170, 193);
            this.Cancel.Name = "Cancel";
            this.Cancel.Size = new System.Drawing.Size(75, 31);
            this.Cancel.Style = DevComponents.DotNetBar.eDotNetBarStyle.StyleManagerControlled;
            this.Cancel.TabIndex = 5;
            this.Cancel.Text = "關閉";
            this.Cancel.Click += new System.EventHandler(this.Cancel_Click);
            // 
            // OK
            // 
            this.OK.AccessibleRole = System.Windows.Forms.AccessibleRole.PushButton;
            this.OK.ColorTable = DevComponents.DotNetBar.eButtonColor.Flat;
            this.OK.Font = new System.Drawing.Font("標楷體", 12F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(136)));
            this.OK.Image = global::CreateBallon.Properties.Resources.Confirm_24px;
            this.OK.Location = new System.Drawing.Point(89, 193);
            this.OK.Name = "OK";
            this.OK.Size = new System.Drawing.Size(75, 31);
            this.OK.Style = DevComponents.DotNetBar.eDotNetBarStyle.StyleManagerControlled;
            this.OK.TabIndex = 4;
            this.OK.Text = "確定";
            this.OK.Click += new System.EventHandler(this.OK_Click);
            // 
            // chb_UserDefine
            // 
            // 
            // 
            // 
            this.chb_UserDefine.BackgroundStyle.CornerType = DevComponents.DotNetBar.eCornerType.Square;
            this.chb_UserDefine.Font = new System.Drawing.Font("標楷體", 12F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(136)));
            this.chb_UserDefine.Location = new System.Drawing.Point(12, 135);
            this.chb_UserDefine.Name = "chb_UserDefine";
            this.chb_UserDefine.Size = new System.Drawing.Size(310, 57);
            this.chb_UserDefine.Style = DevComponents.DotNetBar.eDotNetBarStyle.StyleManagerControlled;
            this.chb_UserDefine.TabIndex = 6;
            this.chb_UserDefine.Text = "使用者自定義\r\n(當客戶已提供泡泡球位置)";
            this.chb_UserDefine.CheckedChanged += new System.EventHandler(this.chb_UserDefine_CheckedChanged);
            // 
            // SGC
            // 
            this.SGC.FilterExprColors.SysFunction = System.Drawing.Color.DarkRed;
            this.SGC.Location = new System.Drawing.Point(39, 248);
            this.SGC.Name = "SGC";
            // 
            // 
            // 
            this.SGC.PrimaryGrid.ColumnDragBehavior = DevComponents.DotNetBar.SuperGrid.ColumnDragBehavior.None;
            this.SGC.PrimaryGrid.Columns.Add(this.gridColumn1);
            this.SGC.PrimaryGrid.Columns.Add(this.gridColumn3);
            this.SGC.PrimaryGrid.Columns.Add(this.gridColumn2);
            this.SGC.PrimaryGrid.Columns.Add(this.gridColumn4);
            this.SGC.PrimaryGrid.MultiSelect = false;
            this.SGC.PrimaryGrid.ShowRowHeaders = false;
            this.SGC.Size = new System.Drawing.Size(250, 304);
            this.SGC.TabIndex = 9;
            this.SGC.Text = "superGridControl1";
            this.SGC.CellClick += new System.EventHandler<DevComponents.DotNetBar.SuperGrid.GridCellClickEventArgs>(this.SGC_CellClick);
            this.SGC.CellValueChanged += new System.EventHandler<DevComponents.DotNetBar.SuperGrid.GridCellValueChangedEventArgs>(this.SGC_CellValueChanged);
            // 
            // gridColumn1
            // 
            this.gridColumn1.CellStyles.Default.Alignment = DevComponents.DotNetBar.SuperGrid.Style.Alignment.MiddleCenter;
            this.gridColumn1.ColumnSortMode = DevComponents.DotNetBar.SuperGrid.ColumnSortMode.None;
            this.gridColumn1.EditorType = typeof(DevComponents.DotNetBar.SuperGrid.GridLabelXEditControl);
            this.gridColumn1.Name = "序號";
            this.gridColumn1.ResizeMode = DevComponents.DotNetBar.SuperGrid.ColumnResizeMode.None;
            this.gridColumn1.Width = 50;
            // 
            // gridColumn3
            // 
            this.gridColumn3.CellStyles.Default.Alignment = DevComponents.DotNetBar.SuperGrid.Style.Alignment.MiddleCenter;
            this.gridColumn3.ColumnSortMode = DevComponents.DotNetBar.SuperGrid.ColumnSortMode.None;
            this.gridColumn3.EditorType = typeof(DevComponents.DotNetBar.SuperGrid.GridLabelXEditControl);
            this.gridColumn3.Name = "尺寸位置";
            this.gridColumn3.ResizeMode = DevComponents.DotNetBar.SuperGrid.ColumnResizeMode.None;
            // 
            // gridColumn2
            // 
            this.gridColumn2.CellStyles.Default.Alignment = DevComponents.DotNetBar.SuperGrid.Style.Alignment.MiddleCenter;
            this.gridColumn2.ColumnSortMode = DevComponents.DotNetBar.SuperGrid.ColumnSortMode.None;
            this.gridColumn2.Name = "自定泡泡號";
            this.gridColumn2.ResizeMode = DevComponents.DotNetBar.SuperGrid.ColumnResizeMode.None;
            // 
            // gridColumn4
            // 
            this.gridColumn4.ColumnSortMode = DevComponents.DotNetBar.SuperGrid.ColumnSortMode.None;
            this.gridColumn4.EditorType = typeof(DevComponents.DotNetBar.SuperGrid.GridLabelXEditControl);
            this.gridColumn4.Name = "Dimension";
            this.gridColumn4.ResizeMode = DevComponents.DotNetBar.SuperGrid.ColumnResizeMode.None;
            this.gridColumn4.Visible = false;
            // 
            // CreateBallonDlg
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 12F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.ClientSize = new System.Drawing.Size(334, 232);
            this.Controls.Add(this.SGC);
            this.Controls.Add(this.chb_UserDefine);
            this.Controls.Add(this.Cancel);
            this.Controls.Add(this.OK);
            this.Controls.Add(this.chb_Regeneration);
            this.Controls.Add(this.chb_keepOrigination);
            this.DoubleBuffered = true;
            this.EnableGlass = false;
            this.MaximizeBox = false;
            this.MinimizeBox = false;
            this.Name = "CreateBallonDlg";
            this.Text = "泡泡圖選項";
            this.ResumeLayout(false);

        }

        #endregion

        private DevComponents.DotNetBar.StyleManager styleManager1;
        private DevComponents.DotNetBar.Controls.CheckBoxX chb_keepOrigination;
        private DevComponents.DotNetBar.Controls.CheckBoxX chb_Regeneration;
        private DevComponents.DotNetBar.ButtonX OK;
        private DevComponents.DotNetBar.ButtonX Cancel;
        private DevComponents.DotNetBar.Controls.CheckBoxX chb_UserDefine;
        private DevComponents.DotNetBar.SuperGrid.SuperGridControl SGC;
        private DevComponents.DotNetBar.SuperGrid.GridColumn gridColumn1;
        private DevComponents.DotNetBar.SuperGrid.GridColumn gridColumn2;
        private DevComponents.DotNetBar.SuperGrid.GridColumn gridColumn3;
        private DevComponents.DotNetBar.SuperGrid.GridColumn gridColumn4;
    }
}