using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using NHibernate;
using CaxGlobaltek;

namespace AddDeleteDB
{
    public class Customer
    {
        private static ISession session = MyHibernateHelper.SessionFactory.OpenSession();
        public static bool GetCustomerData(out List<string> listCustomerName)
        {
            listCustomerName = new List<string>();
            try
            {
                IList<Sys_Customer> sysCustomer = session.QueryOver<Sys_Customer>().List<Sys_Customer>();
                foreach (Sys_Customer i in sysCustomer)
                {
                    listCustomerName.Add(i.customerName.ToString());
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
