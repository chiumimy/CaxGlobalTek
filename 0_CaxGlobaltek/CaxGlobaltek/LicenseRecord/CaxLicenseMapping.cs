using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace CaxGlobaltek
{
    public class Sys_ACLicense
    {
        public virtual Int32 AClicenseSrNo { get; set; }
        public virtual string department { get; set; }
        public virtual string userChinese { get; set; }
        public virtual string username { get; set; }
        public virtual string login { get; set; }
        public virtual string logout { get; set; }
        public virtual string workingtime { get; set; }
        public virtual string totaltime { get; set; }
        public virtual string date { get; set; }
    }

    public class Sys_AVMLicense
    {
        public virtual Int32 AVMlicenseSrNo { get; set; }
        public virtual string department { get; set; }
        public virtual string userChinese { get; set; }
        public virtual string username { get; set; }
        public virtual string login { get; set; }
        public virtual string logout { get; set; }
        public virtual string workingtime { get; set; }
        public virtual string totaltime { get; set; }
        public virtual string date { get; set; }
    }

    public class Sys_NX9License
    {
        public virtual Int32 NX9licenseSrNo { get; set; }
        public virtual string department { get; set; }
        public virtual string userChinese { get; set; }
        public virtual string username { get; set; }
        public virtual string login { get; set; }
        public virtual string logout { get; set; }
        public virtual string workingtime { get; set; }
        public virtual string totaltime { get; set; }
        public virtual string date { get; set; }
    }

    public class Sys_User
    {
        public virtual Int32 userSrNo { get; set; }
        public virtual string department { get; set; }
        public virtual string userChinese { get; set; }
        public virtual string userEnglish { get; set; }
    }
    
}
