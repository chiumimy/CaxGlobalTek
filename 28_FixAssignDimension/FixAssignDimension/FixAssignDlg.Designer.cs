namespace FixAssignDimension
{
    partial class FixAssignDlg
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
            this.ListSheet = new DevComponents.DotNetBar.Controls.ComboBoxEx();
            this.labelX2 = new DevComponents.DotNetBar.LabelX();
            this.DiCount = new DevComponents.DotNetBar.Controls.TextBoxX();
            this.splitContainer1 = new System.Windows.Forms.SplitContainer();
            this.selDimen = new DevComponents.DotNetBar.ButtonX();
            this.CleaningAttr = new DevComponents.DotNetBar.ButtonX();
            this.Close = new DevComponents.DotNetBar.ButtonX();
            this.OK = new DevComponents.DotNetBar.ButtonX();
            this.splitContainer1.Panel1.SuspendLayout();
            this.splitContainer1.Panel2.SuspendLayout();
            this.splitContainer1.SuspendLayout();
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
            this.labelX1.Font = new System.Drawing.Font("標楷體", 9.75F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(136)));
            this.labelX1.Location = new System.Drawing.Point(12, 12);
            this.labelX1.Name = "labelX1";
            this.labelX1.Size = new System.Drawing.Size(80, 23);
            this.labelX1.TabIndex = 0;
            this.labelX1.Text = "選擇圖紙：";
            // 
            // ListSheet
            // 
            this.ListSheet.DisplayMember = "Text";
            this.ListSheet.DrawMode = System.Windows.Forms.DrawMode.OwnerDrawFixed;
            this.ListSheet.FormattingEnabled = true;
            this.ListSheet.ItemHeight = 16;
            this.ListSheet.Location = new System.Drawing.Point(80, 12);
            this.ListSheet.Name = "ListSheet";
            this.ListSheet.Size = new System.Drawing.Size(152, 22);
            this.ListSheet.Style = DevComponents.DotNetBar.eDotNetBarStyle.StyleManagerControlled;
            this.ListSheet.TabIndex = 1;
            this.ListSheet.SelectedIndexChanged += new System.EventHandler(this.ListSheet_SelectedIndexChanged);
            // 
            // labelX2
            // 
            // 
            // 
            // 
            this.labelX2.BackgroundStyle.CornerType = DevComponents.DotNetBar.eCornerType.Square;
            this.labelX2.Font = new System.Drawing.Font("標楷體", 9.75F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(136)));
            this.labelX2.Location = new System.Drawing.Point(7, 51);
            this.labelX2.Name = "labelX2";
            this.labelX2.Size = new System.Drawing.Size(79, 23);
            this.labelX2.TabIndex = 3;
            this.labelX2.Text = "尺寸數量：";
            // 
            // DiCount
            // 
            // 
            // 
            // 
            this.DiCount.Border.Class = "TextBoxBorder";
            this.DiCount.Border.CornerType = DevComponents.DotNetBar.eCornerType.Square;
            this.DiCount.Location = new System.Drawing.Point(82, 51);
            this.DiCount.Name = "DiCount";
            this.DiCount.PreventEnterBeep = true;
            this.DiCount.Size = new System.Drawing.Size(36, 22);
            this.DiCount.TabIndex = 4;
            // 
            // splitContainer1
            // 
            this.splitContainer1.BorderStyle = System.Windows.Forms.BorderStyle.FixedSingle;
            this.splitContainer1.Location = new System.Drawing.Point(12, 47);
            this.splitContainer1.Name = "splitContainer1";
            // 
            // splitContainer1.Panel1
            // 
            this.splitContainer1.Panel1.Controls.Add(this.DiCount);
            this.splitContainer1.Panel1.Controls.Add(this.selDimen);
            this.splitContainer1.Panel1.Controls.Add(this.labelX2);
            // 
            // splitContainer1.Panel2
            // 
            this.splitContainer1.Panel2.Controls.Add(this.CleaningAttr);
            this.splitContainer1.Size = new System.Drawing.Size(220, 100);
            this.splitContainer1.SplitterDistance = 130;
            this.splitContainer1.TabIndex = 7;
            // 
            // selDimen
            // 
            this.selDimen.AccessibleRole = System.Windows.Forms.AccessibleRole.PushButton;
            this.selDimen.ColorTable = DevComponents.DotNetBar.eButtonColor.Flat;
            this.selDimen.Font = new System.Drawing.Font("標楷體", 9F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(136)));
            this.selDimen.Image = global::FixAssignDimension.Properties.Resources.Rulers_24px;
            this.selDimen.Location = new System.Drawing.Point(7, 12);
            this.selDimen.Name = "selDimen";
            this.selDimen.Size = new System.Drawing.Size(114, 33);
            this.selDimen.Style = DevComponents.DotNetBar.eDotNetBarStyle.StyleManagerControlled;
            this.selDimen.TabIndex = 2;
            this.selDimen.Text = "選擇尺寸(0)";
            this.selDimen.Click += new System.EventHandler(this.selDimen_Click);
            // 
            // CleaningAttr
            // 
            this.CleaningAttr.AccessibleRole = System.Windows.Forms.AccessibleRole.PushButton;
            this.CleaningAttr.ColorTable = DevComponents.DotNetBar.eButtonColor.Flat;
            this.CleaningAttr.Font = new System.Drawing.Font("標楷體", 9.75F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(136)));
            this.CleaningAttr.Image = global::FixAssignDimension.Properties.Resources.broom_24px;
            this.CleaningAttr.Location = new System.Drawing.Point(10, 3);
            this.CleaningAttr.Name = "CleaningAttr";
            this.CleaningAttr.Size = new System.Drawing.Size(64, 92);
            this.CleaningAttr.Style = DevComponents.DotNetBar.eDotNetBarStyle.StyleManagerControlled;
            this.CleaningAttr.TabIndex = 8;
            this.CleaningAttr.Text = "移除屬性(0)";
            this.CleaningAttr.Click += new System.EventHandler(this.CleaningAttr_Click);
            // 
            // Close
            // 
            this.Close.AccessibleRole = System.Windows.Forms.AccessibleRole.PushButton;
            this.Close.ColorTable = DevComponents.DotNetBar.eButtonColor.Flat;
            this.Close.Font = new System.Drawing.Font("標楷體", 12F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(136)));
            this.Close.Image = global::FixAssignDimension.Properties.Resources.close_24px;
            this.Close.Location = new System.Drawing.Point(138, 158);
            this.Close.Name = "Close";
            this.Close.Size = new System.Drawing.Size(94, 30);
            this.Close.Style = DevComponents.DotNetBar.eDotNetBarStyle.StyleManagerControlled;
            this.Close.TabIndex = 6;
            this.Close.Text = "關閉";
            this.Close.Click += new System.EventHandler(this.Close_Click);
            // 
            // OK
            // 
            this.OK.AccessibleRole = System.Windows.Forms.AccessibleRole.PushButton;
            this.OK.ColorTable = DevComponents.DotNetBar.eButtonColor.Flat;
            this.OK.Font = new System.Drawing.Font("標楷體", 12F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(136)));
            this.OK.Image = global::FixAssignDimension.Properties.Resources.Confirm_24px;
            this.OK.Location = new System.Drawing.Point(12, 158);
            this.OK.Name = "OK";
            this.OK.Size = new System.Drawing.Size(94, 30);
            this.OK.Style = DevComponents.DotNetBar.eDotNetBarStyle.StyleManagerControlled;
            this.OK.TabIndex = 5;
            this.OK.Text = "確認";
            this.OK.Click += new System.EventHandler(this.OK_Click);
            // 
            // FixAssignDlg
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 12F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.ClientSize = new System.Drawing.Size(246, 207);
            this.Controls.Add(this.splitContainer1);
            this.Controls.Add(this.Close);
            this.Controls.Add(this.OK);
            this.Controls.Add(this.ListSheet);
            this.Controls.Add(this.labelX1);
            this.DoubleBuffered = true;
            this.MaximizeBox = false;
            this.MinimizeBox = false;
            this.Name = "FixAssignDlg";
            this.Text = "FixAssignDlg";
            this.Load += new System.EventHandler(this.FixAssignDlg_Load);
            this.splitContainer1.Panel1.ResumeLayout(false);
            this.splitContainer1.Panel2.ResumeLayout(false);
            this.splitContainer1.ResumeLayout(false);
            this.ResumeLayout(false);

        }

        #endregion

        private DevComponents.DotNetBar.StyleManager styleManager1;
        private DevComponents.DotNetBar.LabelX labelX1;
        private DevComponents.DotNetBar.Controls.ComboBoxEx ListSheet;
        private DevComponents.DotNetBar.ButtonX selDimen;
        private DevComponents.DotNetBar.LabelX labelX2;
        private DevComponents.DotNetBar.Controls.TextBoxX DiCount;
        private DevComponents.DotNetBar.ButtonX OK;
        private DevComponents.DotNetBar.ButtonX Close;
        private System.Windows.Forms.SplitContainer splitContainer1;
        private DevComponents.DotNetBar.ButtonX CleaningAttr;
    }
}