using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Windows.Forms;
using NXOpen;
using NXOpen.UF;
using System.IO;
using CaxGlobaltek;
using NXOpen.Utilities;
using NHibernate;
using DevComponents.DotNetBar;

namespace FixInsUpload
{
    public partial class FixInsUploadDlg : DevComponents.DotNetBar.Office2007Form
    {
        private static Session theSession = Session.GetSession();
        private static UI theUI;
        private static UFSession theUfSession = UFSession.GetUFSession();
        public static Part workPart = theSession.Parts.Work;
        public static Part displayPart = theSession.Parts.Display;
        public static ISession session = MyHibernateHelper.SessionFactory.OpenSession();
        public bool status;
        public string[] PicPathStr, PicNameStr, splitFullPath;
        public string PhotoFolderPath, op1, S_PicPath, S_Folder;

        public FixInsUploadDlg()
        {
            InitializeComponent();
        }

        private void FixInsUploadDlg_Load(object sender, EventArgs e)
        {
            InitializeLabel();
        }

        public void InitializeLabel()
        {
            try
            {
                FixInsNo.Text = workPart.GetStringAttribute("PARTNUMBERPOS");
                ERPNo.Text = workPart.GetStringAttribute("ERPCODEPOS");
                Desc.Text = workPart.GetStringAttribute("PARTDESCRIPTIONPOS");

                //由檔案路徑拆出：料號、客戶版次、製程版次、OP
                splitFullPath = Path.GetDirectoryName(workPart.FullPath).Split('\\');
                op1 = Path.GetFileNameWithoutExtension(workPart.FullPath).Split(new string[] { "OIS" }, StringSplitOptions.RemoveEmptyEntries)[1];
                op1 = op1.Substring(0, 3);

                //建立SERVER圖片目錄
                S_Folder = string.Format(@"{0}\{1}\{2}\{3}\{4}\OP{5}\OIS\{6}", CaxEnv.GetGlobaltekTaskDir(), splitFullPath[3], splitFullPath[4], splitFullPath[5], splitFullPath[6], op1, Path.GetFileNameWithoutExtension(workPart.FullPath));
            }
            catch (System.Exception ex)
            {
                MessageBox.Show("請先執行【檢、治具】使用的PartInformation");
                this.Close();
            }
        }

        private void SelPic_Click(object sender, EventArgs e)
        {
            try
            {

                string PicFilter = "jpg Files (*.jpg)|*.jpg|eps Files (*.eps)|*.eps|gif Files (*.gif)|*.gif|bmp Files (*.bmp)|*.bmp|png Files (*.png)|*.png|All Files (*.*)|*.*";
                status = CaxPublic.OpenFilesDialog(out PicNameStr, out PicPathStr, "", PicFilter);
                if (!status)
                {
                    MessageBox.Show("2D圖選擇失敗");
                    return;
                }
                PhotoFolderPath = string.Format(@"{0}\OP{1}\OIS\{2}", Path.GetDirectoryName(workPart.FullPath), op1, Path.GetFileNameWithoutExtension(workPart.FullPath));
                if (!Directory.Exists(PhotoFolderPath))
                {
                    System.IO.Directory.CreateDirectory(PhotoFolderPath);
                }
                for (int i = 0; i < PicNameStr.Length;i++ )
                {
                    if (PicPath.Text != "")
                    {
                        PicPath.Text = Path.GetFileNameWithoutExtension(PicNameStr[i]) + "、" + PicPath.Text;
                        S_PicPath = S_PicPath + "," + string.Format(@"{0}\{1}", S_Folder, PicNameStr[i]);
                    }
                    else
                    {
                        PicPath.Text = Path.GetFileNameWithoutExtension(PicNameStr[i]);
                        S_PicPath = string.Format(@"{0}\{1}", S_Folder, PicNameStr[i]);
                    }
                }
            }
            catch (System.Exception ex)
            {
                MessageBox.Show(ex.ToString());
            }
        }

        private void Close_Click(object sender, EventArgs e)
        {
            this.Close();
        }

        private void Upload_Click(object sender, EventArgs e)
        {
            try
            {
                if (PicPath.Text == "")
                {
                    MessageBox.Show("請先選擇OIS製程圖片(JPG、PNG)");
                    return;
                }

                //取得WorkPart資訊並檢查資料是否完整
                CaxME.WorkPartAttribute sWorkPartAttribute = new CaxME.WorkPartAttribute();
                status = Function.GetWorkPartAttribute(workPart, out sWorkPartAttribute);
                if (!status)
                {
                    MessageBox.Show("workPart屬性取得失敗，無法上傳");
                    this.Close();
                    return;
                }
                if (sWorkPartAttribute.draftingVer == "" || sWorkPartAttribute.draftingDate == "" ||
                    sWorkPartAttribute.partDescription == "" || sWorkPartAttribute.material == "")
                {
                    MessageBox.Show("量測資訊不足");
                    this.Close();
                    return;
                }

                //取得所有量測尺寸資料
                int SheetCount = 0;
                NXOpen.Tag[] SheetTagAry = null;
                theUfSession.Draw.AskDrawings(out SheetCount, out SheetTagAry);
                List<CaxME.DimensionData> listDimensionData = new List<CaxME.DimensionData>();
                for (int i = 0; i < SheetCount; i++)
                {
                    //打開Sheet並記錄所有OBJ
                    NXOpen.Drawings.DrawingSheet CurrentSheet = (NXOpen.Drawings.DrawingSheet)NXObjectManager.Get(SheetTagAry[i]);
                    CurrentSheet.Open();
                    CurrentSheet.View.UpdateDisplay();
                    DisplayableObject[] SheetObj = CurrentSheet.View.AskVisibleObjects();
                    status = CaxME.RecordFixDimension(SheetObj, sWorkPartAttribute, ref listDimensionData);
                    if (!status)
                    {
                        this.Close();
                        return;
                    }
                }

                //將圖片存到本機Globaltek內
                for (int i = 0; i < PicNameStr.Length; i++)
                {
                    string destFileName = string.Format(@"{0}\{1}", PhotoFolderPath, PicNameStr[i]);
                    File.Copy(PicPathStr[i], destFileName, true);
                }


                //由料號查peSrNo
                Com_PEMain comPEMain = new Com_PEMain();
                status = Function.GetCom_PEMain(splitFullPath, out comPEMain);
                if (!status)
                {
                    this.Close();
                    return;
                }

                //由peSrNo和Op查partOperationSrNo
                Com_PartOperation comPartOperation = new Com_PartOperation();
                status = Function.GetCom_PartOperation(op1, comPEMain, out comPartOperation);
                if (!status)
                {
                    this.Close();
                    return;
                }

                #region 比對資料庫FixInspection是否有同筆數據
                bool Is_Exit = true;
                Com_FixInspection comFixInspection = new Com_FixInspection();
                comFixInspection = session.QueryOver<Com_FixInspection>()
                    .Where(x => x.comPartOperation == comPartOperation)
                    //.And(x => x.fixinsDescription == Desc.Text)
                    //.And(x => x.fixinsERP == ERPNo.Text)
                    //.And(x => x.fixinsNo == FixInsNo.Text)
                    .And(x => x.fixPartName == Path.GetFileNameWithoutExtension(workPart.FullPath)).SingleOrDefault();
                if (comFixInspection == null)
                {
                    Is_Exit = false;
                }

                if (Is_Exit)
                {
                    if (eTaskDialogResult.Yes == CaxPublic.ShowMsgYesNo("此檢、治具已存在上一筆資料，是否更新?"))
                    {
                        #region 刪除Com_FixDimension
                        IList<Com_FixDimension> listComFixDimension = session.QueryOver<Com_FixDimension>().Where(x => x.comFixInspection == comFixInspection).List();
                        using (ITransaction trans = session.BeginTransaction())
                        {
                            foreach (Com_FixDimension i in listComFixDimension)
                            {
                                session.Delete(i);
                            }
                            trans.Commit();
                        }
                        #endregion
                        #region 刪除Com_FixInspection
                        using (ITransaction trans = session.BeginTransaction())
                        {
                            session.Delete(comFixInspection);
                            trans.Commit();
                        }
                        #endregion
                        Is_Exit = false;
                    }
                }

                if (!Is_Exit)
                {
                    comFixInspection = new Com_FixInspection();
                    comFixInspection.comPartOperation = comPartOperation;
                    comFixInspection.fixinsDescription = Desc.Text;
                    comFixInspection.fixinsERP = ERPNo.Text;
                    comFixInspection.fixinsNo = FixInsNo.Text;
                    comFixInspection.fixPicPath = S_PicPath;
                    comFixInspection.fixPartName = Path.GetFileNameWithoutExtension(workPart.FullPath);

                    IList<Com_FixDimension> listCom_FixDimension = new List<Com_FixDimension>();
                    foreach (CaxME.DimensionData i in listDimensionData)
                    {
                        Com_FixDimension cCom_FixDimension = new Com_FixDimension();
                        cCom_FixDimension.comFixInspection = comFixInspection;

                        CaxME.MappingData(i, ref cCom_FixDimension);
                        listCom_FixDimension.Add(cCom_FixDimension);
                    }
                    comFixInspection.comFixDimension = listCom_FixDimension;

                    using (ITransaction trans = session.BeginTransaction())
                    {
                        session.Save(comFixInspection);
                        trans.Commit();
                    }

                    //傳OIS圖到SERVER
                    
                    if (!Directory.Exists(S_Folder))
                    {
                        System.IO.Directory.CreateDirectory(S_Folder);
                    }
                    CaxPublic.DirectoryCopy(PhotoFolderPath, S_Folder, true);
                }
                
                #endregion

                MessageBox.Show("上傳完成");
            }
            catch (System.Exception ex)
            {
                MessageBox.Show(ex.ToString());
            }
        }


    }
}
