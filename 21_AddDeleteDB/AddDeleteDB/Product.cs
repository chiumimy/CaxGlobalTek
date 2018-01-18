using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using NHibernate;
using CaxGlobaltek;

namespace AddDeleteDB
{
    public class Product
    {
        private static ISession session = MyHibernateHelper.SessionFactory.OpenSession();
        public static bool GetProductData(out List<string> listProduct)
        {
            listProduct = new List<string>();
            try
            {
                IList<Sys_Product> sysProduct = session.QueryOver<Sys_Product>().List();
                foreach (Sys_Product i in sysProduct)
                {
                    listProduct.Add(i.productName);
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
