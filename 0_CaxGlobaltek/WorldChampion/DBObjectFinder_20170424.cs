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
        private AccessDbProvider dataProvider;
        private Hashtable tableNameMap
        {
            get
            {
                Hashtable tempHash = new Hashtable();
                tempHash.Add("WorldChampion.MRL.MillingToolData", "MILL_TOOL_DATA");
                return tempHash;
            }
        }


        public DBObjectFinder(string connectString)
        {
            dataProvider = new AccessDbProvider(connectString, tableNameMap);
        }

        public ArrayList FindMillingTool(string cutter_id)
        {
            string criteria = null;

            if (cutter_id != null)
            {
                criteria = "CUTTER_ID=" + cutter_id;
            }

            ArrayList queryArray = 
                dataProvider.QueryObject(Type.GetType(typeof(WorldChampion.MRL.MillingToolData).AssemblyQualifiedName), criteria, null);

            return queryArray;
        }

        public bool AddMillingToolData(object millToolData)
        {
            return dataProvider.InsertObject(millToolData);
        }

        public int FindMillingToolCount()
        {
            return dataProvider.QueryObjectCount(Type.GetType(typeof(WorldChampion.MRL.MillingToolData).AssemblyQualifiedName));
        }

        public bool UpdateMillingToolData(object millToolData,string cutter_id)
        {
             string criteria = null;

            if (cutter_id != null)
            {
                criteria = "CUTTER_ID=" + cutter_id;
            }

            return dataProvider.UpdateObject(millToolData, criteria, new string[] { "CUTTER_ID" });
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
