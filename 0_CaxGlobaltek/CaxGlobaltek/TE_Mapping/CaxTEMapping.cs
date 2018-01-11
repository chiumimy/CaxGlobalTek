using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace CaxGlobaltek
{
    public class Sys_TEExcel
    {
        public virtual Int32 teExcelSrNo { get; set; }
        public virtual string teExcelType { get; set; }
        public virtual IList<Com_MEMain> comTEMain { get; set; }
    }

    public class Com_TEMain
    {
        public virtual Int32 teSrNo { get; set; }
        public virtual Com_PartOperation comPartOperation { get; set; }
        public virtual Sys_TEExcel sysTEExcel { get; set; }
        public virtual IList<Com_ShopDoc> comShopDoc { get; set; }
        public virtual IList<Com_ControlDimen> comControlDimen { get; set; }
        public virtual IList<Com_ToolList> comToolList { get; set; }
        public virtual string ncGroupName { get; set; }
        public virtual string totalCuttingTime { get; set; }
        public virtual string fixtureImgPath { get; set; }
        public virtual string machineNo { get; set; }
        public virtual string designed { get; set; }
        public virtual string reviewed { get; set; }
        public virtual string approved { get; set; }
        public virtual string createDate { get; set; }
    }

    public class Com_ControlDimen
    {
        public virtual Com_TEMain comTEMain { get; set; }
        public virtual Int32 controlDimenSrNo { get; set; }
        public virtual string toolNo { get; set; }
        public virtual Int32 controlBallon { get; set; }
        public virtual string controlDimen { get; set; }
    }

    public class Com_ShopDoc
    {
        public virtual Int32 shopDocSrNo { get; set; }
        public virtual Com_TEMain comTEMain { get; set; }
        public virtual string toolNo { get; set; }
        public virtual string toolID { get; set; }
        public virtual string operationName { get; set; }
        public virtual string holderID { get; set; }
        public virtual string feed { get; set; }
        public virtual string speed { get; set; }
        public virtual string machiningtime { get; set; }
        public virtual string opImagePath { get; set; }
        public virtual string partStock { get; set; }
        public virtual string cutterLife { get; set; }
        public virtual string extension { get; set; }
    }

    public class Com_ToolList
    {
        public virtual Int32 toolListSrNo { get; set; }
        public virtual Com_TEMain comTEMain { get; set; }
        public virtual string toolNumber { get; set; }
        public virtual string erpNumber { get; set; }
        public virtual string cutterQty { get; set; }
        public virtual string cutterLife { get; set; }
        public virtual string fluteQty { get; set; }
        public virtual string title { get; set; }
        public virtual string specification { get; set; }
        public virtual string note { get; set; }
        public virtual string accessory { get; set; }
    }

    public class Sys_MachineType
    {
        public virtual Int32 machineTypeSrNo { get; set; }
        public virtual IList<Sys_MachineNo> sysMachineNo { get; set; }
        public virtual string machineType { get; set; }
    }

    public class Sys_MachineNo
    {
        public virtual Int32 machineNoSrNo { get; set; }
        public virtual Sys_MachineType sysMachineType { get; set; }
        public virtual string machineNo { get; set; }
        public virtual string machineName { get; set; }
        public virtual string machineID { get; set; }
        public virtual string postprocessor { get; set; }
    }
}
