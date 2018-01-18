namespace PostProcessor
{
    partial class PostProcessorDlg
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
            System.ComponentModel.ComponentResourceManager resources = new System.ComponentModel.ComponentResourceManager(typeof(PostProcessorDlg));
            this.styleManager1 = new DevComponents.DotNetBar.StyleManager(this.components);
            this.labelX1 = new DevComponents.DotNetBar.LabelX();
            this.comboBoxNCgroup = new DevComponents.DotNetBar.Controls.ComboBoxEx();
            this.groupBox1 = new System.Windows.Forms.GroupBox();
            this.SuperGridOperPanel = new DevComponents.DotNetBar.SuperGrid.SuperGridControl();
            this.SingleSelect = new DevComponents.DotNetBar.SuperGrid.GridColumn();
            this.OperName = new DevComponents.DotNetBar.SuperGrid.GridColumn();
            this.PostProcessor = new DevComponents.DotNetBar.SuperGrid.GridColumn();
            this.MachineNo = new DevComponents.DotNetBar.Controls.TextBoxX();
            this.groupBox3 = new System.Windows.Forms.GroupBox();
            this.ExtensionName = new DevComponents.DotNetBar.Controls.TextBoxX();
            this.labelX3 = new DevComponents.DotNetBar.LabelX();
            this.labelX2 = new DevComponents.DotNetBar.LabelX();
            this.SuperGridPostPanel = new DevComponents.DotNetBar.SuperGrid.SuperGridControl();
            this.PostProcessorName = new DevComponents.DotNetBar.SuperGrid.GridColumn();
            this.CloseDlg = new DevComponents.DotNetBar.ButtonX();
            this.Output = new DevComponents.DotNetBar.ButtonX();
            this.groupBox1.SuspendLayout();
            this.groupBox3.SuspendLayout();
            this.SuspendLayout();
            // 
            // styleManager1
            // 
            this.styleManager1.ManagerStyle = DevComponents.DotNetBar.eStyle.Windows7Blue;
            this.styleManager1.MetroColorParameters = new DevComponents.DotNetBar.Metro.ColorTables.MetroColorGeneratorParameters(System.Drawing.Color.White, System.Drawing.Color.FromArgb(((int)(((byte)(43)))), ((int)(((byte)(87)))), ((int)(((byte)(154))))));
            // 
            // labelX1
            // 
            this.labelX1.BackColor = System.Drawing.Color.Transparent;
            // 
            // 
            // 
            this.labelX1.BackgroundStyle.CornerType = DevComponents.DotNetBar.eCornerType.Square;
            this.labelX1.Font = new System.Drawing.Font("標楷體", 9.75F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(136)));
            this.labelX1.Location = new System.Drawing.Point(9, 21);
            this.labelX1.Name = "labelX1";
            this.labelX1.Size = new System.Drawing.Size(75, 23);
            this.labelX1.TabIndex = 0;
            this.labelX1.Text = "群組名稱：";
            // 
            // comboBoxNCgroup
            // 
            this.comboBoxNCgroup.DisplayMember = "Text";
            this.comboBoxNCgroup.DrawMode = System.Windows.Forms.DrawMode.OwnerDrawFixed;
            this.comboBoxNCgroup.DropDownStyle = System.Windows.Forms.ComboBoxStyle.DropDownList;
            this.comboBoxNCgroup.Font = new System.Drawing.Font("新細明體", 9F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(136)));
            this.comboBoxNCgroup.FormattingEnabled = true;
            this.comboBoxNCgroup.ItemHeight = 16;
            this.comboBoxNCgroup.Location = new System.Drawing.Point(79, 20);
            this.comboBoxNCgroup.Name = "comboBoxNCgroup";
            this.comboBoxNCgroup.Size = new System.Drawing.Size(180, 22);
            this.comboBoxNCgroup.Style = DevComponents.DotNetBar.eDotNetBarStyle.StyleManagerControlled;
            this.comboBoxNCgroup.TabIndex = 1;
            this.comboBoxNCgroup.SelectedIndexChanged += new System.EventHandler(this.comboBoxNCgroup_SelectedIndexChanged);
            // 
            // groupBox1
            // 
            this.groupBox1.Controls.Add(this.SuperGridOperPanel);
            this.groupBox1.Controls.Add(this.comboBoxNCgroup);
            this.groupBox1.Controls.Add(this.labelX1);
            this.groupBox1.Font = new System.Drawing.Font("標楷體", 9F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(136)));
            this.groupBox1.Location = new System.Drawing.Point(12, 9);
            this.groupBox1.Name = "groupBox1";
            this.groupBox1.Size = new System.Drawing.Size(265, 339);
            this.groupBox1.TabIndex = 2;
            this.groupBox1.TabStop = false;
            this.groupBox1.Text = "程式";
            // 
            // SuperGridOperPanel
            // 
            this.SuperGridOperPanel.FilterExprColors.SysFunction = System.Drawing.Color.DarkRed;
            this.SuperGridOperPanel.Location = new System.Drawing.Point(6, 50);
            this.SuperGridOperPanel.Name = "SuperGridOperPanel";
            // 
            // 
            // 
            this.SuperGridOperPanel.PrimaryGrid.Columns.Add(this.SingleSelect);
            this.SuperGridOperPanel.PrimaryGrid.Columns.Add(this.OperName);
            this.SuperGridOperPanel.PrimaryGrid.Columns.Add(this.PostProcessor);
            this.SuperGridOperPanel.PrimaryGrid.SelectionGranularity = DevComponents.DotNetBar.SuperGrid.SelectionGranularity.Row;
            this.SuperGridOperPanel.PrimaryGrid.ShowRowHeaders = false;
            this.SuperGridOperPanel.Size = new System.Drawing.Size(253, 283);
            this.SuperGridOperPanel.TabIndex = 2;
            this.SuperGridOperPanel.Text = "superGridControl1";
            this.SuperGridOperPanel.RowClick += new System.EventHandler<DevComponents.DotNetBar.SuperGrid.GridRowClickEventArgs>(this.SuperGridOperPanel_RowClick);
            // 
            // SingleSelect
            // 
            this.SingleSelect.CellStyles.Default.Alignment = DevComponents.DotNetBar.SuperGrid.Style.Alignment.MiddleCenter;
            this.SingleSelect.ColumnSortMode = DevComponents.DotNetBar.SuperGrid.ColumnSortMode.None;
            this.SingleSelect.EditorType = typeof(DevComponents.DotNetBar.SuperGrid.GridCheckBoxXEditControl);
            this.SingleSelect.Name = "挑選";
            this.SingleSelect.ResizeMode = DevComponents.DotNetBar.SuperGrid.ColumnResizeMode.None;
            this.SingleSelect.Visible = false;
            this.SingleSelect.Width = 40;
            // 
            // OperName
            // 
            this.OperName.CellStyles.Default.Alignment = DevComponents.DotNetBar.SuperGrid.Style.Alignment.MiddleCenter;
            this.OperName.ColumnSortMode = DevComponents.DotNetBar.SuperGrid.ColumnSortMode.None;
            this.OperName.EditorType = typeof(DevComponents.DotNetBar.SuperGrid.GridLabelXEditControl);
            this.OperName.Name = "程式名稱";
            this.OperName.ResizeMode = DevComponents.DotNetBar.SuperGrid.ColumnResizeMode.None;
            this.OperName.Width = 60;
            // 
            // PostProcessor
            // 
            this.PostProcessor.AutoSizeMode = DevComponents.DotNetBar.SuperGrid.ColumnAutoSizeMode.Fill;
            this.PostProcessor.CellStyles.Default.Alignment = DevComponents.DotNetBar.SuperGrid.Style.Alignment.MiddleLeft;
            this.PostProcessor.ColumnSortMode = DevComponents.DotNetBar.SuperGrid.ColumnSortMode.None;
            this.PostProcessor.Name = "後處理器";
            this.PostProcessor.ResizeMode = DevComponents.DotNetBar.SuperGrid.ColumnResizeMode.None;
            this.PostProcessor.Width = 180;
            // 
            // MachineNo
            // 
            // 
            // 
            // 
            this.MachineNo.Border.Class = "TextBoxBorder";
            this.MachineNo.Border.CornerType = DevComponents.DotNetBar.eCornerType.Square;
            this.MachineNo.Font = new System.Drawing.Font("新細明體", 9F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(136)));
            this.MachineNo.Location = new System.Drawing.Point(51, 20);
            this.MachineNo.Name = "MachineNo";
            this.MachineNo.PreventEnterBeep = true;
            this.MachineNo.Size = new System.Drawing.Size(108, 22);
            this.MachineNo.TabIndex = 3;
            // 
            // groupBox3
            // 
            this.groupBox3.Controls.Add(this.ExtensionName);
            this.groupBox3.Controls.Add(this.labelX3);
            this.groupBox3.Controls.Add(this.MachineNo);
            this.groupBox3.Controls.Add(this.labelX2);
            this.groupBox3.Controls.Add(this.SuperGridPostPanel);
            this.groupBox3.Font = new System.Drawing.Font("標楷體", 9F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(136)));
            this.groupBox3.Location = new System.Drawing.Point(283, 9);
            this.groupBox3.Name = "groupBox3";
            this.groupBox3.Size = new System.Drawing.Size(297, 339);
            this.groupBox3.TabIndex = 4;
            this.groupBox3.TabStop = false;
            this.groupBox3.Text = "後處理器";
            // 
            // ExtensionName
            // 
            // 
            // 
            // 
            this.ExtensionName.Border.Class = "TextBoxBorder";
            this.ExtensionName.Border.CornerType = DevComponents.DotNetBar.eCornerType.Square;
            this.ExtensionName.Font = new System.Drawing.Font("新細明體", 9F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(136)));
            this.ExtensionName.Location = new System.Drawing.Point(233, 20);
            this.ExtensionName.Name = "ExtensionName";
            this.ExtensionName.PreventEnterBeep = true;
            this.ExtensionName.Size = new System.Drawing.Size(58, 22);
            this.ExtensionName.TabIndex = 7;
            // 
            // labelX3
            // 
            // 
            // 
            // 
            this.labelX3.BackgroundStyle.CornerType = DevComponents.DotNetBar.eCornerType.Square;
            this.labelX3.Font = new System.Drawing.Font("標楷體", 9.75F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(136)));
            this.labelX3.Location = new System.Drawing.Point(175, 20);
            this.labelX3.Name = "labelX3";
            this.labelX3.Size = new System.Drawing.Size(75, 23);
            this.labelX3.TabIndex = 5;
            this.labelX3.Text = "副檔名：";
            // 
            // labelX2
            // 
            // 
            // 
            // 
            this.labelX2.BackgroundStyle.CornerType = DevComponents.DotNetBar.eCornerType.Square;
            this.labelX2.Font = new System.Drawing.Font("標楷體", 9.75F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(136)));
            this.labelX2.Location = new System.Drawing.Point(6, 20);
            this.labelX2.Name = "labelX2";
            this.labelX2.Size = new System.Drawing.Size(75, 23);
            this.labelX2.TabIndex = 4;
            this.labelX2.Text = "機台：";
            // 
            // SuperGridPostPanel
            // 
            this.SuperGridPostPanel.FilterExprColors.SysFunction = System.Drawing.Color.DarkRed;
            this.SuperGridPostPanel.Location = new System.Drawing.Point(6, 50);
            this.SuperGridPostPanel.Name = "SuperGridPostPanel";
            // 
            // 
            // 
            this.SuperGridPostPanel.PrimaryGrid.Columns.Add(this.PostProcessorName);
            this.SuperGridPostPanel.PrimaryGrid.MultiSelect = false;
            this.SuperGridPostPanel.PrimaryGrid.SelectionGranularity = DevComponents.DotNetBar.SuperGrid.SelectionGranularity.Row;
            this.SuperGridPostPanel.PrimaryGrid.ShowRowHeaders = false;
            this.SuperGridPostPanel.Size = new System.Drawing.Size(285, 283);
            this.SuperGridPostPanel.TabIndex = 0;
            this.SuperGridPostPanel.Text = "superGridControl2";
            this.SuperGridPostPanel.RowClick += new System.EventHandler<DevComponents.DotNetBar.SuperGrid.GridRowClickEventArgs>(this.SuperGridPostPanel_RowClick);
            this.SuperGridPostPanel.RowDoubleClick += new System.EventHandler<DevComponents.DotNetBar.SuperGrid.GridRowDoubleClickEventArgs>(this.SuperGridPostPanel_RowDoubleClick);
            // 
            // PostProcessorName
            // 
            this.PostProcessorName.AutoSizeMode = DevComponents.DotNetBar.SuperGrid.ColumnAutoSizeMode.Fill;
            this.PostProcessorName.CellStyles.Default.Alignment = DevComponents.DotNetBar.SuperGrid.Style.Alignment.MiddleLeft;
            this.PostProcessorName.ColumnSortMode = DevComponents.DotNetBar.SuperGrid.ColumnSortMode.None;
            this.PostProcessorName.EditorType = typeof(DevComponents.DotNetBar.SuperGrid.GridLabelXEditControl);
            this.PostProcessorName.Name = "後處理器名稱";
            this.PostProcessorName.Width = 280;
            // 
            // CloseDlg
            // 
            this.CloseDlg.AccessibleRole = System.Windows.Forms.AccessibleRole.PushButton;
            this.CloseDlg.ColorTable = DevComponents.DotNetBar.eButtonColor.Flat;
            this.CloseDlg.Font = new System.Drawing.Font("標楷體", 12F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(136)));
            this.CloseDlg.Image = global::PostProcessor.Properties.Resources.close_16px;
            this.CloseDlg.Location = new System.Drawing.Point(500, 354);
            this.CloseDlg.Name = "CloseDlg";
            this.CloseDlg.Size = new System.Drawing.Size(80, 33);
            this.CloseDlg.Style = DevComponents.DotNetBar.eDotNetBarStyle.StyleManagerControlled;
            this.CloseDlg.TabIndex = 6;
            this.CloseDlg.Text = "關閉";
            this.CloseDlg.Click += new System.EventHandler(this.CloseDlg_Click);
            // 
            // Output
            // 
            this.Output.AccessibleRole = System.Windows.Forms.AccessibleRole.PushButton;
            this.Output.ColorTable = DevComponents.DotNetBar.eButtonColor.Flat;
            this.Output.Font = new System.Drawing.Font("標楷體", 12F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(136)));
            this.Output.Image = global::PostProcessor.Properties.Resources.confirm_16px;
            this.Output.Location = new System.Drawing.Point(414, 354);
            this.Output.Name = "Output";
            this.Output.Size = new System.Drawing.Size(80, 33);
            this.Output.Style = DevComponents.DotNetBar.eDotNetBarStyle.StyleManagerControlled;
            this.Output.TabIndex = 5;
            this.Output.Text = "確認";
            this.Output.Click += new System.EventHandler(this.Output_Click);
            // 
            // PostProcessorDlg
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 12F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.AutoSizeMode = System.Windows.Forms.AutoSizeMode.GrowAndShrink;
            this.ClientSize = new System.Drawing.Size(591, 399);
            this.Controls.Add(this.CloseDlg);
            this.Controls.Add(this.Output);
            this.Controls.Add(this.groupBox3);
            this.Controls.Add(this.groupBox1);
            this.DoubleBuffered = true;
            this.Icon = ((System.Drawing.Icon)(resources.GetObject("$this.Icon")));
            this.Name = "PostProcessorDlg";
            this.Text = "PostProcessorDlg";
            this.Load += new System.EventHandler(this.PostProcessorDlg_Load);
            this.groupBox1.ResumeLayout(false);
            this.groupBox3.ResumeLayout(false);
            this.ResumeLayout(false);

        }

        #endregion

        private DevComponents.DotNetBar.StyleManager styleManager1;
        private DevComponents.DotNetBar.LabelX labelX1;
        private DevComponents.DotNetBar.Controls.ComboBoxEx comboBoxNCgroup;
        private System.Windows.Forms.GroupBox groupBox1;
        private DevComponents.DotNetBar.SuperGrid.SuperGridControl SuperGridOperPanel;
        private DevComponents.DotNetBar.SuperGrid.GridColumn OperName;
        private DevComponents.DotNetBar.SuperGrid.GridColumn PostProcessor;
        private System.Windows.Forms.GroupBox groupBox3;
        private DevComponents.DotNetBar.SuperGrid.SuperGridControl SuperGridPostPanel;
        private DevComponents.DotNetBar.SuperGrid.GridColumn PostProcessorName;
        private DevComponents.DotNetBar.ButtonX Output;
        private DevComponents.DotNetBar.ButtonX CloseDlg;
        private DevComponents.DotNetBar.SuperGrid.GridColumn SingleSelect;
        private DevComponents.DotNetBar.Controls.TextBoxX MachineNo;
        private DevComponents.DotNetBar.LabelX labelX2;
        private DevComponents.DotNetBar.Controls.TextBoxX ExtensionName;
        private DevComponents.DotNetBar.LabelX labelX3;
    }
}