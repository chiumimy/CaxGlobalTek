using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using NHibernate;
using CaxGlobaltek;

namespace AddDeleteDB
{
    public class RenewERP
    {
        private static ISession session = MyHibernateHelper.SessionFactory.OpenSession();
        public struct PartData
        {
            public string CustomerName { get; set; }
            public string PartNo { get; set; }
        }
        public static bool GetPartData(out List<PartData> listPartData)
        {
            IList<Com_PEMain> listComPEMain = session.QueryOver<Com_PEMain>().List();
            listPartData = new List<PartData>();
            try
            {
                foreach (Com_PEMain i in listComPEMain)
                {
                    PartData sPartData = new PartData();
                    sPartData.CustomerName = session.QueryOver<Sys_Customer>()
                        .Where(x => x.customerSrNo == i.sysCustomer.customerSrNo).SingleOrDefault().customerName;
                    sPartData.PartNo = i.partName;
                    if (listPartData.Contains(sPartData))
                    {
                        continue;
                    }
                    listPartData.Add(sPartData);
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
