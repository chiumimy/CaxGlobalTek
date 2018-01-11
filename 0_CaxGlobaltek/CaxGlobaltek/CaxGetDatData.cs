using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Windows.Forms;


namespace CaxGlobaltek
{
    

    public class CaxGetDatData
    {
        /// <summary>
        /// 取得METEDownload_Upload.dat資料
        /// </summary>
        /// <param name="cMETE_Download_Upload_Path"></param>
        /// <returns></returns>
        public static bool GetMETEDownload_Upload(out METE_Download_Upload_Path cMETE_Download_Upload_Path)
        {
            cMETE_Download_Upload_Path = new METE_Download_Upload_Path();
            try
            {
                string METEDownload_Upload_dat = "METEDownload_Upload_New.dat";
                string METEDownload_Upload_Path = string.Format(@"{0}\{1}", CaxEnv.GetGlobaltekEnvDir(), METEDownload_Upload_dat);
                CaxPublic.ReadMETEDownloadUpload_Path(METEDownload_Upload_Path, out cMETE_Download_Upload_Path);
            }
            catch (System.Exception ex)
            {
                return false;
            }
            return true;
        }

        /// <summary>
        /// 取得METEDownloadData.dat資料
        /// </summary>
        /// <param name="cMETEDownloadData"></param>
        /// <returns></returns>
        public static bool GetMETEDownloadData(out METEDownloadData cMETEDownloadData)
        {
            cMETEDownloadData = new METEDownloadData();
            try
            {
                string METEDownloadDat_dat = "METEDownloadData.dat";
                string METEDownloadDatPath = string.Format(@"{0}\{1}", CaxEnv.GetGlobaltekTaskDir(), METEDownloadDat_dat);
                CaxPublic.ReadMETEDownloadData(METEDownloadDatPath, out cMETEDownloadData);
            }
            catch (System.Exception ex)
            {
                return false;
            }
            return true;
        }

        /// <summary>
        /// 取得template_post.dat內容
        /// </summary>
        /// <returns></returns>
        public static string[] GetTemplatePostData()
        {
            string[] DatData = new string[]{};
            try
            {
                string TemplatePostPath = string.Format(@"{0}\{1}", CaxEnv.GetGlobaltekPostProcessorDir(), "template_post.dat");
                return DatData = System.IO.File.ReadAllLines(TemplatePostPath);
            }
            catch (System.Exception ex)
            {
                return DatData;
            }
        }

        /// <summary>
        /// 取得ControlerConfig.dat資料  (路徑：IP:Globaltek\TE_Config\ControlerConfig.dat)
        /// </summary>
        /// <param name="cControlerConfig"></param>
        /// <returns></returns>
        public static bool GetControlerConfigData(out ControlerConfig cControlerConfig)
        {
            cControlerConfig = new ControlerConfig();
            try
            {
                string ControlerConfig_dat = "ControlerConfig.dat";
                string ControlerConfig_Path = string.Format(@"{0}\{1}\{2}\{3}", CaxEnv.GetGlobaltekEnvDir(), "TE_Config", "Config", ControlerConfig_dat);
                if (!System.IO.File.Exists(ControlerConfig_Path))
                {
                    MessageBox.Show("路徑：" + ControlerConfig_Path + "不存在");
                    return false;
                }

                CaxPublic.ReadControlerConfig(ControlerConfig_Path, out cControlerConfig);
            }
            catch (System.Exception ex)
            {
                return false;
            }
            return true;
        }

        /// <summary>
        /// 取得DraftingConfig.dat資料  (路徑：IP:Globaltek\ME_Config\DraftingConfig.dat)
        /// </summary>
        /// <param name="cControlerConfig"></param>
        /// <returns></returns>
        public static bool GetDraftingConfigData(out DraftingConfig cDraftingData)
        {
            cDraftingData = new DraftingConfig();
            try
            {
                string DraftingConfig_dat = "DraftingConfig.dat";
                string DraftingConfig_Path = string.Format(@"{0}\{1}\{2}\{3}", CaxEnv.GetGlobaltekEnvDir(), "ME_Config", "Config", DraftingConfig_dat);
                if (!System.IO.File.Exists(DraftingConfig_Path))
                {
                    MessageBox.Show("路徑：" + DraftingConfig_Path + "不存在");
                    return false;
                }

                CaxPublic.ReadDraftingConfig(DraftingConfig_Path, out cDraftingData);
            }
            catch (System.Exception ex)
            {
            	return false;
            }
            return true;
        }

        /// <summary>
        /// 取得AssignGaugeData.dat資料  (路徑：IP:Globaltek\ME_Config\AssignGaugeData.dat)
        /// </summary>
        /// <param name="AGtxt"></param>
        /// <returns></returns>
        public static bool GetAssignGaugeData(out string[] AGData)
        {
            AGData = new string[] { };
            try
            {
                string AssignGaugeData_dat = "AssignGaugeData.dat";
                string AssignGaugeData_Path = string.Format(@"{0}\{1}\{2}\{3}", CaxEnv.GetGlobaltekEnvDir(), "ME_Config", "Config", AssignGaugeData_dat);
                AGData = System.IO.File.ReadAllLines(AssignGaugeData_Path);
            }
            catch (System.Exception ex)
            {
                return false;
            }
            return true;
        }

        /// <summary>
        /// 取得圖紙區域資料
        /// </summary>
        /// <param name="cCoordinateData"></param>
        /// <returns></returns>
        public static bool GetDraftingCoordinateData(out CoordinateData cCoordinateData)
        {
            cCoordinateData = new CoordinateData();
            try
            {
                string DraftingCoordinate_dat = "DraftingCoordinate.dat";
                string DraftingCoordinate_Path = string.Format(@"{0}\{1}\{2}\{3}", CaxEnv.GetGlobaltekEnvDir(), "ME_Config", "Config", DraftingCoordinate_dat);
                if (!System.IO.File.Exists(DraftingCoordinate_Path))
                {
                    MessageBox.Show("路徑：" + DraftingCoordinate_Path + "不存在");
                    return false;
                }

                CaxPublic.ReadCoordinateData(DraftingCoordinate_Path, out cCoordinateData);
            }
            catch (System.Exception ex)
            {
                return false;
            }
            return true;
        }

    }
}
