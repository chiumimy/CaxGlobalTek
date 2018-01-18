namespace OutputExcelForm
{
    partial class OutputForm
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
        public void InitializeComponent()
        {
            System.ComponentModel.ComponentResourceManager resources = new System.ComponentModel.ComponentResourceManager(typeof(OutputForm));
            this.labelX1 = new DevComponents.DotNetBar.LabelX();
            this.CusComboBox = new DevComponents.DotNetBar.Controls.ComboBoxEx();
            this.labelX2 = new DevComponents.DotNetBar.LabelX();
            this.PartNoCombobox = new DevComponents.DotNetBar.Controls.ComboBoxEx();
            this.labelX3 = new DevComponents.DotNetBar.LabelX();
            this.CusVerCombobox = new DevComponents.DotNetBar.Controls.ComboBoxEx();
            this.SuperTabControl = new DevComponents.DotNetBar.SuperTabControl();
            this.superTabControlPanel4 = new DevComponents.DotNetBar.SuperTabControlPanel();
            this.SGC_FixInsPanel = new DevComponents.DotNetBar.SuperGrid.SuperGridControl();
            this.gridColumn8 = new DevComponents.DotNetBar.SuperGrid.GridColumn();
            this.gridColumn13 = new DevComponents.DotNetBar.SuperGrid.GridColumn();
            this.gridColumn14 = new DevComponents.DotNetBar.SuperGrid.GridColumn();
            this.gridColumn15 = new DevComponents.DotNetBar.SuperGrid.GridColumn();
            this.gridColumn16 = new DevComponents.DotNetBar.SuperGrid.GridColumn();
            this.Tab_FixIns = new DevComponents.DotNetBar.SuperTabItem();
            this.superTabControlPanel2 = new DevComponents.DotNetBar.SuperTabControlPanel();
            this.SGC_TEPanel = new DevComponents.DotNetBar.SuperGrid.SuperGridControl();
            this.gridColumn1 = new DevComponents.DotNetBar.SuperGrid.GridColumn();
            this.gridColumn2 = new DevComponents.DotNetBar.SuperGrid.GridColumn();
            this.gridColumn3 = new DevComponents.DotNetBar.SuperGrid.GridColumn();
            this.gridColumn4 = new DevComponents.DotNetBar.SuperGrid.GridColumn();
            this.gridColumn5 = new DevComponents.DotNetBar.SuperGrid.GridColumn();
            this.Tab_TE = new DevComponents.DotNetBar.SuperTabItem();
            this.superTabControlPanel3 = new DevComponents.DotNetBar.SuperTabControlPanel();
            this.SGC_PEPanel = new DevComponents.DotNetBar.SuperGrid.SuperGridControl();
            this.gridColumn7 = new DevComponents.DotNetBar.SuperGrid.GridColumn();
            this.gridColumn9 = new DevComponents.DotNetBar.SuperGrid.GridColumn();
            this.gridColumn10 = new DevComponents.DotNetBar.SuperGrid.GridColumn();
            this.gridColumn11 = new DevComponents.DotNetBar.SuperGrid.GridColumn();
            this.gridColumn12 = new DevComponents.DotNetBar.SuperGrid.GridColumn();
            this.Tab_PE = new DevComponents.DotNetBar.SuperTabItem();
            this.superTabControlPanel1 = new DevComponents.DotNetBar.SuperTabControlPanel();
            this.SGC_MEPanel = new DevComponents.DotNetBar.SuperGrid.SuperGridControl();
            this.select = new DevComponents.DotNetBar.SuperGrid.GridColumn();
            this.ExcelType = new DevComponents.DotNetBar.SuperGrid.GridColumn();
            this.gridColumn6 = new DevComponents.DotNetBar.SuperGrid.GridColumn();
            this.ExcelForm = new DevComponents.DotNetBar.SuperGrid.GridColumn();
            this.OutputPath = new DevComponents.DotNetBar.SuperGrid.GridColumn();
            this.Tab_ME = new DevComponents.DotNetBar.SuperTabItem();
            this.pictureBox4 = new System.Windows.Forms.PictureBox();
            this.pictureBox3 = new System.Windows.Forms.PictureBox();
            this.pictureBox2 = new System.Windows.Forms.PictureBox();
            this.pictureBox1 = new System.Windows.Forms.PictureBox();
            this.labelX4 = new DevComponents.DotNetBar.LabelX();
            this.Op1Combobox = new DevComponents.DotNetBar.Controls.ComboBoxEx();
            this.OK = new DevComponents.DotNetBar.ButtonX();
            this.Close = new DevComponents.DotNetBar.ButtonX();
            this.highlighter1 = new DevComponents.DotNetBar.Validator.Highlighter();
            this.labelX5 = new DevComponents.DotNetBar.LabelX();
            this.OpVerCombobox = new DevComponents.DotNetBar.Controls.ComboBoxEx();
            this.labelX6 = new DevComponents.DotNetBar.LabelX();
            this.Op2Text = new DevComponents.DotNetBar.LabelX();
            ((System.ComponentModel.ISupportInitialize)(this.SuperTabControl)).BeginInit();
            this.SuperTabControl.SuspendLayout();
            this.superTabControlPanel4.SuspendLayout();
            this.superTabControlPanel2.SuspendLayout();
            this.superTabControlPanel3.SuspendLayout();
            this.superTabControlPanel1.SuspendLayout();
            ((System.ComponentModel.ISupportInitialize)(this.pictureBox4)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.pictureBox3)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.pictureBox2)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.pictureBox1)).BeginInit();
            this.SuspendLayout();
            // 
            // labelX1
            // 
            // 
            // 
            // 
            this.labelX1.BackgroundStyle.CornerType = DevComponents.DotNetBar.eCornerType.Square;
            this.labelX1.Font = new System.Drawing.Font("標楷體", 12F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(136)));
            this.labelX1.Location = new System.Drawing.Point(44, 12);
            this.labelX1.Name = "labelX1";
            this.labelX1.Size = new System.Drawing.Size(58, 35);
            this.labelX1.TabIndex = 0;
            this.labelX1.Text = "客戶：";
            // 
            // CusComboBox
            // 
            this.CusComboBox.DisplayMember = "Text";
            this.CusComboBox.DrawMode = System.Windows.Forms.DrawMode.OwnerDrawFixed;
            this.CusComboBox.DropDownStyle = System.Windows.Forms.ComboBoxStyle.DropDownList;
            this.CusComboBox.FormattingEnabled = true;
            this.CusComboBox.ItemHeight = 16;
            this.CusComboBox.Location = new System.Drawing.Point(97, 17);
            this.CusComboBox.Name = "CusComboBox";
            this.CusComboBox.Size = new System.Drawing.Size(136, 22);
            this.CusComboBox.Style = DevComponents.DotNetBar.eDotNetBarStyle.StyleManagerControlled;
            this.CusComboBox.TabIndex = 2;
            this.CusComboBox.SelectedIndexChanged += new System.EventHandler(this.CusComboBox_SelectedIndexChanged);
            // 
            // labelX2
            // 
            // 
            // 
            // 
            this.labelX2.BackgroundStyle.CornerType = DevComponents.DotNetBar.eCornerType.Square;
            this.labelX2.Font = new System.Drawing.Font("標楷體", 12F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(136)));
            this.labelX2.Location = new System.Drawing.Point(284, 12);
            this.labelX2.Name = "labelX2";
            this.labelX2.Size = new System.Drawing.Size(58, 35);
            this.labelX2.TabIndex = 4;
            this.labelX2.Text = "料號：";
            // 
            // PartNoCombobox
            // 
            this.PartNoCombobox.DisplayMember = "Text";
            this.PartNoCombobox.DrawMode = System.Windows.Forms.DrawMode.OwnerDrawFixed;
            this.PartNoCombobox.DropDownStyle = System.Windows.Forms.ComboBoxStyle.DropDownList;
            this.PartNoCombobox.FormattingEnabled = true;
            this.PartNoCombobox.ItemHeight = 16;
            this.PartNoCombobox.Location = new System.Drawing.Point(335, 18);
            this.PartNoCombobox.Name = "PartNoCombobox";
            this.PartNoCombobox.Size = new System.Drawing.Size(164, 22);
            this.PartNoCombobox.Style = DevComponents.DotNetBar.eDotNetBarStyle.StyleManagerControlled;
            this.PartNoCombobox.TabIndex = 5;
            this.PartNoCombobox.SelectedIndexChanged += new System.EventHandler(this.PartNoCombobox_SelectedIndexChanged);
            // 
            // labelX3
            // 
            // 
            // 
            // 
            this.labelX3.BackgroundStyle.CornerType = DevComponents.DotNetBar.eCornerType.Square;
            this.labelX3.Font = new System.Drawing.Font("標楷體", 12F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(136)));
            this.labelX3.Location = new System.Drawing.Point(44, 53);
            this.labelX3.Name = "labelX3";
            this.labelX3.Size = new System.Drawing.Size(89, 35);
            this.labelX3.TabIndex = 7;
            this.labelX3.Text = "客版：";
            // 
            // CusVerCombobox
            // 
            this.CusVerCombobox.DisplayMember = "Text";
            this.CusVerCombobox.DrawMode = System.Windows.Forms.DrawMode.OwnerDrawFixed;
            this.CusVerCombobox.DropDownStyle = System.Windows.Forms.ComboBoxStyle.DropDownList;
            this.CusVerCombobox.FormattingEnabled = true;
            this.CusVerCombobox.ItemHeight = 16;
            this.CusVerCombobox.Location = new System.Drawing.Point(96, 58);
            this.CusVerCombobox.Name = "CusVerCombobox";
            this.CusVerCombobox.Size = new System.Drawing.Size(73, 22);
            this.CusVerCombobox.Style = DevComponents.DotNetBar.eDotNetBarStyle.StyleManagerControlled;
            this.CusVerCombobox.TabIndex = 8;
            this.CusVerCombobox.SelectedIndexChanged += new System.EventHandler(this.CusVerCombobox_SelectedIndexChanged);
            // 
            // SuperTabControl
            // 
            // 
            // 
            // 
            // 
            // 
            // 
            this.SuperTabControl.ControlBox.CloseBox.Name = "";
            // 
            // 
            // 
            this.SuperTabControl.ControlBox.MenuBox.Name = "";
            this.SuperTabControl.ControlBox.Name = "";
            this.SuperTabControl.ControlBox.SubItems.AddRange(new DevComponents.DotNetBar.BaseItem[] {
            this.SuperTabControl.ControlBox.MenuBox,
            this.SuperTabControl.ControlBox.CloseBox});
            this.SuperTabControl.Controls.Add(this.superTabControlPanel4);
            this.SuperTabControl.Controls.Add(this.superTabControlPanel2);
            this.SuperTabControl.Controls.Add(this.superTabControlPanel1);
            this.SuperTabControl.Controls.Add(this.superTabControlPanel3);
            this.SuperTabControl.Location = new System.Drawing.Point(12, 127);
            this.SuperTabControl.Name = "SuperTabControl";
            this.SuperTabControl.ReorderTabsEnabled = true;
            this.SuperTabControl.SelectedTabFont = new System.Drawing.Font("新細明體", 9F, System.Drawing.FontStyle.Bold);
            this.SuperTabControl.SelectedTabIndex = 0;
            this.SuperTabControl.Size = new System.Drawing.Size(487, 233);
            this.SuperTabControl.TabFont = new System.Drawing.Font("標楷體", 12F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(136)));
            this.SuperTabControl.TabIndex = 9;
            this.SuperTabControl.Tabs.AddRange(new DevComponents.DotNetBar.BaseItem[] {
            this.Tab_PE,
            this.Tab_ME,
            this.Tab_TE,
            this.Tab_FixIns});
            this.SuperTabControl.Text = "superTabControl1";
            // 
            // superTabControlPanel4
            // 
            this.superTabControlPanel4.Controls.Add(this.SGC_FixInsPanel);
            this.superTabControlPanel4.Dock = System.Windows.Forms.DockStyle.Fill;
            this.superTabControlPanel4.Location = new System.Drawing.Point(0, 46);
            this.superTabControlPanel4.Name = "superTabControlPanel4";
            this.superTabControlPanel4.Size = new System.Drawing.Size(487, 187);
            this.superTabControlPanel4.TabIndex = 0;
            this.superTabControlPanel4.TabItem = this.Tab_FixIns;
            // 
            // SGC_FixInsPanel
            // 
            this.SGC_FixInsPanel.FilterExprColors.SysFunction = System.Drawing.Color.DarkRed;
            this.SGC_FixInsPanel.Location = new System.Drawing.Point(3, 3);
            this.SGC_FixInsPanel.Name = "SGC_FixInsPanel";
            // 
            // 
            // 
            this.SGC_FixInsPanel.PrimaryGrid.Columns.Add(this.gridColumn8);
            this.SGC_FixInsPanel.PrimaryGrid.Columns.Add(this.gridColumn13);
            this.SGC_FixInsPanel.PrimaryGrid.Columns.Add(this.gridColumn14);
            this.SGC_FixInsPanel.PrimaryGrid.Columns.Add(this.gridColumn15);
            this.SGC_FixInsPanel.PrimaryGrid.Columns.Add(this.gridColumn16);
            this.SGC_FixInsPanel.PrimaryGrid.ShowRowHeaders = false;
            this.SGC_FixInsPanel.Size = new System.Drawing.Size(481, 181);
            this.SGC_FixInsPanel.TabIndex = 0;
            this.SGC_FixInsPanel.Text = "superGridControl1";
            // 
            // gridColumn8
            // 
            this.gridColumn8.CellStyles.Default.Alignment = DevComponents.DotNetBar.SuperGrid.Style.Alignment.MiddleCenter;
            this.gridColumn8.EditorType = typeof(DevComponents.DotNetBar.SuperGrid.GridCheckBoxXEditControl);
            this.gridColumn8.Name = "選擇";
            this.gridColumn8.Width = 40;
            // 
            // gridColumn13
            // 
            this.gridColumn13.CellStyles.Default.Alignment = DevComponents.DotNetBar.SuperGrid.Style.Alignment.MiddleLeft;
            this.gridColumn13.EditorType = typeof(DevComponents.DotNetBar.SuperGrid.GridLabelXEditControl);
            this.gridColumn13.Name = "檔案名稱";
            this.gridColumn13.Width = 130;
            // 
            // gridColumn14
            // 
            this.gridColumn14.CellStyles.Default.Alignment = DevComponents.DotNetBar.SuperGrid.Style.Alignment.MiddleLeft;
            this.gridColumn14.EditorType = typeof(DevComponents.DotNetBar.SuperGrid.GridLabelXEditControl);
            this.gridColumn14.Name = "品名";
            this.gridColumn14.Width = 130;
            // 
            // gridColumn15
            // 
            this.gridColumn15.CellStyles.Default.Alignment = DevComponents.DotNetBar.SuperGrid.Style.Alignment.MiddleLeft;
            this.gridColumn15.EditorType = typeof(DevComponents.DotNetBar.SuperGrid.GridLabelXEditControl);
            this.gridColumn15.Name = "模.檢.治編號";
            this.gridColumn15.Width = 130;
            // 
            // gridColumn16
            // 
            this.gridColumn16.CellStyles.Default.Alignment = DevComponents.DotNetBar.SuperGrid.Style.Alignment.MiddleLeft;
            this.gridColumn16.EditorType = typeof(DevComponents.DotNetBar.SuperGrid.GridLabelXEditControl);
            this.gridColumn16.Name = "ERP編號";
            this.gridColumn16.Width = 130;
            // 
            // Tab_FixIns
            // 
            this.Tab_FixIns.AttachedControl = this.superTabControlPanel4;
            this.Tab_FixIns.GlobalItem = false;
            this.Tab_FixIns.Image = global::OutputExcelForm.Properties.Resources.ruler_32px;
            this.Tab_FixIns.Name = "Tab_FixIns";
            this.Tab_FixIns.Text = "模.檢.治";
            // 
            // superTabControlPanel2
            // 
            this.superTabControlPanel2.Controls.Add(this.SGC_TEPanel);
            this.superTabControlPanel2.Dock = System.Windows.Forms.DockStyle.Fill;
            this.superTabControlPanel2.Location = new System.Drawing.Point(0, 46);
            this.superTabControlPanel2.Name = "superTabControlPanel2";
            this.superTabControlPanel2.Size = new System.Drawing.Size(487, 187);
            this.superTabControlPanel2.TabIndex = 0;
            this.superTabControlPanel2.TabItem = this.Tab_TE;
            // 
            // SGC_TEPanel
            // 
            this.SGC_TEPanel.FilterExprColors.SysFunction = System.Drawing.Color.DarkRed;
            this.SGC_TEPanel.Location = new System.Drawing.Point(3, 3);
            this.SGC_TEPanel.Name = "SGC_TEPanel";
            // 
            // 
            // 
            this.SGC_TEPanel.PrimaryGrid.Columns.Add(this.gridColumn1);
            this.SGC_TEPanel.PrimaryGrid.Columns.Add(this.gridColumn2);
            this.SGC_TEPanel.PrimaryGrid.Columns.Add(this.gridColumn3);
            this.SGC_TEPanel.PrimaryGrid.Columns.Add(this.gridColumn4);
            this.SGC_TEPanel.PrimaryGrid.Columns.Add(this.gridColumn5);
            this.SGC_TEPanel.PrimaryGrid.ShowRowHeaders = false;
            this.SGC_TEPanel.Size = new System.Drawing.Size(481, 181);
            this.SGC_TEPanel.TabIndex = 0;
            this.SGC_TEPanel.Text = "superGridControl1";
            // 
            // gridColumn1
            // 
            this.gridColumn1.CellStyles.Default.Alignment = DevComponents.DotNetBar.SuperGrid.Style.Alignment.MiddleCenter;
            this.gridColumn1.EditorType = typeof(DevComponents.DotNetBar.SuperGrid.GridCheckBoxXEditControl);
            this.gridColumn1.Name = "選擇";
            this.gridColumn1.Width = 40;
            // 
            // gridColumn2
            // 
            this.gridColumn2.CellStyles.Default.Alignment = DevComponents.DotNetBar.SuperGrid.Style.Alignment.MiddleCenter;
            this.gridColumn2.EditorType = typeof(DevComponents.DotNetBar.SuperGrid.GridLabelXEditControl);
            this.gridColumn2.Name = "Excel";
            this.gridColumn2.Width = 60;
            // 
            // gridColumn3
            // 
            this.gridColumn3.CellStyles.Default.Alignment = DevComponents.DotNetBar.SuperGrid.Style.Alignment.MiddleCenter;
            this.gridColumn3.EditorType = typeof(DevComponents.DotNetBar.SuperGrid.GridLabelXEditControl);
            this.gridColumn3.Name = "NC群組";
            this.gridColumn3.Width = 80;
            // 
            // gridColumn4
            // 
            this.gridColumn4.CellStyles.Default.Alignment = DevComponents.DotNetBar.SuperGrid.Style.Alignment.MiddleCenter;
            this.gridColumn4.EditorType = typeof(DevComponents.DotNetBar.SuperGrid.GridComboBoxExEditControl);
            this.gridColumn4.Name = "廠區";
            // 
            // gridColumn5
            // 
            this.gridColumn5.AutoSizeMode = DevComponents.DotNetBar.SuperGrid.ColumnAutoSizeMode.Fill;
            this.gridColumn5.EditorType = typeof(DevComponents.DotNetBar.SuperGrid.GridLabelXEditControl);
            this.gridColumn5.Name = "輸出路徑(桌面)";
            // 
            // Tab_TE
            // 
            this.Tab_TE.AttachedControl = this.superTabControlPanel2;
            this.Tab_TE.GlobalItem = false;
            this.Tab_TE.Image = global::OutputExcelForm.Properties.Resources.wrench_32px;
            this.Tab_TE.Name = "Tab_TE";
            this.Tab_TE.Text = "TE";
            // 
            // superTabControlPanel3
            // 
            this.superTabControlPanel3.Controls.Add(this.SGC_PEPanel);
            this.superTabControlPanel3.Dock = System.Windows.Forms.DockStyle.Fill;
            this.superTabControlPanel3.Location = new System.Drawing.Point(0, 46);
            this.superTabControlPanel3.Name = "superTabControlPanel3";
            this.superTabControlPanel3.Size = new System.Drawing.Size(487, 187);
            this.superTabControlPanel3.TabIndex = 0;
            this.superTabControlPanel3.TabItem = this.Tab_PE;
            // 
            // SGC_PEPanel
            // 
            this.SGC_PEPanel.FilterExprColors.SysFunction = System.Drawing.Color.DarkRed;
            this.SGC_PEPanel.Location = new System.Drawing.Point(3, 3);
            this.SGC_PEPanel.Name = "SGC_PEPanel";
            // 
            // 
            // 
            this.SGC_PEPanel.PrimaryGrid.Columns.Add(this.gridColumn7);
            this.SGC_PEPanel.PrimaryGrid.Columns.Add(this.gridColumn9);
            this.SGC_PEPanel.PrimaryGrid.Columns.Add(this.gridColumn10);
            this.SGC_PEPanel.PrimaryGrid.Columns.Add(this.gridColumn11);
            this.SGC_PEPanel.PrimaryGrid.Columns.Add(this.gridColumn12);
            this.SGC_PEPanel.PrimaryGrid.ShowRowHeaders = false;
            this.SGC_PEPanel.Size = new System.Drawing.Size(481, 181);
            this.SGC_PEPanel.TabIndex = 0;
            this.SGC_PEPanel.Text = "superGridControl1";
            // 
            // gridColumn7
            // 
            this.gridColumn7.CellStyles.Default.Alignment = DevComponents.DotNetBar.SuperGrid.Style.Alignment.MiddleCenter;
            this.gridColumn7.EditorType = typeof(DevComponents.DotNetBar.SuperGrid.GridCheckBoxXEditControl);
            this.gridColumn7.Name = "選擇";
            this.gridColumn7.Width = 40;
            // 
            // gridColumn9
            // 
            this.gridColumn9.EditorType = typeof(DevComponents.DotNetBar.SuperGrid.GridLabelXEditControl);
            this.gridColumn9.Name = "Excel";
            this.gridColumn9.Width = 120;
            // 
            // gridColumn10
            // 
            this.gridColumn10.CellStyles.Default.Alignment = DevComponents.DotNetBar.SuperGrid.Style.Alignment.MiddleCenter;
            this.gridColumn10.EditorType = typeof(DevComponents.DotNetBar.SuperGrid.GridLabelXEditControl);
            this.gridColumn10.Name = "客版";
            this.gridColumn10.Width = 40;
            // 
            // gridColumn11
            // 
            this.gridColumn11.CellStyles.Default.Alignment = DevComponents.DotNetBar.SuperGrid.Style.Alignment.MiddleCenter;
            this.gridColumn11.EditorType = typeof(DevComponents.DotNetBar.SuperGrid.GridLabelXEditControl);
            this.gridColumn11.Name = "製版";
            this.gridColumn11.Width = 40;
            // 
            // gridColumn12
            // 
            this.gridColumn12.AutoSizeMode = DevComponents.DotNetBar.SuperGrid.ColumnAutoSizeMode.Fill;
            this.gridColumn12.EditorType = typeof(DevComponents.DotNetBar.SuperGrid.GridLabelXEditControl);
            this.gridColumn12.Name = "輸出路徑(桌面)";
            // 
            // Tab_PE
            // 
            this.Tab_PE.AttachedControl = this.superTabControlPanel3;
            this.Tab_PE.GlobalItem = false;
            this.Tab_PE.Image = global::OutputExcelForm.Properties.Resources.flow_tree_32px;
            this.Tab_PE.Name = "Tab_PE";
            this.Tab_PE.Text = "PE";
            // 
            // superTabControlPanel1
            // 
            this.superTabControlPanel1.Controls.Add(this.SGC_MEPanel);
            this.superTabControlPanel1.Dock = System.Windows.Forms.DockStyle.Fill;
            this.superTabControlPanel1.Location = new System.Drawing.Point(0, 46);
            this.superTabControlPanel1.Name = "superTabControlPanel1";
            this.superTabControlPanel1.Size = new System.Drawing.Size(487, 187);
            this.superTabControlPanel1.TabIndex = 1;
            this.superTabControlPanel1.TabItem = this.Tab_ME;
            // 
            // SGC_MEPanel
            // 
            this.SGC_MEPanel.FilterExprColors.SysFunction = System.Drawing.Color.DarkRed;
            this.SGC_MEPanel.Location = new System.Drawing.Point(3, 3);
            this.SGC_MEPanel.Name = "SGC_MEPanel";
            // 
            // 
            // 
            this.SGC_MEPanel.PrimaryGrid.ColumnDragBehavior = DevComponents.DotNetBar.SuperGrid.ColumnDragBehavior.None;
            this.SGC_MEPanel.PrimaryGrid.Columns.Add(this.select);
            this.SGC_MEPanel.PrimaryGrid.Columns.Add(this.ExcelType);
            this.SGC_MEPanel.PrimaryGrid.Columns.Add(this.gridColumn6);
            this.SGC_MEPanel.PrimaryGrid.Columns.Add(this.ExcelForm);
            this.SGC_MEPanel.PrimaryGrid.Columns.Add(this.OutputPath);
            this.SGC_MEPanel.PrimaryGrid.ShowRowHeaders = false;
            this.SGC_MEPanel.Size = new System.Drawing.Size(481, 181);
            this.SGC_MEPanel.TabIndex = 0;
            this.SGC_MEPanel.Text = "superGridControl1";
            // 
            // select
            // 
            this.select.CellStyles.Default.Alignment = DevComponents.DotNetBar.SuperGrid.Style.Alignment.MiddleCenter;
            this.select.EditorType = typeof(DevComponents.DotNetBar.SuperGrid.GridCheckBoxXEditControl);
            this.select.Name = "選擇";
            this.select.Width = 40;
            // 
            // ExcelType
            // 
            this.ExcelType.CellStyles.Default.Alignment = DevComponents.DotNetBar.SuperGrid.Style.Alignment.MiddleCenter;
            this.ExcelType.Name = "Excel";
            this.ExcelType.Width = 60;
            // 
            // gridColumn6
            // 
            this.gridColumn6.CellStyles.Default.Alignment = DevComponents.DotNetBar.SuperGrid.Style.Alignment.MiddleCenter;
            this.gridColumn6.EditorType = typeof(DevComponents.DotNetBar.SuperGrid.GridLabelXEditControl);
            this.gridColumn6.Name = "圖版";
            this.gridColumn6.Width = 50;
            // 
            // ExcelForm
            // 
            this.ExcelForm.CellStyles.Default.Alignment = DevComponents.DotNetBar.SuperGrid.Style.Alignment.MiddleCenter;
            this.ExcelForm.EditorType = typeof(DevComponents.DotNetBar.SuperGrid.GridComboBoxExEditControl);
            this.ExcelForm.Name = "廠區";
            this.ExcelForm.Width = 120;
            // 
            // OutputPath
            // 
            this.OutputPath.AutoSizeMode = DevComponents.DotNetBar.SuperGrid.ColumnAutoSizeMode.Fill;
            this.OutputPath.EditorType = typeof(DevComponents.DotNetBar.SuperGrid.GridLabelXEditControl);
            this.OutputPath.Name = "輸出路徑(桌面)";
            // 
            // Tab_ME
            // 
            this.Tab_ME.AttachedControl = this.superTabControlPanel1;
            this.Tab_ME.GlobalItem = false;
            this.Tab_ME.Image = global::OutputExcelForm.Properties.Resources.files_32px;
            this.Tab_ME.Name = "Tab_ME";
            this.Tab_ME.Text = "ME";
            // 
            // pictureBox4
            // 
            this.pictureBox4.Image = global::OutputExcelForm.Properties.Resources.OIS_48px;
            this.pictureBox4.Location = new System.Drawing.Point(313, 51);
            this.pictureBox4.Name = "pictureBox4";
            this.pictureBox4.Size = new System.Drawing.Size(31, 35);
            this.pictureBox4.SizeMode = System.Windows.Forms.PictureBoxSizeMode.Zoom;
            this.pictureBox4.TabIndex = 10;
            this.pictureBox4.TabStop = false;
            // 
            // pictureBox3
            // 
            this.pictureBox3.Image = global::OutputExcelForm.Properties.Resources.folder_48px;
            this.pictureBox3.Location = new System.Drawing.Point(12, 51);
            this.pictureBox3.Name = "pictureBox3";
            this.pictureBox3.Size = new System.Drawing.Size(31, 35);
            this.pictureBox3.SizeMode = System.Windows.Forms.PictureBoxSizeMode.Zoom;
            this.pictureBox3.TabIndex = 6;
            this.pictureBox3.TabStop = false;
            // 
            // pictureBox2
            // 
            this.pictureBox2.Image = global::OutputExcelForm.Properties.Resources.material_48px;
            this.pictureBox2.Location = new System.Drawing.Point(247, 11);
            this.pictureBox2.Name = "pictureBox2";
            this.pictureBox2.Size = new System.Drawing.Size(31, 35);
            this.pictureBox2.SizeMode = System.Windows.Forms.PictureBoxSizeMode.Zoom;
            this.pictureBox2.TabIndex = 3;
            this.pictureBox2.TabStop = false;
            // 
            // pictureBox1
            // 
            this.pictureBox1.Image = global::OutputExcelForm.Properties.Resources.company_48px;
            this.pictureBox1.Location = new System.Drawing.Point(12, 11);
            this.pictureBox1.Name = "pictureBox1";
            this.pictureBox1.Size = new System.Drawing.Size(31, 35);
            this.pictureBox1.SizeMode = System.Windows.Forms.PictureBoxSizeMode.Zoom;
            this.pictureBox1.TabIndex = 1;
            this.pictureBox1.TabStop = false;
            // 
            // labelX4
            // 
            // 
            // 
            // 
            this.labelX4.BackgroundStyle.CornerType = DevComponents.DotNetBar.eCornerType.Square;
            this.labelX4.Font = new System.Drawing.Font("標楷體", 12F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(136)));
            this.labelX4.Location = new System.Drawing.Point(349, 53);
            this.labelX4.Name = "labelX4";
            this.labelX4.Size = new System.Drawing.Size(58, 35);
            this.labelX4.TabIndex = 11;
            this.labelX4.Text = "製程：";
            // 
            // Op1Combobox
            // 
            this.Op1Combobox.DisplayMember = "Text";
            this.Op1Combobox.DrawMode = System.Windows.Forms.DrawMode.OwnerDrawFixed;
            this.Op1Combobox.DropDownStyle = System.Windows.Forms.ComboBoxStyle.DropDownList;
            this.Op1Combobox.FormattingEnabled = true;
            this.Op1Combobox.ItemHeight = 16;
            this.Op1Combobox.Location = new System.Drawing.Point(400, 58);
            this.Op1Combobox.Name = "Op1Combobox";
            this.Op1Combobox.Size = new System.Drawing.Size(99, 22);
            this.Op1Combobox.Style = DevComponents.DotNetBar.eDotNetBarStyle.StyleManagerControlled;
            this.Op1Combobox.TabIndex = 12;
            this.Op1Combobox.SelectedIndexChanged += new System.EventHandler(this.Op1Combobox_SelectedIndexChanged);
            // 
            // OK
            // 
            this.OK.AccessibleRole = System.Windows.Forms.AccessibleRole.PushButton;
            this.OK.ColorTable = DevComponents.DotNetBar.eButtonColor.Flat;
            this.OK.Font = new System.Drawing.Font("標楷體", 11.25F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(136)));
            this.OK.Image = global::OutputExcelForm.Properties.Resources.ok_32px;
            this.OK.Location = new System.Drawing.Point(133, 366);
            this.OK.Name = "OK";
            this.OK.Size = new System.Drawing.Size(109, 41);
            this.OK.Style = DevComponents.DotNetBar.eDotNetBarStyle.StyleManagerControlled;
            this.OK.TabIndex = 13;
            this.OK.Text = "輸出表單";
            this.OK.TextAlignment = DevComponents.DotNetBar.eButtonTextAlignment.Right;
            this.OK.Click += new System.EventHandler(this.OK_Click);
            // 
            // Close
            // 
            this.Close.AccessibleRole = System.Windows.Forms.AccessibleRole.PushButton;
            this.Close.ColorTable = DevComponents.DotNetBar.eButtonColor.Flat;
            this.Close.Font = new System.Drawing.Font("標楷體", 11.25F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(136)));
            this.Close.Image = global::OutputExcelForm.Properties.Resources.cancel_32px;
            this.Close.Location = new System.Drawing.Point(264, 366);
            this.Close.Name = "Close";
            this.Close.Size = new System.Drawing.Size(109, 41);
            this.Close.Style = DevComponents.DotNetBar.eDotNetBarStyle.StyleManagerControlled;
            this.Close.TabIndex = 14;
            this.Close.Text = "關閉視窗";
            this.Close.TextAlignment = DevComponents.DotNetBar.eButtonTextAlignment.Right;
            this.Close.Click += new System.EventHandler(this.Close_Click);
            // 
            // highlighter1
            // 
            this.highlighter1.ContainerControl = this;
            // 
            // labelX5
            // 
            // 
            // 
            // 
            this.labelX5.BackgroundStyle.CornerType = DevComponents.DotNetBar.eCornerType.Square;
            this.labelX5.Font = new System.Drawing.Font("標楷體", 12F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(136)));
            this.labelX5.Location = new System.Drawing.Point(177, 53);
            this.labelX5.Name = "labelX5";
            this.labelX5.Size = new System.Drawing.Size(56, 35);
            this.labelX5.TabIndex = 16;
            this.labelX5.Text = "製版：";
            // 
            // OpVerCombobox
            // 
            this.OpVerCombobox.DisplayMember = "Text";
            this.OpVerCombobox.DrawMode = System.Windows.Forms.DrawMode.OwnerDrawFixed;
            this.OpVerCombobox.DropDownStyle = System.Windows.Forms.ComboBoxStyle.DropDownList;
            this.OpVerCombobox.FormattingEnabled = true;
            this.OpVerCombobox.ItemHeight = 16;
            this.OpVerCombobox.Location = new System.Drawing.Point(230, 58);
            this.OpVerCombobox.Name = "OpVerCombobox";
            this.OpVerCombobox.Size = new System.Drawing.Size(73, 22);
            this.OpVerCombobox.Style = DevComponents.DotNetBar.eDotNetBarStyle.StyleManagerControlled;
            this.OpVerCombobox.TabIndex = 17;
            this.OpVerCombobox.SelectedIndexChanged += new System.EventHandler(this.OpVerCombobox_SelectedIndexChanged);
            // 
            // labelX6
            // 
            // 
            // 
            // 
            this.labelX6.BackgroundStyle.CornerType = DevComponents.DotNetBar.eCornerType.Square;
            this.labelX6.Font = new System.Drawing.Font("標楷體", 12F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(136)));
            this.labelX6.Location = new System.Drawing.Point(12, 94);
            this.labelX6.Name = "labelX6";
            this.labelX6.Size = new System.Drawing.Size(75, 23);
            this.labelX6.TabIndex = 18;
            this.labelX6.Text = "製程序：";
            // 
            // Op2Text
            // 
            // 
            // 
            // 
            this.Op2Text.BackgroundStyle.CornerType = DevComponents.DotNetBar.eCornerType.Square;
            this.Op2Text.Font = new System.Drawing.Font("標楷體", 9.75F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(136)));
            this.Op2Text.Location = new System.Drawing.Point(83, 92);
            this.Op2Text.Name = "Op2Text";
            this.Op2Text.Size = new System.Drawing.Size(417, 23);
            this.Op2Text.TabIndex = 19;
            // 
            // OutputForm
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 12F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.ClientSize = new System.Drawing.Size(512, 420);
            this.Controls.Add(this.Op2Text);
            this.Controls.Add(this.labelX6);
            this.Controls.Add(this.OpVerCombobox);
            this.Controls.Add(this.labelX5);
            this.Controls.Add(this.Close);
            this.Controls.Add(this.OK);
            this.Controls.Add(this.Op1Combobox);
            this.Controls.Add(this.labelX4);
            this.Controls.Add(this.pictureBox4);
            this.Controls.Add(this.SuperTabControl);
            this.Controls.Add(this.CusVerCombobox);
            this.Controls.Add(this.labelX3);
            this.Controls.Add(this.pictureBox3);
            this.Controls.Add(this.PartNoCombobox);
            this.Controls.Add(this.labelX2);
            this.Controls.Add(this.pictureBox2);
            this.Controls.Add(this.CusComboBox);
            this.Controls.Add(this.pictureBox1);
            this.Controls.Add(this.labelX1);
            this.Icon = ((System.Drawing.Icon)(resources.GetObject("$this.Icon")));
            this.Name = "OutputForm";
            this.Text = "輸出表單";
            this.FormClosing += new System.Windows.Forms.FormClosingEventHandler(this.OutputForm_FormClosing);
            ((System.ComponentModel.ISupportInitialize)(this.SuperTabControl)).EndInit();
            this.SuperTabControl.ResumeLayout(false);
            this.superTabControlPanel4.ResumeLayout(false);
            this.superTabControlPanel2.ResumeLayout(false);
            this.superTabControlPanel3.ResumeLayout(false);
            this.superTabControlPanel1.ResumeLayout(false);
            ((System.ComponentModel.ISupportInitialize)(this.pictureBox4)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.pictureBox3)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.pictureBox2)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.pictureBox1)).EndInit();
            this.ResumeLayout(false);

        }

        #endregion

        private DevComponents.DotNetBar.LabelX labelX1;
        private System.Windows.Forms.PictureBox pictureBox1;
        private DevComponents.DotNetBar.Controls.ComboBoxEx CusComboBox;
        private System.Windows.Forms.PictureBox pictureBox2;
        private DevComponents.DotNetBar.LabelX labelX2;
        private System.Windows.Forms.PictureBox pictureBox3;
        private DevComponents.DotNetBar.LabelX labelX3;
        private DevComponents.DotNetBar.SuperTabControl SuperTabControl;
        private DevComponents.DotNetBar.SuperTabControlPanel superTabControlPanel1;
        private DevComponents.DotNetBar.SuperTabItem Tab_ME;
        private System.Windows.Forms.PictureBox pictureBox4;
        private DevComponents.DotNetBar.LabelX labelX4;
        private DevComponents.DotNetBar.ButtonX OK;
        private DevComponents.DotNetBar.ButtonX Close;
        private DevComponents.DotNetBar.SuperGrid.SuperGridControl SGC_MEPanel;
        private DevComponents.DotNetBar.SuperGrid.GridColumn select;
        private DevComponents.DotNetBar.SuperGrid.GridColumn ExcelType;
        private DevComponents.DotNetBar.SuperGrid.GridColumn ExcelForm;
        private DevComponents.DotNetBar.SuperGrid.GridColumn OutputPath;
        private DevComponents.DotNetBar.SuperTabControlPanel superTabControlPanel2;
        private DevComponents.DotNetBar.SuperTabItem Tab_TE;
        private DevComponents.DotNetBar.SuperGrid.SuperGridControl SGC_TEPanel;
        private DevComponents.DotNetBar.SuperGrid.GridColumn gridColumn1;
        private DevComponents.DotNetBar.SuperGrid.GridColumn gridColumn2;
        private DevComponents.DotNetBar.SuperGrid.GridColumn gridColumn3;
        private DevComponents.DotNetBar.SuperGrid.GridColumn gridColumn4;
        private DevComponents.DotNetBar.SuperGrid.GridColumn gridColumn5;
        private DevComponents.DotNetBar.Controls.ComboBoxEx PartNoCombobox;
        private DevComponents.DotNetBar.Controls.ComboBoxEx CusVerCombobox;
        private DevComponents.DotNetBar.Controls.ComboBoxEx Op1Combobox;
        private DevComponents.DotNetBar.Validator.Highlighter highlighter1;
        private DevComponents.DotNetBar.SuperGrid.GridColumn gridColumn6;
        private DevComponents.DotNetBar.Controls.ComboBoxEx OpVerCombobox;
        private DevComponents.DotNetBar.LabelX labelX5;
        private DevComponents.DotNetBar.SuperTabControlPanel superTabControlPanel3;
        private DevComponents.DotNetBar.SuperTabItem Tab_PE;
        private DevComponents.DotNetBar.SuperGrid.SuperGridControl SGC_PEPanel;
        private DevComponents.DotNetBar.SuperGrid.GridColumn gridColumn7;
        private DevComponents.DotNetBar.SuperGrid.GridColumn gridColumn9;
        private DevComponents.DotNetBar.SuperGrid.GridColumn gridColumn10;
        private DevComponents.DotNetBar.SuperGrid.GridColumn gridColumn11;
        private DevComponents.DotNetBar.SuperGrid.GridColumn gridColumn12;
        private DevComponents.DotNetBar.SuperTabControlPanel superTabControlPanel4;
        private DevComponents.DotNetBar.SuperTabItem Tab_FixIns;
        private DevComponents.DotNetBar.SuperGrid.SuperGridControl SGC_FixInsPanel;
        private DevComponents.DotNetBar.SuperGrid.GridColumn gridColumn8;
        private DevComponents.DotNetBar.SuperGrid.GridColumn gridColumn13;
        private DevComponents.DotNetBar.SuperGrid.GridColumn gridColumn14;
        private DevComponents.DotNetBar.SuperGrid.GridColumn gridColumn15;
        private DevComponents.DotNetBar.SuperGrid.GridColumn gridColumn16;
        private DevComponents.DotNetBar.LabelX Op2Text;
        private DevComponents.DotNetBar.LabelX labelX6;
    }
}

