using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using Iesi.Collections.Generic;

namespace CaxGlobaltek
{
    public class Sys_License
    {
        public virtual Int32 licenseSrNo { get; set; }
        public virtual string expireTime { get; set; }
        public virtual string password { get; set; }
    }

    public class Sys_PFM
    {
        public virtual Int32 pFMSrNo { get; set; }
        public virtual string pFMName { get; set; }
    }

    public class Sys_PEoF
    {
        public virtual Int32 pEoFSrNo { get; set; }
        public virtual string pEoFName { get; set; }
    }

    public class Sys_PCoF
    {
        public virtual Int32 pCoFSrNo { get; set; }
        public virtual string pCoFName { get; set; }
    }

    public class Sys_Prevention
    {
        public virtual Int32 preventionSrNo { get; set; }
        public virtual string preventionName { get; set; }
    }

    public class Sys_Detection
    {
        public virtual Int32 detectionSrNo { get; set; }
        public virtual string detectionName { get; set; }
    }

    public class Sys_Customer
    {
        public virtual Int32 customerSrNo { get; set; }
        public virtual string customerName { get; set; }
        public virtual IList<Com_PEMain> comPEMain { get; set; }
    }

    public class Sys_Operation2
    {
        public virtual Int32 operation2SrNo { get; set; }
        public virtual string operation2Name { get; set; }
        public virtual string category { get; set; }
        public virtual IList<Com_PartOperation> comPartOperation { get; set; }
    }

    public class Com_PEMain
    {
        public virtual Int32 peSrNo { get; set; }
        public virtual string partName { get; set; }
        public virtual string partDes { get; set; }
        public virtual string partMaterial { get; set; }
        public virtual string customerVer { get; set; }
        public virtual string opVer { get; set; }
        public virtual string materialSource { get; set; }
        public virtual string eRPStd { get; set; }
        public virtual string partFilePath { get; set; }
        public virtual string billetFilePath { get; set; }
        public virtual string createDate { get; set; }
        public virtual Sys_Customer sysCustomer { get; set; }
        public virtual IList<Com_PartOperation> comPartOperation { get; set; }
    }

    public class Com_PartOperation
    {
        public virtual Int32 partOperationSrNo { get; set; }
        public virtual string operation1 { get; set; }
        public virtual string form { get; set; }
        public virtual string erpCode { get; set; }
        public virtual Com_PEMain comPEMain { get; set; }
        public virtual Sys_Operation2 sysOperation2 { get; set; }
        public virtual IList<Com_MEMain> comMEMain { get; set; }
        public virtual IList<Com_TEMain> comTEMain { get; set; }
    }
}
