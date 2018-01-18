using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using CaxGlobaltek;
using NXOpen;
using NXOpen.UF;

namespace FixInsUpload
{
    public class Function
    {
        private static Session theSession = Session.GetSession();
        private static UI theUI;
        private static UFSession theUfSession = UFSession.GetUFSession();

        public static bool GetCom_PEMain(string[] splitFullPath, out Com_PEMain comPEMain)
        {
            comPEMain = new Com_PEMain(); 
            try
            {
                comPEMain = FixInsUploadDlg.session.QueryOver<Com_PEMain>().Where(x => x.partName == splitFullPath[4]).Where(x => x.customerVer == splitFullPath[5]).Where(x => x.opVer == splitFullPath[6]).SingleOrDefault();
                if (comPEMain == null)
                {
                    System.Windows.Forms.MessageBox.Show("資料庫無此料號，無法上傳");
                    return false;
                }
            }
            catch (System.Exception ex)
            {
                return false;
            }
            return true;
        }

        public static bool GetCom_PartOperation(string op1, Com_PEMain comPEMain, out Com_PartOperation comPartOperation)
        {
            comPartOperation = new Com_PartOperation();
            try
            {
                comPartOperation = FixInsUploadDlg.session.QueryOver<Com_PartOperation>().Where(x => x.comPEMain.peSrNo == comPEMain.peSrNo).Where(x => x.operation1 == op1).SingleOrDefault<Com_PartOperation>();
                if (comPartOperation == null)
                {
                    System.Windows.Forms.MessageBox.Show("資料庫無此製程序，無法上傳");
                    return false;
                }
            }
            catch (System.Exception ex)
            {
                return false;
            }
            return true;
        }

        public static bool GetWorkPartAttribute(Part workPart, out CaxME.WorkPartAttribute sWorkPartAttribute)
        {
            sWorkPartAttribute = new CaxME.WorkPartAttribute();
            try
            {
                try
                {
                    sWorkPartAttribute.draftingVer = workPart.GetStringAttribute("REVSTARTPOS");
                    sWorkPartAttribute.draftingVer = sWorkPartAttribute.draftingVer.Split(',')[0];
                }
                catch (System.Exception ex) { sWorkPartAttribute.draftingVer = ""; }

                try { sWorkPartAttribute.partDescription = workPart.GetStringAttribute("PARTDESCRIPTIONPOS"); }
                catch (System.Exception ex) { sWorkPartAttribute.partDescription = ""; }

                try 
                { 
                    sWorkPartAttribute.draftingDate = workPart.GetStringAttribute("REVDATESTARTPOS");
                    sWorkPartAttribute.draftingDate = sWorkPartAttribute.draftingDate.Split(',')[0];
                }
                catch (System.Exception ex) { sWorkPartAttribute.draftingDate = ""; }

                try { sWorkPartAttribute.material = workPart.GetStringAttribute("MATERIALPOS"); }
                catch (System.Exception ex) { sWorkPartAttribute.material = ""; }

                sWorkPartAttribute.createDate = DateTime.Now.ToString();
            }
            catch (System.Exception ex)
            {
                return false;
            }
            return true;
        }
    }
}
