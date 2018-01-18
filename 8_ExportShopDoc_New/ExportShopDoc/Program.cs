using System;
using NXOpen;
using NXOpen.UF;
using ExportShopDoc;
using CaxGlobaltek;
using System.Windows.Forms;
using NXOpenUI;
using System.IO;
using System.Text;

public class Program
{
    // class members
    private static Session theSession;
    private static UI theUI;
    private static UFSession theUfSession;
    public static Program theProgram;
    public static bool isDisposeCalled;

    //------------------------------------------------------------------------------
    // Constructor
    //------------------------------------------------------------------------------
    public Program()
    {
        try
        {
            theSession = Session.GetSession();
            theUI = UI.GetUI();
            theUfSession = UFSession.GetUFSession();
            isDisposeCalled = false;
        }
        catch (NXOpen.NXException ex)
        {
            // ---- Enter your exception handling code here -----
            // UI.GetUI().NXMessageBox.Show("Message", NXMessageBox.DialogType.Error, ex.Message);
        }
    }

    //------------------------------------------------------------------------------
    //  Explicit Activation
    //      This entry point is used to activate the application explicitly
    //------------------------------------------------------------------------------
    public static int Main(string[] args)
    {
        int retValue = 0;
        try
        {
            theProgram = new Program();

            //TODO: Add your application code here 

            bool status;
            //StreamReader _read = new StreamReader(string.Format(@"D:\O5566"));
            //string getStr = _read.ReadToEnd();
            //MessageBox.Show(Encoding.UTF8.GetByteCount(getStr).ToString());

            //return 0;
            #region 檢查METEDownload_Upload.dat是否存在
            /*
            status = CaxCheckDat.CheckMETEDownload_Upload();
            if (!status)
            {
                MessageBox.Show("METEDownload_Upload.dat不存在");
                return retValue;
            }
            */
            #endregion

            #region 關閉 Preferences->Manufacturing->Operation->Regresh before Each Path

            //NXOpen.CAM.Preferences preferences1;
            //preferences1 = theSession.CAMSession.CreateCamPreferences();
            //preferences1.ReplayRefreshBeforeEachPath = false;
            //preferences1.Commit();
            //preferences1.Destroy();

            #endregion

            int module_id;
            theUfSession.UF.AskApplicationModule(out module_id);
            if (module_id != UFConstants.UF_APP_CAM)
            {
                MessageBox.Show("請先轉換為加工模組後再執行！");
                return retValue;
            }
            
            Application.EnableVisualStyles();
            ExportShopDocDlg cExportShopDocDlg = new ExportShopDocDlg();
            FormUtilities.ReparentForm(cExportShopDocDlg);
            System.Windows.Forms.Application.Run(cExportShopDocDlg);
            cExportShopDocDlg.Dispose();

            //cExportShopDocDlg.ShowDialog();

            theProgram.Dispose();
        }
        catch (NXOpen.NXException ex)
        {
            // ---- Enter your exception handling code here -----

        }
        return retValue;
    }

    //------------------------------------------------------------------------------
    // Following method disposes all the class members
    //------------------------------------------------------------------------------
    public void Dispose()
    {
        try
        {
            if (isDisposeCalled == false)
            {
                //TODO: Add your application code here 
            }
            isDisposeCalled = true;
        }
        catch (NXOpen.NXException ex)
        {
            // ---- Enter your exception handling code here -----

        }
    }

    public static int GetUnloadOption(string arg)
    {
        //Unloads the image explicitly, via an unload dialog
        //return System.Convert.ToInt32(Session.LibraryUnloadOption.Explicitly);

        //Unloads the image immediately after execution within NX
        return System.Convert.ToInt32(Session.LibraryUnloadOption.Immediately);

        //Unloads the image when the NX session terminates
        // return System.Convert.ToInt32(Session.LibraryUnloadOption.AtTermination);
    }

}

