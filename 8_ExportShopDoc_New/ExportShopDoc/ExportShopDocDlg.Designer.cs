namespace ExportShopDoc
{
    partial class ExportShopDocDlg
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
            System.ComponentModel.ComponentResourceManager resources = new System.ComponentModel.ComponentResourceManager(typeof(ExportShopDocDlg));
            this.styleManager1 = new DevComponents.DotNetBar.StyleManager(this.components);
            this.groupBox2 = new System.Windows.Forms.GroupBox();
            this.ProgramStart = new DevComponents.DotNetBar.Controls.TextBoxX();
            this.labelX4 = new DevComponents.DotNetBar.LabelX();
            this.ProgramCount = new DevComponents.DotNetBar.LabelX();
            this.labelX2 = new DevComponents.DotNetBar.LabelX();
            this.ConfirmRename = new DevComponents.DotNetBar.ButtonX();
            this.superGridProg = new DevComponents.DotNetBar.SuperGrid.SuperGridControl();
            this.gridColumn1 = new DevComponents.DotNetBar.SuperGrid.GridColumn();
            this.gridColumn2 = new DevComponents.DotNetBar.SuperGrid.GridColumn();
            this.gridColumn8 = new DevComponents.DotNetBar.SuperGrid.GridColumn();
            this.gridColumn3 = new DevComponents.DotNetBar.SuperGrid.GridColumn();
            this.gridColumn4 = new DevComponents.DotNetBar.SuperGrid.GridColumn();
            this.gridColumn6 = new DevComponents.DotNetBar.SuperGrid.GridColumn();
            this.gridColumn7 = new DevComponents.DotNetBar.SuperGrid.GridColumn();
            this.gridColumn9 = new DevComponents.DotNetBar.SuperGrid.GridColumn();
            this.gridColumn5 = new DevComponents.DotNetBar.SuperGrid.GridColumn();
            this.gridColumn10 = new DevComponents.DotNetBar.SuperGrid.GridColumn();
            this.GroupSaveView = new DevComponents.DotNetBar.ButtonX();
            this.comboBoxNCgroup = new DevComponents.DotNetBar.Controls.ComboBoxEx();
            this.labelX1 = new DevComponents.DotNetBar.LabelX();
            this.groupBox3 = new System.Windows.Forms.GroupBox();
            this.FixturePath = new System.Windows.Forms.TextBox();
            this.SelFixtuePath = new DevComponents.DotNetBar.ButtonX();
            this.groupBox4 = new System.Windows.Forms.GroupBox();
            this.ClearnMachineNo = new DevComponents.DotNetBar.ButtonX();
            this.MachineNo = new DevComponents.DotNetBar.Controls.TextBoxX();
            this.MachineTree = new DevComponents.AdvTree.AdvTree();
            this.nodeConnector1 = new DevComponents.AdvTree.NodeConnector();
            this.elementStyle1 = new DevComponents.DotNetBar.ElementStyle();
            this.groupBox5 = new System.Windows.Forms.GroupBox();
            this.Approved = new DevComponents.DotNetBar.Controls.TextBoxX();
            this.Reviewed = new DevComponents.DotNetBar.Controls.TextBoxX();
            this.Designed = new DevComponents.DotNetBar.Controls.TextBoxX();
            this.labelX5 = new DevComponents.DotNetBar.LabelX();
            this.labelX6 = new DevComponents.DotNetBar.LabelX();
            this.labelX7 = new DevComponents.DotNetBar.LabelX();
            this.ToolControlDimenBtn = new DevComponents.DotNetBar.ButtonX();
            this.SetProgName = new DevComponents.DotNetBar.ButtonX();
            this.SelectMachine = new DevComponents.DotNetBar.ButtonX();
            this.ControlDimen = new DevComponents.DotNetBar.ButtonX();
            this.CloseDlg = new DevComponents.DotNetBar.ButtonX();
            this.ExportExcel = new DevComponents.DotNetBar.ButtonX();
            this.groupBox2.SuspendLayout();
            this.groupBox3.SuspendLayout();
            this.groupBox4.SuspendLayout();
            ((System.ComponentModel.ISupportInitialize)(this.MachineTree)).BeginInit();
            this.groupBox5.SuspendLayout();
            this.SuspendLayout();
            // 
            // styleManager1
            // 
            this.styleManager1.ManagerStyle = DevComponents.DotNetBar.eStyle.Windows7Blue;
            this.styleManager1.MetroColorParameters = new DevComponents.DotNetBar.Metro.ColorTables.MetroColorGeneratorParameters(System.Drawing.Color.White, System.Drawing.Color.FromArgb(((int)(((byte)(43)))), ((int)(((byte)(87)))), ((int)(((byte)(154))))));
            // 
            // groupBox2
            // 
            this.groupBox2.Controls.Add(this.ProgramStart);
            this.groupBox2.Controls.Add(this.labelX4);
            this.groupBox2.Controls.Add(this.ProgramCount);
            this.groupBox2.Controls.Add(this.labelX2);
            this.groupBox2.Controls.Add(this.ConfirmRename);
            this.groupBox2.Controls.Add(this.superGridProg);
            this.groupBox2.Controls.Add(this.GroupSaveView);
            this.groupBox2.Controls.Add(this.comboBoxNCgroup);
            this.groupBox2.Controls.Add(this.labelX1);
            this.groupBox2.Font = new System.Drawing.Font("標楷體", 9.75F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(136)));
            this.groupBox2.Location = new System.Drawing.Point(12, 12);
            this.groupBox2.Name = "groupBox2";
            this.groupBox2.Size = new System.Drawing.Size(260, 427);
            this.groupBox2.TabIndex = 4;
            this.groupBox2.TabStop = false;
            this.groupBox2.Text = "程式更名";
            // 
            // ProgramStart
            // 
            // 
            // 
            // 
            this.ProgramStart.Border.Class = "TextBoxBorder";
            this.ProgramStart.Border.CornerType = DevComponents.DotNetBar.eCornerType.Square;
            this.ProgramStart.Location = new System.Drawing.Point(182, 54);
            this.ProgramStart.Name = "ProgramStart";
            this.ProgramStart.PreventEnterBeep = true;
            this.ProgramStart.Size = new System.Drawing.Size(67, 23);
            this.ProgramStart.TabIndex = 12;
            this.ProgramStart.KeyDown += new System.Windows.Forms.KeyEventHandler(this.ProgramStart_KeyDown);
            // 
            // labelX4
            // 
            // 
            // 
            // 
            this.labelX4.BackgroundStyle.CornerType = DevComponents.DotNetBar.eCornerType.Square;
            this.labelX4.Location = new System.Drawing.Point(111, 54);
            this.labelX4.Name = "labelX4";
            this.labelX4.Size = new System.Drawing.Size(74, 23);
            this.labelX4.TabIndex = 11;
            this.labelX4.Text = "程式起始：";
            // 
            // ProgramCount
            // 
            // 
            // 
            // 
            this.ProgramCount.BackgroundStyle.CornerType = DevComponents.DotNetBar.eCornerType.Square;
            this.ProgramCount.Location = new System.Drawing.Point(77, 52);
            this.ProgramCount.Name = "ProgramCount";
            this.ProgramCount.Size = new System.Drawing.Size(51, 23);
            this.ProgramCount.TabIndex = 10;
            // 
            // labelX2
            // 
            // 
            // 
            // 
            this.labelX2.BackgroundStyle.CornerType = DevComponents.DotNetBar.eCornerType.Square;
            this.labelX2.Location = new System.Drawing.Point(6, 54);
            this.labelX2.Name = "labelX2";
            this.labelX2.Size = new System.Drawing.Size(75, 23);
            this.labelX2.TabIndex = 9;
            this.labelX2.Text = "程式條數：";
            // 
            // ConfirmRename
            // 
            this.ConfirmRename.AccessibleRole = System.Windows.Forms.AccessibleRole.PushButton;
            this.ConfirmRename.ColorTable = DevComponents.DotNetBar.eButtonColor.Flat;
            this.ConfirmRename.Font = new System.Drawing.Font("標楷體", 9.75F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(136)));
            this.ConfirmRename.Image = global::ExportShopDoc.Properties.Resources.Modify_16px;
            this.ConfirmRename.Location = new System.Drawing.Point(182, 398);
            this.ConfirmRename.Name = "ConfirmRename";
            this.ConfirmRename.Size = new System.Drawing.Size(67, 23);
            this.ConfirmRename.Style = DevComponents.DotNetBar.eDotNetBarStyle.StyleManagerControlled;
            this.ConfirmRename.TabIndex = 5;
            this.ConfirmRename.Text = "更名";
            this.ConfirmRename.Click += new System.EventHandler(this.ConfirmRename_Click);
            // 
            // superGridProg
            // 
            this.superGridProg.FilterExprColors.SysFunction = System.Drawing.Color.DarkRed;
            this.superGridProg.Location = new System.Drawing.Point(6, 83);
            this.superGridProg.Name = "superGridProg";
            // 
            // 
            // 
            this.superGridProg.PrimaryGrid.ColumnDragBehavior = DevComponents.DotNetBar.SuperGrid.ColumnDragBehavior.None;
            this.superGridProg.PrimaryGrid.Columns.Add(this.gridColumn1);
            this.superGridProg.PrimaryGrid.Columns.Add(this.gridColumn2);
            this.superGridProg.PrimaryGrid.Columns.Add(this.gridColumn8);
            this.superGridProg.PrimaryGrid.Columns.Add(this.gridColumn3);
            this.superGridProg.PrimaryGrid.Columns.Add(this.gridColumn4);
            this.superGridProg.PrimaryGrid.Columns.Add(this.gridColumn6);
            this.superGridProg.PrimaryGrid.Columns.Add(this.gridColumn7);
            this.superGridProg.PrimaryGrid.Columns.Add(this.gridColumn9);
            this.superGridProg.PrimaryGrid.Columns.Add(this.gridColumn5);
            this.superGridProg.PrimaryGrid.Columns.Add(this.gridColumn10);
            this.superGridProg.PrimaryGrid.SelectionGranularity = DevComponents.DotNetBar.SuperGrid.SelectionGranularity.Row;
            this.superGridProg.PrimaryGrid.ShowRowHeaders = false;
            this.superGridProg.Size = new System.Drawing.Size(243, 309);
            this.superGridProg.TabIndex = 4;
            this.superGridProg.Text = "superGridControl1";
            this.superGridProg.RowClick += new System.EventHandler<DevComponents.DotNetBar.SuperGrid.GridRowClickEventArgs>(this.superGridProg_RowClick);
            // 
            // gridColumn1
            // 
            this.gridColumn1.CellStyles.Default.Alignment = DevComponents.DotNetBar.SuperGrid.Style.Alignment.MiddleCenter;
            this.gridColumn1.ColumnSortMode = DevComponents.DotNetBar.SuperGrid.ColumnSortMode.None;
            this.gridColumn1.EditorType = typeof(DevComponents.DotNetBar.SuperGrid.GridLabelXEditControl);
            this.gridColumn1.HeaderText = "更名前";
            this.gridColumn1.Name = "更名前";
            this.gridColumn1.ResizeMode = DevComponents.DotNetBar.SuperGrid.ColumnResizeMode.None;
            // 
            // gridColumn2
            // 
            this.gridColumn2.CellStyles.Default.Alignment = DevComponents.DotNetBar.SuperGrid.Style.Alignment.MiddleCenter;
            this.gridColumn2.ColumnSortMode = DevComponents.DotNetBar.SuperGrid.ColumnSortMode.None;
            this.gridColumn2.EditorType = typeof(DevComponents.DotNetBar.SuperGrid.GridLabelXEditControl);
            this.gridColumn2.HeaderText = "更名後";
            this.gridColumn2.Name = "更名後";
            this.gridColumn2.Width = 80;
            // 
            // gridColumn8
            // 
            this.gridColumn8.ColumnSortMode = DevComponents.DotNetBar.SuperGrid.ColumnSortMode.None;
            this.gridColumn8.HeaderText = "刀號";
            this.gridColumn8.Name = "刀號";
            this.gridColumn8.Visible = false;
            this.gridColumn8.Width = 40;
            // 
            // gridColumn3
            // 
            this.gridColumn3.HeaderText = "刀具名稱";
            this.gridColumn3.Name = "刀具名稱";
            this.gridColumn3.Visible = false;
            this.gridColumn3.Width = 170;
            // 
            // gridColumn4
            // 
            this.gridColumn4.HeaderText = "刀柄名稱";
            this.gridColumn4.Name = "刀柄名稱";
            this.gridColumn4.Visible = false;
            this.gridColumn4.Width = 110;
            // 
            // gridColumn6
            // 
            this.gridColumn6.HeaderText = "加工長度";
            this.gridColumn6.Name = "加工長度";
            this.gridColumn6.Visible = false;
            this.gridColumn6.Width = 60;
            // 
            // gridColumn7
            // 
            this.gridColumn7.HeaderText = "進給";
            this.gridColumn7.Name = "進給";
            this.gridColumn7.Visible = false;
            this.gridColumn7.Width = 50;
            // 
            // gridColumn9
            // 
            this.gridColumn9.ColumnSortMode = DevComponents.DotNetBar.SuperGrid.ColumnSortMode.None;
            this.gridColumn9.HeaderText = "轉速";
            this.gridColumn9.Name = "轉速";
            this.gridColumn9.Visible = false;
            this.gridColumn9.Width = 50;
            // 
            // gridColumn5
            // 
            this.gridColumn5.HeaderText = "加工時間";
            this.gridColumn5.Name = "加工時間";
            this.gridColumn5.Visible = false;
            this.gridColumn5.Width = 70;
            // 
            // gridColumn10
            // 
            this.gridColumn10.AutoSizeMode = DevComponents.DotNetBar.SuperGrid.ColumnAutoSizeMode.Fill;
            this.gridColumn10.CellStyles.Default.TextColor = System.Drawing.Color.White;
            this.gridColumn10.ColumnSortMode = DevComponents.DotNetBar.SuperGrid.ColumnSortMode.None;
            this.gridColumn10.EditorType = typeof(DevComponents.DotNetBar.SuperGrid.GridButtonXEditControl);
            this.gridColumn10.HeaderText = "拍照";
            this.gridColumn10.InfoImage = global::ExportShopDoc.Properties.Resources.camera_24px;
            this.gridColumn10.Name = "拍照";
            // 
            // GroupSaveView
            // 
            this.GroupSaveView.AccessibleRole = System.Windows.Forms.AccessibleRole.PushButton;
            this.GroupSaveView.ColorTable = DevComponents.DotNetBar.eButtonColor.Flat;
            this.GroupSaveView.Font = new System.Drawing.Font("標楷體", 9.75F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(136)));
            this.GroupSaveView.Image = global::ExportShopDoc.Properties.Resources.camera_24px;
            this.GroupSaveView.Location = new System.Drawing.Point(6, 398);
            this.GroupSaveView.Name = "GroupSaveView";
            this.GroupSaveView.Size = new System.Drawing.Size(92, 23);
            this.GroupSaveView.Style = DevComponents.DotNetBar.eDotNetBarStyle.StyleManagerControlled;
            this.GroupSaveView.TabIndex = 8;
            this.GroupSaveView.Text = "群組拍照";
            this.GroupSaveView.Click += new System.EventHandler(this.GroupSaveView_Click);
            // 
            // comboBoxNCgroup
            // 
            this.comboBoxNCgroup.DisplayMember = "Text";
            this.comboBoxNCgroup.DrawMode = System.Windows.Forms.DrawMode.OwnerDrawFixed;
            this.comboBoxNCgroup.DropDownStyle = System.Windows.Forms.ComboBoxStyle.DropDownList;
            this.comboBoxNCgroup.Font = new System.Drawing.Font("新細明體", 9F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(136)));
            this.comboBoxNCgroup.FormattingEnabled = true;
            this.comboBoxNCgroup.ItemHeight = 16;
            this.comboBoxNCgroup.Location = new System.Drawing.Point(106, 21);
            this.comboBoxNCgroup.Name = "comboBoxNCgroup";
            this.comboBoxNCgroup.Size = new System.Drawing.Size(143, 22);
            this.comboBoxNCgroup.Style = DevComponents.DotNetBar.eDotNetBarStyle.StyleManagerControlled;
            this.comboBoxNCgroup.TabIndex = 1;
            this.comboBoxNCgroup.SelectedIndexChanged += new System.EventHandler(this.comboBoxNCgroup_SelectedIndexChanged);
            // 
            // labelX1
            // 
            // 
            // 
            // 
            this.labelX1.BackgroundStyle.CornerType = DevComponents.DotNetBar.eCornerType.Square;
            this.labelX1.Font = new System.Drawing.Font("標楷體", 9.75F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(136)));
            this.labelX1.Location = new System.Drawing.Point(6, 21);
            this.labelX1.Name = "labelX1";
            this.labelX1.Size = new System.Drawing.Size(102, 23);
            this.labelX1.TabIndex = 0;
            this.labelX1.Text = "程式群組名稱：";
            // 
            // groupBox3
            // 
            this.groupBox3.Controls.Add(this.FixturePath);
            this.groupBox3.Controls.Add(this.SelFixtuePath);
            this.groupBox3.Font = new System.Drawing.Font("標楷體", 9.75F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(136)));
            this.groupBox3.Location = new System.Drawing.Point(12, 445);
            this.groupBox3.Name = "groupBox3";
            this.groupBox3.Size = new System.Drawing.Size(260, 55);
            this.groupBox3.TabIndex = 9;
            this.groupBox3.TabStop = false;
            this.groupBox3.Text = "治具圖片路徑";
            // 
            // FixturePath
            // 
            this.FixturePath.Font = new System.Drawing.Font("新細明體", 9.75F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(136)));
            this.FixturePath.Location = new System.Drawing.Point(6, 21);
            this.FixturePath.Name = "FixturePath";
            this.FixturePath.Size = new System.Drawing.Size(190, 23);
            this.FixturePath.TabIndex = 1;
            // 
            // SelFixtuePath
            // 
            this.SelFixtuePath.AccessibleRole = System.Windows.Forms.AccessibleRole.PushButton;
            this.SelFixtuePath.ColorTable = DevComponents.DotNetBar.eButtonColor.Flat;
            this.SelFixtuePath.Font = new System.Drawing.Font("標楷體", 9.75F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(136)));
            this.SelFixtuePath.Image = global::ExportShopDoc.Properties.Resources.folder_16px;
            this.SelFixtuePath.Location = new System.Drawing.Point(202, 21);
            this.SelFixtuePath.Name = "SelFixtuePath";
            this.SelFixtuePath.Size = new System.Drawing.Size(47, 22);
            this.SelFixtuePath.Style = DevComponents.DotNetBar.eDotNetBarStyle.StyleManagerControlled;
            this.SelFixtuePath.TabIndex = 0;
            this.SelFixtuePath.Click += new System.EventHandler(this.SelFixtuePath_Click);
            // 
            // groupBox4
            // 
            this.groupBox4.Controls.Add(this.ClearnMachineNo);
            this.groupBox4.Controls.Add(this.MachineNo);
            this.groupBox4.Controls.Add(this.MachineTree);
            this.groupBox4.Font = new System.Drawing.Font("標楷體", 9.75F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(136)));
            this.groupBox4.Location = new System.Drawing.Point(295, 12);
            this.groupBox4.Name = "groupBox4";
            this.groupBox4.Size = new System.Drawing.Size(275, 478);
            this.groupBox4.TabIndex = 11;
            this.groupBox4.TabStop = false;
            this.groupBox4.Text = "機台型號";
            // 
            // ClearnMachineNo
            // 
            this.ClearnMachineNo.AccessibleRole = System.Windows.Forms.AccessibleRole.PushButton;
            this.ClearnMachineNo.ColorTable = DevComponents.DotNetBar.eButtonColor.Flat;
            this.ClearnMachineNo.Image = global::ExportShopDoc.Properties.Resources.broom_16px;
            this.ClearnMachineNo.Location = new System.Drawing.Point(180, 449);
            this.ClearnMachineNo.Name = "ClearnMachineNo";
            this.ClearnMachineNo.Size = new System.Drawing.Size(89, 23);
            this.ClearnMachineNo.Style = DevComponents.DotNetBar.eDotNetBarStyle.StyleManagerControlled;
            this.ClearnMachineNo.TabIndex = 10;
            this.ClearnMachineNo.Text = "清空機台";
            this.ClearnMachineNo.Click += new System.EventHandler(this.ClearnMachineNo_Click);
            // 
            // MachineNo
            // 
            // 
            // 
            // 
            this.MachineNo.Border.Class = "TextBoxBorder";
            this.MachineNo.Border.CornerType = DevComponents.DotNetBar.eCornerType.Square;
            this.MachineNo.Location = new System.Drawing.Point(6, 449);
            this.MachineNo.Name = "MachineNo";
            this.MachineNo.PreventEnterBeep = true;
            this.MachineNo.Size = new System.Drawing.Size(168, 23);
            this.MachineNo.TabIndex = 9;
            // 
            // MachineTree
            // 
            this.MachineTree.AccessibleRole = System.Windows.Forms.AccessibleRole.Outline;
            this.MachineTree.AllowDrop = true;
            this.MachineTree.BackColor = System.Drawing.SystemColors.Window;
            // 
            // 
            // 
            this.MachineTree.BackgroundStyle.Class = "TreeBorderKey";
            this.MachineTree.BackgroundStyle.CornerType = DevComponents.DotNetBar.eCornerType.Square;
            this.MachineTree.ColumnsVisible = false;
            this.MachineTree.Font = new System.Drawing.Font("新細明體", 9F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(136)));
            this.MachineTree.GridColumnLines = false;
            this.MachineTree.Location = new System.Drawing.Point(6, 21);
            this.MachineTree.Name = "MachineTree";
            this.MachineTree.NodesConnector = this.nodeConnector1;
            this.MachineTree.NodeStyle = this.elementStyle1;
            this.MachineTree.PathSeparator = ";";
            this.MachineTree.Size = new System.Drawing.Size(263, 422);
            this.MachineTree.Styles.Add(this.elementStyle1);
            this.MachineTree.TabIndex = 0;
            this.MachineTree.Text = "advTree1";
            this.MachineTree.BeforeExpand += new DevComponents.AdvTree.AdvTreeNodeCancelEventHandler(this.MachineTree_BeforeExpand);
            this.MachineTree.NodeClick += new DevComponents.AdvTree.TreeNodeMouseEventHandler(this.MachineTree_NodeClick);
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
            // groupBox5
            // 
            this.groupBox5.Controls.Add(this.Approved);
            this.groupBox5.Controls.Add(this.Reviewed);
            this.groupBox5.Controls.Add(this.Designed);
            this.groupBox5.Controls.Add(this.labelX5);
            this.groupBox5.Controls.Add(this.labelX6);
            this.groupBox5.Controls.Add(this.labelX7);
            this.groupBox5.Font = new System.Drawing.Font("標楷體", 9.75F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(136)));
            this.groupBox5.Location = new System.Drawing.Point(295, 496);
            this.groupBox5.Name = "groupBox5";
            this.groupBox5.Size = new System.Drawing.Size(275, 82);
            this.groupBox5.TabIndex = 10;
            this.groupBox5.TabStop = false;
            this.groupBox5.Text = "人員資訊";
            // 
            // Approved
            // 
            // 
            // 
            // 
            this.Approved.Border.Class = "TextBoxBorder";
            this.Approved.Border.CornerType = DevComponents.DotNetBar.eCornerType.Square;
            this.Approved.Font = new System.Drawing.Font("新細明體", 9F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(136)));
            this.Approved.Location = new System.Drawing.Point(196, 51);
            this.Approved.Name = "Approved";
            this.Approved.PreventEnterBeep = true;
            this.Approved.Size = new System.Drawing.Size(73, 22);
            this.Approved.TabIndex = 11;
            // 
            // Reviewed
            // 
            // 
            // 
            // 
            this.Reviewed.Border.Class = "TextBoxBorder";
            this.Reviewed.Border.CornerType = DevComponents.DotNetBar.eCornerType.Square;
            this.Reviewed.Font = new System.Drawing.Font("新細明體", 9F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(136)));
            this.Reviewed.Location = new System.Drawing.Point(101, 51);
            this.Reviewed.Name = "Reviewed";
            this.Reviewed.PreventEnterBeep = true;
            this.Reviewed.Size = new System.Drawing.Size(73, 22);
            this.Reviewed.TabIndex = 13;
            // 
            // Designed
            // 
            // 
            // 
            // 
            this.Designed.Border.Class = "TextBoxBorder";
            this.Designed.Border.CornerType = DevComponents.DotNetBar.eCornerType.Square;
            this.Designed.Font = new System.Drawing.Font("新細明體", 9F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(136)));
            this.Designed.Location = new System.Drawing.Point(6, 51);
            this.Designed.Name = "Designed";
            this.Designed.PreventEnterBeep = true;
            this.Designed.Size = new System.Drawing.Size(73, 22);
            this.Designed.TabIndex = 11;
            // 
            // labelX5
            // 
            // 
            // 
            // 
            this.labelX5.BackgroundStyle.CornerType = DevComponents.DotNetBar.eCornerType.Square;
            this.labelX5.Font = new System.Drawing.Font("標楷體", 9.75F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(136)));
            this.labelX5.Location = new System.Drawing.Point(196, 22);
            this.labelX5.Name = "labelX5";
            this.labelX5.Size = new System.Drawing.Size(52, 23);
            this.labelX5.TabIndex = 12;
            this.labelX5.Text = "批准";
            // 
            // labelX6
            // 
            // 
            // 
            // 
            this.labelX6.BackgroundStyle.CornerType = DevComponents.DotNetBar.eCornerType.Square;
            this.labelX6.Font = new System.Drawing.Font("標楷體", 9.75F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(136)));
            this.labelX6.Location = new System.Drawing.Point(6, 22);
            this.labelX6.Name = "labelX6";
            this.labelX6.Size = new System.Drawing.Size(52, 23);
            this.labelX6.TabIndex = 0;
            this.labelX6.Text = "設計";
            // 
            // labelX7
            // 
            // 
            // 
            // 
            this.labelX7.BackgroundStyle.CornerType = DevComponents.DotNetBar.eCornerType.Square;
            this.labelX7.Font = new System.Drawing.Font("標楷體", 9.75F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(136)));
            this.labelX7.Location = new System.Drawing.Point(101, 22);
            this.labelX7.Name = "labelX7";
            this.labelX7.Size = new System.Drawing.Size(52, 23);
            this.labelX7.TabIndex = 11;
            this.labelX7.Text = "審核";
            // 
            // ToolControlDimenBtn
            // 
            this.ToolControlDimenBtn.AccessibleRole = System.Windows.Forms.AccessibleRole.PushButton;
            this.ToolControlDimenBtn.ColorTable = DevComponents.DotNetBar.eButtonColor.Flat;
            this.ToolControlDimenBtn.Font = new System.Drawing.Font("標楷體", 9F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(136)));
            this.ToolControlDimenBtn.Image = global::ExportShopDoc.Properties.Resources.wrench_24px;
            this.ToolControlDimenBtn.Location = new System.Drawing.Point(166, 506);
            this.ToolControlDimenBtn.Name = "ToolControlDimenBtn";
            this.ToolControlDimenBtn.Size = new System.Drawing.Size(106, 33);
            this.ToolControlDimenBtn.Style = DevComponents.DotNetBar.eDotNetBarStyle.StyleManagerControlled;
            this.ToolControlDimenBtn.TabIndex = 14;
            this.ToolControlDimenBtn.Text = "刀號<->尺寸";
            this.ToolControlDimenBtn.Click += new System.EventHandler(this.ToolControlDimenBtn_Click);
            // 
            // SetProgName
            // 
            this.SetProgName.AccessibleRole = System.Windows.Forms.AccessibleRole.PushButton;
            this.SetProgName.ColorTable = DevComponents.DotNetBar.eButtonColor.Flat;
            this.SetProgName.Image = global::ExportShopDoc.Properties.Resources.confirm_16px1;
            this.SetProgName.Location = new System.Drawing.Point(659, 33);
            this.SetProgName.Name = "SetProgName";
            this.SetProgName.Size = new System.Drawing.Size(28, 23);
            this.SetProgName.Style = DevComponents.DotNetBar.eDotNetBarStyle.StyleManagerControlled;
            this.SetProgName.TabIndex = 13;
            this.SetProgName.Click += new System.EventHandler(this.SetProgName_Click);
            // 
            // SelectMachine
            // 
            this.SelectMachine.AccessibleRole = System.Windows.Forms.AccessibleRole.PushButton;
            this.SelectMachine.ColorTable = DevComponents.DotNetBar.eButtonColor.Flat;
            this.SelectMachine.Font = new System.Drawing.Font("標楷體", 9F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(136)));
            this.SelectMachine.Image = global::ExportShopDoc.Properties.Resources.machine_16px1;
            this.SelectMachine.Location = new System.Drawing.Point(12, 506);
            this.SelectMachine.Name = "SelectMachine";
            this.SelectMachine.Size = new System.Drawing.Size(55, 33);
            this.SelectMachine.Style = DevComponents.DotNetBar.eDotNetBarStyle.StyleManagerControlled;
            this.SelectMachine.TabIndex = 13;
            this.SelectMachine.Text = "機台";
            this.SelectMachine.Click += new System.EventHandler(this.SelectMachine_Click);
            // 
            // ControlDimen
            // 
            this.ControlDimen.AccessibleRole = System.Windows.Forms.AccessibleRole.PushButton;
            this.ControlDimen.ColorTable = DevComponents.DotNetBar.eButtonColor.Flat;
            this.ControlDimen.Font = new System.Drawing.Font("標楷體", 9F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(136)));
            this.ControlDimen.Image = global::ExportShopDoc.Properties.Resources.Rulers_24px;
            this.ControlDimen.Location = new System.Drawing.Point(73, 506);
            this.ControlDimen.Name = "ControlDimen";
            this.ControlDimen.Size = new System.Drawing.Size(87, 33);
            this.ControlDimen.Style = DevComponents.DotNetBar.eDotNetBarStyle.StyleManagerControlled;
            this.ControlDimen.TabIndex = 12;
            this.ControlDimen.Text = "管制尺寸";
            this.ControlDimen.Click += new System.EventHandler(this.ControlDimen_Click);
            // 
            // CloseDlg
            // 
            this.CloseDlg.AccessibleRole = System.Windows.Forms.AccessibleRole.PushButton;
            this.CloseDlg.ColorTable = DevComponents.DotNetBar.eButtonColor.Flat;
            this.CloseDlg.Font = new System.Drawing.Font("標楷體", 9F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(136)));
            this.CloseDlg.Image = global::ExportShopDoc.Properties.Resources.close_16px;
            this.CloseDlg.Location = new System.Drawing.Point(222, 545);
            this.CloseDlg.Name = "CloseDlg";
            this.CloseDlg.Size = new System.Drawing.Size(50, 33);
            this.CloseDlg.Style = DevComponents.DotNetBar.eDotNetBarStyle.StyleManagerControlled;
            this.CloseDlg.TabIndex = 7;
            this.CloseDlg.Text = "關閉";
            this.CloseDlg.Click += new System.EventHandler(this.CloseDlg_Click);
            // 
            // ExportExcel
            // 
            this.ExportExcel.AccessibleRole = System.Windows.Forms.AccessibleRole.PushButton;
            this.ExportExcel.ColorTable = DevComponents.DotNetBar.eButtonColor.Flat;
            this.ExportExcel.Font = new System.Drawing.Font("標楷體", 9F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(136)));
            this.ExportExcel.Image = global::ExportShopDoc.Properties.Resources.confirm_16px;
            this.ExportExcel.Location = new System.Drawing.Point(166, 545);
            this.ExportExcel.Name = "ExportExcel";
            this.ExportExcel.Size = new System.Drawing.Size(50, 33);
            this.ExportExcel.Style = DevComponents.DotNetBar.eDotNetBarStyle.StyleManagerControlled;
            this.ExportExcel.TabIndex = 6;
            this.ExportExcel.Text = "確認";
            this.ExportExcel.Click += new System.EventHandler(this.ExportExcel_Click);
            // 
            // ExportShopDocDlg
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 12F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.AutoSizeMode = System.Windows.Forms.AutoSizeMode.GrowAndShrink;
            this.ClientSize = new System.Drawing.Size(589, 591);
            this.Controls.Add(this.ToolControlDimenBtn);
            this.Controls.Add(this.SetProgName);
            this.Controls.Add(this.SelectMachine);
            this.Controls.Add(this.ControlDimen);
            this.Controls.Add(this.groupBox4);
            this.Controls.Add(this.groupBox5);
            this.Controls.Add(this.groupBox3);
            this.Controls.Add(this.CloseDlg);
            this.Controls.Add(this.ExportExcel);
            this.Controls.Add(this.groupBox2);
            this.DoubleBuffered = true;
            this.Icon = ((System.Drawing.Icon)(resources.GetObject("$this.Icon")));
            this.MaximizeBox = false;
            this.MinimizeBox = false;
            this.Name = "ExportShopDocDlg";
            this.Text = " ";
            this.Load += new System.EventHandler(this.ExportShopDocDlg_Load);
            this.groupBox2.ResumeLayout(false);
            this.groupBox3.ResumeLayout(false);
            this.groupBox3.PerformLayout();
            this.groupBox4.ResumeLayout(false);
            ((System.ComponentModel.ISupportInitialize)(this.MachineTree)).EndInit();
            this.groupBox5.ResumeLayout(false);
            this.ResumeLayout(false);

        }

        #endregion

        private DevComponents.DotNetBar.StyleManager styleManager1;
        private System.Windows.Forms.GroupBox groupBox2;
        private DevComponents.DotNetBar.SuperGrid.SuperGridControl superGridProg;
        private DevComponents.DotNetBar.SuperGrid.GridColumn gridColumn1;
        private DevComponents.DotNetBar.SuperGrid.GridColumn gridColumn2;
        private DevComponents.DotNetBar.Controls.ComboBoxEx comboBoxNCgroup;
        private DevComponents.DotNetBar.LabelX labelX1;
        private DevComponents.DotNetBar.ButtonX ConfirmRename;
        private DevComponents.DotNetBar.SuperGrid.GridColumn gridColumn3;
        private DevComponents.DotNetBar.SuperGrid.GridColumn gridColumn4;
        private DevComponents.DotNetBar.SuperGrid.GridColumn gridColumn5;
        private DevComponents.DotNetBar.ButtonX ExportExcel;
        private DevComponents.DotNetBar.SuperGrid.GridColumn gridColumn6;
        private DevComponents.DotNetBar.SuperGrid.GridColumn gridColumn7;
        private DevComponents.DotNetBar.SuperGrid.GridColumn gridColumn8;
        private DevComponents.DotNetBar.SuperGrid.GridColumn gridColumn9;
        private DevComponents.DotNetBar.ButtonX CloseDlg;
        private DevComponents.DotNetBar.SuperGrid.GridColumn gridColumn10;
        private DevComponents.DotNetBar.ButtonX GroupSaveView;
        private System.Windows.Forms.GroupBox groupBox3;
        private System.Windows.Forms.TextBox FixturePath;
        private DevComponents.DotNetBar.ButtonX SelFixtuePath;
        private System.Windows.Forms.GroupBox groupBox4;
        private DevComponents.AdvTree.AdvTree MachineTree;
        private DevComponents.AdvTree.NodeConnector nodeConnector1;
        private DevComponents.DotNetBar.ElementStyle elementStyle1;
        private System.Windows.Forms.GroupBox groupBox5;
        private DevComponents.DotNetBar.Controls.TextBoxX Approved;
        private DevComponents.DotNetBar.Controls.TextBoxX Reviewed;
        private DevComponents.DotNetBar.Controls.TextBoxX Designed;
        private DevComponents.DotNetBar.LabelX labelX5;
        private DevComponents.DotNetBar.LabelX labelX6;
        private DevComponents.DotNetBar.LabelX labelX7;
        private DevComponents.DotNetBar.Controls.TextBoxX MachineNo;
        private DevComponents.DotNetBar.ButtonX ClearnMachineNo;
        private DevComponents.DotNetBar.ButtonX ControlDimen;
        private DevComponents.DotNetBar.LabelX labelX2;
        private DevComponents.DotNetBar.Controls.TextBoxX ProgramStart;
        private DevComponents.DotNetBar.LabelX labelX4;
        private DevComponents.DotNetBar.LabelX ProgramCount;
        private DevComponents.DotNetBar.ButtonX SetProgName;
        private DevComponents.DotNetBar.ButtonX SelectMachine;
        private DevComponents.DotNetBar.ButtonX ToolControlDimenBtn;
    }
}