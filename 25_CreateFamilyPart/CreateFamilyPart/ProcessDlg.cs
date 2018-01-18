using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Windows.Forms;
using DevComponents.DotNetBar.SuperGrid;
using CaxGlobaltek;
using NHibernate;

namespace CreateFamilyPart
{
    public partial class ProcessDlg : DevComponents.DotNetBar.Office2007Form
    {
        public static ISession session = MyHibernateHelper.SessionFactory.OpenSession();
        public static GridPanel panel = new GridPanel();
        public static IList<Com_PartOperation> listComPartOperation = new List<Com_PartOperation>();
        public ProcessDlg(IList<Com_PartOperation> listComPartOperation)
        {
            InitializeComponent();
            //建立GridPanel
            panel = Old_SGC_Panel.PrimaryGrid;
            foreach (Com_PartOperation i in listComPartOperation)
            {
                panel.Rows.Add(new GridRow(i.operation1,
                    session.QueryOver<Sys_Operation2>().Where(x => x.operation2SrNo == i.sysOperation2.operation2SrNo)
                    .SingleOrDefault().operation2Name, i.erpCode));
            }
        }

        private void Close_Click(object sender, EventArgs e)
        {
            this.Close();
        }
    }
}
