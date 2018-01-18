namespace OpenMOT
{
    partial class OpenMOTDlg
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
            this.labelX1 = new DevComponents.DotNetBar.LabelX();
            this.labelX2 = new DevComponents.DotNetBar.LabelX();
            this.labelX3 = new DevComponents.DotNetBar.LabelX();
            this.labelX4 = new DevComponents.DotNetBar.LabelX();
            this.labelX5 = new DevComponents.DotNetBar.LabelX();
            this.comboBoxCus = new DevComponents.DotNetBar.Controls.ComboBoxEx();
            this.comboBoxPartNo = new DevComponents.DotNetBar.Controls.ComboBoxEx();
            this.comboBoxCusVer = new DevComponents.DotNetBar.Controls.ComboBoxEx();
            this.comboBoxOpVer = new DevComponents.DotNetBar.Controls.ComboBoxEx();
            this.styleManager1 = new DevComponents.DotNetBar.StyleManager(this.components);
            this.Close = new DevComponents.DotNetBar.ButtonX();
            this.OK = new DevComponents.DotNetBar.ButtonX();
            this.labelX6 = new DevComponents.DotNetBar.LabelX();
            this.MOTName = new DevComponents.DotNetBar.LabelX();
            this.SuspendLayout();
            // 
            // labelX1
            // 
            // 
            // 
            // 
            this.labelX1.BackgroundStyle.CornerType = DevComponents.DotNetBar.eCornerType.Square;
            this.labelX1.Font = new System.Drawing.Font("標楷體", 12F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(136)));
            this.labelX1.Location = new System.Drawing.Point(12, 12);
            this.labelX1.Name = "labelX1";
            this.labelX1.Size = new System.Drawing.Size(142, 23);
            this.labelX1.TabIndex = 0;
            this.labelX1.Text = "工程師職稱：PE";
            // 
            // labelX2
            // 
            // 
            // 
            // 
            this.labelX2.BackgroundStyle.CornerType = DevComponents.DotNetBar.eCornerType.Square;
            this.labelX2.Font = new System.Drawing.Font("標楷體", 12F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(136)));
            this.labelX2.Location = new System.Drawing.Point(12, 41);
            this.labelX2.Name = "labelX2";
            this.labelX2.Size = new System.Drawing.Size(96, 23);
            this.labelX2.TabIndex = 1;
            this.labelX2.Text = "客戶：";
            // 
            // labelX3
            // 
            // 
            // 
            // 
            this.labelX3.BackgroundStyle.CornerType = DevComponents.DotNetBar.eCornerType.Square;
            this.labelX3.Font = new System.Drawing.Font("標楷體", 12F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(136)));
            this.labelX3.Location = new System.Drawing.Point(12, 70);
            this.labelX3.Name = "labelX3";
            this.labelX3.Size = new System.Drawing.Size(96, 23);
            this.labelX3.TabIndex = 2;
            this.labelX3.Text = "料號：";
            // 
            // labelX4
            // 
            // 
            // 
            // 
            this.labelX4.BackgroundStyle.CornerType = DevComponents.DotNetBar.eCornerType.Square;
            this.labelX4.Font = new System.Drawing.Font("標楷體", 12F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(136)));
            this.labelX4.Location = new System.Drawing.Point(12, 99);
            this.labelX4.Name = "labelX4";
            this.labelX4.Size = new System.Drawing.Size(96, 23);
            this.labelX4.TabIndex = 3;
            this.labelX4.Text = "客戶版次：";
            // 
            // labelX5
            // 
            // 
            // 
            // 
            this.labelX5.BackgroundStyle.CornerType = DevComponents.DotNetBar.eCornerType.Square;
            this.labelX5.Font = new System.Drawing.Font("標楷體", 12F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(136)));
            this.labelX5.Location = new System.Drawing.Point(12, 128);
            this.labelX5.Name = "labelX5";
            this.labelX5.Size = new System.Drawing.Size(96, 23);
            this.labelX5.TabIndex = 4;
            this.labelX5.Text = "製程版次：";
            // 
            // comboBoxCus
            // 
            this.comboBoxCus.DisplayMember = "Text";
            this.comboBoxCus.DrawMode = System.Windows.Forms.DrawMode.OwnerDrawFixed;
            this.comboBoxCus.DropDownStyle = System.Windows.Forms.ComboBoxStyle.DropDownList;
            this.comboBoxCus.FormattingEnabled = true;
            this.comboBoxCus.ItemHeight = 16;
            this.comboBoxCus.Location = new System.Drawing.Point(65, 41);
            this.comboBoxCus.Name = "comboBoxCus";
            this.comboBoxCus.Size = new System.Drawing.Size(207, 22);
            this.comboBoxCus.Style = DevComponents.DotNetBar.eDotNetBarStyle.StyleManagerControlled;
            this.comboBoxCus.TabIndex = 5;
            this.comboBoxCus.SelectedIndexChanged += new System.EventHandler(this.comboBoxCus_SelectedIndexChanged);
            // 
            // comboBoxPartNo
            // 
            this.comboBoxPartNo.DisplayMember = "Text";
            this.comboBoxPartNo.DrawMode = System.Windows.Forms.DrawMode.OwnerDrawFixed;
            this.comboBoxPartNo.DropDownStyle = System.Windows.Forms.ComboBoxStyle.DropDownList;
            this.comboBoxPartNo.FormattingEnabled = true;
            this.comboBoxPartNo.ItemHeight = 16;
            this.comboBoxPartNo.Location = new System.Drawing.Point(65, 71);
            this.comboBoxPartNo.Name = "comboBoxPartNo";
            this.comboBoxPartNo.Size = new System.Drawing.Size(207, 22);
            this.comboBoxPartNo.Style = DevComponents.DotNetBar.eDotNetBarStyle.StyleManagerControlled;
            this.comboBoxPartNo.TabIndex = 6;
            this.comboBoxPartNo.SelectedIndexChanged += new System.EventHandler(this.comboBoxPartNo_SelectedIndexChanged);
            // 
            // comboBoxCusVer
            // 
            this.comboBoxCusVer.DisplayMember = "Text";
            this.comboBoxCusVer.DrawMode = System.Windows.Forms.DrawMode.OwnerDrawFixed;
            this.comboBoxCusVer.DropDownStyle = System.Windows.Forms.ComboBoxStyle.DropDownList;
            this.comboBoxCusVer.FormattingEnabled = true;
            this.comboBoxCusVer.ItemHeight = 16;
            this.comboBoxCusVer.Location = new System.Drawing.Point(99, 100);
            this.comboBoxCusVer.Name = "comboBoxCusVer";
            this.comboBoxCusVer.Size = new System.Drawing.Size(173, 22);
            this.comboBoxCusVer.Style = DevComponents.DotNetBar.eDotNetBarStyle.StyleManagerControlled;
            this.comboBoxCusVer.TabIndex = 7;
            this.comboBoxCusVer.SelectedIndexChanged += new System.EventHandler(this.comboBoxCusVer_SelectedIndexChanged);
            // 
            // comboBoxOpVer
            // 
            this.comboBoxOpVer.DisplayMember = "Text";
            this.comboBoxOpVer.DrawMode = System.Windows.Forms.DrawMode.OwnerDrawFixed;
            this.comboBoxOpVer.DropDownStyle = System.Windows.Forms.ComboBoxStyle.DropDownList;
            this.comboBoxOpVer.FormattingEnabled = true;
            this.comboBoxOpVer.ItemHeight = 16;
            this.comboBoxOpVer.Location = new System.Drawing.Point(99, 129);
            this.comboBoxOpVer.Name = "comboBoxOpVer";
            this.comboBoxOpVer.Size = new System.Drawing.Size(173, 22);
            this.comboBoxOpVer.Style = DevComponents.DotNetBar.eDotNetBarStyle.StyleManagerControlled;
            this.comboBoxOpVer.TabIndex = 8;
            this.comboBoxOpVer.SelectedIndexChanged += new System.EventHandler(this.comboBoxOpVer_SelectedIndexChanged);
            // 
            // styleManager1
            // 
            this.styleManager1.ManagerStyle = DevComponents.DotNetBar.eStyle.Windows7Blue;
            this.styleManager1.MetroColorParameters = new DevComponents.DotNetBar.Metro.ColorTables.MetroColorGeneratorParameters(System.Drawing.Color.White, System.Drawing.Color.FromArgb(((int)(((byte)(43)))), ((int)(((byte)(87)))), ((int)(((byte)(154))))));
            // 
            // Close
            // 
            this.Close.AccessibleRole = System.Windows.Forms.AccessibleRole.PushButton;
            this.Close.ColorTable = DevComponents.DotNetBar.eButtonColor.Flat;
            this.Close.Font = new System.Drawing.Font("標楷體", 12F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(136)));
            this.Close.Image = global::OpenMOT.Properties.Resources.close_16px;
            this.Close.Location = new System.Drawing.Point(197, 215);
            this.Close.Name = "Close";
            this.Close.Size = new System.Drawing.Size(75, 34);
            this.Close.Style = DevComponents.DotNetBar.eDotNetBarStyle.StyleManagerControlled;
            this.Close.TabIndex = 11;
            this.Close.Text = "關閉";
            this.Close.Click += new System.EventHandler(this.Close_Click);
            // 
            // OK
            // 
            this.OK.AccessibleRole = System.Windows.Forms.AccessibleRole.PushButton;
            this.OK.ColorTable = DevComponents.DotNetBar.eButtonColor.Flat;
            this.OK.Font = new System.Drawing.Font("標楷體", 12F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(136)));
            this.OK.Image = global::OpenMOT.Properties.Resources.confirm_16px;
            this.OK.Location = new System.Drawing.Point(116, 215);
            this.OK.Name = "OK";
            this.OK.Size = new System.Drawing.Size(75, 34);
            this.OK.Style = DevComponents.DotNetBar.eDotNetBarStyle.StyleManagerControlled;
            this.OK.TabIndex = 10;
            this.OK.Text = "開啟";
            this.OK.Click += new System.EventHandler(this.OK_Click);
            // 
            // labelX6
            // 
            // 
            // 
            // 
            this.labelX6.BackgroundStyle.CornerType = DevComponents.DotNetBar.eCornerType.Square;
            this.labelX6.Font = new System.Drawing.Font("標楷體", 12F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(136)));
            this.labelX6.Location = new System.Drawing.Point(12, 157);
            this.labelX6.Name = "labelX6";
            this.labelX6.Size = new System.Drawing.Size(103, 23);
            this.labelX6.TabIndex = 12;
            this.labelX6.Text = "開啟檔案：";
            // 
            // MOTName
            // 
            // 
            // 
            // 
            this.MOTName.BackgroundStyle.CornerType = DevComponents.DotNetBar.eCornerType.Square;
            this.MOTName.Font = new System.Drawing.Font("標楷體", 12F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(136)));
            this.MOTName.Location = new System.Drawing.Point(12, 186);
            this.MOTName.Name = "MOTName";
            this.MOTName.Size = new System.Drawing.Size(260, 23);
            this.MOTName.TabIndex = 13;
            // 
            // OpenMOTDlg
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 12F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.ClientSize = new System.Drawing.Size(286, 259);
            this.Controls.Add(this.MOTName);
            this.Controls.Add(this.labelX6);
            this.Controls.Add(this.Close);
            this.Controls.Add(this.OK);
            this.Controls.Add(this.comboBoxOpVer);
            this.Controls.Add(this.comboBoxCusVer);
            this.Controls.Add(this.comboBoxPartNo);
            this.Controls.Add(this.comboBoxCus);
            this.Controls.Add(this.labelX5);
            this.Controls.Add(this.labelX4);
            this.Controls.Add(this.labelX3);
            this.Controls.Add(this.labelX2);
            this.Controls.Add(this.labelX1);
            this.DoubleBuffered = true;
            this.Name = "OpenMOTDlg";
            this.Text = "OpenMOTDlg";
            this.ResumeLayout(false);

        }

        #endregion

        private DevComponents.DotNetBar.LabelX labelX1;
        private DevComponents.DotNetBar.LabelX labelX2;
        private DevComponents.DotNetBar.LabelX labelX3;
        private DevComponents.DotNetBar.LabelX labelX4;
        private DevComponents.DotNetBar.LabelX labelX5;
        private DevComponents.DotNetBar.Controls.ComboBoxEx comboBoxCus;
        private DevComponents.DotNetBar.Controls.ComboBoxEx comboBoxPartNo;
        private DevComponents.DotNetBar.Controls.ComboBoxEx comboBoxCusVer;
        private DevComponents.DotNetBar.Controls.ComboBoxEx comboBoxOpVer;
        private DevComponents.DotNetBar.StyleManager styleManager1;
        private DevComponents.DotNetBar.ButtonX OK;
        private DevComponents.DotNetBar.ButtonX Close;
        private DevComponents.DotNetBar.LabelX labelX6;
        private DevComponents.DotNetBar.LabelX MOTName;
    }
}