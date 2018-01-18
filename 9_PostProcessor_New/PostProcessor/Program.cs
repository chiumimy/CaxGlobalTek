using System;
using NXOpen;
using NXOpen.UF;
using CaxGlobaltek;
using System.Windows.Forms;
using PostProcessor;
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
            

            //string a = string.Format(@"{0}\{1}", CaxEnv.GetGlobaltekPostProcessorDir(), "template_post.dat");
            //string[] b = System.IO.File.ReadAllLines(a);
            //CaxLog.ShowListingWindow(b.Length.ToString());
            //for (int i = 0; i < b.Length; i++ )
            //{
            //    if (i>6)
            //    {
            //        CaxLog.ShowListingWindow(b[i]);
            //    }
            //}
            bool status;

            #region (註解中)檢查template_post.dat是否存在

//             status = CaxCheckDat.CheckTemplatePostDat();
//             if (!status)
//             {
//                 MessageBox.Show("template_post.dat不存在");
//                 return retValue;
//             }

            #endregion

            Application.EnableVisualStyles();
            PostProcessorDlg cPostProcessorDlg = new PostProcessorDlg();
            FormUtilities.ReparentForm(cPostProcessorDlg);
            System.Windows.Forms.Application.Run(cPostProcessorDlg);
            cPostProcessorDlg.Dispose();

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

