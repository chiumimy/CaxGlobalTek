namespace AssignGauge
{
    partial class Inspection
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
            this.groupBox1 = new System.Windows.Forms.GroupBox();
            this.FQCcheckBox = new DevComponents.DotNetBar.Controls.CheckBoxX();
            this.SelDimen = new DevComponents.DotNetBar.ButtonX();
            this.FAIcheckBox = new DevComponents.DotNetBar.Controls.CheckBoxX();
            this.IPQCcheckBox = new DevComponents.DotNetBar.Controls.CheckBoxX();
            this.IQCcheckBox = new DevComponents.DotNetBar.Controls.CheckBoxX();
            this.Freq_Units = new DevComponents.DotNetBar.Controls.ComboBoxEx();
            this.Freq_1 = new System.Windows.Forms.TextBox();
            this.Freq_0 = new System.Windows.Forms.TextBox();
            this.Gauge = new DevComponents.DotNetBar.Controls.ComboBoxEx();
            this.SameData = new System.Windows.Forms.CheckBox();
            this.SelfCheck_1 = new System.Windows.Forms.TextBox();
            this.labelX6 = new DevComponents.DotNetBar.LabelX();
            this.SelfCheck_0 = new System.Windows.Forms.TextBox();
            this.SelfCheck_Units = new DevComponents.DotNetBar.Controls.ComboBoxEx();
            this.labelX5 = new DevComponents.DotNetBar.LabelX();
            this.labelX7 = new DevComponents.DotNetBar.LabelX();
            this.SelfCheckGauge = new DevComponents.DotNetBar.Controls.ComboBoxEx();
            this.Ins_Close = new DevComponents.DotNetBar.ButtonX();
            this.Ins_OK = new DevComponents.DotNetBar.ButtonX();
            this.groupBox1.SuspendLayout();
            this.SuspendLayout();
            // 
            // styleManager1
            // 
            this.styleManager1.ManagerStyle = DevComponents.DotNetBar.eStyle.Windows7Blue;
            this.styleManager1.MetroColorParameters = new DevComponents.DotNetBar.Metro.ColorTables.MetroColorGeneratorParameters(System.Drawing.Color.White, System.Drawing.Color.FromArgb(((int)(((byte)(43)))), ((int)(((byte)(87)))), ((int)(((byte)(154))))));
            // 
            // groupBox1
            // 
            this.groupBox1.BackColor = System.Drawing.Color.Transparent;
            this.groupBox1.Controls.Add(this.FQCcheckBox);
            this.groupBox1.Controls.Add(this.SelDimen);
            this.groupBox1.Controls.Add(this.FAIcheckBox);
            this.groupBox1.Controls.Add(this.IPQCcheckBox);
            this.groupBox1.Controls.Add(this.IQCcheckBox);
            this.groupBox1.Controls.Add(this.Freq_Units);
            this.groupBox1.Controls.Add(this.Freq_1);
            this.groupBox1.Controls.Add(this.Freq_0);
            this.groupBox1.Controls.Add(this.Gauge);
            this.groupBox1.Controls.Add(this.SameData);
            this.groupBox1.Controls.Add(this.SelfCheck_1);
            this.groupBox1.Controls.Add(this.labelX6);
            this.groupBox1.Controls.Add(this.SelfCheck_0);
            this.groupBox1.Controls.Add(this.SelfCheck_Units);
            this.groupBox1.Controls.Add(this.labelX5);
            this.groupBox1.Controls.Add(this.labelX7);
            this.groupBox1.Controls.Add(this.SelfCheckGauge);
            this.groupBox1.Font = new System.Drawing.Font("標楷體", 9.75F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(136)));
            this.groupBox1.Location = new System.Drawing.Point(12, 12);
            this.groupBox1.Name = "groupBox1";
            this.groupBox1.Size = new System.Drawing.Size(264, 242);
            this.groupBox1.TabIndex = 15;
            this.groupBox1.TabStop = false;
            this.groupBox1.Text = "選擇檢具/頻率";
            // 
            // FQCcheckBox
            // 
            this.FQCcheckBox.BackColor = System.Drawing.Color.Transparent;
            // 
            // 
            // 
            this.FQCcheckBox.BackgroundStyle.CornerType = DevComponents.DotNetBar.eCornerType.Square;
            this.FQCcheckBox.Font = new System.Drawing.Font("Cambria", 9.75F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.FQCcheckBox.Location = new System.Drawing.Point(207, 51);
            this.FQCcheckBox.Name = "FQCcheckBox";
            this.FQCcheckBox.Size = new System.Drawing.Size(58, 23);
            this.FQCcheckBox.Style = DevComponents.DotNetBar.eDotNetBarStyle.StyleManagerControlled;
            this.FQCcheckBox.TabIndex = 22;
            this.FQCcheckBox.Text = "FQC";
            this.FQCcheckBox.CheckedChanged += new System.EventHandler(this.FQCcheckBox_CheckedChanged);
            // 
            // SelDimen
            // 
            this.SelDimen.AccessibleRole = System.Windows.Forms.AccessibleRole.PushButton;
            this.SelDimen.ColorTable = DevComponents.DotNetBar.eButtonColor.Flat;
            this.SelDimen.Image = global::AssignGauge.Properties.Resources.Rulers_24px;
            this.SelDimen.Location = new System.Drawing.Point(15, 22);
            this.SelDimen.Name = "SelDimen";
            this.SelDimen.Size = new System.Drawing.Size(131, 52);
            this.SelDimen.Style = DevComponents.DotNetBar.eDotNetBarStyle.StyleManagerControlled;
            this.SelDimen.TabIndex = 0;
            this.SelDimen.Text = "選擇尺寸(0)";
            this.SelDimen.Click += new System.EventHandler(this.SelDimen_Click);
            // 
            // FAIcheckBox
            // 
            this.FAIcheckBox.BackColor = System.Drawing.Color.Transparent;
            // 
            // 
            // 
            this.FAIcheckBox.BackgroundStyle.CornerType = DevComponents.DotNetBar.eCornerType.Square;
            this.FAIcheckBox.Font = new System.Drawing.Font("Cambria", 9.75F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.FAIcheckBox.Location = new System.Drawing.Point(151, 22);
            this.FAIcheckBox.Name = "FAIcheckBox";
            this.FAIcheckBox.Size = new System.Drawing.Size(43, 23);
            this.FAIcheckBox.Style = DevComponents.DotNetBar.eDotNetBarStyle.StyleManagerControlled;
            this.FAIcheckBox.TabIndex = 21;
            this.FAIcheckBox.Text = "FAI";
            this.FAIcheckBox.CheckedChanged += new System.EventHandler(this.FAIcheckBox_CheckedChanged);
            // 
            // IPQCcheckBox
            // 
            this.IPQCcheckBox.BackColor = System.Drawing.Color.Transparent;
            // 
            // 
            // 
            this.IPQCcheckBox.BackgroundStyle.CornerType = DevComponents.DotNetBar.eCornerType.Square;
            this.IPQCcheckBox.Font = new System.Drawing.Font("Cambria", 9.75F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.IPQCcheckBox.Location = new System.Drawing.Point(151, 51);
            this.IPQCcheckBox.Name = "IPQCcheckBox";
            this.IPQCcheckBox.Size = new System.Drawing.Size(54, 23);
            this.IPQCcheckBox.Style = DevComponents.DotNetBar.eDotNetBarStyle.StyleManagerControlled;
            this.IPQCcheckBox.TabIndex = 20;
            this.IPQCcheckBox.Text = "IPQC";
            this.IPQCcheckBox.CheckedChanged += new System.EventHandler(this.IPQCcheckBox_CheckedChanged);
            // 
            // IQCcheckBox
            // 
            this.IQCcheckBox.BackColor = System.Drawing.Color.Transparent;
            // 
            // 
            // 
            this.IQCcheckBox.BackgroundStyle.CornerType = DevComponents.DotNetBar.eCornerType.Square;
            this.IQCcheckBox.Font = new System.Drawing.Font("Cambria", 9.75F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.IQCcheckBox.Location = new System.Drawing.Point(207, 22);
            this.IQCcheckBox.Name = "IQCcheckBox";
            this.IQCcheckBox.Size = new System.Drawing.Size(43, 23);
            this.IQCcheckBox.Style = DevComponents.DotNetBar.eDotNetBarStyle.StyleManagerControlled;
            this.IQCcheckBox.TabIndex = 19;
            this.IQCcheckBox.Text = "IQC";
            this.IQCcheckBox.CheckedChanged += new System.EventHandler(this.IQCcheckBox_CheckedChanged);
            // 
            // Freq_Units
            // 
            this.Freq_Units.DisplayMember = "Text";
            this.Freq_Units.DrawMode = System.Windows.Forms.DrawMode.OwnerDrawFixed;
            this.Freq_Units.DropDownStyle = System.Windows.Forms.ComboBoxStyle.DropDownList;
            this.Freq_Units.FormattingEnabled = true;
            this.Freq_Units.ItemHeight = 17;
            this.Freq_Units.Location = new System.Drawing.Point(135, 115);
            this.Freq_Units.Name = "Freq_Units";
            this.Freq_Units.Size = new System.Drawing.Size(110, 23);
            this.Freq_Units.Style = DevComponents.DotNetBar.eDotNetBarStyle.StyleManagerControlled;
            this.Freq_Units.TabIndex = 18;
            // 
            // Freq_1
            // 
            this.Freq_1.Location = new System.Drawing.Point(86, 115);
            this.Freq_1.Name = "Freq_1";
            this.Freq_1.Size = new System.Drawing.Size(43, 23);
            this.Freq_1.TabIndex = 17;
            // 
            // Freq_0
            // 
            this.Freq_0.Location = new System.Drawing.Point(15, 115);
            this.Freq_0.Name = "Freq_0";
            this.Freq_0.Size = new System.Drawing.Size(43, 23);
            this.Freq_0.TabIndex = 16;
            // 
            // Gauge
            // 
            this.Gauge.DisplayMember = "Text";
            this.Gauge.DrawMode = System.Windows.Forms.DrawMode.OwnerDrawFixed;
            this.Gauge.DropDownStyle = System.Windows.Forms.ComboBoxStyle.DropDownList;
            this.Gauge.Font = new System.Drawing.Font("新細明體", 9.75F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(136)));
            this.Gauge.FormattingEnabled = true;
            this.Gauge.ItemHeight = 17;
            this.Gauge.Location = new System.Drawing.Point(15, 87);
            this.Gauge.Name = "Gauge";
            this.Gauge.Size = new System.Drawing.Size(230, 23);
            this.Gauge.Style = DevComponents.DotNetBar.eDotNetBarStyle.StyleManagerControlled;
            this.Gauge.TabIndex = 15;
            // 
            // SameData
            // 
            this.SameData.AutoSize = true;
            this.SameData.Location = new System.Drawing.Point(92, 154);
            this.SameData.Name = "SameData";
            this.SameData.Size = new System.Drawing.Size(54, 17);
            this.SameData.TabIndex = 12;
            this.SameData.Text = "同上";
            this.SameData.UseVisualStyleBackColor = true;
            this.SameData.CheckedChanged += new System.EventHandler(this.SameData_CheckedChanged);
            // 
            // SelfCheck_1
            // 
            this.SelfCheck_1.Location = new System.Drawing.Point(86, 208);
            this.SelfCheck_1.Name = "SelfCheck_1";
            this.SelfCheck_1.Size = new System.Drawing.Size(43, 23);
            this.SelfCheck_1.TabIndex = 9;
            // 
            // labelX6
            // 
            this.labelX6.BackColor = System.Drawing.Color.Transparent;
            // 
            // 
            // 
            this.labelX6.BackgroundStyle.CornerType = DevComponents.DotNetBar.eCornerType.Square;
            this.labelX6.Font = new System.Drawing.Font("Cambria", 9.75F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.labelX6.Location = new System.Drawing.Point(61, 208);
            this.labelX6.Name = "labelX6";
            this.labelX6.Size = new System.Drawing.Size(32, 23);
            this.labelX6.TabIndex = 4;
            this.labelX6.Text = "PC/";
            // 
            // SelfCheck_0
            // 
            this.SelfCheck_0.Location = new System.Drawing.Point(15, 208);
            this.SelfCheck_0.Name = "SelfCheck_0";
            this.SelfCheck_0.Size = new System.Drawing.Size(43, 23);
            this.SelfCheck_0.TabIndex = 8;
            // 
            // SelfCheck_Units
            // 
            this.SelfCheck_Units.DisplayMember = "Text";
            this.SelfCheck_Units.DrawMode = System.Windows.Forms.DrawMode.OwnerDrawFixed;
            this.SelfCheck_Units.DropDownStyle = System.Windows.Forms.ComboBoxStyle.DropDownList;
            this.SelfCheck_Units.FormattingEnabled = true;
            this.SelfCheck_Units.ItemHeight = 17;
            this.SelfCheck_Units.Location = new System.Drawing.Point(135, 208);
            this.SelfCheck_Units.Name = "SelfCheck_Units";
            this.SelfCheck_Units.Size = new System.Drawing.Size(110, 23);
            this.SelfCheck_Units.Style = DevComponents.DotNetBar.eDotNetBarStyle.StyleManagerControlled;
            this.SelfCheck_Units.TabIndex = 11;
            // 
            // labelX5
            // 
            this.labelX5.BackColor = System.Drawing.Color.Transparent;
            // 
            // 
            // 
            this.labelX5.BackgroundStyle.CornerType = DevComponents.DotNetBar.eCornerType.Square;
            this.labelX5.Font = new System.Drawing.Font("Cambria", 9.75F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.labelX5.Location = new System.Drawing.Point(18, 150);
            this.labelX5.Name = "labelX5";
            this.labelX5.Size = new System.Drawing.Size(75, 23);
            this.labelX5.TabIndex = 3;
            this.labelX5.Text = "SelfCheck：";
            // 
            // labelX7
            // 
            this.labelX7.BackColor = System.Drawing.Color.Transparent;
            // 
            // 
            // 
            this.labelX7.BackgroundStyle.CornerType = DevComponents.DotNetBar.eCornerType.Square;
            this.labelX7.Font = new System.Drawing.Font("Cambria", 9.75F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.labelX7.Location = new System.Drawing.Point(61, 114);
            this.labelX7.Name = "labelX7";
            this.labelX7.Size = new System.Drawing.Size(32, 23);
            this.labelX7.TabIndex = 2;
            this.labelX7.Text = "PC/";
            // 
            // SelfCheckGauge
            // 
            this.SelfCheckGauge.DisplayMember = "Text";
            this.SelfCheckGauge.DrawMode = System.Windows.Forms.DrawMode.OwnerDrawFixed;
            this.SelfCheckGauge.DropDownHeight = 220;
            this.SelfCheckGauge.DropDownStyle = System.Windows.Forms.ComboBoxStyle.DropDownList;
            this.SelfCheckGauge.Font = new System.Drawing.Font("新細明體", 9.75F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(136)));
            this.SelfCheckGauge.FormattingEnabled = true;
            this.SelfCheckGauge.IntegralHeight = false;
            this.SelfCheckGauge.ItemHeight = 17;
            this.SelfCheckGauge.Location = new System.Drawing.Point(15, 179);
            this.SelfCheckGauge.Name = "SelfCheckGauge";
            this.SelfCheckGauge.Size = new System.Drawing.Size(230, 23);
            this.SelfCheckGauge.Style = DevComponents.DotNetBar.eDotNetBarStyle.StyleManagerControlled;
            this.SelfCheckGauge.TabIndex = 13;
            // 
            // Ins_Close
            // 
            this.Ins_Close.AccessibleRole = System.Windows.Forms.AccessibleRole.PushButton;
            this.Ins_Close.ColorTable = DevComponents.DotNetBar.eButtonColor.Flat;
            this.Ins_Close.Font = new System.Drawing.Font("標楷體", 12F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(136)));
            this.Ins_Close.Image = global::AssignGauge.Properties.Resources.close_16px;
            this.Ins_Close.Location = new System.Drawing.Point(147, 260);
            this.Ins_Close.Name = "Ins_Close";
            this.Ins_Close.Size = new System.Drawing.Size(75, 34);
            this.Ins_Close.Style = DevComponents.DotNetBar.eDotNetBarStyle.StyleManagerControlled;
            this.Ins_Close.TabIndex = 17;
            this.Ins_Close.Text = "關閉";
            this.Ins_Close.Click += new System.EventHandler(this.Ins_Close_Click);
            // 
            // Ins_OK
            // 
            this.Ins_OK.AccessibleRole = System.Windows.Forms.AccessibleRole.PushButton;
            this.Ins_OK.ColorTable = DevComponents.DotNetBar.eButtonColor.Flat;
            this.Ins_OK.Font = new System.Drawing.Font("標楷體", 12F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(136)));
            this.Ins_OK.Image = global::AssignGauge.Properties.Resources.confirm_16px;
            this.Ins_OK.Location = new System.Drawing.Point(63, 260);
            this.Ins_OK.Name = "Ins_OK";
            this.Ins_OK.Size = new System.Drawing.Size(75, 34);
            this.Ins_OK.Style = DevComponents.DotNetBar.eDotNetBarStyle.StyleManagerControlled;
            this.Ins_OK.TabIndex = 16;
            this.Ins_OK.Text = "確認";
            this.Ins_OK.Click += new System.EventHandler(this.Ins_OK_Click);
            // 
            // Inspection
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 12F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.AutoSizeMode = System.Windows.Forms.AutoSizeMode.GrowAndShrink;
            this.ClientSize = new System.Drawing.Size(288, 305);
            this.Controls.Add(this.Ins_Close);
            this.Controls.Add(this.Ins_OK);
            this.Controls.Add(this.groupBox1);
            this.DoubleBuffered = true;
            this.Name = "Inspection";
            this.Text = "Inspection";
            this.FormClosed += new System.Windows.Forms.FormClosedEventHandler(this.Inspection_FormClosed);
            this.Load += new System.EventHandler(this.Inspection_Load);
            this.groupBox1.ResumeLayout(false);
            this.groupBox1.PerformLayout();
            this.ResumeLayout(false);

        }

        #endregion

        private DevComponents.DotNetBar.StyleManager styleManager1;
        private DevComponents.DotNetBar.ButtonX SelDimen;
        private System.Windows.Forms.GroupBox groupBox1;
        private DevComponents.DotNetBar.Controls.CheckBoxX FQCcheckBox;
        private DevComponents.DotNetBar.Controls.CheckBoxX FAIcheckBox;
        private DevComponents.DotNetBar.Controls.CheckBoxX IPQCcheckBox;
        private DevComponents.DotNetBar.Controls.CheckBoxX IQCcheckBox;
        private DevComponents.DotNetBar.Controls.ComboBoxEx Freq_Units;
        private System.Windows.Forms.TextBox Freq_1;
        private System.Windows.Forms.TextBox Freq_0;
        private DevComponents.DotNetBar.Controls.ComboBoxEx Gauge;
        private System.Windows.Forms.CheckBox SameData;
        private System.Windows.Forms.TextBox SelfCheck_1;
        private DevComponents.DotNetBar.LabelX labelX6;
        private System.Windows.Forms.TextBox SelfCheck_0;
        private DevComponents.DotNetBar.Controls.ComboBoxEx SelfCheck_Units;
        private DevComponents.DotNetBar.LabelX labelX5;
        private DevComponents.DotNetBar.LabelX labelX7;
        private DevComponents.DotNetBar.Controls.ComboBoxEx SelfCheckGauge;
        private DevComponents.DotNetBar.ButtonX Ins_OK;
        private DevComponents.DotNetBar.ButtonX Ins_Close;
    }
}