namespace ExportToolList
{
    partial class ExportToolListDlg
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
            System.ComponentModel.ComponentResourceManager resources = new System.ComponentModel.ComponentResourceManager(typeof(ExportToolListDlg));
            this.styleManager1 = new DevComponents.DotNetBar.StyleManager(this.components);
            this.SGC = new DevComponents.DotNetBar.SuperGrid.SuperGridControl();
            this.gridColumn2 = new DevComponents.DotNetBar.SuperGrid.GridColumn();
            this.gridColumn14 = new DevComponents.DotNetBar.SuperGrid.GridColumn();
            this.gridColumn9 = new DevComponents.DotNetBar.SuperGrid.GridColumn();
            this.gridColumn10 = new DevComponents.DotNetBar.SuperGrid.GridColumn();
            this.gridColumn11 = new DevComponents.DotNetBar.SuperGrid.GridColumn();
            this.gridColumn12 = new DevComponents.DotNetBar.SuperGrid.GridColumn();
            this.gridColumn13 = new DevComponents.DotNetBar.SuperGrid.GridColumn();
            this.gridColumn3 = new DevComponents.DotNetBar.SuperGrid.GridColumn();
            this.comboBoxNCName = new DevComponents.DotNetBar.Controls.ComboBoxEx();
            this.Cancel = new DevComponents.DotNetBar.ButtonX();
            this.OK = new DevComponents.DotNetBar.ButtonX();
            this.labelX1 = new DevComponents.DotNetBar.LabelX();
            this.productNo = new DevComponents.DotNetBar.Controls.TextBoxX();
            this.ois = new DevComponents.DotNetBar.Controls.TextBoxX();
            this.SuspendLayout();
            // 
            // styleManager1
            // 
            this.styleManager1.ManagerStyle = DevComponents.DotNetBar.eStyle.Windows7Blue;
            this.styleManager1.MetroColorParameters = new DevComponents.DotNetBar.Metro.ColorTables.MetroColorGeneratorParameters(System.Drawing.Color.White, System.Drawing.Color.FromArgb(((int)(((byte)(43)))), ((int)(((byte)(87)))), ((int)(((byte)(154))))));
            // 
            // SGC
            // 
            this.SGC.FilterExprColors.SysFunction = System.Drawing.Color.DarkRed;
            this.SGC.Location = new System.Drawing.Point(12, 50);
            this.SGC.Name = "SGC";
            // 
            // 
            // 
            this.SGC.PrimaryGrid.Columns.Add(this.gridColumn2);
            this.SGC.PrimaryGrid.Columns.Add(this.gridColumn9);
            this.SGC.PrimaryGrid.Columns.Add(this.gridColumn10);
            this.SGC.PrimaryGrid.Columns.Add(this.gridColumn11);
            this.SGC.PrimaryGrid.Columns.Add(this.gridColumn12);
            this.SGC.PrimaryGrid.Columns.Add(this.gridColumn13);
            this.SGC.PrimaryGrid.Columns.Add(this.gridColumn14);
            this.SGC.PrimaryGrid.Columns.Add(this.gridColumn3);
            this.SGC.PrimaryGrid.ShowRowHeaders = false;
            this.SGC.Size = new System.Drawing.Size(848, 249);
            this.SGC.TabIndex = 2;
            this.SGC.Text = "superGridControl1";
            // 
            // gridColumn2
            // 
            this.gridColumn2.CellStyles.Default.Alignment = DevComponents.DotNetBar.SuperGrid.Style.Alignment.MiddleCenter;
            this.gridColumn2.EditorType = typeof(DevComponents.DotNetBar.SuperGrid.GridLabelXEditControl);
            this.gridColumn2.HeaderText = "刀號";
            this.gridColumn2.Name = "刀號";
            this.gridColumn2.Width = 40;
            // 
            // gridColumn14
            // 
            this.gridColumn14.EditorType = typeof(DevComponents.DotNetBar.SuperGrid.GridLabelXEditControl);
            this.gridColumn14.Name = "規格";
            this.gridColumn14.Width = 180;
            // 
            // gridColumn9
            // 
            this.gridColumn9.EditorType = typeof(DevComponents.DotNetBar.SuperGrid.GridLabelXEditControl);
            this.gridColumn9.Name = "ERP編號";
            this.gridColumn9.Width = 130;
            // 
            // gridColumn10
            // 
            this.gridColumn10.EditorType = typeof(DevComponents.DotNetBar.SuperGrid.GridLabelXEditControl);
            this.gridColumn10.Name = "刀具用量";
            this.gridColumn10.Width = 70;
            // 
            // gridColumn11
            // 
            this.gridColumn11.EditorType = typeof(DevComponents.DotNetBar.SuperGrid.GridLabelXEditControl);
            this.gridColumn11.Name = "刀具壽命";
            this.gridColumn11.Width = 70;
            // 
            // gridColumn12
            // 
            this.gridColumn12.EditorType = typeof(DevComponents.DotNetBar.SuperGrid.GridLabelXEditControl);
            this.gridColumn12.Name = "可用刃數";
            this.gridColumn12.Width = 70;
            // 
            // gridColumn13
            // 
            this.gridColumn13.EditorType = typeof(DevComponents.DotNetBar.SuperGrid.GridLabelXEditControl);
            this.gridColumn13.Name = "品名";
            this.gridColumn13.Width = 180;
            // 
            // gridColumn3
            // 
            this.gridColumn3.AutoSizeMode = DevComponents.DotNetBar.SuperGrid.ColumnAutoSizeMode.Fill;
            this.gridColumn3.CellStyles.Default.Alignment = DevComponents.DotNetBar.SuperGrid.Style.Alignment.MiddleLeft;
            this.gridColumn3.EditorType = typeof(DevComponents.DotNetBar.SuperGrid.GridLabelXEditControl);
            this.gridColumn3.Name = "備註";
            // 
            // comboBoxNCName
            // 
            this.comboBoxNCName.DisplayMember = "Text";
            this.comboBoxNCName.DrawMode = System.Windows.Forms.DrawMode.OwnerDrawFixed;
            this.comboBoxNCName.DropDownStyle = System.Windows.Forms.ComboBoxStyle.DropDownList;
            this.comboBoxNCName.FormattingEnabled = true;
            this.comboBoxNCName.ItemHeight = 16;
            this.comboBoxNCName.Location = new System.Drawing.Point(133, 12);
            this.comboBoxNCName.Name = "comboBoxNCName";
            this.comboBoxNCName.Size = new System.Drawing.Size(121, 22);
            this.comboBoxNCName.Style = DevComponents.DotNetBar.eDotNetBarStyle.StyleManagerControlled;
            this.comboBoxNCName.TabIndex = 5;
            this.comboBoxNCName.SelectedIndexChanged += new System.EventHandler(this.comboBoxNCName_SelectedIndexChanged);
            // 
            // Cancel
            // 
            this.Cancel.AccessibleRole = System.Windows.Forms.AccessibleRole.PushButton;
            this.Cancel.ColorTable = DevComponents.DotNetBar.eButtonColor.Flat;
            this.Cancel.Font = new System.Drawing.Font("標楷體", 12F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(136)));
            this.Cancel.Image = global::ExportToolList.Properties.Resources.close_16px;
            this.Cancel.Location = new System.Drawing.Point(785, 305);
            this.Cancel.Name = "Cancel";
            this.Cancel.Size = new System.Drawing.Size(75, 32);
            this.Cancel.Style = DevComponents.DotNetBar.eDotNetBarStyle.StyleManagerControlled;
            this.Cancel.TabIndex = 4;
            this.Cancel.Text = "關閉";
            this.Cancel.Click += new System.EventHandler(this.Cancel_Click);
            // 
            // OK
            // 
            this.OK.AccessibleRole = System.Windows.Forms.AccessibleRole.PushButton;
            this.OK.ColorTable = DevComponents.DotNetBar.eButtonColor.Flat;
            this.OK.Font = new System.Drawing.Font("標楷體", 12F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(136)));
            this.OK.Image = global::ExportToolList.Properties.Resources.confirm_16px;
            this.OK.Location = new System.Drawing.Point(704, 305);
            this.OK.Name = "OK";
            this.OK.Size = new System.Drawing.Size(75, 32);
            this.OK.Style = DevComponents.DotNetBar.eDotNetBarStyle.StyleManagerControlled;
            this.OK.TabIndex = 3;
            this.OK.Text = "輸出";
            this.OK.Click += new System.EventHandler(this.OK_Click);
            // 
            // labelX1
            // 
            // 
            // 
            // 
            this.labelX1.BackgroundStyle.CornerType = DevComponents.DotNetBar.eCornerType.Square;
            this.labelX1.Font = new System.Drawing.Font("標楷體", 12F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(136)));
            this.labelX1.Image = global::ExportToolList.Properties.Resources.list_24px;
            this.labelX1.Location = new System.Drawing.Point(12, 12);
            this.labelX1.Name = "labelX1";
            this.labelX1.Size = new System.Drawing.Size(124, 23);
            this.labelX1.TabIndex = 0;
            this.labelX1.Text = "群組名稱：";
            this.labelX1.TextAlignment = System.Drawing.StringAlignment.Center;
            // 
            // productNo
            // 
            // 
            // 
            // 
            this.productNo.Border.Class = "TextBoxBorder";
            this.productNo.Border.CornerType = DevComponents.DotNetBar.eCornerType.Square;
            this.productNo.Location = new System.Drawing.Point(12, 311);
            this.productNo.Name = "productNo";
            this.productNo.PreventEnterBeep = true;
            this.productNo.Size = new System.Drawing.Size(124, 22);
            this.productNo.TabIndex = 6;
            this.productNo.WatermarkText = "輸入產品料號";
            // 
            // ois
            // 
            // 
            // 
            // 
            this.ois.Border.Class = "TextBoxBorder";
            this.ois.Border.CornerType = DevComponents.DotNetBar.eCornerType.Square;
            this.ois.Location = new System.Drawing.Point(142, 311);
            this.ois.Name = "ois";
            this.ois.PreventEnterBeep = true;
            this.ois.Size = new System.Drawing.Size(100, 22);
            this.ois.TabIndex = 7;
            this.ois.WatermarkText = "輸入步序";
            // 
            // ExportToolListDlg
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 12F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.ClientSize = new System.Drawing.Size(872, 349);
            this.Controls.Add(this.ois);
            this.Controls.Add(this.productNo);
            this.Controls.Add(this.comboBoxNCName);
            this.Controls.Add(this.Cancel);
            this.Controls.Add(this.OK);
            this.Controls.Add(this.SGC);
            this.Controls.Add(this.labelX1);
            this.DoubleBuffered = true;
            this.Icon = ((System.Drawing.Icon)(resources.GetObject("$this.Icon")));
            this.Name = "ExportToolListDlg";
            this.Text = "刀具清單";
            this.Load += new System.EventHandler(this.ExportToolListDlg_Load);
            this.ResumeLayout(false);

        }

        #endregion

        private DevComponents.DotNetBar.StyleManager styleManager1;
        private DevComponents.DotNetBar.LabelX labelX1;
        private DevComponents.DotNetBar.SuperGrid.SuperGridControl SGC;
        private DevComponents.DotNetBar.SuperGrid.GridColumn gridColumn2;
        private DevComponents.DotNetBar.ButtonX OK;
        private DevComponents.DotNetBar.ButtonX Cancel;
        private DevComponents.DotNetBar.Controls.ComboBoxEx comboBoxNCName;
        private DevComponents.DotNetBar.Controls.TextBoxX productNo;
        private DevComponents.DotNetBar.Controls.TextBoxX ois;
        private DevComponents.DotNetBar.SuperGrid.GridColumn gridColumn14;
        private DevComponents.DotNetBar.SuperGrid.GridColumn gridColumn9;
        private DevComponents.DotNetBar.SuperGrid.GridColumn gridColumn10;
        private DevComponents.DotNetBar.SuperGrid.GridColumn gridColumn11;
        private DevComponents.DotNetBar.SuperGrid.GridColumn gridColumn12;
        private DevComponents.DotNetBar.SuperGrid.GridColumn gridColumn13;
        private DevComponents.DotNetBar.SuperGrid.GridColumn gridColumn3;
    }
}