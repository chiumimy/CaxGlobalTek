using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using NHibernate;
using CaxGlobaltek;
using System.Windows.Forms;

namespace AddDeleteDB
{
    public class Operation2
    {
        private static ISession session = MyHibernateHelper.SessionFactory.OpenSession();
        public static bool GetOperation2Data(out List<string> listOperation2)
        {
            listOperation2 = new List<string>();
            try
            {
                IList<Sys_Operation2> sysOperation2 = session.QueryOver<Sys_Operation2>().List<Sys_Operation2>();
                foreach (Sys_Operation2 i in sysOperation2)
                {
                    listOperation2.Add(i.operation2Name);
                }
            }
            catch (System.Exception ex)
            {
                return false;
            }
            return true;
        }
        public static bool GetOperation2Data(out Dictionary<string, List<string>> DicOperation2)
        {
            DicOperation2 = new Dictionary<string, List<string>>();
            try
            {
                IList<Sys_Operation2> sysOperation2 = session.QueryOver<Sys_Operation2>().List<Sys_Operation2>();
                foreach (Sys_Operation2 i in sysOperation2)
                {
                    if (i.category == null)
                    {
                        continue;
                    }
                    List<string> tempValue = new List<string>();
                    if (DicOperation2.TryGetValue(i.category, out tempValue))
                    {
                        tempValue.Add(i.operation2Name);
                        DicOperation2[i.category] = tempValue;
                    }
                    else
                    {
                        tempValue = new List<string>();
                        tempValue.Add(i.operation2Name);
                        DicOperation2.Add(i.category, tempValue);
                    }
                }
            }
            catch (System.Exception ex)
            {
                return false;
            }
            return true;
        }
        public static bool CheckData(string CategoryText, string AddText, Dictionary<string, List<string>> DicOperation2)
        {
            try
            {
                if (CategoryText == "" || AddText == "")
                {
                    return false;
                }
                foreach (KeyValuePair<string, List<string>> kvp in DicOperation2)
                {
                    foreach (string i in kvp.Value)
                    {
                        if (i.ToUpper() == AddText.ToUpper())
                        {
                            MessageBox.Show(AddText + "已存在");
                            return false;
                        }
                    }
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
