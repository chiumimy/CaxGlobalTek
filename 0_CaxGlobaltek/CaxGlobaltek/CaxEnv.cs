using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace CaxGlobaltek
{
    public class CaxEnv
    {
        private static string ServerEnvVari = "Globaltek_Server_Env";
        private static string ServerEnvTaskVari = "Globaltek_Server_Env_Task";
        private static string LocalEnvTaskVari = "Globaltek_Local_Env_Task";
        private static string ServerPostProcessorVari = "PostProcessor";

        /// <summary>
        /// 回傳：IP\Globaltek
        /// </summary>
        /// <returns></returns>
        public static string GetGlobaltekEnvDir()
        {
            string GlobaltekEnvDir = Environment.GetEnvironmentVariable(ServerEnvVari);
            return GlobaltekEnvDir;
        }

        /// <summary>
        /// 回傳：IP\Globaltek\Task
        /// </summary>
        /// <returns></returns>
        public static string GetGlobaltekTaskDir()
        {
            string GlobaltekEnvTaskDir = Environment.GetEnvironmentVariable(ServerEnvTaskVari);
            return GlobaltekEnvTaskDir;
        }

        /// <summary>
        /// 回傳：[Local]\Globaltek\Task
        /// </summary>
        /// <returns></returns>
        public static string GetLocalTaskDir()
        {
            string LocalTaskDir = Environment.GetEnvironmentVariable(LocalEnvTaskVari);
            return LocalTaskDir;
        }

        /// <summary>
        /// 回傳：IP\Globaltek\MACH\resource\postprocessor
        /// </summary>
        /// <returns></returns>
        public static string GetGlobaltekPostProcessorDir()
        {
            string GlobaltekPostProcessorDir = Environment.GetEnvironmentVariable(ServerPostProcessorVari);
            return GlobaltekPostProcessorDir;
        }
    }
}
