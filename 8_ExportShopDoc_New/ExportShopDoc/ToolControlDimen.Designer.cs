namespace ExportShopDoc
{
    partial class ToolControlDimen
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
            this.DraftingRev = new DevComponents.DotNetBar.Controls.ComboBoxEx();
            this.OpRev = new DevComponents.DotNetBar.LabelX();
            this.CusRev = new DevComponents.DotNetBar.LabelX();
            this.CusName = new DevComponents.DotNetBar.LabelX();
            this.labelX5 = new DevComponents.DotNetBar.LabelX();
            this.OIS = new DevComponents.DotNetBar.LabelX();
            this.labelX6 = new DevComponents.DotNetBar.LabelX();
            this.PartNo = new DevComponents.DotNetBar.LabelX();
            this.labelX1 = new DevComponents.DotNetBar.LabelX();
            this.labelX2 = new DevComponents.DotNetBar.LabelX();
            this.labelX4 = new DevComponents.DotNetBar.LabelX();
            this.labelX3 = new DevComponents.DotNetBar.LabelX();
            this.OISTree = new DevComponents.AdvTree.AdvTree();
            this.columnHeader1 = new DevComponents.AdvTree.ColumnHeader();
            this.columnHeader2 = new DevComponents.AdvTree.ColumnHeader();
            this.columnHeader3 = new DevComponents.AdvTree.ColumnHeader();
            this.nodeConnector1 = new DevComponents.AdvTree.NodeConnector();
            this.elementStyle1 = new DevComponents.DotNetBar.ElementStyle();
            this.labelX7 = new DevComponents.DotNetBar.LabelX();
            this.ToolNoCombo = new DevComponents.DotNetBar.Controls.ComboBoxEx();
            this.LookPath = new DevComponents.DotNetBar.ButtonX();
            this.buttonX1 = new DevComponents.DotNetBar.ButtonX();
            this.SGC_ToolPath = new DevComponents.DotNetBar.SuperGrid.SuperGridControl();
            this.gridColumn1 = new DevComponents.DotNetBar.SuperGrid.GridColumn();
            this.gridColumn2 = new DevComponents.DotNetBar.SuperGrid.GridColumn();
            this.groupBox1.SuspendLayout();
            ((System.ComponentModel.ISupportInitialize)(this.OISTree)).BeginInit();
            this.SuspendLayout();
            // 
            // styleManager1
            // 
            this.styleManager1.ManagerStyle = DevComponents.DotNetBar.eStyle.Windows7Blue;
            this.styleManager1.MetroColorParameters = new DevComponents.DotNetBar.Metro.ColorTables.MetroColorGeneratorParameters(System.Drawing.Color.White, System.Drawing.Color.FromArgb(((int)(((byte)(43)))), ((int)(((byte)(87)))), ((int)(((byte)(154))))));
            // 
            // groupBox1
            // 
            this.groupBox1.Controls.Add(this.DraftingRev);
            this.groupBox1.Controls.Add(this.OpRev);
            this.groupBox1.Controls.Add(this.CusRev);
            this.groupBox1.Controls.Add(this.CusName);
            this.groupBox1.Controls.Add(this.labelX5);
            this.groupBox1.Controls.Add(this.OIS);
            this.groupBox1.Controls.Add(this.labelX6);
            this.groupBox1.Controls.Add(this.PartNo);
            this.groupBox1.Controls.Add(this.labelX1);
            this.groupBox1.Controls.Add(this.labelX2);
            this.groupBox1.Controls.Add(this.labelX4);
            this.groupBox1.Controls.Add(this.labelX3);
            this.groupBox1.Font = new System.Drawing.Font("標楷體", 12F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(136)));
            this.groupBox1.Location = new System.Drawing.Point(12, 12);
            this.groupBox1.Name = "groupBox1";
            this.groupBox1.Size = new System.Drawing.Size(324, 115);
            this.groupBox1.TabIndex = 0;
            this.groupBox1.TabStop = false;
            this.groupBox1.Text = "基礎資訊";
            // 
            // DraftingRev
            // 
            this.DraftingRev.DisplayMember = "Text";
            this.DraftingRev.DrawMode = System.Windows.Forms.DrawMode.OwnerDrawFixed;
            this.DraftingRev.DropDownStyle = System.Windows.Forms.ComboBoxStyle.DropDownList;
            this.DraftingRev.Font = new System.Drawing.Font("新細明體", 9F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(136)));
            this.DraftingRev.FormattingEnabled = true;
            this.DraftingRev.ItemHeight = 16;
            this.DraftingRev.Location = new System.Drawing.Point(266, 84);
            this.DraftingRev.Name = "DraftingRev";
            this.DraftingRev.Size = new System.Drawing.Size(49, 22);
            this.DraftingRev.Style = DevComponents.DotNetBar.eDotNetBarStyle.StyleManagerControlled;
            this.DraftingRev.TabIndex = 1;
            this.DraftingRev.SelectedIndexChanged += new System.EventHandler(this.DraftingRev_SelectedIndexChanged);
            // 
            // OpRev
            // 
            // 
            // 
            // 
            this.OpRev.BackgroundStyle.CornerType = DevComponents.DotNetBar.eCornerType.Square;
            this.OpRev.Font = new System.Drawing.Font("新細明體", 9F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(136)));
            this.OpRev.Location = new System.Drawing.Point(266, 54);
            this.OpRev.Name = "OpRev";
            this.OpRev.Size = new System.Drawing.Size(40, 23);
            this.OpRev.TabIndex = 4;
            this.OpRev.Text = "labelX10";
            // 
            // CusRev
            // 
            // 
            // 
            // 
            this.CusRev.BackgroundStyle.CornerType = DevComponents.DotNetBar.eCornerType.Square;
            this.CusRev.Font = new System.Drawing.Font("新細明體", 9F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(136)));
            this.CusRev.Location = new System.Drawing.Point(266, 26);
            this.CusRev.Name = "CusRev";
            this.CusRev.Size = new System.Drawing.Size(40, 23);
            this.CusRev.TabIndex = 5;
            this.CusRev.Text = "labelX11";
            // 
            // CusName
            // 
            // 
            // 
            // 
            this.CusName.BackgroundStyle.CornerType = DevComponents.DotNetBar.eCornerType.Square;
            this.CusName.Font = new System.Drawing.Font("新細明體", 9F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(136)));
            this.CusName.Location = new System.Drawing.Point(76, 27);
            this.CusName.Name = "CusName";
            this.CusName.Size = new System.Drawing.Size(126, 23);
            this.CusName.TabIndex = 1;
            this.CusName.Text = "labelX7";
            // 
            // labelX5
            // 
            // 
            // 
            // 
            this.labelX5.BackgroundStyle.CornerType = DevComponents.DotNetBar.eCornerType.Square;
            this.labelX5.Location = new System.Drawing.Point(208, 84);
            this.labelX5.Name = "labelX5";
            this.labelX5.Size = new System.Drawing.Size(71, 23);
            this.labelX5.TabIndex = 4;
            this.labelX5.Text = "圖版：";
            // 
            // OIS
            // 
            // 
            // 
            // 
            this.OIS.BackgroundStyle.CornerType = DevComponents.DotNetBar.eCornerType.Square;
            this.OIS.Font = new System.Drawing.Font("新細明體", 9F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(136)));
            this.OIS.Location = new System.Drawing.Point(76, 84);
            this.OIS.Name = "OIS";
            this.OIS.Size = new System.Drawing.Size(126, 23);
            this.OIS.TabIndex = 3;
            this.OIS.Text = "labelX9";
            // 
            // labelX6
            // 
            // 
            // 
            // 
            this.labelX6.BackgroundStyle.CornerType = DevComponents.DotNetBar.eCornerType.Square;
            this.labelX6.Location = new System.Drawing.Point(208, 56);
            this.labelX6.Name = "labelX6";
            this.labelX6.Size = new System.Drawing.Size(71, 23);
            this.labelX6.TabIndex = 5;
            this.labelX6.Text = "製版：";
            // 
            // PartNo
            // 
            // 
            // 
            // 
            this.PartNo.BackgroundStyle.CornerType = DevComponents.DotNetBar.eCornerType.Square;
            this.PartNo.Font = new System.Drawing.Font("新細明體", 9F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(136)));
            this.PartNo.Location = new System.Drawing.Point(76, 56);
            this.PartNo.Name = "PartNo";
            this.PartNo.Size = new System.Drawing.Size(126, 23);
            this.PartNo.TabIndex = 2;
            this.PartNo.Text = "labelX8";
            // 
            // labelX1
            // 
            // 
            // 
            // 
            this.labelX1.BackgroundStyle.CornerType = DevComponents.DotNetBar.eCornerType.Square;
            this.labelX1.Location = new System.Drawing.Point(19, 27);
            this.labelX1.Name = "labelX1";
            this.labelX1.Size = new System.Drawing.Size(71, 23);
            this.labelX1.TabIndex = 0;
            this.labelX1.Text = "客戶：";
            // 
            // labelX2
            // 
            // 
            // 
            // 
            this.labelX2.BackgroundStyle.CornerType = DevComponents.DotNetBar.eCornerType.Square;
            this.labelX2.Location = new System.Drawing.Point(19, 56);
            this.labelX2.Name = "labelX2";
            this.labelX2.Size = new System.Drawing.Size(71, 23);
            this.labelX2.TabIndex = 1;
            this.labelX2.Text = "料號：";
            // 
            // labelX4
            // 
            // 
            // 
            // 
            this.labelX4.BackgroundStyle.CornerType = DevComponents.DotNetBar.eCornerType.Square;
            this.labelX4.Location = new System.Drawing.Point(208, 27);
            this.labelX4.Name = "labelX4";
            this.labelX4.Size = new System.Drawing.Size(71, 23);
            this.labelX4.TabIndex = 3;
            this.labelX4.Text = "客版：";
            // 
            // labelX3
            // 
            // 
            // 
            // 
            this.labelX3.BackgroundStyle.CornerType = DevComponents.DotNetBar.eCornerType.Square;
            this.labelX3.Location = new System.Drawing.Point(19, 85);
            this.labelX3.Name = "labelX3";
            this.labelX3.Size = new System.Drawing.Size(71, 23);
            this.labelX3.TabIndex = 2;
            this.labelX3.Text = "步序：";
            // 
            // OISTree
            // 
            this.OISTree.AccessibleRole = System.Windows.Forms.AccessibleRole.Outline;
            this.OISTree.AllowDrop = true;
            this.OISTree.BackColor = System.Drawing.SystemColors.Window;
            // 
            // 
            // 
            this.OISTree.BackgroundStyle.Class = "TreeBorderKey";
            this.OISTree.BackgroundStyle.CornerType = DevComponents.DotNetBar.eCornerType.Square;
            this.OISTree.Columns.Add(this.columnHeader1);
            this.OISTree.Columns.Add(this.columnHeader2);
            this.OISTree.Columns.Add(this.columnHeader3);
            this.OISTree.GridColumnLines = false;
            this.OISTree.GridRowLines = true;
            this.OISTree.Location = new System.Drawing.Point(12, 133);
            this.OISTree.MultiSelect = true;
            this.OISTree.Name = "OISTree";
            this.OISTree.NodesConnector = this.nodeConnector1;
            this.OISTree.NodeStyle = this.elementStyle1;
            this.OISTree.PathSeparator = ";";
            this.OISTree.SelectionBoxStyle = DevComponents.AdvTree.eSelectionStyle.FullRowSelect;
            this.OISTree.Size = new System.Drawing.Size(324, 270);
            this.OISTree.Styles.Add(this.elementStyle1);
            this.OISTree.TabIndex = 1;
            this.OISTree.Text = "advTree1";
            this.OISTree.NodeMouseUp += new DevComponents.AdvTree.TreeNodeMouseEventHandler(this.OISTree_NodeMouseUp);
            // 
            // columnHeader1
            // 
            this.columnHeader1.Name = "columnHeader1";
            this.columnHeader1.Text = "泡泡球";
            this.columnHeader1.Width.Absolute = 50;
            // 
            // columnHeader2
            // 
            this.columnHeader2.Name = "columnHeader2";
            this.columnHeader2.Text = "尺寸資訊";
            this.columnHeader2.Width.Absolute = 200;
            // 
            // columnHeader3
            // 
            this.columnHeader3.Name = "columnHeader3";
            this.columnHeader3.Text = "刀號";
            this.columnHeader3.Width.Absolute = 44;
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
            // labelX7
            // 
            // 
            // 
            // 
            this.labelX7.BackgroundStyle.CornerType = DevComponents.DotNetBar.eCornerType.Square;
            this.labelX7.Font = new System.Drawing.Font("標楷體", 9F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(136)));
            this.labelX7.Location = new System.Drawing.Point(12, 418);
            this.labelX7.Name = "labelX7";
            this.labelX7.Size = new System.Drawing.Size(75, 23);
            this.labelX7.TabIndex = 3;
            this.labelX7.Text = "刀號選擇：";
            // 
            // ToolNoCombo
            // 
            this.ToolNoCombo.DisplayMember = "Text";
            this.ToolNoCombo.DrawMode = System.Windows.Forms.DrawMode.OwnerDrawFixed;
            this.ToolNoCombo.DropDownStyle = System.Windows.Forms.ComboBoxStyle.DropDownList;
            this.ToolNoCombo.FormattingEnabled = true;
            this.ToolNoCombo.ItemHeight = 16;
            this.ToolNoCombo.Location = new System.Drawing.Point(76, 418);
            this.ToolNoCombo.Name = "ToolNoCombo";
            this.ToolNoCombo.Size = new System.Drawing.Size(51, 22);
            this.ToolNoCombo.Style = DevComponents.DotNetBar.eDotNetBarStyle.StyleManagerControlled;
            this.ToolNoCombo.TabIndex = 4;
            this.ToolNoCombo.SelectedIndexChanged += new System.EventHandler(this.ToolNoCombo_SelectedIndexChanged);
            // 
            // LookPath
            // 
            this.LookPath.AccessibleRole = System.Windows.Forms.AccessibleRole.PushButton;
            this.LookPath.ColorTable = DevComponents.DotNetBar.eButtonColor.Flat;
            this.LookPath.Font = new System.Drawing.Font("標楷體", 9F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(136)));
            this.LookPath.Image = global::ExportShopDoc.Properties.Resources.eye_16px;
            this.LookPath.Location = new System.Drawing.Point(146, 418);
            this.LookPath.Name = "LookPath";
            this.LookPath.Size = new System.Drawing.Size(92, 23);
            this.LookPath.Style = DevComponents.DotNetBar.eDotNetBarStyle.StyleManagerControlled;
            this.LookPath.TabIndex = 5;
            this.LookPath.Text = "程式路徑";
            this.LookPath.Click += new System.EventHandler(this.LookPath_Click);
            // 
            // buttonX1
            // 
            this.buttonX1.AccessibleRole = System.Windows.Forms.AccessibleRole.PushButton;
            this.buttonX1.ColorTable = DevComponents.DotNetBar.eButtonColor.Flat;
            this.buttonX1.Font = new System.Drawing.Font("標楷體", 9F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(136)));
            this.buttonX1.Image = global::ExportShopDoc.Properties.Resources.confirm_16px;
            this.buttonX1.Location = new System.Drawing.Point(244, 418);
            this.buttonX1.Name = "buttonX1";
            this.buttonX1.Size = new System.Drawing.Size(92, 23);
            this.buttonX1.Style = DevComponents.DotNetBar.eDotNetBarStyle.StyleManagerControlled;
            this.buttonX1.TabIndex = 2;
            this.buttonX1.Text = "確認/關閉";
            this.buttonX1.Click += new System.EventHandler(this.buttonX1_Click);
            // 
            // SGC_ToolPath
            // 
            this.SGC_ToolPath.FilterExprColors.SysFunction = System.Drawing.Color.DarkRed;
            this.SGC_ToolPath.Location = new System.Drawing.Point(360, 22);
            this.SGC_ToolPath.Name = "SGC_ToolPath";
            // 
            // 
            // 
            this.SGC_ToolPath.PrimaryGrid.ColumnDragBehavior = DevComponents.DotNetBar.SuperGrid.ColumnDragBehavior.None;
            this.SGC_ToolPath.PrimaryGrid.Columns.Add(this.gridColumn1);
            this.SGC_ToolPath.PrimaryGrid.Columns.Add(this.gridColumn2);
            this.SGC_ToolPath.PrimaryGrid.SelectionGranularity = DevComponents.DotNetBar.SuperGrid.SelectionGranularity.Row;
            this.SGC_ToolPath.PrimaryGrid.ShowRowHeaders = false;
            this.SGC_ToolPath.Size = new System.Drawing.Size(165, 381);
            this.SGC_ToolPath.TabIndex = 6;
            this.SGC_ToolPath.Text = "superGridControl1";
            this.SGC_ToolPath.RowClick += new System.EventHandler<DevComponents.DotNetBar.SuperGrid.GridRowClickEventArgs>(this.SGC_ToolPath_RowClick);
            // 
            // gridColumn1
            // 
            this.gridColumn1.CellStyles.Default.Alignment = DevComponents.DotNetBar.SuperGrid.Style.Alignment.MiddleCenter;
            this.gridColumn1.ColumnSortMode = DevComponents.DotNetBar.SuperGrid.ColumnSortMode.None;
            this.gridColumn1.EditorType = typeof(DevComponents.DotNetBar.SuperGrid.GridLabelXEditControl);
            this.gridColumn1.HeaderText = "刀號";
            this.gridColumn1.Name = "gridColumn1";
            this.gridColumn1.ResizeMode = DevComponents.DotNetBar.SuperGrid.ColumnResizeMode.None;
            this.gridColumn1.Width = 50;
            // 
            // gridColumn2
            // 
            this.gridColumn2.AutoSizeMode = DevComponents.DotNetBar.SuperGrid.ColumnAutoSizeMode.Fill;
            this.gridColumn2.CellStyles.Default.Alignment = DevComponents.DotNetBar.SuperGrid.Style.Alignment.MiddleCenter;
            this.gridColumn2.ColumnSortMode = DevComponents.DotNetBar.SuperGrid.ColumnSortMode.None;
            this.gridColumn2.EditorType = typeof(DevComponents.DotNetBar.SuperGrid.GridLabelXEditControl);
            this.gridColumn2.HeaderText = "程式";
            this.gridColumn2.Name = "gridColumn2";
            this.gridColumn2.ResizeMode = DevComponents.DotNetBar.SuperGrid.ColumnResizeMode.None;
            // 
            // ToolControlDimen
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 12F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.AutoSizeMode = System.Windows.Forms.AutoSizeMode.GrowAndShrink;
            this.ClientSize = new System.Drawing.Size(539, 453);
            this.Controls.Add(this.SGC_ToolPath);
            this.Controls.Add(this.LookPath);
            this.Controls.Add(this.ToolNoCombo);
            this.Controls.Add(this.labelX7);
            this.Controls.Add(this.buttonX1);
            this.Controls.Add(this.OISTree);
            this.Controls.Add(this.groupBox1);
            this.DoubleBuffered = true;
            this.Name = "ToolControlDimen";
            this.Text = "ToolControlDimen";
            this.FormClosed += new System.Windows.Forms.FormClosedEventHandler(this.ToolControlDimen_FormClosed);
            this.groupBox1.ResumeLayout(false);
            ((System.ComponentModel.ISupportInitialize)(this.OISTree)).EndInit();
            this.ResumeLayout(false);

        }

        #endregion

        private DevComponents.DotNetBar.StyleManager styleManager1;
        private System.Windows.Forms.GroupBox groupBox1;
        private DevComponents.DotNetBar.LabelX labelX1;
        private DevComponents.DotNetBar.LabelX labelX2;
        private DevComponents.DotNetBar.LabelX labelX3;
        private DevComponents.DotNetBar.LabelX labelX4;
        private DevComponents.DotNetBar.LabelX labelX5;
        private DevComponents.DotNetBar.LabelX labelX6;
        private DevComponents.DotNetBar.LabelX CusName;
        private DevComponents.DotNetBar.LabelX PartNo;
        private DevComponents.DotNetBar.LabelX OIS;
        private DevComponents.DotNetBar.LabelX OpRev;
        private DevComponents.DotNetBar.LabelX CusRev;
        private DevComponents.DotNetBar.Controls.ComboBoxEx DraftingRev;
        private DevComponents.AdvTree.AdvTree OISTree;
        private DevComponents.AdvTree.NodeConnector nodeConnector1;
        private DevComponents.DotNetBar.ElementStyle elementStyle1;
        private DevComponents.AdvTree.ColumnHeader columnHeader1;
        private DevComponents.AdvTree.ColumnHeader columnHeader2;
        private DevComponents.AdvTree.ColumnHeader columnHeader3;
        private DevComponents.DotNetBar.ButtonX buttonX1;
        private DevComponents.DotNetBar.LabelX labelX7;
        private DevComponents.DotNetBar.Controls.ComboBoxEx ToolNoCombo;
        private DevComponents.DotNetBar.ButtonX LookPath;
        private DevComponents.DotNetBar.SuperGrid.SuperGridControl SGC_ToolPath;
        private DevComponents.DotNetBar.SuperGrid.GridColumn gridColumn1;
        private DevComponents.DotNetBar.SuperGrid.GridColumn gridColumn2;
    }
}