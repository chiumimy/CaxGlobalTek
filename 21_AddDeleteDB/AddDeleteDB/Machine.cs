using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using NHibernate;
using CaxGlobaltek;

namespace AddDeleteDB
{
    public class Machine
    {
        private static ISession session = MyHibernateHelper.SessionFactory.OpenSession();
        public static bool GetMachineData(out Dictionary<string, List<Form1.MachineNoData>> DicMachine)
        {
            DicMachine = new Dictionary<string, List<Form1.MachineNoData>>();
            try
            {
                IList<Sys_MachineType> sysMachineType = session.QueryOver<Sys_MachineType>().List<Sys_MachineType>();
                foreach (Sys_MachineType i in sysMachineType)
                {
                    List<Form1.MachineNoData> ListMachineNoData = new List<Form1.MachineNoData>();
                    IList<Sys_MachineNo> sysMachineNo = session.QueryOver<Sys_MachineNo>()
                                                                    .Where(x => x.sysMachineType.machineTypeSrNo == i.machineTypeSrNo)
                                                                    .List<Sys_MachineNo>();
                    foreach (Sys_MachineNo j in sysMachineNo)
                    {
                        Form1.MachineNoData sMachineNoData = new Form1.MachineNoData();
                        sMachineNoData.MachineNo = j.machineNo;
                        sMachineNoData.MachineID = j.machineID;
                        sMachineNoData.MachineName = j.machineName;
                        ListMachineNoData.Add(sMachineNoData);
                    }
                    DicMachine.Add(i.machineType, ListMachineNoData);
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
