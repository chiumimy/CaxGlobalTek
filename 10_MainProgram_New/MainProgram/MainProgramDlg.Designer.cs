namespace MainProgram
{
    partial class MainProgramDlg
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
            this.ControlerGroup = new System.Windows.Forms.GroupBox();
            this.chb_EXTCALL = new System.Windows.Forms.CheckBox();
            this.chb_CALL = new System.Windows.Forms.CheckBox();
            this.chb_M198 = new System.Windows.Forms.CheckBox();
            this.chb_M98 = new System.Windows.Forms.CheckBox();
            this.chb_Fanuc = new System.Windows.Forms.CheckBox();
            this.chb_Heidenhain = new System.Windows.Forms.CheckBox();
            this.chb_Siemens = new System.Windows.Forms.CheckBox();
            this.NCGroup = new System.Windows.Forms.GroupBox();
            this.up_arrow = new System.Windows.Forms.PictureBox();
            this.down_arrow = new System.Windows.Forms.PictureBox();
            this.left_arrow = new System.Windows.Forms.PictureBox();
            this.right_arrow = new System.Windows.Forms.PictureBox();
            this.DeleteSel = new DevComponents.DotNetBar.ButtonX();
            this.listView5 = new System.Windows.Forms.ListView();
            this.OperName = ((System.Windows.Forms.ColumnHeader)(new System.Windows.Forms.ColumnHeader()));
            this.ToolNumber = ((System.Windows.Forms.ColumnHeader)(new System.Windows.Forms.ColumnHeader()));
            this.listView4 = new System.Windows.Forms.ListView();
            this.columnHeader1 = ((System.Windows.Forms.ColumnHeader)(new System.Windows.Forms.ColumnHeader()));
            this.listView3 = new System.Windows.Forms.ListView();
            this.columnHeader3 = ((System.Windows.Forms.ColumnHeader)(new System.Windows.Forms.ColumnHeader()));
            this.CopyItem = new DevComponents.DotNetBar.ButtonX();
            this.listView1 = new System.Windows.Forms.ListView();
            this.columnHeader2 = ((System.Windows.Forms.ColumnHeader)(new System.Windows.Forms.ColumnHeader()));
            this.comboBoxNCgroup = new DevComponents.DotNetBar.Controls.ComboBoxEx();
            this.labelX2 = new DevComponents.DotNetBar.LabelX();
            this.AddCondition = new System.Windows.Forms.GroupBox();
            this.ClearTextBox = new DevComponents.DotNetBar.ButtonX();
            this.UserDefineTxt = new DevComponents.DotNetBar.Controls.ComboBoxEx();
            this.UserAddCondition = new DevComponents.DotNetBar.ButtonX();
            this.UserCondition = new System.Windows.Forms.TextBox();
            this.CloseDlg = new DevComponents.DotNetBar.ButtonX();
            this.ExportMainProg = new DevComponents.DotNetBar.ButtonX();
            this.SiePath = new DevComponents.DotNetBar.Controls.TextBoxX();
            this.chb_SelPath = new System.Windows.Forms.CheckBox();
            this.ControlerGroup.SuspendLayout();
            this.NCGroup.SuspendLayout();
            ((System.ComponentModel.ISupportInitialize)(this.up_arrow)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.down_arrow)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.left_arrow)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.right_arrow)).BeginInit();
            this.AddCondition.SuspendLayout();
            this.SuspendLayout();
            // 
            // styleManager1
            // 
            this.styleManager1.ManagerStyle = DevComponents.DotNetBar.eStyle.Windows7Blue;
            this.styleManager1.MetroColorParameters = new DevComponents.DotNetBar.Metro.ColorTables.MetroColorGeneratorParameters(System.Drawing.Color.White, System.Drawing.Color.FromArgb(((int)(((byte)(43)))), ((int)(((byte)(87)))), ((int)(((byte)(154))))));
            // 
            // ControlerGroup
            // 
            this.ControlerGroup.Controls.Add(this.chb_EXTCALL);
            this.ControlerGroup.Controls.Add(this.chb_CALL);
            this.ControlerGroup.Location = new System.Drawing.Point(530, 90);
            this.ControlerGroup.Name = "ControlerGroup";
            this.ControlerGroup.Size = new System.Drawing.Size(220, 94);
            this.ControlerGroup.TabIndex = 0;
            this.ControlerGroup.TabStop = false;
            this.ControlerGroup.Text = "控制器";
            // 
            // chb_EXTCALL
            // 
            this.chb_EXTCALL.AutoSize = true;
            this.chb_EXTCALL.Location = new System.Drawing.Point(135, 24);
            this.chb_EXTCALL.Name = "chb_EXTCALL";
            this.chb_EXTCALL.Size = new System.Drawing.Size(76, 16);
            this.chb_EXTCALL.TabIndex = 11;
            this.chb_EXTCALL.Text = "EXTCALL";
            this.chb_EXTCALL.UseVisualStyleBackColor = true;
            this.chb_EXTCALL.CheckedChanged += new System.EventHandler(this.chb_EXTCALL_CheckedChanged);
            // 
            // chb_CALL
            // 
            this.chb_CALL.AutoSize = true;
            this.chb_CALL.Location = new System.Drawing.Point(83, 24);
            this.chb_CALL.Name = "chb_CALL";
            this.chb_CALL.Size = new System.Drawing.Size(54, 16);
            this.chb_CALL.TabIndex = 10;
            this.chb_CALL.Text = "CALL";
            this.chb_CALL.UseVisualStyleBackColor = true;
            this.chb_CALL.CheckedChanged += new System.EventHandler(this.chb_CALL_CheckedChanged);
            // 
            // chb_M198
            // 
            this.chb_M198.AutoSize = true;
            this.chb_M198.Location = new System.Drawing.Point(128, 90);
            this.chb_M198.Name = "chb_M198";
            this.chb_M198.Size = new System.Drawing.Size(52, 16);
            this.chb_M198.TabIndex = 9;
            this.chb_M198.Text = "M198";
            this.chb_M198.UseVisualStyleBackColor = true;
            this.chb_M198.CheckedChanged += new System.EventHandler(this.chb_M198_CheckedChanged);
            // 
            // chb_M98
            // 
            this.chb_M98.AutoSize = true;
            this.chb_M98.Location = new System.Drawing.Point(76, 90);
            this.chb_M98.Name = "chb_M98";
            this.chb_M98.Size = new System.Drawing.Size(46, 16);
            this.chb_M98.TabIndex = 8;
            this.chb_M98.Text = "M98";
            this.chb_M98.UseVisualStyleBackColor = true;
            this.chb_M98.CheckedChanged += new System.EventHandler(this.chb_M98_CheckedChanged);
            // 
            // chb_Fanuc
            // 
            this.chb_Fanuc.AutoSize = true;
            this.chb_Fanuc.Location = new System.Drawing.Point(11, 90);
            this.chb_Fanuc.Name = "chb_Fanuc";
            this.chb_Fanuc.Size = new System.Drawing.Size(52, 16);
            this.chb_Fanuc.TabIndex = 7;
            this.chb_Fanuc.Text = "Fanuc";
            this.chb_Fanuc.UseVisualStyleBackColor = true;
            this.chb_Fanuc.CheckedChanged += new System.EventHandler(this.chb_Fanuc_CheckedChanged);
            // 
            // chb_Heidenhain
            // 
            this.chb_Heidenhain.AutoSize = true;
            this.chb_Heidenhain.Location = new System.Drawing.Point(11, 68);
            this.chb_Heidenhain.Name = "chb_Heidenhain";
            this.chb_Heidenhain.Size = new System.Drawing.Size(77, 16);
            this.chb_Heidenhain.TabIndex = 6;
            this.chb_Heidenhain.Text = "Heidenhain";
            this.chb_Heidenhain.UseVisualStyleBackColor = true;
            this.chb_Heidenhain.CheckedChanged += new System.EventHandler(this.chb_Heidenhain_CheckedChanged);
            // 
            // chb_Siemens
            // 
            this.chb_Siemens.AutoSize = true;
            this.chb_Siemens.Location = new System.Drawing.Point(12, 12);
            this.chb_Siemens.Name = "chb_Siemens";
            this.chb_Siemens.Size = new System.Drawing.Size(62, 16);
            this.chb_Siemens.TabIndex = 5;
            this.chb_Siemens.Text = "Siemens";
            this.chb_Siemens.UseVisualStyleBackColor = true;
            this.chb_Siemens.CheckedChanged += new System.EventHandler(this.chb_Simens_CheckedChanged);
            // 
            // NCGroup
            // 
            this.NCGroup.Controls.Add(this.up_arrow);
            this.NCGroup.Controls.Add(this.down_arrow);
            this.NCGroup.Controls.Add(this.left_arrow);
            this.NCGroup.Controls.Add(this.right_arrow);
            this.NCGroup.Controls.Add(this.DeleteSel);
            this.NCGroup.Controls.Add(this.listView5);
            this.NCGroup.Controls.Add(this.listView4);
            this.NCGroup.Controls.Add(this.listView3);
            this.NCGroup.Controls.Add(this.CopyItem);
            this.NCGroup.Controls.Add(this.listView1);
            this.NCGroup.Controls.Add(this.comboBoxNCgroup);
            this.NCGroup.Controls.Add(this.labelX2);
            this.NCGroup.Location = new System.Drawing.Point(12, 120);
            this.NCGroup.Name = "NCGroup";
            this.NCGroup.Size = new System.Drawing.Size(495, 335);
            this.NCGroup.TabIndex = 2;
            this.NCGroup.TabStop = false;
            this.NCGroup.Text = "程式";
            // 
            // up_arrow
            // 
            this.up_arrow.Image = global::MainProgram.Properties.Resources.up_arrow_48px;
            this.up_arrow.Location = new System.Drawing.Point(447, 151);
            this.up_arrow.Name = "up_arrow";
            this.up_arrow.Size = new System.Drawing.Size(39, 31);
            this.up_arrow.SizeMode = System.Windows.Forms.PictureBoxSizeMode.Zoom;
            this.up_arrow.TabIndex = 21;
            this.up_arrow.TabStop = false;
            this.up_arrow.Click += new System.EventHandler(this.up_arrow_Click);
            // 
            // down_arrow
            // 
            this.down_arrow.Image = global::MainProgram.Properties.Resources.down_arrow_48px;
            this.down_arrow.Location = new System.Drawing.Point(447, 188);
            this.down_arrow.Name = "down_arrow";
            this.down_arrow.Size = new System.Drawing.Size(39, 31);
            this.down_arrow.SizeMode = System.Windows.Forms.PictureBoxSizeMode.Zoom;
            this.down_arrow.TabIndex = 20;
            this.down_arrow.TabStop = false;
            this.down_arrow.Click += new System.EventHandler(this.down_arrow_Click);
            // 
            // left_arrow
            // 
            this.left_arrow.Image = global::MainProgram.Properties.Resources.left_arrow_48px;
            this.left_arrow.Location = new System.Drawing.Point(203, 188);
            this.left_arrow.Name = "left_arrow";
            this.left_arrow.Size = new System.Drawing.Size(39, 31);
            this.left_arrow.SizeMode = System.Windows.Forms.PictureBoxSizeMode.Zoom;
            this.left_arrow.TabIndex = 19;
            this.left_arrow.TabStop = false;
            this.left_arrow.Click += new System.EventHandler(this.left_arrow_Click);
            // 
            // right_arrow
            // 
            this.right_arrow.Image = global::MainProgram.Properties.Resources.right_arrow_48px;
            this.right_arrow.Location = new System.Drawing.Point(203, 151);
            this.right_arrow.Name = "right_arrow";
            this.right_arrow.Size = new System.Drawing.Size(39, 31);
            this.right_arrow.SizeMode = System.Windows.Forms.PictureBoxSizeMode.Zoom;
            this.right_arrow.TabIndex = 18;
            this.right_arrow.TabStop = false;
            this.right_arrow.Click += new System.EventHandler(this.right_arrow_Click);
            // 
            // DeleteSel
            // 
            this.DeleteSel.AccessibleRole = System.Windows.Forms.AccessibleRole.PushButton;
            this.DeleteSel.ColorTable = DevComponents.DotNetBar.eButtonColor.Flat;
            this.DeleteSel.Font = new System.Drawing.Font("標楷體", 9F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(136)));
            this.DeleteSel.Location = new System.Drawing.Point(379, 23);
            this.DeleteSel.Name = "DeleteSel";
            this.DeleteSel.Size = new System.Drawing.Size(61, 23);
            this.DeleteSel.Style = DevComponents.DotNetBar.eDotNetBarStyle.StyleManagerControlled;
            this.DeleteSel.TabIndex = 17;
            this.DeleteSel.Text = "刪除";
            this.DeleteSel.Click += new System.EventHandler(this.DeleteSel_Click);
            // 
            // listView5
            // 
            this.listView5.Columns.AddRange(new System.Windows.Forms.ColumnHeader[] {
            this.OperName,
            this.ToolNumber});
            this.listView5.GridLines = true;
            this.listView5.HeaderStyle = System.Windows.Forms.ColumnHeaderStyle.Nonclickable;
            this.listView5.Location = new System.Drawing.Point(17, 52);
            this.listView5.Name = "listView5";
            this.listView5.Size = new System.Drawing.Size(173, 277);
            this.listView5.Sorting = System.Windows.Forms.SortOrder.Ascending;
            this.listView5.TabIndex = 16;
            this.listView5.UseCompatibleStateImageBehavior = false;
            this.listView5.View = System.Windows.Forms.View.Details;
            this.listView5.MouseUp += new System.Windows.Forms.MouseEventHandler(this.listView5_MouseUp);
            // 
            // OperName
            // 
            this.OperName.Text = "程式名";
            this.OperName.Width = 82;
            // 
            // ToolNumber
            // 
            this.ToolNumber.Text = "刀號";
            this.ToolNumber.TextAlign = System.Windows.Forms.HorizontalAlignment.Center;
            this.ToolNumber.Width = 82;
            // 
            // listView4
            // 
            this.listView4.Columns.AddRange(new System.Windows.Forms.ColumnHeader[] {
            this.columnHeader1});
            this.listView4.GridLines = true;
            this.listView4.HeaderStyle = System.Windows.Forms.ColumnHeaderStyle.None;
            this.listView4.Location = new System.Drawing.Point(256, 288);
            this.listView4.Name = "listView4";
            this.listView4.Size = new System.Drawing.Size(184, 41);
            this.listView4.TabIndex = 14;
            this.listView4.UseCompatibleStateImageBehavior = false;
            this.listView4.View = System.Windows.Forms.View.Details;
            // 
            // columnHeader1
            // 
            this.columnHeader1.Width = 180;
            // 
            // listView3
            // 
            this.listView3.Columns.AddRange(new System.Windows.Forms.ColumnHeader[] {
            this.columnHeader3});
            this.listView3.GridLines = true;
            this.listView3.HeaderStyle = System.Windows.Forms.ColumnHeaderStyle.None;
            this.listView3.Location = new System.Drawing.Point(256, 52);
            this.listView3.Name = "listView3";
            this.listView3.Size = new System.Drawing.Size(184, 43);
            this.listView3.TabIndex = 13;
            this.listView3.UseCompatibleStateImageBehavior = false;
            this.listView3.View = System.Windows.Forms.View.Details;
            // 
            // columnHeader3
            // 
            this.columnHeader3.Width = 180;
            // 
            // CopyItem
            // 
            this.CopyItem.AccessibleRole = System.Windows.Forms.AccessibleRole.PushButton;
            this.CopyItem.ColorTable = DevComponents.DotNetBar.eButtonColor.Flat;
            this.CopyItem.Font = new System.Drawing.Font("標楷體", 9F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(136)));
            this.CopyItem.Location = new System.Drawing.Point(256, 23);
            this.CopyItem.Name = "CopyItem";
            this.CopyItem.Size = new System.Drawing.Size(61, 23);
            this.CopyItem.Style = DevComponents.DotNetBar.eDotNetBarStyle.StyleManagerControlled;
            this.CopyItem.TabIndex = 6;
            this.CopyItem.Text = "複製";
            this.CopyItem.Click += new System.EventHandler(this.CopyItem_Click);
            // 
            // listView1
            // 
            this.listView1.AllowDrop = true;
            this.listView1.Columns.AddRange(new System.Windows.Forms.ColumnHeader[] {
            this.columnHeader2});
            this.listView1.GridLines = true;
            this.listView1.HeaderStyle = System.Windows.Forms.ColumnHeaderStyle.None;
            this.listView1.LabelWrap = false;
            this.listView1.Location = new System.Drawing.Point(256, 101);
            this.listView1.Name = "listView1";
            this.listView1.Size = new System.Drawing.Size(184, 181);
            this.listView1.TabIndex = 4;
            this.listView1.UseCompatibleStateImageBehavior = false;
            this.listView1.View = System.Windows.Forms.View.Details;
            this.listView1.ItemDrag += new System.Windows.Forms.ItemDragEventHandler(this.listView1_ItemDrag);
            this.listView1.DragDrop += new System.Windows.Forms.DragEventHandler(this.listView1_DragDrop);
            this.listView1.DragEnter += new System.Windows.Forms.DragEventHandler(this.listView1_DragEnter);
            this.listView1.DragOver += new System.Windows.Forms.DragEventHandler(this.listView1_DragOver);
            this.listView1.DragLeave += new System.EventHandler(this.listView1_DragLeave);
            this.listView1.MouseUp += new System.Windows.Forms.MouseEventHandler(this.listView1_MouseUp);
            // 
            // columnHeader2
            // 
            this.columnHeader2.Width = 200;
            // 
            // comboBoxNCgroup
            // 
            this.comboBoxNCgroup.DisplayMember = "Text";
            this.comboBoxNCgroup.DrawMode = System.Windows.Forms.DrawMode.OwnerDrawFixed;
            this.comboBoxNCgroup.DropDownStyle = System.Windows.Forms.ComboBoxStyle.DropDownList;
            this.comboBoxNCgroup.FormattingEnabled = true;
            this.comboBoxNCgroup.ItemHeight = 16;
            this.comboBoxNCgroup.Location = new System.Drawing.Point(88, 21);
            this.comboBoxNCgroup.Name = "comboBoxNCgroup";
            this.comboBoxNCgroup.Size = new System.Drawing.Size(102, 22);
            this.comboBoxNCgroup.Style = DevComponents.DotNetBar.eDotNetBarStyle.StyleManagerControlled;
            this.comboBoxNCgroup.TabIndex = 0;
            this.comboBoxNCgroup.SelectedIndexChanged += new System.EventHandler(this.comboBoxNCgroup_SelectedIndexChanged);
            // 
            // labelX2
            // 
            // 
            // 
            // 
            this.labelX2.BackgroundStyle.CornerType = DevComponents.DotNetBar.eCornerType.Square;
            this.labelX2.Font = new System.Drawing.Font("標楷體", 9.75F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(136)));
            this.labelX2.Location = new System.Drawing.Point(17, 23);
            this.labelX2.Name = "labelX2";
            this.labelX2.Size = new System.Drawing.Size(75, 23);
            this.labelX2.TabIndex = 1;
            this.labelX2.Text = "群組名稱：";
            // 
            // AddCondition
            // 
            this.AddCondition.Controls.Add(this.ClearTextBox);
            this.AddCondition.Controls.Add(this.UserDefineTxt);
            this.AddCondition.Controls.Add(this.UserAddCondition);
            this.AddCondition.Controls.Add(this.UserCondition);
            this.AddCondition.Location = new System.Drawing.Point(238, 12);
            this.AddCondition.Name = "AddCondition";
            this.AddCondition.Size = new System.Drawing.Size(269, 94);
            this.AddCondition.TabIndex = 1;
            this.AddCondition.TabStop = false;
            this.AddCondition.Text = "新增條件";
            // 
            // ClearTextBox
            // 
            this.ClearTextBox.AccessibleRole = System.Windows.Forms.AccessibleRole.PushButton;
            this.ClearTextBox.ColorTable = DevComponents.DotNetBar.eButtonColor.Flat;
            this.ClearTextBox.Font = new System.Drawing.Font("標楷體", 9F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(136)));
            this.ClearTextBox.Image = global::MainProgram.Properties.Resources.broom_16px;
            this.ClearTextBox.Location = new System.Drawing.Point(218, 52);
            this.ClearTextBox.Name = "ClearTextBox";
            this.ClearTextBox.Size = new System.Drawing.Size(45, 32);
            this.ClearTextBox.Style = DevComponents.DotNetBar.eDotNetBarStyle.StyleManagerControlled;
            this.ClearTextBox.TabIndex = 5;
            this.ClearTextBox.Text = "清空";
            this.ClearTextBox.Click += new System.EventHandler(this.ClearTextBox_Click);
            // 
            // UserDefineTxt
            // 
            this.UserDefineTxt.DisplayMember = "Text";
            this.UserDefineTxt.DrawMode = System.Windows.Forms.DrawMode.OwnerDrawFixed;
            this.UserDefineTxt.FormattingEnabled = true;
            this.UserDefineTxt.ItemHeight = 16;
            this.UserDefineTxt.Location = new System.Drawing.Point(161, 18);
            this.UserDefineTxt.Name = "UserDefineTxt";
            this.UserDefineTxt.Size = new System.Drawing.Size(102, 22);
            this.UserDefineTxt.Style = DevComponents.DotNetBar.eDotNetBarStyle.StyleManagerControlled;
            this.UserDefineTxt.TabIndex = 2;
            this.UserDefineTxt.SelectedIndexChanged += new System.EventHandler(this.UserDefineTxt_SelectedIndexChanged);
            // 
            // UserAddCondition
            // 
            this.UserAddCondition.AccessibleRole = System.Windows.Forms.AccessibleRole.PushButton;
            this.UserAddCondition.ColorTable = DevComponents.DotNetBar.eButtonColor.Flat;
            this.UserAddCondition.Font = new System.Drawing.Font("標楷體", 9F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(136)));
            this.UserAddCondition.Image = global::MainProgram.Properties.Resources.add_16px;
            this.UserAddCondition.Location = new System.Drawing.Point(161, 52);
            this.UserAddCondition.Name = "UserAddCondition";
            this.UserAddCondition.Size = new System.Drawing.Size(45, 32);
            this.UserAddCondition.Style = DevComponents.DotNetBar.eDotNetBarStyle.StyleManagerControlled;
            this.UserAddCondition.TabIndex = 1;
            this.UserAddCondition.Text = "新增";
            this.UserAddCondition.Click += new System.EventHandler(this.UserAddCondition_Click);
            // 
            // UserCondition
            // 
            this.UserCondition.Location = new System.Drawing.Point(6, 18);
            this.UserCondition.Multiline = true;
            this.UserCondition.Name = "UserCondition";
            this.UserCondition.ScrollBars = System.Windows.Forms.ScrollBars.Both;
            this.UserCondition.Size = new System.Drawing.Size(149, 70);
            this.UserCondition.TabIndex = 0;
            this.UserCondition.WordWrap = false;
            // 
            // CloseDlg
            // 
            this.CloseDlg.AccessibleRole = System.Windows.Forms.AccessibleRole.PushButton;
            this.CloseDlg.ColorTable = DevComponents.DotNetBar.eButtonColor.Flat;
            this.CloseDlg.Font = new System.Drawing.Font("標楷體", 9F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(136)));
            this.CloseDlg.Image = global::MainProgram.Properties.Resources.Error_24px;
            this.CloseDlg.Location = new System.Drawing.Point(432, 461);
            this.CloseDlg.Name = "CloseDlg";
            this.CloseDlg.Size = new System.Drawing.Size(75, 37);
            this.CloseDlg.Style = DevComponents.DotNetBar.eDotNetBarStyle.StyleManagerControlled;
            this.CloseDlg.TabIndex = 4;
            this.CloseDlg.Text = "關閉";
            this.CloseDlg.Click += new System.EventHandler(this.CloseDlg_Click);
            // 
            // ExportMainProg
            // 
            this.ExportMainProg.AccessibleRole = System.Windows.Forms.AccessibleRole.PushButton;
            this.ExportMainProg.ColorTable = DevComponents.DotNetBar.eButtonColor.Flat;
            this.ExportMainProg.Font = new System.Drawing.Font("標楷體", 9F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(136)));
            this.ExportMainProg.Image = global::MainProgram.Properties.Resources.Confirm_24px;
            this.ExportMainProg.Location = new System.Drawing.Point(351, 461);
            this.ExportMainProg.Name = "ExportMainProg";
            this.ExportMainProg.Size = new System.Drawing.Size(75, 37);
            this.ExportMainProg.Style = DevComponents.DotNetBar.eDotNetBarStyle.StyleManagerControlled;
            this.ExportMainProg.TabIndex = 3;
            this.ExportMainProg.Text = "確認";
            this.ExportMainProg.Click += new System.EventHandler(this.ExportMainProg_Click);
            // 
            // SiePath
            // 
            // 
            // 
            // 
            this.SiePath.Border.Class = "TextBoxBorder";
            this.SiePath.Border.CornerType = DevComponents.DotNetBar.eCornerType.Square;
            this.SiePath.Location = new System.Drawing.Point(12, 34);
            this.SiePath.Name = "SiePath";
            this.SiePath.PreventEnterBeep = true;
            this.SiePath.Size = new System.Drawing.Size(220, 22);
            this.SiePath.TabIndex = 10;
            // 
            // chb_SelPath
            // 
            this.chb_SelPath.AutoSize = true;
            this.chb_SelPath.Location = new System.Drawing.Point(80, 12);
            this.chb_SelPath.Name = "chb_SelPath";
            this.chb_SelPath.Size = new System.Drawing.Size(72, 16);
            this.chb_SelPath.TabIndex = 11;
            this.chb_SelPath.Text = "指定路徑";
            this.chb_SelPath.UseVisualStyleBackColor = true;
            this.chb_SelPath.CheckedChanged += new System.EventHandler(this.chb_SelPath_CheckedChanged);
            // 
            // MainProgramDlg
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 12F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.ClientSize = new System.Drawing.Size(519, 505);
            this.Controls.Add(this.chb_SelPath);
            this.Controls.Add(this.SiePath);
            this.Controls.Add(this.CloseDlg);
            this.Controls.Add(this.ExportMainProg);
            this.Controls.Add(this.chb_M198);
            this.Controls.Add(this.NCGroup);
            this.Controls.Add(this.chb_M98);
            this.Controls.Add(this.AddCondition);
            this.Controls.Add(this.chb_Fanuc);
            this.Controls.Add(this.ControlerGroup);
            this.Controls.Add(this.chb_Heidenhain);
            this.Controls.Add(this.chb_Siemens);
            this.DoubleBuffered = true;
            this.Name = "MainProgramDlg";
            this.Text = "MainProgramDlg";
            this.Load += new System.EventHandler(this.MainProgramDlg_Load);
            this.ControlerGroup.ResumeLayout(false);
            this.ControlerGroup.PerformLayout();
            this.NCGroup.ResumeLayout(false);
            ((System.ComponentModel.ISupportInitialize)(this.up_arrow)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.down_arrow)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.left_arrow)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.right_arrow)).EndInit();
            this.AddCondition.ResumeLayout(false);
            this.AddCondition.PerformLayout();
            this.ResumeLayout(false);
            this.PerformLayout();

        }

        #endregion

        private DevComponents.DotNetBar.StyleManager styleManager1;
        private System.Windows.Forms.GroupBox ControlerGroup;
        private System.Windows.Forms.CheckBox chb_M198;
        private System.Windows.Forms.CheckBox chb_M98;
        private System.Windows.Forms.CheckBox chb_Fanuc;
        private System.Windows.Forms.CheckBox chb_Heidenhain;
        private System.Windows.Forms.CheckBox chb_Siemens;
        private System.Windows.Forms.GroupBox NCGroup;
        private System.Windows.Forms.ListView listView1;
        private DevComponents.DotNetBar.Controls.ComboBoxEx comboBoxNCgroup;
        private DevComponents.DotNetBar.LabelX labelX2;
        private System.Windows.Forms.GroupBox AddCondition;
        private DevComponents.DotNetBar.ButtonX CopyItem;
        private DevComponents.DotNetBar.Controls.ComboBoxEx UserDefineTxt;
        private DevComponents.DotNetBar.ButtonX UserAddCondition;
        private System.Windows.Forms.TextBox UserCondition;
        private DevComponents.DotNetBar.ButtonX ExportMainProg;
        private DevComponents.DotNetBar.ButtonX CloseDlg;
        private System.Windows.Forms.ListView listView4;
        private System.Windows.Forms.ListView listView3;
        private System.Windows.Forms.ColumnHeader OperName;
        private System.Windows.Forms.ColumnHeader ToolNumber;
        public System.Windows.Forms.ListView listView5;
        private System.Windows.Forms.ColumnHeader columnHeader2;
        private System.Windows.Forms.ColumnHeader columnHeader1;
        private System.Windows.Forms.ColumnHeader columnHeader3;
        private DevComponents.DotNetBar.ButtonX DeleteSel;
        private DevComponents.DotNetBar.ButtonX ClearTextBox;
        private System.Windows.Forms.PictureBox up_arrow;
        private System.Windows.Forms.PictureBox down_arrow;
        private System.Windows.Forms.PictureBox left_arrow;
        private System.Windows.Forms.PictureBox right_arrow;
        private System.Windows.Forms.CheckBox chb_EXTCALL;
        private System.Windows.Forms.CheckBox chb_CALL;
        private DevComponents.DotNetBar.Controls.TextBoxX SiePath;
        private System.Windows.Forms.CheckBox chb_SelPath;
    }
}