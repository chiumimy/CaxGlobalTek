using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using NHibernate;
using CaxGlobaltek;
using DevComponents.DotNetBar.Controls;

namespace PFMEA
{
    public class GetOpRevData
    {
        public static ISession session = MyHibernateHelper.SessionFactory.OpenSession();
        public static bool SetOpVerData(Sys_Customer customerSrNo, string partNo, string cusVer, ComboBoxEx OpVerCombobox)
        {
            try
            {
                IList<Com_PEMain> comPEMain = session.QueryOver<Com_PEMain>()
                                              .Where(x => x.sysCustomer == customerSrNo)
                                              .Where(x => x.partName == partNo)
                                              .Where(x => x.customerVer == cusVer)
                                              .List<Com_PEMain>();
                //CusVerCombobox.DisplayMember = "customerVer";
                //CusVerCombobox.ValueMember = "peSrNo";
                foreach (Com_PEMain i in comPEMain)
                {
                    if (OpVerCombobox.Items.Contains(i.opVer))
                    {
                        continue;
                    }
                    OpVerCombobox.Items.Add(i.opVer);
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
