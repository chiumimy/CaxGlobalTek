namespace MEUpload
{
    partial class MEUploadDlg
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
            this.PartNoLabel = new DevComponents.DotNetBar.LabelX();
            this.labelX3 = new DevComponents.DotNetBar.LabelX();
            this.OISLabel = new DevComponents.DotNetBar.LabelX();
            this.OK = new DevComponents.DotNetBar.ButtonX();
            this.labelX5 = new DevComponents.DotNetBar.LabelX();
            this.listView1 = new System.Windows.Forms.ListView();
            this.Cancel = new DevComponents.DotNetBar.ButtonX();
            this.ExportPFD = new DevComponents.DotNetBar.Controls.CheckBoxX();
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
            this.labelX1.Location = new System.Drawing.Point(12, 12);
            this.labelX1.Name = "labelX1";
            this.labelX1.Size = new System.Drawing.Size(75, 23);
            this.labelX1.TabIndex = 0;
            this.labelX1.Text = "料號：";
            // 
            // PartNoLabel
            // 
            // 
            // 
            // 
            this.PartNoLabel.BackgroundStyle.CornerType = DevComponents.DotNetBar.eCornerType.Square;
            this.PartNoLabel.Font = new System.Drawing.Font("新細明體", 13F);
            this.PartNoLabel.Location = new System.Drawing.Point(65, 9);
            this.PartNoLabel.Name = "PartNoLabel";
            this.PartNoLabel.Size = new System.Drawing.Size(207, 23);
            this.PartNoLabel.TabIndex = 1;
            this.PartNoLabel.Text = "labelX2";
            // 
            // labelX3
            // 
            // 
            // 
            // 
            this.labelX3.BackgroundStyle.CornerType = DevComponents.DotNetBar.eCornerType.Square;
            this.labelX3.Font = new System.Drawing.Font("標楷體", 12F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(136)));
            this.labelX3.Location = new System.Drawing.Point(12, 46);
            this.labelX3.Name = "labelX3";
            this.labelX3.Size = new System.Drawing.Size(75, 23);
            this.labelX3.TabIndex = 2;
            this.labelX3.Text = "步序：";
            // 
            // OISLabel
            // 
            // 
            // 
            // 
            this.OISLabel.BackgroundStyle.CornerType = DevComponents.DotNetBar.eCornerType.Square;
            this.OISLabel.Font = new System.Drawing.Font("新細明體", 13F);
            this.OISLabel.Location = new System.Drawing.Point(65, 43);
            this.OISLabel.Name = "OISLabel";
            this.OISLabel.Size = new System.Drawing.Size(115, 23);
            this.OISLabel.TabIndex = 3;
            this.OISLabel.Text = "labelX4";
            // 
            // OK
            // 
            this.OK.AccessibleRole = System.Windows.Forms.AccessibleRole.PushButton;
            this.OK.ColorTable = DevComponents.DotNetBar.eButtonColor.Flat;
            this.OK.Font = new System.Drawing.Font("標楷體", 12F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(136)));
            this.OK.Image = global::MEUpload.Properties.Resources.upload_24px;
            this.OK.Location = new System.Drawing.Point(65, 357);
            this.OK.Name = "OK";
            this.OK.Size = new System.Drawing.Size(75, 32);
            this.OK.Style = DevComponents.DotNetBar.eDotNetBarStyle.StyleManagerControlled;
            this.OK.TabIndex = 5;
            this.OK.Text = "確認";
            this.OK.Click += new System.EventHandler(this.OK_Click);
            // 
            // labelX5
            // 
            // 
            // 
            // 
            this.labelX5.BackgroundStyle.CornerType = DevComponents.DotNetBar.eCornerType.Square;
            this.labelX5.Font = new System.Drawing.Font("標楷體", 12F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(136)));
            this.labelX5.Location = new System.Drawing.Point(12, 110);
            this.labelX5.Name = "labelX5";
            this.labelX5.Size = new System.Drawing.Size(124, 23);
            this.labelX5.TabIndex = 6;
            this.labelX5.Text = "本次上傳檔案";
            // 
            // listView1
            // 
            this.listView1.Location = new System.Drawing.Point(12, 139);
            this.listView1.Name = "listView1";
            this.listView1.Size = new System.Drawing.Size(260, 212);
            this.listView1.TabIndex = 7;
            this.listView1.UseCompatibleStateImageBehavior = false;
            this.listView1.View = System.Windows.Forms.View.List;
            // 
            // Cancel
            // 
            this.Cancel.AccessibleRole = System.Windows.Forms.AccessibleRole.PushButton;
            this.Cancel.ColorTable = DevComponents.DotNetBar.eButtonColor.Flat;
            this.Cancel.Font = new System.Drawing.Font("標楷體", 12F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(136)));
            this.Cancel.Image = global::MEUpload.Properties.Resources.close_24px;
            this.Cancel.Location = new System.Drawing.Point(154, 357);
            this.Cancel.Name = "Cancel";
            this.Cancel.Size = new System.Drawing.Size(75, 32);
            this.Cancel.Style = DevComponents.DotNetBar.eDotNetBarStyle.StyleManagerControlled;
            this.Cancel.TabIndex = 8;
            this.Cancel.Text = "關閉";
            this.Cancel.Click += new System.EventHandler(this.Cancel_Click);
            // 
            // ExportPFD
            // 
            // 
            // 
            // 
            this.ExportPFD.BackgroundStyle.CornerType = DevComponents.DotNetBar.eCornerType.Square;
            this.ExportPFD.Font = new System.Drawing.Font("標楷體", 12F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(136)));
            this.ExportPFD.Location = new System.Drawing.Point(12, 75);
            this.ExportPFD.Name = "ExportPFD";
            this.ExportPFD.Size = new System.Drawing.Size(260, 23);
            this.ExportPFD.Style = DevComponents.DotNetBar.eDotNetBarStyle.StyleManagerControlled;
            this.ExportPFD.TabIndex = 9;
            this.ExportPFD.Text = "輸出OIS製程圖PDF";
            // 
            // MEUploadDlg
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 12F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.ClientSize = new System.Drawing.Size(284, 401);
            this.Controls.Add(this.ExportPFD);
            this.Controls.Add(this.Cancel);
            this.Controls.Add(this.listView1);
            this.Controls.Add(this.labelX5);
            this.Controls.Add(this.OK);
            this.Controls.Add(this.OISLabel);
            this.Controls.Add(this.labelX3);
            this.Controls.Add(this.PartNoLabel);
            this.Controls.Add(this.labelX1);
            this.DoubleBuffered = true;
            this.Name = "MEUploadDlg";
            this.Text = "MEUploadDlg";
            this.Load += new System.EventHandler(this.MEUploadDlg_Load);
            this.ResumeLayout(false);

        }

        #endregion

        private DevComponents.DotNetBar.StyleManager styleManager1;
        private DevComponents.DotNetBar.LabelX labelX1;
        private DevComponents.DotNetBar.LabelX PartNoLabel;
        private DevComponents.DotNetBar.LabelX labelX3;
        private DevComponents.DotNetBar.LabelX OISLabel;
        private DevComponents.DotNetBar.ButtonX OK;
        private DevComponents.DotNetBar.LabelX labelX5;
        private System.Windows.Forms.ListView listView1;
        private DevComponents.DotNetBar.ButtonX Cancel;
        private DevComponents.DotNetBar.Controls.CheckBoxX ExportPFD;
    }
}