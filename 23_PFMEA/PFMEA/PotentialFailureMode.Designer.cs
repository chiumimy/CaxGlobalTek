namespace PFMEA
{
    partial class PotentialFailureMode
    {
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
            this.SGC_PFM = new DevComponents.DotNetBar.SuperGrid.SuperGridControl();
            this.gridColumn1 = new DevComponents.DotNetBar.SuperGrid.GridColumn();
            this.gridColumn2 = new DevComponents.DotNetBar.SuperGrid.GridColumn();
            this.Manual = new DevComponents.DotNetBar.Controls.TextBoxX();
            this.labelX1 = new DevComponents.DotNetBar.LabelX();
            this.AddBtn = new DevComponents.DotNetBar.ButtonX();
            this.OK = new DevComponents.DotNetBar.ButtonX();
            this.SuspendLayout();
            // 
            // styleManager1
            // 
            this.styleManager1.ManagerStyle = DevComponents.DotNetBar.eStyle.Windows7Blue;
            this.styleManager1.MetroColorParameters = new DevComponents.DotNetBar.Metro.ColorTables.MetroColorGeneratorParameters(System.Drawing.Color.White, System.Drawing.Color.FromArgb(((int)(((byte)(43)))), ((int)(((byte)(87)))), ((int)(((byte)(154))))));
            // 
            // SGC_PFM
            // 
            this.SGC_PFM.FilterExprColors.SysFunction = System.Drawing.Color.DarkRed;
            this.SGC_PFM.Location = new System.Drawing.Point(12, 12);
            this.SGC_PFM.Name = "SGC_PFM";
            // 
            // 
            // 
            this.SGC_PFM.PrimaryGrid.Columns.Add(this.gridColumn1);
            this.SGC_PFM.PrimaryGrid.Columns.Add(this.gridColumn2);
            this.SGC_PFM.PrimaryGrid.ShowRowHeaders = false;
            this.SGC_PFM.Size = new System.Drawing.Size(486, 229);
            this.SGC_PFM.TabIndex = 2;
            this.SGC_PFM.Text = "superGridControl1";
            // 
            // gridColumn1
            // 
            this.gridColumn1.CellStyles.Default.Alignment = DevComponents.DotNetBar.SuperGrid.Style.Alignment.MiddleCenter;
            this.gridColumn1.EditorType = typeof(DevComponents.DotNetBar.SuperGrid.GridCheckBoxXEditControl);
            this.gridColumn1.HeaderText = "選擇";
            this.gridColumn1.Name = "gridColumn1";
            this.gridColumn1.Width = 50;
            // 
            // gridColumn2
            // 
            this.gridColumn2.AutoSizeMode = DevComponents.DotNetBar.SuperGrid.ColumnAutoSizeMode.Fill;
            this.gridColumn2.EditorType = typeof(DevComponents.DotNetBar.SuperGrid.GridLabelXEditControl);
            this.gridColumn2.HeaderText = "潛在失效模式";
            this.gridColumn2.Name = "gridColumn2";
            // 
            // Manual
            // 
            // 
            // 
            // 
            this.Manual.Border.Class = "TextBoxBorder";
            this.Manual.Border.CornerType = DevComponents.DotNetBar.eCornerType.Square;
            this.Manual.Font = new System.Drawing.Font("新細明體", 12F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(136)));
            this.Manual.Location = new System.Drawing.Point(85, 254);
            this.Manual.Name = "Manual";
            this.Manual.PreventEnterBeep = true;
            this.Manual.Size = new System.Drawing.Size(185, 27);
            this.Manual.TabIndex = 9;
            // 
            // labelX1
            // 
            // 
            // 
            // 
            this.labelX1.BackgroundStyle.CornerType = DevComponents.DotNetBar.eCornerType.Square;
            this.labelX1.Font = new System.Drawing.Font("標楷體", 12F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(136)));
            this.labelX1.Location = new System.Drawing.Point(12, 254);
            this.labelX1.Name = "labelX1";
            this.labelX1.Size = new System.Drawing.Size(79, 27);
            this.labelX1.TabIndex = 10;
            this.labelX1.Text = "手動新增";
            // 
            // AddBtn
            // 
            this.AddBtn.AccessibleRole = System.Windows.Forms.AccessibleRole.PushButton;
            this.AddBtn.ColorTable = DevComponents.DotNetBar.eButtonColor.Flat;
            this.AddBtn.Image = global::PFMEA.Properties.Resources.Modify_16px;
            this.AddBtn.Location = new System.Drawing.Point(276, 254);
            this.AddBtn.Name = "AddBtn";
            this.AddBtn.Size = new System.Drawing.Size(32, 27);
            this.AddBtn.Style = DevComponents.DotNetBar.eDotNetBarStyle.StyleManagerControlled;
            this.AddBtn.TabIndex = 11;
            this.AddBtn.Click += new System.EventHandler(this.AddBtn_Click);
            // 
            // OK
            // 
            this.OK.AccessibleRole = System.Windows.Forms.AccessibleRole.PushButton;
            this.OK.ColorTable = DevComponents.DotNetBar.eButtonColor.Flat;
            this.OK.Font = new System.Drawing.Font("標楷體", 12F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(136)));
            this.OK.Image = global::PFMEA.Properties.Resources.Confirm_24px;
            this.OK.Location = new System.Drawing.Point(377, 247);
            this.OK.Name = "OK";
            this.OK.Size = new System.Drawing.Size(121, 34);
            this.OK.Style = DevComponents.DotNetBar.eDotNetBarStyle.StyleManagerControlled;
            this.OK.TabIndex = 7;
            this.OK.Text = "確定/離開";
            this.OK.Click += new System.EventHandler(this.OK_Click);
            // 
            // PotentialFailureMode
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 12F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.ClientSize = new System.Drawing.Size(510, 290);
            this.Controls.Add(this.Manual);
            this.Controls.Add(this.AddBtn);
            this.Controls.Add(this.labelX1);
            this.Controls.Add(this.OK);
            this.Controls.Add(this.SGC_PFM);
            this.DoubleBuffered = true;
            this.Name = "PotentialFailureMode";
            this.Text = "潛在失效模式";
            this.ResumeLayout(false);

        }

        #endregion

        private DevComponents.DotNetBar.StyleManager styleManager1;
        private DevComponents.DotNetBar.SuperGrid.SuperGridControl SGC_PFM;
        private DevComponents.DotNetBar.SuperGrid.GridColumn gridColumn1;
        private DevComponents.DotNetBar.SuperGrid.GridColumn gridColumn2;
        private DevComponents.DotNetBar.ButtonX OK;
        private DevComponents.DotNetBar.Controls.TextBoxX Manual;
        private DevComponents.DotNetBar.LabelX labelX1;
        private DevComponents.DotNetBar.ButtonX AddBtn;
    }
}