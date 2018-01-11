using System;
using System.Collections.Generic;
using System.Data;
using System.Data.OleDb;
using System.Windows.Forms;
using System.Linq;
using System.Text;
using System.Collections.Specialized;
using System.Collections;
using System.Reflection;
using System.Configuration;

namespace WorldChampion
{
    public class AccessDbProvider
    {
        private string dataSource;
        private OleDbConnection connect;
        //private NameValueCollection tableMap;
        private Hashtable tableMap;

        public AccessDbProvider(string _dataSource, Hashtable _tableMap)
        {
            dataSource = _dataSource;
            tableMap = _tableMap;
            connect = new OleDbConnection();
        }


        public ArrayList QueryObject(Type myType, string criteria, string order)
        {
            ArrayList objList = new ArrayList();

            if (myType == null)
            {
                return objList;
            }

            try
            {
                //獲得物件各屬性欄位
                FieldInfo[] fields = myType.GetFields();

                //連結DB
                connect.ConnectionString = dataSource;
                connect.Open();

                //建立SQL語法
                criteria = (criteria == null) ? null : " WHERE " + criteria;
                order = (order == null) ? null : " ORDER BY " + order;

                //獲得所有欄位
                string tableColumnStr = GetColumnStr(fields);
                //獲得表名
                string tableName = GetTableName(myType.FullName);
                //string tableName = myType.Name;

                //輸入SQL語法
                OleDbCommand command = connect.CreateCommand();
                command.CommandText = "SELECT " + tableColumnStr + " FROM " + tableName + criteria + order;

                //LOG
                //StaticData.logFile.StatusOut(command.CommandText);

                OleDbDataReader reader = command.ExecuteReader();

                //用reader讀取每個ROW
                while (reader.Read())
                {
                    //從myType建立物件
                    object tmp = (object)Activator.CreateInstance(myType);

                    //輪詢物件每個屬性
                    for (int i = 0; i < fields.Length; i++)
                    {

                        FieldInfo property1 = fields[i];
                        if (reader.IsDBNull(i))
                        {
                            property1.SetValue(tmp, null);
                            continue;
                        }

                        //數據表中紀錄的屬性值
                        object fieldValue = reader.GetValue(i);

                        //根據物件屬性不同的型別，記錄DB的屬性質
                        switch (Type.GetTypeCode(property1.FieldType))
                        {
                            case TypeCode.String:
                                property1.SetValue(tmp, Convert.ToString(fieldValue));
                                break;
                            case TypeCode.Int32:
                                property1.SetValue(tmp, Convert.ToInt32(fieldValue));
                                break;
                            case TypeCode.Single:
                                property1.SetValue(tmp, Convert.ToSingle(fieldValue));
                                break;
                            case TypeCode.Double:
                                property1.SetValue(tmp, Convert.ToDouble(fieldValue));
                                break;
                            default:
                                property1.SetValue(tmp, Convert.ToString(fieldValue));
                                break;
                        }
                    }
                    objList.Add(tmp);
                }
            }
            catch (System.Exception ex)
            {
                MessageBox.Show(ex.Message);
            }
            finally
            {
                connect.Close();
            }
            return objList;
        }
        /// <summary>
        /// 從tableMap(類別對應到數據表名稱的Hashtable)中以類別名稱typeName查找數據表名稱回傳
        /// </summary>
        /// <param name="typeName"></param>
        /// <returns></returns>
        private string GetTableName(string typeName)
        {
            string table = tableMap[typeName].ToString();
            if (table != null)
            {
                return table;
            }
            else
            {
                throw new ArgumentNullException();
            }
        }
        /// <summary>
        /// 根據Type的FieldInfo獲得的物件各屬性串聯字串(對應數據表各欄位逗號串聯)
        /// </summary>
        /// <param name="fields"></param>
        /// <returns></returns>
        private string GetColumnStr(FieldInfo[] fields)
        {
            string columnStr = "";
            bool first = true;

            foreach (FieldInfo field in fields)
            {
                if (first)
                {
                    columnStr = columnStr + field.Name;
                    first = false;
                }
                else
                {
                    columnStr = columnStr + "," + field.Name;
                }
            }

            return columnStr;
        }

        public bool InsertObject(object dbObject, string[] exclude)
        {
            try
            {

                Type myType = dbObject.GetType();

                //獲得物件各屬性欄位
                FieldInfo[] fields = myType.GetFields();

                //建立SQL語法
                bool first = true;
                string columnString = "";
                string valueString = "";
                foreach (FieldInfo field in fields)
                {
                    if (exclude.Contains(field.Name))
                    {
                        continue;
                    }

                    if (first)
                    {
                        columnString = columnString + field.Name;
                        if (Type.GetTypeCode(field.FieldType) == TypeCode.String)
                        {
                            valueString = valueString + "'" + field.GetValue(dbObject) + "'";
                        }
                        else
                        {
                            valueString = valueString + field.GetValue(dbObject);
                        }
                      
                        first = false;
                    }
                    else
                    {
                        columnString = columnString + "," + field.Name;
                        if (Type.GetTypeCode(field.FieldType) == TypeCode.String)
                        {
                            valueString = valueString + "," + "'" + field.GetValue(dbObject) + "'";
                        }
                        else
                        {
                            valueString = valueString + "," + field.GetValue(dbObject);
                        }
                    }
                }

                //連結DB
                connect.ConnectionString = dataSource;
                connect.Open();

                //獲得表名
                string tableName = GetTableName(myType.FullName);

                //輸入SQL語法
                OleDbCommand command = connect.CreateCommand();
                command.CommandText = string.Format("INSERT INTO {0} ({1}) VALUES ({2})", tableName, columnString, valueString);

                command.ExecuteNonQuery(); 
            }
            catch (System.Exception ex)
            {
                MessageBox.Show("UpdateObject()..." + ex.Message);
                return false;
            }
            finally
            {
                connect.Close();
            }
            return true;
        }

        public bool UpdateObject(object dbObject, string criteria, string[] exclude)
        {
            try
            {

                Type myType = dbObject.GetType();

                //獲得物件各屬性欄位
                FieldInfo[] fields = myType.GetFields();

                //建立SQL語法
                criteria = (criteria == null) ? null : " WHERE " + criteria;

                bool first = true;
                string updateString = "";
                foreach (FieldInfo field in fields)
                {
                    if (exclude.Contains(field.Name))
                    {
                        continue;
                    }

                    if (first)
                    {
                        if (Type.GetTypeCode(field.FieldType) == TypeCode.String)
                        {
                            updateString = updateString + string.Format("{0}='{1}'", field.Name, field.GetValue(dbObject));
                        }
                        else
                        {
                            updateString = updateString + string.Format("{0}={1}", field.Name, field.GetValue(dbObject));
                        }

                        first = false;
                    }
                    else
                    {
                        if (Type.GetTypeCode(field.FieldType) == TypeCode.String)
                        {
                            updateString = updateString +", "+ string.Format("{0}='{1}'", field.Name, field.GetValue(dbObject));
                        }
                        else
                        {
                            updateString = updateString + ", " + string.Format("{0}={1}", field.Name, field.GetValue(dbObject));
                        }
                    }
                }

                //連結DB
                connect.ConnectionString = dataSource;
                connect.Open();

                //獲得表名
                string tableName = GetTableName(myType.FullName);

                //輸入SQL語法
                OleDbCommand command = connect.CreateCommand();
                command.CommandText = string.Format("UPDATE {0} SET {1} {2}", tableName, updateString, criteria);

                command.ExecuteNonQuery();
            }
            catch (System.Exception ex)
            {
                MessageBox.Show("UpdateObject()..." + ex.Message);
                return false;
            }
            finally
            {
                connect.Close();
            }
            return true;
        }

        public int QueryObjectCount(Type myType)
        {
            int count = 0;

            if (myType == null)
            {
                return count;
            }

            try
            {
                //獲得物件各屬性欄位
                FieldInfo[] fields = myType.GetFields();

                //連結DB
                connect.ConnectionString = dataSource;
                connect.Open();

                //獲得表名
                string tableName = GetTableName(myType.FullName);

                //輸入SQL語法
                OleDbCommand command = connect.CreateCommand();
                command.CommandText = string.Format("SELECT COUNT ({0}) FROM {1}", fields[0].Name,tableName);

                //執行
                OleDbDataReader reader = command.ExecuteReader();
                reader.Read();
                count = Convert.ToInt32(reader.GetValue(0));
            }
            catch (System.Exception ex)
            {
                MessageBox.Show(ex.Message);
            }
            finally
            {
                connect.Close();
            }

            return count;
        }

        public ArrayList QueryDistinctObject(Type myType, string column, string criteria)
        {
            ArrayList objList = new ArrayList();

            if (myType == null)
            {
                return objList;
            }

            try
            {
                //獲得物件各屬性欄位
                FieldInfo[] fields = myType.GetFields();

                //連結DB
                connect.ConnectionString = dataSource;
                connect.Open();

                //獲得表名
                string tableName = GetTableName(myType.FullName);

                //建立SQL語法
                criteria = (criteria == null) ? null : " WHERE " + criteria;

                //輸入SQL語法
                OleDbCommand command = connect.CreateCommand();
                command.CommandText = string.Format("SELECT DISTINCT {0} FROM {1} {2}", column, tableName, criteria);

                OleDbDataReader reader = command.ExecuteReader();

                //用reader讀取每個ROW
                while (reader.Read())
                {
                    objList.Add(reader.GetValue(0));
                }
            }
            catch (System.Exception ex)
            {
                MessageBox.Show(ex.Message);
            }
            finally
            {
                connect.Close();
            }
            return objList;
        }

        public bool DeleteObject(Type myType, string criteria)
        {
            try
            {
                //連結DB
                connect.ConnectionString = dataSource;
                connect.Open();

                //建立SQL語法
                criteria = (criteria == null) ? null : " WHERE " + criteria;

                //獲得表名
                string tableName = GetTableName(myType.FullName);

                //輸入SQL語法
                OleDbCommand command = connect.CreateCommand();
                command.CommandText = string.Format("DELETE FROM {0} {1}", tableName, criteria);

                command.ExecuteNonQuery();
            }
            catch (System.Exception ex)
            {
                MessageBox.Show(ex.Message);
                return false;
            }
            finally
            {
                connect.Close();
            }
            return true;
        }



















        public int GetDbIdentity()
        {
            int count = 0;
            try
            {
                //連結DB
                connect.ConnectionString = dataSource;
                connect.Open();
                /*
                //建立SQL語法
                criteria = (criteria == null) ? null : " WHERE " + criteria;
                //獲得表名
                string tableName = GetTableName(myType.FullName);
                */
                //輸入SQL語法
                OleDbCommand command = connect.CreateCommand();
                command.CommandText = string.Format("select @@identity");

                //執行
                OleDbDataReader reader = command.ExecuteReader();
                reader.Read();
                count = Convert.ToInt32(reader.GetValue(0));
            }
            catch (System.Exception ex)
            {
                MessageBox.Show(ex.Message);
            }
            finally
            {
                connect.Close();
            }
            return count;
        }
        //資料庫連線
        public static OleDbConnection OleDbOpenConn(string mdbName)
        {
            try
            {
                //mdb存在目錄
                string dllPath = System.AppDomain.CurrentDomain.BaseDirectory;
                //mdb全路徑
                string connectString = string.Format("Provider=Microsoft.Jet.OLEDB.4.0;Data Source=" + dllPath + mdbName);
                //mdb connect物件
                OleDbConnection oleDbConnect = new OleDbConnection();
                oleDbConnect.ConnectionString = connectString;
                oleDbConnect.Open();

                return oleDbConnect;
            }
            catch (System.Exception ex)
            {
                MessageBox.Show(ex.Message);
                return null;
            }
        }
        //取得資料表
        public static DataTable GetOleDbDataTable(string Database, string OleDbString)
        {
            DataTable myDataTable = new DataTable();
            OleDbConnection icn = OleDbOpenConn(Database);
            OleDbDataAdapter da = new OleDbDataAdapter(OleDbString, icn);
            DataSet ds = new DataSet();
            ds.Clear();
            da.Fill(ds);
            myDataTable = ds.Tables[0];
            if (icn.State == ConnectionState.Open) icn.Close();
            return myDataTable;
        }
        //對資料表進行新增、修改及刪除等功能
        public static void OleDbInsertUpdateDelete(string Database, string OleDbSelectString)
        {
            string cnstr = string.Format("Provider=Microsoft.Jet.OLEDB.4.0;Data Source=" + Database);
            OleDbConnection icn = OleDbOpenConn(cnstr);
            OleDbCommand cmd = new OleDbCommand(OleDbSelectString, icn);
            cmd.ExecuteNonQuery();
            if (icn.State == ConnectionState.Open) icn.Close();
        }
    }
}