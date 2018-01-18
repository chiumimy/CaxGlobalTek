namespace AssignGauge
{
    partial class Product
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
            this.SelDimen = new DevComponents.DotNetBar.ButtonX();
            this.ProductData = new DevComponents.DotNetBar.Controls.ComboBoxEx();
            this.Chara_OK = new DevComponents.DotNetBar.ButtonX();
            this.Chara_Closed = new DevComponents.DotNetBar.ButtonX();
            this.SuspendLayout();
            // 
            // styleManager1
            // 
            this.styleManager1.ManagerStyle = DevComponents.DotNetBar.eStyle.Windows7Blue;
            this.styleManager1.MetroColorParameters = new DevComponents.DotNetBar.Metro.ColorTables.MetroColorGeneratorParameters(System.Drawing.Color.White, System.Drawing.Color.FromArgb(((int)(((byte)(43)))), ((int)(((byte)(87)))), ((int)(((byte)(154))))));
            // 
            // SelDimen
            // 
            this.SelDimen.AccessibleRole = System.Windows.Forms.AccessibleRole.PushButton;
            this.SelDimen.ColorTable = DevComponents.DotNetBar.eButtonColor.Flat;
            this.SelDimen.Font = new System.Drawing.Font("標楷體", 9.75F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(136)));
            this.SelDimen.Image = global::AssignGauge.Properties.Resources.Rulers_24px;
            this.SelDimen.Location = new System.Drawing.Point(12, 12);
            this.SelDimen.Name = "SelDimen";
            this.SelDimen.Size = new System.Drawing.Size(131, 52);
            this.SelDimen.Style = DevComponents.DotNetBar.eDotNetBarStyle.StyleManagerControlled;
            this.SelDimen.TabIndex = 0;
            this.SelDimen.Text = "選擇尺寸(0)";
            this.SelDimen.Click += new System.EventHandler(this.SelDimen_Click);
            // 
            // ProductData
            // 
            this.ProductData.DisplayMember = "Text";
            this.ProductData.DrawMode = System.Windows.Forms.DrawMode.OwnerDrawFixed;
            this.ProductData.FormattingEnabled = true;
            this.ProductData.ItemHeight = 16;
            this.ProductData.Location = new System.Drawing.Point(12, 70);
            this.ProductData.Name = "ProductData";
            this.ProductData.Size = new System.Drawing.Size(131, 22);
            this.ProductData.Style = DevComponents.DotNetBar.eDotNetBarStyle.StyleManagerControlled;
            this.ProductData.TabIndex = 1;
            // 
            // Chara_OK
            // 
            this.Chara_OK.AccessibleRole = System.Windows.Forms.AccessibleRole.PushButton;
            this.Chara_OK.ColorTable = DevComponents.DotNetBar.eButtonColor.Flat;
            this.Chara_OK.Font = new System.Drawing.Font("標楷體", 9F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(136)));
            this.Chara_OK.Image = global::AssignGauge.Properties.Resources.confirm_16px;
            this.Chara_OK.Location = new System.Drawing.Point(17, 109);
            this.Chara_OK.Name = "Chara_OK";
            this.Chara_OK.Size = new System.Drawing.Size(51, 37);
            this.Chara_OK.Style = DevComponents.DotNetBar.eDotNetBarStyle.StyleManagerControlled;
            this.Chara_OK.TabIndex = 2;
            this.Chara_OK.Text = "確認";
            this.Chara_OK.Click += new System.EventHandler(this.Chara_OK_Click);
            // 
            // Chara_Closed
            // 
            this.Chara_Closed.AccessibleRole = System.Windows.Forms.AccessibleRole.PushButton;
            this.Chara_Closed.ColorTable = DevComponents.DotNetBar.eButtonColor.Flat;
            this.Chara_Closed.Font = new System.Drawing.Font("標楷體", 9F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(136)));
            this.Chara_Closed.Image = global::AssignGauge.Properties.Resources.close_16px;
            this.Chara_Closed.Location = new System.Drawing.Point(92, 109);
            this.Chara_Closed.Name = "Chara_Closed";
            this.Chara_Closed.Size = new System.Drawing.Size(51, 37);
            this.Chara_Closed.Style = DevComponents.DotNetBar.eDotNetBarStyle.StyleManagerControlled;
            this.Chara_Closed.TabIndex = 3;
            this.Chara_Closed.Text = "關閉";
            this.Chara_Closed.Click += new System.EventHandler(this.Chara_Closed_Click);
            // 
            // Product
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 12F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.AutoSizeMode = System.Windows.Forms.AutoSizeMode.GrowAndShrink;
            this.ClientSize = new System.Drawing.Size(160, 162);
            this.Controls.Add(this.Chara_Closed);
            this.Controls.Add(this.Chara_OK);
            this.Controls.Add(this.ProductData);
            this.Controls.Add(this.SelDimen);
            this.DoubleBuffered = true;
            this.MaximizeBox = false;
            this.MinimizeBox = false;
            this.Name = "Product";
            this.Text = "Chara.";
            this.FormClosed += new System.Windows.Forms.FormClosedEventHandler(this.Product_FormClosed);
            this.ResumeLayout(false);

        }

        #endregion

        private DevComponents.DotNetBar.StyleManager styleManager1;
        private DevComponents.DotNetBar.ButtonX SelDimen;
        private DevComponents.DotNetBar.Controls.ComboBoxEx ProductData;
        private DevComponents.DotNetBar.ButtonX Chara_OK;
        private DevComponents.DotNetBar.ButtonX Chara_Closed;
    }
}