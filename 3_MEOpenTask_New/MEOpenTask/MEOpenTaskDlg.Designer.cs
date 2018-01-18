namespace MEOpenTask
{
    partial class MEOpenTaskDlg
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
            this.labelX1 = new DevComponents.DotNetBar.LabelX();
            this.comboBoxCus = new DevComponents.DotNetBar.Controls.ComboBoxEx();
            this.labelX2 = new DevComponents.DotNetBar.LabelX();
            this.comboBoxPartNo = new DevComponents.DotNetBar.Controls.ComboBoxEx();
            this.labelX3 = new DevComponents.DotNetBar.LabelX();
            this.comboBoxCusVer = new DevComponents.DotNetBar.Controls.ComboBoxEx();
            this.checkWNoBillet = new DevComponents.DotNetBar.Controls.CheckBoxX();
            this.label001 = new DevComponents.DotNetBar.LabelX();
            this.groupPanel1 = new DevComponents.DotNetBar.Controls.GroupPanel();
            this.groupBox900 = new System.Windows.Forms.GroupBox();
            this.button900SelPart = new DevComponents.DotNetBar.ButtonX();
            this.label900 = new DevComponents.DotNetBar.LabelX();
            this.groupBoxW = new System.Windows.Forms.GroupBox();
            this.labelW = new DevComponents.DotNetBar.LabelX();
            this.buttonWSelPart = new DevComponents.DotNetBar.ButtonX();
            this.checkWHasBillet = new DevComponents.DotNetBar.Controls.CheckBoxX();
            this.groupBox001 = new System.Windows.Forms.GroupBox();
            this.button001SelPart = new DevComponents.DotNetBar.ButtonX();
            this.check001NoBillet = new DevComponents.DotNetBar.Controls.CheckBoxX();
            this.check001HasBillet = new DevComponents.DotNetBar.Controls.CheckBoxX();
            this.superGridPanel = new DevComponents.DotNetBar.SuperGrid.SuperGridControl();
            this.gridColumn1 = new DevComponents.DotNetBar.SuperGrid.GridColumn();
            this.gridColumn2 = new DevComponents.DotNetBar.SuperGrid.GridColumn();
            this.openFileDialog1 = new System.Windows.Forms.OpenFileDialog();
            this.labelX4 = new DevComponents.DotNetBar.LabelX();
            this.comboBoxOpVer = new DevComponents.DotNetBar.Controls.ComboBoxEx();
            this.labelX5 = new DevComponents.DotNetBar.LabelX();
            this.Close = new DevComponents.DotNetBar.ButtonX();
            this.buttonOpenTask = new DevComponents.DotNetBar.ButtonX();
            this.groupPanel1.SuspendLayout();
            this.groupBox900.SuspendLayout();
            this.groupBoxW.SuspendLayout();
            this.groupBox001.SuspendLayout();
            this.SuspendLayout();
            // 
            // styleManager1
            // 
            this.styleManager1.ManagerStyle = DevComponents.DotNetBar.eStyle.Windows7Blue;
            this.styleManager1.MetroColorParameters = new DevComponents.DotNetBar.Metro.ColorTables.MetroColorGeneratorParameters(System.Drawing.Color.White, System.Drawing.Color.FromArgb(((int)(((byte)(43)))), ((int)(((byte)(87)))), ((int)(((byte)(154))))));
            // 
            // labelX1
            // 
            // 
            // 
            // 
            this.labelX1.BackgroundStyle.CornerType = DevComponents.DotNetBar.eCornerType.Square;
            this.labelX1.Font = new System.Drawing.Font("標楷體", 12F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(136)));
            this.labelX1.Location = new System.Drawing.Point(13, 38);
            this.labelX1.Name = "labelX1";
            this.labelX1.Size = new System.Drawing.Size(69, 23);
            this.labelX1.TabIndex = 1;
            this.labelX1.Text = "客戶：";
            // 
            // comboBoxCus
            // 
            this.comboBoxCus.DisplayMember = "Text";
            this.comboBoxCus.DrawMode = System.Windows.Forms.DrawMode.OwnerDrawFixed;
            this.comboBoxCus.DropDownStyle = System.Windows.Forms.ComboBoxStyle.DropDownList;
            this.comboBoxCus.FormattingEnabled = true;
            this.comboBoxCus.ItemHeight = 16;
            this.comboBoxCus.Location = new System.Drawing.Point(67, 36);
            this.comboBoxCus.Name = "comboBoxCus";
            this.comboBoxCus.Size = new System.Drawing.Size(238, 22);
            this.comboBoxCus.Style = DevComponents.DotNetBar.eDotNetBarStyle.StyleManagerControlled;
            this.comboBoxCus.TabIndex = 2;
            this.comboBoxCus.SelectedIndexChanged += new System.EventHandler(this.comboBoxCus_SelectedIndexChanged);
            // 
            // labelX2
            // 
            // 
            // 
            // 
            this.labelX2.BackgroundStyle.CornerType = DevComponents.DotNetBar.eCornerType.Square;
            this.labelX2.Font = new System.Drawing.Font("標楷體", 12F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(136)));
            this.labelX2.Location = new System.Drawing.Point(13, 67);
            this.labelX2.Name = "labelX2";
            this.labelX2.Size = new System.Drawing.Size(69, 23);
            this.labelX2.TabIndex = 3;
            this.labelX2.Text = "料號：";
            // 
            // comboBoxPartNo
            // 
            this.comboBoxPartNo.DisplayMember = "Text";
            this.comboBoxPartNo.DrawMode = System.Windows.Forms.DrawMode.OwnerDrawFixed;
            this.comboBoxPartNo.DropDownStyle = System.Windows.Forms.ComboBoxStyle.DropDownList;
            this.comboBoxPartNo.FormattingEnabled = true;
            this.comboBoxPartNo.ItemHeight = 16;
            this.comboBoxPartNo.Location = new System.Drawing.Point(67, 65);
            this.comboBoxPartNo.Name = "comboBoxPartNo";
            this.comboBoxPartNo.Size = new System.Drawing.Size(238, 22);
            this.comboBoxPartNo.Style = DevComponents.DotNetBar.eDotNetBarStyle.StyleManagerControlled;
            this.comboBoxPartNo.TabIndex = 4;
            this.comboBoxPartNo.SelectedIndexChanged += new System.EventHandler(this.comboBoxPartNo_SelectedIndexChanged);
            // 
            // labelX3
            // 
            // 
            // 
            // 
            this.labelX3.BackgroundStyle.CornerType = DevComponents.DotNetBar.eCornerType.Square;
            this.labelX3.Font = new System.Drawing.Font("標楷體", 12F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(136)));
            this.labelX3.Location = new System.Drawing.Point(13, 96);
            this.labelX3.Name = "labelX3";
            this.labelX3.Size = new System.Drawing.Size(93, 23);
            this.labelX3.TabIndex = 5;
            this.labelX3.Text = "客戶版本：";
            // 
            // comboBoxCusVer
            // 
            this.comboBoxCusVer.DisplayMember = "Text";
            this.comboBoxCusVer.DrawMode = System.Windows.Forms.DrawMode.OwnerDrawFixed;
            this.comboBoxCusVer.DropDownStyle = System.Windows.Forms.ComboBoxStyle.DropDownList;
            this.comboBoxCusVer.FormattingEnabled = true;
            this.comboBoxCusVer.ItemHeight = 16;
            this.comboBoxCusVer.Location = new System.Drawing.Point(97, 94);
            this.comboBoxCusVer.Name = "comboBoxCusVer";
            this.comboBoxCusVer.Size = new System.Drawing.Size(208, 22);
            this.comboBoxCusVer.Style = DevComponents.DotNetBar.eDotNetBarStyle.StyleManagerControlled;
            this.comboBoxCusVer.TabIndex = 6;
            this.comboBoxCusVer.SelectedIndexChanged += new System.EventHandler(this.comboBoxCusVer_SelectedIndexChanged);
            // 
            // checkWNoBillet
            // 
            this.checkWNoBillet.BackColor = System.Drawing.Color.Transparent;
            // 
            // 
            // 
            this.checkWNoBillet.BackgroundStyle.CornerType = DevComponents.DotNetBar.eCornerType.Square;
            this.checkWNoBillet.Location = new System.Drawing.Point(13, 45);
            this.checkWNoBillet.Name = "checkWNoBillet";
            this.checkWNoBillet.Size = new System.Drawing.Size(151, 24);
            this.checkWNoBillet.Style = DevComponents.DotNetBar.eDotNetBarStyle.StyleManagerControlled;
            this.checkWNoBillet.TabIndex = 1;
            this.checkWNoBillet.Text = "無前段製程檔案";
            this.checkWNoBillet.CheckedChanged += new System.EventHandler(this.checkWNoBillet_CheckedChanged);
            // 
            // label001
            // 
            this.label001.BackColor = System.Drawing.Color.White;
            // 
            // 
            // 
            this.label001.BackgroundStyle.CornerType = DevComponents.DotNetBar.eCornerType.Square;
            this.label001.Location = new System.Drawing.Point(224, 21);
            this.label001.Name = "label001";
            this.label001.Size = new System.Drawing.Size(183, 23);
            this.label001.TabIndex = 3;
            // 
            // groupPanel1
            // 
            this.groupPanel1.CanvasColor = System.Drawing.SystemColors.Control;
            this.groupPanel1.ColorSchemeStyle = DevComponents.DotNetBar.eDotNetBarStyle.Office2007;
            this.groupPanel1.Controls.Add(this.groupBox900);
            this.groupPanel1.Controls.Add(this.groupBoxW);
            this.groupPanel1.Controls.Add(this.groupBox001);
            this.groupPanel1.DisabledBackColor = System.Drawing.Color.Empty;
            this.groupPanel1.Font = new System.Drawing.Font("標楷體", 12F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(136)));
            this.groupPanel1.Location = new System.Drawing.Point(13, 352);
            this.groupPanel1.Name = "groupPanel1";
            this.groupPanel1.Size = new System.Drawing.Size(433, 253);
            // 
            // 
            // 
            this.groupPanel1.Style.BackColor2SchemePart = DevComponents.DotNetBar.eColorSchemePart.PanelBackground2;
            this.groupPanel1.Style.BackColorGradientAngle = 90;
            this.groupPanel1.Style.BackColorSchemePart = DevComponents.DotNetBar.eColorSchemePart.PanelBackground;
            this.groupPanel1.Style.BorderBottom = DevComponents.DotNetBar.eStyleBorderType.Solid;
            this.groupPanel1.Style.BorderBottomWidth = 1;
            this.groupPanel1.Style.BorderColorSchemePart = DevComponents.DotNetBar.eColorSchemePart.PanelBorder;
            this.groupPanel1.Style.BorderLeft = DevComponents.DotNetBar.eStyleBorderType.Solid;
            this.groupPanel1.Style.BorderLeftWidth = 1;
            this.groupPanel1.Style.BorderRight = DevComponents.DotNetBar.eStyleBorderType.Solid;
            this.groupPanel1.Style.BorderRightWidth = 1;
            this.groupPanel1.Style.BorderTop = DevComponents.DotNetBar.eStyleBorderType.Solid;
            this.groupPanel1.Style.BorderTopWidth = 1;
            this.groupPanel1.Style.CornerDiameter = 4;
            this.groupPanel1.Style.CornerType = DevComponents.DotNetBar.eCornerType.Rounded;
            this.groupPanel1.Style.TextAlignment = DevComponents.DotNetBar.eStyleTextAlignment.Center;
            this.groupPanel1.Style.TextColorSchemePart = DevComponents.DotNetBar.eColorSchemePart.PanelText;
            this.groupPanel1.Style.TextLineAlignment = DevComponents.DotNetBar.eStyleTextAlignment.Near;
            // 
            // 
            // 
            this.groupPanel1.StyleMouseDown.CornerType = DevComponents.DotNetBar.eCornerType.Square;
            // 
            // 
            // 
            this.groupPanel1.StyleMouseOver.CornerType = DevComponents.DotNetBar.eCornerType.Square;
            this.groupPanel1.TabIndex = 7;
            this.groupPanel1.Text = "選擇組裝方式";
            this.groupPanel1.Visible = false;
            // 
            // groupBox900
            // 
            this.groupBox900.BackColor = System.Drawing.Color.Transparent;
            this.groupBox900.Controls.Add(this.button900SelPart);
            this.groupBox900.Controls.Add(this.label900);
            this.groupBox900.ForeColor = System.Drawing.Color.Black;
            this.groupBox900.Location = new System.Drawing.Point(3, 164);
            this.groupBox900.Name = "groupBox900";
            this.groupBox900.Size = new System.Drawing.Size(419, 48);
            this.groupBox900.TabIndex = 8;
            this.groupBox900.TabStop = false;
            this.groupBox900.Text = "製程序900";
            // 
            // button900SelPart
            // 
            this.button900SelPart.AccessibleRole = System.Windows.Forms.AccessibleRole.PushButton;
            this.button900SelPart.ColorTable = DevComponents.DotNetBar.eButtonColor.OrangeWithBackground;
            this.button900SelPart.Image = global::MEOpenTask.Properties.Resources.folder_16px;
            this.button900SelPart.Location = new System.Drawing.Point(14, 19);
            this.button900SelPart.Name = "button900SelPart";
            this.button900SelPart.Size = new System.Drawing.Size(49, 23);
            this.button900SelPart.Style = DevComponents.DotNetBar.eDotNetBarStyle.StyleManagerControlled;
            this.button900SelPart.TabIndex = 8;
            this.button900SelPart.Click += new System.EventHandler(this.button900SelPart_Click);
            // 
            // label900
            // 
            this.label900.BackColor = System.Drawing.Color.White;
            // 
            // 
            // 
            this.label900.BackgroundStyle.CornerType = DevComponents.DotNetBar.eCornerType.Square;
            this.label900.Location = new System.Drawing.Point(78, 19);
            this.label900.Name = "label900";
            this.label900.Size = new System.Drawing.Size(329, 23);
            this.label900.TabIndex = 7;
            // 
            // groupBoxW
            // 
            this.groupBoxW.BackColor = System.Drawing.Color.Transparent;
            this.groupBoxW.Controls.Add(this.labelW);
            this.groupBoxW.Controls.Add(this.checkWNoBillet);
            this.groupBoxW.Controls.Add(this.buttonWSelPart);
            this.groupBoxW.Controls.Add(this.checkWHasBillet);
            this.groupBoxW.Location = new System.Drawing.Point(3, 83);
            this.groupBoxW.Name = "groupBoxW";
            this.groupBoxW.Size = new System.Drawing.Size(419, 75);
            this.groupBoxW.TabIndex = 6;
            this.groupBoxW.TabStop = false;
            this.groupBoxW.Text = "製程序W階";
            // 
            // labelW
            // 
            this.labelW.BackColor = System.Drawing.Color.White;
            // 
            // 
            // 
            this.labelW.BackgroundStyle.CornerType = DevComponents.DotNetBar.eCornerType.Square;
            this.labelW.Location = new System.Drawing.Point(224, 21);
            this.labelW.Name = "labelW";
            this.labelW.Size = new System.Drawing.Size(183, 23);
            this.labelW.TabIndex = 7;
            // 
            // buttonWSelPart
            // 
            this.buttonWSelPart.AccessibleRole = System.Windows.Forms.AccessibleRole.PushButton;
            this.buttonWSelPart.ColorTable = DevComponents.DotNetBar.eButtonColor.OrangeWithBackground;
            this.buttonWSelPart.Image = global::MEOpenTask.Properties.Resources.folder_16px;
            this.buttonWSelPart.Location = new System.Drawing.Point(169, 21);
            this.buttonWSelPart.Name = "buttonWSelPart";
            this.buttonWSelPart.Size = new System.Drawing.Size(49, 23);
            this.buttonWSelPart.Style = DevComponents.DotNetBar.eDotNetBarStyle.StyleManagerControlled;
            this.buttonWSelPart.TabIndex = 5;
            this.buttonWSelPart.Click += new System.EventHandler(this.buttonWSelPart_Click);
            // 
            // checkWHasBillet
            // 
            this.checkWHasBillet.BackColor = System.Drawing.Color.Transparent;
            // 
            // 
            // 
            this.checkWHasBillet.BackgroundStyle.CornerType = DevComponents.DotNetBar.eCornerType.Square;
            this.checkWHasBillet.Location = new System.Drawing.Point(13, 21);
            this.checkWHasBillet.Name = "checkWHasBillet";
            this.checkWHasBillet.Size = new System.Drawing.Size(150, 24);
            this.checkWHasBillet.Style = DevComponents.DotNetBar.eDotNetBarStyle.StyleManagerControlled;
            this.checkWHasBillet.TabIndex = 4;
            this.checkWHasBillet.Text = "有前段製程檔案";
            this.checkWHasBillet.CheckedChanged += new System.EventHandler(this.checkWHasBillet_CheckedChanged);
            // 
            // groupBox001
            // 
            this.groupBox001.BackColor = System.Drawing.Color.Transparent;
            this.groupBox001.Controls.Add(this.button001SelPart);
            this.groupBox001.Controls.Add(this.check001NoBillet);
            this.groupBox001.Controls.Add(this.check001HasBillet);
            this.groupBox001.Controls.Add(this.label001);
            this.groupBox001.Location = new System.Drawing.Point(3, 3);
            this.groupBox001.Name = "groupBox001";
            this.groupBox001.Size = new System.Drawing.Size(419, 74);
            this.groupBox001.TabIndex = 5;
            this.groupBox001.TabStop = false;
            this.groupBox001.Text = "製程序001";
            // 
            // button001SelPart
            // 
            this.button001SelPart.AccessibleRole = System.Windows.Forms.AccessibleRole.PushButton;
            this.button001SelPart.ColorTable = DevComponents.DotNetBar.eButtonColor.OrangeWithBackground;
            this.button001SelPart.Image = global::MEOpenTask.Properties.Resources.folder_16px;
            this.button001SelPart.Location = new System.Drawing.Point(169, 21);
            this.button001SelPart.Name = "button001SelPart";
            this.button001SelPart.Size = new System.Drawing.Size(49, 23);
            this.button001SelPart.Style = DevComponents.DotNetBar.eDotNetBarStyle.StyleManagerControlled;
            this.button001SelPart.TabIndex = 2;
            this.button001SelPart.Click += new System.EventHandler(this.button001SelPart_Click);
            // 
            // check001NoBillet
            // 
            // 
            // 
            // 
            this.check001NoBillet.BackgroundStyle.CornerType = DevComponents.DotNetBar.eCornerType.Square;
            this.check001NoBillet.Location = new System.Drawing.Point(13, 45);
            this.check001NoBillet.Name = "check001NoBillet";
            this.check001NoBillet.Size = new System.Drawing.Size(100, 23);
            this.check001NoBillet.Style = DevComponents.DotNetBar.eDotNetBarStyle.StyleManagerControlled;
            this.check001NoBillet.TabIndex = 1;
            this.check001NoBillet.Text = "無胚料檔案";
            this.check001NoBillet.CheckedChanged += new System.EventHandler(this.check001NoBillet_CheckedChanged);
            // 
            // check001HasBillet
            // 
            // 
            // 
            // 
            this.check001HasBillet.BackgroundStyle.CornerType = DevComponents.DotNetBar.eCornerType.Square;
            this.check001HasBillet.Location = new System.Drawing.Point(13, 21);
            this.check001HasBillet.Name = "check001HasBillet";
            this.check001HasBillet.Size = new System.Drawing.Size(100, 23);
            this.check001HasBillet.Style = DevComponents.DotNetBar.eDotNetBarStyle.StyleManagerControlled;
            this.check001HasBillet.TabIndex = 0;
            this.check001HasBillet.Text = "有胚料檔案";
            this.check001HasBillet.CheckedChanged += new System.EventHandler(this.check001HasBillet_CheckedChanged);
            // 
            // superGridPanel
            // 
            this.superGridPanel.FilterExprColors.SysFunction = System.Drawing.Color.DarkRed;
            this.superGridPanel.Location = new System.Drawing.Point(13, 151);
            this.superGridPanel.Name = "superGridPanel";
            // 
            // 
            // 
            this.superGridPanel.PrimaryGrid.Columns.Add(this.gridColumn1);
            this.superGridPanel.PrimaryGrid.Columns.Add(this.gridColumn2);
            this.superGridPanel.PrimaryGrid.MultiSelect = false;
            this.superGridPanel.PrimaryGrid.ShowRowHeaders = false;
            this.superGridPanel.Size = new System.Drawing.Size(292, 137);
            this.superGridPanel.TabIndex = 9;
            this.superGridPanel.Text = "superGridControl1";
            this.superGridPanel.CellValueChanged += new System.EventHandler<DevComponents.DotNetBar.SuperGrid.GridCellValueChangedEventArgs>(this.superGridPanel_CellValueChanged);
            // 
            // gridColumn1
            // 
            this.gridColumn1.CellStyles.Default.Alignment = DevComponents.DotNetBar.SuperGrid.Style.Alignment.MiddleCenter;
            this.gridColumn1.ColumnSortMode = DevComponents.DotNetBar.SuperGrid.ColumnSortMode.None;
            this.gridColumn1.EditorType = typeof(DevComponents.DotNetBar.SuperGrid.GridCheckBoxXEditControl);
            this.gridColumn1.FillWeight = 50;
            this.gridColumn1.HeaderText = "#";
            this.gridColumn1.Name = "製程序";
            this.gridColumn1.Width = 30;
            // 
            // gridColumn2
            // 
            this.gridColumn2.AutoSizeMode = DevComponents.DotNetBar.SuperGrid.ColumnAutoSizeMode.Fill;
            this.gridColumn2.ColumnSortMode = DevComponents.DotNetBar.SuperGrid.ColumnSortMode.None;
            this.gridColumn2.FillWeight = 50;
            this.gridColumn2.HeaderText = "檔案名";
            this.gridColumn2.Name = "檔案名";
            this.gridColumn2.ReadOnly = true;
            // 
            // openFileDialog1
            // 
            this.openFileDialog1.FileName = "openFileDialog1";
            // 
            // labelX4
            // 
            // 
            // 
            // 
            this.labelX4.BackgroundStyle.CornerType = DevComponents.DotNetBar.eCornerType.Square;
            this.labelX4.Font = new System.Drawing.Font("標楷體", 12F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(136)));
            this.labelX4.Location = new System.Drawing.Point(13, 9);
            this.labelX4.Name = "labelX4";
            this.labelX4.Size = new System.Drawing.Size(149, 23);
            this.labelX4.TabIndex = 10;
            this.labelX4.Text = "工程師職稱：ME";
            // 
            // comboBoxOpVer
            // 
            this.comboBoxOpVer.DisplayMember = "Text";
            this.comboBoxOpVer.DrawMode = System.Windows.Forms.DrawMode.OwnerDrawFixed;
            this.comboBoxOpVer.DropDownStyle = System.Windows.Forms.ComboBoxStyle.DropDownList;
            this.comboBoxOpVer.FormattingEnabled = true;
            this.comboBoxOpVer.ItemHeight = 16;
            this.comboBoxOpVer.Location = new System.Drawing.Point(97, 122);
            this.comboBoxOpVer.Name = "comboBoxOpVer";
            this.comboBoxOpVer.Size = new System.Drawing.Size(208, 22);
            this.comboBoxOpVer.Style = DevComponents.DotNetBar.eDotNetBarStyle.StyleManagerControlled;
            this.comboBoxOpVer.TabIndex = 11;
            this.comboBoxOpVer.SelectedIndexChanged += new System.EventHandler(this.comboBoxOpVer_SelectedIndexChanged);
            // 
            // labelX5
            // 
            this.labelX5.BackColor = System.Drawing.Color.Transparent;
            // 
            // 
            // 
            this.labelX5.BackgroundStyle.CornerType = DevComponents.DotNetBar.eCornerType.Square;
            this.labelX5.Font = new System.Drawing.Font("標楷體", 12F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(136)));
            this.labelX5.Location = new System.Drawing.Point(13, 122);
            this.labelX5.Name = "labelX5";
            this.labelX5.Size = new System.Drawing.Size(93, 23);
            this.labelX5.TabIndex = 12;
            this.labelX5.Text = "製程版次：";
            // 
            // Close
            // 
            this.Close.AccessibleRole = System.Windows.Forms.AccessibleRole.PushButton;
            this.Close.ColorTable = DevComponents.DotNetBar.eButtonColor.Flat;
            this.Close.Font = new System.Drawing.Font("標楷體", 12F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(136)));
            this.Close.Image = global::MEOpenTask.Properties.Resources.close_16px;
            this.Close.Location = new System.Drawing.Point(230, 294);
            this.Close.Name = "Close";
            this.Close.Size = new System.Drawing.Size(75, 32);
            this.Close.Style = DevComponents.DotNetBar.eDotNetBarStyle.StyleManagerControlled;
            this.Close.TabIndex = 13;
            this.Close.Text = "關閉";
            this.Close.Click += new System.EventHandler(this.Close_Click);
            // 
            // buttonOpenTask
            // 
            this.buttonOpenTask.AccessibleRole = System.Windows.Forms.AccessibleRole.PushButton;
            this.buttonOpenTask.ColorTable = DevComponents.DotNetBar.eButtonColor.Flat;
            this.buttonOpenTask.Font = new System.Drawing.Font("標楷體", 12F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(136)));
            this.buttonOpenTask.Image = global::MEOpenTask.Properties.Resources.confirm_16px;
            this.buttonOpenTask.Location = new System.Drawing.Point(149, 294);
            this.buttonOpenTask.Name = "buttonOpenTask";
            this.buttonOpenTask.Size = new System.Drawing.Size(75, 32);
            this.buttonOpenTask.Style = DevComponents.DotNetBar.eDotNetBarStyle.StyleManagerControlled;
            this.buttonOpenTask.TabIndex = 8;
            this.buttonOpenTask.Text = "開啟";
            this.buttonOpenTask.Click += new System.EventHandler(this.buttonOpenTask_Click);
            // 
            // MEOpenTaskDlg
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 12F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.AutoSizeMode = System.Windows.Forms.AutoSizeMode.GrowAndShrink;
            this.ClientSize = new System.Drawing.Size(321, 338);
            this.Controls.Add(this.Close);
            this.Controls.Add(this.comboBoxOpVer);
            this.Controls.Add(this.labelX5);
            this.Controls.Add(this.labelX4);
            this.Controls.Add(this.superGridPanel);
            this.Controls.Add(this.buttonOpenTask);
            this.Controls.Add(this.groupPanel1);
            this.Controls.Add(this.comboBoxCusVer);
            this.Controls.Add(this.labelX3);
            this.Controls.Add(this.comboBoxPartNo);
            this.Controls.Add(this.labelX2);
            this.Controls.Add(this.comboBoxCus);
            this.Controls.Add(this.labelX1);
            this.DoubleBuffered = true;
            this.MaximizeBox = false;
            this.MinimizeBox = false;
            this.Name = "MEOpenTaskDlg";
            this.Text = "ME開啟料號";
            this.groupPanel1.ResumeLayout(false);
            this.groupBox900.ResumeLayout(false);
            this.groupBoxW.ResumeLayout(false);
            this.groupBox001.ResumeLayout(false);
            this.ResumeLayout(false);

        }

        #endregion

        private DevComponents.DotNetBar.StyleManager styleManager1;
        private DevComponents.DotNetBar.LabelX labelX1;
        private DevComponents.DotNetBar.Controls.ComboBoxEx comboBoxCus;
        private DevComponents.DotNetBar.LabelX labelX2;
        private DevComponents.DotNetBar.Controls.ComboBoxEx comboBoxPartNo;
        private DevComponents.DotNetBar.LabelX labelX3;
        private DevComponents.DotNetBar.Controls.ComboBoxEx comboBoxCusVer;
        private DevComponents.DotNetBar.Controls.CheckBoxX checkWNoBillet;
        private DevComponents.DotNetBar.ButtonX button001SelPart;
        private DevComponents.DotNetBar.LabelX label001;
        private DevComponents.DotNetBar.Controls.GroupPanel groupPanel1;
        private DevComponents.DotNetBar.ButtonX buttonOpenTask;
        private DevComponents.DotNetBar.Controls.CheckBoxX checkWHasBillet;
        private System.Windows.Forms.GroupBox groupBox900;
        private DevComponents.DotNetBar.ButtonX button900SelPart;
        private DevComponents.DotNetBar.LabelX label900;
        private System.Windows.Forms.GroupBox groupBoxW;
        private DevComponents.DotNetBar.LabelX labelW;
        private DevComponents.DotNetBar.ButtonX buttonWSelPart;
        private System.Windows.Forms.GroupBox groupBox001;
        private DevComponents.DotNetBar.Controls.CheckBoxX check001NoBillet;
        private DevComponents.DotNetBar.Controls.CheckBoxX check001HasBillet;
        private DevComponents.DotNetBar.SuperGrid.SuperGridControl superGridPanel;
        private DevComponents.DotNetBar.SuperGrid.GridColumn gridColumn1;
        private DevComponents.DotNetBar.SuperGrid.GridColumn gridColumn2;
        private System.Windows.Forms.OpenFileDialog openFileDialog1;
        private DevComponents.DotNetBar.LabelX labelX4;
        private DevComponents.DotNetBar.Controls.ComboBoxEx comboBoxOpVer;
        private DevComponents.DotNetBar.LabelX labelX5;
        private DevComponents.DotNetBar.ButtonX Close;
    }
}