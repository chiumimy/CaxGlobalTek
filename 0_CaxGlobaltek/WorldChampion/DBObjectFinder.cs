using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Collections;
using System.Configuration;

namespace WorldChampion
{
    public class DBObjectFinder
    {
        //連結DB的工具
        private AccessDbProvider dataProvider;
        //資料表跟類別對應雜湊表
        private Hashtable tableNameMap
        {
            get
            {
                Hashtable tempHash = new Hashtable();
                tempHash.Add("WorldChampion.MRL.MillingToolData", "MILL_TOOL_DATA");
                tempHash.Add("WorldChampion.MRL.MillingGradeMap", "MILL_GRADE_MAP");
                tempHash.Add("WorldChampion.MRL.MillingHolderData", "MILL_HOLDER_DATA");
                tempHash.Add("WorldChampion.MRL.MillingHolderStep", "MILL_HOLDER_STEP");
                return tempHash;
            }
        }

        //初始化，輸入連結字串
        public DBObjectFinder(string connectString)
        {
            dataProvider = new AccessDbProvider(connectString, tableNameMap);
        }

        //獲得銑削刀具資料
        public ArrayList FindMillingTool(string supplier)
        {
            string criteria = null;

            if (supplier != null)
            {
                criteria = string.Format("SUPPLIER= '{0}'", supplier);
            }

            ArrayList queryArray = 
                dataProvider.QueryObject(Type.GetType(typeof(WorldChampion.MRL.MillingToolData).AssemblyQualifiedName), criteria, null);

            return queryArray;
        }
        //插入新的銑削刀具資料
        public bool AddMillingToolData(object millToolData)
        {
            return dataProvider.InsertObject(millToolData, new string[] { "CUTTER_ID" });
        }
        //獲得DB內銑削刀具的數量
        public int FindMillingToolCount()
        {
            return dataProvider.QueryObjectCount(Type.GetType(typeof(WorldChampion.MRL.MillingToolData).AssemblyQualifiedName));
        }
        //更新銑削刀具資料
        public bool UpdateMillingToolData(object millToolData, string cutter_id)
        {
            string criteria = null;

            if (cutter_id != null)
            {
                criteria = "CUTTER_ID=" + cutter_id;
            }

            return dataProvider.UpdateObject(millToolData, criteria, new string[] { "CUTTER_ID" });
        }
        //刪除銑削刀具資料
        public bool DeleteMillingToolData(string cutter_id)
        {
            string criteria = null;

            if (cutter_id != null)
            {
                criteria = string.Format("CUTTER_ID= {0}", cutter_id);
            }

            return dataProvider.DeleteObject(Type.GetType(typeof(WorldChampion.MRL.MillingToolData).AssemblyQualifiedName), criteria);
   
        }
        //獲得銑削刀具資料中的供應商欄位的所有成員
        public ArrayList FindMillToolSupplierList()
        {
            ArrayList supplierList = dataProvider.QueryDistinctObject(Type.GetType(typeof(WorldChampion.MRL.MillingToolData).AssemblyQualifiedName), "SUPPLIER", null);

            return supplierList;
        }
        //獲得銑削刀具材質與切削材料對應資料
        public ArrayList FindMillingGradeMap(string supplier)
        {
            string criteria = null;

            if (supplier != null)
            {
                criteria = string.Format("SUPPLIER= '{0}'", supplier);
            }

            ArrayList queryArray =
                dataProvider.QueryObject(Type.GetType(typeof(WorldChampion.MRL.MillingGradeMap).AssemblyQualifiedName), criteria, null);

            return queryArray;
        }
        //刪除銑削刀具材質與切削材料對應資料
        public bool DeleteMillingGradeMap(string supplier)
        {
            string criteria = null;

            if (supplier != null)
            {
                criteria = string.Format("SUPPLIER= '{0}'", supplier);
            }

            return dataProvider.DeleteObject(Type.GetType(typeof(WorldChampion.MRL.MillingGradeMap).AssemblyQualifiedName), criteria);
        }
        //插入新的銑削刀具材質與切削材料對應資料
        public bool AddMillingGradeMap(object millGradeMap)
        {
            return dataProvider.InsertObject(millGradeMap, new string[] { "MAP_ID" });
        }


        //獲得銑削刀把資料
        public ArrayList FindMillingHolder(string supplier, bool isType = false)
        {
            string criteria = null;

            if (supplier != null)
            {
                criteria = string.Format("SUPPLIER= '{0}' AND IS_TYPE='{1}' ", supplier, isType);
            }
            else
            {
                criteria = string.Format("IS_TYPE='{0}' ", isType);
            }

            ArrayList queryArray =
                dataProvider.QueryObject(Type.GetType(typeof(WorldChampion.MRL.MillingHolderData).AssemblyQualifiedName), criteria, null);

            return queryArray;
        }
        public ArrayList FindMillingHolder(string supplier, string designation, bool isType = false)
        {
            string criteria = string.Format("SUPPLIER= '{0}' AND DESIGNATION = '{1}' AND IS_TYPE='{2}' ", supplier, designation, isType);
            
            ArrayList queryArray =
                dataProvider.QueryObject(Type.GetType(typeof(WorldChampion.MRL.MillingHolderData).AssemblyQualifiedName), criteria, null);

            return queryArray;
        }

        //獲得銑削刀把階段資料
        public ArrayList FindMillingHolderStep(string holder_id)
        {
            string criteria = null;

            if (holder_id != null)
            {
                criteria = string.Format("HOLDER_ID= '{0}'", holder_id);
            }

            ArrayList queryArray = dataProvider.QueryObject(
                Type.GetType(typeof(WorldChampion.MRL.MillingHolderStep).AssemblyQualifiedName), criteria, "STEP_ID");

            return queryArray;
        }
        //獲得銑削刀把資料中的供應商欄位的所有成員
        public ArrayList FindMillHolderSupplierList()
        {
            string criteria = string.Format("IS_TYPE='false' ");

            ArrayList supplierList =
                dataProvider.QueryDistinctObject(Type.GetType(typeof(WorldChampion.MRL.MillingHolderData).AssemblyQualifiedName), "SUPPLIER", criteria);

            return supplierList;
        }

        //刪除銑削刀把資料(包含刀把階段資料)
        public bool DeleteMillingHolderData(string holder_id)
        {
            bool status;

            string criteria1 = null;
            string criteria2 = null;

            if (holder_id != null)
            {
                criteria1 = string.Format("HOLDER_ID= {0}", holder_id);
                criteria2 = string.Format("HOLDER_ID= '{0}' ", holder_id);
            }

            status = dataProvider.DeleteObject(Type.GetType(typeof(WorldChampion.MRL.MillingHolderData).AssemblyQualifiedName), criteria1);
            status = dataProvider.DeleteObject(Type.GetType(typeof(WorldChampion.MRL.MillingHolderStep).AssemblyQualifiedName), criteria2);
            
            return status;
        }
        //刪除銑削刀把階段資料
        public bool DeleteMillingHolderStepData(string holder_id)
        {
            bool status;

            string criteria = null;

            if (holder_id != null)
            {
                criteria = string.Format("HOLDER_ID= '{0}' ", holder_id);
            }

            status = dataProvider.DeleteObject(Type.GetType(typeof(WorldChampion.MRL.MillingHolderStep).AssemblyQualifiedName), criteria);

            return status;
        }



        //插入新的銑削刀把資料
        public bool AddMillingHolderData(object millHolderData)
        {
            return dataProvider.InsertObject(millHolderData, new string[] { "HOLDER_ID" });
        }
        //插入新的銑削刀把階段資料
        public bool AddMillingHolderStepData(object millHolderStepData)
        {
            return dataProvider.InsertObject(millHolderStepData, new string[] { "STEP_ID" });
        }

        //更新銑削刀把資料
        public bool UpdateMillingHolderData(object millHolderData, string holder_id)
        {
            string criteria = null;

            if (holder_id != null)
            {
                criteria = "HOLDER_ID=" + holder_id;
            }

            return dataProvider.UpdateObject(millHolderData, criteria, new string[] { "HOLDER_ID" });
        }

        





        //product_class
        public ArrayList FindProductClassByRid(string rid)
        {
            string ctiteria = null;

            if (rid != null)
            {
                ctiteria = "rid=" + rid + " AND TRASH='n' AND ONLINE='y' ";
            }

            ArrayList queryArray = dataProvider.QueryObject(Type.GetType("STP2PRT.product_class"), ctiteria, "orders");
            return queryArray;
        }
        //product_column_name
        public ArrayList FindProductColumnNameByRid(string rid)
        {
            string ctiteria = null;

            if (rid != null)
            {
                ctiteria = "rid=" + rid + "";
            }

            ArrayList queryArray = dataProvider.QueryObject(Type.GetType("STP2PRT.product_column_name"), ctiteria, null);
            return queryArray;
        }
        //product_part
        public ArrayList FindProductPartByRid(string rid)
        {
            string ctiteria = null;

            if (rid != null)
            {
                ctiteria = "rid=" + rid + " AND TRASH='n' AND ONLINE='y' ";
            }

            ArrayList queryArray = dataProvider.QueryObject(Type.GetType("STP2PRT.product_part"), ctiteria, "name");
            return queryArray;
        }
        //product_part_name
        public ArrayList FindProductPartNameByRid(string rid)
        {
            string ctiteria = null;

            if (rid != null)
            {
                ctiteria = "rid=" + rid + "";
            }

            ArrayList queryArray = dataProvider.QueryObject(Type.GetType("STP2PRT.product_part_name"), ctiteria, null);
            return queryArray;
        }
    }
}
