using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using Newtonsoft.Json;
using Iesi.Collections.Generic;

namespace CaxGlobaltek
{
    public class PECreateData
    {
        public virtual string cusName { get; set; }
        public virtual string partName { get; set; }
        public virtual string partDesc { get; set; }
        public virtual string material { get; set; }
        public virtual string cusRev { get; set; }
        public virtual string opRev { get; set; }
        public virtual string partFilePath { get; set; }
        public virtual string billetFilePath { get; set; }
        public virtual string head_tail { get; set; }
        public virtual string removeNum { get; set; }
        public virtual string erp { get; set; }
        public virtual string materialResource { get; set; }
        public virtual List<Operation> listOperation { get; set; }
        public virtual List<string> oper1Ary { get; set; }
        public virtual List<string> oper2Ary { get; set; }

        //public string PartPath { get; set; }
    }

    #region OperationArray JSON 結構

    public class OperationArray
    {
        public List<string> Operation1Array { get; set; }
        public List<string> Operation2Array { get; set; }
    }

    #endregion

    public class Operation
    {
        public string Oper1 { get; set; }
        public string Oper2 { get; set; }
        public string ERPCode { get; set; }
        public string Form { get; set; }
        //public string CAMFolderPath { get; set; }
        //public string OISFolderPath { get; set; }
        //public string ThridOperPartPath { get; set; }
    }


    #region 客戶名稱 JSON 結構

    public class CusName
    {
        public List<string> CustomerName { get; set; }
    }

    #endregion

    #region PE相關程式碼

    public class CaxPE
    {
        private static string PE_ConfigVari = "PE_Config";

        /// <summary>
        /// 回傳：IP\Globaltek\PE\Config
        /// </summary>
        /// <returns></returns>
        public static string GetPEConfigDir()
        {
            string PEConfigPath = Environment.GetEnvironmentVariable(PE_ConfigVari);
            return PEConfigPath;
        }

        /// <summary>
        /// 取得每一筆料號的製程資料
        /// </summary>
        /// <param name="cPECreateData"></param>
        /// <returns></returns>
        public static bool ReadPECreateData(string jsonPath, out PECreateData cPECreateData)
        {
            cPECreateData = new PECreateData();
            try
            {
                if (!System.IO.File.Exists(jsonPath))
                {
                    return false;
                }

                bool status;

                string jsonText;
                status = ReadFileDataUTF8(jsonPath, out jsonText);
                if (!status)
                {
                    return false;
                }

                cPECreateData = JsonConvert.DeserializeObject<PECreateData>(jsonText);
            }
            catch (System.Exception ex)
            {
                return false;
            }

            return true;
        }

        public static bool ReadCustomerNameData(string jsonPath, out CusName cCusName)
        {
            cCusName = null;

            try
            {
                if (!System.IO.File.Exists(jsonPath))
                {
                    return false;
                }

                bool status;

                string jsonText;
                status = ReadFileDataUTF8(jsonPath, out jsonText);
                if (!status)
                {
                    return false;
                }

                cCusName = JsonConvert.DeserializeObject<CusName>(jsonText);
            }
            catch (System.Exception ex)
            {
                return false;
            }

            return true;
        }

        public static bool ReadOperationArrayData(string jsonPath, out OperationArray cOperationArray)
        {
            cOperationArray = null;

            try
            {
                if (!System.IO.File.Exists(jsonPath))
                {
                    return false;
                }

                bool status;

                string jsonText;
                status = ReadFileDataUTF8(jsonPath, out jsonText);
                if (!status)
                {
                    return false;
                }

                cOperationArray = JsonConvert.DeserializeObject<OperationArray>(jsonText);
            }
            catch (System.Exception ex)
            {
                return false;
            }

            return true;
        }

        public static bool ReadFileDataUTF8(string file_path, out string allContent)
        {
            allContent = "";

            if (!System.IO.File.Exists(file_path))
            {
                return false;
            }

            string line;
            System.IO.StreamReader file = new System.IO.StreamReader(file_path, Encoding.UTF8);

            int index = 0;
            while ((line = file.ReadLine()) != null)
            {
                if (index == 0)
                {
                    allContent += line;
                }
                else
                {
                    allContent += "\n";
                    allContent += line;
                }
                index++;
            }
            file.Close();

            return true;
        }
    }

    #endregion
    
}
