namespace PFMEA
{
    partial class Detection
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
            this.AddBtn = new DevComponents.DotNetBar.ButtonX();
            this.OK = new DevComponents.DotNetBar.ButtonX();
            this.labelX1 = new DevComponents.DotNetBar.LabelX();
            this.SGC_Detection = new DevComponents.DotNetBar.SuperGrid.SuperGridControl();
            this.gridColumn1 = new DevComponents.DotNetBar.SuperGrid.GridColumn();
            this.gridColumn2 = new DevComponents.DotNetBar.SuperGrid.GridColumn();
            this.Manual = new DevComponents.DotNetBar.Controls.TextBoxX();
            this.SuspendLayout();
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
            this.AddBtn.TabIndex = 0;
            this.AddBtn.Click += new System.EventHandler(this.AddBtn_Click);
            // 
            // OK
            // 
            this.OK.AccessibleRole = System.Windows.Forms.AccessibleRole.PushButton;
            this.OK.ColorTable = DevComponents.DotNetBar.eButtonColor.Flat;
            this.OK.Font = new System.Drawing.Font("標楷體", 12F);
            this.OK.Image = global::PFMEA.Properties.Resources.Confirm_24px;
            this.OK.Location = new System.Drawing.Point(377, 247);
            this.OK.Name = "OK";
            this.OK.Size = new System.Drawing.Size(121, 34);
            this.OK.Style = DevComponents.DotNetBar.eDotNetBarStyle.StyleManagerControlled;
            this.OK.TabIndex = 1;
            this.OK.Text = "確定/離開";
            this.OK.Click += new System.EventHandler(this.OK_Click);
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
            this.labelX1.TabIndex = 3;
            this.labelX1.Text = "手動新增";
            // 
            // SGC_Detection
            // 
            this.SGC_Detection.FilterExprColors.SysFunction = System.Drawing.Color.DarkRed;
            this.SGC_Detection.Location = new System.Drawing.Point(12, 12);
            this.SGC_Detection.Name = "SGC_Detection";
            // 
            // 
            // 
            this.SGC_Detection.PrimaryGrid.Columns.Add(this.gridColumn1);
            this.SGC_Detection.PrimaryGrid.Columns.Add(this.gridColumn2);
            this.SGC_Detection.PrimaryGrid.ShowRowHeaders = false;
            this.SGC_Detection.Size = new System.Drawing.Size(486, 229);
            this.SGC_Detection.TabIndex = 4;
            this.SGC_Detection.Text = "superGridControl1";
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
            this.gridColumn2.HeaderText = "檢測措施";
            this.gridColumn2.Name = "gridColumn2";
            // 
            // Manual
            // 
            // 
            // 
            // 
            this.Manual.Border.Class = "TextBoxBorder";
            this.Manual.Border.CornerType = DevComponents.DotNetBar.eCornerType.Square;
            this.Manual.Font = new System.Drawing.Font("新細明體", 12F);
            this.Manual.Location = new System.Drawing.Point(85, 254);
            this.Manual.Name = "Manual";
            this.Manual.PreventEnterBeep = true;
            this.Manual.Size = new System.Drawing.Size(185, 27);
            this.Manual.TabIndex = 5;
            // 
            // Detection
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 12F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.ClientSize = new System.Drawing.Size(510, 290);
            this.Controls.Add(this.Manual);
            this.Controls.Add(this.SGC_Detection);
            this.Controls.Add(this.labelX1);
            this.Controls.Add(this.OK);
            this.Controls.Add(this.AddBtn);
            this.DoubleBuffered = true;
            this.Name = "Detection";
            this.Text = "檢測措施";
            this.ResumeLayout(false);

        }

        #endregion

        private DevComponents.DotNetBar.ButtonX AddBtn;
        private DevComponents.DotNetBar.ButtonX OK;
        private DevComponents.DotNetBar.LabelX labelX1;
        private DevComponents.DotNetBar.SuperGrid.SuperGridControl SGC_Detection;
        private DevComponents.DotNetBar.Controls.TextBoxX Manual;
        private DevComponents.DotNetBar.SuperGrid.GridColumn gridColumn1;
        private DevComponents.DotNetBar.SuperGrid.GridColumn gridColumn2;
    }
}