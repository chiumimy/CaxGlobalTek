using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Windows.Forms;
using DevComponents.DotNetBar;
using NXOpen;
using NXOpen.UF;
using CaxGlobaltek;

namespace AssignGauge
{
    public partial class Product : Office2007Form
    {
        public static Session theSession = Session.GetSession();
        public static UI theUI;
        public static UFSession theUfSession = UFSession.GetUFSession();
        public static Part workPart = theSession.Parts.Work;
        public static Part displayPart = theSession.Parts.Display;
        public static NXObject[] SelDimensionAry;

        private bool status;
        public static Form parentForm = new Form();

        public Product(Form pForm, List<string> listProduct)
        {
            InitializeComponent();
            //將父層的Form傳進來
            parentForm = pForm;

            ProductData.Items.Add("");
            foreach (string i in listProduct)
                ProductData.Items.Add(i);

            ProductData.Text = "選擇尺寸類型";
        }

        private void Chara_OK_Click(object sender, EventArgs e)
        {
            try
            {
                if (ProductData.Text == "選擇尺寸類型" || ProductData.Text == "")
                {
                    MessageBox.Show("請選擇尺寸類型");
                    return;
                }

                foreach (NXObject i in SelDimensionAry)
                {
                    i.SetAttribute("PRODUCT", ProductData.Text);
                }
                MessageBox.Show("Assign成功");
                SelDimen.Text = "選擇物件(0)";
            }
            catch (System.Exception ex)
            {
                return;
            }
        }

        private void Chara_Closed_Click(object sender, EventArgs e)
        {
            this.Close();
        }

        private void SelDimen_Click(object sender, EventArgs e)
        {
            try
            {
                this.Hide();
                CaxPublic.SelectObjects(out SelDimensionAry);
                this.Show();
                SelDimen.Text = string.Format("選擇物件({0})", SelDimensionAry.Length.ToString());
            }
            catch (System.Exception ex)
            {
                return;
            }
        }

        private void Product_FormClosed(object sender, FormClosedEventArgs e)
        {
            parentForm.Show();
        }
    }
}
