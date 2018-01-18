using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using NHibernate;
using CaxGlobaltek;
using DevComponents.DotNetBar.Controls;

namespace PFMEA
{
    public class GetCusRevData
    {
        public static ISession session = MyHibernateHelper.SessionFactory.OpenSession();
        public static bool SetCusVerData(Sys_Customer customerSrNo, string partNo, ComboBoxEx CusVerCombobox)
        {
            try
            {
                IList<Com_PEMain> comPEMain = session.QueryOver<Com_PEMain>()
                                              .Where(x => x.sysCustomer == customerSrNo)
                                              .Where(x => x.partName == partNo)
                                              .List<Com_PEMain>();
                //CusVerCombobox.DisplayMember = "customerVer";
                //CusVerCombobox.ValueMember = "peSrNo";
                foreach (Com_PEMain i in comPEMain)
                {
                    if (CusVerCombobox.Items.Contains(i.customerVer))
                    {
                        continue;
                    }
                    CusVerCombobox.Items.Add(i.customerVer);
                }
                //CusVerCombobox.Items.Add(comPEMain);

            }
            catch (System.Exception ex)
            {
                return false;
            }
            return true;
        }
    }
}
