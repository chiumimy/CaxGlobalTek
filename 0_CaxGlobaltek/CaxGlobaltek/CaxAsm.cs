using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using NXOpen;
using NXOpen.UF;
using System.Collections;
using System.IO;
using NXOpen.Utilities;
using System.Windows.Forms;
using NXOpen.Assemblies;

namespace CaxGlobaltek
{
    public class CaxAsm
    {
        private static UFSession theUfSession = UFSession.GetUFSession();
        private static Session theSession = Session.GetSession();

        public static int DisplayPartIsAsm(out Tag tagRootPart)
        {
            tagRootPart = NXOpen.Tag.Null;

            try
            {
                Part displayPart = theSession.Parts.Display;
                if (displayPart == null)
                {
                    return -1;
                }
                tagRootPart = theUfSession.Assem.AskRootPartOcc(displayPart.Tag);
                if (tagRootPart == NXOpen.Tag.Null)
                {
                    return -2;
                }

            }
            catch (System.Exception ex)
            {
                return -1001;
            }

            return 0;
        }

        public static bool CreateNewAsm(string newAsmName, string TemplateFileName = "assembly-mm-template.prt")
        {
            try
            {
                Session theSession = Session.GetSession();
                // ----------------------------------------------
                //   Menu: File->New...
                // ----------------------------------------------

                FileNew fileNew1;
                fileNew1 = theSession.Parts.FileNew();
                fileNew1.TemplateFileName = TemplateFileName;
                fileNew1.Application = FileNewApplication.Assemblies;
                fileNew1.Units = NXOpen.Part.Units.Millimeters;
                //fileNew1.TemplateType = FileNewTemplateType.Item;

                //fileNew1.NewFileName = "D:\\NX_part\\cim0001-case2\\work_cnc3\\assembly1.prt";
                fileNew1.NewFileName = newAsmName;
                fileNew1.MasterFileName = "";
                fileNew1.UseBlankTemplate = false;
                fileNew1.MakeDisplayedPart = true;

                NXObject nXObject1;
                nXObject1 = fileNew1.Commit();

                Part workPart = theSession.Parts.Work;
                Part displayPart = theSession.Parts.Display;
                fileNew1.Destroy();
            }
            catch (System.Exception ex)
            {
                MessageBox.Show(ex.Message);
                return false;
            }

            return true;
        }

        public static bool CreateNewEmptyComp(string comp_path, out NXOpen.Assemblies.Component component, string TemplateFileName = "model-plain-1-mm-template.prt", string ReferenceSetName = "Entire Part")
        {
            try
            {
                Session theSession = Session.GetSession();
                Part workPart = theSession.Parts.Work;
                Part displayPart = theSession.Parts.Display;
                // ----------------------------------------------
                //   Menu: Assemblies->Components->Create New Component...
                // ----------------------------------------------
                FileNew fileNew1;
                fileNew1 = theSession.Parts.FileNew();
                fileNew1.TemplateFileName = TemplateFileName;
                fileNew1.Application = FileNewApplication.Modeling;
                fileNew1.Units = NXOpen.Part.Units.Millimeters;
                //fileNew1.TemplateType = FileNewTemplateType.Item;
                fileNew1.NewFileName = comp_path;
                fileNew1.MasterFileName = "";
                fileNew1.UseBlankTemplate = false;
                fileNew1.MakeDisplayedPart = false;
                string newNameNoExt = "";
                newNameNoExt = Path.GetFileNameWithoutExtension(comp_path);
                string newNameNoExtUpper = "";
                newNameNoExtUpper = newNameNoExt.ToUpper();

                NXOpen.Assemblies.CreateNewComponentBuilder createNewComponentBuilder1;
                
                createNewComponentBuilder1 = workPart.AssemblyManager.CreateNewComponentBuilder();
                
                createNewComponentBuilder1.NewComponentName = newNameNoExtUpper;
                
                createNewComponentBuilder1.ReferenceSet = NXOpen.Assemblies.CreateNewComponentBuilder.ComponentReferenceSetType.EntirePartOnly;
                
                createNewComponentBuilder1.ReferenceSetName = ReferenceSetName;
                
                // ----------------------------------------------
                //   Dialog Begin Create New Component
                // ----------------------------------------------
                createNewComponentBuilder1.ReferenceSet = NXOpen.Assemblies.CreateNewComponentBuilder.ComponentReferenceSetType.Model;
                createNewComponentBuilder1.NewFile = fileNew1;
                NXObject nXObject1;
                nXObject1 = createNewComponentBuilder1.Commit();

                component = (NXOpen.Assemblies.Component)nXObject1;

                //UI.GetUI().NXMessageBox.Show("Message", NXMessageBox.DialogType.Information, component.Name);


                createNewComponentBuilder1.Destroy();
            }
            catch (System.Exception ex)
            {
                CaxLog.ShowListingWindow(ex.ToString());
                component = null;
                return false;
            }

            return true;
        }

        public static bool CreateNewEmptyCompNoReturn(string comp_path, string TemplateFileName = "model-plain-1-mm-template.prt", string ReferenceSetName = "Entire Part")
        {
            try
            {
                Session theSession = Session.GetSession();
                Part workPart = theSession.Parts.Work;
                Part displayPart = theSession.Parts.Display;
                // ----------------------------------------------
                //   Menu: Assemblies->Components->Create New Component...
                // ----------------------------------------------
                FileNew fileNew1;
                fileNew1 = theSession.Parts.FileNew();
                fileNew1.TemplateFileName = TemplateFileName;
                fileNew1.Application = FileNewApplication.Modeling;
                //fileNew1.Application = FileNewApplication.Gateway;
                fileNew1.Units = NXOpen.Part.Units.Millimeters;
                //fileNew1.TemplateType = FileNewTemplateType.Item;
                //string sss = Path.GetDirectoryName(comp_path) + Path.GetFileName(comp_path);

                //string sss = comp_path.Replace(@"\\", @"\");
                fileNew1.NewFileName = comp_path;
                fileNew1.MasterFileName = "";
                fileNew1.UseBlankTemplate = false;
                fileNew1.MakeDisplayedPart = false;
                //string newNameNoExt = "";
                //newNameNoExt = Path.GetFileNameWithoutExtension(comp_path);
                //string newNameNoExtUpper = "";
                //newNameNoExtUpper = newNameNoExt.ToUpper();

                NXOpen.Assemblies.CreateNewComponentBuilder createNewComponentBuilder1;

                createNewComponentBuilder1 = workPart.AssemblyManager.CreateNewComponentBuilder();

                //createNewComponentBuilder1.NewComponentName = newNameNoExtUpper;

                createNewComponentBuilder1.ReferenceSet = NXOpen.Assemblies.CreateNewComponentBuilder.ComponentReferenceSetType.EntirePartOnly;

                createNewComponentBuilder1.ReferenceSetName = ReferenceSetName;

                // ----------------------------------------------
                //   Dialog Begin Create New Component
                // ----------------------------------------------
                createNewComponentBuilder1.ReferenceSet = NXOpen.Assemblies.CreateNewComponentBuilder.ComponentReferenceSetType.Model;
                //createNewComponentBuilder1.ReferenceSet = NXOpen.Assemblies.CreateNewComponentBuilder.ComponentReferenceSetType.EntirePartOnly;
                createNewComponentBuilder1.NewFile = fileNew1;
                NXObject nXObject1;
                nXObject1 = createNewComponentBuilder1.Commit();
                CaxPart.Save();
                //UI.GetUI().NXMessageBox.Show("Message", NXMessageBox.DialogType.Information, component.Name);
                

                createNewComponentBuilder1.Destroy();
            }
            catch (System.Exception ex)
            {
                CaxLog.ShowListingWindow(ex.ToString());
                return false;
            }

            return true;
        }

        /// <summary>
        /// 取得子Comp集合(只找下一層)
        /// </summary>
        /// <param name="compAry"></param>
        /// <param name="comp"></param>
        /// <returns></returns>
        public static bool GetCompChildren(out List<NXOpen.Assemblies.Component> compAry, Component comp = null)
        {
            compAry = new List<NXOpen.Assemblies.Component>();
            try
            {
                CaxAsm.SetWorkComponent(comp);
                Part rootPart = theSession.Parts.Work;
                NXOpen.Assemblies.ComponentAssembly casm = rootPart.ComponentAssembly;
                NXOpen.Assemblies.Component[] tempCompAry = casm.RootComponent.GetChildren();
                compAry.AddRange(tempCompAry.ToList());
            }
            catch (System.Exception ex)
            {
                //compAry = null;
                return false;
            }
            return true;
        }

        public static bool SetWorkComponent(NXOpen.Assemblies.Component component)
        {
            try
            {
                NXOpen.Assemblies.Component component1 = component;
                PartLoadStatus partLoadStatus1;
                theSession.Parts.SetWorkComponent(component1, out partLoadStatus1);
                partLoadStatus1.Dispose();
            }
            catch (System.Exception ex)
            {
                return false;
            }

            return true;
        }

        public static bool AddComponentToAsmByDefault(string componentPath, out NXOpen.Assemblies.Component newComponent, string referenceSetName = "MODEL", int layer = -1, bool uomAsNgc = true)
        {
            newComponent = null;
            try
            {
                string componentName = Path.GetFileNameWithoutExtension(componentPath);

                /*
                Session theSession = Session.GetSession();
                Part workPart = theSession.Parts.Work;

                string componentName = Path.GetFileNameWithoutExtension(componentPath);
                //string componentName = Path.GetFileName(componentPath);

                BasePart basePart1;
                PartLoadStatus partLoadStatus1;
                basePart1 = theSession.Parts.OpenBase(componentPath, out partLoadStatus1);
                partLoadStatus1.Dispose();

                //組裝設計檔
                Point3d basePoint1 = new Point3d(0.0, 0.0, 0.0);
                Matrix3x3 orientation1;
                orientation1.Xx = 1.0;
                orientation1.Xy = 0.0;
                orientation1.Xz = 0.0;
                orientation1.Yx = 0.0;
                orientation1.Yy = 1.0;
                orientation1.Yz = 0.0;
                orientation1.Zx = 0.0;
                orientation1.Zy = 0.0;
                orientation1.Zz = 1.0;
                PartLoadStatus partLoadStatus2;
                //NXOpen.Assemblies.Component componentDesign;
                newComponent = workPart.ComponentAssembly.AddComponent(componentPath, referenceSetName, componentName, basePoint1, orientation1, layer, out partLoadStatus2, uomAsNgc);
                partLoadStatus2.Dispose();
                */


                Session theSession = Session.GetSession();
                Part workPart = theSession.Parts.Work;
                Part displayPart = theSession.Parts.Display;
                // ----------------------------------------------
                //   Menu: Assemblies->Components->Add Component...
                // ----------------------------------------------
                NXOpen.Session.UndoMarkId markId1;
                markId1 = theSession.SetUndoMark(NXOpen.Session.MarkVisibility.Visible, "Add Component");

                NXOpen.Session.UndoMarkId markId2;
                markId2 = theSession.SetUndoMark(NXOpen.Session.MarkVisibility.Invisible, "Start");

                theSession.SetUndoMarkName(markId2, "Add Component Dialog");
                // 20150610 Stewart 重複組裝同一個零件時，OpenBase會錯
                try
                {
                    PartLoadStatus partLoadStatus1;
                    BasePart basePart1;
                    basePart1 = theSession.Parts.OpenBase(componentPath, out partLoadStatus1);
                    partLoadStatus1.Dispose();
                }
                catch (System.Exception ex)
                {
                }
                // 20150610
                int nErrs1;
                nErrs1 = theSession.UpdateManager.DoUpdate(markId1);

                NXOpen.Session.UndoMarkId markId3;
                markId3 = theSession.SetUndoMark(NXOpen.Session.MarkVisibility.Invisible, "Add Component");

                theSession.DeleteUndoMark(markId3, null);

                NXOpen.Session.UndoMarkId markId4;
                markId4 = theSession.SetUndoMark(NXOpen.Session.MarkVisibility.Invisible, "Add Component");

                Point3d basePoint1 = new Point3d(0.0, 0.0, 0.0);
                Matrix3x3 orientation1;
                orientation1.Xx = 1.0;
                orientation1.Xy = 0.0;
                orientation1.Xz = 0.0;
                orientation1.Yx = 0.0;
                orientation1.Yy = 1.0;
                orientation1.Yz = 0.0;
                orientation1.Zx = 0.0;
                orientation1.Zy = 0.0;
                orientation1.Zz = 1.0;
                PartLoadStatus partLoadStatus2;
                //NXOpen.Assemblies.Component component1;

                newComponent = workPart.ComponentAssembly.AddComponent(componentPath, referenceSetName, componentName, basePoint1, orientation1, -1, out partLoadStatus2, true);

                partLoadStatus2.Dispose();
                NXObject[] objects1 = new NXObject[0];
                int nErrs2;
                nErrs2 = theSession.UpdateManager.AddToDeleteList(objects1);

                theSession.DeleteUndoMark(markId4, null);

                theSession.SetUndoMarkName(markId2, "Add Component");

                theSession.DeleteUndoMark(markId2, null);


            }
            catch (System.Exception ex)
            {
                //CaxLog.ShowListingWindow(ex.Message);
                return false;
            }

            return true;
        }

        /// <summary>
        /// 取得子Comp集合(IsCycle=true 遞迴; IsCycle=false 不遞迴，預設為true)
        /// </summary>
        /// <param name="FatherComp"></param>
        /// <param name="ListChildrenComp"></param>
        /// <returns></returns>
        public static bool GetCompChildren(NXOpen.Assemblies.Component FatherComp, ref List<NXOpen.Assemblies.Component> ListChildrenComp, bool IsCycle = true)
        {
            try
            {
                NXOpen.Assemblies.Component[] ChildrenCompAry = FatherComp.GetChildren();
                ListChildrenComp.AddRange(ChildrenCompAry.ToArray());
                if (IsCycle)
                {
                    foreach (NXOpen.Assemblies.Component i in ChildrenCompAry)
                    {
                        GetCompChildren(i, ref ListChildrenComp, IsCycle);
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
        /// 以另存檔案的方式修改Part名稱，可指定是否刪除原檔案，預設為刪除
        /// </summary>
        /// <param name="Part"></param>
        /// <param name="NewPartName"></param>
        /// <param name="DeleteOldPart"></param>
        /// <returns></returns>
        public static bool RenamePart(NXOpen.Part part, string NewPartName, bool DeleteOldPart = true)
        {
            try
            {
                Part currentworkpart = theSession.Parts.Work;

                theSession.Parts.SetWork(part);

                string oldPartPath = part.FullPath;
                string newPartPath = string.Format("{0}\\{1}.prt", Path.GetDirectoryName(oldPartPath), NewPartName);
                if (File.Exists(newPartPath))
                {
                    File.Delete(newPartPath);
                }
                //另存成新的檔案架構名稱
                PartSaveStatus partSaveStatus1;
                partSaveStatus1 = part.SaveAs(Path.GetDirectoryName(newPartPath) + "\\" + Path.GetFileNameWithoutExtension(newPartPath));
                partSaveStatus1.Dispose();
                //刪除舊有檔案
                if (DeleteOldPart)
                {
                    if (File.Exists(oldPartPath))
                    {
                        File.Delete(oldPartPath);
                    }
                }

                theSession.Parts.SetWork(currentworkpart);
            }
            catch (Exception ex)
            {
                CaxLog.ShowListingWindow(ex.Message);
                return false;
            }
            return true;
        }
    
    
    }
}
