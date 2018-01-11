using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace CaxGlobaltek
{
    public class Sys_TipTop
    {
        public virtual Int32 tipTopSrNo { get; set; }
        public virtual string productNo { get; set; }
        public virtual string stepNo { get; set; }
        public virtual string partNo { get; set; }
        public virtual string ois { get; set; }
        public virtual string erpNo { get; set; }
        public virtual string toolNo { get; set; }
        public virtual string usedCount { get; set; }
        public virtual string toolLife { get; set; }
        public virtual string toolChangeTime { get; set; }
        public virtual string toolSpec { get; set; }
        public virtual string toolUGSpec { get; set; }
    }
}
