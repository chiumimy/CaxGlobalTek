using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using NXOpen;
using NXOpen.UF;
using System.Collections;
using NXOpen.Utilities;
using System.Windows.Forms;

namespace CaxGlobaltek
{
    public class CaxPart
    {
        public static Session theSession = Session.GetSession();

        public static bool SaveAll()
        {
            try
            {
                bool anyPartsModified1;
                PartSaveStatus partSaveStatus1;
                theSession.Parts.SaveAll(out anyPartsModified1, out partSaveStatus1);

                partSaveStatus1.Dispose();
            }
            catch (System.Exception ex)
            {
                return false;
            }
            return true;
        }
        public static bool Save(BasePart.SaveComponents saveComponentParts = NXOpen.BasePart.SaveComponents.True, BasePart.CloseAfterSave close = NXOpen.BasePart.CloseAfterSave.False)
        {
            try
            {
                //Part workPart = theSession.Parts.Work;
                Part displayPart = theSession.Parts.Display;
                // ----------------------------------------------
                //   Menu: File->Save
                // ----------------------------------------------
                PartSaveStatus partSaveStatus1;
                partSaveStatus1 = displayPart.Save(saveComponentParts, close);
                partSaveStatus1.Dispose();
            }
            catch (System.Exception ex)
            {
                MessageBox.Show(ex.ToString());
                return false;
            }

            return true;
        }

        /// <summary>
        /// 開啟選擇的檔案
        /// </summary>
        /// <param name="partPath"></param>
        /// <param name="newPart"></param>
        /// <returns></returns>
        public static bool OpenBaseDisplay(string partPath, out BasePart newPart)
        {
            newPart = null;

            try
            {
                PartLoadStatus partLoadStatus;
                newPart = theSession.Parts.OpenBaseDisplay(partPath, out partLoadStatus);
                partLoadStatus.Dispose();
            }
            catch (System.Exception ex)
            {
                //theSession.ListingWindow.Open();
                //theSession.ListingWindow.WriteLine(ex.Message);
                return false;
            }
            return true;
        }

        /// <summary>
        /// 開啟選擇的檔案
        /// </summary>
        /// <param name="partPath"></param>
        /// <returns></returns>
        public static bool OpenBaseDisplay(string partPath)
        {
            try
            {
                PartLoadStatus partLoadStatus;
                theSession.Parts.OpenBaseDisplay(partPath, out partLoadStatus);
                partLoadStatus.Dispose();
            }
            catch (System.Exception ex)
            {
                //theSession.ListingWindow.Open();
                //theSession.ListingWindow.WriteLine(ex.Message);
                return false;
            }
            return true;
        }

        /// <summary>
        /// 關閉所有檔案
        /// </summary>
        /// <param name="closeModified"></param>
        /// <param name="responses"></param>
        /// <returns></returns>
        public static bool CloseAllParts(BasePart.CloseModified closeModified = NXOpen.BasePart.CloseModified.CloseModified, PartCloseResponses responses = null)
        {
            try
            {
                //Session theSession = Session.GetSession();
                //Part workPart = theSession.Parts.Work;
                //Part displayPart = theSession.Parts.Display;
                // ----------------------------------------------
                //   Menu: File->Close->All Parts
                // ----------------------------------------------
                theSession.Parts.CloseAll(closeModified, responses);

                //workPart = null;
                //displayPart = null;
            }
            catch (System.Exception ex)
            {
                return false;
            }

            return true;
        }

        public static bool ClosePartMemory(string partName)
        {
            try
            {
                Session theSession = Session.GetSession();
                Part workPart = theSession.Parts.Work;
                Part displayPart = theSession.Parts.Display;
                // ----------------------------------------------
                //   Menu: File->Close->Selected Parts...
                // ----------------------------------------------
                //NXOpen.Session.UndoMarkId markId1;
                //markId1 = theSession.SetUndoMark(NXOpen.Session.MarkVisibility.Visible, "Start");

                //theSession.SetUndoMarkName(markId1, "Close Part Dialog");

                //NXOpen.Session.UndoMarkId markId2;
                //markId2 = theSession.SetUndoMark(NXOpen.Session.MarkVisibility.Invisible, "Close Part");

                //theSession.DeleteUndoMark(markId2, null);

                //theSession.SetUndoMarkName(markId1, "Close Part");

                Part part1 = (Part)theSession.Parts.FindObject(partName);
                part1.Close(NXOpen.BasePart.CloseWholeTree.False, NXOpen.BasePart.CloseModified.UseResponses, null);

                // ----------------------------------------------
                //   Menu: Tools->Journal->Stop Recording
                // ----------------------------------------------
            }
            catch (System.Exception ex)
            {
                return false;
            }
            return true;
        }
    }
}
