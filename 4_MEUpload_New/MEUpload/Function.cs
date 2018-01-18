using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using NXOpen;
using NXOpen.UF;
using System.IO;
using CaxGlobaltek;
using System.Windows.Forms;
using MEUpload.DatabaseClass;

namespace MEUpload
{
    public class Function
    {
        public static Session theSession = Session.GetSession();
        public static UI theUI;
        public static UFSession theUfSession = UFSession.GetUFSession();
        public static bool status;

        public struct PartDirData
        {
            public string PartLocalDir { get; set; }
            public string PartServer1Dir { get; set; }
            //public string PartServer2Dir { get; set; }
        }

        public struct WorkPartAttribute
        {
            public string meExcelType { get; set; }
            public string draftingVer { get; set; }
            public string draftingDate { get; set; }
            public string partDescription { get; set; }
            public string material { get; set; }
            public string createDate { get; set; }
        }

        public struct ObjectAttribute
        {
            public string singleObjExcel { get; set; }
            public string singleSelfCheckExcel { get; set; }
            public string keyChara { get; set; }
            public string productName { get; set; }
            public string customerBalloon { get; set; }
            public string spcControl { get; set; }
        }

        public static bool GetComponentPath(Part displayPart, METE_Download_Upload_Path cMETE_Download_Upload_Path, ListView listView1, ref Dictionary<string, PartDirData> DicPartDirData)
        {
            try
            {
                PartDirData sPartDirData = new PartDirData();
                sPartDirData.PartLocalDir = displayPart.FullPath;
                sPartDirData.PartServer1Dir = string.Format(@"{0}\{1}", cMETE_Download_Upload_Path.Server_ShareStr, Path.GetFileName(displayPart.FullPath));
                //sPartDirData.PartServer2Dir = string.Format(@"{0}\{1}", cMETE_Download_Upload_Path.Server_ShareStr, "OP" + PartInfo.OIS);
                DicPartDirData.Add(Path.GetFileNameWithoutExtension(displayPart.FullPath), sPartDirData);
                listView1.Items.Add(Path.GetFileName(displayPart.FullPath));

                NXOpen.Assemblies.ComponentAssembly casm = displayPart.ComponentAssembly;
                List<NXOpen.Assemblies.Component> ListChildrenComp = new List<NXOpen.Assemblies.Component>();
                CaxAsm.GetCompChildren(casm.RootComponent, ref ListChildrenComp);
                foreach (NXOpen.Assemblies.Component i in ListChildrenComp)
                {
                    string ServerPartPath = string.Format(@"{0}\{1}", cMETE_Download_Upload_Path.Server_ShareStr, i.DisplayName + ".prt");

                    if (!DicPartDirData.TryGetValue(i.DisplayName, out sPartDirData))
                    {
                        sPartDirData = sPartDirData = new PartDirData();
                        if (!File.Exists(string.Format(@"{0}\{1}", Path.GetDirectoryName(displayPart.FullPath), i.DisplayName + ".prt")))
                        {
                            MessageBox.Show("零件：" + i.DisplayName + "找不到，請檢察本機資料夾是否存在");
                            return false;
                        }
                        sPartDirData.PartLocalDir = string.Format(@"{0}\{1}", Path.GetDirectoryName(displayPart.FullPath), i.DisplayName + ".prt");
                        sPartDirData.PartServer1Dir = ServerPartPath;
                        DicPartDirData.Add(i.DisplayName, sPartDirData);
                        listView1.Items.Add(i.DisplayName + ".prt");
                    }
                    //sPartDirData.PartServer2Dir = string.Format(@"{0}\{1}", cMETE_Download_Upload_Path.Server_ShareStr, "OP" + PartInfo.OIS);
                }
            }
            catch (System.Exception ex)
            {
                return false;
            }
            return true;
        }
        public static bool UploadPart(Dictionary<string, PartDirData> DicPartDirData, out List<string> ListPartName)
        {
            ListPartName = new List<string>();
            try
            {
                foreach (KeyValuePair<string, Function.PartDirData> kvp in DicPartDirData)
                {
                    //判斷Part是否存在
                    if (!File.Exists(kvp.Value.PartLocalDir))
                    {
                        MessageBox.Show(Path.GetFileName(kvp.Value.PartLocalDir) + "不存在，無法上傳");
                        return false;
                    }
                    try
                    {
                        File.Copy(kvp.Value.PartLocalDir, kvp.Value.PartServer1Dir, true);
                    }
                    catch (System.Exception ex)
                    {
                        MessageBox.Show(ex.ToString());
                    }

                    ListPartName.Add(kvp.Key + ".prt");
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
                try { sWorkPartAttribute.meExcelType = workPart.GetStringAttribute("EXCELTYPE"); }
                catch (System.Exception ex) { sWorkPartAttribute.meExcelType = ""; }

                try
                {
                    sWorkPartAttribute.draftingVer = workPart.GetStringAttribute("REVSTARTPOS");
                    sWorkPartAttribute.draftingVer = sWorkPartAttribute.draftingVer.Split(',')[0];
                }
                catch (System.Exception ex) { sWorkPartAttribute.draftingVer = ""; }

                try { sWorkPartAttribute.partDescription = workPart.GetStringAttribute("PARTDESCRIPTIONPOS"); }
                catch (System.Exception ex) { sWorkPartAttribute.partDescription = ""; }

                try { sWorkPartAttribute.draftingDate = workPart.GetStringAttribute("REVDATESTARTPOS"); }
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
        public static bool GetObjectAttribute(DisplayableObject singleObj, out ObjectAttribute sObjectAttribute)
        {
            sObjectAttribute = new ObjectAttribute();
            try
            {
                //取得Non-SelfCheck的Excel屬性
                try { sObjectAttribute.singleObjExcel = singleObj.GetStringAttribute(CaxME.DimenAttr.AssignExcelType); }
                catch (System.Exception ex) { sObjectAttribute.singleObjExcel = ""; }

                //取得SelfCheck的Excel屬性
                try { sObjectAttribute.singleSelfCheckExcel = singleObj.GetStringAttribute(CaxME.DimenAttr.SelfCheckExcel); }
                catch (System.Exception ex) { sObjectAttribute.singleSelfCheckExcel = ""; }

                //取得keyChara、productName、customerBalloon、SPCControl
                try { sObjectAttribute.keyChara = singleObj.GetStringAttribute(CaxME.DimenAttr.KC); }
                catch (System.Exception ex) { sObjectAttribute.keyChara = ""; }

                try { sObjectAttribute.productName = singleObj.GetStringAttribute(CaxME.DimenAttr.Product); }
                catch (System.Exception ex) { sObjectAttribute.productName = ""; }

                try { sObjectAttribute.customerBalloon = singleObj.GetStringAttribute(CaxME.DimenAttr.CustomerBalloon); }
                catch (System.Exception ex) { sObjectAttribute.customerBalloon = ""; }

                try { sObjectAttribute.spcControl = singleObj.GetStringAttribute(CaxME.DimenAttr.SpcControl); }
                catch (System.Exception ex) { sObjectAttribute.spcControl = ""; }
            }
            catch (System.Exception ex)
            {
                return false;
            }
            return true;
        }
        public static bool RecordDimension(DisplayableObject[] SheetObj, WorkPartAttribute sWorkPartAttribute, ref List<DimensionData> listDimensionData)
        {
            try
            {
                foreach (DisplayableObject singleObj in SheetObj)
                {
                    //取得尺寸的屬性
                    ObjectAttribute sObjectAttribute = new ObjectAttribute();
                    status = Function.GetObjectAttribute(singleObj, out sObjectAttribute);
                    if (!status)
                    {
                        MessageBox.Show("singleObj屬性取得失敗，無法上傳");
                        return false;
                    }

                    if (sObjectAttribute.singleObjExcel == "" & sObjectAttribute.singleSelfCheckExcel == "")
                    {
                        continue;
                    }

                    //如果有Non-SelfCheck，則記錄一筆資料
                    if (sObjectAttribute.singleObjExcel != "")
                    {
                        string[] splitExcel = sObjectAttribute.singleObjExcel.Split(',');
                        foreach (string dimenExcelType in splitExcel)
                        {
                            DimensionData cDimensionData = new DimensionData();
                            status = Database.GetDimensionData(dimenExcelType, singleObj, out cDimensionData);
                            if (!status)
                            {
                                continue;
                            }
                            cDimensionData.draftingVer = sWorkPartAttribute.draftingVer;
                            cDimensionData.draftingDate = sWorkPartAttribute.draftingDate;
                            cDimensionData.keyChara = sObjectAttribute.keyChara;
                            cDimensionData.productName = sObjectAttribute.productName;
                            cDimensionData.excelType = dimenExcelType;
                            cDimensionData.spcControl = sObjectAttribute.spcControl;
                            if (sObjectAttribute.customerBalloon != "")
                            {
                                cDimensionData.customerBalloon = Convert.ToInt32(sObjectAttribute.customerBalloon);
                            }
                            listDimensionData.Add(cDimensionData);
                        }
                    }
                    //如果有SelfCheck，則記錄一筆資料
                    if (sObjectAttribute.singleSelfCheckExcel != "")
                    {
                        DimensionData cDimensionData = new DimensionData();
                        status = Database.GetDimensionData(sObjectAttribute.singleSelfCheckExcel, singleObj, out cDimensionData);
                        if (!status)
                        {
                            continue;
                        }
                        cDimensionData.draftingVer = sWorkPartAttribute.draftingVer;
                        cDimensionData.draftingDate = sWorkPartAttribute.draftingDate;
                        cDimensionData.keyChara = sObjectAttribute.keyChara;
                        cDimensionData.productName = sObjectAttribute.productName;
                        cDimensionData.excelType = sObjectAttribute.singleSelfCheckExcel;
                        cDimensionData.spcControl = sObjectAttribute.spcControl;
                        if (sObjectAttribute.customerBalloon != "")
                        {
                            cDimensionData.customerBalloon = Convert.ToInt32(sObjectAttribute.customerBalloon);
                        }
                        listDimensionData.Add(cDimensionData);
                    }
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
