using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace WorldChampion
{
    public class CommonFunction
    {
        //字串轉換成Unicode
        public static string StringToUnicodeWithMoney(string normalString)
        {
            string unicodeString = "";
            char[] stringCharAry = normalString.ToCharArray();
            for (int i = 0; i < stringCharAry.Length; i++)
            {
                //取得字元Unicode編碼的兩個位元組(0-255)
                byte[] charUnicodeBytes = Encoding.Unicode.GetBytes(stringCharAry[i].ToString());
                //BIG ENDIAN方式
                string unicodeChar = "$" + charUnicodeBytes[1].ToString("X2") + charUnicodeBytes[0].ToString("X2");
                unicodeString += unicodeChar;
            }
            return unicodeString;
        }

        //Unicode轉換成字串
        public static string UnicodeWithMoneyToString(string unicodeString)
        {
            string originalString = "";

            //先取得編碼前的字元數
            int len = unicodeString.Length / 5;
            //以字元數為處理次數
            for (int i = 0; i <= len - 1; i++)
            {
                //取得單一編碼
                string singleUnicode = "";
                //取得部分編碼，同時去掉開頭的$字元
                singleUnicode = unicodeString.Substring(0, 5).Substring(1);
                //砍掉處理過的編碼
                unicodeString = unicodeString.Substring(5);
                //取得編碼的位元組
                byte[] bytes = new byte[2];
                //BIG ENDIAN編碼方式
                bytes[1] = byte.Parse(int.Parse(singleUnicode.Substring(0, 2), System.Globalization.NumberStyles.HexNumber).ToString());
                bytes[0] = byte.Parse(int.Parse(singleUnicode.Substring(2, 2), System.Globalization.NumberStyles.HexNumber).ToString());
                originalString += Encoding.Unicode.GetString(bytes);
            }
            return originalString;
        }

        //使用winrar.exe解壓縮方法
        public static bool ExtractFileByWinrar(string exeFilePath,string winrarExePath, string aimFolder, string specificFile=" ")
        {
            try
            {

                System.Diagnostics.Process Process1 = new System.Diagnostics.Process();
                Process1.StartInfo.FileName = winrarExePath;
                Process1.StartInfo.CreateNoWindow = true;

                Process1.StartInfo.Arguments = string.Format(" x -o+ {0} {1} {2} ", exeFilePath, specificFile, aimFolder);

                Process1.Start();
                if (Process1.HasExited)
                {
                    int iExitCode = Process1.ExitCode;
                    if (iExitCode == 0)
                    {
                        System.Windows.Forms.MessageBox.Show(iExitCode.ToString() + " 正常完成");
                    }
                    else
                    {
                        System.Windows.Forms.MessageBox.Show(iExitCode.ToString() + " 有錯完成");
                    }
                }


                #region 範例
                //string strtxtPath = "C:\\freezip\\free.txt";
                //string strzipPath = "C:\\freezip\\free.zip";
                //System.Diagnostics.Process Process1 = new System.Diagnostics.Process();
                //Process1.StartInfo.FileName = "Winrar.exe";
                //Process1.StartInfo.CreateNoWindow = true;

                //// 1
                ////壓縮c:\freezip\free.txt(即文件夾及其下文件freezip\free.txt)
                ////到c:\freezip\free.rar
                //strzipPath = "C:\\freezip\\free";//默認壓縮方式為 .rar
                //Process1.StartInfo.Arguments = " a -r " + strzipPath + " " + strtxtPath;

                //// 2
                ////壓縮c:\freezip\free.txt(即文件夾及其下文件freezip\free.txt)
                ////到c:\freezip\free.rar
                //strzipPath = "C:\\freezip\\free";//設置壓縮方式為 .zip
                //Process1.StartInfo.Arguments = " a -afzip " + strzipPath + " " + strtxtPath;

                //// 3
                ////壓縮c:\freezip\free.txt(即文件夾及其下文件freezip\free.txt)
                ////到c:\freezip\free.zip  直接設定為free.zip
                //Process1.StartInfo.Arguments = " a -r "+strzipPath+" " + strtxtPath ;

                //// 4
                ////搬遷壓縮c:\freezip\free.txt(即文件夾及其下文件freezip\free.txt)
                ////到c:\freezip\free.rar 壓縮後 原文件將不存在
                //Process1.StartInfo.Arguments = " m " + strzipPath + " " + strtxtPath;

                //// 5
                ////壓縮c:\freezip\下的free.txt(即文件free.txt)
                ////到c:\freezip\free.zip  直接設定為free.zip 只有文件 而沒有文件夾
                //Process1.StartInfo.Arguments = " a -ep " + strzipPath + " " + strtxtPath;

                //// 6
                ////解壓縮c:\freezip\free.rar
                ////到 c:\freezip\
                //strtxtPath = "c:\\freezip\\";
                //Process1.StartInfo.Arguments = " x " + strzipPath + " " + strtxtPath;

                //// 7
                ////加密壓縮c:\freezip\free.txt(即文件夾及其下文件freezip\free.txt)
                ////到c:\freezip\free.zip  密碼為123456 注意參數間不要空格
                //Process1.StartInfo.Arguments = " a -p123456 " + strzipPath + " " + strtxtPath;

                //// 8
                ////解壓縮加密的c:\freezip\free.rar
                ////到 c:\freezip\   密碼為123456 注意參數間不要空格
                //strtxtPath = "c:\\freezip\\";
                //Process1.StartInfo.Arguments = " x -p123456 " + strzipPath + " " + strtxtPath;

                //// 9 
                ////-o+ 覆蓋 已經存在的文件
                //// -o- 不覆蓋 已經存在的文件
                //strtxtPath = "c:\\freezip\\";
                //Process1.StartInfo.Arguments = " x -o+ " + strzipPath + " " + strtxtPath;

                ////10
                //// 只從指定的zip中
                //// 解壓出free1.txt
                //// 到指定路徑下
                //// 壓縮包中的其他文件 不予解壓
                //strtxtPath = "c:\\freezip\\";
                //Process1.StartInfo.Arguments = " x " + strzipPath + " " +" free1.txt" + " " + strtxtPath;

                //// 11
                //// 通過 -y 對所有詢問回應為"是" 以便 即便發生錯誤 也不彈出WINRAR的窗口
                //// -cl 轉換文件名為小寫字母 
                //strtxtPath = "c:\\freezip\\";
                //Process1.StartInfo.Arguments = " t -y -cl " + strzipPath + " " + " free1.txt";

                //Process1.Start();
                //if (Process1.HasExited)
                //{
                //    int iExitCode = Process1.ExitCode;
                //    if (iExitCode == 0)
                //    {
                //        Response.Write(iExitCode.ToString() + " 正常完成");
                //    }
                //    else
                //    {
                //        Response.Write(iExitCode.ToString() + " 有錯完成");
                //    }
                //}
                #endregion


             
            }
            catch (System.Exception ex)
            {
                System.Windows.Forms.MessageBox.Show(ex.Message);
                return false;
            }
            return true;
        }

        //取得當前時間編碼
        public static string EncryptDateTimeNowTicks()
        {
            string encryptStr = "";
            try
            {
                encryptStr = DateTime.Now.Ticks.ToString();
            }
            catch (System.Exception ex)
            {
                return "";
            }
            return encryptStr;
        }

        //加密
        public static string SystemEncryptByKey(string toEncrypt, string key)
        {
            while (16 > key.Length)
            {
                key += "_";
            }

            byte[] keyArray = UTF8Encoding.UTF8.GetBytes(key);
            byte[] toEncryptArray = UTF8Encoding.UTF8.GetBytes(toEncrypt);
            System.Security.Cryptography.RijndaelManaged rDel = new System.Security.Cryptography.RijndaelManaged();
            rDel.Key = keyArray;
            rDel.Mode = System.Security.Cryptography.CipherMode.ECB;
            rDel.Padding = System.Security.Cryptography.PaddingMode.PKCS7;
            System.Security.Cryptography.ICryptoTransform cTransform = rDel.CreateEncryptor();
            byte[] resultArray = cTransform.TransformFinalBlock(toEncryptArray, 0, toEncryptArray.Length);
            return Convert.ToBase64String(resultArray, 0, resultArray.Length);
        }
        //解密
        public static string SystemDecryptByKey(string toDecrypt, string key)
        {
            while (16 > key.Length)
            {
                key += "_";
            }

            byte[] keyArray = UTF8Encoding.UTF8.GetBytes(key);
            byte[] toEncryptArray = Convert.FromBase64String(toDecrypt);

            System.Security.Cryptography.RijndaelManaged rDel = new System.Security.Cryptography.RijndaelManaged();
            rDel.Key = keyArray;
            rDel.Mode = System.Security.Cryptography.CipherMode.ECB;
            rDel.Padding = System.Security.Cryptography.PaddingMode.PKCS7;

            System.Security.Cryptography.ICryptoTransform cTransform = rDel.CreateDecryptor();
            byte[] resultArray = cTransform.TransformFinalBlock(toEncryptArray, 0, toEncryptArray.Length);

            return UTF8Encoding.UTF8.GetString(resultArray);
        }

        //讀取文檔內容
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

        //角度轉換弧度
        public static double DegreeToRadian(double degree)
        {
            return Math.PI * degree / 180.0;
        }
    }
}
