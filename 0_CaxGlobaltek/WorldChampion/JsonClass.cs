using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

using Newtonsoft.Json;

namespace WorldChampion
{
    public class JsonClass
    {
        #region  Json讀取寫出
        //讀取JSON文檔
        public static bool ReadJsonData<T>(string JsonStyleLoadPath, out T JsonRead)
        {
            JsonRead = default(T);
            try
            {
                bool status = false;
                //檢查檔案是否存在
                if (!System.IO.File.Exists(JsonStyleLoadPath))
                {
                    return false;
                }

                string jsonText = "";
                status = CommonFunction.ReadFileDataUTF8(JsonStyleLoadPath, out jsonText);
                if (!status)
                {
                    //UI.GetUI().NXMessageBox.Show("Message", NXMessageBox.DialogType.Error, "配置檔讀取失敗：" + JsonStyleLoadPath);
                    return false;
                }
                JsonRead = JsonConvert.DeserializeObject<T>(jsonText);
            }
            catch (System.Exception ex)
            {
                return false;
            }
            return true;
        }

        //寫出JSON文檔(
        public static bool WriteJsonFileData(string jsonPath, object jsonObject)
        {
            try
            {
                System.IO.File.WriteAllText(jsonPath, JsonConvert.SerializeObject(jsonObject, Formatting.Indented));
            }
            catch (System.Exception ex)
            {
                return false;
            }
            return true;
        }

        //寫出JSON文檔
        public static bool WriteJsonFileData(string jsonPath, object jsonObject, Formatting formatting)
        {
            try
            {
                System.IO.File.WriteAllText(jsonPath, JsonConvert.SerializeObject(jsonObject, formatting));
            }
            catch (System.Exception ex)
            {
                return false;
            }
            return true;
        }


        #endregion
    }

}
