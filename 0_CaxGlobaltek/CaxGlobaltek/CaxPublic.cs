using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using Newtonsoft.Json;
using System.Windows.Forms;
using NXOpen;
using System.IO;
using System.Text.RegularExpressions;
using DevComponents.DotNetBar;
using NHibernate;

namespace CaxGlobaltek
{
    #region METE_Download_Upload Path Json 結構

    public class METE_Download_Upload_Path
    {
        public string Server_IP { get; set; }
        public string Server_ShareStr { get; set; }
        public string Server_Folder_MODEL { get; set; }
        public string Server_Folder_CAM { get; set; }
        public string Server_Folder_OIS { get; set; }
        public string Server_MEDownloadPart { get; set; }
        public string Server_TEDownloadPart { get; set; }
        public string Server_ShopDoc { get; set; }
        public string Server_IPQC { get; set; }
        public string Server_SelfCheck { get; set; }
        public string Server_IQC { get; set; }
        public string Server_FAI { get; set; }
        public string Server_FQC { get; set; }
        public string Local_IP { get; set; }
        public string Local_ShareStr { get; set; }
        public string Local_Folder_MODEL { get; set; }
        public string Local_Folder_CAM { get; set; }
        public string Local_Folder_OIS { get; set; }
    }

    #endregion

    #region METEDownloadData Json 結構

    public class CusRev
    {
        public string RevNo { get; set; }
        public List<string> OperAry1 { get; set; }
        public List<string> OperAry2 { get; set; }
    }

    public class CusPart
    {
        public string PartNo { get; set; }
        public List<CusRev> CusRev { get; set; }
    }

    public class EntirePartAry
    {
        public string CusName { get; set; }
        public List<CusPart> CusPart { get; set; }
    }

    public class METEDownloadData
    {
        public List<EntirePartAry> EntirePartAry { get; set; }
    }

    #endregion

    #region 控制器MasterValue Json 結構

    public class Controler
    {
        public string CompanyName { get; set; }
        public string MasterValue1 { get; set; }
        public string MasterValue2 { get; set; }
        public string MasterValue3 { get; set; }
    }

    public class ControlerConfig
    {
        public List<Controler> Controler { get; set; }
    }

    #endregion

    #region DraftingConfig Json 結構

    public class Drafting
    {
        public string SheetSize { get; set; }
        public string PartDescriptionPosText { get; set; }
        public string PartDescriptionPos { get; set; }
        public string PartNumberPosText { get; set; }
        public string PartNumberPos { get; set; }
        public string RevStartPosText { get; set; }
        public string RevStartPos { get; set; }
        public string PartUnitPosText { get; set; }
        public string PartUnitPos { get; set; }
        public string RevRowHeight { get; set; }
        public string PartDescriptionFontSize { get; set; }
        public string PartNumberFontSize { get; set; }
        public string RevFontSize { get; set; }
        public string PartUnitFontSize { get; set; }
        public string ERPcodePosText { get; set; }
        public string ERPcodePos { get; set; }
        public string ERPRevPosText { get; set; }
        public string ERPRevPos { get; set; }
        public string TolTitle0PosText { get; set; }
        public string TolTitle0Pos { get; set; }
        public string TolTitle1PosText { get; set; }
        public string TolTitle1Pos { get; set; }
        public string TolTitle2PosText { get; set; }
        public string TolTitle2Pos { get; set; }
        public string TolTitle3PosText { get; set; }
        public string TolTitle3Pos { get; set; }
        public string TolTitle4PosText { get; set; }
        public string TolTitle4Pos { get; set; }
        public string AngleTitlePosText { get; set; }
        public string AngleTitlePos { get; set; }
        public string TolValue0PosText { get; set; }
        public string TolValue0Pos { get; set; }
        public string TolValue1PosText { get; set; }
        public string TolValue1Pos { get; set; }
        public string TolValue2PosText { get; set; }
        public string TolValue2Pos { get; set; }
        public string TolValue3PosText { get; set; }
        public string TolValue3Pos { get; set; }
        public string TolValue4PosText { get; set; }
        public string TolValue4Pos { get; set; }
        public string AngleValuePosText { get; set; }
        public string AngleValuePos { get; set; }
        public string TolFontSize { get; set; }
        public string RevDateStartPosText { get; set; }
        public string RevDateStartPos { get; set; }
        public string AuthDatePosText { get; set; }
        public string AuthDatePos { get; set; }
        public string MaterialPosText { get; set; }
        public string MaterialPos { get; set; }
        public string RevDateFontSize { get; set; }
        public string AuthDateFontSize { get; set; }
        public string MaterialFontSize { get; set; }
        public string MatMinFontSize { get; set; }
        public string PartNumberWidth { get; set; }
        public string PartDescriptionWidth { get; set; }
        public string MaterialWidth { get; set; }
        public string ProcNamePosText { get; set; }
        public string ProcNamePos { get; set; }
        public string ProcNameFontSize { get; set; }
        public string PageNumberPosText { get; set; }
        public string PageNumberPos { get; set; }
        public string PageNumberFontSize { get; set; }
        public string SecondPartNumberPosText { get; set; }
        public string SecondPartNumberPos { get; set; }
        public string SecondPageNumberPosText { get; set; }
        public string SecondPageNumberPos { get; set; }
        public string SecondPartNumberWidth { get; set; }
        //製圖.校對.審核.說明.製圖審核
        public string PreparedPosText { get; set; }
        public string PreparedPos { get; set; }
        public string ReviewedPosText { get; set; }
        public string ReviewedPos { get; set; }
        public string ApprovedPosText { get; set; }
        public string ApprovedPos { get; set; }
        public string InstructionPosText { get; set; }
        public string InstructionPos { get; set; }
        public string InstApprovedPosText { get; set; }
        public string InstApprovedPos { get; set; }
    }

    public class DraftingConfig
    {
        public List<Drafting> Drafting { get; set; }
    }

    #endregion

    #region test
    public class RegionX
    {
        public string Zone { get; set; }
        public string X0 { get; set; }
        public string X1 { get; set; }
    }

    public class RegionY
    {
        public string Zone { get; set; }
        public string Y0 { get; set; }
        public string Y1 { get; set; }
    }

    public class DraftingCoordinate
    {
        public string SheetSize { get; set; }
        public List<RegionX> RegionX { get; set; }
        public List<RegionY> RegionY { get; set; }
    }

    public class CoordinateData
    {
        public List<DraftingCoordinate> DraftingCoordinate { get; set; }
    }
    #endregion

    #region PartInfo 取得客戶、料號、客戶版次、製程序資料

    public struct PartInfo
    {
        public string CusName { get; set; }
        public string PartNo { get; set; }
        public string CusRev { get; set; }
        public string OpRev { get; set; }
        public string OpNum { get; set; }
    }

    #endregion

    public class CaxPublic
    {
        public static bool ReadMETEDownloadData(string jsonPath, out METEDownloadData cOperationArray)
        {
            cOperationArray = null;

            try
            {
                if (!System.IO.File.Exists(jsonPath))
                {
                    return false;
                }

                bool status;

                string jsonText;
                status = ReadFileDataUTF8(jsonPath, out jsonText);
                if (!status)
                {
                    return false;
                }

                cOperationArray = JsonConvert.DeserializeObject<METEDownloadData>(jsonText);
            }
            catch (System.Exception ex)
            {
                return false;
            }

            return true;
        }

        public static bool ReadMETEDownloadUpload_Path(string jsonPath, out METE_Download_Upload_Path cOperationArray)
        {
            cOperationArray = null;

            try
            {
                if (!System.IO.File.Exists(jsonPath))
                {
                    return false;
                }

                bool status;

                string jsonText;
                status = ReadFileDataUTF8(jsonPath, out jsonText);
                if (!status)
                {
                    return false;
                }

                cOperationArray = JsonConvert.DeserializeObject<METE_Download_Upload_Path>(jsonText);
            }
            catch (System.Exception ex)
            {
                return false;
            }

            return true;
        }

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

        public static bool ReadControlerConfig(string jsonPath, out ControlerConfig cControlerConfig)
        {
            cControlerConfig = null;
            try
            {
                if (!System.IO.File.Exists(jsonPath))
                {
                    return false;
                }

                bool status;

                string jsonText;
                status = ReadFileDataUTF8(jsonPath, out jsonText);
                if (!status)
                {
                    return false;
                }

                cControlerConfig = JsonConvert.DeserializeObject<ControlerConfig>(jsonText);
            }
            catch (System.Exception ex)
            {
                return false;
            }
            return true;
        }

        public static bool ReadDraftingConfig(string jsonPath, out DraftingConfig cDraftingConfig)
        {
            cDraftingConfig = null;
            try
            {
                if (!System.IO.File.Exists(jsonPath))
                {
                    return false;
                }

                bool status;

                string jsonText;
                status = ReadFileDataUTF8(jsonPath, out jsonText);
                if (!status)
                {
                    return false;
                }

                cDraftingConfig = JsonConvert.DeserializeObject<DraftingConfig>(jsonText);
            }
            catch (System.Exception ex)
            {
                return false;
            }
            return true;
        }

        /// <summary>
        /// 單選，fileName=(檔名+副檔名)，filePath=(路徑+檔名+副檔名)，defaultPath=預設開啟路徑，filter=預設為Part檔，使用者可自行定義，
        /// </summary>
        /// <param name="fileName"></param>
        /// <param name="filePath"></param>
        /// <param name="defaultPath"></param>
        /// <param name="filter"></param>
        /// <returns></returns>
        public static bool OpenFileDialog(out string fileName, out string filePath, string defaultPath = "", string filter = "Part Files (*.prt)|*.prt|All Files (*.*)|*.*")
        {
            fileName = "";
            filePath = "";
            try
            {
                OpenFileDialog cOpenFileDialog = new OpenFileDialog();
                cOpenFileDialog.InitialDirectory = defaultPath;
                cOpenFileDialog.Filter = filter;
                DialogResult result = cOpenFileDialog.ShowDialog();
                if (result == DialogResult.OK)
                {
                    //取得檔案名稱(檔名+副檔名)
                    fileName = cOpenFileDialog.SafeFileName;
                    //取得檔案完整路徑(路徑+檔名+副檔名)
                    filePath = cOpenFileDialog.FileName;

                    //MessageBox.Show(textPartFileName.Text);
                }
            }
            catch (System.Exception ex)
            {
                return false;
            }
            return true;
        }
        /// <summary>
        /// 多選，fileName=(檔名+副檔名)，filePath=(路徑+檔名+副檔名)，defaultPath=預設開啟路徑，filter=預設為Part檔，使用者可自行定義，
        /// </summary>
        /// <param name="fileName"></param>
        /// <param name="filePath"></param>
        /// <param name="defaultPath"></param>
        /// <param name="filter"></param>
        /// <returns></returns>
        public static bool OpenFilesDialog(out string[] fileName, out string[] filePath, string defaultPath = "", string filter = "Part Files (*.prt)|*.prt|All Files (*.*)|*.*")
        {
            fileName = new string[]{};
            filePath = new string[] { };
            try
            {
                OpenFileDialog cOpenFileDialog = new OpenFileDialog();
                cOpenFileDialog.Multiselect = true;
                cOpenFileDialog.InitialDirectory = defaultPath;
                cOpenFileDialog.Filter = filter;
                DialogResult result = cOpenFileDialog.ShowDialog();
                if (result == DialogResult.OK)
                {
                    //取得檔案名稱(檔名+副檔名)
                    fileName = cOpenFileDialog.SafeFileNames;
                    //取得檔案完整路徑(路徑+檔名+副檔名)
                    filePath = cOpenFileDialog.FileNames;

                    //MessageBox.Show(textPartFileName.Text);
                }
            }
            catch (System.Exception ex)
            {
                return false;
            }
            return true;
        }
        public static bool SaveFileDialog(string initialDire, out string filePath)
        {
            filePath = "";
            try
            {
                SaveFileDialog cSaveFileDialog = new SaveFileDialog();
                cSaveFileDialog.Filter = "Excel Files (*.xls)|*.xls";
                //cSaveFileDialog.InitialDirectory = initialDire;
                cSaveFileDialog.FileName = initialDire;
                DialogResult result = cSaveFileDialog.ShowDialog();
                if (result == DialogResult.OK)
                {
                    filePath = cSaveFileDialog.FileName;
                }
                else
                {
                    filePath = initialDire;
                }
            }
            catch (System.Exception ex)
            {
                return false;
            }
            return true;
        }

        /// <summary>
        /// 多選物件
        /// </summary>
        /// <param name="objary"></param>
        /// <returns></returns>
        public static bool SelectObjects(out NXObject[] objary)
        {
            objary = new NXObject[] { };
            try
            {
                UI theUI = UI.GetUI();
                objary = new NXObject[] { };
                theUI.SelectionManager.SelectObjects("Select Object", "Select Object", Selection.SelectionScope.AnyInAssembly, true, false, out objary);
            }
            catch (System.Exception ex)
            {
                return false;
            }
            return true;
        }

        /// <summary>
        /// 刪除指定的Object
        /// </summary>
        /// <param name="Nxobject"></param>
        /// <returns></returns>
        public static bool DelectObject(NXObject Nxobject)
        {
            try
            {
                Session theSession = Session.GetSession();
                Part workPart = theSession.Parts.Work;
                Part displayPart = theSession.Parts.Display;
                // ----------------------------------------------
                //   Menu: Edit->Delete...
                // ----------------------------------------------
                NXOpen.Session.UndoMarkId markId1;
                markId1 = theSession.SetUndoMark(NXOpen.Session.MarkVisibility.Invisible, "Delete");

                bool notifyOnDelete1;
                notifyOnDelete1 = theSession.Preferences.Modeling.NotifyOnDelete;

                theSession.UpdateManager.ClearErrorList();

                NXOpen.Session.UndoMarkId markId2;
                markId2 = theSession.SetUndoMark(NXOpen.Session.MarkVisibility.Visible, "Delete");

                NXObject[] objects1 = new NXObject[1];
                UI theUI = UI.GetUI();

                objects1[0] = Nxobject;
                int nErrs1;
                nErrs1 = theSession.UpdateManager.AddToDeleteList(objects1);

                bool notifyOnDelete2;
                notifyOnDelete2 = theSession.Preferences.Modeling.NotifyOnDelete;

                int nErrs2;
                nErrs2 = theSession.UpdateManager.DoUpdate(markId2);

                theSession.DeleteUndoMark(markId1, null);
            }
            catch (System.Exception ex)
            {
                return false;
            }

            return true;
        }

        /// <summary>
        /// 取得圖紙各區域座標
        /// </summary>
        /// <param name="jsonPath"></param>
        /// <param name="cCoordinateData"></param>
        /// <returns></returns>
        public static bool ReadCoordinateData(string jsonPath, out CoordinateData cCoordinateData)
        {
            cCoordinateData = null;
            try
            {
                if (!System.IO.File.Exists(jsonPath))
                {
                    return false;
                }

                bool status;

                string jsonText;
                status = ReadFileDataUTF8(jsonPath, out jsonText);
                if (!status)
                {
                    return false;
                }

                cCoordinateData = JsonConvert.DeserializeObject<CoordinateData>(jsonText);
            }
            catch (System.Exception ex)
            {
                return false;
            }
            return true;
        }

        /// <summary>
        /// 取得dat每一行的資料
        /// </summary>
        /// <param name="TxtPath">Dat的路徑</param>
        /// <param name="IndexToRead">想要從哪一行開始抓取</param>
        /// <param name="TxtData">抓取的資料</param>
        /// <returns></returns>
        public static bool ReadFileData(string DatPath, int IndexToRead, out string[] DatData)
        {
            DatData = new string[] { };
            try
            {
                List<string> tempDataStr = new List<string>();
                string[] TemplatePostData = System.IO.File.ReadAllLines(DatPath);
                if (TemplatePostData.Length == 0)
                {
                    CaxLog.ShowListingWindow(Path.GetFileName(DatPath) + "資料為空，請檢查");
                    DatData = new string[] { };
                    return false;
                }
                DatData = new string[TemplatePostData.Length];
                for (int i = 0; i < TemplatePostData.Length; i++)
                {
                    if (i >= IndexToRead)
                    {
                        tempDataStr.Add(TemplatePostData[i]);
                    }
                }
                DatData = tempDataStr.ToArray();
            }
            catch (System.Exception ex)
            {
                DatData = new string[] { };
                return false;
            }
            return true;
        }

        /// <summary>
        /// 複製目錄到指定路徑，參數copySubDirs為是否複製子目錄內的檔案
        /// </summary>
        /// <param name="sourceDirName">原始目錄路徑</param>
        /// <param name="destDirName">目的地目錄路徑</param>
        /// <param name="copySubDirs">是否進行遞迴複製子目錄內的檔案</param>
        /// <returns></returns>
        public static bool DirectoryCopy(string sourceDirName, string destDirName, bool copySubDirs)
        {
            try
            {
                // Get the subdirectories for the specified directory.
                DirectoryInfo dir = new DirectoryInfo(sourceDirName);

                if (!dir.Exists)
                {
                    throw new DirectoryNotFoundException(
                        "Source directory does not exist or could not be found: "
                        + sourceDirName);
                }

                DirectoryInfo[] dirs = dir.GetDirectories();
                // If the destination directory doesn't exist, create it.
                if (!Directory.Exists(destDirName))
                {
                    Directory.CreateDirectory(destDirName);
                }

                // Get the files in the directory and copy them to the new location.
                FileInfo[] files = dir.GetFiles();
                foreach (FileInfo file in files)
                {
                    string temppath = Path.Combine(destDirName, file.Name);
                    file.CopyTo(temppath, true);
                }

                // If copying subdirectories, copy them and their contents to new location.
                if (copySubDirs)
                {
                    foreach (DirectoryInfo subdir in dirs)
                    {
                        string temppath = Path.Combine(destDirName, subdir.Name);
                        DirectoryCopy(subdir.FullName, temppath, copySubDirs);
                    }
                }
            }
            catch (System.Exception ex)
            {
                return false;
            }
            return true;
        }

        /// <summary>
        /// 取得此料號所有資料的Server&Local路徑
        /// </summary>
        /// <param name="user">當前的工程師(輸入ME或TE)</param>
        /// <param name="displayPartFullPath">此料號的全路徑</param>
        /// <param name="cMETE_Download_Upload_Path">輸出路徑</param>
        /// <returns></returns>
        public static bool GetAllPath(string user, string displayPartFullPath, out PartInfo sPartInfo, ref METE_Download_Upload_Path cMETE_Download_Upload_Path)
        {
            sPartInfo = new PartInfo();
            try
            {
                bool status = SplitPartPath(displayPartFullPath, out sPartInfo);
                if (!status)
                {
                    return false;
                }

                if (user == "ME")
                {
                    sPartInfo.OpNum = Path.GetFileNameWithoutExtension(displayPartFullPath).Split(new string[] { "OIS" }, StringSplitOptions.RemoveEmptyEntries)[1];
                }
                else if (user == "TE")
                {
                    sPartInfo.OpNum = Regex.Replace(Path.GetFileNameWithoutExtension(displayPartFullPath).Split('_')[1], "[^0-9]", "");
                }

                //Server路徑
                cMETE_Download_Upload_Path.Server_ShareStr = cMETE_Download_Upload_Path.Server_ShareStr.Replace("[Server_IP]", cMETE_Download_Upload_Path.Server_IP);
                cMETE_Download_Upload_Path.Server_ShareStr = cMETE_Download_Upload_Path.Server_ShareStr.Replace("[CusName]", sPartInfo.CusName);
                cMETE_Download_Upload_Path.Server_ShareStr = cMETE_Download_Upload_Path.Server_ShareStr.Replace("[PartNo]", sPartInfo.PartNo);
                cMETE_Download_Upload_Path.Server_ShareStr = cMETE_Download_Upload_Path.Server_ShareStr.Replace("[CusRev]", sPartInfo.CusRev);
                cMETE_Download_Upload_Path.Server_ShareStr = cMETE_Download_Upload_Path.Server_ShareStr.Replace("[OpRev]", sPartInfo.OpRev);
                cMETE_Download_Upload_Path.Server_Folder_MODEL = cMETE_Download_Upload_Path.Server_Folder_MODEL.Replace("[Server_ShareStr]", cMETE_Download_Upload_Path.Server_ShareStr);
                cMETE_Download_Upload_Path.Server_Folder_CAM = cMETE_Download_Upload_Path.Server_Folder_CAM.Replace("[Server_ShareStr]", cMETE_Download_Upload_Path.Server_ShareStr);
                cMETE_Download_Upload_Path.Server_Folder_CAM = cMETE_Download_Upload_Path.Server_Folder_CAM.Replace("[Oper1]", sPartInfo.OpNum);
                cMETE_Download_Upload_Path.Server_Folder_OIS = cMETE_Download_Upload_Path.Server_Folder_OIS.Replace("[Server_ShareStr]", cMETE_Download_Upload_Path.Server_ShareStr);
                cMETE_Download_Upload_Path.Server_Folder_OIS = cMETE_Download_Upload_Path.Server_Folder_OIS.Replace("[Oper1]", sPartInfo.OpNum);
                cMETE_Download_Upload_Path.Server_MEDownloadPart = cMETE_Download_Upload_Path.Server_MEDownloadPart.Replace("[Server_ShareStr]", cMETE_Download_Upload_Path.Server_ShareStr);
                cMETE_Download_Upload_Path.Server_MEDownloadPart = cMETE_Download_Upload_Path.Server_MEDownloadPart.Replace("[PartNo]", sPartInfo.PartNo);
                cMETE_Download_Upload_Path.Server_MEDownloadPart = cMETE_Download_Upload_Path.Server_MEDownloadPart.Replace("[Oper1]", sPartInfo.OpNum);
                cMETE_Download_Upload_Path.Server_TEDownloadPart = cMETE_Download_Upload_Path.Server_TEDownloadPart.Replace("[Server_ShareStr]", cMETE_Download_Upload_Path.Server_ShareStr);
                cMETE_Download_Upload_Path.Server_TEDownloadPart = cMETE_Download_Upload_Path.Server_TEDownloadPart.Replace("[PartNo]", sPartInfo.PartNo);
                cMETE_Download_Upload_Path.Server_TEDownloadPart = cMETE_Download_Upload_Path.Server_TEDownloadPart.Replace("[Oper1]", sPartInfo.OpNum);
                cMETE_Download_Upload_Path.Server_ShopDoc = cMETE_Download_Upload_Path.Server_ShopDoc.Replace("[Server_IP]", cMETE_Download_Upload_Path.Server_IP);
                cMETE_Download_Upload_Path.Server_IPQC = cMETE_Download_Upload_Path.Server_IPQC.Replace("[Server_IP]", cMETE_Download_Upload_Path.Server_IP);
                cMETE_Download_Upload_Path.Server_SelfCheck = cMETE_Download_Upload_Path.Server_SelfCheck.Replace("[Server_IP]", cMETE_Download_Upload_Path.Server_IP);
                cMETE_Download_Upload_Path.Server_IQC = cMETE_Download_Upload_Path.Server_IQC.Replace("[Server_IP]", cMETE_Download_Upload_Path.Server_IP);
                cMETE_Download_Upload_Path.Server_FAI = cMETE_Download_Upload_Path.Server_FAI.Replace("[Server_IP]", cMETE_Download_Upload_Path.Server_IP);
                cMETE_Download_Upload_Path.Server_FQC = cMETE_Download_Upload_Path.Server_FQC.Replace("[Server_IP]", cMETE_Download_Upload_Path.Server_IP);
                //Local路徑
                cMETE_Download_Upload_Path.Local_ShareStr = cMETE_Download_Upload_Path.Local_ShareStr.Replace("[Local_IP]", cMETE_Download_Upload_Path.Local_IP);
                cMETE_Download_Upload_Path.Local_ShareStr = cMETE_Download_Upload_Path.Local_ShareStr.Replace("[CusName]", sPartInfo.CusName);
                cMETE_Download_Upload_Path.Local_ShareStr = cMETE_Download_Upload_Path.Local_ShareStr.Replace("[PartNo]", sPartInfo.PartNo);
                cMETE_Download_Upload_Path.Local_ShareStr = cMETE_Download_Upload_Path.Local_ShareStr.Replace("[CusRev]", sPartInfo.CusRev);
                cMETE_Download_Upload_Path.Local_ShareStr = cMETE_Download_Upload_Path.Local_ShareStr.Replace("[OpRev]", sPartInfo.OpRev);
                cMETE_Download_Upload_Path.Local_Folder_MODEL = cMETE_Download_Upload_Path.Local_Folder_MODEL.Replace("[Local_ShareStr]", cMETE_Download_Upload_Path.Local_ShareStr);

                if (user == "ME")
                {
                    cMETE_Download_Upload_Path.Local_Folder_OIS = cMETE_Download_Upload_Path.Local_Folder_OIS.Replace("[Local_ShareStr]", cMETE_Download_Upload_Path.Local_ShareStr);
                    cMETE_Download_Upload_Path.Local_Folder_OIS = cMETE_Download_Upload_Path.Local_Folder_OIS.Replace("[Oper1]", sPartInfo.OpNum);
                }
                else if (user == "TE")
                {
                    cMETE_Download_Upload_Path.Local_Folder_CAM = cMETE_Download_Upload_Path.Local_Folder_CAM.Replace("[Local_ShareStr]", cMETE_Download_Upload_Path.Local_ShareStr);
                    cMETE_Download_Upload_Path.Local_Folder_CAM = cMETE_Download_Upload_Path.Local_Folder_CAM.Replace("[Oper1]", sPartInfo.OpNum);
                }
            }
            catch (System.Exception ex)
            {
                return false;
            }
            return true;
        }

        /// <summary>
        /// 適用於所有Json檔案的讀取
        /// </summary>
        /// <typeparam name="T"></typeparam>
        /// <param name="JsonDatLoadPath"></param>
        /// <param name="JsonRead"></param>
        /// <returns></returns>
        public static bool ReadJsonData<T>(string JsonDatLoadPath, out T JsonRead)
        {
            JsonRead = default(T);
            try
            {
                bool status = false;
                if (!System.IO.File.Exists(JsonDatLoadPath))
                {
                    return false;
                }
                string jsonText = "";
                status = ReadFileDataUTF8(JsonDatLoadPath, out jsonText);
                if (!status)
                {
                    return false;
                }
                JsonRead = JsonConvert.DeserializeObject<T>(jsonText);
            }
            catch (System.Exception ex)
            {
                return false;
            }
            return true;
        }

        /// <summary>
        /// 跳出Yes、No的對話框
        /// </summary>
        /// <param name="dialogText"></param>
        /// <param name="dialogIcon"></param>
        /// <param name="dialogTitle"></param>
        /// <param name="dialogHeader"></param>
        /// <returns></returns>
        public static eTaskDialogResult ShowMsgYesNo(string dialogText, eTaskDialogIcon dialogIcon = eTaskDialogIcon.Help, string dialogTitle = "Message", string dialogHeader = "")
        {
            eTaskDialogButton dialogButtons = eTaskDialogButton.Yes | eTaskDialogButton.No;
            eTaskDialogBackgroundColor dialogColor = eTaskDialogBackgroundColor.Silver;
            eTaskDialogResult result = TaskDialog.Show(dialogTitle, dialogIcon, dialogHeader, dialogText, dialogButtons, dialogColor);
            return result;
        }
        
        /// <summary>
        /// 拆解DisplayPart的全路徑，取得客戶名稱、料號、客戶版次、製程版次
        /// </summary>
        /// <param name="partFullPath"></param>
        /// <param name="sPartInfo"></param>
        /// <returns></returns>
        public static bool SplitPartPath(string partFullPath, out PartInfo sPartInfo)
        {
            sPartInfo = new PartInfo();
            try
            {
                string[] SplitPath = partFullPath.Split('\\');
                
                sPartInfo.CusName = SplitPath[3];
                sPartInfo.PartNo = SplitPath[4];
                sPartInfo.CusRev = SplitPath[5];
                sPartInfo.OpRev = SplitPath[6];
            }
            catch (System.Exception ex)
            {
            	return false;
            }
            return true;
        }

        /// <summary>
        /// License檢查
        /// </summary>
        /// <returns></returns>
        public static bool CheckLicense()
        {
            try
            {
                ISession session = MyHibernateHelper.SessionFactory.OpenSession();
                Sys_License sysLicense = session.QueryOver<Sys_License>().SingleOrDefault();
                string expireTime = Crypt.Crypt.Decrypt(sysLicense.expireTime, sysLicense.password);
                if (Convert.ToInt32(DateTime.Now.ToString("yyyyMMdd")) > Convert.ToInt32(expireTime))
                    return false;
            }
            catch (System.Exception ex)
            {
                return false;
            }
            return true;
        }

        public static bool ReadTempFileData(string jsonPath, out PECreateData cPECreateData)
        {
            cPECreateData = new PECreateData();
            try
            {
                if (!System.IO.File.Exists(jsonPath))
                {
                    return false;
                }

                bool status;

                string jsonText;
                status = ReadFileDataUTF8(jsonPath, out jsonText);
                if (!status)
                {
                    return false;
                }

                cPECreateData = JsonConvert.DeserializeObject<PECreateData>(jsonText);
            }
            catch (System.Exception ex)
            {
                return false;
            }
            return true;
        }

        
    }
}
