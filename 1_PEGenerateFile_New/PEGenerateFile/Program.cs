using System;
using NXOpen;
using NXOpen.UF;
using PEGenerateFile;
using System.Windows.Forms;
using CaxGlobaltek;
using NXOpenUI;

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

            
            //取得CustomerName配置檔
            //string CustomerName_dat = "CustomerName.dat";
            //string CustomerNameDatPath = string.Format(@"{0}\{1}", CaxPE.GetPEConfigDir(), CustomerName_dat);
            //status = CaxCheckDat.CheckCustomerName();
            //if (!status)
            //{
            //    MessageBox.Show("取得CustomerNameDatPath失敗");
            //    return retValue;
            //}
            
            //取得OperationArray配置檔
            //string OperationArray_dat = "OperationArray.dat";
            //string OperationArrayDatPath = string.Format(@"{0}\{1}", CaxPE.GetPEConfigDir(), OperationArray_dat);
            //status = CaxCheckDat.CheckOperationArray();
            //if (!status)
            //{
            //    MessageBox.Show("取得OperationArrayDatPath失敗");
            //    return retValue;
            //}

            Application.EnableVisualStyles();
            PEGenerateDlg cPEGenerateDlg = new PEGenerateDlg();
            FormUtilities.ReparentForm(cPEGenerateDlg);
            System.Windows.Forms.Application.Run(cPEGenerateDlg);
            cPEGenerateDlg.Dispose();

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

