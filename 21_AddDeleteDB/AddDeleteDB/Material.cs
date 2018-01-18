using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using NHibernate;
using CaxGlobaltek;
using System.Windows.Forms;

namespace AddDeleteDB
{
    public class Material
    {
        private static ISession session = MyHibernateHelper.SessionFactory.OpenSession();
        public static bool GetMaterialData(out List<string> listMaterial)
        {
            listMaterial = new List<string>();
            try
            {
                IList<Sys_Material> sysMaterial = session.QueryOver<Sys_Material>().List<Sys_Material>();
                foreach (Sys_Material i in sysMaterial)
                {
                    listMaterial.Add(i.materialName);
                }
            }
            catch (System.Exception ex)
            {
                return false;
            }
            return true;
        }
        public static bool GetMaterialData(out Dictionary<string, List<string>> DicMaterial)
        {
            DicMaterial = new Dictionary<string, List<string>>();
            try
            {
                IList<Sys_Material> sysMaterial = session.QueryOver<Sys_Material>().List<Sys_Material>();
                foreach (Sys_Material i in sysMaterial)
                {
                    if (i.category == null)
                    {
                        continue;
                    }
                    List<string> tempValue = new List<string>();
                    if (DicMaterial.TryGetValue(i.category, out tempValue))
                    {
                        tempValue.Add(i.materialName);
                        DicMaterial[i.category] = tempValue;
                    }
                    else
                    {
                        tempValue = new List<string>();
                        tempValue.Add(i.materialName);
                        DicMaterial.Add(i.category, tempValue);
                    }
                }
            }
            catch (System.Exception ex)
            {
                return false;
            }
            return true;
        }
        public static bool CheckData(string CategoryText, string AddText, Dictionary<string, List<string>> DicMaterial)
        {
            try
            {
                if (CategoryText == "" || AddText == "")
                {
                    return false;
                }
                foreach (KeyValuePair<string, List<string>> kvp in DicMaterial)
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
