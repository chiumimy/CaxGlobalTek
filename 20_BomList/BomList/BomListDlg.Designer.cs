namespace BomList
{
    partial class BomListDlg
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
            System.ComponentModel.ComponentResourceManager resources = new System.ComponentModel.ComponentResourceManager(typeof(BomListDlg));
            this.advTree1 = new DevComponents.AdvTree.AdvTree();
            this.columnHeader1 = new DevComponents.AdvTree.ColumnHeader();
            this.columnHeader2 = new DevComponents.AdvTree.ColumnHeader();
            this.nodeConnector1 = new DevComponents.AdvTree.NodeConnector();
            this.elementStyle1 = new DevComponents.DotNetBar.ElementStyle();
            this.ERPPartText = new DevComponents.DotNetBar.Controls.TextBoxX();
            this.Update = new System.Windows.Forms.PictureBox();
            this.Close = new DevComponents.DotNetBar.ButtonX();
            this.OK = new DevComponents.DotNetBar.ButtonX();
            this.RemoveHead = new DevComponents.DotNetBar.Controls.CheckBoxX();
            this.RemoveTail = new DevComponents.DotNetBar.Controls.CheckBoxX();
            this.RemoveCount = new DevComponents.DotNetBar.Controls.TextBoxX();
            this.RemoveSymbol = new DevComponents.DotNetBar.Controls.CheckBoxX();
            this.labelX1 = new DevComponents.DotNetBar.LabelX();
            ((System.ComponentModel.ISupportInitialize)(this.advTree1)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.Update)).BeginInit();
            this.SuspendLayout();
            // 
            // advTree1
            // 
            this.advTree1.AccessibleRole = System.Windows.Forms.AccessibleRole.Outline;
            this.advTree1.AllowDrop = true;
            this.advTree1.AlternateRowColor = System.Drawing.Color.Transparent;
            this.advTree1.BackColor = System.Drawing.SystemColors.Window;
            // 
            // 
            // 
            this.advTree1.BackgroundStyle.Class = "TreeBorderKey";
            this.advTree1.BackgroundStyle.CornerType = DevComponents.DotNetBar.eCornerType.Square;
            this.advTree1.Columns.Add(this.columnHeader1);
            this.advTree1.Columns.Add(this.columnHeader2);
            this.advTree1.Location = new System.Drawing.Point(12, 66);
            this.advTree1.MultiSelect = true;
            this.advTree1.MultiSelectRule = DevComponents.AdvTree.eMultiSelectRule.AnyNode;
            this.advTree1.Name = "advTree1";
            this.advTree1.NodesConnector = this.nodeConnector1;
            this.advTree1.NodeStyle = this.elementStyle1;
            this.advTree1.PathSeparator = ";";
            this.advTree1.SelectionBoxStyle = DevComponents.AdvTree.eSelectionStyle.FullRowSelect;
            this.advTree1.Size = new System.Drawing.Size(470, 21);
            this.advTree1.Styles.Add(this.elementStyle1);
            this.advTree1.TabIndex = 0;
            this.advTree1.Text = "advTree1";
            this.advTree1.BeforeExpand += new DevComponents.AdvTree.AdvTreeNodeCancelEventHandler(this.advTree1_BeforeExpand);
            this.advTree1.NodeClick += new DevComponents.AdvTree.TreeNodeMouseEventHandler(this.advTree1_NodeClick);
            // 
            // columnHeader1
            // 
            this.columnHeader1.Name = "columnHeader1";
            this.columnHeader1.Text = "檔案名稱";
            this.columnHeader1.Width.Absolute = 300;
            // 
            // columnHeader2
            // 
            this.columnHeader2.Name = "columnHeader2";
            this.columnHeader2.Text = "ERP編號";
            this.columnHeader2.Width.Absolute = 300;
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
            // ERPPartText
            // 
            // 
            // 
            // 
            this.ERPPartText.Border.Class = "TextBoxBorder";
            this.ERPPartText.Border.CornerType = DevComponents.DotNetBar.eCornerType.Square;
            this.ERPPartText.Location = new System.Drawing.Point(33, 13);
            this.ERPPartText.Name = "ERPPartText";
            this.ERPPartText.PreventEnterBeep = true;
            this.ERPPartText.Size = new System.Drawing.Size(175, 22);
            this.ERPPartText.TabIndex = 3;
            // 
            // Update
            // 
            this.Update.Image = global::BomList.Properties.Resources.cycle_32px;
            this.Update.Location = new System.Drawing.Point(437, 14);
            this.Update.Name = "Update";
            this.Update.Size = new System.Drawing.Size(45, 46);
            this.Update.SizeMode = System.Windows.Forms.PictureBoxSizeMode.Zoom;
            this.Update.TabIndex = 4;
            this.Update.TabStop = false;
            this.Update.Click += new System.EventHandler(this.Update_Click);
            // 
            // Close
            // 
            this.Close.AccessibleRole = System.Windows.Forms.AccessibleRole.PushButton;
            this.Close.ColorTable = DevComponents.DotNetBar.eButtonColor.OrangeWithBackground;
            this.Close.Font = new System.Drawing.Font("標楷體", 9.75F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(136)));
            this.Close.Image = global::BomList.Properties.Resources.close_16px;
            this.Close.Location = new System.Drawing.Point(258, 93);
            this.Close.Name = "Close";
            this.Close.Size = new System.Drawing.Size(58, 31);
            this.Close.Style = DevComponents.DotNetBar.eDotNetBarStyle.StyleManagerControlled;
            this.Close.TabIndex = 2;
            this.Close.Text = "關閉";
            this.Close.Click += new System.EventHandler(this.Close_Click);
            // 
            // OK
            // 
            this.OK.AccessibleRole = System.Windows.Forms.AccessibleRole.PushButton;
            this.OK.ColorTable = DevComponents.DotNetBar.eButtonColor.OrangeWithBackground;
            this.OK.Font = new System.Drawing.Font("標楷體", 9F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(136)));
            this.OK.Image = global::BomList.Properties.Resources.confirm_16px;
            this.OK.Location = new System.Drawing.Point(163, 93);
            this.OK.Name = "OK";
            this.OK.Size = new System.Drawing.Size(58, 31);
            this.OK.Style = DevComponents.DotNetBar.eDotNetBarStyle.StyleManagerControlled;
            this.OK.TabIndex = 1;
            this.OK.Text = "確認";
            this.OK.Click += new System.EventHandler(this.OK_Click);
            // 
            // RemoveHead
            // 
            // 
            // 
            // 
            this.RemoveHead.BackgroundStyle.CornerType = DevComponents.DotNetBar.eCornerType.Square;
            this.RemoveHead.Location = new System.Drawing.Point(228, 13);
            this.RemoveHead.Name = "RemoveHead";
            this.RemoveHead.Size = new System.Drawing.Size(60, 23);
            this.RemoveHead.Style = DevComponents.DotNetBar.eDotNetBarStyle.StyleManagerControlled;
            this.RemoveHead.TabIndex = 5;
            this.RemoveHead.Text = "去頭";
            this.RemoveHead.CheckedChanged += new System.EventHandler(this.RemoveHead_CheckedChanged);
            // 
            // RemoveTail
            // 
            // 
            // 
            // 
            this.RemoveTail.BackgroundStyle.CornerType = DevComponents.DotNetBar.eCornerType.Square;
            this.RemoveTail.Location = new System.Drawing.Point(228, 37);
            this.RemoveTail.Name = "RemoveTail";
            this.RemoveTail.Size = new System.Drawing.Size(60, 23);
            this.RemoveTail.Style = DevComponents.DotNetBar.eDotNetBarStyle.StyleManagerControlled;
            this.RemoveTail.TabIndex = 6;
            this.RemoveTail.Text = "去尾";
            this.RemoveTail.CheckedChanged += new System.EventHandler(this.RemoveTail_CheckedChanged);
            // 
            // RemoveCount
            // 
            // 
            // 
            // 
            this.RemoveCount.Border.Class = "TextBoxBorder";
            this.RemoveCount.Border.CornerType = DevComponents.DotNetBar.eCornerType.Square;
            this.RemoveCount.Location = new System.Drawing.Point(278, 37);
            this.RemoveCount.Name = "RemoveCount";
            this.RemoveCount.PreventEnterBeep = true;
            this.RemoveCount.Size = new System.Drawing.Size(130, 22);
            this.RemoveCount.TabIndex = 7;
            this.RemoveCount.Text = "請輸入要去除的位數";
            // 
            // RemoveSymbol
            // 
            // 
            // 
            // 
            this.RemoveSymbol.BackgroundStyle.CornerType = DevComponents.DotNetBar.eCornerType.Square;
            this.RemoveSymbol.Location = new System.Drawing.Point(278, 13);
            this.RemoveSymbol.Name = "RemoveSymbol";
            this.RemoveSymbol.Size = new System.Drawing.Size(148, 23);
            this.RemoveSymbol.Style = DevComponents.DotNetBar.eDotNetBarStyle.StyleManagerControlled;
            this.RemoveSymbol.TabIndex = 8;
            this.RemoveSymbol.Text = "去符號   \" - \"、\" _ \"、...";
            this.RemoveSymbol.CheckedChanged += new System.EventHandler(this.RemoveSymbol_CheckedChanged);
            // 
            // labelX1
            // 
            // 
            // 
            // 
            this.labelX1.BackgroundStyle.CornerType = DevComponents.DotNetBar.eCornerType.Square;
            this.labelX1.Location = new System.Drawing.Point(33, 37);
            this.labelX1.Name = "labelX1";
            this.labelX1.Size = new System.Drawing.Size(175, 23);
            this.labelX1.TabIndex = 9;
            this.labelX1.Text = "(可手動定義ERP料號)";
            this.labelX1.TextAlignment = System.Drawing.StringAlignment.Center;
            // 
            // BomListDlg
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 12F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.AutoSizeMode = System.Windows.Forms.AutoSizeMode.GrowAndShrink;
            this.ClientSize = new System.Drawing.Size(494, 132);
            this.Controls.Add(this.labelX1);
            this.Controls.Add(this.RemoveSymbol);
            this.Controls.Add(this.RemoveCount);
            this.Controls.Add(this.RemoveTail);
            this.Controls.Add(this.RemoveHead);
            this.Controls.Add(this.Update);
            this.Controls.Add(this.ERPPartText);
            this.Controls.Add(this.Close);
            this.Controls.Add(this.OK);
            this.Controls.Add(this.advTree1);
            this.Icon = ((System.Drawing.Icon)(resources.GetObject("$this.Icon")));
            this.Name = "BomListDlg";
            this.Text = "BomListDlg";
            this.Load += new System.EventHandler(this.BomListDlg_Load);
            ((System.ComponentModel.ISupportInitialize)(this.advTree1)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.Update)).EndInit();
            this.ResumeLayout(false);

        }

        #endregion

        private DevComponents.AdvTree.AdvTree advTree1;
        private DevComponents.AdvTree.NodeConnector nodeConnector1;
        private DevComponents.DotNetBar.ElementStyle elementStyle1;
        private DevComponents.AdvTree.ColumnHeader columnHeader1;
        private DevComponents.AdvTree.ColumnHeader columnHeader2;
        private DevComponents.DotNetBar.ButtonX OK;
        private DevComponents.DotNetBar.ButtonX Close;
        private DevComponents.DotNetBar.Controls.TextBoxX ERPPartText;
        private System.Windows.Forms.PictureBox Update;
        private DevComponents.DotNetBar.Controls.TextBoxX RemoveCount;
        private DevComponents.DotNetBar.Controls.CheckBoxX RemoveHead;
        private DevComponents.DotNetBar.Controls.CheckBoxX RemoveTail;
        private DevComponents.DotNetBar.Controls.CheckBoxX RemoveSymbol;
        private DevComponents.DotNetBar.LabelX labelX1;
    }
}