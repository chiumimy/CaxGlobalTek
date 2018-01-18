namespace CreateFamilyPart
{
    partial class ProcessDlg
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
            this.Old_SGC_Panel = new DevComponents.DotNetBar.SuperGrid.SuperGridControl();
            this.gridColumn1 = new DevComponents.DotNetBar.SuperGrid.GridColumn();
            this.gridColumn2 = new DevComponents.DotNetBar.SuperGrid.GridColumn();
            this.gridColumn3 = new DevComponents.DotNetBar.SuperGrid.GridColumn();
            this.Close = new DevComponents.DotNetBar.ButtonX();
            this.SuspendLayout();
            // 
            // styleManager1
            // 
            this.styleManager1.ManagerStyle = DevComponents.DotNetBar.eStyle.Windows7Blue;
            this.styleManager1.MetroColorParameters = new DevComponents.DotNetBar.Metro.ColorTables.MetroColorGeneratorParameters(System.Drawing.Color.White, System.Drawing.Color.FromArgb(((int)(((byte)(43)))), ((int)(((byte)(87)))), ((int)(((byte)(154))))));
            // 
            // Old_SGC_Panel
            // 
            this.Old_SGC_Panel.FilterExprColors.SysFunction = System.Drawing.Color.DarkRed;
            this.Old_SGC_Panel.Location = new System.Drawing.Point(12, 12);
            this.Old_SGC_Panel.Name = "Old_SGC_Panel";
            // 
            // 
            // 
            this.Old_SGC_Panel.PrimaryGrid.Columns.Add(this.gridColumn1);
            this.Old_SGC_Panel.PrimaryGrid.Columns.Add(this.gridColumn2);
            this.Old_SGC_Panel.PrimaryGrid.Columns.Add(this.gridColumn3);
            this.Old_SGC_Panel.PrimaryGrid.ShowRowHeaders = false;
            this.Old_SGC_Panel.Size = new System.Drawing.Size(364, 238);
            this.Old_SGC_Panel.TabIndex = 0;
            this.Old_SGC_Panel.Text = "superGridControl1";
            // 
            // gridColumn1
            // 
            this.gridColumn1.CellStyles.Default.Alignment = DevComponents.DotNetBar.SuperGrid.Style.Alignment.MiddleCenter;
            this.gridColumn1.EditorType = typeof(DevComponents.DotNetBar.SuperGrid.GridLabelXEditControl);
            this.gridColumn1.HeaderText = "製程序";
            this.gridColumn1.Name = "OP1";
            this.gridColumn1.Width = 70;
            // 
            // gridColumn2
            // 
            this.gridColumn2.EditorType = typeof(DevComponents.DotNetBar.SuperGrid.GridLabelXEditControl);
            this.gridColumn2.HeaderText = "製程別";
            this.gridColumn2.Name = "OP2";
            this.gridColumn2.Width = 180;
            // 
            // gridColumn3
            // 
            this.gridColumn3.AutoSizeMode = DevComponents.DotNetBar.SuperGrid.ColumnAutoSizeMode.Fill;
            this.gridColumn3.CellStyles.Default.Alignment = DevComponents.DotNetBar.SuperGrid.Style.Alignment.MiddleCenter;
            this.gridColumn3.EditorType = typeof(DevComponents.DotNetBar.SuperGrid.GridLabelXEditControl);
            this.gridColumn3.HeaderText = "ERP編號";
            this.gridColumn3.Name = "ERP";
            // 
            // Close
            // 
            this.Close.AccessibleRole = System.Windows.Forms.AccessibleRole.PushButton;
            this.Close.ColorTable = DevComponents.DotNetBar.eButtonColor.Flat;
            this.Close.Font = new System.Drawing.Font("標楷體", 9.75F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(136)));
            this.Close.Image = global::CreateFamilyPart.Properties.Resources.close_16px;
            this.Close.Location = new System.Drawing.Point(301, 256);
            this.Close.Name = "Close";
            this.Close.Size = new System.Drawing.Size(75, 37);
            this.Close.Style = DevComponents.DotNetBar.eDotNetBarStyle.StyleManagerControlled;
            this.Close.TabIndex = 1;
            this.Close.Text = "關閉";
            this.Close.Click += new System.EventHandler(this.Close_Click);
            // 
            // ProcessDlg
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 12F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.AutoSizeMode = System.Windows.Forms.AutoSizeMode.GrowAndShrink;
            this.ClientSize = new System.Drawing.Size(388, 305);
            this.Controls.Add(this.Close);
            this.Controls.Add(this.Old_SGC_Panel);
            this.DoubleBuffered = true;
            this.Name = "ProcessDlg";
            this.Text = "ProcessDlg";
            this.ResumeLayout(false);

        }

        #endregion

        private DevComponents.DotNetBar.StyleManager styleManager1;
        private DevComponents.DotNetBar.SuperGrid.SuperGridControl Old_SGC_Panel;
        private DevComponents.DotNetBar.ButtonX Close;
        private DevComponents.DotNetBar.SuperGrid.GridColumn gridColumn1;
        private DevComponents.DotNetBar.SuperGrid.GridColumn gridColumn2;
        private DevComponents.DotNetBar.SuperGrid.GridColumn gridColumn3;
    }
}