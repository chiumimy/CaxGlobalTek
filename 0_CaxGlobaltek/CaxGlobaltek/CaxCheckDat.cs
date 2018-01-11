using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Windows.Forms;

namespace CaxGlobaltek
{
    public class CaxCheckDat
    {
        /// <summary>
        /// 檢查METEDownload_Upload.dat是否存在
        /// </summary>
        /// <returns></returns>
        public static bool CheckMETEDownload_Upload()
        {
            try
            {
                string METEDownload_Upload_dat = "METEDownload_Upload_New.dat";
                string METEDownload_Upload_Path = string.Format(@"{0}\{1}", CaxEnv.GetGlobaltekEnvDir(), METEDownload_Upload_dat);
                if (!System.IO.File.Exists(METEDownload_Upload_Path))
                {
                    MessageBox.Show("METEDownload_Upload_New.dat不存在");
                    return false;
                }
            }
            catch (System.Exception ex)
            {
                return false;
            }
            return true;
        }

        /// <summary>
        /// 檢查CustomerName.dat是否存在
        /// </summary>
        /// <returns></returns>
        public static bool CheckCustomerName()
        {
            try
            {
                string CustomerName_dat = "CustomerName.dat";
                string CustomerNameDatPath = string.Format(@"{0}\{1}", CaxPE.GetPEConfigDir(), CustomerName_dat);
                if (!System.IO.File.Exists(CustomerNameDatPath))
                {
                    MessageBox.Show("CustomerName.dat不存在");
                    return false;
                }
            }
            catch (System.Exception ex)
            {
                return false;
            }
            return true;
        }

        /// <summary>
        /// 檢查OperationArray.dat是否存在
        /// </summary>
        /// <returns></returns>
        public static bool CheckOperationArray()
        {
            try
            {
                string OperationArray_dat = "OperationArray.dat";
                string OperationArrayDatPath = string.Format(@"{0}\{1}", CaxPE.GetPEConfigDir(), OperationArray_dat);
                if (!System.IO.File.Exists(OperationArrayDatPath))
                {
                    MessageBox.Show("OperationArray.dat不存在");
                    return false;
                }
            }
            catch (System.Exception ex)
            {
                return false;
            }
            return true;
        }

        /// <summary>
        /// 檢查METEDownloadData.dat是否存在
        /// </summary>
        /// <returns></returns>
        public static bool CheckMETEDownloadData()
        {
            try
            {
                string METEDownloadData_dat = "METEDownloadData.dat";
                string METEDownloadData_Path = string.Format(@"{0}\{1}", CaxEnv.GetGlobaltekTaskDir(), METEDownloadData_dat);
                if (!System.IO.File.Exists(METEDownloadData_Path))
                {
                    MessageBox.Show("METEDownloadData.dat不存在");
                    return false;
                }
            }
            catch (System.Exception ex)
            {
                return false;
            }
            return true;
        }

        /// <summary>
        /// 檢查template_post.dat是否存在
        /// </summary>
        /// <returns></returns>
        public static bool CheckTemplatePostDat()
        {
            try
            {
                string TemplatePost_dat = "template_post.dat";
                string TemplatePostData_Path = string.Format(@"{0}\{1}", CaxEnv.GetGlobaltekPostProcessorDir(), TemplatePost_dat);
                if (!System.IO.File.Exists(TemplatePostData_Path))
                {
                    MessageBox.Show("template_post.dat不存在");
                    return false;
                }
            }
            catch (System.Exception ex)
            {
                return false;
            }
            return true;
        }

        /// <summary>
        /// 檢查ControlerConfig.dat是否存在  (路徑：IP:Globaltek\TE_Config\ControlerConfig.dat)
        /// </summary>
        /// <returns></returns>
        public static bool CheckControlerConfigDat()
        {
            try
            {
                string ControlerConfig_dat = "ControlerConfig.dat";
                string ControlerConfig_Path = string.Format(@"{0}\{1}\{2}{3}", CaxEnv.GetGlobaltekEnvDir(), "TE_Config", "Config", ControlerConfig_dat);
                if (!System.IO.File.Exists(ControlerConfig_Path))
                {
                    MessageBox.Show("ControlerConfig.dat不存在");
                    return false;
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
