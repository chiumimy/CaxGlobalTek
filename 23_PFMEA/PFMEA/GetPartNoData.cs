using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using CaxGlobaltek;
using DevComponents.DotNetBar.Controls;
using NHibernate;
using System.Windows.Forms;

namespace PFMEA
{
    public class GetPartNoData
    {
        public static ISession session = MyHibernateHelper.SessionFactory.OpenSession();
        public static bool SetPartNoData(Sys_Customer SysCustomer, ComboBoxEx PartNoCombo)
        {
            try
            {
                //MessageBox.Show(SysCustomer.customerSrNo.ToString());
                IList<Com_PEMain> comPEMain = session.QueryOver<Com_PEMain>()
                                              .Where(x => x.sysCustomer == SysCustomer)
                                              .List<Com_PEMain>();

                foreach (Com_PEMain i in comPEMain)
                {
                    if (PartNoCombo.Items.Contains(i.partName))
                        continue;

                    PartNoCombo.Items.Add(i.partName);
                }
            }
            catch (System.Exception ex)
            {
                return false;
            }
            return true;
        }
    }
}
