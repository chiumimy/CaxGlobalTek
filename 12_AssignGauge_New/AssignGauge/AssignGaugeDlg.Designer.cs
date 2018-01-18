namespace AssignGauge
{
    partial class AssignGaugeDlg
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
            System.ComponentModel.ComponentResourceManager resources = new System.ComponentModel.ComponentResourceManager(typeof(AssignGaugeDlg));
            this.styleManager1 = new DevComponents.DotNetBar.StyleManager(this.components);
            this.SelfCheckGauge = new DevComponents.DotNetBar.Controls.ComboBoxEx();
            this.SameData = new System.Windows.Forms.CheckBox();
            this.SelfCheck_Units = new DevComponents.DotNetBar.Controls.ComboBoxEx();
            this.SelfCheck_1 = new System.Windows.Forms.TextBox();
            this.SelfCheck_0 = new System.Windows.Forms.TextBox();
            this.labelX6 = new DevComponents.DotNetBar.LabelX();
            this.labelX5 = new DevComponents.DotNetBar.LabelX();
            this.groupBox2 = new System.Windows.Forms.GroupBox();
            this.DimenChara = new DevComponents.DotNetBar.ButtonX();
            this.AbsCheDimen = new DevComponents.DotNetBar.ButtonX();
            this.RepairDimen = new DevComponents.DotNetBar.ButtonX();
            this.SelectDimen = new DevComponents.DotNetBar.ButtonX();
            this.ListSheet = new DevComponents.DotNetBar.Controls.ComboBoxEx();
            this.labelX1 = new DevComponents.DotNetBar.LabelX();
            this.chb_Remove = new System.Windows.Forms.CheckBox();
            this.chb_Assign = new System.Windows.Forms.CheckBox();
            this.groupBox1 = new System.Windows.Forms.GroupBox();
            this.FQCcheckBox = new DevComponents.DotNetBar.Controls.CheckBoxX();
            this.FAIcheckBox = new DevComponents.DotNetBar.Controls.CheckBoxX();
            this.IPQCcheckBox = new DevComponents.DotNetBar.Controls.CheckBoxX();
            this.IQCcheckBox = new DevComponents.DotNetBar.Controls.CheckBoxX();
            this.Freq_Units = new DevComponents.DotNetBar.Controls.ComboBoxEx();
            this.Freq_1 = new System.Windows.Forms.TextBox();
            this.Freq_0 = new System.Windows.Forms.TextBox();
            this.Gauge = new DevComponents.DotNetBar.Controls.ComboBoxEx();
            this.labelX7 = new DevComponents.DotNetBar.LabelX();
            this.OK = new DevComponents.DotNetBar.ButtonX();
            this.Cancel = new DevComponents.DotNetBar.ButtonX();
            this.groupBox2.SuspendLayout();
            this.groupBox1.SuspendLayout();
            this.SuspendLayout();
            // 
            // styleManager1
            // 
            this.styleManager1.ManagerStyle = DevComponents.DotNetBar.eStyle.Windows7Blue;
            this.styleManager1.MetroColorParameters = new DevComponents.DotNetBar.Metro.ColorTables.MetroColorGeneratorParameters(System.Drawing.Color.White, System.Drawing.Color.FromArgb(((int)(((byte)(43)))), ((int)(((byte)(87)))), ((int)(((byte)(154))))));
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
            this.SelfCheckGauge.Location = new System.Drawing.Point(8, 130);
            this.SelfCheckGauge.Name = "SelfCheckGauge";
            this.SelfCheckGauge.Size = new System.Drawing.Size(230, 23);
            this.SelfCheckGauge.Style = DevComponents.DotNetBar.eDotNetBarStyle.StyleManagerControlled;
            this.SelfCheckGauge.TabIndex = 13;
            // 
            // SameData
            // 
            this.SameData.AutoSize = true;
            this.SameData.Location = new System.Drawing.Point(88, 110);
            this.SameData.Name = "SameData";
            this.SameData.Size = new System.Drawing.Size(54, 17);
            this.SameData.TabIndex = 12;
            this.SameData.Text = "同上";
            this.SameData.UseVisualStyleBackColor = true;
            this.SameData.CheckedChanged += new System.EventHandler(this.SameData_CheckedChanged);
            // 
            // SelfCheck_Units
            // 
            this.SelfCheck_Units.DisplayMember = "Text";
            this.SelfCheck_Units.DrawMode = System.Windows.Forms.DrawMode.OwnerDrawFixed;
            this.SelfCheck_Units.DropDownStyle = System.Windows.Forms.ComboBoxStyle.DropDownList;
            this.SelfCheck_Units.FormattingEnabled = true;
            this.SelfCheck_Units.ItemHeight = 17;
            this.SelfCheck_Units.Location = new System.Drawing.Point(128, 158);
            this.SelfCheck_Units.Name = "SelfCheck_Units";
            this.SelfCheck_Units.Size = new System.Drawing.Size(110, 23);
            this.SelfCheck_Units.Style = DevComponents.DotNetBar.eDotNetBarStyle.StyleManagerControlled;
            this.SelfCheck_Units.TabIndex = 11;
            // 
            // SelfCheck_1
            // 
            this.SelfCheck_1.Location = new System.Drawing.Point(79, 159);
            this.SelfCheck_1.Name = "SelfCheck_1";
            this.SelfCheck_1.Size = new System.Drawing.Size(43, 23);
            this.SelfCheck_1.TabIndex = 9;
            // 
            // SelfCheck_0
            // 
            this.SelfCheck_0.Location = new System.Drawing.Point(8, 159);
            this.SelfCheck_0.Name = "SelfCheck_0";
            this.SelfCheck_0.Size = new System.Drawing.Size(43, 23);
            this.SelfCheck_0.TabIndex = 8;
            // 
            // labelX6
            // 
            this.labelX6.BackColor = System.Drawing.Color.Transparent;
            // 
            // 
            // 
            this.labelX6.BackgroundStyle.CornerType = DevComponents.DotNetBar.eCornerType.Square;
            this.labelX6.Font = new System.Drawing.Font("Cambria", 9.75F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.labelX6.Location = new System.Drawing.Point(55, 158);
            this.labelX6.Name = "labelX6";
            this.labelX6.Size = new System.Drawing.Size(32, 23);
            this.labelX6.TabIndex = 4;
            this.labelX6.Text = "PC/";
            // 
            // labelX5
            // 
            this.labelX5.BackColor = System.Drawing.Color.Transparent;
            // 
            // 
            // 
            this.labelX5.BackgroundStyle.CornerType = DevComponents.DotNetBar.eCornerType.Square;
            this.labelX5.Font = new System.Drawing.Font("Cambria", 9.75F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.labelX5.Location = new System.Drawing.Point(14, 106);
            this.labelX5.Name = "labelX5";
            this.labelX5.Size = new System.Drawing.Size(75, 23);
            this.labelX5.TabIndex = 3;
            this.labelX5.Text = "SelfCheck：";
            // 
            // groupBox2
            // 
            this.groupBox2.BackColor = System.Drawing.Color.Transparent;
            this.groupBox2.Controls.Add(this.DimenChara);
            this.groupBox2.Controls.Add(this.AbsCheDimen);
            this.groupBox2.Controls.Add(this.RepairDimen);
            this.groupBox2.Controls.Add(this.SelectDimen);
            this.groupBox2.Controls.Add(this.ListSheet);
            this.groupBox2.Controls.Add(this.labelX1);
            this.groupBox2.Font = new System.Drawing.Font("標楷體", 9.75F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(136)));
            this.groupBox2.Location = new System.Drawing.Point(12, 12);
            this.groupBox2.Name = "groupBox2";
            this.groupBox2.Size = new System.Drawing.Size(247, 120);
            this.groupBox2.TabIndex = 0;
            this.groupBox2.TabStop = false;
            this.groupBox2.Text = "Selection";
            // 
            // DimenChara
            // 
            this.DimenChara.AccessibleRole = System.Windows.Forms.AccessibleRole.PushButton;
            this.DimenChara.ColorTable = DevComponents.DotNetBar.eButtonColor.Flat;
            this.DimenChara.Location = new System.Drawing.Point(127, 84);
            this.DimenChara.Name = "DimenChara";
            this.DimenChara.Size = new System.Drawing.Size(114, 28);
            this.DimenChara.Style = DevComponents.DotNetBar.eDotNetBarStyle.StyleManagerControlled;
            this.DimenChara.TabIndex = 7;
            this.DimenChara.Text = "Product";
            this.DimenChara.Click += new System.EventHandler(this.DimenChara_Click);
            // 
            // AbsCheDimen
            // 
            this.AbsCheDimen.AccessibleRole = System.Windows.Forms.AccessibleRole.PushButton;
            this.AbsCheDimen.ColorTable = DevComponents.DotNetBar.eButtonColor.Flat;
            this.AbsCheDimen.Location = new System.Drawing.Point(7, 84);
            this.AbsCheDimen.Name = "AbsCheDimen";
            this.AbsCheDimen.Size = new System.Drawing.Size(114, 28);
            this.AbsCheDimen.Style = DevComponents.DotNetBar.eDotNetBarStyle.StyleManagerControlled;
            this.AbsCheDimen.TabIndex = 6;
            this.AbsCheDimen.Text = "Key Chara.(0)";
            this.AbsCheDimen.Click += new System.EventHandler(this.AbsCheDimen_Click);
            // 
            // RepairDimen
            // 
            this.RepairDimen.AccessibleRole = System.Windows.Forms.AccessibleRole.PushButton;
            this.RepairDimen.ColorTable = DevComponents.DotNetBar.eButtonColor.Flat;
            this.RepairDimen.Location = new System.Drawing.Point(127, 50);
            this.RepairDimen.Name = "RepairDimen";
            this.RepairDimen.Size = new System.Drawing.Size(114, 28);
            this.RepairDimen.Style = DevComponents.DotNetBar.eDotNetBarStyle.StyleManagerControlled;
            this.RepairDimen.TabIndex = 5;
            this.RepairDimen.Text = "Remove(0)";
            this.RepairDimen.Click += new System.EventHandler(this.RepairDimen_Click);
            // 
            // SelectDimen
            // 
            this.SelectDimen.AccessibleRole = System.Windows.Forms.AccessibleRole.PushButton;
            this.SelectDimen.ColorTable = DevComponents.DotNetBar.eButtonColor.Flat;
            this.SelectDimen.Location = new System.Drawing.Point(7, 50);
            this.SelectDimen.Name = "SelectDimen";
            this.SelectDimen.Size = new System.Drawing.Size(114, 28);
            this.SelectDimen.Style = DevComponents.DotNetBar.eDotNetBarStyle.StyleManagerControlled;
            this.SelectDimen.TabIndex = 4;
            this.SelectDimen.Text = "Assign";
            this.SelectDimen.Click += new System.EventHandler(this.SelectDimen_Click);
            // 
            // ListSheet
            // 
            this.ListSheet.DisplayMember = "Text";
            this.ListSheet.DrawMode = System.Windows.Forms.DrawMode.OwnerDrawFixed;
            this.ListSheet.DropDownStyle = System.Windows.Forms.ComboBoxStyle.DropDownList;
            this.ListSheet.Font = new System.Drawing.Font("新細明體", 9.75F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(136)));
            this.ListSheet.FormattingEnabled = true;
            this.ListSheet.ItemHeight = 17;
            this.ListSheet.Location = new System.Drawing.Point(88, 19);
            this.ListSheet.Name = "ListSheet";
            this.ListSheet.Size = new System.Drawing.Size(148, 23);
            this.ListSheet.Style = DevComponents.DotNetBar.eDotNetBarStyle.StyleManagerControlled;
            this.ListSheet.TabIndex = 1;
            this.ListSheet.SelectedIndexChanged += new System.EventHandler(this.ListSheet_SelectedIndexChanged);
            // 
            // labelX1
            // 
            // 
            // 
            // 
            this.labelX1.BackgroundStyle.CornerType = DevComponents.DotNetBar.eCornerType.Square;
            this.labelX1.Location = new System.Drawing.Point(14, 21);
            this.labelX1.Name = "labelX1";
            this.labelX1.Size = new System.Drawing.Size(88, 23);
            this.labelX1.TabIndex = 0;
            this.labelX1.Text = "選擇圖紙：";
            // 
            // chb_Remove
            // 
            this.chb_Remove.AutoSize = true;
            this.chb_Remove.Font = new System.Drawing.Font("Cambria", 9.75F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.chb_Remove.Location = new System.Drawing.Point(394, 65);
            this.chb_Remove.Name = "chb_Remove";
            this.chb_Remove.Size = new System.Drawing.Size(71, 19);
            this.chb_Remove.TabIndex = 3;
            this.chb_Remove.Text = "Remove";
            this.chb_Remove.UseVisualStyleBackColor = true;
            this.chb_Remove.CheckedChanged += new System.EventHandler(this.chb_Remove_CheckedChanged);
            // 
            // chb_Assign
            // 
            this.chb_Assign.AutoSize = true;
            this.chb_Assign.Font = new System.Drawing.Font("Cambria", 9.75F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.chb_Assign.Location = new System.Drawing.Point(323, 65);
            this.chb_Assign.Name = "chb_Assign";
            this.chb_Assign.Size = new System.Drawing.Size(63, 19);
            this.chb_Assign.TabIndex = 2;
            this.chb_Assign.Text = "Assign";
            this.chb_Assign.UseVisualStyleBackColor = true;
            this.chb_Assign.CheckedChanged += new System.EventHandler(this.chb_Assign_CheckedChanged);
            // 
            // groupBox1
            // 
            this.groupBox1.BackColor = System.Drawing.Color.Transparent;
            this.groupBox1.Controls.Add(this.FQCcheckBox);
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
            this.groupBox1.Location = new System.Drawing.Point(281, 138);
            this.groupBox1.Name = "groupBox1";
            this.groupBox1.Size = new System.Drawing.Size(247, 194);
            this.groupBox1.TabIndex = 14;
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
            this.FQCcheckBox.Location = new System.Drawing.Point(184, 16);
            this.FQCcheckBox.Name = "FQCcheckBox";
            this.FQCcheckBox.Size = new System.Drawing.Size(58, 23);
            this.FQCcheckBox.Style = DevComponents.DotNetBar.eDotNetBarStyle.StyleManagerControlled;
            this.FQCcheckBox.TabIndex = 22;
            this.FQCcheckBox.Text = "FQC";
            this.FQCcheckBox.CheckedChanged += new System.EventHandler(this.FQCcheckBox_CheckedChanged);
            // 
            // FAIcheckBox
            // 
            this.FAIcheckBox.BackColor = System.Drawing.Color.Transparent;
            // 
            // 
            // 
            this.FAIcheckBox.BackgroundStyle.CornerType = DevComponents.DotNetBar.eCornerType.Square;
            this.FAIcheckBox.Font = new System.Drawing.Font("Cambria", 9.75F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.FAIcheckBox.Location = new System.Drawing.Point(26, 16);
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
            this.IPQCcheckBox.Location = new System.Drawing.Point(128, 16);
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
            this.IQCcheckBox.Location = new System.Drawing.Point(77, 16);
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
            this.Freq_Units.Location = new System.Drawing.Point(128, 68);
            this.Freq_Units.Name = "Freq_Units";
            this.Freq_Units.Size = new System.Drawing.Size(110, 23);
            this.Freq_Units.Style = DevComponents.DotNetBar.eDotNetBarStyle.StyleManagerControlled;
            this.Freq_Units.TabIndex = 18;
            this.Freq_Units.TextChanged += new System.EventHandler(this.Freq_Units_TextChanged);
            // 
            // Freq_1
            // 
            this.Freq_1.Location = new System.Drawing.Point(79, 68);
            this.Freq_1.Name = "Freq_1";
            this.Freq_1.Size = new System.Drawing.Size(43, 23);
            this.Freq_1.TabIndex = 17;
            this.Freq_1.TextChanged += new System.EventHandler(this.Freq_1_TextChanged);
            // 
            // Freq_0
            // 
            this.Freq_0.Location = new System.Drawing.Point(8, 68);
            this.Freq_0.Name = "Freq_0";
            this.Freq_0.Size = new System.Drawing.Size(43, 23);
            this.Freq_0.TabIndex = 16;
            this.Freq_0.TextChanged += new System.EventHandler(this.Freq_0_TextChanged);
            // 
            // Gauge
            // 
            this.Gauge.DisplayMember = "Text";
            this.Gauge.DrawMode = System.Windows.Forms.DrawMode.OwnerDrawFixed;
            this.Gauge.DropDownStyle = System.Windows.Forms.ComboBoxStyle.DropDownList;
            this.Gauge.Font = new System.Drawing.Font("新細明體", 9.75F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(136)));
            this.Gauge.FormattingEnabled = true;
            this.Gauge.ItemHeight = 17;
            this.Gauge.Location = new System.Drawing.Point(8, 40);
            this.Gauge.Name = "Gauge";
            this.Gauge.Size = new System.Drawing.Size(230, 23);
            this.Gauge.Style = DevComponents.DotNetBar.eDotNetBarStyle.StyleManagerControlled;
            this.Gauge.TabIndex = 15;
            this.Gauge.SelectedIndexChanged += new System.EventHandler(this.Gauge_SelectedIndexChanged);
            // 
            // labelX7
            // 
            this.labelX7.BackColor = System.Drawing.Color.Transparent;
            // 
            // 
            // 
            this.labelX7.BackgroundStyle.CornerType = DevComponents.DotNetBar.eCornerType.Square;
            this.labelX7.Font = new System.Drawing.Font("Cambria", 9.75F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.labelX7.Location = new System.Drawing.Point(54, 67);
            this.labelX7.Name = "labelX7";
            this.labelX7.Size = new System.Drawing.Size(32, 23);
            this.labelX7.TabIndex = 2;
            this.labelX7.Text = "PC/";
            // 
            // OK
            // 
            this.OK.AccessibleRole = System.Windows.Forms.AccessibleRole.PushButton;
            this.OK.ColorTable = DevComponents.DotNetBar.eButtonColor.Flat;
            this.OK.Font = new System.Drawing.Font("標楷體", 9.75F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(136)));
            this.OK.Image = global::AssignGauge.Properties.Resources.confirm_16px;
            this.OK.Location = new System.Drawing.Point(100, 138);
            this.OK.Name = "OK";
            this.OK.Size = new System.Drawing.Size(75, 30);
            this.OK.Style = DevComponents.DotNetBar.eDotNetBarStyle.StyleManagerControlled;
            this.OK.TabIndex = 1;
            this.OK.Text = "確認";
            this.OK.Click += new System.EventHandler(this.OK_Click);
            // 
            // Cancel
            // 
            this.Cancel.AccessibleRole = System.Windows.Forms.AccessibleRole.PushButton;
            this.Cancel.ColorTable = DevComponents.DotNetBar.eButtonColor.Flat;
            this.Cancel.Font = new System.Drawing.Font("標楷體", 9.75F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(136)));
            this.Cancel.Image = global::AssignGauge.Properties.Resources.close_16px;
            this.Cancel.Location = new System.Drawing.Point(184, 138);
            this.Cancel.Name = "Cancel";
            this.Cancel.Size = new System.Drawing.Size(75, 30);
            this.Cancel.Style = DevComponents.DotNetBar.eDotNetBarStyle.StyleManagerControlled;
            this.Cancel.TabIndex = 2;
            this.Cancel.Text = "關閉";
            this.Cancel.Click += new System.EventHandler(this.Cancel_Click);
            // 
            // AssignGaugeDlg
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 12F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.AutoSizeMode = System.Windows.Forms.AutoSizeMode.GrowAndShrink;
            this.ClientSize = new System.Drawing.Size(274, 179);
            this.Controls.Add(this.groupBox2);
            this.Controls.Add(this.groupBox1);
            this.Controls.Add(this.OK);
            this.Controls.Add(this.chb_Remove);
            this.Controls.Add(this.chb_Assign);
            this.Controls.Add(this.Cancel);
            this.DoubleBuffered = true;
            this.EnableGlass = false;
            this.Icon = ((System.Drawing.Icon)(resources.GetObject("$this.Icon")));
            this.Name = "AssignGaugeDlg";
            this.Text = "尺寸特性";
            this.Load += new System.EventHandler(this.AssignGaugeDlg_Load);
            this.groupBox2.ResumeLayout(false);
            this.groupBox1.ResumeLayout(false);
            this.groupBox1.PerformLayout();
            this.ResumeLayout(false);
            this.PerformLayout();

        }

        #endregion

        private DevComponents.DotNetBar.StyleManager styleManager1;
        private System.Windows.Forms.TextBox SelfCheck_1;
        private System.Windows.Forms.TextBox SelfCheck_0;
        private DevComponents.DotNetBar.LabelX labelX6;
        private DevComponents.DotNetBar.LabelX labelX5;
        private DevComponents.DotNetBar.Controls.ComboBoxEx SelfCheck_Units;
        private System.Windows.Forms.CheckBox SameData;
        private DevComponents.DotNetBar.Controls.ComboBoxEx SelfCheckGauge;
        private DevComponents.DotNetBar.ButtonX Cancel;
        private DevComponents.DotNetBar.ButtonX OK;
        private System.Windows.Forms.GroupBox groupBox2;
        private DevComponents.DotNetBar.ButtonX SelectDimen;
        private System.Windows.Forms.CheckBox chb_Remove;
        private System.Windows.Forms.CheckBox chb_Assign;
        private DevComponents.DotNetBar.Controls.ComboBoxEx ListSheet;
        private DevComponents.DotNetBar.LabelX labelX1;
        private System.Windows.Forms.GroupBox groupBox1;
        private DevComponents.DotNetBar.Controls.ComboBoxEx Freq_Units;
        private System.Windows.Forms.TextBox Freq_1;
        private System.Windows.Forms.TextBox Freq_0;
        private DevComponents.DotNetBar.Controls.ComboBoxEx Gauge;
        private DevComponents.DotNetBar.LabelX labelX7;
        private DevComponents.DotNetBar.Controls.CheckBoxX FQCcheckBox;
        private DevComponents.DotNetBar.Controls.CheckBoxX FAIcheckBox;
        private DevComponents.DotNetBar.Controls.CheckBoxX IPQCcheckBox;
        private DevComponents.DotNetBar.Controls.CheckBoxX IQCcheckBox;
        private DevComponents.DotNetBar.ButtonX DimenChara;
        private DevComponents.DotNetBar.ButtonX AbsCheDimen;
        private DevComponents.DotNetBar.ButtonX RepairDimen;
    }
}