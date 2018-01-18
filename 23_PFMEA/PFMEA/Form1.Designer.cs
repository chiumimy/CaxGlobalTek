namespace PFMEA
{
    partial class Form1
    {
        /// <summary>
        /// 設計工具所需的變數。
        /// </summary>
        private System.ComponentModel.IContainer components = null;

        /// <summary>
        /// 清除任何使用中的資源。
        /// </summary>
        /// <param name="disposing">如果應該處置 Managed 資源則為 true，否則為 false。</param>
        protected override void Dispose(bool disposing)
        {
            if (disposing && (components != null))
            {
                components.Dispose();
            }
            base.Dispose(disposing);
        }

        #region Windows Form 設計工具產生的程式碼

        /// <summary>
        /// 此為設計工具支援所需的方法 - 請勿使用程式碼編輯器
        /// 修改這個方法的內容。
        /// </summary>
        private void InitializeComponent()
        {
            this.components = new System.ComponentModel.Container();
            System.ComponentModel.ComponentResourceManager resources = new System.ComponentModel.ComponentResourceManager(typeof(Form1));
            this.OISTree = new DevComponents.AdvTree.AdvTree();
            this.columnHeader1 = new DevComponents.AdvTree.ColumnHeader();
            this.columnHeader2 = new DevComponents.AdvTree.ColumnHeader();
            this.nodeConnector1 = new DevComponents.AdvTree.NodeConnector();
            this.elementStyle1 = new DevComponents.DotNetBar.ElementStyle();
            this.styleManager1 = new DevComponents.DotNetBar.StyleManager(this.components);
            this.labelX1 = new DevComponents.DotNetBar.LabelX();
            this.CustCombo = new DevComponents.DotNetBar.Controls.ComboBoxEx();
            this.labelX2 = new DevComponents.DotNetBar.LabelX();
            this.PartNoCombo = new DevComponents.DotNetBar.Controls.ComboBoxEx();
            this.CusRevCombo = new DevComponents.DotNetBar.Controls.ComboBoxEx();
            this.OpRevCombo = new DevComponents.DotNetBar.Controls.ComboBoxEx();
            this.labelX3 = new DevComponents.DotNetBar.LabelX();
            this.labelX4 = new DevComponents.DotNetBar.LabelX();
            this.buttonItem1 = new DevComponents.DotNetBar.ButtonItem();
            this.PFM = new DevComponents.DotNetBar.ButtonX();
            this.PEoF = new DevComponents.DotNetBar.ButtonX();
            this.Class = new DevComponents.DotNetBar.ButtonX();
            this.PCoF = new DevComponents.DotNetBar.ButtonX();
            this.CPCP = new DevComponents.DotNetBar.ButtonX();
            this.CPCD = new DevComponents.DotNetBar.ButtonX();
            this.SevCombo = new DevComponents.DotNetBar.Controls.ComboBoxEx();
            this.OccCombo = new DevComponents.DotNetBar.Controls.ComboBoxEx();
            this.DetCombo = new DevComponents.DotNetBar.Controls.ComboBoxEx();
            this.labelX5 = new DevComponents.DotNetBar.LabelX();
            this.labelX6 = new DevComponents.DotNetBar.LabelX();
            this.labelX7 = new DevComponents.DotNetBar.LabelX();
            this.groupBox1 = new System.Windows.Forms.GroupBox();
            this.IndexSetting = new DevComponents.DotNetBar.ButtonX();
            this.Cancel = new DevComponents.DotNetBar.ButtonX();
            this.OK = new DevComponents.DotNetBar.ButtonX();
            ((System.ComponentModel.ISupportInitialize)(this.OISTree)).BeginInit();
            this.groupBox1.SuspendLayout();
            this.SuspendLayout();
            // 
            // OISTree
            // 
            this.OISTree.AccessibleRole = System.Windows.Forms.AccessibleRole.Outline;
            this.OISTree.AllowDrop = true;
            this.OISTree.AllowUserToReorderColumns = true;
            this.OISTree.AlternateRowColor = System.Drawing.Color.Transparent;
            this.OISTree.BackColor = System.Drawing.Color.FromArgb(((int)(((byte)(255)))), ((int)(((byte)(255)))), ((int)(((byte)(192)))));
            // 
            // 
            // 
            this.OISTree.BackgroundStyle.Class = "TreeBorderKey";
            this.OISTree.BackgroundStyle.CornerType = DevComponents.DotNetBar.eCornerType.Square;
            this.OISTree.Columns.Add(this.columnHeader1);
            this.OISTree.Columns.Add(this.columnHeader2);
            this.OISTree.DragDropEnabled = false;
            this.OISTree.ExpandButtonType = DevComponents.AdvTree.eExpandButtonType.Triangle;
            this.OISTree.GridColumnLineResizeEnabled = true;
            this.OISTree.GridColumnLines = false;
            this.OISTree.GridLinesColor = System.Drawing.Color.Transparent;
            this.OISTree.GridRowLines = true;
            this.OISTree.Location = new System.Drawing.Point(12, 41);
            this.OISTree.MultiSelect = true;
            this.OISTree.Name = "OISTree";
            this.OISTree.NodesConnector = this.nodeConnector1;
            this.OISTree.NodeStyle = this.elementStyle1;
            this.OISTree.PathSeparator = ";";
            this.OISTree.SelectionBoxStyle = DevComponents.AdvTree.eSelectionStyle.FullRowSelect;
            this.OISTree.Size = new System.Drawing.Size(923, 341);
            this.OISTree.Styles.Add(this.elementStyle1);
            this.OISTree.TabIndex = 0;
            this.OISTree.Text = "advTree1";
            this.OISTree.BeforeExpand += new DevComponents.AdvTree.AdvTreeNodeCancelEventHandler(this.OISTree_BeforeExpand);
            this.OISTree.NodeMouseUp += new DevComponents.AdvTree.TreeNodeMouseEventHandler(this.OISTree_NodeMouseUp);
            this.OISTree.NodeClick += new DevComponents.AdvTree.TreeNodeMouseEventHandler(this.OISTree_NodeClick);
            // 
            // columnHeader1
            // 
            this.columnHeader1.CellsBackColor = System.Drawing.Color.White;
            this.columnHeader1.Name = "columnHeader1";
            this.columnHeader1.SortingEnabled = false;
            this.columnHeader1.Text = "製程序";
            this.columnHeader1.Width.Absolute = 100;
            // 
            // columnHeader2
            // 
            this.columnHeader2.Name = "columnHeader2";
            this.columnHeader2.SortingEnabled = false;
            this.columnHeader2.Text = "製程別";
            this.columnHeader2.Width.Absolute = 1500;
            // 
            // nodeConnector1
            // 
            this.nodeConnector1.LineColor = System.Drawing.SystemColors.ControlText;
            // 
            // elementStyle1
            // 
            this.elementStyle1.CornerType = DevComponents.DotNetBar.eCornerType.Square;
            this.elementStyle1.Name = "elementStyle1";
            this.elementStyle1.TextColor = System.Drawing.SystemColors.ControlText;
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
            this.labelX1.Location = new System.Drawing.Point(12, 12);
            this.labelX1.Name = "labelX1";
            this.labelX1.Size = new System.Drawing.Size(61, 23);
            this.labelX1.TabIndex = 1;
            this.labelX1.Text = "客戶：";
            // 
            // CustCombo
            // 
            this.CustCombo.DisplayMember = "Text";
            this.CustCombo.DrawMode = System.Windows.Forms.DrawMode.OwnerDrawFixed;
            this.CustCombo.DropDownStyle = System.Windows.Forms.ComboBoxStyle.DropDownList;
            this.CustCombo.FormattingEnabled = true;
            this.CustCombo.ItemHeight = 16;
            this.CustCombo.Location = new System.Drawing.Point(65, 12);
            this.CustCombo.Name = "CustCombo";
            this.CustCombo.Size = new System.Drawing.Size(136, 22);
            this.CustCombo.Style = DevComponents.DotNetBar.eDotNetBarStyle.StyleManagerControlled;
            this.CustCombo.TabIndex = 2;
            this.CustCombo.SelectedIndexChanged += new System.EventHandler(this.CustCombo_SelectedIndexChanged);
            // 
            // labelX2
            // 
            // 
            // 
            // 
            this.labelX2.BackgroundStyle.CornerType = DevComponents.DotNetBar.eCornerType.Square;
            this.labelX2.Font = new System.Drawing.Font("標楷體", 12F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(136)));
            this.labelX2.Location = new System.Drawing.Point(280, 13);
            this.labelX2.Name = "labelX2";
            this.labelX2.Size = new System.Drawing.Size(75, 23);
            this.labelX2.TabIndex = 3;
            this.labelX2.Text = "料號：";
            // 
            // PartNoCombo
            // 
            this.PartNoCombo.DisplayMember = "Text";
            this.PartNoCombo.DrawMode = System.Windows.Forms.DrawMode.OwnerDrawFixed;
            this.PartNoCombo.DropDownStyle = System.Windows.Forms.ComboBoxStyle.DropDownList;
            this.PartNoCombo.FormattingEnabled = true;
            this.PartNoCombo.ItemHeight = 16;
            this.PartNoCombo.Location = new System.Drawing.Point(335, 13);
            this.PartNoCombo.Name = "PartNoCombo";
            this.PartNoCombo.Size = new System.Drawing.Size(170, 22);
            this.PartNoCombo.Style = DevComponents.DotNetBar.eDotNetBarStyle.StyleManagerControlled;
            this.PartNoCombo.TabIndex = 4;
            this.PartNoCombo.SelectedIndexChanged += new System.EventHandler(this.PartNoCombo_SelectedIndexChanged);
            // 
            // CusRevCombo
            // 
            this.CusRevCombo.DisplayMember = "Text";
            this.CusRevCombo.DrawMode = System.Windows.Forms.DrawMode.OwnerDrawFixed;
            this.CusRevCombo.DropDownStyle = System.Windows.Forms.ComboBoxStyle.DropDownList;
            this.CusRevCombo.FormattingEnabled = true;
            this.CusRevCombo.ItemHeight = 16;
            this.CusRevCombo.Location = new System.Drawing.Point(673, 12);
            this.CusRevCombo.Name = "CusRevCombo";
            this.CusRevCombo.Size = new System.Drawing.Size(49, 22);
            this.CusRevCombo.Style = DevComponents.DotNetBar.eDotNetBarStyle.StyleManagerControlled;
            this.CusRevCombo.TabIndex = 5;
            this.CusRevCombo.SelectedIndexChanged += new System.EventHandler(this.CusRevCombo_SelectedIndexChanged);
            // 
            // OpRevCombo
            // 
            this.OpRevCombo.DisplayMember = "Text";
            this.OpRevCombo.DrawMode = System.Windows.Forms.DrawMode.OwnerDrawFixed;
            this.OpRevCombo.DropDownStyle = System.Windows.Forms.ComboBoxStyle.DropDownList;
            this.OpRevCombo.FormattingEnabled = true;
            this.OpRevCombo.ItemHeight = 16;
            this.OpRevCombo.Location = new System.Drawing.Point(886, 12);
            this.OpRevCombo.Name = "OpRevCombo";
            this.OpRevCombo.Size = new System.Drawing.Size(49, 22);
            this.OpRevCombo.Style = DevComponents.DotNetBar.eDotNetBarStyle.StyleManagerControlled;
            this.OpRevCombo.TabIndex = 6;
            this.OpRevCombo.SelectedIndexChanged += new System.EventHandler(this.OpRevCombo_SelectedIndexChanged);
            // 
            // labelX3
            // 
            // 
            // 
            // 
            this.labelX3.BackgroundStyle.CornerType = DevComponents.DotNetBar.eCornerType.Square;
            this.labelX3.Font = new System.Drawing.Font("標楷體", 12F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(136)));
            this.labelX3.Location = new System.Drawing.Point(586, 12);
            this.labelX3.Name = "labelX3";
            this.labelX3.Size = new System.Drawing.Size(99, 23);
            this.labelX3.TabIndex = 8;
            this.labelX3.Text = "客戶版次：";
            // 
            // labelX4
            // 
            // 
            // 
            // 
            this.labelX4.BackgroundStyle.CornerType = DevComponents.DotNetBar.eCornerType.Square;
            this.labelX4.Font = new System.Drawing.Font("標楷體", 12F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(136)));
            this.labelX4.Location = new System.Drawing.Point(798, 11);
            this.labelX4.Name = "labelX4";
            this.labelX4.Size = new System.Drawing.Size(99, 23);
            this.labelX4.TabIndex = 9;
            this.labelX4.Text = "製程版次：";
            // 
            // buttonItem1
            // 
            this.buttonItem1.Name = "buttonItem1";
            this.buttonItem1.Text = "buttonItem1";
            // 
            // PFM
            // 
            this.PFM.AccessibleRole = System.Windows.Forms.AccessibleRole.PushButton;
            this.PFM.ColorTable = DevComponents.DotNetBar.eButtonColor.Flat;
            this.PFM.Font = new System.Drawing.Font("標楷體", 11.25F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(136)));
            this.PFM.Location = new System.Drawing.Point(12, 388);
            this.PFM.Name = "PFM";
            this.PFM.Size = new System.Drawing.Size(110, 32);
            this.PFM.Style = DevComponents.DotNetBar.eDotNetBarStyle.StyleManagerControlled;
            this.PFM.TabIndex = 13;
            this.PFM.Text = "潛在失效模式";
            this.PFM.Click += new System.EventHandler(this.PFM_Click);
            // 
            // PEoF
            // 
            this.PEoF.AccessibleRole = System.Windows.Forms.AccessibleRole.PushButton;
            this.PEoF.ColorTable = DevComponents.DotNetBar.eButtonColor.Flat;
            this.PEoF.Font = new System.Drawing.Font("標楷體", 11.25F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(136)));
            this.PEoF.Location = new System.Drawing.Point(128, 388);
            this.PEoF.Name = "PEoF";
            this.PEoF.Size = new System.Drawing.Size(110, 32);
            this.PEoF.Style = DevComponents.DotNetBar.eDotNetBarStyle.StyleManagerControlled;
            this.PEoF.TabIndex = 14;
            this.PEoF.Text = "潛在失效影響";
            this.PEoF.Click += new System.EventHandler(this.PEoF_Click);
            // 
            // Class
            // 
            this.Class.AccessibleRole = System.Windows.Forms.AccessibleRole.PushButton;
            this.Class.ColorTable = DevComponents.DotNetBar.eButtonColor.Flat;
            this.Class.Font = new System.Drawing.Font("標楷體", 11.25F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(136)));
            this.Class.Location = new System.Drawing.Point(12, 426);
            this.Class.Name = "Class";
            this.Class.Size = new System.Drawing.Size(110, 32);
            this.Class.Style = DevComponents.DotNetBar.eDotNetBarStyle.StyleManagerControlled;
            this.Class.TabIndex = 16;
            this.Class.Text = "Class";
            this.Class.Click += new System.EventHandler(this.Class_Click);
            // 
            // PCoF
            // 
            this.PCoF.AccessibleRole = System.Windows.Forms.AccessibleRole.PushButton;
            this.PCoF.ColorTable = DevComponents.DotNetBar.eButtonColor.Flat;
            this.PCoF.Font = new System.Drawing.Font("標楷體", 11.25F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(136)));
            this.PCoF.Location = new System.Drawing.Point(244, 388);
            this.PCoF.Name = "PCoF";
            this.PCoF.Size = new System.Drawing.Size(110, 32);
            this.PCoF.Style = DevComponents.DotNetBar.eDotNetBarStyle.StyleManagerControlled;
            this.PCoF.TabIndex = 17;
            this.PCoF.Text = "潛在失效原因";
            this.PCoF.Click += new System.EventHandler(this.PCoF_Click);
            // 
            // CPCP
            // 
            this.CPCP.AccessibleRole = System.Windows.Forms.AccessibleRole.PushButton;
            this.CPCP.ColorTable = DevComponents.DotNetBar.eButtonColor.Flat;
            this.CPCP.Font = new System.Drawing.Font("標楷體", 11.25F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(136)));
            this.CPCP.Location = new System.Drawing.Point(128, 426);
            this.CPCP.Name = "CPCP";
            this.CPCP.Size = new System.Drawing.Size(110, 32);
            this.CPCP.Style = DevComponents.DotNetBar.eDotNetBarStyle.StyleManagerControlled;
            this.CPCP.TabIndex = 19;
            this.CPCP.Text = "預防措施";
            this.CPCP.Click += new System.EventHandler(this.CPCP_Click);
            // 
            // CPCD
            // 
            this.CPCD.AccessibleRole = System.Windows.Forms.AccessibleRole.PushButton;
            this.CPCD.ColorTable = DevComponents.DotNetBar.eButtonColor.Flat;
            this.CPCD.Font = new System.Drawing.Font("標楷體", 11.25F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(136)));
            this.CPCD.Location = new System.Drawing.Point(244, 426);
            this.CPCD.Name = "CPCD";
            this.CPCD.Size = new System.Drawing.Size(110, 32);
            this.CPCD.Style = DevComponents.DotNetBar.eDotNetBarStyle.StyleManagerControlled;
            this.CPCD.TabIndex = 20;
            this.CPCD.Text = "預防措施檢測";
            this.CPCD.Click += new System.EventHandler(this.CPCD_Click);
            // 
            // SevCombo
            // 
            this.SevCombo.DisplayMember = "Text";
            this.SevCombo.DrawMode = System.Windows.Forms.DrawMode.OwnerDrawFixed;
            this.SevCombo.DropDownStyle = System.Windows.Forms.ComboBoxStyle.DropDownList;
            this.SevCombo.Font = new System.Drawing.Font("新細明體", 9F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(136)));
            this.SevCombo.FormattingEnabled = true;
            this.SevCombo.ItemHeight = 16;
            this.SevCombo.Location = new System.Drawing.Point(60, 26);
            this.SevCombo.Name = "SevCombo";
            this.SevCombo.Size = new System.Drawing.Size(52, 22);
            this.SevCombo.Style = DevComponents.DotNetBar.eDotNetBarStyle.StyleManagerControlled;
            this.SevCombo.TabIndex = 21;
            // 
            // OccCombo
            // 
            this.OccCombo.DisplayMember = "Text";
            this.OccCombo.DrawMode = System.Windows.Forms.DrawMode.OwnerDrawFixed;
            this.OccCombo.DropDownStyle = System.Windows.Forms.ComboBoxStyle.DropDownList;
            this.OccCombo.Font = new System.Drawing.Font("新細明體", 9F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(136)));
            this.OccCombo.FormattingEnabled = true;
            this.OccCombo.ItemHeight = 16;
            this.OccCombo.Location = new System.Drawing.Point(172, 26);
            this.OccCombo.Name = "OccCombo";
            this.OccCombo.Size = new System.Drawing.Size(52, 22);
            this.OccCombo.Style = DevComponents.DotNetBar.eDotNetBarStyle.StyleManagerControlled;
            this.OccCombo.TabIndex = 22;
            // 
            // DetCombo
            // 
            this.DetCombo.DisplayMember = "Text";
            this.DetCombo.DrawMode = System.Windows.Forms.DrawMode.OwnerDrawFixed;
            this.DetCombo.DropDownStyle = System.Windows.Forms.ComboBoxStyle.DropDownList;
            this.DetCombo.Font = new System.Drawing.Font("新細明體", 9F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(136)));
            this.DetCombo.FormattingEnabled = true;
            this.DetCombo.ItemHeight = 16;
            this.DetCombo.Location = new System.Drawing.Point(284, 26);
            this.DetCombo.Name = "DetCombo";
            this.DetCombo.Size = new System.Drawing.Size(52, 22);
            this.DetCombo.Style = DevComponents.DotNetBar.eDotNetBarStyle.StyleManagerControlled;
            this.DetCombo.TabIndex = 23;
            // 
            // labelX5
            // 
            // 
            // 
            // 
            this.labelX5.BackgroundStyle.CornerType = DevComponents.DotNetBar.eCornerType.Square;
            this.labelX5.Font = new System.Drawing.Font("標楷體", 11.25F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(136)));
            this.labelX5.Location = new System.Drawing.Point(15, 26);
            this.labelX5.Name = "labelX5";
            this.labelX5.Size = new System.Drawing.Size(39, 23);
            this.labelX5.TabIndex = 24;
            this.labelX5.Text = "嚴重";
            // 
            // labelX6
            // 
            // 
            // 
            // 
            this.labelX6.BackgroundStyle.CornerType = DevComponents.DotNetBar.eCornerType.Square;
            this.labelX6.Font = new System.Drawing.Font("標楷體", 11.25F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(136)));
            this.labelX6.Location = new System.Drawing.Point(127, 26);
            this.labelX6.Name = "labelX6";
            this.labelX6.Size = new System.Drawing.Size(39, 23);
            this.labelX6.TabIndex = 25;
            this.labelX6.Text = "發生";
            // 
            // labelX7
            // 
            // 
            // 
            // 
            this.labelX7.BackgroundStyle.CornerType = DevComponents.DotNetBar.eCornerType.Square;
            this.labelX7.Font = new System.Drawing.Font("標楷體", 11.25F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(136)));
            this.labelX7.Location = new System.Drawing.Point(239, 25);
            this.labelX7.Name = "labelX7";
            this.labelX7.Size = new System.Drawing.Size(39, 23);
            this.labelX7.TabIndex = 26;
            this.labelX7.Text = "偵測";
            // 
            // groupBox1
            // 
            this.groupBox1.Controls.Add(this.OccCombo);
            this.groupBox1.Controls.Add(this.SevCombo);
            this.groupBox1.Controls.Add(this.IndexSetting);
            this.groupBox1.Controls.Add(this.labelX5);
            this.groupBox1.Controls.Add(this.labelX7);
            this.groupBox1.Controls.Add(this.labelX6);
            this.groupBox1.Controls.Add(this.DetCombo);
            this.groupBox1.Font = new System.Drawing.Font("標楷體", 11.25F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(136)));
            this.groupBox1.Location = new System.Drawing.Point(360, 390);
            this.groupBox1.Name = "groupBox1";
            this.groupBox1.Size = new System.Drawing.Size(399, 68);
            this.groupBox1.TabIndex = 27;
            this.groupBox1.TabStop = false;
            this.groupBox1.Text = "指數設定";
            // 
            // IndexSetting
            // 
            this.IndexSetting.AccessibleRole = System.Windows.Forms.AccessibleRole.PushButton;
            this.IndexSetting.ColorTable = DevComponents.DotNetBar.eButtonColor.Flat;
            this.IndexSetting.Font = new System.Drawing.Font("標楷體", 12F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(136)));
            this.IndexSetting.Image = global::PFMEA.Properties.Resources.Modify_16px;
            this.IndexSetting.Location = new System.Drawing.Point(350, 25);
            this.IndexSetting.Name = "IndexSetting";
            this.IndexSetting.Size = new System.Drawing.Size(34, 25);
            this.IndexSetting.Style = DevComponents.DotNetBar.eDotNetBarStyle.StyleManagerControlled;
            this.IndexSetting.TabIndex = 27;
            this.IndexSetting.Click += new System.EventHandler(this.IndexSetting_Click);
            // 
            // Cancel
            // 
            this.Cancel.AccessibleRole = System.Windows.Forms.AccessibleRole.PushButton;
            this.Cancel.ColorTable = DevComponents.DotNetBar.eButtonColor.Flat;
            this.Cancel.Font = new System.Drawing.Font("標楷體", 12F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(136)));
            this.Cancel.Image = global::PFMEA.Properties.Resources.Error_24px;
            this.Cancel.Location = new System.Drawing.Point(860, 390);
            this.Cancel.Name = "Cancel";
            this.Cancel.Size = new System.Drawing.Size(75, 68);
            this.Cancel.Style = DevComponents.DotNetBar.eDotNetBarStyle.StyleManagerControlled;
            this.Cancel.TabIndex = 12;
            this.Cancel.Text = "關閉";
            this.Cancel.Click += new System.EventHandler(this.Cancel_Click);
            // 
            // OK
            // 
            this.OK.AccessibleRole = System.Windows.Forms.AccessibleRole.PushButton;
            this.OK.ColorTable = DevComponents.DotNetBar.eButtonColor.Flat;
            this.OK.Font = new System.Drawing.Font("標楷體", 12F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(136)));
            this.OK.Image = global::PFMEA.Properties.Resources.Confirm_24px;
            this.OK.Location = new System.Drawing.Point(779, 390);
            this.OK.Name = "OK";
            this.OK.Size = new System.Drawing.Size(75, 68);
            this.OK.Style = DevComponents.DotNetBar.eDotNetBarStyle.StyleManagerControlled;
            this.OK.TabIndex = 11;
            this.OK.Text = "輸出";
            this.OK.Click += new System.EventHandler(this.OK_Click);
            // 
            // Form1
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 12F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.ClientSize = new System.Drawing.Size(949, 475);
            this.Controls.Add(this.groupBox1);
            this.Controls.Add(this.CPCD);
            this.Controls.Add(this.CPCP);
            this.Controls.Add(this.PCoF);
            this.Controls.Add(this.Class);
            this.Controls.Add(this.PEoF);
            this.Controls.Add(this.PFM);
            this.Controls.Add(this.Cancel);
            this.Controls.Add(this.OK);
            this.Controls.Add(this.OpRevCombo);
            this.Controls.Add(this.labelX4);
            this.Controls.Add(this.CusRevCombo);
            this.Controls.Add(this.labelX3);
            this.Controls.Add(this.PartNoCombo);
            this.Controls.Add(this.labelX2);
            this.Controls.Add(this.CustCombo);
            this.Controls.Add(this.labelX1);
            this.Controls.Add(this.OISTree);
            this.DoubleBuffered = true;
            this.Icon = ((System.Drawing.Icon)(resources.GetObject("$this.Icon")));
            this.Name = "Form1";
            this.Text = "PFMEA";
            ((System.ComponentModel.ISupportInitialize)(this.OISTree)).EndInit();
            this.groupBox1.ResumeLayout(false);
            this.ResumeLayout(false);

        }

        #endregion

        private DevComponents.AdvTree.AdvTree OISTree;
        private DevComponents.AdvTree.NodeConnector nodeConnector1;
        private DevComponents.DotNetBar.StyleManager styleManager1;
        private DevComponents.AdvTree.ColumnHeader columnHeader1;
        private DevComponents.AdvTree.ColumnHeader columnHeader2;
        private DevComponents.DotNetBar.LabelX labelX1;
        private DevComponents.DotNetBar.Controls.ComboBoxEx CustCombo;
        private DevComponents.DotNetBar.LabelX labelX2;
        private DevComponents.DotNetBar.Controls.ComboBoxEx PartNoCombo;
        private DevComponents.DotNetBar.Controls.ComboBoxEx CusRevCombo;
        private DevComponents.DotNetBar.Controls.ComboBoxEx OpRevCombo;
        private DevComponents.DotNetBar.LabelX labelX3;
        private DevComponents.DotNetBar.LabelX labelX4;
        private DevComponents.DotNetBar.ButtonX OK;
        private DevComponents.DotNetBar.ButtonX Cancel;
        private DevComponents.DotNetBar.ButtonItem buttonItem1;
        private DevComponents.DotNetBar.ButtonX PFM;
        private DevComponents.DotNetBar.ButtonX PEoF;
        private DevComponents.DotNetBar.ButtonX Class;
        private DevComponents.DotNetBar.ButtonX PCoF;
        private DevComponents.DotNetBar.ButtonX CPCP;
        private DevComponents.DotNetBar.ButtonX CPCD;
        private DevComponents.DotNetBar.Controls.ComboBoxEx SevCombo;
        private DevComponents.DotNetBar.Controls.ComboBoxEx OccCombo;
        private DevComponents.DotNetBar.Controls.ComboBoxEx DetCombo;
        private DevComponents.DotNetBar.LabelX labelX5;
        private DevComponents.DotNetBar.LabelX labelX6;
        private DevComponents.DotNetBar.LabelX labelX7;
        private System.Windows.Forms.GroupBox groupBox1;
        private DevComponents.DotNetBar.ButtonX IndexSetting;
        private DevComponents.DotNetBar.ElementStyle elementStyle1;
    }
}

