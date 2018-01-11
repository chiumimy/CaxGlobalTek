using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Text;

namespace WorldChampion
{
    public class CommonLogFile
    {
        private string logFilePath = null;
        private bool isPrintTime = true;
        private string processName = null;
        private bool isPrintProceeName = true;

        #region =====構造函數=====
        public CommonLogFile()
        {}
        public CommonLogFile(string _logFilePath, bool _isPrintTime = true, bool _isPrintProceeName = false, bool _isOverWrite = false)
        {
            logFilePath = _logFilePath;
            isPrintTime = _isPrintTime;

            if (_isOverWrite)
            {
                if (File.Exists(logFilePath))
                {
                    try
                    {
                        File.Delete(logFilePath);
                    }
                    catch
                    { }
                }
            }
        }
        #endregion

        #region =====輸出log=====
        public void StatusOut(string messageFormat, params object[] args)
        {
            //確認目錄存在
            string logFileDirectory = Path.GetDirectoryName(logFilePath);
            if (!Directory.Exists(logFileDirectory))
            {
                Directory.CreateDirectory(logFileDirectory);
            }

            StreamWriter sw = new StreamWriter(logFilePath, true);
            string message = string.Format(messageFormat, args);

            try
            {
                //if (isSendLogMessage)
                //{
                //    SendLogMessage(message);
                //}
                string PreMessage = "";
                //#region =====custom premessage=====
                //if (isPrintMoldName)
                //{
                //    PreMessage += mold_name;
                //}
                if (isPrintTime)
                {
                    PreMessage += System.DateTime.Now.ToString() + " ";
                }
                //#endregion

                //message
                message = PreMessage + message;
                sw.WriteLine(message);
                sw.Close();
            }
            catch
            {
                sw.Close();
            }

            Console.WriteLine(message);
        }
        #endregion

        #region =====Set=====
        public void SetLogFile(string _logFilePath,  bool _isPrintTime = true,bool _isPrintProcessName = false,bool _isOverWrite = false)
        {
            logFilePath = _logFilePath;
            isPrintTime = _isPrintTime;
            isPrintProceeName = _isPrintProcessName;
            //isSendLogMessage = is_send_log_message;
            //messageSendToWndName = msg_send_to_wnd_name;

            if (System.Diagnostics.Process.GetCurrentProcess().MainModule.ModuleName != "")
            {
                processName = System.Diagnostics.Process.GetCurrentProcess().MainModule.ModuleName + " ";
            }
            else
            {
                processName = System.Reflection.Assembly.GetExecutingAssembly().GetName().Name + " ";
            }

            if (!File.Exists(logFilePath))
            {
                File.Create(logFilePath).Close();
            }

            if (_isOverWrite)
            {
                if (File.Exists(logFilePath))
                {
                    try
                    {
                        File.Delete(logFilePath);
                    }
                    catch
                    { }
                }
            }

        }
        #endregion

    }
}
